using System;
using PX.Data;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;

namespace PX.Objects.FS
{
    #region PXProjection
    [Serializable]
    [PXProjection(typeof(
            Select2<FSPostDet,
                 InnerJoin<FSPostInfo,
                    On<FSPostDet.postID, Equal<FSPostInfo.postID>>,
                LeftJoin<FSAppointmentDet,
                    On<FSAppointmentDet.postID, Equal<FSPostInfo.postID>>,
                LeftJoin<FSSODet,
                    On<FSSODet.postID, Equal<FSPostInfo.postID>>,
                LeftJoin<FSAppointment,
                    On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>,
                LeftJoin<FSServiceOrder,
                    On<FSServiceOrder.sOID, Equal<FSSODet.sOID>, Or<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>,
                LeftJoin<Customer,
                    On<Customer.bAccountID, Equal<FSServiceOrder.billCustomerID>>,
                LeftJoin<FSGeoZonePostalCode,
                    On<FSGeoZonePostalCode.postalCode, Equal<FSServiceOrder.postalCode>>,
                LeftJoin<FSGeoZone,
                    On<FSGeoZone.geoZoneID, Equal<FSGeoZonePostalCode.geoZoneID>>>>>>>>>>>))]
    #endregion
    public class PostingBatchDetail : IBqlTable
    {
        #region BatchID
        public abstract class batchID : PX.Data.IBqlField
        {
        }

        [PXDBInt(IsKey = true, BqlField = typeof(FSPostDet.batchID))]
        [PXParent(typeof(Select<FSPostBatch, Where<FSPostBatch.batchID, Equal<Current<PostingBatchDetail.batchID>>>>))]
        [PXDBLiteDefault(typeof(FSPostBatch.batchID))]
        [PXUIField(DisplayName = "Batch ID")]
        public virtual int? BatchID { get; set; }
        #endregion
        #region SOFields
        #region SOPosted
        public abstract class sOPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool(BqlField = typeof(FSPostDet.sOPosted))]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through Sales Order")]
        public virtual bool? SOPosted { get; set; }
        #endregion
        #region SOOrderType
        public abstract class sOOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, BqlField = typeof(FSPostDet.sOOrderType))]
        [PXUIField(DisplayName = "Sales Order Type")]
        public virtual string SOOrderType { get; set; }
        #endregion
        #region SOOrderNbr
        public abstract class sOOrderNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true, BqlField = typeof(FSPostDet.sOOrderNbr))]
        [PXUIField(DisplayName = "Sales Order Nbr.")]
        public virtual string SOOrderNbr { get; set; }
        #endregion
        #region SOLineNbr
        public abstract class sOLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSPostDet.sOLineNbr))]
        [PXUIField(DisplayName = "Sales Order Line Nbr.")]
        public virtual int? SOLineNbr { get; set; }
        #endregion
        #endregion
        #region ARFields
        #region ARPosted
        public abstract class aRPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool(BqlField = typeof(FSPostDet.aRPosted))]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through AR")]
        public virtual bool? ARPosted { get; set; }
        #endregion
        #region ARDocType
        public abstract class arDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true, BqlField = typeof(FSPostDet.arDocType))]
        [PXUIField(DisplayName = "AR Document Type")]
        public virtual string ARDocType { get; set; }
        #endregion
        #region ARRefNbr
        public abstract class arRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true, BqlField = typeof(FSPostDet.arRefNbr))]
        [PXUIField(DisplayName = "AR Reference Nbr.")]
        public virtual string ARRefNbr { get; set; }
        #endregion
        #region ARLineNbr
        public abstract class aRLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSPostDet.aRLineNbr))]
        [PXUIField(DisplayName = "AR Line Nbr.")]
        public virtual int? ARLineNbr { get; set; }
        #endregion
        #endregion
        #region APFields
        #region APPosted
        public abstract class aPPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool(BqlField = typeof(FSPostDet.aPPosted))]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through AP")]
        public virtual bool? APPosted { get; set; }
        #endregion
        #region apDocType
        public abstract class apDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true, BqlField = typeof(FSPostDet.apDocType))]
        [PXUIField(DisplayName = "AP Document Type")]
        public virtual string APDocType { get; set; }
        #endregion
        #region apRefNbr
        public abstract class apRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true, BqlField = typeof(FSPostDet.apRefNbr))]
        [PXUIField(DisplayName = "AP Reference Nbr.")]
        public virtual string APRefNbr { get; set; }
        #endregion
        #region APLineNbr
        public abstract class aPLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSPostDet.aPLineNbr))]
        [PXUIField(DisplayName = "AP Line Nbr.")]
        public virtual int? APLineNbr { get; set; }
        #endregion
        #endregion
        #region INFields
        #region INPosted
        public abstract class iNPosted : PX.Data.IBqlField
        {
        }

        [PXDBBool(BqlField = typeof(FSPostDet.iNPosted))]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Invoiced through IN")]
        public virtual bool? INPosted { get; set; }
        #endregion
        #region INDocType
        public abstract class iNDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true, BqlField = typeof(FSPostDet.iNDocType))]
        [PXUIField(DisplayName = "IN Document Type")]
        public virtual string INDocType { get; set; }
        #endregion
        #region INRefNbr
        public abstract class iNRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true, BqlField = typeof(FSPostDet.iNRefNbr))]
        [PXUIField(DisplayName = "IN Reference Nbr.")]
        public virtual string INRefNbr { get; set; }
        #endregion
        #region INLineNbr
        public abstract class iNLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSPostDet.iNLineNbr))]
        [PXUIField(DisplayName = "IN Line Nbr.")]
        public virtual int? INLineNbr { get; set; }
        #endregion
        #endregion
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
                if (this.APPosted == true)
                {
                    return this.APRefNbr;
                }
                else if (this.ARPosted == true)
                {
                    return this.ARRefNbr;
                }
                else if (this.INPosted == true)
                {
                    return this.INRefNbr;
                }
                else if (this.SOPosted == true)
                {
                    return this.SOOrderNbr;
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
                if (this.APPosted == true)
                {
                    return this.APDocType;
                }
                else if (this.ARPosted == true)
                {
                    return this.ARDocType;
                }
                else if (this.INPosted == true)
                {
                    return this.INDocType;
                }
                else if (this.SOPosted == true)
                {
                    return this.SOOrderType;
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
        [PXUIField(DisplayName = "Document", Enabled = false)]
        public virtual string Mem_PostedIn
        {
            get
            {
                //Value cannot be calculated with PXFormula attribute
                if (this.APPosted == true)
                {
                    return ID.Batch_PostTo.AP;
                }
                else if (this.ARPosted == true)
                {
                    return ID.Batch_PostTo.AR;
                }
                else if (this.INPosted == true)
                {
                    return ID.Batch_PostTo.IN;
                }
                else if (this.SOPosted == true)
                {
                    return ID.Batch_PostTo.SO;
                }

                return string.Empty;
            }
        }
        #endregion
        #region SrvOrdType
        // CacheAttached in ServiceOrderInq
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsKey = true, IsFixed = true, BqlField = typeof(FSServiceOrder.srvOrdType))]
        [PXUIField(DisplayName = "Service Order Type", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorSrvOrdType]
        public virtual string SrvOrdType { get; set; }
        #endregion
        #region AppointmentRefNbr
        public abstract class appointmentRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof(FSAppointment.refNbr))]
        [PXUIField(DisplayName = "Appointment Nbr.", Visibility = PXUIVisibility.SelectorVisible, Visible = true, Enabled = true)]
        [PXSelector(typeof(Search2<FSAppointment.refNbr,
                            LeftJoin<FSServiceOrder,
                                On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>,
                            LeftJoin<BAccount,
                                On<BAccount.bAccountID, Equal<FSServiceOrder.customerID>>>>,
                        Where<
                            FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>,
                        OrderBy<
                            Desc<FSAppointment.refNbr>>>),
                    new Type[] {
                                typeof(FSAppointment.refNbr),
                                typeof(FSServiceOrder.refNbr),
                                typeof(BAccount.acctName),
                                typeof(FSAppointment.docDesc),
                                typeof(FSAppointment.status),
                                typeof(FSAppointment.scheduledDateTimeBegin)
                    })]
        public virtual string AppointmentRefNbr { get; set; }
        #endregion
        #region SORefNbr
        public abstract class sORefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof(FSServiceOrder.refNbr))]
        [PXUIField(DisplayName = "Service Order Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorSORefNbr]
        public virtual string SORefNbr { get; set; }
        #endregion
        #region BillCustomerID
        public abstract class billCustomerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.billCustomerID))]
        [PXUIField(DisplayName = "Billing Customer ID")]
        [FSSelectorCustomer]
        public virtual int? BillCustomerID { get; set; }
        #endregion
        #region ActualDateTimeBegin
        public abstract class actualDateTimeBegin : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time", BqlField = typeof(FSAppointment.actualDateTimeBegin))]
        [PXUIField(DisplayName = "Actual Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? ActualDateTimeBegin { get; set; }
        #endregion
        #region ActualDateTimeEnd
        public abstract class actualDateTimeEnd : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time", BqlField = typeof(FSAppointment.actualDateTimeEnd))]
        [PXUIField(DisplayName = "Actual Date End", Visibility = PXUIVisibility.Invisible)]
        public virtual DateTime? ActualDateTimeEnd { get; set; }
        #endregion
        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.branchLocationID))]
        [PXUIField(DisplayName = "Branch Location ID")]
        [PXSelector(typeof(Search<FSBranchLocation.branchLocationID,
                            Where<FSBranchLocation.branchID, Equal<Current<FSServiceOrder.branchID>>>>),
                            SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
                            DescriptionField = typeof(FSBranchLocation.descr))]
        [PXFormula(typeof(Default<FSServiceOrder.branchID>))]
        public virtual int? BranchLocationID { get; set; }
        #endregion
        #region GeoZoneCD
        public abstract class geoZoneCD : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true, BqlField = typeof(FSGeoZone.geoZoneCD))]
        [PXUIField(DisplayName = "Service Area ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSGeoZone.geoZoneCD))]
        public virtual string GeoZoneCD { get; set; }
        #endregion
        #region DocDesc
        public abstract class docDesc : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true, BqlField = typeof(FSAppointment.docDesc))]
        [PXUIField(DisplayName = "Description")]
        public virtual string DocDesc { get; set; }
        #endregion
        #region SOID
        public abstract class sOID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.sOID))]
        [PXUIField(Enabled = false, Visible = false, DisplayName = "Service Order ID")]
        public virtual int? SOID { get; set; }
        #endregion
        #region AppointmentID
        public abstract class appointmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSAppointment.appointmentID))]
        public virtual int? AppointmentID { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0], BqlField = typeof(FSAppointment.noteID))]
        public virtual Guid? NoteID { get; set; }
        #endregion
        #region AcctName
        [PXDBString(60, IsUnicode = true, BqlField = typeof(Customer.acctName))]
        [PXDefault]
        [PXFieldDescription]
        [PXUIField(DisplayName = "Customer Name", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string AcctName { get; set; }
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
    }
}
