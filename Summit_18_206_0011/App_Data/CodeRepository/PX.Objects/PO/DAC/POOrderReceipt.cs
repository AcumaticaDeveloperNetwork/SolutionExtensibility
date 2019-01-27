using System;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.IN;
using PX.Objects.CM;

namespace PX.Objects.PO
{
	[System.SerializableAttribute()]
	public partial class POOrderReceipt : PX.Data.IBqlTable
	{
		#region ReceiptNbr
		public abstract class receiptNbr : PX.Data.IBqlField
		{
		}
		protected String _ReceiptNbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(POReceipt.receiptNbr))]
		[PXDefault()]
		[PXParent(typeof(Select<POReceipt,Where<POReceipt.receiptNbr,Equal<Current<POOrderReceipt.receiptNbr>>>>))]
		public virtual String ReceiptNbr
		{
			get
			{
				return this._ReceiptNbr;
			}
			set
			{
				this._ReceiptNbr = value;
			}
		}
		#endregion
		#region POType
		public abstract class pOType : PX.Data.IBqlField
		{
		}
		protected String _POType;
		[PXDBString(2, IsKey = true, IsFixed = true)]
		[PXDefault()]
		[POOrderType.List()]
		[PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, Required = false)]
		public virtual String POType
		{
			get
			{
				return this._POType;
			}
			set
			{
				this._POType = value;
			}
		}
		#endregion
		#region PONbr
		public abstract class pONbr : PX.Data.IBqlField
		{
		}
		protected String _PONbr;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Order Nbr.", Visibility = PXUIVisibility.SelectorVisible, Required = false)]
		[PO.RefNbr(typeof(Search<POOrder.orderNbr>), Filterable = true)]
		public virtual String PONbr
		{
			get
			{
				return this._PONbr;
			}
			set
			{
				this._PONbr = value;
			}
		}
		#endregion
		#region ReceiptNoteID
		public abstract class receiptNoteID : PX.Data.IBqlField
		{
		}
		protected Guid? _ReceiptNoteID;
		[PXDBGuid(IsImmutable = true)]
		[PXDefault(typeof(POReceipt.noteID), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Guid? ReceiptNoteID
		{
			get
			{
				return this._ReceiptNoteID;
			}
			set
			{
				this._ReceiptNoteID = value;
			}
		}
		#endregion
		#region OrderNoteID
		public abstract class orderNoteID : PX.Data.IBqlField
		{
		}
		protected Guid? _OrderNoteID;
		[SO.SOOrderShipment.CopiedNoteID]
		public virtual Guid? OrderNoteID
		{
			get
			{
				return this._OrderNoteID;
			}
			set
			{
				this._OrderNoteID = value;
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
	}

	[PXProjection(typeof(Select3<POOrderReceipt,
								LeftJoin<POOrder,
									 On<POOrderReceipt.pOType, Equal<POOrder.orderType>,
									And<POOrderReceipt.pONbr, Equal<POOrder.orderNbr>>>>,
								OrderBy<Asc<POOrderReceipt.pONbr>>>), Persistent = false)]
	[SerializableAttribute()]
	public partial class POOrderReceiptLink : POOrderReceipt
	{
		#region ReceiptNbr
		public new abstract class receiptNbr : PX.Data.IBqlField
		{
		}
		#endregion
		#region OrderType
		public new abstract class pOType : PX.Data.IBqlField
		{
		}
		#endregion
		#region OrderNbr
		public new abstract class pONbr : PX.Data.IBqlField
		{
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected String _Status;
		[PXDBString(1, IsFixed = true, BqlField = typeof(POOrder.status))]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[POOrderStatus.List()]
		public virtual String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region ReceiptNoteID
		public new abstract class receiptNoteID : PX.Data.IBqlField
		{
		}
		#endregion
		#region OrderNoteID
		public new abstract class orderNoteID : PX.Data.IBqlField
		{
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : PX.Data.IBqlField
		{
		}
		protected String _TaxZoneID;
		[PXDBString(10, IsUnicode = true, BqlField = typeof(POOrder.taxZoneID))]
		[PXUIField(DisplayName = "Vendor Tax Zone", Visibility = PXUIVisibility.Visible)]
		public virtual String TaxZoneID
		{
			get
			{
				return this._TaxZoneID;
			}
			set
			{
				this._TaxZoneID = value;
			}
		}
		#endregion
		#region TermsID
		public abstract class termsID : IBqlField { }
		[PXDBString(10, IsUnicode = true, BqlField = typeof(POOrder.termsID))]
		[PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.Visible)]
		public virtual string TermsID { get; set; }
		#endregion
		#region PayToVendorID
		public abstract class payToVendorID : IBqlField { }
		[POOrderPayToVendor(CacheGlobal = true, Filterable = true, BqlField = typeof(POOrder.payToVendorID))]
		public virtual int? PayToVendorID { get; set; }
		#endregion
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof(POOrder.curyID))]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		protected Int64? _CuryInfoID;
		[PXDBLong(BqlField = typeof(POOrder.curyInfoID))]
		public virtual Int64? CuryInfoID
		{
			get
			{
				return this._CuryInfoID;
			}
			set
			{
				this._CuryInfoID = value;
			}
		}
		#endregion
		#region UnbilledOrderQty
		public abstract class unbilledOrderQty : IBqlField
		{
		}
		[PXDBQuantity(BqlField = typeof(POOrder.unbilledOrderQty))]
		[PXUIField(DisplayName = "Unbilled Quantity", Enabled = false)]
		public virtual decimal? UnbilledOrderQty
		{
			get;
			set;
		}
		#endregion
		#region CuryUnbilledOrderTotal
		public abstract class curyUnbilledOrderTotal : IBqlField
		{
		}
		[PXDBCurrency(typeof(POOrderReceiptLink.curyInfoID), typeof(POOrderReceiptLink.unbilledOrderTotal), BqlField = typeof(POOrder.curyUnbilledOrderTotal))]
		[PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
		public virtual decimal? CuryUnbilledOrderTotal
		{
			get;
			set;
		}
		#endregion
		#region UnbilledOrderTotal
		public abstract class unbilledOrderTotal : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POOrder.unbilledOrderTotal))]
		public virtual decimal? UnbilledOrderTotal
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public new abstract class Tstamp : PX.Data.IBqlField
		{
		}
		#endregion
	}
}
