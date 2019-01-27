using PX.Common;

namespace PX.Objects.FS
{
    public static class TX
    {
        //FSEquipment - LocationType
        [PXLocalizable]
        public static class LocationType
        {
            public const string COMPANY  = "Company";
            public const string CUSTOMER = "Customer";
        }

        //FSEquipment - Condition
        [PXLocalizable]
        public static class Condition
        {
            public const string NEW  = "New";
            public const string USED = "Used";
        }

        //FSLicense - OwnerType
        [PXLocalizable]
        public static class OwnerType
        {
            public const string BUSINESS = "Business";
            public const string EMPLOYEE = "Staff Member";
        }

        //FSLicense - TermType
        [PXLocalizable]
        public static class TermType
        {
            public const string DAYS  = "Days";
            public const string MONTH = "Months";
        }

        //FSxService - BillingRule
        [PXLocalizable]
        public static class BillingRule
        {
            public const string TIME      = "Time";
            public const string FLAT_RATE = "Flat Rate";
            public const string NONE      = "None";
        }

        //FSEmployeeSchedule - ScheduleType
        [PXLocalizable]
        public static class ScheduleType
        {
            public const string AVAILABILITY   = "Availability";
            public const string UNAVAILABILITY = "Unavailability";
            public const string BUSY           = "Busy because of an Appointment";
        }

        [PXLocalizable]
        public class Schedule_EntityType
        {
            public const string CONTRACT = "Contract";
            public const string EMPLOYEE = "Employee";
        }

        [PXLocalizable]
        public class PostDoc_EntityType
        {
            public const string APPOINTMENT     = "Appointment";
            public const string SERVICE_ORDER   = "Service Order";
            public const string CONTRACT        = "Service Contract";
        }

        [PXLocalizable]
        public class ContractType_BillingFrequency
        {
            public const string NONE            = "None";
            public const string TIME_OF_SERVICE = "Time of Service";
            public const string BEG_OF_CONTRACT = "Beg. of Contract";
            public const string END_OF_CONTRACT = "End of Contract";
            public const string DAYS_30_60_90   = "30/60/90 Days";
            public const string MONTHLY         = "Monthly";
            public const string EVERY_4TH_MONTH = "Every 4th Month";
            public const string SEMI_ANNUAL     = "Semi Yearly";
            public const string ANNUAL          = "Yearly";
        }

        [PXLocalizable]
        public static class ContractPeriod_Actions
        {
            public const string SEARCH_BILLING_PERIOD           = "Search by Billing Periods";
            public const string MODIFY_UPCOMING_BILLING_PERIOD  = "Modify Upcoming Billing Period";
        }

        [PXLocalizable]
        public class Schedule_FrequencyType
        {
            public const string DAILY   = "Daily";
            public const string WEEKLY  = "Weekly";
            public const string MONTHLY = "Monthly";
            public const string ANNUAL  = "Yearly";
        }

        [PXLocalizable]
        public class Contract_BillingType
        {
            public const string AS_PERFORMED_BILLINGS = "As Performed Billings";
            public const string STANDARDIZED_BILLINGS = "Standardized Plus Usage/Overage Billings";
        }

        [PXLocalizable]
        public class Contract_BillTo
        {
            public const string CUSTOMERACCT = "Customer Account";
            public const string SPECIFICACCT = "Specific Account";
        }

        [PXLocalizable]
        public class Contract_ExpirationType
        {
            public const string EXPIRING  = "Expiring";
            public const string UNLIMITED = "Unlimited";
        }

        [PXLocalizable]
        public class Contract_BillingPeriod
        {
            public const string ONETIME  = "One-Time";
            public const string WEEK     = "Week";
            public const string MONTH    = "Month";
            public const string QUARTER  = "Quarter";
            public const string HALFYEAR = "Half a Year";
            public const string YEAR     = "Year";
        }

        //Cache Names
        [PXLocalizable]
        public static class TableName
        {
            public const string ZIP_CODE                        = "Zip Code";
            public const string CAUSE                           = "Cause";
            public const string SKILL                           = "Skill";
            public const string RESOLUTION                      = "Resolution";
            public const string EQUIPMENT                       = "Equipment";            
            public const string LICENSE                         = "License";
            public const string LICENSE_TYPE                    = "License Type";
            public const string PROBLEM                         = "Problem";
            public const string SERVICE_CODE                    = "Service";
            public const string SERVICE_CODE_PROBLEM            = "Service - Problem";
            public const string FAMILY                          = "Family";
            public const string FAMILY_SERVICE                  = "Family Service";
            public const string SERVICE_LICENSE_TYPE            = "Service - License Type";
            public const string SERVICE_STATUS                  = "Service Status";
            public const string REASON                          = "Reason";
            public const string GEOGRAPHIC_ZONE                 = "Service Area";
            public const string GEOGRAPHIC_ZONE_EMPLOYEE        = "Service Area - Employee";
            public const string GEOGRAPHIC_POSTAL_CODE          = "Service Area - Postal Code";
            public const string EMPLOYEE_SCHEDULE               = "Employee Schedule";
            public const string APPOINTMENT                     = "Appointment";
            public const string SERVICE_ORDER                   = "Service Order";
            public const string BILL_OF_MATERIALS               = "Bill of Materials";
            public const string BILL_OF_MATERIALS_DETAIL        = "Bill of Materials Detail";
            public const string SODET                           = "Service Order Detail";
            public const string ORDER_STAGE                     = "Order Stage";
            public const string ORDER_TYPE                      = "Order Type";
            public const string EQUIPMENT_TYPE                  = "Equipment Type";
            public const string BRANCH_LOCATION                 = "Branch Location";
            public const string REPORT_CLASS                    = "Report Class";
            public const string ROOM                            = "Room";
            public const string REFERRAL_SRC_TYPE               = "Referral Source Type";
            public const string CUSTOMER_PRODUCT                = "Customer Product";
            public const string ROUTE_DOCUMENT                  = "Route Document";
            public const string SERVICE_CONTRACTS               = "Service Contracts";
            public const string ROUTES_SETUP                    = "Routes Setup";
            public const string ROUTE                           = "Route";
            public const string ROUTE_SHIFT                     = "Route Shift";
            public const string VEHICLE_TYPE                    = "Vehicle Types";
            public const string VEHICLE_TYPE_LICENSE            = "Vehicle Type Licenses";
            public const string SHIFT                           = "Shift";
            public const string MANUFACTURER                    = "Manufacturers";
            public const string MANUFACTURER_MODEL              = "Manufacturer Models";
            public const string STOCK_ITEM_WARRANTY             = "Stock Item Warranties";
            public const string MODEL_WARRANTY                  = "Model Warranties";
            public const string CONTRACT_GENERATION_HISTORY     = "Contract Generation History";
            public const string SETUP                           = TX.ModuleName.SERVICE_DISPATCH + " Preferences";
            public const string WEEKCODE_DATE                   = "Contracts/Routes calendar Week Code";
            public const string GENERATION_LOG_ERROR            = "Generation Log Error";
            public const string MODEL_TEMPLATE_COMPONENT        = "Model Template Component";
            public const string BILLING_CYCLE                   = "Billing Cycle";
            public const string FSX_SOLINE                      = "Sales Order detail Extension";
            public const string FSX_ARTRAN                      = "Accounts Receivable detail Extension";
            public const string FSX_APTRAN                      = "Accounts Payable detail Extension";
            public const string ADDRESS                         = "Address";
            public const string CONTACT                         = "Contact";
        }

        //Error messages
        [PXLocalizable]
        public static class Error
        {
            public const string ID_ALREADY_USED                                                                = "This ID is already in use.";
            public const string FIELD_MAY_NOT_BE_EMPTY                                                         = "\"{0}\" may not be empty.";
            public const string NULL_OBJECT_REFERENCE                                                          = "Null Object reference. Table name: {0}.";
            public const string STAFF_MEMBERS_COUNT_EXCEEDS_LICENSE_LIMIT                                      = "The staff members count exceeds the license limit: existing staff members = {0}, license limit = {1}.";
            public const string VEHICLES_COUNT_EXCEEDS_LICENSE_LIMIT                                           = "The vehicles count exceeds the license limit: existing vehicles = {0}, license limit = {1}.";

            //@TODO SD-7025
            public const string FIELD_EMPTY                                                                    = "This field cannot be blank.";
            public const string ZERO_OR_NEGATIVE_QTY                                                           = "Quantity must be greater than 0.";
            public const string LINE_HAS_INVALID_DATA                                                          = "Line has invalid data.";
            public const string FIELD_MUST_BE_EMPTY_FOR_LINE_TYPE                                              = "Field must be empty for the selected Line Type.";
            public const string DATA_REQUIRED_FOR_LINE_TYPE                                                    = "Data is required for the selected Line Type.";
            public const string ID_ALREADY_USED_SAME_LEVEL                                                     = "This ID is already in use for the current level.";
            public const string ID_ALREADY_USED_PARENT                                                         = "This ID is already in use for the parent level.";
            public const string NOT_EMPLOYEE_SELECTED                                                          = "Please select an Employee.";
            public const string CURRENT_DOCUMENT_NOT_SERVICES_TO_SCHEDULE                                      = "The current document does not have services to schedule.";
            public const string NEGATIVE_QTY                                                                   = "This value cannot be negative.";
            public const string POSITIVE_QTY                                                                   = "The quantity must be greater than 0.";
            public const string ISSUE_EXPIRATION_DATE_INCONSISTENCY                                            = "The issue date must be prior to the expiration date.";
            public const string NULL_OR_ZERO_HOURS                                                             = "Hours should be greater than 0 (zero).";
            public const string END_DATE_LESSER_THAN_START_DATE                                                = "Invalid dates. End date cannot be prior to the Start date.";
            public const string END_TIME_LESSER_THAN_START_TIME                                                = "Invalid times. End time cannot be prior to the Start time.";
            public const string START_TIME_GREATER_THAN_END_TIME                                               = "The start time cannot be later than the end time. Correct the values.";
            public const string START_DATE_LESSER_THAN_PRESENT                                                 = "Invalid dates. Start date and time cannot be prior to present date and time.";
            public const string SLAETA_GREATER_EQUAL_PROMISEDDATE                                              = "SLA Date must be greater or equal than promised date.";
            public const string CUSTOMER_CHANGE_NOT_ALLOWED_APP_STATUS                                         = "The Customer cannot be changed because the Service Order already has ongoing Appointments.";
            public const string CUSTOMER_CHANGE_NOT_ALLOWED_SO_STATUS                                          = "The Customer cannot be changed because the Service Order status is already different from Open or Hold.";
            public const string SINGLE_SERVICE_RESTRICTION                                                     = "The Single-Service option has been set for the current Service Order Type. Multiple service lines are not allowed for this Service Order.";
            public const string SRVORD_SINGLE_APPOINTMENT                                                      = "The Single-Appointment option has been set for the current Service Order Type. Multiple Appointments are not allowed for this Service Order.";
            public const string ACTUAL_DATES_APPOINTMENT_MISSING                                               = "The Appointment cannot be completed. Please fill out the actual Date and Time fields.";
            public const string SERVICE_LICENSE_TYPES_REQUIREMENTS_MISSING                                     = "The Employees in this Appointment do not have the Licenses that the Service requires.";
            public const string SERVICE_SKILL_REQUIREMENTS_MISSING                                             = "The Employee does not have the Skills required to complete this Appointment Service(s).";
            public const string SERVICE_SKILL_REQUIREMENTS_MISSING_GENERAL                                     = "The Employees in this Appointment do not have the Skills that the Service requires.";
            public const string APPOINTMENT_EMPLOYEE_MISMATCH_GEOZONE                                          = "Employee not assigned to work on this service area. The postal code for this Appointment is not included in the service area where this employee could work.";
            public const string EMPLOYEES_MISSING_TO_VALIDATE_SERVICES                                         = "There are no Employees in order to validate the Services in this Appointment. Please add one or more Employees in the Employees tab.";
            public const string EMPLOYEES_MISSING_TO_VALIDATE_GEOZONE                                          = "There are no Employees in order to validate the Service Order Service Area. Please add one or more Employees in the Employees tab.";
            public const string APPOINTMENT_START_VALIDATE_SERVICE                                             = "An Appointment without Services cannot be started. At least one Service must be added in the Details tab.";
            public const string APPOINTMENT_COMPLETE_VALIDATE_SERVICE                                          = "An Appointment without Services cannot be completed. At least one Service must be added in the Details tab.";
            public const string SELECT_VALID_ACTION                                                            = "A valid action has to be selected.";
            public const string SIGNED_OFF_SERVICE                                                             = "This Service Order has been already signed off.";
            public const string CHECKOUT_NEEDED_BEFORE_SIGNOFF                                                 = "The Service Order must be checked out before signing it off.";
            public const string CHECKED_OUT_SERVICE                                                            = "This Service Order has been already checked out.";
            public const string SIGNED_OFF_NEEDED_FOR_SERVICE                                                  = "The Service Order must be signed off in order to create its invoice.";
            public const string CHECKOUT_AND_SIGNED_OFF_NEEDED_FOR_SERVICE                                     = "The Service Order must be checked out and signed off in order to create its invoice.";
            public const string SERVICE_ORDER_ALREADY_POSTED                                                   = "This Service Order has been already posted to Sales Order.";
            public const string SERVICE_ORDER_POSTING_INCONSISTENCY                                            = "Part of the detail lines in this Service Order were already posted, but the corresponding records in the control table are missing or corrupted. Please contact M5 technical support.";
            public const string SERVICE_ORDER_SOORDER_INCONSISTENCY                                            = "This Service Order has an invalid Sales Order reference. Please contact M5 technical support.";
            public const string SERVICE_ORDER_BILLCUSTOMER_MISSING                                             = "The Billing Customer has not been defined for the Service Order Number {0}. Please go to the Service Orders screen and assign it.";
            public const string SERVICE_ORDER_NOT_FOUND_IN_SERVICEORDERGRAPH                                   = "Error trying to update the ServiceOrder: the ServiceOrderGraph was not loaded.";
            public const string RESOURCES_MISSING_TO_VALIDATE_SERVICES                                         = "There are some services in the detail tab that require one or more missing resources. Check the detail tab to see the specific requirements for each service.";
            public const string QUOTE_SELECTION_NOT_ALLOWED                                                    = "The Quote functionality cannot be activated as there are already appointments scheduled for this Service Order.";
            public const string ATTENDEE_RECORD_REPEATED                                                       = "This attendee already exists.";
            public const string MINIMUN_VALUE                                                                  = "The minimum value allowed for this field is {0}";
            public const string MINIMUN_VALUE_NAME_FIELD                                                       = "The minimum value allowed for {0} is {1}";
            public const string EQUIPMENT_TYPE_MISSING                                                         = "Missing {0} resource(s) of the following resource type: {1}.";
            public const string APPOINTMENT_NOT_EDITABLE                                                       = "This Appointment cannot be modified because either it or its Service Order has been Closed or Canceled.<br><br> For more details please review it in the Appointment screen. <br> Hint: Click on the Appointment number in the Appointment box.";
            public const string INVALID_ACTION_FOR_CURRENT_SERVICE_ORDER_STATUS                                = "This action is invalid for the current Service Order status.";
            public const string SERVICE_ORDER_NOT_FOUND                                                        = "The service order was not found.";
            public const string SERVICE_ORDER_TYPE_NOT_FOUND                                                   = "Service Order Type '{0}' not found.";
            public const string SETUP_NOT_DEFINED                                                              = "There is no setup record defined for Service Management.";
            public const string SERVICE_ORDER_CANNOT_BE_DELETED_BECAUSE_OF_ITS_STATUS                          = "This Service Order cannot be deleted because of its current status.";
            public const string CUSTOMER_CANNOT_BE_CHANGED_BECAUSE_THERE_ARE_SERVICE_LINES_IN_THIS_APPOINTMENT = "The CustomerID cannot be changed because there are service lines in this Appointment.";
            public const string CUSTOMER_CANNOT_BE_CHANGED_BECAUSE_THERE_ARE_SERVICE_LINES_IN_THE_SERVICEORDER = "The CustomerID cannot be changed because there are service lines in the Service Order.";
            public const string EQUIPMENT_ID_ALREADY_USED                                                      = "This ID is already in use by an Equipment.";
            public const string CONTRACT_INCORRECT_EXECUTION_LIMIT                                             = "The Execution Limit has to be greater than 0.";
            public const string CONTRACT_INCORRECT_DAILY_FREQUENCY                                             = "For the Daily Schedule Type, the frequency has to be greater than 0.";
            public const string CONTRACT_INCORRECT_WEEKLY_FREQUENCY                                            = "For the Weekly Schedule Type, the frequency has to be greater than 0.";
            public const string CONTRACT_UNDEFINED_EXPIRATION_DATE                                             = "The Expiration Date has to be set.";
            public const string CONTRACT_UNDEFINED_WEEK_DAY                                                    = "At least 1 day of the week has to be selected.";
            public const string CONTRACT_SCHEDULE_TYPE_CANT_BE_CHANGED                                         = "The Schedule Type for this Contract cannot be changed because an upcoming execution is already scheduled.";
            public const string CONTRACT_SRVORDTYPE_UNDEFINED                                                  = "The Service Order Type for the Contracts has to be defined in the 'Service Management Preferences' screen.";
            public const string EMPLOYEE_NOT_AVAILABLE_WITH_APPOINTMENTS                                       = "This Employee has at least one appointment for the given Date and Time.";
            public const string ROOM_NOT_AVAILABLE_WITH_APPOINTMENTS                                           = "This Room has at least one appointment for the given Date and Time.";
            public const string ROOM_REQUIRED_FOR_THIS_SRVORDTYPE                                              = "A Room must be specified for the selected Service Order Type.";
            public const string APPOINTMENT_REQUIRES_SERVICE                                                   = "This appointment requires at least one service due to the Service Order Type selected.";
            public const string APPOINTMENT_REQUIRES_EMPLOYEE                                                  = "This appointment requires at least one employee due to the Service Order Type selected.";
            public const string APPOINTMENT_DOES_NOT_ALLOW_MULTI_EMPLOYEES                                     = "The Service Order Type selected for this appointment does not allow to assign multiple employees.";
            public const string WARRANTY_DURATION_ZERO_OR_NULL                                                 = "The warranty duration must be greater than 0";
            public const string WARRANTY_DURATION_TYPE_NULL                                                    = "A duration type for this Warranty has to be selected";
            public const string WARRANTY_DFLT_VENDOR_NULL                                                      = "A vendor has to be selected";
            public const string WRKPROCESS_NOT_FOUND                                                           = "The FSWrkProcess record cannot be found.";
            public const string MAPS_MISSING_REQUIRED_PARAMETERS                                               = "At least one origin and one destination have to be included.";
            public const string MAPS_STATUS_CODE_OK                                                            = "The request is successful.";
            public const string MAPS_STATUS_CODE_CREATED                                                       = "A new resource is created.";
            public const string MAPS_STATUS_CODE_ACCEPTED                                                      = "The request has been accepted for processing.";
            public const string MAPS_STATUS_CODE_BAD_REQUEST                                                   = "The request contained an error.";
            public const string MAPS_STATUS_CODE_UNAUTHORIZED                                                  = "Access was denied. You may have entered your credentials incorrectly, or you might not have access to the requested resource or operation.";
            public const string MAPS_STATUS_CODE_FORBIDDEN                                                     = "The request is for something forbidden. Authorization will not help.";
            public const string MAPS_STATUS_CODE_NOT_FOUND                                                     = "The requested resource was not found.";
            public const string MAPS_STATUS_CODE_TOO_MANY_REQUESTS                                             = "The user has sent too many requests in a given amount of time. The account is being rate limited.";
            public const string MAPS_STATUS_CODE_INTERNAL_SERVER_ERROR                                         = "Your request could not be completed because there was a problem with the service.";
            public const string MAPS_STATUS_CODE_SERVICE_UNAVAILABLE                                           = "There's a problem with the service right now. Please try again later.";
            public const string MAPS_FAILED_REVERSE_GEOCODE                                                    = "Failed to find the address.";
            public const string MAPS_CONNECTION_FAILED                                                         = "Failed to connect with Google API. Please check your connection.";
            public const string CUSTOM_APPOINTMENT_FIELD_NOT_FOUND                                             = "Custom Appointment Field not found.";
            public const string CUSTOM_APPOINTMENT_STATUS_NOT_FOUND                                            = "Custom Appointment Status not found.";
            public const string VALIDATE_ADDRESS_MISSING_FIELDS                                                = "The fields 'City', 'Country', 'State' and 'Postal Code' must have a value in order to validate the address.";
            public const string VALIDATE_APPOINTMENT_ADDRESS_BEFORE_SAVING                                     = "You must validate the 'Appointment Address' information to be able to save the changes you have made.";
            public const string SERVICE_ORDER_IS_NOT_OPEN                                                      = "This appointment can not be cloned because its service order is not Open";
            public const string SELECT_AT_LEAST_ONE_MONTH                                                      = "You must select at least one month";
            public const string VEHICLE_NOT_MATCHING_APPOINTMENT_BRANCHLOCATION                                = "This vehicle can not be assigned because its branch location does not match the branch location of at least one appointment of the actual route. Please, try with another vehicle";
            public const string VEHICLE_NOT_FOUND_IN_CURRENT_BRANCHLOCATION                                    = "This vehicle was not found in the current branch location.";
            public const string ROLLBACK_ROUTE_CONTRACT_GENERATION_FAILED                                      = "Cannot roll back last generation process: At least one Appointment of the last generation is not in Status: " + TX.Status_Appointment.AUTOMATIC_SCHEDULED;
            public const string ROLLBACK_SERVICE_CONTRACT_GENERATION_FAILED                                    = "Cannot roll back last generation process: At least one Service Order has an Appointment created";
            public const string EMAIL_CANNOT_BE_NULL_IF_SENDAPPNOTIFICATION_IS_TRUE                            = "The field 'EMail' must have a value if the 'Send Appointment Notification' option is enabled";
            public const string CUSTOMER_CONTACT_ADDRESS_OPTION_NOT_AVAILABLE                                  = "The Appointment address can't be taken from the Customer Contact as the 'Contact is Required' option is disabled.";
            public const string INVALID_ACTUAL_APPOINTMENT_DURATION                                            = "The appointment's actual duration cannot be zero. Start Time must be different than End Time.";
            public const string INVALID_SCHEDULED_APPOINTMENT_DURATION                                         = "The appointment's scheduled duration cannot be zero. Start Time must be different than End Time.";
            public const string ADDRESS_VALIDATION_FAILED                                                      = "The address validation failed. Please verify the entered address.";
            public const string LINE_SERVICE_WAS_APPROVED_IN_ANOTHER_TIMECARD                                  = "This service line was already approved in the Time Card document {0}. Please, remove it from this document.";
            public const string INVALID_SO_STATUS_TRANSITION                                                   = "Invalid Service Order status transition.";
            public const string INVALID_APPOINTMENT_STATUS_TRANSITION                                          = "Invalid Appointment status transition.";
            public const string PROJECT_IS_NOT_ACTIVE                                                          = "The actual project is not active. Please, remove it from this document.";
            public const string EXECUTION_DATE_MUST_BE_GREATHER_OR_EQUAL_THAN_SCHEDULED_DATE                   = "The Actual date must be greaher or equal to the Scheduled date";
            public const string CANNOT_CLOSED_APPOINTMENT_BECAUSE_TIME_REGISTERED_OR_ACTUAL_TIMES              = "One or more Staff Times have not been approved";
            public const string CANNOT_UPDATE_APPOINTMENT_BECAUSE_STATUS_IS_CLOSED_OR_CANCELLED                = "The Appointment can't be updated because it's in a CLOSED or CANCELLED status.";
            public const string CANNOT_UPDATE_DATE_BECAUSE_ITS_SET_IN_AN_APPOINTMENT                           = "Can't change the Time Card date because it's already set in the linked appointment";
            public const string SALES_SUB_MASK_UNDEFINED_IN_SERVICE_ORDER_TYPE                                 = "The Sales Sub Mask is not defined in the Service Order Type {0}.";
            public const string SOME_SUBACCOUNT_SEGMENT_SOURCE_IS_NOT_SPECIFIED                                = "Some subaccount segment source is not specified.";
            public const string INVALID_APPOINTMENT_DURATION                                                   = "The appointment's actual duration range cannot be zero. Start Time must be different from End Time";
            public const string LINE_SERVICE_WAS_APPROVED_FROM_ANOTHER_TIMECARD                                = "This service line was approved from another Time Card document. Please, remove it from this document";
            public const string CANNOT_DELETE_SALES_ORDER_BECAUSE_IT_HAS_A_SERVICE_ORDER                       = "The current Sales Order cannot be deleted because there is a Service Order linked to it. Please delete the associated Service Order first.";
            public const string CANNOT_CLOSE_APPOINTMENT_BECAUSE_TIME_IS_NOT_APPROVED                          = "This appointment cannot be closed. The service time in one or more Service lines has not been approved. Approve the Service line(s) using the Employee Time Card page (EP305000) or uncheck the (Require Time Approval to create invoice) checkbox inside the Time & Expense Integration section located in the Service Management Preferences screen (FS100100)";
            public const string SRV_CLASS_REQUIRED_TO_CONVERT_NON_STOCKITEM_TO_SERVICE                         = "A Service Class is required to convert this Non-stock Item to a Service";
            public const string SERVICE_ORDER_HAS_APPOINTMENTS                                                 = "This Service Order is linked to one or more Appointments. Please delete the appointment(s) before deleting this Service Order";
            public const string FSSODET_LINKED_TO_APPOINTMENTS                                                 = "This {0} is linked to one or more Appointments. Please delete the {0}s in the appointment(s) before deleting this {0}";
            public const string SERVICE_LINKED_TO_PICKUP_DELIVERY_ITEMS                                        = "This Service is linked to one or more Pickup/Delivery items. Please delete the Pickup/Delivery items before deleting this Service";
            public const string LICENSE_NEED_NUMBERING_ID                                                      = "The numbering sequence for the License has not been set. Please define it in the 'Service Management Preferences' screen";
            public const string SCHEDULED_DATE_UNAVAILABLE                                                     = "No Scheduled Time available.";
            public const string PROJECT_MUST_BELONG_TO_CUSTOMER                                                = "The currently selected project must belong to this customer.";
            public const string ROUTE_EMPLOYEE_PRIORITY_PREFERERENCE_GREATER_THAN_ZERO                         = "The Priority Preference must be greater than zero.";
            public const string START_DATE_LESSER_THAN_DEFAULT_DATE                                            = "Invalid dates. Start date cannot be prior to the Default date.";
            public const string INVALID_WEEKCODE_GENERATION_OPTIONS                                            = "Inserting the generation options raised one or more errors. Please Review.";
            public const string ROUTE_MAX_APPOINTMENT_QTY_GREATER_THAN_ZERO                                    = "Entry must be greater than zero.";
            public const string WEEKCODE_MUST_NOT_BE_EMPTY                                                     = "Week Codes may not be empty or blank.";
            public const string WEEKCODE_LENGTH_MUST_LESS_OR_EQUAL_THAN_4                                      = "The length of each Week Code must be less than or equal to 4.";
            public const string WEEKCODE_CHAR_NOT_ALLOWED                                                      = "The character {0} is not allowed to build valid Week Codes.";
            public const string WEEKCODE_BAD_FORMED                                                            = "At least one Week Code is not correctly structured. Check field label for examples.";
            public const string ROUTE_SHORT_NOT_DUPLICATED                                                     = "The Route Short can not be duplicated.";
            public const string INVALID_ROUTE_EXECUTION_DAYS_FOR_APPOINTMENT                                   = "An error has occurred when creating an appointment. The scheduled day of the week for the appointment does not correspond to the execution days defined for the RouteID: {0}. Please review the recurrence of this schedule or modify the execution days of the RouteID: {0}.";
            public const string ROUTE_MAX_APPOINTMENT_QTY_EXCEEDED                                             = "Appointment not created, it exceeds the maximum quantity of appointments created for the route.";
            public const string WEEKCODE_NOT_MATCH_WITH_SCHEDULE                                               = "Cannot create the appointment on {1}. There is not a Week Code in the schedule {0} that corresponds to this specific date.";
            public const string WEEKCODE_NOT_MATCH_WITH_ROUTE_SCHEDULE                                         = "Cannot create the appointment on {1}. There is not a Week Code in the default route for the schedule {0} that corresponds to this specific date.";
            public const string WEEKCODE_NOT_MATCH_WITH_ROUTE                                                  = "Cannot create the appointment on {1}. There is not a Week Code in the route for the appointment in the schedule {0} that corresponds to this specific date.";
            public const string ROUTE_DOCUMENT_DATE_NOT_MATCH_WITH_WEEKCODE                                    = "This date doesn't belong to the WeekCode: '{0}' set for the Route: '{1}'";
            public const string ROUTE_DOCUMENT_DATE_NOT_MATCH_WITH_EXECUTION_DAYS                              = "This date weekday: '{0}' doesn't belong to the execution days set for the Route: '{1}'";
            public const string DRIVER_DELETION_ERROR                                                          = "This driver can't be deleted from this screen. To delete this driver you must go the Route Document Details (FS304000) select the Route {0} and click the clear selection button next to the Driver field";
            public const string SCREENID_INCORRECT_FORMAT                                                      = "The ScreenID provided doesn't follow the standard format";
            public const string VENDORID_CANTBE_NULL_IF_HASVENDORWARRANTY_EQUALS_TRUE                          = "'Vendor ID' can not be null if the 'Has Vendor Warranty' checkbox is on";
            public const string VENDORWARRANTYDURATION_CANTBE_NULL_IF_HASVENDORWARRANTY_EQUALS_TRUE            = "'Vendor Warranty Duration' cannot be null if the 'Has Vendor Warranty' checkbox is on";
            public const string CPNYWARRANTYDURATION_CANTBE_NULL_IF_HASVENDORWARRANTY_EQUALS_TRUE              = "'Company Warranty Duration' cannot be null or zero if the 'Has Vendor Warranty' checkbox is on";
            public const string CPNYWARRANTYDURATION_CANTBE_NULL_IF_HASCPNYWARRANTY_EQUALS_TRUE                = "'Company Warranty Duration' cannot be null or zero if the 'Has Company Warranty' checkbox is on";
            public const string MANUFACTURERID_CANTBE_NULL_IF_EQENABLED_EQUALS_TRUE                            = "'Manufacturer ID' can not be empty if the Item Class for this Stock Item is enabled in the " + TX.ModuleName.EQUIPMENT_MODULE + " Module";
            public const string CUSTOMER_SIGNATURE_MISSING                                                     = "The Appointment cannot be completed. The Customer Signature is required to complete this Appointment.";
            public const string CUSTOMER_FULLNAME_MISSING                                                      = "The Appointment cannot be completed. The Full Name is required to complete this Appointment.";
            public const string CUSTOMER_AGREEMENT_MISSING                                                     = "The Appointment cannot be completed. You must accept the agreement to complete this Appointment.";
            public const string CUSTOMER_SIGNATURE_INFO_INCOMPLETE                                             = "The Appointment cannot be completed. You need to provide the required information in the Signature tab.";
            public const string QUOTE_HAS_SERVICE_ORDERS                                                       = "This Quote is linked to one or more Service Orders. Please delete the Service Order(s) linked in the Related Service Orders tab before deleting this Quote";
            public const string REQUIRED_CONTACT_MISSING                                                       = "A contact is required. Please, choose another Service Order Type for this Schedule or uncheck the option 'Contact is Required' from the current Schedule's Service Order Type";
            public const string SO_TYPE_ROUTE_NOT_EXIST                                                        = "A new Appointment cannot be created because a Service Order Type of Routes behavior does not exist. Please go to the Service Order Type page to create it.";
            public const string INVALID_ROUTE_STATUS_TRANSITION                                                = "Invalid Route status transition.";
            public const string ROUTE_NEED_APPOINTMENTS_TO_CHANGE_STATUS                                       = "A Route Document without Appointments cannot be {0}. At least one Appointment must be assigned to this Route.";
            public const string ROUTE_DOCUMENT_APPOINTMENTS_NOT_POSTED                                         = "Cannot close the route: There are appointments that have not been posted to Inventory.";
            public const string ACTUAL_DATES_APPOINTMENT_MISSING_TO_POST                                       = "The Appointment cannot be posted to Inventory. Please fill out the actual Date and Time fields.";
            public const string POST_ORDER_TYPE_MISSING_IN_SETUP                                               = "There is not a valid Post Order Type defined in the " + TX.TableName.SETUP + ". Please define it in order to be able to post documents into Sales Order.";
            public const string POST_ORDER_NEGATIVE_BALANCE_TYPE_MISSING_IN_SETUP                              = "There is not a valid Post Order Type for Negative Balance defined in the " + TX.TableName.SETUP + ". Please define it in order to be able to post documents into Sales Order.";
            public const string NOTHING_TO_BE_POSTED                                                           = "There are no lines to be posted in the Appointment selection.";
            public const string BILLING_CYCLE_TYPE_NOT_VALID                                                   = "Billing Cycle type not valid.";
            public const string BACCOUNT_TYPE_DOES_NOT_MATCH_WITH_STAFF_MEMBERS_OPTIONS                        = "BAccount entity does not match Employee or Vendor options.";
            public const string STAFF_MEMBER_INCONSISTENCY                                                     = "The staff member selected has an invalid reference. Please contact M5 technical support.";
            public const string INVALID_METHOD_ARGUMENTS                                                       = "Technical error. Invalid arguments supplied.";
            public const string APPOINTMENT_SHARED                                                             = "This Appointment has been already shared with this Employee. An Employee is not allowed to have the same Appointment more than once.";
            public const string APPOINTMENT_NOT_FOUND                                                          = "The selected appointment cannot be found. Please refresh and try again.";
            public const string TECHNICAL_ERROR                                                                = "Technical Error: Please try again the action requested.";
            public const string SERVICE_ORDER_SELECTED_IS_NULL                                                 = "Technical Error: ServiceOrderRelated.Current == null";
            public const string RECORD_NOT_FOUND                                                               = "{0} record not found.";
            public const string APPOINTMENT_ITEM_CANNOT_BE_POSTED_TO_IN_NO_ITEMS_RELATED                       = "The record cannot be processed.  The service type of the service is configured as Not Items Related";
            public const string ROUTE_CANT_BE_COMPLETED_APPOINTMENTS_IN_ROUTE_HAVE_ISSUES                      = "Route can not be completed. Some appointments have issues. Please, see details below.";
            public const string CANT_DEFINE_BILLING_CYCLE_BILLED_BY_SERVICE_ORDER_AND_GROUPED_BY_APPOINTMENT   = "Cannot save the record. A Billing Cycle billed by Service Orders cannot be grouped by Appointments. Please select another Invoice grouping option.";
            public const string ROUTE_CANT_BE_CLOSED_APPOINTMENTS_IN_ROUTE_HAVE_ISSUES                         = "Route can not be closed. Some appointments have issues. Please, see details below.";
            public const string SERVICE_ORDER_CANT_BE_CLOSED_APPOINTMENTS_HAVE_ISSUES                          = "Service Order can not be closed. Some Appointments have issues. Please, see details below.";
            public const string SERVICE_ORDER_CANT_BE_COMPLETED_APPOINTMENTS_HAVE_ISSUES                       = "Service Order can not be completed. Some Appointments have issues. Please, see details below.";
            public const string SERVICE_ORDER_CANT_BE_CANCELED_APPOINTMENTS_HAVE_ISSUES                        = "Service Order can not be canceled. Some Appointments have issues. Please, see details below.";
            public const string SERVICE_ORDER_CANT_BE_CANCELED_APPOINTMENTS_HAVE_INVALID_STATUS                = "Service Order can not be canceled. Some Appointments have invalid statuses. To be able to cancel a Service Order, its Appointments must have one of the following status: {0}, {1} or {2}.";
            public const string SRV_ORD_TYPE_ERROR_DELETING_SO_USING_IT                                        = "This Service Order Type can't be deleted. Some Service Orders are using it";
            public const string SRV_ORD_TYPE_ERROR_DELETING_CONTRACT_USING_IT                                  = "This Service Order Type can't be deleted. Some Service Contracts are using it";
            public const string SELECT_AT_LEAST_ONE_DAY_OF_WEEK                                                = "You must select at least one day of the week";
            public const string BILLING_CYCLE_ERROR_DELETING_CUSTOMER_USING_IT                                 = "This Billing Cycle can't be deleted. Some Customers are using it";
            public const string BILLING_CYCLE_ERROR_DELETING_VENDOR_USING_IT                                   = "This Billing Cycle can't be deleted. Some Vendors are using it";
            public const string ADDITIONAL_DRIVER_EQUAL_MAIN_DRIVER                                            = "The Driver and Additional Driver cannot be equal";
            public const string VEHICLES_CANNOT_BE_EQUAL                                                       = "The vehicles cannot be repeated";
            public const string INVENTORY_ITEM_UOM_INCONSISTENCY                                               = "There is an inconsistency in the UOM defined in Sales Prices screen and in the Service screen for the service {0}.";
            public const string MAX_NBR_TRIPS_PER_DAY                                                          = "The Route reached the maximum number of trips on {0}";
            public const string ID_OF_TRIPS_ALREADY_EXIST                                                      = "The Trip Nbr. already exist. Please change it";
            public const string CANNOT_DELETE_ROUTE_APP_SO_STATUS                                              = "This record cannot be deleted, there are Appointments or Service Orders in status Completed or Closed related to the route.";
            public const string INVALID_WARRANTY_DURATION                                                      = "Warranty duration can't be less than zero";
            public const string EQUIPMENT_SOURCE_REFERENCE_DELETED                                             = "The source reference of this record has been deleted.";
            public const string EQUIPMENT_NUMBERING_SEQUENCE_MISSING_SETUP                                     = "You must specify the Equipment Numbering Sequence.";
            public const string SOLINE_HAS_RELATED_APPOINTMENT_DETAILS                                         = "This line can't be modified because is linked to one or more appointments.";
            public const string ERROR_DELETING_RELATED_SERVICE_ORDER                                           = "The related Service Order {0} can't be deleted.";
            public const string SOME_SOLINES_HAVE_RELATED_APPOINTMENT_DETAILS                                  = "Some document details of this Sales Order are related with one or more appointment(s).";
            public const string RECURRENCE_DAYS_ROUTE_DAYS_MISMATCH                                            = "The specified recurrence dates does not match the Route's definition.";
            public const string FSSODET_LINE_IS_RELATED_TO_A_SOLINE                                            = "This line can't be deleted because is linked to the line Number: {0} of the Sales Order that created this Service Order. Please remove the line from the source Sales Order.";
            public const string CANNOT_CHANGE_SEND_INVOICE_TYPE                                                = "The Service Order Address option for Send Invoice To is available only for grouping by Service Order or Appointment in the Billing Cycle.";
            public const string ADDRESS_CONTACT_CANNOT_BE_NULL                                                 = "{0} must be specified to process this item.";
            public const string TIMECARDS_ACTUAL_TIMES_NOT_SPECIFIED                                           = "Please define the Actual Times of this line in the appointment.";
            public const string SELECT_AT_LEAST_ONE_OPTION                                                     = "You must select at least one option";
            public const string START_TIME_GREATER_HEADER_ACTUAL_END_TIME                                      = "The {0}'s actual start time is greater than the appointment's actual end time. Please, correct that value.";
            public const string ACTUAL_DATE_TIMES_ARE_REQUIRED                                                 = "At least one {0}'s actual start/end time is not provided. Please, provide the required value(s), otherwise uncheck the option 'Require Actual Start/End Time of Service Lines to Complete Appointment' in the Service Order Type.";
            public const string NO_AVAILABLE_BRANCH_LOCATION_IN_CURRENT_BRANCH                                 = "The Service Order can not be created. There are no available branch locations in the current branch. Please, select another branch or create a branch location for the current branch.";
            public const string DEFAULT_SERVICE_ORDER_TYPE_NOT_PROVIDED                                        = "The default Service Order Type is not provided. Please, provide this value in the Service Management Preferences screen";
            public const string POSTING_MODULE_IS_INVALID                                                      = "The posting module '{0}' is invalid.";
            public const string UPDATING_FSSODET_PO_REFERENCES                                                 = "There was an error updating the related Service Order items of this Purchase Order: ";
            public const string MISSING_VENDOR_OR_LOCATION                                                     = "Vendor and vendor location should be defined.";
            public const string MISSING_LINK_ENTITY_STAFF_MEMBER                                               = "Cannot update Time Activities. One or more assigned Staff Members need to be linked to an user.";
            public const string INVALID_POSTING_TARGET                                                         = "Invalid posting target.";
            public const string CANNOT_UPDATE_DOCUMENT_BECAUSE_BATCH_STATUS_IS_TEMPORARY                       = "The Document can't be updated because Batch is in a TEMPORARY status.";
            public const string DISTRIBUTION_MODULE_IS_DISABLED                                                = "The Distribution Module is disabled.";
            public const string ROUTE_CANT_BE_COMPLETED_APPOINTMENTS_NEED_TO_BE_COMPLETED                      = "The route can not be completed because some appointments need to be completed, canceled or closed.";
            public const string MISSING_CUSTOMER_BILLING_ADDRESS_SOURCE                                        = "Current Customer's Billing address source is missing. Please provide a valid source in the Customer's billing options";
            public const string DOCUMENT_MODULE_DIFERENT_T0_BATCH_MODULE                                       = "The module specified in the document record ({0}) is different to the indicated in the batch record ({1}).";
            public const string INVENTORY_NOT_ALLOWED_AS_COMPONENT                                             = "The Inventory ID selected does not mathc with the Class ID for this component. Please select another one.";
            public const string EQUIPMENT_ACTION_MODEL_EQUIPMENT_REQUIRED                                      = "Inventory ID must be a model equipment for this action.";
            public const string EQUIPMENT_ACTION_COMPONENT_REQUIRED                                            = "Inventory ID must be a component for this action.";
            public const string EQUIPMENT_ACTION_TARGET_EQUIP_OR_NEW_TARGET_EQUIP_REQUIRED                     = "You must specified a Targer Equipment ID or Model Equipment Line Nbr for this action.";
            public const string MISSING_CUSTOMER_BILLING_CYCLE                                                 = "Customer has no billing cycle set for the current Service Order Type. Please, assign it before saving";
            public const string CANNOT_DELETE_DOCUMENT_IT_HAS_A_SERVICE_ORDER                                  = "The current document cannot be deleted because there is a Service Order linked to it. Please delete the associated Service Order first.";
            public const string EQUIPMENT_COMPONENT_ROW_QTY_EXCEEDED                                           = "This component exceeds the original quantity specified in the model component.";
            public const string EQUIPMENT_COMPONENTS_QTY_ERROR                                                 = "There are errors related to the quantity of the components specified.";
            public const string ROUTE_SERVICE_CANNOT_BE_HANDLED_WITH_NONROUTE_SRVORDTYPE                       = "Route service cannot be handled with current non-route Service Order Type.";
            public const string NONROUTE_SERVICE_CANNOT_BE_HANDLED_WITH_ROUTE_SRVORDTYPE                       = "Non-route service cannot be handled with current route Service Order Type.";
            public const string AP_POSTING_VENDOR_NOT_FOUND                                                    = "This customer is not defined as a vendor. To extend the customer account to a customer and vendor account, select the Extend to Vendor action for this account on the form toolbar of the Customers (AR303000) form.";
            public const string SERVICE_ORDER_TYPE_DOES_NOT_ALLOW_AUTONUMBERING                                = "The appointment cannot be saved because a manual numbering sequence is assigned to the service order type {0} and the service order cannot be created automatically for the appointment. Create the service order on the Service Orders (FS300100) form first or modify the numbering sequence of the service order type on the Service Order Types (FS202300) form.";
            public const string INVALID_SERVICE_CONTRACT_STATUS_TRANSITION                                     = "The transition of the service contract status is invalid.";
            public const string EXPIRATION_DATE_LOWER_UPCOMING_STATUS                                          = "The expiration date must be later than the upcoming status date.";
            public const string EXPIRATION_DATE_LOWER_BUSINESS_DATE                                            = "The expiration date must be later than the business date.";
            public const string EFFECTIVE_DATE_LOWER_ACTUAL_DATE                                               = "The effective date must be later than or the same as the actual date.";
            public const string EFFECTIVE_DATE_GREATER_END_DATE                                                = "The effective date must be earlier than the expiration date.";
            public const string SCHEDULE_DATE_LESSER_THAN_CONTRACT_DATE                                        = "The dates are invalid. The schedule start date must be the same as or later than the start date of the related contract.";
            public const string SCHEDULE_START_DATE_GREATER_THAN_CONTRACT_END_DATE                             = "The dates are invalid. The schedule start date must be earlier than the expiration date of the related contract.";
            public const string SCHEDULE_END_DATE_GREATER_THAN_CONTRACT_END_DATE                               = "The dates are invalid. The schedule expiration date must be earlier than or the same as the expiration date of the related contract. ";
            public const string CONTRACT_START_DATE_GREATER_THAN_SCHEDULE_START_DATE                           = "The dates are invalid. The contract start date cannot be later than the start date of any of the contract schedules.";
            public const string CONTRACT_END_DATE_LESSER_THAN_SCHEDULE_END_DATE                                = "The dates are invalid. The contract end date cannot be earlier than the end date of any of the contract schedules.";
            public const string NO_DELETION_ALLOWED_DOCLINE_LINKED_TO_APP_SO                                   = "The line cannot be deleted because it is related to an appointment or service order.";
            public const string NO_UPDATE_ALLOWED_DOCLINE_LINKED_TO_APP_SO                                     = "This value cannot be updated because it is related to an appointment or service order.";
            public const string DUPLICATING_POSTING_DOCUMENT                                                   = "Attempt to create duplicated Invoice.";
            public const string NO_UPDATE_BILLING_CYCLE_SERVICE_CONTRACT_RELATED                               = "The billing cycle cannot be modified because it has been assigned to at least one customer whose service orders are related to a prepaid contract.";
            public const string PROCESSOR_ALREADY_RUNNING_A_PROCESS                                            = "This Processor is already running a process.";
            public const string STEP_GROUP_ALREADY_ASSIGNED_TO_PROCESSOR                                       = "The Step Group is already assigned to a Processor.";
            public const string PROCESSOR_DOES_NOT_HAVE_A_STEP_GROUP_ASSIGNED                                  = "This Processor doesn't have a Step Group assigned.";
            public const string STEP_GROUP_DOES_NOT_HAVE_A_PROCESSOR_ASSIGNED                                  = "This Step Group doesn't have a Processor assigned.";
            public const string STEP_GROUP_STILLS_RUNNING                                                      = "This Step Group is still running.";
            public const string INVALID_STEP_STATUS_RUNNING_STEPMETHOD                                         = "Invalid StepStatus trying to run StepMethod for the Step '{0}'.";
            public const string INVALID_STEP_STATUS_RUNNING_ONSTEPMETHODCOMPLETED                              = "Invalid StepStatus running OnStepMethodCompleted for the Step '{0}'.";
            public const string INVALID_STEP_STATUS_RUNNING_SETERROR                                           = "Invalid StepStatus running SetError for the Step '{0}'.";
            public const string PERIOD_WITHOUT_DETAILS                                                         = "Period without Details.";
            public const string STOCKITEM_NOT_HANDLED_BY_SRVORDTYPE                                            = "The service order cannot be created because the {0} stock item cannot be added to orders of the selected service order type. Select a service order type that generates invoices in the Sales Orders module.";
            public const string CANNOT_CLONE_APPOINMENT_SERVICE_ORDER_COMPLETED                                = "The appointment cannot be cloned because the associated service order has been completed.";
            public const string QTY_POSTED_ERROR                                                               = "The quantity in the posted document does not match the quantity in the source document.";
            public const string QTY_APPOINTMENT_SERIAL_ERROR                                                   = "Quantity cannot be greater than one for stock items with tracked serial numbers.";
            public const string QTY_APPOINTMENT_GREATER_THAN_SERVICEORDER                                      = "The total quantity for the line has exceeded the quantity specified in the service order.";
            public const string REPEATED_APPOINTMENT_SERIAL_ERROR                                              = "A serial number cannot be repeated for a stock item in different appointments.";
            public const string CANNOT_ADD_INVENTORY_TYPE_LINES_BECAUSE_SO_POSTED                              = "A new {0} cannot be added to this appointment because an invoice has already been generated for the related service order.";
            public const string SCHEDULE_TYPE_NONE                                                             = "The document cannot be saved because the associated service contract has the None schedule type.";
            public const string ACTUAL_DURATION_CANNOT_BE_GREATER_24HRS                                        = "The actual duration cannot be greater than 24 hours.";
        }

        //Warning messages
       [PXLocalizable]
        public static class Warning
        {
            public const string END_TIME_PRIOR_TO_START_TIME_SHIFT                          = "End time is prior to the Start time: This Shift will end in the following day.";
            public const string END_TIME_PRIOR_TO_START_TIME_APPOINTMENT                    = "End time is prior to the Start time: This Appointment will end in the following day.";
            public const string NO_EXECUTION_DAYS_SELECTED_FOR_ROUTE                        = "No execution days selected for this route. Any generation of appointments for this route will fail."; 
            public const string DEFAULT_EMAIL_NOT_CONFIGURED                                = "The notification Email of the Service Order Type could not be sent because the Default Email is not configured. Please define it in the Email Preferences (SM204001) page.";
            public const string SALES_ORDER_NOT_INVOICE                                     = "Recommended option should post to Invoice. Otherwise it will require shipment.";
            public const string NO_VEHICLES_MATCHING_VEHICLETYPES_IN_SERVICES_IN_ROUTE      = "There are no Vehicles that match the Vehicle Types required by all the Services in this Route";
            public const string NO_DRIVER_TO_ASSIGN_TO_ROUTE                                = "There are no Drivers to assign to this Route.";
            public const string END_TIME_AUTOMATICALLY_CALCULATED_NOTIFICATION              = "The End Time has been updated using the {0} Duration Total of the Appointment because the Services grid has been modified.";
            public const string CPNY_WARRANTY_DURATION_LESSTHAN_VENDOR_WARANTY_DURATION     = "'Company Warranty Duration' should not be less than 'Vendor Warranty Duration'";
            public const string CANT_CREATE_A_SERVICE_ORDER_FROM_AN_INVOICED_SALES_ORDER    = "This Sales Order was originated from a Service Order. A new Service Order can't be created.";
            public const string INCOMPLETE_APPOINTMENT_ADDRESS                              = "This appointment's address is incomplete. It could produce Statistics and Routes inconsistencies.";
            public const string ROUTE_MISSING_DRIVER_OR_AND_VEHICLE                         = "This Route is missing a {0}. Do you want to proceed anyway?";
            public const string INVALID_SERVICE_DURATION                                    = "Service duration cannot be less than one minute.";
            public const string REQUIRES_SERIAL_NUMBER                                      = "Requires Serial Nbr.";
            public const string CANNOT_MODIFY_FIELD                                         = "This field cannot be modified because there are {0} related to this {1}.";
            public const string ITEM_WITH_NO_WARRANTIES_CONFIGURED                          = "This Item does not have any components/warranties configured";
            public const string SCHEDULE_WILL_NOT_AFFECT_SYSTEM_UNTIL_GENERATION_OCCURS     = "This schedule will not affect the system until a generation process takes place.";
            public const string SCHEDULE_BEGIN_RESET                                        = "The beginning of the schedule has been reset. All new generations will now start on the custom start date. Are you sure you want to continue?";
            public const string CUSTOMER_DOES_NOT_MATCH_PROJECT                             = "Customer on the line doesn't match the Customer on the Project or Contract.";
            public const string ACTUAL_DATE_AFTER_SLA                                       = "Actual Date is after SLA Date.";
            public const string SHELL_FUNCTION_DEPRECATED                                   = "ShellFunction is deprecated, please use OnTransactionInserted instead.";
            public const string CUSTOMER_CLASS_BILLING_SETTINGS                             = "Please confirm if you want to update current customer BILLING settings with the customer class defaults. Otherwise, original BILLING settings will be preserved.";
            public const string CUSTOMER_MULTIPLE_BILLING_OPTIONS_CHANGING                  = "By changing this setting, it will update billing information on all existing documents. This may take a while.";
            public const string WARRANTY_CANNOT_BE_CALCULATED_BECAUSE_EMPTY_INSTALLATIONDATE = "The warranty cannot be calculated because the installation date is empty.";
            public const string WARRANTY_CANNOT_BE_CALCULATED_BECAUSE_EMPTY_SALEDATE        = "The warranty cannot be calculated because the sale date is empty.";
            public const string WARRANTY_CANNOT_BE_CALCULATED_BECAUSE_EMPTY_INSTALLATIONDATE_AND_SALEDATE = "The warranty cannot be calculated because both the installation date and sale date are empty.";
            public const string DOCUMENTS_WITHOUT_BILLING_INFO                              = "Some Documents are not visible on this screen, they have not billing settings associated.";
            public const string USE_FIX_SERVICE_ORDERS_BUTTON                               = "Use Fix Service Orders Without Billing Settings button.";
            public const string SCHEDULES_WITHOUT_NEXT_EXECUTION                            = "Some schedules are not displayed on the form because the next execution dates are not defined for them. Click the Fix Schedules Without Next Execution button to define the dates and view the schedules on the form.";
            public const string RETRYING_CREATE_INVOICE_AFTER_ERROR                         = "Retrying CreateInvoice with the BatNbr = '{0}' and GroupKey = '{1}' after getting error: {2}.";
            public const string WAITING_FOR_PARTS                                           = "The receipt of at least one item is needed.";
            public const string APPOINTMENT_WAS_NOT_FINISHED                                = "The appointment was not finished.";
            public const string SIGNED_APP_EMAIL_ACTION_IS_DISABLED                         = "The Send Email with Signed Appointment action cannot be performed. To perform the action, sign the appointment by using the mobile app.";
        }

        [PXLocalizable]
        public static class ModuleName
        {
            public const string SERVICE_DISPATCH    = "Service Management";
            public const string SERVICE_DESCRIPTOR  = "(SERVICE)";
            public const string EQUIPMENT_MODULE    = "Equipment Management";
            public const string ROUTES_MODULE       = "Routes Management";
        }

        [PXLocalizable]
        public static class ScreenName
        {
            public const string SERVICE_PREFERENCES = "Service Management Preferences";
        }

        [PXLocalizable]
        public static class ButtonDisplays
        {
            public const string DeleteProc    = "Delete";
            public const string DeleteAllProc = "Delete All";
        }

        [PXLocalizable]
        public static class SourceType_ServiceOrder
        {
            public const string CASE                = "CR - Case";
            public const string OPPORTUNITY         = "CR - Opportunity";
            public const string SALES_ORDER         = "SO - Sales Order";
            public const string SERVICE_DISPATCH    = "SD - Service Dispatch";
            public const string QUOTE               = "SD - Quote";
        }

        #region Priority+Severity
        [PXLocalizable]
        public static class Priority_ALL
        {
            public const string LOW = "Low";
            public const string MEDIUM = "Medium";
            public const string HIGH = "High";
        }
        #endregion

        #region LineType
        [PXLocalizable]
        public static class LineType_ALL
        {
            public const string INVENTORY_ITEM      = "Inventory Item";
            public const string SERVICE             = "Service";
            public const string NONSTOCKITEM        = "Non-Stock Item";
            public const string COMMENT_PART        = "Comment";
            public const string INSTRUCTION_PART    = "Instruction";
            public const string COMMENT_SERVICE     = "Comment";
            public const string INSTRUCTION_SERVICE = "Instruction";
            public const string SERVICE_TEMPLATE    = "Service Template";
            public const string PICKUP_DELIVERY     = "Pickup/Delivery";
            public const string LABOR_ITEM          = "Labor";
        }
        #endregion

        #region PriceType
        [PXLocalizable]
        public static class PriceType
        {
            public const string CONTRACT    = "Contract";
            public const string CUSTOMER    = "Customer";
            public const string PRICE_CLASS = "Customer Price Class";
            public const string BASE        = "Base";
            public const string DEFAULT     = "Default";
        }
        #endregion

        #region Status
        [PXLocalizable]
        public class Status_Parts
        {
            public const string OPEN      = "Open";
            public const string CANCELED  = "Cancelled";
            public const string COMPLETED = "Completed";
        }

        [PXLocalizable]
        public class Status_AppointmentDet : Status_Parts
        {
            public const string IN_PROCESS = "In Process";
        }

        [PXLocalizable]
        public static class Status_Appointment
        {
            public const string AUTOMATIC_SCHEDULED = "Scheduled by System";
            public const string MANUAL_SCHEDULED    = "Not Started";
            public const string IN_PROCESS          = "In Process";
            public const string CANCELED            = "Canceled";
            public const string COMPLETED           = "Completed";
            public const string CLOSED              = "Closed";
            public const string ON_HOLD             = "On Hold";

            // This status contains both AUTOMATIC_SCHEDULED and MANUAL_SCHEDULED statuses for External Control
            public const string SCHEDULED           = "Scheduled";
        }

        [PXLocalizable]
        public static class Status_ServiceOrder
        {
            public const string OPEN      = "Open";
            public const string QUOTE     = "Quote";
            public const string ON_HOLD   = "On Hold";
            public const string CANCELED  = "Canceled";
            public const string CLOSED    = "Closed";
            public const string COMPLETED = "Completed";
        }

        [PXLocalizable]
        public static class Status_ServiceContract
        {
            public const string DRAFT = "Draft";
            public const string ACTIVE   = "Active";
            public const string SUSPENDED = "Suspended";
            public const string CANCELED = "Canceled";
            public const string EXPIRED = "Expired";
        }

        [PXLocalizable]
        public static class Status_ContractPeriod
        {
            public const string ACTIVE   = "Active";
            public const string PENDING  = "Pending for Invoice";
            public const string INACTIVE = "Inactive";
            public const string INVOICED = "Invoiced";
        }

        [PXLocalizable]
        public static class Status_Route
        {
            public const string OPEN       = "Open";
            public const string IN_PROCESS = "In Process";
            public const string CANCELED   = "Canceled";
            public const string COMPLETED  = "Completed";
            public const string CLOSED     = "Closed";
        }

        [PXLocalizable]
        public static class Status_Posting
        {
            public const string NOTHING_TO_POST = "Nothing to Post";
            public const string PENDING_TO_POST = "Pending to Post";
            public const string POSTED          = "Posted";
        }
        #endregion

        [PXLocalizable]
        public static class FuelType_Equipment
        {
            public const string REGULAR_UNLEADED = "Regular Unleaded";
            public const string PREMIUM_UNLEADED = "Premium Unleaded";
            public const string DIESEL           = "Diesel";
            public const string OTHER            = "Other";
        }

        #region Confirmed_Appointment
        [PXLocalizable]
        public class Confirmed_Appointment
        {
            public const string ALL = "<ALL>";
            public const string CONFIRMED = "Confirmed";
            public const string NOT_CONFIRMED = "Not Confirmed";
        }

        [PXLocalizable]
        public class ValidationType
        {
            public const string PREVENT = "Prevent";
            public const string WARN = "Warn";
            public const string NOT_VALIDATE = "Do Not Validate";
        }

        [PXLocalizable]
        public class SourcePrice
        {
            public const string CONTRACT   = "Contract";
            public const string PRICE_LIST = "Regular Price";
        }

        [PXLocalizable]
        public class RecordType_ContractAction
        {
            public const string CONTRACT = "Contract";
            public const string SCHEDULE = "Schedule";
        }

        [PXLocalizable]
        public class Action_ContractAction
        {
            public const string CREATE              = "Create (New)";
            public const string ACTIVATE            = "Activate";
            public const string SUSPEND             = "Suspend";
            public const string CANCEL              = "Cancel";
            public const string EXPIRE              = "Expire";
            public const string INACTIVATE_SCHEDULE = "Inactivate - Schedule";
            public const string DELETE_SCHEDULE     = "Delete - Schedule";
        }

        [PXLocalizable]
        public static class PeriodType
        {
            public const string DAY = "Day";
            public const string WEEK = "Week";
            public const string MONTH = "Month";
        }

        #endregion

        #region ServiceOrderType
        [PXLocalizable]
        public static class SrvOrdType_RecordType
        {
            //public const string TRAVEL              = "Travel Time";
            //public const string TRAINING            = "Training Time";
            //public const string DOWNTIME            = "Down Time";
            public const string SERVICE_ORDER       = "Service Order";
            public const string RECURRING_TEMPLATE  = "Recurring Template";
        }

        [PXLocalizable]
        public static class SrvOrdType_SalesAcctSource
        {
            public const string INVENTORY_ITEM      = "Inventory Item";
            public const string WAREHOUSE           = "Warehouse";
            public const string POSTING_CLASS       = "Posting Class";
            public const string CUSTOMER_LOCATION   = "Customer/Vendor Location";
        }

        [PXLocalizable]
        public static class Contract_SalesAcctSource
        {
            public const string CUSTOMER_LOCATION   = "Customer/Vendor Location";
            public const string INVENTORY_ITEM      = "Inventory Item";
        }

        [PXLocalizable]
        public static class SrvOrdType_GenerateInvoiceBy
        {
            public const string CRM_AR = "CRM/AR";
            public const string SALES_ORDER = "Sales Order";
            public const string PROJECT = "Project";
            public const string NOT_BILLABLE = "Not Billable";
        }

        [PXLocalizable]
        public static class SrvOrdType_PostTo
        {
            public const string NONE = "None";
            public const string ACCOUNTS_RECEIVABLE_MODULE = "Accounts Receivable";
            public const string SALES_ORDER_MODULE = "Sales Order";
        }

        [PXLocalizable]
        public static class Contract_PostTo
        {
            public const string ACCOUNTS_RECEIVABLE_MODULE = "Accounts Receivable";
            public const string SALES_ORDER_MODULE = "Sales Order";
        }

        [PXLocalizable]
        public static class SrvOrdType_StartAppointmentActionBehavior
        {
            public const string NOTHING                 = "Sets Nothing";
            public const string HEADER_ONLY             = "Sets Actual Start Time in Header";
            public const string HEADER_SERVICE_LINES    = "Sets Actual Start Time in Header and Service Lines";
        }

        [PXLocalizable]
        public static class SrvOrdType_CompleteAppointmentActionBehavior
        {
            public const string NOTHING                 = "Sets Nothing";
            public const string HEADER_ONLY             = "Sets Actual End Time in Header";
            public const string HEADER_SERVICE_LINES    = "Sets Actual End Time in Header and Service Lines";
        }

        //FSSrvOrdType - NewBusinessAcctType
        [PXLocalizable]
        public static class BusinessAcctType
        {
            public const string CUSTOMER = "Customer";
            public const string PROSPECT = "Prospect";
        }

        [PXLocalizable]
        public static class Source_Info
        {
            public const string BUSINESS_ACCOUNT = "Business Account";
            public const string CUSTOMER_CONTACT = "Customer Contact";
            public const string BRANCH_LOCATION = "Branch Location";
        }
        #endregion

        #region Attendees
        [PXLocalizable]
        public class Attendees
        {
            public const string UPDATE_ATTENDEES = "Update Attendees";
        }
        #endregion

        //ReasonType - FSReason
        [PXLocalizable]
        public class ReasonType
        {
            public const string CANCEL_SERVICE_ORDER = "Cancel Service Order";
            public const string CANCEL_APPOINTMENT   = "Cancel Appointment";
            public const string WORKFLOW_STAGE       = "Workflow Stage";
            public const string APPOINTMENT_DETAIL   = "Appointment Detail";
            public const string GENERAL              = "General";
        }

        [PXLocalizable]
        public static class Setup_CopyTimeSpentFrom
        {            
            public const string ACTUAL_DURATION     = "Actual Service Duration";
            public const string ESTIMATED_DURATION  = "Estimated Service Duration";
            public const string NONE                = "None";
        }

        [PXLocalizable]
        public static class CreateInvoice_ActionType
        {
            public const string AR    = "Create invoice(s) in AR";
            public const string SO    = "Create invoice(s) in Sales Order";
            public const string AR_SO = "Create invoice(s) in AR & Sales Order";
        }

        [PXLocalizable]
        public static class Dispatch_Board
        {
            public const string DISPLAY_NAME_FILTER       = "DisplayName";
            public const string EMPLOYEE_ID_FILTER        = "EmployeeID";
            public const string ASSIGNED_EMPLOYEE_FILTER  = "AssignedEmpID";
            public const string REPORT_TO_EMPLOYEE_FILTER = "ReportsTo";
            public const string LIKETEXT_FILTER           = "LikeText";
            public const string SKILL_FILTER              = "Skill";
            public const string LICENSE_TYPE_FILTER       = "LicenseType";
            public const string PROBLEM_FILTER            = "Problem";
            public const string SERVICE_CLASS_FILTER      = "ServiceClass";
            public const string GEO_ZONE_FILTER           = "GeoZone";
            public const string SERVICE_FILTER            = "Service";
            public const string DEFINED_SCHEDULER_FILTER  = "DefinedScheduler";
        }

        [PXLocalizable]
        public static class CustomTextFields
        {
            public const string DRIVER_ID            = "Driver ID";
            public const string VEHICLE_ID           = "Vehicle ID";
            public const string CUSTOMER_LOCATION    = "Customer Location";
            public const string CUSTOMER_ID          = "Customer ID";
            public const string DESCRIPTION          = "Description";
            public const string ESTIMATED_DURATION   = "Estimated Duration";
            public const string CUSTOMER_NAME        = "Customer Name";
            public const string STAFF_MEMBER_ID      = "Staff Member ID";
            public const string STAFF_MEMBER_NAME    = "Staff Member Name";
            public const string PRIORITY_PREFERENCE  = "Priority Option";
            public const string SERVICE_ORDER        = "Service Order";
            public const string SERVICE_ORDER_DETAIL = "Service Order Detail";
        }

        [PXLocalizable]
        public static class RecurrenceDescription
        {
            public const string ST           = "st";
            public const string ND           = "nd";
            public const string RD           = "rd";
            public const string TH           = "th";
            public const string ON           = "on";
            public const string OF           = "of";
            public const string THAT         = "that";
            public const string THE          = "the";
            public const string AND          = "and";
            public const string DAYS         = "Days";
            public const string WEEKS        = "Weeks";
            public const string MONTHS       = "Months";
            public const string YEARS        = "Years";
            public const string DAY          = "day";
            public const string MONTH        = "month";
            public const string OCCURS_EVERY = "Occurs every";
        }

        [PXLocalizable]
        public static class WildCards
        {
            public const string STAFF = "Staff";
            public const string SERVICE = "Service";
            public const string SHIPPING_ADDRESS = "Shipping Address";
            public const string SHIPPING_CONTACT = "Shipping Contac";
        }

        [PXLocalizable]
        public static class Messages
        {
            public const string UPDATE_SCHEDULEDTIME_WITH_SERVICESTIME             = "The Sum of the Services Estimated Duration does not match the duration defined in the Scheduled Time section. Do you want to update the End Scheduled Time?";
            public const string ASK_UPDATE_ATTENDEES                               = "The Appointments associated to this Service Order may already have attendees assigned. Would you like to replace them with the attendees from this Service Order?";
            public const string ASK_UPDATE_ACTUAL_DURATION_TOTAL                   = "The actual duration of the appointmet is lesser than the services actual duration summatory. Would you like to replace it anyway?";
            public const string NO_CUSTOMER_LOCATION                               = "No Customer Location defined";
            public const string POSITION_PROPAGATE_CONFIRM                         = "Changes will be saved. Do you want to propagate the changes to the associated Employees?";
            public const string ASK_CONFIRM_ROLLBACK_ADVANCED_CONTRACT_GENERATION  = "All the Service Orders generated in the latest generation process will be deleted. Do you want to continue?";
            public const string ASK_CONFIRM_ROLLBACK_ROUTES_CONTRACT_GENERATION    = "All Routes and Appointments generated in the latest generation process will be deleted. Do you want to continue?";                                    
            public const string ASK_CONFIRM_ROLLBACK_SCHEDULE_GENERATION           = "All schedule rules generated in the last run process are going to be deleted. Do you want to continue?";
            public const string ASK_CONFIRM_UNASSIGN_APPOINTMENT                   = "The selected appointment will be unassigned. Are you sure?";
            public const string ASK_CONFIRM_DELETE_APPOINTMENT_FROM_DB             = "The selected appointment will be deleted from all the records. Are you sure?";          
            public const string MASKCUSTOMERLOCATION                               = "Customer Location";
            public const string MASKITEM                                           = "Inventory Item";
            public const string MASKSERVICEORDERTYPE                               = "Service Order Type";
            public const string MASKCOMPANY                                        = "Branch";
            public const string MASKBRANCHLOCATION                                 = "Branch Location";
            public const string MASKPOSTINGCLASS                                   = "Posting Class";
            public const string MASKSALESPERSON                                    = "Salesperson";
            public const string MASKWAREHOUSE                                      = "Warehouse";
            public const string LIST_LAST_ITEM_PREFIX                              = " and ";
            public const string NO_STAFF_ASSIGNED_FOR_THE_APPOINTMENT              = "THERE IS NO STAFF ASSIGNED FOR THIS APPOINTMENT";
            public const string NO_CONTACT_FOR_THE_CUSTOMER                        = "CONTACT NAME MISSING";
            public const string NO_CONTACT_CELL_FOR_THE_CUSTOMER                   = "CONTACT CELL MISSING";
            public const string NO_CONTACT_CELL_FOR_THE_STAFF                      = "STAFF CONTACT CELL MISSING";
            public const string ASK_CONFIRM_CALENDAR_WEEKCODE_GENERATION           = "Calendar Week Code will be automatically generated from {0} to {1}. Do you want to proceed?";
            public const string CANNOT_HIDE_ROOMS_IN_SM                            = "Currently there is at least one Service Order Type requiring rooms. Turning off this feature, will also disable the rooms requirement for the Service Order Types. Would you like to proceed with this change?";
            public const string RECORD_PROCESSED_SUCCESSFULLY                      = "Record processed successfully.";
            public const string COULD_NOT_PROCESS_RECORD_BY_BILLING_GROUP_WITHDOC  = "Could not process this record. Its billing group is having an error with document ( Service Order Type: {0}, Reference Nbr.: {1} ).";
            public const string COULD_NOT_PROCESS_RECORD_BY_BILLING_GROUP          = "Could not process this record. Its billing group is having an error.";
            public const string COULD_NOT_PROCESS_RECORD                           = "Could not process this record.";
			public const string CLEAR_ALL_FILTERS                                  = "Clear All Filters";
            public const string SERVICE_ORDER_TYPE_USED_FOR_RECURRING_APPOINTMENTS = "This Service Order Type will be used for the recurring appointments";
            public const string ASK_CONFIRM_ROUTE_CLOSING                          = "The current route will be closed. Do you want to proceed?";
            public const string ASK_CONFIRM_ROUTE_UNCLOSING                        = "The current route will be unclosed. Do you want to proceed?";
            public const string ASK_CONFIRM_APP_SO_UNCLOSING                       = "The current appointment and its service order will be unclosed. Do you want to proceed?";
            public const string ASK_CONFIRM_APPOINTMENT_UNCLOSING                  = "The current appointment will be unclosed. Do you want to proceed?";
            public const string ASK_CONFIRM_SERVICE_ORDER_UNCLOSING                = "The current service order will be unclosed. Do you want to proceed?";
            public const string ASK_CONFIRM_CHANGE_UOM_FSSALESPRICE                = "Do you want to replicate this Base Unit change to the details of Service Contracts?";
            public const string ASK_CONFIRM_SERVICE_ORDER_CLOSING                  = "This Service Order still has open Appointments. If you close the Service Order its appointments will also be closed. Do you want to proceed?";
            public const string ASK_CONFIRM_DELETE_CURRENT_ROUTE                   = "The Appointments and Service Orders will be also deleted with this action. Are you sure?";
            public const string EQUIPMENT_IS_INSTATUS                              = "Equipment is {0}.";
            public const string VEHICLE_IS_INSTATUS                                = "Vehicle is {0}.";
            public const string ACCESS_RIGHTS_NOTIFICATION                         = "You have insufficient access rights to perform this action.";
            public const string ASK_CONFIRM_MODEL_AND_COMPONETS_RESET              = "Please confirm if you want to update current Manufacturer Model and Components with the Model defaults. Original values will be preserved otherwise.";
            public const string ERROR_CREATING_POSTING_BATCH                       = "An error occurred creating the Posting Batch {0}. The batch will be deleted.";
            public const string ERROR_CREATING_INVOICE_IN_POSTING_BATCH            = "The generation of invoice has failed.";
            public const string ASK_CONFIRM_SERVICE_ORDER_COMPLETING               = "This Service Order does not have any other Appointment to be completed. Do you want to complete this Service Order?";
            public const string ASK_CONFIRM_SERVICE_ORDER_CANCELING                = "All Appointments in this Service Order are cancelled. Do you want to cancel this Service Order?";
            public const string EMPLOYEE_IS_IN_STATUS                              = "Employee is {0}.";
            public const string COMPONENT_ALREADY_REPLACED                         = "Component selected was already replaced.";
            public const string ServiceOrderTax                                    = "Service Order Tax";
            public const string AppointmentTax                                     = "Appointment Tax";
            public const string CREATE_INVOICE_BILLING_CYCLE                       = "Create invoice document for Billing Cycle '{0}', GroupKey '{1}'.";
            public const string APPLY_PREPAYMENT_BILLING_CYCLE                     = "Apply Prepayments on the invoice documents generated for Billing Cycle '{0}'.";
            public const string CREATE_FSPOSTBATCH                                 = "Create FSPostBatch for Billing Cycle '{0}'.";
            public const string COMPLETE_FSPOSTBATCH_BILLING_CYCLE                 = "Complete FSPostBatch for Billing Cycle '{0}'.";
            public const string INVOICE_POSSIBLE_ERRORS                            = "Possible error(s) in current {0}: ";
            public const string RETRY_CREATE_INVOICE                               = "Retrying CreateInvoice with the BatNbr = {0} and GroupKey = {1} after getting error: {2}.";
            public const string DONT_APPROVED_DOCUMENTS_CANNOT_BE_SELECTED         = "A Prepayment cannot be created for a Service Order with one of the following statuses: On Hold, Quote, or Canceled.";
        }

        [PXLocalizable]
        public static class WebDialogTitles
        {
            public const string UPDATE_ACTUAL_DURATION_TOTAL            = "Update Attendees";
            public const string SRVORDER_NOTE_WINDOW                    = "Service Order Note";
            public const string POSITION_PROPAGATE_CONFIRM              = "Propagation Confirmation";
            public const string CONFIRM_ROLLBACK_CONTRACT_GENERATION    = "Confirm roll back generation";
            public const string CONFIRM_UNASSIGN_APPOINTMENT            = "Confirm Unassign Appointment";
            public const string CONFIRM_CALENDAR_WEEKCODE_GENERATION    = "Confirm Calendar Week Code Generation";
            public const string CONFIRM_MANAGE_ROOMS                    = "Confirm Manage Rooms change";
            public const string CONFIRM_ROUTE_CLOSING                   = "Confirm Route Closing";
            public const string CONFIRM_ROUTE_UNCLOSING                 = "Confirm Route Unclosing";
            public const string CONFIRM_APPOINTMENT_UNCLOSING           = "Confirm Appointment Unclosing";
            public const string CONFIRM_SERVICE_ORDER_UNCLOSING         = "Confirm Service Order Unclosing";
            public const string CONFIRM_SERVICE_ORDER_CLOSING           = "Confirm Service Order Closing";
            public const string CONFIRM_ROUTE_DELETING                  = "Confirm Delete Current Route";
            public const string CONFIRM_CHANGE_FSSALESPRICE_UOM         = "Confirm Base Unit change for Service Contract";
            public const string CONFIRM_SERVICE_ORDER_COMPLETING        = "Complete Service Order";
            public const string CONFIRM_SERVICE_ORDER_CANCELING         = "Cancel Service Order";
            public const string UPDATE_BILLING_SETTINGS                 = "Update Billing Settings";
        }

        [PXLocalizable]
        public static class AppResizePrecision_Setup
        {
            public const string MINUTES_10 = "10 MINUTES";
            public const string MINUTES_15 = "15 MINUTES";
            public const string MINUTES_30 = "30 MINUTES";
            public const string MINUTES_60 = "60 MINUTES";
        }

        // FSModelWarranty - WarrantyDurationType
        [PXLocalizable]
        public static class WarrantyDurationType
        {
            public const string DAY   = "Days";
            public const string MONTH = "Months";
            public const string YEAR  = "Years";
        }

        // FSModelWarranty - DfltWarrantyApplicationOrder
        [PXLocalizable]
        public static class WarrantyApplicationOrder
        {
            public const string COMPANY = "Company";
            public const string VENDOR  = "Vendor";
        }

        [PXLocalizable]
        public static class ModelType
        {
            public const string EQUIPMENT   = "Equipment";
            public const string REPLACEMENT = "Replacement Part";
        }

        // SourceType_Equipment
        [PXLocalizable]
        public class SourceType_Equipment
        {
            public const string SM_EQUIPMENT       = "SD - Equipment";
            public const string VEHICLE            = "SD - Vehicle";
            public const string EP_EQUIPMENT       = "EP - Equipment";
            public const string AR_INVOICE         = "AR - Invoice";
        }

        // SourceType_Equipment_ALL
        //This class is used for filtering purposes only
        [PXLocalizable]
        public class SourceType_Equipment_ALL : SourceType_Equipment
        {
            public const string ALL = "All";
        }

        [PXLocalizable]
        public class OwnerType_Equipment
        {
            public const string CUSTOMER        = "Customer";
            public const string OWN_COMPANY     = "Company";
        }       

        public class MapsWebService
        {
            public const string URL_PREFIX = "https://dev.virtualearth.net/REST/v1/Routes/Driving?distanceUnit=mi&o=xml&";
        }

        public class ViewNames
        {
            public const string ServiceContractAnswers = "Service Contract Answers";
        }

        [PXLocalizable]
        public static class FrecuencySchedule
        {
            public const string DAILY             = "Daily";
            public const string WEEKLY            = "Weekly";
            public const string MONTHSPECIFICDATE = "Specific Date in a Month";
            public const string MONTHWEEKDAY      = "Specific Week Day of the Month";
        }

        [PXLocalizable]
        public static class ActionType_ProcessServiceContracts
        {
            public const string STATUS = "Update to Upcoming Status";
            public const string PERIOD = "Activate Upcoming Billing Period";
        }

        #region Almanac

        #region WeekDays
        [PXLocalizable]
        public class WeekDays
        {
            public const string SUNDAY = "Sunday";
            public const string MONDAY = "Monday";
            public const string TUESDAY = "Tuesday";
            public const string WEDNESDAY = "Wednesday";
            public const string THURSDAY = "Thursday";
            public const string FRIDAY = "Friday";
            public const string SATURDAY = "Saturday";
            public const string WEEKEND = "Weekend";
            public const string WEEKDAY = "Weekday";
            public const string ANYDAY = "Any";
        }
        #endregion

        #region Month
        [PXLocalizable]
        public class Months
        {
            public const string JANUARY = "January";
            public const string FEBRUARY = "February";
            public const string MARCH = "March";
            public const string APRIL = "April";
            public const string MAY = "May";
            public const string JUNE = "June";
            public const string JULY = "July";
            public const string AUGUST = "August";
            public const string SEPTEMBER = "September";
            public const string OCTOBER = "October";
            public const string NOVEMBER = "November";
            public const string DECEMBER = "December";
        }
        #endregion

        #region ShortMonth
        [PXLocalizable]
        public class ShortMonths
        {
            public const string JANUARY = "JAN";
            public const string FEBRUARY = "FEB";
            public const string MARCH = "MAR";
            public const string APRIL = "APR";
            public const string MAY = "MAY";
            public const string JUNE = "JUN";
            public const string JULY = "JUL";
            public const string AUGUST = "AUG";
            public const string SEPTEMBER = "SEP";
            public const string OCTOBER = "OCT";
            public const string NOVEMBER = "NOV";
            public const string DECEMBER = "DEC";
        }
        #endregion

        #endregion

        #region TimePositioning
        #region Counting
        [PXLocalizable]
        public class Counting
        {
            public const string FIRST = "First";
            public const string SECOND = "Second";
            public const string THIRD = "Third";
            public const string FOURTH = "Fourth";
            public const string FIFTH = "Fifth";
            public const string SIXTH = "Sixth";
            public const string SEVENTH = "Seventh";
            public const string EIGHTH = "Eighth";
            public const string NINTH = "Ninth";
            public const string LAST = "Last";
        }
        #endregion

        #endregion
        [PXLocalizable]
        public static class RecordType_ServiceContract
        {
            public const string SERVICE_CONTRACT           = "Service Contract";
            public const string ROUTE_SERVICE_CONTRACT     = "Route Service Contract";
            public const string EMPLOYEE_SCHEDULE_CONTRACT = "Employee Schedule Contract";
        }

        [PXLocalizable]
        public static class ScheduleGenType_ServiceContract
        {
            public const string SERVICE_ORDER = "Service Orders";
            public const string APPOINTMENT   = "Appointments";
            public const string NONE = "None";
        }

        [PXLocalizable]
        public static class Behavior_SrvOrderType
        {
            public const string REGULAR_APPOINTMENT  = "Regular";
            public const string ROUTE_APPOINTMENT    = "Route";
            public const string INTERNAL_APPOINTMENT = "Internal";
            public const string QUOTE                = "Quote";
        }

        [PXLocalizable]
        public static class PreAcctSource_Setup
        {
            public const string CUSTOMER_LOCATION = "Customer Location";
            public const string INVENTORY_ITEM = "Inventory Item";
        }

        [PXLocalizable]
        public static class ContactType_ApptMail
        {
            public const string CUSTOMER        = "Customer";
            public const string STAFF_MEMBERS   = "Staff Members";

            public const string VENDOR          = "Vendor";
            public const string GEOZONE_STAFF   = "Service Area Staff";
            public const string SALESPERSON     = "Salesperson";
        }

        [PXLocalizable]
        public class EmployeeTimeSlotLevel
        {
            public const string BASE     = "Base";
            public const string COMPRESS = "Compressed";
        }

        [PXLocalizable]
        public static class Service_Action_Type
        {
            public const string NO_ITEMS_RELATED = "N/A";
            public const string PICKED_UP_ITEMS  = "Pick Up Items";
            public const string DELIVERED_ITEMS  = "Deliver Items";
        }

        [PXLocalizable]
        public static class Appointment_Service_Action_Type
        {
            public const string PICKED_UP_ITEMS     = "Picked Up";
            public const string DELIVERED_ITEMS     = "Delivered";
        }

        [PXLocalizable]
        public static class CalendarBoardAccess
        {
            public const string MULTI_EMP_CALENDAR  = "Schedule on the Calendar Board";
            public const string SINGLE_EMP_CALENDAR = "Schedule on the Staff Calendar Board";
            public const string ROOM_CALENDAR       = "Schedule on the Room Calendar Board";
        }

        [PXLocalizable]
        public static class ActionCalendarBoardAccess
        {
            public const string MULTI_EMP_CALENDAR  = "Schedule on the Calendar Board";
            public const string SINGLE_EMP_CALENDAR = "Schedule on the Staff Calendar Board";
        }

        [PXLocalizable]
        public static class AppointmentTotalTimesLabels
        {
            public const string ESTIMATED = "Estimated";
            public const string ACTUAL    = "Actual";
        }

        [PXLocalizable]
        public static class Billing_By
        {
            public const string APPOINTMENT     = "Appointments";
            public const string SERVICE_ORDER   = "Service Orders";
            public const string CONTRACT        = "Contracts";
        }

        [PXLocalizable]
        public static class Billing_Cycle_Type
        {
            public const string APPOINTMENT     = "Appointments";
            public const string SERVICE_ORDER   = "Service Orders";
            public const string TIME_FRAME      = "Time Frame";
            public const string PURCHASE_ORDER  = "Customer Order";
            public const string WORK_ORDER      = "External Reference";
        }

        [PXLocalizable]
        public static class Time_Cycle_Type
        {
            public const string WEEKDAY      = "Fixed Day of Week";
            public const string DAY_OF_MONTH = "Fixed Day of Month";
        }

        [PXLocalizable]
        public static class Frequency_Type
        {
            public const string WEEKLY  = "Weekly";
            public const string MONTHLY = "Monthly";
            public const string NONE    = "None";
        }

        [PXLocalizable]
        public static class Send_Invoices_To
        {
            public const string BILLING_CUSTOMER_BILL_TO          = "Billing Customer";
            public const string DEFAULT_BILLING_CUSTOMER_LOCATION = "Default Billing Customer Location";
            public const string SO_BILLING_CUSTOMER_LOCATION      = "Specific Billing Customer Location";
            public const string SERVICE_ORDER_ADDRESS             = "Service Order";
        }

        [PXLocalizable]
        public static class Ship_To
        {
            public const string BILLING_CUSTOMER_BILL_TO     = "Billing Customer";
            public const string SO_BILLING_CUSTOMER_LOCATION = "Specific Billing Customer Location";
            public const string SO_CUSTOMER_LOCATION         = "Specific Customer Location";
            public const string SERVICE_ORDER_ADDRESS        = "Service Order";
        }

        [PXLocalizable]
        public static class Default_Billing_Customer_Source
        {
            public const string SERVICE_ORDER_CUSTOMER = "Service Order Customer";
            public const string DEFAULT_CUSTOMER = "Default Customer";
            public const string SPECIFIC_CUSTOMER = "Specific Customer";
        }

        [PXLocalizable]
        public static class RouteLocationType
        {
            public const string BRANCH_LOCATION     = "BRLC";
            public const string EMPLOYEE_LOCATION   = "EMLC"; //For future use
        }
        
        [PXLocalizable]
        public static class Batch_PostTo
        {
            public const string AR_AP   = "Accounts Receivable and/or Accounts Payable";
            public const string AR      = "Accounts Receivable";
            public const string SO      = "Sales Orders";
            public const string AP      = "Accounts Payable";
            public const string IN      = "Inventory";
        }

        [PXLocalizable]
        public static class Route_Location
        {
            public const string START_LOCATION = "START LOCATION";
            public const string END_LOCATION = "END LOCATION";
        }

        [PXLocalizable]
        public static class Status_Batch
        {
            public const string Temporary = "Temporary";
            public const string Completed = "Completed";
        }

		[PXLocalizable]
        public static class Equipment_Item_Class
        {
            public const string PART_OTHER_INVENTORY = "Part or Other Inventory";
            public const string MODEL_EQUIPMENT = "Model Equipment";
            public const string COMPONENT = "Component";
            public const string CONSUMABLE = "Consumable";
        }

        [PXLocalizable]
        public static class CloningType_CloneAppointment
        {
            public const string SINGLE      = "Single";
            public const string MULTIPLE    = "Multiple";
        }

        #region RecurrencyFrecuency
        public class RecurrencyFrecuency
        {
            public static string[] counters = { Counting.FIRST, Counting.SECOND, Counting.THIRD, Counting.FOURTH, Counting.LAST };
            public static string[] daysOfWeek = { WeekDays.SUNDAY, WeekDays.MONDAY, WeekDays.TUESDAY, WeekDays.WEDNESDAY, WeekDays.THURSDAY, WeekDays.FRIDAY, WeekDays.SATURDAY };
        }
        #endregion

        public static class Equipment_Status
        {
            public const string ACTIVE      = "Active";
            public const string SUSPENDED   = "Suspended";
            public const string DISPOSED    = "Disposed";
        }

        public static class Equipment_Action
        {
            public const string NONE                         = "N/A";
            public const string SELLING_TARGET_EQUIPMENT     = "Selling Model Equipment";
            public const string REPLACING_TARGET_EQUIPMENT   = "Replacing Target Equipment";
            public const string CREATING_COMPONENT           = "Selling Optional Component";
            public const string UPGRADING_COMPONENT          = "Upgrading Component";
            public const string REPLACING_COMPONENT          = "Replacing Component";
        }

        [PXLocalizable]
        public static class ServiceOrder_Action_Filter
        {
            public const string UNDEFINED     = "<SELECT>";
            public const string COMPLETE      = "Complete Order";
            public const string CANCEL        = "Cancel Order";
            public const string REOPEN        = "Reopen Order";
            public const string CLOSE         = "Close Order";
            public const string UNCLOSE       = "Unclose Order";
            public const string ALLOWINVOICE  = "Allow Invoice";
        }

        public static class GenericInquiries_GUID
        {
            public const string EQUIPMENT_SUMMARY = "e850784d-9b5c-45f9-a7ca-085aa07cdcdb";
            public const string SERVICE_CLASSES = "1a963056-485f-42c8-81d9-2a06610128da";
            public const string LICENSES = "715aebe2-d1e7-4d45-9ba3-41a1251141e9";
            public const string SERVICE_ORDER_HISTORY = "84b92648-c42e-41e8-855c-4aa9144b9eda";
            public const string SERVICES = "cfde9093-83c9-49b3-a04b-6d08e3b75b10";
            public const string STAFF_SCHEDULE_RULES = "ae872579-713f-4b93-95ad-89d8dc51a7e6";
            public const string SERVICE_ORDER_DETAILS_HISTORY = "dd5375be-b8f0-43cb-870c-900371df5942";
            public const string CONTRACT_SUMMARY = "4c33d513-ef82-4b7a-aafc-913e856bf89c";
            public const string MODEL_EQUIPMENT_SUMMARY = "686044fa-e926-4282-8dd1-ac2d943dd33b";
            public const string CONTRACT_SCHEDULE_DETAILS_SUMMARY = "5566eca3-a20a-4d9c-8b1d-bb6b32ae6e9f";
            public const string CONTRACT_SCHEDULE_SUMMARY = "09c88688-263d-426a-a19e-de1d0c3d3ad3";
            public const string COMPONENT_SUMMARY = "8bc77dbd-a02e-41b1-89ef-e7e498b612ec";
            public const string APPOINTMENT_DETAILS_HISTORY = "6e27759d-b6ac-4105-ba3e-fa62a6cd0c67";
        }

        public static class FriendlyViewName
        {
            [PXLocalizable]
            public static class Appointment
            {
                public const string SERVICEORDER_RELATED        = "Service Order";
                public const string APPOINTMENT_SELECTED        = "Appointment";
                public const string APPOINTMENT_DET_SERVICES    = "Appointment Services";
                public const string APPOINTMENT_DET_PARTS       = "Appointment Parts";
                public const string APPOINTMENT_ATTENDEES       = "Appointment Attendees";
                public const string APPOINTMENT_RESOURCES       = "Appointment Resources";
                public const string APPOINTMENT_EMPLOYEES       = "Appointment Employees";
                public const string PICKUP_DELIVERY_ITEMS       = "Pickup Delivery Items";
                public const string APPOINTMENT_POSTED_IN       = "Appointment Posting Info";
            }

            [PXLocalizable]
            public static class ServiceOrder
            {
                public const string SERVICEORDER_RELATED        = "Service Order";
                public const string SERVICEORDER_DET_SERVICES   = "Service Order Services";
                public const string SERVICEORDER_DET_PARTS      = "Service Order Parts";
                public const string SERVICEORDER_APPOINTMENTS   = "Appointments in Service Order";
                public const string SERVICEORDER_RESOURCES      = "Service Order Resources";
                public const string SERVICEORDER_EMPLOYEES      = "Service Order Employees";
                public const string SERVICEORDER_EQUIPMENT      = "Service Order Equipment";
                public const string SERVICEORDER_ATTENDEES      = "Service Order Attendees";
                public const string SERVICEORDER_POSTED_IN      = "Service Order Posting Info";
            }

            [PXLocalizable]
            public static class Common
            {
                public const string SERVICEORDERTYPE_SELECTED = "Service Order Type";
            }
        }
    }
}
