using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using System;

using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.SERVICE_ORDER)]
    [PXPrimaryGraph(typeof(ServiceOrderEntry))]
    public class FSServiceOrder : PX.Data.IBqlTable
    {
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsKey = true, IsFixed = true, InputMask = ">AAAA")]
        [PXUIField(DisplayName = "Service Order Type", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDefault(typeof(FSSetup.dfltSrvOrdType))]
        [FSSelectorSrvOrdType]
        [PX.Data.EP.PXFieldDescription]
        public virtual string SrvOrdType { get; set; }
        #endregion
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault]
        [PXUIField(DisplayName = "Service Order Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorSORefNbr]
        [AutoNumber(typeof(Search<FSSrvOrdType.srvOrdNumberingID,
                        Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>>),
                    typeof(AccessInfo.businessDate))]
        [PX.Data.EP.PXFieldDescription]
        public virtual string RefNbr { get; set; }
        #endregion
        #region SOID
        public abstract class sOID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        public virtual int? SOID { get; set; }
        #endregion
        #region Attributes
        /// <summary>
        /// A service field, which is necessary for the <see cref="CSAnswers">dynamically 
        /// added attributes</see> defined at the <see cref="FSSrvOrdType">customer 
        /// class</see> level to function correctly.
        /// </summary>
        [CRAttributesField(typeof(FSServiceOrder.srvOrdType), typeof(FSServiceOrder.noteID))]
        public virtual string[] Attributes { get; set; }
        #endregion
        #region AddressLine1
        public abstract class addressLine1 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Address Line 1")]
        public virtual string AddressLine1 { get; set; }
        #endregion
        #region AddressLine2
        public abstract class addressLine2 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Address Line 2")]
        public virtual string AddressLine2 { get; set; }
        #endregion
        #region AddressLine3
        public abstract class addressLine3 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Address Line 3", Visible = false, Enabled = false)]
        public virtual string AddressLine3 { get; set; }
        #endregion
        #region AddressValidated
        public abstract class addressValidated : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Address Validated", Enabled = true)]
        [PXFormula(typeof(Default<FSServiceOrder.addressLine1>))]
        [PXFormula(typeof(Default<FSServiceOrder.addressLine2>))]
        [PXFormula(typeof(Default<FSServiceOrder.postalCode>))]
        [PXFormula(typeof(Default<FSServiceOrder.countryID>))]
        [PXFormula(typeof(Default<FSServiceOrder.city>))]
        [PXFormula(typeof(Default<FSServiceOrder.state>))]
        public virtual bool? AddressValidated { get; set; }
        #endregion
        #region AllowInvoice
        public abstract class allowInvoice : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Allow Invoice", Enabled = false)]
        public virtual bool? AllowInvoice { get; set; }
        #endregion
        #region AssignedEmpID
        public abstract class assignedEmpID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [FSSelector_StaffMember_All]
        [PXUIField(DisplayName = "Supervisor", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual int? AssignedEmpID { get; set; }
        #endregion
        #region AutoDocDesc
        public abstract class autoDocDesc : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Service description", Visible = true, Enabled = false)]
        public virtual string AutoDocDesc { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Customer", Visibility = PXUIVisibility.SelectorVisible)]
        [PXRestrictor(typeof(Where<BAccountSelectorBase.status, IsNull,
                Or<BAccountSelectorBase.status, Equal<BAccount.status.active>,
                Or<BAccountSelectorBase.status, Equal<BAccount.status.oneTime>>>>), 
                PX.Objects.AR.Messages.CustomerIsInStatus, typeof(BAccountSelectorBase.status))]
        [FSSelectorBusinessAccount_CU_PR_VC]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region LocationID
        public abstract class locationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>,
                            And<MatchWithBranch<Location.cBranchID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Location", DirtyRead = true)]
        [PXRestrictor(typeof(Where<Location.isActive, Equal<True>>), IN.Messages.InactiveLocation, typeof(Location.locationCD))]
        [PXDefault(typeof(Coalesce<Search2<BAccountR.defLocationID,
            InnerJoin<CRLocation, On<CRLocation.bAccountID, Equal<BAccountR.bAccountID>, And<CRLocation.locationID, Equal<BAccountR.defLocationID>>>>,
            Where<BAccountR.bAccountID, Equal<Current<FSServiceOrder.customerID>>,
                And<CRLocation.isActive, Equal<True>,
                And<MatchWithBranch<CRLocation.cBranchID>>>>>,
            Search<CRLocation.locationID,
            Where<CRLocation.bAccountID, Equal<Current<FSServiceOrder.customerID>>,
            And<CRLocation.isActive, Equal<True>, And<MatchWithBranch<CRLocation.cBranchID>>>>>>))]
        public virtual int? LocationID { get; set; }
        #endregion
        #region BillCustomerID
        public abstract class billCustomerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Billing Customer")]
        [FSSelectorCustomer]
        public virtual int? BillCustomerID { get; set; }
        #endregion
        #region BillLocationID
        public abstract class billLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>,
                            And<MatchWithBranch<Location.cBranchID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Billing Location", DirtyRead = true)]
        [PXRestrictor(typeof(Where<Location.isActive, Equal<True>>), IN.Messages.InactiveLocation, typeof(Location.locationCD))]
        public virtual int? BillLocationID { get; set; }
        #endregion
        #region CauseID
        public abstract class causeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Cause")]
        [PXSelector(typeof(FSCause.causeID), SubstituteKey = typeof(FSCause.causeCD), DescriptionField = typeof(FSCause.descr))]
        public virtual int? CauseID { get; set; }
        #endregion
        #region City
        public abstract class city : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "City")]
        public virtual string City { get; set; }
        #endregion
        #region ContactID
        public abstract class contactID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Contact ID")]
        [FSSelectorContact]
        public virtual int? ContactID { get; set; }
        #endregion
        #region ContractID
        public abstract class contractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Contract", Enabled = false)]
        [FSSelectorContract]
        [PXRestrictor(typeof(Where<Contract.status, Equal<FSServiceContract.status.Active>>), "Restrictor 1")]
        [PXRestrictor(typeof(Where<Current<AccessInfo.businessDate>, LessEqual<Contract.graceDate>, Or<Contract.expireDate, IsNull>>), "Restrictor 2")]
        [PXRestrictor(typeof(Where<Current<AccessInfo.businessDate>, GreaterEqual<Contract.startDate>>), "Restrictor 3", typeof(Contract.startDate))]
        public virtual int? ContractID { get; set; }
        #endregion
        #region CountryID
        public abstract class countryID : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "Country")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [Country]
        public virtual string CountryID { get; set; }
        #endregion
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(AccessInfo.branchID))]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(Search<Branch.branchID>), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.acctName))]
        public virtual int? BranchID { get; set; }
        #endregion
        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(
            Search<FSxUserPreferences.dfltBranchLocationID, 
            Where<
                PX.SM.UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>,
                And<PX.SM.UserPreferences.defBranchID, Equal<Current<FSServiceOrder.branchID>>>>>))]
        [PXUIField(DisplayName = "Branch Location")]
        [PXSelector(typeof(
            Search<FSBranchLocation.branchLocationID, 
            Where<
                FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>), 
            SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
            DescriptionField = typeof(FSBranchLocation.descr))]
        [PXFormula(typeof(Default<FSServiceOrder.branchID>))]
        public virtual int? BranchLocationID { get; set; }
        #endregion
        #region RoomID
        public abstract class roomID : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Room")]
        [PXSelector(typeof(
            Search<FSRoom.roomID, 
            Where<
                FSRoom.branchLocationID, Equal<Current<FSServiceOrder.branchLocationID>>>>), 
            SubstituteKey = typeof(FSRoom.roomID), DescriptionField = typeof(FSRoom.descr))]
        public virtual string RoomID { get; set; }
        #endregion
        #region OrderDate
        public abstract class orderDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? OrderDate { get; set; }
        #endregion
        #region CuryID
        public abstract class curyID : PX.Data.IBqlField { }
        [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
        [PXDefault(typeof(Search<Company.baseCuryID>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Currency.curyID))]
        [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual String CuryID { get; set; }
        #endregion
        #region CuryInfoID
        public abstract class curyInfoID : PX.Data.IBqlField { }
        [PXDBLong]
        [CurrencyInfo]
        public virtual Int64? CuryInfoID { get; set; }
        #endregion
        #region DfltProjectTaskID
        public abstract class dfltProjectTaskID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Default Project Task")]
        [PXFormula(typeof(Default<FSServiceOrder.projectID>))]
        [FSSelectorActive_AR_SO_ProjectTask(typeof(Where<PMTask.projectID, Equal<Current<FSServiceOrder.projectID>>>))]
        public virtual int? DfltProjectTaskID { get; set; }
        #endregion
        #region DocDesc
        public abstract class docDesc : PX.Data.IBqlField
        {
        }

        [PXDBString(Common.Constants.TranDescLength, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string DocDesc { get; set; }
        #endregion
        #region EMail
        public abstract class eMail : PX.Data.IBqlField
        {
        }

        [PXDBEmail]
        [PXMassMergableField]
        [PXUIField(DisplayName = "Email")]
        public virtual string EMail { get; set; }
        #endregion
        #region EstimatedDurationTotal
        public abstract class estimatedDurationTotal : PX.Data.IBqlField
        {
        }

        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXUIField(DisplayName = "Estimated Duration", Enabled = false)]
        [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? EstimatedDurationTotal { get; set; }
        #endregion
        #region Fax
        public abstract class fax : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Fax")]
        [PhoneValidation]
        [PXMassMergableField]
        public virtual string Fax { get; set; }
        #endregion
        #region Hold
        public abstract class hold : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Hold")]
        public virtual bool? Hold { get; set; }
        #endregion
        #region LongDescr
        public abstract class longDescr : PX.Data.IBqlField
        {
        }

        [PXDBString(int.MaxValue, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string LongDescr { get; set; }
        #endregion
        #region Phone1
        public abstract class phone1 : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Phone 1")]
        [PhoneValidation]
        [PXMassMergableField]
        public virtual string Phone1 { get; set; }
        #endregion
        #region Phone2
        public abstract class phone2 : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Phone 2")]
        [PhoneValidation]
        [PXMassMergableField]
        public virtual string Phone2 { get; set; }
        #endregion
        #region Phone3
        public abstract class phone3 : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Phone 3", Visible = false, Enabled = false)]
        [PhoneValidation]
        [PXMassMergableField]
        public virtual string Phone3 { get; set; }
        #endregion
        #region PostalCode
        public abstract class postalCode : PX.Data.IBqlField
        {
        }

        [PXDBString(20)]
        [PXUIField(DisplayName = "Postal Code")]
        [PXZipValidation(typeof(Country.zipCodeRegexp), typeof(Country.zipCodeMask), typeof(Address.countryID))]
        [PXDynamicMask(typeof(Search<Country.zipCodeMask, Where<Country.countryID, Equal<Current<FSServiceOrder.countryID>>>>))]
        [PXFormula(typeof(Default<FSServiceOrder.countryID>))]
        public virtual string PostalCode { get; set; }
        #endregion

        #region EstimatedOrderTotal
        public abstract class estimatedOrderTotal : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Base Estimated Total", Enabled = false)]
        public virtual Decimal? EstimatedOrderTotal { get; set; }
        #endregion
        #region CuryEstimatedOrderTotal
        public abstract class curyEstimatedOrderTotal : PX.Data.IBqlField { }
        [PXDBCurrency(typeof(curyInfoID), typeof(estimatedOrderTotal))]
        [PXUIField(DisplayName = "Estimated Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryEstimatedOrderTotal { get; set; }
        #endregion
        #region BillableOrderTotal
        public abstract class billableOrderTotal : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Base Billable Total", Enabled = false)]
        public virtual Decimal? BillableOrderTotal { get; set; }
        #endregion
        #region CuryBillableOrderTotal
        public abstract class curyBillableOrderTotal : PX.Data.IBqlField { }
        [PXDBCurrency(typeof(curyInfoID), typeof(billableOrderTotal))]
        [PXUIField(DisplayName = "Line Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryBillableOrderTotal { get; set; }
        #endregion

        #region Priority
        public abstract class priority : ListField_Priority_ServiceOrder
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ID.Priority_ServiceOrder.MEDIUM)]
        [PXUIField(DisplayName = "Priority", Visibility = PXUIVisibility.SelectorVisible)]
        [priority.ListAtrribute]
        public virtual string Priority { get; set; }
        #endregion
        #region ProblemID
        public abstract class problemID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Problem")]
        [PXSelector(typeof(Search2<FSProblem.problemID,
                            InnerJoin<FSSrvOrdTypeProblem, On<FSProblem.problemID, Equal<FSSrvOrdTypeProblem.problemID>>,
                            InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSSrvOrdTypeProblem.srvOrdType>>>>,
                            Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>),
                            SubstituteKey = typeof(FSProblem.problemCD), DescriptionField = typeof(FSProblem.descr))]
        public virtual int? ProblemID { get; set; }
        #endregion
        #region PromisedDate
        public abstract class promisedDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Promised Date")]
        public virtual DateTime? PromisedDate { get; set; }
        #endregion
        #region ProjectID
        public abstract class projectID : PX.Data.IBqlField
        {
        }

        [ProjectDefault]
        [PXRestrictor(typeof(Where<PMProject.isActive, Equal<True>>), PM.Messages.InactiveContract, typeof(PMProject.contractCD))]
        [PXRestrictor(typeof(Where<PMProject.isCancelled, Equal<False>>), PM.Messages.CancelledContract, typeof(PMProject.contractCD))]
        [ProjectBase(typeof(FSServiceOrder.customerID))]
        public virtual int? ProjectID { get; set; }
        #endregion  
        #region ResolutionDate
        public abstract class resolutionDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Resolution Date")]
        public virtual DateTime? ResolutionDate { get; set; }
        #endregion
        #region ResolutionID
        public abstract class resolutionID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Resolution")]
        [PXSelector(typeof(FSResolution.resolutionID), SubstituteKey = typeof(FSResolution.resolutionCD), DescriptionField = typeof(FSResolution.descr))]
        public virtual int? ResolutionID { get; set; }
        #endregion
        #region Severity
        public abstract class severity : ListField_Severity_ServiceOrder
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ID.Severity_ServiceOrder.MEDIUM)]
        [PXUIField(DisplayName = "Severity", Visibility = PXUIVisibility.SelectorVisible)]
        [severity.ListAtrribute]
        public virtual string Severity { get; set; }
        #endregion
        #region SLAETA
        public abstract class sLAETA : PX.Data.IBqlField
        {
        }

        protected DateTime? _SLAETA;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Deadline SLA")]
        [PXUIField(DisplayName = "Deadline - SLA", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? SLAETA
        {
            get
            {
                return this._SLAETA;
            }

            set
            {
                this.SLAETAUTC = value;
                this._SLAETA = value;
            }
        }
        #endregion
        #region SourceDocType
        public abstract class sourceDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        [PXUIField(DisplayName = "Source Document Type", Enabled = false)]
        public virtual string SourceDocType { get; set; }
        #endregion
        #region SourceID
        public abstract class sourceID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? SourceID { get; set; }
        #endregion
        #region SourceRefNbr
        public abstract class sourceRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Source Ref. Nbr.", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string SourceRefNbr { get; set; }
        #endregion
        #region SourceType
        public abstract class sourceType : ListField_SourceType_ServiceOrder
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.SourceType_ServiceOrder.SERVICE_DISPATCH)]
        [PXUIField(DisplayName = "Source Type", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
        [sourceType.ListAtrribute]
        public virtual string SourceType { get; set; }
        #endregion
        #region State
        public abstract class state : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "State")]
        [State(typeof(FSServiceOrder.countryID), DescriptionField = typeof(State.name))]
        [PXFormula(typeof(Default<FSServiceOrder.countryID>))]
        public virtual string State { get; set; }
        #endregion
        #region Status
        public abstract class status : ListField_Status_ServiceOrder
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ID.Status_ServiceOrder.OPEN)]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [status.ListAtrribute]
        public virtual string Status { get; set; }
        #endregion
        #region WFStageID
        public abstract class wFStageID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Workflow Stage")]
        [FSSelectorWorkflowStage(typeof(FSServiceOrder.srvOrdType))]
        [PXDefault(typeof(Search2<FSWFStage.wFStageID,
                    InnerJoin<FSSrvOrdType,
                        On<
                            FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>,
                    Where<
                        FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>,
                    OrderBy<
                        Asc<FSWFStage.parentWFStageID,
                        Asc<FSWFStage.sortOrder>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? WFStageID { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0], ShowInReferenceSelector=true)]
        [PXSearchable(SM.SearchCategory.FS, "SM {0}: {1} - {3}", new Type[] { typeof(FSServiceOrder.srvOrdType), typeof(FSServiceOrder.refNbr), typeof(FSServiceOrder.customerID), typeof(Customer.acctName) },
           new Type[] { typeof(Customer.acctCD), typeof(FSServiceOrder.srvOrdType), typeof(FSServiceOrder.custWorkOrderRefNbr), typeof(FSServiceOrder.docDesc) },
           NumberFields = new Type[] { typeof(FSServiceOrder.refNbr) },
           Line1Format = "{0:d}{1}{2}", Line1Fields = new Type[] { typeof(FSServiceOrder.orderDate), typeof(FSServiceOrder.status), typeof(FSServiceOrder.custWorkOrderRefNbr) },
           Line2Format = "{0}", Line2Fields = new Type[] { typeof(FSServiceOrder.docDesc) },
           MatchWithJoin = typeof(InnerJoin<Customer, On<Customer.bAccountID, Equal<FSServiceOrder.customerID>>>),
           SelectForFastIndexing = typeof(Select2<FSServiceOrder, InnerJoin<Customer, On<FSServiceOrder.customerID, Equal<Customer.bAccountID>>>>)
        )]
        
        public virtual Guid? NoteID { get; set; }
        #endregion
        #region LineCntr
        public abstract class lineCntr : PX.Data.IBqlField
        {
        }

        [PXDBInt()]
        [PXDefault(0)]
        public virtual Int32? LineCntr { get; set; }
        #endregion
        #region SplitLineCntr
        public abstract class splitLineCntr : PX.Data.IBqlField
        {
        }

        [PXDBInt()]
        [PXDefault(0)]
        public virtual Int32? SplitLineCntr { get; set; }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        [PXUIField(DisplayName = "Created By")]
        public virtual Guid? CreatedByID { get; set; }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "Created By Screen ID")]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "Last Modified By")]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "Last Modified By Screen ID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
        #region BAccountRequired
        public abstract class bAccountRequired : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Customer Required", Enabled = false)]
        public virtual bool? BAccountRequired { get; set; }
        #endregion
        #region Quote
        public abstract class quote : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Quote", Enabled = false)]
        public virtual bool? Quote { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSServiceContract.serviceContractID,
                           Where<
                                FSServiceContract.customerID, Equal<Current<FSServiceOrder.customerID>>>>),
                           SubstituteKey = typeof(FSServiceContract.refNbr))]
        [PXUIField(DisplayName = "Source Service Contract Nbr.", Enabled = false, FieldClass = "FSCONTRACT")]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region ScheduleID
        public abstract class scheduleID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSSchedule.scheduleID,
                           Where<
                                FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>,
                                And<FSSchedule.entityID, Equal<Current<FSServiceOrder.serviceContractID>>>>>),
                           SubstituteKey = typeof(FSSchedule.refNbr))]
        [PXUIField(DisplayName = "Schedule Ref. Nbr.", Enabled = false, FieldClass = "FSCONTRACT")]
        public virtual int? ScheduleID { get; set; }
        #endregion
        #region FinPeriodID
        public abstract class finPeriodID : PX.Data.IBqlField
        {
        }

        [PXDBString(6, IsFixed = true)]
        [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string FinPeriodID { get; set; }
        #endregion
        #region GenerationID
        public abstract class generationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Generation ID")]
        public virtual int? GenerationID { get; set; }
        #endregion
        #region TotalAttendees
        public abstract class totalAttendees : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(Visible = false)]
        public virtual int? TotalAttendees { get; set; }
        #endregion
        #region CustWorkOrderRefNbr
        public abstract class custWorkOrderRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "External Reference")]
        public virtual string CustWorkOrderRefNbr { get; set; }
        #endregion
        #region CustPORefNbr
        public abstract class custPORefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(40, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "Customer Order")]
        public virtual string CustPORefNbr { get; set; }
        #endregion
        #region ServiceCount
        public abstract class serviceCount : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Service Count", Enabled = false)]
        public virtual int? ServiceCount { get; set; }
        #endregion
        #region ScheduledServiceCount
        public abstract class scheduledServiceCount : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Scheduled Service Count", Enabled = false)]
        public virtual int? ScheduledServiceCount { get; set; }
        #endregion
        #region CompleteServiceCount
        public abstract class completeServiceCount : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Complete Service Count", Enabled = false)]
        public virtual int? CompleteServiceCount { get; set; }
        #endregion
        #region PostedBy
        public abstract class postedBy : ListField_Billing_By
        {
        }

        [PXDBString(2, IsFixed = true)]
        public virtual string PostedBy { get; set; }
        #endregion
        #region PendingAPARSOPost
        public abstract class pendingAPARSOPost : IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        public virtual bool? PendingAPARSOPost { get; set; }
        #endregion
        #region PendingINPost
        public abstract class pendingINPost : IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        public virtual bool? PendingINPost { get; set; }
        #endregion
        #region CBID
        public abstract class cBID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? CBID { get; set; }
        #endregion

        #region SalesPersonID
        public abstract class salesPersonID : PX.Data.IBqlField
        {
        }

        [SalesPerson(DisplayName = "Salesperson")]
        [PXDefault(typeof(Search<CustDefSalesPeople.salesPersonID, 
                          Where<CustDefSalesPeople.bAccountID, Equal<Current<FSServiceOrder.customerID>>, 
                          And<CustDefSalesPeople.locationID, Equal<Current<FSServiceOrder.locationID>>, 
                          And<CustDefSalesPeople.isDefault, Equal<True>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSServiceOrder.customerID>))]
        [PXFormula(typeof(Default<FSServiceOrder.locationID>))]
        public virtual int? SalesPersonID { get; set; }
        #endregion
        #region Commissionable
        public abstract class commissionable : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(typeof(
            Search<FSSrvOrdType.commissionable,
                Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Commissionable")]
        public virtual bool? Commissionable { get; set; }
        #endregion

        #region CutOffDate
        public abstract class cutOffDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Cut-Off Date")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? CutOffDate { get; set; }
        #endregion

        #region ApptDurationTotal
        public abstract class apptDurationTotal : PX.Data.IBqlField
        {
        }

        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXUIField(DisplayName = "Appointment Duration", Enabled = false)]
        [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? ApptDurationTotal { get; set; }
        #endregion
        #region ApptOrderTotal
        public abstract class apptOrderTotal : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Base Appointment Amount", Enabled = false)]
        public virtual Decimal? ApptOrderTotal { get; set; }
        #endregion
        #region CuryApptOrderTotal
        public abstract class curyApptOrderTotal : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXUIField(DisplayName = "Appointment Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual decimal? CuryApptOrderTotal { get; set; }
        #endregion

        #region BillServiceContractID
        public abstract class billServiceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(Null), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSServiceOrder.billCustomerID>))]
        [FSSelectorPrepaidServiceContract(typeof(FSServiceOrder.customerID), typeof(FSServiceOrder.billCustomerID))]
        [PXUIField(DisplayName = "Service Contract Nbr.", FieldClass = "FSCONTRACT")]
        public virtual int? BillServiceContractID { get; set; }
        #endregion
        #region BillContractPeriodID 
        public abstract class billContractPeriodID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [FSSelectorContractBillingPeriod]
        [PXDefault(typeof(Search<FSContractPeriod.contractPeriodID,
                            Where2<
                                Where<
                                    FSContractPeriod.startPeriodDate, LessEqual<Current<FSServiceOrder.orderDate>>,
                                        And<FSContractPeriod.endPeriodDate, GreaterEqual<Current<FSServiceOrder.orderDate>>>>,
                                And<
                                    FSContractPeriod.serviceContractID, Equal<Current<FSServiceOrder.billServiceContractID>>,
                                    And<FSContractPeriod.status, Equal<FSContractPeriod.status.Active>,
                                    And<Current<FSBillingCycle.billingBy>, Equal<FSBillingCycle.billingBy.ServiceOrder>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSServiceOrder.billCustomerID, FSServiceOrder.orderDate>))]
        [PXUIField(DisplayName = "Service Contract Period", Enabled = false)]
        public virtual int? BillContractPeriodID { get; set; }
        #endregion

        #region CuryCostTotal
        public abstract class curyCostTotal : PX.Data.IBqlField
        {
        }

        [PXDBCurrency(typeof(curyInfoID), typeof(costTotal))]
        [PXUIField(DisplayName = "Cost Total")]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryCostTotal { get; set; }
        #endregion
        #region CostTotal
        public abstract class costTotal : PX.Data.IBqlField
        {
        }

        [PXDBPriceCost()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CostTotal { get; set; }
		#endregion

        #region SOCuryCompletedBillableTotal
        public abstract class sOCuryCompletedBillableTotal : PX.Data.IBqlField
        {
        }

        [PXCurrency(typeof(curyInfoID), typeof(sOCompletedBillableTotal))]
        [PXUIField(DisplayName = "Billable Total", Enabled = false)]
        public virtual Decimal? SOCuryCompletedBillableTotal { get; set; }
        #endregion
        #region SOCompletedBillableTotal
        public abstract class sOCompletedBillableTotal : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        public virtual Decimal? SOCompletedBillableTotal { get; set; }
        #endregion

        #region SOCuryUnpaidBalanace
        public abstract class sOCuryUnpaidBalanace : PX.Data.IBqlField
        {
        }

        [PXCurrency(typeof(curyInfoID), typeof(sOUnpaidBalanace))]
        [PXUIField(DisplayName = "Service Order Unpaid Balance", Enabled = false)]
        public virtual Decimal? SOCuryUnpaidBalanace { get; set; }
        #endregion
        #region SOUnpaidBalanace
        public abstract class sOUnpaidBalanace : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        public virtual Decimal? SOUnpaidBalanace { get; set; }
        #endregion

        #region SOCuryBillableUnpaidBalanace
        public abstract class sOCuryBillableUnpaidBalanace : PX.Data.IBqlField
        {
        }

        [PXCurrency(typeof(curyInfoID), typeof(sOBillableUnpaidBalanace))]
        [PXUIField(DisplayName = "Service Order Billable Unpaid Balance", Enabled = false)]
        public virtual Decimal? SOCuryBillableUnpaidBalanace { get; set; }
        #endregion
        #region SOBillableUnpaidBalanace
        public abstract class sOBillableUnpaidBalanace : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        public virtual Decimal? SOBillableUnpaidBalanace { get; set; }
        #endregion


        #region SOPrepaymentReceived
        public abstract class sOPrepaymentReceived : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        [PXUIField(DisplayName = "Prepayment Received", Enabled = false)]
        public virtual Decimal? SOPrepaymentReceived { get; set; }
        #endregion
        #region SOPrepaymentRemaining
        public abstract class sOPrepaymentRemaining : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        [PXUIField(DisplayName = "Prepayment Remaining", Enabled = false)]
        public virtual Decimal? SOPrepaymentRemaining { get; set; }
        #endregion
        #region SOPrepaymentApplied
        public abstract class sOPrepaymentApplied : PX.Data.IBqlField
        {
        }

        [PXDecimal]
        [PXUIField(DisplayName = "Prepayment Applied", Enabled = false)]
        public virtual Decimal? SOPrepaymentApplied { get; set; }
        #endregion

		#region WaitingForParts
        public abstract class waitingForParts : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Waiting for Purchased Items", Enabled = false)]
        public virtual bool? WaitingForParts { get; set; }
        #endregion
        #region AppointmentsNeeded
        public abstract class appointmentsNeeded : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Appointments Needed", Enabled = false)]
        public virtual bool? AppointmentsNeeded { get; set; }
        #endregion

        #region MemoryHelper
        #region Selected
        public abstract class selected : IBqlField
        {
        }

        [PXBool]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        #endregion

        #region ReportLocationID
        public abstract class reportLocationID : IBqlField
        {
        }

        [PXInt]
        [PXSelector(typeof(Search<Location.locationID,
                           Where<
                                Location.bAccountID, Equal<Optional<FSServiceOrder.customerID>>>>),
                           SubstituteKey = typeof(Location.locationCD), 
                           DescriptionField = typeof(Location.descr))]
        public virtual int? ReportLocationID { get; set; }
        #endregion

        #region Attention
        public abstract class attention : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Attention", Enabled = false)]
        public virtual string Attention { get; set; }
        #endregion
        
        #region Mem_ReturnValueID
        [PXInt]
        public virtual int? Mem_ReturnValueID { get; set; }
        #endregion

        #region Mem_ShowAttendees
        //Used to manage whether the Attendees tab (Service Order screen) is visible or not.
        public abstract class mem_ShowAttendees : IBqlField
        {
        }

        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(Visible = false)]
        public virtual bool? Mem_ShowAttendees { get; set; }
        #endregion

        #region Mem_Invoiced
        public abstract class mem_Invoiced : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXUIField(DisplayName = "Invoiced", Enabled = false)]
        public virtual bool? Mem_Invoiced
        {
            get
            {
                return this.PostedBy != null && this.PostedBy == ID.Billing_By.SERVICE_ORDER;
            }
        }
        #endregion

        #region AppointmentsCompleted
        [PXInt]
        public virtual int? AppointmentsCompletedCntr { get; set; }
        #endregion
        #region AppointmentsCompletedOrClosed
        [PXInt]
        public virtual int? AppointmentsCompletedOrClosedCntr { get; set; }
        #endregion
        #region MemRefNbr
        public abstract class memRefNbr : PX.Data.IBqlField
        {
        }

        [PXString(17, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCC")]
        public virtual string MemRefNbr { get; set; }
        #endregion
        #region MemAcctName
        public abstract class memAcctName : PX.Data.IBqlField
        {
        }
        [PXString(62, IsUnicode = true)]
        public virtual string MemAcctName { get; set; }
        #endregion

        #region IsPrepaymentEnable
        public abstract class isPrepaymentEnable : IBqlField
        {
        }

        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(Visible = false)]
        public virtual bool? IsPrepaymentEnable { get; set; }
        #endregion

		#region UpdateAppWaitingForParts
        public abstract class updateAppWaitingForParts : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual bool? UpdateAppWaitingForParts { get; set; }
        #endregion

        #endregion
        #region DispatchBoardHelper
        #region SLARemaning
        public abstract class sLARemaning : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string SLARemaning { get; set; }
        #endregion
        #region CustomerDisplayName
        public abstract class customerDisplayName : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string CustomerDisplayName { get; set; }
        #endregion
        #region ContactName
        public abstract class contactName : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string ContactName { get; set; }
        #endregion
        #region ContactPhone
        public abstract class contactPhone : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string ContactPhone { get; set; }
        #endregion
        #region ContactEmail
        public abstract class contactEmail : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string ContactEmail { get; set; }
        #endregion
        #region AssignedEmployeeDisplayName
        public abstract class assignedEmployeeDisplayName : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string AssignedEmployeeDisplayName { get; set; }
        #endregion
        #region ServicesRemaning
        public abstract class servicesRemaning : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? ServicesRemaning { get; set; }
        #endregion
        #region ServicesCount
        public abstract class servicesCount : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? ServicesCount { get; set; }
        #endregion
        #region ServiceClassIDs
        public abstract class serviceClassIDs : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual Array ServiceClassIDs { get; set; }
        #endregion
        #region BranchLocationDesc
        public abstract class branchLocationDesc : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string BranchLocationDesc { get; set; }
        #endregion
        #region ServiceOrderTreeHelper
        #region TreeID
        public abstract class treeID : PX.Data.IBqlField
        {
        }

        [PXInt]
        public virtual int? TreeID { get; set; }
        #endregion
        #region Text
        public abstract class text : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string Text { get; set; }
        #endregion
        #region Leaf
        public abstract class leaf : PX.Data.IBqlField
        {
        }

        [PXBool]
        public virtual bool? Leaf { get; set; }
        #endregion
        #region Rows
        public abstract class rows : PX.Data.IBqlField
        {
        }

        public virtual object Rows { get; set; }
        #endregion
        #endregion
        #endregion

        #region UTC Fields
        #region SLAETAUTC
        public abstract class sLAETAUTC : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time")]
        [PXUIField(DisplayName = "Deadline - SLA", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? SLAETAUTC { get; set; }
        #endregion
        #endregion

        #region Tax Fields
        #region CuryVatExemptTotal
        public abstract class curyVatExemptTotal : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.vATReporting>))]
        [PXDBCurrency(typeof(FSServiceOrder.curyInfoID), typeof(FSServiceOrder.vatExemptTotal))]
        [PXUIField(DisplayName = "VAT Exempt Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryVatExemptTotal { get; set; }
        #endregion
        #region VatExemptTotal
        public abstract class vatExemptTotal : PX.Data.IBqlField { }

        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? VatExemptTotal { get; set; }
        #endregion
        #region CuryVatTaxableTotal
        public abstract class curyVatTaxableTotal : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.vATReporting>))]
        [PXDBCurrency(typeof(FSServiceOrder.curyInfoID), typeof(FSServiceOrder.vatTaxableTotal))]
        [PXUIField(DisplayName = "VAT Taxable Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryVatTaxableTotal { get; set; }
        #endregion
        #region VatTaxableTotal
        public abstract class vatTaxableTotal : PX.Data.IBqlField { }

        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? VatTaxableTotal { get; set; }
        #endregion

        #region TaxZoneID
        public abstract class taxZoneID : PX.Data.IBqlField { }

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Customer Tax Zone")]
        [PXSelector(typeof(TaxZone.taxZoneID), DescriptionField = typeof(TaxZone.descr), Filterable = true)]
        [PXFormula(typeof(Default<FSServiceOrder.branchID>))]
        [PXFormula(typeof(Default<FSServiceOrder.billLocationID>))]
        public virtual String TaxZoneID { get; set; }
        #endregion

        #region TaxTotal
        public abstract class taxTotal : PX.Data.IBqlField { }

        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? TaxTotal { get; set; }
        #endregion
        #region CuryTaxTotal
        public abstract class curyTaxTotal : PX.Data.IBqlField { }

        [PXDBCurrency(typeof(FSServiceOrder.curyInfoID), typeof(FSServiceOrder.taxTotal))]
        [PXUIField(DisplayName = "Tax Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryTaxTotal { get; set; }
        #endregion

        #region DiscTot
        public abstract class discTot : PX.Data.IBqlField { }

        [PXDBBaseCury()]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? DiscTot { get; set; }
        #endregion
        #region CuryDiscTot
        public abstract class curyDiscTot : PX.Data.IBqlField { }

        [PXDBCurrency(typeof(FSServiceOrder.curyInfoID), typeof(FSServiceOrder.discTot))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Discount")]
        public virtual Decimal? CuryDiscTot { get; set; }
        #endregion

        #region DocTotal
        public abstract class docTotal : PX.Data.IBqlField { }
        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Base Order Total", Enabled = false)]
        public virtual Decimal? DocTotal { get; set; }
        #endregion
        #region CuryDocTotal
        public abstract class curyDocTotal : PX.Data.IBqlField { }
        [PXDependsOnFields(typeof(curyBillableOrderTotal), typeof(curyDiscTot), typeof(curyTaxTotal))]
        [PXDBCurrency(typeof(curyInfoID), typeof(docTotal))]
        [PXUIField(DisplayName = "Service Order Total", Enabled = false)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        public virtual Decimal? CuryDocTotal { get; set; }
        #endregion

        #region IsTaxValid
        public abstract class isTaxValid : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Tax is up to date", Enabled = false)]
        public virtual Boolean? IsTaxValid { get; set; }
        #endregion
        #endregion

        #region IsCalledFromQuickProcess
        public abstract class isCalledFromQuickProcess : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual bool? IsCalledFromQuickProcess { get; set; }
        #endregion
    }
}
