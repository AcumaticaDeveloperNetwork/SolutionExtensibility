using PX.Common;
using PX.Data;
using System;
using PX.Objects.CR;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    [PXPrimaryGraph(typeof(ServiceContractScheduleEntry))]
    public class FSContractSchedule : FSSchedule
	{
        #region CustomerID
        public abstract new class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Customer", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorContractScheduleCustomer(typeof(Where<FSServiceContract.recordType, Equal<FSServiceContract.recordType.ServiceContract>>))]
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
                                        FSServiceContract.customerID, Equal<Current<FSContractSchedule.customerID>>,
                                        And<FSServiceContract.recordType, Equal<FSServiceContract.recordType.ServiceContract>>>>),
                           SubstituteKey = typeof(FSServiceContract.refNbr))]
        public override int? EntityID { get; set; }
        #endregion
        #region RefNbr
        public new abstract class refNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Schedule Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<FSContractSchedule.refNbr,
                           Where<FSContractSchedule.entityID, Equal<Current<FSContractSchedule.entityID>>,
                               And<FSContractSchedule.entityType, Equal<FSContractSchedule.entityType.Contract>>>,
                           OrderBy<Desc<FSContractSchedule.refNbr>>>))]
        [ContractScheduleAutoNumber]
        public override string RefNbr { get; set; }
        #endregion
        #region SrvOrdType
        public new abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        [PXUIField(DisplayName = "Service Order Type")]
        [PXDefault]
        [FSSelectorContractSrvOrdType]
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
                                       FSServiceContract.customerID, Equal<Current<FSContractSchedule.customerID>>,
                                       And<FSServiceContract.serviceContractID, Equal<Current<FSContractSchedule.entityID>>>>>))]
        public override string ScheduleGenType { get; set; }
        #endregion
    }
}