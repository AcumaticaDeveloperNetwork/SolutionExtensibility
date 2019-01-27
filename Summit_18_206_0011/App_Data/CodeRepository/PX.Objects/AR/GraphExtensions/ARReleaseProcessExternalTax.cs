﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TaxProvider;

namespace PX.Objects.AR
{
    public class ARReleaseProcessExternalTax : PXGraphExtension<ARReleaseProcess>
    {
	    public static bool IsActive()
	    {
		    return PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();
	    }

		protected Func<PXGraph, string, ITaxProvider> TaxProviderFactory;

		public ARReleaseProcessExternalTax()
	    {
		    TaxProviderFactory = ExternalTax.TaxProviderFactory;
	    }

	    public virtual bool IsExternalTax(string taxZoneID)
	    {
		    return ExternalTax.IsExternalTax(Base, taxZoneID);
	    }

	    protected Lazy<SOInvoiceEntry> LazySoInvoiceEntry =
		    new Lazy<SOInvoiceEntry>(() => PXGraph.CreateInstance<SOInvoiceEntry>());

	    protected Lazy<ARInvoiceEntry> LazyArInvoiceEntry =
		    new Lazy<ARInvoiceEntry>(() => PXGraph.CreateInstance<ARInvoiceEntry>());

		[PXOverride]
        public virtual ARRegister OnBeforeRelease(ARRegister ardoc)
        {
            var invoice = ardoc as ARInvoice;

            if (invoice == null || !IsExternalTax(invoice.TaxZoneID))
                return ardoc;

            if (invoice.OrigModule == GL.BatchModule.SO)
            {
				var rg = LazySoInvoiceEntry.Value;
	            rg.Clear();

				rg.Document.Current = PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Select(rg, invoice.DocType, invoice.RefNbr);
                rg.Document.Current.ApplyPaymentWhenTaxAvailable = invoice.ApplyPaymentWhenTaxAvailable;
                rg.SODocument.Current = PXSelect<SOInvoice, Where<SOInvoice.docType, Equal<Required<SOInvoice.docType>>, And<SOInvoice.refNbr, Equal<Required<SOInvoice.refNbr>>>>>.Select(rg, invoice.DocType, invoice.RefNbr);

                return rg.CalculateExternalTax(rg.Document.Current);
            } else {
	            var rg = LazyArInvoiceEntry.Value;
	            rg.Clear();

				rg.Document.Current = PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Select(rg, invoice.DocType, invoice.RefNbr);

                return rg.CalculateExternalTax(rg.Document.Current);
            }
        }

	    [PXOverride]
	    public virtual ARInvoice CommitExternalTax(ARInvoice doc)
	    {
		    if (doc != null && doc.IsTaxValid == true && doc.NonTaxable == false && IsExternalTax(doc.TaxZoneID) && doc.InstallmentNbr == null)
		    {
			    if (TaxPluginMaint.IsActive(Base, doc.TaxZoneID))
			    {
				    var service = ExternalTax.TaxProviderFactory(Base, doc.TaxZoneID);

				    CommitTaxRequest request = new CommitTaxRequest();
				    request.CompanyCode = ExternalTax.CompanyCodeFromBranch(Base, doc.TaxZoneID, doc.BranchID);
				    request.DocCode = string.Format("AR.{0}.{1}", doc.DocType, doc.RefNbr);

				    if (doc.DocType == ARDocType.CreditMemo)
					    request.DocType = TaxDocumentType.ReturnInvoice;
				    else
					    request.DocType = TaxDocumentType.SalesInvoice;


				    CommitTaxResult result = service.CommitTax(request);
				    if (result.IsSuccess)
				    {
					    doc.IsTaxPosted = true;
				    }
				    else
				    {
					    //Avalara retuned an error - The given document is already marked as posted on the avalara side.
					    if (!result.IsSuccess && result.Messages.Any(t => t.Contains("Expected Posted")))
					    {
						    //ignore this error - everything is cool
					    }
					    else
					    {
						    //show as warning.
						    StringBuilder sb = new StringBuilder();
						    foreach (var msg in result.Messages)
						    {
							    sb.AppendLine(msg);
						    }

						    if (sb.Length > 0)
						    {
							    doc.WarningMessage = PXMessages.LocalizeFormatNoPrefixNLA(Messages.PostingToExternalTaxProviderFailed, sb.ToString());
						    }
					    }
				    }
			    }
		    }

		    return doc;
	    }
	}
}
