<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_picklist.aspx.vb" MasterPageFile="~/Site.Master" Inherits="NewCRM.glo_picklist" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Parameter');
        aBc('Dropdownlist');

        function reloadOboutGrid(cat) {
            if (cat == "parent") {
                grid1.refresh();
            }
            if (cat == "detail") {
                grid2.refresh();
            }
        }
        
        function fBack() {
            window.location.href = "GlobalSetting.aspx";
        }
    </script>
    <style type="text/css">
        .left-part
        {
            width:auto;
            width: 40%;
            float: left;
            position: relative;
            
        }
        .right-part
        {
            width:auto;
            max-width: 60%;
            float: left;
            position: relative;
        }
        .part-container
        {
            width:98%;
            margin: 0 10px;
        }
        .ob_gCS, .ob_gCS div, .ob_gCS_F, .ob_gCS_F div 
        {
            display: none;
        }
        .ob_gFEC
        {
            position:relative;
        }
        
    </style>
    <div style="margin:auto;width:98%;">
        <div style="margin:10px 0px 20px 0px;">
            <input type="button" value="Back" style="margin:10px;padding:3px 10px;" onclick="fBack();" />
        </div>
        <%--<div style="margin-bottom:20px;height:auto;float:left;width:100%;">--%>
        <div class="left-part">
            <div class="part-container">
                <obout:Grid id="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="15"
			        AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
                    AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                    <Columns>
                        <obout:Column ID="Column1" DataField="ldr_id" Visible="false" runat="server"/>
                        <obout:Column ID="Column2" DataField="ldr_object" HeaderText="Object" runat="server" ShowFilterCriterias="false" Width="30%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtObject" RowEditTemplateControlPropertyName="value" FilterTemplateId="OjbectFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column3" DataField="ldr_name" HeaderText="Description" runat="server" ShowFilterCriterias="false" Width="30%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtName" RowEditTemplateControlPropertyName="value" FilterTemplateId="NameFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column6" HeaderText="Action" runat="server" Width="12%">
                            <TemplateSettings TemplateID="viewBtnTemplate" />
                        </obout:Column>
                    </Columns>
                    <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                    <Templates>
                        <obout:GridTemplate runat="server" ID="viewBtnTemplate">
                            <Template>
                                <%--<a class="ob_gALF" href="javascript:fDetail('<%# Container.DataItem("ldr_object") %>');">Detail</a>--%>
                                <asp:LinkButton ID="zzz" oncommand="detailDdl" runat="server" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Detail  </asp:LinkButton>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="OjbectFilter" ControlID="OjbectF">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="OjbectF" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="NameFilter" ControlID="NameF">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="NameF" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="tplRemoveFilter">
                            <Template>
                                <a onclick="grid1.removeFilter()" class="ob_gALF" style="cursor:pointer">Clear Filter</a>
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                    <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter"/>
                </obout:Grid>
            </div>
        </div>
        
        <div class="right-part">
            <div class="part-container">
                <obout:Grid id="grid2" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="15"
			        AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="true"
                    AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" 
                    OnRebind="RebindGrid" OnUpdateCommand="UpdateRecord" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord"
                    FolderStyle="~/styles/grid">
                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete2" OnClientAdd="OnBeforeAdd2" OnClientEdit="OnBeforeEdit2" />
                    <Columns>
                        <obout:Column ID="Column4" DataField="ldd_id" ReadOnly="true" Visible="false" runat="server"/>
                        <obout:Column ID="Column9" DataField="ldd_object" runat="server" Visible="false" ReadOnly="true" />
                        <obout:Column ID="Column5" DataField="ldd_code" HeaderText="Code" runat="server" ShowFilterCriterias="false" Width="15%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtCode" RowEditTemplateControlPropertyName="value" FilterTemplateId="CodeFilter" EditTemplateId="tplEditCode" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column7" DataField="ldd_desc" HeaderText="Description" runat="server" ShowFilterCriterias="false" Width="35%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="DescFilter" EditTemplateId="tplEditDesc" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column ID="Column8" DataField="ldd_status" HeaderText="Status" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                            <TemplateSettings RowEditTemplateControlId="txtStats" RowEditTemplateControlPropertyName="value" FilterTemplateId="StatsFilter" EditTemplateId="tplEditStats" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column id="Column10" runat="server" HeaderText="Action" AllowDelete="true" Width="15%" AllowEdit="true"></obout:Column>
                        <%--<obout:Column ID="Column9" runat="server" Width="7%">
                            <TemplateSettings TemplateID="deleteTemplate" />
                        </obout:Column>--%>
                    </Columns>
                    <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                    <Templates>
                        <obout:GridTemplate runat="server" ID="deleteTemplate">
                            <Template>
                                <asp:LinkButton ID="zzz" runat="server" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="tplRemoveFilter2">
                            <Template>
                                <a onclick="grid2.removeFilter()" class="ob_gALF" style="cursor:pointer">Clear Filter</a>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="CodeFilter" ControlID="CodeF">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="CodeF" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter2" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="DescFilter" ControlID="DescF">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="DescF" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter2" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="StatsFilter" ControlID="StatsF">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="StatsF" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter2" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>

                        <obout:GridTemplate runat="server" ID="tplEditCode" ControlID="txtCode">
                            <Template>
                                <input type="text" id="txtCode" name="txtCode" class="inpTxt" style="width:90%;" />
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="tplEditDesc" ControlID="txtDesc">
                            <Template>
                                <input type="text" id="txtDesc" name="txtDesc" class="inpTxt" style="width:95%;" />
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="tplEditStats" ControlID="ddlStats">
                            <Template>
                                <select id="ddlStats" name="ddlStats" class="inpDdl">
                                    <option value="Active" selected="selected">Active</option>
                                    <option value="Inactive">Inactive</option>
                                </select>
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                    <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter2"/>
                </obout:Grid>
            </div>
        </div>
        <%--</div>--%>
    </div>
    <script>
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

        function applyFilter2() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeout);
            }
            applyFilterTimeout = window.setTimeout(doFiltering2, 500);
        }

        function doFiltering2() {
            grid2.filter();
        }

        /* -- */
        function OnBeforeDelete(record) {
            if (confirm("Apakah Anda yakin ingin menghapus?") == false) {
                return false;
            }
            return true;
        }

        function OnBeforeEdit(record) {
            fEdit(record.ldr_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "glo_picklist_par_det.aspx?id=" + (id);
            oWindow(l_obj_argument.url, 800, 400);
        }

        function OnBeforeDelete2(record) {
            if (confirm("Are you certain want to delete?") == false) {
                return false;
            }
            return true;
        }

        function OnBeforeEdit2(record) {
            var msg = "";
            if (record.ldd_code == "") {
                msg += "Please fill code.\n";
            }

            if (record.ldd_desc == "") {
                msg += "Please fill description.\n";
            }

            if (record.ldd_status == "") {
                msg += "Please select status.\n";
            }

            if (msg != "") {
                alert(msg);
                //return false;
            }
            else
                return true;
        }

        function OnBeforeAdd2(record) {
            var msg = "";
            if (record.ldd_code == "") {
                msg += "Please fill code.\n";
            }

            if (record.ldd_desc == "") {
                msg += "Please fill description.\n";
            }

            if (record.ldd_status == "") {
                msg += "Please select status.\n";
            }

            if (msg != "") {
                alert(msg);
                //return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>