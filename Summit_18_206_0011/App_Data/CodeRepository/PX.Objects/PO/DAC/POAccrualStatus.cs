using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.IN;

namespace PX.Objects.PO
{
	[Serializable]
	[PXCacheName(Messages.POAccrualStatus)]
	public class POAccrualStatus : IBqlTable
	{
		#region RefNoteID
		public abstract class refNoteID : IBqlField
		{
		}
		[PXDBGuid(IsKey = true)]
		[PXDefault]
		public virtual Guid? RefNoteID
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault]
		public virtual int? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region Type
		public abstract class type : IBqlField
		{
		}
		[PXDBString(1, IsFixed = true, IsKey = true)]
		[PXDefault]
		[POAccrualType.List]
		public virtual string Type
		{
			get;
			set;
		}
		#endregion
		#region LineType
		public abstract class lineType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true)]
		[PXDefault]
		public virtual string LineType
		{
			get;
			set;
		}
		#endregion
		#region OrderType
		public abstract class orderType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string OrderType
		{
			get;
			set;
		}
		#endregion
		#region OrderNbr
		public abstract class orderNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string OrderNbr
		{
			get;
			set;
		}
		#endregion
		#region OrderLineNbr
		public abstract class orderLineNbr : IBqlField
		{
		}
		[PXDBInt]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? OrderLineNbr
		{
			get;
			set;
		}
		#endregion
		#region ReceiptType
		public abstract class receiptType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string ReceiptType
		{
			get;
			set;
		}
		#endregion
		#region ReceiptNbr
		public abstract class receiptNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String ReceiptNbr
		{
			get;
			set;
		}
		#endregion

		#region VendorID
		public abstract class vendorID : IBqlField
		{
		}
		[Vendor]
		[PXDBDefault]
		public virtual int? VendorID
		{
			get;
			set;
		}
		#endregion
		#region PayToVendorID
		public abstract class payToVendorID : IBqlField
		{
		}
		[BasePayToVendor]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? PayToVendorID
		{
			get;
			set;
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : IBqlField
		{
		}
		[AnyInventory]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? InventoryID
		{
			get;
			set;
		}
		#endregion
		#region SubItemID
		public abstract class subItemID : IBqlField
		{
		}
		[SubItem]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? SubItemID
		{
			get;
			set;
		}
		#endregion
		#region SiteID
		public abstract class siteID : IBqlField
		{
		}
		[Site]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? SiteID
		{
			get;
			set;
		}
		#endregion
		#region AcctID
		public abstract class acctID : IBqlField
		{
		}
		[Account]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? AcctID
		{
			get;
			set;
		}
		#endregion
		#region SubID
		public abstract class subID : IBqlField
		{
		}
		[SubAccount]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual int? SubID
		{
			get;
			set;
		}
		#endregion

		#region OrigUOM
		public abstract class origUOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String OrigUOM
		{
			get;
			set;
		}
		#endregion
		#region OrigQty
		public abstract class origQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? OrigQty
		{
			get;
			set;
		}
		#endregion
		#region BaseOrigQty
		public abstract class baseOrigQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BaseOrigQty
		{
			get;
			set;
		}
		#endregion
		#region OrigCuryID
		public abstract class origCuryID : IBqlField
		{
		}
		[PXDBString(5, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string OrigCuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigAmt
		public abstract class curyOrigAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryOrigAmt
		{
			get;
			set;
		}
		#endregion
		#region OrigAmt
		public abstract class origAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? OrigAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigCost
		public abstract class curyOrigCost : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryOrigCost
		{
			get;
			set;
		}
		#endregion
		#region OrigCost
		public abstract class origCost : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? OrigCost
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigDiscAmt
		public abstract class curyOrigDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? CuryOrigDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region OrigDiscAmt
		public abstract class origDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? OrigDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region ReceivedUOM
		public abstract class receivedUOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String ReceivedUOM
		{
			get;
			set;
		}
		#endregion
		#region ReceivedQty
		public abstract class receivedQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? ReceivedQty
		{
			get;
			set;
		}
		#endregion
		#region BaseReceivedQty
		public abstract class baseReceivedQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BaseReceivedQty
		{
			get;
			set;
		}
		#endregion
		#region ReceivedCost
		public abstract class receivedCost : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? ReceivedCost
		{
			get;
			set;
		}
		#endregion
		#region BilledUOM
		public abstract class billedUOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String BilledUOM
		{
			get;
			set;
		}
		#endregion
		#region BilledQty
		public abstract class billedQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? BilledQty
		{
			get;
			set;
		}
		#endregion
		#region BaseBilledQty
		public abstract class baseBilledQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BaseBilledQty
		{
			get;
			set;
		}
		#endregion
		#region BillCuryID
		public abstract class billCuryID : IBqlField
		{
		}
		[PXDBString(5, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string BillCuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryBilledAmt
		public abstract class curyBilledAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryBilledAmt
		{
			get;
			set;
		}
		#endregion
		#region BilledAmt
		public abstract class billedAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BilledAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryBilledCost
		public abstract class curyBilledCost : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryBilledCost
		{
			get;
			set;
		}
		#endregion
		#region BilledCost
		public abstract class billedCost : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BilledCost
		{
			get;
			set;
		}
		#endregion
		#region CuryBilledDiscAmt
		public abstract class curyBilledDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryBilledDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region BilledDiscAmt
		public abstract class billedDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BilledDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region PPVAmt
		public abstract class pPVAmt : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? PPVAmt
		{
			get;
			set;
		}
		#endregion

		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField
		{
		}
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField
		{
		}
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField
		{
		}
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField
		{
		}
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField
		{
		}
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField
		{
		}
		[PXDBLastModifiedByScreenID]
		public virtual String LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField
		{
		}
		[PXDBTimestamp(RecordComesFirst = true)]
		public virtual Byte[] tstamp
		{
			get;
			set;
		}
		#endregion
	}

	[Serializable]
	[PXHidden]
	[PXProjection(typeof(Select4<POAccrualStatus,
		Where<POAccrualStatus.type, Equal<POAccrualType.receipt>>,
		Aggregate<
			GroupBy<POAccrualStatus.orderType,
			GroupBy<POAccrualStatus.orderNbr,
			GroupBy<POAccrualStatus.orderLineNbr,
			Sum<POAccrualStatus.receivedQty,
			Sum<POAccrualStatus.baseReceivedQty,
			Sum<POAccrualStatus.receivedCost,
			Sum<POAccrualStatus.billedQty,
			Sum<POAccrualStatus.baseBilledQty,
			Sum<POAccrualStatus.curyBilledAmt,
			Sum<POAccrualStatus.billedAmt,
			Sum<POAccrualStatus.curyBilledCost,
			Sum<POAccrualStatus.billedCost,
			Sum<POAccrualStatus.billedDiscAmt,
			Sum<POAccrualStatus.curyBilledDiscAmt,
			Sum<POAccrualStatus.pPVAmt>>>>>>>>>>>>>>>>>), Persistent = false)]
	public class POAccrualStatusSummary : IBqlTable
	{
		#region LineType
		public abstract class lineType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true, BqlField = typeof(POAccrualStatus.lineType))]
		public virtual string LineType
		{
			get;
			set;
		}
		#endregion
		#region OrderType
		public abstract class orderType : IBqlField
		{
		}
		[PXDBString(2, IsFixed = true, BqlField = typeof(POAccrualStatus.orderType))]
		public virtual string OrderType
		{
			get;
			set;
		}
		#endregion
		#region OrderNbr
		public abstract class orderNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, BqlField = typeof(POAccrualStatus.orderNbr))]
		public virtual string OrderNbr
		{
			get;
			set;
		}
		#endregion
		#region OrderLineNbr
		public abstract class orderLineNbr : IBqlField
		{
		}
		[PXDBInt(BqlField = typeof(POAccrualStatus.orderLineNbr))]
		public virtual int? OrderLineNbr
		{
			get;
			set;
		}
		#endregion
		#region OrigUOM
		public abstract class origUOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true, BqlField = typeof(POAccrualStatus.origUOM))]
		public virtual String OrigUOM
		{
			get;
			set;
		}
		#endregion
		#region OrigQty
		public abstract class origQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POAccrualStatus.origQty))]
		public virtual decimal? OrigQty
		{
			get;
			set;
		}
		#endregion
		#region BaseOrigQty
		public abstract class baseOrigQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POAccrualStatus.baseOrigQty))]
		public virtual decimal? BaseOrigQty
		{
			get;
			set;
		}
		#endregion
		#region OrigCuryID
		public abstract class origCuryID : IBqlField
		{
		}
		[PXDBString(5, IsUnicode = true, BqlField = typeof(POAccrualStatus.origCuryID))]
		public virtual string OrigCuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigAmt
		public abstract class curyOrigAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.curyOrigAmt))]
		public virtual decimal? CuryOrigAmt
		{
			get;
			set;
		}
		#endregion
		#region OrigAmt
		public abstract class origAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.origAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? OrigAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigCost
		public abstract class curyOrigCost : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.curyOrigCost))]
		public virtual decimal? CuryOrigCost
		{
			get;
			set;
		}
		#endregion
		#region OrigCost
		public abstract class origCost : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.origCost))]
		public virtual decimal? OrigCost
		{
			get;
			set;
		}
		#endregion
		#region CuryOrigDiscAmt
		public abstract class curyOrigDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.curyOrigDiscAmt))]
		public virtual decimal? CuryOrigDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region OrigDiscAmt
		public abstract class origDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.origDiscAmt))]
		public virtual decimal? OrigDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region ReceivedUOM
		public abstract class receivedUOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true, BqlField = typeof(POAccrualStatus.receivedUOM))]
		public virtual String ReceivedUOM
		{
			get;
			set;
		}
		#endregion
		#region ReceivedQty
		public abstract class receivedQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POAccrualStatus.receivedQty))]
		public virtual decimal? ReceivedQty
		{
			get;
			set;
		}
		#endregion
		#region BaseReceivedQty
		public abstract class baseReceivedQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POAccrualStatus.baseReceivedQty))]
		public virtual decimal? BaseReceivedQty
		{
			get;
			set;
		}
		#endregion
		#region ReceivedCost
		public abstract class receivedCost : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.receivedCost))]
		public virtual decimal? ReceivedCost
		{
			get;
			set;
		}
		#endregion
		#region BilledUOM
		public abstract class billedUOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true, BqlField = typeof(POAccrualStatus.billedUOM))]
		public virtual String BilledUOM
		{
			get;
			set;
		}
		#endregion
		#region BilledQty
		public abstract class billedQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POAccrualStatus.billedQty))]
		public virtual decimal? BilledQty
		{
			get;
			set;
		}
		#endregion
		#region BaseBilledQty
		public abstract class baseBilledQty : IBqlField
		{
		}
		[PXDBDecimal(6, BqlField = typeof(POAccrualStatus.baseBilledQty))]
		public virtual decimal? BaseBilledQty
		{
			get;
			set;
		}
		#endregion
		#region BillCuryID
		public abstract class billCuryID : IBqlField
		{
		}
		[PXDBString(5, IsUnicode = true, BqlField = typeof(POAccrualStatus.billCuryID))]
		public virtual string BillCuryID
		{
			get;
			set;
		}
		#endregion
		#region CuryBilledAmt
		public abstract class curyBilledAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.curyBilledAmt))]
		public virtual decimal? CuryBilledAmt
		{
			get;
			set;
		}
		#endregion
		#region BilledAmt
		public abstract class billedAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.billedAmt))]
		public virtual decimal? BilledAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryBilledCost
		public abstract class curyBilledCost : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.curyBilledCost))]
		public virtual decimal? CuryBilledCost
		{
			get;
			set;
		}
		#endregion
		#region BilledCost
		public abstract class billedCost : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.billedCost))]
		public virtual decimal? BilledCost
		{
			get;
			set;
		}
		#endregion
		#region CuryBilledDiscAmt
		public abstract class curyBilledDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.curyBilledDiscAmt))]
		public virtual decimal? CuryBilledDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region BilledDiscAmt
		public abstract class billedDiscAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.billedDiscAmt))]
		public virtual decimal? BilledDiscAmt
		{
			get;
			set;
		}
		#endregion
		#region PPVAmt
		public abstract class pPVAmt : IBqlField
		{
		}
		[PXDBDecimal(4, BqlField = typeof(POAccrualStatus.pPVAmt))]
		public virtual decimal? PPVAmt
		{
			get;
			set;
		}
		#endregion
	}
}
