using PX.Data.EP;
using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.TM;

namespace PX.Objects.CT
{

	[PXCacheName(Messages.ContractTemplate)]
	[PXPrimaryGraph(typeof(TemplateMaint))]
	[Serializable]
	[PXBreakInheritance]
	public partial class ContractTemplate : Contract
	{
		#region ContractID
		public new abstract class contractID : PX.Data.IBqlField
		{
		}

		[PXDBIdentity()]
		[PXSelector(typeof(ContractTemplate.contractID))]
		[PXUIField(DisplayName = "Template ID")]
		public override Int32? ContractID
		{
			get
			{
				return this._ContractID;
			}
			set
			{
				this._ContractID = value;
			}
		}
		#endregion
		#region BaseType
		public new abstract class baseType : IBqlField
		{
		}
		[PXUIField(DisplayName = "Entity Type", Enabled = false)]
		[PXDBString(1, IsFixed = true, IsKey = true)]
		[CTPRType.List]
		[PXDefault(CTPRType.ContractTemplate)]
		public override string BaseType
		{
			get => base.BaseType;
			set => base.BaseType = value;
		}
		#endregion
		#region ContractCD
		public new abstract class contractCD : PX.Data.IBqlField
		{
		}

		[PXDimensionSelector(ContractTemplateAttribute.DimensionName, 
			typeof(Search<ContractTemplate.contractCD, 
				Where<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>>), 
			typeof(ContractTemplate.contractCD), DescriptionField = typeof(ContractTemplate.description))]
		[PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = "Contract Template", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public override String ContractCD
		{
			get
			{
				return this._ContractCD;
			}
			set
			{
				this._ContractCD = value;
			}
		}
        #endregion
        #region NoteID
        public new abstract class noteID : PX.Data.IBqlField
        {
        }
        [PXNote()]
        public override Guid? NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }
        #endregion
        #region Description
        public new abstract class description : PX.Data.IBqlField
		{
		}
		[PXDBLocalizableString(60, IsUnicode = true)]
		[PXDefault(PersistingCheck=PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public override String Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		#endregion
		#region Status
		public new abstract class status : PX.Data.IBqlField
		{
		}
		[PXDBString(1, IsFixed = true)]
		[Contract.status.List]
		[PXDefault(Contract.status.Active)]
		[PXUIField(DisplayName = "Active", Required = true, Visibility = PXUIVisibility.SelectorVisible)]
		public override String Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}
		#endregion
		#region CustomerID
		public new abstract class customerID : PX.Data.IBqlField
		{
		}
		[PXDBInt()]
		public override Int32? CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				this._CustomerID = value;
			}
		}
		#endregion
		#region StartDate
		public new abstract class startDate : PX.Data.IBqlField
		{
		}
		[PXDBDate()]
		public override DateTime? StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				this._StartDate = value;
			}
		}
		#endregion
		#region ExpireDate
		public new abstract class expireDate : PX.Data.IBqlField
		{
		}
		[PXDBDate()]
		public override DateTime? ExpireDate
		{
			get
			{
				return this._ExpireDate;
			}
			set
			{
				this._ExpireDate = value;
			}
		}
		#endregion
		#region IsTemplate
		[Obsolete(Common.InternalMessages.PropertyIsObsoleteAndWillBeRemoved2019R2)]
		public new abstract class isTemplate : IBqlField
		{
		}
		#endregion
		#region IsContinuous
		public new abstract class isContinuous : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXUIField(DisplayName = "Shift Expiration Date on Renewal", Visibility = PXUIVisibility.Visible)]
		[PXDefault(true)]
		public override Boolean? IsContinuous
		{
			get
			{
				return this._IsContinuous;
			}
			set
			{
				this._IsContinuous = value;
			}
		}
		#endregion
		#region TemplateID
		public new abstract class templateID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		public override Int32? TemplateID
		{
			get
			{
				return this._TemplateID;
			}
			set
			{
				this._TemplateID = value;
			}
		}
		#endregion
		#region AutomaticReleaseAR
		public new abstract class automaticReleaseAR : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Automatically Release AR Documents", Visibility=PXUIVisibility.Visible)]
		public override Boolean? AutomaticReleaseAR
		{
			get
			{
				return this._AutomaticReleaseAR;
			}
			set
			{
				this._AutomaticReleaseAR = value;
			}
		}
		#endregion
		#region DetailedBilling
		public new abstract class detailedBilling : PX.Data.IBqlField
		{
		}
		#endregion
		#region LocationID
		public new abstract class locationID : PX.Data.IBqlField
		{
		}
		
		[PXDBInt]
		public override Int32? LocationID
		{
			get
			{
				return this._LocationID;
			}
			set
			{
				this._LocationID = value;
			}
		}
		#endregion
		#region TerminationDate
		public new abstract class terminationDate : PX.Data.IBqlField
		{
		}
		[PXDBDate()]
		[PXUIField(DisplayName = "Termination Date")]
		public override DateTime? TerminationDate
		{
			get
			{
				return this._TerminationDate;
			}
			set
			{
				this._TerminationDate = value;
			}
		}
		#endregion
		#region GracePeriod
		public new abstract class gracePeriod : IBqlField {}
		[PXDBInt(MinValue = 0, MaxValue = 365)]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Grace Period")]
		[PXUIEnabled(typeof(Where<type, Equal<type.renewable>>))]
		public override int? GracePeriod
		{
			get
			{
				return _GracePeriod;
			}
			set
			{
				_GracePeriod = value;
			}
		}
		#endregion
		#region GraceDate
		public new abstract class graceDate : IBqlField { }

		/// <summary>
		/// End Date of Grace Period.
		/// </summary>
		[PXDBCalced(typeof(Add<ContractTemplate.expireDate, ContractTemplate.gracePeriod>), typeof(DateTime))]
		public override DateTime? GraceDate { get; set; }
		#endregion
		#region CuryID
		public new abstract class curyID : PX.Data.IBqlField
		{
		}
		#endregion
		#region AllowOverrideFormulaDescription
		public abstract class allowOverrideFormulaDescription : IBqlField { }
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Enable Overriding Formulas in Contracts")]
		public virtual Boolean? AllowOverrideFormulaDescription { get; set; }
		#endregion
		#region ApproverID
		public new abstract class approverID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXEPEmployeeSelector]
		[PXUIField(DisplayName = "Contract Activity Approver")]
		public override Int32? ApproverID
		{
			get
			{
				return this._ApproverID;
			}
			set
			{
				this._ApproverID = value;
			}
		}
		#endregion
		#region OwnerID
		public new abstract class ownerID : IBqlField { }
		[PXDBGuid()]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXOwnerSelector(typeof(Contract.workgroupID))]
		[PXUIField(DisplayName = "Owner")]
		public override Guid? OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				this._OwnerID = value;
			}
		}
		#endregion
		#region RevID
		new public abstract class revID : PX.Data.IBqlField
		{
		}
		#endregion
		#region LineCtr
		new public abstract class lineCtr : PX.Data.IBqlField
		{
		}
		#endregion
		#region DurationType
		public new abstract class durationType : IBqlField { }
		#endregion

		#region Attributes
		public new abstract class attributes : IBqlField
		{
		}
		[CRAttributesField(typeof(ContractTemplate.contractID))]
		public override string[] Attributes { get; set; }
		#endregion
		#region ItemClassStrID
		public abstract class contractStrID : IBqlField { }
		[PXString]
		[PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
		public virtual string ContractStrID => this.ContractID.ToString();
		#endregion
	}
}
