using System;
using PX.Data;
﻿
namespace PX.Objects.FS
{
	[System.SerializableAttribute]
	public class FSWrkProcess : PX.Data.IBqlTable
	{
		#region ProcessID
		public abstract class processID : PX.Data.IBqlField
		{
		}
		[PXDBIdentity(IsKey = true)]
		[PXUIField(Enabled = false, Visible=false)]
        public virtual int? ProcessID { get; set; }
		#endregion
		#region SrvOrdType
		public abstract class srvOrdType : PX.Data.IBqlField
		{
		}
		[PXDBString(4, IsFixed = true)]
		[PXUIField(DisplayName = "Service Order Type")]
        public virtual string SrvOrdType { get; set; }
		#endregion
		#region SOID
		public abstract class sOID : PX.Data.IBqlField
		{
		}
		[PXDBInt]
		[PXUIField(DisplayName = "Service Order ID")]
        public virtual int? SOID { get; set; }
		#endregion
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Branch ID")]
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
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        [PXUIField(DisplayName = "Customer ID")]
        public virtual int? CustomerID { get; set; }
        #endregion  
        #region RoomID
        public abstract class roomID : PX.Data.IBqlField
		{
		}
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Room ID")]
        public virtual string RoomID { get; set; }
		#endregion
		#region ScheduledDateTimeBegin
		public abstract class scheduledDateTimeBegin : PX.Data.IBqlField
		{
		}
        protected DateTime? _ScheduledDateTimeBegin;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
		[PXUIField(DisplayName = "Scheduled Date Time Begin")]
        public virtual DateTime? ScheduledDateTimeBegin
        {
            get
            {
                return this._ScheduledDateTimeBegin;
            }

            set
            {
                this.ScheduledDateTimeBeginUTC = value;
                this._ScheduledDateTimeBegin = value;
            }
        }
        #endregion
        #region ScheduledDateTimeEnd
        public abstract class scheduledDateTimeEnd : PX.Data.IBqlField
		{
		}
        protected DateTime? _ScheduledDateTimeEnd;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true)]
		[PXUIField(DisplayName = "Scheduled Date Time End")]
        public virtual DateTime? ScheduledDateTimeEnd
        {
            get
            {
                return this._ScheduledDateTimeEnd;
            }

            set
            {
                this.ScheduledDateTimeEndUTC = value;
                this._ScheduledDateTimeEnd = value;
            }
        }
        #endregion
        #region LineRefList
        public abstract class lineRefList : PX.Data.IBqlField
		{
		}
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Line Ref. List")]
        public virtual string LineRefList { get; set; }
		#endregion
		#region EmployeeIDList
		public abstract class employeeIDList : PX.Data.IBqlField
		{
		}
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Employee ID List")]
        public virtual string EmployeeIDList { get; set; }
		#endregion
		#region EquipmentIDList
		public abstract class equipmentIDList : PX.Data.IBqlField
		{
		}
		[PXDBString(256, IsUnicode = true)]
		[PXUIField(DisplayName = "Equipment ID List")]
        public virtual string EquipmentIDList { get; set; }
		#endregion
		#region TargetScreenID
		public abstract class targetScreenID : PX.Data.IBqlField
		{
		}
		[PXDBString(8, IsFixed = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Target Screen ID")]
        public virtual string TargetScreenID { get; set; }
		#endregion
		#region ExtraParms
		public abstract class extraParms : PX.Data.IBqlField
		{
		}
		[PXDBString(int.MaxValue, IsUnicode = true)]
		[PXUIField(DisplayName = "Extra Parameters")]
        public virtual string ExtraParms { get; set; }
        #endregion
        #region SMEquipmentID
        public abstract class smEquipmentID : PX.Data.IBqlField
        {
        }
        [PXDBInt]
        public virtual int? SMEquipmentID { get; set; }
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
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		[PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
		#endregion

        #region UTC Fields
        #region ScheduledDateTimeBeginUTC
        public abstract class scheduledDateTimeBeginUTC : PX.Data.IBqlField
        {
        }
        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
        [PXUIField(DisplayName = "Scheduled Date Time Begin")]
        public virtual DateTime? ScheduledDateTimeBeginUTC { get; set; }
        #endregion
        #region ScheduledDateTimeEndUTC
        public abstract class scheduledDateTimeEndUTC : PX.Data.IBqlField
        {
        }
        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
        [PXUIField(DisplayName = "Scheduled Date Time End")]
        public virtual DateTime? ScheduledDateTimeEndUTC { get; set; }
        #endregion
        #endregion
    }
}
