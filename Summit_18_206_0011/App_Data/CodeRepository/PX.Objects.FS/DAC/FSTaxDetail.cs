using PX.Data;
using PX.Objects.CM;
using PX.Objects.TX;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXHidden]
	public class FSTaxDetail : TaxDetail, PX.Data.IBqlTable
	{
        #region EntityType
        public abstract class entityType : ListField_PostDoc_EntityType
        {
        }
        [PXDBString(2, IsKey = true, IsFixed = true)]
        [PXDefault]
        [entityType.ListAtrribute]
        [PXUIField(DisplayName = "EntityType", Visibility = PXUIVisibility.Visible, Visible = true)]
        public virtual String EntityType { get; set; }
        #endregion

		#region EntityID
		public abstract class entityID : PX.Data.IBqlField
		{
		}

        [PXDBInt(IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Entity ID", Visibility = PXUIVisibility.Visible, Visible = true)]
		public virtual int? EntityID { get; set; }
        #endregion
        #region LineNbr
        public abstract class lineNbr : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
        [PXDBDefault(typeof(FSSODetUNION.lineNbr))]
        [PXUIField(DisplayName = "LineNbr", Visibility = PXUIVisibility.Visible, Visible = false)]
        [PXParent(typeof(Select<FSSODetUNION,
                            Where<
                                FSSODetUNION.sOID, Equal<Current<FSServiceOrderTax.entityID>>,
                                And<FSSODetUNION.lineNbr, Equal<Current<FSServiceOrderTax.lineNbr>>>>>))]
        public virtual Int32? LineNbr { get; set; }
        #endregion

        #region TaxID
        public abstract class taxID : PX.Data.IBqlField
		{
		}
		[PXDBString(Tax.taxID.Length, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Tax ID")]
		[PXSelector(typeof(Tax.taxID), DescriptionField = typeof(Tax.descr))]
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
		[CurrencyInfo(typeof(FSServiceOrder.curyInfoID))]
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
		protected decimal? _CuryTaxableAmt;
		[PXDBCurrency(typeof(curyInfoID), typeof(taxableAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryTaxableAmt
		{
			get
			{
				return this._CuryTaxableAmt;
			}
			set
			{
				this._CuryTaxableAmt = value;
			}
		}
		#endregion
		#region TaxableAmt
		public abstract class taxableAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _TaxableAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? TaxableAmt
		{
			get
			{
				return this._TaxableAmt;
			}
			set
			{
				this._TaxableAmt = value;
			}
		}
		#endregion
		#region CuryTaxAmt
		public abstract class curyTaxAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _CuryTaxAmt;
		[PXDBCurrency(typeof(curyInfoID), typeof(taxAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? CuryTaxAmt
		{
			get
			{
				return this._CuryTaxAmt;
			}
			set
			{
				this._CuryTaxAmt = value;
			}
		}
		#endregion
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}
		protected Decimal? _TaxAmt;
		[PXDBDecimal(4)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
		public virtual Decimal? TaxAmt
		{
			get
			{
				return this._TaxAmt;
			}
			set
			{
				this._TaxAmt = value;
			}
		}
		#endregion

		#region CuryExpenseAmt
		public abstract class curyExpenseAmt : PX.Data.IBqlField
		{
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
	}
}
