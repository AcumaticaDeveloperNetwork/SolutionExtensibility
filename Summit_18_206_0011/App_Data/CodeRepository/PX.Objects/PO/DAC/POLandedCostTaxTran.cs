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
using PX.Objects.SO;
using PX.Objects.TX;

namespace PX.Objects.PO
{
    [Serializable]
	[PXCacheName(Messages.POLandedCostTaxTran)]
	public class POLandedCostTaxTran : TaxDetail, PX.Data.IBqlTable
	{
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
		}

		[PXDBString(1, IsFixed = true, IsKey = true)]
		[PXDBDefault(typeof(POLandedCostDoc.docType))]
		[PXUIField(DisplayName = "Document Type", Enabled = false, Visible = false)]
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

		[PXDBString(15, IsUnicode = true, InputMask = "", IsKey = true)]
		[PXDBDefault(typeof(POLandedCostDoc.refNbr))]
		[PXUIField(DisplayName = "Document Nbr.", Enabled = false, Visible = false)]
		[PXParent(typeof(Select<POLandedCostDoc, Where<POLandedCostDoc.docType, Equal<Current<docType>>, And<POLandedCostDoc.refNbr, Equal<Current<refNbr>>>>>))]

		public virtual String RefNbr
		{
			get;
			set;
		}
		#endregion
		#region TaxID
		public abstract class taxID : PX.Data.IBqlField
		{
		}
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Tax ID", Visibility = PXUIVisibility.Visible)]
		[PXSelector(typeof(Tax.taxID), DescriptionField = typeof(Tax.descr), DirtyRead = true)]
		public override String TaxID
		{
			get
			{
				return this._TaxID;
			}
			set
			{
				this._TaxID = value;
			}
		}
		#endregion
		#region RecordID
		public abstract class recordID : PX.Data.IBqlField
		{
		}
		protected Int32? _RecordID;
		[PXDBIdentity(IsKey = true)]
		public virtual Int32? RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				this._RecordID = value;
			}
		}
		#endregion
		#region JurisType
		public abstract class jurisType : PX.Data.IBqlField
		{
		}
		protected String _JurisType;
		[PXDBString(9, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Jurisdiction Type")]
		public virtual String JurisType
		{
			get
			{
				return this._JurisType;
			}
			set
			{
				this._JurisType = value;
			}
		}
		#endregion
		#region JurisName
		public abstract class jurisName : PX.Data.IBqlField
		{
		}
		protected String _JurisName;
		[PXDBString(200, IsUnicode = true)]
		[PXUIField(DisplayName = "Tax Jurisdiction Name")]
		public virtual String JurisName
		{
			get
			{
				return this._JurisName;
			}
			set
			{
				this._JurisName = value;
			}
		}
		#endregion
		#region TaxRate
		public abstract class taxRate : PX.Data.IBqlField
		{
		}
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : PX.Data.IBqlField
		{
		}
		[PXDBLong()]
		[CurrencyInfo(typeof(POLandedCostDoc.curyInfoID))]
		public override Int64? CuryInfoID
		{
			get
			{
				return this._CuryInfoID;
			}
			set
			{
				this._CuryInfoID = value;
			}
		}
		#endregion
		#region CuryTaxableAmt
		public abstract class curyTaxableAmt : PX.Data.IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(taxableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		[PXUnboundFormula(typeof(Switch<Case<WhereExempt<taxID>, curyTaxableAmt>, decimal0>), typeof(SumCalc<POLandedCostDoc.curyVatExemptTotal>))]
		[PXUnboundFormula(typeof(Switch<Case<WhereTaxable<taxID>, curyTaxableAmt>, decimal0>), typeof(SumCalc<POLandedCostDoc.curyVatTaxableTotal>))]
		public virtual Decimal? CuryTaxableAmt
		{
			get;
			set;
		}
		#endregion		
		#region TaxableAmt
		public abstract class taxableAmt : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? TaxableAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryTaxAmt
		public abstract class curyTaxAmt : PX.Data.IBqlField
		{
		}

		[PXDBCurrency(typeof(curyInfoID), typeof(taxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryTaxAmt
		{
			get;
			set;
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? TaxAmt
		{
			get;
			set;
		}
		#endregion
		#region CuryExpenseAmt
		public abstract class curyExpenseAmt : PX.Data.IBqlField
		{
		}
		[PXDBCurrency(typeof(curyInfoID), typeof(expenseAmt))]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Expense Amount", Visibility = PXUIVisibility.Visible)]
		public override Decimal? CuryExpenseAmt
		{
			get; set;
		}
		#endregion
		#region ExpenseAmt
		public abstract class expenseAmt : PX.Data.IBqlField
		{
		}
		#endregion
		#region TaxZoneID
		public abstract class taxZoneID : IBqlField { }

		[PXDBString(10, IsUnicode = true)]
		public virtual string TaxZoneID
		{
			get;
			set;
		}
		#endregion
	}
}
