using System;
using System.Collections.Generic;
using System.Text;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    [PXCacheName(TX.TableName.GENERATION_LOG_ERROR)]
    public class FSGenerationLogError : PX.Data.IBqlTable
    {
        #region LogID
        public abstract class logID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? LogID { get; set; }
        #endregion
        #region GenerationID
        public abstract class generationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Generation ID")]
        public virtual int? GenerationID { get; set; }
        #endregion
        #region ScheduleID
        public abstract class scheduleID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Schedule ID")]
        public virtual int? ScheduleID { get; set; }
        #endregion
        #region ErrorMessage
        public abstract class errorMessage : PX.Data.IBqlField
        {
        }

        [PXDBString(int.MaxValue, IsUnicode = true)]
        [PXUIField(DisplayName = "Error Message")]
        public virtual string ErrorMessage { get; set; }
        #endregion
        #region ErrorDate
        public abstract class errorDate : PX.Data.IBqlField
        {
        }

        protected DateTime? _ErrorDate;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Generation Date")]
        [PXUIField(DisplayName = "Generation Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? ErrorDate
        {
            get
            {
                return this._ErrorDate;
            }

            set
            {
                this.ErrorDateUTC = value;
                this._ErrorDate = value;
            }
        }
        #endregion
        #region ProcessType
        public abstract class processType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        public virtual string ProcessType { get; set; }
        #endregion
        #region Ignore
        public abstract class ignore : PX.Data.IBqlField
        {
        }

        [PXDefault(false)]
        [PXDBBool]
        [PXUIField(DisplayName = "Ignore")]
        public virtual bool? Ignore { get; set; }
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

        #region UTC Fields
        #region ErrorDateUTC
        public abstract class errorDateUTC : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Generation Date")]
        [PXUIField(DisplayName = "Generation Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? ErrorDateUTC { get; set; }
        #endregion
        #endregion
    }
}
