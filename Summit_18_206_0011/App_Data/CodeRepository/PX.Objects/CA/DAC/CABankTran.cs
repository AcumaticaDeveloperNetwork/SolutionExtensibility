﻿using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Web.UI;
using PX.Objects.Common.Attributes;

namespace PX.Objects.CA
{
    /// <summary>
    /// Contains the main properties of CA bank transactions and their classes.
    /// CA bank transactions are edited on the Process Bank Transactions (CA306000) form (which corresponds to the <see cref="CABankTransactionsMaint"/> graph).
    /// Also CA bank transactions are edited on the Process Incoming Payments (AR305000) form (which corresponds to the <see cref="CABankIncomingPaymentsMaint"/> graph).
    /// </summary>
    [System.SerializableAttribute]
    [PXCacheName(Messages.BankTransaction)]
	public partial class CABankTran : PX.Data.IBqlTable, ICADocSource
	{
		#region CashAccountID
		public abstract class cashAccountID : IBqlField { }

        /// <summary>
        /// The cash account specified on the bank statement for which you want to upload bank transactions.
        /// This field is a part of the compound key of the document.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CashAccount.CashAccountID"/> field.
        /// </value>
	    [PXDBInt(IsKey = true)]
	    [PXDefault(typeof(CABankTranHeader.cashAccountID))]
	    public virtual int? CashAccountID
	    {
	        get;
            set;
	    }
		#endregion
		#region TranID
		public abstract class tranID : IBqlField
		{
		}

        /// <summary>
        /// The unique identifier of the CA bank transaction.
        /// This field is the key field.
        /// </summary>
	    [PXDBIdentity(IsKey = true)]
	    public virtual int? TranID
	    {
	        get;
            set;
	    }
		#endregion
        #region TranType
        public abstract class tranType : IBqlField
        {
        }

        /// <summary>
        /// The type of the bank tansaction.
        ///  The field is linked to the <see cref="CABankTranHeader.TranType"/> field.
        /// </summary>
        /// <value>
        /// The field can have one of the following values:
        /// <c>"S"</c>: Bank Statement Import,
        /// <c>"I"</c>: Payments Import
        /// </value>
	    [PXDBString(1, IsFixed = true)]
	    [PXDefault(typeof(CABankTranHeader.tranType))]
	    [CABankTranType.List]
	    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
	    public virtual string TranType
	    {
	        get;
            set;
	    }
        #endregion
		#region HeaderRefNbr
		public abstract class headerRefNbr : IBqlField
		{
		}

        /// <summary>
        /// The reference number of the imported bank statement (<see cref="CABankTranHeader">CABankTranHeader</see>),
        /// which the system generates automatically in accordance with the numbering sequence assigned to statements on the Cash Management Preferences (CA101000) form.
        /// </summary>
	    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
	    [PXDBDefault(typeof(CABankTranHeader.refNbr))]
	    [PXUIField(DisplayName = "Statement Nbr.")]
	    [PXParent(typeof(Select<CABankTranHeader, Where<CABankTranHeader.refNbr, Equal<Current<CABankTran.headerRefNbr>>,
	                            And<CABankTranHeader.tranType, Equal<Current<CABankTran.tranType>>>>>))]
	    public virtual string HeaderRefNbr
	    {
	        get;
            set;
	    }
		#endregion
		#region ExtTranID
		public abstract class extTranID : IBqlField
		{
		}

        /// <summary>
        /// The external identifier of the transaction.
        /// </summary>
	    [PXDBString(255, IsUnicode = true)]
	    [PXUIField(DisplayName = "Ext. Tran. ID", Visible = false)]
	    public virtual string ExtTranID
	    {
	        get;
            set;
	    }
		#endregion
		#region DrCr
		public abstract class drCr : IBqlField
		{
		}

        /// <summary>
        /// The balance type of the bank transaction.
        /// </summary>
        /// <value>
        /// The field can have one of the following values:
        /// <c>"D"</c>: Receipt,
        /// <c>"C"</c>: Disbursement
        /// </value>
	    [PXDBString(1, IsFixed = true)]
	    [PXDefault(CADrCr.CACredit)]
	    [CADrCr.List]
	    [PXUIField(DisplayName = "DrCr")]
	    public virtual string DrCr
	    {
	        get;
            set;
	    }
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of currency of the bank transaction.
        /// </summary>
	    [PXDBString(5, IsUnicode = true)]
	    [PXDefault]
	    [PXSelector(typeof(Currency.curyID), CacheGlobal = true)]
	    [PXUIField(DisplayName = "Currency")]
	    public virtual string CuryID
	    {
	        get;
            set;
	    }
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of the exchange rate record for the bank transaction amount.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CurrencyInfo.CuryInfoID"/> field.
        /// </value>
        [PXDBLong]
	    [CurrencyInfoConditional(typeof(CABankTran.createDocument), ModuleCode = GL.BatchModule.CA)]
	    public virtual long? CuryInfoID
	    {
	        get;
            set;
	    }
		#endregion
		#region TranDate
		public abstract class tranDate : IBqlField
		{
		}

        /// <summary>
        /// The transaction date.
        /// </summary>
	    [PXDBDate]
	    [PXDefault]
	    [PXUIField(DisplayName = "Tran. Date")]
	    public virtual DateTime? TranDate
	    {
	        get;
            set;
	    }

		#endregion
		#region TranEntryDate
		public abstract class tranEntryDate : IBqlField
		{
		}

		/// <summary>
		/// The bank transaction entry date.
		/// </summary>
		[PXDBDate]
		[PXUIField(DisplayName = "Tran. Entry Date", Visible = false)]
		public virtual DateTime? TranEntryDate
		{
			get;
			set;
		}
		#endregion
		#region CuryTranAmt
		public abstract class curyTranAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount of the bank transaction in the selected currency.
        /// </summary>
	    [PXDBCury(typeof(CABankTran.curyID))]
	    [PXDefault(TypeCode.Decimal, "0.0")]
	    [PXUIField(DisplayName = "CuryTranAmt")]
	    public virtual decimal? CuryTranAmt
	    {
	        get;
            set;
	    }
		#endregion	
		#region OrigCuryID
		public abstract class origCuryID : IBqlField
		{
		}

        /// <summary>
        /// The currency of the matching document.
        /// </summary>
	    [PXDBString(5, IsUnicode = true)]
	    [PXSelector(typeof(Currency.curyID), CacheGlobal = true)]
	    [PXUIField(DisplayName = "Orig. Currency", Visible = false)]
	    public virtual string OrigCuryID
	    {
	        get;
            set;
	    }
        #endregion
        
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField
		{
		}

        /// <summary>
        /// The external reference number of the transaction.
        /// </summary>
	    [PXDBString(40, IsUnicode = true)]
	    [PXUIField(DisplayName = "Ext. Ref. Nbr.")]
	    public virtual string ExtRefNbr
	    {
	        get;
            set;
	    }
		#endregion
		#region TranDesc
		public abstract class tranDesc : IBqlField
		{
		}

        /// <summary>
        /// The description of the bank transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Tran. Desc")]
	    public virtual string TranDesc
	    {
	        get;
            set;
	    }
		#endregion
        #region UserDesc
        public abstract class userDesc : IBqlField
        {
        }

        /// <summary>
        /// The description of the bank transaction.
        /// You can use this field to specify a user description of the bank transaction while keeping the original bank description (<see cref="TranDesc"/>) untouched.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Tran. Desc", Enabled = true)]
	    public virtual string UserDesc
	    {
	        get;
            set;
	    }
        #endregion
		#region PayeeName
		public abstract class payeeName : IBqlField
		{
		}

        /// <summary>
        /// The payee name, if any, specified for a transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Payee Name", Visible = false)]
	    public virtual string PayeeName
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeeAddress1
		public abstract class payeeAddress1 : IBqlField
		{
		}

        /// <summary>
        /// The payee address, if any, specified for a transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Payee Address1", Visible = false)]
	    public virtual string PayeeAddress1
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeeCity
		public abstract class payeeCity : IBqlField
		{
		}

        /// <summary>
        /// The payee city, if any, specified for a transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Payee City", Visible = false)]
	    public virtual string PayeeCity
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeeState
		public abstract class payeeState : IBqlField
		{
		}

        /// <summary>
        /// The payee state, if any, specified for a transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Payee State", Visible = false)]
	    public virtual string PayeeState
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeePostalCode
		public abstract class payeePostalCode : IBqlField
		{
		}

        /// <summary>
        /// The payee postal code, if any, specified for a transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Payee Postal Code", Visible = false)]
	    public virtual string PayeePostalCode
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeePhone
		public abstract class payeePhone : IBqlField
		{
		}

        /// <summary>
        /// The payee phone, if any, specified for a transaction.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Payee Phone", Visible = false)]
	    public virtual string PayeePhone
	    {
	        get;
            set;
	    }
		#endregion
		#region TranCode
		public abstract class tranCode : IBqlField
		{
		}

        /// <summary>
        /// The external code from the bank.
        /// </summary>
	    [PXDBString(35, IsUnicode = true)]
	    [PXUIField(DisplayName = "Tran. Code", Visible = false)]
	    public virtual string TranCode
	    {
	        get;
            set;
	    }
		#endregion
		#region OrigModule
		public abstract class origModule : IBqlField
		{
		}

        /// <summary>
        /// The original module of the matching document.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        /// <value>
        /// The field can have one of the following values:
        /// <c>"AP"</c>: Accounts Payable,
        /// <c>"AR"</c>: Accounts Receivable,
        /// <c>"CA"</c>: Cash Management.
        /// </value>
        [PXDBString(2, IsFixed = true)]
	    [PXStringList(new string[] {GL.BatchModule.AP, GL.BatchModule.AR, GL.BatchModule.CA},
	        new string[] {GL.Messages.ModuleAP, GL.Messages.ModuleAR, GL.Messages.ModuleCA})]
	    [PXUIField(DisplayName = "Module", Enabled = false)]
	    [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
	    public virtual string OrigModule
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeeBAccountID
		public abstract class payeeBAccountID : IBqlField
		{
		}

        /// <summary>
        /// The vendor or customer associated with the document, by its business account ID.
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXVendorCustomerSelector(typeof(CABankTran.origModule))]
	    [PXUIField(DisplayName = "Business Account", Visible = false)]
	    public virtual int? PayeeBAccountID
	    {
	        get;
            set;
	    }
		#endregion
		#region PayeeLocationID
		public abstract class payeeLocationID : IBqlField
		{
		}

        /// <summary>
        /// The location of the vendor or customer. 
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
	    [LocationID(typeof(Where<Location.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>),
	        DisplayName = "Location", DescriptionField = typeof(Location.descr), Visible = false)]
	    [PXDefault(typeof(Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>),
	        PersistingCheck = PXPersistingCheck.Nothing)]
	    public virtual int? PayeeLocationID
	    {
	        get;
            set;
	    }
		#endregion
		#region PaymentMethodID
		public abstract class paymentMethodID : IBqlField
		{
		}

        /// <summary>
        /// The payment method used by a customer or vendor for the document. 
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
	    [PXDBString(10, IsUnicode = true)]
	    [PXDefault(typeof(Coalesce<Coalesce<Search2<Customer.defPaymentMethodID, InnerJoin<PaymentMethod,
	                    On<PaymentMethod.paymentMethodID, Equal<Customer.defPaymentMethodID>, And<PaymentMethod.useForAR, Equal<True>>>,
	                    InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<Customer.defPaymentMethodID>,
	                            And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>,
	                Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>, And<Customer.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>,
	            Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>,
	                    And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
	                        And<PaymentMethodAccount.useForAR, Equal<True>>>>>,
	                Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>,
	                        And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aRIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>,
	        Coalesce<Search2<Location.vPaymentMethodID, InnerJoin<Vendor, On<Location.bAccountID, Equal<Vendor.bAccountID>,
	                                And<Location.locationID, Equal<Vendor.defLocationID>>>,
	                        InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<Location.vPaymentMethodID>,
	                            And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>,
	                Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>,
	                    And<Vendor.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>,
	            Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>,
	                    And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
	                        And<PaymentMethodAccount.useForAP, Equal<True>>>>>,
	                Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>,
	                    And<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.isActive, Equal<boolTrue>>>>,
	                OrderBy<Asc<PaymentMethodAccount.aPIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>>),
	        PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXSelector(typeof(Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID,
	            Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
	                And<Where2<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>,
	                And<PaymentMethodAccount.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>,
	                And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>>, Where<PaymentMethod.isActive, Equal<boolTrue>,
	            And<Where2<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>,
	            And<PaymentMethod.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>,
	            And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof(PaymentMethod.descr))]
	    [PXUIField(DisplayName = "Payment Method", Visible = false)]
	    public virtual string PaymentMethodID
	    {
	        get;
            set;
	    }
		#endregion
		#region PMInstanceID
		public abstract class pMInstanceID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of the credit card or account that is used by a customer or vendor for the document.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXDBInt]
        [PXUIField(DisplayName = "Card/Account No", Visible = false)]
	    [PXDefault(typeof(Coalesce<Search2<Customer.defPMInstanceID, InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>,
	                        And<CustomerPaymentMethod.bAccountID, Equal<Customer.bAccountID>>>>, Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>,
	                        And<Customer.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, And<CustomerPaymentMethod.isActive, Equal<True>,
	                        And<CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>>>>>>,
	        Search<CustomerPaymentMethod.pMInstanceID, Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>,
	                And<CustomerPaymentMethod.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>,
	                    And<CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>,
	                        And<CustomerPaymentMethod.isActive, Equal<True>>>>>,
	            OrderBy<Desc<CustomerPaymentMethod.expirationDate,
	                Desc<CustomerPaymentMethod.pMInstanceID>>>>>),
	        PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXSelector(typeof(Search<CustomerPaymentMethod.pMInstanceID,
	        Where<CustomerPaymentMethod.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>,
	            And<CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>,
	                And<CustomerPaymentMethod.isActive, Equal<boolTrue>>>>>),
	        DescriptionField = typeof(CustomerPaymentMethod.descr))]
		[DeprecatedProcessing]
	    public virtual int? PMInstanceID
	    {
	        get;
            set;
	    }
		#endregion
		#region InvoiceInfo
		public abstract class invoiceInfo : IBqlField
		{
		}

        /// <summary>
        /// The reference number of the document (invoice or bill) generated to match a payment. 
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
	    [PXDBString(256, IsUnicode = true)]
	    [PXUIField(DisplayName = "Invoice Nbr.", Visible = false)]
	    public virtual string InvoiceInfo
	    {
	        get;
            set;
	    }
        #endregion
		#region DocumentMatched
		public abstract class documentMatched : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the payment and ready to be processed. 
        /// That is, the bank transaction has been matched to an existing transaction in the system, or details of a new document that matches this transaction have been specified.
        /// </summary>
        [PXDBBool]
        [PXHeaderImage(Sprite.AliasControl + "@" + Sprite.Control.CompleteHead)]
	    [PXDefault(false)]
	    [PXUIField(DisplayName = "Matched", Visible = true, Enabled = false)]
	    public virtual bool? DocumentMatched
	    {
	        get;
            set;
	    }
		#endregion
		#region Validated
		public abstract class validated : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that all required details of a new document (the details that are required for the document to match the transaction) were specified.
        /// This is a virtual field and it has no representation in the database.
        /// The preliminary state of the <see cref="DocumentMatched"/> flag.
        /// </summary>
        [PXBool]
	    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXUIField(DisplayName = "Validated", Visible = false, Visibility = PXUIVisibility.Invisible, Enabled = false)]
	    public virtual bool? Validated
	    {
	        get;
            set;
	    }
		#endregion
		#region RuleApplied
		public abstract class ruleApplied : IBqlField
		{
		}
     
		/// <summary>
        /// Specifies (if set to <c>true</c>) that the rule was applied to clear the transaction on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Rule Applied", Visible = false, Visibility = PXUIVisibility.Invisible, Enabled = false)]
		public virtual bool? RuleApplied
		{
			[PXDependsOnFields(typeof(ruleID), typeof(createDocument))]
			get
			{
				return this.CreateDocument == true && this.RuleID != null;
			}
		}
		#endregion	
		#region ApplyRuleEnabled
		public abstract class applyRuleEnabled : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the button <c>Create Rule</c> is enabled.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
        [PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Create Rule Enabled", Visible = false, Visibility = PXUIVisibility.Invisible, Enabled = false)]
		public virtual bool? ApplyRuleEnabled
		{
			[PXDependsOnFields(typeof(ruleID), typeof(createDocument))]
			get
			{
				return this.CreateDocument == true && this.RuleID == null;
			}
		}
		#endregion
		#region MatchedToExisting
		public abstract class matchedToExisting : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the transaction in the system.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
	    [PXBool]
	    [PXUIField(DisplayName = "Matched", Visible = true, Enabled = false)]
	    public virtual bool? MatchedToExisting
	    {
	        get;
            set;
	    }
		#endregion
		#region MatchedToInvoice
		public abstract class matchedToInvoice : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the invoice.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
	    [PXBool]
	    [PXUIField(DisplayName = "Matched to Invoice", Visible = false, Enabled = false)]
	    public virtual bool? MatchedToInvoice
	    {
	        get;
            set;
	    }
		#endregion
		#region CreateDocument
		public abstract class createDocument : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that a new payment will be created for the selected bank transactions. 
        /// </summary>
	    [PXDBBool]
	    [PXDefault(false)]
	    [PXUIField(DisplayName = "Create")]
	    public virtual bool? CreateDocument
	    {
	        get;
            set;
	    }
		#endregion
		#region Status
		public abstract class status : IBqlField
		{
		}

        /// <summary>
        /// The status of the bank transaction.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
        /// <value>
        /// The field can have one of the following values:
        /// <c>"M"</c>: The bank transaction is matched to the payment and ready to be processed.
        /// <c>"I"</c>: The bank transaction is matched to the invoice.
        /// <c>"C"</c>: The bank transactions will be matched to a new payment.
        /// <c>"H"</c>: The bank transaction is hidden from the statement on the Process Bank Transactions (CA306000) form.
        /// <c>string.Empty</c>: The <see cref="DocumentMatched"/>, <see cref="MatchedToInvoice"/>, <see cref="CreateDocument"/>, and <see cref="Hidden"/> flags are set to <c>false</c>.
        /// </value>
        [PXString(1, IsFixed = true)]
		[CABankTranStatus.List]
		[PXUIField(DisplayName = "Match Type", Visibility = PXUIVisibility.SelectorVisible, Visible = false, Enabled = false)]
		public virtual string Status
		{
			[PXDependsOnFields(typeof(hidden), typeof(createDocument), typeof(matchedToInvoice), typeof(documentMatched))]
			get
			{
				if (this.Hidden == true)
				{
					return CABankTranStatus.Hidden;
				}
				if (this.CreateDocument == true)
				{
					return CABankTranStatus.Created;
				}
				if (this.MatchedToInvoice == true)
				{
					return CABankTranStatus.InvoiceMatched;
				}
				if (this.DocumentMatched == true)
				{
					return CABankTranStatus.Matched;
				}
				return string.Empty;
			}
		}
		#endregion
		#region Processed
		public abstract class processed : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that this bank transaction is processed.
        /// </summary>
	    [PXDBBool]
	    [PXDefault(false)]
	    [PXUIField(DisplayName = "Processed")]
	    public virtual bool? Processed
	    {
	        get;
            set;
	    }
		#endregion
		#region EntryTypeID
		public abstract class entryTypeID : IBqlField
		{
		}

        /// <summary>
        /// The identifier of an entry type that is used as a template for a new cash transaction to be created to match the selected bank transaction. 
        /// The field is displayed if the <c>CA</c> option is selected in the <see cref="OrigModule"/> field.
        /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CAEntryType.EntryTypeId"/> field.
        /// </value>
	    [PXDBString(10, IsUnicode = true)]
	    [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXSelector(typeof(Search2<CAEntryType.entryTypeId,
	        InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>,
	        Where<CashAccountETDetail.accountID, Equal<Current<CABankTran.cashAccountID>>,
	            And<CAEntryType.module, Equal<GL.BatchModule.moduleCA>,
	                And<Where<CAEntryType.drCr, Equal<Current<CABankTran.drCr>>>>>>>),
	        DescriptionField = typeof(CAEntryType.descr))]
	    [PXUIField(DisplayName = "Entry Type ID", Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
	    public virtual string EntryTypeID
	    {
	        get;
            set;
	    }
		#endregion
		#region CuryDebitAmt
		public abstract class curyDebitAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount of the receipt in the selected currency.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
		[PXCury(typeof(CABankTran.curyID))]
		[PXUIField(DisplayName = "Receipt")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXFormula(null, typeof(SumCalc<CABankTranHeader.curyDebitsTotal>))]
		public virtual decimal? CuryDebitAmt
		{
			[PXDependsOnFields(typeof(drCr), typeof(curyTranAmt))]
			get
			{
				return (this.DrCr == CADrCr.CADebit) ? this.CuryTranAmt : decimal.Zero;
			}

			set
			{
				if (value != 0m)
				{
					this.CuryTranAmt = value;
					this.DrCr = CADrCr.CADebit;
				}
				else if (this.DrCr == CADrCr.CADebit)
				{
					this.CuryTranAmt = 0m;
				}
			}
		}
		#endregion
		#region CuryCreditAmt
		public abstract class curyCreditAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount of the disbursement in the selected currency.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
		[PXCury(typeof(CABankTran.curyID))]
		[PXUIField(DisplayName = "Disbursement")]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXFormula(null, typeof(SumCalc<CABankTranHeader.curyCreditsTotal>))]
		public virtual decimal? CuryCreditAmt
		{
			[PXDependsOnFields(typeof(drCr), typeof(curyTranAmt))]
			get
			{
				return (this.DrCr == CADrCr.CACredit) ? -this.CuryTranAmt : decimal.Zero;
			}

			set
			{
				if (value != 0m)
				{
					this.CuryTranAmt = -value;
					this.DrCr = CADrCr.CACredit;
				}
				else if (this.DrCr == CADrCr.CACredit)
				{
					this.CuryTranAmt = 0m;
				}
			}
		}
        #endregion
		#region CuryTotalAmt
		public abstract class curyTotalAmt : IBqlField
		{
		}

        /// <summary>
        /// The total amount of the created document in the selected currency.
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
        [PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Amount")]
		[PXCury(typeof(CABankTran.curyID))]
		public virtual decimal? CuryTotalAmt
		{
			get
			{
                return (this.DrCr == CADrCr.CACredit) ? (-1 * this.CuryTranAmt) : this.CuryTranAmt;
			}

			set
			{
			}
		}
		#endregion
        #region CuryApplAmtCA
        public abstract class curyApplAmtCA : IBqlField
        {
        }

        /// <summary>
        /// The amount of the transaction for which the documents (to match the bank transaction) are added.
        /// Represented in the selected currency.
        /// This field is displayed if the <c>CA</c> option is selected in the <see cref="OrigModule"/> field.
        /// </summary>
	    [PXDBCury(typeof(CABankTran.curyID))]
	    [PXUIField(DisplayName = "Amount Used", Visibility = PXUIVisibility.Visible, Enabled = false)]
	    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
	    public virtual decimal? CuryApplAmtCA
	    {
	        get;
            set;
	    }
        #endregion
        #region CuryUnappliedBalCA
        public abstract class curyUnappliedBalCA : IBqlField
        {
        }

        /// <summary>
        /// The balance of the transaction for which you can add the documents. 
        /// This field is displayed if the <c>CA</c> option is selected in the <see cref="OrigModule"/> field.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
        [PXCury(typeof(CABankTran.curyID))]
        [PXUIField(DisplayName = "Balance Left", Visibility = PXUIVisibility.Visible, Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual decimal? CuryUnappliedBalCA
        {
            [PXDependsOnFields(typeof(curyTotalAmt), typeof(curyApplAmtCA))]
            get
            {
                return (this.CuryTotalAmt ?? decimal.Zero) - (this.CuryApplAmtCA ?? decimal.Zero);
            }

            set
            {
            }
        }
        #endregion
		#region CuryApplAmt
		public abstract class curyApplAmt : IBqlField
		{
		}

        /// <summary>
        /// The amount of the application for this payment. 
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// </summary>
	    [PXDBCury(typeof(CABankTran.curyID))]
	    [PXUIField(DisplayName = "Application Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
	    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
	    public virtual decimal? CuryApplAmt
	    {
	        get;
            set;
	    }

		#endregion
		#region CuryUnappliedBal
		public abstract class curyUnappliedBal : IBqlField
		{
		}

        /// <summary>
        /// The unapplied balance of the document in the selected currency.
        /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
		[PXCury(typeof(CABankTran.curyID))]
		[PXUIField(DisplayName = "Unapplied Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryUnappliedBal
		{
			[PXDependsOnFields(typeof(curyTotalAmt), typeof(curyApplAmt))]
			get
			{
				return (this.CuryTotalAmt ?? decimal.Zero) - (this.CuryApplAmt ?? decimal.Zero);
			}

			set
			{
			}
		}
		#endregion
		#region DocType
		public abstract class docType : IBqlField
		{
		}

        [PXString(3, IsFixed = true)]
		[PXDefault]
		[APPaymentType.List]
		[PXFieldDescription]
		public string DocType
		{
			get
			{
				if (this.OrigModule == GL.BatchModule.AP)
				{
					if (this.DrCr == CADrCr.CACredit)
					{
                        return APDocType.Check;
					}
                    return APDocType.Refund;
				}
				if (this.DrCr == CADrCr.CACredit)
				{
                    return ARDocType.Refund;
				}
                return ARDocType.Payment;
			}

			set
			{
			}
		}
		#endregion
		#region LineCntr
		public abstract class lineCntr : IBqlField
		{
		}

        /// <summary>
        /// The counter of related adjustments.
        /// The <c>PXParentAttribute</c> from the <see cref="CABankTranAdjustment.AdjNbr"/> field links on this field.
        /// </summary>
	    [PXDBInt]
	    [PXDefault(0)]
	    public virtual int? LineCntr
	    {
	        get;
            set;
	    }
		#endregion
        #region LineCntrCA
        public abstract class lineCntrCA : IBqlField
        {
        }

        /// <summary>
        /// The counter of related details.
        /// The <c>PXParentAttribute</c> from the <see cref="CABankTranDetail.LineNbr"/> field links on this field.
        /// </summary>
	    [PXDBInt]
	    [PXDefault(0)]
	    public virtual int? LineCntrCA
	    {
	        get;
            set;
	    }
        #endregion
		#region PayeeBAccountIDCopy
		public abstract class payeeBAccountIDCopy : IBqlField
		{
		}

        /// <summary>
        /// The copy of the <see cref="PayeeBAccountID"/> field.
        /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
		[PXInt]
		[PXSelector(typeof(Search<BAccountR.bAccountID>), SubstituteKey = typeof(BAccountR.acctCD), DescriptionField = typeof(BAccountR.acctName))]
		[PXUIField(DisplayName = "Business Account")]
		public virtual int? PayeeBAccountIDCopy
		{
			get
			{
				return this.PayeeBAccountID;
			}

			set
			{
				this.PayeeBAccountID = value;
			}
		}
		#endregion
		#region PayeeLocationIDCopy
		public abstract class payeeLocationIDCopy : IBqlField
		{
		}

        /// <summary>
        /// The copy of the <see cref="PayeeLocationID"/> field .
        /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
		[PXInt]
		[PXUIField(DisplayName = "Location", Visibility = PXUIVisibility.Visible, FieldClass = "LOCATION")]
		[LocationIDBase(typeof(Where<Location.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>), DisplayName = "Location", DescriptionField = typeof(Location.descr))]
		public virtual int? PayeeLocationIDCopy
		{
			get
			{
				return this.PayeeLocationID;
			}

			set
			{
				this.PayeeLocationID = value;
			}
		}
		#endregion
		#region PaymentMethodIDCopy
		public abstract class paymentMethodIDCopy : IBqlField
		{
		}

        /// <summary>
        /// The copy of the <see cref="PaymentMethodID"/> field.
        /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
		[PXString(10, IsUnicode = true)]
		[PXSelector(typeof(Search2<PaymentMethod.paymentMethodID,
				InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID,
				Equal<PaymentMethod.paymentMethodID>,
				And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
					And<Where2<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>>>,
						Or<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>>,
				Where<PaymentMethod.isActive, Equal<boolTrue>,
					And<Where2<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>>>,
						Or<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof(PaymentMethod.descr))]
		[PXUIField(DisplayName = "Payment Method", Visible = true)]
		public virtual string PaymentMethodIDCopy
		{
			get
			{
				return this.PaymentMethodID;
			}

			set
			{
				this.PaymentMethodID = value;
			}
		}
		#endregion
		#region PMInstanceIDCopy
		public abstract class pMInstanceIDCopy : IBqlField
		{
		}

        /// <summary>
        /// The copy of the <see cref="PMInstanceID"/> field.
        /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
		[PXInt]
		[PXUIField(DisplayName = "Card/Account No")]
		[PXSelector(typeof(Search<CustomerPaymentMethod.pMInstanceID,
									Where<CustomerPaymentMethod.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>,
									  And<CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>,
									  And<CustomerPaymentMethod.isActive, Equal<boolTrue>>>>>),
									  DescriptionField = typeof(CustomerPaymentMethod.descr))]
		public virtual int? PMInstanceIDCopy
		{
			get
			{
				return this.PMInstanceID;
			}

			set
			{
				this.PMInstanceID = value;
			}
		}
		#endregion
        #region RuleID
        public abstract class ruleID : IBqlField { }

        /// <summary>
        /// The identifier of the rule that was applied to the bank transaction to create a document.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="CABankTranRule.RuleID"/> field.
        /// </value>
        [PXDBInt]
	    [PXSelector(typeof(CABankTranRule.ruleID), SubstituteKey = typeof(CABankTranRule.ruleCD))]
	    [PXUIField(DisplayName = "Applied Rule", Enabled = false)]
	    public int? RuleID
	    {
	        get;
            set;
	    }
        #endregion
		#region Hidden
		public abstract class hidden : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that this bank transaction has been hidden from the statement on the Process Bank Transactions (CA306000) form.
        /// </summary>
	    [PXDBBool]
	    [PXDefault(false)]
	    [PXUIField(DisplayName = "Hidden", Enabled = false)]
	    public virtual bool? Hidden
	    {
	        get;
            set;
	    }        
        #endregion
		#region InvoiceNotFound
		public abstract class invoiceNotFound : IBqlField
		{
		}

        /// <summary>
        /// Specifies (if set to <c>true</c>) that the invoice for matching to this bank transaction wasn't found.
        /// </summary>
	    [PXDBBool]
	    public bool? InvoiceNotFound
	    {
            get;
            set;
	    }
		#endregion
        #region CountMatches
        public abstract class countMatches : IBqlField
        {
        }

        /// <summary>
        /// The count of matched payments.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
	    [PXInt]
	    [PXUIField(Visible = false, Enabled = false)]
	    public virtual int? CountMatches
	    {
            get;
            set;
	    }
        #endregion
        #region CountInvoiceMatches
        public abstract class countInvoiceMatches : IBqlField
        {
        }

        /// <summary>
        /// The count of matched invoices.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
	    [PXInt]
	    [PXUIField(Visible = false, Enabled = false)]
	    public virtual int? CountInvoiceMatches
	    {
	        get;
            set;
	    }
        #endregion
        #region MatchStatsInfo
        public abstract class matchStatsInfo : IBqlField
        {
        }

        /// <summary>
        /// The user-friendly brief description of the status of the selected transaction.
        /// The field is displayed in the bottom of the table with bank transactions on the Process Bank Transactions (CA306000) form.
        /// This is a virtual field and it has no representation in the database.
        /// </summary>
	    [PXString]
	    [PXUIField(DisplayName = "MatchStatsInfo", Enabled = false, Visibility = PXUIVisibility.Invisible, Visible = false)]
	    public virtual string MatchStatsInfo
	    {
	        get;
            set;
	    }
        #endregion
        #region AcctName
        public abstract class acctName : IBqlField
        {
        }

        /// <summary>
        /// The name of the vendor or customer associated with the document.
        /// </summary>
        [PXInt]
        [PXSelector(typeof(Search<BAccountR.bAccountID>), SubstituteKey = typeof(BAccountR.acctName))]
        [PXUIField(DisplayName = CR.Messages.BAccountName, Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
        public virtual int? AcctName
        {
            get
            {
                return this.PayeeBAccountID;
            }

            set
            {
                this.PayeeBAccountID = value;
            }
        }
        #endregion
        #region PayeeBAccountID1
        public abstract class payeeBAccountID1 : IBqlField
        {
        }

        /// <summary>
        /// The copy of the <see cref="PayeeBAccountID"/> field.
        /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search<BAccountR.bAccountID>), SubstituteKey = typeof(BAccountR.acctCD), DescriptionField = typeof(BAccountR.acctName))]
        [PXUIField(DisplayName = "Business Account", Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
        public virtual int? PayeeBAccountID1
        {
            get
            {
                return this.PayeeBAccountID;
            }

            set
            {
                this.PayeeBAccountID = value;
            }
        }
        #endregion
        #region PayeeLocationID1
        public abstract class payeeLocationID1 : IBqlField
        {
        }

        /// <summary>
        /// The copy of the <see cref="PayeeLocationID"/> field.
        /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXInt]
        [PXSelector(typeof(Search<Location.locationID, Where<Location.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>), SubstituteKey = typeof(Location.locationCD), DescriptionField = typeof(Location.descr))]
        [PXUIField(DisplayName = "Location", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
        [PXDefault(typeof(Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? PayeeLocationID1
        {
            get
            {
                return this.PayeeLocationID;
            }

            set
            {
                this.PayeeLocationID = value;
            }
        }
        #endregion
        #region PaymentMethodID1
        public abstract class paymentMethodID1 : IBqlField
        {
        }

        /// <summary>
        /// The copy of the <see cref="PaymentMethodID"/> field.
        /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXString(10, IsUnicode = true)]
        [PXDefault(typeof(Coalesce<
                            Coalesce<
                                Search2<Customer.defPaymentMethodID,
                                    InnerJoin<PaymentMethod,
                                        On<PaymentMethod.paymentMethodID, Equal<Customer.defPaymentMethodID>,
                                        And<PaymentMethod.useForAR, Equal<True>>>,
                                    InnerJoin<PaymentMethodAccount,
                                        On<PaymentMethodAccount.paymentMethodID, Equal<Customer.defPaymentMethodID>,
                                        And<PaymentMethodAccount.useForAR, Equal<True>,
                                        And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>,
                                    Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>,
                                        And<Customer.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>,
                                Search2<PaymentMethodAccount.paymentMethodID,
                                    InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>,
                                        And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
                                        And<PaymentMethodAccount.useForAR, Equal<True>>>>>,
                                    Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>,
                                        And<PaymentMethod.useForAR, Equal<True>,
                                        And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aRIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>,
                            Coalesce<
                                Search2<Location.vPaymentMethodID,
                                    InnerJoin<Vendor, On<Location.bAccountID, Equal<Vendor.bAccountID>, And<Location.locationID, Equal<Vendor.defLocationID>>>,
                                    InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<Location.vPaymentMethodID>,
                                        And<PaymentMethodAccount.useForAP, Equal<True>,
                                        And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>,
                                    Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>,
                                        And<Vendor.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>,
                                Search2<PaymentMethodAccount.paymentMethodID,
                                    InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>,
                                        And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
                                        And<PaymentMethodAccount.useForAP, Equal<True>>>>>,
                                    Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>,
                                        And<PaymentMethod.useForAP, Equal<True>,
                                        And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aPIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>>),
                    PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search2<PaymentMethod.paymentMethodID,
                InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID,
                Equal<PaymentMethod.paymentMethodID>,
                And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>,
                    And<Where2<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>>>,
                        Or<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>>,
                Where<PaymentMethod.isActive, Equal<boolTrue>,
                    And<Where2<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>>>,
                        Or<Where<Current<CABankTran.origModule>, Equal<GL.BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof(PaymentMethod.descr))]
        [PXUIField(DisplayName = "Payment Method", Visible = false)]
        public virtual string PaymentMethodID1
        {
            get
            {
                return this.PaymentMethodID;
            }

            set
            {
                this.PaymentMethodID = value;
            }
        }
        #endregion
        #region InvoiceInfo1
        public abstract class invoiceInfo1 : IBqlField
        {
        }

        /// <summary>
        /// The copy of the <see cref="InvoiceInfo"/> field.
        /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Invoice Nbr.", Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
        public virtual string InvoiceInfo1
        {
            get
            {
                return this.InvoiceInfo;
            }

            set
            {
                this.InvoiceInfo = value;
            }
        }
        #endregion
        #region EntryTypeID1
        public abstract class entryTypeID1 : IBqlField
        {
        }

        /// <summary>
        /// The copy of the <see cref="EntryTypeID"/> field.
        /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXString(10, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search2<CAEntryType.entryTypeId,
                              InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>,
                              Where<CashAccountETDetail.accountID, Equal<Current<CABankTran.cashAccountID>>,
                                And<CAEntryType.module, Equal<GL.BatchModule.moduleCA>,
                                And<Where<CAEntryType.drCr, Equal<Current<CABankTran.drCr>>>>>>>),
                      DescriptionField = typeof(CAEntryType.descr))]
        [PXUIField(DisplayName = "Entry Type ID", Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
        public virtual string EntryTypeID1
        {
            get
            {
                return this.EntryTypeID;
            }

            set
            {
                this.EntryTypeID = value;
            }
        }
        #endregion
        #region OrigModule1
        public abstract class origModule1 : IBqlField
        {
        }

        /// <summary>
        /// The copy of the <see cref="OrigModule"/> field.
        /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
        /// </summary>
        [PXString(2, IsFixed = true)]
        [PXStringList(new string[] { GL.BatchModule.AP, GL.BatchModule.AR, GL.BatchModule.CA, }, new string[] { GL.Messages.ModuleAP, GL.Messages.ModuleAR, GL.Messages.ModuleCA })]
        [PXUIField(DisplayName = "Module", Enabled = false, Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual string OrigModule1
        {
            get
            {
                return this.OrigModule;
            }

            set
            {
                this.OrigModule = value;
            }
        }
        #endregion
		#region CuryWOAmt
		public abstract class curyWOAmt : IBqlField
		{
		}

        /// <summary>
        /// The total amount of write-offs specified for documents to be applied in the selected currency. 
        /// This field is displayed if the <c>"AR"</c> option is selected in the <see cref="OrigModule"/> field.
        /// </summary>
		[PXCurrency(typeof(CABankTran.curyInfoID), typeof(CABankTran.wOAmt))]
		[PXUIField(DisplayName = "Write-Off Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual decimal? CuryWOAmt
		{
			get;
			set;
		}
		#endregion
		#region WOAmt
		public abstract class wOAmt : IBqlField
		{
		}

        /// <summary>
        /// The total amount of write-offs specified for documents to be applied in the base currency. 
        /// </summary>
	    [PXDecimal(4)]
	    public virtual decimal? WOAmt
	    {
	        get;
            set;
	    }
        #endregion

        #region Audit fields
        #region NoteID
        public abstract class noteID : IBqlField
        {
        }

        [PXNote]
        public virtual Guid? NoteID
        {
            get;
            set;
        }
        #endregion
        #region CreatedByID
        public abstract class createdByID : IBqlField
        {
        }

        [PXDBCreatedByID]
        public virtual Guid? CreatedByID
        {
            get;
            set;
        }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID
        {
            get;
            set;
        }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : IBqlField
        {
        }

        [PXDBCreatedDateTime]
        public virtual DateTime? CreatedDateTime
        {
            get;
            set;
        }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : IBqlField
        {
        }

        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID
        {
            get;
            set;
        }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID
        {
            get;
            set;
        }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime
        {
            get;
            set;
        }
        #endregion
        #region tstamp
        public abstract class Tstamp : IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp
        {
            get;
            set;
        }
        #endregion
        #endregion

        #region ICADocSource Members

        public int? BAccountID
        {
            get
            {
                return this.PayeeBAccountID;
            }

            set
            {
                this.PayeeBAccountID = value;
            }
        }

        public int? LocationID
        {
            get
            {
                return this.PayeeLocationID;
            }

            set
            {
                this.PayeeLocationID = value;
            }
        }

        #region ClearDate
        public abstract class cleared : IBqlField
        {
        }
        [PXBool]
        public bool? Cleared
        {
            get
            {
                return true;
            }
        }
        #endregion
        #region ClearDate
        public abstract class clearDate : IBqlField
        {
        }
        [PXDate]
        public virtual DateTime? ClearDate
        {
            get
            {
                return TranDate;
            }
        }
        #endregion

        public int? CARefTranAccountID
        {
            get
            {
                return null;
            }
        }

        public long? CARefTranID
        {
            get
            {
                return null;
            }
        }

        public int? CARefSplitLineNbr
        {
            get
            {
                return null;
            }
        }

        public decimal? CuryOrigDocAmt
        {
            [PXDependsOnFields(typeof(curyTranAmt))]
            get
            {
                return this.CuryTranAmt.HasValue ? (this.CuryTranAmt.Value != decimal.Zero ? this.CuryTranAmt * Math.Sign(this.CuryTranAmt.Value) : decimal.Zero) : null; //Document sign is inverted compared to the CATran's
            }
        }

        long? ICADocSource.CuryInfoID
        {
            get
            {
                return null;
            }
        }

        string ICADocSource.FinPeriodID
        {
            get
            {
                return null;
            }
        }

        string ICADocSource.InvoiceNbr
        {
            get
            {
                return InvoiceInfo;
            }
        }

        string ICADocSource.TranDesc
        {
            get
            {
                return UserDesc;
            }
        }
        #endregion
    }

    public class CABankTranType
	{
		public class ListAttribute : PXStringListAttribute
		{
		    public ListAttribute()
		        : base(
		            new string[] {Statement, PaymentImport},
		            new string[] {Messages.Statement, Messages.PaymentImport}) { }
		}

		public const string Statement = "S";
        public const string PaymentImport = "I";
		
		public class statement : Constant<string>
		{
			public statement() : base(Statement) { }
		}

		public class paymentImport : Constant<string>
		{
			public paymentImport() : base(PaymentImport) { }
		}
	}

	public class CABankTranStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
			new string[] { Matched, InvoiceMatched, Created, Hidden },
			new string[] { Messages.Matched, Messages.InvoiceMatched, Messages.Created, Messages.Hidden }) { }
		}

		public class ImagesListAttribute : PXImagesListAttribute
		{
		    private static string getCustomSprite(string sprite)
		    {
		        return $"{Sprite.AliasMain}@{sprite}";
		    }

            public ImagesListAttribute() : base(new string[] { Matched, InvoiceMatched, Created, Hidden },
		        new string[] { Messages.Matched, Messages.InvoiceMatched, Messages.Created, Messages.Hidden },
		        new string[] { getCustomSprite(Sprite.Main.Link), getCustomSprite(Sprite.Main.LinkWB), getCustomSprite(Sprite.Main.RecordAdd), getCustomSprite(Sprite.Main.Preview) }) { }
        }

        public static bool IsMatchedToInvoice(CABankTran tran, CABankTranMatch match)
		{
			return !(match != null && (match.CATranID != null || (match.DocType == CATranType.CABatch && match.DocModule == GL.BatchModule.AP)));
		}

		public const string Matched = "M";
		public const string InvoiceMatched = "I";
		public const string Created = "C";
		public const string Hidden = "H";

		public class hold : Constant<string>
		{
			public hold() : base(Matched) { }
		}

		public class balanced : Constant<string>
		{
			public balanced() : base(InvoiceMatched) { }
		}

		public class unposted : Constant<string>
		{
			public unposted() : base(Created) { }
		}

		public class posted : Constant<string>
		{
			public posted() : base(Hidden) { }
		}
	}
}
