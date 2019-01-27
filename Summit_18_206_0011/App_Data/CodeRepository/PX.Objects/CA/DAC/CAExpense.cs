﻿using System;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CM;

namespace PX.Objects.CA
{
	[Serializable]
	[PXCacheName("CAExpense")]
	public class CAExpense : AP.IPaymentCharge, IBqlTable
	{
		#region DocType
		public string DocType
		{
			get { return CATranType.CATransferExp; }
			set { }
		}
		#endregion
		#region RefNbr
		public abstract class refNbr : IBqlField { }

		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(CATransfer.transferNbr))]
		[PXParent(typeof(Select<CATransfer, Where<CATransfer.transferNbr, Equal<Current<refNbr>>>>))]
		public string RefNbr { get; set; }
		#endregion
		#region LineNbr
		public abstract class lineNbr : IBqlField { }

		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
		[PXLineNbr(typeof(CATransfer.expenseCntr))]
		public int? LineNbr { get; set; }
		#endregion
		#region CashAccountID
		public abstract class cashAccountID : IBqlField { }

		[CashAccount(DisplayName = "Cash Account", Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof(CashAccount.descr), Required = true)]
		[PXDefault]
		public int? CashAccountID { get; set; }
		#endregion
		#region BranchID
		public abstract class branchID : IBqlField { }

		[PXDBInt]
		[PXDefault(typeof(Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CAExpense.cashAccountID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<cashAccountID>))]
		public virtual Int32? BranchID { get; set; }
		#endregion
		#region AdjRefNbr
		[Obsolete("Will be removed in Acumatica 2019R2")]
		public abstract class adjRefNbr : IBqlField { }

		[Obsolete("Will be removed in Acumatica 2019R2")]
		[PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
		public string AdjRefNbr { get; set; }
		#endregion
		#region Hold
		[Obsolete("Will be removed in Acumatica 2019R2")]
		public abstract class hold : IBqlField { }

		[Obsolete("Will be removed in Acumatica 2019R2")]
		[PXBool]
		[PXUIField(Enabled = false, Visible = false)]
		[PXDBScalar(typeof(Search<CAAdj.hold, Where<CAAdj.adjRefNbr, Equal<adjRefNbr>>>))]
		public bool? Hold { get; set; }
		#endregion
		#region TranDate
		public abstract class tranDate : IBqlField { }

		[PXDBDate]
		[PXDefault(typeof(CATransfer.outDate))]
		[PXUIField(DisplayName = "Doc. Date")]
		public virtual DateTime? TranDate { get; set; }
		#endregion
		#region TranPeriodID
		public abstract class tranPeriodID : IBqlField { }

		[TranPeriodID(
			typeof(tranDate), 
			typeof(cashAccountID),
			typeof(Selector<cashAccountID, CashAccount.branchID>))]
		public virtual string TranPeriodID { get; set; }
		#endregion
		#region FinPeriodID
		public abstract class finPeriodID : IBqlField { }

		[CAOpenPeriod(typeof(tranDate), typeof(cashAccountID), typeof(Selector<cashAccountID, CashAccount.branchID>), ValidatePeriod = PeriodValidation.DefaultSelectUpdate)]
		[PXUIField(DisplayName = "Fin. Period")]
		public virtual string FinPeriodID { get; set; }
		#endregion
		#region CuryInfoID
		public abstract class curyInfoID : IBqlField { }

		[PXDBLong]
		[CurrencyInfo(ModuleCode = BatchModule.CA, CuryIDField = nameof(CuryID), CuryRateField = nameof(AdjCuryRate))]
		public long? CuryInfoID { get; set; }
		#endregion
		#region CuryID
		public abstract class curyID : IBqlField { }

		[PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
		[PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
		[PXSelector(typeof(Currency.curyID))]
		[PXDefault(typeof(Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<cashAccountID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual string CuryID { get; set; }
		#endregion
		#region AdjCuryRate
		public abstract class adjCuryRate : IBqlField { }

		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXDecimal(8)]
		[PXUIField(DisplayName = "Currency Rate", Visibility = PXUIVisibility.Visible)]
		public virtual decimal? AdjCuryRate { get; set; }
		#endregion
		#region EntryTypeID
		public abstract class entryTypeID : IBqlField { }

		[PXDBString(10, IsUnicode = true)]
		[PXDefault]
		[PXSelector(typeof(Search2<CAEntryType.entryTypeId,
			InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>,
			Where<CashAccountETDetail.accountID, Equal<Current<cashAccountID>>,
				And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>),
			DescriptionField = typeof(CAEntryType.descr), DirtyRead = false)]
		[PXUIField(DisplayName = "Entry Type")]
		public virtual string EntryTypeID { get; set; }
		#endregion
		#region AccountID
		public abstract class accountID : IBqlField { }

		[PXDefault(typeof(Coalesce<Search<CashAccountETDetail.offsetAccountID, Where<CashAccountETDetail.entryTypeID, Equal<Current<entryTypeID>>,
							And<CashAccountETDetail.accountID, Equal<Current<cashAccountID>>>>>,
						Search<CAEntryType.accountID, Where<CAEntryType.entryTypeId, Equal<Current<entryTypeID>>>>>))]
		[Account(typeof(branchID), typeof(Search2<Account.accountID, LeftJoin<CashAccount, On<CashAccount.accountID, Equal<Account.accountID>>,
													InnerJoin<CAEntryType, On<CAEntryType.entryTypeId, Equal<Current<entryTypeID>>>>>,
												Where2<Where<CAEntryType.useToReclassifyPayments, Equal<False>,
												And<Where<Account.curyID, IsNull, Or<Account.curyID, Equal<Current<curyID>>>>>>,
													Or<Where<CashAccount.cashAccountID, IsNotNull,
														And<CashAccount.curyID, Equal<Current<curyID>>,
														And<CashAccount.cashAccountID, NotEqual<Current<cashAccountID>>>>>>>>), DisplayName = "Offset Account", DescriptionField = typeof(Account.description))]
		[PXFormula(typeof(Default<cashAccountID, entryTypeID>))]
		public virtual int? AccountID { get; set; }
		#endregion
		#region SubID
		public abstract class subID : IBqlField { }

		[PXDefault]
		[PXFormula(typeof(Selector<entryTypeID, CAEntryType.subID>))]
		[SubAccount(typeof(accountID), DisplayName = "Offset Subaccount", Required = true)]
		[PXFormula(typeof(Default<cashAccountID, entryTypeID>))]
		public virtual int? SubID { get; set; }
		#endregion
		#region DrCr
		public abstract class drCr : IBqlField { }

		[PXDBString(1, IsFixed = true)]
		[PXDefault(typeof(Search<CAEntryType.drCr, Where<CAEntryType.entryTypeId, Equal<Current<entryTypeID>>>>))]
		[CADrCr.List]
		[PXUIField(DisplayName = "Disb./Receipt", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFormula(typeof(Default<entryTypeID>))]
		public string DrCr { get; set; }
		#endregion

		#region CashTranID
		public abstract class cashTranID : IBqlField { }

		[PXDBLong]
		[ExpenseCashTranID]
		public long? CashTranID { get; set; }
		#endregion
		#region CuryTranAmt
		public abstract class curyTranAmt : IBqlField { }

		[PXDBCurrency(typeof(curyInfoID), typeof(tranAmt))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Amount")]
		public decimal? CuryTranAmt { get; set; }
		#endregion
		#region TranAmt
		public abstract class tranAmt : IBqlField { }

		[PXDBBaseCury]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Base Currency Amount", Enabled = false)]
		public virtual decimal? TranAmt { get; set; }
		#endregion
		#region Released
		public abstract class released : IBqlField { }

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Released")]
		public bool? Released { get; set; }
		#endregion
		#region Cleared
		public abstract class cleared : IBqlField { }

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Cleared")]
		public virtual bool? Cleared { get; set; }
		#endregion
		#region ClearDate
		public abstract class clearDate : IBqlField { }

		[PXDBDate]
		[PXUIField(DisplayName = "Clear Date")]
		public virtual DateTime? ClearDate { get; set; }
		#endregion
		#region ExtRefNbr
		public abstract class extRefNbr : IBqlField { }

		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Document Ref.")]
		public virtual string ExtRefNbr { get; set; }
		#endregion
		#region TranDesc
		public abstract class tranDesc : IBqlField { }

		[PXDBString(Common.Constants.TranDescLength, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
		[PXDefault(typeof(Search<CAEntryType.descr, Where<CAEntryType.entryTypeId, Equal<Current<CAExpense.entryTypeID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
		[PXFormula(typeof(Default<entryTypeID>))]
		public virtual string TranDesc { get; set; }
		#endregion
		#region BatchNbr
		public abstract class batchNbr : IBqlField { }

		[PXString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.SelectorVisible, Visible = false, Enabled = false)]
		[PXDBScalar(typeof(Search<CATran.batchNbr, Where<CATran.tranID, Equal<cashTranID>>>))]
		public virtual string BatchNbr { get; set; }
		#endregion
		#region NoteID
		public abstract class noteID : IBqlField { }

		[PXNote]
		public virtual Guid? NoteID { get; set; }
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField { }

		[PXDBCreatedByID]
		public virtual Guid? CreatedByID { get; set; }
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField { }

		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID { get; set; }
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField { }

		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime { get; set; }
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField { }

		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID { get; set; }
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField { }

		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField { }

		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion
		#region tstamp
		public abstract class Tstamp : IBqlField { }

		[PXDBTimestamp]
		public virtual byte[] tstamp { get; set; }
		#endregion
	}
}