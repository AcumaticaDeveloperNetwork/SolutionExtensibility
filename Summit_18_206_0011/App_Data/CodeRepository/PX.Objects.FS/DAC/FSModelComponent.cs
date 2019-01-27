using System;
using PX.Data;
using PX.Objects.IN;
﻿
namespace PX.Objects.FS
{	
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.MODEL_WARRANTY)]
    public class FSModelComponent : PX.Data.IBqlTable
	{
        #region ModelID
        public abstract class modelID : PX.Data.IBqlField
        {
        }
        [PXDBInt(IsKey = true)]
        [PXDBLiteDefault(typeof(InventoryItem.inventoryID))]
        [PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<FSModelComponent.modelID>>>>))]
        public virtual int? ModelID { get; set; }
        #endregion
        #region ComponentID
        public abstract class componentID : PX.Data.IBqlField
        {
        }
        [PXDBInt(IsKey=true)]
        [PXDefault]
        [PXUIField(DisplayName = "Component ID")]
        [FSSelectorComponentID]
        public virtual int? ComponentID { get; set; }

        #endregion
        #region Active
        public abstract class active : PX.Data.IBqlField
        {
        }
        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? Active { get; set; }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
        {
        }
        [PXDBString(250, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string Descr { get; set; }
        #endregion
        #region RequireSerial
        public abstract class requireSerial : PX.Data.IBqlField
        {
        }
        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Requires Serial")]
        public virtual bool? RequireSerial { get; set; }
        #endregion
        #region VendorWarrantyValue
        public abstract class vendorWarrantyValue : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0)]
        [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Vendor Warranty")]
        public virtual int? VendorWarrantyValue { get; set; }
        #endregion
        #region VendorWarrantyType
        public abstract class vendorWarrantyType : ListField_WarrantyDurationType
        {
        }

        [PXDBString(1, IsFixed = true)]
        [vendorWarrantyType.ListAtrribute]
        [PXDefault(ID.WarrantyDurationType.MONTH, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Vendor Warranty Type")]
        public virtual string VendorWarrantyType { get; set; }
        #endregion
        #region VendorID
        public abstract class vendorID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Vendor ID")]
        [FSSelectorBusinessAccount_VE]
        public virtual int? VendorID { get; set; }
        #endregion
        #region CpnyWarrantyValue
        public abstract class cpnyWarrantyValue : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0)]
        [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Company Warranty")]
        public virtual int? CpnyWarrantyValue { get; set; }
        #endregion
        #region CpnyWarrantyType
        public abstract class cpnyWarrantyType : ListField_WarrantyDurationType
        {
        }

        [PXDBString(1, IsFixed = true)]
        [vendorWarrantyType.ListAtrribute]
        [PXDefault(ID.WarrantyDurationType.MONTH, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Company Warranty Type")]
        public virtual string CpnyWarrantyType { get; set; }
        #endregion
        #region ClassID
        public abstract class classID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Item Class ID")]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXSelector(typeof(
            Search2<INItemClass.itemClassID,
            InnerJoin<FSModelTemplateComponent, On<FSModelTemplateComponent.classID, Equal<INItemClass.itemClassID>>>,
            Where<
                FSModelTemplateComponent.componentID, Equal<Current<FSModelComponent.componentID>>,
                And<FSModelTemplateComponent.modelTemplateID, Equal<Current<InventoryItem.itemClassID>>,
                And<FSxEquipmentModelTemplate.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>>>),
            SubstituteKey = typeof(INItemClass.itemClassCD),
            DescriptionField = typeof(INItemClass.descr))]
        public virtual int? ClassID { get; set; }
        #endregion
        #region Qty
        public abstract class qty : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 1)]
        [PXUIField(DisplayName = "Quantity")]
        public virtual int? Qty { get; set; }
        #endregion
        #region Optional
        public abstract class optional : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Optional", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual bool? Optional { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [StockItem(Enabled = true)]
        [PXRestrictor(typeof(
                        Where<
                            FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>,
                            And<InventoryItem.itemClassID, Equal<Current<FSModelComponent.classID>>>>)
                        , TX.Error.INVENTORY_NOT_ALLOWED_AS_COMPONENT)]
        public virtual int? InventoryID { get; set; }
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

