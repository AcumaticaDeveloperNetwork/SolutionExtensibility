using System;
using PX.Data;
﻿
namespace PX.Objects.FS
{

	[System.SerializableAttribute]
    [PXCacheName(TX.TableName.SKILL)]
    [PXPrimaryGraph(typeof(SkillMaint))]
	public class FSSkill : PX.Data.IBqlTable
	{
		#region SkillID
		public abstract class skillID : PX.Data.IBqlField
		{
		}
		[PXDBIdentity]
		public virtual int? SkillID { get; set; }

		#endregion
		#region SkillCD
		public abstract class skillCD : PX.Data.IBqlField
		{
		}
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXDefault]
        [NormalizeWhiteSpace]
		[PXUIField(DisplayName = "Skill ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string SkillCD { get; set; }

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
        #region IsDriverSkill
        public abstract class isDriverSkill : PX.Data.IBqlField
        {
        }
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Driver Skill")]
        public virtual bool? IsDriverSkill { get; set; }
        #endregion
	}
}