using PX.Data;
using PX.Objects.AP;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class APInvoiceEntryExtension : PXGraphExtension<APInvoiceEntry>
    {
        #region Views
        public PXSetup<USSetup> USSetup;

        public PXSelect<USSimpleInvoice, Where<USSimpleInvoice.refNbr, Equal<Current<APInvoice.refNbr>>>> USSimpleInvoices;
        #endregion

        #region CacheAttached
        [PXDBString(15, IsKey = true)]
        [PXDBDefault(typeof(APInvoice.refNbr))]
        [PXParent(typeof(Select<APInvoice, Where<APInvoice.refNbr, Equal<Current<USSimpleInvoice.invoiceRef>>>>))]
        protected virtual void USSimpleInvoice_RefNbr_CacheAttached(PXCache sender)
        {
        }
        #endregion

        #region Methods
        public virtual APTran GenerateAPTran(USSimpleInvoiceLine detail)
        {
            APTran tran = new APTran();
            tran.InventoryID = detail.InventoryID;
            tran.Qty = detail.Qty;
            tran.ProjectID = NonProject.ID;
            return tran;
        }
        #endregion
    }
}
