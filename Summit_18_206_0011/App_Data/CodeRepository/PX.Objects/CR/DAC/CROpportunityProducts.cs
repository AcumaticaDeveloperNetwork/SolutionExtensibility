using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects.SO;
using PX.Objects.PM;
using PX.Objects.GL;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.Common;

namespace PX.Objects.CR
{
    [PXCacheName(Messages.OpportunityProducts)]
    [Serializable]
    public partial class CROpportunityProducts : IBqlTable, IHasMinGrossProfit, ISortOrder
    {
		#region QuoteID
		public abstract class quoteID : PX.Data.IBqlField { }
		[PXDBGuid(IsKey = true)]
		[PXDBDefault(typeof(CROpportunity.quoteNoteID))]
		[PXParent(typeof(Select<CROpportunity,
			Where<CROpportunity.quoteNoteID, Equal<Current<CROpportunityProducts.quoteID>>>>))]
		public virtual Guid? QuoteID { get; set; }
		#endregion

        #region LineNbr
        public abstract class lineNbr : IBqlField { }

		[PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
		[PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(CROpportunity.productCntr))]
        public virtual Int32? LineNbr { get; set; }
		#endregion

		#region SortOrder
		public abstract class sortOrder : PX.Data.IBqlField
		{
		}
		[PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
		[PXDBInt]
		public virtual Int32? SortOrder
		{
			get;
			set;
		}
		#endregion

		#region GroupLineNbr
		public abstract class groupLineNbr : PX.Data.IBqlField
		{
		}
		
		[PXUIField(DisplayName = "Group Line Nbr.", Visible = false, Enabled = false)]
		[PXDBInt]
		public virtual Int32? GroupLineNbr
		{
			get;
			set;
		}
		#endregion

		#region IsGroup
		public abstract class isGroup : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Group", Enabled = false)]
		public virtual Boolean? IsGroup
		{
			get;
			set;
		}
		#endregion

		#region LineType
		public abstract class lineType : PX.Data.IBqlField { }

        [PXDBString(3, IsFixed = false)]
        [PXUIField(DisplayName = "Type")]
        [CROpportunityProductLineType()]
        [PXDefault(typeof(CROpportunityProductLineTypeAttribute.distribution))]
        public virtual string LineType { get; set; }
        #endregion

        #region CuryInfoID
        public abstract class curyInfoID : IBqlField { }
        [PXDBLong]
        [CurrencyInfo(typeof(CROpportunity.curyInfoID))]
        public virtual Int64? CuryInfoID { get; set; }
		#endregion
		
		#region InventoryID
		public abstract class inventoryID : IBqlField { }

        [Inventory(Filterable = true)]
        [PXForeignReference(typeof(Field<inventoryID>.IsRelatedTo<InventoryItem.inventoryID>))]
        public virtual Int32? InventoryID { get; set; }
		#endregion

		#region ExpenseAccountGroupID
		public abstract class expenseAccountGroupID : PX.Data.IBqlField
		{
		}

		[PXDBInt]
		public virtual Int32? ExpenseAccountGroupID
		{
			get;
			set;
		}
		#endregion

		#region RevenueAccountGroupID
		public abstract class revenueAccountGroupID : PX.Data.IBqlField
		{
		}

		[PXDBInt]
		public virtual Int32? RevenueAccountGroupID
		{
			get;
			set;
		}
		#endregion

		#region EmployeeID

		public abstract class employeeID : IBqlField { }

		[PXDBInt]
		[PXUIField(DisplayName = "Employee ID")]
		[EP.PXEPEmployeeSelectorAttribute]
		public virtual Int32? EmployeeID { get; set; }

		#endregion

		#region UOM
		public abstract class uOM : IBqlField { }

	    [PXDefault(typeof(Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
	    [INUnit(typeof(CROpportunityProducts.inventoryID))]
	    public virtual String UOM { get; set; }
	    #endregion

        #region Quantity
        public abstract class quantity : IBqlField { }

        protected Decimal? _Quantity;
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXDBQuantity]
        [PXUIField(DisplayName = "Quantity", Visibility = PXUIVisibility.Visible)]
        public virtual Decimal? Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }
        #endregion

        #region CuryUnitPrice
        public abstract class curyUnitPrice : IBqlField { }

        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXDBCurrency(typeof(CommonSetup.decPlPrcCst), typeof(CROpportunityProducts.curyInfoID), typeof(CROpportunityProducts.unitPrice))]
        [PXUIField(DisplayName = "Unit Price", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual Decimal? CuryUnitPrice { get; set; }
        #endregion

        #region UnitPrice
        public abstract class unitPrice : IBqlField { }

        [PXDBPriceCost()]
        [PXDefault(TypeCode.Decimal, "0.0")]        
        public virtual Decimal? UnitPrice { get; set; }
		#endregion

		#region CuryUnitCost
		public abstract class curyUnitCost : IBqlField { }

		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXDBCurrency(typeof(CommonSetup.decPlPrcCst), typeof(CROpportunityProducts.curyInfoID), typeof(CROpportunityProducts.unitCost))]
		[PXUIField(DisplayName = "Unit Cost")]
		public virtual Decimal? CuryUnitCost { get; set; }
		#endregion

		#region CalculateDiscountsOnImport
		public abstract class calculateDiscountsOnImport : PX.Data.IBqlField
		{
		}
		protected Boolean? _CalculateDiscountsOnImport;
		[PXBool()]
		[PXUIField(DisplayName = "Calculate automatic discounts on import")]
		public virtual Boolean? CalculateDiscountsOnImport
		{
			get
			{
				return this._CalculateDiscountsOnImport;
			}
			set
			{
				this._CalculateDiscountsOnImport = value;
			}
		}
		#endregion

		#region UnitCost
		public abstract class unitCost : IBqlField { }

		[PXDBPriceCost()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? UnitCost { get; set; }
		#endregion

		#region ManualDisc
		public abstract class manualDisc : IBqlField { }

        [ManualDiscountMode(typeof(CROpportunityProducts.curyDiscAmt), typeof(CROpportunityProducts.discPct))]
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Manual Discount", Visibility = PXUIVisibility.Visible)]
        public virtual Boolean? ManualDisc { get; set; }
        #endregion

        #region DiscPct
        public abstract class discPct : IBqlField { }

        [PXDBDecimal(6, MinValue = 0, MaxValue = 100)]
        [PXUIField(DisplayName = "Discount, %")]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscPct { get; set; }
        #endregion

        #region CuryExtPrice
        public abstract class curyExtPrice : IBqlField { }

        [PXDBCurrency(typeof(CROpportunityProducts.curyInfoID), typeof(CROpportunityProducts.extPrice))]
        [PXUIField(DisplayName = "Ext. Price")]
        [PXFormula(typeof(Mult<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitPrice>))] 
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryExtPrice { get; set; }
        #endregion

        #region ExtPrice
        public abstract class extPrice : IBqlField { }

        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? ExtPrice { get; set; }
		#endregion

		#region CuryExtCost
		public abstract class curyExtCost : IBqlField { }

		[PXDBCurrency(typeof(CROpportunityProducts.curyInfoID), typeof(CROpportunityProducts.extCost))]
		[PXUIField(DisplayName = "Ext. Cost")]
		[PXFormula(typeof(Mult<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitCost>))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? CuryExtCost { get; set; }
		#endregion

		#region ExtPrice
		public abstract class extCost : IBqlField { }

		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual Decimal? ExtCost { get; set; }
		#endregion

		#region CuryDiscAmt
		public abstract class curyDiscAmt : IBqlField { }

        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXDBCurrency(typeof(CommonSetup.decPlPrcCst), typeof(CROpportunityProducts.curyInfoID), typeof(CROpportunityProducts.discAmt), MinValue = 0)]
        [PXFormula(typeof(Div<Mult<CROpportunityProducts.curyExtPrice, CROpportunityProducts.discPct>, decimal100>))]
        [PXUIField(DisplayName = "Discount Amount")]
        public virtual Decimal? CuryDiscAmt { get; set; }
        #endregion

        #region DiscAmt
        public abstract class discAmt : IBqlField { }

        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscAmt { get; set; }
        #endregion

        #region CuryAmount
        public abstract class curyAmount : IBqlField { }

        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXDBCurrency(typeof(CROpportunityProducts.curyInfoID), typeof(CROpportunityProducts.amount))]
        [PXUIField(DisplayName = "Amount", Enabled = false)]
        [PXFormula(typeof(Sub<CROpportunityProducts.curyExtPrice, CROpportunityProducts.curyDiscAmt>))]
        public virtual Decimal? CuryAmount { get; set; }
        #endregion

        #region Amount
        public abstract class amount : IBqlField { }

        [PXDBDecimal(4)]
        public virtual Decimal? Amount { get; set; }
        #endregion

        #region CustomerID
        public abstract class customerID : IBqlField { }

        [PXDBInt]
        [PXDBDefault(typeof(CROpportunity.bAccountID), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Int32? CustomerID { get; set; }
        #endregion

        #region Descr
        public abstract class descr : IBqlField { }

        [PXDBLocalizableString(Common.Constants.TranDescLength, IsUnicode = true)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible)]
        public virtual String Descr { get; set; }
        #endregion

        #region SubItemID
        public abstract class subItemID : IBqlField { }

        [SubItem(typeof(CROpportunityProducts.inventoryID))]
        public virtual Int32? SubItemID { get; set; }
        #endregion       

        #region TaxCategoryID
        public abstract class taxCategoryID : PX.Data.IBqlField
        {
        }
        protected String _TaxCategoryID;
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
        [PXSelector(typeof(TaxCategory.taxCategoryID), DescriptionField = typeof(TaxCategory.descr))]
		[PXFormula(typeof(Default<CROpportunityProducts.inventoryID>))]
        [PXDefault(typeof(Search<InventoryItem.taxCategoryID,
                Where<InventoryItem.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]        
        public virtual String TaxCategoryID
        {
            get
            {
                return this._TaxCategoryID;
            }
            set
            {
                this._TaxCategoryID = value;
            }
        }
        #endregion

        #region ProjectID
        public abstract class projectID : IBqlField { }
        [PXDBInt]
        [PXDefault(typeof(CROpportunity.projectID), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Int32? ProjectID { get; set; }
        #endregion

        #region TaskID
        public abstract class taskID : IBqlField { }
		[PXDefault(typeof(Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<projectID>>, And<PMTask.isDefault, Equal<True>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[ActiveProjectTask(typeof(CROpportunityProducts.projectID), BatchModule.CR, DisplayName = "Project Task")]
        public virtual Int32? TaskID { get; set; }
		#endregion

		#region TaskCD
		public abstract class taskCD : PX.Data.IBqlField
		{
		}

		[PXDBString()]
		[PXUIField(DisplayName = "Project Task")]
		[PXDimension(ProjectTaskAttribute.DimensionName)]
		public virtual string TaskCD
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
		[CostCode(null, typeof(taskID))]
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

		#region IsFree
		public abstract class isFree : IBqlField { }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Free Item")]
        public virtual Boolean? IsFree { get; set; }
        #endregion

        #region ManualPrice
        public abstract class manualPrice : PX.Data.IBqlField
        {
        }
        protected Boolean? _ManualPrice;
        [PXDBBool()]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Manual Price")]
        public virtual Boolean? ManualPrice
        {
            get
            {
                return this._ManualPrice;
            }
            set
            {
                this._ManualPrice = value;
            }
        }
        #endregion

        #region SiteID
        public abstract class siteID : IBqlField { }
	    [PXFormula(typeof(Default<CROpportunityProducts.inventoryID>))]
		[POSiteAvail(typeof(CROpportunityProducts.inventoryID), typeof(CROpportunityProducts.subItemID))]
        [PXDefault(typeof(
                Coalesce<
                    Search<Location.cSiteID, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>,
                    Search<InventoryItem.dfltSiteID, Where<InventoryItem.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]
		[PXForeignReference(typeof(Field<siteID>.IsRelatedTo<INSite.siteID>))]
        public virtual Int32? SiteID { get; set; }
        #endregion

        #region TextForProductsGrid
        public abstract class textForProductsGrid : PX.Data.IBqlField { }
        [PXUIField(DisplayName = "  ", Enabled = false)]
        [PXString()]
        public virtual String TextForProductsGrid
        {
            set; get;            
        }
        #endregion

        #region POCreate
        public abstract class pOCreate : PX.Data.IBqlField
        {
        }
        [PXDBBool()]
        [PXDefault(false,
            typeof(Search<INItemSiteSettings.pOCreate,
                            Where<INItemSiteSettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>,
                                 And<INItemSiteSettings.siteID, Equal<Current<CROpportunityProducts.siteID>>>>>))]
        [PXUIField(DisplayName = "Mark for PO")]
        public virtual Boolean? POCreate
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
        [AP.Vendor(typeof(Search<BAccountR.bAccountID,
            Where<AP.Vendor.type, NotEqual<BAccountType.employeeType>>>))]
        [PXRestrictor(typeof(Where<AP.Vendor.status, IsNull,
                                Or<AP.Vendor.status, Equal<BAccount.status.active>,
                                Or<AP.Vendor.status, Equal<BAccount.status.oneTime>>>>), AP.Messages.VendorIsInStatus, typeof(AP.Vendor.status))]
        [PXDefault(typeof(Search<INItemSiteSettings.preferredVendorID,
            Where<INItemSiteSettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<CROpportunityProducts.siteID>>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<CROpportunityProducts.siteID>))]
        [PXUIField(DisplayName = "Vendor ID")]
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

        #region NoteID
        public abstract class noteID : IBqlField { }

        [PXNote]
        public virtual Guid? NoteID { get; set; }
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

		#region DiscountsAppliedToLine
		public abstract class discountsAppliedToLine : PX.Data.IBqlField
		{
		}
		protected ushort[] _DiscountsAppliedToLine;
		[PXDBPackedIntegerArray()]
		public virtual ushort[] DiscountsAppliedToLine
		{
			get
			{
				return this._DiscountsAppliedToLine;
			}
			set
			{
				this._DiscountsAppliedToLine = value;
			}
		}
		#endregion
		#region GroupDiscountRate
		public abstract class groupDiscountRate : PX.Data.IBqlField
        {
        }
        protected Decimal? _GroupDiscountRate;
        [PXDBDecimal(18)]
        [PXDefault(TypeCode.Decimal, "1.0")]
        public virtual Decimal? GroupDiscountRate
        {
            get
            {
                return this._GroupDiscountRate;
            }
            set
            {
                this._GroupDiscountRate = value;
            }
        }
        #endregion
        #region DocumentDiscountRate
        public abstract class documentDiscountRate : PX.Data.IBqlField
        {
        }
        protected Decimal? _DocumentDiscountRate;
        [PXDBDecimal(18)]
        [PXDefault(TypeCode.Decimal, "1.0")]
        public virtual Decimal? DocumentDiscountRate
        {
            get
            {
                return this._DocumentDiscountRate;
            }
            set
            {
                this._DocumentDiscountRate = value;
            }
        }
        #endregion
        #region DiscountID
        public abstract class discountID : PX.Data.IBqlField
        {
        }
        protected String _DiscountID;
        [PXDBString(10, IsUnicode = true)]
        [PXSelector(typeof(Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
        [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
        public virtual String DiscountID
        {
            get
            {
                return this._DiscountID;
            }
            set
            {
                this._DiscountID = value;
            }
        }
        #endregion
        #region DiscountSequenceID
        public abstract class discountSequenceID : PX.Data.IBqlField
        {
        }
        protected String _DiscountSequenceID;
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
        public virtual String DiscountSequenceID
        {
            get
            {
                return this._DiscountSequenceID;
            }
            set
            {
                this._DiscountSequenceID = value;
            }
        }
        #endregion

        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }
        protected Guid? _CreatedByID;
        [PXDBCreatedByID()]
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
        [PXDBCreatedDateTime()]
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
        [PXDBLastModifiedByID()]
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

        [PXDBLastModifiedDateTime()]
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

        #region IHasMinGrossProfit Members
        public decimal? Qty => Quantity;
        #endregion

        #region StockItemType
        public abstract class stockItemType : PX.Data.IBqlField
        {
        }
        protected String _StockItemType;
        [PXString(1, IsFixed = true)]
        [PXFormula(typeof(Selector<CROpportunityProducts.inventoryID, InventoryItem.itemType>))]
        public virtual String StockItemType
        {
            get
            {
                return this._StockItemType;
            }
            set
            {
                this._StockItemType = value;
            }
        }
        #endregion

    }
}
