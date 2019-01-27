<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="US501000.aspx.cs" Inherits="Page_US501000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" Width="100%" runat="server" Visible="True" PrimaryView="Documents" TypeName="USScouts.USSimpleInvoiceRelease" />
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="400px" Width="100%" Style="z-index: 100" AllowPaging="True" AllowSearch="True" Caption="Documents" DataSourceID="ds" BatchUpdate="True" AdjustPageSize="Auto"
		SkinID="PrimaryInquire" SyncPosition="True" TabIndex="1000" >
<EmptyMsg ComboAddMessage="No records found.
Try to change filter or modify parameters above to see records here." NamedComboMessage="No records found as &#39;{0}&#39;.
Try to change filter or modify parameters above to see records here." NamedComboAddMessage="No records found as &#39;{0}&#39;.
Try to change filter or modify parameters above to see records here." FilteredMessage="No records found.
Try to change filter to see records here." FilteredAddMessage="No records found.
Try to change filter to see records here." NamedFilteredMessage="No records found as &#39;{0}&#39;.
Try to change filter to see records here." NamedFilteredAddMessage="No records found as &#39;{0}&#39;.
Try to change filter to see records here." AnonFilteredMessage="No records found.
Try to change filter to see records here." AnonFilteredAddMessage="No records found.
Try to change filter to see records here."></EmptyMsg>
		<Levels>
			<px:PXGridLevel DataMember="Documents">
				<RowTemplate>
                    <px:PXLayoutRule runat="server">
                    </px:PXLayoutRule>
                    <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edDescription" runat="server" AlreadyLocalized="False" DataField="Description" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXDateTimeEdit ID="edDate" runat="server" AlreadyLocalized="False" DataField="Date" DefaultLocale="">
                    </px:PXDateTimeEdit>
                    <px:PXSelector ID="edScoutID" runat="server" DataField="ScoutID">
                    </px:PXSelector>
                    <px:PXTextEdit ID="edScoutID_USScout_description" runat="server" AlreadyLocalized="False" DataField="ScoutID_USScout_description" DefaultLocale="">
                    </px:PXTextEdit>
                </RowTemplate>
				<Columns>

				    <px:PXGridColumn DataField="RefNbr" Width="140px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Description" Width="280px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Date" Width="90px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ScoutID" Width="140px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="ScoutID_USScout_description" Width="280px">
                    </px:PXGridColumn>

				</Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="400" />
		<ActionBar DefaultAction="viewDocument" />
	</px:PXGrid>
</asp:Content>
