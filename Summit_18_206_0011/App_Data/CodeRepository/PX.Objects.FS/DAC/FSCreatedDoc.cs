using PX.Data;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSCreatedDoc : PX.Data.IBqlTable
    {
        #region BatchID
        public abstract class batchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? BatchID { get; set; }
        #endregion
        #region RecordID
        public abstract class recordID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? RecordID { get; set; }
        #endregion
        #region PostTo
        public abstract class postTo : ListField_PostTo
        {
        }

        [PXDBString(2, IsFixed = true)]
        [postTo.ListAtrribute]
        [PXUIField(DisplayName = "Posted to", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string PostTo { get; set; }
        #endregion
        #region CreatedDocType
        public abstract class createdDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(4)]
        public virtual string CreatedDocType { get; set; }
        #endregion
        #region CreatedRefNbr
        public abstract class createdRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        public virtual string CreatedRefNbr { get; set; }
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
    }
}
