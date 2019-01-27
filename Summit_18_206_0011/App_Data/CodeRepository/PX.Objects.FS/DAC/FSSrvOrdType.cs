using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.SO;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.ORDER_TYPE)]
    [PXPrimaryGraph(typeof(SvrOrdTypeMaint))]
	public class FSSrvOrdType : PX.Data.IBqlTable
	{
        #region SrvOrdTypeID
        public abstract class srvOrdTypeID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        [PXUIField(Enabled = false)]
        public virtual int? SrvOrdTypeID { get; set; }
        #endregion
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
		{
		}

        [PXDBString(4, IsKey = true, InputMask = ">AAAA", IsFixed = true)]
		[PXUIField(DisplayName = "Service Order Type", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSSrvOrdType.srvOrdType))]
        [PXDefault]
        [NormalizeWhiteSpace]
        public virtual string SrvOrdType { get; set; }
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
        #region AllowPartialBilling
        public abstract class allowPartialBilling : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Partial Billing", Enabled = false)]
        public virtual bool? AllowPartialBilling { get; set; }
        #endregion
        #region AllowQuickProcess
        public abstract class allowQuickProcess : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Quick Process")]
        public virtual bool? AllowQuickProcess { get; set; }
        #endregion
        #region AppAddressSource
        public abstract class appAddressSource : ListField_SrvOrdType_AddressSource
        {
        }

        [PXDBString(2)]
        [appAddressSource.ListAtrribute]
        [PXDefault(ID.SrvOrdType_AppAddressSource.BUSINESS_ACCOUNT)]
        [PXUIField(DisplayName = "Take Address and Contact Information From")]
        public virtual string AppAddressSource { get; set; }
        #endregion
        #region AppContactInfoSource
        public abstract class appContactInfoSource : PX.Data.IBqlField
        {
        }

        [PXDBString(2)]
        [PXUIField(DisplayName = "Appointment Contact info source", Visible = false)]
        public virtual string AppContactInfoSource { get; set; }
        #endregion
        #region AppWithMultEmp
        public abstract class appWithMultEmp : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Assignment of Multiple Staff Members")]
        public virtual bool? AppWithMultEmp { get; set; }
        #endregion
        #region AppWithoutEmp
        public abstract class appWithoutEmp : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Creation Without Assigning a Staff Member")]
        public virtual bool? AppWithoutEmp { get; set; }
        #endregion
        #region AppWithoutSrv
        public abstract class appWithoutSrv : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Creation Without Specifying a Service")]
        public virtual bool? AppWithoutSrv { get; set; }
        #endregion
        #region BAccountRequired
        public abstract class bAccountRequired : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Business Account", Enabled = false)]
        public virtual bool? BAccountRequired { get; set; }
        #endregion
        #region Behavior
        public abstract class behavior : ListField_Behavior_SrvOrdType
        {
        }

        [PXDBString(2, IsFixed = true)]
        [behavior.ListAttribute]
        [PXDefault(ID.Behavior_SrvOrderType.REGULAR_APPOINTMENT)]
        [PXUIField(DisplayName = "Behavior")]
        public virtual string Behavior { get; set; }
        #endregion
        #region BillSeparately
        public abstract class billSeparately : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Bill Separately", Enabled = false)]
        public virtual bool? BillSeparately { get; set; }
        #endregion
        #region CombineSubFrom

        public abstract class combineSubFrom : PX.Data.IBqlField
        {
        }

        [PXDefault]
        [SubAccountMask(DisplayName = "Combine Sales Sub. From")]
        public virtual string CombineSubFrom { get; set; }
        #endregion
        #region CompleteSrvOrdWhenSrvDone
        public abstract class completeSrvOrdWhenSrvDone : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Complete Service Order When Its Appointments Are Completed")]
        public virtual bool? CompleteSrvOrdWhenSrvDone { get; set; }
        #endregion
        #region CloseSrvOrdWhenSrvDone
        public abstract class closeSrvOrdWhenSrvDone : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Close Service Orders when its Appointments are Closed")]
        public virtual bool? CloseSrvOrdWhenSrvDone { get; set; }
        #endregion
        #region DfltTermID_SO_AR
        public abstract class dfltTermIDARSO : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Default Terms for AR and SO", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<Terms.termsID,
                            Where<
                                Terms.visibleTo, Equal<TermsVisibleTo.all>,
                                Or<Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>),
                    DescriptionField = typeof(Terms.descr), Filterable = true)]
        public virtual string DfltTermIDARSO { get; set; }
        #endregion
        #region DfltTermIDAP
        public abstract class dfltTermIDAP : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Default Terms for AP", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<Terms.termsID,
                            Where<
                                Terms.visibleTo, Equal<TermsVisibleTo.all>,
                                Or<Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>),
                    DescriptionField = typeof(Terms.descr), Filterable = true)]
        public virtual string DfltTermIDAP { get; set; }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
        {
        }

        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string Descr { get; set; }
        #endregion
        #region EnableINPosting
        public abstract class enableINPosting : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXFormula(typeof(Default<FSSrvOrdType.postTo>))]
        [PXFormula(typeof(Default<FSSrvOrdType.behavior>))]
        [PXUIField(DisplayName = "Post Pickup/Delivery Items to Inventory")]
        public virtual bool? EnableINPosting { get; set; }
        #endregion
        #region GenerateInvoiceBy
        public abstract class generateInvoiceBy : ListField_SrvOrdType_GenerateInvoiceBy
        {
        }

        [PXDBString(4)]
        [generateInvoiceBy.ListAtrribute]
        [PXDefault(ID.SrvOrdType_GenerateInvoiceBy.SALES_ORDER)]
        [PXUIField(DisplayName = "Generate Invoice By")]
        public virtual string GenerateInvoiceBy { get; set; }
        #endregion
        #region InvoiceCompleteAppointment
        public abstract class allowInvoiceOnlyClosedAppointment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Appointment Closing for Invoice Generation")]
        public virtual bool? AllowInvoiceOnlyClosedAppointment { get; set; }
        #endregion
        #region PostNegBalanceToAP
        public abstract class postNegBalanceToAP : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXFormula(typeof(Default<FSSrvOrdType.postTo>))]
        [PXUIField(DisplayName = "Create a Bill for Negative Balances")]
        public virtual bool? PostNegBalanceToAP { get; set; }
        #endregion
        #region PostOrderType
        public abstract class postOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        [PXDefault]
        [PXUIField(DisplayName = "Order Type for Invoice", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<SOOrderType.orderType,
                            Where<
                                SOOrderType.active, Equal<True>,
                                And<FSxSOOrderType.enableFSIntegration, Equal<True>>>>),
                    DescriptionField = typeof(SOOrderType.descr))]
        public virtual string PostOrderType { get; set; }
        #endregion
        #region PostOrderTypeNegativeBalance
        public abstract class postOrderTypeNegativeBalance : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        [PXDefault]
        [PXUIField(DisplayName = "Order Type for Negative Balance Invoice", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<SOOrderType.orderType,
                            Where<
                                SOOrderType.active, Equal<True>,
                                And<FSxSOOrderType.enableFSIntegration, Equal<True>,
                                And<SOOrderType.aRDocType, Equal<ARInvoiceType.creditMemo>>>>>),
                    DescriptionField = typeof(SOOrderType.descr))]
        public virtual string PostOrderTypeNegativeBalance { get; set; }
        #endregion
        #region AllocationOrderType
        public abstract class allocationOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        [PXDefault(typeof(Search<SOOrderType.orderType, 
                    Where<SOOrderType.active, Equal<True>,
                    And<SOOrderType.orderType, Equal<SOOrderTypeConstants.salesOrder>>>>))]
        [PXUIField(DisplayName = "Order Type for Allocation", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(Search<SOOrderType.orderType, 
                    Where<SOOrderType.active, Equal<True>, 
                    And<SOOrderType.behavior, Equal<SOOrderTypeConstants.salesOrder>>>>), 
                    DescriptionField = typeof(SOOrderType.descr))]
        public virtual string AllocationOrderType { get; set; }
        #endregion
        #region PostTo
        public abstract class postTo : ListField_PostTo_SrvOrdType
        {
        }

        [PXDBString(2)]
        [PXDefault(ID.SrvOrdType_PostTo.ACCOUNTS_RECEIVABLE_MODULE)]
        [PXUIField(DisplayName = "Generate Invoices In")]
        public virtual string PostTo { get; set; }
        #endregion
        #region RequireAppConfirmation
        public abstract class requireAppConfirmation : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Appointment Confirmation Required", Visible = false)]
        public virtual bool? RequireAppConfirmation { get; set; }
        #endregion        
        #region RequireAddressValidation
        public abstract class requireAddressValidation : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Address Validation")]
        public virtual bool? RequireAddressValidation { get; set; }
        #endregion
        #region RequireContact
        public abstract class requireContact : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Contact")]
        public virtual bool? RequireContact { get; set; }
        #endregion
        #region RequireCustomerSignature
        public abstract class requireCustomerSignature : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Require Customer Signature on Mobile App")]
        public virtual bool? RequireCustomerSignature { get; set; }
        #endregion
        #region RequireRoom
        public abstract class requireRoom : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Room")]
        public virtual bool? RequireRoom { get; set; }
        #endregion
        #region RequireRoute
        public abstract class requireRoute : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Route", Enabled = false)]
        public virtual bool? RequireRoute { get; set; }
        #endregion
        #region SalesAcctSource
        public abstract class salesAcctSource : ListField_SrvOrdType_SalesAcctSource
        {
        }

        [PXDBString(2)]
        [salesAcctSource.ListAtrribute]
        [PXDefault(ID.SrvOrdType_SalesAcctSource.CUSTOMER_LOCATION)]
        [PXUIField(DisplayName = "Use Sales Account From")]
        public virtual string SalesAcctSource { get; set; }
        #endregion
        #region SingleAppointment
        public abstract class singleAppointment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Only One Appointment per Service Order")]
        public virtual bool? SingleAppointment { get; set; }
        #endregion        
        #region SingleService
        public abstract class singleService : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Only One Service per Service Order/Appointment")]
        public virtual bool? SingleService { get; set; }
        #endregion
        #region SrvOrdNumberingID
        public abstract class srvOrdNumberingID : PX.Data.IBqlField
        {
        }

        [PXDBString(10)]
        [PXDefault]
        [PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "Numbering Sequence")]
        public virtual string SrvOrdNumberingID { get; set; }
        #endregion
        #region SubID
        public abstract class subID : PX.Data.IBqlField
        {
        }

        [SubAccount]
        [PXUIField(DisplayName = "General Subaccount")]
        public virtual int? SubID { get; set; }
        #endregion
        #region RequireTimeApprovalToInvoice
        public abstract class requireTimeApprovalToInvoice : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Time Approval to Close Appointments")]
        public virtual bool? RequireTimeApprovalToInvoice { get; set; }
        #endregion
        #region CreateTimeActivitiesFromAppointment
        public abstract class createTimeActivitiesFromAppointment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(typeof(Search<FSSetup.enableEmpTimeCardIntegration>))]
        [PXUIField(DisplayName = "Automatically Create Time Activities from Appointments")]
        public virtual bool? CreateTimeActivitiesFromAppointment { get; set; }
        #endregion
        #region DfltEarningType
        public abstract class dfltEarningType : PX.Data.IBqlField
        { 
        }

        [PXDBString(2, IsFixed = true, IsUnicode = false, InputMask = ">LL")]
        [PXDefault(typeof(Search<EPSetup.regularHoursType>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(EPEarningType.typeCD))]
        [PXUIField(DisplayName = "Default Earning Type")]
        public virtual string DfltEarningType { get; set; }
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
        [PXUIField(DisplayName = "Created On")]
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
        [PXUIField(DisplayName = "Last Modified On")]
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

        #region StartAppointmentActionBehavior
        public abstract class startAppointmentActionBehavior : ListField_SrvOrdType_StartAppointmentActionBehavior
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.SrvOrdType_StartAppointmentActionBehavior.HEADER_ONLY)]
        [startAppointmentActionBehavior.ListAtrribute]
        [PXUIField(DisplayName = "Start Appointment Action behavior")]
        public virtual string StartAppointmentActionBehavior { get; set; }
        #endregion
        #region CompleteAppointmentActionBehavior
        public abstract class completeAppointmentActionBehavior : ListField_SrvOrdType_CompleteAppointmentActionBehavior
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.SrvOrdType_CompleteAppointmentActionBehavior.NOTHING)]
        [completeAppointmentActionBehavior.ListAtrribute]
        [PXUIField(DisplayName = "Complete Appointment Action behavior")]
        public virtual string CompleteAppointmentActionBehavior { get; set; }
        #endregion
        #region UpdateServiceActualDateTimeBegin
        public abstract class updateServiceActualDateTimeBegin : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Update Actual Start Time in Service Lines When Updating Actual Start Time in Header")]
        public virtual bool? UpdateServiceActualDateTimeBegin { get; set; }
        #endregion
        #region UpdateServiceActualDateTimeEnd
        public abstract class updateServiceActualDateTimeEnd : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Update End Time in Service Lines When Updating Actual End Time in Header")]
        public virtual bool? UpdateServiceActualDateTimeEnd { get; set; }
        #endregion
        #region RequireServiceActualDateTimes
        public abstract class requireServiceActualDateTimes : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Actual Start/End Time of Service Lines to Complete the Appointment")]
        public virtual bool? RequireServiceActualDateTimes { get; set; }
        #endregion
        #region UpdateHeaderActualDateTimes
        public abstract class updateHeaderActualDateTimes : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Update Actual Start/End Time of Header Based on Minimum/Maximum Service Lines")]
        public virtual bool? UpdateHeaderActualDateTimes { get; set; }
        #endregion
        #region KeepActualDateTimes
        public abstract class keepActualDateTimes : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Manually Handle Time per Service Line")]
        public virtual bool? KeepActualDateTimes { get; set; }
        #endregion
        #region SalesPersonID
        public abstract class salesPersonID : PX.Data.IBqlField
        {
        }

        [SalesPerson(DisplayName = "Salesperson ID")]
        public virtual int? SalesPersonID { get; set; }
        #endregion
        #region Commissionable
        public abstract class commissionable : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Commissionable")]
        public virtual bool? Commissionable { get; set; }
        #endregion

        #region Notes and Attachments
        #region CopyNotesFromCustomer
        public abstract class copyNotesFromCustomer : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Notes From Customer")]
        public virtual bool? CopyNotesFromCustomer { get; set; }
        #endregion
        #region CopyAttachmentsFromCustomer
        public abstract class copyAttachmentsFromCustomer : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Attachments From Customer")]
        public virtual bool? CopyAttachmentsFromCustomer { get; set; }
        #endregion
        #region CopyNotesFromCustomerLocation
        public abstract class copyNotesFromCustomerLocation : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Notes From Customer Location")]
        public virtual bool? CopyNotesFromCustomerLocation { get; set; }
        #endregion
        #region CopyAttachmentsFromCustomerLocation
        public abstract class copyAttachmentsFromCustomerLocation : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Attachments From Customer Location")]
        public virtual bool? CopyAttachmentsFromCustomerLocation { get; set; }
        #endregion
        #region CopyNotesToAppoinment
        public abstract class copyNotesToAppoinment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Notes To Appoinment")]
        public virtual bool? CopyNotesToAppoinment { get; set; }
        #endregion
        #region CopyAttachmentsToAppoinment
        public abstract class copyAttachmentsToAppoinment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Attachments To Appoinment")]
        public virtual bool? CopyAttachmentsToAppoinment { get; set; }
        #endregion
        #region CopyLineNotesToInvoice
        public abstract class copyLineNotesToInvoice : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Line Notes To Invoice")]
        public virtual bool? CopyLineNotesToInvoice { get; set; }
        #endregion
        #region CopyLineAttachmentsToInvoice
        public abstract class copyLineAttachmentsToInvoice : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Copy Line Attachments To Invoice")]
        public virtual bool? CopyLineAttachmentsToInvoice { get; set; }
        #endregion
        #endregion

        #region ShowQuickProcessTab
        public abstract class showQuickProcessTab : PX.Data.IBqlField
        {
        }

        [PXBool]
        public virtual bool? ShowQuickProcessTab
        {
            get
            {
                return this.AllowQuickProcess == true 
                        && this.PostTo != ID.SrvOrdType_PostTo.NONE
                            && this.PostTo != ID.SrvOrdType_PostTo.ACCOUNTS_RECEIVABLE_MODULE;
            }
        }
        #endregion
    }
}
