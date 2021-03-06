<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SO302010.aspx.cs"
    Inherits="Page_SO302010" Title="Pick, Pack and Ship" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:content id="cont1" contentplaceholderid="phDS" runat="Server">
	<script language="javascript" type="text/javascript">
	    function Barcode_Initialize(ctrl) {
	        ctrl.element.addEventListener('keydown', function (e) {
	            if (e.keyCode === 13) { //Enter key 
	                e.preventDefault();
	                e.stopPropagation();
	                var ds = px_alls["ds"];
	                ds.executeCallback("scan");
	            }
	        });
	    }

	    function ActionCallback(callbackContext) {
	        var baseUrl = (location.href.indexOf("HideScript") > 0) ? "../../" : "../../../";
	        var edStatus = px_alls["edStatus"];

	        if ((callbackContext.info.name == "confirm" || callbackContext.info.name == "confirmAll" || callbackContext.info.name == "scan" || callbackContext.info.name == "ElapsedTime") && callbackContext.control.longRunInProcess == null && edStatus != null) {
	            if (edStatus.getValue() == "OK") {
	                var audio = new Audio(baseUrl + 'Sounds/success.wav');
	                audio.play();
	            }
	            else if (edStatus.getValue() == "CLR") {
	                var audio = new Audio(baseUrl + 'Sounds/balloon.wav');
	                audio.play();
	            }
	            else if (edStatus.getValue() == "SCN" || edStatus.getValue() == "INF") {
	                var audio = new Audio(baseUrl + 'Sounds/balloon.wav');
	                audio.play();
	            }
	            else if (edStatus.getValue() == "ERR") {
	                var audio = new Audio(baseUrl + 'Sounds/asterisk.wav');
	                audio.play();
	            }
	        }
	    };

	    window.addEventListener('load', function () { px_callback.addHandler(ActionCallback); });
		
		function RefreshWebPage_Click(btn, ev)
		{
			px_alls.ds.isDirty = false;	
			px_cm.checkDataChanges = function() { return false; }
			window.location.reload(true);
		}

		window.addEventListener('keydown', function(e) {
			var KEYCODE_B = 66;
			var KEYCODE_GRAVE_ACCENT = 192;
			var KEYCODE_NO = 78;
			var KEYCODE_YES = 89;
			var key = e.keyCode ? e.keyCode : e.which;

			var messageBox = document.getElementById("messageBox");
			var messageBoxYesButton = document.getElementById("messageBox_0");
			var messageBoxNoButton = document.getElementById("messageBox_1");
			
			var refreshConfirmationPanel = document.getElementById("<%=PanelRefreshConfirmation.ClientID %>");
			var messageBoxRefreshConfirmationYesButton = document.getElementById("ctl00_phG_PanelRefreshConfirmation_edRefreshConfirmationYesButton");
			var messageBoxRefreshConfirmationNoButton = document.getElementById("ctl00_phG_PanelRefreshConfirmation_edRefreshConfirmationCancelButton");
			
			if (messageBox && messageBox.style && messageBox.style.display === 'none')
			{
				messageBox = undefined;
				messageBoxYesButton = undefined;
				messageBoxNoButton = undefined;
			}
			
			if (refreshConfirmationPanel && refreshConfirmationPanel.style && refreshConfirmationPanel.style.display === 'none')
			{
				refreshConfirmationPanel = undefined;
				messageBoxRefreshConfirmationYesButton = undefined;
				messageBoxRefreshConfirmationNoButton = undefined;
			}
			
			if (key === KEYCODE_GRAVE_ACCENT)
			{
				// KeyCode 192 = '`' (grave accent), refresh web page				
				refreshConfirmationPanel = document.getElementById("<%=PanelRefreshConfirmation.ClientID %>");
				refreshConfirmationPanel.style.display = 'block';
				e.preventDefault();
			}
			else if (messageBoxNoButton && key === KEYCODE_NO)
			{
				// KeyCode 78 = 'N', click on message box No button
			    messageBoxNoButton.focus();
			    messageBoxNoButton.click();
			    
				px_alls["edBarcode"].focus();
				e.preventDefault();
			}
			else if (messageBoxYesButton && key === KEYCODE_YES)
			{
				// KeyCode 89 = 'Y', click on message box Yes button
			    messageBoxYesButton.focus();
			    messageBoxYesButton.click();
				
				px_alls["edBarcode"].focus();
				e.preventDefault();
			}
			else if (messageBoxRefreshConfirmationNoButton && key === KEYCODE_NO)
			{
				// KeyCode 78 = 'N', click on message box No button
			    messageBoxRefreshConfirmationNoButton.focus();
			    messageBoxRefreshConfirmationNoButton.click();
			    e.preventDefault();
				px_alls["edBarcode"].focus();
			}
			else if (messageBoxRefreshConfirmationYesButton && key === KEYCODE_YES)
			{
				// KeyCode 89 = 'Y', click on message box Yes button
			    messageBoxRefreshConfirmationYesButton.focus();
			    messageBoxRefreshConfirmationYesButton.click();
				px_alls["edBarcode"].focus();
            }
			// STX non printable character, transcoded as Ctl-B in keydown event
			else if (e.ctrlKey && key === KEYCODE_B) {
			    // Focus barcode UI field when receiving barcode prefix (STX character)
			    px_alls["edBarcode"].focus();

			    // Prevent IE from firing open bookmark shortcut (Ctl-B)
			    e.preventDefault();
			}
		});
	</script>

	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Objects.SO.PickPackShip" PrimaryView="Document">
		<CallbackCommands>
			<px:PXDSCallbackCommand Name="Confirm" Visible="True" CommitChanges="True" />
			<px:PXDSCallbackCommand Name="ConfirmAll" Visible="True" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="Scan" Visible="False" CommitChanges="True" />
            <px:PXDSCallbackCommand Name="Allocations" Visible="False" CommitChanges="true" DependOnGrid="grid" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:content>
<asp:content id="cont2" contentplaceholderid="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Height="100px" Width="100%" Visible="true" DataMember="Document" DefaultControlID="edBarcode">
        <Template>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="L" />
            <px:PXTextEdit ID="edBarcode" runat="server" DataField="Barcode">
                <ClientEvents Initialize="Barcode_Initialize" />
            </px:PXTextEdit>            
			<px:PXSelector ID="edShipmentNbr" runat="server" DataField="ShipmentNbr" AutoRefresh="true" AllowEdit="true" CommitChanges="true" AutoComplete="false" />
            <px:PXCheckBox ID="edLotSerialSearch" runat="server" DataField="LotSerialSearch" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="L" ColumnWidth="M" />
            <px:PXLayoutRule runat="server" ColumnSpan="2" />
            <px:PXTextEdit ID="edMessage" runat="server" DataField="Message" Width="640px" Style="font-size: 10pt; font-weight: bold;" SuppressLabel="true" />
            <px:PXNumberEdit ID="edQuantity" runat="server" DataField="Quantity" />
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="M" />
            <px:PXGroupBox ID="gbMode" runat="server" Caption="Scan Mode" DataField="ScanMode" RenderSimple="True" RenderStyle="Simple">
                <Template>
                    <px:PXRadioButton ID="rbAdd" runat="server" GroupName="gbMode"
                        Text="Add" Value="A" />
                    <px:PXRadioButton ID="rbRemove" runat="server" GroupName="gbMode"
                        Text="Remove" Value="R" />
                </Template>
                <ContentLayout Layout="Stack" Orientation="Horizontal" />
            </px:PXGroupBox>
           
            <%--Always hidden, used by JavaScript to decide which sound to play--%>
            <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" />
            <px:PXTextEdit ID="edStatus" runat="server" DataField="Status" SuppressLabel="true" />
        </Template>
    </px:PXFormView>
</asp:content>
<asp:content id="cont3" contentplaceholderid="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Height="540px" Style="z-index: 100;" Width="100%">
        <Items>
            <px:PXTabItem Text="Document Details">
                <Template>
                    <px:PXGrid ID="grid" runat="server" DataSourceID="ds" Style="height: 250px;" Width="100%" 
                        OnRowDataBound="grid_RowDataBound" SkinID="Inquire" StatusField="Availability" SyncPosition="true" Height="372px" TabIndex="-7372">
                        <Levels>
                            <px:PXGridLevel DataMember="Transactions" DataKeyNames="ShipmentNbr,LineNbr">
                                <Columns>
                                    <px:PXGridColumn DataField="Availability" Width="1px" />
                                    <px:PXGridColumn DataField="ShipmentNbr" Width="90px" />
                                    <px:PXGridColumn DataField="LineNbr" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrigOrderType" Width="36px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrigOrderNbr" Width="90px" />
                                    <px:PXGridColumn AllowUpdate="False" DataField="OrigLineNbr" TextAlign="Right" Width="54px" />
                                    <px:PXGridColumn DataField="InventoryID" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" AutoCallBack="True" RenderEditorText="True" />
                                    <px:PXGridColumn DataField="SubItemID" DisplayFormat="&gt;AA-A" Width="45px" NullText="<SPLIT>" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="TranDesc" Width="180px" />
                                    <px:PXGridColumn AllowNull="False" DataField="IsFree" TextAlign="Center" Type="CheckBox" />
                                    <px:PXGridColumn DataField="SiteID" DisplayFormat="&gt;AAAAAAAAAA" Width="81px" AutoCallBack="True" />
                                    <px:PXGridColumn DataField="UOM" Width="54px" AutoCallBack="True" />
                                    <px:PXGridColumn AllowNull="False" DataField="PickedQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" AutoCallBack="True" DataField="ShippedQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="OriginalShippedQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="OrigOrderQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="OpenOrderQty" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn AllowNull="False" DataField="CompleteQtyMin" TextAlign="Right" Width="81px" />
                                    <px:PXGridColumn DataField="ShipComplete" Width="117px" RenderEditorText="True" />
                                </Columns>
                                <RowTemplate>
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                                    <px:PXTextEdit ID="edOrigOrderType" runat="server" DataField="OrigOrderType" Enabled="False" />
                                    <px:PXTextEdit ID="edOrigOrderNbr" runat="server" DataField="OrigOrderNbr" Enabled="False" />
                                    <px:PXSegmentMask CommitChanges="True" ID="edInventoryID" runat="server" DataField="InventoryID" AllowEdit="True" />
                                    <px:PXSegmentMask CommitChanges="True" ID="edSubItemID" runat="server" DataField="SubItemID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXCheckBox ID="chkIsFree" runat="server" DataField="IsFree" Enabled="False" />
                                    <px:PXSegmentMask CommitChanges="True" ID="edSiteID" runat="server" DataField="SiteID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSegmentMask ID="edLocationID" runat="server" DataField="LocationID" AutoRefresh="True">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.siteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSegmentMask>
                                    <px:PXSelector CommitChanges="True" ID="edUOM" runat="server" DataField="UOM">
                                        <Parameters>
                                            <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                        </Parameters>
                                    </px:PXSelector>
                                    <px:PXNumberEdit ID="edShippedQty" runat="server" DataField="ShippedQty" />
                                    <px:PXNumberEdit ID="edOrigOrderQty" runat="server" DataField="OrigOrderQty" Enabled="False" />
                                    <px:PXLayoutRule runat="server" ColumnSpan="2" />
                                    <px:PXTextEdit ID="edTranDesc" runat="server" DataField="TranDesc" />
                                    <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="S" ControlSize="XM" />
                                    <px:PXTextEdit ID="edShipmentNbr" runat="server" DataField="ShipmentNbr" />
                                    <px:PXNumberEdit ID="edLineNbr" runat="server" DataField="LineNbr" />
                                    <px:PXDropDown ID="edShipComplete" runat="server" AllowNull="False" DataField="ShipComplete" />
                                </RowTemplate>
                                <Layout FormViewHeight="" />
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar>
                            <CustomItems>
                                <px:PXToolBarButton Text="Allocations" Key="cmdLS" CommandName="Allocations" CommandSourceID="ds" DependOnGrid="grid" />
                            </CustomItems>
                        </ActionBar>
                         <AutoSize Enabled="True" />
                        <AutoSize Container="Window" Enabled="True" MinHeight="400" />
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Packages">
                <Template>
                    <px:PXSplitContainer runat="server" SplitterPosition="200" SavePosition="True" ID="sp1" Height="500px" SkinID="Horizontal">
                        <AutoSize Enabled="True" />
				        <Template1>
							<px:PXGrid ID="gridPackages" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 0px; height: 372px;" Width="100%" SkinID="Details" BorderWidth="0px" SyncPosition="True">
								<Levels>
									<px:PXGridLevel DataMember="Packages">
										<Columns>
											<px:PXGridColumn DataField="IsCurrent" TextAlign="Center" Type="CheckBox" CommitChanges="True" />
											<px:PXGridColumn DataField="BoxID" DisplayFormat="&gt;aaaaaaaaaaaaaaa" Label="Box ID" Width="117px" />
											<px:PXGridColumn AutoGenerateOption="NotSet" DataField="Description" MaxLength="30" Width="200px" />
											<px:PXGridColumn AllowNull="False" DataField="Weight" TextAlign="Right" Width="91px" />
											<px:PXGridColumn AllowNull="False" DataField="WeightUOM" Width="91px" />
											<px:PXGridColumn AllowNull="False" DataField="DeclaredValue" TextAlign="Right" Width="91px" />
											<px:PXGridColumn AllowNull="False" DataField="COD" Label="C.O.D. Amount" TextAlign="Right" Width="91px" />
										</Columns>
									</px:PXGridLevel>
								</Levels>
								<AutoCallBack Target="gridPackageDetailSplit" Command="Refresh" />
								<AutoSize Enabled="True" MinHeight="150" />
							</px:PXGrid>
						</Template1>
				        <Template2>
			                <px:PXGrid ID="gridPackageDetailSplit" runat="server" DataSourceID="ds" Style="z-index: 100; left: 0px; top: 0px; height: 372px;" Width="100%" SkinID="Inquire" BorderWidth="0px" Caption="Contents of Current Package">
				                <Levels>
			                        <px:PXGridLevel DataMember="PackageSplits">
					                    <Columns>
                                            <px:PXGridColumn DataField="LineNbr" Width="100" />
				                            <px:PXGridColumn DataField="InventoryID" Width="200px" />
			                                <px:PXGridColumn DataField="SubItemID" Width="120" />
				                            <px:PXGridColumn DataField="UOM" Width="70" />
				                            <px:PXGridColumn DataField="Qty" Width="100" />
						                </Columns>
						            </px:PXGridLevel>
						        </Levels>                
						        <AutoSize Enabled="True" />
						        <Mode InitNewRow="True" />
				                <Parameters>
				                    <px:PXSyncGridParam ControlID="gridPackages" />
						        </Parameters>
						    </px:PXGrid>
					    </Template2>
					</px:PXSplitContainer>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Scan Log">
                <Template>
                    <px:PXGrid ID="gridLog" runat="server" DataSourceID="ds" Style="height: 250px;" Width="100%" OnRowDataBound="grid_RowDataBound" SkinID="Inquire" Height="372px">
						<Levels>
							<px:PXGridLevel DataMember="ScanLogs">
								<Columns>
									<px:PXGridColumn DataField="LogLineDate" Width="160px" />
									<px:PXGridColumn DataField="LogBarcode" Width="200px" />
									<px:PXGridColumn DataField="LogMessage" Width="400px" />
								</Columns>
							</px:PXGridLevel>
						</Levels>                
						<AutoSize Enabled="True" />
					</px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Enabled="True" Container="Window" />
    </px:PXTab>
    <%-- Settings --%>
    <px:PXSmartPanel ID="PanelSettings" runat="server" Height="300px" Width="400px" Caption="Settings" CaptionVisible="True"
        Key="UserSetup" AutoCallBack-Command="Refresh" AutoCallBack-Enabled="True" AutoCallBack-Target="frmSettings">
        <px:PXFormView ID="frmSettings" runat="server" DataSourceID="ds" DataMember="UserSetup" SkinID="Transparent">
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule7" runat="server" LabelsWidth="M" ControlSize="M" StartGroup="True" SuppressLabel="True" GroupCaption="General"/>
				<px:PXGroupBox ID="gbPromptType" runat="server" Caption="General" CommitChanges="True"
							   DataField="PromptChoice" RenderSimple="True" RenderStyle="Simple" Height="101">
					<Template>
						<px:PXRadioButton ID="rbLotSerial" runat="server" GroupName="gbPromptType"
							Text="Auto-select LotSerial/Number and Location" Value="S" />
						<px:PXRadioButton ID="rbLocation" runat="server" GroupName="gbPromptType"
							Text="Prompt for LotSerial/Number and Location" Value="L" />
						<px:PXRadioButton ID="rbMonthly" runat="server" GroupName="gbPromptType"
							Text="Prompt for LotSerial/Number" Value="N" />
					</Template>
					<ContentLayout LabelsWidth="S" Layout="Stack" />
				</px:PXGroupBox>

                <px:PXLayoutRule ID="PXLayoutRule1" runat="server" LabelsWidth="M" ControlSize="M" StartGroup="True" SuppressLabel="True" GroupCaption="Print Carrier Label"/>
                <px:PXCheckBox ID="edCarrierLabelPrintWithDeviceHub" runat="server" DataField="CarrierLabelPrintWithDeviceHub" CommitChanges="true" />
                <px:PXCheckBox ID="edCarrierLabelDefinePrinterManually" runat="server" DataField="CarrierLabelDefinePrinterManually" CommitChanges="true" />
                <px:PXLayoutRule ID="PXLayoutRule5" runat="server" LabelsWidth="M" ControlSize="M" SuppressLabel="False"/>
                <px:PXSelector ID="edCarrierLabelPrinterName" runat="server" DataField="CarrierLabelPrinterName" CommitChanges="true" AutoComplete="false" />
                
                <px:PXLayoutRule ID="PXLayoutRule2" runat="server" LabelsWidth="M" ControlSize="M" StartGroup="True" SuppressLabel="True" GroupCaption="Print Shipment Confirmation"/>
                <px:PXCheckBox ID="edShipmentConfirmationPrintWithDeviceHub" runat="server" DataField="ShipmentConfirmationPrintWithDeviceHub" CommitChanges="true" />
                <px:PXCheckBox ID="edShipmentConfirmationDefinePrinterManually" runat="server" DataField="ShipmentConfirmationDefinePrinterManually" CommitChanges="true" />
                <px:PXLayoutRule ID="PXLayoutRule3" runat="server" LabelsWidth="M" ControlSize="M" SuppressLabel="False"/>
                <px:PXSelector ID="edShipmentConfirmationPrinterName" runat="server" DataField="ShipmentConfirmationPrinterName" CommitChanges="true" AutoComplete="false" />                

                <px:PXLayoutRule ID="PXLayoutRule4" runat="server" LabelsWidth="M" ControlSize="M" StartGroup="True" SuppressLabel="True" GroupCaption="Scale"/>
                <px:PXCheckBox ID="PXCheckBox1" runat="server" DataField="UseScale" CommitChanges="true" />
                <px:PXLayoutRule ID="PXLayoutRule6" runat="server" LabelsWidth="M" ControlSize="M" SuppressLabel="False"/>
                <px:PXSelector ID="PXSelector1" runat="server" DataField="ScaleID" CommitChanges="true" AutoComplete="false" />
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel2" runat="server" SkinID="Buttons">
            <px:PXButton ID="pbClose" runat="server" DialogResult="OK" Text="Close"/>
        </px:PXPanel>
    </px:PXSmartPanel>
    <%-- Bin/Lot/Serial Numbers --%>
    <px:PXSmartPanel ID="PanelLS" runat="server" Width="764px" Caption="Allocations" CaptionVisible="True"
        Key="Splits" AutoCallBack-Command="Refresh" AutoCallBack-Enabled="True" AutoCallBack-Target="grid2">
        <px:PXGrid ID="grid2" runat="server" Width="100%" AutoAdjustColumns="True" DataSourceID="ds" Style="border-width: 1px 0px; left: 0px; top: 0px; height: 192px;" SyncPosition="true">
            <AutoSize Enabled="true" />
            <Mode InitNewRow="True" />
            <Parameters>
                <px:PXSyncGridParam ControlID="grid" />
            </Parameters>
            <Levels>
                <px:PXGridLevel DataMember="splits">
                    <Columns>
                        <px:PXGridColumn DataField="InventoryID" Width="108px" />
                        <px:PXGridColumn DataField="SubItemID" Width="108px" />
                        <px:PXGridColumn DataField="LocationID" AllowShowHide="Server" Width="108px" CommitChanges="true" />
                        <px:PXGridColumn DataField="LotSerialNbr" AllowShowHide="Server" Width="108px" CommitChanges="true" />
                        <px:PXGridColumn DataField="Qty" Width="108px" TextAlign="Right" />
                        <px:PXGridColumn DataField="UOM" Width="108px" />
                        <px:PXGridColumn DataField="ExpireDate" AllowShowHide="Server" Width="90px" />
                        <px:PXGridColumn AllowUpdate="False" DataField="InventoryID_InventoryItem_descr" Width="108px" />
                    </Columns>
                    <RowTemplate>
                        <px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="M" ControlSize="XM" />
                        <px:PXSegmentMask ID="edSubItemID2" runat="server" DataField="SubItemID" AutoRefresh="true" />
                        <px:PXSegmentMask ID="edLocationID2" runat="server" DataField="LocationID" AutoRefresh="true">
                            <Parameters>
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.siteID" PropertyName="DataValues[&quot;SiteID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                            </Parameters>
                        </px:PXSegmentMask>
                        <px:PXNumberEdit ID="edQty2" runat="server" DataField="Qty" />
                        <px:PXSelector ID="edUOM2" runat="server" DataField="UOM" AutoRefresh="true">
                            <Parameters>
                                <px:PXControlParam ControlID="grid" Name="SOShipLine.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                            </Parameters>
                        </px:PXSelector>
                        <px:PXSelector ID="edLotSerialNbr2" runat="server" DataField="LotSerialNbr" AutoRefresh="true">
                            <Parameters>
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.inventoryID" PropertyName="DataValues[&quot;InventoryID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.subItemID" PropertyName="DataValues[&quot;SubItemID&quot;]" Type="String" />
                                <px:PXControlParam ControlID="grid2" Name="SOShipLineSplit.locationID" PropertyName="DataValues[&quot;LocationID&quot;]" Type="String" />
                            </Parameters>
                        </px:PXSelector>
                        <px:PXDateTimeEdit ID="edExpireDate2" runat="server" DataField="ExpireDate" />
                    </RowTemplate>
                    <Layout ColumnsMenu="False" />
                </px:PXGridLevel>
            </Levels>
        </px:PXGrid>
        <px:PXPanel ID="PXPanel1" runat="server" SkinID="Buttons">
            <px:PXButton ID="btnSave" runat="server" DialogResult="OK" Text="OK" />
        </px:PXPanel>
    </px:PXSmartPanel>
    <%-- Refresh Confirmation --%>
    <px:PXSmartPanel ID="PanelRefreshConfirmation" runat="server" Height="120px" Width="400px" Caption="Refresh Confirmation" CaptionVisible="True"
				     style="width: 400px; position: absolute; top: 132px; left: 126px; z-index: 5000;">
		<px:PXFormView ID="formRefreshConfirmation" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" CaptionVisible="False">
			<ContentStyle BackColor="Transparent" BorderStyle="None" ></ContentStyle>
            <Template>
                <px:PXLayoutRule ID="PXLayoutRule13435" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
				<px:PXLabel ID="edTechnicianID" runat="server" Text="Are you sure you want to refresh the page?" style="font-size:16px" />
            </Template>
        </px:PXFormView>
        <px:PXPanel ID="PXPanel10" runat="server" SkinID="Buttons">
            <px:PXButton ID="edRefreshConfirmationYesButton" runat="server" DialogResult="Yes" Text="Yes">
				<ClientEvents Click="RefreshWebPage_Click" />
			</px:PXButton>
            <px:PXButton ID="edRefreshConfirmationCancelButton" runat="server" DialogResult="Cancel" Text="No" />
        </px:PXPanel>
    </px:PXSmartPanel>
</asp:content>
