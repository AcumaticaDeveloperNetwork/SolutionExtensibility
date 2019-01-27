using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    [Serializable]
    [PXProjection(
        typeof(Select2<FSRouteContractSchedule,
               InnerJoin<FSServiceContract,
                    On<FSRouteContractSchedule.entityID, Equal<FSServiceContract.serviceContractID>,
                    And<FSRouteContractSchedule.entityType, Equal<FSRouteContractSchedule.entityType.Contract>>>>,
               Where<
                    FSServiceContract.recordType, Equal<FSServiceContract.recordType.RouteServiceContract>,
                    And<FSServiceContract.status, Equal<FSServiceContract.status.Active>>>>))]
    public class FSRouteContractScheduleFSServiceContract : FSRouteContractSchedule
    {
        #region CustomerLocationID
        public new abstract class customerLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(
                        Where<
                            Location.bAccountID, Equal<Optional<customerID>>,
                            And<Location.isActive, Equal<True>, And<MatchWithBranch<Location.cBranchID>>>>),
                    DescriptionField = typeof(Location.descr),
                    Visibility = PXUIVisibility.SelectorVisible,
                    BqlField = typeof(FSServiceContract.customerLocationID))]
        public override int? CustomerLocationID { get; set; }
        #endregion

        #region ServiceContractRefNbr
        public abstract class serviceContractRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof(FSServiceContract.refNbr))]
        [PXUIField(DisplayName = "Service Contract Nbr.", Visibility = PXUIVisibility.SelectorVisible, Visible = true, Enabled = true)]
        [PXSelector(
            typeof(Search<FSServiceContract.refNbr,
                   Where<
                        FSServiceContract.customerID, Equal<Current<FSRouteContractScheduleFSServiceContract.customerID>>,
                        And<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>>,
                   OrderBy<Desc<FSServiceContract.refNbr>>>))]
        [ServiceContractAutoNumber]
        public virtual string ServiceContractRefNbr { get; set; }
        #endregion

        #region DocDesc
        public abstract class docDesc : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true, BqlField = typeof(FSServiceContract.docDesc))]
        [PXUIField(DisplayName = "Description")]
        public virtual string DocDesc { get; set; }
        #endregion

        #region RefNbr
        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Schedule Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(
            typeof(Search<FSRouteContractScheduleFSServiceContract.refNbr,
                   Where<
                        FSRouteContractScheduleFSServiceContract.entityID, Equal<Current<FSRouteContractScheduleFSServiceContract.entityID>>,
                        And<FSRouteContractScheduleFSServiceContract.entityType, Equal<FSRouteContractScheduleFSServiceContract.entityType.Contract>>>,
                   OrderBy<Desc<FSRouteContractScheduleFSServiceContract.refNbr>>>))]
        public override string RefNbr { get; set; }
        #endregion
    }
}
