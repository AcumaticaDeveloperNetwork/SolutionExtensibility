using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
﻿
namespace PX.Objects.FS
{
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.ROUTE_DOCUMENT)]
    [PXPrimaryGraph(typeof(RouteDocumentMaint))]
	public class FSRouteDocument : PX.Data.IBqlTable
	{
		#region RefNbr
        /* Cache_Attached RouteClosingMaint */
        public abstract class refNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Route Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [AutoNumber(typeof(Search<FSRouteSetup.routeNumberingID>), typeof(AccessInfo.businessDate))]
        [PXSelector(typeof(Search3<FSRouteDocument.refNbr, OrderBy<Desc<FSRouteDocument.refNbr>>>))]
        [PX.Data.EP.PXFieldDescription]
        public virtual string RefNbr { get; set; }
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
        #region RouteDocumentID
        public abstract class routeDocumentID : PX.Data.IBqlField
		{
		}
		[PXDBIdentity]
		[PXUIField(Enabled = false)]
        public virtual int? RouteDocumentID { get; set; }
		#endregion
		#region Date
		public abstract class date : PX.Data.IBqlField
		{
		}
		[PXDBDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? Date { get; set; }
		#endregion
        #region RouteID
        public abstract class routeID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXDefault]
        [PXUIField(DisplayName = "Route", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorRouteID]
        [PX.Data.EP.PXFieldDescription]
        public virtual int? RouteID { get; set; }
        #endregion
		#region DriverID
		public abstract class driverID : PX.Data.IBqlField
		{
		}
        [PXDBInt]
        [PXUIField(DisplayName = "Driver", Visibility = PXUIVisibility.SelectorVisible)]
        [PXRestrictor(typeof(Where<EPEmployeeFSRouteEmployee.status, IsNull,
                    Or<EPEmployeeFSRouteEmployee.status, NotEqual<BAccount.status.inactive>>>),
                    TX.Messages.EMPLOYEE_IS_IN_STATUS, typeof(BAccountSelectorBase.status))]
        [FSSelector_Driver_RouteDocumentRouteID]
        public virtual int? DriverID { get; set; }
		#endregion
        #region AdditionalDriverID
        public abstract class additionalDriverID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Additional Driver", Visibility = PXUIVisibility.SelectorVisible)]
        [PXRestrictor(typeof(Where<EPEmployeeFSRouteEmployee.status, IsNull,
                    Or<EPEmployeeFSRouteEmployee.status, NotEqual<BAccount.status.inactive>>>),
                    TX.Messages.EMPLOYEE_IS_IN_STATUS, typeof(BAccountSelectorBase.status))]
        [FSSelector_Driver_RouteDocumentRouteID]
        public virtual int? AdditionalDriverID { get; set; }
        #endregion
        #region RouteStatsUpdated
        public abstract class routeStatsUpdated : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(typeof(Search<FSRouteSetup.autoCalculateRouteStats>))]
        [PXUIField(DisplayName = "Route Stats Updated")]
        public virtual bool? RouteStatsUpdated { get; set; }
        #endregion 
		#region Status
        public abstract class status : ListField_Status_Route
		{
		}
		[PXDBString(1, IsFixed = true)]
        [PXDefault(ID.Status_Route.OPEN)]
        [status.ListAtrribute]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual string Status { get; set; }
		#endregion
		#region TimeBegin
		public abstract class timeBegin : PX.Data.IBqlField
		{
		}
        protected DateTime? _TimeBegin;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Start Time")]
        public virtual DateTime? TimeBegin
        {
            get
            {
                return this._TimeBegin;
            }

            set
            {
                this.TimeBeginUTC = value;
                this._TimeBegin = value;
            }
        }
        #endregion
        #region TimeEnd
        public abstract class timeEnd : PX.Data.IBqlField
		{
		}
        protected DateTime? _TimeEnd;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time")]
        [PXUIField(DisplayName = "End Time", Enabled = false, Visible = false)]
        public virtual DateTime? TimeEnd
        {
            get
            {
                return this._TimeEnd;
            }

            set
            {
                this.TimeEndUTC = value;
                this._TimeEnd = value;
            }
        }
        #endregion
        #region TripNbr
        public abstract class tripNbr : PX.Data.IBqlField
        {
        }
        [PXDefault(1)]
        [PXFormula(typeof(Default<FSRouteDocument.date>))]
        [PXFormula(typeof(Default<FSRouteDocument.routeID>))]
        [PXDBInt(MinValue = 0, MaxValue = Int32.MaxValue)]
        [PXUIField(DisplayName = "Trip Nbr.")]
        public virtual int? TripNbr { get; set; }
        #endregion
        #region ActualStartTime
        public abstract class actualStartTime : PX.Data.IBqlField
        {
        }
        protected DateTime? _ActualStartTime;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual Start Time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Actual Start Time")]
        public virtual DateTime? ActualStartTime
        {
            get
            {
                return this._ActualStartTime;
            }

            set
            {
                this.ActualStartTimeUTC = value;
                this._ActualStartTime = value;
            }
        }
        #endregion
        #region ActualEndTime
        public abstract class actualEndTime : PX.Data.IBqlField
        {
        }
        protected DateTime? _ActualEndTime;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual End Time")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Actual End Time")]
        public virtual DateTime? ActualEndTime
        {
            get
            {
                return this._ActualEndTime;
            }

            set
            {
                this.ActualEndTimeUTC = value;
                this._ActualEndTime = value;
            }
        }
        #endregion
		#region TotalNumAppointments
		public abstract class totalNumAppointments : PX.Data.IBqlField
		{
		}
		[PXDBInt]
        [PXUIField(DisplayName = "Number of Appointments", Enabled = false)]
        public virtual int? TotalNumAppointments { get; set; }
		#endregion
		#region TotalDuration
		public abstract class totalDuration : PX.Data.IBqlField
		{
		}
        [PXUIField(DisplayName = "Total Driving Duration", Enabled = false)]
        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        public virtual int? TotalDuration { get; set; }
		#endregion
        #region TotalDistance
        public abstract class totalDistance : PX.Data.IBqlField
        {
        }
        [PXDBDecimal]
        public virtual decimal? TotalDistance { get; set; }
        #endregion
        #region TotalDistanceFriendly
        public abstract class totalDistanceFriendly : PX.Data.IBqlField
        {
        }
        [PXDBString]
        [PXUIField(DisplayName = "Total Distance", Enabled = false)]
        public virtual string TotalDistanceFriendly { get; set; }
        #endregion
        #region TotalServices
        public abstract class totalServices : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXUIField(DisplayName = "Total Services", Enabled = false)]
        public virtual int? TotalServices { get; set; }
		#endregion
		#region TotalServicesDuration
		public abstract class totalServicesDuration : PX.Data.IBqlField
		{
		}
        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXUIField(DisplayName = "Total Services Duration", Enabled = false)]
        public virtual int? TotalServicesDuration { get; set; }
		#endregion
		#region TotalTravelTime
		public abstract class totalTravelTime : PX.Data.IBqlField
		{
		}
        [PXUIField(DisplayName = "Total Route Duration", Enabled = false)]
        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        public virtual int? TotalTravelTime { get; set; }
		#endregion
		#region VehicleID
		public abstract class vehicleID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
        [PXUIField(DisplayName = "Vehicle", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorVehicle]
        [PXRestrictor(typeof(Where<FSVehicle.status, Equal<EPEquipmentStatus.EquipmentStatusActive>>),
                TX.Messages.VEHICLE_IS_INSTATUS, typeof(FSVehicle.status))]
        public virtual int? VehicleID { get; set; }
		#endregion
        #region AditionalVehicleID1
        public abstract class additionalVehicleID1 : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Additional Vehicle 1", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorVehicle]
        [PXRestrictor(typeof(Where<FSVehicle.status, Equal<EPEquipmentStatus.EquipmentStatusActive>>),
                TX.Messages.VEHICLE_IS_INSTATUS, typeof(FSVehicle.status))]
        public virtual int? AdditionalVehicleID1 { get; set; }
        #endregion
        #region AditionalVehicleID2
        public abstract class additionalVehicleID2 : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Additional Vehicle 2", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorVehicle]
        [PXRestrictor(typeof(Where<FSVehicle.status, Equal<EPEquipmentStatus.EquipmentStatusActive>>),
                TX.Messages.VEHICLE_IS_INSTATUS, typeof(FSVehicle.status))]
        public virtual int? AdditionalVehicleID2 { get; set; }
        #endregion
        #region GPSLatitudeStart
        public abstract class gPSLatitudeStart : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXUIField(DisplayName = "Latitude", Enabled = false)]
        public virtual decimal? GPSLatitudeStart { get; set; }
        #endregion
        #region GPSLongitudeStart
        public abstract class gPSLongitudeStart : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXUIField(DisplayName = "Longitude", Enabled = false)]
        public virtual decimal? GPSLongitudeStart { get; set; }
        #endregion
        #region GPSLatitudeComplete
        public abstract class gPSLatitudeComplete : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXUIField(DisplayName = "Latitude", Enabled = false)]
        public virtual decimal? GPSLatitudeComplete { get; set; }
        #endregion
        #region GPSLongitudeComplete
        public abstract class gPSLongitudeComplete : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXUIField(DisplayName = "Longitude", Enabled = false)]
        public virtual decimal? GPSLongitudeComplete { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }
        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0], ShowInReferenceSelector = true)]
        public virtual Guid? NoteID { get; set; }
        #endregion
        #region GeneratedBySystem
        public abstract class generatedBySystem : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Generated by System", Enabled = false)]
        public virtual bool? GeneratedBySystem { get; set; }
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
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		[PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
		#endregion

        #region Additional Info Tab

        #region Miles
        public abstract class miles : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 999)]
        [PXUIField(DisplayName = "Miles")]
        public virtual int? Miles { get; set; }
        #endregion
        #region Weight
        public abstract class weight : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 99999)]
        [PXUIField(DisplayName = "Weight")]
        public virtual int? Weight { get; set; }
        #endregion
        #region FuelQty
        public abstract class fuelQty : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 999)]
        [PXUIField(DisplayName = "Fuel Qty.")]
        public virtual int? FuelQty { get; set; }
        #endregion
        #region FuelType
        public abstract class fuelType : ListField_FuelType_Equipment
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ID.FuelType_Equipment.REGULAR_UNLEADED)]
        [PXUIField(DisplayName = "Fuel Type", Visibility = PXUIVisibility.SelectorVisible)]
        [ListField_FuelType_Equipment.ListAtrribute]
        public virtual string FuelType { get; set; }
        #endregion
        #region Oil
        public abstract class oil : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 99)]
        [PXUIField(DisplayName = "Oil")]
        public virtual int? Oil { get; set; }
        #endregion
        #region AntiFreeze
        public abstract class antiFreeze : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 99)]
        [PXUIField(DisplayName = "Anti-freeze")]
        public virtual int? AntiFreeze { get; set; }
        #endregion
        #region DEF
        public abstract class dEF : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 99)]
        [PXUIField(DisplayName = "DEF")]
        public virtual int? DEF { get; set; }
        #endregion
        #region Propane
        public abstract class propane : PX.Data.IBqlField
        {
        }
        [PXDBInt(MinValue = 0, MaxValue = 99)]
        [PXUIField(DisplayName = "Propane")]
        public virtual int? Propane { get; set; }
        #endregion

        #endregion
        #region Attributes
        /// <summary>
        /// A service field, which is necessary for the <see cref="CSAnswers">dynamically 
        /// added attributes</see> defined at the <see cref="FSRoute">Route 
        /// screen</see> level to function correctly.
        /// </summary>
        [CRAttributesField(typeof(FSRouteDocument.routeCD))]
        public virtual string[] Attributes { get; set; }
        #endregion

        //This dummy field is used to prevent access to the Route Document Detail page without having set the Service Management Preferences
        #region RouteNumberingID
        public abstract class routeNumberingID : PX.Data.IBqlField
        {
        }
        [PXString(10)]
        [PXDefault(typeof(FSRouteSetup.routeNumberingID), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Route Numbering ID")]
        public virtual string RouteNumberingID { get; set; }
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

        #region Mem_ActualDuration
        public abstract class mem_ActualDuration : PX.Data.IBqlField
        {
        }
        
        [PXUIField(DisplayName = "Actual Duration", Enabled = false)]
        [PXTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        [PXFormula(typeof(DateDiff<FSRouteDocument.actualStartTime, FSRouteDocument.actualEndTime, DateDiff.minute>))]
        public virtual int? Mem_ActualDuration { get; set; }
        #endregion

        #region Mem_BusinessDateTime
        public abstract class mem_BusinessDateTime : PX.Data.IBqlField
        {
        }

        [PXDateAndTime]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXFormula(typeof(PX.Objects.CS.TimeZoneNow))]
        public virtual DateTime? Mem_BusinessDateTime { get; set; }
        #endregion

        #region GPSLatitudeLongitude    
        public abstract class gpsLatitudeLongitude : PX.Data.IBqlField
        {
        }

        [PXString(255)]
        [PXUIField(DisplayName = "GPS Latitude Longitude", Enabled = false)]
        public virtual string GPSLatitudeLongitude { get; set; }
        #endregion

        #region MustRecalculateStats
        [PXBool]
        public virtual bool? MustRecalculateStats { get; set; }
        #endregion

        #region RouteCD
        // Needed for attributes
        public abstract class routeCD : PX.Data.IBqlField
        {
        }

        [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXUIField(Visible = false)]
        [PXDefault(typeof(Search<FSRoute.routeCD, Where<FSRoute.routeID, Equal<Current<FSRouteDocument.routeID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual string RouteCD { get; set; }
        #endregion
        #endregion

        #region UTC Fields
        #region TimeBeginUTC
        public abstract class timeBeginUTC : PX.Data.IBqlField
        {
        }
        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time")]
        [PXUIField(DisplayName = "Start Time")]
        public virtual DateTime? TimeBeginUTC { get; set; }
        #endregion
        #region TimeEndUTC
        public abstract class timeEndUTC : PX.Data.IBqlField
        {
        }
        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time")]
        [PXUIField(DisplayName = "End Time", Enabled = false, Visible = false)]
        public virtual DateTime? TimeEndUTC { get; set; }
        #endregion
        #region ActualStartTimeUTC
        public abstract class actualStartTimeUTC : PX.Data.IBqlField
        {
        }
        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual Start Time")]
        [PXUIField(DisplayName = "Actual Start Time")]
        public virtual DateTime? ActualStartTimeUTC { get; set; }
        #endregion
        #region ActualEndTimeUTC
        public abstract class actualEndTimeUTC : PX.Data.IBqlField
        {
        }
        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual End Time")]
        [PXUIField(DisplayName = "Actual End Time")]
        public virtual DateTime? ActualEndTimeUTC { get; set; }
        #endregion
        #endregion
    }
	
}