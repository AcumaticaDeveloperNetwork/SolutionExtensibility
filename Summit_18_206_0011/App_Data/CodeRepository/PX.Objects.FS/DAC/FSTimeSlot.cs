using System;
using PX.Data;
using PX.SM;

namespace PX.Objects.FS
{
	[System.SerializableAttribute]
	public class FSTimeSlot : PX.Data.IBqlTable
	{
        #region TimeSlotID
        public abstract class timeSlotID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? TimeSlotID { get; set; }

        #endregion
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Branch ID")]
        [PXSelector(typeof(Search<Branch.branchID>), SubstituteKey = typeof(Branch.branchCD))]
        public virtual int? BranchID { get; set; }
        #endregion
		#region BranchLocationID
		public abstract class branchLocationID : PX.Data.IBqlField
		{
		}

        [PXDBInt]
        [PXUIField(DisplayName = "Branch Location ID")]
        public virtual int? BranchLocationID { get; set; }
		#endregion
		#region EmployeeID
		public abstract class employeeID : PX.Data.IBqlField
		{
		}

        [PXDBInt]
        [PXUIField(DisplayName = "Employee ID")]
        public virtual int? EmployeeID { get; set; }
		#endregion
		#region TimeStart
		public abstract class timeStart : PX.Data.IBqlField
		{
		}

        protected DateTime? _TimeStart;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
        [PXUIField(DisplayName = "Time Start")]
        public virtual DateTime? TimeStart
        {
            get
            {
                return this._TimeStart;
            }

            set
            {
                this.TimeStartUTC = value;
                this._TimeStart = value;
            }
        }
        #endregion
        #region TimeEnd
        public abstract class timeEnd : PX.Data.IBqlField
		{
		}

        protected DateTime? _TimeEnd;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
        [PXUIField(DisplayName = "Time End")]
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
        #region ScheduleID
        public abstract class scheduleID : PX.Data.IBqlField
		{
		}

        [PXDBInt]
        [PXUIField(DisplayName = "Schedule ID")]
        public virtual int? ScheduleID { get; set; }
		#endregion
        #region RecordCount
		public abstract class recordCount : PX.Data.IBqlField
		{
		}

        [PXDBInt]
        [PXUIField(DisplayName = "Record Count")]
        public virtual int? RecordCount { get; set; }
		#endregion
        #region ScheduleType
        public abstract class scheduleType : ListField_ScheduleType
        {
        }

        [PXDBString(1)]
        [scheduleType.ListAtrribute]
        public virtual string ScheduleType { get; set; }
        #endregion
        #region GenerationID
        public abstract class generationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Generation ID")]
        public virtual int? GenerationID { get; set; }
        #endregion
        #region TimeDiff
        public abstract class timeDiff : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "Time Difference", Enabled = false)]
        public virtual decimal? TimeDiff { get; set; }
        #endregion
        #region SlotLevel
        public abstract class slotLevel : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(ID.EmployeeTimeSlotLevel.BASE)]
        [PXUIField(DisplayName = "Slot Level")]
        public virtual int? SlotLevel { get; set; }
        #endregion
        #region CalendarHelpers
        #region CustomID
        public abstract class customID : PX.Data.IBqlField
        {
        }

        [PXString]        
        public virtual string CustomID { get; set; }
        #endregion
        #region WrkEmployeeScheduleID
        public abstract class wrkEmployeeScheduleID : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string WrkEmployeeScheduleID { get; set; }
        #endregion
        #region BranchLocationDesc
        public abstract class branchLocationDesc : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string BranchLocationDesc { get; set; }
        #endregion
        #region BranchLocationCD
        public abstract class branchLocationCD : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string BranchLocationCD { get; set; }
        #endregion
        #region CustomDateTimeStart
        public abstract class customDateTimeStart : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string CustomDateTimeStart 
        {
            get 
            { 
                //Value cannot be calculated with PXFormula attribute
                if (this.TimeStart != null)
                {
                    return this.TimeStart.ToString(); 
                }

                return string.Empty;
            }
        }
        #endregion
        #region CustomDateTimeEnd
        public abstract class customDateTimeEnd : PX.Data.IBqlField
        {
        }

        [PXString]
        public virtual string CustomDateTimeEnd 
        { 
            get 
            {
                //Value cannot be calculated with PXFormula attribute
                if (this.TimeEnd != null)
                {
                    return this.TimeEnd.ToString();
                }

                return string.Empty;
            } 
        }
        #endregion
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        [PXUIField(DisplayName = "CreatedByID")]
        public virtual Guid? CreatedByID { get; set; }

        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "CreatedByScreenID")]
        public virtual string CreatedByScreenID { get; set; }

        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = "CreatedDateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }

        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "LastModifiedByID")]
        public virtual Guid? LastModifiedByID { get; set; }

        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "LastModifiedByScreenID")]
        public virtual string LastModifiedByScreenID { get; set; }

        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = "LastModifiedDateTime")]
        public virtual DateTime? LastModifiedDateTime { get; set; }

        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        [PXUIField(DisplayName = "tstamp")]
        public virtual byte[] tstamp { get; set; }

        #endregion

        #region UTC Fields

        #region TimeStartUTC
        public abstract class timeStartUTC : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
        [PXUIField(DisplayName = "Time Start")]
        public virtual DateTime? TimeStartUTC { get; set; }
        #endregion
        #region TimeEndUTC
        public abstract class timeEndUTC : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
        [PXUIField(DisplayName = "Time End")]
        public virtual DateTime? TimeEndUTC { get; set; }
        #endregion
        #endregion
    }
}