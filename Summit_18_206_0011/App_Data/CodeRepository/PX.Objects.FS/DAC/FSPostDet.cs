using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;

namespace PX.Objects.FS
{
	[System.SerializableAttribute]
	public class FSPostDet : PX.Data.IBqlTable
	{
		#region BatchID
        public abstract class batchID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
        [PXParent(typeof(Select<FSPostBatch, Where<FSPostBatch.batchID, Equal<Current<FSPostDet.batchID>>>>))]
        [PXDBLiteDefault(typeof(FSPostBatch.batchID))]
        [PXUIField(DisplayName = "Batch ID")]
        public virtual int? BatchID { get; set; }
		#endregion
		#region PostDetID
        public abstract class postDetID : PX.Data.IBqlField
		{
		}

		[PXDBIdentity(IsKey = true)]
		[PXUIField(Enabled = false)]
        public virtual int? PostDetID { get; set; }
		#endregion
        #region PostID
        public abstract class postID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(Enabled = false)]
        public virtual int? PostID { get; set; }
        #endregion
        #region SOFields
        #region SOPosted
        public abstract class sOPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through Sales Order")]
        public virtual bool? SOPosted { get; set; }
        #endregion
        #region SOOrderType
        public abstract class sOOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "Sales Order Type")]
        public virtual string SOOrderType { get; set; }
        #endregion
        #region SOOrderNbr
        public abstract class sOOrderNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Sales Order Nbr.")]
        public virtual string SOOrderNbr { get; set; }
        #endregion
        #region SOLineNbr
        public abstract class sOLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Sales Order Line Nbr.")]
        public virtual int? SOLineNbr { get; set; }
        #endregion
        #endregion
        #region ARFields
        #region ARPosted
        public abstract class aRPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through AR")]
        public virtual bool? ARPosted { get; set; }
        #endregion
        #region ARDocType
        public abstract class arDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true)]
        [PXUIField(DisplayName = "AR Document Type")]
        public virtual string ARDocType { get; set; }
        #endregion
        #region ARRefNbr
        public abstract class arRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "AR Reference Nbr.")]
        public virtual string ARRefNbr { get; set; }
        #endregion
        #region ARLineNbr
        public abstract class aRLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "AR Line Nbr.")]
        public virtual int? ARLineNbr { get; set; }
        #endregion
        #endregion
        #region APFields
        #region APPosted
        public abstract class aPPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through AP")]
        public virtual bool? APPosted { get; set; }
        #endregion
        #region apDocType
        public abstract class apDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true)]
        [PXUIField(DisplayName = "AP Document Type")]
        public virtual string APDocType { get; set; }
        #endregion
        #region apRefNbr
        public abstract class apRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "AP Reference Nbr.")]
        public virtual string APRefNbr { get; set; }
        #endregion
        #region APLineNbr
        public abstract class aPLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "AP Line Nbr.")]
        public virtual int? APLineNbr { get; set; }
        #endregion
        #endregion
        #region INFields
        #region INPosted
        public abstract class iNPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through IN")]
        public virtual bool? INPosted { get; set; }
        #endregion
        #region INDocType
        public abstract class iNDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "IN Document Type")]
        public virtual string INDocType { get; set; }
        #endregion
        #region INRefNbr
        public abstract class iNRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "IN Reference Nbr.")]
        public virtual string INRefNbr { get; set; }
        #endregion
        #region INLineNbr
        public abstract class iNLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "IN Line Nbr.")]
        public virtual int? INLineNbr { get; set; }
        #endregion
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
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedDateTime]
        public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion
		#region Tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}

		[PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
        #region MemoryHelper
        #region Mem_DocNbr
        public abstract class mem_DocNbr : PX.Data.IBqlField
        {
        }

        [PXString(15)]
        [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
        public virtual string Mem_DocNbr
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (APPosted == true)
                {
                    return APRefNbr;
                }
                else if (ARPosted == true)
                {
                    return ARRefNbr;
                }
                else if (INPosted == true)
                {
                    return INRefNbr;
                }
                else if (SOPosted == true)
                {
                    return SOOrderNbr;
                }

                return string.Empty;
            }
        }
        #endregion
        #region Mem_DocType
        public abstract class mem_DocType : PX.Data.IBqlField
        {
        }

        [PXString(3)]
        [PXUIField(DisplayName = "Document Type", Enabled = false)]
        public virtual string Mem_DocType
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (APPosted == true)
                {
                    return APDocType;
                }
                else if (ARPosted == true)
                {
                    return ARDocType;
                }
                else if (INPosted == true)
                {
                    return INDocType;
                }
                else if (SOPosted == true)
                {
                    return SOOrderType;
                }

                return string.Empty;
            }
        }
        #endregion
        #region Mem_PostedIn
        public abstract class mem_PostedIn : PX.Data.IBqlField
        {
        }

        [PXString(2)]
        [PXUIField(DisplayName = "Post To", Enabled = false)]
        public virtual string Mem_PostedIn
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (APPosted == true)
                {
                    return ID.Batch_PostTo.AP;
                }
                else if (ARPosted == true)
                {
                    return ID.Batch_PostTo.AR;
                }
                else if (INPosted == true)
                {
                    return ID.Batch_PostTo.IN;
                }
                else if (SOPosted == true)
                {
                    return ID.Batch_PostTo.SO;
                }

                return string.Empty;
            }
        }
        #endregion
        #region InvoiceRefNbr
        public abstract class invoiceRefNbr : PX.Data.IBqlField
        {
        }

        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Invoice Nbr.")]
        [PXSelector(typeof(Search<ARInvoice.refNbr>))]
        public virtual string InvoiceRefNbr { get; set; }
        #endregion
        #endregion
    }
}