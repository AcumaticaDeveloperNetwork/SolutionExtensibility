using System.Linq;
using PX.Data;

namespace PX.Objects.FS
{
    #region ContractScheduleAutoNumber
    public class ContractScheduleAutoNumberAttribute : AlternateAutoNumberAttribute
    {
        /// <summary>
        /// Allows to calculate the <c>RefNbr</c> sequence when trying to insert a new register.
        /// </summary>
        protected override bool SetRefNbr(PXCache cache, object row)
        {
            FSContractSchedule fsContractScheduleRow = (FSContractSchedule)row;

            if (fsContractScheduleRow.EntityID == null)
            {
                return false;
            }

            FSServiceContract fsServiceContractRow = ServiceContractCore.ServiceContract_View.Select(cache.Graph, fsContractScheduleRow.EntityID);

            if (fsServiceContractRow == null)
            {
                return false;
            }

            FSContractSchedule fsContractScheduleRow_tmp = PXSelectReadonly<FSContractSchedule,
                                                            Where<
                                                                FSContractSchedule.entityID, Equal<Current<FSContractSchedule.entityID>>,
                                                                And<FSContractSchedule.entityType, Equal<FSContractSchedule.entityType.Contract>>>,
                                                            OrderBy<
                                                                Desc<FSContractSchedule.scheduleID>>>
                                                            .SelectWindowed(cache.Graph, 0, 1);

            string refNbr = string.Empty;

            if (fsContractScheduleRow_tmp != null
                    && fsContractScheduleRow_tmp.RefNbr != null)
            {
                refNbr = fsContractScheduleRow_tmp.RefNbr;
            }

            if(refNbr.LastIndexOf("<NEW>") != -1)
            {
                refNbr = string.Empty;
            }

            fsContractScheduleRow.RefNbr = SharedFunctions.GetNextRefNbr(fsServiceContractRow.RefNbr, refNbr);

            return true;
        }
    }
    #endregion

    #region RouteContractScheduleAutoNumber
    public class RouteContractScheduleAutoNumberAttribute : AlternateAutoNumberAttribute
    {
        /// <summary>
        /// Allows to calculate the <c>RefNbr</c> sequence when trying to insert a new register.
        /// </summary>
        protected override bool SetRefNbr(PXCache cache, object row)
        {
            FSRouteContractSchedule fsRouteContractScheduleRow = (FSRouteContractSchedule)row;

            if (fsRouteContractScheduleRow.EntityID == null)
            {
                return false;
            }

            FSServiceContract fsServiceContractRow = ServiceContractCore.ServiceContract_View.Select(cache.Graph, fsRouteContractScheduleRow.EntityID);

            if (fsServiceContractRow == null)
            {
                return false;
            }

            FSRouteContractSchedule fsRouteContractScheduleRow_tmp = PXSelectReadonly<FSRouteContractSchedule,
                                                            Where<
                                                                FSRouteContractSchedule.entityID, Equal<Current<FSRouteContractSchedule.entityID>>,
                                                                And<FSRouteContractSchedule.entityType, Equal<FSRouteContractSchedule.entityType.Contract>>>,
                                                            OrderBy<
                                                                Desc<FSRouteContractSchedule.scheduleID>>>
                                                            .SelectWindowed(cache.Graph, 0, 1);

            string refNbr = string.Empty;

            if (fsRouteContractScheduleRow_tmp != null)
            {
                refNbr = fsRouteContractScheduleRow_tmp.RefNbr;
            }

            if (refNbr.LastIndexOf("<NEW>") != -1)
            {
                refNbr = string.Empty;
            }

            fsRouteContractScheduleRow.RefNbr = SharedFunctions.GetNextRefNbr(fsServiceContractRow.RefNbr, refNbr);

            return true;
        }
    }
    #endregion
}