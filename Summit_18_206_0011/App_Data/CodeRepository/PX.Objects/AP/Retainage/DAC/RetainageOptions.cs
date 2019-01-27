using System;

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CM;
using PX.Objects.CS;

namespace PX.Objects.AP
{
	[Serializable]
	public partial class RetainageOptions : IBqlTable
	{
		#region DocDate
		public abstract class docDate : IBqlField { }

		[PXDBDate]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField { }

		[APOpenPeriod(typeof(RetainageOptions.docDate))]
		[PXDefault]
		[PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string FinPeriodID
		{
			get;
			set;
		}
		#endregion
		#region InvoiceNbr
		public abstract class invoiceNbr : IBqlField { }

		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Retainage Vendor Ref.", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string InvoiceNbr
		{
			get;
			set;
		}
		#endregion

		#region CuryRetainageTotal
		public abstract class curyRetainageTotal : IBqlField { }

		[PXDBCurrency(typeof(APRegister.curyInfoID), typeof(RetainageOptions.retainageTotal))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryRetainageTotal
		{
			get;
			set;
		}
		#endregion
		#region RetainageTotal
		public abstract class retainageTotal : IBqlField { }

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? RetainageTotal
		{
			get;
			set;
		}
		#endregion

		#region RetainagePct
		public abstract class retainagePct : IBqlField { }

		[DBRetainagePercent(
			typeof(True),
			typeof(decimal100),
			typeof(RetainageOptions.curyRetainageTotal),
			typeof(RetainageOptions.curyRetainageAmt),
			typeof(RetainageOptions.retainagePct),
			DisplayName = "Percent to Release")]
		public virtual decimal? RetainagePct
		{
			get;
			set;
		}
		#endregion
		#region CuryRetainageAmt
		public abstract class curyRetainageAmt : IBqlField { }

		[DBRetainageAmount(
			typeof(APInvoice.curyInfoID),
			typeof(RetainageOptions.curyRetainageTotal),
			typeof(RetainageOptions.curyRetainageAmt),
			typeof(RetainageOptions.retainageAmt),
			typeof(RetainageOptions.retainagePct),
			DisplayName = "Retainage to Release")]
		public virtual decimal? CuryRetainageAmt
		{
			get;
			set;
		}
		#endregion
		#region RetainageAmt
		public abstract class retainageAmt : IBqlField { }

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? RetainageAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryRetainageUnreleasedAmt
		public abstract class curyRetainageUnreleasedAmt : IBqlField { }

		[PXDBCurrency(typeof(APInvoice.curyInfoID), typeof(RetainageOptions.retainageUnreleasedAmt))]
		[PXUIField(DisplayName = "Unreleased Retainage", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Sub<RetainageOptions.curyRetainageTotal, RetainageOptions.curyRetainageAmt>))]
		public virtual decimal? CuryRetainageUnreleasedAmt
		{
			get;
			set;
		}
		#endregion
		#region RetainageUnreleasedAmt
		public abstract class retainageUnreleasedAmt : IBqlField { }

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? RetainageUnreleasedAmt
		{
			get;
			set;
		}
		#endregion
	}
}
