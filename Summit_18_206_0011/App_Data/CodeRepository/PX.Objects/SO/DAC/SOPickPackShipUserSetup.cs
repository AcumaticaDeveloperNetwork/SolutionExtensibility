using System;
using PX.Data;
using PX.Objects.CS;
using PX.SM;

namespace PX.Objects.SO
{
	[Serializable]
	[PXCacheName(Messages.SOPickPackShipUserSetup)]
	public class SOPickPackShipUserSetup : IBqlTable
	{
		[PXDBGuid(IsKey = true)]
		[PXDefault(typeof(Search<Users.pKID, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>>))]
		[PXUIField(DisplayName = "User")]
		public virtual Guid? UserID { get; set; }
		public abstract class userID : PX.Data.IBqlField { }
        
        [PXString(1, IsFixed = true)]
        [PXDefault("N")]
        [PXStringList(new string[] { "Auto-select Lot/Serial Number and Location",
                                     "Prompt for Lot/Serial Number and Location",
                                     "Prompt for Lot/Serial Number" }, new string[] { "S", "L", "N" })]
        [PXUIField(DisplayName = "Prompt")]
        public virtual string PromptChoice { get; set; }
        public abstract class promptChoice : PX.Data.IBqlField { }
        
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Auto-select LotSerial/Number and Location")]
        public virtual bool? AutoSelectLotSerialNumberLocation { get; set; }
        public abstract class autoSelectLotSerialNumberLocation : PX.Data.IBqlField { }
        
        [PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Prompt for LotSerial/Number and Location", FieldClass = IN.LocationAttribute.DimensionName)]
		public virtual bool? PromptLocation { get; set; }
		public abstract class promptLocation : PX.Data.IBqlField { }

        #region Print Label Carrier
        #region PrintWithDeviceHub
        public abstract class carrierLabelPrintWithDeviceHub : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXDBBool]
        [PXDefault(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIField(DisplayName = "Print with DeviceHub")]
        public virtual bool? CarrierLabelPrintWithDeviceHub { get; set; }
        #endregion
        #region DefinePrinterManually
        public abstract class carrierLabelDefinePrinterManually : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(carrierLabelPrintWithDeviceHub))]
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Define Printer Manually")]
        [PXFormula(typeof(Default<carrierLabelPrintWithDeviceHub>))]
        public virtual bool? CarrierLabelDefinePrinterManually { get; set; }
        #endregion
        #region Printer
        public abstract class carrierLabelPrinterName : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(carrierLabelDefinePrinterManually))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXPrinterSelector]
        [PXFormula(typeof(Default<carrierLabelDefinePrinterManually>))]
        public virtual string CarrierLabelPrinterName { get; set; }
        #endregion
        #endregion

        #region Print Shipment Confirmation
        #region PrintWithDeviceHub
        public abstract class shipmentConfirmationPrintWithDeviceHub : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXDBBool]
        [PXDefault(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIField(DisplayName = "Print with DeviceHub")]
        public virtual bool? ShipmentConfirmationPrintWithDeviceHub { get; set; }
        #endregion
        #region DefinePrinterManually
        public abstract class shipmentConfirmationDefinePrinterManually : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(shipmentConfirmationPrintWithDeviceHub))]
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Define Printer Manually")]
        [PXFormula(typeof(Default<shipmentConfirmationPrintWithDeviceHub>))]
        public virtual bool? ShipmentConfirmationDefinePrinterManually { get; set; }
        #endregion
        #region Printer
        public abstract class shipmentConfirmationPrinterName : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(shipmentConfirmationDefinePrinterManually))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXPrinterSelector]
        [PXFormula(typeof(Default<shipmentConfirmationDefinePrinterManually>))]
        public virtual string ShipmentConfirmationPrinterName { get; set; }
        #endregion
        #endregion

        #region Scale
        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Use Digital Scale")]
		public virtual bool? UseScale { get; set; }
		public abstract class useScale : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXDBString(10)]
		[PXSelector(typeof(SMScale.scaleID))]
		[PXUIEnabled(typeof(useScale))]
		[PXUIField(DisplayName = "Scale")]
		public virtual string ScaleID { get; set; }
		public abstract class scaleID : PX.Data.IBqlField { }
        #endregion

        #region Cast Operator Override to IPrintable DACs
        public static explicit operator SOPickPackShipUserPrintCarrierLabelSetup(SOPickPackShipUserSetup userSetup)
        {
            return new SOPickPackShipUserPrintCarrierLabelSetup()
            {
                PrintWithDeviceHub = userSetup.CarrierLabelPrintWithDeviceHub,
                DefinePrinterManually = userSetup.CarrierLabelDefinePrinterManually,
                PrinterName = userSetup.CarrierLabelPrinterName
            };
        }

        public static explicit operator SOPickPackShipUserPrintShipmentConfirmationSetup(SOPickPackShipUserSetup userSetup)
        {
            return new SOPickPackShipUserPrintShipmentConfirmationSetup()
            {
                PrintWithDeviceHub = userSetup.ShipmentConfirmationPrintWithDeviceHub,
                DefinePrinterManually = userSetup.ShipmentConfirmationDefinePrinterManually,
                PrinterName = userSetup.ShipmentConfirmationPrinterName
            };
        }
        #endregion
    }

    [Serializable]
    [PXProjection(typeof(Select<SOPickPackShipUserSetup>), Persistent = true)]
    public class SOPickPackShipUserPrintCarrierLabelSetup : IBqlTable, PX.SM.IPrintable
    {
        #region PrintWithDeviceHub
        public abstract class printWithDeviceHub : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXDBBool(BqlField = typeof(SOPickPackShipUserSetup.carrierLabelPrintWithDeviceHub))]
        [PXDefault(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIField(DisplayName = "Print with DeviceHub")]
        public virtual bool? PrintWithDeviceHub { get; set; }
        #endregion
        #region DefinePrinterManually
        public abstract class definePrinterManually : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(printWithDeviceHub))]
        [PXDBBool(BqlField = typeof(SOPickPackShipUserSetup.carrierLabelDefinePrinterManually))]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Define Printer Manually")]
        [PXFormula(typeof(Default<printWithDeviceHub>))]
        public virtual bool? DefinePrinterManually { get; set; }
        #endregion
        #region Printer
        public abstract class printerName : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(definePrinterManually))]
        [PXPrinterSelector(BqlField = typeof(SOPickPackShipUserSetup.carrierLabelPrinterName))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<definePrinterManually>))]
        public virtual string PrinterName { get; set; }
        #endregion
    }

    [Serializable]
    [PXProjection(typeof(Select<SOPickPackShipUserSetup>), Persistent = true)]
    public class SOPickPackShipUserPrintShipmentConfirmationSetup : IBqlTable, PX.SM.IPrintable
    {
        #region PrintWithDeviceHub
        public abstract class printWithDeviceHub : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXDBBool(BqlField = typeof(SOPickPackShipUserSetup.shipmentConfirmationPrintWithDeviceHub))]
        [PXDefault(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIField(DisplayName = "Print with DeviceHub")]
        public virtual bool? PrintWithDeviceHub { get; set; }
        #endregion
        #region DefinePrinterManually
        public abstract class definePrinterManually : IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(printWithDeviceHub))]
        [PXDBBool(BqlField = typeof(SOPickPackShipUserSetup.shipmentConfirmationDefinePrinterManually))]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Define Printer Manually")]
        [PXFormula(typeof(Default<printWithDeviceHub>))]
        public virtual bool? DefinePrinterManually { get; set; }
        #endregion
        #region Printer
        public abstract class printerName : PX.Data.IBqlField { }

        [PXUIVisible(typeof(FeatureInstalled<FeaturesSet.deviceHub>))]
        [PXUIEnabled(typeof(definePrinterManually))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXPrinterSelector(BqlField = typeof(SOPickPackShipUserSetup.shipmentConfirmationPrinterName))]
        [PXFormula(typeof(Default<definePrinterManually>))]
        public virtual string PrinterName { get; set; }
        #endregion
    }
}