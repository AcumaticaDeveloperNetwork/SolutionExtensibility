using System;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class DriverSelectionFilter : IBqlTable
    {
        #region RouteDocumentID
        public abstract class routeDocumentID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Route Document ID", Enabled = false)]
        [PXSelector(typeof(FSRouteDocument.routeDocumentID), SubstituteKey = typeof(FSRouteDocument.refNbr))]
        public virtual int? RouteDocumentID { get; set; }
        #endregion

        #region ShowUnassignedDrivers
        public abstract class showUnassignedDrivers : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Show Available Drivers for this Route only")]
        public virtual bool? ShowUnassignedDrivers { get; set; }
        #endregion
        #region VehicleTypeID
        public abstract class vehicleTypeID : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? VehicleTypeID { get; set; }
        #endregion
    }
}
