using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

namespace PX.Objects.FS
{
    [PXTable(IsOptional = true)]
    public class FSxARTran : PXCacheExtension<ARTran>
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

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSxARTran.billCustomerID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Location ID", Enabled = false, DirtyRead = true)]
        public virtual int? CustomerLocationID { get; set; }
        #endregion
        #region SMEquipmentID
        public abstract class sMEquipmentID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Target Equipment", Enabled = false, FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [PXSelector(typeof(FSEquipment.SMequipmentID), SubstituteKey = typeof(FSEquipment.refNbr))]
        [PXDBInt]
        public virtual int? SMEquipmentID { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Service Contract Nbr.", Enabled = false, FieldClass = "FSCONTRACT")]
        [PXDBInt]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region ContractPeriodID
        public abstract class contractPeriodID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? ContractPeriodID { get; set; }
        #endregion

        #region Equipment Customization
        #region SuspendedSMEquipmentID
        public abstract class suspendedSMEquipmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Suspended Target Equipment ID", FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search<FSEquipment.SMequipmentID>), SubstituteKey = typeof(FSEquipment.refNbr))]
        public virtual int? SuspendedSMEquipmentID { get; set; }
        #endregion
        #region NewTargetEquipmentLineNbr
        public abstract class newTargetEquipmentLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Model Equipment Line Nbr.", FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? NewTargetEquipmentLineNbr { get; set; }
        #endregion
        #region ComponentID
        public abstract class componentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Component ID", FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [PXSelector(typeof(Search<FSModelTemplateComponent.componentID>), SubstituteKey = typeof(FSModelTemplateComponent.componentCD))]
        public virtual int? ComponentID { get; set; }
        #endregion
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
