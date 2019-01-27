using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.EP;
using PX.Objects.IN;
using PX.Objects.AP;
using PX.Objects.PO;
using PX.Objects.CM;
using PX.Objects.GL;

namespace PX.Objects.PM
{
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[PXCacheName(Messages.ChangeOrderLine)]
	[PXPrimaryGraph(typeof(ChangeOrderEntry))]
	[Serializable]
	public class PMChangeOrderLine : PX.Data.IBqlTable, IQuantify
	{
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{			
		}

		[PXDBString(PMChangeOrder.refNbr.Length, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBDefault(typeof(PMChangeOrder.refNbr))]
		[PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
		[PXParent(typeof(Select<PMChangeOrder, Where<PMChangeOrder.refNbr, Equal<Current<PMChangeOrderLine.refNbr>>>>))]
		public virtual String RefNbr
		{
			get;
			set;
		}
		#endregion
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _LineNbr;
		[PXDBInt(IsKey = true)]
		[PXLineNbr(typeof(PMChangeOrder.lineCntr))]
		[PXUIField(DisplayName = "Line Nbr.", Visible = false)]
		public virtual Int32? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region ProjectID
		public abstract class projectID : PX.Data.IBqlField
		{
		}
		protected Int32? _ProjectID;
		[PXDBDefault(typeof(PMChangeOrder.projectID))]
		[PXDBInt]
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
		[PXDefault(typeof(Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
		[ProjectTask(typeof(projectID), AlwaysEnabled = true)]
		public virtual Int32? TaskID
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
		[CostCode(typeof(accountID), typeof(taskID), GL.AccountType.Expense, ReleasedField = typeof(released))]
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
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}
		protected Int32? _InventoryID;
		[Inventory(Filterable = true)]
		[PXParent(typeof(Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<inventoryID>>>>))]
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
		#region SubItemID
		public abstract class subItemID : PX.Data.IBqlField
		{
		}
		protected Int32? _SubItemID;
		[PXDefault(typeof(Search<InventoryItem.defaultSubItemID,
			Where<InventoryItem.inventoryID, Equal<Current<inventoryID>>,
			And<InventoryItem.defaultSubItemOnEntry, Equal<True>>>>),
			PersistingCheck = PXPersistingCheck.Nothing)]
		[SubItem(typeof(inventoryID))]
		public virtual Int32? SubItemID
		{
			get
			{
				return this._SubItemID;
			}
			set
			{
				this._SubItemID = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		
		[PXDBString(256, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual String Description
		{
			get;
			set;
		}
		#endregion
		#region VendorID
		public abstract class vendorID : PX.Data.IBqlField
		{
		}
		protected Int32? _VendorID;
		[PXDefault]
		[Vendor()]
		public virtual Int32? VendorID
		{
			get
			{
				return this._VendorID;
			}
			set
			{
				this._VendorID = value;
			}
		}
		#endregion
		#region POOrderType
		public abstract class pOOrderType : PX.Data.IBqlField
		{
		}
		[PXDefault(PO.POOrderType.RegularOrder)]
		[PXDBString(2, IsFixed = true)]
		[PXUIField(DisplayName = "PO Type", Enabled = false)]
		[PO.POOrderType.RBDList]
		public virtual String POOrderType
		{
			get;
			set;
		}
		#endregion
		#region POOrderNbr
		public abstract class pOOrderNbr : PX.Data.IBqlField
		{
		}
		
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "PO Nbr.")]
		[PXSelector(typeof(Search5<POLine.orderNbr, 
			InnerJoin<POOrder, On<POLine.orderType, Equal<POOrder.orderType>, And<POLine.orderNbr, Equal<POOrder.orderNbr>>>>,
			Where<POLine.orderType, Equal<Current<pOOrderType>>,
			And<POLine.projectID, Equal<Current<PMChangeOrder.projectID>>,
			And<POLine.cancelled, Equal<False>,
			And<POLine.completed, Equal<False>,
			And<POOrder.approved, Equal<True>,
			And<POOrder.behavior, Equal<POBehavior.changeOrder>,
			And<Where<Current<vendorID>, IsNull, Or<POLine.vendorID, Equal<Current<vendorID>>>>>>>>>>>,
			Aggregate<GroupBy<POLine.orderType, GroupBy<POLine.orderNbr, GroupBy<POLine.vendorID>>>>>), 
			typeof(POLine.orderType), typeof(POLine.orderNbr), typeof(POLine.vendorID))]
		public virtual String POOrderNbr
		{
			get;
			set;
		}
		#endregion
		#region POLineNbr
		public abstract class pOLineNbr : PX.Data.IBqlField
		{
		}
		protected Int32? _POLineNbr;
		[PXDBInt()]
		[PXUIField(DisplayName = "PO Line Nbr.")]
		[PXSelector(typeof(Search<POLine.lineNbr, Where<POLine.orderType, Equal<Current<pOOrderType>>, 
			And<POLine.orderNbr, Equal<Current<pOOrderNbr>>,
			And<POLine.projectID, Equal<Current<PMChangeOrder.projectID>>,
			And<POLine.cancelled, Equal<False>,
			And<POLine.completed, Equal<False>>>>>>>),
			typeof(POLine.lineNbr), typeof(POLine.lineType), typeof(POLine.inventoryID), typeof(POLine.tranDesc),
			typeof(POLine.uOM), typeof(POLine.orderQty), typeof(POLine.curyExtCost))]
		public virtual Int32? POLineNbr
		{
			get
			{
				return this._POLineNbr;
			}
			set
			{
				this._POLineNbr = value;
			}
		}
		#endregion
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
		protected String _CuryID;
		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
		[PXDefault(typeof(Search<Company.baseCuryID>))]
		[PXSelector(typeof(Currency.curyID))]
		public virtual String CuryID
		{
			get
			{
				return this._CuryID;
			}
			set
			{
				this._CuryID = value;
			}
		}
		#endregion
		#region UOM
		public abstract class uOM : PX.Data.IBqlField
		{
		}
		protected String _UOM;
		[PXDefault(typeof(Search<InventoryItem.purchaseUnit, Where<InventoryItem.inventoryID, Equal<Current<inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
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
		#region AccountID
		public abstract class accountID : PX.Data.IBqlField
		{
		}
		protected Int32? _AccountID;
		[Account(null, typeof(Search<Account.accountID, Where<Account.accountGroupID, IsNotNull>>), DisplayName = "Account", DescriptionField = typeof(Account.description))]
		public virtual Int32? AccountID
		{
			get
			{
				return this._AccountID;
			}
			set
			{
				this._AccountID = value;
			}
		}
		#endregion
		#region Qty
		public abstract class qty : PX.Data.IBqlField
		{
		}
		protected Decimal? _Qty;
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "0.0")]
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
		#region UnitCost
		public abstract class unitCost : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unit Cost")]
		public virtual Decimal? UnitCost
		{
			get;
			set;
		}
		#endregion
		#region Amount
		public abstract class amount : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount")]
		[PXFormula(typeof(Mult<qty, unitCost>))]
		public virtual Decimal? Amount
		{
			get;
			set;
		}
		#endregion
		#region AmountInBaseCury
		public abstract class amountInBaseCury : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount in Base Currency", Enabled = false)]
		[PXFormula(null, typeof(SumCalc<PMChangeOrder.commitmentTotal>))]
		public virtual Decimal? AmountInBaseCury
		{
			get;
			set;
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
		#region LineType
		public abstract class lineType : PX.Data.IBqlField
		{
		}
		[PXDBString(1, IsFixed = true)]
		[ChangeOrderLineType.List()]
		[PXDefault(ChangeOrderLineType.NewDocument)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		public virtual String LineType
		{
			get;
			set;
		}
		#endregion
	   
	   #region PotentialRevisedQty
	   public abstract class potentialRevisedQty : PX.Data.IBqlField
		{
		}
		[PXQuantity]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? PotentialRevisedQty
		{
			get;
			set;
		}
		#endregion
		#region PotentialRevisedAmount
		public abstract class potentialRevisedAmount : PX.Data.IBqlField
		{
		}

		[PXBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? PotentialRevisedAmount
		{
			get;
			set;
		}
		#endregion

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
		[PXNote(DescriptionField = typeof(PMChangeOrderLine.description))]
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

	}

	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public static class ChangeOrderLineType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				  new[]
				  {
					Pair(Update, Messages.ChangeOrderLine_Update),
					Pair(NewDocument, Messages.ChangeOrderLine_NewDocument),
					Pair(NewLine, Messages.ChangeOrderLine_NewLine),
				  })
			{ }

		}
		public const string Update = "U";
		public const string NewDocument = "L";
		public const string NewLine = "D";

	}
}
