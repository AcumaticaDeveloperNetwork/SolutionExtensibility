using PX.Data;
using System;
using System.Collections;

namespace PX.Objects.FS
{
    public static class TreeWFStageHelper
    {
        public class TreeWFStageView : PXSelectOrderBy<FSWFStage, OrderBy<Asc<FSWFStage.sortOrder>>>
        {
            public TreeWFStageView(PXGraph graph) : base(graph)
            {
            }

            public TreeWFStageView(PXGraph graph, Delegate handler) : base(graph, handler)
            {
            }
        }

        public static IEnumerable treeWFStages(PXGraph graph, string srvOrdType, int? wFStageID)
        {
            if (wFStageID == null)
            {
                wFStageID = 0;
            }

            PXResultset<FSWFStage> fsWFStageSet = PXSelectJoin<FSWFStage,
                    InnerJoin<FSSrvOrdType,
                        On<
                            FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>,
                    Where<
                        FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>,
                        And<FSWFStage.parentWFStageID, Equal<Required<FSWFStage.parentWFStageID>>>>,
                    OrderBy<
                        Asc<FSWFStage.sortOrder>>>.Select(graph, srvOrdType, wFStageID);

            foreach (FSWFStage fsWFStageRow in fsWFStageSet)
            {
                yield return fsWFStageRow;
            }
        }

        public static void EnableDisableActionsByWorkflowStage(
                                PXCache cache,
                                FSWFStage fsWFStageRow,
                                PXAction actionComplete,
                                PXAction actionCancel,
                                PXAction actionClose,
                                PXAction actionReopen)
        {
            if (fsWFStageRow == null)
            {
                return;
            }

            if (fsWFStageRow == null)
            {
                return;
            }

            if (actionComplete != null && fsWFStageRow.AllowComplete == false)
            {
                actionComplete.SetEnabled(false);
            }

            if (actionCancel != null && fsWFStageRow.AllowCancel == false)
            {
                actionCancel.SetEnabled(false);
            }

            if (actionClose != null && fsWFStageRow.AllowClose == false)
            {
                actionClose.SetEnabled(false);
            }

            if (actionReopen != null && fsWFStageRow.AllowReopen == false)
            {
                actionReopen.SetEnabled(false);
            }

            cache.AllowUpdate &= (bool)fsWFStageRow.AllowModify;
            cache.AllowDelete &= (bool)fsWFStageRow.AllowDelete;
        }
    }
}
