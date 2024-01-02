<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="assignment.aspx.vb" Inherits="NewCRM.assignment" %>

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
        function oWindowscroll(url, w, h) {
            var width = w;
            var height = h;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open(url, "subFire", windowFeatures);
            myWindow.focus();
        }
        function popupCallback(value, value2) {
            document.getElementById("hdUser").value = value;
            document.getElementById("txtUser").value = value2;
            
            return false;
        }
        function fBrowse(){
            var position = document.getElementById("hdPosition").value;
            oWindowscroll("../_Admin/userRefByPosition.aspx?position=" + position,900,500);
        }
        function oWindowscroll(url, w, h) {
            var width = w;
            var height = h;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open(url, "popupWindow", windowFeatures);
            myWindow.focus();
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" value="0" id="hdPosition" runat="server" />
    <div style="margin: 0 auto;clear:both;width:98%;">
        <center>
	    <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0;margin-top: 5px;">
            <tr>
                
                <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0; height: auto;">&nbsp;</td>
            </tr>
            <tr bgcolor="#f6f7f7">
                <td width="30%"><b>No. Aplikasi</b></td>
                <td><asp:TextBox ID="txtAppno" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr bgcolor="#ebecec" style="text-align:left;">
                <td><b>User</b></td>
                <td>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="inpTxt" style="margin-right: 5px;width:200px;"></asp:TextBox>
                    <input type="hidden" value="0" id="hdUser" runat="server" />
                    <input type="button" value="Browse" onclick="fBrowse();" />
                </td>
            </tr>
            <tr style="height:30px">
                <td background="../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                    <asp:button id="btnSave" runat="server" Text="Simpan" CssClass="inpBtn"></asp:button>&nbsp;
					<asp:button id="btnCancel" runat="server" Text="Batal" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>&nbsp;
                </td>
			</tr>
        </table>
        </center>
    </div>
    </form>
</body>
</html>
