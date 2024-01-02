<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rule.aspx.vb" Inherits="NewCRM.rule" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function OnBeforeEdit(record) {
            fRuleDetail(record.rul_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fRuleDetail(-1);
            return false;
        }
    </script>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
    <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" 
			AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" PageSize="20"
            AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
        <Columns>
            <obout:Column runat="server" id="Column1" DataField="rul_id" Visible="false"></obout:Column>
            <obout:Column runat="server" id="Column2" DataField="rul_name" HeaderText="Rule Name" Width="20%" ShowFilterCriterias="false" Wrap="true">
                <TemplateSettings RowEditTemplateControlId="txtRulename" RowEditTemplateControlPropertyName="value" FilterTemplateId="RuleNameFilter" />
                <FilterOptions>
                    <obout:FilterOption IsDefault="true" Type="Contains" />
                </FilterOptions>
            </obout:Column>
            <obout:Column runat="server" id="Column3" DataField="rul_sql" HeaderText="SQL Query" Width="50%" ShowFilterCriterias="false" Wrap="true">
                <TemplateSettings RowEditTemplateControlId="txtRuleSQL" RowEditTemplateControlPropertyName="value" FilterTemplateId="RuleSQLFilter" />
                <FilterOptions>
                    <obout:FilterOption IsDefault="true" Type="Contains" />
                </FilterOptions>
            </obout:Column>
            <obout:Column runat="server" id="Column4" DataField="data_name" HeaderText="Master Data" Width="10%" ShowFilterCriterias="false" Wrap="true">
                <TemplateSettings RowEditTemplateControlId="txtRuleType" RowEditTemplateControlPropertyName="value" FilterTemplateId="RuleTypeFilter" />
                <FilterOptions>
                    <obout:FilterOption IsDefault="true" Type="Contains" />
                </FilterOptions>
            </obout:Column>
            <obout:Column runat="server" id="Column5" Width="5%" HeaderText="Action" AllowEdit="true"></obout:Column>
            <obout:Column runat="server" id="Column6" Width="10%">
                <TemplateSettings TemplateID = "deleteTemplate" />
            </obout:Column>
        </Columns>

        <FilteringSettings InitialState="Visible" FilterPosition="Top" />

        <Templates>
            <obout:GridTemplate runat="server" ID="deleteTemplate" >
                <Template>
                    <% If AllowDelete = True Then%>
                        <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                    <% End If%>
                </Template>
            </obout:GridTemplate>
            <obout:GridTemplate runat="server" ID="RuleNameFilter" ControlID="RuleName">
                <Template>
                    <obout:OboutTextBox runat="server" ID="RuleName" Width="100%">
                        <ClientSideEvents OnKeyUp="applyFilter" />
                    </obout:OboutTextBox>
                </Template>
            </obout:GridTemplate>
            <obout:GridTemplate runat="server" ID="RuleSQLFilter" ControlID="RuleSql">
                <Template>
                    <obout:OboutTextBox runat="server" ID="RuleSql" Width="100%">
                        <ClientSideEvents OnKeyUp="applyFilter" />
                    </obout:OboutTextBox>
                </Template>
            </obout:GridTemplate>
            <obout:GridTemplate runat="server" ID="RuleTypeFilter" ControlID="RuleType">
                <Template>
                    <obout:OboutTextBox runat="server" ID="RuleType" Width="100%">
                        <ClientSideEvents OnKeyUp="applyFilter" />
                    </obout:OboutTextBox>
                </Template>
            </obout:GridTemplate>
        </Templates>
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
            document.getElementById("txtEvents").value = "Data was deleted.\n" + document.getElementById("txtEvents").value;
        }
        /* -- */

        function fRuleDetail(scid) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "ruleDet.aspx?scid=" + scid;
            oWindowscroll(l_obj_argument.url, 1000, 600);
        } 
	</script>
</asp:Content>