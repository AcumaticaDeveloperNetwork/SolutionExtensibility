using PX.Data;
using PX.Objects.CS;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSAppointmentInventoryItem : FSAppointmentDet
    {
        #region AppointmentID
        public new abstract class appointmentID : PX.Data.IBqlField
        {
        }
        #endregion
        #region LineType
        public new abstract class lineType : ListField_LineType_Pickup_Delivery
        {
        }

        [PXDBString(5, IsFixed = true)]
        [PXUIField(DisplayName = "Line Type", Enabled = false)]
        [lineType.ListAtrribute]
        [PXDefault(ID.LineType_Pickup_Delivery.PICKUP_DELIVERY)]
        public override string LineType { get; set; }
        #endregion

        #region SODetID
        public new abstract class sODetID : PX.Data.IBqlField
        {
        }

        [PXDefault]
        [PXDBInt]
        [PXUIField(DisplayName = "Line Ref.")]
        [FSSelectorServiceSODetIDInAppointment]
        public override int? SODetID { get; set; }
        #endregion
        #region LineRef
        public new abstract class lineRef : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        [PXFormula(typeof(Selector<sODetID, FSSODet.lineRef>))]
        [PXDefault]
        [PXUIField(DisplayName = "Line Ref.", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public override string LineRef { get; set; }
        #endregion
        #region PickupDeliveryServiceID
        public abstract class pickupDeliveryServiceID : PX.Data.IBqlField
        {
        }

        [Service(Enabled = false)]        
        [PXFormula(typeof(Selector<sODetID, FSSODet.inventoryID>))]
        [PXDefault]
        public virtual int? PickupDeliveryServiceID { get; set; }
        #endregion
        #region ServiceType
        public new abstract class serviceType : ListField_Appointment_Service_Action_Type
        {
        }

        [PXDBString(1, IsFixed = true)]
        [serviceType.List]
        [PXFormula(typeof(Selector<sODetID, FSAppointmentDetService.serviceType>))]
        [PXDefault]
        [PXUIField(DisplayName = "Pickup/Delivery Action", Enabled = false)]
        public override string ServiceType { get; set; }
        #endregion

        #region InventoryID
        public new abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [PXDefault]
        [PXCheckUnique(Where = typeof(Where<FSAppointmentInventoryItem.appointmentID, Equal<Current<FSAppointmentInventoryItem.appointmentID>>,
                                                    And<FSAppointmentInventoryItem.sODetID, Equal<Current<FSAppointmentInventoryItem.sODetID>>>>))]
        [PXFormula(typeof(Default<pickupDeliveryServiceID>))]
        [PickupDeliveryItem]
        public override int? InventoryID { get; set; }
        #endregion
        #region SubItemID
        public new abstract class subItemID : PX.Data.IBqlField
        {
        }

        [SubItem(typeof(inventoryID), DisplayName = "Subitem")]
        [PXDefault(typeof(Search<InventoryItem.defaultSubItemID,
                            Where<
                                InventoryItem.inventoryID, Equal<Current<FSAppointmentInventoryItem.inventoryID>>,
                                And<InventoryItem.defaultSubItemOnEntry, Equal<True>>>>),
                    PersistingCheck = PXPersistingCheck.Nothing)]
        [SubItemStatusVeryfier(typeof(inventoryID), typeof(siteID), InventoryItemStatus.Inactive, InventoryItemStatus.NoSales)]
        public override int? SubItemID { get; set; }
        #endregion

        #region UOM
        public new abstract class uOM : PX.Data.IBqlField
        {
        }

        [INUnit(typeof(inventoryID), DisplayName = "UOM", Enabled = false)]
        [PXDefault(typeof(Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<FSAppointmentInventoryItem.inventoryID>>>>),
                    PersistingCheck = PXPersistingCheck.Nothing)]
        public override string UOM { get; set; }
        #endregion

        #region SiteID
        public new abstract class siteID : PX.Data.IBqlField
        {
        }

        [SiteAvail(typeof(inventoryID), typeof(subItemID), DisplayName = "Warehouse")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIRequired(typeof(IIf<Where<
                                    lineType, NotEqual<FSAppointmentDet.lineType.Comment_Service>,
                                    And<lineType, NotEqual<FSAppointmentDet.lineType.Comment_Part>,
                                    And<lineType, NotEqual<FSAppointmentDet.lineType.Instruction_Service>,
                                    And<lineType, NotEqual<FSAppointmentDet.lineType.Instruction_Part>,
                                    And<lineType, NotEqual<FSAppointmentDet.lineType.Service_Template>>>>>>, True, False>))]
        public override int? SiteID { get; set; }
        #endregion
        #region SiteLocationID
        public new abstract class siteLocationID : PX.Data.IBqlField
        {
        }

        [Location(typeof(siteID), DescriptionField = typeof(INLocation.descr), DisplayName = "Location")]
        public override int? SiteLocationID { get; set; }
        #endregion

        #region ProjectTaskID
        public new abstract class projectTaskID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(FSAppointment.dfltProjectTaskID), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Project Task")]
        [FSSelectorActive_AR_SO_ProjectTask(typeof(Where<PMTask.projectID, Equal<Current<FSAppointmentInventoryItem.projectID>>>))]
        public override int? ProjectTaskID { get; set; }
        #endregion

        #region EstimatedQty
        public new abstract class estimatedQty : PX.Data.IBqlField
        {
        }

        [PXDBQuantity]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Estimated Quantity")]
        public override decimal? EstimatedQty { get; set; }
        #endregion

        #region Qty
        public new abstract class qty : PX.Data.IBqlField
        {
        }

        [PXDBQuantity]
        [PXDefault(typeof(Switch<
                                Case<Where<
                                        FSAppointmentInventoryItem.status, NotEqual<FSAppointmentDet.status.Canceled>,
                                        And<
                                            Where<
                                                Current<FSAppointmentInventoryItem.status>, Equal<FSAppointment.status.InProcess>,
                                                Or<Current<FSAppointmentInventoryItem.status>, Equal<FSAppointment.status.Completed>>>>>,
                                    FSAppointmentInventoryItem.estimatedQty>,
                                SharedClasses.decimal_0>))]
        [PXFormula(typeof(Default<status>))]
        [PXUIField(DisplayName = "Actual Quantity")]
        public override decimal? Qty { get; set; }
        #endregion

        #region BillableQty
        public new abstract class billableQty : PX.Data.IBqlField
        {
        }

        [PXDBQuantity]
        [PXFormula(typeof(Default<FSAppointmentInventoryItem.isBillable, FSAppointmentInventoryItem.contractRelated>))]
        [PXFormula(typeof(Switch<
                                Case<
                                    Where<
                                        FSAppointmentInventoryItem.isPrepaid, Equal<True>>,
                                    SharedClasses.decimal_0,
                                Case<
                                    Where<
                                        FSAppointmentInventoryItem.contractRelated, Equal<True>>,
                                    FSAppointmentInventoryItem.extraUsageQty,
                                Case<
                                    Where2<
                                        Where<
                                            Current<FSAppointment.status>, Equal<FSAppointment.status.AutomaticScheduled>,
                                            Or<Current<FSAppointment.status>, Equal<FSAppointment.status.ManualScheduled>>>,
                                        And<contractRelated, Equal<False>>>,
                                    FSAppointmentInventoryItem.estimatedQty>>>,
                            FSAppointmentInventoryItem.qty>))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Billable Quantity", Enabled = false)]
        public override decimal? BillableQty { get; set; }
        #endregion

        #region AcctID
        public new abstract class acctID : PX.Data.IBqlField
        {
        }

        [PXFormula(typeof(Default<FSAppointmentInventoryItem.inventoryID>))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [Account(Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Account.description), Visible = false)]
        public override int? AcctID { get; set; }
        #endregion
        #region SubID
        public new abstract class subID : PX.Data.IBqlField
        {
        }

        [PXFormula(typeof(Default<FSAppointmentInventoryItem.acctID>))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [SubAccount(typeof(FSAppointmentInventoryItem.acctID), Visibility = PXUIVisibility.Visible, DescriptionField = typeof(Sub.description), Visible = false)]
        public override int? SubID { get; set; }
        #endregion

        #region Tax Fields
        #region TaxCategoryID
        public new abstract class taxCategoryID : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
        [PXDefault(typeof(Search<InventoryItem.taxCategoryID,
            Where<InventoryItem.inventoryID, Equal<Current<FSAppointmentInventoryItem.inventoryID>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<inventoryID>))]
        public override String TaxCategoryID { get; set; }
        #endregion
        #endregion

        #region ContractRelated
        public new abstract class contractRelated : IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Service Contract Item", IsReadOnly = true)]
        public override bool? ContractRelated { get; set; }
        #endregion
        #region ExtraUsageUnitPrice 
        public new abstract class extraUsageUnitPrice : PX.Data.IBqlField
        {
        }

        [PXDBPriceCost]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Base Overage Unit Price", Enabled = false)]
        public override Decimal? ExtraUsageUnitPrice { get; set; }
        #endregion
        #region CuryExtraUsageUnitPrice
        public new abstract class curyExtraUsageUnitPrice : PX.Data.IBqlField
        {
        }

        [PXDBCurrency(typeof(curyInfoID), typeof(extraUsageUnitPrice))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Overage Unit Price", Enabled = false, Visible = false)]
        public override Decimal? CuryExtraUsageUnitPrice { get; set; }
        #endregion

        #region SourceSalesOrderRefNbr
        public new abstract class sourceSalesOrderRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Sales Order Ref. Nbr.", Enabled = false)]
        [PXSelector(typeof(Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<FSAppointmentInventoryItem.sourceSalesOrderType>>>>))]
        public override string SourceSalesOrderRefNbr { get; set; }
        #endregion

        #region PostID
        public new abstract class postID : PX.Data.IBqlField
        {
        }
        #endregion

        #region MemoryHelper

        #region Selected
        public abstract class selected : IBqlField
        {
        }

        [PXBool]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        #endregion

        #region Mem_BatchNbr
        public abstract class mem_BatchNbr : PX.Data.IBqlField
        {
        }

        [PXString(15, IsFixed = true)]
        [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
        public virtual string Mem_BatchNbr { get; set; }
        #endregion
        #region Mem_ServiceType
        public abstract class mem_ServiceType : ListField_Appointment_Service_Action_Type
        {
        }

        [PXString(1, IsFixed = true)]
        [mem_ServiceType.List]
        [PXFormula(typeof(Selector<sODetID, FSAppointmentDet.serviceType>))]
        [PXUIField(DisplayName = "Pickup/Deliver Items", Enabled = false)]
        public virtual string Mem_ServiceType { get; set; }
        #endregion

        #endregion

        #region Methods
        public virtual int? GetINItemID()
        {
            return this.InventoryID;
        }
        #endregion
    }
}