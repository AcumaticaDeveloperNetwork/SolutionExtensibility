//Titles Definition

var TX = {
    /*** Titles ***/
    titles: {
        /* Employee Routes*/
        appointment: 'Appointments',
        appointmentl: 'Appointment',
        cancelChanges: 'Cancel changes',
        employeeList: 'Staff List',
        treeviewTitle: 'Resources',
        postalCode: 'Postal Code',
        location: 'Location',
        travelTime: 'Travel Time',
        address: 'Address',
        /*Routes*/
        customer: 'Route/Customer',
        serviceType: 'Service Type',
        duration: 'Duration',
        servicesDuration: 'Service Duration',
        routeList: 'Route List',
        routeTreeTitle: 'Routes',
        employeeTreeTitle: 'Staff',
        routeInfoTitle: 'Route Information',
        appointmentInfoTitle: 'Appointment Information'
    },

    /*Context menu*/
    appointmentInfo: 'Appointment Info',
    routeInfo: 'Route Info',
    removeNode: 'Remove Resource',
    showRouteInfoWindow: 'Show or Hide Route Appointment Information on the Map',
    showRouteDevice: 'Show or Hide GPS Location on the Map',
    collapseAll: 'Collapse All',

    /*Context menu tooltip*/
    appointmentInfoToolTip: 'Open Appointment Information',
    resourceInfoToolTip: 'Open Resource Information',
    showRouteInfoWindowToolTip: 'Show or Hide All Information Tooltips of this Route on the Map.',
    removeNodeToolTip: 'Remove this Resource from List',
    expandCollapseRouteToolTip: 'Expand or Collapse All Appointments of this Route',
    collapseAllToolTip: 'Collapse All Nodes',
    showRouteDeviceToolTip: 'Show or Hide GPS Location on the Map',

    /* Messages */
    messages: {
        discardUnsavedChanges: 'Any unsaved will be discarded',
        unassignedContact: 'Unassigned appointments',
        googleRouteError: 'There is an error with the appointment of this route. Please verify the address.',
        calculate: '(Calculate)',
        mapApiError: 'This route cannot be traced with the provided data. Please revise the appointments addresses, some are not recognized by the system.'
    },

    /* Buttons */
    buttons: {
        accept: 'Accept',
        cancel: 'Cancel',
        calculate: 'Calculate',
    },

    /* Gmap Marlker title prefix*/
    //@TODO SD-3557 get this from the setup
    markerTitlePrefix: 'Appointment Ref:'

}