<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="workflowMgmt.aspx.vb" Inherits="NewCRM.workflowMgmt" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Analysis');
        aBc('prd_mgmt');
        /** **/

        /** **/
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fLoad();
        function OnBeforeEdit(record) {
            fEdit(record.lsw_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            window.location.href = "workflowMgmt.aspx?wfid=" + (id);
        }
        function fDetail(id) {
            window.location.href = "workflowProcessMgmt.aspx?wfid=" + (id);
        }
    </script>
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
    </style>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <input type="hidden" id="wfid" runat="server" />
        <div style="margin-bottom:20px;">
            <h3>Workflow</h3>
            <table cellspacing="0" cellpadding="1" width="60%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">
                        <asp:button id="btnNew" runat="server" Text="New" CssClass="inpBtn"></asp:button>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Workflow Name</b></td>
                    <td>
                        <input type="text" id="txtWorkflowName" name="txtWorkflowName" runat="server" class="inpTxt" />&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><textarea id="txtWorkflowDesc" name="txtWorkflowDesc" runat="server" class="inpTxt" rows="5" style="width:500px;"></textarea></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Activated</b></td>
                    <td><input type="checkbox" id="chkActive" name="chkActive" runat="server" /></td>
                </tr>
                <tr style="height:30px">
                    <td colspan="2" background="../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;"><asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button></td>
                </tr>
            </table>
        </div>
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="10"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowAddingRecords="false"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="lsw_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="lsw_name" HeaderText="Workflow Name" Width="20%"></obout:Column>
                <obout:Column id="Column3" runat="server" DataField="lsw_desc" HeaderText="Description" Width="45%"></obout:Column>
                <obout:Column id="Column4" runat="server" DataField="lsw_status" HeaderText="Status" Width="20%" ></obout:Column>
                <obout:Column id="Column5" runat="server" AllowEdit="true" HeaderText="Action" Width="5%" ></obout:Column>
                <obout:Column id="Column6" runat="server" Width="10%" >
                    <TemplateSettings TemplateID = "actionTemplate" />
                </obout:Column>
            </Columns>
            <Templates>
                <obout:GridTemplate runat="server" ID="actionTemplate" >
                    <Template>
                        <a ID="detailLink" href="javascript:fDetail('<%# Container.DataItem("lsw_id") %>');" class='ob_gAL'>Detail</a>&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="zzz" runat="server" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
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
            alert("Data has been deleted successfully.")
        }
        /* -- */
    </script>
</asp:Content>