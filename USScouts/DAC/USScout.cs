using PX.Data;
using PX.Data.EP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    [Serializable]
    public class USScout : PX.Data.IBqlTable
    {
        #region LeaderID
        public abstract class scoutID : PX.Data.IBqlField
        {
        }
        [PXDBIdentity]
        public virtual int? ScoutID { get; set; }
        #endregion

        #region MasterCD
        public abstract class scoutCD : IBqlField
        {
        }
        [PXDBString(30, IsKey = true)]
        [PXUIField(DisplayName = "Scout CD")]
        [PXDefault]
        [PXSelector(typeof(Search<scoutCD>), typeof(scoutCD), typeof(description), DescriptionField = typeof(description))]
        [PXFieldDescription]
        public virtual string ScoutCD { get; set; }
        #endregion

        #region Description

        public abstract class description : IBqlField
        {
        }
        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        public virtual string Description { get; set; }
        #endregion

        #region CreatedByID
        public abstract class createdByID : IBqlField
        {
        }
        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }
        #endregion

        #region CreatedByScreenID
        public abstract class createdByScreenID : IBqlField
        {
        }
        [PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID { get; set; }
        #endregion

        #region CreatedDateTime
        public abstract class createdDateTime : IBqlField
        {
        }
        [PXDBCreatedDateTime]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion

        #region LastModifiedByID
        public abstract class lastModifiedByID : IBqlField
        {
        }
        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion

        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : IBqlField
        {
        }
        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion

        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : IBqlField
        {
        }
        [PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion

        #region NoteID
        public abstract class noteID : IBqlField
        {
        }
        [PXNote]
        public virtual Guid? NoteID { get; set; }
        #endregion

        #region tstamp
        public abstract class Tstamp : IBqlField
        {
        }
        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}
