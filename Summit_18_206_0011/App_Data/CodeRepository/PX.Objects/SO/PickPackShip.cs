using System;
using System.Collections;
using PX.Data;
using PX.Objects.IN;
using System.Collections.Generic;
using PX.Common;
using PX.Objects.AR;
using PX.SM;
using PX.Objects.CS;
using System.Globalization;
using System.Linq;

namespace PX.Objects.SO
{
	public static class ScanStatuses
	{
		public const string Success = "OK"; //Causes focus to be sent back to shipment nbr. field
		public const string Clear = "CLR"; //Causes focus to be sent back to shipment nbr. field (same sound as "Scan" status)
		public const string Scan = "SCN";
		public const string Information = "INF";
		public const string Error = "ERR";
	}

	public static class ScanModes
	{
		public const string Add = "A";
		public const string Remove = "R";
	}

	public static class ScanStates
	{
		public const string ShipmentNumber = "N";
		public const string Item = "I";
		public const string LotSerialNumber = "S";
		public const string Location = "L";
		public const string Weight = "W";
	}

	public static class ScanCommands
	{
		public const char CommandChar = '*';

		public const string Cancel = "Z";
		public const string Confirm = "C";
		public const string ConfirmAll = "CX";
		public const string Add = "A";
		public const string Remove = "R";
		public const string Item = "I";
		public const string LotSerial = "S";
		public const string NewPackage = "P";
		public const string NewPackageAutoCalcWeight = "PA";
		public const string PackageComplete = "PC";
	}

	public class ScanLog : IBqlTable
	{
		public abstract class logLineDate : IBqlField { }
		[PXDBDateAndTime(InputMask = "dd-MM-yyyy HH:mm:ss", DisplayMask = "dd-MM-yyyy HH:mm:ss", IsKey = true)]
		[PXUIField(DisplayName = "Time", Enabled = false)]
		public virtual DateTime? LogLineDate { get; set; }

		public abstract class logLine : IBqlField { }
		[PXString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Scan", Enabled = false)]
		public virtual string LogBarcode { get; set; }

		public abstract class logMessage : IBqlField { }
		[PXString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Message", Enabled = false)]
		public virtual string LogMessage { get; set; }
	}

	public class PickPackInfo : IBqlTable
	{
		public abstract class shipmentNbr : IBqlField { }
		[PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault()]
		[PXUIField(DisplayName = "Shipment Nbr.", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search2<SOShipment.shipmentNbr,
			InnerJoin<INSite, On<INSite.siteID, Equal<SOShipment.siteID>>,
			LeftJoinSingleTable<Customer, On<SOShipment.customerID, Equal<Customer.bAccountID>>>>,
			Where2<Match<INSite, Current<AccessInfo.userName>>,
			And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>))]
		[PXRestrictor(typeof(Where<SOShipment.status, Equal<SOShipmentStatus.open>,
							 And<SOShipment.shipmentType, Equal<SOShipmentType.issue>>>), PickPackShip.Msg.ShipmentInvalid, typeof(SOShipment.shipmentNbr))]
		public virtual string ShipmentNbr { get; set; }

		public abstract class barcode : IBqlField { }
		[PXString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Scan")]
		public virtual string Barcode { get; set; }

		public abstract class quantity : IBqlField { }
		[PXDBQuantity]
		[PXDefault(TypeCode.Decimal, "1.0")]
		[PXUIField(DisplayName = "Quantity")]
		public virtual decimal? Quantity { get; set; }

		public abstract class scanMode : IBqlField { }
		[PXString(1, IsFixed = true)]
		[PXStringList(new[] { ScanModes.Add, ScanModes.Remove }, new[] { PickPackShip.Msg.Add, PickPackShip.Msg.Remove })]
		[PXDefault(ScanModes.Add)]
		[PXUIField(DisplayName = "Scan Mode")]
		public virtual string ScanMode { get; set; }

		public abstract class scanState : IBqlField { }
		[PXString(1, IsFixed = true)]
		[PXDefault(ScanStates.ShipmentNumber)]
		[PXUIField(DisplayName = "Scan State")]
		public virtual string ScanState { get; set; }

		public abstract class lotSerialSearch : IBqlField { }
		[PXBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Search Lot/Serial Numbers", FieldClass = "LotSerial")]
		public virtual bool? LotSerialSearch { get; set; }

		public abstract class currentInventoryID : IBqlField { }
		[StockItem]
		public virtual int? CurrentInventoryID { get; set; }

		public abstract class currentSubID : IBqlField { }
		[SubItem]
		public virtual int? CurrentSubID { get; set; }

		public abstract class currentLocationID : IBqlField { }
		[Location]
		public virtual int? CurrentLocationID { get; set; }

		public abstract class currentLotSerialNumber : IBqlField { }
		[PXString]
		public virtual string CurrentLotSerialNumber { get; set; }

		public abstract class currentExpirationDate : IBqlField { }
		[PXDate]
		public virtual DateTime? CurrentExpirationDate { get; set; }

		public abstract class currentPackageLineNbr : IBqlField { }
		[PXInt]
		public virtual int? CurrentPackageLineNbr { get; set; }

		public abstract class status : IBqlField { }
		[PXString(3, IsUnicode = true)]
		[PXUIField(DisplayName = "Status", Enabled = false, Visible = false)]
		public virtual string Status { get; set; }

		public abstract class message : IBqlField { }
		[PXDefault(PickPackShip.Msg.BarcodePrompt)]
		[PXString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Message", Enabled = false)]
		public virtual string Message { get; set; }
	}

	public class PickPackShip : PXGraph<PickPackShip>
	{
		[Serializable]
		public class SOShipLineSplitPick : SOShipLineSplit
		{
			public new abstract class shipmentNbr : PX.Data.IBqlField { }
			public new abstract class lineNbr : PX.Data.IBqlField { }
            public abstract new class lotSerialNbr : PX.Data.IBqlField { }
            public abstract new class inventoryID : PX.Data.IBqlField { }
            public abstract new class subItemID : PX.Data.IBqlField { }

            public abstract class packageLineNbr : IBqlField { }
			[PXUIField(DisplayName = "Package Line Nbr.", Visible = false)]
			[PXInt]
			public virtual int? PackageLineNbr { get; set; }
		}

		[Serializable]
		public class SOPackageDetailPick : SOPackageDetail
		{
			public new abstract class shipmentNbr : PX.Data.IBqlField { }
			public new abstract class lineNbr : PX.Data.IBqlField { }

			public abstract class isCurrent : PX.Data.IBqlField { }
			[PXBool]
			[PXUIField(DisplayName = "Current")]
			public bool? IsCurrent { get; set; }
		}

		[Serializable]
		[PXCacheName(Messages.SOShipLine)]
		public class SOShipLinePick : SOShipLine
		{
			public abstract class pickedQty : IBqlField { }
			[PXQuantity(typeof(SOShipLine.uOM), typeof(SOShipLine.baseShippedQty))]
			[PXUIField(DisplayName = "Picked Qty.", Enabled = false)]
			[PXDefault(TypeCode.Decimal, "0.0")]
			public virtual Decimal? PickedQty { get; set; }
		}

		internal bool IgnoreClear = false;

		public enum ConfirmMode
		{
			PickedItems,
			AllItems
		}

		public readonly Dictionary<string, decimal> kgToWeightUnit = new Dictionary<string, decimal>
		{
			{ "KG", 1m },
			{ "LB", 0.453592m }
		};

		public const double ScaleWeightValiditySeconds = 30;

		public PXSetup<INSetup> Setup;
		public PXSelect<SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>> UserSetup;
		public PXCancel<PickPackInfo> Cancel;
		public PXFilter<PickPackInfo> Document;
		public PXSelect<SOShipLinePick, Where<SOShipLinePick.shipmentNbr, Equal<Current<PickPackInfo.shipmentNbr>>>, OrderBy<Asc<SOShipLinePick.shipmentNbr, Asc<SOShipLine.lineNbr>>>> Transactions;
		public PXSelect<SOShipLineSplitPick, Where<SOShipLineSplitPick.shipmentNbr, Equal<Current<SOShipLinePick.shipmentNbr>>, And<SOShipLineSplitPick.lineNbr, Equal<Current<SOShipLinePick.lineNbr>>>>> Splits;
		public PXSelect<SOPackageDetailPick, Where<SOPackageDetailPick.shipmentNbr, Equal<Current<PickPackInfo.shipmentNbr>>>> Packages;
		public PXSelect<SOShipLineSplitPick, Where<SOPackageDetailSplit.shipmentNbr, Equal<Current<SOPackageDetailPick.shipmentNbr>>>> PackageSplits;
		public PXSelectOrderBy<ScanLog, OrderBy<Desc<ScanLog.logLineDate>>> ScanLogs;

		public override void Clear(PXClearOption option)
		{
			//See comments in Complete function inside PickPackShipCacheManager cache below.
			if (!IgnoreClear)
			{
				base.Clear(option);
			}
		}

		protected void PickPackInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			EnsureUserSetupExists();

			Transactions.Cache.AllowDelete = false;
			Transactions.Cache.AllowInsert = false;
			Transactions.Cache.AllowUpdate = false;
			Splits.Cache.AllowDelete = false;
			Splits.Cache.AllowInsert = false;
			Splits.Cache.AllowUpdate = false;
			ScanLogs.Cache.AllowDelete = false;
			ScanLogs.Cache.AllowInsert = false;
			ScanLogs.Cache.AllowUpdate = false;
			Packages.Cache.AllowInsert = false; //Manual deletion and edit of weight/value is possible
			PackageSplits.Cache.AllowUpdate = false;
			PackageSplits.Cache.AllowInsert = false;
			PackageSplits.Cache.AllowUpdate = false;

			var doc = this.Document.Current;
			Confirm.SetEnabled(doc != null && doc.ShipmentNbr != null);
			ConfirmAll.SetEnabled(doc != null && doc.ShipmentNbr != null);
		}

		[PXMergeAttributes(Method = MergeMethod.Append)]
		[PXUIField(DisplayName = "Shipment Line Nbr.")]
		protected virtual void SOShipLineSplitPick_LineNbr_CacheAttached(PXCache sender)
		{
		}

		[PXMergeAttributes(Method = MergeMethod.Merge)]
		[PXUIField(DisplayName = "Line Nbr.", Visible = true)]
		protected virtual void SOShipLinePick_LineNbr_CacheAttached(PXCache sender)
		{
		}

		protected void PickPackInfo_ShipmentNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			var doc = e.Row as PickPackInfo;
			if (doc == null) return;

			SelectShipment();
		}

		protected virtual void EnsureUserSetupExists()
		{
			UserSetup.Current = UserSetup.Select();
			if (UserSetup.Current == null)
			{
				UserSetup.Current = UserSetup.Insert((SOPickPackShipUserSetup)UserSetup.Cache.CreateInstance());
			}
		}

		private void SelectShipment()
		{
			var doc = this.Document.Current;
			var shipment = PXSelectorAttribute.Select<PickPackInfo.shipmentNbr>(this.Document.Cache, doc);

			if (shipment != null && IsValidShipment(doc.ShipmentNbr))
			{
				doc.Status = ScanStatuses.Scan;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.ShipmentReady, doc.ShipmentNbr);
				SetScanState(ScanStates.Item);
			}
			else
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = shipment != null ? PXMessages.LocalizeFormatNoPrefix(Msg.ShipmentInvalid, doc.ShipmentNbr) :
												 PXMessages.LocalizeFormatNoPrefix(Msg.ShipmentNbrMissing, doc.ShipmentNbr);
				SetScanState(ScanStates.ShipmentNumber);
				doc.ShipmentNbr = null;
			}

			ClearScreen(false);
		}

		protected IEnumerable scanLogs()
		{
			foreach (ScanLog row in ScanLogs.Cache.Cached)
			{
				yield return row;
			}
		}

		protected IEnumerable splits()
		{
			//We only use this view as a container for picked lot/serial numbers. We don't care about what's in the DB for this shipment.
			foreach (SOShipLineSplit row in Splits.Cache.Cached)
			{
				if (this.Document.Current != null && row.ShipmentNbr == this.Document.Current.ShipmentNbr &&
					this.Transactions.Current != null && row.LineNbr == this.Transactions.Current.LineNbr)
				{
					yield return row;
				}
			}
		}

		protected IEnumerable packages()
		{
			//We only use this view as a container for picked packages. We don't care about what's in the DB for this shipment.
			foreach (SOPackageDetailPick row in Packages.Cache.Cached)
			{
				if (this.Document.Current != null && row.ShipmentNbr == this.Document.Current.ShipmentNbr && Packages.Cache.GetStatus(row) == PXEntryStatus.Inserted)
				{
					yield return row;
				}
			}
		}

		protected IEnumerable packageSplits()
		{
			//We only use this view as a container for picked package details. We don't care about what's in the DB for this shipment.
			foreach (SOShipLineSplitPick row in PackageSplits.Cache.Cached)
			{
				if (this.Packages.Current != null && row.PackageLineNbr == this.Packages.Current.LineNbr)
				{
					yield return row;
				}
			}
		}

		public PXAction<PickPackInfo> allocations;
		[PXUIField(DisplayName = "Allocations")]
		[PXButton]
		protected virtual void Allocations()
		{
			this.Splits.AskExt();
		}

		public PXAction<PickPackInfo> Scan;
		[PXUIField(DisplayName = "Scan")]
		[PXButton]
		protected virtual void scan()
		{
			var doc = this.Document.Current;

			if (String.IsNullOrEmpty(doc.Barcode))
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.BarcodePrompt);
			}
			else
			{
				if (doc.Barcode[0] == ScanCommands.CommandChar)
				{
					ProcessCommands(doc.Barcode);
				}
				else
				{
					switch (doc.ScanState)
					{
						case ScanStates.ShipmentNumber:
							ProcessShipmentNumber(doc.Barcode);
							break;
						case ScanStates.Item:
							ProcessItemBarcode(doc.Barcode);
							break;
						case ScanStates.LotSerialNumber:
                            ProcessLotSerialBarcode(doc.Barcode, null, false, null);
                            break;
						case ScanStates.Location:
							ProcessLocationBarcode(doc.Barcode);
							break;
						case ScanStates.Weight:
							ProcessWeight(doc.Barcode);
							break;
					}
				}

				InsertScanLog();
			}

			doc.Barcode = String.Empty;
			this.Document.Update(doc);
		}

		protected virtual void ProcessShipmentNumber(string barcode)
		{
			var doc = this.Document.Current;
			doc.ShipmentNbr = barcode.Trim();
			SelectShipment();
		}

		protected virtual void ProcessCommands(string barcode)
		{
			var doc = this.Document.Current;
			string[] commands = barcode.Split(ScanCommands.CommandChar);

			int quantity = 0;
			if (int.TryParse(commands[1].ToUpperInvariant(), out quantity))
			{
				if (IsQuantityEnabled())
				{
					doc.Quantity = quantity;
					doc.Status = ScanStatuses.Information;
					doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.CommandSetQuantity, quantity);
				}
				else
				{
					doc.Status = ScanStatuses.Error;
					doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandAccessRightsError);
				}
			}
			else
			{
				switch (commands[1].ToUpperInvariant())
				{
					case ScanCommands.Add:
						this.Document.Current.ScanMode = ScanModes.Add;
						doc.Status = ScanStatuses.Information;
						doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandAdd);
						break;
					case ScanCommands.Remove:
						this.Document.Current.ScanMode = ScanModes.Remove;
						doc.Status = ScanStatuses.Information;
						doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandRemove);
						break;
					case ScanCommands.Item:
						this.Document.Current.LotSerialSearch = false;
						doc.Status = ScanStatuses.Information;
						doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandInventory);
						break;
					case ScanCommands.LotSerial:
						this.Document.Current.LotSerialSearch = true;
						doc.Status = ScanStatuses.Information;
						doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandLot);
						break;
					case ScanCommands.Confirm:
						if (Confirm.GetEnabled())
						{
							this.Confirm.Press();
						}
						else
						{
							doc.Status = ScanStatuses.Error;
							doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandAccessRightsError);
						}
						break;
					case ScanCommands.ConfirmAll:
						if (ConfirmAll.GetEnabled())
						{
							this.ConfirmAll.Press();
						}
						else
						{
							doc.Status = ScanStatuses.Error;
							doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandAccessRightsError);
						}
						break;
					case ScanCommands.Cancel:
						if (doc.ScanState == ScanStates.Item)
						{
							ClearScreen(true);
							doc.Status = ScanStatuses.Clear;
							doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandClear);
						}
						else if (doc.ScanState != ScanStates.ShipmentNumber)
						{
							SetScanState(ScanStates.Item);
							doc.Status = ScanStatuses.Information;
							doc.Message = PXMessages.LocalizeNoPrefix(Msg.BarcodePrompt);
						}
						break;
					case ScanCommands.NewPackage:
						ProcessNewPackageCommand(commands, false);
						break;
					case ScanCommands.NewPackageAutoCalcWeight:
						ProcessNewPackageCommand(commands, true);
						break;
					case ScanCommands.PackageComplete:
						ProcessPackageCompleteCommand(false);
						break;
					default:
						doc.Status = ScanStatuses.Error;
						doc.Message = PXMessages.LocalizeNoPrefix(Msg.CommandUnknown);
						break;
				}
			}
		}

		protected virtual void ProcessWeight(string barcode)
		{
			var doc = this.Document.Current;

			decimal weight = 0;
			if (decimal.TryParse(barcode, out weight) && weight >= 0)
			{
				SetCurrentPackageWeight(weight);
			}
			else
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.PackageInvalidWeight, barcode);
			}
		}

		protected virtual decimal ConvertKilogramToWeightUnit(decimal weight, string weightUnit)
		{
			decimal conversionFactor;

			if (kgToWeightUnit.TryGetValue(weightUnit.Trim().ToUpperInvariant(), out conversionFactor))
			{
				return weight / conversionFactor;
			}
			else
			{
				throw new PXException(Msg.PackageWrongWeightUnit, weightUnit);
			}
		}

		protected virtual void ClearScreen(bool clearShipmentNbr)
		{
			if (clearShipmentNbr)
			{
				this.Document.Current.ShipmentNbr = null;
				SetScanState(ScanStates.ShipmentNumber);
			}

			this.Document.Current.CurrentInventoryID = null;
			this.Document.Current.CurrentSubID = null;
			this.Document.Current.CurrentLocationID = null;
			this.Document.Current.CurrentLotSerialNumber = null;
			this.Document.Current.CurrentExpirationDate = null;
			this.Document.Current.CurrentPackageLineNbr = null;
			this.Transactions.Cache.Clear();
			this.Transactions.Cache.ClearQueryCache();
			this.Splits.Cache.Clear();
			this.Splits.Cache.ClearQueryCache();
			this.ScanLogs.Cache.Clear();
			this.ScanLogs.Cache.ClearQueryCache();
			this.Packages.Cache.Clear();
			this.Packages.Cache.ClearQueryCache();
			this.PackageSplits.Cache.Clear();
			this.PackageSplits.Cache.ClearQueryCache();
		}

		protected virtual void ProcessItemBarcode(string barcode)
		{
			var doc = this.Document.Current;
            bool isAutoSelect = IsAutoSelectLotSerialNumber();

            if (doc.LotSerialSearch == true)
			{
				INLotSerialStatus lotSerialStatus = GetLotSerialStatus(barcode);
				if (lotSerialStatus == null)
				{
					doc.Status = ScanStatuses.Error;
					doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.LotMissing, barcode);
					return;
				}
				else
				{
					INLotSerClass lotSerialClass = GetLotSerialClass(lotSerialStatus.InventoryID);
					if (ValidateLotSerialStatus(barcode, lotSerialStatus, lotSerialClass))
					{
						SetCurrentInventoryIDByLotSerial(lotSerialStatus);
					}
					else
					{
						return;
					}
				}
			}
			else
			{
				bool lotSerialNumbered = false;
				if (!SetCurrentInventoryIDByItemBarcode(barcode, out lotSerialNumbered))
				{
					doc.Status = ScanStatuses.Error;
					doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.BarcodeMissing, barcode);
					return;
				}

				if (lotSerialNumbered)
				{
                    if (isAutoSelect)
                    {
                        List<SOShipLineSplit> selectedSplits = AutoSelectSplit(doc.ScanMode, doc.Quantity, doc.ShipmentNbr, null, doc.CurrentInventoryID, doc.CurrentSubID);

                        if (selectedSplits != null)
                        {
                            decimal pickedQuantity = doc.Quantity.HasValue ? doc.Quantity.Value : 0M;
                            decimal? originalPickedQuantity = pickedQuantity;
                            SOShipLineSplit lastSelectedSplits = selectedSplits.LastOrDefault();

                            foreach (SOShipLineSplit selectedSplit in selectedSplits)
                            {
                                if (selectedSplit != null && selectedSplit.LotSerialNbr != null)
                                {
                                    bool isLastSelectedSplit = selectedSplit == lastSelectedSplits;

                                    if (doc.ScanMode == ScanModes.Add)
                                    {
                                        doc.Quantity = selectedSplit.Qty.HasValue ? selectedSplit.Qty : 0M;
                                        pickedQuantity -= doc.Quantity.Value;

                                        if (isLastSelectedSplit && pickedQuantity > 0)
                                        {
                                            // Add picked quantity overflow to last item
                                            doc.Quantity += pickedQuantity;
                                        }

                                        ProcessLotSerialBarcode(selectedSplit.LotSerialNbr, selectedSplit.LocationID, isLastSelectedSplit, originalPickedQuantity);
                                    }
                                    else if (doc.ScanMode == ScanModes.Remove)
                                    {
                                        doc.Quantity = 1M;
                                        ProcessLotSerialBarcode(selectedSplit.LotSerialNbr, selectedSplit.LocationID, isLastSelectedSplit, originalPickedQuantity);
                                    }
                                }
                            }
                        }
                        else
                        {
                            doc.Status = ScanStatuses.Error;
                            doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.LotMissing, barcode);
                        }
                    }
                    else
                    {
                        doc.Status = ScanStatuses.Scan;
                        doc.Message = PXMessages.LocalizeNoPrefix(Msg.LotScanPrompt);
                        SetScanState(ScanStates.LotSerialNumber);
                    }
                    return;
				}
			}

			if (IsLocationRequired())
			{
				doc.Status = ScanStatuses.Scan;
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.LocationPrompt);
				SetScanState(ScanStates.Location);
				return;
			}

            ProcessPick(true, null);
        }

        protected virtual void ProcessLotSerialBarcode(string barcode, int? locationID, bool resetScanState, decimal? originalPickedQuantity)
        {
			var doc = this.Document.Current;

			INLotSerialStatus lotSerialStatus = GetLotSerialStatus(barcode);
			INLotSerClass lotSerialClass = GetLotSerialClass(doc.CurrentInventoryID);

			if (ValidateLotSerialStatus(barcode, lotSerialStatus, lotSerialClass))
			{
				doc.CurrentLotSerialNumber = barcode;
				doc.CurrentExpirationDate = lotSerialStatus.ExpireDate;

				if (IsLocationRequired())
				{
					doc.Status = ScanStatuses.Scan;
					doc.Message = PXMessages.LocalizeNoPrefix(Msg.LocationPrompt);
					SetScanState(ScanStates.Location);
					return;
				}
                else if (locationID != null)
                {
                    doc.CurrentLocationID = locationID;
                }

                ProcessPick(resetScanState, originalPickedQuantity);
			}
		}

        protected virtual List<SOShipLineSplit> AutoSelectSplit(string scanMode, decimal? pickedQuantity, string shipmentNbr, string lotSerialNumber, int? inventoryID, int? subItemID)
        {
            pickedQuantity = pickedQuantity.HasValue ? pickedQuantity.Value : 0M;
            subItemID = Setup.Current.UseInventorySubItem.HasValue && Setup.Current.UseInventorySubItem.Value && subItemID.HasValue ? subItemID : null;

            // Filter picked split items by item
            IEnumerable<SOShipLineSplitPick> pickedSplits = Splits.Select().RowCast<SOShipLineSplitPick>().Where(x => x.InventoryID == inventoryID);
            
            // Filter picked split items by sub item
            if (subItemID != null)
                pickedSplits = pickedSplits.Where(x => x.SubItemID == subItemID);

            IEnumerable<SOShipLineSplit> shipmentSplits = PXSelectReadonly<SOShipLineSplit,
                                                          Where<SOShipLineSplit.shipmentNbr, Equal<Required<SOShipLineSplit.shipmentNbr>>,
                                                          And<SOShipLineSplit.inventoryID, Equal<Optional<SOShipLineSplit.inventoryID>>,
                                                          And<SOShipLineSplit.subItemID, Equal<Optional<SOShipLineSplit.subItemID>>>>>>.Select(this,
                                                                                                                                               shipmentNbr,
                                                                                                                                               inventoryID,
                                                                                                                                               subItemID).RowCast<SOShipLineSplit>();

            List<SOShipLineSplit> selectedSplits = new List<SOShipLineSplit>();

            if (scanMode == ScanModes.Add)
            {
                // No need to auto-select Lot/SerialNumber
                if (lotSerialNumber != null)
                    return new List<SOShipLineSplit>() { shipmentSplits.Where(x => x.LotSerialNbr == lotSerialNumber).FirstOrDefault() };

                // Continue picking same Lot/SerialNumber
                foreach (IGrouping<string, SOShipLineSplit> shipmentSplitsBySerial in shipmentSplits.ToLookup(x => x.LotSerialNbr))
                {
                    foreach (SOShipLineSplit shipmentSplit in shipmentSplits.Where(x => x.LotSerialNbr == shipmentSplitsBySerial.Key))
                    {
                        decimal qty = shipmentSplit.Qty.HasValue ? shipmentSplit.Qty.Value : 0M;

                        foreach (SOShipLineSplitPick pickedSplit in pickedSplits.Where(x => x.LotSerialNbr == shipmentSplitsBySerial.Key && shipmentSplit.Qty > 0))
                            qty--;

                        if (qty >= pickedQuantity)
                        {
                            selectedSplits.Add(shipmentSplit);

                            return selectedSplits;
                        }
                        else if (qty > 0 && shipmentSplit.Qty.HasValue)
                        {
                            pickedQuantity -= qty;
                            selectedSplits.Add(shipmentSplit);
                        }
                        else
                        {
                            // Remove picked split for shipment split
                            shipmentSplits = shipmentSplits.Where(x => x.LotSerialNbr != shipmentSplitsBySerial.Key);
                        }
                    }
                }
            }
            else if (scanMode == ScanModes.Remove)
            {
                pickedSplits = pickedSplits.Reverse();
                selectedSplits.AddRange(pickedSplits.Take(Convert.ToInt32(pickedQuantity.Value, CultureInfo.InvariantCulture)));
            }

            return selectedSplits.Count > 0 ? selectedSplits : new List<SOShipLineSplit>() { shipmentSplits.FirstOrDefault() };
        }

        protected virtual void ProcessLocationBarcode(string barcode)
		{
			var doc = this.Document.Current;

			INLocation location = PXSelectJoin<INLocation, InnerJoin<SOShipment, On<INLocation.siteID, Equal<SOShipment.siteID>>>,
								 Where<SOShipment.shipmentNbr, Equal<Current<PickPackInfo.shipmentNbr>>,
								 And<INLocation.locationCD, Equal<Required<INLocation.locationCD>>>>>.Select(this, barcode);

			if (location == null)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.LocationInvalid, barcode);
				return;
			}

			doc.CurrentLocationID = location.LocationID;

            ProcessPick(true, null);
        }

		protected virtual void InsertScanLog()
		{
			var doc = this.Document.Current;

			ScanLog scanLog = (ScanLog)this.ScanLogs.Cache.CreateInstance();
			scanLog.LogLineDate = PXTimeZoneInfo.Now;

			if (!String.IsNullOrEmpty(doc.Barcode))
			{
				scanLog.LogBarcode = doc.Barcode;
			}

			if (!String.IsNullOrEmpty(doc.Message))
			{
				scanLog.LogMessage = doc.Message;
			}

			ScanLogs.Cache.Insert(scanLog);
		}

        public virtual bool IsAutoSelectLotSerialNumber()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() && UserSetup.Current.AutoSelectLotSerialNumberLocation.HasValue && UserSetup.Current.AutoSelectLotSerialNumberLocation.Value;
        }

        protected virtual bool IsLocationRequired()
		{
			return PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() && this.UserSetup.Current.PromptLocation == true;
		}

		protected virtual bool SetCurrentInventoryIDByItemBarcode(string barcode, out bool lotSerialNumbered)
		{
			var doc = this.Document.Current;
			var rec = (PXResult<INItemXRef, InventoryItem, INLotSerClass, INSubItem>)
						  PXSelectJoin<INItemXRef,
							InnerJoin<InventoryItem,
											On<InventoryItem.inventoryID, Equal<INItemXRef.inventoryID>,
											And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>,
											And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>,
											And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>,
							InnerJoin<INLotSerClass,
										 On<InventoryItem.lotSerClassID, Equal<INLotSerClass.lotSerClassID>>,
							InnerJoin<INSubItem,
										 On<INSubItem.subItemID, Equal<INItemXRef.subItemID>>>>>,
							Where<INItemXRef.alternateID, Equal<Required<PickPackInfo.barcode>>,
											And<INItemXRef.alternateType, Equal<INAlternateType.barcode>>>>
							.SelectSingleBound(this, new object[] { doc }, barcode);

			lotSerialNumbered = false;

			if (rec == null)
			{
				return false;
			}
			else
			{
				var inventoryItem = (InventoryItem)rec;
				var sub = (INSubItem)rec;
				var lsclass = (INLotSerClass)rec;

				if (lsclass.LotSerTrack != INLotSerTrack.NotNumbered)
				{
					if (lsclass.LotSerAssign == INLotSerAssign.WhenUsed && lsclass.LotSerTrackExpiration == true)
					{
						//TODO: Implement support for this by prompting for expiration date (ScanStates.ExpirationDate)
						throw new NotImplementedException(Msg.LotNotSupported);
					}

					if (inventoryItem.BaseUnit != inventoryItem.SalesUnit)
					{
						//TODO: Implement support for this by prompting user to enter as many serial/lot as what's included in the SaleUnit.
						throw new NotImplementedException("Items which are lot/serial tracked must use the same base and sale unit of measures.");
					}

					lotSerialNumbered = true;
				}

				doc.CurrentInventoryID = inventoryItem.InventoryID;
				doc.CurrentSubID = sub.SubItemID;
				return true;
			}
		}

		protected virtual void SetScanState(string state)
		{
			var doc = this.Document.Current;

			//Add any state transition logic to this switch case
			switch (state)
			{
				case ScanStates.Item:
					doc.Quantity = 1;
					doc.ScanMode = ScanModes.Add;
					doc.CurrentInventoryID = null;
					doc.CurrentSubID = null;
					doc.CurrentLocationID = null;
					doc.CurrentLotSerialNumber = null;
					doc.CurrentExpirationDate = null;
					break;
			}

			this.Document.Current.ScanState = state;
		}

		protected virtual void ProcessPick(bool resetScanState, decimal? originalPickedQuantity)
        {
			var doc = this.Document.Current;

			if (Document.Current.ScanMode == ScanModes.Add && AddPick(doc.CurrentInventoryID, doc.CurrentSubID, doc.Quantity, doc.CurrentLocationID, doc.CurrentLotSerialNumber, doc.CurrentExpirationDate))
			{
                if (resetScanState)
                {
                    doc.Status = ScanStatuses.Scan;
                    doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.InventoryAdded, originalPickedQuantity.HasValue ? originalPickedQuantity.Value : Document.Current.Quantity, ((InventoryItem)PXSelectorAttribute.Select<PickPackInfo.currentInventoryID>(this.Document.Cache, doc)).InventoryCD.TrimEnd());
                    SetScanState(ScanStates.Item);
                }
            }
			else if (Document.Current.ScanMode == ScanModes.Remove && RemovePick(doc.CurrentInventoryID, doc.CurrentSubID, doc.Quantity, doc.CurrentLocationID, doc.CurrentLotSerialNumber))
			{
                if (resetScanState)
                {
                    doc.Status = ScanStatuses.Scan;
                    doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.InventoryRemoved, originalPickedQuantity.HasValue ? originalPickedQuantity.Value : Document.Current.Quantity, ((InventoryItem)PXSelectorAttribute.Select<PickPackInfo.currentInventoryID>(this.Document.Cache, doc)).InventoryCD.TrimEnd());
                    SetScanState(ScanStates.Item);
                }
            }
			else
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.InventoryMissing);
				SetScanState(ScanStates.Item); //Otherwise we can get stuck in ScanStates.Location
			}
		}

		protected virtual void ProcessNewPackageCommand(string[] commands, bool autoCalcWeight)
		{
			var doc = this.Document.Current;

			if (commands.Length != 3)
			{
				//We're expecting something that looks like *P*LARGE
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.PackageCommandMissingBoxId);
				return;
			}

			if (doc.CurrentPackageLineNbr != null)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.PackageIncompleteError, ScanCommands.CommandChar, ScanCommands.PackageComplete);
				return;
			}

			string boxID = commands[2];
			var box = (CSBox)PXSelect<CSBox, Where<CSBox.boxID, Equal<Required<CSBox.boxID>>>>.Select(this, boxID);
			if (box == null)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.PackageBoxMissing, boxID);
			}
			else
			{
				var newPackage = (SOPackageDetailPick)this.Packages.Cache.CreateInstance();
				newPackage.ShipmentNbr = doc.ShipmentNbr;
				newPackage.BoxID = box.BoxID;
				newPackage.Description = PXMessages.LocalizeFormatNoPrefix(Msg.PackageForShipment, doc.ShipmentNbr);
				newPackage = this.Packages.Insert(newPackage);
				doc.CurrentPackageLineNbr = newPackage.LineNbr;

				ProcessPackageCompleteCommand(autoCalcWeight);
			}
		}

		protected virtual void ProcessPackageCompleteCommand(bool autoCalcWeight)
		{
			var doc = this.Document.Current;

			if (doc.CurrentPackageLineNbr == null)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.PackageMissingCurrent);
			}
			else
			{
				//Attach any unlinked splits to newly inserted package
				foreach (SOShipLineSplitPick split in this.Splits.Cache.Cached)
				{
					if (split.PackageLineNbr == null)
					{
						split.PackageLineNbr = doc.CurrentPackageLineNbr;
						this.Splits.Update(split);
					}
				}

				if (autoCalcWeight)
				{
					ProcessAutoCalcWeight();
				}
				else if (this.UserSetup.Current.UseScale == true)
				{
					ProcessScaleWeight();
				}
				else
				{
					PromptForPackageWeight(false);
				}
			}
		}

		protected virtual void ProcessAutoCalcWeight()
		{
			var doc = this.Document.Current;
			decimal weight = 0M;

			if (!CalculatePackageWeightFromItemsAndBoxConfiguration(out weight))
			{
				PromptForPackageWeight(true);
			}
			else
			{
				SetCurrentPackageWeight(weight);
			}
		}

		protected virtual void ProcessScaleWeight()
		{
			var doc = this.Document.Current;
			var scale = (SMScale)PXSelect<SMScale, Where<SMScale.scaleID, Equal<Required<SOPickPackShipUserSetup.scaleID>>>>.Select(this, this.UserSetup.Current.ScaleID);

			if (scale == null)
			{
				throw new PXException(PXMessages.LocalizeFormatNoPrefix(Msg.ScaleMissing, this.UserSetup.Current.ScaleID));
			}

			if (scale.LastModifiedDateTime.Value.AddSeconds(ScaleWeightValiditySeconds) < DateTime.Now)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.ScaleTimeout, this.UserSetup.Current.ScaleID, ScaleWeightValiditySeconds);
			}
			else
			{
				decimal convertedWeight = ConvertKilogramToWeightUnit(scale.LastWeight.GetValueOrDefault(), Setup.Current.WeightUOM);
				SetCurrentPackageWeight(convertedWeight);
			}
		}

		protected virtual void PromptForPackageWeight(bool autoCalcFailed)
		{
			var doc = this.Document.Current;
			doc.Status = ScanStatuses.Error;

			if (autoCalcFailed)
			{
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.PackageWeightAutoCalcFailedPrompt);
			}
			else
			{
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.PackageWeightPrompt);
			}

			SetScanState(ScanStates.Weight);
		}

		protected virtual bool CalculatePackageWeightFromItemsAndBoxConfiguration(out decimal weight)
		{
			weight = 0M;

			// Add items weight
			foreach (SOShipLineSplitPick split in this.Splits.Cache.Cached)
			{
				if (split.PackageLineNbr != this.Document.Current.CurrentPackageLineNbr) continue;

				SOShipLinePick currentShipLine = (SOShipLinePick)this.Transactions.Search<SOShipLinePick.lineNbr>(split.LineNbr);

				if (currentShipLine != null)
					weight += split.BaseQty.GetValueOrDefault() * currentShipLine.UnitWeigth.GetValueOrDefault();
			}

			if (weight == 0)
			{
				return false;
			}

			// Add box weight
			CSBox box = PXSelect<CSBox, Where<CSBox.boxID, Equal<Required<CSBox.boxID>>>>.Select(this, GetCurrentPackageDetailPick().BoxID);
			if (box == null)
			{
				//Shouldn't happen
				return false;
			}
			else
			{
				weight = decimal.Round(weight + box.BoxWeight.Value, SOPackageInfo.BoxWeightPrecision);
			}

			return true;
		}

		protected virtual void SetCurrentPackageWeight(decimal weight)
		{
			var doc = this.Document.Current;
			SOPackageDetailPick package = GetCurrentPackageDetailPick();

			package.Weight = weight;
			this.Packages.Update(package);

			doc.Status = ScanStatuses.Information;
			doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.PackageComplete, weight, Setup.Current.WeightUOM);
			doc.CurrentPackageLineNbr = null;
			SetScanState(ScanStates.Item);
		}

		protected virtual SOPackageDetailPick GetCurrentPackageDetailPick()
		{
			SOPackageDetailPick package = (SOPackageDetailPick)this.Packages.Search<SOPackageDetailPick.lineNbr>(this.Document.Current.CurrentPackageLineNbr);

			if (package == null)
			{
				throw new PXException(PXMessages.LocalizeFormatNoPrefix(Msg.PackageLineNbrMissing, this.Document.Current.CurrentPackageLineNbr));
			}

			return package;
		}

		protected virtual INLotSerClass GetLotSerialClass(int? inventoryID)
		{
			return (INLotSerClass)PXSelectJoin<INLotSerClass,
					InnerJoin<InventoryItem, On<INLotSerClass.lotSerClassID, Equal<InventoryItem.lotSerClassID>>>,
					Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Select(this, inventoryID);
		}

        protected virtual INLotSerialStatus GetLotSerialStatus(string barcode)
        {
            var doc = this.Document.Current;
            INLotSerialStatus lotSerialStatus = null;

            PXResultset<INLotSerialStatus> searchLotSerialStatus;

            if (doc.CurrentInventoryID == null)
            {
                searchLotSerialStatus = PXSelectJoin<INLotSerialStatus,
                                        InnerJoin<SOShipment, On<INLotSerialStatus.siteID, Equal<SOShipment.siteID>>>,
                                        Where<INLotSerialStatus.qtyOnHand, Greater<Zero>,
                                        And<SOShipment.shipmentNbr, Equal<Current<PickPackInfo.shipmentNbr>>,
                                        And<INLotSerialStatus.lotSerialNbr, Equal<Required<INLotSerialStatus.lotSerialNbr>>>>>>.Select(this, barcode);
            }
            else
            {
                searchLotSerialStatus = PXSelectJoin<INLotSerialStatus,
                                        InnerJoin<SOShipment, On<INLotSerialStatus.siteID, Equal<SOShipment.siteID>>>,
                                        Where<INLotSerialStatus.inventoryID, Equal<Required<InventoryItem.inventoryID>>,
                                        And<INLotSerialStatus.qtyOnHand, Greater<Zero>,
                                        And<SOShipment.shipmentNbr, Equal<Current<PickPackInfo.shipmentNbr>>,
                                        And<INLotSerialStatus.lotSerialNbr, Equal<Required<INLotSerialStatus.lotSerialNbr>>>>>>>.Select(this, doc.CurrentInventoryID, barcode);
            }

            foreach (INLotSerialStatus ls in searchLotSerialStatus)
            {
                if (lotSerialStatus == null)
                {
                    lotSerialStatus = ls;
                }
                else
                {
                    throw new PXException(Msg.LotUniquenessError);
                }
            }

            return lotSerialStatus;
        }

        protected virtual void SetCurrentInventoryIDByLotSerial(INLotSerialStatus lotSerialStatus)
		{
			var doc = this.Document.Current;
			doc.CurrentInventoryID = lotSerialStatus.InventoryID;
			doc.CurrentSubID = lotSerialStatus.SubItemID;
			doc.CurrentLocationID = lotSerialStatus.LocationID;
			doc.CurrentLotSerialNumber = lotSerialStatus.LotSerialNbr;
			doc.CurrentExpirationDate = lotSerialStatus.ExpireDate;
		}

		protected virtual bool ValidateLotSerialStatus(string barcode, INLotSerialStatus lotSerialStatus, INLotSerClass lotSerialClass)
		{
			var doc = this.Document.Current;

			if (lotSerialClass != null &&
				lotSerialClass.LotSerTrack != INLotSerTrack.NotNumbered &&
				lotSerialClass.LotSerAssign == INLotSerAssign.WhenReceived)
			{
				if (lotSerialStatus == null)
				{
					doc.Status = ScanStatuses.Error;
					doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.LotMissing, barcode);
					return false;
				}
				else if (lotSerialClass.LotSerTrackExpiration == true &&
						 IsLotExpired(lotSerialStatus))
				{
					doc.Status = ScanStatuses.Error;
					doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.LotExpired, barcode);
					return false;
				}
			}

			return true;
		}

		protected virtual bool IsLotExpired(INLotSerialStatus lotSerialStatus)
		{
			return lotSerialStatus != null && lotSerialStatus.ExpireDate <= PXTimeZoneInfo.Now;
		}

		protected virtual bool AddPick(int? inventoryID, int? subID, decimal? quantity, int? locationID, string lotSerialNumber, DateTime? expirationDate)
		{
			SOShipLinePick firstMatchingLine = null;
			foreach (SOShipLinePick pickLine in this.Transactions.Select())
			{
				if (pickLine.InventoryID != inventoryID || (pickLine.SubItemID != subID && Setup.Current.UseInventorySubItem == true)) continue;
				if (firstMatchingLine == null) firstMatchingLine = pickLine;
				if (pickLine.PickedQty.GetValueOrDefault() >= pickLine.ShippedQty.GetValueOrDefault()) continue;

				//We first try to fill all the lines sequentially - item may be present multiple times on the shipment
				decimal quantityForCurrentPickLine = Math.Min(quantity.GetValueOrDefault(), pickLine.ShippedQty.GetValueOrDefault() - pickLine.PickedQty.GetValueOrDefault());
				pickLine.PickedQty = pickLine.PickedQty.GetValueOrDefault() + quantityForCurrentPickLine;
				this.Transactions.Update(pickLine);

				AddPickToCurrentLineSplits(locationID ?? pickLine.LocationID, lotSerialNumber, expirationDate, quantityForCurrentPickLine);

				quantity = quantity - quantityForCurrentPickLine;

				if (quantity == 0)
				{
					return true;
				}
			}

			if (firstMatchingLine != null)
			{
				//All the lines are already filled; just over-pick the first one.
				firstMatchingLine.PickedQty = firstMatchingLine.PickedQty.GetValueOrDefault() + quantity;
				this.Transactions.Update(firstMatchingLine);
				AddPickToCurrentLineSplits(locationID ?? firstMatchingLine.LocationID, lotSerialNumber, expirationDate, quantity.GetValueOrDefault());

				return true;
			}
			else
			{
				//Item not found.
				return false;
			}
		}

		protected virtual void AddPickToCurrentLineSplits(int? locationID, string lotSerialNumber, DateTime? expirationDate, decimal quantity)
		{
			if (String.IsNullOrEmpty(lotSerialNumber))
			{
				//This is not a serialized item, we can add quantity to existing split.
				bool foundMatchingSplit = false;
				foreach (SOShipLineSplitPick split in this.Splits.Select())
				{
					// Splits are linked to the corresponding package line number. If this is a new package, PackageLineNbr will be null.
					if (split.LocationID == locationID && (split.PackageLineNbr == this.Document.Current.CurrentPackageLineNbr || split.PackageLineNbr == null && this.Document.Current.CurrentPackageLineNbr == null))
					{
						split.Qty += quantity;
						this.Splits.Update(split);
						foundMatchingSplit = true;
						break;
					}
				}

				if (!foundMatchingSplit)
				{
					InsertSplit(quantity, locationID, lotSerialNumber, expirationDate);
				}
			}
			else
			{
				//Each lot/serial split needs to be inserted as a separate line.
				for (int i = 0; i < quantity; i++)
				{
					InsertSplit(1, locationID, lotSerialNumber, expirationDate);
				}
			}
		}

		protected virtual void InsertSplit(decimal quantity, int? locationID, string lotSerialNumber, DateTime? expirationDate)
		{
			var split = (SOShipLineSplitPick)this.Splits.Cache.CreateInstance();
			split.Qty = quantity;
			split.LocationID = locationID;
			split.LotSerialNbr = lotSerialNumber;
			split.ExpireDate = expirationDate;
			split.PackageLineNbr = this.Document.Current.CurrentPackageLineNbr;
			this.Splits.Insert(split);
		}

		protected virtual decimal GetTotalQuantityPickedForLotSerial(int? inventoryID, int? subID, string lotSerialNumber)
		{
			decimal total = 0;

			foreach (SOShipLinePick pickLine in this.Transactions.Select())
			{
				if (pickLine.InventoryID == inventoryID && pickLine.SubItemID == subID)
				{
					this.Transactions.Current = pickLine;
					foreach (SOShipLineSplit split in this.Splits.Select())
					{
						if (split.LotSerialNbr == lotSerialNumber)
						{
							total = total + split.Qty.GetValueOrDefault();
						}
					}
				}
			}

			return total;
		}

        public virtual bool RemovePick(int? inventoryID, int? subID, decimal? quantity, int? locationID, string lotSerialNumber)
        {
            IEnumerable<SOShipLinePick> orderedTransactions = this.Transactions.Select().RowCast<SOShipLinePick>().ToList();

            foreach (SOShipLinePick pickLine in IsAutoSelectLotSerialNumber() ? orderedTransactions.Reverse() : orderedTransactions)
            {
                if (pickLine.PickedQty.GetValueOrDefault() <= 0 ||
                    pickLine.InventoryID != inventoryID ||
                    (pickLine.SubItemID != subID && Setup.Current.UseInventorySubItem == true))
                    continue;

                this.Transactions.Current = pickLine;

                foreach (SOShipLineSplitPick split in this.Splits.Select().RowCast<SOShipLineSplitPick>().ToList())
                {
                    if (split.LocationID != locationID && locationID != null) continue;
                    if (split.PackageLineNbr != this.Document.Current.CurrentPackageLineNbr) continue;
                    if (split.LotSerialNbr != lotSerialNumber && !String.IsNullOrEmpty(lotSerialNumber)) continue;

                    decimal quantityToRemoveForSplit = Math.Min(split.Qty.GetValueOrDefault(), quantity.GetValueOrDefault());
                    quantity -= quantityToRemoveForSplit;
                    split.Qty -= quantityToRemoveForSplit;

                    if (split.Qty == 0)
                    {
                        this.Splits.Delete(split);
                    }
                    else
                    {
                        this.Splits.Update(split);
                    }

                    pickLine.PickedQty -= quantityToRemoveForSplit;

                    if (pickLine.PickedQty == 0)
                    {
                        pickLine.PickedQty = null;
                    }

                    this.Transactions.Update(pickLine);

                    if (quantity == 0)
                    {
                        break;
                    }
                }

                if (quantity == 0)
                {
                    break;
                }
            }

            if (quantity == 0)
            {
                return true;
            }
            else
            {
                //TODO: Handle situation where we were able to partially remove a pick
                //returning false will show InventoryMissing message which is inaccurate
                return false;
            }
        }

        public PXAction<PickPackInfo> Confirm;
		[PXUIField(DisplayName = "Confirm Picked")]
		[PXButton]
		protected virtual void confirm()
		{
			ConfirmShipment(ConfirmMode.PickedItems);
		}

		public PXAction<PickPackInfo> ConfirmAll;
		[PXUIField(DisplayName = "Confirm All")]
		[PXButton]
		protected virtual void confirmAll()
		{
			ConfirmShipment(ConfirmMode.AllItems);
		}

		protected virtual void ConfirmShipment(ConfirmMode confirmMode)
		{
			var doc = this.Document.Current;
			doc.Status = ScanStatuses.Information;
			doc.Message = String.Empty;
			SOShipmentEntry graph = PXGraph.CreateInstance<SOShipmentEntry>();
			SOShipment shipment = null;

			if (doc.CurrentPackageLineNbr != null)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.PackageCompletePrompt, ScanCommands.CommandChar, ScanCommands.PackageComplete);
				this.Document.Update(doc);
				return;
			}

			shipment = graph.Document.Search<SOShipment.shipmentNbr>(doc.ShipmentNbr);
			if (shipment == null)
			{
				doc.Status = ScanStatuses.Error;
				doc.Message = PXMessages.LocalizeNoPrefix(Msg.ShipmentMissing);
				this.Document.Update(doc);
				return;
			}

			bool isExternalShippingApplication = false;
			string shippingApplicationType = null;
			var carrier = (Carrier)PXSelectorAttribute.Select<SOShipment.shipVia>(graph.Document.Cache, graph.Document.Current);
			if(carrier != null)
			{ 
				isExternalShippingApplication = carrier.IsExternalShippingApplication.GetValueOrDefault();
				shippingApplicationType = carrier.ShippingApplicationType;
			}

			if (confirmMode == ConfirmMode.AllItems || !IsConfirmationNeeded() ||
				this.Document.Ask(PXMessages.LocalizeNoPrefix(Msg.ShipmentQuantityMismatchPrompt), MessageButtons.YesNo) == PX.Data.WebDialogResult.Yes)
			{
				var cacheManager = new PickPackShipCacheManager();

				PXLongOperation.StartOperation(this, () =>
				{

					try
					{
						PXLongOperation.SetCustomInfo(cacheManager);

						graph.Document.Current = shipment;

						if (confirmMode == ConfirmMode.PickedItems)
						{
							UpdateShipmentLinesWithPickResults(graph);
						}
						UpdateShipmentPackages(graph);

						if (isExternalShippingApplication)
						{ 
							//In this mode, the shipment is confirmed by the external shipping integration after pushing back freight cost and tracking numbers
						}
						else
						{
							PXAction action = graph.Actions["Action"];
							var adapter = new PXAdapter(new PXView.Dummy(graph, graph.Document.View.BqlSelect, new List<object> { graph.Document.Current }));

							adapter.Menu = SOShipmentEntryActionsAttribute.Messages.ConfirmShipment;
							action.PressButton(adapter);

                            graph.Clear();
                            graph.Document.Current = graph.Document.Search<SOShipment.shipmentNbr>(shipment.ShipmentNbr);
                        }

						PreparePrintJobs(graph);

						doc.Status = ScanStatuses.Success;
						if (confirmMode == ConfirmMode.AllItems)
						{
							doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.ShipmentConfirmedFull, doc.ShipmentNbr);
						}
						else if (confirmMode == ConfirmMode.PickedItems)
						{
							doc.Message = PXMessages.LocalizeFormatNoPrefix(Msg.ShipmentConfirmedPicked, doc.ShipmentNbr);
						}
						else
						{
							System.Diagnostics.Debug.Assert(false, "ConfirmMode invalid");
						}

						ClearScreen(true);
						this.Document.Update(doc);

						if(isExternalShippingApplication)
						{
							// We can't directly redirect this page to our custom URL protocol handler, or else the long-run process will continue spinning indefinitely. We go through an intermediate HTML page that does it.
							throw new PXRedirectToUrlException($"../../Frames/ShipmentAppLauncher.html?ShipmentApplicationType={shippingApplicationType}&ShipmentNbr={shipment.ShipmentNbr}", PXBaseRedirectException.WindowMode.NewWindow, true, string.Empty);
						} 
					}
					catch(PXRedirectToUrlException)
					{
						//Just rethrow, bypassing our standard exception handling mechanism
						throw;
					}
					catch (Exception e)
					{
						doc.Status = ScanStatuses.Error;
						doc.Message = e.Message;
						this.Document.Update(doc);
						throw;
					}
				});
			}
		}

		public PXAction<PickPackInfo> Settings;
		[PXUIField(DisplayName = "Settings")]
		[PXButton]
		protected virtual void settings()
		{
			if (UserSetup.AskExt() == WebDialogResult.OK)
			{
				PXCache cache = Caches[typeof(SOPickPackShipUserSetup)];
				cache.Persist(PXDBOperation.Insert);
				cache.Persist(PXDBOperation.Update);
				cache.Clear();
			}
		}
		
		protected virtual void PreparePrintJobs(SOShipmentEntry graph)
		{
            if (!PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
                return;
            
			var printSetup = (SOPickPackShipUserSetup)UserSetup.Select();
            var printCarrierLabelSetup = (SOPickPackShipUserPrintCarrierLabelSetup)printSetup;
            var printShippingConfirmationSetup = (SOPickPackShipUserPrintShipmentConfirmationSetup)printSetup;

            PXAdapter deviceHubAdapter = new PXAdapter(new PXView.Dummy(graph, graph.Document.View.BqlSelect, new List<object> { graph.Document.Current }))
            {
                //Device Hub require this flag to know if supported
                MassProcess = true
            };
            
            //Labels should ALWAYS be printer first because they go out faster, and that gives time to user to peel/stick them while shipment confirmation is spooling
            if (printCarrierLabelSetup.PrintWithDeviceHub == true)
			{
                try
                {
                    deviceHubAdapter.Arguments = graph.Caches[typeof(SOPickPackShipUserPrintCarrierLabelSetup)].ToDictionary(printCarrierLabelSetup);
                    graph.PrintCarrierLabels(new List<SOShipment>() { graph.Document.Current }, deviceHubAdapter);
                }
                catch(PXBaseRedirectException) { } //Never redirect in Pick Pack Ship
            }

			if (printShippingConfirmationSetup.PrintWithDeviceHub == true)
			{
                try
                {
                    deviceHubAdapter.Arguments = graph.Caches[typeof(SOPickPackShipUserPrintShipmentConfirmationSetup)].ToDictionary(printShippingConfirmationSetup);
                    graph.Report(deviceHubAdapter, SOReports.PrintShipmentConfirmation);
                }
                catch (PXBaseRedirectException) { } //Never redirect in Pick Pack Ship
			}
		}

		protected virtual bool IsConfirmationNeeded()
		{
			foreach (SOShipLinePick pickLine in this.Transactions.Select())
			{
				if (pickLine.PickedQty.GetValueOrDefault() != pickLine.ShippedQty.GetValueOrDefault())
				{
					return true;
				}
			}

			return false;
		}

		protected virtual bool IsQuantityEnabled()
		{
			foreach (PXEventSubscriberAttribute attribute in Document.Cache.GetAttributesReadonly<PickPackInfo.quantity>())
				if (attribute is PXUIFieldAttribute)
					return ((PXUIFieldAttribute)attribute).Enabled;

			return false;
		}

		private bool IsValidShipment(string shipmentNbr)
		{
			object newValue = shipmentNbr;

			try
			{ 
				Document.Cache.RaiseFieldVerifying<PickPackInfo.shipmentNbr>(Document.Current, ref newValue);
			}
			catch
			{
				// PXRestrictor clause is executed in FieldVerifying and will throw if condition is not met
				return false;
			}

			return true;
		}

		protected virtual void UpdateShipmentLinesWithPickResults(SOShipmentEntry graph)
		{
			foreach (SOShipLinePick pickLine in this.Transactions.Select())
			{
				graph.Transactions.Current = graph.Transactions.Search<SOShipLine.lineNbr>(pickLine.LineNbr);
				if (graph.Transactions.Current != null)
				{
					//Update shipped quantity to match what was picked
					if (graph.Transactions.Current.ShippedQty != pickLine.PickedQty)
					{
						graph.Transactions.Current.ShippedQty = pickLine.PickedQty.GetValueOrDefault();
						graph.Transactions.Update(graph.Transactions.Current);
					}

					//Set any lot/serial numbers as well as locations that were assigned
					this.Transactions.Current = pickLine;
					bool initialized = false;
					foreach (SOShipLineSplit split in this.Splits.Select())
					{
						if (this.Splits.Cache.GetStatus(split) == PXEntryStatus.Inserted)
						{
							if (!initialized)
							{
								//Delete any pre-existing split
								foreach (SOShipLineSplit s in graph.splits.Select())
								{
									graph.splits.Delete(s);
								}
								initialized = true;
							}

							graph.splits.Insert(split);
						}
					}
				}
				else
				{
					throw new PXException(PXMessages.LocalizeFormatNoPrefix(Msg.ShipmentLineMissing, pickLine.LineNbr));
				}
			}
		}

		protected virtual void UpdateShipmentPackages(SOShipmentEntry graph)
		{
			//Delete any existing package row - we ignore what auto-packaging configured and override with packages that were actually used.
			foreach (SOPackageDetail package in graph.Packages.Select())
			{
				graph.Packages.Delete(package);
			}

			foreach (SOPackageDetail package in this.Packages.Select())
			{
				package.Confirmed = true;
				graph.Packages.Insert(package);

				foreach (SOShipLineSplitPick split in this.Splits.Cache.Cached)
				{
					if (split.PackageLineNbr == package.LineNbr)
					{
						SOPackageDetailSplit packageSplit = (SOPackageDetailSplit)graph.Caches[typeof(SOPackageDetailSplit)].CreateInstance();
						packageSplit.ShipmentLineNbr = split.LineNbr;
						packageSplit.InventoryID = split.InventoryID;
						packageSplit.SubItemID = split.SubItemID;
						packageSplit.Qty = split.Qty;
						packageSplit.UOM = split.UOM;
						packageSplit.BaseQty = split.BaseQty;

						//TODO: Replace with actual view when integrating into Acumatica codebase
						graph.Caches[typeof(SOPackageDetailSplit)].Insert(packageSplit);
					}
				}
			}
		}

		protected virtual void SOPackageDetailPick_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			var row = e.Row as SOPackageDetailPick;
			if (row == null) return;

			//Detach any splits that was linked to the just deleted package
			foreach (SOShipLineSplitPick split in this.Splits.Cache.Cached)
			{
				if (split.PackageLineNbr == row.LineNbr)
				{
					split.PackageLineNbr = null;
					this.Splits.Update(split);
				}
			}
		}

		protected virtual void SOPackageDetailPick_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			SOPackageDetailPick row = e.Row as SOPackageDetailPick;
			if (row == null) return;

			row.WeightUOM = Setup.Current.WeightUOM;
			row.IsCurrent = (this.Document.Current.CurrentPackageLineNbr != null && row.LineNbr == this.Document.Current.CurrentPackageLineNbr);
		}

		protected void SOPackageDetailPick_IsCurrent_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SOPackageDetailPick row = e.Row as SOPackageDetailPick;
			if (row == null) return;

			if (row.IsCurrent == true)
			{
				this.Document.Current.CurrentPackageLineNbr = row.LineNbr;
				this.Document.Update(this.Document.Current);
				this.Packages.View.RequestRefresh(); //To have previously current row unchecked -- not needed when unchecking current
			}
			else
			{
				this.Document.Current.CurrentPackageLineNbr = null;
				this.Document.Update(this.Document.Current);
			}
		}

		protected void SOShipLinePick_PickedQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SOShipLinePick soShipLinePick = e.Row as SOShipLinePick;

			if (soShipLinePick != null)
			{
				const string quantityDisplayFormat = "{0:0.00}";

				sender.RaiseExceptionHandling<SOShipLinePick.pickedQty>(
					soShipLinePick,
					soShipLinePick.PickedQty,
					new PXSetPropertyException<SOShipLinePick.pickedQty>(PXMessages.LocalizeFormatNoPrefix(
							Msg.InventoryUpdated,
							string.Format(CultureInfo.InvariantCulture, quantityDisplayFormat, e.OldValue ?? 0M),
							string.Format(CultureInfo.InvariantCulture, quantityDisplayFormat, soShipLinePick.PickedQty ?? 0M),
							soShipLinePick.UOM.Trim()),
						PXErrorLevel.RowInfo));
			}
		}

        public void SOPickPackShipUserSetup_PromptChoice_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
        {
            SOPickPackShipUserSetup soPickPackShipUserSetup = e.Row as SOPickPackShipUserSetup;

            if (soPickPackShipUserSetup != null)
            {
                if (soPickPackShipUserSetup.AutoSelectLotSerialNumberLocation.HasValue && soPickPackShipUserSetup.AutoSelectLotSerialNumberLocation.Value)
                {
                    e.ReturnValue = "S";
                }
                else if (soPickPackShipUserSetup.PromptLocation.HasValue && soPickPackShipUserSetup.PromptLocation.Value)
                {
                    e.ReturnValue = "L";
                }
                else
                {
                    e.ReturnValue = "N";
                }
            }
        }

        public void SOPickPackShipUserSetup_PromptChoice_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
        {
            SOPickPackShipUserSetup soPickPackShipUserSetup = e.Row as SOPickPackShipUserSetup;

            if (soPickPackShipUserSetup != null && e.NewValue != null)
            {
                switch ((string)e.NewValue)
                {
                    case "S":
                        soPickPackShipUserSetup.AutoSelectLotSerialNumberLocation = true;
                        soPickPackShipUserSetup.PromptLocation = false;
                        break;

                    case "L":
                        soPickPackShipUserSetup.AutoSelectLotSerialNumberLocation = false;
                        soPickPackShipUserSetup.PromptLocation = true;
                        break;
                    default:
                        soPickPackShipUserSetup.AutoSelectLotSerialNumberLocation = false;
                        soPickPackShipUserSetup.PromptLocation = false;
                        break;
                }
            }
        }

        [PXLocalizable]
		public static class Msg
		{
			public const string Add = "Add";
			public const string Remove = "Remove";

			#region Barcode
			public const string BarcodeMissing = "Barcode {0} not found in database.";
			public const string BarcodePrompt = "Please scan a barcode.";
			#endregion

			#region Command
			public const string CommandAdd = "Add mode set.";
			public const string CommandClear = "Screen cleared.";
			public const string CommandInventory = "Ready to search by item barcode.";
			public const string CommandLot = "Ready to search by lot/serial number.";
			public const string CommandRemove = "Remove mode set.";
			public const string CommandUnknown = "Unknown command.";
			public const string CommandSetQuantity = "Quantity set to {0}.";
			public const string CommandAccessRightsError = "Insufficient access rights to perform this command.";
			#endregion

			#region Lot/Serial
			public const string LotExpired = "Lot/serial {0} is expired.";
			public const string LotInvalidQuantity = "Lot/serial {0} not found in sufficient quantity on shipment.";
			public const string LotMissing = "Lot/serial {0} not found in database.";
			public const string LotNotSupported = "Lot/serial numbers that are assigned when used and which require tracking of expiration date are not supported with this tool.";
			public const string LotNotSupportedUOM = "Items which are lot/serial tracked must use the same base and sale unit of measures.";
			public const string LotScanPrompt = "Please scan lot/serial number.";
			public const string LotSplitQuantityError = "Unexpected split quantity for lot/serial {0} (Quantity: {1}).";
			public const string LotUniquenessError = "More than one lot/serial entry was found. This is not yet supported, please search by Inventory ID.";
			public const string SerialDuplicateError = "Serial {0} has already been picked.";
			public const string SerialInvalidQuantity = "Quantity for serial numbered items must be 1.";
			#endregion

			#region Location
			public const string LocationPrompt = "Please scan location.";
			public const string LocationInvalid = "Location {0} not found in database.";

			#endregion

			#region Inventory
			public const string InventoryAdded = "Added {0} x {1}.";
			public const string InventoryRemoved = "Removed {0} x {1}.";
			public const string InventoryMissing = "Item not found on shipment or in current package.";
			public const string InventoryUpdated = "Picked quantity updated from {0} to {1} {2}.";
			#endregion

			#region Package
			public const string PackageBoxMissing = "Box {0} not found in database.";
			public const string PackageComplete = "Package is complete. Weight: {0:0.0000} {1}";
			public const string PackageCompletePrompt = "Please complete the current package using the {0}{1} command.";
			public const string PackageCommandMissingBoxId = "The New Package command must be followed by a Box ID.";
			public const string PackageIncompleteError = "Please complete the current package using the {0}{1} command.";
			public const string PackageInvalidFileExtension = "Unsupported file extension attached to the package for Shipment {0}/{1}";
			public const string PackageInvalidWeight = "{0} is not a valid weight.";
			public const string PackageLineNbrMissing = "Unable to find package line {0} - was it deleted manually?";
			public const string PackageMissingCurrent = "There is no package currently selected or in process.";
			public const string PackageRemoveInventoryError = "The system was not able to locate package details for the item you just removed.";
			public const string PackageWeightAutoCalcFailedPrompt = "Weight couldn't be calculated automatically. Please enter package total weight and press enter.";
			public const string PackageWeightPrompt = "Please enter package total weight and press enter.";
			public const string PackageWrongWeightUnit = "Wrong weight unit: {0}, only KG and LB are supported.";
			#endregion

			#region Shipment
			public const string ShipmentConfirmedFull = "Shipment {0} confirmed in full.";
			public const string ShipmentConfirmedPicked = "Shipment {0} confirmed as picked.";
			public const string ShipmentLineMissing = "Line {0} not found in shipment.";
			public const string ShipmentMissing = "Shipment not found.";
			public const string ShipmentNbrMissing = "Shipment {0} not found.";
			public const string ShipmentInvalid = "Shipment {0} status is invalid for processing.";
			public const string ShipmentQuantityMismatchPrompt = "The quantity picked for one or more lines doesn't match with the shipment. Do you want to continue?";
			public const string ShipmentReady = "Shipment {0} loaded and ready to pick.";

			public const string PackageForShipment = "Package for shipment {0}";
			#endregion

			#region Scale
			public const string ScaleMissing = "Scale {0} not found in database.";
			public const string ScaleTimeout = "Measurement on scale {0} is more than {1} seconds old. Remove package from the scale and weigh it again.";
			#endregion
		}
	}

	public class PickPackShipCacheManager : IPXCustomInfo
	{
		public void Complete(PXLongRunStatus status, PXGraph graph)
		{
			// AC-86693
			// When you confirm the shipment, system starts a long-run operation to invoke Confirm on the shipment screen. 
			// If any error is raised during the confirm shipment (ex: "At least one package is required."), the process aborts. 
			// The problem is that normal behaviour in Acumatica is to clear caches, and reload from DB. 
			// The result is that you loose all the state that was stored in caches as part of the picking/packing process, and have to start over.
			// This flag works around that limitation by blocking Clear() call on the graph after abortion of the long-run operation.
			if (status == PXLongRunStatus.Aborted && graph is PickPackShip)
			{
				((PickPackShip)graph).IgnoreClear = true;
			}
		}
	}
}