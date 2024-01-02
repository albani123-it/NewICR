<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="stressTest.aspx.vb" Inherits="NewCRM.stressTest" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Basel');
        aBc('stress');
        /** **/

        /** **/
        function OnBeforeEdit(record) {
            fEdit(record.ssm_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "stressTesDet.aspx?id=" + (id);
            oWindowscroll(l_obj_argument.url, 1350, 600);
        }
        function fDetail(code, typ) {
            var l_obj_argument = new Object();
            if (typ == "View") {
                l_obj_argument.url = "stressTestRes.aspx?id=" + (code);
                oWindow(l_obj_argument.url, 800, 540);
            }
            else {
                l_obj_argument.url = "stressTesDet.aspx?id=" + (code);
                oWindow(l_obj_argument.url, 1250, 600);
            }

        }
    </script>
    <div style="clear:both; margin:0 auto; padding:0; width:98%">
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" 
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="ssm_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="ssm_name" HeaderText="Name" Width="15%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtName" RowEditTemplateControlPropertyName="value" FilterTemplateId="NameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column3" runat="server" DataField="ssm_desc" HeaderText="Description" Width="20%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="DescFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="ssm_req_date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Request Date" Width="15%" AllowFilter="false"></obout:Column>
                <obout:Column id="Column5" runat="server" DataField="ssm_finish_date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Finish Date" Width="15%" AllowFilter="false"></obout:Column>
                <obout:Column id="Column6" runat="server" DataField="stat" HeaderText="Status" Width="15%" ShowFilterCriterias="false">
                    <TemplateSettings RowEditTemplateControlId="txtStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="StatusFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column7" runat="server" Width="20%">
                    <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>
            </Columns>
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
            <Templates>
                <obout:GridTemplate runat="server" ID="deleteTemplate">
                    <Template>
                        <a ID="viewLink" href="javascript:fDetail('<%# Container.DataItem("ssm_id") %>','View');" class='ob_gAL'>View Result</a>&nbsp&nbsp;&nbsp;
                        <a ID="editLink" href="javascript:fDetail('<%# Container.DataItem("ssm_id") %>','Edit');" class='ob_gAL'>Edit</a>&nbsp&nbsp;&nbsp;
                        <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton> &nbsp;
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="NameFilter" ControlID="Name">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="Name" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="DescFilter" ControlID="Desc">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="Desc" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="StatusFilter" ControlID="Status">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="Status" Width="100%">
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
