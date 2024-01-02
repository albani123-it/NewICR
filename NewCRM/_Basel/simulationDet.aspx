<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="simulationDet.aspx.vb" Inherits="NewCRM.simulationDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
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
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45)
                return false;
            return true;
        }

        function rangeNumber(num){
            if(num > 100){
                alert("Max number is 100");
                return 100;
            }
            return num;
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <div>
        <center>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius:  5px 5px 0 0;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../Images/bg-head.gif" bgcolor="#007cc2" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="20%"><b>Name</b></td>
                    <td><asp:TextBox runat="server" ID="txtStsName" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:TextBox runat="server" ID="txtStsDesc" TextMode="MultiLine" Width="70%" Rows="5" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Data for Simulation</b></td>
                    <td>
                        <%--<asp:DropDownList ID="txtData" runat="server">
                            <asp:ListItem Value="10">10%</asp:ListItem>
                            <asp:ListItem Value="20">20%</asp:ListItem>
                            <asp:ListItem Value="30">30%</asp:ListItem>
                            <asp:ListItem Value="40">40%</asp:ListItem>
                            <asp:ListItem Value="50">50%</asp:ListItem>
                            <asp:ListItem Value="60">60%</asp:ListItem>
                            <asp:ListItem Value="70">70%</asp:ListItem>
                            <asp:ListItem Value="80">80%</asp:ListItem>
                            <asp:ListItem Value="90">90%</asp:ListItem>
                            <asp:ListItem Value="100">100%</asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:TextBox runat="server" ID="txtData" CssClass="inpTxt" Width="40px" onkeypress="return isNumberKey(event)" onkeyup="this.value=rangeNumber(this.value);"></asp:TextBox>
                        &nbsp %
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;">
                <tr background="../Images/bg-head.gif" bgcolor="#007cc2" style="border-radius: 5px 5px 0 0;">
                    <td background="../Images/bg-head.gif">&nbsp;</td>
                </tr>
                <tr>
                    <td style="padding:0px;">
                        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Tahoma;font-size: 10px;height: 22px;" background="../Styles/grid/header.gif" >
                            <tr style="height:33px;font-weight:bold;">
                                <td width="12px" style="padding-left:14px;">&nbsp;</td>
                                <td width="25%" style="min-width:325px;">Asset Category</td>
                                <td width="25%">Description</td>
                                <td width="15%">Asset Class</td>
                                <td width="15%">Approach</td>
                                <td width="10%">Status</td>
                                <td width="10%" style="text-align:center;">Use Stress</td>
                            </tr>
                        </table>
                        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Verdana;font-size: 10px;color:#5E5E70;height: 22px;">
                            <%= getAssetList()%>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;margin-bottom:20px;">
                <tr background="../styles/grid/footer.gif" style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Close" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
    <script type="text/javascript">
        function expcoll(a) {

            var path = location.pathname.split('/');

            //alert(location.protocol + '//' + location.host + '/' + location.pathname);
            var pathz = location.protocol + '//' + location.host + '/' + path[1] + '/'

            if (document.getElementById('img' + a).getAttribute('src') == "../Images/ec_minus.png" || document.getElementById('img' + a).getAttribute('src') == pathz + "Images/ec_minus.png") {
                document.getElementById('img' + a).src = pathz + "Images/ec_plus.png";
                PageMethods.GetData(a, onSuccess, onerror);
            }
            else {

                document.getElementById('img' + a).src = pathz + "Images/ec_minus.png";
                PageMethods.GetData(a, onSuccess2, onerror);
            }

        }
        function onSuccess(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {
                document.getElementById('trdet' + res[i + 1]).style.display = "none";
                PageMethods.GetDataModel(res[i + 1], onSuccessModel, onerror);
            }
        }
        function onSuccess2(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {
                document.getElementById('trdet' + res[i + 1]).style.display = "";
                PageMethods.GetDataModel(res[i + 1], onSuccessModel2, onerror);
            }
        }
        function onSuccessModel(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {

                document.getElementById('trdetail' + res[0] + "_" + res[i + 1]).style.display = "none";
            }
        }
        function onSuccessModel2(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {
                document.getElementById('trdetail' + res[0] + "_" + res[i + 1]).style.display = "";
            }
        }
	</script>
</body>
</html>
