using System;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class RouteServiceContractFilter : IBqlTable
    {
        #region RouteID
        public abstract class routeID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Route")]
        [FSSelectorRouteID]
        [PXFormula(typeof(Default<RouteServiceContractFilter.vehicleTypeID>))]
        public virtual int? RouteID { get; set; }
        #endregion
        #region FromDate
        public abstract class fromDate : PX.Data.IBqlField
        {
        }

        [PXDateAndTime(UseTimeZone = false)]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Generate from", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? FromDate { get; set; }
        #endregion
        #region ToDate
        public abstract class toDate : PX.Data.IBqlField
        {
        }

        [PXDateAndTime(UseTimeZone = false)]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Generate Up To", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? ToDate { get; set; }
        #endregion
        #region VehicleTypeID
        public abstract class vehicleTypeID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Vehicle Type")]
        [PXSelector(typeof(FSVehicleType.vehicleTypeID), SubstituteKey = typeof(FSVehicleType.vehicleTypeCD), DescriptionField = typeof(FSVehicleType.descr))]
        public virtual int? VehicleTypeID { get; set; }
        #endregion
        #region PreassignedDriver
        public abstract class preassignedDriver : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Preassign Driver")]
        public virtual bool? PreassignedDriver { get; set; }
        #endregion
        #region PreassignedDriver
        public abstract class preassignedVehicle : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Preassign Vehicle")]
        public virtual bool? PreassignedVehicle { get; set; }
        #endregion
        #region ScheduleID
        //Not shown on screen: Needed to filter one schedule when RouteScheduleProcess is launched from RouteContractScheduleEntry
        public abstract class scheduleID : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? ScheduleID { get; set; }
        #endregion
    }
}