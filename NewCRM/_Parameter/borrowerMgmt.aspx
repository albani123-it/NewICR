<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="borrowerMgmt.aspx.vb" Inherits="NewCRM.borrowerMgmt" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Parameter');
        aBc('brw_mgmt');
        /** **/

        /** **/
        function OnBeforeEdit(record) {
            fBrwEdit(record.brw_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fBrwEdit(-1);
            return false;
        }

        function fBrwEdit(id) {
            var l_obj_argument = new Object();  
            l_obj_argument.url = "borrowerMgmtDet.aspx?brw_id=" + (id);
            oWindow(l_obj_argument.url, 600, 300);
        }
    </script>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="20"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="brw_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="brw_type" HeaderText="Borrower Type" Width="20%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtBrwType" RowEditTemplateControlPropertyName="value" FilterTemplateId="BrwTypeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column3" runat="server" DataField="lst_sec_name" HeaderText="Borrower Sector" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtBrwSec" RowEditTemplateControlPropertyName="value" FilterTemplateId="BrwSecFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="scc_name" HeaderText="Borrower Rating Model Name" Width="25%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtBrwSccName" RowEditTemplateControlPropertyName="value" FilterTemplateId="BrwSccNameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" DataField="brw_cut_approved" HeaderText="Cut Off Approved" Width="10%" AllowFilter="false" Wrap="true"></obout:Column>
                <obout:Column id="Column6" runat="server" DataField="brw_cut_rejected" HeaderText="Cut Off Rejected" Width="10%" AllowFilter="false" Wrap="true"></obout:Column>
                <obout:Column id="Column7" runat="server" DataField="brw_is_active" HeaderText="Status" Width="10%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtBrwStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="BrwStatusFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column8" runat="server" HeaderText="Action" Width="5%" AllowEdit="true"></obout:Column>
                <obout:Column id="Column9" runat="server" Width="10%">
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
                <obout:GridTemplate runat="server" ID="BrwTypeFilter" ControlID="BrwType">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="BrwType" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="BrwSecFilter" ControlID="BrwSec">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="BrwSec" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="BrwSccNameFilter" ControlID="BrwSccName">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="BrwSccName" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="BrwStatusFilter" ControlID="BrwStatus">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="BrwStatus" Width="100%">
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