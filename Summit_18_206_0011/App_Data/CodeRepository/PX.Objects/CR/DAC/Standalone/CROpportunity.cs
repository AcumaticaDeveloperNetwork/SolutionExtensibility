﻿using System.Collections.Generic;
using PX.Common;
using PX.Data.EP;
using System;
using System.Linq;
using System.Text;
using PX.Data;
using PX.Objects.CM;
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
	public partial class CROpportunity : PX.Data.IBqlTable
	{
		#region Selected
		public abstract class selected : IBqlField { }

		[PXBool]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Selected", Visibility = PXUIVisibility.Service)]
		public virtual bool? Selected { get; set; }
		#endregion		

		#region OpportunityID
		public abstract class opportunityID : PX.Data.IBqlField { }

		public const int OpportunityIDLength = 10;

		[PXDBString(OpportunityIDLength, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXUIField(DisplayName = "Opportunity ID", Visibility = PXUIVisibility.SelectorVisible)]
		[AutoNumber(typeof(CRSetup.opportunityNumberingID), typeof(AccessInfo.businessDate))]
		[PXSelector(typeof(Search3<CROpportunity.opportunityID,
				OrderBy<Desc<CROpportunity.opportunityID>>>),
			new[] { typeof(CROpportunity.opportunityID),
				typeof(CROpportunity.status),
				typeof(CROpportunity.closeDate),
				typeof(CROpportunity.stageID),
				typeof(CROpportunity.classID),
				},
			Filterable = true)]
		[PXFieldDescription]
		public virtual String OpportunityID { get; set; }
		#endregion

		#region ConvertedLeadID
		public abstract class convertedLeadID : IBqlField { }
		[PXDBInt]
		[PXUIField(DisplayName = "Source Lead", Enabled = false)]
		[PXSelector(typeof(Contact.contactID), DescriptionField = typeof(Contact.displayName))]
		public virtual Int32? ConvertedLeadID { get; set; }
		#endregion

		#region ClassID
		public abstract class classID : PX.Data.IBqlField { }

		[PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
		[PXUIField(DisplayName = "Class ID")]
		[PXDefault]
		[PXSelector(typeof(CROpportunityClass.cROpportunityClassID),
			DescriptionField = typeof(CROpportunityClass.description), CacheGlobal = true)]
		[PXMassUpdatableField]
		public virtual String ClassID { get; set; }
		#endregion

		#region Subject
		public abstract class subject : PX.Data.IBqlField { }
		[PXDBString(255, IsUnicode = true)]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Subject", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual String Subject { get; set; }
		#endregion

		#region Details
		public abstract class details : PX.Data.IBqlField { }

		[PXDBText(IsUnicode = true)]
		[PXUIField(DisplayName = "Details")]
		public virtual String Details { get; set; }
		#endregion

		#region CloseDate
		public abstract class closeDate : PX.Data.IBqlField { }

		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXMassUpdatableField]
		[PXUIField(DisplayName = "Estimation", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? CloseDate { get; set; }
		#endregion
		
		#region StageChangedDate
		public abstract class stageChangedDate : PX.Data.IBqlField { }

		[PXDBDate(PreserveTime = true)]
		[PXUIField(DisplayName = "Stage Change Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? StageChangedDate { get; set; }
		#endregion

		#region StageID
		public abstract class stageID : PX.Data.IBqlField { }

		[PXDBString(2)]
		[PXUIField(DisplayName = "Stage")]
		[CROpportunityStages(typeof(classID), typeof(stageChangedDate), OnlyActiveStages = true)]
		[PXDefault]
		[PXMassUpdatableField]
		public virtual String StageID { get; set; }
		#endregion

		#region MajorStatus
		public abstract class majorStatus : IBqlField { }

		private int? _MajorStatus;
		[PXDBInt]
		[OpportunityMajorStatusesAttribute]
		[PXDefault(OpportunityMajorStatusesAttribute._JUSTCREATED, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(Visible = false, DisplayName = "Major Status")]
		public virtual int? MajorStatus
		{
			get
			{
				return _MajorStatus;
			}
			set
			{
				_MajorStatus = value;
			}
		}

		#endregion

		#region Status
		public abstract class status : PX.Data.IBqlField { }

		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible)]
        [PXStringList(new string[0], new string[0])]
        [PXMassUpdatableField]
		[PXDefault()]
		public virtual string Status { get; set; }

		#endregion

		#region Resolution
		public abstract class resolution : PX.Data.IBqlField { }

		[PXDBString(2, IsFixed = true)]
		[PXStringList(new string[0], new string[0])]
		[PXUIField(DisplayName = "Reason")]
		[PXMassUpdatableField]
		public virtual String Resolution { get; set; }
		#endregion

		#region AssignDate
		public abstract class assignDate : IBqlField { }

		[PXDBDate(PreserveTime = true)]
		[PXUIField(DisplayName = "Assignment Date")]
		public virtual DateTime? AssignDate { get; set; }
		#endregion

		#region ClosingDate
		public abstract class closingDate : IBqlField { }

		[PXDBDate(PreserveTime = true)]
		[PXUIField(DisplayName = "Closing Date")]
		public virtual DateTime? ClosingDate { get; set; }
		#endregion
	
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField { }
		[PXNote(
			DescriptionField = typeof(CROpportunity.opportunityID),
			Selector = typeof(CROpportunity.opportunityID)
			)]
		public virtual Guid? NoteID { get; set; }
		#endregion

		#region Source
		public abstract class source : IBqlField { }

		[PXDBString(1, IsFixed = true)]
		[PXUIField(DisplayName = "Source", Visibility = PXUIVisibility.Visible, Visible = true)]
		[CRMSources]
		[PXMassUpdatableField]
		public virtual string Source { get; set; }
        #endregion

        #region ExternalRef
        public abstract class externalRef : IBqlField { }

        [PXDBString(255, IsFixed = true)]
        [PXUIField(DisplayName = "External Ref.")]
        public virtual string ExternalRef { get; set; }
        #endregion

        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField { }

		[PXDBTimestamp()]
		public virtual Byte[] tstamp { get; set; }
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
	
		#region DefQuoteID
		public abstract class defQuoteID : PX.Data.IBqlField
		{
		}

		[PXDBGuid]
		[PXDefault()]
		public virtual Guid? DefQuoteID { get; set; }
		#endregion
	}
}