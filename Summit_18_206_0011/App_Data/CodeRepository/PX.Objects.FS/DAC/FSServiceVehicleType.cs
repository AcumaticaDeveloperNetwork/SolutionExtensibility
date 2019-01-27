using System;
using PX.Data;
using PX.Objects.IN;

namespace PX.Objects.FS
{
	[System.SerializableAttribute]
    [PXPrimaryGraph(typeof(NonStockItemMaint))]
	public class FSServiceVehicleType : PX.Data.IBqlTable
	{
		#region ServiceID
		public abstract class serviceID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
        [PXDBLiteDefault(typeof(InventoryItem.inventoryID))]
        [PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<FSServiceVehicleType.serviceID>>>>))]
        public virtual int? ServiceID { get; set; }
		#endregion
		#region VehicleTypeID
		public abstract class vehicleTypeID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Vehicle Type ID")]
        [PXSelector(typeof(Search<FSVehicleType.vehicleTypeID>), SubstituteKey = typeof(FSVehicleType.vehicleTypeCD))]
        public virtual int? VehicleTypeID { get; set; }
        #endregion
        #region PriorityPreference
        public abstract class priorityPreference : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(1)]
        [PXUIField(DisplayName = "Priority Preference", Required = true)]
        public virtual int? PriorityPreference { get; set; }
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