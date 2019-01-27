<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="US100000.aspx.cs" Inherits="Page_US100000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" 
		PrimaryView="Setup" TypeName="USScouts.USSetupMaint" SuspendUnloading="False">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" />			
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" 
		Width="100%" DataMember="Setup" 
		EmailingGraph="" Caption="General Settings" TemplateContainer="" TabIndex="500">
		<Template>
		    <px:PXLayoutRule runat="server" LabelsWidth="SM" ControlSize="M"  />            		               		    
		    <px:PXCheckBox ID="edEnableInAP" runat="server" AlreadyLocalized="False" DataField="EnableInAP" Text="Enable in AP">
            </px:PXCheckBox>
		    <px:PXSelector ID="edSimpleNumberingID" runat="server" CommitChanges="True" DataField="SimpleNumberingID">
            </px:PXSelector>
		    <px:PXSegmentMask ID="edVendorID" runat="server" DataField="VendorID">
            </px:PXSegmentMask>
		</Template>
	    <AutoSize Container="Window" Enabled="true"/>
	</px:PXFormView>
</asp:Content>
