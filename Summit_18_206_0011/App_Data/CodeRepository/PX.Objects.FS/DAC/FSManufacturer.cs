using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.GL;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    [PXCacheName(TX.TableName.MANUFACTURER)]
    [PXPrimaryGraph(typeof(ManufacturerMaint))]
    public class FSManufacturer : PX.Data.IBqlTable
    {
        #region ManufacturerID
        public abstract class manufacturerID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        [PXUIField(Enabled = false)]
        public virtual int? ManufacturerID { get; set; }
        #endregion
        #region ManufacturerCD
        public abstract class manufacturerCD : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXDefault]
        [NormalizeWhiteSpace]
        [PXUIField(DisplayName = "Manufacturer ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSManufacturer.manufacturerCD))]
        public virtual string ManufacturerCD { get; set; }
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
        #region City
        public abstract class city : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "City")]
        public virtual string City { get; set; }
        #endregion
        #region ContactID
        public abstract class contactID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Contact ID")]
        [PXSelector(typeof(Search2<Contact.contactID,
                            InnerJoin<BAccount,
                                On<BAccount.bAccountID, Equal<Contact.bAccountID>>>,
                             Where<
                                Contact.contactType, NotEqual<ContactTypesAttribute.bAccountProperty>,
                            And<
                                Where<
                                    BAccount.type, Equal<BAccountType.customerType>,
                                    Or<BAccount.type, Equal<BAccountType.prospectType>,
                                    Or<BAccount.type, Equal<BAccountType.combinedType>,
                                    Or<BAccount.type, Equal<BAccountType.vendorType>>>>>>>>), 
                            new Type[]
                            {
                                typeof(Contact.displayName),
                                typeof(Contact.salutation),
                                typeof(Contact.fullName),
                                typeof(Contact.eMail),
                                typeof(Contact.phone1),
                                typeof(BAccount.type)
                            },
                            DescriptionField = typeof(Contact.displayName))]
        public virtual int? ContactID { get; set; }
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
        #region IsValidated
        public abstract class isValidated : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Validated")]
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
        [PXFormula(typeof(Default<FSManufacturer.countryID>))]
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
        [State(typeof(FSManufacturer.countryID), DescriptionField = typeof(State.name))]
        [PXUIField(DisplayName = "State")]
        [PXFormula(typeof(Default<FSManufacturer.countryID>))]
        public virtual string State { get; set; }
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