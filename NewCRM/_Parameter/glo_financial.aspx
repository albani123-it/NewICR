<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="glo_financial.aspx.vb" Inherits="NewCRM.glo_financial" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Parameter');
        aBc('financial');
        /** **/

        /** **/
        function OnBeforeEdit(record) {
            fEdit(record.acc_code_en);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit("-1");
            return false;
        }
        function fBack() {
            window.location.href = "GlobalSetting.aspx";
        }
        function fEdit(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "glo_financial_det.aspx?c=" + (id);
            oWindow(l_obj_argument.url, 800, 550);
        }
    </script>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <div style="margin:10px 0px 10px 0px;">
            <input type="button" value="Back" style="padding:3px 10px;" onclick="fBack();" />
        </div>
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="15"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="acc_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="acc_code_en" Visible="false"></obout:Column>
                <obout:Column id="Column3" runat="server" DataField="acc_code" HeaderText="Code" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCode" RowEditTemplateControlPropertyName="value" FilterTemplateId="CodeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="acc_desc" HeaderText="Description" Width="40%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="DescFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" DataField="acc_input_str" HeaderText="Inputable" Width="10%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtInputable" RowEditTemplateControlPropertyName="value" FilterTemplateId="InputableFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column6" runat="server" DataField="acc_report_str" HeaderText="Report" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtReport" RowEditTemplateControlPropertyName="value" FilterTemplateId="ReportFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column7" runat="server" HeaderText="Action" Width="8%" AllowEdit="true"></obout:Column>
                <obout:Column id="Column8" runat="server" Width="10%">
                    <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>
            </Columns>
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
            <Templates>
                 <obout:GridTemplate runat="server" ID="deleteTemplate" >
                    <Template>
                        <%  If AllowDelete = True Then%>
                                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                        <%  End If%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="CodeFilter" ControlID="CodeF">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CodeF" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="DescFilter" ControlID="DescF">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="DescF" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="InputableFilter" ControlID="InputableF">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="InputableF" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="ReportFilter" ControlID="ReportF">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="ReportF" Width="100%">
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

        /* -- */
        function OnBeforeDelete(record) {
            if (confirm("Are you sure you want to delete?") == false) {
                return false;
            }
            return true;
        }
        function OnDelete(record) {
            alert("Data has been deleted successfully.")
        }
        /* -- */
    </script>
</asp:Content>