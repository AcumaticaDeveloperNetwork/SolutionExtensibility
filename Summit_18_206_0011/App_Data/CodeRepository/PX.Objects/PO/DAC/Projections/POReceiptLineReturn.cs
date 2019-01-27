using System;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;

namespace PX.Objects.PO
{
	[PXProjection(typeof(Select2<POReceiptLine,
		InnerJoin<POReceipt, On<POReceipt.receiptNbr, Equal<POReceiptLine.receiptNbr>>>,
		Where<POReceipt.receiptType, Equal<POReceiptType.poreceipt>, And<POReceipt.released, Equal<True>,
			And<Sub<POReceiptLine.baseReceiptQty, POReceiptLine.baseReturnedQty>, Greater<decimal0>>>>>), Persistent = false)]
	[Serializable]
	[PXHidden]
	public partial class POReceiptLineReturn : IBqlTable, IPOReturnLineSource
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
		{
			get;
			set;
		}
		#endregion
		#region POType
		public abstract class pOType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true, BqlField = typeof(POReceiptLine.pOType))]
		[POOrderType.List]
		[PXUIField(DisplayName = "Order Type", Enabled = false)]
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
		[PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
		[PO.RefNbr(typeof(Search<POOrder.orderNbr>), Filterable = true)]
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
		[PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
		public virtual int? POLineNbr
		{
			get;
			set;
		}
		#endregion
		#region ReceiptNbr
		public abstract class receiptNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof(POReceipt.receiptNbr))]
		[POReceiptType.RefNbr(typeof(Search<POReceipt.receiptNbr>), Filterable = true)]
		[PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
		public virtual string ReceiptNbr
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true, BqlField = typeof(POReceiptLine.lineNbr))]
		[PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
		public virtual int? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region ReceiptType
		public abstract class receiptType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true, BqlField = typeof(POReceipt.receiptType))]
		[POReceiptType.List]
		[PXUIField(DisplayName = "Type", Enabled = false)]
		public virtual string ReceiptType
		{
			get;
			set;
		}
		#endregion
		#region VendorID
		public abstract class vendorID : IBqlField
		{
		}
		[VendorActive(Enabled = false, DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true, BqlField = typeof(POReceipt.vendorID))]
		public virtual int? VendorID
		{
			get;
			set;
		}
		#endregion
		#region VendorLocationID
		public abstract class vendorLocationID : IBqlField
		{
		}
		[LocationID(typeof(Where<Location.bAccountID, Equal<Current<POReceipt.vendorID>>>), DescriptionField = typeof(Location.descr), Enabled = false, BqlField = typeof(POReceipt.vendorLocationID))]
		public virtual int? VendorLocationID
		{
			get;
			set;
		}
		#endregion
		#region ReceiptDate
		public abstract class receiptDate : IBqlField
		{
		}
		[PXDBDate(BqlField = typeof(POReceipt.receiptDate))]
		[PXUIField(DisplayName = "Date", Enabled = false)]
		public virtual DateTime? ReceiptDate
		{
			get;
			set;
		}
		#endregion
		#region InvoiceNbr
		public abstract class invoiceNbr : IBqlField
		{
		}
		[PXDBString(40, IsUnicode = true, BqlField = typeof(POReceipt.invoiceNbr))]
		[PXUIField(DisplayName = "Vendor Ref.", Enabled = false)]
		public virtual string InvoiceNbr
		{
			get;
			set;
		}
		#endregion
		#region SiteID
		public abstract class siteID : IBqlField
		{
		}
		[Site(Enabled = false, BqlField = typeof(POReceiptLine.siteID))]
		public virtual int? SiteID
		{
			get;
			set;
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : IBqlField
		{
		}
		[Inventory(Enabled = false, BqlField = typeof(POReceiptLine.inventoryID))]
		public virtual int? InventoryID
		{
			get;
			set;
		}
		#endregion
		#region UOM
		public abstract class uOM : IBqlField
		{
		}
		[INUnit(typeof(POReceiptLine.inventoryID), Enabled = false, BqlField = typeof(POReceiptLine.uOM))]
		public virtual string UOM
		{
			get;
			set;
		}
		#endregion
		#region ReceiptQty
		public abstract class receiptQty : IBqlField
		{
		}
		[PXDBQuantity(BqlField = typeof(POReceiptLine.receiptQty))]
		[PXUIField(DisplayName = "Receipt Qty.", Enabled = false)]
		public virtual decimal? ReceiptQty
		{
			get;
			set;
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

		#region LineType
		public abstract class lineType : IBqlField
		{
		}
		[PXDBString(POReceiptLine.lineType.Length, IsFixed = true, BqlField = typeof(POReceiptLine.lineType))]
		public virtual String LineType
		{
			get;
			set;
		}
		#endregion
		#region SubItemID
		public abstract class subItemID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.subItemID))]
		public virtual int? SubItemID
		{
			get;
			set;
		}
		#endregion
		#region LocationID
		public abstract class locationID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.locationID))]
		public virtual int? LocationID
		{
			get;
			set;
		}
		#endregion
		#region LotSerialNbr
		public abstract class lotSerialNbr : IBqlField
		{
		}
		[PXDBString(INLotSerialStatus.lotSerialNbr.LENGTH, IsUnicode = true, BqlField = typeof(POReceiptLine.lotSerialNbr))]
		public virtual String LotSerialNbr
		{
			get;
			set;
		}
		#endregion
		#region ExpireDate
		public abstract class expireDate : IBqlField
		{
		}
		[PXDBDate(BqlField = typeof(POReceiptLine.expireDate))]
		public virtual DateTime? ExpireDate
		{
			get;
			set;
		}
		#endregion
		#region BaseReturnedQty
		public abstract class baseReturnedQty : IBqlField
		{
		}
		[PXDBQuantity(BqlField = typeof(POReceiptLine.baseReturnedQty))]
		public virtual decimal? BaseReturnedQty
		{
			get;
			set;
		}
		#endregion
		#region ReturnedQty
		public abstract class returnedQty : IBqlField
		{
		}
		[PXQuantity]
		[PXUIField(DisplayName = "Returned Qty.", Enabled = false)]
		public virtual decimal? ReturnedQty
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public abstract class costCodeID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.costCodeID))]
		public virtual int? CostCodeID
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
		#region POAccrualAcctID
		public abstract class pOAccrualAcctID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.pOAccrualAcctID))]
		public virtual int? POAccrualAcctID
		{
			get;
			set;
		}
		#endregion
		#region POAccrualSubID
		public abstract class pOAccrualSubID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.pOAccrualSubID))]
		public virtual int? POAccrualSubID
		{
			get;
			set;
		}
		#endregion
		#region ProjectID
		public abstract class projectID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.projectID))]
		public virtual int? ProjectID
		{
			get;
			set;
		}
		#endregion
		#region TaskID
		public abstract class taskID : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POReceiptLine.taskID))]
		public virtual int? TaskID
		{
			get;
			set;
		}
		#endregion
		#region TranDesc
		public abstract class tranDesc : IBqlField
		{
		}
		[PXDBString(256, IsUnicode = true, BqlField = typeof(POReceiptLine.tranDesc))]
		public virtual string TranDesc
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
		#region UnitCost
		public abstract class unitCost : IBqlField
		{
		}
		[PXDBPriceCost(BqlField = typeof(POReceiptLine.unitCost))]
		public virtual decimal? UnitCost
		{
			get;
			set;
		}
		#endregion
	}
}
