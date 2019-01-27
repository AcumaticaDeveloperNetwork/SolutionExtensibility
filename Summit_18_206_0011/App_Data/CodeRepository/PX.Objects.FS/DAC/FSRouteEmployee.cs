using PX.Data;
using System;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSRouteEmployee : PX.Data.IBqlTable
    {
        #region RouteID
        public abstract class routeID : PX.Data.IBqlField
        {
        }

        [PXDefault]
        [PXDBInt(IsKey = true)]
        [PXDBLiteDefault(typeof(FSRoute.routeID))]
        [PXParent(typeof(Select<FSRoute, Where<FSRoute.routeID, Equal<Current<FSRouteEmployee.routeID>>>>))]
        public virtual int? RouteID { get; set; }
        #endregion
        #region EmployeeID
        public abstract class employeeID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Employee ID", Required = true)]
        [FSSelector_Driver_All]
        public virtual int? EmployeeID { get; set; }
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
