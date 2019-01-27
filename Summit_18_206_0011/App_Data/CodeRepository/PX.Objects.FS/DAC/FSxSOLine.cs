using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

namespace PX.Objects.FS
{
    [PXTable(IsOptional = true)]
    public class FSxSOLine : PXCacheExtension<SOLine>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region SDPosted
        public abstract class sDPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        public virtual bool? SDPosted { get; set; }
        #endregion
        #region SDSelected
        public abstract class sDSelected : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Appointment", Visible = false)]
        public virtual bool? SDSelected { get; set; }
        #endregion
        #region Source
        public abstract class source : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXUIField(Enabled = false, Visible = false)]
        public virtual string Source { get; set; }
        #endregion
        #region SOID
        public abstract class sOID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false, Visible = false)]
        [PXSelector(typeof(Search<FSServiceOrder.sOID>), SubstituteKey = typeof(FSServiceOrder.refNbr))]
        [PXDBInt]
        public virtual int? SOID { get; set; }
        #endregion
        #region AppointmentID
        public abstract class appointmentID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false, Visible = false)]
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
        [PXUIField(DisplayName = "Service Appointment Date", Enabled = false, Visible = false)]
        public virtual DateTime? AppointmentDate { get; set; }
        #endregion
        #region ServiceOrderDate
        public abstract class serviceOrderDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Service Order Date", Enabled = false, Visible = false)]
        public virtual DateTime? ServiceOrderDate { get; set; }
        #endregion
        #region BillCustomerID
        public abstract class billCustomerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Billing Customer ID", Visible = false)]
        [FSSelectorCustomer]
        public virtual int? BillCustomerID { get; set; }
        #endregion
        #region CustomerLocationID
        public abstract class customerLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSxSOLine.billCustomerID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Location ID", Enabled = false, DirtyRead = true)]
        public virtual int? CustomerLocationID { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Service Contract Nbr.", Enabled = false, Visible = false, FieldClass = "FSCONTRACT")]
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

        #region SMEquipmentID
        public abstract class sMEquipmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Target Equipment ID", Visible = false, FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search<FSEquipment.SMequipmentID,
                        Where<
                            FSEquipment.customerID, Equal<Current<SOOrder.customerID>>,
                            And<FSEquipment.status, Equal<FSEquipment.status.Active>>>>),
                    SubstituteKey = typeof(FSEquipment.refNbr),
                    DescriptionField = typeof(FSEquipment.descr))]
        public virtual int? SMEquipmentID { get; set; }
        #endregion
        #region NewTargetEquipmentLineNbr
        public abstract class newTargetEquipmentLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Model Equipment Line Nbr.", Visible = false, FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [FSSelectorNewTargetEquipmentSalesOrder]
        public virtual int? NewTargetEquipmentLineNbr { get; set; }
        #endregion
        #region ComponentID
        public abstract class componentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Component ID", Visible = false, FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [FSSelectorComponentIDSalesOrder]
        public virtual int? ComponentID { get; set; }
        #endregion
        #region EquipmentLineRef
        public abstract class equipmentLineRef : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Component Line Ref.", Visible = false, FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [FSSelectorEquipmentLineRefSalesOrder]
        public virtual int? EquipmentLineRef { get; set; }
        #endregion
        #region EquipmentAction
        public abstract class equipmentAction : ListField_EquipmentAction
        {
        }

        [PXDBString(2, IsFixed = true)]
        [equipmentAction.ListAtrribute]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Equipment Action", Visible = false, FieldClass = FSSetup.EquipmentManagementFieldClass)]
        public virtual string EquipmentAction { get; set; }
        #endregion
        #region Comment
        public abstract class comment : PX.Data.IBqlField
        {
        }

        [PXDBString(int.MaxValue, IsUnicode = true)]
        [PXUIField(DisplayName = "Equipment Action Comment", FieldClass = FSSetup.EquipmentManagementFieldClass, Visible = false)]
        [SkipSetExtensionVisibleInvisible]
        public virtual string Comment { get; set; }
        #endregion
        #region EquipmentItemClass
        public abstract class equipmentItemClass : PX.Data.IBqlField
        {
        }

        [PXString(2, IsFixed = true)]
        public virtual string EquipmentItemClass { get; set; }
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
