using System;
using PX.Data;

namespace PX.Objects.Common.Discount
{
	/// <summary>
	/// Recalculate Prices and Discounts filter
	/// </summary>
	[Serializable]
	public class RecalcDiscountsParamFilter : IBqlTable
	{
		#region RecalcTarget
		[PXDBString(3, IsFixed = true)]
		[PXDefault(AllLines)]
		[PXStringList(
			new[] {CurrentLine, AllLines},
			new[] {AR.Messages.CurrentLine, AR.Messages.AllLines})]
		[PXUIField(DisplayName = "Recalculate")]
		public virtual String RecalcTarget { get; set; }
		public abstract class recalcTarget : IBqlField { }

		public const string CurrentLine = "LNE";
		public const string AllLines = "ALL";
		#endregion
		#region RecalcUnitPrices
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Set Current Unit Prices", Visible = true)]
		public virtual Boolean? RecalcUnitPrices { get; set; }
		public abstract class recalcUnitPrices : IBqlField { }
		#endregion
		#region OverrideManualPrices
		[PXDBBool]
		[PXDefault(false)]
		[PXUIEnabled(typeof(recalcUnitPrices))]
		[PXFormula(typeof(Switch<Case<Where<recalcUnitPrices, Equal<False>>, False>, overrideManualPrices>))]
		[PXUIField(DisplayName = "Override Manual Prices", Visible = true)]
		public virtual Boolean? OverrideManualPrices { get; set; }
		public abstract class overrideManualPrices : IBqlField { }
		#endregion
		#region RecalcDiscounts
		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Recalculate Discounts")]
		public virtual Boolean? RecalcDiscounts { get; set; }
		public abstract class recalcDiscounts : IBqlField { }
		#endregion
		#region OverrideManualDiscounts
		[PXDBBool]
		[PXDefault(false)]
		[PXUIEnabled(typeof(recalcDiscounts))]
		[PXFormula(typeof(Switch<Case<Where<recalcDiscounts, Equal<False>>, False>, overrideManualDiscounts>))]
		[PXUIField(DisplayName = "Override Manual Line Discounts")]
		public virtual Boolean? OverrideManualDiscounts { get; set; }
		public abstract class overrideManualDiscounts : IBqlField { }
		#endregion
		#region OverrideManualDocGroupDiscounts
		[PXDBBool]
		[PXDefault(false)]
		[PXUIEnabled(typeof(recalcDiscounts))]
		[PXFormula(typeof(Switch<Case<Where<recalcDiscounts, Equal<False>>, False>, overrideManualDocGroupDiscounts>))]
		[PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
		public virtual Boolean? OverrideManualDocGroupDiscounts { get; set; }
		public abstract class overrideManualDocGroupDiscounts : IBqlField { }
		#endregion
		#region UseRecalcFilter
		[PXDBBool]
		[PXDefault(false)]
		public virtual Boolean? UseRecalcFilter { get; set; }
		public abstract class useRecalcFilter : IBqlField { }
		#endregion
	}
}