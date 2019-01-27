using System;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSCustomFieldAppointment : PX.Data.IBqlTable
    {
        #region CustomFieldAppointmentID
        public abstract class customFieldAppointmentID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        [PXUIField(Enabled = false)]
        public virtual int? CustomFieldAppointmentID { get; set; }
        #endregion
        #region Active
        public abstract class active : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? Active { get; set; }
        #endregion
        #region FieldDescr
        public abstract class fieldDescr : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string FieldDescr { get; set; }
        #endregion
        #region FieldImg
        public abstract class fieldImg : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Field Image")]
        public virtual string FieldImg { get; set; }
        #endregion
        #region FieldName
        public abstract class fieldName : PX.Data.IBqlField
        {
        }

        [PXDBString(60, IsUnicode = true)]
        [PXDefault]
        [PXUIField(DisplayName = "Field Name")]
        public virtual string FieldName { get; set; }
        #endregion
        #region Position
        public abstract class position : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Position")]
        public virtual int? Position { get; set; }
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
    }
}