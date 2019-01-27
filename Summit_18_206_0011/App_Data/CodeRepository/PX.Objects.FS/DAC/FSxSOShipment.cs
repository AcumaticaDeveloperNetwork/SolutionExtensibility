using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;

namespace PX.Objects.FS
{
    [PXTable(IsOptional = true)]
    public class FSxSOShipment : PXCacheExtension<SOShipment>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region Installed
        public abstract class installed : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Installed", Enabled = false)]
        public virtual bool? Installed { get; set; }
        #endregion
    }
}