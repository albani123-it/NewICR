<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="workflowProcessMgmt.aspx.vb" Inherits="NewCRM.workflowProcessMgmt" %>

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
            fEdit(record.lwp_id);
            return false;
        }


        function fEdit(id) {
            var wfid = document.getElementById("<%= wfid.clientID %>").value;
            window.location.href = "workflowProcessMgmt.aspx?wfid=" + (wfid) + "&wfpid=" + id;
        }
        function fDetail(url) {
            var id = document.getElementById("<%= wfid.clientID %>").value;
            window.location.href = url +"?wfid=" + (id);
        }
    </script>
    <style>
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
        ul.top-bar {
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow: hidden;
            padding-left:5px;
            background: #D2D2D2;
        }

        li.list-bar {
            float: left;
            font-weight: bold;
            line-height: 1.5em;
            font-family: Trebuchet MS,sans-serif;
           
        }
        
        li.list-bar-nav {
            float: right;
            font-weight: bold;
            line-height: 1.5em;
            font-family: Trebuchet MS,sans-serif;
           
        }

        li.list-bar a {
            display: block;
            color: #000;
            text-align: center;
            padding: 14px 16px;
            text-decoration: none;
        }
        
        li.list-bar-nav a {
            display: block;
            color: #000;
            text-align: center;
            padding: 14px 16px;
            text-decoration: none;
        }

        li.list-bar a:hover
        {
            background-color:transparent;
            background-color: #ebebeb;
             border-radius:11px;
            -moz-border-radius:11px;
            -webkit-border-radius:11px;
        }

        .active {
            background-color: #ebebeb;
            border-radius:11px;
            -moz-border-radius:11px;
            -webkit-border-radius:11px;
            color: #fff;
            border:thin solid #cccccc;
        }
    </style>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <input type="hidden" id="wfid" runat="server" />
        <input type="hidden" id="wfpid" runat="server" />
        <div style="margin-bottom:20px;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;"">
            <ul class="top-bar">
              <li class="list-bar"><a href="javascript:fDetail('workflowProcessMgmt.aspx');" class="active">Workflow Process</a></li>
              <li class="list-bar"><a href="javascript:fDetail('workflowProcessDet.aspx');">Workflow Process Detail</a></li>
              <li class="list-bar"><a href="javascript:fDetail('workflowMenuRole.aspx');">Workflow Role</a></li>
              <li class="list-bar" style="float:right"><a href="javascript:fDetail('workflowProcessDet.aspx');"">Next >></a></li>
            </ul>
        </div>
        <div style="margin-bottom:20px;">
            <table cellspacing="0" cellpadding="1" width="60%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">
                        <asp:button id="btnNew" runat="server" Text="New" CssClass="inpBtn"></asp:button>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Workflow</b></td>
                    <td><asp:DropDownList ID="ddlWorkflow" runat="server" CssClass="inpDdlDisabled"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Process ID</b></td>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" CssClass="inpTxt"></asp:TextBox>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">*</span><br />
                        <span>Numeric Only</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Process Name</b></td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="inpTxt" style="width:500px;"></asp:TextBox>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                    </td>
                </tr>
                <tr style="height:30px">
                    <td colspan="2" background="../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button>
                    </td>
                </tr>
            </table>
        </div>
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="10"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowAddingRecords="false"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="lwp_id" HeaderText="Workflow Process ID" Width="10%"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="lwp_name" HeaderText="Workflow Process Name" Width="40%"></obout:Column>
                <obout:Column id="Column3" runat="server" DataField="lsw_name" HeaderText="Workflow" Width="20%"></obout:Column>
                <obout:Column id="Column5" runat="server" AllowEdit="true" HeaderText="Action" Width="5%"></obout:Column>
                <obout:Column id="Column6" runat="server" >
                    <TemplateSettings TemplateID = "actionTemplate" />
                </obout:Column>
            </Columns>
            <Templates>
                <obout:GridTemplate runat="server" ID="actionTemplate" >
                    <Template>
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