using PX.Data;
using PX.Objects.CM;
using PX.Objects.IN;
using System;

namespace PX.Objects.PO
{
	/// <summary>
	/// This class is used for Update of unbilled amounts in POReceiptLine ( during Release AP Document process)
	/// </summary>
	[PXProjection(typeof(Select<POReceiptLine>), Persistent = true)]
	[Serializable]
	public partial class POReceiptLineR1 : IBqlTable, ISortOrder
	{
		#region ReceiptNbr
		public abstract class receiptNbr : PX.Data.IBqlField
		{
		}
		protected String _ReceiptNbr;

		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof(POReceiptLine.receiptNbr))]
		[PXDefault()]
		[PXParent(typeof(Select<POReceipt, Where<POReceipt.receiptNbr, Equal<Current<POReceiptLineR1.receiptNbr>>>>))]
		[PXUIField(DisplayName = "Receipt Nbr.", Visibility = PXUIVisibility.Invisible, Visible = false)]
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
		#region ReceiptType
		public abstract class receiptType : PX.Data.IBqlField
		{
		}
		protected String _ReceiptType;
		[PXDBString(2, IsFixed = true, BqlField = typeof(POReceiptLine.receiptType))]
		[PXDBDefault(typeof(POReceipt.receiptType))]
		public virtual String ReceiptType
		{
			get
			{
				return this._ReceiptType;
			}
			set
			{
				this._ReceiptType = value;
			}
		}
		#endregion

		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true, BqlField = typeof(POReceiptLine.lineNbr))]
		[PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region SortOrder
		public abstract class sortOrder : PX.Data.IBqlField
		{
		}
		protected Int32? _SortOrder;
		[PXDBInt(BqlField = typeof(POReceiptLine.sortOrder))]
		public virtual Int32? SortOrder
		{
			get
			{
				return this._SortOrder;
			}
			set
			{
				this._SortOrder = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[Inventory(Filterable = true, BqlField = typeof(POReceiptLine.inventoryID))]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region SubItemID
		public abstract class subItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubItemID;
		[SubItem(BqlField = typeof(POReceiptLine.subItemID))]
		public virtual Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region SiteID
		public abstract class siteID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.siteID))]
		public virtual int? SiteID
		{
			get;
			set;
		}
		#endregion
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[INUnit(typeof(POReceiptLineR1.inventoryID), DisplayName = "UOM", BqlField = typeof(POReceiptLine.uOM))]
		public virtual String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region POType
		public abstract class pOType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true, BqlField = typeof(POReceiptLine.pOType))]
		public virtual string POType
		{
			get;
			set;
		}
		#endregion
		#region PONbr
		public abstract class pONbr : IBqlField
		{
		}
		[PXDBString(POReceiptLine.pONbr.Length, IsUnicode = true, BqlField = typeof(POReceiptLine.pONbr))]
		public virtual string PONbr
		{
			get;
			set;
		}
		#endregion
		#region POLineNbr
		public abstract class pOLineNbr : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.pOLineNbr))]
		public virtual int? POLineNbr
		{
			get;
			set;
		}
		#endregion
		#region POAccrualType
		public abstract class pOAccrualType : IBqlField
		{
		}
		[PXDBString(1, IsFixed = true, BqlField = typeof(POReceiptLine.pOAccrualType))]
		public virtual string POAccrualType
		{
			get;
			set;
		}
		#endregion
		#region ReceiptQty
		public abstract class receiptQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _ReceiptQty;

		[PXDBQuantity(typeof(POReceiptLineR1.uOM), typeof(POReceiptLineR1.baseReceiptQty), HandleEmptyKey = true, BqlField = typeof(POReceiptLine.receiptQty))]
		public virtual Decimal? ReceiptQty
		{
			get
			{
				return this._ReceiptQty;
			}
			set
			{
				this._ReceiptQty = value;
			}
		}
		#endregion
		#region BaseReceiptQty
		public abstract class baseReceiptQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POReceiptLine.baseReceiptQty))]
		public virtual decimal? BaseReceiptQty
		{
			get;
			set;
		}
		#endregion
		#region TranCost
		public abstract class tranCost : IBqlField
		{
		}
		[PXDBBaseCury(BqlField = typeof(POReceiptLine.tranCost))]
		public virtual decimal? TranCost
		{
			get;
			set;
		}
		#endregion
		#region TranCostFinal
		public abstract class tranCostFinal : IBqlField
		{
		}
		[PXDBBaseCury(BqlField = typeof(POReceiptLine.tranCostFinal))]
		public virtual decimal? TranCostFinal
		{
			get;
			set;
		}
		#endregion
		#region InvtMult
		public abstract class invtMult : PX.Data.IBqlField
		{
		}
		protected Int16? _InvtMult;
		[PXDBShort(BqlField = typeof(POReceiptLine.invtMult))]
		public virtual Int16? InvtMult
		{
			get
			{
				return this._InvtMult;
			}
			set
			{
				this._InvtMult = value;
			}
		}
		#endregion
		#region UnbilledQty
		public abstract class unbilledQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _UnbilledQty;
		[PXDBQuantity(typeof(POReceiptLineR1.uOM), typeof(POReceiptLineR1.baseUnbilledQty), HandleEmptyKey = true, BqlField = typeof(POReceiptLine.unbilledQty))]
		[PXFormula(null, typeof(SumCalc<POReceipt.unbilledQty>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
		public virtual Decimal? UnbilledQty
		{
			get
			{
				return this._UnbilledQty;
			}
			set
			{
				this._UnbilledQty = value;
			}
		}
		#endregion
		#region BaseUnbilledQty
		public abstract class baseUnbilledQty : PX.Data.IBqlField
		{
		}
		protected Decimal? _BaseUnbilledQty;
		[PXDBDecimal(6, BqlField = typeof(POReceiptLine.baseUnbilledQty))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BaseUnbilledQty
		{
			get
			{
				return this._BaseUnbilledQty;
			}
			set
			{
				this._BaseUnbilledQty = value;
			}
		}
		#endregion
		#region BillPPVAmt
		public abstract class billPPVAmt : IBqlField
		{
		}
		[PXDBBaseCury(BqlField = typeof(POReceiptLine.billPPVAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? BillPPVAmt
		{
			get;
			set;
		}
		#endregion

		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		protected String _LineType;
		[PXDBString(2, IsFixed = true, BqlField = typeof(POReceiptLine.lineType))]
		public virtual String LineType
		{
			get
			{
				return this._LineType;
			}
			set
			{
				this._LineType = value;
			}
		}
		#endregion
		#region Released
		public abstract class released : IBqlField
		{
		}
		[PXDBBool(BqlField = typeof(POReceiptLine.released))]
		public virtual bool? Released
		{
			get;
			set;
		}
		#endregion
		#region ExpenseAcctID
		public abstract class expenseAcctID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.expenseAcctID))]
		public virtual int? ExpenseAcctID
		{
			get;
			set;
		}
		#endregion
		#region ExpenseSubID
		public abstract class expenseSubID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.expenseSubID))]
		public virtual int? ExpenseSubID
		{
			get;
			set;
		}
		#endregion

		public decimal APSign
		{
			[PXDependsOnFields(typeof(invtMult))]
			get { return this._InvtMult < 0 ? Decimal.MinusOne : Decimal.One; }
		}
	}
}
