﻿using System;
using System.Collections.Generic;

using PX.Data;
using PX.Objects.CS;
using PX.Objects.TX;


namespace PX.Objects.AP
{
	/// <summary>
	/// Specialized for <see cref="FeaturesSet.Retainage"/> feature version of the APTaxAttribute. <br/>
	/// Provides Tax calculation for <see cref="APTran.CuryRetainageAmt"/> amount in the line, by default is attached 
	/// to <see cref="APTran"/> (details) and <see cref="APInvoice"/> (header). <br/>
	/// Normally, should be placed on the <see cref="APTran.TaxCategoryID"/> field. <br/>
	/// Internally, it uses <see cref="APInvoiceEntry"/> graph, otherwise taxes will not be calculated 
	/// (<see cref="Tax.TaxCalcLevel"/> is set to <see cref="TaxCalc.NoCalc"/>).<br/>
	/// As a result of this attribute work a set of <see cref="APTax"/> tran related to each line and to their parent will be created <br/>
	/// <example>
	/// [APRetainedTaxAttribute(typeof(APRegister), typeof(APTax), typeof(APTaxTran))]
	/// </example>
	/// </summary>
	public class APRetainedTaxAttribute : APTaxAttribute
	{
		protected override short SortOrder
		{
			get
			{
				return 1;
			}
		}

		public APRetainedTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
			: base(ParentType, TaxType, TaxSumType)
		{
			Init();
		}

		public APRetainedTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType, Type CalcMode)
			: base(ParentType, TaxType, TaxSumType, CalcMode)
		{
			Init();
		}

		private readonly HashSet<string> allowedParentFields = new HashSet<string>();

		private void Init()
		{
			#region Line fields

			CuryTranAmt = typeof(APTran.curyRetainageAmt);
			GroupDiscountRate = typeof(APTran.groupDiscountRate);

			#endregion

			#region Parent fields

			CuryLineTotal = typeof(APInvoice.curyLineRetainageTotal);
			CuryTaxTotal = typeof(APInvoice.curyRetainedTaxTotal);
			CuryDiscTot = typeof(APInvoice.curyRetainedDiscTotal);
			CuryDocBal = typeof(APInvoice.curyRetainageTotal);

			allowedParentFields.Add(_CuryLineTotal);
			allowedParentFields.Add(_CuryTaxTotal);
			allowedParentFields.Add(_CuryDiscTot);
			allowedParentFields.Add(_CuryDocBal);

			#endregion

			_CuryOrigTaxableAmt = string.Empty;
			_CuryTaxableAmt = typeof(APTax.curyRetainedTaxableAmt).Name;
			_CuryTaxAmt = typeof(APTax.curyRetainedTaxAmt).Name;

			_Attributes.Clear();
			_Attributes.Add(new PXUnboundFormulaAttribute(
				typeof(Switch<Case<Where<APTran.lineType, NotEqual<SO.SOLineType.discount>>, APTran.curyRetainageAmt>, decimal0>),
				typeof(SumCalc<APInvoice.curyLineRetainageTotal>)));
		}

		protected override List<object> SelectTaxes<WhereType>(PXGraph graph, object row, PXTaxCheck taxchk, params object[] parameters)
		{
			return 
				IsRetainedTaxes(graph)
					? base.SelectTaxes<WhereType>(graph, row, taxchk, parameters)
					: new List<object>();
		}

		protected override void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
		{
			if (IsRetainedTaxes(sender.Graph))
			{
				base.DefaultTaxes(sender, row, DefaultExisting);
			}
		}

		protected override bool IsTaxRowAmountUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			// This method should be overriden
			// to switch on a tax calculation on 
			// Tax_RowUpdated event in the case 
			// when retainage amount have been changed.
			//
			return 
				base.IsTaxRowAmountUpdated(sender, e) ||
				!sender.ObjectsEqual<APTax.curyRetainedTaxAmt>(e.Row, e.OldRow);
		}

		protected override bool IsDeductibleVATTax(Tax tax)
		{
			// We shouldn't affect retainage tax amount
			// in this case, because it will be separated
			// further in the child retainage Bill.
			//
			return false;
		}

		protected override void ParentSetValue(PXGraph graph, string fieldname, object value)
		{
			// Retained taxes should affect
			// only parent fields described in the
			// current implementation of the
			// Init() method.
			//
			if (allowedParentFields.Contains(fieldname))
			{
				base.ParentSetValue(graph, fieldname, value);
			}
		}

		protected override bool IsRoundingNeeded(PXGraph graph)
		{
			// We shouldn't affect CuryRoundDiff value
			// by the APRetainedTax attribute.
			//
			return false;
		}

		protected override void ResetRoundingDiff(PXGraph graph) { }

		protected override void ReDefaultTaxes(PXCache cache, List<object> details) { }

		protected override void ReDefaultTaxes(PXCache cache, object clearDet, object defaultDet, bool defaultExisting = true) { }

		protected override void SetExtCostExt(PXCache sender, object child, decimal? value) { }

		protected override void AdjustTaxableAmount(PXCache sender, object row, List<object> taxitems, ref decimal CuryTaxableAmt, string TaxCalcType) { }

		protected override void SetTaxableAmt(PXCache sender, object row, decimal? value) { }

		protected override void SetTaxAmt(PXCache sender, object row, decimal? value) { }

		protected override decimal? GetTaxableAmt(PXCache sender, object row)
		{
			return 0m;
		}

		protected override decimal? GetTaxAmt(PXCache sender, object row)
		{
			return 0m;
		}
	}
}
