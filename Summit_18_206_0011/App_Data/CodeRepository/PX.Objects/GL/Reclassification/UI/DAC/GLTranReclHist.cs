using System;
using System.Collections.Generic;
using PX.Data;
using PX.Objects.CM;

namespace PX.Objects.GL.Reclassification.UI
{
    [PXBreakInheritance]
    public class GLTranReclHist : GLTran
    {
        #region field to reuse
        public abstract new class selected : IBqlField { }
        public abstract new class includedInReclassHistory:IBqlField { }
        public abstract new class branchID:IBqlField { }
        public abstract new class module:IBqlField { }
        public abstract new class batchNbr:IBqlField { }
        public abstract new class lineNbr:IBqlField { }
        public abstract new class ledgerID:IBqlField { }
        public abstract new class accountID:IBqlField { }
        public abstract new class subID:IBqlField { }
        public abstract new class projectID:IBqlField { }
        public abstract new class taskID:IBqlField { }
        public abstract new class costCodeID:IBqlField { }
        public abstract new class refNbr:IBqlField { }
        public abstract new class inventoryID:IBqlField { }
        public abstract new class uOM:IBqlField { }
        public abstract new class qty:IBqlField { }
        public abstract new class debitAmt:IBqlField { }
        public abstract new class creditAmt:IBqlField { }
        public abstract new class curyInfoID:IBqlField { }
        public abstract new class curyDebitAmt:IBqlField { }
        public abstract new class curyCreditAmt:IBqlField { }
        public abstract new class released:IBqlField { }
        public abstract new class posted:IBqlField { }
        public abstract new class nonBillable:IBqlField { }
        public abstract new class isInterCompany:IBqlField { }
        public abstract new class summPost:IBqlField { }
        public abstract new class zeroPost:IBqlField { }
        public abstract new class origModule:IBqlField { }
        public abstract new class origBatchNbr:IBqlField { }
        public abstract new class origLineNbr:IBqlField { }
        public abstract new class origAccountID:IBqlField { }
        public abstract new class origSubID:IBqlField { }
        public abstract new class tranID:IBqlField { }
        public abstract new class tranType:IBqlField { }
        public abstract new class tranClass:IBqlField { }
        public abstract new class tranDesc:IBqlField { }
        public abstract new class tranDate:IBqlField { }
        public abstract new class tranLineNbr:IBqlField { }
        public abstract new class referenceID:IBqlField { }
        public abstract new class finPeriodID:IBqlField { }
        public abstract new class tranPeriodID:IBqlField { }
        public abstract new class cATranID:IBqlField { }
        public abstract new class pMTranID:IBqlField { }
        public abstract new class origPMTranID:IBqlField { }
        public abstract new class ledgerBalanceType:IBqlField { }
        public abstract new class accountRequireUnits:IBqlField { }
        public abstract new class taxID:IBqlField { }
        public abstract new class taxCategoryID:IBqlField { }
        public abstract new class noteID:IBqlField { }
        public abstract new class reclassificationProhibited:IBqlField { }
        public abstract new class reclassBatchModule:IBqlField { }
        public abstract new class reclassBatchNbr:IBqlField { }
        public abstract new class isReclassReverse:IBqlField { }
        public abstract new class reclassType:IBqlField { }
        public abstract new class curyReclassRemainingAmt:IBqlField { }
        public abstract new class reclassRemainingAmt:IBqlField { }
        public abstract new class reclassified:IBqlField { }
        public abstract new class reclassSourceTranModule:IBqlField { }
        public abstract new class reclassSourceTranBatchNbr:IBqlField { }
        public abstract new class reclassSourceTranLineNbr:IBqlField { }
        public abstract new class reclassSeqNbr:IBqlField { }
        #endregion

        public GLTranReclHist() { }

        public GLTranReclHist(string module, string batchNbr, int? lineNbr)
        {
            Module = module;
            BatchNbr = batchNbr;
            LineNbr = lineNbr;
        }

        public GLTranReclHist(GLTran tran)
        {
            IncludedInReclassHistory = tran.IncludedInReclassHistory;
            BranchID = tran.BranchID;
            Module = tran.Module;
            BatchNbr = tran.BatchNbr;
            LineNbr = tran.LineNbr;
            LedgerID = tran.LedgerID;
            AccountID = tran.AccountID;
            SubID = tran.SubID;
            ProjectID = tran.ProjectID;
            TaskID = tran.TaskID;
            CostCodeID = tran.CostCodeID;
            RefNbr = tran.RefNbr;
            InventoryID = tran.InventoryID;
            UOM = tran.UOM;
            Qty = tran.Qty;
            DebitAmt = tran.DebitAmt;
            CreditAmt = tran.CreditAmt;
            CuryInfoID = tran.CuryInfoID;
            CuryDebitAmt = tran.CuryDebitAmt;
            CuryCreditAmt = tran.CuryCreditAmt;
            Released = tran.Released;
            Posted = tran.Posted;
            NonBillable = tran.NonBillable;
            IsInterCompany = tran.IsInterCompany;
            SummPost = tran.SummPost;
            ZeroPost = tran.ZeroPost;
            OrigModule = tran.OrigModule;
            OrigBatchNbr = tran.OrigBatchNbr;
            OrigLineNbr = tran.OrigLineNbr;
            OrigAccountID = tran.OrigAccountID;
            OrigSubID = tran.OrigSubID;
            TranID = tran.TranID;
            TranType = tran.TranType;
            TranClass = tran.TranClass;
            TranDesc = tran.TranDesc;
            TranDate = tran.TranDate;
            TranLineNbr = tran.TranLineNbr;
            ReferenceID = tran.ReferenceID;
            FinPeriodID = tran.FinPeriodID;
            TranPeriodID = tran.TranPeriodID;
            CATranID = tran.CATranID;
            PMTranID = tran.PMTranID;
            OrigPMTranID = tran.OrigPMTranID;
            LedgerBalanceType = tran.LedgerBalanceType;
            AccountRequireUnits = tran.AccountRequireUnits;
            TaxID = tran.TaxID;
            TaxCategoryID = tran.TaxCategoryID;
            NoteID = tran.NoteID;
            ReclassificationProhibited = tran.ReclassificationProhibited;
            ReclassBatchModule = tran.ReclassBatchModule;
            ReclassBatchNbr = tran.ReclassBatchNbr;
            IsReclassReverse = tran.IsReclassReverse;
            ReclassType = tran.ReclassType;
            CuryReclassRemainingAmt = tran.CuryReclassRemainingAmt;
            ReclassRemainingAmt = tran.ReclassRemainingAmt;
            Reclassified = tran.Reclassified;
            ReclassSourceTranModule = tran.ReclassSourceTranModule;
            ReclassSourceTranBatchNbr = tran.ReclassSourceTranBatchNbr;
            ReclassSourceTranLineNbr = tran.ReclassSourceTranLineNbr;
            ReclassSeqNbr = tran.ReclassSeqNbr;
            tstamp = tran.tstamp;
            CreatedByID = tran.CreatedByID;
            CreatedByScreenID = tran.CreatedByScreenID;
            CreatedDateTime = tran.CreatedDateTime;
            LastModifiedByID = tran.LastModifiedByID;
            LastModifiedByScreenID = tran.LastModifiedByScreenID;
            LastModifiedDateTime = tran.LastModifiedDateTime;
        }

        #region Selected

        [PXBool]
        [PXUIField(DisplayName = "Selected", Visible = true, Enabled = true)]
        public override bool? Selected { get; set; }
        #endregion
        #region SplittedIcon
        public abstract class splitIcon : IBqlField { }

        [PXUIField(DisplayName = "", Enabled = false, Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
        [PXImage]
        public virtual string SplitIcon { get; set; }
        #endregion
        #region ActionDesc
        public abstract class actionDesc : IBqlField { }

        [PXString]
        [PXUIField(DisplayName = "Action", IsReadOnly = true, Visibility = PXUIVisibility.Visible)]
        [ReclassAction.List]
        public virtual string ActionDesc { get; set; }
        #endregion
        #region SortOrder
        public abstract class sortOrder : IBqlField { }

        [PXInt]
        [PXDefault]
        public virtual int? SortOrder
        {
            get;
            set;
        }
        #endregion
        #region BranchID
        [Branch(typeof(Batch.branchID), Enabled = false)]
        public override int? BranchID { get; set; }
        #endregion
        #region BatchNbr
        [PXDBString(15, IsUnicode = true, IsKey = true)]
        [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, IsReadOnly = true)]
        public override string BatchNbr { get; set; }
        #endregion
        #region AccountID
        [Account(typeof(GLTran.branchID), LedgerID = typeof(GLTran.ledgerID), DescriptionField = typeof(Account.description), Enabled = false)]
        public override int? AccountID { get; set; }
        #endregion
        #region SubID
        [SubAccount(typeof(GLTran.accountID), typeof(GLTran.branchID), true, Enabled = false)]
        public override int? SubID { get; set; }
        #endregion
        #region CuryDebitAmt
        [PXDBCurrency(typeof(GLTran.curyInfoID), typeof(GLTran.debitAmt))]
        [PXUIField(DisplayName = "Debit Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public override decimal? CuryDebitAmt { get; set; }
        #endregion
        #region CuryCreditAmt
        [PXDBCurrency(typeof(GLTran.curyInfoID), typeof(GLTran.creditAmt))]
        [PXUIField(DisplayName = "Credit Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public override decimal? CuryCreditAmt { get; set; }
        #endregion
        #region TranDesc
        [PXDBString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Transaction Description", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public override string TranDesc { get; set; }
        #endregion
        #region FinPeriodID
        [FinPeriodID]
        [PXUIField(DisplayName = "Post Period", Enabled = false)]
        public override string FinPeriodID { get;  set; }
        #endregion
        #region IsParent
        public abstract class isParent : IBqlField { }

        [PXBool]
        public bool? IsParent { get; set; }
		#endregion
		#region IsSplited
		public abstract class isSplited : IBqlField { }

		[PXBool]
		public bool? IsSplited { get; set; }
		#endregion
		#region IsCurrent
		public abstract class isCurrent : IBqlField { }

        [PXBool]
        public bool? IsCurrent { get; set; }
        #endregion

        public GLTranReclHist ParentTran { get; set; }

        private List<GLTranReclHist> _ChildTrans;
        public List<GLTranReclHist> ChildTrans
        {
            get
            {
                if(_ChildTrans == null)
                {
                    _ChildTrans = new List<GLTranReclHist>();
                }

                return _ChildTrans;
            }
            set
            {
                _ChildTrans = value;
            }
        }
    }
}
