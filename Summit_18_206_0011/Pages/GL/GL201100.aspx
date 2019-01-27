<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true"
	ValidateRequest="false" CodeFile="GL201100.aspx.cs" Inherits="Page_GL201100"
	Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>
<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
	<px:PXDataSource ID="ds" runat="server" AutoCallBack="True" Visible="True" Width="100%"
		TypeName="PX.Objects.GL.OrganizationFinPeriodMaint" PrimaryView="OrgFinYear" PageLoadBehavior="SearchSavedKeys">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="Save" Visible="false" />
			<px:PXDSCallbackCommand Name="First" PostData="Self" StartNewGroup="True" />
			<px:PXDSCallbackCommand Name="CopyPaste" Visible="False" />
			<px:PXDSCallbackCommand Name="Insert" Visible="false" />
			<px:PXDSCallbackCommand Name="Delete" Visible="False" />
		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
	<px:PXFormView ID="form" runat="server" Width="100%"
		DataMember="OrgFinYear" NoteIndicator="True" FilesIndicator="True"
		ActivityIndicator="True" ActivityField="NoteActivity">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartColumn="True" LabelsWidth="SM" ControlSize="SM" />
			<px:PXSegmentMask ID="edOrganizationID" runat="server" DataField="OrganizationID">
				<AutoCallBack Command="Cancel" Target="ds"/>
			</px:PXSegmentMask>
			<px:PXSelector ID="edYear" runat="server" DataField="Year" AutoRefresh = "true">
				<AutoCallBack Command="Cancel" Target="ds"/>
			</px:PXSelector>
			<px:PXDateTimeEdit ID="edStartDate" runat="server" DataField="StartDate" Enabled="False" />
			<px:PXNumberEdit ID="edFinPeriods" runat="server" DataField="FinPeriods" Enabled="False" />
		</Template>
	</px:PXFormView>

</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXGrid ID="grid" runat="server" Height="150px"
		Width="100%" Caption="Financial Year Periods" SkinID="Inquire">
		<Levels>
			<px:PXGridLevel DataMember="OrgFinPeriods">
				<Columns>
					<px:PXGridColumn DataField="FinPeriodID" Width="80px" />
					<px:PXGridColumn DataField="StartDateUI" Width="90px" />
					<px:PXGridColumn DataField="EndDateUI" Width="90px" AutoCallBack="true" />
					<px:PXGridColumn DataField="Descr" Width="200px" />
					<px:PXGridColumn DataField="Status" />
					<px:PXGridColumn DataField="APClosed" TextAlign="Center" Type="CheckBox" Width="70px" />
					<px:PXGridColumn DataField="ARClosed" TextAlign="Center" Type="CheckBox" Width="70px" />
					<px:PXGridColumn DataField="INClosed" TextAlign="Center" Type="CheckBox" Width="70px" />
					<px:PXGridColumn DataField="CAClosed" TextAlign="Center" Type="CheckBox" Width="70px" />
					<px:PXGridColumn DataField="FAClosed" TextAlign="Center" Type="CheckBox" Width="70px" />
				</Columns>
				<Layout FormViewHeight=""></Layout>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" />
	</px:PXGrid>
</asp:Content>
