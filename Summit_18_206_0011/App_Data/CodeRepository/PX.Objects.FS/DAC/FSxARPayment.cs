using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    [PXTable(IsOptional = true)]
    public class FSxARPayment : PXCacheExtension<ARPayment>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Service Contract Nbr.")]
        [FSSelectorServiceContract(typeof(FSServiceContract.serviceContractID), 
                                   typeof(ListField_RecordType_ContractSchedule.ServiceContract), 
                                   typeof(ARPayment.customerID), 
                                   typeof(Where<FSServiceContract.status, Equal<FSServiceContract.status.Active>,
                                            And<FSServiceContract.billingType, Equal<ListField_Contract_BillingType.StandardizedBillings>>>), 
                                   SubstituteKey = typeof(FSServiceContract.refNbr))]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region ChkServiceManagement
        public abstract class ChkServiceManagement : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXUIField(Visible = false)]
        public virtual bool? chkServiceManagement
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}