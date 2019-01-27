using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    [PXVirtual]
    public sealed class FSxARInvoice : PXCacheExtension<ARInvoice>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

		public abstract class hasFSEquipmentInfo : IBqlField { }

		[PXBool]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public bool? HasFSEquipmentInfo { get; set; }
    }
}
