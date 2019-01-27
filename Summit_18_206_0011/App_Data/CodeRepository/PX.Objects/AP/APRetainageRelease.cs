using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using static PX.Objects.AP.APInvoiceEntry;

namespace PX.Objects.AP
{
	[Serializable]
	public partial class APRetainageFilter : IBqlTable
	{
		#region DocDate
		public abstract class docDate : PX.Data.IBqlField { }

		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DocDate { get; set; }
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField { }

		[APOpenPeriod(typeof(APRetainageFilter.docDate),
			typeof(APRetainageFilter.branchID),
			useMasterOrganizationIDByDefault: true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String FinPeriodID { get; set; }
		#endregion

		#region BranchID
		public abstract class branchID : PX.Data.IBqlField { }

		[Branch(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? BranchID { get; set; }
		#endregion

		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField { }

		[VendorActive(
			Visibility = PXUIVisibility.SelectorVisible,
			Required = false,
			DescriptionField = typeof(Vendor.acctName))]
		public virtual int? VendorID { get; set; }
		#endregion

		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField { }

		[APActiveProjectAttibute()]
		public virtual Int32? ProjectID { get; set; }
		#endregion

		#region ShowBillsWithOpenBalance
		public abstract class showBillsWithOpenBalance : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Show Bills with Open Balance", Visibility = PXUIVisibility.Visible)]
		public virtual Boolean? ShowBillsWithOpenBalance { get; set; }
		#endregion
	}

	[Serializable]
	public class APInvoiceExt : APInvoice
	{
		#region CuryID
		public new abstract class curyID : IBqlField { }

		/// <summary>
		/// Code of the <see cref="PX.Objects.CM.Currency">Currency</see> of the document.
		/// </summary>
		/// <value>
		/// Defaults to the <see cref="Company.BaseCuryID">company's base currency</see>.
		/// </value>
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, FieldClass = nameof(FeaturesSet.Multicurrency))]
		[PXDefault(typeof(Search<Company.baseCuryID>))]
		[PXSelector(typeof(Currency.curyID))]
		public override string CuryID
		{
			get;
			set;
		}

		#endregion

		#region RetainageReleasePct
		public abstract class retainageReleasePct : IBqlField { }

		[UnboundRetainagePercent(
			typeof(True),
			typeof(decimal100),
			typeof(APInvoiceExt.curyRetainageUnreleasedAmt),
			typeof(APInvoiceExt.curyRetainageReleasedAmt),
			typeof(APInvoiceExt.retainageReleasePct),
			DisplayName = "Percent to Release")]
		public virtual decimal? RetainageReleasePct
		{
			get;
			set;
		}
		#endregion

		#region CuryRetainageReleasedAmt
		public abstract class curyRetainageReleasedAmt : IBqlField { }

		[UnboundRetainageAmount(
			typeof(APInvoiceExt.curyInfoID),
			typeof(APInvoiceExt.curyRetainageUnreleasedAmt),
			typeof(APInvoiceExt.curyRetainageReleasedAmt),
			typeof(APInvoiceExt.retainageReleasedAmt),
			typeof(APInvoiceExt.retainageReleasePct),
			DisplayName = "Retainage to Release")]
		public virtual decimal? CuryRetainageReleasedAmt
		{
			get;
			set;
		}
		#endregion
		#region RetainageReleasedAmt

		public abstract class retainageReleasedAmt : IBqlField { }
		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? RetainageReleasedAmt
		{
			get;
			set;
		}
		#endregion

		#region CuryRetainageUnreleasedCalcAmt
		public abstract class curyRetainageUnreleasedCalcAmt : IBqlField { }

		[PXCurrency(typeof(APInvoiceExt.curyInfoID), typeof(APInvoiceExt.retainageUnreleasedCalcAmt))]
		[PXUIField(DisplayName = "Unreleased Retainage")]
		[PXFormula(typeof(Sub<APInvoiceExt.curyRetainageUnreleasedAmt, APInvoiceExt.curyRetainageReleasedAmt>))]
		public virtual decimal? CuryRetainageUnreleasedCalcAmt
		{
			get;
			set;
		}
		#endregion
		#region RetainageUnreleasedCalcAmt
		public abstract class retainageUnreleasedCalcAmt : IBqlField { }

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? RetainageUnreleasedCalcAmt
		{
			get;
			set;
		}
		#endregion

		#region RetainageVendorRef
		public abstract class retainageVendorRef : IBqlField { }
		[PXString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Retainage Vendor Ref.", Visibility = PXUIVisibility.SelectorVisible)]
		[APVendorRefNbr]
		public virtual string RetainageVendorRef
		{
			get;
			set;
		}
		#endregion
	}

	[TableAndChartDashboardType]
	public class APRetainageRelease : PXGraph<APRetainageRelease>
	{
		public PXFilter<APRetainageFilter> Filter;
		public PXCancel<APRetainageFilter> Cancel;

		[PXFilterable]
		public PXFilteredProcessing<APInvoiceExt, APRetainageFilter,
			Where2<
				Where<Current2<APRetainageFilter.vendorID>, IsNull, Or<APInvoiceExt.vendorID, Equal<Current2<APRetainageFilter.vendorID>>>>,
				And2<Where<Current2<APRetainageFilter.projectID>, IsNull, Or<APInvoiceExt.projectID, Equal<Current2<APRetainageFilter.projectID>>>>,
				And2<Where<Current2<APRetainageFilter.branchID>, IsNull, Or<APInvoiceExt.branchID, Equal<Current2<APRetainageFilter.branchID>>>>,
				And2<Where<Current<APRetainageFilter.showBillsWithOpenBalance>, Equal<True>,
					Or<Where<APInvoiceExt.curyDocBal, Equal<decimal0>,
					And<Current<APRetainageFilter.showBillsWithOpenBalance>, NotEqual<True>>>>>,
				And<APInvoiceExt.curyRetainageUnreleasedAmt, Greater<decimal0>,
				And<APInvoiceExt.curyRetainageTotal, Greater<decimal0>,
				And<APInvoiceExt.retainageApply, Equal<True>,
				And<APInvoiceExt.released, Equal<True>,
				And<APInvoiceExt.docDate, LessEqual<Current<APRetainageFilter.docDate>>>>>>>>>>>, 
			OrderBy<Asc<APInvoiceExt.refNbr>>> DocumentList;

		public PXSetup<APSetup> APSetup;

		public PXAction<APRetainageFilter> viewDocument;
		[PXButton]
		public virtual IEnumerable ViewDocument(PXAdapter adapter)
		{
			if (DocumentList.Current != null)
			{
				PXRedirectHelper.TryRedirect(DocumentList.Cache, DocumentList.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
			}
			return adapter.Get();
		}

		protected virtual IEnumerable documentList()
		{
			foreach (PXResult<APInvoiceExt> result in PXSelect<APInvoiceExt,
				Where2<
					Where<Current2<APRetainageFilter.vendorID>, IsNull, Or<APInvoiceExt.vendorID, Equal<Current2<APRetainageFilter.vendorID>>>>,
					And2<Where<Current2<APRetainageFilter.projectID>, IsNull, Or<APInvoiceExt.projectID, Equal<Current2<APRetainageFilter.projectID>>>>,
					And2<Where<Current2<APRetainageFilter.branchID>, IsNull, Or<APInvoiceExt.branchID, Equal<Current2<APRetainageFilter.branchID>>>>,
					And2<Where<Current<APRetainageFilter.showBillsWithOpenBalance>, Equal<True>,
						Or<Where<APInvoiceExt.curyDocBal, Equal<decimal0>,
						And<Current<APRetainageFilter.showBillsWithOpenBalance>, NotEqual<True>>>>>,
					And<APInvoiceExt.curyRetainageUnreleasedAmt, Greater<decimal0>,
					And<APInvoiceExt.curyRetainageTotal, Greater<decimal0>,
					And<APInvoiceExt.docType, Equal<APDocType.invoice>,
					And<APInvoiceExt.retainageApply, Equal<True>,
					And<APInvoiceExt.released, Equal<True>,
					And<APInvoiceExt.docDate, LessEqual<Current<APRetainageFilter.docDate>>>>>>>>>>>>,
				OrderBy<Asc<APInvoiceExt.refNbr>>>.Select(this))
			{
				APInvoiceExt doc = result;

				PXResult<APRetainageInvoice> NotReleasedRetainageInvoice = PXSelect<APRetainageInvoice,
				Where<APRetainageInvoice.isRetainageDocument, Equal<True>,
					And<APRetainageInvoice.origDocType, Equal<Required<APInvoice.docType>>,
					And<APRetainageInvoice.origRefNbr, Equal<Required<APInvoice.refNbr>>,
					And<APRetainageInvoice.released, NotEqual<True>>>>>>.SelectSingleBound(this, null, doc.DocType, doc.RefNbr);

				if (NotReleasedRetainageInvoice == null)
					yield return doc;
			}
		}

		public APRetainageRelease()
		{
			APSetup setup = APSetup.Current;

			bool isRequireSingleProjectPerDocument = APSetup.Current?.RequireSingleProjectPerDocument == true;

			PXUIFieldAttribute.SetVisible<APRetainageFilter.projectID>(Filter.Cache, null, isRequireSingleProjectPerDocument);
			PXUIFieldAttribute.SetVisible<APInvoiceExt.projectID>(DocumentList.Cache, null, isRequireSingleProjectPerDocument);
		}

		public static void ReleaseRetainage(APInvoiceEntry graph, APInvoiceExt invoice, APRetainageFilter filter, bool isAutoRelease)
		{
			graph.Clear(PXClearOption.ClearAll);
			PXUIFieldAttribute.SetError(graph.Document.Cache, null, null, null);

			RetainageOptions retainageOptions = new RetainageOptions();
			retainageOptions.DocDate = filter.DocDate;
			retainageOptions.FinPeriodID = filter.FinPeriodID;
			retainageOptions.CuryRetainageAmt = invoice.CuryRetainageReleasedAmt;
			retainageOptions.InvoiceNbr = invoice.RetainageVendorRef;

			APInvoiceEntryRetainage retainageExt = graph.GetExtension<APInvoiceEntryRetainage>();

			APInvoice retainageInvoice = retainageExt.ReleaseRetainageProc(invoice, retainageOptions, isAutoRelease);
			graph.Save.Press();

			if (isAutoRelease && retainageInvoice.Hold != true)
			{
				List<APRegister> toRelease = new List<APRegister>() { retainageInvoice };
				using (new PXTimeStampScope(null))
				{
					APDocumentRelease.ReleaseDoc(toRelease, true);
				}
			}
		}

		protected virtual void APRetainageFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			APRetainageFilter filter = e.Row as APRetainageFilter;
			if (filter == null) return;

			bool isAutoRelease = APSetup.Current?.RetainageBillsAutoRelease == true;
			
			DocumentList.SetProcessDelegate<APInvoiceEntry>(
				delegate (APInvoiceEntry graph, APInvoiceExt item)
				{
					ReleaseRetainage(graph, item, filter, isAutoRelease);
				});
		}

		protected virtual void APInvoiceExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			APInvoiceExt invoice = e.Row as APInvoiceExt;
			if (invoice == null) return;

			PXUIFieldAttribute.SetEnabled(sender, invoice, false);
			PXUIFieldAttribute.SetEnabled<APInvoiceExt.selected>(sender, invoice, true);
			PXUIFieldAttribute.SetEnabled<APInvoiceExt.retainageReleasePct>(sender, invoice, true);
			PXUIFieldAttribute.SetEnabled<APInvoiceExt.curyRetainageReleasedAmt>(sender, invoice, true);
			PXUIFieldAttribute.SetEnabled<APInvoiceExt.retainageVendorRef>(sender, invoice, true);

			APVendorRefNbrAttribute aPVendorRefNbrAttribute = sender.GetAttributesReadonly<APInvoiceExt.retainageVendorRef>()
				.OfType<APVendorRefNbrAttribute>().FirstOrDefault();
			if (aPVendorRefNbrAttribute != null)
			{
				var args = new PXFieldVerifyingEventArgs(invoice, invoice.RetainageVendorRef, true);
				aPVendorRefNbrAttribute.FieldVerifying(sender, args);
			}

			if (invoice.Selected ?? true)
			{
				Dictionary<String, String> errors = PXUIFieldAttribute.GetErrors(sender, invoice, PXErrorLevel.Error);
				if (errors.Count > 0)
				{
					invoice.Selected = false;
					DocumentList.Cache.SetStatus(invoice, PXEntryStatus.Updated);
					sender.RaiseExceptionHandling<APInvoiceExt.selected>(
						invoice,
						null,
						new PXSetPropertyException(Messages.ErrorRaised, PXErrorLevel.RowError));

					PXUIFieldAttribute.SetEnabled<APInvoiceExt.selected>(sender, invoice, false);
				}
			}
		}

		public override bool IsDirty => false;
	}
}