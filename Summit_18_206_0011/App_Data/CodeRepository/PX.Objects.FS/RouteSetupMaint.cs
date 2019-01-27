using PX.Data;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    public class RouteSetupMaint : PXGraph<RouteSetupMaint>
    {
        public PXSave<FSRouteSetup> Save;
        public PXCancel<FSRouteSetup> Cancel;
        public PXSelect<FSRouteSetup> RouteSetupRecord;
        public PXSelect<FSSetup> SetupRecord;

        public RouteSetupMaint()
            : base()
        {
            FSSetup setup = PXSelectReadonly<FSSetup>.Select(this);
            if (setup == null)
            {
                throw new PXSetupNotEnteredException(ErrorMessages.SetupNotEntered, typeof(FSSetup), PXMessages.LocalizeNoPrefix(TX.ScreenName.SERVICE_PREFERENCES));
            }
        }

        public virtual void FSRouteSetup_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            if (SetupRecord.Current == null)
            {
                SetupRecord.Current = SetupRecord.Select();
            }
        }

        public virtual void FSSetup_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            FSSetup fsSetupRow = (FSSetup)e.Row;

            EquipmentSetupMaint.EnableDisable_Document(cache, fsSetupRow);
        }
    }
}