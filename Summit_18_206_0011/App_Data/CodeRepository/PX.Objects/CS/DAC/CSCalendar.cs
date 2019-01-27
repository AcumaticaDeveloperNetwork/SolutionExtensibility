namespace PX.Objects.CS
{
	using System;
	using PX.Data;

	[System.SerializableAttribute()]
	[PXPrimaryGraph(
		new Type[] { typeof(CSCalendarMaint) },
		new Type[] { typeof(Select<CSCalendar, 
			Where<CSCalendar.calendarID, Equal<Current<CSCalendar.calendarID>>>>)
		})]
	[PXCacheName(Messages.Calendar)]
	public partial class CSCalendar : PX.Data.IBqlTable
	{
		#region CalendarID
		public abstract class calendarID : PX.Data.IBqlField
		{
		}
		protected String _CalendarID;
		[PXDBString(10, IsUnicode = true, IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Calendar ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<CSCalendar.calendarID>), DescriptionField = typeof(CSCalendar.description))]
		public virtual String CalendarID
		{
			get
			{
				return this._CalendarID;
			}
			set
			{
				this._CalendarID = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected String _Description;
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		#endregion
		#region SunWorkDay
		public abstract class sunWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _SunWorkDay;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Sunday")]
		public virtual Boolean? SunWorkDay
		{
			get
			{
				return this._SunWorkDay;
			}
			set
			{
				this._SunWorkDay = value;
			}
		}
		#endregion
		#region SunStartTime
		public abstract class sunStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _SunStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.sunWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.sunStartTime>>))]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXUIField(DisplayName = "Sunday Start Time", Required = false)]
		public virtual DateTime? SunStartTime
		{
			get
			{
				return this._SunStartTime;
			}
			set
			{
				this._SunStartTime = value;
			}
		}
		#endregion
		#region SunEndTime
		public abstract class sunEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _SunEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.sunWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.sunEndTime>>))]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXUIField(DisplayName = "Sunday End Time", Required = false)]
		public virtual DateTime? SunEndTime
		{
			get
			{
				return this._SunEndTime;
			}
			set
			{
				this._SunEndTime = value;
			}
		}
		#endregion
		#region SunGoodsMoves
		public abstract class sunGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _SunGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? SunGoodsMoves
		{
			get
			{
				return this._SunGoodsMoves;
			}
			set
			{
				this._SunGoodsMoves = value;
			}
		}
		#endregion
		#region MonWorkDay
		public abstract class monWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _MonWorkDay;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Monday")]
		public virtual Boolean? MonWorkDay
		{
			get
			{
				return this._MonWorkDay;
			}
			set
			{
				this._MonWorkDay = value;
			}
		}
		#endregion
		#region MonStartTime
		public abstract class monStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _MonStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.monWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.monStartTime>>))]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXUIField(DisplayName = "Monday Start Time")]
		public virtual DateTime? MonStartTime
		{
			get
			{
				return this._MonStartTime;
			}
			set
			{
				this._MonStartTime = value;
			}
		}
		#endregion
		#region MonEndTime
		public abstract class monEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _MonEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.monWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.monEndTime>>))]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXUIField(DisplayName = "Monday End Time")]
		public virtual DateTime? MonEndTime
		{
			get
			{
				return this._MonEndTime;
			}
			set
			{
				this._MonEndTime = value;
			}
		}
		#endregion
		#region MonGoodsMoves
		public abstract class monGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _MonGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? MonGoodsMoves
		{
			get
			{
				return this._MonGoodsMoves;
			}
			set
			{
				this._MonGoodsMoves = value;
			}
		}
		#endregion
		#region TueWorkDay
		public abstract class tueWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _TueWorkDay;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Tuesday")]
		public virtual Boolean? TueWorkDay
		{
			get
			{
				return this._TueWorkDay;
			}
			set
			{
				this._TueWorkDay = value;
			}
		}
		#endregion
		#region TueStartTime
		public abstract class tueStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _TueStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.tueWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.tueStartTime>>))]
		[PXUIField(DisplayName = "Tuesday Start Time")]
		public virtual DateTime? TueStartTime
		{
			get
			{
				return this._TueStartTime;
			}
			set
			{
				this._TueStartTime = value;
			}
		}
		#endregion
		#region TueEndTime
		public abstract class tueEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _TueEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.tueWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.tueEndTime>>))]
		[PXUIField(DisplayName = "Tuesday End Time")]
		public virtual DateTime? TueEndTime
		{
			get
			{
				return this._TueEndTime;
			}
			set
			{
				this._TueEndTime = value;
			}
		}
		#endregion
		#region TueGoodsMoves
		public abstract class tueGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _TueGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? TueGoodsMoves
		{
			get
			{
				return this._TueGoodsMoves;
			}
			set
			{
				this._TueGoodsMoves = value;
			}
		}
		#endregion
		#region WedWorkDay
		public abstract class wedWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _WedWorkDay;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Wednesday")]
		public virtual Boolean? WedWorkDay
		{
			get
			{
				return this._WedWorkDay;
			}
			set
			{
				this._WedWorkDay = value;
			}
		}
		#endregion
		#region WedStartTime
		public abstract class wedStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _WedStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.wedWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.wedStartTime>>))]
		[PXUIField(DisplayName = "Wednesday Start Time")]
		public virtual DateTime? WedStartTime
		{
			get
			{
				return this._WedStartTime;
			}
			set
			{
				this._WedStartTime = value;
			}
		}
		#endregion
		#region WedEndTime
		public abstract class wedEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _WedEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.wedWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.wedEndTime>>))]
		[PXUIField(DisplayName = "Wednesday End Time")]
		public virtual DateTime? WedEndTime
		{
			get
			{
				return this._WedEndTime;
			}
			set
			{
				this._WedEndTime = value;
			}
		}
		#endregion
		#region WedGoodsMoves
		public abstract class wedGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _WedGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? WedGoodsMoves
		{
			get
			{
				return this._WedGoodsMoves;
			}
			set
			{
				this._WedGoodsMoves = value;
			}
		}
		#endregion
		#region ThuWorkDay
		public abstract class thuWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _ThuWorkDay;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Thursday")]
		public virtual Boolean? ThuWorkDay
		{
			get
			{
				return this._ThuWorkDay;
			}
			set
			{
				this._ThuWorkDay = value;
			}
		}
		#endregion
		#region ThuStartTime
		public abstract class thuStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _ThuStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.thuWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.thuStartTime>>))]
		[PXUIField(DisplayName = "Thursday Start Time")]
		public virtual DateTime? ThuStartTime
		{
			get
			{
				return this._ThuStartTime;
			}
			set
			{
				this._ThuStartTime = value;
			}
		}
		#endregion
		#region ThuEndTime
		public abstract class thuEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _ThuEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.thuWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.thuEndTime>>))]
		[PXUIField(DisplayName = "Thursday End Time")]
		public virtual DateTime? ThuEndTime
		{
			get
			{
				return this._ThuEndTime;
			}
			set
			{
				this._ThuEndTime = value;
			}
		}
		#endregion
		#region ThuGoodsMoves
		public abstract class thuGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _ThuGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? ThuGoodsMoves
		{
			get
			{
				return this._ThuGoodsMoves;
			}
			set
			{
				this._ThuGoodsMoves = value;
			}
		}
		#endregion
		#region FriWorkDay
		public abstract class friWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _FriWorkDay;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Friday")]
		public virtual Boolean? FriWorkDay
		{
			get
			{
				return this._FriWorkDay;
			}
			set
			{
				this._FriWorkDay = value;
			}
		}
		#endregion
		#region FriStartTime
		public abstract class friStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _FriStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.friWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.friStartTime>>))]
		[PXUIField(DisplayName = "Friday Start Time")]
		public virtual DateTime? FriStartTime
		{
			get
			{
				return this._FriStartTime;
			}
			set
			{
				this._FriStartTime = value;
			}
		}
		#endregion
		#region FriEndTime
		public abstract class friEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _FriEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.friWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.friEndTime>>))]
		[PXUIField(DisplayName = "Friday End Time")]
		public virtual DateTime? FriEndTime
		{
			get
			{
				return this._FriEndTime;
			}
			set
			{
				this._FriEndTime = value;
			}
		}
		#endregion
		#region FriGoodsMoves
		public abstract class friGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _FriGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? FriGoodsMoves
		{
			get
			{
				return this._FriGoodsMoves;
			}
			set
			{
				this._FriGoodsMoves = value;
			}
		}
		#endregion
		#region SatWorkDay
		public abstract class satWorkDay : PX.Data.IBqlField
		{
		}
		protected Boolean? _SatWorkDay;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Saturday")]
		public virtual Boolean? SatWorkDay
		{
			get
			{
				return this._SatWorkDay;
			}
			set
			{
				this._SatWorkDay = value;
			}
		}
		#endregion
		#region SatStartTime
		public abstract class satStartTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _SatStartTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 09:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.satWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.satStartTime>>))]
		[PXUIField(DisplayName = "Saturday Start Time", Required = false)]
		public virtual DateTime? SatStartTime
		{
			get
			{
				return this._SatStartTime;
			}
			set
			{
				this._SatStartTime = value;
			}
		}
		#endregion
		#region SatEndTime
		public abstract class satEndTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _SatEndTime;
		[PXDBTime(DisplayMask = "t", UseTimeZone = false)]
		[PXDefault(TypeCode.DateTime, "01/01/2008 18:00:00")]
		[PXFormula(typeof(Switch<Case<Where<CSCalendar.satWorkDay, Equal<False>>, Null>, DefaultValue<CSCalendar.satEndTime>>))]
		[PXUIField(DisplayName = "Saturday End Time", Required = false)]
		public virtual DateTime? SatEndTime
		{
			get
			{
				return this._SatEndTime;
			}
			set
			{
				this._SatEndTime = value;
			}
		}
		#endregion
		#region SatGoodsMoves
		public abstract class satGoodsMoves : PX.Data.IBqlField
		{
		}
		protected Boolean? _SatGoodsMoves;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = " ")]
		public virtual Boolean? SatGoodsMoves
		{
			get
			{
				return this._SatGoodsMoves;
			}
			set
			{
				this._SatGoodsMoves = value;
			}
		}
		#endregion
		#region TimeZone
		public abstract class timeZone : PX.Data.IBqlField
		{
		}
		protected String _TimeZone;
		[PXDBString(32)]
		[PXUIField(DisplayName = "Time Zone")]
		[PXTimeZone]
		public virtual String TimeZone
		{
			get
			{
				return this._TimeZone;
			}
			set
			{
				this._TimeZone = value;
			}
		}
		#endregion

		public virtual bool IsWorkDay(DateTime date)
		{
			switch (date.DayOfWeek)
			{
				case DayOfWeek.Sunday:
					return (SunWorkDay == true);
				case DayOfWeek.Monday:
					return (MonWorkDay == true);
				case DayOfWeek.Tuesday:
					return (TueWorkDay == true);
				case DayOfWeek.Wednesday:
					return (WedWorkDay == true);
				case DayOfWeek.Thursday:
					return (ThuWorkDay == true);
				case DayOfWeek.Friday:
					return (FriWorkDay == true);
				case DayOfWeek.Saturday:
					return (SatWorkDay == true);
			}
			return false;
		}
	}
}
