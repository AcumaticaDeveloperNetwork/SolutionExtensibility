using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.FS
{
    #region PXProjection
    [Serializable]
    [PXProjection(typeof(
            Select2<FSContractPostDoc,
                 InnerJoin<FSServiceContract,
                    On<FSServiceContract.serviceContractID, Equal<FSContractPostDoc.serviceContractID>>,
                LeftJoin<Customer,
                    On<Customer.bAccountID, Equal<FSServiceContract.billCustomerID>>>>>))]
    #endregion
    public class ContractPostBatchDetail : IBqlTable
    {
        #region ContractPostDocID
        public abstract class contractPostDocID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true, BqlField = typeof(FSContractPostDoc.contractPostDocID))]
        [PXUIField(DisplayName = "Contract Post Doc. ID")]
        public virtual int? ContractPostDocID { get; set; }
        #endregion
        #region ContractPostBatchID
        public abstract class contractPostBatchID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true, BqlField = typeof(FSContractPostDoc.contractPostBatchID))]
        [PXUIField(DisplayName = "Contract Post Batch ID")]
        public virtual int? ContractPostBatchID { get; set; }
        #endregion
        #region PostedTO
        public abstract class postedTO : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof(FSContractPostDoc.postedTO))]
        [PXUIField(DisplayName = "Posted to")]
        public virtual string PostedTO { get; set; }
        #endregion
        #region PostRefNbr
        public abstract class postRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, BqlField = typeof(FSContractPostDoc.postRefNbr))]
        [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
        public virtual string PostRefNbr { get; set; }
        #endregion
        #region PostDocType
        public abstract class postDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, BqlField = typeof(FSContractPostDoc.postDocType))]
        [PXUIField(DisplayName = "Document Type", Enabled = false)]
        public virtual string PostDocType { get; set; }
        #endregion
        #region ContractRefNbr
        [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof(FSServiceContract.refNbr))]
        [PXUIField(DisplayName = "Contract Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string ContractRefNbr { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceContract.serviceContractID))]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region BillCustomerID
        public abstract class billCustomerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceContract.billCustomerID))]
        [PXUIField(DisplayName = "Billing Customer ID")]
        [FSSelectorBAccountTypeCustomerOrCombined]
        public virtual int? BillCustomerID { get; set; }
        #endregion
        #region AcctName
        [PXDBString(60, IsUnicode = true, BqlField = typeof(Customer.acctName))]
        [PXFieldDescription]
        [PXUIField(DisplayName = "Customer Name", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string AcctName { get; set; }
        #endregion
        #region BillLocationID
        public abstract class billLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<ContractPostBatchDetail.billCustomerID>>>),
                    BqlField = typeof(FSServiceContract.billLocationID),
                    DescriptionField = typeof(Location.descr), DisplayName = "Billing Location", DirtyRead = true)]
        public virtual int? BillLocationID { get; set; }
        #endregion
        #region StartDate
        public abstract class startDate : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "Start Date")]
        [PXDBDate(BqlField = typeof(FSServiceContract.startDate))]
        public virtual DateTime? StartDate { get; set; }
        #endregion
        #region NextBillingInvoiceDate 
        public abstract class nextBillingInvoiceDate : PX.Data.IBqlField
        {
        }

        [PXDBDate(BqlField = typeof(FSServiceContract.nextBillingInvoiceDate))]
        [PXUIField(DisplayName = "Next Billing Date", Enabled = false)]
        public virtual DateTime? NextBillingInvoiceDate { get; set; }
        #endregion
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceContract.branchID))]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.acctName))]
        public virtual int? BranchID { get; set; }
        #endregion
        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceContract.branchLocationID))]
        [PXUIField(DisplayName = "Branch Location")]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID,
                    Where<FSBranchLocation.branchID, Equal<Current<ContractPostBatchDetail.branchID>>>>),
                    SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
                    DescriptionField = typeof(FSBranchLocation.descr))]
        public virtual int? BranchLocationID { get; set; }
        #endregion
        #region DocDesc
        public abstract class docDesc : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true, BqlField = typeof(FSServiceContract.docDesc))]
        [PXUIField(DisplayName = "Description")]
        public virtual string DocDesc { get; set; }
        #endregion
    }
}