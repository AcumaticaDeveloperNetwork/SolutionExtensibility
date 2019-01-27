using System;
using PX.Data;

namespace PX.Objects.FS
{
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.GEOGRAPHIC_ZONE_EMPLOYEE)]
	public class FSGeoZoneEmp : PX.Data.IBqlTable
	{
		#region GeoZoneID
		public abstract class geoZoneID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Service Area ID")]
        [PXDBLiteDefault(typeof(FSGeoZone.geoZoneID))]
        [PXParent(typeof(Select<FSGeoZone, Where<FSGeoZone.geoZoneID, Equal<Current<FSGeoZoneEmp.geoZoneID>>>>))]
		public virtual int? GeoZoneID { get; set; }
		#endregion
		#region EmployeeID
		public abstract class employeeID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Employee ID")]
        [FSSelector_Employee_All]
		public virtual int? EmployeeID { get; set; }
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
        [PXUIField(DisplayName = "Created By Screen ID")]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
        [PXUIField(DisplayName = "Last Modified By Screen ID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
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
