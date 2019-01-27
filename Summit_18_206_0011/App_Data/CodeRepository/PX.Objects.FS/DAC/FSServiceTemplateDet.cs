using System;
using PX.Data;
using PX.Objects.IN;

namespace PX.Objects.FS
{
	[Serializable]
	public class FSServiceTemplateDet : PX.Data.IBqlTable
	{
		#region ServiceTemplateID
		public abstract class serviceTemplateID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
        [PXParent(typeof(Select<FSServiceTemplate, Where<FSServiceTemplate.serviceTemplateID, Equal<Current<FSServiceTemplateDet.serviceTemplateID>>>>))]
        [PXDBLiteDefault(typeof(FSServiceTemplate.serviceTemplateID))]
        [PXUIField(DisplayName = "Service Template ID")]
        public virtual int? ServiceTemplateID { get; set; }
		#endregion
		#region ServiceTemplateDetID
		public abstract class serviceTemplateDetID : PX.Data.IBqlField
		{
		}

		[PXDBIdentity(IsKey = true)]
		[PXUIField(Enabled = false)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? ServiceTemplateDetID { get; set; }
		#endregion
		#region LineType
        public abstract class lineType : PX.Data.IBqlField
		{
		}

		[PXDBString(5, IsFixed = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Line Type")]
        public virtual string LineType { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<lineType>))]
        [InventoryIDByLineType(typeof(lineType), Filterable = true)]
        [PXRestrictor(typeof(
                        Where<
                            InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>,
                            Or<FSxServiceClass.requireRoute, Equal<True>,
                            Or<Current<FSSrvOrdType.requireRoute>, Equal<False>>>>),
                TX.Error.NONROUTE_SERVICE_CANNOT_BE_HANDLED_WITH_ROUTE_SRVORDTYPE)]
        [PXRestrictor(typeof(
                        Where<
                            InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>,
                            Or<FSxServiceClass.requireRoute, Equal<False>,
                            Or<Current<FSSrvOrdType.requireRoute>, Equal<True>>>>),
                TX.Error.ROUTE_SERVICE_CANNOT_BE_HANDLED_WITH_NONROUTE_SRVORDTYPE)]
        public virtual int? InventoryID { get; set; }
        #endregion
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}

        [PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "1.0")]
		[PXUIField(DisplayName = "Quantity")]
        public virtual decimal? Qty { get; set; }
		#endregion
		#region TranDesc
		public abstract class tranDesc : PX.Data.IBqlField
		{
		}

		[PXDBString(Common.Constants.TranDescLength, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Selector<inventoryID, InventoryItem.descr>))]
		[PXUIField(DisplayName = "Transaction Description")]
        public virtual string TranDesc { get; set; }
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}

		[PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}

		[PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID { get; set; }
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}

		[PXDBCreatedDateTime]
        public virtual DateTime? CreatedDateTime { get; set; }
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}

		[PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
		#endregion
	}
}