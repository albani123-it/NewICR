<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_capital_det.aspx.vb" Inherits="NewCRM.glo_capital_det" %>

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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            return true;
        }
        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num);
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <div>
        <center>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Month</b></td>
                    <td><asp:DropDownList ID="ddlMonth" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Year</b></td>
                    <td><asp:DropDownList ID="ddlYear" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Capital</b></td>
                    <td><asp:TextBox ID="txtCapital" runat="server" value="0" CssClass="inpTxt" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>House Limit</b></td>
                    <td><asp:TextBox ID="txtHouseLimit" runat="server" value="0" CssClass="inpTxt" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"></asp:TextBox></td>
                </tr>
                <tr background="../styles/grid/footer.gif" style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn" OnClientClick="this.disabled='true';" UseSubmitBehavior="false"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
