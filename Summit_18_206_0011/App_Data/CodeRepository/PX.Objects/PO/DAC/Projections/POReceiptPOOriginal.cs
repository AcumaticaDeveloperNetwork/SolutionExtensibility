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
	public partial class POReceiptPOOriginal : IBqlTable
	{
		#region ReceiptType
		public abstract class receiptType : PX.Data.IBqlField
		{
		}
		[PXDBString(2, IsFixed = true, IsKey = true, InputMask = "", BqlField = typeof(POReceipt.receiptType))]
		[POReceiptType.List()]
		[PXUIField(DisplayName = "PR Type")]
		public virtual String ReceiptType
		{
			get;
			set;
		}
		#endregion

		#region ReceiptNbr
		public abstract class receiptNbr : PX.Data.IBqlField
		{
			public const string DisplayName = "PR Nbr.";
		}
		protected String _ReceiptNbr;
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof(POReceipt.receiptNbr))]
		[PXUIField(DisplayName = receiptNbr.DisplayName, Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Optional<receiptType>>>>), Filterable = true)]
		public virtual String ReceiptNbr
		{
			get;
			set;
		}
		#endregion

		#region DocDate
		public abstract class docDate : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// Date of the document.
		/// </summary>
		[PXDBDate(BqlField = typeof(POReceipt.receiptDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? DocDate
		{
			get;
			set;
		}
		#endregion

		#region FinPeriodID
		public abstract class finPeriodID : PX.Data.IBqlField
		{
		}

		[FinPeriodID]
		[PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String FinPeriodID
		{
			get;
			set;
		}
		#endregion

		#region Status
		public abstract class status : IBqlField { }

		[PXDBString(1, IsFixed = true, BqlField = typeof(POReceipt.status))]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[POReceiptStatus.List]
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
		[PXDBQuantity(BqlField = typeof(POReceiptLine.baseReceiptQty))]
		[PXUIField(DisplayName = "Total Qty.", Enabled = false)]
		public virtual Decimal? TotalQty
		{
			get;
			set;
		}
		#endregion

		#region InvtDocType
		public abstract class invtDocType : PX.Data.IBqlField
		{
		}

		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "IN Doc. Type", Enabled = false)]
		[INDocType.List()]
		public virtual String InvtDocType
		{
			get;
			set;
		}
		#endregion

		#region InvtRefNbr
		public abstract class invtRefNbr : PX.Data.IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "IN Ref. Nbr.", Enabled = false)]
		[PXSelector(typeof(Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<invtDocType>>>>))]
		public virtual String InvtRefNbr
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
