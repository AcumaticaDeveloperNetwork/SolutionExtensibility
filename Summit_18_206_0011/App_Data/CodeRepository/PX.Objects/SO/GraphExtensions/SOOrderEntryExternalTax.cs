﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.CS.Contracts.Interfaces;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.TaxProvider;

namespace PX.Objects.SO
{
	public class SOOrderEntryExternalTax : ExternalTax<SOOrderEntry, SOOrder>
	{
		public static bool IsActive()
		{
			return PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();
		}

		public override SOOrder CalculateExternalTax(SOOrder order)
		{
			if (IsExternalTax(order.TaxZoneID))
				return CalculateExternalTax(order, false);

			return order;
		}

		public SOOrder CalculateExternalTax(SOOrder order, bool forceRecalculate)
		{
			var toAddress = GetToAddress(order);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			Debug.Print("{0} Enter CalculateTax", DateTime.Now.TimeOfDay);
			Debug.Indent();

			var service = TaxProviderFactory(Base, order.TaxZoneID);

			GetTaxRequest getRequest = null;
			GetTaxRequest getRequestOpen = null;
			GetTaxRequest getRequestUnbilled = null;
			GetTaxRequest getRequestFreight = null;

			bool isValidByDefault = true;

			SOOrderType orderType = PXSelect<SOOrderType, Where<SOOrderType.orderType, Equal<Required<SOOrder.orderType>>>>.Select(this.Base, order.OrderType);

			if (orderType.INDocType != INTranType.Transfer && !IsNonTaxable(toAddress))
			{
				if (order.IsTaxValid != true || forceRecalculate)
				{
					getRequest = BuildGetTaxRequest(order);

					if (getRequest.CartItems.Count > 0)
					{
						isValidByDefault = false;
					}
					else
					{
						getRequest = null;
					}
				}

				if (order.IsOpenTaxValid != true || forceRecalculate)
				{
					getRequestOpen = BuildGetTaxRequestOpen(order);
					if (getRequestOpen.CartItems.Count > 0)
					{
						isValidByDefault = false;
					}
					else
					{
						getRequestOpen = null;
					}
				}

				if (order.IsUnbilledTaxValid != true || forceRecalculate)
				{
					getRequestUnbilled = BuildGetTaxRequestUnbilled(order);
					if (getRequestUnbilled.CartItems.Count > 0)
					{
						isValidByDefault = false;
					}
					else
					{
						getRequestUnbilled = null;
					}
				}

				if (order.IsFreightTaxValid != true || forceRecalculate)
				{
					getRequestFreight = BuildGetTaxRequestFreight(order);
					if (getRequestFreight.CartItems.Count > 0)
					{
						isValidByDefault = false;
					}
					else
					{
						getRequestFreight = null;
					}
				}
			}

			if (isValidByDefault)
			{
				order.CuryTaxTotal = 0;
				order.CuryOpenTaxTotal = 0;
				order.CuryUnbilledTaxTotal = 0;
				order.IsTaxValid = true;
				order.IsOpenTaxValid = true;
				order.IsUnbilledTaxValid = true;
				order.IsFreightTaxValid = true;

				Base.Document.Update(order);

				foreach (SOTaxTran item in Base.Taxes.Select())
				{
					Base.Taxes.Delete(item);
				}

				using (var ts = new PXTransactionScope())
				{
					Base.Persist(typeof(SOTaxTran), PXDBOperation.Delete);
					Base.Persist(typeof(SOOrder), PXDBOperation.Update);
					PXTimeStampScope.PutPersisted(Base.Document.Cache, order, PXDatabase.SelectTimeStamp());
					ts.Complete();
				}
				return order;
			}

			GetTaxResult result = null;
			GetTaxResult resultOpen = null;
			GetTaxResult resultUnbilled = null;
			GetTaxResult resultFreight = null;

			bool getTaxFailed = false;
			if (getRequest != null)
			{
				Stopwatch sw2 = new Stopwatch();
				sw2.Start();
				result = service.GetTax(getRequest);
				sw2.Stop();
				Debug.Print("{0} GetTax(request) in {1} millisec", DateTime.Now.TimeOfDay, sw2.ElapsedMilliseconds);

				if (!result.IsSuccess)
				{
					getTaxFailed = true;
				}
			}
			if (getRequestOpen != null)
			{
				if (getRequest != null && IsSame(getRequest, getRequestOpen))
				{
					resultOpen = result;
				}
				else
				{
					Stopwatch sw2 = new Stopwatch();
					sw2.Start();
					resultOpen = service.GetTax(getRequestOpen);
					sw2.Stop();
					Debug.Print("{0} GetTax(requestOpen) in {1} millisec", DateTime.Now.TimeOfDay, sw2.ElapsedMilliseconds);
					if (!resultOpen.IsSuccess)
					{
						getTaxFailed = true;
					}
				}
			}
			if (getRequestUnbilled != null)
			{
				if (getRequest != null && IsSame(getRequest, getRequestUnbilled))
				{
					resultUnbilled = result;
				}
				else
				{
					Stopwatch sw2 = new Stopwatch();
					sw2.Start();
					resultUnbilled = service.GetTax(getRequestUnbilled);
					sw2.Stop();
					Debug.Print("{0} GetTax(requestUnbilled) in {1} millisec", DateTime.Now.TimeOfDay, sw2.ElapsedMilliseconds);
					if (!resultUnbilled.IsSuccess)
					{
						getTaxFailed = true;
					}
				}
			}
			if (getRequestFreight != null)
			{
				Stopwatch sw2 = new Stopwatch();
				sw2.Start();
				resultFreight = service.GetTax(getRequestFreight);
				sw2.Stop();
				Debug.Print("{0} GetTax(requestFreight) in {1} millisec", DateTime.Now.TimeOfDay, sw2.ElapsedMilliseconds);
				if (!resultFreight.IsSuccess)
				{
					getTaxFailed = true;
				}
			}

			if (!getTaxFailed)
			{
				try
				{
					ApplyTax(order, result, resultOpen, resultUnbilled, resultFreight);
					Stopwatch sw2 = new Stopwatch();
					sw2.Start();
					order.IsTaxValid = true;
					order.IsOpenTaxValid = true;
					order.IsUnbilledTaxValid = true;
					order.IsFreightTaxValid = true;

					sw2.Stop();
					Debug.Print("{0} PXDatabase.Update<SOOrder> in {1} millisec", DateTime.Now.TimeOfDay, sw2.ElapsedMilliseconds);
					using (var ts = new PXTransactionScope())
					{
						Base.Document.Update(order);
						Base.Persist(typeof(SOOrder), PXDBOperation.Update);
						PXTimeStampScope.PutPersisted(Base.Document.Cache, order, PXDatabase.SelectTimeStamp());
						ts.Complete();
					}
				}
				catch (PXOuterException ex)
				{
					string msg = TX.Messages.FailedToApplyTaxes;
					foreach (string err in ex.InnerMessages)
					{
						msg += Environment.NewLine + err;
					}

					throw new PXException(ex, msg);
				}
				catch (Exception ex)
				{
					string msg = TX.Messages.FailedToApplyTaxes;
					msg += Environment.NewLine + ex.Message;

					throw new PXException(ex, msg);
				}
			}
			else
			{
				ResultBase taxResult = result ?? resultOpen ?? resultUnbilled ?? resultFreight;
				if (taxResult != null)
					LogMessages(taxResult);

				throw new PXException(TX.Messages.FailedToGetTaxes);
			}

			sw.Stop();
			Debug.Unindent();
			Debug.Print("{0} Exit CalculateTax in {1} millisec", DateTime.Now.TimeOfDay, sw.ElapsedMilliseconds);

			return order;
		}

		[PXOverride]
		public virtual void RecalculateExternalTaxes()
		{
			if (Base.Document.Current != null && IsExternalTax(Base.Document.Current.TaxZoneID) && !skipExternalTaxCalcOnSave && !Base.IsTransferOrder &&
			    (Base.Document.Current.IsTaxValid != true || Base.Document.Current.IsOpenTaxValid != true || Base.Document.Current.IsUnbilledTaxValid != true)
			)
			{
				if (Base.RecalculateExternalTaxesSync)
				{
					SOOrder doc = new SOOrder();
					doc.OrderType = Base.Document.Current.OrderType;
					doc.OrderNbr = Base.Document.Current.OrderNbr;
					SOExternalTaxCalc.Process(doc);

				}
				else
				{
					Debug.Print("{0} SOExternalTaxCalc.Process(doc) Async", DateTime.Now.TimeOfDay);
					PXLongOperation.StartOperation(Base, delegate ()
					{
						Debug.Print("{0} Inside PXLongOperation.StartOperation", DateTime.Now.TimeOfDay);
						SOOrder doc = new SOOrder();
						doc.OrderType = Base.Document.Current.OrderType;
						doc.OrderNbr = Base.Document.Current.OrderNbr;
						SOExternalTaxCalc.Process(doc);

					});
				}
			}
		}

		public PXAction<SOOrder> recalcExternalTax;
		[PXUIField(DisplayName = "Recalculate External Tax", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Select)]
		[PXButton()]
		public virtual IEnumerable RecalcExternalTax(PXAdapter adapter)
		{
			if (Base.Document.Current != null && IsExternalTax(Base.Document.Current.TaxZoneID))
			{
				var order = Base.Document.Current;
				CalculateExternalTax(Base.Document.Current, true);

				Base.Clear(PXClearOption.ClearAll);
				Base.Document.Current = Base.Document.Search<SOOrder.orderNbr>(order.OrderNbr, order.OrderType);

				yield return Base.Document.Current;
			}
			else
			{
				foreach (var res in adapter.Get())
				{
					yield return res;
				}
			}
		}

		protected virtual void _(Events.RowSelected<SOOrder> e)
		{
			if (e.Row == null)
				return;

			var isExternalTax = IsExternalTax(e.Row.TaxZoneID);

			if (isExternalTax == true && ((SOOrder)e.Row).IsTaxValid != true && !Base.IsTransferOrder)
			{
				PXUIFieldAttribute.SetWarning<SOOrder.curyTaxTotal>(e.Cache, e.Row, AR.Messages.TaxIsNotUptodate);
			}
			else if (isExternalTax == true && ((SOOrder)e.Row).IsFreightTaxValid != true && !Base.IsTransferOrder)
				PXUIFieldAttribute.SetWarning<SOOrder.curyTaxTotal>(e.Cache, e.Row, AR.Messages.TaxIsNotUptodate);

			if (isExternalTax == true && ((SOOrder)e.Row).IsOpenTaxValid != true && !Base.IsTransferOrder)
				PXUIFieldAttribute.SetWarning<SOOrder.curyOpenTaxTotal>(e.Cache, e.Row, AR.Messages.TaxIsNotUptodate);

			if (isExternalTax == true && ((SOOrder)e.Row).IsUnbilledTaxValid != true && !Base.IsTransferOrder)
			{
				PXUIFieldAttribute.SetWarning<SOOrder.curyUnbilledTaxTotal>(e.Cache, e.Row, AR.Messages.TaxIsNotUptodate);
				PXUIFieldAttribute.SetWarning<SOOrder.curyUnbilledOrderTotal>(e.Cache, e.Row, PX.Objects.SO.Messages.UnbilledBalanceWithoutTaxTaxIsNotUptodate);
			}


			Base.Taxes.Cache.AllowInsert = !isExternalTax;
			Base.Taxes.Cache.AllowUpdate = !isExternalTax;
			Base.Taxes.Cache.AllowDelete = !isExternalTax;
		}

		protected virtual void _(Events.RowUpdated<SOOrder> e)
		{
			//if any of the fields that was saved in avalara has changed mark doc as TaxInvalid.
			if (IsExternalTax(e.Row.TaxZoneID))
			{
				if (!e.Cache.ObjectsEqual<SOOrder.avalaraCustomerUsageType, SOOrder.orderDate, SOOrder.taxZoneID, SOOrder.customerID, SOOrder.shipAddressID, SOOrder.willCall>(e.Row, e.OldRow))
				{
					e.Row.IsTaxValid = false;
					e.Row.IsOpenTaxValid = false;
					e.Row.IsUnbilledTaxValid = false;
				}

				if (!e.Cache.ObjectsEqual<SOLine.openAmt>(e.Row, e.OldRow))
				{
					e.Row.IsOpenTaxValid = false;
				}

				if (!e.Cache.ObjectsEqual<SOLine.unbilledAmt>(e.Row, e.OldRow))
				{
					e.Row.IsUnbilledTaxValid = false;
				}

				if (!e.Cache.ObjectsEqual<SOOrder.curyFreightTot, SOOrder.freightTaxCategoryID>(e.OldRow, e.Row))
				{
					e.Row.IsFreightTaxValid = false;
					e.Row.IsTaxValid = false;
					e.Row.IsOpenTaxValid = false;
					e.Row.IsUnbilledTaxValid = false;
				}
			}
		}

		protected virtual void _(Events.RowInserted<SOLine> e)
		{
			if(e.Cache.Graph.IsCopyPasteContext)
			{
				//if any of the fields that was saved in avalara has changed mark doc as TaxInvalid.
				InvalidateExternalTax(Base.Document.Current);
			}
		}

		protected virtual void _(Events.RowUpdated<SOLine> e)
		{
			//if any of the fields that was saved in avalara has changed mark doc as TaxInvalid.
			if (Base.Document.Current != null && IsExternalTax(Base.Document.Current.TaxZoneID))
			{
				if (!e.Cache.ObjectsEqual<
						SOLine.salesAcctID,
						SOLine.inventoryID,
						SOLine.tranDesc,
						SOLine.lineAmt,
						SOLine.orderDate,
						SOLine.taxCategoryID,
						SOLine.siteID
					>(e.Row, e.OldRow) ||
					(e.Row.POSource == INReplenishmentSource.DropShipToOrder) != (e.OldRow.POSource == INReplenishmentSource.DropShipToOrder))
				{
					InvalidateExternalTax(Base.Document.Current);
				}

				if (!e.Cache.ObjectsEqual<SOLine.openAmt>(e.Row, e.OldRow))
				{
					Base.Document.Current.IsOpenTaxValid = false;
				}

				if (!e.Cache.ObjectsEqual<SOLine.unbilledAmt>(e.Row, e.OldRow))
				{
					Base.Document.Current.IsUnbilledTaxValid = false;
				}
			}
		}

		#region SOShippingAddress Events
		protected virtual void _(Events.RowUpdated<SOShippingAddress> e)
		{
			if (e.Row == null) return;
			if (e.Cache.ObjectsEqual<SOShippingAddress.postalCode, SOShippingAddress.countryID, SOShippingAddress.state>(e.Row, e.OldRow) == false)
				InvalidateExternalTax(Base.Document.Current);
		}

		protected virtual void _(Events.RowInserted<SOShippingAddress> e)
		{
			if (e.Row == null) return;
			InvalidateExternalTax(Base.Document.Current);
		}

		protected virtual void _(Events.RowDeleted<SOShippingAddress> e)
		{
			if (e.Row == null) return;
			InvalidateExternalTax(Base.Document.Current);
		}

		protected virtual void _(Events.FieldUpdating<SOShippingAddress, SOShippingAddress.overrideAddress> e)
		{
			if (e.Row == null) return;
			InvalidateExternalTax(Base.Document.Current);
		} 
		#endregion

		protected virtual GetTaxRequest BuildGetTaxRequest(SOOrder order)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			Debug.Indent();

			if (order == null)
				throw new PXArgumentException(ErrorMessages.ArgumentNullException);


			Customer cust = (Customer)Base.customer.View.SelectSingleBound(new object[] { order });
			Location loc = (Location)Base.location.View.SelectSingleBound(new object[] { order });

			IAddressBase fromAddress = GetFromAddress(order);
			IAddressBase toAddress = GetToAddress(order);

			Debug.Print("{0} Select Customer, Location, Addresses in {1} millisec", DateTime.Now.TimeOfDay, sw.ElapsedMilliseconds);

			if (fromAddress == null)
				throw new PXException(Messages.FailedGetFromAddressSO);

			if (toAddress == null)
				throw new PXException(Messages.FailedGetToAddressSO);

			GetTaxRequest request = new GetTaxRequest();
			request.CompanyCode = CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
			request.CurrencyCode = order.CuryID;
			request.CustomerCode = cust.AcctCD;
			request.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
			request.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
			request.DocCode = string.Format("SO.{0}.{1}", order.OrderType, order.OrderNbr);
			request.DocDate = order.OrderDate.GetValueOrDefault();
			request.LocationCode = GetExternalTaxProviderLocationCode(order);

			Sign sign = Sign.Plus;

			if (!string.IsNullOrEmpty(order.AvalaraCustomerUsageType))
			{
				request.CustomerUsageType = order.AvalaraCustomerUsageType;
			}
			if (!string.IsNullOrEmpty(loc.CAvalaraExemptionNumber))
			{
				request.ExemptionNo = loc.CAvalaraExemptionNumber;
			}

			SOOrderType orderType = (SOOrderType)Base.soordertype.View.SelectSingleBound(new object[] { order });

			if (orderType.DefaultOperation == SOOperation.Receipt)
			{
				request.DocType = TaxDocumentType.ReturnOrder;
				sign = Sign.Minus;

				PXSelectBase<SOLine> selectLineWithInvoiceDate = new PXSelect<SOLine,
				Where<SOLine.orderType, Equal<Required<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Required<SOLine.orderNbr>>,
				And<SOLine.invoiceDate, IsNotNull>>>>(Base);

				SOLine soLine = selectLineWithInvoiceDate.SelectSingle(order.OrderType, order.OrderNbr);
				if (soLine != null && soLine.TranDate != null)
				{
					request.TaxOverride.Reason = Messages.ReturnReason;
					request.TaxOverride.TaxDate = soLine.TranDate.Value;
					request.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
				}

			}
			else
			{
				request.DocType = TaxDocumentType.SalesOrder;
			}


			PXSelectBase<SOLine> select = new PXSelectJoin<SOLine,
				LeftJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<SOLine.inventoryID>>,
					LeftJoin<Account, On<Account.accountID, Equal<SOLine.salesAcctID>>>>,
				Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>,
					And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>,
				OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>(Base);

			request.Discount = order.CuryDiscTot.GetValueOrDefault();

			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			foreach (PXResult<SOLine, InventoryItem, Account> res in select.View.SelectMultiBound(new object[] { order }))
			{
				SOLine tran = (SOLine)res;
				InventoryItem item = (InventoryItem)res;
				Account salesAccount = (Account)res;

				var line = new TaxCartItem();
				line.Index = tran.LineNbr ?? 0;

				if (orderType.DefaultOperation != tran.Operation)
					line.Amount = Sign.Minus * sign * tran.CuryLineAmt.GetValueOrDefault();
				else
					line.Amount = sign * tran.CuryLineAmt.GetValueOrDefault();

				line.Description = tran.TranDesc;
				line.DestinationAddress = AddressConverter.ConvertTaxAddress(GetToAddress(order, tran));
				line.OriginAddress = AddressConverter.ConvertTaxAddress(GetFromAddress(order, tran));
				line.ItemCode = item.InventoryCD;
				line.Quantity = Math.Abs(tran.Qty.GetValueOrDefault());
				line.Discounted = request.Discount > 0;
				line.RevAcct = salesAccount.AccountCD;

				line.TaxCode = tran.TaxCategoryID;

				request.CartItems.Add(line);
			}
			sw2.Stop();
			Debug.Print("{0} Select detail lines in {1} millisec.", DateTime.Now.TimeOfDay, sw2.ElapsedMilliseconds);
			
			Debug.Unindent();
			sw.Stop();
			Debug.Print("{0} BuildGetTaxRequest() in {1} millisec.", DateTime.Now.TimeOfDay, sw.ElapsedMilliseconds);

			return request;
		}

		protected virtual GetTaxRequest BuildGetTaxRequestOpen(SOOrder order)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			if (order == null)
				throw new PXArgumentException(ErrorMessages.ArgumentNullException);

			Customer cust = (Customer)Base.customer.View.SelectSingleBound(new object[] { order });
			Location loc = (Location)Base.location.View.SelectSingleBound(new object[] { order });

			IAddressBase fromAddress = GetFromAddress(order);
			IAddressBase toAddress = GetToAddress(order);

			if (fromAddress == null)
				throw new PXException(Messages.FailedGetFromAddressSO);

			if (toAddress == null)
				throw new PXException(Messages.FailedGetToAddressSO);

			GetTaxRequest request = new GetTaxRequest();
			request.CompanyCode = CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
			request.CurrencyCode = order.CuryID;
			request.CustomerCode = cust.AcctCD;
			request.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
			request.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
			request.DocCode = string.Format("SO.{0}.{1}", order.OrderType, order.OrderNbr);
			request.DocDate = order.OrderDate.GetValueOrDefault();
			request.LocationCode = GetExternalTaxProviderLocationCode(order);

			int mult = 1;

			if (!string.IsNullOrEmpty(order.AvalaraCustomerUsageType))
			{
				request.CustomerUsageType = order.AvalaraCustomerUsageType;
			}
			if (!string.IsNullOrEmpty(loc.CAvalaraExemptionNumber))
			{
				request.ExemptionNo = loc.CAvalaraExemptionNumber;
			}

			SOOrderType orderType = (SOOrderType)Base.soordertype.View.SelectSingleBound(new object[] { order });

			if (orderType.DefaultOperation == SOOperation.Receipt)
			{
				request.DocType = TaxDocumentType.ReturnOrder;
				mult = -1;

				PXSelectBase<SOLine> selectLineWithInvoiceDate = new PXSelect<SOLine,
				Where<SOLine.orderType, Equal<Required<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Required<SOLine.orderNbr>>,
				And<SOLine.invoiceDate, IsNotNull>>>>(Base);

				SOLine soLine = selectLineWithInvoiceDate.SelectSingle(order.OrderType, order.OrderNbr);
				if (soLine != null && soLine.TranDate != null)
				{
					request.TaxOverride.Reason = Messages.ReturnReason;
					request.TaxOverride.TaxDate = soLine.TranDate.Value;
					request.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
				}

			}
			else
			{
				request.DocType = TaxDocumentType.SalesOrder;
			}


			PXSelectBase<SOLine> select = new PXSelectJoin<SOLine,
				LeftJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<SOLine.inventoryID>>,
					LeftJoin<Account, On<Account.accountID, Equal<SOLine.salesAcctID>>>>,
				Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>,
					And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>,
				OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>(Base);

			request.Discount = order.CuryDiscTot.GetValueOrDefault();

			foreach (PXResult<SOLine, InventoryItem, Account> res in select.View.SelectMultiBound(new object[] { order }))
			{
				SOLine tran = (SOLine)res;
				InventoryItem item = (InventoryItem)res;
				Account salesAccount = (Account)res;

				if (tran.OpenAmt >= 0)
				{
					var line = new TaxCartItem();
					line.Index = tran.LineNbr ?? 0;
					if (orderType.DefaultOperation != tran.Operation)
						line.Amount = -1 * mult * tran.CuryOpenAmt.GetValueOrDefault();
					else
						line.Amount = mult * tran.CuryOpenAmt.GetValueOrDefault();
					line.Description = tran.TranDesc;
					line.DestinationAddress = AddressConverter.ConvertTaxAddress(GetToAddress(order, tran));
					line.OriginAddress = AddressConverter.ConvertTaxAddress(GetFromAddress(order, tran));
					line.ItemCode = item.InventoryCD;
					line.Quantity = Math.Abs(tran.OpenQty.GetValueOrDefault());
					line.Discounted = request.Discount > 0;
					line.RevAcct = salesAccount.AccountCD;

					line.TaxCode = tran.TaxCategoryID;

					request.CartItems.Add(line);
				}
			}
			
			sw.Stop();
			Debug.Print("BuildGetTaxRequestOpen() in {0} millisec.", sw.ElapsedMilliseconds);

			return request;
		}

		protected virtual GetTaxRequest BuildGetTaxRequestUnbilled(SOOrder order)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			if (order == null)
				throw new PXArgumentException(ErrorMessages.ArgumentNullException);

			Customer cust = (Customer)Base.customer.View.SelectSingleBound(new object[] { order });
			Location loc = (Location)Base.location.View.SelectSingleBound(new object[] { order });

			IAddressBase fromAddress = GetFromAddress(order);
			IAddressBase toAddress = GetToAddress(order);

			if (fromAddress == null)
				throw new PXException(Messages.FailedGetFromAddressSO);

			if (toAddress == null)
				throw new PXException(Messages.FailedGetToAddressSO);

			GetTaxRequest request = new GetTaxRequest();
			request.CompanyCode = CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
			request.CurrencyCode = order.CuryID;
			request.CustomerCode = cust.AcctCD;
			request.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
			request.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
			request.DocCode = string.Format("{0}.{1}.Open", order.OrderType, order.OrderNbr);
			request.DocDate = order.OrderDate.GetValueOrDefault();
			request.LocationCode = GetExternalTaxProviderLocationCode(order);

			int mult = 1;

			if (!string.IsNullOrEmpty(order.AvalaraCustomerUsageType))
			{
				request.CustomerUsageType = order.AvalaraCustomerUsageType;
			}
			if (!string.IsNullOrEmpty(loc.CAvalaraExemptionNumber))
			{
				request.ExemptionNo = loc.CAvalaraExemptionNumber;
			}

			SOOrderType orderType = (SOOrderType)Base.soordertype.View.SelectSingleBound(new object[] { order });

			if (orderType.DefaultOperation == SOOperation.Receipt)
			{
				request.DocType = TaxDocumentType.ReturnOrder;
				mult = -1;

				PXSelectBase<SOLine> selectLineWithInvoiceDate = new PXSelect<SOLine,
				Where<SOLine.orderType, Equal<Required<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Required<SOLine.orderNbr>>,
				And<SOLine.invoiceDate, IsNotNull>>>>(Base);

				SOLine soLine = selectLineWithInvoiceDate.SelectSingle(order.OrderType, order.OrderNbr);
				if (soLine != null && soLine.TranDate != null)
				{
					request.TaxOverride.Reason = Messages.ReturnReason;
					request.TaxOverride.TaxDate = soLine.TranDate.Value;
					request.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
				}

			}
			else
			{
				request.DocType = TaxDocumentType.SalesOrder;
			}


			PXSelectBase<SOLine> select = new PXSelectJoin<SOLine,
				LeftJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<SOLine.inventoryID>>,
					LeftJoin<Account, On<Account.accountID, Equal<SOLine.salesAcctID>>>>,
				Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>,
					And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>,
				OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>(Base);

			request.Discount = order.CuryDiscTot.GetValueOrDefault();

			foreach (PXResult<SOLine, InventoryItem, Account> res in select.View.SelectMultiBound(new object[] { order }))
			{
				SOLine tran = (SOLine)res;
				InventoryItem item = (InventoryItem)res;
				Account salesAccount = (Account)res;

				if (tran.UnbilledAmt >= 0)
				{
					var line = new TaxCartItem();
					line.Index = tran.LineNbr ?? 0;
					if (orderType.DefaultOperation != tran.Operation)
						line.Amount = -1 * mult * tran.CuryUnbilledAmt.GetValueOrDefault();
					else
						line.Amount = mult * tran.CuryUnbilledAmt.GetValueOrDefault();
					line.Description = tran.TranDesc;
					line.DestinationAddress = AddressConverter.ConvertTaxAddress(GetToAddress(order, tran));
					line.OriginAddress = AddressConverter.ConvertTaxAddress(GetFromAddress(order, tran));
					line.ItemCode = item.InventoryCD;
					line.Quantity = Math.Abs(tran.UnbilledQty.GetValueOrDefault());
					line.Discounted = request.Discount > 0;
					line.RevAcct = salesAccount.AccountCD;

					line.TaxCode = tran.TaxCategoryID;

					request.CartItems.Add(line);
				}
			}
			
			sw.Stop();
			Debug.Print("BuildGetTaxRequestUnbilled() in {0} millisec.", sw.ElapsedMilliseconds);

			return request;
		}

		protected virtual GetTaxRequest BuildGetTaxRequestFreight(SOOrder order)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			
			if (order == null)
				throw new PXArgumentException(ErrorMessages.ArgumentNullException);

			Customer cust = (Customer)Base.customer.View.SelectSingleBound(new object[] { order });
			Location loc = (Location)Base.location.View.SelectSingleBound(new object[] { order });

			IAddressBase fromAddress = GetFromAddress(order);
			IAddressBase toAddress = GetToAddress(order);

			if (fromAddress == null)
				throw new PXException(Messages.FailedGetFromAddressSO);

			if (toAddress == null)
				throw new PXException(Messages.FailedGetToAddressSO);

			GetTaxRequest request = new GetTaxRequest();
			request.CompanyCode = CompanyCodeFromBranch(order.TaxZoneID, order.BranchID);
			request.CurrencyCode = order.CuryID;
			request.CustomerCode = cust.AcctCD;
			request.OriginAddress = AddressConverter.ConvertTaxAddress(fromAddress);
			request.DestinationAddress = AddressConverter.ConvertTaxAddress(toAddress);
			request.DocCode = $"{order.OrderType}.{order.OrderNbr}.Freight";
			request.DocDate = order.OrderDate.GetValueOrDefault();
			request.LocationCode = GetExternalTaxProviderLocationCode(order);

			int mult = 1;

			if (!string.IsNullOrEmpty(order.AvalaraCustomerUsageType))
			{
				request.CustomerUsageType = order.AvalaraCustomerUsageType;
			}
			if (!string.IsNullOrEmpty(loc.CAvalaraExemptionNumber))
			{
				request.ExemptionNo = loc.CAvalaraExemptionNumber;
			}

			SOOrderType orderType = (SOOrderType)Base.soordertype.View.SelectSingleBound(new object[] { order });

			if (orderType.ARDocType == ARDocType.CreditMemo)
			{
				request.DocType = TaxDocumentType.ReturnOrder;
				mult = -1;
			}
			else
			{
				request.DocType = TaxDocumentType.SalesOrder;
			}

			if (order.CuryFreightTot > 0)
			{
				var line = new TaxCartItem();
				line.Index = short.MaxValue;
				line.Quantity = 1;
				line.Amount = mult * order.CuryFreightTot.GetValueOrDefault();
				line.Description = PXMessages.LocalizeNoPrefix(Messages.FreightDesc);
				line.DestinationAddress = request.DestinationAddress;
				line.OriginAddress = request.OriginAddress;
				line.ItemCode = "N/A";
				line.Discounted = false;
				line.TaxCode = order.FreightTaxCategoryID;

				request.CartItems.Add(line);
			}

			sw.Stop();
			Debug.Print("BuildGetTaxRequestFreight() in {0} millisec.", sw.ElapsedMilliseconds);
			
			return request;
		}

		protected virtual void ApplyTax(SOOrder order, GetTaxResult result, GetTaxResult resultOpen, GetTaxResult resultUnbilled, GetTaxResult resultFreight)
		{
			
			TaxZone taxZone = (TaxZone)Base.taxzone.View.SelectSingleBound(new object[] { order });
			if (taxZone == null)
			{
				throw new PXException(Messages.TaxZoneIsNotSet);
			}

			AP.Vendor vendor = PXSelect<AP.Vendor, Where<AP.Vendor.bAccountID, Equal<Required<AP.Vendor.bAccountID>>>>.Select(Base, taxZone.TaxVendorID);

			if (vendor == null)
				throw new PXException(Messages.ExternalTaxVendorNotFound);

			var sign = ((SOOrderType)Base.soordertype.View.SelectSingleBound(new object[] { order })).DefaultOperation == SOOperation.Receipt
				? Sign.Minus
				: Sign.Plus;

			if (result != null)
			{
				//Clear all existing Tax transactions:
				PXSelectBase<SOTaxTran> TaxesSelect =
					new PXSelectJoin<SOTaxTran, InnerJoin<Tax, On<Tax.taxID, Equal<SOTaxTran.taxID>>>, Where<SOTaxTran.orderType,
						Equal<Current<SOOrder.orderType>>, And<SOTaxTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>(Base);
				foreach (PXResult<SOTaxTran, Tax> res in TaxesSelect.View.SelectMultiBound(new object[] { order }))
				{
					SOTaxTran taxTran = res;
					Base.Taxes.Delete(taxTran);
				}

				Base.Views.Caches.Add(typeof(Tax));

				decimal freightTax = 0;
				if (resultFreight != null)
					freightTax = sign * resultFreight.TotalTaxAmount;

				bool requireControlTotal = Base.soordertype.Current.RequireControlTotal == true;

				if (order.Hold != true)
					Base.soordertype.Current.RequireControlTotal = false;

				var taxDetails = new List<PX.TaxProvider.TaxDetail>();
				for (int i = 0; i < result.TaxSummary.Length; i++)
					taxDetails.Add(result.TaxSummary[i]);

				if (resultFreight != null)
				{
					for (int i = 0; i < resultFreight.TaxSummary.Length; i++)
						taxDetails.Add(resultFreight.TaxSummary[i]);
				}

				try
				{
					foreach (var taxDetail in taxDetails)
					{
						string taxID = taxDetail.TaxName;
						if (string.IsNullOrEmpty(taxID))
							taxID = taxDetail.JurisCode;

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
									Descr = PXMessages.LocalizeFormatNoPrefixNLA(TX.Messages.ExternalTaxProviderTaxFor, taxDetail.JurisType, taxDetail.JurisName),
									TaxType = CSTaxType.Sales,
									TaxCalcType = CSTaxCalcType.Doc,
									TaxCalcLevel = CSTaxCalcLevel.CalcOnItemAmt,
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

						var tax = new SOTaxTran
								{
									OrderType = order.OrderType,
									OrderNbr = order.OrderNbr,
									TaxID = taxID,
									CuryTaxAmt = sign * taxDetail.TaxAmount,
									CuryTaxableAmt = sign * taxDetail.TaxableAmount,
									CuryUnshippedTaxAmt = sign * taxDetail.TaxAmount,
									CuryUnshippedTaxableAmt = sign * taxDetail.TaxableAmount,
									CuryUnbilledTaxAmt = sign * taxDetail.TaxAmount,
									CuryUnbilledTaxableAmt = sign * taxDetail.TaxableAmount,
									TaxRate = Convert.ToDecimal(taxDetail.Rate) * 100,
									JurisType = taxDetail.JurisType,
									JurisName = taxDetail.JurisName
								};
						Base.Taxes.Insert(tax);

					}

					Base.Document.SetValueExt<SOOrder.curyTaxTotal>(order, sign * result.TotalTaxAmount + freightTax);
				}
				finally
				{
					Base.soordertype.Current.RequireControlTotal = requireControlTotal;
				}
			}


			if (resultUnbilled != null)
				Base.Document.SetValueExt<SOOrder.curyUnbilledTaxTotal>(order, sign * resultUnbilled.TotalTaxAmount);

			if (resultOpen != null)
				Base.Document.SetValueExt<SOOrder.curyOpenTaxTotal>(order, sign * resultOpen.TotalTaxAmount);

			Base.Document.Update(order);

			SkipTaxCalcAndSave();
		}

		protected virtual bool IsSame(GetTaxRequest x, GetTaxRequest y)
		{
			if (x.CartItems.Count != y.CartItems.Count)
				return false;

			for (int i = 0; i < x.CartItems.Count; i++)
			{
				if (x.CartItems[i].Amount != y.CartItems[i].Amount)
					return false;
			}

			return true;
		}

		protected virtual IAddressBase GetFromAddress(SOOrder order)
		{
			var branch =
				PXSelectJoin<Branch,
				InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Branch.bAccountID>>,
				InnerJoin<Address, On<Address.addressID, Equal<BAccountR.defAddressID>>>>,
				Where<Branch.branchID, Equal<Required<Branch.branchID>>>>
				.Select(Base, order.BranchID)
				.RowCast<Address>()
				.FirstOrDefault()
				.With(ValidAddressFrom<BAccountR.defAddressID>);

			return branch;
		}

		protected virtual IAddressBase GetFromAddress(SOOrder order, SOLine line)
		{
			Boolean isDropShip = line.POCreate == true && line.POSource == INReplenishmentSource.DropShipToOrder;
			IAddressBase vendorAddress = isDropShip
				? PXSelectJoin<Address,
					InnerJoin<Location, On<Location.defAddressID, Equal<Address.addressID>>,
					InnerJoin<Vendor, On<Vendor.defLocationID, Equal<Location.locationID>>>>,
					Where<Vendor.bAccountID, Equal<Current<SOLine.vendorID>>>>
					.SelectSingleBound(Base, new[] { line })
					.RowCast<Address>()
					.FirstOrDefault()
					.With(ValidAddressFrom<Vendor.defLocationID>)
				: null;
			return vendorAddress
				?? PXSelectJoin<Address,
					InnerJoin<INSite, On<Address.addressID, Equal<INSite.addressID>>>,
					Where<INSite.siteID, Equal<Current<SOLine.siteID>>>>
					.SelectSingleBound(Base, new[] { line })
					.RowCast<Address>()
					.FirstOrDefault()
					.With(ValidAddressFrom<INSite.addressID>)
				?? GetFromAddress(order);
		}

		protected virtual IAddressBase GetToAddress(SOOrder order)
		{
			if (order.WillCall == true)
				return GetFromAddress(order);
			else
				return ((SOShippingAddress)PXSelect<SOShippingAddress, Where<SOShippingAddress.addressID, Equal<Required<SOOrder.shipAddressID>>>>.Select(Base, order.ShipAddressID)).With(ValidAddressFrom<SOOrder.shipAddressID>);
		}

		protected virtual IAddressBase GetToAddress(SOOrder order, SOLine line)
		{
			if (order.WillCall == true && line.SiteID != null && !(line.POCreate == true && line.POSource == INReplenishmentSource.DropShipToOrder))
				return GetFromAddress(order, line); // will call
			else
				return ((SOShippingAddress)PXSelect<SOShippingAddress, Where<SOShippingAddress.addressID, Equal<Required<SOOrder.shipAddressID>>>>.Select(Base, order.ShipAddressID)).With(ValidAddressFrom<SOOrder.shipAddressID>);
		}

		private IAddressBase ValidAddressFrom<TFieldSource>(IAddressBase address)
			where TFieldSource : IBqlField
		{
			if (!string.IsNullOrEmpty(address.PostalCode)) return address;
			throw new PXException(PickAddressError<TFieldSource>(address));
		}

		private string PickAddressError<TFieldSource>(IAddressBase address)
			where TFieldSource : IBqlField
		{
			if (typeof(TFieldSource) == typeof(SOOrder.shipAddressID))
				return PXSelectReadonly<SOOrder, Where<SOOrder.shipAddressID, Equal<Required<Address.addressID>>>>
					.SelectWindowed(Base, 0, 1, ((SOAddress)address).AddressID).First().GetItem<SOOrder>()
					.With(e => PXMessages.LocalizeFormat(AR.Messages.AvalaraAddressSourceError, EntityHelper.GetFriendlyEntityName<SOOrder>(), new EntityHelper(Base).GetRowID(e)));

			if (typeof(TFieldSource) == typeof(Vendor.defLocationID))
				return PXSelectReadonly<Vendor, Where<Vendor.defLocationID, Equal<Required<Address.addressID>>>>
					.SelectWindowed(Base, 0, 1, ((Address)address).AddressID).First().GetItem<Vendor>()
					.With(e => PXMessages.LocalizeFormat(AR.Messages.AvalaraAddressSourceError, EntityHelper.GetFriendlyEntityName<Vendor>(), new EntityHelper(Base).GetRowID(e)));

			if (typeof(TFieldSource) == typeof(INSite.addressID))
				return PXSelectReadonly<INSite, Where<INSite.addressID, Equal<Required<Address.addressID>>>>
					.SelectWindowed(Base, 0, 1, ((Address)address).AddressID).First().GetItem<INSite>()
					.With(e => PXMessages.LocalizeFormat(AR.Messages.AvalaraAddressSourceError, EntityHelper.GetFriendlyEntityName<INSite>(), new EntityHelper(Base).GetRowID(e)));

			if (typeof(TFieldSource) == typeof(BAccountR.defAddressID))
				return PXSelectReadonly<BAccountR, Where<BAccountR.defAddressID, Equal<Required<Address.addressID>>>>
					.SelectWindowed(Base, 0, 1, ((Address)address).AddressID).First().GetItem<BAccountR>()
					.With(e => PXMessages.LocalizeFormat(AR.Messages.AvalaraAddressSourceError, EntityHelper.GetFriendlyEntityName<BAccountR>(), new EntityHelper(Base).GetRowID(e)));

			throw new ArgumentOutOfRangeException("Unknown address source used");
		}

		protected virtual bool IsCommonCarrier(string carrierID)
		{
			if (string.IsNullOrEmpty(carrierID))
			{
				return false; //pickup;
			}
			else
			{
				Carrier carrier = PXSelect<Carrier, Where<Carrier.carrierID, Equal<Required<Carrier.carrierID>>>>.Select(Base, carrierID);
				if (carrier == null)
				{
					return false;
				}
				else
				{
					return carrier.IsCommonCarrier == true;
				}
			}
		}

		private void InvalidateExternalTax(SOOrder order, bool keepFreight = false)
		{
			if (order == null || !IsExternalTax(order.TaxZoneID)) return;
			order.IsTaxValid = false;
			order.IsOpenTaxValid = false;
			order.IsUnbilledTaxValid = false;
			if (keepFreight == false)
				order.IsFreightTaxValid = false;
		}
	}
}
