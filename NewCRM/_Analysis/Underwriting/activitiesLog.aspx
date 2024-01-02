<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="activitiesLog.aspx.vb" Inherits="NewCRM.activitiesLog" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function fLogs(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "activityLogDet.aspx?code=" + id;
            oWindowscroll(l_obj_argument.url, 1250, 600);
        }
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
<body onload="fLoad();">
    <form id="form1" runat="server">
    <div style="margin: 0 auto;clear:both;width:98%;">
	    <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0;margin-top: 5px;">
            <tr>
                <td background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;height:auto;">&nbsp;</td>
            </tr>
        </table>
        <table cellSpacing="0" cellPadding="0" width="100%" style="border: thin solid #ccc;">
            <tr>
		        <td style="padding-left:0;">
                    <obout:Grid id="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="false" PageSize="-1"
			            AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
                        AllowPageSizeSelection="false" AllowFiltering="false" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                        <Columns>
                            <obout:Column ID="Column1" DataField="id" Visible="false" runat="server" />
                            <obout:Column ID="Column2" DataField="code" Visible="false" runat="server" />
                            <obout:Column ID="Column3" DataField="desc" HeaderText="Page" runat="server" Width="20%" Wrap="true" />
                            <obout:Column ID="Column4" DataField="update_by" HeaderText="Update By" runat="server" Width="20%" Wrap="true" />
                            <obout:Column ID="Column5" DataField="update_date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Update Date" runat="server" Width="20%" Wrap="true" />
                            <obout:Column ID="Column6" DataField="status" HeaderText="Status" runat="server" Width="15%" Wrap="true" />
                            <obout:Column id="Column7" runat="server" Width="10%" HeaderText="Action">
                                <TemplateSettings TemplateID = "viewTemplate" />
                            </obout:Column>
                        </Columns>
                        <Templates>
                            <obout:GridTemplate runat="server" ID="viewTemplate" >
                                <Template>
                                    <asp:LinkButton ID="zzz" OnCommand="export" runat="server" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Export  </asp:LinkButton>
                                    <%--<a class="ob_gALF" href="javascript:fLogs('<%# Container.DataItem("code") %>');">Export</a>--%>
                                </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
