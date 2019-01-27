using System;
using PX.Data;
using PX.Objects.CR;

namespace PX.Objects.FS
{
  [System.SerializableAttribute]
  public class RouteAppointmentGPSLocationFilter : PX.Data.IBqlTable
  {
        #region ServiceID
        public abstract class serviceID : PX.Data.IBqlField
        {
        }
  
        [PXInt]
        [ServiceAttribute(DisplayName = "Service")]
        public virtual int? ServiceID { get; set; }
        #endregion
        #region DateFrom
        public abstract class dateFrom : PX.Data.IBqlField
        {
        }
  
        [PXDate]
        [PXUIField(DisplayName = "From Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? DateFrom { get; set; }
        #endregion
        #region DateTo
        public abstract class dateTo : PX.Data.IBqlField
        {
        }
  
        [PXDate]
        [PXUIField(DisplayName = "To Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? DateTo { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Customer", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorBAccountTypeCustomerOrCombined]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region CustomerLocationID
        public abstract class customerLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXFormula(typeof(Default<RouteAppointmentGPSLocationFilter.customerID>))]
        [PXSelector(typeof(Search<Location.locationID,
                        Where<
                            Location.bAccountID, Equal<Current<RouteAppointmentGPSLocationFilter.customerID>>>>), 
        SubstituteKey = typeof(Location.locationCD), DescriptionField = typeof(Location.descr))]
        [PXUIField(DisplayName = "Customer Location")]
        public virtual int? CustomerLocationID { get; set; }
        #endregion
        #region RouteID
        public abstract class routeID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Route")]
        [FSSelectorRouteID]
        public virtual int? RouteID { get; set; }
        #endregion
        #region RouteDocumentID
        public abstract class routeDocumentID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXFormula(typeof(Default<RouteAppointmentGPSLocationFilter.routeID>))]
        [PXSelector(typeof(Search<FSRouteDocument.routeDocumentID,
                        Where<
                            FSRouteDocument.routeID, Equal<Current<RouteAppointmentGPSLocationFilter.routeID>>>>), SubstituteKey = typeof(FSRouteDocument.refNbr))]
        [PXUIField(DisplayName = "Route Nbr.")]
        public virtual int? RouteDocumentID { get; set; }
        #endregion
        #region LoadData
        public abstract class loadData : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Generate", Visible = false)]
        public virtual bool? LoadData { get; set; }
        #endregion
    }
}