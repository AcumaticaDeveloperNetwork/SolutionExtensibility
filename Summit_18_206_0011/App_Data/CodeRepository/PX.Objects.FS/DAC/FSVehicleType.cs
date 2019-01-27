using System;
using PX.Data;
﻿
namespace PX.Objects.FS
{
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.VEHICLE_TYPE)]
    [PXPrimaryGraph(typeof(VehicleTypeMaint))]
	public class FSVehicleType : PX.Data.IBqlTable
	{
		#region VehicleTypeID
		public abstract class vehicleTypeID : PX.Data.IBqlField
		{
		}
		[PXDBIdentity]
        public virtual int? VehicleTypeID { get; set; }
		#endregion
		#region VehicleTypeCD
		public abstract class vehicleTypeCD : PX.Data.IBqlField
		{
		}               
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXDefault] 
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "Vehicle Type ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<FSVehicleType.vehicleTypeCD>))]
        public virtual string VehicleTypeCD { get; set; }
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		[PXDBString(60, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string Descr { get; set; }
		#endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }
        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0])]
        public virtual Guid? NoteID { get; set; }
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