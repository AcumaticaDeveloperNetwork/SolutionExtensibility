using PX.Data;
using PX.Data.EP;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    [Serializable]
    public class USSimpleInvoice : IBqlTable
    {
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
        [PXDefault]
        [PXFieldDescription]
        [PXSelector(typeof(Search<USSimpleInvoice.refNbr>), Filterable = true)]
        [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
        [AutoNumber(typeof(USSetup.simpleNumberingID), typeof(date))]
        public virtual string RefNbr { get; set; }
        #endregion

        #region ScoutLeaderID
        public abstract class scoutID : PX.Data.IBqlField
        {
        }
        [USScoutTran(Required = true)]
        public virtual int? ScoutID { get; set; }
        #endregion

        #region Status
        public abstract class status : PX.Data.IBqlField
        {
        }
        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Status", Enabled = false)]
        [USSimpleInvoiceStatus.List]
        [PXDefault(USSimpleInvoiceStatus.Pending)]
        public virtual string Status { get; set; }
        #endregion

        #region Description
        public abstract class description : PX.Data.IBqlField
        {
        }
        [PXUIField(DisplayName = "Description")]
        [PXDBString(255)]
        public virtual string Description { get; set; }
        #endregion

        #region Date
        public abstract class date : PX.Data.IBqlField
        {
        }
        [PXUIField(DisplayName = "Date")]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXDBDate]
        public virtual DateTime? Date { get; set; }
        #endregion

        #region InvoiceRef
        public abstract class invoiceRef : PX.Data.IBqlField
        {
        }
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Invoice Reference Nbr", Enabled = false)]
        public virtual string InvoiceRef { get; set; }
        #endregion

        #region Selected
        public abstract class selected : PX.Data.IBqlField
        {
        }
        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
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

        #region tStamp
        public abstract class Tstamp : IBqlField
        {
        }
        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}
