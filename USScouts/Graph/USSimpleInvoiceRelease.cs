using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class USSimpleInvoiceRelease : PXGraph<USSimpleInvoiceRelease>
    {
        public USSimpleInvoiceRelease()
        {
            Documents.SetProcessDelegate(CreateInvoice);
        }

        public PXCancel<USSimpleInvoice> Cancel;

        public PXProcessing<USSimpleInvoice> Documents;

        public static void CreateInvoice(List<USSimpleInvoice> invoices)
        {
            foreach(USSimpleInvoice invoice in invoices)
            {
                try
                {

                }
                catch
                {

                }
            }
        }
    }
}
