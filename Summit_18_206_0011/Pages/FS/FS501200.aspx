<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" 
    ValidateRequest="false" CodeFile="FS501200.aspx.cs" Title="Untitled Page" Inherits="Page_FS501200" %>
    <%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

        <asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
            <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" PrimaryView="Filter" TypeName="PX.Objects.FS.ProcessServiceContracts">
            </px:PXDataSource>
        </asp:Content>
        <asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
            <px:PXFormView ID="form" runat="server" DataSourceID="ds" 
                Style="z-index: 100" Width="100%" DataMember="Filter" TabIndex="700" 
                DefaultControlID="">
                <Template>
                    <px:PXLayoutRule runat="server" StartRow="True" ControlSize="SM" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXDropDown CommitChanges="True" ID="edActionType" runat="server" DataField="ActionType" />
                    <px:PXSelector ID="edBranchID" runat="server" CommitChanges="True" 
                        DataField="BranchID" AutoRefresh="true">
                    </px:PXSelector>
                    <px:PXSelector ID="edBranchLocationID" runat="server" CommitChanges="True" 
                        DataField="BranchLocationID" AutoRefresh="true">
                    </px:PXSelector>
                    <px:PXLayoutRule runat="server" ControlSize="SM" StartColumn="True">
                    </px:PXLayoutRule>
                    <px:PXSegmentMask ID="edCustomerID" runat="server" CommitChanges="True" DataField="CustomerID" AutoRefresh="true"/>
                    <px:PXSegmentMask ID="edCustomerLocationID" runat="server" CommitChanges="True" 
                        DataField="CustomerLocationID" AutoRefresh="true">
                    </px:PXSegmentMask>
                    <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr" CommitChanges="True" AutoRefresh="true" />
                    
                </Template>
            </px:PXFormView>
        </asp:Content>
        <asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
            <px:PXGrid ID="gridServiceContracts" runat="server" AllowPaging="True" DataSourceID="ds" 
                Style="z-index: 100" Width="100%"
                SkinID="PrimaryInquire" TabIndex="500" SyncPosition="True" BatchUpdate="True">
                <Levels>
                    <px:PXGridLevel DataMember="ServiceContracts">
                        <RowTemplate>
                        <px:PXCheckBox ID="edSelected" runat="server" DataField="Selected" Text="Selected"/>
                        <px:PXSelector ID="edBranchID" runat="server" DataField="BranchID"/>
                        <px:PXSelector ID="edBranchLocationID" runat="server" DataField="BranchLocationID"/>
                        <px:PXSelector ID="edCustomerID" runat="server" DataField="CustomerID"/>
                        <px:PXSelector ID="edCustomerLocationID" runat="server" DataField="CustomerLocationID"/>
                        <px:PXSelector ID="edRefNbr" runat="server" DataField="RefNbr" AllowEdit="True"/>
                        <px:PXTextEdit ID="edDocDesc" runat="server" DataField="DocDesc"/>
                        <px:PXDateTimeEdit ID="edStartDate" runat="server" DataField="StartDate"/>
                        <px:PXDateTimeEdit ID="edEndDate" runat="server" DataField="EndDate"/>
                        <px:PXTextEdit ID="edUpcomingStatus" runat="server" DataField="UpcomingStatus"/>
                        <px:PXDateTimeEdit ID="edStatusEffectiveUntilDate" runat="server" DataField="StatusEffectiveUntilDate"/>
                        </RowTemplate>
                        <Columns>
                            <px:PXGridColumn AllowCheckAll="True" DataField="Selected" TextAlign="Center" Type="CheckBox" 
                                Width="80px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="BranchID" Width="130px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="BranchLocationID" Width="130px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="CustomerID" Width="150px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="CustomerLocationID" Width="130px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="RefNbr" Width="120px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="DocDesc" Width="200px"/>
                            <px:PXGridColumn DataField="StartDate" Width="110px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="EndDate" Width="110px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="UpcomingStatus" Width="110px">
                            </px:PXGridColumn>
                            <px:PXGridColumn DataField="StatusEffectiveUntilDate" Width="110px">
                            </px:PXGridColumn>
                        </Columns>
                    </px:PXGridLevel>
                </Levels>
                <AutoSize Container="Window" Enabled="True" MinHeight="150" />
            </px:PXGrid>
        </asp:Content>