using PX.Data;
using System;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CT;
using PX.Objects.IN;

namespace PX.Objects.PM
{
	
	[System.SerializableAttribute()]
    [PXPrimaryGraph(typeof(SetupMaint))]
    [PXCacheName(Messages.PMSetupMaint)]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMSetup : PX.Data.IBqlTable
	{
		public const string DefaultNonProjectCode = "X";

		#region IsActive
		public abstract class isActive : PX.Data.IBqlField
		{
		}
		[PXBool()]
		[PXUIField(Visible=false)]
		public virtual Boolean? IsActive
		{
            get { return PXAccess.FeatureInstalled<FeaturesSet.projectModule>(); }			
		}
		#endregion		
		#region NonProjectCode
		public abstract class nonProjectCode : PX.Data.IBqlField
		{
		}
		protected string _NonProjectCode;
		[PXDefault(DefaultNonProjectCode)]
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Non-Project Code")]
		public virtual string NonProjectCode
		{
			get
			{
				return this._NonProjectCode;
			}
			set
			{
				this._NonProjectCode = value;
			}
		}
		#endregion
		#region EmptyItemCode
		public abstract class emptyItemCode : PX.Data.IBqlField
		{
		}
		[PXDefault(PMInventorySelectorAttribute.EmptyComponentCD)]
		[PXDBString(30, IsUnicode = true)]
		[PXUIField(DisplayName = "Empty Item Code")]
		public virtual string EmptyItemCode
		{
			get;
			set;
		}
        #endregion
		#region EmptyItemUOM
		public abstract class emptyItemUOM : PX.Data.IBqlField
		{
		}
		[PXDBString(6, IsUnicode = true)]
		[PXDefault("HOUR")]
		[PXUIField(DisplayName = "Empty Item UOM")]
		[PXSelector(typeof(Search4<INUnit.fromUnit,
							Where<INUnit.unitType, Equal<INUnitType.global>>,
							Aggregate<GroupBy<INUnit.fromUnit>>>))]
		public virtual string EmptyItemUOM
		{
		    get;
		    set;
		}
		#endregion
		#region TranNumbering
		public abstract class tranNumbering : PX.Data.IBqlField
		{
		}
		protected String _TranNumbering;
		[PXDefault("PMTRAN")]
		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "Transaction Numbering Sequence")]
		public virtual String TranNumbering
		{
			get
			{
				return this._TranNumbering;
			}
			set
			{
				this._TranNumbering = value;
			}
		}
		#endregion
		#region ProformaNumbering
		public abstract class proformaNumbering : PX.Data.IBqlField
		{
		}
		[PXDefault("PROFORMA")]
		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Pro Forma Numbering Sequence")]
		public virtual String ProformaNumbering
		{
            get;
            set;
		}
		#endregion
		#region ChangeOrderNumbering
		public abstract class changeOrderNumbering : PX.Data.IBqlField
		{
		}
		[PXDefault("CHANGEORD")]
		[PXDBString(10, IsUnicode = true)]
		[PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
		[PXUIField(DisplayName = "Change Order Numbering Sequence", FieldClass = PMChangeOrder.FieldClass)]
		public virtual String ChangeOrderNumbering
		{
			get;
			set;
		}
		#endregion
		#region DefaultChangeOrderClassID
		public abstract class defaultChangeOrderClassID : PX.Data.IBqlField
		{
		}
		[PXForeignReference(typeof(Field<defaultChangeOrderClassID>.IsRelatedTo<PMChangeOrderClass.classID>))]
		[PXDBString(PMChangeOrderClass.classID.Length, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Default Change Order Class", Visibility = PXUIVisibility.SelectorVisible, FieldClass = PMChangeOrder.FieldClass)]
		[PXSelector(typeof(Search<PMChangeOrderClass.classID, Where<PMChangeOrderClass.isActive, Equal<True>>>), DescriptionField = typeof(PMChangeOrderClass.description))]
		public virtual String DefaultChangeOrderClassID
		{
			get;
			set;
		}
		#endregion
		#region AutoPost
		public abstract class autoPost : PX.Data.IBqlField
		{
		}
		protected Boolean? _AutoPost;
		[PXDBBool()]
		[PXDefault(true)]
        [PXUIField(DisplayName = "Automatically Post on Release")]
		public virtual Boolean? AutoPost
		{
			get
			{
				return this._AutoPost;
			}
			set
			{
				this._AutoPost = value;
			}
		}
		#endregion
		#region AutoReleaseAllocation
		public abstract class autoReleaseAllocation : PX.Data.IBqlField
		{
		}
		protected Boolean? _AutoReleaseAllocation;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Automatically Release Allocations")]
		public virtual Boolean? AutoReleaseAllocation
		{
			get
			{
				return this._AutoReleaseAllocation;
			}
			set
			{
				this._AutoReleaseAllocation = value;
			}
		}
		#endregion
		#region BatchNumberingID
		public abstract class batchNumberingID : IBqlField
		{
		}
		protected String _BatchNumberingID;
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("BATCH")]
		[PXUIField(DisplayName = "Batch Numbering Sequence")]
		[PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
		public virtual String BatchNumberingID
		{
			get
			{
				return this._BatchNumberingID;
			}
			set
			{
				this._BatchNumberingID = value;
			}
		}
		#endregion
		#region ExpenseAccountSource
		public abstract class expenseAccountSource : PX.Data.IBqlField
		{
		}
		protected String _ExpenseAccountSource;
		[PXDBString(1, IsFixed = true)]
		[PMExpenseAccountSource.List()]
		[PXDefault(PMExpenseAccountSource.InventoryItem)]
		[PXUIField(DisplayName = "Expense Account Source", Required = true)]
		public virtual String ExpenseAccountSource
		{
			get
			{
				return this._ExpenseAccountSource;
			}
			set
			{
				this._ExpenseAccountSource = value;
			}
		}
		#endregion
		#region ExpenseSubMask
		public abstract class expenseSubMask : PX.Data.IBqlField
		{
		}
		protected String _ExpenseSubMask;
		[PXDefault]
        [SubAccountMaskAttribute(DisplayName = "Expense Sub. Source")]
		public virtual String ExpenseSubMask
		{
			get
			{
				return this._ExpenseSubMask;
			}
			set
			{
				this._ExpenseSubMask = value;
			}
		}
		#endregion
        #region ExpenseAccrualAccountSource
        public abstract class expenseAccrualAccountSource : PX.Data.IBqlField
        {
        }
        protected String _ExpenseAccrualAccountSource;
        [PXDBString(1, IsFixed = true)]
        [PMExpenseAccountSource.AccrualList()]
        [PXDefault(PMExpenseAccountSource.InventoryItem)]
        [PXUIField(DisplayName = "Expense Accrual Account Source", Required = true)]
        public virtual String ExpenseAccrualAccountSource
        {
            get
            {
                return this._ExpenseAccrualAccountSource;
            }
            set
            {
                this._ExpenseAccrualAccountSource = value;
            }
        }
        #endregion
        #region ExpenseAccrualSubMask
        public abstract class expenseAccrualSubMask : PX.Data.IBqlField
        {
        }
        protected String _ExpenseAccrualSubMask;
        [PXDefault]
        [SubAccountMaskAttribute(DisplayName = "Expense Accrual Sub. Source")]
        public virtual String ExpenseAccrualSubMask
        {
            get
            {
                return this._ExpenseAccrualSubMask;
            }
            set
            {
                this._ExpenseAccrualSubMask = value;
            }
        }
        #endregion
		#region AssignmentMapID
		public abstract class assignmentMapID : PX.Data.IBqlField
		{
		}
		protected int? _AssignmentMapID;
		[PXDBInt]
		[PXSelector(
			typeof(Search<
				EPAssignmentMap.assignmentMapID, 
				Where<
					EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeProject>,
					And<EPAssignmentMap.mapType, NotEqual<EPMapType.assignment>>>>))]
		[PXUIField(DisplayName = "Project Approval Map")]
		public virtual int? AssignmentMapID
		{
			get
			{
				return this._AssignmentMapID;
			}
			set
			{
				this._AssignmentMapID = value;
			}
		}
		#endregion
        #region AssignmentNotificationID
        public abstract class  assignmentNotificationID : PX.Data.IBqlField
        {
        }
        protected int? _AssignmentNotificationID;
        [PXDBInt]
        [PXSelector(typeof(Search<PX.SM.Notification.notificationID>))]
        [PXUIField(DisplayName = "Pending Project Approval Notification")]
        public virtual int? AssignmentNotificationID
        {
            get
            {
                return this._AssignmentNotificationID;
            }
            set
            {
                this._AssignmentNotificationID = value;
            }
        }
		#endregion
		#region ProformaApprovalMapID
		public abstract class proformaApprovalMapID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(
			typeof(Search<
				EPAssignmentMap.assignmentMapID,
				Where<
					EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeProforma>,
					And<EPAssignmentMap.mapType, Equal<EPMapType.approval>>>>))]
		[PXUIField(DisplayName = "Pro Forma Approval Map")]
		public virtual int? ProformaApprovalMapID
		{
			get;set;
		}
		#endregion
		#region ProformaApprovalNotificationID
		public abstract class proformaApprovalNotificationID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(typeof(Search<PX.SM.Notification.notificationID>))]
		[PXUIField(DisplayName = "Pending Pro Forma Approval Notification")]
		public virtual int? ProformaApprovalNotificationID
		{
			get;set;
		}
		#endregion
		#region ProformaAssignmentMapID
		public abstract class proformaAssignmentMapID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(
			typeof(Search<
				EPAssignmentMap.assignmentMapID,
				Where<
					EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeProforma>,
					And<EPAssignmentMap.mapType, Equal<EPMapType.assignment>>>>))]
		[PXUIField(DisplayName = "Pro Forma Assignment Map", Visible = false)]
		public virtual int? ProformaAssignmentMapID
		{
			get; set;
		}
		#endregion
		#region ProformaAssignmentNotificationID
		public abstract class proformaAssignmentNotificationID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(typeof(Search<PX.SM.Notification.notificationID>))]
		[PXUIField(DisplayName = "Pro Forma Assignment Notification", Visible = false)]
		public virtual int? ProformaAssignmentNotificationID
		{
			get; set;
		}
		#endregion
		#region ChangeOrderApprovalMapID
		public abstract class changeOrderApprovalMapID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(
			typeof(Search<
				EPAssignmentMap.assignmentMapID,
				Where<
					EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeChangeOrder>,
					And<EPAssignmentMap.mapType, Equal<EPMapType.approval>>>>))]
		[PXUIField(DisplayName = "Change Order Approval Map", FieldClass = PMChangeOrder.FieldClass)]
		public virtual int? ChangeOrderApprovalMapID
		{
			get; set;
		}
		#endregion
		#region ChangeOrderApprovalNotificationID
		public abstract class changeOrderApprovalNotificationID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(typeof(Search<PX.SM.Notification.notificationID>))]
		[PXUIField(DisplayName = "Pending Change Order Approval Notification", FieldClass = PMChangeOrder.FieldClass)]
		public virtual int? ChangeOrderApprovalNotificationID
		{
			get; set;
		}
		#endregion
		#region QuoteTemplateID
		public abstract class quoteTemplateID : PX.Data.IBqlField { }
		[PXUIField(DisplayName = "Default Quote Template", FieldClass = ProjectAttribute.DimensionName)]
		[PXDimensionSelector(ProjectAttribute.DimensionName,
				typeof(Search2<PMProject.contractID,
						LeftJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<PMProject.contractID>>>,
							Where<PMProject.baseType, Equal<CTPRType.projectTemplate>, And<PMProject.isActive, Equal<True>>>>),
				typeof(PMProject.contractCD),
				typeof(PMProject.contractCD),
				typeof(PMProject.description),
				typeof(PMProject.budgetLevel),
				typeof(PMProject.billingID),
				typeof(ContractBillingSchedule.type),
				typeof(PMProject.approverID),
				DescriptionField = typeof(PMProject.description))]
		[PXDBInt()]
		public virtual Int32? QuoteTemplateID { get; set; }
		#endregion
		#region QuoteApprovalMapID
		public abstract class quoteApprovalMapID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(
			typeof(Search<
				EPAssignmentMap.assignmentMapID,
				Where<
					EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeProjectQuotes>,
					And<EPAssignmentMap.mapType, NotEqual<EPMapType.assignment>>>>))]
		[PXUIField(DisplayName = "Quote Approval Map")]
		public virtual int? QuoteApprovalMapID
		{
			get; set;
		}
		#endregion

		#region QuoteApprovalNotificationID
		public abstract class quoteApprovalNotificationID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXSelector(typeof(Search<PX.SM.Notification.notificationID>))]
		[PXDefault(292, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Quote Pending Approval Notification")]
		public virtual int? QuoteApprovalNotificationID
		{
			get; set;
		}
		#endregion

		#region CostCommitmentTracking
		public abstract class costCommitmentTracking : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Internal Cost Commitment Tracking")]
		public virtual Boolean? CostCommitmentTracking
		{
			get;
			set;
		}
		#endregion
		#region RevenueCommitmentTracking
		public abstract class revenueCommitmentTracking : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Internal Revenue Commitment Tracking")]
		public virtual Boolean? RevenueCommitmentTracking
		{
			get;
			set;
		}
		#endregion
		#region CutoffDate
		public abstract class cutoffDate : PX.Data.IBqlField
		{
		}
		protected String _CutoffDate;
		[PXDBString(1, IsFixed = true)]
		[PMCutOffDate.List()]
		[PXDefault(PMCutOffDate.Included)]
		[PXUIField(DisplayName = "Billing Cut-off Date", Required = true)]
		public virtual String CutoffDate
		{
			get
			{
				return this._CutoffDate;
			}
			set
			{
				this._CutoffDate = value;
			}
		}
		#endregion
		#region OverLimitErrorLevel
		public abstract class overLimitErrorLevel : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXUIField(DisplayName = "Validate T&M Revenue Budget Limits")]
		[OverLimitValidationOption.List]
		[PXDefault(OverLimitValidationOption.Error)]
		public virtual String OverLimitErrorLevel
		{
			get;
			set;
		}
		#endregion
		#region CostBudgetUpdateMode
		public abstract class costBudgetUpdateMode : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXUIField(DisplayName = "Cost Budget Update")]
		[CostBudgetUpdateModes.List]
		[PXDefault(CostBudgetUpdateModes.Detailed)]
		public virtual String CostBudgetUpdateMode
		{
			get;
			set;
		}
		#endregion
		#region VisibleInGL
		public abstract class visibleInGL : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInGL;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "GL")]
		public virtual Boolean? VisibleInGL
		{
			get
			{
				return this._VisibleInGL;
			}
			set
			{
				this._VisibleInGL = value;
			}
		}
		#endregion
		#region VisibleInAP
		public abstract class visibleInAP : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInAP;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "AP")]
		public virtual Boolean? VisibleInAP
		{
			get
			{
				return this._VisibleInAP;
			}
			set
			{
				this._VisibleInAP = value;
			}
		}
		#endregion
		#region VisibleInAR
		public abstract class visibleInAR : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInAR;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "AR")]
		public virtual Boolean? VisibleInAR
		{
			get
			{
				return this._VisibleInAR;
			}
			set
			{
				this._VisibleInAR = value;
			}
		}
		#endregion
		#region VisibleInSO
		public abstract class visibleInSO : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInSO;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "SO")]
		public virtual Boolean? VisibleInSO
		{
			get
			{
				return this._VisibleInSO;
			}
			set
			{
				this._VisibleInSO = value;
			}
		}
		#endregion
		#region VisibleInPO
		public abstract class visibleInPO : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInPO;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "PO")]
		public virtual Boolean? VisibleInPO
		{
			get
			{
				return this._VisibleInPO;
			}
			set
			{
				this._VisibleInPO = value;
			}
		}
		#endregion
		
		#region VisibleInTA
		public abstract class visibleInTA : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInTA;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Time Entries")]
		public virtual Boolean? VisibleInTA
		{
			get
			{
				return this._VisibleInTA;
			}
			set
			{
				this._VisibleInTA = value;
			}
		}
		#endregion
		#region VisibleInEA
		public abstract class visibleInEA : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInEA;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Expenses")]
		public virtual Boolean? VisibleInEA
		{
			get
			{
				return this._VisibleInEA;
			}
			set
			{
				this._VisibleInEA = value;
			}
		}
		#endregion
		#region VisibleInIN
		public abstract class visibleInIN : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInIN;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "IN")]
		public virtual Boolean? VisibleInIN
		{
			get
			{
				return this._VisibleInIN;
			}
			set
			{
				this._VisibleInIN = value;
			}
		}
		#endregion
		#region VisibleInCA
		public abstract class visibleInCA : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInCA;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "CA")]
		public virtual Boolean? VisibleInCA
		{
			get
			{
				return this._VisibleInCA;
			}
			set
			{
				this._VisibleInCA = value;
			}
		}
		#endregion
		#region VisibleInCR
		public abstract class visibleInCR : PX.Data.IBqlField
		{
		}
		protected Boolean? _VisibleInCR;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "CRM")]
		public virtual Boolean? VisibleInCR
		{
			get
			{
				return this._VisibleInCR;
			}
			set
			{
				this._VisibleInCR = value;
			}
		}
		#endregion
		#region RestrictProjectSelect
		public abstract class restrictProjectSelect : PX.Data.IBqlField
		{
		}
		protected String _RestrictProjectSelect;
		[PMRestrictOption.List]
		[PXDBString(1)]
		[PXDefault(PMRestrictOption.CustomerProjects)]
		[PXUIField(DisplayName = "Restrict Project Selection")]
		public virtual String RestrictProjectSelect
		{
			get
			{
				return this._RestrictProjectSelect;
			}
			set
			{
				this._RestrictProjectSelect = value;
			}
		}
		#endregion

		#region QuoteNumberingID
		public abstract class quoteNumberingID : PX.Data.IBqlField { }
		[PXDBString(10, IsUnicode = true)]
		[PXDefault("PMQUOTE")]
		[PXUIField(DisplayName = "Quote Numbering Sequence")]
		[PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
		public virtual String QuoteNumberingID { get; set; }
		#endregion

		#region System Columns
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#endregion
	}

	[PXBreakInheritance]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMSetupProjectApproval : PMSetup, IAssignedMap
	{
		int? IAssignedMap.AssignmentMapID
		{
			get
			{
				return this.AssignmentMapID;
			}
			set
			{
				this.AssignmentMapID = value;
			}
		}

		int? IAssignedMap.AssignmentNotificationID
		{
			get
			{
				return this.AssignmentNotificationID;
			}
			set
			{
				this.AssignmentNotificationID = value;
			}
		}

		bool? IAssignedMap.IsActive
		{
			get
			{
				return this.IsActive;
			}
		}
	}

	[PXBreakInheritance]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public class PMSetupProformaApproval : PMSetup, IAssignedMap
	{
		int? IAssignedMap.AssignmentMapID
		{
			get
			{
				return this.ProformaAssignmentMapID;
			}
			set
			{
				this.ProformaAssignmentMapID = value;
			}
		}

		int? IAssignedMap.AssignmentNotificationID
		{
			get
			{
				return this.ProformaAssignmentNotificationID;
			}
			set
			{
				this.ProformaAssignmentNotificationID = value;
			}
		}

		bool? IAssignedMap.IsActive
		{
			get
			{
				return this.IsActive;
			}
		}
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class PMExpenseAccountSource
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(InventoryItem, Messages.AccountSource_LaborItem),
					Pair(Project, Messages.AccountSource_Project),
					Pair(Task, Messages.AccountSource_Task),
					Pair(Employee, Messages.AccountSource_Employee),
				}) {}
		}

		public class AccrualListAttribute : PXStringListAttribute
		{
			public AccrualListAttribute() : base(
				new[]
				{
					Pair(InventoryItem, Messages.AccountSource_LaborItem_Accrual),
					Pair(Project, Messages.AccountSource_ProjectAccrual),
					Pair(Task, Messages.AccountSource_Task_Accrual),
				}) {}
		}

		public const string Project = "P";
		public const string Task = "T";
		public const string InventoryItem = "I";
		public const string Employee = "E";
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class PMCutOffDate
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(Included, Messages.Included),
					Pair(Excluded, Messages.Excluded),
				}) {}
		}

		public const string Included = "I";
		public const string Excluded = "E";
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class PMRestrictOption
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { AllProjects, CustomerProjects },
				new string[] { Messages.PMRestrict_AllProjects, Messages.PMRestrict_CustomerProjects })
			{; }
		}

		public const string AllProjects = "A";
		public const string CustomerProjects = "C";

		public class allProjects : Constant<string>
		{
			public allProjects() : base(AllProjects) {; }
		}

		public class customerProjects : Constant<string>
		{
			public customerProjects() : base(CustomerProjects) {; }
		}

	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class OverLimitValidationOption
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Error, Warning },
				new string[] { Messages.Validation_Error, Messages.Validation_Warning })
			{; }
		}

		public const string Error = "E";
		public const string Warning = "W";
		
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class CostBudgetUpdateModes
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Detailed, Summary },
				new string[] { Messages.BudgetUpdate_Detailed, Messages.BudgetUpdate_Summary })
			{; }
		}

		public const string Detailed = "D";
		public const string Summary = "S";
		
	}
}
