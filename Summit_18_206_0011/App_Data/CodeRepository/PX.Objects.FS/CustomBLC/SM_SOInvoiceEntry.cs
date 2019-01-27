using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System.Collections;

namespace PX.Objects.FS
{
    public class SM_SOInvoiceEntry : PXGraphExtension<SOInvoiceEntry>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>()
                    && PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
        }

        public virtual bool IsFSIntegrationEnabled()
        {
            if (IsActive() == false)
            {
                return false;
            }


            ARInvoice arInvoiceRow = Base.Document.Current;
            FSxARInvoice fsxARInvoiceRow = Base.Document.Cache.GetExtension<FSxARInvoice>(arInvoiceRow);

            return SM_ARInvoiceEntry.IsFSIntegrationEnabled(arInvoiceRow, fsxARInvoiceRow);
        }

        public PXAction<ARInvoice> release;
        [PXUIField(DisplayName = "Release", Visible = false)]
        [PXButton]

        public IEnumerable Release(PXAdapter adapter)
        {
            PXGraph.InstanceCreated.AddHandler<ARReleaseProcess>((graph) =>
            {
                graph.GetExtension<SM_ARReleaseProcess>().processEquipmentAndComponents = true;
            });
            return Base.release.Press(adapter);
        }

        protected virtual void ARInvoice_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
        {
            SM_ARInvoiceEntry.SetUnpersistedFSInfo(cache, e);
        }

        protected virtual void ARTran_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
        {
            bool fsIntegrationEnabled = IsFSIntegrationEnabled();
            DACHelper.SetExtensionVisibleInvisible<FSxARTran>(cache, e, fsIntegrationEnabled, false);

            if (e.Row == null)
            {
                return;
            }
        }
    }
}
