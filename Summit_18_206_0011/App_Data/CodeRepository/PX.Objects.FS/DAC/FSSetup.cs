using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.SETUP)]
    [PXPrimaryGraph(typeof(SetupMaint))]
	public class FSSetup : PX.Data.IBqlTable
	{
        public const string ServiceManagementFieldClass = "SERVICEMANAGEMENT";
        public const string EquipmentManagementFieldClass = "EQUIPMENTMANAGEMENT";

        #region AppAutoConfirmGap
        public abstract class appAutoConfirmGap : PX.Data.IBqlField
		{
		}				

        [PXDBTimeSpanLong(Format = TimeSpanFormatType.ShortHoursMinutes)]
		[PXDefault(ID.TimeConstants.HOURS_12, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Appointment Auto-Confirm Time")]
		public virtual int? AppAutoConfirmGap { get; set; }
		#endregion		                
        #region AppResizePrecision
        public abstract class appResizePrecision : ListField_AppResizePrecision
		{
		}			

	    [PXDBInt]
		[PXDefault(ID.TimeConstants.MINUTES_30)]
        [appResizePrecision.ListAtrribute]
		[PXUIField(DisplayName = "Appointment Resize Precision")]
		public virtual int? AppResizePrecision { get; set; }
		#endregion		                
        #region CalendarID
		public abstract class calendarID : PX.Data.IBqlField
		{
		}

        [PXDefault]
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Work Calendar")]
        [PXSelector(typeof(CSCalendar.calendarID), DescriptionField = typeof(CSCalendar.description))]
		public virtual string CalendarID { get; set; }
		#endregion        
        #region CustomAppointmentName
        public abstract class customAppointmentName : PX.Data.IBqlField
        {
        }

        [PXDBString(30, InputMask = "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL")]
        [PXUIField(DisplayName = "Appointment Name")]
        public virtual string CustomAppointmentName { get; set; }
        #endregion		        
        #region CustomBranchLocationName
		public abstract class customBranchLocationName : PX.Data.IBqlField
		{
		}		

		[PXDBString(30, IsUnicode = true, InputMask = "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL")]        
		[PXUIField(DisplayName = "Branch Location Name")]
		public virtual string CustomBranchLocationName { get; set; }
		#endregion                
        #region CustomEmployeeName
        public abstract class customEmployeeName : PX.Data.IBqlField
        {
        }

        [PXDBString(30, InputMask = "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL")]
        [PXUIField(DisplayName = "Staff Member Name")]
        public virtual string CustomEmployeeName { get; set; }
        #endregion
        #region ShowServiceOrderDaysGap
        public abstract class showServiceOrderDaysGap : PX.Data.IBqlField
        {
        }

        [PXDefault(14, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBInt(MinValue = 0, MaxValue = 1000)]
        [PXUIField(DisplayName = "Show Service Orders in a Period Of", Visible = true)]
        public virtual int? ShowServiceOrderDaysGap { get; set; }
        #endregion
        #region DenyWarnByGeoZone
        public abstract class denyWarnByGeoZone : ListField_AppointmentValidation
        {
		}		

		[PXDBString(1, IsFixed = true)]
        [denyWarnByGeoZone.ListAttribute]
        [PXDefault(ID.ValidationType.NOT_VALIDATE)]
        [PXUIField(DisplayName = "Service Areas")]
        public virtual string DenyWarnByGeoZone { get; set; }
		#endregion
		#region DenyWarnByLicense
        public abstract class denyWarnByLicense : ListField_AppointmentValidation
        {
		}		

		[PXDBString(1, IsFixed = true)]
        [denyWarnByLicense.ListAttribute]
        [PXDefault(ID.ValidationType.NOT_VALIDATE)]
        [PXUIField(DisplayName = "Licenses")]
        public virtual string DenyWarnByLicense { get; set; }
		#endregion
		#region DenyWarnBySkill
        public abstract class denyWarnBySkill : ListField_AppointmentValidation
        {
		}		

		[PXDBString(1, IsFixed = true)]
        [denyWarnBySkill.ListAttribute]
		[PXDefault(ID.ValidationType.NOT_VALIDATE)]
		[PXUIField(DisplayName = "Skills")]
        public virtual string DenyWarnBySkill { get; set; }
		#endregion        
        #region DfltAppAddressSource
        public abstract class dfltAppAddressSource : PX.Data.IBqlField
		{
		}		

		[PXDBString(2, IsFixed = true)]
		[PXDefault(ID.Source_Info.BUSINESS_ACCOUNT)]
		[PXUIField(DisplayName = "Default Appointment Address Source")]        
        public virtual string DfltAppAddressSource { get; set; }
        #endregion        
        #region DfltAppContactInfoSource
        public abstract class dfltAppContactInfoSource : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.Source_Info.BUSINESS_ACCOUNT)]
        [PXUIField(DisplayName = "Default Appointment Contact Info Source", Visible = false)]
        public virtual string DfltAppContactInfoSource { get; set; }
		#endregion        
        #region EmpSchedulePrecision
        public abstract class empSchedulePrecision : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Employee Schedule Precision", Visible = false)]
        public virtual int? EmpSchedulePrecision { get; set; }
        #endregion
        #region InitialAppRefNbr
        public abstract class initialAppRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(4, InputMask = "9999")]
        [PXDefault("1")]
        [PXUIField(DisplayName = "Initial Appointment Ref. Nbr.")]
        public virtual string InitialAppRefNbr { get; set; }
        #endregion
        #region RequireBranchLocationInEmpSchedule
        public abstract class requireBranchLocationInEmpSchedule : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Branch Location in Staff Schedules")]
        public virtual bool? RequireBranchLocationInEmpSchedule { get; set; }
        #endregion  
        #region DfltBusinessAcctType
        public abstract class dfltBusinessAcctType : ListField_SrvOrdType_NewBusinessAcctType
        {
        }

        [PXDBString(10)]
        [dfltBusinessAcctType.ListAtrribute]
        [PXDefault(ID.BusinessAcctType.CUSTOMER)]
        [PXUIField(DisplayName = "Default Business Account Type")]
        public virtual string DfltBusinessAcctType { get; set; }
        #endregion
        #region DfltSrvOrdType
        public abstract class dfltSrvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsUnicode = true, IsFixed = true)]
        [PXUIField(DisplayName = "Default Service Order Type")]
        [FSSelectorSrvOrdTypeNOTQuote]
        public virtual string DfltSrvOrdType { get; set; }
        #endregion
        #region DfltSOSrvOrdType
        public abstract class dfltSOSrvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsUnicode = true, IsFixed = true)]
        [PXUIField(DisplayName = "Default Service Order Type for Sales Orders")]
        [FSSelectorSalesOrderSrvOrdType]
        public virtual string DfltSOSrvOrdType { get; set; }
        #endregion         
        #region DfltCasesSrvOrdType
        public abstract class dfltCasesSrvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsUnicode = true, IsFixed = true)]
        [PXUIField(DisplayName = "Default Service Order Type for Cases")]
        [FSSelectorActiveSrvOrdType]
        public virtual string DfltCasesSrvOrdType { get; set; }
        #endregion         
        #region DfltOpportunitySrvOrdType
        public abstract class dfltOpportunitySrvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsUnicode = true)]
        [PXUIField(DisplayName = "Default Opportunities Service Order Type")]
        [FSSelectorActiveSrvOrdType]
        public virtual string DfltOpportunitySrvOrdType { get; set; }
        #endregion  
        #region DaysAheadRecurringAppointments
        public abstract class daysAheadRecurringAppointments : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(1)]
        [PXUIField(DisplayName = "Number of days ahead for recurring appointments", Visible = false)]
        public virtual int? DaysAheadRecurringAppointments { get; set; }
        #endregion 
        #region DenyWarnByEquipment
        public abstract class denyWarnByEquipment : ListField_AppointmentValidation
        {
        }

        [PXDBString(1, IsFixed = true)]
        [denyWarnByEquipment.ListAttribute]
        [PXDefault(ID.ValidationType.NOT_VALIDATE)]
        [PXUIField(DisplayName = "Equipments")]
        public virtual string DenyWarnByEquipment { get; set; }
        #endregion        
        #region EmpSchdlNumberingID
        public abstract class empSchdlNumberingID : PX.Data.IBqlField
        {
        }

        [PXDBString(10)]
        [PXDefault]
        [PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "Staff Schedule Numbering Sequence")]
        public virtual string EmpSchdlNumberingID { get; set; }
        #endregion
        #region LicenseNumberingID
        public abstract class licenseNumberingID : PX.Data.IBqlField
        {
        }

        [PXDBString(10)]
        [PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "License Numbering Sequence")]
        public virtual string LicenseNumberingID { get; set; }
        #endregion
        #region EquipmentNumberingID
        public abstract class equipmentNumberingID : PX.Data.IBqlField
        {
        }

        [PXDBString(10)]
        [PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "Equipment Numbering Sequence")]
        public virtual string EquipmentNumberingID { get; set; }
        #endregion
        #region DenyWarnByAppOverlap
        public abstract class denyWarnByAppOverlap : ListField_AppointmentValidation
        {
        }

        [PXDBString(1, IsFixed = true)]
        [denyWarnByAppOverlap.ListAttribute]
        [PXDefault(ID.ValidationType.NOT_VALIDATE)]
        [PXUIField(DisplayName = "Overlapping Appointments")]
        public virtual string DenyWarnByAppOverlap { get; set; }
        #endregion
        #region AppointmentInPast
        public abstract class appointmentInPast : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Creation of Appointments in the Past")]
        public virtual bool? AppointmentInPast { get; set; }
        #endregion
        #region ManageRooms
        public abstract class manageRooms : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Rooms")]
        public virtual bool? ManageRooms { get; set; }
        #endregion
        #region ManageAttendees
        public abstract class manageAttendees : PX.Data.IBqlField
        {
        }
       
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Attendees")]
        public virtual bool? ManageAttendees { get; set; }
        #endregion
        #region DispatchBoardHelper
        #region DfltBranchID
        public abstract class dfltBranchID : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? DfltBranchID { get; set; }
        #endregion
        #endregion
        #region EnableEmpTimeCardIntegration
        public abstract class enableEmpTimeCardIntegration : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Time & Expenses Integration")]
        public virtual bool? EnableEmpTimeCardIntegration { get; set; }
        #endregion
        #region PostBatchNumberingID
        public abstract class postBatchNumberingID : PX.Data.IBqlField
        {
        }

        [PXDBString(10)]
        [PXDefault]
        [PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "Posting Batch Numbering Sequence")]
        public virtual string PostBatchNumberingID { get; set; }
        #endregion
        #region CustomerMultipleBillingOptions
        public abstract class customerMultipleBillingOptions : PX.Data.IBqlField
        { 
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Manage Multiple Billing Options per Customer")]
        public virtual bool? CustomerMultipleBillingOptions { get; set; }
        #endregion 
        #region AlertBeforeCloseServiceOrder
        public abstract class alertBeforeCloseServiceOrder : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Alert About Open Appointments Before Service Orders Are Closed")]
        public virtual bool? AlertBeforeCloseServiceOrder { get; set; }
        #endregion
        #region FilterInvoicingManually
        public abstract class filterInvoicingManually : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Filter Invoice Generation Forms Manually")]
        public virtual bool? FilterInvoicingManually { get; set; }
        #endregion
        #region EnableSeasonScheduleContract
        public abstract class enableSeasonScheduleContract : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Seasons in Schedule Contracts")]
        public virtual bool? EnableSeasonScheduleContract { get; set; }
        #endregion
        #region EquipmentCalculateWarrantyFrom
        public abstract class equipmentCalculateWarrantyFrom : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.EquipmentWarrantyFrom.SALES_ORDER_DATE)]
        [PXUIField(DisplayName = "Calculate Warranty From")]
        public virtual string EquipmentCalculateWarrantyFrom { get; set; }
        #endregion
        #region DfltCalendarStartTime
        public abstract class dfltCalendarStartTime : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameTime = "Calendar Start Time")]
        [PXUIField(DisplayName = "Calendar Start Time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? DfltCalendarStartTime { get; set; }
        #endregion
        #region MapApiKey
        public abstract class mapApiKey : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Bing Map API Key")]
        public virtual string MapApiKey { get; set; }
        #endregion
        #region TrackAppointmentLocation
        public abstract class trackAppointmentLocation : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Track Start and Completion Appointment Locations")]
        public virtual bool? TrackAppointmentLocation { get; set; }
        #endregion
        #region EnableGPSTracking
        public abstract class enableGPSTracking : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Track Mobile Devices Using GPS")]
        public virtual bool? EnableGPSTracking { get; set; }
        #endregion
        #region GPSRefreshTrackingTime
        public abstract class gPSRefreshTrackingTime : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(30)]
        [PXUIField(DisplayName = "Refresh GPS Locations on Maps Every")]
        public virtual int? GPSRefreshTrackingTime { get; set; }
        #endregion
        #region DisableFixScheduleAction
        public abstract class disableFixScheduleAction : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Fix Schedules Without Next Execution Date")]
        public virtual bool? DisableFixScheduleAction { get; set; }
        #endregion
        #region ContractPostTo
        public abstract class contractPostTo : ListField_PostTo_Contract
        {
        }

        [PXDBString(2)]
        [PXDefault(ID.Contract_PostTo.ACCOUNTS_RECEIVABLE_MODULE)]
        [PXUIField(DisplayName = "Post To")]
        public virtual string ContractPostTo { get; set; }
        #endregion
        #region DfltContractTermID_SO_AR
        public abstract class dfltContractTermIDARSO : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Default Terms", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<Terms.termsID,
                            Where<
                                Terms.visibleTo, Equal<TermsVisibleTo.all>,
                                Or<Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>),
                    DescriptionField = typeof(Terms.descr), Filterable = true)]
        public virtual string DfltContractTermIDARSO { get; set; }
        #endregion
        #region ContractPostOrderType
        public abstract class contractPostOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        [PXDefault]
        [PXUIField(DisplayName = "Order Type for Invoice", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<SOOrderType.orderType,
                            Where<SOOrderType.active, Equal<True>,
                                And<FSxSOOrderType.enableFSIntegration, Equal<True>>>>),
                    DescriptionField = typeof(SOOrderType.descr))]
        public virtual string ContractPostOrderType { get; set; }
        #endregion
        #region ContractCombineSubFrom

        public abstract class contractCombineSubFrom : PX.Data.IBqlField
        {
        }

        [PXDefault]
        [SubAccountMask(true, DisplayName = "Combine Sales Sub. From")]
        public virtual string ContractCombineSubFrom { get; set; }
        #endregion
        #region ContractSalesAcctSource
        public abstract class contractSalesAcctSource : ListField_Contract_SalesAcctSource
        {
        }

        [PXDBString(2)]
        [contractSalesAcctSource.ListAtrribute]
        [PXDefault(ID.SrvOrdType_SalesAcctSource.CUSTOMER_LOCATION)]
        [PXUIField(DisplayName = "Use Sales Account From")]
        public virtual string ContractSalesAcctSource { get; set; }
        #endregion
        #region EnableContractPeriodWhenInvoice
        public abstract class enableContractPeriodWhenInvoice : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Activate Upcoming Period on Invoice Generation")]
        public virtual bool? EnableContractPeriodWhenInvoice { get; set; }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        [PXUIField(DisplayName = "CreatedByID")]
        public virtual Guid? CreatedByID { get; set; }

        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "CreatedByScreenID")]
        public virtual string CreatedByScreenID { get; set; }

        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = "CreatedDateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }

        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "LastModifiedByID")]
        public virtual Guid? LastModifiedByID { get; set; }

        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "LastModifiedByScreenID")]
        public virtual string LastModifiedByScreenID { get; set; }

        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = "LastModifiedDateTime")]
        public virtual DateTime? LastModifiedDateTime { get; set; }

        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        [PXUIField(DisplayName = "tstamp")]
        public virtual byte[] tstamp { get; set; }

        #endregion
        #region EnableAllTargetEquipment
        public abstract class enableAllTargetEquipment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Service on All Target Equipment")]
        public virtual bool? EnableAllTargetEquipment { get; set; }
        #endregion  
        #region ReadyToUpgradeTo2017R2
        public abstract class readyToUpgradeTo2017R2 : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        public virtual bool? ReadyToUpgradeTo2017R2 { get; set; }
        #endregion

        #region MemoryHelper
        #region CustomDfltCalendarStartTime
        public abstract class customDfltCalendarStartTime : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string CustomDfltCalendarStartTime
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (this.DfltCalendarStartTime != null)
                {
                    return this.DfltCalendarStartTime.ToString();
                }

                return string.Empty;
            }
        }
        #endregion
        #region BillingOptionsChanged
        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual bool? BillingOptionsChanged { get; set; }
        #endregion
        #endregion
    }
}
