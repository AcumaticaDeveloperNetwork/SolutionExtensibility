namespace ProjectOne
{
    public class SomeGraph : PXGraph<SomeGraph, SomeHeader>
    {

        #region Views

        public PXSelect<SomeHeader> Document;

        public PXSelect<SomeHeader,
                                    Where<SomeHeader.keyField, Equal<Current<SomeHeader.keyField>>>> CurrentDocument;

        public PXSelect<SomeDetail,
                                    Where<SomeDetail.keyField, Equal<Current<SomeHeader.keyField>>>> Details;

        #endregion

        #region Buttons

        public PXAction<SomeHeader> Process;

        [PXButton]
        [PXUIField(DisplayName = "Process")]
        protected virtual IEnumerable process(PXAdapter adapter)
        {
            SomeHeader header = Document.Current;

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

        public static void ProcessMethod(SomeHeader header)
        {
            //You should not use the instance of the Graph from the UI in long operations, because they will not be run asynchronously, and could hang-up/timeout
            SomeGraph graph = PXGraph.CreateInstance<SomeGraph>();

            APInvoiceEntry apInvoiceEntry = PXGraph.CreateInstance<APInvoiceEntry>();

            //Logic for creating the APInvoice here

            APTran apTran;

            foreach (SomeDetail detail in PXSelectReadonly<SomeDetail,
                                                                        Where<SomDetail.keyField, Equal<Required<SomeDetail.keyField>>>>.Select(apInvoiceEntry, header.KeyField))
            {
                apTran = apInvoiceEntryExtension.GenerateAPTran(detail);

                apInvoiceEntry.Transactions.Insert(apTran);
            }

            //Only press Save once
            apInvoiceEntry.Save.Press();
        }
        #endregion

    }

    public class APInvoiceEntryExtension : PXGraphExtension<APInvoiceEntry>
    {
        //In 2018 R1, Acumatica finally allowed Devs to PXOverride virtual methods from Graph Extensions
        protected virtual APTran GenerateAPTran(SomeDetail detail)
        {
            //Code to create APTran from Detail data
        }
    }
}

namespace ProjectTwo
{

	public class APInvoiceEntryExtension : PXGraphExtension<APInvoiceEntryExtension, APInvoiceEntry>
	{
				
		#region Overrides
		
		public delegate APTran GenerateAPTranDel(SomeDetail detail);
		
		[PXOverride]
		protected virtual APTran GenerateAPTran(SomeDetail detail, GenerateAPTranDel del)
		{
			//Get the APTran your base code would have generated
			APTran tran = del(detail);
			
			//Add our custom field data to APTranExtension
			
			return tran;
		}
		
		#endregion
		
	}
}