using PX.Data;
using PX.Objects.CR;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSCreateServiceOrderOnOpportunityFilter : PX.Data.IBqlTable
    {
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXString(4, IsFixed = true, InputMask = ">AAAA")]
        [PXUIField(DisplayName = "Service Order Type", Required = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [FSSelectorActiveSrvOrdType]
        public virtual string SrvOrdType { get; set; }
        #endregion

        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXDefault(typeof(
            Search<FSxUserPreferences.dfltBranchLocationID,
            Where<
                PX.SM.UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>,
                And<PX.SM.UserPreferences.defBranchID, Equal<Current<FSServiceOrder.branchID>>>>>), PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID,
                            Where<FSBranchLocation.branchID, Equal<Current<AccessInfo.branchID>>>>),
                            SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
                            DescriptionField = typeof(FSBranchLocation.descr))]
        [PXUIField(DisplayName = "Branch Location", Required = true)]
        public virtual int? BranchLocationID { get; set; }
        #endregion
    }
}
