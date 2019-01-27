using PX.Data;
using PX.Objects.AP;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    [Serializable]
    public class USSimpleInvoiceLine : IBqlTable
    {
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }
        [PXDBDefault(typeof(USSimpleInvoice.refNbr))]
        [PXDBString(15, IsUnicode = true, IsKey = true)]
        [PXParent(typeof(Select<USSimpleInvoice, Where<USSimpleInvoice.refNbr, Equal<Current<USSimpleInvoiceLine.refNbr>>>>))]
        [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
        public virtual string RefNbr { get; set; }
        #endregion

        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }
        [APTranInventoryItem(Filterable = true, IsKey = true, Required = true)]
        public virtual int? InventoryID { get; set; }
        #endregion

        #region Qty
        public abstract class qty : PX.Data.IBqlField
        {
        }
        [PXDBQuantity]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Quantity", Visibility = PXUIVisibility.Visible)]
        public virtual decimal? Qty { get; set; }
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

        #region tStamp
        public abstract class Tstamp : IBqlField
        {
        }
        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}
