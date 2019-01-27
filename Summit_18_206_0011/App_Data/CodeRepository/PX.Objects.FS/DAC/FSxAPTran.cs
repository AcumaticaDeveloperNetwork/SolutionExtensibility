using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

namespace PX.Objects.FS
{
    [PXTable(IsOptional = true)]
    public class FSxAPTran : PXCacheExtension<APTran>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region Source
        public abstract class source : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXUIField(Enabled = false)]
        public virtual string Source { get; set; }
        #endregion
        #region SOID
        public abstract class sOID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
        [PXSelector(typeof(Search<FSServiceOrder.sOID>), SubstituteKey = typeof(FSServiceOrder.refNbr))]
        [PXDBInt]
        public virtual int? SOID { get; set; }
        #endregion
        #region AppointmentID
        public abstract class appointmentID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false)]
        [PXSelector(typeof(Search<FSAppointment.appointmentID>), SubstituteKey = typeof(FSAppointment.refNbr))]
        [PXDBInt]        
        public virtual int? AppointmentID { get; set; }
        #endregion
        #region AppDetID
        public abstract class appDetID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? AppDetID { get; set; }
        #endregion
        #region SODetID
        public abstract class sODetID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? SODetID { get; set; }
        #endregion
        #region AppointmentDate
        public abstract class appointmentDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Service Appointment Date", Enabled = false)]
        public virtual DateTime? AppointmentDate { get; set; }
        #endregion
        #region ServiceOrderDate
        public abstract class serviceOrderDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Service Order Date", Enabled = false)]
        public virtual DateTime? ServiceOrderDate { get; set; }
        #endregion
        #region BillCustomerID
        public abstract class billCustomerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Billing Customer ID")]
        [FSSelectorCustomer]
        public virtual int? BillCustomerID { get; set; }
        #endregion
        #region CustomerLocationID
        public abstract class customerLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSxAPTran.billCustomerID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Location ID", Enabled = false, DirtyRead = true)]
        public virtual int? CustomerLocationID { get; set; }
        #endregion

        #region Mem_PreviousPostID
        public abstract class mem_PreviousPostID : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? Mem_PreviousPostID { get; set; }
        #endregion
        #region Mem_TableSource
        public abstract class mem_TableSource : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string Mem_TableSource { get; set; }
        #endregion
    }
}
