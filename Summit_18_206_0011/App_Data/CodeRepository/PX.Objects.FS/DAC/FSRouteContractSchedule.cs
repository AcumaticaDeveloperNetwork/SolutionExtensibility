using PX.Data;
using System;
using PX.Objects.CR;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    [PXPrimaryGraph(typeof(RouteServiceContractScheduleEntry))]
    public class FSRouteContractSchedule : FSSchedule
	{
        #region CustomerID
        public abstract new class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Customer", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorContractScheduleCustomer(typeof(Where<FSServiceContract.recordType, Equal<FSServiceContract.recordType.RouteServiceContract>>))]
        [PXRestrictor(typeof(Where<BAccountSelectorBase.status, IsNull,
               Or<BAccountSelectorBase.status, Equal<BAccount.status.active>,
               Or<BAccountSelectorBase.status, Equal<BAccount.status.oneTime>>>>),
               PX.Objects.AR.Messages.CustomerIsInStatus, typeof(BAccountSelectorBase.status))]
        public override int? CustomerID { get; set; }
        #endregion
        #region EntityID
        [PXDBInt(IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Service Contract Nbr.")]
        [PXSelector(typeof(Search<FSServiceContract.serviceContractID,
                                    Where<
                                        FSServiceContract.customerID, Equal<Current<FSRouteContractSchedule.customerID>>,
                                        And<FSServiceContract.recordType, Equal<FSServiceContract.recordType.RouteServiceContract>>>>),
                           SubstituteKey = typeof(FSServiceContract.refNbr))]
        public override int? EntityID { get; set; }
        #endregion
        #region RefNbr
        public new abstract class refNbr : PX.Data.IBqlField
        {
        }

        //Included in FSRouteContractScheduleFSServiceContract projection
        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Schedule Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<FSRouteContractSchedule.refNbr,
                           Where<FSRouteContractSchedule.entityID, Equal<Current<FSRouteContractSchedule.entityID>>,
                               And<FSRouteContractSchedule.entityType, Equal<FSRouteContractSchedule.entityType.Contract>>>,
                           OrderBy<Desc<FSRouteContractSchedule.refNbr>>>))]
        [RouteContractScheduleAutoNumber]
        public override string RefNbr { get; set; }
        #endregion
        #region SrvOrdType
        public new abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        [PXUIField(DisplayName = "Service Order Type")]
        [PXDefault]
        [FSSelectorRouteContractSrvOrdTypeAttribute]
        public override string SrvOrdType { get; set; }
        #endregion
        #region ScheduleGenType
        public new abstract class scheduleGenType : ListField_ScheduleGenType_ContractSchedule
        {
        }

        [PXDBString(2, IsUnicode = true)]
        [scheduleGenType.ListAtrribute]
        [PXUIField(DisplayName = "Schedule Generation Type")]
        [PXDefault(typeof(Search<FSServiceContract.scheduleGenType,
                                  Where<
                                       FSServiceContract.customerID, Equal<Current<FSRouteContractSchedule.customerID>>,
                                       And<FSServiceContract.serviceContractID, Equal<Current<FSRouteContractSchedule.entityID>>>>>))]
        public override string ScheduleGenType { get; set; }
        #endregion
    }
}