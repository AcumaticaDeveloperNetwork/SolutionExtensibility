using System;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.TX;
using System.Collections;
using PX.Objects.CS;
using System.Collections.Generic;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.PM
{
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[PXCacheName(Messages.Budget)]
	[Serializable]
	public class PMChangeOrderBudget : PX.Data.IBqlTable, IProjectFilter, IQuantify
	{
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
			public const int Length = 15;
		}

		[PXDBString(refNbr.Length, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBDefault(typeof(PMChangeOrder.refNbr))]
		[PXUIField(DisplayName = "Ref. Nbr.", Enabled = false)]
		public virtual String RefNbr
		{
			get;
			set;
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMChangeOrderBudget.type, Equal<Current<PMChangeOrderBudget.type>>>>>))]
		[PXDefault(typeof(PMChangeOrder.projectID))]
		[PXForeignReference(typeof(Field<projectID>.IsRelatedTo<PMProject.contractID>))]
		[PXDBInt(IsKey = true)]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public abstract class projectTaskID : PX.Data.IBqlField
		{
		}

		public int? TaskID => ProjectTaskID;

		[PXDefault(typeof(Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMChangeOrderBudget.type, Equal<Current<type>>>>>))]
		[PXDBInt(IsKey = true)]
		[PXForeignReference(typeof(Field<projectTaskID>.IsRelatedTo<PMTask.taskID>))]
		public virtual Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public abstract class costCodeID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostCodeID;
		[CostCode(IsKey = true, ReleasedField = typeof(released))]
		public virtual Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupID;
		[PXDefault]
		[AccountGroup(IsKey=true)]
		[PXForeignReference(typeof(Field<accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
		public virtual Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
        [PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible)]
		[PMInventorySelector]
		[PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<inventoryID>>>>))]
		[PXDefault]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion

		#region Type
		public abstract class type : PX.Data.IBqlField
		{
		}
		protected string _Type;
		[PXDBString(1)]
		[PXDefault]
		[PMAccountType.List]
		[PXUIField(DisplayName ="Type", Enabled = false)]
		public virtual string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
		#region Rate
		public abstract class rate : PX.Data.IBqlField
		{
		}
		protected Decimal? _Rate;
		[PXDBPriceCost]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unit Rate")]
		public virtual Decimal? Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				this._Rate = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		[PXDBString(Common.Constants.TranDescLength, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Description
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
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}
		protected Decimal? _Qty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Quantity")]
		public virtual Decimal? Qty
		{
			get
			{
				return this._Qty;
			}
			set
			{
				this._Qty = value;
			}
		}
		#endregion
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[PXDefault(typeof(Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public virtual String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region Amount
		public abstract class amount : PX.Data.IBqlField
		{
		}
		
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Amount")]
		public virtual Decimal? Amount
		{
			get;
			set;
		}
		#endregion
		#region RevisedQty
		public abstract class revisedQty : PX.Data.IBqlField
		{
		}
		[PXQuantity]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Revised Budgeted Quantity", Enabled = false)]
		public virtual Decimal? RevisedQty
		{
			get;
			set;
		}
		#endregion
		#region RevisedAmount
		public abstract class revisedAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Revised Budgeted Amount", Enabled = false)]
		public virtual Decimal? RevisedAmount
		{
			get;
			set;
		}
		#endregion

		#region PreviouslyApprovedQty
		public abstract class previouslyApprovedQty : PX.Data.IBqlField
		{
		}
		[PXQuantity]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? PreviouslyApprovedQty
		{
			get;
			set;
		}
		#endregion
		#region PreviouslyApprovedAmount
		public abstract class previouslyApprovedAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? PreviouslyApprovedAmount
		{
			get;
			set;
		}
		#endregion
		#region CommittedCOQty
		public abstract class committedCOQty : PX.Data.IBqlField
		{
		}
		[PXQuantity]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? CommittedCOQty
		{
			get;
			set;
		}
		#endregion
		#region CommittedCOAmount
		public abstract class committedCOAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? CommittedCOAmount
		{
			get;
			set;
		}
		#endregion
		#region OtherDraftRevisedAmount
		public abstract class otherDraftRevisedAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? OtherDraftRevisedAmount
		{
			get;
			set;
		}
		#endregion
		#region TotalPotentialRevisedAmount
		public abstract class totalPotentialRevisedAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? TotalPotentialRevisedAmount
		{
			[PXDependsOnFields(typeof(otherDraftRevisedAmount), typeof(revisedAmount))]
			get
			{
				return OtherDraftRevisedAmount + RevisedAmount;
			}
		}
		#endregion
		#region Released
		public abstract class released : PX.Data.IBqlField
		{
		}
		protected Boolean? _Released;
		[PXDBBool()]
		[PXUIField(DisplayName = "Released", Enabled = false)]
		[PXDefault(false)]
		public virtual Boolean? Released
		{
			get
			{
				return this._Released;
			}
			set
			{
				this._Released = value;
			}
		}
		#endregion
		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
        [PXNote]
		public virtual Guid? NoteID
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
	
		public virtual BudgetKeyTuple GetBudgetKey()
		{
			return new BudgetKeyTuple(ProjectID.GetValueOrDefault(),
				ProjectTaskID.GetValueOrDefault(),
				AccountGroupID.GetValueOrDefault(),
				InventoryID.GetValueOrDefault(PMInventorySelectorAttribute.EmptyInventoryID),
				CostCodeID.GetValueOrDefault(CostCodeAttribute.GetDefaultCostCode()));
		}
	}

	/// <summary>
	/// Used in Reports to calculate Previously invoiced amount.
	/// </summary>
	[System.SerializableAttribute()]
	[PXProjection(typeof(Select4<PMChangeOrderBudget,
		Where<PMChangeOrderBudget.refNbr, Less<Current<PMChangeOrderPrevioslyAmount.refNbr>>, And<PMChangeOrderBudget.type, Equal<GL.AccountType.income>>>,
		Aggregate<GroupBy<PMChangeOrderBudget.projectID,
			GroupBy<PMChangeOrderBudget.projectTaskID,
			GroupBy<PMChangeOrderBudget.accountGroupID,
			GroupBy<PMChangeOrderBudget.inventoryID,
			GroupBy<PMChangeOrderBudget.costCodeID,
			Sum<PMChangeOrderBudget.amount>>>>>>>>), Persistent = false)]
	public class PMChangeOrderPrevioslyAmount : IBqlTable
	{
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}
		[PXDBString(PMChangeOrderBudget.refNbr.Length, IsUnicode = true, IsKey = true, BqlField = typeof(PMChangeOrderBudget.refNbr))]
		public virtual String RefNbr
		{
			get;
			set;
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXDBInt(IsKey = true, BqlField = typeof(PMChangeOrderBudget.projectID))]
		[PXForeignReference(typeof(Field<projectID>.IsRelatedTo<PMProject.contractID>))]
		public virtual Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region TaskID
		public abstract class taskID : PX.Data.IBqlField
		{
		}
		protected Int32? _TaskID;
		[PXDBInt(IsKey = true, BqlField = typeof(PMChangeOrderBudget.projectTaskID))]
		[PXForeignReference(typeof(Field<taskID>.IsRelatedTo<PMTask.taskID>))]
		public virtual Int32? TaskID
		{
			get
			{
				return this._TaskID;
			}
			set
			{
				this._TaskID = value;
			}
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[PXDBInt(BqlField = typeof(PMChangeOrderBudget.inventoryID))]
		public virtual Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region CostCodeID
		public abstract class costCodeID : PX.Data.IBqlField
		{
		}
		protected Int32? _CostCodeID;
		[PXDBInt(BqlField = typeof(PMChangeOrderBudget.costCodeID))]
		public virtual Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountGroupID;
		[PXDBInt(IsKey = true, BqlField = typeof(PMChangeOrderBudget.accountGroupID))]
		[PXForeignReference(typeof(Field<accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
		public virtual Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region LineTotal
		public abstract class amount : PX.Data.IBqlField
		{
		}
		[PXDBBaseCury(BqlField = typeof(PMChangeOrderBudget.amount))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total")]
		public virtual Decimal? Amount
		{
			get; set;
		}
		#endregion
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[PXBreakInheritance]
	[Serializable]
	public class PMChangeOrderRevenueBudget : PMChangeOrderBudget
	{
		#region RefNbr
		public new abstract class refNbr : PX.Data.IBqlField
		{			
		}

		[PXParent(typeof(Select<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<refNbr>>, And<Current<type>, Equal<GL.AccountType.income>>>>))]
		[PXDBString(PMChangeOrderBudget.refNbr.Length, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBDefault(typeof(PMChangeOrder.refNbr))]
		[PXUIField(DisplayName = "Ref. Nbr.", Enabled = false)]
		public override String RefNbr
		{
			get;
			set;
		}
		#endregion
		#region ProjectID
		public new abstract class projectID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMChangeOrderRevenueBudget.type, Equal<GL.AccountType.income>>>>))]
		[PXDefault(typeof(PMChangeOrder.projectID))]
		[PXForeignReference(typeof(Field<projectID>.IsRelatedTo<PMProject.contractID>))]
		[PXDBInt(IsKey = true)]
		public override Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public new abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMChangeOrderRevenueBudget.type, Equal<GL.AccountType.income>>>>))]
		[PXForeignReference(typeof(Field<projectTaskID>.IsRelatedTo<PMTask.taskID>))]
		[ProjectTask(typeof(PMChangeOrderRevenueBudget.projectID), IsKey = true, AlwaysEnabled = true)]
		[PXForeignReference(typeof(Field<projectTaskID>.IsRelatedTo<PMTask.taskID>))]
		public override Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public new abstract class costCodeID : PX.Data.IBqlField
		{
		}
		[CostCode(null, typeof(projectTaskID), GL.AccountType.Income, typeof(accountGroupID),  IsKey = true, ReleasedField = typeof(released), SkipVerificationForDefault = true)]
		public override Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public new abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[AccountGroup(typeof(Where<PMAccountGroup.type, Equal<GL.AccountType.income>>), IsKey = true)]
		[PXForeignReference(typeof(Field<accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
		public override Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}
		
		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID")]
		[PXDefault]
		[PMInventorySelector]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region Type
		public new abstract class type : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXDefault(GL.AccountType.Income)]
		[PMAccountType.List()]
		[PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
		public override string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion
		
		#region UOM
		public new abstract class uOM : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public override String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region Rate
		public new abstract class rate : PX.Data.IBqlField
		{
		}
		[PXDBPriceCost]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unit Rate")]
		public override Decimal? Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				this._Rate = value;
			}
		}
		#endregion
		#region Description
		public new abstract class description : PX.Data.IBqlField
		{
		}
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
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
		
		#region Amount
		public new abstract class amount : PX.Data.IBqlField
		{
		}
		[PXFormula(typeof(Mult<qty, rate>), typeof(SumCalc<PMChangeOrder.revenueTotal>))]
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Amount")]
		public override Decimal? Amount
		{
			get;
			set;
		}
		#endregion
	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[PXBreakInheritance]
	[Serializable]
	public class PMChangeOrderCostBudget : PMChangeOrderBudget
	{
		#region RefNbr
		public new abstract class refNbr : PX.Data.IBqlField
		{
		}

		[PXParent(typeof(Select<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<refNbr>>, And<Current<type>, Equal<GL.AccountType.expense>>>>))]
		[PXDBString(PMChangeOrderBudget.refNbr.Length, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBDefault(typeof(PMChangeOrder.refNbr))]
		[PXUIField(DisplayName = "Ref. Nbr.", Enabled = false)]
		public override String RefNbr
		{
			get;
			set;
		}
		#endregion
		#region ProjectID
		public new abstract class projectID : PX.Data.IBqlField
		{
		}
		[PXParent(typeof(Select<PMProject, Where<PMProject.contractID, Equal<Current<projectID>>, And<PMChangeOrderCostBudget.type, Equal<GL.AccountType.expense>>>>))]
		[PXDefault(typeof(PMChangeOrder.projectID))]
		[PXForeignReference(typeof(Field<projectID>.IsRelatedTo<PMProject.contractID>))]
		[PXDBInt(IsKey = true)]
		public override Int32? ProjectID
		{
			get
			{
				return this._ProjectID;
			}
			set
			{
				this._ProjectID = value;
			}
		}
		#endregion
		#region ProjectTaskID
		public new abstract class projectTaskID : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
		[PXForeignReference(typeof(Field<projectTaskID>.IsRelatedTo<PMTask.taskID>))]
		[PXParent(typeof(Select<PMTask, Where<PMTask.taskID, Equal<Current<projectTaskID>>, And<PMChangeOrderCostBudget.type, Equal<GL.AccountType.expense>>>>))]		
		[ProjectTask(typeof(PMChangeOrderCostBudget.projectID), IsKey = true, AlwaysEnabled = true, DirtyRead = true)]
		[PXForeignReference(typeof(Field<projectTaskID>.IsRelatedTo<PMTask.taskID>))]
		public override Int32? ProjectTaskID
		{
			get;
			set;
		}
		#endregion
		#region CostCodeID
		public new abstract class costCodeID : PX.Data.IBqlField
		{
		}
		[CostCode(null, typeof(projectTaskID), GL.AccountType.Expense, typeof(accountGroupID), IsKey = true, ReleasedField = typeof(released))]
		public override Int32? CostCodeID
		{
			get
			{
				return this._CostCodeID;
			}
			set
			{
				this._CostCodeID = value;
			}
		}
		#endregion
		#region AccountGroupID
		public new abstract class accountGroupID : PX.Data.IBqlField
		{
		}
		[PXDefault]
		[PXForeignReference(typeof(Field<accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
		[AccountGroup(typeof(Where<PMAccountGroup.isExpense, Equal<True>>), IsKey = true)]
		public override Int32? AccountGroupID
		{
			get
			{
				return this._AccountGroupID;
			}
			set
			{
				this._AccountGroupID = value;
			}
		}
		#endregion
		#region InventoryID
		public new abstract class inventoryID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Inventory ID")]
		[PXDefault]
		[PMInventorySelector]
		public override Int32? InventoryID
		{
			get
			{
				return this._InventoryID;
			}
			set
			{
				this._InventoryID = value;
			}
		}
		#endregion
		#region Type
		public new abstract class type : PX.Data.IBqlField
		{
		}
		[PXDBString(1)]
		[PXDefault(GL.AccountType.Expense)]
		[PMAccountType.List()]
		[PXUIField(DisplayName = "Budget Type", Visible = false, Enabled = false)]
		public override string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}
		#endregion

		#region UOM
		public new abstract class uOM : PX.Data.IBqlField
		{
		}
		[PXDefault(typeof(Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<PMBudget.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PMUnit(typeof(inventoryID))]
		public override String UOM
		{
			get
			{
				return this._UOM;
			}
			set
			{
				this._UOM = value;
			}
		}
		#endregion
		#region Rate
		public new abstract class rate : PX.Data.IBqlField
		{
		}
		[PXDBPriceCost]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unit Rate")]
		public override Decimal? Rate
		{
			get
			{
				return this._Rate;
			}
			set
			{
				this._Rate = value;
			}
		}
		#endregion
		#region Description
		public new abstract class description : PX.Data.IBqlField
		{
		}
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
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

		#region Amount
		public new abstract class amount : PX.Data.IBqlField
		{
		}
		[PXFormula(typeof(Mult<qty, rate>), typeof(SumCalc<PMChangeOrder.costTotal>))]
		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount")]
		public override Decimal? Amount
		{
			get;
			set;
		}
		#endregion
	}


	
}
