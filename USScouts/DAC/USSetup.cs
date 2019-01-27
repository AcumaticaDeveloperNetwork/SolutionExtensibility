using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    [System.Serializable]
    public class USSetup : PX.Data.IBqlTable
    {
        #region EnableInAP
        public abstract class enableInAP : IBqlField
        {
        }
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable in AP")]
        public virtual bool? EnableInAP { get; set; }
        #endregion

        #region InvoiceNumberingID
        public abstract class simpleNumberingID : IBqlField
        {
        }
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Simple Invoice Numbering ID")]
        [PXDefault()]
        [PXSelector(typeof(Numbering.numberingID))]
        public virtual string SimpleNumberingID { get; set; }
        #endregion

        #region VendorID
        public abstract class vendorID : PX.Data.IBqlField
        {
        }
        [VendorActive(
            Visibility = PXUIVisibility.SelectorVisible,
            DescriptionField = typeof(Vendor.acctName),
            CacheGlobal = true,
            Filterable = true)]
        [PXDefault]
        public virtual int? VendorID { get; set; }
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

        #region tstamp
        public abstract class Tstamp : IBqlField
        {
        }
        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}
