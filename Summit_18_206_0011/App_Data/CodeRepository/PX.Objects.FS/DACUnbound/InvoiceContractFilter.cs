using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.AR;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class InvoiceContractPeriodFilter : IBqlTable
    {
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXDefault(typeof(AccessInfo.branchID))]
        [PXUIField(DisplayName = "Branch")]
        public virtual int? BranchID { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Billing Customer")]
        [FSSelectorCustomer]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region UpToDate

        public abstract class upToDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Up to Date")]
        public virtual DateTime? UpToDate { get; set; }
        #endregion
        #region InvoiceDate
        public abstract class invoiceDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXFormula(typeof(InvoiceContractPeriodFilter.upToDate))]
        [PXUIField(DisplayName = "Invoice Date")]
        public virtual DateTime? InvoiceDate { get; set; }
        #endregion
        #region InvoiceFinPeriodID
        public abstract class invoiceFinPeriodID : PX.Data.IBqlField
        {
        }

        [AROpenPeriod(typeof(InvoiceContractPeriodFilter.invoiceDate), typeof(branchID))]
        [PXUIField(DisplayName = "Invoice Period", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string InvoiceFinPeriodID { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Service Contract Nbr.")]
        [PXSelector(typeof(
                Search<
                    FSServiceContract.serviceContractID,
                Where2<
                    Where<
                        FSServiceContract.recordType, Equal<FSServiceContract.recordType.ServiceContract>,
                        Or<
                            FSServiceContract.recordType, Equal<FSServiceContract.recordType.RouteServiceContract>>>,
                And<
                    Where<
                        Current<InvoiceContractPeriodFilter.customerID>, IsNull,
                    Or<
                        FSServiceContract.billCustomerID, Equal<Current<InvoiceContractPeriodFilter.customerID>>>>>>>),
                SubstituteKey = typeof(FSServiceContract.refNbr))]
        public virtual int? ServiceContractID { get; set; }
        #endregion
    }
}