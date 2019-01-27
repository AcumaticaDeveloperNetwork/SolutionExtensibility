using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;

namespace PX.Objects.FS
{
    [Serializable]
    [PXPrimaryGraph(typeof(AppointmentEntry))]
    [PXProjection(typeof(
        Select2<FSAppointment,
                InnerJoin<FSServiceOrder,
                    On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>,
                CrossJoinSingleTable<FSSetup>>>))]
    public partial class FSAppointmentInRoute : FSAppointment
    {
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt(BqlField = typeof(FSServiceOrder.customerID))]
        [PXUIField(DisplayName = "Customer ID")]
        [FSSelectorCustomer]
        public virtual int? CustomerID { get; set; }
        #endregion

        #region LocationID
        public abstract class locationID : PX.Data.IBqlField
        {
        }

        [LocationID(BqlField = typeof(FSServiceOrder.locationID), DisplayName = "Location ID", DescriptionField = typeof(Location.descr))]
        public virtual int? LocationID { get; set; }
        #endregion

        #region State
        public abstract class state : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.state))]
        [PXUIField(DisplayName = "State")]
        [State(typeof(FSServiceOrder.countryID), DescriptionField = typeof(State.name))]
        public virtual string State { get; set; }
        #endregion

        #region AddressLine1
        public abstract class addressLine1 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.addressLine1))]
        [PXUIField(DisplayName = "Address Line 1")]
        public virtual string AddressLine1 { get; set; }
        #endregion

        #region AddressLine2
        public abstract class addressLine2 : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.addressLine2))]
        [PXUIField(DisplayName = "Address Line 2")]
        public virtual string AddressLine2 { get; set; }
        #endregion

        #region PostalCode
        public abstract class postalCode : PX.Data.IBqlField
        {
        }

        [PXDBString(20, BqlField = typeof(FSServiceOrder.postalCode))]
        [PXUIField(DisplayName = "Postal code")]
        [PXZipValidation(typeof(Country.zipCodeRegexp), typeof(Country.zipCodeMask), typeof(Address.countryID))]
        [PXDynamicMask(typeof(Search<Country.zipCodeMask, Where<Country.countryID, Equal<Current<FSServiceOrder.countryID>>>>))]
        [PXFormula(typeof(Default<FSServiceOrder.countryID>))]
        public virtual string PostalCode { get; set; }
        #endregion

        #region City
        public abstract class city : PX.Data.IBqlField
        {
        }

        [PXDBString(50, IsUnicode = true, BqlField = typeof(FSServiceOrder.city))]
        [PXUIField(DisplayName = "City")]
        public virtual string City { get; set; }
        #endregion

        #region MapApiKey
        public abstract class mapApiKey : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true, BqlField = typeof(FSSetup.mapApiKey))]
        [PXUIField(DisplayName = "Map API Key")]
        public virtual string MapApiKey { get; set; }
        #endregion
    }
}