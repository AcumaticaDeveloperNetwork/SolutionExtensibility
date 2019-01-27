using System;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSScheduleDetService : FSScheduleDet
    {
        #region LineType
        public new abstract class lineType : ListField_LineType_Service_ServiceContract
        {
        }

        [PXDBString(5, IsFixed = true)]
        [PXUIField(DisplayName = "Line Type")]
        [lineType.ListAtrribute]
        [PXDefault(ID.LineType_ServiceContract.SERVICE)]
        public override string LineType { get; set; }
        #endregion
        #region InventoryID
        [ContractSchedule_ServiceInventory]
        public override int? InventoryID { get; set; }
        #endregion
        #region SMEquipmentID
        public new abstract class SMequipmentID : PX.Data.IBqlField
        {
        }
        #endregion
        #region ComponentID
        public new abstract class componentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Component ID", FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [FSSelectorComponentIDByFSEquipmentComponent(typeof(SMequipmentID))]
        public override int? ComponentID { get; set; }
        #endregion
        #region EquipmentLineRef
        public new abstract class equipmentLineRef : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Component Line Ref.", FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [FSSelectorEquipmentLineRef(typeof(SMequipmentID), typeof(componentID))]
        public override int? EquipmentLineRef { get; set; }
        #endregion
    }
}