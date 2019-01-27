using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX;
using PX.Objects.TX.GraphExtensions;
using PX.TaxProvider;

namespace PX.Objects.FS
{
    public class AppointmentEntryExternalTax : ExternalTax<AppointmentEntryBase, FSAppointment>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();
        }

        [PXOverride]
        public void Persist()
        {
            if (Base.AppointmentSelected.Current != null && IsExternalTax(Base.AppointmentSelected.Current.TaxZoneID) && !skipExternalTaxCalcOnSave && Base.AppointmentSelected.Current.IsTaxValid != true)
            {
                CalculateExternalTax(Base.AppointmentSelected.Current, Base.ServiceOrderRelated.Current);
            }
        }

        protected virtual void _(Events.RowInserted<FSAppointmentDetUNION> e)
        {
            if (IsExternalTax(Base.AppointmentSelected.Current.TaxZoneID))
            {
                Base.AppointmentSelected.Current.IsTaxValid = false;
                if (Base.AppointmentSelected.Cache.GetStatus(Base.AppointmentSelected.Current) == PXEntryStatus.Notchanged)
                    Base.AppointmentSelected.Cache.SetStatus(Base.AppointmentSelected.Current, PXEntryStatus.Updated);
            }
        }

        protected virtual void _(Events.RowDeleted<FSAppointmentDetUNION> e)
        {
            if (IsExternalTax(Base.AppointmentSelected.Current.TaxZoneID))
            {
                Base.AppointmentSelected.Current.IsTaxValid = false;
                if (Base.AppointmentSelected.Cache.GetStatus(Base.AppointmentSelected.Current) == PXEntryStatus.Notchanged)
                    Base.AppointmentSelected.Cache.SetStatus(Base.AppointmentSelected.Current, PXEntryStatus.Updated);
            }
        }
        protected virtual void _(Events.RowUpdated<FSAppointmentDetUNION> e)
        {
            if (IsExternalTax(Base.AppointmentSelected.Current.TaxZoneID))
            {
                Base.AppointmentSelected.Current.IsTaxValid = false;
                if (Base.AppointmentSelected.Cache.GetStatus(Base.AppointmentSelected.Current) == PXEntryStatus.Notchanged)
                    Base.AppointmentSelected.Cache.SetStatus(Base.AppointmentSelected.Current, PXEntryStatus.Updated);
            }
        }

        public override FSAppointment CalculateExternalTax(FSAppointment order)
        {
            return null;
        }

        public virtual FSAppointment CalculateExternalTax(FSAppointment order, FSServiceOrder serviceOrder)
        {
            var toAddress = GetToAddress(order, serviceOrder);
            bool isNonTaxable = IsNonTaxable(toAddress);

            if (isNonTaxable)
            {
                ApplyTax(order, GetTaxResult.Empty);
                order.IsTaxValid = true;
                order = Base.AppointmentSelected.Update(order);

                SkipTaxCalcAndSave();

                return order;
            }

            var service = TaxProviderFactory(Base, order.TaxZoneID);

            GetTaxRequest getRequest = null;
            bool isValidByDefault = true;

            if (order.IsTaxValid != true)
            {
                getRequest = BuildGetTaxRequest(order, serviceOrder);

                if (getRequest.CartItems.Count > 0)
                {
                    isValidByDefault = false;
                }
                else
                {
                    getRequest = null;
                }
            }

            if (isValidByDefault)
            {
                order.IsTaxValid = true;
                order = Base.AppointmentSelected.Update(order);
                SkipTaxCalcAndSave();

                return order;
            }

            GetTaxResult result = service.GetTax(getRequest);
            if (result.IsSuccess)
            {
                try
                {
                    ApplyTax(order, result);

                    order.IsTaxValid = true;
                    order = Base.AppointmentSelected.Update(order);
                    SkipTaxCalcAndSave();
                }
                catch (PXOuterException ex)
                {
                    string msg = PX.Objects.TX.Messages.FailedToApplyTaxes;
                    foreach (string err in ex.InnerMessages)
                    {
                        msg += Environment.NewLine + err;
                    }

                    throw new PXException(ex, msg);
                }
                catch (Exception ex)
                {
                    throw new PXException(ex, PX.Objects.TX.Messages.FailedToApplyTaxes);
                }
            }
            else
            {
                LogMessages(result);

                throw new PXException(PX.Objects.TX.Messages.FailedToGetTaxes);
            }

            return order;
        }

        protected GetTaxRequest BuildGetTaxRequest(FSAppointment order, FSServiceOrder serviceOrder)
        {
            if (order == null)
                throw new PXArgumentException(ErrorMessages.ArgumentNullException);

            BAccount cust = (BAccount)PXSelect<BAccount,
                Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.
                Select(Base, serviceOrder.BillCustomerID);

            Location loc = (Location)PXSelect<Location,
                Where<Location.bAccountID, Equal<Required<Location.bAccountID>>, And<Location.locationID, Equal<Required<Location.locationID>>>>>.
                Select(Base, serviceOrder.BillCustomerID, serviceOrder.BillLocationID);

            IAddressBase addressFrom = GetFromAddress();
            IAddressBase addressTo = GetToAddress(order, serviceOrder);

            if (addressFrom == null)
                throw new PXException(PX.Objects.CR.Messages.FailedGetFromAddressSO);

            if (addressTo == null)
                throw new PXException(PX.Objects.CR.Messages.FailedGetToAddressSO);

            GetTaxRequest request = new GetTaxRequest();
            request.CompanyCode = CompanyCodeFromBranch(order.TaxZoneID, Base.Accessinfo.BranchID);
            request.CurrencyCode = order.CuryID;
            request.CustomerCode = cust.AcctCD;
            request.OriginAddress = AddressConverter.ConvertTaxAddress(addressFrom);
            request.DestinationAddress = AddressConverter.ConvertTaxAddress(addressTo);
            request.DocCode = $"CR.{order.AppointmentID}";
            request.DocDate = order.ExecutionDate.GetValueOrDefault();

            int mult = 1;

            if (!string.IsNullOrEmpty(loc.CAvalaraCustomerUsageType))
            {
                request.CustomerUsageType = loc.CAvalaraCustomerUsageType;
            }
            if (!string.IsNullOrEmpty(loc.CAvalaraExemptionNumber))
            {
                request.ExemptionNo = loc.CAvalaraExemptionNumber;
            }

            request.DocType = TaxDocumentType.SalesOrder;

            PXSelectBase<FSAppointmentDet> select = new PXSelectJoin<FSAppointmentDet,
                LeftJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>,
                    LeftJoin<Account, On<Account.accountID, Equal<InventoryItem.salesAcctID>>>>,
                Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>>,
                OrderBy<Asc<FSAppointmentDet.apptLineNbr>>>(Base);

            foreach (PXResult<FSAppointmentDet, InventoryItem, Account> res in select.View.SelectMultiBound(new object[] { order }))
            {
                FSAppointmentDet tran = (FSAppointmentDet)res;
                InventoryItem item = (InventoryItem)res;
                Account salesAccount = (Account)res;

                var line = new TaxCartItem();
                line.Index = tran.ApptLineNbr ?? 0;
                line.Amount = mult * tran.CuryBillableTranAmt.GetValueOrDefault();
                line.Description = tran.TranDesc;
                line.DestinationAddress = request.DestinationAddress;
                line.OriginAddress = request.OriginAddress;
                line.ItemCode = item.InventoryCD;
                line.Quantity = Math.Abs(tran.BillableQty.GetValueOrDefault());
                line.Discounted = request.Discount > 0;
                line.RevAcct = salesAccount.AccountCD;
                line.TaxCode = tran.TaxCategoryID;

                request.CartItems.Add(line);
            }

            return request;
        }

        protected void ApplyTax(FSAppointment order, GetTaxResult result)
        {
            TaxZone taxZone = null;
            AP.Vendor vendor = null;

            if (result.TaxSummary.Length > 0)
            {
                taxZone = (TaxZone)PXSetup<TaxZone, Where<TaxZone.taxZoneID, Equal<Required<FSAppointment.taxZoneID>>>>.Select(Base, order.TaxZoneID);
                vendor = (AP.Vendor)PXSelect<AP.Vendor, Where<AP.Vendor.bAccountID, Equal<Required<AP.Vendor.bAccountID>>>>.Select(Base, taxZone.TaxVendorID);

                if (vendor == null)
                    throw new PXException(PX.Objects.CR.Messages.ExternalTaxVendorNotFound);
            }

            //Clear all existing Tax transactions:
            foreach (PXResult<FSAppointmentTaxTran, Tax> res in Base.Taxes.View.SelectMultiBound(new object[] { order }))
            {
                FSAppointmentTaxTran taxTran = (FSAppointmentTaxTran)res;
                Base.Taxes.Delete(taxTran);
            }

            Base.Views.Caches.Add(typeof(Tax));

            for (int i = 0; i < result.TaxSummary.Length; i++)
            {
                string taxID = GetTaxID(result.TaxSummary[i]);

                //Insert Tax if not exists - just for the selectors sake
                Tax tx = PXSelect<Tax, Where<Tax.taxID, Equal<Required<Tax.taxID>>>>.Select(Base, taxID);
                if (tx == null)
                {
                    tx = new Tax();
                    tx.TaxID = taxID;
                    tx.Descr = PXMessages.LocalizeFormatNoPrefixNLA(PX.Objects.TX.Messages.ExternalTaxProviderTaxId, taxID);
                    tx.TaxType = CSTaxType.Sales;
                    tx.TaxCalcType = CSTaxCalcType.Doc;
                    tx.TaxCalcLevel = result.TaxSummary[i].TaxCalculationLevel.ToCSTaxCalcLevel();
                    tx.TaxApplyTermsDisc = CSTaxTermsDiscount.ToTaxableAmount;
                    tx.SalesTaxAcctID = vendor.SalesTaxAcctID;
                    tx.SalesTaxSubID = vendor.SalesTaxSubID;
                    tx.ExpenseAccountID = vendor.TaxExpenseAcctID;
                    tx.ExpenseSubID = vendor.TaxExpenseSubID;
                    tx.TaxVendorID = taxZone.TaxVendorID;
                    tx.IsExternal = true;

                    Base.Caches[typeof(Tax)].Insert(tx);
                }

                FSAppointmentTaxTran tax = new FSAppointmentTaxTran();
                tax.EntityType = ID.PostDoc_EntityType.APPOINTMENT;
                tax.EntityID = order.AppointmentID;
                tax.TaxID = taxID;
                tax.CuryTaxAmt = Math.Abs(result.TaxSummary[i].TaxAmount);
                tax.CuryTaxableAmt = Math.Abs(result.TaxSummary[i].TaxableAmount);
                tax.TaxRate = Convert.ToDecimal(result.TaxSummary[i].Rate) * 100;

                Base.Taxes.Insert(tax);
            }

            Base.AppointmentSelected.SetValueExt<FSAppointment.curyTaxTotal>(order, Math.Abs(result.TotalTaxAmount));

            decimal? СuryDocTotal = AppointmentEntry.GetCuryDocTotal(order.CuryBillableLineTotal, order.CuryDiscTot, order.CuryTaxTotal, 0);
            Base.AppointmentSelected.SetValueExt<FSAppointment.curyDocTotal>(order, СuryDocTotal ?? 0m);
        }

        protected IAddressBase GetFromAddress()
        {
            PXSelectBase<Branch> select = new PXSelectJoin
                <Branch, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<Branch.bAccountID>>,
                    InnerJoin<Address, On<Address.addressID, Equal<BAccount.defAddressID>>>>,
                    Where<Branch.branchID, Equal<Required<Branch.branchID>>>>(Base);

            foreach (PXResult<Branch, BAccount, Address> res in select.Select(Base.Accessinfo.BranchID))
                return (Address)res;

            return null;
        }

        protected IAddressBase GetToAddress(FSAppointment order, FSServiceOrder serviceOrder)
        {
            CRAddress crShipAddress = null;

            if (crShipAddress != null)
                return crShipAddress;

            Address shipAddress = null;

            Location loc = (Location)PXSelect<Location,
                Where<Location.bAccountID, Equal<Required<Location.bAccountID>>, And<Location.locationID, Equal<Required<Location.locationID>>>>>.
                Select(Base, serviceOrder.BillCustomerID, serviceOrder.BillLocationID);

            if (loc != null)
            {
                shipAddress = PXSelect<Address, Where<Address.addressID, Equal<Required<Address.addressID>>>>.Select(Base, loc.DefAddressID);
            }

            return shipAddress;
        }
    }
}