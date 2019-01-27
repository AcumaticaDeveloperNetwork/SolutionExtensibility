using PX.Data;
using PX.Objects.CM;
using PX.Objects.IN;
using System;
using PX.Objects.PO;

namespace PX.Objects.AP
{
	public partial class APInvoiceEntry
	{
		/// <summary>
		/// POOrder + Unbilled Service Items Projection
		/// </summary>
		[Obsolete(Common.Messages.WillBeRemovedInAcumatica2019R1)]
		[Serializable]

		public partial class POOrderRS : POOrder
		{
			#region Selected
			public new abstract class selected : PX.Data.IBqlField
			{
			}
			#endregion

			#region CuryInfoID
			public new abstract class curyInfoID : IBqlField
			{
			}
			#endregion
			#region CuryUnbilledOrderTotal
			public abstract new class curyUnbilledOrderTotal : IBqlField
			{
			}
			[PXDBCurrency(typeof(POOrderRS.curyInfoID), typeof(POOrderRS.unbilledOrderTotal))]
			[PXUIField(DisplayName = "Unbilled Amt.", Enabled = false)]
			public override decimal? CuryUnbilledOrderTotal
			{
				get;
				set;
			}
			#endregion
			#region UnbilledOrderTotal
			public abstract new class unbilledOrderTotal : IBqlField
			{
			}
			[PXDBDecimal(4)]
			[PXDefault(TypeCode.Decimal, "0.0")]
			public override decimal? UnbilledOrderTotal
			{
				get;
				set;
			}
			#endregion
			#region UnbilledOrderQty
			public abstract new class unbilledOrderQty : IBqlField
			{
			}
			[PXDBQuantity]
			[PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
			public override decimal? UnbilledOrderQty
			{
				get;
				set;
			}
			#endregion
		}
	}
}