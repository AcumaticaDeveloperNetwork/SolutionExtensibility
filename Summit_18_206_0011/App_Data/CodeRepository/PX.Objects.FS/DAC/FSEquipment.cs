using PX.Data;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

using CRLocation = PX.Objects.CR.Standalone.Location;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.EQUIPMENT)]
    [PXPrimaryGraph(typeof(SMEquipmentMaint))]
    public class FSEquipment : PX.Data.IBqlTable
    {
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Equipment Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
        [FSSelectorSMEquipmentRefNbr]
        [PX.Data.EP.PXFieldDescription]
        public virtual string RefNbr { get; set; }
        #endregion
        #region SMEquipmentID
        public abstract class SMequipmentID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        public virtual int? SMEquipmentID { get; set; }
        #endregion
        #region Barcode
        public abstract class barcode : PX.Data.IBqlField
        {
        }

        [PXDBString(20, IsUnicode = true)]
        [PXUIField(DisplayName = "Barcode")]
        public virtual string Barcode { get; set; }
        #endregion
        #region IsVehicle
        public abstract class isVehicle : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Vehicle", Enabled = false)]
        public virtual bool? IsVehicle { get; set; }
        #endregion
        #region ManufacturerID
        public abstract class manufacturerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Manufacturer", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSManufacturer.manufacturerID),
                        SubstituteKey = typeof(FSManufacturer.manufacturerCD),
                        DescriptionField = typeof(FSManufacturer.descr))]
        public virtual int? ManufacturerID { get; set; }
        #endregion
        #region ManufacturerModelID
        public abstract class manufacturerModelID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Manufacturer Model", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(Search<FSManufacturerModel.manufacturerModelID,
                                Where<
                                    FSManufacturerModel.manufacturerID, Equal<Current<FSEquipment.manufacturerID>>>>),
                            SubstituteKey = typeof(FSManufacturerModel.manufacturerModelCD),
                            DescriptionField = typeof(FSManufacturerModel.descr))]
        [PXFormula(typeof(Default<FSEquipment.manufacturerID>))]
        public virtual int? ManufacturerModelID { get; set; }
        #endregion
        #region PropertyType
        public abstract class propertyType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true)]
        [FADetails.propertyType.List]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Property Type")]
        public virtual string PropertyType { get; set; }
        #endregion
        #region SerialNumber
        public abstract class serialNumber : PX.Data.IBqlField
        {
        }

        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Serial Nbr.")]
        public virtual string SerialNumber { get; set; }
        #endregion
        #region Status
        public abstract class status : ListField_Equipment_Status
        {
        }

        [PXDBString(1, IsFixed = true)]
        [status.ListAtrribute]
        [PXDefault(ID.Equipment_Status.ACTIVE)]
        [PXUIField(DisplayName = "Status")]
        public virtual string Status { get; set; }
        #endregion
        #region TagNbr
        public abstract class tagNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(20, IsUnicode = true)]
        [PXUIField(DisplayName = "Tag Nbr.")]
        public virtual string TagNbr { get; set; }
        #endregion
        #region NoteID
        public abstract class noteID : PX.Data.IBqlField
        {
        }

        [PXUIField(DisplayName = "NoteID")]
        [PXSearchable(SM.SearchCategory.FS, "SM {0}: {1} {2}", new Type[] { typeof(FSEquipment.refNbr), typeof(FSEquipment.descr), typeof(FSEquipment.serialNumber) },
           new Type[] { typeof(FSEquipment.refNbr), typeof(FSEquipment.descr), typeof(FSEquipment.serialNumber) },
           NumberFields = new Type[] { typeof(FSEquipment.refNbr) },
           Line1Format = "{0:d} {1} {2}", Line1Fields = new Type[] { typeof(FSEquipment.registeredDate), typeof(FSEquipment.status), typeof(FSEquipment.equipmentTypeID) },
           Line2Format = "{0}", Line2Fields = new Type[] { typeof(FSEquipment.descr) }
        )]
        [PXNote(new Type[0], ShowInReferenceSelector = true)]
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
        #region EquipmentTypeID
        public abstract class equipmentTypeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Equipment Type")]
        [FSSelectorEquipmentType]
        public virtual int? EquipmentTypeID { get; set; }
        #endregion
        #region SourceID
        public abstract class sourceID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? SourceID { get; set; }
        #endregion
        #region SourceDocType
        public abstract class sourceDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true)]
        [PXUIField(DisplayName = "Document Type", Enabled = false)]
        public virtual string SourceDocType { get; set; }
        #endregion
        #region SourceRefNbr
        public abstract class sourceRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Document Ref. Nbr.", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
        public virtual string SourceRefNbr { get; set; }
        #endregion
        #region SourceType
        public abstract class sourceType : ListField_SourceType_Equipment
        {
        }

        [PXDBString(3, IsFixed = true)]
        [PXDefault(ID.SourceType_Equipment.SM_EQUIPMENT)]
        [PXUIField(DisplayName = "Document Type", Enabled = false)]
        [sourceType.ListAtrribute]
        public virtual string SourceType { get; set; }
        #endregion
        #region ManufacturingYear
        public abstract class manufacturingYear : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        [PXUIField(DisplayName = "Manufacturing Year")]
        public virtual string ManufacturingYear { get; set; }
        #endregion
        #region DateInstalled
        public abstract class dateInstalled : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Installation Date")]
        public virtual DateTime? DateInstalled { get; set; }
        #endregion
        #region VendorID
        public abstract class vendorID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [FSSelectorBusinessAccount_VE]
        [PXUIField(DisplayName = "Vendor")]
        public virtual int? VendorID { get; set; }
        #endregion
        #region PurchDate
        public abstract class purchDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Purchase Date")]
        public virtual DateTime? PurchDate { get; set; }
        #endregion
        #region PurchPONumber
        public abstract class purchPONumber : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Purchase Order Nbr.")]
        public virtual string PurchPONumber { get; set; }
        #endregion
        #region PurchAmount
        public abstract class purchAmount : PX.Data.IBqlField
        {
        }

        [PXDBBaseCury]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Purchase Amount")]
        public virtual decimal? PurchAmount { get; set; }
        #endregion
        #region RegistrationNbr
        public abstract class registrationNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(30, IsUnicode = true)]
        [PXUIField(DisplayName = "Registration Nbr.")]
        public virtual string RegistrationNbr { get; set; }
        #endregion
        #region RegisteredDate
        public abstract class registeredDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Registered Date")]
        public virtual DateTime? RegisteredDate { get; set; }
        #endregion
        #region Axles
        public abstract class axles : PX.Data.IBqlField
        {
        }

        [PXDBShort]
        [PXDefault((short)0)]
        [PXUIField(DisplayName = "Axles")]
        public virtual short? Axles { get; set; }
        #endregion
        #region FuelType
        public abstract class fuelType : ListField_FuelType_Equipment
        {
        }

        [PXDBString(1, IsFixed = true)]
        [fuelType.ListAtrribute]
        [PXUIField(DisplayName = "Fuel Type")]
        public virtual string FuelType { get; set; }
        #endregion
        #region FuelTank1
        public abstract class fuelTank1 : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Tank 1 - Gallons")]
        public virtual decimal? FuelTank1 { get; set; }
        #endregion
        #region FuelTank2
        public abstract class fuelTank2 : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Tank 2 - Gallons")]
        public virtual decimal? FuelTank2 { get; set; }
        #endregion
        #region GrossVehicleWeight
        public abstract class grossVehicleWeight : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Gross Vehicle Weight")]
        public virtual decimal? GrossVehicleWeight { get; set; }
        #endregion
        #region MaxMiles
        public abstract class maxMiles : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Max Miles")]
        public virtual decimal? MaxMiles { get; set; }
        #endregion
        #region TareWeight
        public abstract class tareWeight : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Tare Weight")]
        public virtual decimal? TareWeight { get; set; }
        #endregion
        #region WeightCapacity
        public abstract class weightCapacity : PX.Data.IBqlField
        {
        }

        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Weight Capacity")]
        public virtual decimal? WeightCapacity { get; set; }
        #endregion
        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Customer")]
        [FSSelectorCustomer]
        public virtual int? CustomerID { get; set; }
        #endregion
        #region CustomerLocationID
        public abstract class customerLocationID : PX.Data.IBqlField
        {
        }

        [LocationID(typeof(Where<Location.bAccountID, Equal<Current<FSEquipment.customerID>>,
                           And<MatchWithBranch<Location.cBranchID>>>),
                    DescriptionField = typeof(Location.descr), DisplayName = "Location", DirtyRead = true)]
        [PXRestrictor(typeof(Where<Location.isActive, Equal<True>>), IN.Messages.InactiveLocation, typeof(Location.locationCD))]
        [PXDefault(typeof(Coalesce<Search2<BAccountR.defLocationID,
            InnerJoin<CRLocation, On<CRLocation.bAccountID, Equal<BAccountR.bAccountID>, And<CRLocation.locationID, Equal<BAccountR.defLocationID>>>>,
            Where<BAccountR.bAccountID, Equal<Current<FSEquipment.customerID>>,
                And<CRLocation.isActive, Equal<True>,
                And<MatchWithBranch<CRLocation.cBranchID>>>>>,
            Search<CRLocation.locationID,
            Where<CRLocation.bAccountID, Equal<Current<FSEquipment.customerID>>,
            And<CRLocation.isActive, Equal<True>, And<MatchWithBranch<CRLocation.cBranchID>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSEquipment.customerID>))]
        public virtual int? CustomerLocationID { get; set; }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
        {
        }

        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Description")]
        [PX.Data.EP.PXFieldDescription]
        public virtual string Descr { get; set; }
        #endregion
        #region VehicleTypeID
        public abstract class vehicleTypeID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Vehicle Type ID")]
        [PXSelector(typeof(FSVehicleType.vehicleTypeID), SubstituteKey = typeof(FSVehicleType.vehicleTypeCD))]
        public virtual int? VehicleTypeID { get; set; }
        #endregion
        #region SiteID
        public abstract class siteID : PX.Data.IBqlField
        {
        }

        [Site(DisplayName = "Warehouse", DescriptionField = typeof(INSite.descr), Enabled = false)]
        public virtual int? SiteID { get; set; }
        #endregion
        #region LocationID
        public abstract class locationID : PX.Data.IBqlField
        {
        }

        [Location(typeof(FSEquipment.siteID), DisplayName = "Warehouse Location", KeepEntry = false, DescriptionField = typeof(INLocation.descr), Enabled = false)]
        public virtual int? LocationID { get; set; }
        #endregion
        #region ColorID
        public abstract class colorID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Color ID")]
        [PXSelector(typeof(SystemColor.colorID), SubstituteKey = typeof(SystemColor.colorName))]
        public virtual int? ColorID { get; set; }
        #endregion
        #region EngineNo
        public abstract class engineNo : PX.Data.IBqlField
        {
        }

        [PXDBString(30, IsUnicode = true)]
        [PXUIField(DisplayName = "Engine Nbr.")]
        public virtual string EngineNo { get; set; }
        #endregion
        #region InventoryID
        public abstract class inventoryID : PX.Data.IBqlField
        {
        }

        [EquipmentModelItem(Filterable = true, DirtyRead = false)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual int? InventoryID { get; set; }
        #endregion
        #region OwnerType
        public abstract class ownerType : ListField_OwnerType_Equipment
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.OwnerType_Equipment.OWN_COMPANY, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Owner Type")]
        [ownerType.ListAtrribute]
        public virtual string OwnerType { get; set; }
        #endregion
        #region OwnerID
        public abstract class ownerID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Customer")]
        [FSSelectorCustomer]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<FSEquipment.ownerType>))]
        public virtual int? OwnerID { get; set; }
        #endregion
        #region INSerialNumber
        public abstract class iNSerialNumber : PX.Data.IBqlField
        {
        }

        [PXDBString(60, IsUnicode = true)]
        [PXUIField(DisplayName = "Model Serial Nbr.", Enabled = false)]
        public virtual string INSerialNumber { get; set; }
        #endregion
        #region SubItemID
        public abstract class subItemID : PX.Data.IBqlField
        {
        }

        [SubItem(DisplayName = "Subitem")]
        public virtual int? SubItemID { get; set; }
        #endregion
        #region RequireMaintenance
        public abstract class requireMaintenance : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Target Equipment")]
        public virtual bool? RequireMaintenance { get; set; }
        #endregion
        #region ResourceEquipment
        public abstract class resourceEquipment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXFormula(typeof(Default<FSEquipment.ownerType, FSEquipment.ownerID>))]
        [PXUIField(DisplayName = "Resource Equipment")]
        public virtual bool? ResourceEquipment { get; set; }
        #endregion
        #region SalesDate
        public abstract class salesDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Sales Date")]
        public virtual DateTime? SalesDate { get; set; }
        #endregion
        #region ARTranLineNbr
        public abstract class arTranLineNbr : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? ARTranLineNbr { get; set; }
        #endregion
        #region LocationType
        public abstract class locationType : ListField_LocationType
        {
        }

        [PXDBString(2, IsFixed = true)]
        [PXDefault(ID.LocationType.CUSTOMER, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Location Type")]
        [locationType.ListAtrribute]
        public virtual string LocationType { get; set; }
        #endregion
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(AccessInfo.branchID), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Branch")]
        [PXSelector(typeof(Search<Branch.branchID>), SubstituteKey = typeof(Branch.branchCD), DescriptionField = typeof(Branch.acctName))]
        public virtual int? BranchID { get; set; }
        #endregion
        #region BranchLocationID
        public abstract class branchLocationID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXDefault(typeof(
            Search<FSxUserPreferences.dfltBranchLocationID,
            Where<
                PX.SM.UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>,
                And<PX.SM.UserPreferences.defBranchID, Equal<Current<FSEquipment.branchID>>,
                And<Current<locationType>, Equal<locationType.Company>>>>>),
            PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Branch Location")]
        [PXSelector(typeof(
            Search<FSBranchLocation.branchLocationID,
            Where<
                FSBranchLocation.branchID, Equal<Current<FSEquipment.branchID>>>>),
            SubstituteKey = typeof(FSBranchLocation.branchLocationCD),
            DescriptionField = typeof(FSBranchLocation.descr))]
        [PXFormula(typeof(Default<FSEquipment.branchID>))]
        public virtual int? BranchLocationID { get; set; }
        #endregion
        #region InstServiceOrderNbr
        public abstract class instServiceOrderID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSServiceOrder.sOID>),
            SubstituteKey = typeof(FSServiceOrder.refNbr),
            DescriptionField = typeof(FSServiceOrder.docDesc))]
        [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
        public virtual int? InstServiceOrderID { get; set; }
        #endregion
        #region InstAppointmentID
        public abstract class instAppointmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSAppointment.appointmentID>),
            SubstituteKey = typeof(FSAppointment.refNbr),
            DescriptionField = typeof(FSAppointment.docDesc))]
        [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false)]
        public virtual int? InstAppointmentID { get; set; }
        #endregion
        #region DisposalDate
        public abstract class disposalDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Disposal Date")]
        public virtual DateTime? DisposalDate { get; set; }
        #endregion
        #region ReplaceEquipmentID
        public abstract class replaceEquipmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(
            Search<FSEquipment.SMequipmentID,
            Where2<
                Where<
                    Current<FSEquipment.ownerType>, Equal<FSEquipment.ownerType.OwnCompany>>,
                    And<FSEquipment.ownerType, Equal<ownerType.OwnCompany>,
                Or<Where<
                    Current<FSEquipment.ownerType>, Equal<FSEquipment.ownerType.Customer>,
                    And<FSEquipment.ownerType, Equal<ownerType.Customer>,
                    And<Current<FSEquipment.customerID>, Equal<FSEquipment.customerID>>>>>>>>),
            SubstituteKey = typeof(FSEquipment.refNbr),
            DescriptionField = typeof(FSEquipment.descr))]
        [PXUIField(DisplayName = "Replacement Equipment Nbr.")]
        public virtual int? ReplaceEquipmentID { get; set; }
        #endregion
        #region DispServiceOrderID
        public abstract class dispServiceOrderID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSServiceOrder.sOID>),
            SubstituteKey = typeof(FSServiceOrder.refNbr),
            DescriptionField = typeof(FSServiceOrder.docDesc))]
        [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
        public virtual int? DispServiceOrderID { get; set; }
        #endregion
        #region DispAppointmentID
        public abstract class dispAppointmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXSelector(typeof(Search<FSAppointment.appointmentID>),
            SubstituteKey = typeof(FSAppointment.refNbr),
            DescriptionField = typeof(FSAppointment.docDesc))]
        [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false)]
        public virtual int? DispAppointmentID { get; set; }
        #endregion
        #region CpnyWarrantyValue
        public abstract class cpnyWarrantyValue : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0)]
        [PXUIField(DisplayName = "Company Warranty")]
        public virtual int? CpnyWarrantyValue { get; set; }
        #endregion
        #region CpnyWarrantyType
        public abstract class cpnyWarrantyType : ListField_WarrantyDurationType
        {
        }

        [PXDBString(1, IsFixed = true)]
        [cpnyWarrantyType.ListAtrribute]
        [PXDefault(ID.WarrantyDurationType.MONTH)]
        [PXUIField]
        public virtual string CpnyWarrantyType { get; set; }
        #endregion
        #region CpnyWarrantyEndDate
        public abstract class cpnyWarrantyEndDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Company Warranty End Date", Enabled = false)]
        public virtual DateTime? CpnyWarrantyEndDate { get; set; }
        #endregion
        #region VendorWarrantyValue
        public abstract class vendorWarrantyValue : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 0)]
        [PXUIField(DisplayName = "Vendor Warranty")]
        public virtual int? VendorWarrantyValue { get; set; }
        #endregion
        #region VendorWarrantyType
        public abstract class vendorWarrantyType : ListField_WarrantyDurationType
        {
        }

        [PXDBString(1, IsFixed = true)]
        [vendorWarrantyType.ListAtrribute]
        [PXDefault(ID.WarrantyDurationType.MONTH)]
        [PXUIField]
        public virtual string VendorWarrantyType { get; set; }
        #endregion
        #region VendorWarrantyEndDate
        public abstract class vendorWarrantyEndDate : PX.Data.IBqlField
        {
        }

        [PXDBDate]
        [PXUIField(DisplayName = "Vendor Warranty End Date", Enabled = false)]
        public virtual DateTime? VendorWarrantyEndDate { get; set; }
        #endregion
        #region SalesOrderType
        public abstract class salesOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true)]
        [PXUIField(DisplayName = "Sales Order Type")]
        public virtual string SalesOrderType { get; set; }
        #endregion
        #region SalesOrderNbr
        public abstract class salesOrderNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Sales Order Nbr.", Enabled = false)]
        [PXSelector(typeof(
            Search<SOOrder.orderNbr, 
            Where<
                SOOrder.orderType, Equal<Current<FSEquipment.salesOrderType>>>>))]
        public virtual string SalesOrderNbr { get; set; }
        #endregion
        #region EquipmentReplacedID
        public abstract class equipmentReplacedID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Equipment Replaced", Enabled = false)]
        [PXSelector(typeof(
            Search<FSEquipment.SMequipmentID,
            Where<
                FSEquipment.SMequipmentID, NotEqual<Current<FSEquipment.SMequipmentID>>>>), 
            SubstituteKey = typeof(FSEquipment.refNbr))]
        public virtual int? EquipmentReplacedID { get; set; }
        #endregion
        #region ImageUrl
        public abstract class imageUrl : PX.Data.IBqlField
        {
        }
        protected String _ImageUrl;

        /// <summary>
        /// The URL of the image associated with the item.
        /// </summary>
        [PXDBString(255)]
        [PXUIField(DisplayName = "Image")]
        public virtual String ImageUrl
        {
            get
            {
                return this._ImageUrl;
            }
            set
            {
                this._ImageUrl = value;
            }
        }
        #endregion
        #region MemoryHelper
        #region Mem_UnassignedVehicle
        public abstract class mem_UnassignedVehicle : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXUIField(DisplayName = "Already Assigned", Enabled = false)]
        public virtual bool? Mem_UnassignedVehicle { get; set; }
        #endregion
        #region Mem_Description
        public abstract class mem_Description : PX.Data.IBqlField
        {
        }

        [PXString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Equipment Description")]
        public virtual string Mem_Description { get; set; }
        #endregion
        #region Mem_ReplacedEquipment
        public abstract class mem_ReplacedEquipment : PX.Data.IBqlField
        {
        }

        [PXString]
        [PXUIField(DisplayName = "Suspended Target Equipment ID")]
        public virtual string Mem_ReplacedEquipment { get; set; }
        #endregion
        #region EquipmentTypeCD
        // Needed for attributes
        public abstract class equipmentTypeCD : PX.Data.IBqlField
        {
        }

        [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
        [PXUIField(Visible = false)]
        [PXDefault(typeof(Search<FSEquipmentType.equipmentTypeCD, Where<FSEquipmentType.equipmentTypeID, Equal<Current<FSEquipment.equipmentTypeID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual string EquipmentTypeCD { get; set; }
        #endregion
        #endregion
        #region Attributes
        /// <summary>
        /// A service field, which is necessary for the <see cref="CSAnswers">dynamically 
        /// added attributes</see> defined at the <see cref="FSVehicleType">Vehicle
        /// screen</see> level to function correctly.
        /// </summary>
        [CRAttributesField(typeof(FSEquipment.equipmentTypeCD), typeof(FSEquipment.noteID))]
        public virtual string[] Attributes { get; set; }
        #endregion

        #region ReportSMEquipmentID
        public abstract class reportSMEquipmentID : IBqlField
        {
        }

        [PXInt]
        [PXSelector(typeof(Search<refNbr,
                           Where<
                                FSEquipment.customerID, Equal<Optional<FSEquipment.customerID>>>>),
                            new Type[]
                            {
                                typeof(FSEquipment.refNbr),
                                typeof(FSEquipment.customerID),
                                typeof(FSEquipment.status),
                                typeof(FSEquipment.customerLocationID)
                            })]
        public virtual int? ReportSMEquipmentID { get; set; }
        #endregion
    }
}
