using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;

namespace PX.Objects.PO
{
	[Serializable]
	[PXHidden]
	public partial class POReceiptAPDoc : IBqlTable
	{
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
		}

		[PXString(3, IsKey = true, IsFixed = true)]
		[APDocType.List()]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
		public virtual String DocType
		{
			get;
			set;
		}
		#endregion

		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}

		[PXString(15, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
		[PXSelector(typeof(Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<Optional<docType>>>>), Filterable = true)]
		public virtual String RefNbr
		{
			get;
			set;
		}
		#endregion
		
		#region DocDate
		public abstract class docDate : PX.Data.IBqlField
		{
		}

		[PXDate()]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion

		#region Status
		public abstract class status : IBqlField { }

		[PXString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[APDocStatus.List]
		public virtual string Status
		{
			get;
			set;
		}
		#endregion

		#region TotalQty
		public abstract class totalQty : PX.Data.IBqlField
		{
		}
		[PXQuantity()]
		[PXUIField(DisplayName = "Billed Qty.", Enabled = false)]
		public virtual Decimal? TotalQty
		{
			get;
			set;
		}
		#endregion

		#region TotalAmt
		public abstract class totalAmt : PX.Data.IBqlField
		{
		}

		[PXBaseCury()]
		[PXUIField(DisplayName = "Billed Amt.", Enabled = false)]
		public virtual Decimal? TotalAmt
		{
			get;
			set;
		}
		#endregion

		#region AccruedQty
		public abstract class accruedQty : PX.Data.IBqlField
		{
		}

		[PXQuantity()]
		[PXUIField(DisplayName = "Accrued Qty.")]
		public virtual Decimal? AccruedQty
		{
			get;
			set;
		}
		#endregion

		#region AccruedAmt
		public abstract class accruedAmt : PX.Data.IBqlField
		{
		}

		[PXBaseCury()]
		[PXUIField(DisplayName = "Accrued Amt.")]
		public virtual Decimal? AccruedAmt
		{
			get;
			set;
		}
		#endregion

		#region TotalPPVAmt
		public abstract class totalPPVAmt : PX.Data.IBqlField
		{
		}

		[PXBaseCury()]
		[PXUIField(DisplayName = "PPV Amt")]
		public virtual Decimal? TotalPPVAmt
		{
			get;
			set;
		}
		#endregion

		#region StatusText
		public abstract class statusText : PX.Data.IBqlField
		{
		}
		[PXString]
		public virtual String StatusText
		{
			get;
			set;
		}
		#endregion
	}
}
