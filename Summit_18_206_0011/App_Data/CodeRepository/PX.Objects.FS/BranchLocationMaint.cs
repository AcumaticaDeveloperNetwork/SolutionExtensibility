using System.Collections;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;

namespace PX.Objects.FS
{
    public class BranchLocationMaint : PXGraph<BranchLocationMaint, FSBranchLocation>
    {
        [PXHidden]
        public PXSetup<FSSetup> SetupRecord;

        public PXSelect<FSBranchLocation> BranchLocationRecords;

        public PXSelect<FSRoom, 
                        Where<
                            FSRoom.branchLocationID, Equal<Current<FSBranchLocation.branchLocationID>>>> 
                        RoomRecords;

        public PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Current<FSBranchLocation.branchID>>>> Branch;

        #region CacheAttached
        #region FSRoom_RoomID
        [PXDefault]
        [PXDBString(10, IsUnicode = true, InputMask = ">AAAAAAAAAA")]
        [PXUIField(DisplayName = "Room ID", Visibility = PXUIVisibility.SelectorVisible)]
        protected virtual void FSRoom_RoomID_CacheAttached(PXCache sender)
        {
        }
        #endregion
        #endregion

        #region Action Buttons
        #region ViewOnMap
        public PXAction<FSBranchLocation> viewMainOnMap;
        [PXUIField(DisplayName = "View on Map", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewMainOnMap(PXAdapter adapter)
        {
            var googleMap = new PX.Data.GoogleMapRedirector();

            Country countryRow = PXSelect<Country,
                                    Where<
                                        Country.countryID, Equal<Current<FSBranchLocation.countryID>>>>
                                    .Select(this);
            State stateRow = PXSelect<State,
                                Where<
                                    State.stateID, Equal<Current<FSBranchLocation.state>>>>
                                .Select(this);

            if (countryRow != null)
            {
                string country = countryRow.Description;
                string state = string.Empty;
                if (stateRow != null)
                {
                    state = stateRow.Name;
                }

                string city = BranchLocationRecords.Current.City;
                string postalcode = BranchLocationRecords.Current.PostalCode;
                string addressline1 = BranchLocationRecords.Current.AddressLine1;
                string addressline2 = BranchLocationRecords.Current.AddressLine2;
                string addressline3 = BranchLocationRecords.Current.AddressLine3;

                googleMap.ShowAddress(country, state, city, postalcode, addressline1, addressline2, addressline3);
            }

            return adapter.Get();
        }
        #endregion
        #region Open Room
        [PXViewDetailsButton(typeof(FSBranchLocation))]
        public PXAction<FSBranchLocation> openRoom;
        [PXUIField(DisplayName = "Open Room", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable OpenRoom(PXAdapter adapter)
        {
            FSRoom fsRoomRow = RoomRecords.Current;

            if (fsRoomRow != null)
            {
                RoomMaint graphRoomServiceClassMaint = PXGraph.CreateInstance<RoomMaint>();
                graphRoomServiceClassMaint.RoomRecords.Current = graphRoomServiceClassMaint.RoomRecords.Search<FSRoom.roomID>(fsRoomRow.RoomID, fsRoomRow.BranchLocationID);

                throw new PXRedirectRequiredException(graphRoomServiceClassMaint, null) { Mode = PXBaseRedirectException.WindowMode.NewWindow };
            }

            return adapter.Get();
        }
        #endregion
        #region Validate Address
        public PXAction<FSBranchLocation> validateAddress;
        [PXUIField(DisplayName = "Validate", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ValidateAddress(PXAdapter adapter)
        {
            if (BranchLocationRecords.Current != null && BranchLocationRecords.Current.IsValidated == false)
            {
                if (BranchLocationRecords.Current.CountryID != null && BranchLocationRecords.Current.City != null
                    && BranchLocationRecords.Current.State != null && BranchLocationRecords.Current.PostalCode != null)
                {
                    string addressString = ServiceOrderCore.GetAddress(
                                                                    BranchLocationRecords.Current.AddressLine1,
                                                                    BranchLocationRecords.Current.AddressLine2,
                                                                    BranchLocationRecords.Current.City,
                                                                    BranchLocationRecords.Current.State,
                                                                    BranchLocationRecords.Current.CountryID,
                                                                    BranchLocationRecords.Current.PostalCode);

                    if (Geocoder.GetStatus(addressString, SetupRecord.Current.MapApiKey).CompareTo(System.Net.HttpStatusCode.OK.ToString()) == 0)
                    {
                        BranchLocationRecords.Cache.SetValue<FSBranchLocation.isValidated>(BranchLocationRecords.Current, true);
                    }
                    else
                    {
                        throw new PXException(TX.Error.ADDRESS_VALIDATION_FAILED);
                    }
                }
                else
                {
                    throw new PXException(TX.Error.VALIDATE_ADDRESS_MISSING_FIELDS);
                }
            }

            return adapter.Get();
        }
        #endregion
        #endregion

        #region FSBranchLocationEventHanldlers
        protected void FSBranchLocation_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            FSBranchLocation fsBranchLocationRow = (FSBranchLocation)e.Row;

            if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>()
                    && !PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
            {
                fsBranchLocationRow.DfltSiteID = null;
            }
        }

        protected void FSBranchLocation_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            FSBranchLocation fsBranchLocationRow = (FSBranchLocation)e.Row;
            EnableDisable_ActionButtons(cache, fsBranchLocationRow);

            PXDefaultAttribute.SetPersistingCheck<FSBranchLocation.dfltSiteID>(
                cache,
                fsBranchLocationRow,
                GetPersistingCheckValueForDfltSiteID(PXAccess.FeatureInstalled<FeaturesSet.warehouse>()
                                                        || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>()));
        }

        protected virtual void FSRoom_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            FSRoom fsRoomRow = (FSRoom)e.Row;

            if (fsRoomRow.RoomID != null && !string.IsNullOrEmpty(fsRoomRow.Descr))
            {
                PXUIFieldAttribute.SetEnabled<FSRoom.roomID>(cache, fsRoomRow, false);
                PXUIFieldAttribute.SetEnabled<FSRoom.descr>(cache, fsRoomRow, false);
                PXUIFieldAttribute.SetEnabled<FSRoom.floorNbr>(cache, fsRoomRow, false);
            }
        }

        protected virtual void FSRoom_RoomID_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            FSRoom fsRoomRow = (FSRoom)e.Row;
            FSRoom fsRoom_tmp = PXSelect<FSRoom,
                                Where<
                                    FSRoom.branchLocationID, Equal<Required<FSRoom.branchLocationID>>,
                                    And<FSRoom.roomID, Equal<Required<FSRoom.roomID>>>>>
                                .Select(cache.Graph, fsRoomRow.BranchLocationID, e.NewValue);

            if (fsRoom_tmp != null)
            { 
                e.Cancel = true;
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// The Action buttons get enabled or disabled.
        /// </summary>
        private void EnableDisable_ActionButtons(PXCache cache, FSBranchLocation fsBranchLocationRow)
        {
            //Validate address action
            if (BranchLocationRecords.Current != null)
            {
                if (BranchLocationRecords.Current.CountryID != null && BranchLocationRecords.Current.City != null
                    && BranchLocationRecords.Current.State != null && BranchLocationRecords.Current.PostalCode != null
                    && BranchLocationRecords.Current.IsValidated == false)
                {
                    validateAddress.SetEnabled(true);
                }
                else
                {
                    validateAddress.SetEnabled(false);
                }
            }
        }

        /// <summary>
        /// Checks if the distribution module is enable and return the corresponding PersistingCheck value.
        /// </summary>
        /// <returns>PXPersistingCheck.NullOrBlank is the distribution module is enabled otherwise returns PXPersistingCheck.Nothing.</returns>
        private PXPersistingCheck GetPersistingCheckValueForDfltSiteID(bool isDistributionModuleEnabled)
        {
            return isDistributionModuleEnabled ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing; 
        }

        #endregion
    }
}