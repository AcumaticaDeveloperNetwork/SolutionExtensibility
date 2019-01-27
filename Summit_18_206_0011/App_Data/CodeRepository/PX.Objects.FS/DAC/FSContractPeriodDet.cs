﻿using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.EP;
using PX.Objects.CM;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSContractPeriodDet : PX.Data.IBqlTable
    {
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDBLiteDefault(typeof(FSServiceContract.serviceContractID))]
        [PXUIField(DisplayName = "Service Contract ID")]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region ContractPeriodID
        public abstract class contractPeriodID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXParent(typeof(Select<FSContractPeriod, Where<FSContractPeriod.contractPeriodID, Equal<Current<FSContractPeriodDet.contractPeriodID>>>>))]
        [PXDBLiteDefault(typeof(FSContractPeriod.contractPeriodID))]

        public virtual int? ContractPeriodID { get; set; }
        #endregion
        #region ContractPeriodDetID
        public abstract class contractPeriodDetID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? ContractPeriodDetID { get; set; }
        #endregion
        #region LineType
        public abstract class lineType : ListField_LineType_ContractPeriod
        {
        }

        [PXDBString(5, IsFixed = true)]
        [PXUIField(DisplayName = "Line Type")]
        [lineType.ListAtrribute]
        [PXDefault(ID.LineType_ContractPeriod.SERVICE)]
        public virtual string LineType { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [PXDefault]
        [PXUIField(DisplayName = "Inventory ID")]
        [InventoryIDByLineType(typeof(lineType), Filterable = true, IsKey = true)]
        [PXRestrictor(typeof(
                        Where<
                            InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>,
                            Or<FSxServiceClass.requireRoute, Equal<True>,
                            Or<Current<FSServiceContract.recordType>, Equal<FSServiceContract.recordType.ServiceContract>>>>),
                TX.Error.NONROUTE_SERVICE_CANNOT_BE_HANDLED_WITH_ROUTE_SRVORDTYPE)]
        [PXRestrictor(typeof(
                        Where<
                            InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>,
                            Or<FSxServiceClass.requireRoute, Equal<False>,
                            Or<Current<FSServiceContract.recordType>, Equal<FSServiceContract.recordType.RouteServiceContract>>>>),
                TX.Error.ROUTE_SERVICE_CANNOT_BE_HANDLED_WITH_NONROUTE_SRVORDTYPE)]
        [PXCheckUnique(typeof(SMequipmentID),
                       Where = typeof(Where<FSContractPeriodDet.serviceContractID, Equal<Current<FSContractPeriodDet.serviceContractID>>,
                                            And<FSContractPeriodDet.contractPeriodID, Equal<Current<FSContractPeriodDet.contractPeriodID>>,
                                                  And<
                                                      Where<Current<FSContractPeriodDet.SMequipmentID>, IsNull,
                                                            Or<FSContractPeriodDet.SMequipmentID, Equal<Current<FSContractPeriodDet.SMequipmentID>>>>>>>))]
        public virtual int? InventoryID { get; set; }
        #endregion
        #region UOM
        public abstract class uOM : PX.Data.IBqlField
        {
        }

        [INUnit(typeof(inventoryID), DisplayName = "UOM", Enabled = false)]
        [PXDefault(typeof(Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<FSContractPeriodDet.inventoryID>>>>))]
        public virtual string UOM { get; set; }
        #endregion
        #region SMEquipmentID
        public abstract class SMequipmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Target Equipment ID", FieldClass = FSSetup.EquipmentManagementFieldClass)]
        [FSSelectorContractPeriodEquipment]
        [PXRestrictor(typeof(Where<FSEquipment.status, Equal<EPEquipmentStatus.EquipmentStatusActive>>),
                        TX.Messages.EQUIPMENT_IS_INSTATUS, typeof(FSEquipment.status))]
        public virtual int? SMEquipmentID { get; set; }
        #endregion
        #region BillingRule
        public abstract class billingRule : ListField_BillingRule_ContractPeriod
        {
        }

        [PXDBString(4, IsFixed = true)]
        [billingRule.ListAtrribute]
        [PXUIField(DisplayName = "Billing Rule")]
        public virtual string BillingRule { get; set; }
        #endregion
        #region Qty
        public abstract class qty : PX.Data.IBqlField
        {
        }

        [PXDBQuantity]
        [PXFormula(typeof(Default<FSContractPeriodDet.inventoryID>))]
        [PXDefault(TypeCode.Decimal, "1.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Quantity", Visible = false)]
        public virtual decimal? Qty { get; set; }
        #endregion
        #region Time
        public abstract class time : PX.Data.IBqlField
        {
        }

        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXFormula(typeof(Default<FSContractPeriodDet.inventoryID>))]
        [PXUIField(DisplayName = "Time", Enabled = false, Visible = false)]
        public virtual int? Time { get; set; }
        #endregion
        #region UsedQty  
        public abstract class usedQty : PX.Data.IBqlField
        {
        }

        [PXDBDecimal]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Used Period Quantity", Enabled = false, Visible = false)]
        public virtual decimal? UsedQty { get; set; }
        #endregion
        #region UsedTime
        public abstract class usedTime : PX.Data.IBqlField
        {
        }

        [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXUIField(DisplayName = "Used Period Time", Enabled = false, Visible = false)]
        public virtual int? UsedTime { get; set; }
        #endregion
        #region RecurringUnitPrice
        public abstract class recurringUnitPrice : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXUIField(DisplayName = "Recurring Item Price")]
        public virtual decimal? RecurringUnitPrice { get; set; }
        #endregion
        #region RecurringTotalPrice
        public abstract class recurringTotalPrice : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXFormula(typeof(Default<FSContractPeriodDet.recurringUnitPrice>))]
        [PXFormula(typeof(Default<FSContractPeriodDet.time>))]
        [PXFormula(typeof(Default<FSContractPeriodDet.qty>))]
        [PXUIField(DisplayName = "Total Recurring Price", Enabled = false)]
        [PXUnboundFormula(typeof(recurringTotalPrice), typeof(SumCalc<FSContractPeriod.periodTotal>))]
        public virtual decimal? RecurringTotalPrice { get; set; }
        #endregion
        #region OverageItemPrice
        public abstract class overageItemPrice : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXUIField(DisplayName = "Overage Item Price")]
        public virtual decimal? OverageItemPrice { get; set; }
        #endregion
        #region Rollover
        public abstract class rollover : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Rollover", IsReadOnly = true)]
        public virtual bool? Rollover { get; set; }
        #endregion
        #region RemainingQty 
        public abstract class remainingQty : PX.Data.IBqlField
        {
        }

        [PXDBDecimal]
        [PXDefault(typeof(Switch<
                            Case<Where<usedQty, LessEqual<qty>>,
                                Sub<qty, usedQty>>,
                            SharedClasses.decimal_0>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSContractPeriodDet.qty, FSContractPeriodDet.usedQty>))]
        [PXUIField(DisplayName = "Remaining Period Quantity", Enabled = false, Visible = false)]
        public virtual decimal? RemainingQty { get; set; }
        #endregion
        #region RemainingTime
        public abstract class remainingTime : PX.Data.IBqlField
        {
        }

        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXDefault(typeof(Switch<
                            Case<Where<usedTime, LessEqual<time>>,
                                Sub<time, usedTime>>,
                            SharedClasses.int_0>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSContractPeriodDet.time, FSContractPeriodDet.usedTime>))]
        [PXUIField(DisplayName = "Remaining Period Time", Enabled = false, Visible = false)]
        public virtual int? RemainingTime { get; set; }
        #endregion
        #region ScheduledQty  
        public abstract class scheduledQty : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        [PXUIField(DisplayName = "Scheduled Period Quantity", Enabled = false, Visible = false)]
        public virtual decimal? ScheduledQty { get; set; }
        #endregion
        #region ScheduledTime
        public abstract class scheduledTime : PX.Data.IBqlField
        {
        }

        [PXDefault(0, PersistingCheck =PXPersistingCheck.Nothing)]
        [PXTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXUIField(DisplayName = "Scheduled Period Time", Enabled = false, Visible = false)]
        public virtual int? ScheduledTime { get; set; }
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
        [PXUIField(DisplayName = "Created DateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "Last Modified By ID")]
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
        [PXUIField(DisplayName = "Last Modified Date Time")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion

        #region Memory Fields
        #region RegularPrice
        public abstract class regularPrice : IBqlField
        {
        }

        [PXBaseCury]
        [PXUIField(DisplayName = "Regular Price", IsReadOnly = true, Visible = false)]
        public virtual decimal? RegularPrice { get; set; }
        #endregion

        #region Amount
        public abstract class amount : PX.Data.IBqlField
        {
        }
        [PXString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Amount")]
        public virtual String Amount { get; set; }
        #endregion
        #region RemainingAmount
        public abstract class remainingAmount : PX.Data.IBqlField
        {
        }
        [PXString(255, IsUnicode = true)]
        [PXUIField(DisplayName = " Remaining Period Amount", Enabled = false)]
        public virtual String RemainingAmount { get; set; }
        #endregion
        #region UsedAmount
        public abstract class usedAmount : PX.Data.IBqlField
        {
        }
        [PXString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Used Period Amount", Enabled = false)]
        public virtual String UsedAmount { get; set; }
        #endregion 
        #region ScheduledAmount
        public abstract class scheduledAmount : PX.Data.IBqlField
        {
        }
        [PXString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Scheduled Period Amount", Enabled = false)]
        public virtual String ScheduledAmount { get; set; }
        #endregion
        #endregion
    }
}