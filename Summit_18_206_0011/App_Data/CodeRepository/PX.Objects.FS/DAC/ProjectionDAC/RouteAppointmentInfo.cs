using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXPrimaryGraph(typeof(AppointmentEntry))]
    [PXProjection(typeof(
        Select5<FSAppointment,
                InnerJoin<FSServiceOrder,
                    On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>,
                LeftJoin<FSSODet,
                    On<FSSODet.sOID, Equal<FSServiceOrder.sOID>>,
                LeftJoin<FSAppointmentDet,
                    On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>,
                        And<FSAppointmentDet.sODetID, Equal<FSSODet.sODetID>>>,
                LeftJoin<FSAppointmentEmployee,
                    On<FSAppointmentEmployee.appointmentID, Equal<FSAppointment.appointmentID>>,
                LeftJoin<BAccountStaffMember,
                    On<FSAppointmentEmployee.employeeID, Equal<BAccountStaffMember.bAccountID>>,
                LeftJoin<Customer,
                    On<Customer.bAccountID, Equal<FSServiceOrder.customerID>>,
                LeftJoin<FSGeoZonePostalCode,
                    On<FSGeoZonePostalCode.postalCode, Equal<FSServiceOrder.postalCode>>,
                LeftJoin<FSSrvOrdType,
                    On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>,
                LeftJoin<FSRouteDocument,
                    On<FSRouteDocument.routeDocumentID, Equal<FSAppointment.routeDocumentID>>,
                LeftJoin<FSCustomerBillingSetup,
                    On<FSCustomerBillingSetup.customerID, Equal<FSServiceOrder.customerID>,
                        And<FSCustomerBillingSetup.srvOrdType, Equal<FSSrvOrdType.srvOrdType>,
                        And<FSCustomerBillingSetup.active, Equal<True>>>>>>>>>>>>>>,
                Aggregate<
                    GroupBy<FSAppointment.appointmentID,
                    GroupBy<FSAppointment.noteID,
                    GroupBy<FSAppointment.confirmed>>>>>))]
    public partial class RouteAppointmentInfo : FSAppointment
    {
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.customerID))]
        [PXUIField(DisplayName = "Customer ID")]
        [FSSelectorCustomer]
        public virtual int? CustomerID { get; set; }
        #endregion

        #region LocationID
        public abstract class locationID : PX.Data.IBqlField
        {
        }

        [LocationID(BqlField = typeof(FSServiceOrder.locationID), DisplayName = "Location ID", DescriptionField = typeof(Location.descr))]
        public virtual int? LocationID { get; set; }
        #endregion

        #region State
        public abstract class state : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.state))]
        [PXUIField(DisplayName = "State")]
        [State(typeof(FSServiceOrder.countryID), DescriptionField = typeof(State.name))]
        public virtual string State { get; set; }
        #endregion

        #region Priority
        public abstract class priority : ListField_Priority_ServiceOrder
        {
        }

        [PXDBString(1, IsFixed = true, BqlField = typeof(FSServiceOrder.priority))]
        [PXUIField(DisplayName = "Priority", Visibility = PXUIVisibility.SelectorVisible)]
        [priority.ListAtrribute]
        public virtual string Priority { get; set; }
        #endregion

        #region ServiceID
        public abstract class serviceID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSSODet.inventoryID))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(Search<InventoryItem.inventoryID>), SubstituteKey = typeof(InventoryItem.inventoryCD), DescriptionField = typeof(InventoryItem.descr))]
        public virtual int? ServiceID { get; set; }
        #endregion

        #region SODetID
        public abstract class sODetID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSAppointmentDet.sODetID))]
        [PXUIField(DisplayName = "Line Ref.")]
        [FSSelectorSODetIDService]
        public virtual int? SODetID { get; set; }
        #endregion

        #region GeoZoneID
        public abstract class geoZoneID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSGeoZonePostalCode.geoZoneID))]
        [PXUIField(DisplayName = "Geographical Zone ID")]
        [PXSelector(typeof(Search<FSGeoZone.geoZoneID>), SubstituteKey = typeof(FSGeoZone.geoZoneCD), DescriptionField = typeof(FSGeoZone.descr))]
        public virtual int? GeoZoneID { get; set; }
        #endregion

        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.branchLocationID))]
        [PXUIField(DisplayName = "Branch Location ID")]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID>), SubstituteKey = typeof(FSBranchLocation.branchLocationCD), DescriptionField = typeof(FSBranchLocation.descr))]
        public virtual int? BranchLocationID { get; set; }
        #endregion

        #region RoomID
        public abstract class roomID : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true, BqlField = typeof(FSServiceOrder.roomID))]
        [PXUIField(DisplayName = "Room")]
        [PXSelector(typeof(Search<FSRoom.roomID>), SubstituteKey = typeof(FSRoom.roomID), DescriptionField = typeof(FSRoom.descr))]
        public virtual string RoomID { get; set; }
        #endregion

        #region EmployeeID
        public abstract class employeeID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSAppointmentEmployee.employeeID))]
        [PXUIField(DisplayName = "Staff Member ID", TabOrder = 0)]
        [PXSelector(typeof(Search<BAccountStaffMember.bAccountID>), SubstituteKey = typeof(BAccountStaffMember.acctCD), DescriptionField = typeof(BAccountStaffMember.acctName))]
        public virtual int? EmployeeID { get; set; }
        #endregion

        #region BillCustomerID
        public new abstract class billCustomerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.billCustomerID))]
        [PXUIField(DisplayName = "Billing Customer ID")]
        [FSSelectorCustomer]
        public override int? BillCustomerID { get; set; }
        #endregion

        #region BillLocationID
        public abstract class billLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>>),
                    BqlField = typeof(FSServiceOrder.billLocationID),
                    DescriptionField = typeof(Location.descr), DisplayName = "Billing Location", DirtyRead = true)]
        public virtual int? BillLocationID { get; set; }
        #endregion

        #region CustomerBillingCycleID
        public abstract class customerBillingCycleID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSCustomerBillingSetup.billingCycleID))]
        [PXDefault]
        [PXSelector(typeof(Search<FSBillingCycle.billingCycleID>),
                    SubstituteKey = typeof(FSBillingCycle.billingCycleCD),
                    DescriptionField = typeof(FSBillingCycle.descr))]
        [PXUIField(DisplayName = "Billing Cycle ID", Enabled = false)]
        public virtual int? CustomerBillingCycleID { get; set; }
        #endregion

        #region CustomerBillingSrvOrdType
        public abstract class customerBillingSrvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(BqlField = typeof(FSCustomerBillingSetup.srvOrdType))]
        [PXDefault]
        [FSSelectorSrvOrdTypeNOTQuote]
        [PXUIField(DisplayName = "Service Order Type")]
        public virtual string CustomerBillingSrvOrdType { get; set; }
        #endregion

        #region FSxCustomerBillingCycleID
        public abstract class fsxCustomerBillingCycleID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSxCustomer.billingCycleID))]
        [PXSelector(typeof(FSBillingCycle.billingCycleID), SubstituteKey = typeof(FSBillingCycle.billingCycleCD), DescriptionField = typeof(FSBillingCycle.descr))]
        [PXUIField(DisplayName = "Billing Cycle ID", Enabled = false)]
        public virtual int? FSxCustomerBillingCycleID { get; set; }
        #endregion

        #region SLAETA
        public abstract class sLAETA : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(BqlField = typeof(FSServiceOrder.sLAETA))]
        [PXUIField(DisplayName = "Deadline - SLA", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? SLAETA { get; set; }
        #endregion

        #region AddressLine1
        public abstract class addressLine1 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.addressLine1))]
        [PXUIField(DisplayName = "Address Line 1")]
        public virtual string AddressLine1 { get; set; }
        #endregion

        #region AddressLine2
        public abstract class addressLine2 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.addressLine2))]
        [PXUIField(DisplayName = "Address Line 2")]
        public virtual string AddressLine2 { get; set; }
        #endregion

        #region AddressLine3
        public abstract class addressLine3 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.addressLine3))]
        [PXUIField(DisplayName = "Address Line 3", Visible = false, Enabled = false)]
        public virtual string AddressLine3 { get; set; }
        #endregion

        #region PostalCode
        public abstract class postalCode : PX.Data.IBqlField
        {
        }

        [PXDBString(20, BqlField = typeof(FSServiceOrder.postalCode))]
        [PXUIField(DisplayName = "Postal code")]
        [PXZipValidation(typeof(Country.zipCodeRegexp), typeof(Country.zipCodeMask), typeof(Address.countryID))]
        [PXDynamicMask(typeof(Search<Country.zipCodeMask, Where<Country.countryID, Equal<Current<FSServiceOrder.countryID>>>>))]
        [PXFormula(typeof(Default<FSServiceOrder.countryID>))]
        public virtual string PostalCode { get; set; }
        #endregion

        #region City
        public abstract class city : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.city))]
        [PXUIField(DisplayName = "City")]
        public virtual string City { get; set; }
        #endregion
    }
}