using System;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSContractAction : PX.Data.IBqlTable
    {
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
        [PXParent(typeof(Select<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Current<FSContractAction.serviceContractID>>>>))]
        [PXDBLiteDefault(typeof(FSServiceContract.serviceContractID))]
        public virtual int? ServiceContractID { get; set; }
        #endregion
        #region RecordID
        public abstract class recordID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? RecordID { get; set; }
        #endregion
        #region Type
        public abstract class type : ListField_RecordType_ContractAction
        {
        }

        [type.ListAtrribute]
        [PXDBString(1, IsUnicode = true)]
        [PXUIField(DisplayName = "Type", Enabled = false)]
        public virtual string Type { get; set; }
        #endregion
        #region Action
        public abstract class action : ListField_Action_ContractAction
        {
        }

        [action.ListAtrribute]
        [PXDBString(1, IsUnicode = true)]
        [PXUIField(DisplayName = "Action", Enabled = false)]
        public virtual string Action { get; set; }
        #endregion
        #region ActionBusinessDate
        public abstract class actionBusinessDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Date", Enabled = false)]
        public virtual DateTime? ActionBusinessDate { get; set; }
        #endregion
        #region EffectiveDate
        public abstract class effectiveDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Effective Date", Enabled = false)]
        public virtual DateTime? EffectiveDate { get; set; }
        #endregion
        #region ScheduleChangeRecurrence
        public abstract class scheduleChangeRecurrence : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Change Recurrence", Enabled = false)]
        public virtual bool? ScheduleChangeRecurrence { get; set; }
        #endregion
        #region ScheduleNextExecutionDate
        public abstract class scheduleNextExecutionDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Effective Recurrence Start Date", Enabled = false)]
        public virtual DateTime? ScheduleNextExecutionDate { get; set; }
        #endregion
        #region ScheduleRecurrenceDescr
        public abstract class scheduleRecurrenceDescr : PX.Data.IBqlField
        {
        }
        [PXDBString(int.MaxValue, IsUnicode = true)]
        [PXUIField(DisplayName = "Recurrence Description", Enabled = false)]
        public virtual string ScheduleRecurrenceDescr { get; set; }
        #endregion
        #region ScheduleRefNbr
        public abstract class scheduleRefNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Schedule Ref. Nbr.", Enabled = false)]
        public virtual string ScheduleRefNbr { get; set; }
        #endregion
        #region Applied
        public abstract class applied : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Applied", Enabled = false)]
        public virtual bool? Applied { get; set; }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        [PXUIField(DisplayName = "Created By ID")]
        public virtual Guid? CreatedByID { get; set; }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "Created By Screen ID")]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = "Created DateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "Last Modified By ID")]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "Last Modified By Screen ID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = "Last Modified Date Time")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
    }
}