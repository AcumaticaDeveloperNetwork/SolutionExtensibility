using PX.Data;
using PX.Objects.AP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class USSimpleInvoiceEntry : PXGraph<USSimpleInvoiceEntry, USSimpleInvoice>
    {
        #region Views
        public PXSetup<USSetup> Setup;

        public PXSelect<USSimpleInvoice> Document;

        public PXSelect<USSimpleInvoiceLine, Where<USSimpleInvoiceLine.refNbr, Equal<Current<USSimpleInvoice.refNbr>>>> Transactions;
        #endregion

        #region Buttons
        public PXAction<USSimpleInvoice> Process;

        [PXButton]
        [PXUIField(DisplayName = "Create Invoice")]
        protected virtual IEnumerable process(PXAdapter adapter)
        {
            USSimpleInvoice header = Document.Current;

            if (header != null)
            {
                //All long operations must be run as such
                PXLongOperation.StartOperation(this, () => ProcessMethod(header));
            }

            //All buttons must return adapter.Get();
            return adapter.Get();
        }
        #endregion

        #region Methods
        public static void ProcessMethod(USSimpleInvoice header)
        {
            try
            {
                //Only for accessing virtual methods
                //You should not use the instance of the Graph from the UI in long operations, because they will not be run asynchronously, and could hang-up/timeout
                APInvoiceEntry apInvoiceEntry = PXGraph.CreateInstance<APInvoiceEntry>();
                APInvoiceEntryExtension aPInvoiceEntryExtension = apInvoiceEntry.GetExtension<APInvoiceEntryExtension>();
                //Logic for creating the APInvoice here
                APTran apTran;

                APInvoice invoice = apInvoiceEntry.Document.Insert();
                invoice.DocDesc = header.Description;
                invoice.DocDate = header.Date;
                invoice.VendorID = aPInvoiceEntryExtension?.USSetup?.Current?.VendorID;
                invoice.InvoiceNbr = header.ScoutID.ToString();
                invoice = apInvoiceEntry.Document.Update(invoice);

                foreach (USSimpleInvoiceLine detail in PXSelectReadonly<USSimpleInvoiceLine,
                                                                            Where<USSimpleInvoiceLine.refNbr, Equal<Required<USSimpleInvoice.refNbr>>>>.Select(apInvoiceEntry, header.RefNbr))
                {
                    apTran = aPInvoiceEntryExtension.GenerateAPTran(detail);

                    apInvoiceEntry.Transactions.Insert(apTran);
                }
                apInvoiceEntry.Save.Press();
                header.InvoiceRef = apInvoiceEntry.Document.Current.RefNbr;
                header.Status = USSimpleInvoiceStatus.Created;
                aPInvoiceEntryExtension.USSimpleInvoices.Update(header);
                apInvoiceEntry.Save.Press();
            }
            catch (Exception e)
            {
                throw new PXException("An error occured while processing the record : " + e.Message);
            }
        }
        #endregion

        #region Events
        public virtual void USSimpleInvoice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
        {
            USSimpleInvoice row = e.Row as USSimpleInvoice;
            if (row != null)
            {
                bool enabled = (row.Status == USSimpleInvoiceStatus.Pending);
                Process.SetEnabled(enabled);

                PXUIFieldAttribute.SetEnabled<USSimpleInvoice.date>(sender, null, enabled);
                PXUIFieldAttribute.SetEnabled<USSimpleInvoice.scoutID>(sender, null, enabled);
                PXUIFieldAttribute.SetEnabled<USSimpleInvoice.description>(sender, null, enabled);

                Transactions.Cache.AllowDelete = enabled;
                Transactions.Cache.AllowInsert = enabled;
                Transactions.Cache.AllowUpdate = enabled;
            }
        }
        #endregion
    }
}
