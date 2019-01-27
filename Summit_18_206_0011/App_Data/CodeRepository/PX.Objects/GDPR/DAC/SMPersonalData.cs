using System;
using PX.Data;

namespace PX.Objects.GDPR
{
	[Serializable]
	[PXHidden]
	public class SMPersonalData : IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField { }

		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
		public virtual bool? Selected { get; set; }
		#endregion

		#region Table
		public abstract class table : IBqlField { }

		[PXDBString(100, IsKey = true, IsUnicode = true)]
		[PXUIField(DisplayName = "Table name")]
		public virtual String Table { get; set; }
		#endregion

		#region Field
		public abstract class field : IBqlField { }

		[PXDBString(100, IsKey = true, IsUnicode = true)]
		[PXUIField(DisplayName = "Field name")]
		public virtual String Field { get; set; }
		#endregion

		#region NoteID
		public abstract class noteID : IBqlField { }

		[PXDBGuid(IsKey = true)]
		public virtual Guid? NoteID { get; set; }
		#endregion

		#region TopParentNoteID
		public abstract class topParentNoteID : IBqlField { }

		[PXDBGuid]
		public virtual Guid? TopParentNoteID { get; set; }
		#endregion

		#region Value
		public abstract class value : IBqlField { }

		[PXDBString(100, IsUnicode = true)]
		[PXUIField(DisplayName = "Value")]
		public virtual String Value { get; set; }
		#endregion

		#region CreatedDateTime
		public abstract class createdDateTime : IBqlField { }

		[PXDBCreatedDateTimeUtc]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? CreatedDateTime { get; set; }
		#endregion
	}
}