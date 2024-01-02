<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="scorecardChar.aspx.vb" Inherits="NewCRM.scorecardChar" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.opener.location.href = window.opener.location.href;
            window.close();
        }
        function oWindow(url, w, h) {
            var width = w;
            var height = h;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open(url, "subFire", windowFeatures);
        }
        function OnBeforeEdit(record) {
            fEdit(record.sch_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            var scid = document.getElementById("scid").value;
            var cat = document.getElementById("sccat").value;
            
            var l_obj_argument = new Object();
            l_obj_argument.url = "scorecardCharDet.aspx?scid=" + scid + "&schid=" + id + "&cat=" + cat;
            oWindow(l_obj_argument.url, 800, 540);
        }
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="scid" runat="server" />
        <input type="hidden" id="sccat" runat="server" />
    <div>
        <table class="table table-striped table-bordered">
            <tr>
                
                 <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">
                    <asp:Button ID="btnNew" runat="server" Text="Add New" CssClass="btn btn-info" OnClientClick="OnBeforeAdd()" />
                <//td>
            </tr>
            <tr>
                <td colspan="2" style="padding:0">
                    <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" ShowFooter="true"
		                AutoGenerateColumns="false" EnableRecordHover="false" Width="100%" AllowPaging="true" AllowRecordSelection="true"
                        AllowPageSizeSelection="false" AllowSorting="true" AllowAddingRecords="false" AllowFiltering="true"
		                FolderStyle="~\styles\grid">
                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" OnBeforeClientEdit="OnBeforeEdit" />
                        <Columns>
                            <obout:Column ID="Column1" runat="server" DataField="sch_id" HeaderText="" Visible="false"></obout:Column>
                            <obout:Column ID="Column2" runat="server" DataField="sch_field" HeaderText="Field" Width="50%" ShowFilterCriterias="false">
                                <TemplateSettings RowEditTemplateControlId="txtSchField" RowEditTemplateControlPropertyName="value" FilterTemplateId="SchFieldFilter" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                </FilterOptions>
                            </obout:Column>
                            <obout:Column ID="Column3" runat="server" DataField="sch_status" HeaderText="Status" Width="20%" ShowFilterCriterias="false">
                                <TemplateSettings RowEditTemplateControlId="txtSchStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="SchStatusFilter" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                </FilterOptions>
                            </obout:Column>
                            <obout:Column ID="Column4" runat="server" HeaderText="Action" Width="10%" AllowEdit="true"></obout:Column>
                            <obout:Column ID="Column5" runat="server" Width="10%">
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
                            <obout:GridTemplate runat="server" ID="SchFieldFilter" ControlID="SchField">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="SchField" Width="100%">
                                        <ClientSideEvents OnKeyUp="applyFilter" />
                                    </obout:OboutTextBox>
                                </Template>
                            </obout:GridTemplate>
                            <obout:GridTemplate runat="server" ID="SchStatusFilter" ControlID="SchStatus">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="SchStatus" Width="100%">
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
            <tr background="../styles/btn_bank/footer.gif" style="height:30px">
                <td colspan="2" style="border-radius: 0 0 5px 5px;">
				    <asp:button id="btnCancel" runat="server" Text="Close" CausesValidation="False" CssClass="btn btn-danger" onclientclick="CloseMe()"></asp:button>
                </td>
			</tr>
        </table>
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
    </form>
</body>
</html>
