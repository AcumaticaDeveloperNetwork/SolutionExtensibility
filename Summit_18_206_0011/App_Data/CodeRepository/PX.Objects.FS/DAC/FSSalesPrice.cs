using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.IN;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSSalesPrice : PX.Data.IBqlTable
    {
        #region SalesPriceID
        public abstract class salesPriceID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        public virtual int? SalesPriceID { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Inventory ID")]
        public virtual int? InventoryID { get; set; }
        #endregion
        #region LineType
        public abstract class lineType : ListField_LineType_SalesPrices
        {
        }

        [PXDBString(5, IsFixed = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Line Type", Enabled = false)]
        [lineType.ListAtrribute]
        public virtual string LineType { get; set; }
        #endregion
        #region UnitPrice
        public abstract class unitPrice : PX.Data.IBqlField
        {
        }

        [PXDBPriceCost]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual decimal? UnitPrice { get; set; }
        #endregion
        #region UOM
        public abstract class uOM : PX.Data.IBqlField
        {
        }

        [INUnit(DisplayName = "UOM", Enabled = false)]
        public virtual string UOM { get; set; }
        #endregion
        #region CuryID
        public abstract class curyID : PX.Data.IBqlField
        {
        }

        [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
        [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, Required = false)]
        [PXSelector(typeof(Currency.curyID))]
        public virtual string CuryID { get; set; }
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
        [PXUIField(DisplayName = "Created By Screen ID")]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
        [PXUIField(DisplayName = "Last Modified By Screen ID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0])]
        public virtual Guid? NoteID { get; set; }
        #endregion

        #region Mem_UnitPrice
        public abstract class mem_UnitPrice : PX.Data.IBqlField
        {
        }

        [PXPriceCost]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Unit Price")]
        public virtual decimal? Mem_UnitPrice { get; set; }
        #endregion
    }
}