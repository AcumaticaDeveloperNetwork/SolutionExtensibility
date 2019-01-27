using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    [PXCacheName(TX.TableName.BRANCH_LOCATION)]
    [PXPrimaryGraph(typeof(BranchLocationMaint))]
    public class FSBranchLocation : PX.Data.IBqlTable
    {
        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        [PXUIField(Enabled = false)]
        public virtual int? BranchLocationID { get; set; }
        #endregion
        #region BranchLocationCD
        public abstract class branchLocationCD : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXSelector(typeof(FSBranchLocation.branchLocationCD))]
        [PXUIField(DisplayName = "Branch Location ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXDefault]
        [NormalizeWhiteSpace]
        public virtual string BranchLocationCD { get; set; }
        #endregion
        #region AddressLine1
        public abstract class addressLine1 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Address Line 1")]
        public virtual string AddressLine1 { get; set; }
        #endregion
        #region AddressLine2
        public abstract class addressLine2 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Address Line 2")]
        public virtual string AddressLine2 { get; set; }
        #endregion
        #region AddressLine3
        public abstract class addressLine3 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Address Line 3")]
        public virtual string AddressLine3 { get; set; }
        #endregion
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(AccessInfo.branchID))]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(Branch.branchID), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.acctName))]
        public virtual int? BranchID { get; set; }
        #endregion
        #region City
        public abstract class city : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "City")]
        public virtual string City { get; set; }
        #endregion
        #region CountryID
        public abstract class countryID : PX.Data.IBqlField
        {
        }

        [PXDefault(typeof(Search<Branch.countryID, Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "Country")]
        [Country]
        public virtual string CountryID { get; set; }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
        {
        }

        [PXDBString(60, IsUnicode = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string Descr { get; set; }
        #endregion
        #region EMail
        public abstract class eMail : PX.Data.IBqlField
        {
        }

        [PXDBEmail]
        [PXUIField(DisplayName = "Email")]
        public virtual string EMail { get; set; }
        #endregion
        #region Fax
        public abstract class fax : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PhoneValidation]
        [PXUIField(DisplayName = "Fax")]
        public virtual string Fax { get; set; }
        #endregion
        // Field does not allow null
        #region IsValidated
        public abstract class isValidated : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Address Validated", Enabled = false)]
        [PXFormula(typeof(Default<FSBranchLocation.addressLine1>))]
        [PXFormula(typeof(Default<FSBranchLocation.addressLine2>))]
        [PXFormula(typeof(Default<FSBranchLocation.postalCode>))]
        [PXFormula(typeof(Default<FSBranchLocation.countryID>))]
        [PXFormula(typeof(Default<FSBranchLocation.city>))]
        [PXFormula(typeof(Default<FSBranchLocation.state>))]
        public virtual bool? IsValidated { get; set; }
        #endregion
        #region Phone1
        public abstract class phone1 : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PXUIField(DisplayName = "Phone 1")]
        [PhoneValidation]
        public virtual string Phone1 { get; set; }
        #endregion
        #region Phone2
        public abstract class phone2 : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PhoneValidation]
        [PXUIField(DisplayName = "Phone 2")]
        public virtual string Phone2 { get; set; }
        #endregion
        #region Phone3
        public abstract class phone3 : PX.Data.IBqlField
        {
        }

        [PXDBString(50)]
        [PhoneValidation]
        [PXUIField(DisplayName = "Phone 3")]
        public virtual string Phone3 { get; set; }
        #endregion
        #region PostalCode
        public abstract class postalCode : PX.Data.IBqlField
        {
        }

        [PXDBString(20)]
        [PXUIField(DisplayName = "Postal Code")]
        [PXFormula(typeof(Default<FSBranchLocation.countryID>))]
        public virtual string PostalCode { get; set; }
        #endregion
        #region Salutation
        public abstract class salutation : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
	    [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Attention")]
        public virtual string Salutation { get; set; }
        #endregion
        #region State
        public abstract class state : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [State(typeof(FSBranchLocation.countryID), DescriptionField = typeof(State.name))]
        [PXUIField(DisplayName = "State")]
        [PXFormula(typeof(Default<FSBranchLocation.countryID>))]
        public virtual string State { get; set; }
        #endregion
        #region SubID
        public abstract class subID : PX.Data.IBqlField
        {
        }

        [SubAccount(DisplayName = "General Subaccount")]
        public virtual int? SubID { get; set; }
        #endregion
        #region WebSite
        public abstract class webSite : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
        [PXMassMergableField]
        [PXUIField(DisplayName = "Web")]
        public virtual string WebSite { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "NoteID")]
        [PXNote(new Type[0])]
        public virtual Guid? NoteID { get; set; }
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
        [PXUIField(DisplayName = "Created On")]
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
        [PXUIField(DisplayName = "Last Modified On")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
        #region DfltSiteID
        public abstract class dfltSiteID : PX.Data.IBqlField
        {
        }

        [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
        [Site(DisplayName = "Default Warehouse", DescriptionField = typeof(INSite.descr))]
        public virtual int? DfltSiteID { get; set; }
        #endregion
        #region DfltSubItemID
        public abstract class dfltSubItemID : PX.Data.IBqlField
        {
        }

        [SubItem(DisplayName = "Default Subitem")]
        public virtual int? DfltSubItemID { get; set; }
        #endregion
        #region DfltUOM
        public abstract class dfltUOM : PX.Data.IBqlField
        {
        }

        [INUnit(DisplayName = "Default Unit")]
        public virtual string DfltUOM { get; set; }
        #endregion

        #region RoomFeatureEnabled
        public abstract class roomFeatureEnabled : IBqlField
        {
        }

        [PXBool]
        [PXFormula(typeof(Current<FSSetup.manageRooms>))]
        [PXUIField(Visible = false)]
        public virtual bool? RoomFeatureEnabled { get; set; }
        #endregion
    }
}
