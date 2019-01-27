using System;
using System.Globalization;
using PX.Data;

namespace PX.Objects.FS
{	
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.WEEKCODE_DATE)]
    [PXPrimaryGraph(typeof(CalendarWeekCodeMaint))]
     public class FSWeekCodeDate : PX.Data.IBqlTable
	{
		#region WeekCodeDate
        public abstract class weekCodeDate : PX.Data.IBqlField
		{
		}

        [PXDBDate(IsKey = true)]
		[PXUIField(DisplayName = "Date")]
        public virtual DateTime? WeekCodeDate { get; set; }
		#endregion
        #region WeekCode
        public abstract class weekCode : PX.Data.IBqlField
		{
		}

        [PXDBString(4, IsUnicode = true, InputMask = ">CCCC")]
        [PXDefault]
        [PXUIField(DisplayName = "Week Code", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string WeekCode { get; set; }
		#endregion
        #region WeekCodeP1
        public abstract class weekCodeP1 : PX.Data.IBqlField
        {
        }

        [PXDBString(1, IsUnicode = true, InputMask = ">C")]
        [PXUIField(DisplayName = "Week Code P1", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string WeekCodeP1 { get; set; }
        #endregion
        #region WeekCodeP2
        public abstract class weekCodeP2 : PX.Data.IBqlField
        {
        }

        [PXDBString(1, IsUnicode = true, InputMask = ">C")]
        [PXUIField(DisplayName = "Week Code P2", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string WeekCodeP2 { get; set; }
        #endregion
        #region WeekCodeP3
        public abstract class weekCodeP3 : PX.Data.IBqlField
        {
        }

        [PXDBString(1, IsUnicode = true, InputMask = ">C")]
        [PXUIField(DisplayName = "Week Code P3", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string WeekCodeP3 { get; set; }
        #endregion
        #region WeekCodeP3
        public abstract class weekCodeP4 : PX.Data.IBqlField
        {
        }

        [PXDBString(1, IsUnicode = true, InputMask = ">C")]
        [PXUIField(DisplayName = "Week Code P4", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string WeekCodeP4 { get; set; }
        #endregion
        #region BeginDateOfWeek
        public abstract class beginDateOfWeek : PX.Data.IBqlField
		{
		}

        [PXDBDate]
		[PXUIField(DisplayName = "Start Date of Week")]
        public virtual DateTime? BeginDateOfWeek { get; set; }
		#endregion
        #region EndDateOfWeek
        public abstract class endDateOfWeek : PX.Data.IBqlField
		{
		}

        [PXDBDate]
		[PXUIField(DisplayName = "End Date of Week")]
        public virtual DateTime? EndDateOfWeek { get; set; }
		#endregion
        #region DayOfWeek
        public abstract class dayOfWeek : ListField_WeekDaysNumber
        {
        }

        [PXDBInt]
        [dayOfWeek.ListAtrribute]
        [PXUIField(DisplayName = "Day of Week")]
        public virtual int? DayOfWeek { get; set; }
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
		public virtual byte[] tstamp { get; set; }
		#endregion

        #region MemoryHelper

        #region Mem_DayOfWeek
        public abstract class mem_DayOfWeek : PX.Data.IBqlField
        {
        }

        [PXString]
        [PXUIField(DisplayName = "Day of Week")]
        public virtual string Mem_DayOfWeek
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (WeekCodeDate != null && WeekCodeDate.Value != null)
                {
                    //Adding 1 day to reuse getDayOfWeekByID function.
                    return SharedFunctions.getDayOfWeekByID((int)WeekCodeDate.Value.DayOfWeek).ToString(); 
                }

                 return null;
            }
        }
        #endregion
        #region Mem_WeekOfYear
        public abstract class mem_WeekOfYear : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Week of Year")]
        public virtual int? Mem_WeekOfYear
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (WeekCodeDate != null && WeekCodeDate.Value != null)
                {
                    DateTime auxDateTime = WeekCodeDate.Value;
                    /* This presumes that weeks start with Monday.
                     Week 1 is the 1st week of the year with a Thursday in it.
                     If its Monday, Tuesday or Wednesday, then it'll 
                     be the same week# as whatever Thursday, Friday or Saturday are,
                     and we always get those right */
                    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(auxDateTime);
                    if (day >= System.DayOfWeek.Monday && day <= System.DayOfWeek.Wednesday)
                    {
                        auxDateTime = auxDateTime.AddDays(3);
                    }

                    // Return the week of our adjusted day
                    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(auxDateTime, CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);
                }

               return null;
            }
        }
        #endregion

        #endregion
    }
}