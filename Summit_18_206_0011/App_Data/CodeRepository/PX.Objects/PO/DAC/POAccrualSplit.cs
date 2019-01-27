using System;
using PX.Data;
using PX.Objects.AP;

namespace PX.Objects.PO
{
	[Serializable]
	[PXCacheName(Messages.POAccrualSplit)]
	public class POAccrualSplit : IBqlTable
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

		#region APDocType
		public abstract class aPDocType : IBqlField
		{
		}
		[APDocType.List]
		[PXDBString(3, IsKey = true, IsFixed = true)]
		[PXDefault]
		public virtual string APDocType
		{
			get;
			set;
		}
		#endregion
		#region APRefNbr
		public abstract class aPRefNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		public virtual string APRefNbr
		{
			get;
			set;
		}
		#endregion
		#region APLineNbr
		public abstract class aPLineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault]
		public virtual int? APLineNbr
		{
			get;
			set;
		}
		#endregion

		#region POReceiptNbr
		public abstract class pOReceiptNbr : IBqlField
		{
		}
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		public virtual string POReceiptNbr
		{
			get;
			set;
		}
		#endregion
		#region POReceiptLineNbr
		public abstract class pOReceiptLineNbr : IBqlField
		{
		}
		[PXDBInt(IsKey = true)]
		[PXDefault]
		public virtual int? POReceiptLineNbr
		{
			get;
			set;
		}
		#endregion

		#region UOM
		public abstract class uOM : IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string UOM
		{
			get;
			set;
		}
		#endregion
		#region AccruedQty
		public abstract class accruedQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? AccruedQty
		{
			get;
			set;
		}
		#endregion
		#region BaseAccruedQty
		public abstract class baseAccruedQty : IBqlField
		{
		}
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? BaseAccruedQty
		{
			get;
			set;
		}
		#endregion
		#region AccruedCost
		public abstract class accruedCost : IBqlField
		{
		}
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? AccruedCost
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
		#region IsReversed
		public abstract class isReversed : IBqlField
		{
		}
		[PXDBBool]
		[PXDefault(false)]
		public virtual bool? IsReversed
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
}
