using PX.Data;
using PX.Objects.AP;
using PX.Objects.IN;
using System;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class CreatePOFilter : IBqlTable
    {
        #region UpToDate

        public abstract class upToDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Up to Date")]
        public virtual DateTime? UpToDate { get; set; }
        #endregion
        #region ItemClassID
        public abstract class itemClassID : IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Item Class")]
        [PXSelector(typeof(Search<INItemClass.itemClassID>), SubstituteKey = typeof(INItemClass.itemClassCD), DescriptionField = typeof(INItemClass.descr))]
        public virtual int? ItemClassID { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXFormula(typeof(Default<CreatePOFilter.itemClassID>))]
        [PXUIField(DisplayName = "Inventory")]
        [PXSelector(typeof(
                       Search<InventoryItem.inventoryID,
                       Where<
                           InventoryItem.itemClassID, Equal<Current<CreatePOFilter.itemClassID>>,
                           Or<Current<CreatePOFilter.itemClassID>, IsNull>>>), 
                    SubstituteKey = typeof(InventoryItem.inventoryCD),
                    DescriptionField = typeof(InventoryItem.descr))]
        public virtual int? InventoryID { get; set; }
        #endregion
        #region SiteID
        public abstract class siteID : PX.Data.IBqlField
        {
        }

        [Site(DisplayName = "Warehouse", DescriptionField = typeof(INSite.descr))]
        public virtual int? SiteID { get; set; }
        #endregion
        #region POVendorID
        public abstract class poVendorID : PX.Data.IBqlField
        {
        }

        [VendorNonEmployeeActive(DisplayName = "Vendor", DescriptionField = typeof(Vendor.acctName), CacheGlobal = true, Filterable = true)]
        public virtual int? POVendorID { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Customer", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorBusinessAccount_CU_PR_VC]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXString(4, IsFixed = true)]
        [PXUIField(DisplayName = "Service Order Type", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(
            Search<FSSrvOrdType.srvOrdType,
            Where<
                FSSrvOrdType.active, Equal<True>>>))]
        public virtual string SrvOrdType { get; set; }
        #endregion
        #region SORefNbr
        public abstract class sORefNbr : PX.Data.IBqlField
        {
        }

        [PXString]
        [PXUIField(DisplayName = "Service Order Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXFormula(typeof(Default<CreatePOFilter.srvOrdType>))]
        [PXSelector(typeof(
                       Search<FSServiceOrder.refNbr,
                       Where<
                           FSServiceOrder.srvOrdType, Equal<Current<CreatePOFilter.srvOrdType>>,
                           Or<Current<CreatePOFilter.srvOrdType>, IsNull>>,
                       OrderBy<
                            Desc<FSServiceOrder.refNbr>>>),
                    DescriptionField = typeof(FSServiceOrder.docDesc))]
        public virtual string SORefNbr { get; set; }
        #endregion
    }
}
