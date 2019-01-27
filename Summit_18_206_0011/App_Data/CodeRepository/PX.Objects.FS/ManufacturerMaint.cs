using System.Collections;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    public class ManufacturerMaint : PXGraph<ManufacturerMaint, FSManufacturer>
    {
        public PXSelect<FSManufacturer> ManufacturerRecords;
        
        #region Action Buttons
                
        public PXAction<FSManufacturer> viewMainOnMap;
        [PXUIField(DisplayName = "View on Map", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewMainOnMap(PXAdapter adapter)
        {
            var googleMap = new PX.Data.GoogleMapRedirector();

            Country countryRow = PXSelect<Country,
                                    Where<
                                        Country.countryID, Equal<Current<FSManufacturer.countryID>>>>
                                    .Select(this);
            State stateRow = PXSelect<State,
                                Where<
                                    State.stateID, Equal<Current<FSManufacturer.state>>>>
                                .Select(this);

            if (countryRow != null)
            {
                string country = countryRow.Description;
                string state = string.Empty;
                if (stateRow != null)
                {
                    state = stateRow.Name;
                }

                string city = ManufacturerRecords.Current.City;
                string postalcode = ManufacturerRecords.Current.PostalCode;
                string addressline1 = ManufacturerRecords.Current.AddressLine1;
                string addressline2 = ManufacturerRecords.Current.AddressLine2;
                string addressline3 = ManufacturerRecords.Current.AddressLine3;

                googleMap.ShowAddress(country, state, city, postalcode, addressline1, addressline2, addressline3);
            }

            return adapter.Get();
        }
        #endregion
        #region PrivateFunctions
        /// <summary>
        /// Set the Address and Contact fields of the Contact selected (in the ContactID field).
        /// </summary>
        /// <param name="fsManufacturerRow">FSManufacturer row.</param>
        public void SetContactInfo(FSManufacturer fsManufacturerRow)
        {
            PXResult<Contact, Address> bqlResult;

            bqlResult = (PXResult<Contact, Address>)
                                   PXSelectJoin<Contact,
                                       LeftJoin<Address,
                                           On<
                                               Address.bAccountID, Equal<Contact.bAccountID>,
                                               And<Address.addressID, Equal<Contact.defAddressID>>>>,
                                   Where<
                                       Contact.contactID, Equal<Required<Contact.contactID>>>>
                                   .Select(this, fsManufacturerRow.ContactID);
            fsManufacturerRow.Salutation = null;
            fsManufacturerRow.AddressLine1 = null;
            fsManufacturerRow.AddressLine2 = null;
            fsManufacturerRow.AddressLine3 = null;
            fsManufacturerRow.City = null;
            fsManufacturerRow.CountryID = null;
            fsManufacturerRow.State = null;
            fsManufacturerRow.PostalCode = null;
            fsManufacturerRow.IsValidated = null;

            if (bqlResult != null)
            {
                Address adressRow = (Address)bqlResult;
                Contact contactRow = (Contact)bqlResult;

                fsManufacturerRow.Salutation = contactRow.Attention;
                fsManufacturerRow.AddressLine1 = adressRow.AddressLine1;
                fsManufacturerRow.AddressLine2 = adressRow.AddressLine2;
                fsManufacturerRow.AddressLine3 = adressRow.AddressLine3;
                fsManufacturerRow.City = adressRow.City;
                fsManufacturerRow.CountryID = adressRow.CountryID;
                fsManufacturerRow.State = adressRow.State;
                fsManufacturerRow.PostalCode = adressRow.PostalCode;
                fsManufacturerRow.IsValidated = adressRow.IsValidated;
            }
        }

        #endregion
        #region ManufacturerEventHanldlers
        
        protected virtual void FSManufacturer_ContactID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            FSManufacturer fsManufacturerRow = (FSManufacturer)e.Row;
            SetContactInfo(fsManufacturerRow);
        }

        #endregion
    }
}