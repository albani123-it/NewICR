<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="stressTestRes.aspx.vb" Inherits="NewCRM.stressTestRes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        function expcoll(a) {
            if (document.getElementById('tbl' + a).style.display == "" && document.getElementById('tblMin' + a).style.display == "none" ){
                document.getElementById('tbl' + a).style.display="none";
                document.getElementById('tblMin' + a).style.display="";
            }
            else {
                document.getElementById('tbl' + a).style.display="";
                document.getElementById('tblMin' + a).style.display="none";
            }
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <div>
        <center>
            
            <table class="table table-striped table-bordered">
                <tr>
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Name</b></td>
                    <td><asp:Label runat="server" ID="lblName"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:Label runat="server" ID="lblDesc"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Finish Date</b></td>
                    <td><asp:Label runat="server" ID="lblFinishDate"></asp:Label></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;">
                <tr background="../Images/bg-head.gif" bgcolor="#007cc2" style="border-radius: 5px 5px 0 0;">
                    <td background="../Images/bg-head.gif"><b>Detail</b></td>
                </tr>
                <tr>
                    <td style="padding-right:5px;">
                        <%= isiDataStressResult() %>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;margin-bottom:20px;">
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
					    <asp:button id="btnCancel" runat="server" Text="Close" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>    
    </div>
    </form>
</body>
</html>
