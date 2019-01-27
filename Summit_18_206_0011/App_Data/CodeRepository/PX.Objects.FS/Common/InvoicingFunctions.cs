using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PX.Objects.FS
{
    public static class InvoicingFunctions
    {
        public class AddressSource
        {
            public string billing;
            public string shipping;
        }

        public static IInvoiceGraph CreateInvoiceGraph(string module)
        {
            if (module == ID.Batch_PostTo.SO)
            {
                if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
                {
                    return PXGraph.CreateInstance<SOOrderEntry>().GetExtension<SM_SOOrderEntry>();
                }
                else
                {
                    throw new PXException(TX.Error.DISTRIBUTION_MODULE_IS_DISABLED);
                }
            }
            else if (module == ID.Batch_PostTo.AR)
            {
                return PXGraph.CreateInstance<ARInvoiceEntry>().GetExtension<SM_ARInvoiceEntry>();
            }
            else if (module == ID.Batch_PostTo.AP)
            {
                return PXGraph.CreateInstance<APInvoiceEntry>().GetExtension<SM_APInvoiceEntry>();
            }
            else
            {
                throw new PXException(TX.Error.POSTING_MODULE_IS_INVALID, module);
            }
        }

        public static IInvoiceProcessGraph CreateInvoiceProcessGraph(string billingBy)
        {
            if (billingBy == ID.Billing_By.SERVICE_ORDER)
            {
                return PXGraph.CreateInstance<CreateInvoiceByServiceOrderPost>();
            }
            else if (billingBy == ID.Billing_By.APPOINTMENT)
            {
                return PXGraph.CreateInstance<CreateInvoiceByAppointmentPost>();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Sets the SubID for AR or SalesSubID for SO.
        /// </summary>
        public static void SetCombinedSubID(
                                    PXGraph graph,
                                    PXCache sender,
                                    ARTran tranARRow,
                                    APTran tranAPRow,
                                    SOLine tranSORow,
                                    FSSrvOrdType fsSrvOrdTypeRow,
                                    int? branchID,
                                    int? inventoryID,
                                    int? customerLocationID,
                                    int? branchLocationID,
                                    int? salesPersonID,
                                    bool isService)
        {
            if (string.IsNullOrEmpty(fsSrvOrdTypeRow.CombineSubFrom) == true)
            {
                throw new PXException(TX.Error.SALES_SUB_MASK_UNDEFINED_IN_SERVICE_ORDER_TYPE, fsSrvOrdTypeRow.SrvOrdType);
            }

            if (branchID == null || inventoryID == null || customerLocationID == null || branchLocationID == null)
            {
                throw new PXException(TX.Error.SOME_SUBACCOUNT_SEGMENT_SOURCE_IS_NOT_SPECIFIED);
            }

            if ((tranARRow != null && tranARRow.AccountID != null) || (tranAPRow != null && tranAPRow.AccountID != null) || (tranSORow != null && tranSORow.SalesAcctID != null))
            {
                SharedClasses.SubAccountIDList subAcctIDs = SharedFunctions.GetSubAccountIDs(graph, fsSrvOrdTypeRow, inventoryID, branchID, customerLocationID, branchLocationID, salesPersonID, isService);

                object value;
                try
                {
                    if (tranARRow != null)
                    {
                        value = SubAccountMaskAttribute.MakeSub<ARSetup.salesSubMask>(
                            graph,
                            fsSrvOrdTypeRow.CombineSubFrom,
                            new object[] { subAcctIDs.branchLocation_SubID, subAcctIDs.branch_SubID, subAcctIDs.inventoryItem_SubID, subAcctIDs.customerLocation_SubID, subAcctIDs.postingClass_SubID, subAcctIDs.salesPerson_SubID, subAcctIDs.srvOrdType_SubID, subAcctIDs.warehouse_SubID },
                            new Type[] { typeof(FSBranchLocation.subID), typeof(Location.cMPSalesSubID), typeof(InventoryItem.salesSubID), typeof(Location.cSalesSubID), typeof(INPostClass.salesSubID), typeof(SalesPerson.salesSubID), typeof(FSSrvOrdType.subID), isService ? typeof(INSite.salesSubID) : typeof(InventoryItem.salesSubID) });

                        sender.RaiseFieldUpdating<ARTran.subID>(tranARRow, ref value);
                        tranARRow.SubID = (int?)value;
                    }
                    else if (tranAPRow != null)
                    {
                        value = SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>(
                                graph,
                                fsSrvOrdTypeRow.CombineSubFrom,
                                new object[] { subAcctIDs.branchLocation_SubID, subAcctIDs.branch_SubID, subAcctIDs.inventoryItem_SubID, subAcctIDs.customerLocation_SubID, subAcctIDs.postingClass_SubID, subAcctIDs.salesPerson_SubID, subAcctIDs.srvOrdType_SubID, subAcctIDs.warehouse_SubID },
                                new Type[] { typeof(FSBranchLocation.subID), typeof(Location.cMPSalesSubID), typeof(InventoryItem.salesSubID), typeof(Location.cSalesSubID), typeof(INPostClass.salesSubID), typeof(SalesPerson.salesSubID), typeof(FSSrvOrdType.subID), isService ? typeof(INSite.salesSubID) : typeof(InventoryItem.salesSubID) });

                        sender.RaiseFieldUpdating<APTran.subID>(tranSORow, ref value);
                        tranAPRow.SubID = (int?)value;
                    }
                    else if (tranSORow != null)
                    {
                        value = SubAccountMaskAttribute.MakeSub<SOOrderType.salesSubMask>(
                            graph,
                            fsSrvOrdTypeRow.CombineSubFrom,
                                new object[] { subAcctIDs.branchLocation_SubID, subAcctIDs.branch_SubID, subAcctIDs.inventoryItem_SubID, subAcctIDs.customerLocation_SubID, subAcctIDs.postingClass_SubID, subAcctIDs.salesPerson_SubID, subAcctIDs.srvOrdType_SubID, subAcctIDs.warehouse_SubID },
                                new Type[] { typeof(FSBranchLocation.subID), typeof(Location.cMPSalesSubID), typeof(InventoryItem.salesSubID), typeof(Location.cSalesSubID), typeof(INPostClass.salesSubID), typeof(SalesPerson.salesSubID), typeof(FSSrvOrdType.subID), isService ? typeof(INSite.salesSubID) : typeof(InventoryItem.salesSubID) });

                        sender.RaiseFieldUpdating<SOLine.salesSubID>(tranSORow, ref value);
                        tranSORow.SalesSubID = (int?)value;
                    }
                }
                catch (PXException)
                {
                    if (tranARRow != null)
                    {
                        tranARRow.SubID = null;
                    }
                    else if (tranSORow != null)
                    {
                        tranSORow.SalesSubID = null;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the SubID for AR or SalesSubID for SO.
        /// </summary>
        public static void SetCombinedSubID(
                                    PXGraph graph,
                                    PXCache sender,
                                    ARTran tranARRow,
                                    APTran tranAPRow,
                                    SOLine tranSORow,
                                    FSSetup fsSetupRow,
                                    int? branchID,
                                    int? inventoryID,
                                    int? customerLocationID,
                                    int? branchLocationID)
        {
            if (branchID == null || inventoryID == null || customerLocationID == null || branchLocationID == null)
            {
                throw new PXException(TX.Error.SOME_SUBACCOUNT_SEGMENT_SOURCE_IS_NOT_SPECIFIED);
            }

            if ((tranARRow != null && tranARRow.AccountID != null) || (tranAPRow != null && tranAPRow.AccountID != null) || (tranSORow != null && tranSORow.SalesSubID != null))
            {
                InventoryItem inventoryItemRow =
                                                    PXSelect<InventoryItem,
                                                    Where<
                                                        InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>
                                                    .Select(graph, inventoryID);

                Location companyLocationRow =
                                                PXSelectJoin<Location,
                                                InnerJoin<BAccountR,
                                                    On<
                                                        Location.bAccountID, Equal<BAccountR.bAccountID>,
                                                        And<Location.locationID, Equal<BAccountR.defLocationID>>>,
                                                InnerJoin<PX.Objects.GL.Branch,
                                                    On<
                                                        BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>,
                                                Where<
                                                    PX.Objects.GL.Branch.branchID, Equal<Required<ARTran.branchID>>>>
                                                .Select(graph, branchID);

                Location customerLocationRow =
                                                PXSelect<Location,
                                                Where<
                                                    Location.locationID, Equal<Required<Location.locationID>>>>
                                                .Select(graph, customerLocationID);

                FSBranchLocation fsBranchLocationRow =
                                                        PXSelect<FSBranchLocation,
                                                        Where<
                                                            FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>
                                                        .Select(graph, branchLocationID);

                int? customer_SubID = customerLocationRow.CSalesSubID;
                int? item_SubID = inventoryItemRow.SalesSubID;
                int? company_SubID = companyLocationRow.CMPSalesSubID;
                int? branchLocation_SubID = fsBranchLocationRow.SubID;

                object value;
                try
                {
                    if (tranARRow != null)
                    {
                        value = SubAccountMaskAttribute.MakeSub<ARSetup.salesSubMask>(
                            graph,
                            fsSetupRow.ContractCombineSubFrom,
                            new object[] { customer_SubID, item_SubID, company_SubID, branchLocation_SubID },
                            new Type[] { typeof(Location.cSalesSubID), typeof(InventoryItem.salesSubID), typeof(Location.cMPSalesSubID), typeof(FSBranchLocation.subID) },
                            true);

                        sender.RaiseFieldUpdating<ARTran.subID>(tranARRow, ref value);
                        tranARRow.SubID = (int?)value;
                    }
                    else if (tranAPRow != null)
                    {
                        value = SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>(
                                graph,
                                fsSetupRow.ContractCombineSubFrom,
                                new object[] { customer_SubID, item_SubID, company_SubID, branchLocation_SubID },
                                new Type[] { typeof(Location.cSalesSubID), typeof(InventoryItem.salesSubID), typeof(Location.cMPSalesSubID), typeof(FSBranchLocation.subID) },
                                true);

                        sender.RaiseFieldUpdating<APTran.subID>(tranSORow, ref value);
                        tranAPRow.SubID = (int?)value;
                    }
                    else if (tranSORow != null)
                    {
                        value = SubAccountMaskAttribute.MakeSub<SOOrderType.salesSubMask>(
                            graph,
                            fsSetupRow.ContractCombineSubFrom,
                            new object[] { customer_SubID, item_SubID, company_SubID, branchLocation_SubID },
                            new Type[] { typeof(Location.cSalesSubID), typeof(InventoryItem.salesSubID), typeof(Location.cMPSalesSubID), typeof(FSBranchLocation.subID) },
                            true);

                        sender.RaiseFieldUpdating<SOLine.salesSubID>(tranSORow, ref value);
                        tranSORow.SalesSubID = (int?)value;
                    }
                }
                catch (PXException)
                {
                    if (tranARRow != null)
                    {
                        tranARRow.SubID = null;
                    }
                    else if (tranSORow != null)
                    {
                        tranSORow.SalesSubID = null;
                    }
                }
            }
        }

        private static PXResult<Location, Contact, Address> GetContactAndAddressFromLocation(PXGraph graph, int? locationID)
        {
            return
                (PXResult<Location, Contact, Address>)
                PXSelectJoin<Location,
                LeftJoin<Contact,
                    On<
                        Contact.contactID, Equal<Location.defContactID>>,
                LeftJoin<Address,
                    On<
                        Address.addressID, Equal<Location.defAddressID>>>>,
                Where<
                    Location.locationID, Equal<Required<Location.locationID>>>>
                .Select(graph, locationID);
        }

        private static PXResult<Location, Customer, Contact, Address> GetDefaultLocationInformationByAccount(PXGraph graph, int? bAccountID)
        {
            return
                (PXResult<Location, Customer, Contact, Address>)
                PXSelectJoin<Location,
                InnerJoin<Customer,
                    On<
                        Customer.bAccountID, Equal<Location.bAccountID>,
                    And<
                        Customer.defLocationID, Equal<Location.locationID>>>,
                LeftJoin<Contact,
                    On<
                        Contact.contactID, Equal<Location.defContactID>>,
                LeftJoin<Address,
                    On<
                        Address.addressID, Equal<Location.defAddressID>>>>>,
                Where<
                    Location.bAccountID, Equal<Required<Location.bAccountID>>>>
                .Select(graph, bAccountID);
        }

        /// <summary>
        /// Sets the Contact and Address from the Service Order address information according to the posting process.
        /// </summary>
        public static void SetAddress(PXGraph graph, FSServiceOrder fsServiceOrderRow)
        {
            Address addressRow = new Address();
            Contact contactRow = new Contact();

            int? customerID = fsServiceOrderRow.BillCustomerID == null ? fsServiceOrderRow.CustomerID : fsServiceOrderRow.BillCustomerID;
            int? locationID = fsServiceOrderRow.BillLocationID == null ? fsServiceOrderRow.LocationID : fsServiceOrderRow.BillLocationID;
            Customer customerRow = SharedFunctions.GetCustomerRow(graph, customerID);

            AddressSource addressSource = GetCustomerBillingAddressSource(graph, fsServiceOrderRow, customerRow);

            if (addressSource == null
                    || (addressSource != null && string.IsNullOrEmpty(addressSource.billing)))
            {
                throw new PXException(TX.Error.MISSING_CUSTOMER_BILLING_ADDRESS_SOURCE);
            }

            switch (addressSource.billing)
            {
                case ID.Send_Invoices_To.BILLING_CUSTOMER_BILL_TO:

                    contactRow =
                        PXSelect<Contact,
                        Where<
                            Contact.contactID, Equal<Required<Contact.contactID>>>>
                        .Select(graph, customerRow.DefBillContactID);

                    addressRow =
                        PXSelect<Address,
                        Where<
                            Address.addressID, Equal<Required<Address.addressID>>>>
                        .Select(graph, customerRow.DefBillAddressID);

                    break;
                case ID.Send_Invoices_To.SO_BILLING_CUSTOMER_LOCATION:

                    PXResult<Location, Contact, Address> bqlResult = GetContactAndAddressFromLocation(graph, locationID);

                    if (bqlResult != null)
                    {
                        addressRow = (Address)bqlResult;
                        contactRow = (Contact)bqlResult;
                    }

                    break;
                case ID.Send_Invoices_To.SERVICE_ORDER_ADDRESS:

                    contactRow.Salutation = fsServiceOrderRow.Attention;
                    contactRow.EMail = fsServiceOrderRow.EMail;
                    contactRow.Phone1 = fsServiceOrderRow.Phone1;
                    contactRow.Phone2 = fsServiceOrderRow.Phone2;
                    contactRow.Fax = fsServiceOrderRow.Fax;
                    contactRow.RevisionID = 0;

                    addressRow.IsValidated = fsServiceOrderRow.AddressValidated;
                    addressRow.AddressLine1 = fsServiceOrderRow.AddressLine1;
                    addressRow.AddressLine2 = fsServiceOrderRow.AddressLine2;
                    addressRow.AddressLine3 = fsServiceOrderRow.AddressLine3;
                    addressRow.City = fsServiceOrderRow.City;
                    addressRow.CountryID = fsServiceOrderRow.CountryID;
                    addressRow.State = fsServiceOrderRow.State;
                    addressRow.PostalCode = fsServiceOrderRow.PostalCode;
                    addressRow.RevisionID = contactRow.RevisionID;

                    break;
                default:

                    PXResult<Location, Customer, Contact, Address> bqlResultLocationInfo = GetDefaultLocationInformationByAccount(graph, customerID);

                    if (bqlResultLocationInfo != null)
                    {
                        addressRow = (Address)bqlResultLocationInfo;
                        contactRow = (Contact)bqlResultLocationInfo;
                    }

                    break;
            }

            if (addressRow == null)
            {
                throw new PXException(PXMessages.LocalizeFormat(TX.Error.ADDRESS_CONTACT_CANNOT_BE_NULL, TX.TableName.ADDRESS), PXErrorLevel.Error);
            }

            if (contactRow == null)
            {
                throw new PXException(PXMessages.LocalizeFormat(TX.Error.ADDRESS_CONTACT_CANNOT_BE_NULL, TX.TableName.CONTACT), PXErrorLevel.Error);
            }

            if (graph is SOOrderEntry)
            {
                SOBillingContact soBillingContactRow = new SOBillingContact();
                SOBillingAddress soBillingAddressRow = new SOBillingAddress();

                SOOrderEntry graphSOOrderEntry = (SOOrderEntry)graph;

                graphSOOrderEntry.Billing_Contact.SetValueExt<SOBillingContact.overrideContact>(soBillingContactRow, true);
                graphSOOrderEntry.Billing_Address.SetValueExt<SOBillingAddress.overrideAddress>(soBillingAddressRow, true);

                soBillingContactRow.FullName = contactRow.FullName;
                soBillingContactRow.Salutation = contactRow.Salutation;
                soBillingContactRow.Phone1 = contactRow.Phone1;
                soBillingContactRow.Email = contactRow.EMail;
                soBillingContactRow.RevisionID = contactRow.RevisionID != null ? contactRow.RevisionID : 0;
                soBillingContactRow.CustomerID = graphSOOrderEntry.customer.Current.BAccountID;
                soBillingContactRow.IsDefaultContact = false;

                soBillingAddressRow.IsValidated = addressRow.IsValidated;
                soBillingAddressRow.AddressLine1 = addressRow.AddressLine1;
                soBillingAddressRow.AddressLine2 = addressRow.AddressLine2;
                soBillingAddressRow.AddressLine3 = addressRow.AddressLine3;
                soBillingAddressRow.City = addressRow.City;
                soBillingAddressRow.CountryID = addressRow.CountryID;
                soBillingAddressRow.State = addressRow.State;
                soBillingAddressRow.PostalCode = addressRow.PostalCode;
                soBillingAddressRow.RevisionID = addressRow.RevisionID;
                soBillingAddressRow.CustomerAddressID = graphSOOrderEntry.customer.Current.DefAddressID;
                soBillingAddressRow.CustomerID = graphSOOrderEntry.customer.Current.BAccountID;
                soBillingAddressRow.IsDefaultAddress = false;

                soBillingContactRow = graphSOOrderEntry.Billing_Contact.Current = graphSOOrderEntry.Billing_Contact.Update(soBillingContactRow);
                soBillingAddressRow = graphSOOrderEntry.Billing_Address.Current = graphSOOrderEntry.Billing_Address.Update(soBillingAddressRow);

                graphSOOrderEntry.Document.Current.BillAddressID = soBillingAddressRow.AddressID;
                graphSOOrderEntry.Document.Current.BillContactID = soBillingContactRow.ContactID;

                addressRow = new Address();
                contactRow = new Contact();

                SetAddressAndContactForShipping(graph, addressSource.shipping, fsServiceOrderRow, ref addressRow, ref contactRow);

                if (addressRow == null)
                {
                    throw new PXException(PXMessages.LocalizeFormat(TX.Error.ADDRESS_CONTACT_CANNOT_BE_NULL, TX.WildCards.SHIPPING_ADDRESS), PXErrorLevel.Error);
                }

                if (contactRow == null)
                {
                    throw new PXException(PXMessages.LocalizeFormat(TX.Error.ADDRESS_CONTACT_CANNOT_BE_NULL, TX.WildCards.SHIPPING_CONTACT), PXErrorLevel.Error);
                }

                SOShippingContact soShippingContactRow = new SOShippingContact();
                SOShippingAddress soShippingAddressRow = new SOShippingAddress();

                graphSOOrderEntry.Shipping_Contact.SetValueExt<SOShippingContact.overrideContact>(soShippingContactRow, true);
                graphSOOrderEntry.Shipping_Address.SetValueExt<SOShippingAddress.overrideAddress>(soShippingAddressRow, true);

                soShippingContactRow.FullName = contactRow.FullName;
                soShippingContactRow.Salutation = contactRow.Salutation;
                soShippingContactRow.Phone1 = contactRow.Phone1;
                soShippingContactRow.Email = contactRow.EMail;
                soShippingContactRow.RevisionID = contactRow.RevisionID != null ? contactRow.RevisionID : 0;
                soShippingContactRow.CustomerID = graphSOOrderEntry.customer.Current.BAccountID;
                soShippingContactRow.IsDefaultContact = false;

                soShippingAddressRow.IsValidated = addressRow.IsValidated;
                soShippingAddressRow.AddressLine1 = addressRow.AddressLine1;
                soShippingAddressRow.AddressLine2 = addressRow.AddressLine2;
                soShippingAddressRow.AddressLine3 = addressRow.AddressLine3;
                soShippingAddressRow.City = addressRow.City;
                soShippingAddressRow.CountryID = addressRow.CountryID;
                soShippingAddressRow.State = addressRow.State;
                soShippingAddressRow.PostalCode = addressRow.PostalCode;
                soShippingAddressRow.RevisionID = contactRow.RevisionID != null ? contactRow.RevisionID : addressRow.RevisionID;
                soShippingAddressRow.CustomerAddressID = graphSOOrderEntry.customer.Current.DefAddressID;
                soShippingAddressRow.CustomerID = graphSOOrderEntry.customer.Current.BAccountID;
                soShippingAddressRow.IsDefaultAddress = false;

                soShippingContactRow = graphSOOrderEntry.Shipping_Contact.Current = graphSOOrderEntry.Shipping_Contact.Update(soShippingContactRow);
                soShippingAddressRow = graphSOOrderEntry.Shipping_Address.Current = graphSOOrderEntry.Shipping_Address.Update(soShippingAddressRow);

                graphSOOrderEntry.Document.Current.ShipAddressID = soShippingAddressRow.AddressID;
                graphSOOrderEntry.Document.Current.ShipContactID = soShippingContactRow.ContactID;
            }
            else if (graph is ARInvoiceEntry)
            {
                ARContact arContactRow = new ARContact();
                ARAddress arAddressRow = new ARAddress();

                ARInvoiceEntry graphARInvoiceEntry = (ARInvoiceEntry)graph;

                graphARInvoiceEntry.Billing_Contact.SetValueExt<ARContact.overrideContact>(arContactRow, true);
                graphARInvoiceEntry.Billing_Address.SetValueExt<ARAddress.overrideAddress>(arAddressRow, true);

                arContactRow.FullName = contactRow.FullName;
                arContactRow.Salutation = contactRow.Salutation;
                arContactRow.Phone1 = contactRow.Phone1;
                arContactRow.Email = contactRow.EMail;
                arContactRow.RevisionID = contactRow.RevisionID;
                arContactRow.CustomerID = graphARInvoiceEntry.customer.Current.BAccountID;
                arContactRow.IsDefaultContact = false;

                arAddressRow.IsValidated = addressRow.IsValidated;
                arAddressRow.AddressLine1 = addressRow.AddressLine1;
                arAddressRow.AddressLine2 = addressRow.AddressLine2;
                arAddressRow.AddressLine3 = addressRow.AddressLine3;
                arAddressRow.City = addressRow.City;
                arAddressRow.CountryID = addressRow.CountryID;
                arAddressRow.State = addressRow.State;
                arAddressRow.PostalCode = addressRow.PostalCode;
                arAddressRow.RevisionID = addressRow.RevisionID;
                arAddressRow.CustomerAddressID = graphARInvoiceEntry.customer.Current.DefAddressID;
                arAddressRow.CustomerID = graphARInvoiceEntry.customer.Current.BAccountID;
                arAddressRow.IsDefaultBillAddress = false;

                arContactRow = graphARInvoiceEntry.Billing_Contact.Current = graphARInvoiceEntry.Billing_Contact.Update(arContactRow);
                arAddressRow = graphARInvoiceEntry.Billing_Address.Current = graphARInvoiceEntry.Billing_Address.Update(arAddressRow);

                graphARInvoiceEntry.Document.Current.BillAddressID = arAddressRow.AddressID;
                graphARInvoiceEntry.Document.Current.BillContactID = arContactRow.ContactID;
            }
        }

        /// <summary>
        /// Returns the TermID from the Vendor or Customer.
        /// </summary>
        public static string GetTermsIDFromCustomerOrVendor(PXGraph graph, int? customerID, int? vendorID)
        {
            if (customerID != null)
            {
                Customer customerRow = PXSelect<Customer,
                                       Where<
                                            Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Select(graph, customerID);
                return customerRow?.TermsID;
            }
            else if (vendorID != null)
            {
                Vendor vendorRow = PXSelect<Vendor,
                                   Where<
                                        Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Select(graph, vendorID);
                return vendorRow?.TermsID;
            }

            return null;
        }

        public static AddressSource GetCustomerBillingAddressSource(PXGraph graph, FSServiceOrder fsServiceOrderRow, Customer customerRow)
        {
            FSSetup fSSetupRow = PXSelect<FSSetup>.Select(graph);
            AddressSource addressSource = null;

            if (fSSetupRow != null)
            {
                addressSource = new AddressSource();

                if (fSSetupRow.CustomerMultipleBillingOptions == true)
                {
                    FSCustomerBillingSetup customerBillingRow = PXSelect<FSCustomerBillingSetup,
                                                                    Where<FSCustomerBillingSetup.customerID, Equal<Required<FSCustomerBillingSetup.customerID>>,
                                                                        And<FSCustomerBillingSetup.srvOrdType, Equal<Required<FSCustomerBillingSetup.srvOrdType>>,
                                                                        And<FSCustomerBillingSetup.active, Equal<True>>>>>
                                                                .Select(graph, customerRow.BAccountID, fsServiceOrderRow.SrvOrdType);

                    if (customerBillingRow != null)
                    {
                        addressSource.billing = customerBillingRow.SendInvoicesTo;
                        addressSource.shipping = customerBillingRow.BillShipmentSource;
                    }
                }
                else if (fSSetupRow.CustomerMultipleBillingOptions == false && customerRow != null)
                {
                    FSxCustomer fsxCustomerRow = PXCache<Customer>.GetExtension<FSxCustomer>(customerRow);
                    addressSource.billing = fsxCustomerRow.SendInvoicesTo;
                    addressSource.shipping = fsxCustomerRow.BillShipmentSource;
                }
            }

            return addressSource;
        }

        /// <summary>
        /// Cleans the posting information <c>(FSContractPostDoc, FSContractPostDet, FSContractPostBatch, FSContractPostRegister)</c> 
        /// when erasing the entire posted document (SO, AR) coming from a contract.
        /// </summary>
        public static void CleanContractPostingInfoLinkedToDoc(object postedDoc)
        {
            if (postedDoc == null)
            {
                return;
            }

            PXGraph cleanerGraph = new PXGraph();

            string createdDocType = string.Empty;
            string createdRefNbr = string.Empty;
            string postTo = string.Empty;

            PXResultset<FSContractPostDoc> fsContractPostDocRows = new PXResultset<FSContractPostDoc>();
            if (postedDoc is SOOrder)
            {
                createdDocType = ((SOOrder)postedDoc).OrderType;
                createdRefNbr = ((SOOrder)postedDoc).RefNbr;
                postTo = ID.Contract_PostTo.SALES_ORDER_MODULE;
            }
            else if (postedDoc is ARInvoice)
            {
                createdDocType = ((ARRegister)postedDoc).DocType;
                createdRefNbr = ((ARRegister)postedDoc).RefNbr;
                postTo = ID.Contract_PostTo.ACCOUNTS_RECEIVABLE_MODULE;
            }

            PXResultset<FSContractPostDoc> fsContractPostDocRow = PXSelect<
                FSContractPostDoc, 
                Where<FSContractPostDoc.postDocType, Equal<Required<FSContractPostDoc.postDocType>>, 
                    And<FSContractPostDoc.postRefNbr, Equal<Required<FSContractPostDoc.postRefNbr>>, 
                    And<FSContractPostDoc.postedTO, Equal<Required<FSContractPostDoc.postedTO>>>>>>
                .Select(cleanerGraph, createdDocType, createdRefNbr, postTo);

            if (fsContractPostDocRow.Count > 0)
            {
                int? contractPostBatchID = fsContractPostDocRow.FirstOrDefault().GetItem<FSContractPostDoc>().ContractPostBatchID;
                int? contractPostDocID = fsContractPostDocRow.FirstOrDefault().GetItem<FSContractPostDoc>().ContractPostDocID;
                int? serviceContractID = fsContractPostDocRow.FirstOrDefault().GetItem<FSContractPostDoc>().ServiceContractID;
                int? contractPeriodID = fsContractPostDocRow.FirstOrDefault().GetItem<FSContractPostDoc>().ContractPeriodID;

                PXDatabase.Delete<FSContractPostRegister>(
                    new PXDataFieldRestrict<FSContractPostRegister.contractPostBatchID>(contractPostBatchID),
                    new PXDataFieldRestrict<FSContractPostRegister.postedTO>(postTo),
                    new PXDataFieldRestrict<FSContractPostRegister.postDocType>(createdDocType),
                    new PXDataFieldRestrict<FSContractPostRegister.postRefNbr>(createdRefNbr));

                PXDatabase.Delete<FSContractPostDoc>(
                    new PXDataFieldRestrict<FSContractPostDoc.contractPostDocID>(contractPostDocID),
                    new PXDataFieldRestrict<FSContractPostDoc.postedTO>(postTo),
                    new PXDataFieldRestrict<FSContractPostDoc.postDocType>(createdDocType),
                    new PXDataFieldRestrict<FSContractPostDoc.postRefNbr>(createdRefNbr));

                PXDatabase.Delete<FSContractPostDet>(
                    new PXDataFieldRestrict<FSContractPostDet.contractPostDocID>(contractPostDocID));

                PXUpdate<Set<FSContractPeriod.invoiced, False, 
                         Set<FSContractPeriod.status, ListField_Status_ContractPeriod.Pending>>, 
                    FSContractPeriod,
                    Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>,
                        And<FSContractPeriod.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>>>>
                    .Update(cleanerGraph, serviceContractID, contractPeriodID);

                ContractPostBatchMaint contractPostBatchgraph = PXGraph.CreateInstance<ContractPostBatchMaint>();
                contractPostBatchgraph.ContractBatchRecords.Current = contractPostBatchgraph.ContractBatchRecords.Search<FSContractPostDoc.contractPostBatchID>(contractPostBatchID);

                if (contractPostBatchgraph.ContractPostDocRecords.Select().Count() == 0)
                {
                    contractPostBatchgraph.ContractBatchRecords.Delete(contractPostBatchgraph.ContractBatchRecords.Current);
                }

                contractPostBatchgraph.Save.Press();
            }
        }

        /// <summary>
        /// Cleans the posting information <c>(FSCreatedDoc, FSPostDoc, FSPostInfo, FSPostDet, FSPostBatch, FSPostRegister)</c> when erasing the entire posted document (SO, AR, AP).
        /// </summary>
        public static void CleanPostingInfoLinkedToDoc(object postedDoc)
        {
            if (postedDoc == null)
            {
                return;
            }

            PXGraph cleanerGraph = new PXGraph();

            string createdDocType = string.Empty;
            string createdRefNbr = string.Empty;
            string postTo = string.Empty;

            PXResultset<FSPostDet> fsPostDetRows = new PXResultset<FSPostDet>();

            if (postedDoc is SOOrder)
            {
                createdDocType = ((SOOrder)postedDoc).OrderType;
                createdRefNbr = ((SOOrder)postedDoc).RefNbr;
                postTo = ID.Batch_PostTo.SO;
                fsPostDetRows = PXSelect<FSPostDet, Where<FSPostDet.sOOrderNbr, Equal<Required<FSPostDet.sOOrderNbr>>,
                                                        And<FSPostDet.sOOrderType, Equal<Required<FSPostDet.sOOrderType>>>>>.Select(cleanerGraph, createdRefNbr, createdDocType);
            }
            else if (postedDoc is ARInvoice)
            {
                createdDocType = ((ARInvoice)postedDoc).DocType;
                createdRefNbr = ((ARInvoice)postedDoc).RefNbr;
                postTo = ID.Batch_PostTo.AR;
                fsPostDetRows = PXSelect<FSPostDet, Where<FSPostDet.arRefNbr, Equal<Required<FSPostDet.arRefNbr>>,
                                                        And<FSPostDet.arDocType, Equal<Required<FSPostDet.arDocType>>>>>.Select(cleanerGraph, createdRefNbr, createdDocType);
            }
            else if (postedDoc is APInvoice)
            {
                createdDocType = ((APInvoice)postedDoc).DocType;
                createdRefNbr = ((APInvoice)postedDoc).RefNbr;
                postTo = ID.Batch_PostTo.AP;
                fsPostDetRows = PXSelect<FSPostDet, Where<FSPostDet.apRefNbr, Equal<Required<FSPostDet.apRefNbr>>,
                                                        And<FSPostDet.apDocType, Equal<Required<FSPostDet.apDocType>>>>>.Select(cleanerGraph, createdRefNbr, createdDocType);
            }

            if (fsPostDetRows.Count > 0)
            {
                int? batchID = fsPostDetRows.FirstOrDefault().GetItem<FSPostDet>().BatchID;

                PXDatabase.Delete<FSCreatedDoc>(
                    new PXDataFieldRestrict<FSCreatedDoc.batchID>(batchID),
                    new PXDataFieldRestrict<FSCreatedDoc.postTo>(postTo),
                    new PXDataFieldRestrict<FSCreatedDoc.createdDocType>(createdDocType),
                    new PXDataFieldRestrict<FSCreatedDoc.createdRefNbr>(createdRefNbr));

                PXDatabase.Delete<FSPostRegister>(
                    new PXDataFieldRestrict<FSPostRegister.batchID>(batchID),
                    new PXDataFieldRestrict<FSPostRegister.postedTO>(postTo),
                    new PXDataFieldRestrict<FSPostRegister.postDocType>(createdDocType),
                    new PXDataFieldRestrict<FSPostRegister.postRefNbr>(createdRefNbr));

                PXDatabase.Delete<FSPostDoc>(
                    new PXDataFieldRestrict<FSPostDoc.batchID>(batchID),
                    new PXDataFieldRestrict<FSPostDoc.postedTO>(postTo),
                    new PXDataFieldRestrict<FSPostDoc.postDocType>(createdDocType),
                    new PXDataFieldRestrict<FSPostDoc.postRefNbr>(createdRefNbr));

                IInvoiceGraph invoiceGraph = InvoicingFunctions.CreateInvoiceGraph(postTo);

                PostBatchMaint graphPostBatchMaint = PXGraph.CreateInstance<PostBatchMaint>();
                graphPostBatchMaint.BatchRecords.Current = graphPostBatchMaint.BatchRecords.Search<FSPostBatch.batchID>(batchID);

                foreach (FSPostDet fsPostDetRow in fsPostDetRows)
                {
                    invoiceGraph.CleanPostInfo(cleanerGraph, fsPostDetRow);

                    int postedByAPP = PXUpdateJoin<
                        Set<FSAppointment.finPeriodID, Null,
                        Set<FSAppointment.postingStatusAPARSO, ListField_Status_Posting.PendingToPost,
                        Set<FSAppointment.pendingAPARSOPost, True>>>,
                    FSAppointment,
                        InnerJoin<FSAppointmentDet,
                            On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>>,
                    Where<
                        FSAppointmentDet.postID, Equal<Required<FSAppointmentDet.postID>>,
                        And<FSAppointment.pendingAPARSOPost, Equal<False>>>>
                   .Update(cleanerGraph, fsPostDetRow.PostID);

                    if (postedByAPP > 0)
                    {
                        int? sOID = GetServiceOrderFromAppPostID(cleanerGraph, fsPostDetRow.PostID);

                        if (AreAppointmentsPostedInSO(cleanerGraph, sOID) == false)
                        {
                            PXUpdateJoin<
                                Set<FSServiceOrder.finPeriodID, Null,
                                Set<FSServiceOrder.postedBy, Null,
                                Set<FSServiceOrder.pendingAPARSOPost, True>>>,
                            FSServiceOrder,
                                InnerJoin<FSAppointment,
                                    On<FSAppointment.sOID, Equal<FSServiceOrder.sOID>>,
                                InnerJoin<FSAppointmentDet,
                                    On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>>>,
                            Where<
                                FSAppointmentDet.postID, Equal<Required<FSAppointmentDet.postID>>,
                                And<FSAppointment.pendingAPARSOPost, Equal<True>>>>
                           .Update(cleanerGraph, fsPostDetRow.PostID);
                        }
                    }
                    else
                    {
                        PXUpdateJoin<
                            Set<FSServiceOrder.finPeriodID, Null,
                            Set<FSServiceOrder.postedBy, Null,
                            Set<FSServiceOrder.pendingAPARSOPost, True>>>,
                        FSServiceOrder,
                            InnerJoin<FSSODet,
                                On<FSSODet.sOID, Equal<FSServiceOrder.sOID>>>,
                        Where<
                            FSSODet.postID, Equal<Required<FSSODet.postID>>,
                            And<FSServiceOrder.pendingAPARSOPost, Equal<False>>>>
                       .Update(cleanerGraph, fsPostDetRow.PostID);
                    }

                    graphPostBatchMaint.BatchDetails.Delete(fsPostDetRow);
                }

                graphPostBatchMaint.Save.Press();
                int docCount = graphPostBatchMaint.BatchDetailsInfo.Select().Count();

                if (docCount == 0)
                {
                    //Erasing batch if there are no detail lines.
                    graphPostBatchMaint.BatchRecords.Delete(graphPostBatchMaint.BatchRecords.Current);
                }
                else
                {
                    graphPostBatchMaint.BatchRecords.Current.QtyDoc = docCount;
                    graphPostBatchMaint.BatchRecords.Update(graphPostBatchMaint.BatchRecords.Current);
                }

                graphPostBatchMaint.Save.Press();
            }
        }

        public static bool AreAppointmentsPostedInSO(PXGraph graph, int? sOID)
        {
            if (sOID == null)
            {
                return false;
            }

            return PXSelect<FSAppointment,
                            Where<FSAppointment.pendingAPARSOPost, Equal<False>,
                            And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>>
                        .Select(graph, sOID).Count() > 0;
        }

        public static int? GetServiceOrderFromAppPostID(PXGraph graph, int? postID)
        {
            if (postID == null)
            {
                return null;
            }

            FSAppointment fsAppointmentRow = PXSelectJoin<FSAppointment,
                                             InnerJoin<FSAppointmentDet,
                                                  On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>>,
                                             Where<
                                                  FSAppointmentDet.postID, Equal<Required<FSAppointmentDet.postID>>>>
                                            .Select(graph, postID);

            return fsAppointmentRow != null ? fsAppointmentRow.SOID : null;
        }

        public static int? GetServiceOrderFromSOPostID(PXGraph graph, int? postID)
        {
            if (postID == null)
            {
                return null;
            }

            FSServiceOrder fsServiceOrderRow = PXSelectJoin<FSServiceOrder,
                                               InnerJoin<FSSODet,
                                                    On<FSSODet.sOID, Equal<FSServiceOrder.sOID>>>,
                                               Where<
                                                      FSSODet.postID, Equal<Required<FSSODet.postID>>>>
                                              .Select(graph, postID);

            return fsServiceOrderRow != null ? fsServiceOrderRow.SOID : null;
        }

        private static void SetAddressAndContactForShipping(
            PXGraph graph, 
            string infoSource, 
            FSServiceOrder fsServiceOrderRow,
            ref Address addressRow,
            ref Contact contactRow)
        {
            int? locationID = null;
            int? customerID = fsServiceOrderRow.BillCustomerID == null ? fsServiceOrderRow.CustomerID : fsServiceOrderRow.BillCustomerID;

            switch (infoSource)
            {
                case ID.Ship_To.BILLING_CUSTOMER_BILL_TO:

                    PXResult<Location, Customer, Contact, Address> bqlResult =
                        (PXResult<Location, Customer, Contact, Address>)
                        PXSelectJoin<Location,
                        InnerJoin<Customer,
                            On<
                                Customer.defLocationID, Equal<Location.locationID>,
                            And<
                                Customer.bAccountID, Equal<Location.bAccountID>>>,
                        InnerJoin<Contact,
                            On<
                                Contact.contactID, Equal<Location.defContactID>,
                            And<
                                Contact.bAccountID, Equal<Customer.bAccountID>>>,
                        InnerJoin<Address,
                            On<
                                Address.addressID, Equal<Location.defAddressID>,
                            And<
                                Address.bAccountID, Equal<Address.bAccountID>>>>>>,
                            Where<
                            Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>
                        .Select(graph, customerID);

                    contactRow = (Contact)bqlResult;
                    addressRow = (Address)bqlResult;

                    break;

                case ID.Ship_To.SERVICE_ORDER_ADDRESS:

                    contactRow.Salutation   = fsServiceOrderRow.Attention;
                    contactRow.EMail        = fsServiceOrderRow.EMail;
                    contactRow.Phone1       = fsServiceOrderRow.Phone1;
                    contactRow.Phone2       = fsServiceOrderRow.Phone2;
                    contactRow.Fax          = fsServiceOrderRow.Fax;

                    addressRow.IsValidated  = fsServiceOrderRow.AddressValidated;
                    addressRow.AddressLine1 = fsServiceOrderRow.AddressLine1;
                    addressRow.AddressLine2 = fsServiceOrderRow.AddressLine2;
                    addressRow.AddressLine3 = fsServiceOrderRow.AddressLine3;
                    addressRow.City         = fsServiceOrderRow.City;
                    addressRow.CountryID    = fsServiceOrderRow.CountryID;
                    addressRow.State        = fsServiceOrderRow.State;
                    addressRow.PostalCode   = fsServiceOrderRow.PostalCode;
                    addressRow.RevisionID   = contactRow.RevisionID != null ? contactRow.RevisionID : 0;

                    break;

                case ID.Ship_To.SO_CUSTOMER_LOCATION:
                    locationID = fsServiceOrderRow.LocationID;
                    break;

                case ID.Ship_To.SO_BILLING_CUSTOMER_LOCATION:
                    locationID = fsServiceOrderRow.BillLocationID;
                    break;
            }

            if (locationID != null)
            {
                PXResult<Location, Contact, Address> bqlResult = GetContactAndAddressFromLocation(graph, locationID);
                contactRow = (Contact)bqlResult;
                addressRow = (Address)bqlResult;
            }
        }

        public static void ApplyInvoiceActions(PXGraph graph, CreateInvoiceFilter filter, Guid currentProcessID)
        {
            switch (filter.PostTo)
            {
                case ID.Batch_PostTo.SO:

                    if (filter.EmailSalesOrder == true
                        || filter.PrepareInvoice == true
                            || filter.SOQuickProcess == true)
                    {
                        SOOrderEntry soOrderEntryGraph = PXGraph.CreateInstance<SOOrderEntry>();

                        var rows = PXSelectJoin<SOOrder,
                                   InnerJoin<FSPostDoc,
                                        On<FSPostDoc.postRefNbr, Equal<SOOrder.orderNbr>,
                                        And<
                                            Where<FSPostDoc.postOrderType, Equal<SOOrder.orderType>,
                                                Or<FSPostDoc.postOrderTypeNegativeBalance, Equal<SOOrder.orderType>>>>>>,
                                   Where<FSPostDoc.processID, Equal<Required<FSPostDoc.processID>>>>
                                   .Select(graph, currentProcessID);

                        foreach (var row in rows)
                        {
                            SOOrder sOOrder = (SOOrder)row;
                            soOrderEntryGraph.Document.Current = soOrderEntryGraph.Document.Search<SOOrder.orderNbr>(sOOrder.OrderNbr, sOOrder.OrderType);

                            if (sOOrder.Hold == true)
                            {
                                soOrderEntryGraph.Document.Cache.SetValueExt<SOOrder.hold>(sOOrder, false);
                                soOrderEntryGraph.Save.Press();
                            }

                            PXAdapter adapterSO = new PXAdapter(soOrderEntryGraph.CurrentDocument);

                            if (filter.EmailSalesOrder == true)
                            {
                                var args = new Dictionary<string, object>();
                                args["notificationCD"] = "SALES ORDER";

                                adapterSO.Arguments = args;

                                soOrderEntryGraph.notification.PressButton(adapterSO);
                            }

                            if (filter.SOQuickProcess == true
                                    && soOrderEntryGraph.soordertype.Current != null
                                        && soOrderEntryGraph.soordertype.Current.AllowQuickProcess == true)
                            {
                                SO.SOOrderEntry.SOQuickProcess.InitQuickProcessPanel(soOrderEntryGraph, "");
                                PXQuickProcess.Start(soOrderEntryGraph, sOOrder, soOrderEntryGraph.SOQuickProcessExt.QuickProcessParameters.Current);
                            }
                            else
                            {
                                if (filter.PrepareInvoice == true)
                                {
                                    if (soOrderEntryGraph.prepareInvoice.GetEnabled() == true)
                                    {
                                        adapterSO.MassProcess = true;
                                        soOrderEntryGraph.prepareInvoice.PressButton(adapterSO);
                                    }

                                    if (filter.ReleaseInvoice == true)
                                    {
                                        var shipmentsList = soOrderEntryGraph.shipmentlist.Select();

                                        if (shipmentsList.Count > 0)
                                        {
                                            SOOrderShipment soOrderShipmentRow = shipmentsList[0];
                                            SOInvoiceEntry soInvoiceEntryGraph = PXGraph.CreateInstance<SOInvoiceEntry>();
                                            soInvoiceEntryGraph.Document.Current = soInvoiceEntryGraph.Document.Search<ARInvoice.docType, ARInvoice.refNbr>(soOrderShipmentRow.InvoiceType, soOrderShipmentRow.InvoiceNbr, soOrderShipmentRow.InvoiceType);

                                            PXAdapter adapterAR = new PXAdapter(soInvoiceEntryGraph.CurrentDocument);
                                            adapterAR.MassProcess = true;

                                            soInvoiceEntryGraph.release.PressButton(adapterAR);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;

                // @TODO
                case ID.Batch_PostTo.AR_AP:
                    break;
            }
        }

        public static Exception GetErrorInfoInLines(List<ErrorInfo> errorInfoList, Exception e)
        {
            StringBuilder errorMsgBuilder = new StringBuilder();
            errorMsgBuilder.Append(e.Message);

            if (errorInfoList.Count > 0)
            {
                errorMsgBuilder.Append(Environment.NewLine);
                errorMsgBuilder.Append(PXMessages.LocalizeFormat(TX.Messages.INVOICE_POSSIBLE_ERRORS, TX.PostDoc_EntityType.CONTRACT));

                if (errorInfoList[0] != null)
                {
                    errorMsgBuilder.Append(Environment.NewLine + errorInfoList[0].ErrorMessage);
                }
            }

            return new PXException(errorMsgBuilder.ToString());
        }
    }
}
