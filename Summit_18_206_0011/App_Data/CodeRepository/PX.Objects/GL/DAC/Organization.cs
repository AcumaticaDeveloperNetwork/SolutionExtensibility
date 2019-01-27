using System;
using PX.Data;
using PX.Objects.CR;
using PX.SM;
using PX.Objects.CS;
using PX.Objects.CS.DAC;
using PX.Objects.GL.FinPeriods;

namespace PX.Objects.GL.DAC
{
    [PXCacheName(CS.Messages.Company)]
	[Serializable]
	public partial class Organization : IBqlTable, IIncludable
    {
	    #region Selected
	    public abstract class selected : IBqlField
	    {
	    }
	    protected bool? _Selected = false;
	    [PXBool]
	    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
	    [PXUIField(DisplayName = "Selected")]
	    public virtual bool? Selected
	    {
		    get
		    {
			    return _Selected;
		    }
		    set
		    {
			    _Selected = value;
		    }
	    }
	    #endregion
		#region OrganizationID
		public abstract class organizationID : IBqlField { }

		[PXDBIdentity]
        public virtual int? OrganizationID { get; set; }
		#endregion
		#region OrganizationCD
		public abstract class organizationCD : IBqlField { }

		[PXDimension("BIZACCT")]
        [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXUIField(DisplayName = "Company ID")]
		[PXDBDefault(typeof(OrganizationBAccount.acctCD))]
        public virtual string OrganizationCD { get; set; }
        #endregion
        #region OrganizationType
        public abstract class organizationType : IBqlField { }

        [PXDBString(15)]
        [OrganizationTypes.List]
		[PXDefault(OrganizationTypes.WithoutBranches)]
		[PXUIField(DisplayName = "Company Type")]
        public virtual string OrganizationType { get; set; }
		#endregion

		#region FileTaxesByBranches
		public abstract class fileTaxesByBranches : IBqlField { }

	    [PXDBBool]
	    [PXDefault(false)]
		[PXUIField(DisplayName = "File Taxes by Branches")]
	    public virtual bool? FileTaxesByBranches { get; set; }
	    #endregion
		

		// duplicate fields from the Branch DAC
		#region ActualLedgerID
		public abstract class actualLedgerID : IBqlField { }

        [PXDBInt()]
        public virtual int? ActualLedgerID { get; set; }
        #endregion
        #region ActualLedgerCD
        public abstract class actualLedgerCD : IBqlField { }

        [PXString(10, IsUnicode = true)]
        public virtual string ActualLedgerCD { get; set; }
        #endregion
        #region Active
        public abstract class active : IBqlField { }

        [PXDBBool()]
        [PXUIField(DisplayName = "Active", FieldClass = "BRANCH")]
		[PXDefault(true)]
        public virtual bool? Active { get; set; }
		#endregion
		#region OrganizationName
		public abstract class organizationName : PX.Data.IBqlField
		{
		}

	    /// <summary>
	    /// The name of the organization.
	    /// </summary>
	    [PXDBString(60, IsUnicode = true)]
	    [PXUIField(DisplayName = "Company Name", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
	    public virtual String OrganizationName { get; set; }
	    #endregion
		#region RoleName
		public abstract class roleName : IBqlField { }

        /// <summary>
        /// The name of the <see cref="Roles">Role</see> to be used to grant users access to the data of the Organization. 
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Roles.Rolename"/> field.
        /// </value>
		[PXDBString(64, IsUnicode = true, InputMask = "")]
        [PXSelector(typeof(Search<Roles.rolename, Where<Roles.guest, Equal<False>>>), DescriptionField = typeof(Roles.descr))]
        [PXUIField(DisplayName = "Access Role")]
        public virtual string RoleName { get; set; }
        #endregion
        #region PhoneMask
        public abstract class phoneMask : IBqlField { }

        /// <summary>
        /// The mask used to display phone numbers for the objects, which belong to this Organization.
        /// See also the <see cref="Company.PhoneMask"/>.
        /// </summary>
		[PXDBString(50)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Phone Mask")]
        public virtual string PhoneMask { get; set; }
        #endregion
        #region CountryID
        public abstract class countryID : IBqlField { }

        /// <summary>
        /// Identifier of the default Country of the Organization.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="Country.CountryID"/> field.
        /// </value>
		[PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "Default Country")]
        [PXSelector(typeof(Country.countryID), DescriptionField = typeof(Country.description))]
        public virtual string CountryID { get; set; }
		#endregion
		#region DefaultPrinter
		public abstract class defaultPrinter : PX.Data.IBqlField
		{
		}
		protected String _DefaultPrinter;
		[PXPrinterSelector(DisplayName = "Default Printer", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string DefaultPrinter
		{
			get
			{
				return this._DefaultPrinter;
			}
			set
			{
				this._DefaultPrinter = value;
			}
		}
		#endregion

		// "General Info" tab
		#region BAccountID
		public abstract class bAccountID : IBqlField { }

        /// <summary>
        /// Identifier of the <see cref="BAccount">Business Account</see> of the Organization.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="BAccount.BAccountID"/> field.
        /// </value>
        [PXDBInt()]
        [PXUIField(Visible = true, Enabled = false)]
        [PXSelector(typeof(CR.BAccountR.bAccountID), ValidateValue = false)]
		[PXDBDefault(typeof(OrganizationBAccount.bAccountID))]
        public virtual int? BAccountID { get; set; }
        #endregion

        // "Logo" tab
        #region LogoName
        public abstract class logoName : IBqlField { }

        /// <summary>
        /// The name of the logo image file.
        /// </summary>
		[PXDBString(IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Logo File")]
        public string LogoName { get; set; }

	    [PXString]
	    [PXUIField(DisplayName = "Logo File")]
	    public string LogoNameGetter { get { return LogoName; } set { } }
		#endregion
		#region LogoNameReport
		public abstract class logoNameReport : IBqlField { }

        /// <summary>
        /// The name of the report logo image file.
        /// </summary>
		[PXDBString(IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Report Logo File")]
        public string LogoNameReport { get; set; }

	    [PXString]
	    [PXUIField(DisplayName = "Logo File")]
	    public string LogoNameReportGetter { get { return LogoNameReport; } set { } }
		#endregion

		// "1099 Settings" tab
		#region TCC
		public abstract class tCC : IBqlField { }

        /// <summary>
        /// Transmitter Control Code (TCC) for the 1099 form.
        /// </summary>
        [PXDBString(5, IsUnicode = true)]
        [PXUIField(DisplayName = "Transmitter Control Code (TCC)")]
        public virtual string TCC { get; set; }
        #endregion
        #region ForeignEntity
        public abstract class foreignEntity : IBqlField { }

        /// <summary>
        /// Indicates whether the Organization is considered a Foreign Entity in the context of 1099 form.
        /// </summary>
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Foreign Entity")]
        public virtual bool? ForeignEntity { get; set; }
        #endregion
        #region CFSFiler
        public abstract class cFSFiler : IBqlField { }

        /// <summary>
        /// Combined Federal/State Filer for the 1099 form.
        /// </summary>
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Combined Federal/State Filer")]
        public virtual bool? CFSFiler { get; set; }
        #endregion
        #region ContactName
        public abstract class contactName : IBqlField { }

        /// <summary>
        /// Contact Name for the 1099 form.
        /// </summary>
		[PXDBString(40, IsUnicode = true)]
        [PXUIField(DisplayName = "Contact Name")]
        public virtual string ContactName { get; set; }
        #endregion
        #region CTelNumber
        public abstract class cTelNumber : IBqlField { }

        /// <summary>
        /// Contact Phone Number for the 1099 form.
        /// </summary>
		[PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Contact Telephone Number")]
        public virtual string CTelNumber { get; set; }
        #endregion
        #region CEmail
        public abstract class cEmail : IBqlField { }

        /// <summary>
        /// Contact E-mail for the 1099 form.
        /// </summary>
		[PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Contact E-mail")]
        public virtual string CEmail { get; set; }
        #endregion
        #region NameControl
        public abstract class nameControl : IBqlField { }

        /// <summary>
        /// Name Control for the 1099 form.
        /// </summary>
        [PXDBString(4, IsUnicode = true)]
        [PXUIField(DisplayName = "Name Control")]
        public virtual string NameControl { get; set; }
        #endregion
        #region Reporting1099
        public abstract class reporting1099 : IBqlField { }

        [PXDBBool]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "1099-MISC Reporting Entity")]
        public virtual bool? Reporting1099 { get; set; }
		#endregion

		// Technical fields
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField { }

		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField { }

		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID { get; set; }
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField { }

		[PXDBCreatedDateTime]
		[PXUIField(
			DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime,
			Enabled = false,
			IsReadOnly = true)]
		public virtual DateTime? CreatedDateTime { get; set; }
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField { }

		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField { }

		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField { }

		[PXDBLastModifiedDateTime]
		[PXUIField(
			DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime,
			Enabled = false,
			IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		#endregion

		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField{}

        [PXDBTimestamp()]
        public virtual Byte[] tstamp { get; set; }
		#endregion

		//TODO 444 Pank Org GroupMask: I think it should be eliminated 
		#region GroupMask
		public abstract class groupMask : IBqlField { }
        protected Byte[] _GroupMask;

        /// <summary>
        /// The group mask showing which <see cref="PX.SM.RelationGroup">restriction groups</see> the Branch belongs to.
        /// To learn more about the way restriction groups are managed, see the documentation for the GL Account Access (GL.10.40.00) screen
        /// (corresponds to the <see cref="GLAccess"/> graph).
        /// </summary>
        [PXDBGroupMask()]
        public virtual Byte[] GroupMask { get; set; }
        #endregion
        #region Included
        public abstract class included : PX.Data.IBqlField{}

        /// <summary>
        /// An unbound field used in the User Interface to include the Organization into a <see cref="PX.SM.RelationGroup">restriction group</see>.
        /// </summary>
        [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXBool]
        [PXUIField(DisplayName = "Included")]
        public virtual bool? Included { get; set; }
		#endregion
		#region NoteID
	    public abstract class noteID : PX.Data.IBqlField{}

	    [PXNote]
	    public virtual Guid? NoteID { get; set; }
	    #endregion
	}
}
