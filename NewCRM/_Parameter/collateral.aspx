<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="collateral.aspx.vb" Inherits="NewCRM.collateral" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
      
        /** **/

        /** **/
        function OnBeforeEdit(record) {
            fCollEdit(record.coll_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fCollEdit(-1);
            return false;
        }

        function fCollEdit(id){
            var l_obj_argument = new Object();
            l_obj_argument.url = "collateralDet.aspx?collid=" + (id);
            oWindow(l_obj_argument.url, 500, 240);
        }
    </script>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" 
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" PageSize="20"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="coll_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="coll_type" HeaderText="Collateral Type" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCollType" RowEditTemplateControlPropertyName="value" FilterTemplateId="CollTypeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column3" runat="server" DataField="coll_class" HeaderText="Collateral Class" Width="10%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCollClass" RowEditTemplateControlPropertyName="value" FilterTemplateId="CollClassFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="coll_desc" HeaderText="Collateral Description" Width="25%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCollDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="CollDescFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" DataField="coll_source" HeaderText="Data Source" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCollSource" RowEditTemplateControlPropertyName="value" FilterTemplateId="CollSourceFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column6" runat="server" DataField="coll_is_active" HeaderText="Status" Width="8%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCollStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="CollStatusFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column7" runat="server" HeaderText="Action" Width="5%" AllowEdit="true"></obout:Column>
                <obout:Column id="Column8" runat="server" Width="5%">
                    <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>
            </Columns>
            
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />

            <Templates>
                 <obout:GridTemplate runat="server" ID="deleteTemplate" >
                    <Template>
                        <%  If AllowDelete = True Then %>
                                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                        <%  End If%>
                    </Template>
                </obout:GridTemplate> 
                <obout:GridTemplate runat="server" ID="CollTypeFilter" ControlID="CollType">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CollType" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="CollClassFilter" ControlID="CollClass">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CollClass" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="CollDescFilter" ControlID="CollDesc">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CollDesc" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="CollSourceFilter" ControlID="CollSource">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CollSource" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="CollStatusFilter" ControlID="CollStatus">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CollStatus" Width="100%">
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