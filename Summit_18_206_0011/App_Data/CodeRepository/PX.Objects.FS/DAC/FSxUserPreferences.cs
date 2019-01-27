using PX.Data;
using PX.Objects.CS;
using PX.SM;

namespace PX.Objects.FS
{
    [PXTable(typeof(UserPreferences.userID), IsOptional = true)]
    public class FSxUserPreferences : PXCacheExtension<UserPreferences>
	{
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region DfltBranchLocationID
        public abstract class dfltBranchLocationID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Default Branch Location")]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<UserPreferences.defBranchID>>>>), SubstituteKey = typeof(FSBranchLocation.branchLocationCD), DescriptionField = typeof(FSBranchLocation.descr))]
        [PXFormula(typeof(Default<UserPreferences.defBranchID>))]
        public virtual int? DfltBranchLocationID { get; set; }
        #endregion

	}
}
