<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="True" ValidateRequest="False" CodeFile="US303000.aspx.cs" Inherits="Page_US303000" Title="Fund Maintenance" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Scout" TypeName="USScouts.USScoutMaint" Height="36px">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" Height="150px" DataMember="Scout" NoteIndicator="True" FilesIndicator="True" LinkIndicator="True" NotifyIndicator="True" SyncPosition="True" TabIndex="700">
		<Template>
            <px:PXLayoutRule runat="server"></px:PXLayoutRule>
            <px:PXSelector runat="server" DataField="ScoutCD" ID="edScoutCD" CommitChanges="true"></px:PXSelector>
            <px:PXTextEdit runat="server" DataField="Description" DefaultLocale="" AlreadyLocalized="False" ID="edDescription"></px:PXTextEdit>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" Width="100%" Height="100%" DataSourceID="ds" DataMember="CurrentScout" SyncPosition="True">
		<Items>
			<px:PXTabItem Text="General Info">
				<Template>
				    <px:PXLayoutRule runat="server">
                    </px:PXLayoutRule>
				</Template>
			</px:PXTabItem>
		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150"/>
	</px:PXTab>
</asp:Content>