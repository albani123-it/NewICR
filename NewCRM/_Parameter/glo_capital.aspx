<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_capital.aspx.vb" Inherits="NewCRM.glo_capital" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fLoad();
        function OnBeforeAdd() {
            fEdit(-1);
            return false;
        }
        function OnBeforeEdit(record) {
            fEdit(record.ID);
            return false;
        }
        function fEdit(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "glo_capital_det.aspx?id=" + (id);
            oWindowscroll(l_obj_argument.url, 550, 340);
        }
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

        function oWindowscroll(url, w, h) {
            var width = w;
            var height = h;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open(url, "subFire", windowFeatures);
            myWindow.focus();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto;clear:both;width:98%;">
		    <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0;margin-top: 5px;">
                    <tr>
                        <td background="../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;height:auto;">&nbsp;</td>
                    </tr>
            </table>
            <table cellSpacing="0" cellPadding="0" width="100%" style="border: thin solid #ccc;">
                <tr>
					<td>
                        <obout:Grid id="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="15"
			    AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="true"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
                            <Columns>
                                <obout:Column ID="Column1" DataField="ID" Visible="false" runat="server" />
                                <obout:Column ID="Column2" DataField="month_desc" HeaderText="Month" runat="server" Width="15%" ShowFilterCriterias="false">
                                    <TemplateSettings RowEditTemplateControlId="txtMonthDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="MonthFilter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column3" DataField="cap_year" HeaderText="Year" runat="server" Width="15%" ShowFilterCriterias="false">
                                    <TemplateSettings RowEditTemplateControlId="txtYear" RowEditTemplateControlPropertyName="value" FilterTemplateId="YearFilter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column4" DataField="cap_value" HeaderText="Capital" runat="server" Width="25%" DataFormatString="{0:N2}" ShowFilterCriterias="false">
                                    <TemplateSettings RowEditTemplateControlId="txtValue" RowEditTemplateControlPropertyName="value" FilterTemplateId="ValueFilter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column5" DataField="cap_house" HeaderText="House Limit" runat="server" Width="25%" DataFormatString="{0:N2}" ShowFilterCriterias="false">
                                    <TemplateSettings RowEditTemplateControlId="txtHouse" RowEditTemplateControlPropertyName="value" FilterTemplateId="HouseFilter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column6" HeaderText="Action" Width="8%" AllowEdit="true" runat="server" />
                                <obout:Column id="Column7" runat="server" Width="10%">
                                    <TemplateSettings TemplateID = "deleteTemplate" />
                                </obout:Column>
                            </Columns>

                            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                            
                            <Templates>
                                <obout:GridTemplate runat="server" ID="deleteTemplate" >
                                    <Template>
                                        <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="MonthFilter" ControlID="MonthType">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="MonthType" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="YearFilter" ControlID="YearType">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="YearType" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="ValueFilter" ControlID="Val">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="Val" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="HouseFilter" ControlID="House">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="House" Width="100%">
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
                    </td>
                </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
