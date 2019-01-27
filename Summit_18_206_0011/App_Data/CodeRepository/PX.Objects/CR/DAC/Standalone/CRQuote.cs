using System.Collections.Generic;
using PX.Common;
using PX.Data.EP;
using System;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.TM;
using PX.Objects.TX;
using PX.Objects.AR;
using PX.Objects.PM;
using PX.Objects.GL;


namespace PX.Objects.CR.Standalone
{
    public partial class CRQuote : PX.Data.IBqlTable
    {
        #region Selected
        public abstract class selected : IBqlField { }

        [PXBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
        public virtual bool? Selected { get; set; }
        #endregion

		#region QuoteID
		public abstract class quoteID : PX.Data.IBqlField { }
		[PXDBGuid(IsKey = true)]
		[PXDefault(typeof(Standalone.CROpportunityRevision.noteID))]
		public virtual Guid? QuoteID { get; set; }
		#endregion

        #region QuoteNbr
        public abstract class quoteNbr : PX.Data.IBqlField
        {
        }
        protected String _QuoteNbr;
        [AutoNumber(typeof(CRSetup.quoteNumberingID), typeof(AccessInfo.businessDate))]
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXDefault]
        [PXUIField(DisplayName = "Quote Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PX.Data.EP.PXFieldDescription]
        public virtual String QuoteNbr
        {
            get
            {
                return this._QuoteNbr;
            }
            set
            {
                this._QuoteNbr = value;
            }
        }
		#endregion

        #region QuoteType
        public abstract class quoteType : PX.Data.IBqlField { }

        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Type", Visible = true)]
        [PXMassUpdatableField]
        [CRQuoteType()]
        [PXDefault(CRQuoteTypeAttribute.Distribution)]
        public virtual string QuoteType { get; set; }
        #endregion

		#region Subject
	    public abstract class subject : PX.Data.IBqlField { }
	    [PXDBString(255, IsUnicode = true)]
	    [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
	    [PXUIField(DisplayName = "Subject", Visibility = PXUIVisibility.SelectorVisible)]
	    [PXFieldDescription]
	    public virtual String Subject { get; set; }
	    #endregion

		#region ExpirationDate
		public abstract class expirationDate : PX.Data.IBqlField { }

        [PXDBDate()]        
        [PXMassUpdatableField]
        [PXUIField(DisplayName = "Expiration Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? ExpirationDate { get; set; }
        #endregion       
        
        #region Status
        public abstract class status : PX.Data.IBqlField { }

        [PXDBString(1, IsFixed = true)]
        [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible)]
        [PXMassUpdatableField]
        [CRQuoteStatus()]        
        [PXDefault]
        public virtual string Status { get; set; }
        #endregion

        #region NoteID
        public abstract class noteID : PX.Data.IBqlField { }
        [PXNote(
            DescriptionField = typeof(CRQuote.quoteNbr),
            Selector = typeof(CRQuote.quoteNbr)
        )]
        public virtual Guid? NoteID { get; set; }
        #endregion

        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField { }

        [PXDBTimestamp()]
        public virtual Byte[] tstamp { get; set; }
		#endregion

		#region TermsID
	    public abstract class termsID : PX.Data.IBqlField
	    {
	    }
	    protected String _TermsID;
	    [PXDefault(
			typeof(Search2<CustomerClass.termsID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>), 
			PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
	    [PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.SelectorVisible)]
	    [PXSelector(typeof(Search<Terms.termsID, Where<Terms.visibleTo, Equal<TermsVisibleTo.customer>, Or<Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof(Terms.descr), CacheGlobal = true)]
	    public virtual String TermsID
	    {
		    get
		    {
			    return this._TermsID;
		    }
		    set
		    {
			    this._TermsID = value;
		    }
	    }
	    #endregion

		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField { }

        [PXDBCreatedByScreenID()]
        public virtual String CreatedByScreenID { get; set; }
        #endregion

        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField { }

        [PXDBCreatedByID()]
        [PXUIField(DisplayName = "Created By")]
        public virtual Guid? CreatedByID { get; set; }
        #endregion

        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField { }

        [PXDBCreatedDateTimeUtc]
        [PXUIField(DisplayName = "Date Created", Enabled = false)]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion

        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField { }

        [PXDBLastModifiedByID()]
        [PXUIField(DisplayName = "Last Modified By")]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion

        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField { }

        [PXDBLastModifiedByScreenID()]
        public virtual String LastModifiedByScreenID { get; set; }
        #endregion

        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField { }

        [PXDBLastModifiedDateTimeUtc]
        [PXUIField(DisplayName = "Last Modified Date", Enabled = false)]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion    


    }
}
