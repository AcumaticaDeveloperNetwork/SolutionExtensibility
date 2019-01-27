using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    [Serializable]
    [PXPrimaryGraph(typeof(RouteAppointmentForecastingInq))]
    public class FSRouteAppointmentForecasting : IBqlTable
    {
        #region ScheduleID
        public abstract class scheduleID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXSelector(typeof(Search<FSSchedule.scheduleID,
                           Where<
                                FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>,
                                And<FSSchedule.entityID, Equal<Current<FSRouteAppointmentForecasting.serviceContractID>>>>>))]
        [PXUIField(Enabled = false)]
        public virtual int? ScheduleID { get; set; }
        #endregion
        #region StartDate
        public abstract class startDate : PX.Data.IBqlField
        {
        }

        [PXDBDate(IsKey = true)]
        [PXUIField(DisplayName = "Start Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? StartDate { get; set; }
        #endregion
        #region RouteID
        public abstract class routeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [FSSelectorRouteID]
        [PXUIField(DisplayName = "Route ID")]
        public virtual int? RouteID { get; set; }

        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Customer ID", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorBusinessAccount_CU_PR_VC]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region CustomerLocationID
        public abstract class customerLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(DisplayName = "Location ID", DescriptionField = typeof(Location.descr))]
        public virtual int? CustomerLocationID { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSServiceContract.serviceContractID,
                           Where<
                                FSServiceContract.customerID, Equal<Current<FSRouteAppointmentForecasting.customerID>>>>),
                           SubstituteKey = typeof(FSServiceContract.refNbr))]
        [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region SequenceOrder
        public abstract class sequenceOrder : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Sequence Order")]
        public virtual int? SequenceOrder { get; set; }

        #endregion
    }
}