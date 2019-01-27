using System;
using PX.Data;
﻿
namespace PX.Objects.FS
{
	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.RESOLUTION)]
    [PXPrimaryGraph(typeof(ResolutionMaint))]
	public class FSResolution : PX.Data.IBqlTable
	{
		#region ResolutionID
		public abstract class resolutionID : PX.Data.IBqlField
		{
		}
        [PXDBIdentity]
		public virtual int? ResolutionID { get; set; }

		#endregion
		#region ResolutionCD
		public abstract class resolutionCD : PX.Data.IBqlField
		{
		}
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXDefault]
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "Resolution ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string ResolutionCD { get; set; }

		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}
		[PXDBString(60, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string Descr { get; set; }

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
	}
}
