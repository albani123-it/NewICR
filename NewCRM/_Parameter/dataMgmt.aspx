<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="dataMgmt.aspx.vb" Inherits="NewCRM.dataMgmt" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">

        function OnBeforeEdit(record) {
            fEdit(record.data_id,1);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit('-1','-1');
            return false;
        }

        function fEdit(id,mode) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "dataMgmtDet.aspx?id=" + (id) + "&mode=" + mode;
            oWindowscroll(l_obj_argument.url, 1000, 500);
        }

    </script>
    <style type="text/css">		
						
            .ob_gFl .ob_gC, .ob_gFl .ob_gCW
            {
            	height: auto;
            }
	</style>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="20"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="data_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="data_type" HeaderText="Data Type" ShowFilterCriterias="false" Width="20%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtDataTyp" RowEditTemplateControlPropertyName="value" FilterTemplateId="DataTypFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column3" runat="server" DataField="data_name" HeaderText="Data Name" ShowFilterCriterias="false" Width="20%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtDataName" RowEditTemplateControlPropertyName="value" FilterTemplateId="DataNameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="data_desc" HeaderText="Description" ShowFilterCriterias="false" Width="35%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtDataDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="DataDescFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" DataField="is_Active" HeaderText="Status" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtDataStat" RowEditTemplateControlPropertyName="value" FilterTemplateId="DataStatFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column6" runat="server" HeaderText="Action" Width="5%" AllowEdit="true" />
                <obout:Column id="Column7" runat="server" Width="10%" AllowEdit="true">
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
                <obout:GridTemplate runat="server" ID="DataTypFilter" ControlID="DataTyp">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="DataTyp" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="DataNameFilter" ControlID="DataName">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="DataName" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="DataDescFilter" ControlID="DataDesc">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="DataDesc" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="DataStatFilter" ControlID="DataStat">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="DataStat" Width="100%">
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
            <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter" />
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