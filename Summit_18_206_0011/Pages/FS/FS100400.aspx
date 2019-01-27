<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FS100400.aspx.cs" Inherits="Page_FS100400" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
        PrimaryView="RouteSetupRecord" SuspendUnloading="False" 
        TypeName="PX.Objects.FS.RouteSetupMaint">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" ></px:PXDSCallbackCommand>
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="edRouteSetup" runat="server" DataMember="RouteSetupRecord" DataSourceID="ds" Width="100%" DefaultControlID="edRouteNumberingID">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" LabelsWidth="M" ControlSize="M">
			</px:PXLayoutRule>
			<px:PXLayoutRule runat="server" GroupCaption="Numbering Settings" >
			</px:PXLayoutRule>   
			<px:PXSelector ID="edRouteNumberingID" runat="server" 
				DataField="RouteNumberingID" AllowEdit = "True" >
			</px:PXSelector>  
			<px:PXLayoutRule runat="server" GroupCaption="Contract Settings" >
			</px:PXLayoutRule>
			<px:PXCheckBox ID="edEnableSeasonScheduleContractRoute" runat="server" AlignLeft="True" DataField="EnableSeasonScheduleContract">
			</px:PXCheckBox>
			<px:PXLayoutRule runat="server" GroupCaption="Route Settings">
			</px:PXLayoutRule>   
			<px:PXSelector ID="edDfltSrvOrdType" runat="server" 
				DataField="DfltSrvOrdType" >                        
			</px:PXSelector>                      
			<px:PXCheckBox ID="edAutoCalculateRouteStats" runat="server" DataField="AutoCalculateRouteStats" AlignLeft="True">
			</px:PXCheckBox>
			<px:PXCheckBox ID="edGroupINDocumentsByPostingProcess" runat="server" DataField="GroupINDocumentsByPostingProcess" Text="Group IN documents by Posting process" AlignLeft="True" CommitChanges="True">
			</px:PXCheckBox> 
			<px:PXCheckBox ID="edSetFirstManualAppointment" runat="server" DataField="SetFirstManualAppointment" AlignLeft="True">
			</px:PXCheckBox> 
			<px:PXCheckBox ID="edTrackRouteLocation" runat="server" DataField="TrackRouteLocation" AlignLeft="True">
			</px:PXCheckBox>
			<px:PXLayoutRule 
				runat="server" 
				StartColumn="True" 
				GroupCaption="Invoice Generation Settings">
			</px:PXLayoutRule>
			<%-- Posting Settings Fields--%>
			<px:PXGroupBox 
				ID="gbPostTo" 
				runat="server" 
				Caption="Generate Invoices In" 
				DataField="SetupRecord.ContractPostTo" 
				CommitChanges="True">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" ControlSize="SM">
					</px:PXLayoutRule>
					<px:PXRadioButton ID="gbPostTo_op0" runat="server" GroupName="gbPostTo" Text="Accounts Receivable" Value="AR" />
					<px:PXRadioButton ID="gbPostTo_op1" runat="server" GroupName="gbPostTo" Text="Sales Order" Value="SO" />
				</Template>
				<ContentLayout Layout="Stack"></ContentLayout>
			</px:PXGroupBox>
			<px:PXSelector ID="edContractPostOrderType" runat="server" AllowEdit="True" AutoRefresh="True" CommitChanges="True" DataField="SetupRecord.ContractPostOrderType" DataSourceID="ds" LabelWidth="170px" Size="XM" Width="200px">
			</px:PXSelector>
			<px:PXSelector ID="edDfltTermIDARSO" runat="server" AllowEdit="True" AutoRefresh="True" DataField="SetupRecord.DfltContractTermIDARSO" DataSourceID="ds"
			LabelWidth="170px" Size="XM" Width="200px">
			</px:PXSelector>
			<px:PXDropDown ID="edSalesAcctSource" runat="server" CommitChanges="True" DataField="SetupRecord.ContractSalesAcctSource" LabelWidth="170px"
			Size="XM" Width="200px">
			</px:PXDropDown>
			<px:PXSegmentMask ID="edCombineSubFrom" runat="server" CommitChanges="True" DataField="SetupRecord.ContractCombineSubFrom" LabelWidth="170px"
			Size="XM" Width="200px">
			</px:PXSegmentMask>
			<px:PXCheckBox ID="edEnableContractPeriodWhenInvoice" runat="server" DataField="SetupRecord.EnableContractPeriodWhenInvoice" AlignLeft="True">
			</px:PXCheckBox>
			 <%-- Posting Settingss Fields--%>
		</Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" />
	</px:PXFormView>
</asp:Content>
