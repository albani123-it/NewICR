<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="productMgmt.aspx.vb" Inherits="NewCRM.productMgmt" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
       

        /** **/
        function OnBeforeEdit(record) {
            fEdit(record.prd_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "productMgmtDet.aspx?prd_id=" + (id);
            oWindow(l_obj_argument.url, 750, 380);
        }
    </script>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="20"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="prd_id" Visible="false"></obout:Column>
                <obout:Column id="Column3" runat="server" DataField="prd_name" HeaderText="Product Name" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtPrdName" RowEditTemplateControlPropertyName="value" FilterTemplateId="PrdNameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="prd_desc" HeaderText="Product Description" Width="20%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtPrdDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="PrdDescFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column2" runat="server" DataField="lst_prd_type" HeaderText="Product List" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtPrdType" RowEditTemplateControlPropertyName="value" FilterTemplateId="PrdTypeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" DataField="prd_cut_approved" HeaderText="Cut Off Approved" Width="10%" AllowFilter="false" Wrap="true"></obout:Column>
                <obout:Column id="Column6" runat="server" DataField="prd_cut_rejected" HeaderText="Cut Off Rejected" Width="10%" AllowFilter="false" Wrap="true"></obout:Column>
                <obout:Column id="Column7" runat="server" DataField="prd_is_active" HeaderText="Status" Width="8%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtPrdStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="PrdStatusFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column8" runat="server" HeaderText="Action" Width="5%" AllowEdit="true"></obout:Column>
                <obout:Column id="Column9" runat="server" Width="5%">
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
                <obout:GridTemplate runat="server" ID="PrdTypeFilter" ControlID="PrdType">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="PrdType" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="PrdNameFilter" ControlID="PrdName">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="PrdName" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="PrdDescFilter" ControlID="PrdDesc">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="PrdDesc" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="PrdStatusFilter" ControlID="PrdStatus">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="PrdStatus" Width="100%">
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