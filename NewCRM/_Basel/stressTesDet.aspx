<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="stressTesDet.aspx.vb" Inherits="NewCRM.stressTesDet" %>

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
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <div>
        <center>
            
            <table class="table table-striped table-bordered">
                <tr>
                     <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="20%"><b>Name</b></td>
                    <td><asp:TextBox runat="server" ID="txtStsName" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:TextBox runat="server" ID="txtStsDesc" TextMode="MultiLine" Width="70%" Rows="5" CssClass="form-control"></asp:TextBox></td>
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
                                <td width="8%">Asset Class</td>
                                <td width="8%">Approach</td>
                                <td width="5%" style="text-align:center;">Use Stress</td>
                                <td width="15%">PD Shock</td>
                                <td width="15%">LGD Shock</td>
                            </tr>
                        </table>
                        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Verdana;font-size: 10px;color:#5E5E70;height: 22px;">
                            <%= getAssetList()%>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;margin-bottom:20px;">
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Close" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe()"></asp:button>
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
        
        function isNumberInp(evt, Typ, ID) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var ddl,txt;
           
            if (Typ == 0) 
            { ddl = "pdTyp_"; txt = "pdTxt_" }
            else 
            { ddl = "lgdTyp_"; txt = "lgdTxt_" }

            if (document.getElementById(ddl + ID).value != "Notch") {
                if (charCode >= 48 && charCode <= 57)
                    return true;
                return false;
            }
            else {
                if (charCode >= 49 && charCode <= 57) {
                    document.getElementById(txt + ID).value = document.getElementById(txt + ID).value.substr(0, 0);
                    return true;
                }
                else
                return false;
            }
        }

        function onCheck(ID) {
            if (document.getElementById("chkBox_" + ID).checked == false) {
                document.getElementById("pdTxt_" + ID).value = "";
                document.getElementById("lgdTxt_" + ID).value = "";
            }
        }
	</script>
</body>
</html>
