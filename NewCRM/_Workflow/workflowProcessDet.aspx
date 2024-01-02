<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="workflowProcessDet.aspx.vb" Inherits="NewCRM.workflowProcessDet" %>


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
            fEdit(record.lwd_id);
            return false;
        }


        function fEdit(id) {
            var wfid = document.getElementById("<%= wfid.clientID %>").value;
            window.location.href = "workflowProcessDet.aspx?wfid=" + (wfid) + "&wfpdid=" + id;
        }
        function fDetail(url) {
            var id = document.getElementById("<%= wfid.clientID %>").value;
            window.location.href = url +"?wfid=" + (id);
        }

        function fCheck(val) {
            if ( val == "0" ) {
                document.getElementById("<%= txtVar.clientID %>").readOnly = true;
                document.getElementById("<%= txtVar.clientID %>").className = "inpTxtDisabled";

                document.getElementById("<%= ddlCond.clientID %>").disabled = true;
                document.getElementById("<%= ddlCond.clientID %>").className = "inpDdlDisabled";

                document.getElementById("<%= txtValue.clientID %>").readOnly = true;
                document.getElementById("<%= txtValue.clientID %>").className = "inpTxtDisabled";
            }
            else if ( val == "1" ) {
                document.getElementById("<%= txtVar.clientID %>").readOnly = false;
                document.getElementById("<%= txtVar.clientID %>").className = "inpTxt";

                document.getElementById("<%= ddlCond.clientID %>").disabled = false;
                document.getElementById("<%= ddlCond.clientID %>").className = "inpDdl";

                document.getElementById("<%= txtValue.clientID %>").readOnly = false;
                document.getElementById("<%= txtValue.clientID %>").className = "inpTxt";
            }
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
        <input type="hidden" id="wfpdid" runat="server" />
        <div style="margin-bottom:20px;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;"">
            <ul class="top-bar">
              <li class="list-bar"><a href="javascript:fDetail('workflowProcessMgmt.aspx');">Workflow Process</a></li>
              <li class="list-bar"><a href="javascript:fDetail('workflowProcessDet.aspx');" class="active">Workflow Process Detail</a></li>
              <li class="list-bar"><a href="javascript:fDetail('workflowMenuRole.aspx');">Workflow Role</a></li>
              <li class="list-bar" style="float:right"><a href="javascript:fDetail('workflowMenuRole.aspx');"">Next >></a></li>
              <li class="list-bar" style="float:right"><a href="javascript:fDetail('workflowProcessMgmt.aspx');""><< Prev</a></li>
            </ul>
        </div>
        <div style="margin-bottom:20px;">
            <table cellspacing="0" cellpadding="2" width="60%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
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
                    <td><b>Process</b></td>
                    <td>
                        <asp:DropDownList ID="ddlProcess" runat="server" CssClass="inpDdl"></asp:DropDownList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Next Process</b></td>
                    <td>
                        <asp:DropDownList ID="ddlNextAction" runat="server" CssClass="inpDdl"></asp:DropDownList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Action Name</b></td>
                    <td>
                        <asp:TextBox ID="txtActionName" runat="server" CssClass="inpTxt" style="width:400px;"></asp:TextBox>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>With Condition</b></td>
                    <td>
                        <asp:RadioButtonList ID="rdCondition" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Value="1" onclick="fCheck('1');">Yes</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True" onclick="fCheck('0');">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Variable</b></td>
                    <td><asp:TextBox ID="txtVar" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Operator</b></td>
                    <td>
                        <asp:DropDownList ID="ddlCond" runat="server" CssClass="inpDdlDisabled" Enabled="false">
                            <asp:ListItem Value="Select One">Select One</asp:ListItem>
                            <asp:ListItem Value="=">=</asp:ListItem>
                            <asp:ListItem Value="<>"><></asp:ListItem>
                            <asp:ListItem Value=">=">>=</asp:ListItem>
                            <asp:ListItem Value=">">></asp:ListItem>
                            <asp:ListItem Value="<="><=</asp:ListItem>
                            <asp:ListItem Value="<"><</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Value</b></td>
                    <td><asp:TextBox ID="txtValue" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"></asp:TextBox></td>
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
                <obout:Column id="Column1" runat="server" DataField="lwd_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="process" HeaderText="Process" Width="20%" Wrap="true"></obout:Column>
                <obout:Column id="Column3" runat="server" DataField="next_process" HeaderText="Next Process" Width="20%" Wrap="true"></obout:Column>
                <obout:Column id="Column4" runat="server" DataField="with_cond" HeaderText="With Condition" Width="10%" Wrap="true"></obout:Column>
                <obout:Column id="Column9" runat="server" DataField="lwd_action" HeaderText="Action Name" Width="15%" Wrap="true"></obout:Column>
                <obout:Column id="Column7" runat="server" DataField="lsw_name" HeaderText="Workflow" Width="15%" Wrap="true"></obout:Column>
                <obout:Column id="Column5" runat="server" AllowEdit="true" HeaderText="Action" Width="8%"></obout:Column>
                <obout:Column id="Column6" runat="server" Width="10%">
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