<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="scorecard.aspx.vb" Inherits="NewCRM.scorecard" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        /** **/
        function OnBeforeEdit(record) {
            fEdit(record.brw_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit('-1');
            return false;
        }

        function fEdit(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "scorecardDet.aspx?scc_code=" + (id);
            oWindow(l_obj_argument.url, 800, 600);
        }

        function fDetail(code, typ) {
            var l_obj_argument = new Object();
            if (typ == "Rating") {
                l_obj_argument.url = "scorecardRating.aspx?scc_code=" + (code);
                oWindowscroll(l_obj_argument.url, 1000, 600);
            }
            else {
                l_obj_argument.url = "scorecardDet.aspx?scc_code=" + (code);
                oWindowscroll(l_obj_argument.url, 800, 600);
            }
            
        }
    </script>

    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" 
			AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" PageSize="15"
            AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" Wrap="true" DataField="scc_code" HeaderText="Rating Model Code" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtSccCode" RowEditTemplateControlPropertyName="value" FilterTemplateId="SccCodeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column2" runat="server" Wrap="true" DataField="scc_name" HeaderText="Rating Model Name" Width="25%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtSccName" RowEditTemplateControlPropertyName="value" FilterTemplateId="SccNameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column3" runat="server" Wrap="true" DataField="scc_description" HeaderText="Description" Width="35%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtSccDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="SccDescFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" Wrap="true" DataField="scc_category" HeaderText="Rating Model Category" Width="15%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtSccCategory" RowEditTemplateControlPropertyName="value" FilterTemplateId="SccCategoryFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" Wrap="true" DataField="stu_info" HeaderText="Status" Width="10%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtSccStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="SccStatusFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column6" runat="server" Wrap="true" HeaderText="Action" Width="15%">
                    <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>
            </Columns>
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />

            <Templates>
                <obout:GridTemplate runat="server" ID="deleteTemplate" >
                    <Template>
                        <%  If AllowRating = True Then %>
                                <a ID="ratingLink" href="javascript:fDetail('<%# Container.DataItem("scc_code") %>','Rating');" class='ob_gAL'>Rating</a>&nbsp&nbsp;
                        <%  End If%>
                        <%  If AllowEdit = True Then %>
                            <a ID="detailLink" href="javascript:fDetail('<%# Container.DataItem("scc_code") %>','Detail');" class='ob_gAL'>Detail</a>&nbsp&nbsp;        
                        <%  End If%>
                        <%  If AllowDelete = True Then %>
                                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>         
                        <%  End If%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="SccCodeFilter" ControlID="SccCode">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="SccCode" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="SccNameFilter" ControlID="SccName">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="SccName" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="SccDescFilter" ControlID="SccDesc">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="SccDesc" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="SccCategoryFilter" ControlID="SccCategory">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="SccCategory" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="SccStatusFilter" ControlID="SccStatus">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="SccStatus" Width="100%">
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