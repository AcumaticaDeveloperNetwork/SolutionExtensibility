using System;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CS;

namespace PX.Objects.CM.Extensions
{
	public interface IPXCurrency
	{
		string CuryID { get; set; }
		string Description { get; set; }
	}
	/// <summary>
	/// Stores financial settings associated with currencies, thus complementing the <see cref="CurrencyList"/> DAC type.
	/// While <see cref="CurrencyList"/> holds only general information, such as code and precision, the <see cref="Currency"/> DAC provides information
	/// on all accounts and subaccounts associated with a particular currency, such as the Realized Gain and Loss account and subaccount.
	/// The <see cref="Currency"/> DAC also exposes fields with general currency information (such as <see cref="Description"/>),
	/// which are mapped to the corresponding fields in the <see cref="CurrencyList"/> DAC by means of <see cref="PXDBScalarAttribute"/>.
	/// The records of this type (as well as the <see cref="CurrencyList"/> records) are edited on the Currencies (CM202000) form (which corresponds to the <see cref="CurrencyMaint"/> graph).
	/// </summary>
	[PXPrimaryGraph(
		new Type[] { typeof(CurrencyMaint)},
		new Type[] { typeof(Select<Currency, 
			Where<Currency.curyID, Equal<Current<Currency.curyID>>>>)
		})]
	[System.SerializableAttribute()]
	[PXCacheName(CM.Messages.Currency)]
	public partial class Currency : PX.Data.IBqlTable, IPXCurrency
	{
		#region CuryID
		public abstract class curyID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Key field.
        /// Unique identifier of the currency.
        /// </summary>
		[PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
		[PXDBDefault(typeof(CurrencyList.curyID))]
		[PXUIField(DisplayName = "Currency ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<CurrencyList.curyID, Where<CurrencyList.curyID, NotEqual<Current<Company.baseCuryID>>>>), CacheGlobal = true)]
		[PXParent(typeof(Select<CurrencyList, Where<CurrencyList.curyID, Equal<Current<curyID>>>>))]
		[PX.Data.EP.PXFieldDescription]
		public virtual String CuryID
		{
			get;
			set;
		}
		#endregion
		#region RealGainAcctID
		public abstract class realGainAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Realized Gain <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null,
			DisplayName = "Realized Gain Account", 
			Visibility = PXUIVisibility.Visible, 
			DescriptionField = typeof(Account.description))]
		public virtual Int32? RealGainAcctID
		{
			get;
			set;
		}
		#endregion
		#region RealGainSubID
		public abstract class realGainSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Realized Gain <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.realGainAcctID), 
			DescriptionField = typeof(Sub.description), 
			DisplayName = "Realized Gain Subaccount", 
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? RealGainSubID
		{
			get;
			set;
		}
		#endregion
		#region RealLossAcctID
		public abstract class realLossAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Realized Loss <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Realized Loss Account",
		   Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? RealLossAcctID
		{
			get;
			set;
		}
		#endregion
		#region RealLossSubID
		public abstract class realLossSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Realized Loss <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.realLossAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Realized Loss Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? RealLossSubID
		{
			get;
			set;
		}
		#endregion
		#region RevalGainAcctID
		public abstract class revalGainAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Revaluation Gain <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Revaluation Gain Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? RevalGainAcctID
		{
			get;
			set;
		}
		#endregion
		#region RevalGainSubID
		public abstract class revalGainSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Revaluation Gain <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.revalGainAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Revaluation Gain Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? RevalGainSubID
		{
			get;
			set;
		}
		#endregion
		#region RevalLossAcctID
		public abstract class revalLossAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Revaluation Loss <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Revaluation Loss Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? RevalLossAcctID
		{
			get;
			set;
		}
		#endregion
		#region RevalLossSubID
		public abstract class revalLossSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Revaluation Loss <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.revalLossAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Revaluation Loss Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? RevalLossSubID
		{
			get;
			set;
		}
		#endregion
		#region ARProvAcctID
		public abstract class aRProvAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Accounts Receivable Provisioning <see cref="Account"/> associated with the currency.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[Account(null,
			DisplayName = "AR Provisioning Account",
			DescriptionField = typeof(Account.description))]
		public virtual Int32? ARProvAcctID
		{
			get;
			set;
		}
		#endregion
		#region ARProvSubID
		public abstract class aRProvSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Accounts Receivable Provisioning <see cref="Sub">Subaccount</see> associated with the currency.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[SubAccount(typeof(Currency.aRProvAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "AR Provisioning Subaccount")]
		public virtual Int32? ARProvSubID
		{
			get;
			set;
		}
		#endregion
		#region APProvAcctID
		public abstract class aPProvAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Accounts Payable Provisioning <see cref="Account"/> associated with the currency.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[Account(null,
			DisplayName = "AP Provisioning Account",
			DescriptionField = typeof(Account.description))]
		public virtual Int32? APProvAcctID
		{
			get;
			set;
		}
		#endregion
		#region APProvSubID
		public abstract class aPProvSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Accounts Payable Provisioning <see cref="Sub">Subaccount</see> associated with the currency.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[SubAccount(typeof(Currency.aPProvAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "AP Provisioning Subaccount")]
		public virtual Int32? APProvSubID
		{
			get;
			set;
		}
		#endregion
		#region TranslationGainAcctID
		public abstract class translationGainAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Translation Gain <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[PXUIRequired(typeof(FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
		[Account(null, 
			DisplayName = "Translation Gain Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? TranslationGainAcctID
		{
			get;
			set;
		}
		#endregion
		#region TranslationGainSubID
		public abstract class translationGainSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Translation Gain <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[PXUIRequired(typeof(FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
		[SubAccount(typeof(Currency.translationGainAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Translation Gain Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? TranslationGainSubID
		{
			get;
			set;
		}
		#endregion
		#region TranslationLossAcctID
		public abstract class translationLossAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Translation Loss <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[PXUIRequired(typeof(FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
		[Account(null, 
			DisplayName = "Translation Loss Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? TranslationLossAcctID
		{
			get;
			set;
		}
		#endregion
		#region TranslationLossSubID
		public abstract class translationLossSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Translation Loss <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[PXUIRequired(typeof(FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
		[SubAccount(typeof(Currency.translationLossAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Translation Loss Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? TranslationLossSubID
		{
			get;
			set;
		}
		#endregion
		#region UnrealizedGainAcctID
		public abstract class unrealizedGainAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Unrealized Gain <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Unrealized Gain Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? UnrealizedGainAcctID
		{
			get;
			set;
		}
		#endregion
		#region UnrealizedGainSubID
		public abstract class unrealizedGainSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Unrealized Gain <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.unrealizedGainAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Unrealized Gain Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? UnrealizedGainSubID
		{
			get;
			set;
		}
		#endregion
		#region UnrealizedLossAcctID
		public abstract class unrealizedLossAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Unrealized Loss <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Unrealized Loss Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? UnrealizedLossAcctID
		{
			get;
			set;
		}
		#endregion
		#region UnrealizedLossSubID
		public abstract class unrealizedLossSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Unrealized Loss <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.unrealizedLossAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Unrealized Loss Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? UnrealizedLossSubID
		{
			get;
			set;
		}
		#endregion
		#region RoundingGainAcctID
		public abstract class roundingGainAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Rounding Gain <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Rounding Gain Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? RoundingGainAcctID
		{
			get;
			set;
		}
		#endregion
		#region RoundingGainSubID
		public abstract class roundingGainSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Rounding Gain <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.roundingGainAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Rounding Gain Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? RoundingGainSubID
		{
			get;
			set;
		}
		#endregion
		#region RoundingLossAcctID
		public abstract class roundingLossAcctID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Rounding Loss <see cref="Account"/> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Account.AccountID"/> field.
        /// </value>
		[PXDefault]
		[Account(null, 
			DisplayName = "Rounding Loss Account",
			Visibility = PXUIVisibility.Visible,
			DescriptionField = typeof(Account.description))]
		public virtual Int32? RoundingLossAcctID
		{
			get;
			set;
		}
		#endregion
		#region RoundingLossSubID
		public abstract class roundingLossSubID : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// Identifier of the Rounding Loss <see cref="Sub">Subaccount</see> associated with the currency.
        /// Required field.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Sub.SubID"/> field.
        /// </value>
		[PXDefault]
		[SubAccount(typeof(Currency.roundingLossAcctID),
			DescriptionField = typeof(Sub.description),
			DisplayName = "Rounding Loss Subaccount",
			Visibility = PXUIVisibility.Visible)]
		public virtual Int32? RoundingLossSubID
		{
			get;
			set;
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// The user-defined description of the currency.
        /// </summary>
		[PXString(IsUnicode = true)]
		[PXDBScalar(typeof(Search<CurrencyList.description, Where<CurrencyList.curyID, Equal<curyID>>>))]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Description
		{
			get;
			set;
		}
		#endregion
		#region CurySymbol
		public abstract class curySymbol : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// The symbol of the currency.
        /// </summary>
		[PXString(IsUnicode = true)]
		[PXDBScalar(typeof(Search<CurrencyList.curySymbol, Where<CurrencyList.curyID, Equal<curyID>>>))]
		[PXUIField(DisplayName = "Currency Symbol")]
		public virtual String CurySymbol
		{
			get;
			set;
		}
		#endregion
		#region CuryCaption
		public abstract class curyCaption : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// The caption (the name) of the currency.
        /// </summary>
		[PXString(IsUnicode = true)]
		[PXDBScalar(typeof(Search<CurrencyList.curyCaption, Where<CurrencyList.curyID, Equal<curyID>>>))]
		[PXUIField(DisplayName = "Currency Caption")]
		public virtual String CuryCaption
		{
			get;
			set;
		}
		#endregion
		#region DecimalPlaces
		public abstract class decimalPlaces : PX.Data.IBqlField
		{
		}
        /// <summary>
        /// The number of digits after the decimal point used in operations with the currency.
        /// </summary>
        /// <value>
        /// Minimum allowed value is 0, maximum - 4.
        /// </value>
		[PXShort(MinValue = 0, MaxValue = 4)]
		[PXDBScalar(typeof(Search<CurrencyList.decimalPlaces, Where<CurrencyList.curyID, Equal<curyID>>>))]
		[PXUIField(DisplayName = "Decimal Precision")]
		public virtual short? DecimalPlaces
		{
			get;
			set;
		}
		#endregion
		#region NoteID
		public abstract class noteID : IBqlField { }
        /// <summary>
        /// Identifier of the <see cref="PX.Data.Note">Note</see> object, associated with the document.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="PX.Data.Note.NoteID">Note.NoteID</see> field. 
        /// </value>
		[PXNote(DescriptionField = typeof(Currency.curyID))]
		public virtual Guid? NoteID { get; set; }
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
	}
}
