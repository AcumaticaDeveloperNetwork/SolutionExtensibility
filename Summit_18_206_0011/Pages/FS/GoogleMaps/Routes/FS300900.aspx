<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FS300900.aspx.cs" Inherits="Page_FS300900" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="DB" >
    <head id="Head1" runat="server">
        <meta http-equiv="content-type" content="text/html; charset=UTF-8">

        <title>Routes on the Map</title>
        <link rel="stylesheet" href="../../Shared/css/sch-all.css">
        <link rel="stylesheet" href="../../Shared/css/style.css">
        <link rel="stylesheet" href="../../Shared/css/routes-main-new.css">

    </head>
    <body>   
        <!-- Main Container -->
        <div id="main-container">
            <!-- Title -->
            <form id="form1" runat="server">
			    <px_pt:PageTitle ID="pageTitle" runat="server" CustomizationAvailable="false" HelpAvailable="false"/>
			</form>
            <!-- Routes -->
            <div class="container">
                <div id="routes-container">
	            </div>
            </div>
            <!-- End Routes -->
        </div>
       <!-- End Main Container -->

   <!-- Teemplates ToolTip -->
   <%= infoRoute %>

    <!-- Global Variables -->
    <script type="text/javascript">
        var pageUrl= "<%= pageUrl %>";
        var baseUrl= "<%= baseUrl %>";
        var startDate = "<%= startDate %>";
        var RefNbr = "<%= RefNbr %>";
        var MapApiKey = "<%= apiKey %>";
        var mapCallBack = function () {};
        var mapClass;
    </script>

    <!-- Configuration files -->
    <script src="../../Shared/definition/GoogleMaps/ID.js" type="text/javascript"></script>
    <script src="../../Shared/definition/GoogleMaps/TX.js" type="text/javascript"></script>
    <script src="../../Shared/definition/GoogleMaps/Cfg.js" type="text/javascript"></script>
    <script src="../../Shared/definition/Calendars/Messages.js" type="text/javascript"></script>
    <script src="../../../../Scripts/jquery-3.1.1.js" type="text/javascript"></script>

    <script>
        window.onbeforeunload = function(e) {
            return TX.messages.windowCloseWarning;
        };
    </script>

<link rel="stylesheet" href="resources/Routes-all.css"/>
<script type="text/javascript" src="app.js"></script>

    </body>
</html>
