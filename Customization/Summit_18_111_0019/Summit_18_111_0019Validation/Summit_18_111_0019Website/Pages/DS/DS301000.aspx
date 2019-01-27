<%@ page language="C#" masterpagefile="~/MasterPages/FormDetail.master" autoeventwireup="true" validaterequest="false" codefile="DS301000.aspx.cs" inherits="Page_DS301000" title="Untitled Page" %>

<%@ mastertype virtualpath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="true" TypeName="DSScouts.DSInvoiceEntry" PrimaryView="Document">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Document" Caption="Selection" TabIndex="13900">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
		    <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr" CommitChanges="True">
            </px:PXSelector>
            <px:PXSelector ID="edScoutLeaderID" runat="server" DataField="ScoutLeaderID">
            </px:PXSelector>
            <px:PXDateTimeEdit ID="edDate" runat="server" AlreadyLocalized="False" DataField="Date" DefaultLocale="">
            </px:PXDateTimeEdit>
            <px:PXTextEdit ID="edDescription" runat="server" AlreadyLocalized="False" DataField="Description" DefaultLocale="">
            </px:PXTextEdit>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="153px" Style="z-index: 100" Width="100%" Caption="Details" AllowSearch="True" AllowPaging="True" AdjustPageSize="Auto" SkinID="Details"
		TabIndex="9700" RestrictFields="True" SyncPosition="True">
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
			<px:PXGridLevel DataMember="Details">
				<RowTemplate>

				    <px:PXSegmentMask ID="edInventoryID" runat="server" DataField="InventoryID">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edInventoryID_description" runat="server" AlreadyLocalized="False" DataField="InventoryID_description" DefaultLocale="">
                    </px:PXTextEdit>
                    <px:PXNumberEdit ID="edQty" runat="server" AlreadyLocalized="False" DataField="Qty" DefaultLocale="">
                    </px:PXNumberEdit>

				</RowTemplate>
				<Columns>

				    <px:PXGridColumn DataField="InventoryID">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="Qty" TextAlign="Right" Width="100px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="InventoryID_description" Width="200px">
                    </px:PXGridColumn>

				</Columns>

				<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>

