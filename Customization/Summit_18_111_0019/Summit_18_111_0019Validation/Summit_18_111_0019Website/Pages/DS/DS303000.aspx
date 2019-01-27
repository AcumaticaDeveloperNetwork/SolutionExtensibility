<%@ page language="C#" masterpagefile="~/MasterPages/FormDetail.master" autoeventwireup="true" validaterequest="false" codefile="DS303000.aspx.cs" inherits="Page_DS303000" title="Untitled Page" %>

<%@ mastertype virtualpath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="true" TypeName="DSScouts.DSScoutLeaderMaint" PrimaryView="Leader">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Leader" Caption="Selection" DefaultControlID="edVendorID" TabIndex="13900">
		<Template>
			<px:PXLayoutRule runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="XM" />
		    <px:PXSelector ID="edLeaderCD" runat="server" DataField="LeaderCD" AutoRefresh="true">
            </px:PXSelector>
            <px:PXTextEdit ID="edDescription" runat="server" AlreadyLocalized="False" DataField="Description" DefaultLocale="">
            </px:PXTextEdit>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Height="153px" Style="z-index: 100" Width="100%" Caption="Subaccounts" AllowSearch="True" AllowPaging="True" AdjustPageSize="Auto" SkinID="Details"
		TabIndex="9700" RestrictFields="True" SyncPosition="True">
		<Levels>
			<px:PXGridLevel DataMember="Subs">
				<RowTemplate>
				    <px:PXSegmentMask ID="edSubID" runat="server" DataField="SubID">
                    </px:PXSegmentMask>
                    <px:PXTextEdit ID="edSubID_description" runat="server" AlreadyLocalized="False" DataField="SubID_description" DefaultLocale="">
                    </px:PXTextEdit>
				</RowTemplate>
				<Columns>
				    <px:PXGridColumn DataField="SubID" Width="120px">
                    </px:PXGridColumn>
                    <px:PXGridColumn DataField="SubID_description" Width="200px">
                    </px:PXGridColumn>
				</Columns>

				<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>

