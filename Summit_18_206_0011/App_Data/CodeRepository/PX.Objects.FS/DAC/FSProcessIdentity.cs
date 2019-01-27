using System;
using System.Collections.Generic;
using System.Text;
using PX.Data;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSProcessIdentity : PX.Data.IBqlTable
    {
        #region ProcessID
        public abstract class processID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? ProcessID { get; set; }
        #endregion
        #region ProcessType
        public abstract class processType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        public virtual string ProcessType { get; set; }
        #endregion
        #region FilterFromTo
        public abstract class filterFromTo : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "From Date")]
        public virtual DateTime? FilterFromTo { get; set; }
        #endregion
        #region FilterUpTo
        public abstract class filterUpTo : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "To Date")]
        public virtual DateTime? FilterUpTo { get; set; }
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
