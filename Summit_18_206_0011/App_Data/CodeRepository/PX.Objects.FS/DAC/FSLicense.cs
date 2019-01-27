using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    [PXCacheName(TX.TableName.LICENSE)]
    [PXPrimaryGraph(typeof(LicenseMaint))]
    public class FSLicense : PX.Data.IBqlTable
    {
        #region LicenseID
        /* Cache_Attached SM_EmployeeMaint */
        public abstract class licenseID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        [PXUIField(DisplayName = "License ID", Enabled = false)]
        public virtual int? LicenseID { get; set; }
        #endregion
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "License Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSLicense.refNbr))]
        [AutoNumber(typeof(Search<FSSetup.licenseNumberingID>),
                    typeof(AccessInfo.businessDate))]
        public virtual string RefNbr { get; set; }
        #endregion
        // Field does not allow null
        #region CertificateRequired
        public abstract class certificateRequired : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Require Certificate")]
        public virtual bool? CertificateRequired { get; set; }
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
        #region EmployeeID
        public abstract class employeeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Staff Member ID", Enabled = false)]        
        [FSSelector_StaffMember_Employee_Only]
        public virtual int? EmployeeID { get; set; }
        #endregion
        #region ExpirationDate
        public abstract class expirationDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault]
        [PXUIField(DisplayName = "Expiration Date")]
        public virtual DateTime? ExpirationDate { get; set; }
        #endregion
        #region InitialAmount
        public abstract class initialAmount : PX.Data.IBqlField
        {
        }

        [PXDBPriceCost]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Initial Price")]
        public virtual decimal? InitialAmount { get; set; }
        #endregion
        #region InitialTerm
        public abstract class initialTerm : PX.Data.IBqlField
        {
        }

        [PXDBShort(MinValue = 0, MaxValue = 999)]
        [PXDefault((short)0)]
        [PXUIField(DisplayName = "Initial Term")]
        public virtual short? InitialTerm { get; set; }
        #endregion
        #region InitialTermType
        public abstract class initialTermType : ListField_TermType
        {
        }

        [PXDBString(1, IsFixed = true)]
        [initialTermType.ListAtrribute]
        [PXDefault(ID.TermType.DAYS)]
        [PXUIField(DisplayName = "Initial Term Type")]
        public virtual string InitialTermType { get; set; }
        #endregion
        #region IssueByVendorID
        public abstract class issueByVendorID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [FSSelectorBusinessAccount_VE]
        [PXUIField(DisplayName = "Issuing Vendor ID")]
        public virtual int? IssueByVendorID { get; set; }
        #endregion
        #region IssueDate
        public abstract class issueDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault]
        [PXUIField(DisplayName = "Issue Date")]
        public virtual DateTime? IssueDate { get; set; }
        #endregion
        #region IssuingAgencyName
        public abstract class issuingAgencyName : PX.Data.IBqlField
        {
        }

        [PXDBString(60, IsUnicode = true)]
        [PXDefault]
        [PXFormula(typeof(Selector<issueByVendorID, BAccountSelectorBase.acctName>))]
        [PXUIField(DisplayName = "Issuing Agency Name")]
        public virtual string IssuingAgencyName { get; set; }
        #endregion
        #region LicenseTypeID
        public abstract class licenseTypeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault]
        [PXUIField(DisplayName = "License Type")]
        [PXSelector(typeof(FSLicenseType.licenseTypeID), SubstituteKey = typeof(FSLicenseType.licenseTypeCD), DescriptionField = typeof(FSLicense.descr))]
        public virtual int? LicenseTypeID { get; set; }
        #endregion
        #region RenewalAmount
        public abstract class renewalAmount : PX.Data.IBqlField
        {
        }

        [PXDBPriceCost]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Renewal Price")]
        public virtual decimal? RenewalAmount { get; set; }
        #endregion
        #region RenewalTerm
        public abstract class renewalTerm : PX.Data.IBqlField
        {
        }

        [PXDBShort(MinValue = 0, MaxValue = 999)]
        [PXDefault((short)0)]
        [PXUIField(DisplayName = "Renewal Term")]
        public virtual short? RenewalTerm { get; set; }
        #endregion
        #region RenewalTermType
        public abstract class renewalTermType : ListField_TermType
        {
        }

        [PXDBString(1, IsFixed = true)]
        [renewalTermType.ListAtrribute]
        [PXDefault(ID.TermType.DAYS)]
        [PXUIField(DisplayName = "Renewal Term Type")]
        public virtual string RenewalTermType { get; set; }
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
        [PXUIField(DisplayName = "CreatedByID")]
        public virtual Guid? CreatedByID { get; set; }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "CreatedByScreenID")]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = "CreatedDateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "LastModifiedByID")]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "LastModifiedByScreenID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = "LastModifiedDateTime")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
    }
}
