using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO.LandedCosts.Attributes;
using PX.Objects.SO;
using PX.Objects.TX;

namespace PX.Objects.PO
{
    [Serializable]
	[PXCacheName(Messages.POLandedCostDetail)]
	public class POLandedCostDetail : PX.Data.IBqlTable, ISortOrder
	{
		#region Selected
		public abstract class selected : IBqlField
		{
		}
		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
		{
			get;
			set;
		}
		#endregion
		#region BranchID
		public abstract class branchID : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// Identifier of the <see cref="PX.Objects.GL.Branch">Branch</see>, to which the transaction belongs.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
		/// </value>
		[Branch(typeof(POLandedCostDoc.branchID))]
		public virtual Int32? BranchID
		{
			get;
			set;
		}
		#endregion
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
		}
		/// <summary>
		/// [key] The type of the landed cost receipt line.
		/// </summary>
		/// <value>
		/// The field is determined by the type of the parent <see cref="POLandedCostDoc">document</see>.
		/// For the list of possible values see <see cref="POLandedCostDoc.DocType"/>.
		/// </value>
		[POLandedCostDocType.List()]
		[PXDBString(1, IsKey = true, IsFixed = true)]
		[PXDBDefault(typeof(POLandedCostDoc.docType))]
		[PXUIField(DisplayName = "Doc. Type", Visibility = PXUIVisibility.Visible, Visible = false)]
		public virtual String DocType
		{
			get;
			set;
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// [key] Reference number of the parent <see cref="POLandedCostDoc">document</see>.
		/// </summary>
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(POLandedCostDoc.refNbr))]
		[PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXParent(typeof(Select<POLandedCostDoc, Where<POLandedCostDoc.docType, Equal<Current<docType>>, And<POLandedCostDoc.refNbr, Equal<Current<refNbr>>>>>))]
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

		/// <summary>
		/// [key] The number of the transaction line in the document.
		/// </summary>
		/// <value>
		/// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
		/// </value>
		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXLineNbr(typeof(POLandedCostDoc.lineCntr))]
		public virtual Int32? LineNbr
		{
			get;
			set;
		}
		#endregion
		#region SortOrder
		public abstract class sortOrder : PX.Data.IBqlField
		{
			public const string DispalyName = "Line Order";
		}

		[PXDBInt]
		[PXUIField(DisplayName = sortOrder.DispalyName, Visible = false, Enabled = false)]
		public virtual Int32? SortOrder
		{
			get;
			set;
		}
		#endregion

		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		[PXDBLong()]
		[CurrencyInfo(typeof(POLandedCostDoc.curyInfoID))]
		public virtual Int64? CuryInfoID
		{
			get;
			set;
		}
		#endregion

		#region LandedCostCodeID
		public abstract class landedCostCodeID : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// The <see cref="PX.Objects.PO.LandedCostCode">landed cost code</see> used to describe the specific landed costs incurred for the line.
		/// This code is one of the codes associated with the vendor.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="PX.Objects.PO.LandedCostCode.LandedCostCodeID">LandedCostCode.LandedCostCodeID</see> field.
		/// </value>
		[PXDBString(15, IsUnicode = true, IsFixed = false)]
		[PXUIField(DisplayName = "Landed Cost Code")]
		[PXSelector(typeof(Search<Objects.PO.LandedCostCode.landedCostCodeID>))]
		public virtual String LandedCostCodeID
		{
			get;
			set;
		}
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}

		[PXDBString(150, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXLocalizableDefault(typeof(Search<LandedCostCode.descr, Where<LandedCostCode.landedCostCodeID, Equal<Current<landedCostCodeID>>>>),
			typeof(Customer.localeName), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String Descr
		{
			get;
			set;
		}
		#endregion
		#region AllocationMethod
		public abstract class allocationMethod : PX.Data.IBqlField
		{
		}
		[PXDBString(1, IsFixed = true)]
		[PXFormula(typeof(Selector<landedCostCodeID, LandedCostCode.allocationMethod>))]
		[LandedCostAllocationMethod.List()]
		[PXUIField(DisplayName = "Allocation Method", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		public virtual String AllocationMethod
		{
			get;
			set;
		}
		#endregion
		#region CuryLineAmt
		public abstract class curyLineAmt : PX.Data.IBqlField
		{
		}

		[PXDBCurrency(typeof(curyInfoID), typeof(lineAmt))]
		[PXUIField(DisplayName = "Amount")]
		//[PXFormula(typeof(curyLineAmt), typeof(SumCalc<POLandedCostDoc.curyOrigDocAmt>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryLineAmt
		{
			get;
			set;
		}
		#endregion
		#region LineAmt
		public abstract class lineAmt : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? LineAmt
		{
			get;
			set;
		}
		#endregion
		#region TaxCategoryID
		public abstract class taxCategoryID : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// Identifier of the <see cref="TaxCategory">tax category</see> associated with the line.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="TaxCategory.TaxCategoryID"/> field.
		/// Defaults to the <see cref="InventoryItem.TaxCategoryID">tax category associated with the line item</see>.
		/// </value>
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
		[POLandedCostTax(typeof(POLandedCostDoc), typeof(POLandedCostTax), typeof(POLandedCostTaxTran))]
		[PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
		[PXRestrictor(typeof(Where<TaxCategory.active, Equal<True>>), TX.Messages.InactiveTaxCategory, typeof(TaxCategory.taxCategoryID))]
		[PXDefault(typeof(Search<LandedCostCode.taxCategoryID, Where<LandedCostCode.landedCostCodeID, Equal<Current<landedCostCodeID>>>>), 
			PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual String TaxCategoryID
		{
			get;
			set;
		}
		#endregion
		#region InventoryID
		public abstract class inventoryID : PX.Data.IBqlField
		{
		}

		/// <summary>
		/// Identifier of the <see cref="PX.Objects.IN.InventoryItem">inventory item</see> associated with the transaction.
		/// </summary>
		/// <value>
		/// Corresponds to the <see cref="PX.Objects.IN.InventoryItem.InventoryID">InventoryItem.InventoryID</see> field.
		/// </value>
		[PXDBInt()]
		[PXUIField(DisplayName = "Inventory ID", Visibility = PXUIVisibility.Visible)]
		[LandedCostDetailInventory]
		[PXForeignReference(typeof(Field<inventoryID>.IsRelatedTo<InventoryItem.inventoryID>))]
		/*[IN.Inventory(typeof(Search5<IN.InventoryItem.inventoryID,
			InnerJoin<POLandedCostReceiptLine, On<POLandedCostReceiptLine.inventoryID, Equal<IN.InventoryItem.inventoryID>>>,
			Where<POLandedCostReceiptLine.docType, Equal<Current<POLandedCostDoc.docType>>,
				And<POLandedCostReceiptLine.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>, Aggregate<GroupBy<InventoryItem.inventoryID, GroupBy<InventoryItem.inventoryCD, GroupBy<InventoryItem.descr>>>>>), typeof(IN.InventoryItem.inventoryCD), typeof(IN.InventoryItem.descr), DisplayName = "Inventory ID")]
		*/
		public virtual Int32? InventoryID
		{
			get;
			set;
		}
		#endregion
		#region SubItemID
		public abstract class subItemID : PX.Data.IBqlField
		{
		}

		[PXDefault(typeof(Search<InventoryItem.defaultSubItemID,
				Where<InventoryItem.inventoryID, Equal<Current<inventoryID>>,
					And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>),
			PersistingCheck = PXPersistingCheck.Nothing)]
		[SubItem(typeof(inventoryID))]
		public virtual Int32? SubItemID
		{
			get;
			set;
		}
		#endregion

		#region LCAccrualAcct
		public abstract class lCAccrualAcct : PX.Data.IBqlField
		{
		}
		protected Int32? _LCAccrualAcct;

		[PXDBInt]
		[PXDefault(typeof(Search<LandedCostCode.lCAccrualAcct, Where<LandedCostCode.landedCostCodeID, Equal<Current<landedCostCodeID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? LCAccrualAcct
		{
			get
			{
				return this._LCAccrualAcct;
			}
			set
			{
				this._LCAccrualAcct = value;
			}
		}
		#endregion
		#region LCAccrualSub
		public abstract class lCAccrualSub : PX.Data.IBqlField
		{
		}
		protected Int32? _LCAccrualSub;

		[PXDBInt()]
		[PXDefault(typeof(Search<LandedCostCode.lCAccrualSub, Where<LandedCostCode.landedCostCodeID, Equal<Current<landedCostCodeID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Int32? LCAccrualSub
		{
			get
			{
				return this._LCAccrualSub;
			}
			set
			{
				this._LCAccrualSub = value;
			}
		}
		#endregion

		#region APDocType
		public abstract class aPDocType : PX.Data.IBqlField
		{
		}

		[PXDBString(3, IsFixed = true)]
		[PXUIField(DisplayName = "AP Doc. Type", Enabled = false)]
		[APDocType.List()]
		public virtual String APDocType
		{
			get;
			set;
		}
		#endregion
		#region APRefNbr
		public abstract class aPRefNbr : PX.Data.IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "AP Ref. Nbr.", Enabled = false)]
		[PXSelector(typeof(Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<Current<aPDocType>>>>))]
		public virtual String APRefNbr
		{
			get;
			set;
		}
		#endregion
		#region INDocType
		public abstract class iNDocType : PX.Data.IBqlField
		{
		}

		[PXDBString(2, IsFixed = true)]
		[PXUIField(DisplayName = "IN Doc. Type", Enabled = false)]
		[INDocType.List()]
		public virtual String INDocType
		{
			get;
			set;
		}
		#endregion
		#region INRefNbr
		public abstract class iNRefNbr : PX.Data.IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "IN Ref. Nbr.", Enabled = false)]
		[PXSelector(typeof(Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<iNDocType>>>>))]
		public virtual String INRefNbr
		{
			get;
			set;
		}
		#endregion

		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		[PXNote()]
		public virtual Guid? NoteID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}

		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get;
			set;
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}

		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}

		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get;
			set;
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime
		{
			get;
			set;
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}

		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get;
			set;
		}
		#endregion
	}
}
