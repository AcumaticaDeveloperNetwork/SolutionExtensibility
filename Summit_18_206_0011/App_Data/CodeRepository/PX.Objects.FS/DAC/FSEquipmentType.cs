using PX.Data;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.EQUIPMENT_TYPE)]
    [PXPrimaryGraph(typeof(EquipmentTypeMaint))]
	public class FSEquipmentType : PX.Data.IBqlTable
	{
		#region EquipmentTypeID
		public abstract class equipmentTypeID : PX.Data.IBqlField
		{
		}		

		[PXDBIdentity]
		[PXUIField(Enabled = false)]        
		public virtual int? EquipmentTypeID { get; set; }
		#endregion
		#region EquipmentTypeCD
		public abstract class equipmentTypeCD : PX.Data.IBqlField
		{
		}

        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXDefault]
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "Equipment Type", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<FSEquipmentType.equipmentTypeCD>), DescriptionField = typeof(FSEquipmentType.descr))]
		public virtual string EquipmentTypeCD { get; set; }
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
		#region RequireBranchLocation
		public abstract class requireBranchLocation : PX.Data.IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Branch Location")]
		public virtual bool? RequireBranchLocation { get; set; }
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
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}

		[PXDBTimestamp]
		public virtual byte[] tstamp { get; set; }
		#endregion
	}
}