using System;
using PX.Data;

namespace PX.Objects.FA
{
	[Serializable]
	[PXPrimaryGraph(typeof(BookMaint))]
	[PXCacheName(Messages.FABook)]
	public partial class FABook : IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField {}
		/// <summary>
		/// An unbound service field, which indicates that the book is marked for processing.
		/// </summary>
		/// <value>
		/// If the value of the field is <c>true</c>, the book will be processed; otherwise, the book will not be processed.
		/// </value>
		[PXBool]
		[PXUIField(DisplayName = "Selected")]
		public bool? Selected { get; set; }
		#endregion
		#region BookID
		public abstract class bookID : IBqlField {}
		/// <summary>
		/// The identifier of the book.
		/// The identifier is used for foreign references; it can be negative for newly inserted records.
		/// </summary>
		/// <value>
		/// A unique integer number.
		/// </value>
		[PXDBIdentity]
		public virtual int? BookID { get; set; }
		#endregion
		#region BookCode
		public abstract class bookCode : IBqlField {}
		/// <summary>
		/// A string identifier, which contains a key value. This field is also a selector for navigation.
		/// </summary>
		/// <value>The value can be entered only manually.</value>
		[PXDefault]
		[PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
		[PXUIField(DisplayName = "Book ID", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 0)]
		public virtual string BookCode { get; set; }
		#endregion
		#region Description
		public abstract class description : IBqlField {}
		/// <summary>
		/// The description of the book.
		/// </summary>
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
		public virtual string Description { get; set; }
		#endregion
		#region UpdateGL
		public abstract class updateGL : IBqlField {}
		/// <summary>
		/// A flag that determines whether the book posts FA transaction data to the General Ledger module.
		/// </summary>
		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Posting Book")]
		public virtual bool? UpdateGL { get; set; }
		#endregion
		#region MidMonthType
		public abstract class midMonthType : IBqlField
		{
			#region List
			/// <summary>
			/// The type of month middle point.
			/// </summary>
			/// <value>
			/// Allowed values are:
			/// <list type="bullet">
			/// <item> <term><c>F</c></term> <description>Fixed Day. Mid-period is determined by the number of day in the month.</description> </item>
			/// <item> <term><c>N</c></term> <description>Number of Days. Mid-period is determined by the number of days that have passed since the beginning of the period.</description> </item>
			/// </list>
			/// </value>
			public class ListAttribute : PXStringListAttribute
			{
				public ListAttribute()
					: base(
					new string[] { FixedDay, NumberOfDays },
					new string[] { Messages.FixedDay, Messages.NumberOfDays }) { }
			}

			public const string FixedDay = "F";
			public const string NumberOfDays = "N";
			public const string PeriodDaysHalve = "H";

			public class fixedDay : Constant<string>
			{
				public fixedDay() : base(FixedDay) {}
			}
			public class numberOfDays : Constant<string>
			{
				public numberOfDays() : base(NumberOfDays) {}
			}
			public class periodDaysHalve : Constant<string>
			{
				public periodDaysHalve() : base(PeriodDaysHalve) {}
			}
			#endregion
		}
		/// <summary>
		/// The type of the middle point of the period.
		/// </summary>
		/// <value>
		/// The field can have the values, which are described in <see cref="midMonthType.ListAttribute"/>
		/// </value>
		[PXDBString(1, IsFixed = true)]
		[PXDefault(midMonthType.FixedDay)]
		[PXUIField(DisplayName = "Mid-Period Type")]
		[midMonthType.List]
		public virtual string MidMonthType { get; set; }
		#endregion
		#region MidMonthDay
		public abstract class midMonthDay : IBqlField {}
		/// <summary>
		/// Value for <see cref="MidMonthType"/>.
		/// </summary>
		[PXDBShort]
		[PXUIField(DisplayName = "Mid-Period Day")]
		public virtual short? MidMonthDay { get; set; }
		#endregion

		#region FirstCalendarYear
		public abstract class firstCalendarYear : IBqlField { }
		/// <summary>
		/// The first year of calendar for the book.
		/// </summary>
		/// <value>
		/// This is an unbound information field.
		/// </value>
		[PXString(4, IsFixed = true)]
		[PXUIField(DisplayName = "First Calendar Year", Enabled = false)]
		// TODO: AC-106141
		// Use current organization from AccessInfo
		[PXDBScalar(typeof(Search4<
			FABookYear.year, 
			Where<FABookYear.bookID, Equal<FABook.bookID>>, 
			Aggregate<
				Min<FABookYear.year>>>))]
		public virtual string FirstCalendarYear { get; set; }
		#endregion
		#region LastCalendarYear
		public abstract class lastCalendarYear : IBqlField { }
		/// <summary>
		/// The last year of calendar for the book.
		/// </summary>
		/// <value>
		/// This is an unbound information field.
		/// </value>
		[PXString(4, IsFixed = true)]
		[PXUIField(DisplayName = "Last Calendar Year", Enabled = false)]
		// TODO: AC-106141
		// Use current organization from AccessInfo
		[PXDBScalar(typeof(Search4<
			FABookYear.year, 
			Where<FABookYear.bookID, Equal<FABook.bookID>>, 
			Aggregate<
				Max<FABookYear.year>>>))]
		public virtual string LastCalendarYear { get; set; }
		#endregion


		#region tstamp
		public abstract class Tstamp : IBqlField {}
		[PXDBTimestamp]
		public virtual byte[] tstamp { get; set; }
		#endregion
		#region CreatedByID
		public abstract class createdByID : IBqlField {}
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID { get; set; }
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : IBqlField {}
		[PXDBCreatedByScreenID]
		public virtual string CreatedByScreenID { get; set; }
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField {}
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime { get; set; }
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : IBqlField {}
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID { get; set; }
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : IBqlField {}
		[PXDBLastModifiedByScreenID]
		public virtual string LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : IBqlField {}
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion
	}
}
