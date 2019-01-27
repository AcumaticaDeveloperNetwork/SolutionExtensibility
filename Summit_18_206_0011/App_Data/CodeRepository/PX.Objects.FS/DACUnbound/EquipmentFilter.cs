using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    [System.Serializable]
    public class EquipmentFilter : IBqlTable
    { 
        #region CustomerID
        public abstract class customerID : IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Customer ID")]
        [FSSelectorCustomer]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region CustomerLocationID
        public abstract class customerLocationID : IBqlField
        {
        }

        [PXInt]
        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<EquipmentFilter.customerID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Customer Location ID", DirtyRead = true)]
        [PXDefault(typeof(Search<BAccount.defLocationID,
                          Where<
                            BAccount.bAccountID, Equal<Current<customerID>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<customerID>))]
        public virtual int? CustomerLocationID { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : IBqlField
        {
        }

        [EquipmentModelItem(Filterable = true)]
        public virtual int? InventoryID { get; set; }
        #endregion
        #region OwnerID
        public abstract class ownerID : IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Owner ID")]
        [FSSelectorCustomer]
        public virtual int? OwnerID { get; set; }
        #endregion
        #region RefNbr
        public abstract class refNbr : IBqlField
        {
        }

        [PXString]
        [PXUIField(DisplayName = "Equipment Nbr.")]
        [FSSelectorSMEquipmentRefNbr]
        public virtual string RefNbr { get; set; }
        #endregion
        #region RequireMaintenance
        public abstract class requireMaintenance : IBqlField
        {
        }

        [PXBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Show only maintenance equipment")]
        public virtual bool? RequireMaintenance { get; set; }
        #endregion
        #region ResourceEquipment
        public abstract class resourceEquipment : IBqlField
        {
        }

        [PXBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Show only resource equipment")]
        public virtual bool? ResourceEquipment { get; set; }
        #endregion
        #region WarrantyLess
        public abstract class warrantyLess : IBqlField
        {
        }

        [PXBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Show only equipment without warranties and components")]
        public virtual bool? WarrantyLess { get; set; }
        #endregion
    }
}