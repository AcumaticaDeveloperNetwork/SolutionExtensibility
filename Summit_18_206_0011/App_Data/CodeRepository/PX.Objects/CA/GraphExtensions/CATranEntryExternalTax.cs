﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects.TX.GraphExtensions;
using PX.TaxProvider;

namespace PX.Objects.CA
{
	public class CATranEntryExternalTax :  ExternalTax<CATranEntry, CAAdj>
	{
		public static bool IsActive()
		{
			return PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();
		}

		public override CAAdj CalculateExternalTax(CAAdj invoice)
		{
			var toAddress = GetToAddress(invoice);
			bool isNonTaxable = IsNonTaxable(toAddress);

			if (isNonTaxable)
			{
				ApplyTax(invoice, GetTaxResult.Empty);
				invoice.IsTaxValid = true;
				invoice.NonTaxable = true;
				invoice = Base.CAAdjRecords.Update(invoice);

				SkipTaxCalcAndSave();

				return invoice;
			}
			else if (invoice.NonTaxable == true)
			{
				Base.CurrentDocument.SetValueExt<CAAdj.nonTaxable>(invoice, false);
			}

			var service = TaxProviderFactory(Base, invoice.TaxZoneID);

			GetTaxRequest getRequest = BuildGetTaxRequest(invoice);

			if (getRequest.CartItems.Count == 0)
			{
				invoice.IsTaxValid = true;
				invoice.IsTaxSaved = false;
				invoice = Base.CAAdjRecords.Update(invoice);
				SkipTaxCalcAndSave();
			}

			GetTaxResult result = service.GetTax(getRequest);
			if (result.IsSuccess)
			{
				try
				{
					ApplyTax(invoice, result);
					SkipTaxCalcAndSave();
				}
				catch (PXOuterException ex)
				{
					try
					{
						CancelTax(invoice, VoidReasonCode.Unspecified);
					}
					catch (Exception)
					{
						throw new PXException(new PXException(ex, TX.Messages.FailedToApplyTaxes), TX.Messages.FailedToCancelTaxes);
					}

					string msg = TX.Messages.FailedToApplyTaxes;
					foreach (string err in ex.InnerMessages)
					{
						msg += Environment.NewLine + err;
					}

					throw new PXException(ex, msg);
				}
				catch (Exception ex)
				{
					try
					{
						CancelTax(invoice, VoidReasonCode.Unspecified);
					}
					catch (Exception)
					{
						throw new PXException(new PXException(ex, TX.Messages.FailedToApplyTaxes), TX.Messages.FailedToCancelTaxes);
					}

					string msg = TX.Messages.FailedToApplyTaxes;
					msg += Environment.NewLine + ex.Message;

					throw new PXException(ex, msg);
				}

				PostTaxRequest request = new PostTaxRequest();
				request.CompanyCode = getRequest.CompanyCode;
				request.DocCode = getRequest.DocCode;
				request.DocDate = getRequest.DocDate;
				request.DocType = getRequest.DocType;
				request.TotalAmount = result.TotalAmount;
				request.TotalTaxAmount = result.TotalTaxAmount;
				PostTaxResult postResult = service.PostTax(request);

				if (postResult.IsSuccess)
				{
					invoice.IsTaxValid = true;
					invoice = Base.CAAdjRecords.Update(invoice);
					SkipTaxCalcAndSave();
				}

			}
			else
			{
				LogMessages(result);

				throw new PXException(TX.Messages.FailedToGetTaxes);
			}

			return invoice;
		}

		[PXOverride]
		public virtual void Persist()
		{
			if (Base.CAAdjRecords.Current != null && IsExternalTax(Base.CAAdjRecords.Current.TaxZoneID) && Base.CAAdjRecords.Current.IsTaxValid != true && !skipExternalTaxCalcOnSave)
			{
				PXLongOperation.StartOperation(Base, delegate ()
				{
					CAAdj doc = new CAAdj();
					doc.AdjRefNbr = Base.CAAdjRecords.Current.RefNbr;
					CAExternalTaxCalc.Process(doc);
				});
			}
		}

		protected virtual void _(Events.RowUpdated<CASplit> e)
		{
			//if any of the fields that was saved in avalara has changed mark doc as TaxInvalid.
			if (Base.CAAdjRecords.Current != null && IsExternalTax(Base.CAAdjRecords.Current.TaxZoneID) &&
			    !e.Cache.ObjectsEqual<CASplit.accountID, CASplit.inventoryID, CASplit.tranDesc, CASplit.tranAmt, CASplit.taxCategoryID>(e.Row, e.OldRow))
			{
				Base.CAAdjRecords.Current.IsTaxValid = false;
				Base.CAAdjRecords.Update(Base.CAAdjRecords.Current);
			}
		}

		protected virtual void _(Events.RowSelected<CAAdj> e)
		{
			if (e.Row == null)
				return;

			if (IsExternalTax(e.Row.TaxZoneID) && e.Row.IsTaxValid != true)
				PXUIFieldAttribute.SetWarning<CAAdj.curyTaxTotal>(e.Cache, e.Row, AR.Messages.TaxIsNotUptodate);
		}

		protected virtual GetTaxRequest BuildGetTaxRequest(CAAdj invoice)
		{
			if (invoice == null) throw new PXArgumentException(nameof(invoice), ErrorMessages.ArgumentNullException);

			GetTaxRequest request = new GetTaxRequest();
			request.CompanyCode = CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
			request.CurrencyCode = invoice.CuryID;
			request.CustomerCode = "N/A";
			IAddressBase fromAddress = GetToAddress(invoice);
			IAddressBase toAddress = fromAddress;

			if (fromAddress == null)
				throw new PXException(Messages.FailedGetFrom);

			if (toAddress == null)
				throw new PXException(Messages.FailedGetTo);

			request.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
			request.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
			request.DocCode = $"CA.{invoice.AdjRefNbr}";
			request.DocDate = invoice.TranDate.GetValueOrDefault();
			request.LocationCode = GetExternalTaxProviderLocationCode(invoice);
			Location branchLoc = GetBranchLocation(invoice);

			if (branchLoc != null)
			{
				request.CustomerUsageType = branchLoc.CAvalaraCustomerUsageType;
				request.ExemptionNo = branchLoc.CAvalaraExemptionNumber;
			}

			request.DocType = TaxDocumentType.PurchaseInvoice;
			int mult = 1;

			if (invoice.DrCr == CADrCr.CADebit)
				request.DocType = TaxDocumentType.SalesInvoice;
			else
				request.DocType = TaxDocumentType.PurchaseInvoice;

			PXSelectBase<CASplit> select = new PXSelectJoin<CASplit,
				LeftJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<CASplit.inventoryID>>>,
				Where<CASplit.adjRefNbr, Equal<Current<CAAdj.adjRefNbr>>>,
				OrderBy<Asc<CASplit.adjRefNbr, Asc<CASplit.lineNbr>>>>(Base);

			foreach (PXResult<CASplit, InventoryItem> res in select.View.SelectMultiBound(new object[] { invoice }))
			{
				CASplit tran = (CASplit)res;
				InventoryItem item = (InventoryItem)res;

				var line = new TaxCartItem
				{
					Index = tran.LineNbr ?? 0,
					Amount = mult * tran.CuryTranAmt.GetValueOrDefault(),
					Description = tran.TranDesc,
					DestinationAddress = request.DestinationAddress,
					OriginAddress = request.OriginAddress,
					ItemCode = item.InventoryCD,
					Quantity = Math.Abs(tran.Qty.GetValueOrDefault()),
					Discounted = request.Discount > 0,
					TaxCode = tran.TaxCategoryID,
				};

				request.CartItems.Add(line);
			}

			return request;
		}

		protected virtual void ApplyTax(CAAdj invoice, GetTaxResult result)
		{
			TaxZone taxZone = null;
			AP.Vendor vendor = null;
			if (result.TaxSummary.Length > 0)
			{
				taxZone = (TaxZone)Base.taxzone.View.SelectSingleBound(new object[] { invoice });
				vendor = PXSelect<AP.Vendor, Where<AP.Vendor.bAccountID, Equal<Required<AP.Vendor.bAccountID>>>>.Select(Base, taxZone.TaxVendorID);
				if (vendor == null)
					throw new PXException(Messages.ExternalTaxVendorNotFound);
			}
			//Clear all existing Tax transactions:
			foreach (PXResult<CATaxTran, Tax> res in Base.Taxes.View.SelectMultiBound(new object[] { invoice }))
			{
				CATaxTran taxTran = (CATaxTran)res;
				Base.Taxes.Delete(taxTran);
			}

			Base.Views.Caches.Add(typeof(Tax));

			for (int i = 0; i < result.TaxSummary.Length; i++)
			{
				string taxID = result.TaxSummary[i].TaxName;
				if (string.IsNullOrEmpty(taxID))
					taxID = result.TaxSummary[i].JurisCode;

				if (string.IsNullOrEmpty(taxID))
				{
					PXTrace.WriteInformation(Messages.EmptyValuesFromExternalTaxProvider);
					continue;
				}

				//Insert Tax if not exists - just for the selectors sake
				Tax tx = PXSelect<Tax, Where<Tax.taxID, Equal<Required<Tax.taxID>>>>.Select(Base, taxID);

				if (tx == null)
				{
					tx = new Tax
					{
						TaxID = taxID,
						Descr = PXMessages.LocalizeFormatNoPrefixNLA(TX.Messages.ExternalTaxProviderTaxId, taxID),
						TaxType = CSTaxType.Sales,
						TaxCalcType = CSTaxCalcType.Doc,
						TaxCalcLevel = result.TaxSummary[i].TaxCalculationLevel.ToCSTaxCalcLevel(),
						TaxApplyTermsDisc = CSTaxTermsDiscount.ToTaxableAmount,
						SalesTaxAcctID = vendor.SalesTaxAcctID,
						SalesTaxSubID = vendor.SalesTaxSubID,
						ExpenseAccountID = vendor.TaxExpenseAcctID,
						ExpenseSubID = vendor.TaxExpenseSubID,
						TaxVendorID = taxZone.TaxVendorID,
						IsExternal = true
					};

					Base.Caches[typeof(Tax)].Insert(tx);
				}

				CATaxTran tax = new CATaxTran
				{
					Module = BatchModule.CA,
					TranType = invoice.DocType,
					RefNbr = invoice.RefNbr,
					TaxID = taxID,
					CuryTaxAmt = Math.Abs(result.TaxSummary[i].TaxAmount),
					CuryTaxableAmt = Math.Abs(result.TaxSummary[i].TaxableAmount),
					TaxRate = Convert.ToDecimal(result.TaxSummary[i].Rate) * 100,
					TaxType = "S",
					TaxBucketID = 0,
					AccountID = vendor.SalesTaxAcctID,
					SubID = vendor.SalesTaxSubID,
					JurisType = result.TaxSummary[i].JurisType,
					JurisName = result.TaxSummary[i].JurisName
				};

				Base.Taxes.Insert(tax);
			}

			bool requireControlTotal = Base.casetup.Current.RequireControlTotal == true;
			if (invoice.Hold != true)
				Base.casetup.Current.RequireControlTotal = false;

			try
			{
				invoice.CuryTaxTotal = Math.Abs(result.TotalTaxAmount);
				Base.CAAdjRecords.Cache.SetValueExt<CAAdj.isTaxSaved>(invoice, true);
				Base.CAAdjRecords.Update(invoice);
			}
			finally
			{
				Base.casetup.Current.RequireControlTotal = requireControlTotal;
			}
		}

		protected virtual void CancelTax(CAAdj invoice, VoidReasonCode code)
		{
			var request = new VoidTaxRequest();
			request.CompanyCode = CompanyCodeFromBranch(invoice.TaxZoneID, invoice.BranchID);
			request.Code = code;
			request.DocCode = $"CA.{invoice.AdjRefNbr}";

			if (invoice.DrCr == CADrCr.CADebit)
				request.DocType = TaxDocumentType.SalesInvoice;
			else
				request.DocType = TaxDocumentType.PurchaseInvoice;

			var service = TaxProviderFactory(Base, invoice.TaxZoneID);
			var result = service.VoidTax(request);

			bool raiseError = false;
			if (!result.IsSuccess)
			{
				LogMessages(result);

				if (!result.IsSuccess && result.Messages.Any(t => t.Contains("DocumentNotFoundError")))
				{
					//just ignore this error. There is no document to delete in avalara.
				}
				else
				{
					raiseError = true;
				}
			}

			if (raiseError)
			{
				throw new PXException(TX.Messages.FailedToDeleteFromExternalTaxProvider);
			}
			else
			{
				invoice.IsTaxSaved = false;
				invoice.IsTaxValid = false;
				if (Base.CAAdjRecords.Cache.GetStatus(invoice) == PXEntryStatus.Notchanged)
					Base.CAAdjRecords.Cache.SetStatus(invoice, PXEntryStatus.Updated);
			}
		}

		protected virtual IAddressBase GetToAddress(CAAdj invoice)
		{
			PXSelectBase<Branch> select = new PXSelectJoin
				<Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Branch.bAccountID>>,
					InnerJoin<Address, On<Address.addressID, Equal<BAccountR.defAddressID>>>>,
					Where<Branch.branchID, Equal<Required<Branch.branchID>>>>(Base);

			foreach (PXResult<Branch, BAccountR, Address> res in select.Select(invoice.BranchID))
				return (Address)res;

			return null;
		}

		protected virtual Location GetBranchLocation(CAAdj invoice)
		{
			PXSelectBase<Branch> select = new PXSelectJoin
				<Branch, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Branch.bAccountID>>,
					InnerJoin<Location, On<Location.bAccountID, Equal<BAccountR.bAccountID>, And<Location.locationID, Equal<BAccountR.defLocationID>>>>>,
					Where<Branch.branchID, Equal<Required<Branch.branchID>>>>(Base);

			foreach (PXResult<Branch, BAccountR, Location> res in select.Select(invoice.BranchID))
				return (Location)res;

			return null;
		}
	}
}