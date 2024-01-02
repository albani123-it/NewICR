<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="monitoring.aspx.vb" MasterPageFile="~/Site.Master" Inherits="NewCRM.monitoring" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
      
        function fBack() {
            window.location.href = "underwriting/underwriting.aspx";
        }
    </script>
    <style type="text/css">
        .full-container
        {
            width: 98%;
            margin: 0 10px;
        }
    </style>
    <div style="margin:auto;width:98%;">
        <input type="button" value="Back" style="margin:10px;padding:3px 10px;" onclick="fBack();" />
        <div class="full-container">
            <fieldset style="margin-top:10px;border-radius:5px;background:#ebecec;">
                <legend style="font-size:14px;font-weight:bold;">Flow Process</legend>
                <obout:Grid id="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="15"
			        AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
                    AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientAdd="OnBeforeAdd" />
                    <Columns>
                        <obout:Column ID="Column1" DataField="app_id" Visible="false" runat="server"/>
                        <obout:Column ID="Column2" DataField="app_no" HeaderText="Nomor Aplikasi" runat="server" ShowFilterCriterias="false" Width="12%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtAppNo" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppNoFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column3" DataField="process" HeaderText="Process" runat="server" ShowFilterCriterias="false" Width="25%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtProcess" RowEditTemplateControlPropertyName="value" FilterTemplateId="ProcessFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column6" DataField="next_process" HeaderText="Next Process" runat="server" ShowFilterCriterias="false" Width="25%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtNextProcess" RowEditTemplateControlPropertyName="value" FilterTemplateId="NextProcessFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column4" DataField="usr_before" HeaderText="User" runat="server" ShowFilterCriterias="false" Width="20%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtUsr" RowEditTemplateControlPropertyName="value" FilterTemplateId="UsrFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column5" DataField="usr_submit" HeaderText="Submit To" runat="server" ShowFilterCriterias="false" Width="20%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtSubmitTo" RowEditTemplateControlPropertyName="value" FilterTemplateId="SubmitToFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column14" DataField="lwu_date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" runat="server" ShowFilterCriterias="false" Width="15%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtDate" RowEditTemplateControlPropertyName="value" FilterTemplateId="DateFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                    </Columns>
                    <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                    <Templates>
                        <obout:GridTemplate runat="server" ID="tplRemoveFilter">
                            <Template>
                                <a onclick="grid1.removeFilter()" class="ob_gALF" style="cursor:pointer">Clear Filter</a>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="AppNoFilter" ControlID="AppNo">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="AppNo" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="ProcessFilter" ControlID="Process">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="Process" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="NextProcessFilter" ControlID="NextProcess">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="NextProcess" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="UsrFilter" ControlID="Usr">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="Usr" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="SubmitToFilter" ControlID="SubmitTo">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="SubmitTo" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="DateFilter" ControlID="DateF">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="DateF" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                    <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter"/>
                </obout:Grid>
            </fieldset>
        </div>
        <div class="full-container">
            <fieldset style="margin-top:10px;border-radius:5px;background:#ebecec;">
                <legend style="font-size:14px;font-weight:bold;">Logs</legend>
                <obout:Grid id="grid2" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="false" PageSize="-1"
			            AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
                        AllowPageSizeSelection="false" AllowFiltering="false" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                        <Columns>
                            <obout:Column ID="Column7" DataField="id" Visible="false" runat="server" />
                            <obout:Column ID="Column8" DataField="code" Visible="true" runat="server" />
                            <obout:Column ID="Column9" DataField="desc" HeaderText="Page" runat="server" Width="20%" Wrap="true" />
                            <obout:Column ID="Column10" DataField="update_by" HeaderText="Update By" runat="server" Width="20%" Wrap="true" />
                            <obout:Column ID="Column11" DataField="update_date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Update Date" runat="server" Width="20%" Wrap="true" />
                            <obout:Column ID="Column12" DataField="status" HeaderText="Status" runat="server" Width="15%" Wrap="true" />
                            <obout:Column id="Column13" runat="server" Width="10%" HeaderText="Action">
                                <TemplateSettings TemplateID = "viewTemplate" />
                            </obout:Column>
                        </Columns>
                        <Templates>
                            <obout:GridTemplate runat="server" ID="viewTemplate" >
                                <Template>
                                    <asp:LinkButton ID="zzz" OnCommand="export" runat="server" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Export  </asp:LinkButton>
                                </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
        /* -- */
        var applyFilterTimeout = null;

        function applyFilter() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeout);
            }
            applyFilterTimeout = window.setTimeout(doFiltering, 500);
        }

        function doFiltering() {
            grid1.filter();
        }

        /* -- */

        function OnBeforeDelete(record) {
            if (confirm("Apakah Anda yakin ingin menghapus?") == false) {
                return false;
            }
            return true;
        }
        function OnDelete(record) {
            alert("Data berhasil dihapus.");
        }
        function OnBeforeAdd(record) {
            detail("-1", "-1", "");
            return false;
        }
    </script>
</asp:Content>