using PX.Data;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.ORDER_STAGE)]
    [PXPrimaryGraph(typeof(WFStageMaint))]
	public class FSWFStage : PX.Data.IBqlTable
    {
        #region WFID
        public abstract class wFID : PX.Data.IBqlField
		{
		}

        [PXDBInt(IsKey = true)]
        [PXUIField(DisplayName = "Workflow ID")]
        public virtual int? WFID { get; set; }
		#endregion
        #region ParentWFStageID
        public abstract class parentWFStageID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true)]
        [PXDBLiteDefault(typeof(FSWFStage.wFStageID))]
        [PXUIField(DisplayName = "Parent Workflow Stage ID")]
        public virtual int? ParentWFStageID { get; set; }
        #endregion
        #region WFStageID
        public abstract class wFStageID : PX.Data.IBqlField
		{
		}

        [PXDBIdentity]
        [PXUIField(DisplayName = "Workflow Stage ID")]
		public virtual int? WFStageID { get; set; }
		#endregion
        #region WFStageCD
        public abstract class wFStageCD : PX.Data.IBqlField
		{
		}

        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault]
        [PXUIField(DisplayName = "Workflow Stage ID", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string WFStageCD { get; set; }
		#endregion
		#region AllowCancel
		public abstract class allowCancel : PX.Data.IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Allow Cancel")]
		public virtual bool? AllowCancel { get; set; }
		#endregion
		#region AllowDelete
		public abstract class allowDelete : PX.Data.IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Allow Delete")]
		public virtual bool? AllowDelete { get; set; }
		#endregion
		#region AllowModify
		public abstract class allowModify : PX.Data.IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Allow Update")]
		public virtual bool? AllowModify { get; set; }
		#endregion
		#region AllowPost
		public abstract class allowPost : PX.Data.IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Allow Post")]
		public virtual bool? AllowPost { get; set; }
		#endregion
        #region AllowComplete
        public abstract class allowComplete : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Complete")]
        public virtual bool? AllowComplete { get; set; }
        #endregion
        #region AllowReopen
        public abstract class allowReopen : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Reopen")]
        public virtual bool? AllowReopen { get; set; }
        #endregion
        #region AllowClose
        public abstract class allowClose : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Allow Close")]
        public virtual bool? AllowClose { get; set; }
        #endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}

		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string Descr { get; set; }
		#endregion
		#region RequireReason
		public abstract class requireReason : PX.Data.IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Require Reason")]
		public virtual bool? RequireReason { get; set; }
		#endregion
		#region SortOrder
		public abstract class sortOrder : PX.Data.IBqlField
		{
		}

		[PXDBInt]
		[PXDefault(0)]
		[PXUIField(DisplayName = "Sort Order")]
		public virtual int? SortOrder { get; set; }
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
