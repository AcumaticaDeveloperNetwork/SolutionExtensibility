using System;
using PX.Data;
using PX.Objects.GL;

namespace PX.Objects.FS
{
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.ROUTE)]
    [PXPrimaryGraph(typeof(RouteMaint))]
	public class FSRoute : PX.Data.IBqlTable
	{
		#region RouteID
		public abstract class routeID : PX.Data.IBqlField
		{
		}		

		[PXDBIdentity]
		[PXUIField(Enabled = false)]
		public virtual int? RouteID { get; set; }
		#endregion
		#region RouteCD
		public abstract class routeCD : PX.Data.IBqlField
		{
		}

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXSelector(typeof(FSRoute.routeCD))]
        [PXDefault]
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "Route ID", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string RouteCD { get; set; }
		#endregion
        #region ActiveOnMonday
        public abstract class activeOnMonday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Monday")]
        public virtual bool? ActiveOnMonday { get; set; }
        #endregion
        #region ActiveOnTuesday
        public abstract class activeOnTuesday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Tuesday")]
        public virtual bool? ActiveOnTuesday { get; set; }
        #endregion
        #region ActiveOnWednesday
        public abstract class activeOnWednesday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Wednesday")]
        public virtual bool? ActiveOnWednesday { get; set; }
        #endregion
        #region ActiveOnThursday
        public abstract class activeOnThursday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Thursday ")]
        public virtual bool? ActiveOnThursday { get; set; }
        #endregion
        #region ActiveOnFriday
        public abstract class activeOnFriday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Friday")]
        public virtual bool? ActiveOnFriday { get; set; }
        #endregion
        #region ActiveOnSaturday
        public abstract class activeOnSaturday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Saturday")]
        public virtual bool? ActiveOnSaturday { get; set; }
        #endregion
        #region ActiveOnSunday
        public abstract class activeOnSunday : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Sunday")]
        public virtual bool? ActiveOnSunday { get; set; }
        #endregion
        #region BeginTimeOnMonday
        public abstract class beginTimeOnMonday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnMonday { get; set; }
        #endregion
        #region BeginTimeOnTuesday
        public abstract class beginTimeOnTuesday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnTuesday { get; set; }
        #endregion
        #region BeginTimeOnWednesday
        public abstract class beginTimeOnWednesday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnWednesday { get; set; }
        #endregion
        #region BeginTimeOnThursday
        public abstract class beginTimeOnThursday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnThursday { get; set; }
        #endregion
        #region BeginTimeOnFriday
        public abstract class beginTimeOnFriday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnFriday { get; set; }
        #endregion
        #region BeginTimeOnSaturday
        public abstract class beginTimeOnSaturday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnSaturday { get; set; }
        #endregion
        #region BeginTimeOnSunday
        public abstract class beginTimeOnSunday : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual DateTime? BeginTimeOnSunday { get; set; }
        #endregion
        #region NbrTripOnMonday
        public abstract class nbrTripOnMonday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnMonday { get; set; }
        #endregion
        #region NbrTripOnTuesday
        public abstract class nbrTripOnTuesday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnTuesday { get; set; }
        #endregion
        #region NbrTripOnWednesday
        public abstract class nbrTripOnWednesday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnWednesday { get; set; }
        #endregion
        #region NbrTripOnThursday
        public abstract class nbrTripOnThursday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnThursday { get; set; }
        #endregion
        #region NbrTripOnFriday
        public abstract class nbrTripOnFriday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnFriday { get; set; }
        #endregion
        #region NbrTripOnSaturday
        public abstract class nbrTripOnSaturday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnSaturday { get; set; }
        #endregion
        #region NbrTripOnSunday
        public abstract class nbrTripOnSunday : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
        public virtual int? NbrTripOnSunday { get; set; }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
		{
		}

		[PXDBString(60, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string Descr { get; set; }
		#endregion
        #region VehicleTypeID
        public abstract class vehicleTypeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Vehicle Type")]
        [PXSelector(typeof(FSVehicleType.vehicleTypeID), SubstituteKey = typeof(FSVehicleType.vehicleTypeCD), DescriptionField = typeof(FSVehicleType.descr))]
        public virtual int? VehicleTypeID { get; set; }
		#endregion
        #region OriginRouteID
        public abstract class originRouteID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Origin Route")]
        [PXSelector(typeof(FSRoute.routeID), SubstituteKey = typeof(FSRoute.routeCD), DescriptionField = typeof(FSRoute.descr))]
        public virtual int? OriginRouteID { get; set; }
        #endregion
        #region MaxAppointmentQty
        public abstract class maxAppointmentQty : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXDefault(1)]
        [PXUIField(DisplayName = "Max. Appointment Qty.")]
        public virtual int? MaxAppointmentQty { get; set; }
        #endregion
        #region NoAppointmentLimit
        public abstract class noAppointmentLimit : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "No Limit")]
        public virtual bool? NoAppointmentLimit { get; set; }
        #endregion
        #region RouteShort
        public abstract class routeShort : PX.Data.IBqlField
        {
        }

        [PXDBString(10, IsUnicode = true, IsFixed = true, InputMask = ">CCCCCCCCCC")]
        [PXUIField(DisplayName = "Route Short", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string RouteShort { get; set; }
        #endregion
        #region WeekCode
        public abstract class weekCode : PX.Data.IBqlField
        {
        }

        [PXDBString]
        [PXUIField(DisplayName = "Week Code(s) e.g.: 1, 2B, 1ACS")]
        public virtual string WeekCode { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0])]
        public virtual Guid? NoteID { get; set; }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region RouteBeginLocationType
        public abstract class routeBeginLocationType : PX.Data.IBqlField
        {
        }

        [PXDBString(4)]
        [PXDefault(TX.RouteLocationType.BRANCH_LOCATION)]
        [PXUIField(DisplayName = "Start Location Type")]
        public virtual string RouteBeginLocationType { get; set; }
        #endregion
        #region RouteEndLocationType
        public abstract class routeEndLocationType : PX.Data.IBqlField
        {
        }

        [PXDBString(4)]
        [PXDefault(TX.RouteLocationType.BRANCH_LOCATION)]
        [PXUIField(DisplayName = "End Location Type")]
        public virtual string RouteEndLocationType { get; set; }
        #endregion
        #region BeginBranchID
        public abstract class beginBranchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(AccessInfo.branchID))]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.acctName))]
        public virtual int? BeginBranchID { get; set; }
        #endregion
        #region BeginBranchLocationID
        public abstract class beginBranchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(
            Search<FSxUserPreferences.dfltBranchLocationID,
            Where<
                PX.SM.UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>,
                And<PX.SM.UserPreferences.defBranchID, Equal<Current<FSRoute.beginBranchID>>>>>))]
        [PXUIField(DisplayName = "Branch Location")]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID,
                            Where<FSBranchLocation.branchID, Equal<Current<FSRoute.beginBranchID>>>>),
                            SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
                            DescriptionField = typeof(FSBranchLocation.descr))]
        [PXFormula(typeof(Default<FSRoute.beginBranchID>))]
        public virtual int? BeginBranchLocationID { get; set; }
        #endregion
        #region EndBranchID
        public abstract class endBranchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(AccessInfo.branchID))]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.acctName))]
        public virtual int? EndBranchID { get; set; }
        #endregion
        #region EndBranchLocationID
        public abstract class endBranchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(
            Search<FSxUserPreferences.dfltBranchLocationID,
            Where<
                PX.SM.UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>,
                And<PX.SM.UserPreferences.defBranchID, Equal<Current<FSRoute.endBranchID>>>>>))]
        [PXUIField(DisplayName = "Branch Location")]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID,
                            Where<FSBranchLocation.branchID, Equal<Current<FSRoute.endBranchID>>>>),
                            SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
                            DescriptionField = typeof(FSBranchLocation.descr))]
        [PXFormula(typeof(Default<FSRoute.endBranchID>))]
        public virtual int? EndBranchLocationID { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}