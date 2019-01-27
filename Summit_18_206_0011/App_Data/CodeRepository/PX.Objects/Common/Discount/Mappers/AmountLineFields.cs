﻿using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.Extensions.Discount;

namespace PX.Objects.Common.Discount.Mappers
{
	public abstract class AmountLineFields : DiscountedLineMapperBase
	{
		public virtual decimal? Quantity { get; set; }
		public virtual decimal? CuryUnitPrice { get; set; }
		public virtual decimal? CuryExtPrice { get; set; }
		public virtual decimal? CuryLineAmount { get; set; }
		public virtual string UOM { get; set; }
		public virtual decimal? OrigGroupDiscountRate { get; set; }
		public virtual decimal? OrigDocumentDiscountRate { get; set; }
		public virtual decimal? GroupDiscountRate { get; set; }
		public virtual decimal? DocumentDiscountRate { get; set; }
		public virtual string TaxCategoryID { get; set; }
		public virtual bool? FreezeManualDisc { get; set; }

		public abstract class quantity : IBqlField { }
		private abstract class orderQty : IBqlField { }
		private abstract class baseOrderQty : IBqlField { }
		public abstract class curyUnitPrice : IBqlField { }
		private abstract class curyUnitCost : IBqlField { }
		public abstract class curyExtPrice : IBqlField { }
		private abstract class curyExtCost : IBqlField { }
		public abstract class curyLineAmount : IBqlField { }
		private abstract class curyLineAmt : IBqlField { }
		public abstract class uOM : IBqlField { }
		public abstract class origGroupDiscountRate : IBqlField { }
		public abstract class origDocumentDiscountRate : IBqlField { }
		public abstract class groupDiscountRate : IBqlField { }
		public abstract class documentDiscountRate : IBqlField { }
		public abstract class taxCategoryID : IBqlField { }
		public abstract class freezeManualDisc : IBqlField { }

		public string ExtPriceDisplayName => GetDisplayName<curyExtPrice>();

		protected AmountLineFields(PXCache cache, object row) : base(cache, row) { }

		private object GetState<T>() where T : IBqlField => Cache.GetStateExt(MappedLine, Cache.GetField(GetField<T>()));

		private string GetDisplayName<T>() where T : IBqlField
		{
			PXFieldState state = (PXFieldState)GetState<T>();
			return state.DisplayName;
		}

		/// <summary>
		/// Get map to amount line fields
		/// </summary>
		/// <remarks>
		/// QuantityField: Quantity
		/// CuryUnitPriceField: Cury Unit Price
		/// CuryExtPriceField: Quantity * Cury Unit Price field
		/// CuryLineAmountField: (Quantity * Cury Unit Price field) - Cury Discount Amount field
		///</remarks>
		public static AmountLineFields GetMapFor<TLine>(TLine line, PXCache cache)
			where TLine : class, IBqlTable
		{
			Type lineType = line?.GetType() ?? typeof(TLine);
			return GetMapFor(line, cache, lineType);
		}

		internal static AmountLineFields GetMapFor(object line, PXCache cache)
		{
			return GetMapFor(line, cache, line.GetType());
		}

		private static AmountLineFields GetMapFor(object line, PXCache cache, Type lineType)
		{
			if (lineType == typeof(ARTran))
			{
				return DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph)
					? (AmountLineFields)new RetainedAmountLineFields<ARTran.baseQty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate>(cache, line)
					: (AmountLineFields)new RetainedAmountLineFields<ARTran.qty, ARTran.curyUnitPrice, ARTran.curyExtPrice, ARTran.curyTranAmt, ARTran.curyRetainageAmt, ARTran.uOM, ARTran.origGroupDiscountRate, ARTran.origDocumentDiscountRate, ARTran.groupDiscountRate, ARTran.documentDiscountRate>(cache, line);
			}
			if (lineType == typeof(SOLine))
			{
				return DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph)
					? (AmountLineFields)new AmountLineFields<SOLine.baseOrderQty, SOLine.curyUnitPrice, SOLine.curyExtPrice, SOLine.curyLineAmt, SOLine.uOM, origGroupDiscountRate, origDocumentDiscountRate, SOLine.groupDiscountRate, SOLine.documentDiscountRate>(cache, line)
					: (AmountLineFields)new AmountLineFields<SOLine.orderQty, SOLine.curyUnitPrice, SOLine.curyExtPrice, SOLine.curyLineAmt, SOLine.uOM, origGroupDiscountRate, origDocumentDiscountRate, SOLine.groupDiscountRate, SOLine.documentDiscountRate>(cache, line);
			}
			if (lineType == typeof(SOShipLine))
			{
				return DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph)
					? (AmountLineFields)new AmountLineFields<SOShipLine.baseShippedQty, curyUnitCost, curyLineAmt, curyExtCost, SOShipLine.uOM, origGroupDiscountRate, origDocumentDiscountRate, groupDiscountRate, documentDiscountRate>(cache, line)
					: (AmountLineFields)new AmountLineFields<SOShipLine.shippedQty, curyUnitCost, curyLineAmt, curyExtCost, SOShipLine.uOM, origGroupDiscountRate, origDocumentDiscountRate, groupDiscountRate, documentDiscountRate>(cache, line);
			}
			if (lineType == typeof(APTran))
			{
				return DiscountEngine.ApplyQuantityDiscountByBaseUOMForAP(cache.Graph)
					? (AmountLineFields)new RetainedAmountLineFields<APTran.baseQty, APTran.curyUnitCost, APTran.curyLineAmt, APTran.curyTranAmt, APTran.curyRetainageAmt, APTran.uOM, APTran.origGroupDiscountRate, APTran.origDocumentDiscountRate, APTran.groupDiscountRate, APTran.documentDiscountRate>(cache, line)
					: (AmountLineFields)new RetainedAmountLineFields<APTran.qty, APTran.curyUnitCost, APTran.curyLineAmt, APTran.curyTranAmt, APTran.curyRetainageAmt, APTran.uOM, APTran.origGroupDiscountRate, APTran.origDocumentDiscountRate, APTran.groupDiscountRate, APTran.documentDiscountRate>(cache, line);
			}
			if (lineType == typeof(POLine))
			{
				return DiscountEngine.ApplyQuantityDiscountByBaseUOMForAP(cache.Graph)
					? (AmountLineFields)new RetainedAmountLineFields<POLine.baseOrderQty, POLine.curyUnitCost, POLine.curyLineAmt, POLine.curyExtCost, POLine.curyRetainageAmt, POLine.uOM, origGroupDiscountRate, origDocumentDiscountRate, POLine.groupDiscountRate, POLine.documentDiscountRate>(cache, line)
					: (AmountLineFields)new RetainedAmountLineFields<POLine.orderQty, POLine.curyUnitCost, POLine.curyLineAmt, POLine.curyExtCost, POLine.curyRetainageAmt, POLine.uOM, origGroupDiscountRate, origDocumentDiscountRate, POLine.groupDiscountRate, POLine.documentDiscountRate>(cache, line);
			}
			if (lineType == typeof(CROpportunityProducts))
			{
				return new AmountLineFields<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitPrice, CROpportunityProducts.curyExtPrice, CROpportunityProducts.curyAmount, CROpportunityProducts.uOM, origGroupDiscountRate, origDocumentDiscountRate, CROpportunityProducts.groupDiscountRate, CROpportunityProducts.documentDiscountRate, freezeManualDisc>(cache, line);
			}
			if (lineType == typeof(Detail))
			{
				return new AmountLineFields<Detail.quantity, Detail.curyUnitPrice, Detail.curyExtPrice, Detail.curyLineAmount, Detail.uOM, Detail.origGroupDiscountRate, Detail.origDocumentDiscountRate, Detail.groupDiscountRate, Detail.documentDiscountRate>(cache, line);
			}
			return DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR(cache.Graph)
				? (AmountLineFields)new AmountLineFields<baseOrderQty, curyUnitPrice, curyExtPrice, curyLineAmt, uOM, origGroupDiscountRate, origDocumentDiscountRate, groupDiscountRate, documentDiscountRate>(cache, line)
				: (AmountLineFields)new AmountLineFields<orderQty, curyUnitPrice, curyExtPrice, curyLineAmt, uOM, origGroupDiscountRate, origDocumentDiscountRate, groupDiscountRate, documentDiscountRate>(cache, line);
		}
	}

	public class AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField>
		: AmountLineFields
		where QuantityField : IBqlField
		where CuryUnitPriceField : IBqlField
		where CuryExtPriceField : IBqlField
		where CuryLineAmountField : IBqlField
		where UOMField : IBqlField
		where OrigGroupDiscountRateField : IBqlField
		where OrigDocumentDiscountRateField : IBqlField
		where GroupDiscountRateField : IBqlField
		where DocumentDiscountRateField : IBqlField
	{
		public AmountLineFields(PXCache cache, object row) : base(cache, row) { }

		public override Type GetField<T>()
		{
			if (typeof(T) == typeof(quantity))
			{
				return typeof(QuantityField);
			}
			if (typeof(T) == typeof(curyUnitPrice))
			{
				return typeof(CuryUnitPriceField);
			}
			if (typeof(T) == typeof(curyExtPrice))
			{
				return typeof(CuryExtPriceField);
			}
			if (typeof(T) == typeof(curyLineAmount))
			{
				return typeof(CuryLineAmountField);
			}
			if (typeof(T) == typeof(uOM))
			{
				return typeof(UOMField);
			}
			if (typeof(T) == typeof(origGroupDiscountRate))
			{
				return typeof(GroupDiscountRateField);
			}
			if (typeof(T) == typeof(origDocumentDiscountRate))
			{
				return typeof(DocumentDiscountRateField);
			}
			if (typeof(T) == typeof(groupDiscountRate))
			{
				return typeof(GroupDiscountRateField);
			}
			if (typeof(T) == typeof(documentDiscountRate))
			{
				return typeof(DocumentDiscountRateField);
			}
			return null;
		}

		public override decimal? Quantity
		{
			get { return (decimal?) Cache.GetValue<QuantityField>(MappedLine); }
			set { Cache.SetValue<QuantityField>(MappedLine, value); }
		}

		public override decimal? CuryUnitPrice
		{
			get { return (decimal?) Cache.GetValue<CuryUnitPriceField>(MappedLine); }
			set { Cache.SetValue<CuryUnitPriceField>(MappedLine, value); }
		}

		public override decimal? CuryExtPrice
		{
			get { return (decimal?) Cache.GetValue<CuryExtPriceField>(MappedLine); }
			set { Cache.SetValue<CuryExtPriceField>(MappedLine, value); }
		}

		public override decimal? CuryLineAmount
		{
			get { return (decimal?) Cache.GetValue<CuryLineAmountField>(MappedLine); }
			set { Cache.SetValue<CuryLineAmountField>(MappedLine, value); }
		}

		public override string UOM
		{
			get { return (string) Cache.GetValue<UOMField>(MappedLine); }
			set { Cache.SetValue<UOMField>(MappedLine, value); }
		}

		public override decimal? OrigGroupDiscountRate
		{
			get { return (decimal?)Cache.GetValue<OrigGroupDiscountRateField>(MappedLine); }
			set { Cache.SetValue<OrigGroupDiscountRateField>(MappedLine, value); }
		}

		public override decimal? OrigDocumentDiscountRate
		{
			get { return (decimal?)Cache.GetValue<OrigDocumentDiscountRateField>(MappedLine); }
			set { Cache.SetValue<OrigDocumentDiscountRateField>(MappedLine, value); }
		}

		public override decimal? GroupDiscountRate
		{
			get { return (decimal?) Cache.GetValue<GroupDiscountRateField>(MappedLine); }
			set { Cache.SetValue<GroupDiscountRateField>(MappedLine, value); }
		}

		public override decimal? DocumentDiscountRate
		{
			get { return (decimal?) Cache.GetValue<DocumentDiscountRateField>(MappedLine); }
			set { Cache.SetValue<DocumentDiscountRateField>(MappedLine, value); }
		}
	}

	public class AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField, FreezeManualDiscField>
		: AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField>
		where QuantityField : IBqlField
		where CuryUnitPriceField : IBqlField
		where CuryExtPriceField : IBqlField
		where CuryLineAmountField : IBqlField
		where UOMField : IBqlField
		where OrigGroupDiscountRateField : IBqlField
		where OrigDocumentDiscountRateField : IBqlField
		where GroupDiscountRateField : IBqlField
		where DocumentDiscountRateField : IBqlField
		where FreezeManualDiscField : IBqlField
	{
		public AmountLineFields(PXCache cache, object row) : base(cache, row) { }

		public override Type GetField<T>()
		{
			if (typeof(T) == typeof(freezeManualDisc))
			{
				return typeof(FreezeManualDiscField);
			}
			else
			{
				return base.GetField<T>();
			}
		}

		public override bool? FreezeManualDisc
		{
			get { return (bool?)Cache.GetValue<FreezeManualDiscField>(MappedLine); }
			set { Cache.SetValue<FreezeManualDiscField>(MappedLine, value); }
		}
	}

	#region RetainedAmountLineFields
	/// <summary>
	/// A specialized for retainage version of the <see cref="AmountLineFields"/> class
	/// with an additional CuryRetainageAmtField generic parameter.
	/// Discount fields should not be recalculated after retainage fields changing
	/// because they have a higher priority.
	/// </summary>
	public class RetainedAmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, CuryRetainageAmtField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField, FreezeManualDiscField>
		: AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField, FreezeManualDiscField>

		where QuantityField : IBqlField
		where CuryUnitPriceField : IBqlField
		where CuryExtPriceField : IBqlField
		where CuryLineAmountField : IBqlField
		where CuryRetainageAmtField : IBqlField
		where UOMField : IBqlField
		where OrigGroupDiscountRateField : IBqlField
		where OrigDocumentDiscountRateField : IBqlField
		where GroupDiscountRateField : IBqlField
		where DocumentDiscountRateField : IBqlField
		where FreezeManualDiscField : IBqlField
	{
		public RetainedAmountLineFields(PXCache cache, object row)
			: base(cache, row)
		{
		}

		public override decimal? CuryLineAmount
		{
			get
			{
				return base.CuryLineAmount +
						(decimal)(Cache.GetValue<CuryRetainageAmtField>(MappedLine) ?? 0m);
			}
			set
			{
				base.CuryLineAmount = value;
			}
		}
	}

	/// <summary>
	/// A specialized for retainage version of the <see cref="AmountLineFields"/> class
	/// with an additional CuryRetainageAmtField generic parameter.
	/// Discount fields should not be recalculated after retainage fields changing
	/// because they have a higher priority.
	/// </summary>
	public class RetainedAmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, CuryRetainageAmtField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField>
		: AmountLineFields<QuantityField, CuryUnitPriceField, CuryExtPriceField, CuryLineAmountField, UOMField, OrigGroupDiscountRateField, OrigDocumentDiscountRateField, GroupDiscountRateField, DocumentDiscountRateField>

		where QuantityField : IBqlField
		where CuryUnitPriceField : IBqlField
		where CuryExtPriceField : IBqlField
		where CuryLineAmountField : IBqlField
		where CuryRetainageAmtField : IBqlField
		where UOMField : IBqlField
		where OrigGroupDiscountRateField : IBqlField
		where OrigDocumentDiscountRateField : IBqlField
		where GroupDiscountRateField : IBqlField
		where DocumentDiscountRateField : IBqlField
	{
		public RetainedAmountLineFields(PXCache cache, object row)
			: base(cache, row)
		{
		}

		public override decimal? CuryLineAmount
		{
			get
			{
				return base.CuryLineAmount +
						(decimal)(Cache.GetValue<CuryRetainageAmtField>(MappedLine) ?? 0m);
			}
			set
			{
				base.CuryLineAmount = value;
			}
		}
	}

	#endregion

}