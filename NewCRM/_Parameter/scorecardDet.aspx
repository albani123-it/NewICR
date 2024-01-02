<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="scorecardDet.aspx.vb" Inherits="NewCRM.scorecardDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
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
            myWindow = window.open(url, "popupWindow", windowFeatures);
            myWindow.focus();
        }
        function popupCallback(data, destination) {
            document.getElementById(destination).value = document.getElementById(destination).value + ' ' +data;
        }
        $(function () {
            $("#txtGoodCond").bind('keydown', 'alt+f', function () {
                var l_obj_argument = new Object();
                l_obj_argument.url = "scorecardDetValues.aspx?destination=txtGoodCond";
                oWindowscroll(l_obj_argument.url, 410, 350)
            });
            $("#txtBadCond").bind('keydown', 'alt+f', function () {
                var l_obj_argument = new Object();
                l_obj_argument.url = "scorecardDetValues.aspx?destination=txtBadCond";
                oWindowscroll(l_obj_argument.url, 410, 350)
            });
        });
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="scorecard_code" runat="server" />
    <div class="modal-body">
        <center>
                <table class="table table-striped table-bordered">
                <tr>
                    <td colspan="2"  bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="25%"><b>Rating Model Category</b></td>
                    <td><asp:DropDownList ID="ddlSccCategory" runat="server" Width="300px" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Rating Model Code</b></td>
                    <td><asp:Label ID="lblSccCode" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Rating Model Name</b></td>
                    <td><asp:TextBox ID="txtSccName" runat="server" CssClass="form-control" Width="500px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Additive Points</b></td>
                    <td><asp:TextBox ID="txtAddPoint" runat="server" Width="300px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Description</b></td>
                    <td><asp:TextBox ID="txtSccDesc" runat="server" MaxLength="1000" Width="500px" Height="50px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec" style="display:none;">
                    <td><b>Good Condition</b></td>
                    <td>
                        <asp:TextBox ID="txtGoodCond" runat="server" MaxLength="1000" Width="300px" Height="50px" TextMode="MultiLine" CssClass="form-control" Style="float: none;"></asp:TextBox>
                        <br />
                        <span>Browse field (Alt + f)</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td><b>Bad Condition</b></td>
                    <td>
                        <asp:TextBox ID="txtBadCond" runat="server" MaxLength="1000" Width="500px" Height="50px" TextMode="MultiLine" CssClass="form-control" Style="float: none;"></asp:TextBox>
                        <br />
                        <span>Browse field (Alt + f)</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Action</b></td>
                    <td><asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList></td>
                </tr>
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save & Next" CssClass="btn btn-primary"></asp:button>&nbsp;
                        <asp:button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-dark" />&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
