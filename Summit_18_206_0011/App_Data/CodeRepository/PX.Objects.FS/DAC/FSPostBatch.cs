using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.SERVICE_ORDER)]
    [PXPrimaryGraph(
            new Type[] {
                    typeof(PostBatchMaint),
                    typeof(InventoryPostBatchMaint)
            },
            new Type[] {
                    typeof(Where<FSPostBatch.postTo, NotEqual<ListField_PostTo.IN>>),
                    typeof(Where<FSPostBatch.postTo, Equal<ListField_PostTo.IN>>)
            })]
    public class FSPostBatch : PX.Data.IBqlTable
    {
        #region BatchID
        public abstract class batchID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        [PXUIField(Enabled = false, Visible = false, DisplayName = "Batch ID")]
        public virtual int? BatchID { get; set; }
        #endregion
        #region BatchNbr
        public abstract class batchNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<FSPostBatch.batchNbr, 
                                Where<
                                    FSPostBatch.status, NotEqual<FSPostBatch.status.temporary>>>))]
        [AutoNumber(typeof(Search<FSSetup.postBatchNumberingID>),
                    typeof(AccessInfo.businessDate))]
        public virtual string BatchNbr { get; set; }
        #endregion

        #region Status
        public abstract class status : PX.Data.IBqlField
        {
            #region List
            public class ListAttribute : PXStringListAttribute
            {
                public ListAttribute()
                    : base(
                        new string[] { Temporary, Completed },
                        new string[] { TX.Status_Batch.Temporary, TX.Status_Batch.Completed })
                {
                }
            }

            public const string Temporary = "T";
            public const string Completed = "C";

            public class temporary : Constant<string>
            {
                public temporary() : base(Temporary)
                {
                }
            }

            public class completed : Constant<string>
            {
                public completed() : base(Completed)
                {
                }
            }
            #endregion
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(status.Temporary, PersistingCheck = PXPersistingCheck.Nothing)]
        [status.List]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual string Status { get; set; }
        #endregion

        #region PostTo
        public abstract class postTo : ListField_PostTo
        {
        }

        [PXDBString(2, IsFixed = true)]
        [postTo.ListAtrribute]
        [PXUIField(DisplayName = "Generated In", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string PostTo { get; set; }
        #endregion
        #region QtyDoc
        public abstract class qtyDoc : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Documents Processed")]
        public virtual int? QtyDoc { get; set; }
        #endregion        
        #region FinPeriodID
        public abstract class finPeriodID : PX.Data.IBqlField
        {
        }

        [FinPeriodID(BqlField = typeof(FSPostBatch.finPeriodID))]
        [PXUIField(DisplayName = "Invoice Period", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string FinPeriodID { get; set; }
        #endregion
        #region InvoiceDate
        public abstract class invoiceDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Invoice Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? InvoiceDate { get; set; }
        #endregion
        #region BillingCycleID
        public abstract class billingCycleID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Billing Cycle", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSBillingCycle.billingCycleID), 
            SubstituteKey = typeof(FSBillingCycle.billingCycleCD), 
            DescriptionField = typeof(FSBillingCycle.descr))]
        public virtual int? BillingCycleID { get; set; }
        #endregion
        #region UpToDate
        public abstract class upToDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Up to Date")]
        public virtual DateTime? UpToDate { get; set; }
        #endregion        
        #region CutOffDate
        public abstract class cutOffDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Billing Cycle Cut-Off Date")]
        public virtual DateTime? CutOffDate { get; set; }
        #endregion        
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
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
        [PXUIField(DisplayName = "Last Modified By Screen ID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion

        #region Selected
        public abstract class selected : IBqlField
        {
        }

        [PXBool]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        #endregion
    }
}
