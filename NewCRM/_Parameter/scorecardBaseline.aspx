<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="scorecardBaseline.aspx.vb" Inherits="NewCRM.scorecardBaseline" %>

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
        .inpLimit
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            /*behavior:url(scripts/PIE.htc);*/
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 1px 4px;
            background: #f5f5f5;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 11px;
            float:left;
            /*position:relative;       	*/
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
        function formatCurrency(num) {
            var nozero = num
            if (nozero.length == 0 && nozero == "")
                return ""  

            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
            }
            else if(num == "0-")
            {
                return "-"
            }
            else{
                if (isNaN(num))
                    num = "";
                sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num);
            }
        }

        
    </script>
    <script type="text/javascript">
        var gRange
        //        function fShowField(idnya) {

        //            document.getElementById("idHidden").value = idnya
        //            gRange = document.selection.createRange()
        //            lRect = gRange.getBoundingClientRect()
        //            var lObj
        //            if (event.ctrlKey && event.keyCode == 70 && dd == "inte") {
        //                alert(dd);
        //                lObj = document.Form1.lstField
        //            }
        //            if (typeof (lObj) != "undefined") {
        //                lObj.style.visibility = "visible"
        //                lObj.style.left = lRect.left;
        //                lObj.style.top = lRect.top;
        //                lObj.focus();
        //            }

        //        }

        function fHideField(pObj) {//
            pObj.style.visibility = "hidden"
        }

        function fSelect(pObj) {//
            var idnya = document.getElementById("idHidden").value
            if (pObj.selectedIndex > -1)
                gRange.text = pObj.options[pObj.selectedIndex].value
            pObj.style.visibility = "hidden"
            document.getElementById(idnya).focus()
        }

        function fSelectKeyDown(pObj) {
            var idnya = document.getElementById("idHidden").value
            if (event.keyCode == 13) fSelect(pObj);
            else if (event.keyCode == 27) {
                pObj.style.visibility = "hidden";
                document.getElementById(idnya).focus()
            }
        }

        function fTrapUpperLimit() {
            var dd = document.getElementById("numField").value
            if (dd == "nume") {
                if ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 46 || event.keyCode == 45) {
                    return true;
                } else
                    return false;
            }
        }

        function fTrapDefaultScore() {
            keyC = event.keyCode;
            if (keyC == 46 || keyC == 45 || (keyC >= 48 && keyC <= 57)) {
                return true;
            } else {
                return false;
            }
        }

        function round(num, digits) {
            num1 = Math.pow(10, digits);
            return Math.round(num * num1) / num1;
        }

        function fCheckUpperLimit(num) {
            var lmt = 0;
            var nlmt = 0
            var dd = document.getElementById("numField").value
            if (dd == "nume") {
                for (i = 1; i < num; i++) {
                    nmlt = document.getElementById("txtLimit" + i).value;
                    if (nmlt > lmt) {
                        lmt = nmlt;
                    }
                }
                var numlmt = document.getElementById("txtLimit" + num).value;
                if (numlmt <= lmt) {
                    //alert("Next upper limit not less then " + ((lmt * 1) + 1));
                    //document.getElementById("txtLimit" + num).value = (lmt * 1) + 1;
                }
            }
        }

        function sAttribute(a) {
            if (a == "Continues") {
                document.getElementById("tblAttribute").style.display = "none";
            }
            else {
                document.getElementById("tblAttribute").style.display = "table";
            }
        }

        function countBaseline1() { document.getElementById("txtScore1").value = document.getElementById("txtGood1").value * 1 + document.getElementById("txtBad1").value * 1 }
        function countBaseline2() { document.getElementById("txtScore2").value = document.getElementById("txtGood2").value * 1 + document.getElementById("txtBad2").value * 1 }
        function countBaseline3() { document.getElementById("txtScore3").value = document.getElementById("txtGood3").value * 1 + document.getElementById("txtBad3").value * 1 }
        function countBaseline4() { document.getElementById("txtScore4").value = document.getElementById("txtGood4").value * 1 + document.getElementById("txtBad4").value * 1 }
        function countBaseline5() { document.getElementById("txtScore5").value = document.getElementById("txtGood5").value * 1 + document.getElementById("txtBad5").value * 1 }
        function countBaseline6() { document.getElementById("txtScore6").value = document.getElementById("txtGood6").value * 1 + document.getElementById("txtBad6").value * 1 }
        function countBaseline7() { document.getElementById("txtScore7").value = document.getElementById("txtGood7").value * 1 + document.getElementById("txtBad7").value * 1 }
        function countBaseline8() { document.getElementById("txtScore8").value = document.getElementById("txtGood8").value * 1 + document.getElementById("txtBad8").value * 1 }
        function countBaseline9() { document.getElementById("txtScore9").value = document.getElementById("txtGood9").value * 1 + document.getElementById("txtBad9").value * 1 }
        function countBaseline10() { document.getElementById("txtScore10").value = document.getElementById("txtGood10").value * 1 + document.getElementById("txtBad10").value * 1 }
        function countBaseline11() { document.getElementById("txtScore11").value = document.getElementById("txtGood11").value * 1 + document.getElementById("txtBad11").value * 1 }
        function countBaseline12() { document.getElementById("txtScore12").value = document.getElementById("txtGood12").value * 1 + document.getElementById("txtBad12").value * 1 }
        function countBaseline13() { document.getElementById("txtScore13").value = document.getElementById("txtGood13").value * 1 + document.getElementById("txtBad13").value * 1 }
        function countBaseline14() { document.getElementById("txtScore14").value = document.getElementById("txtGood14").value * 1 + document.getElementById("txtBad14").value * 1 }
        function countBaseline15() { document.getElementById("txtScore15").value = document.getElementById("txtGood15").value * 1 + document.getElementById("txtBad15").value * 1 }
        function countBaseline16() { document.getElementById("txtScore16").value = document.getElementById("txtGood16").value * 1 + document.getElementById("txtBad16").value * 1 }
        function countBaseline17() { document.getElementById("txtScore17").value = document.getElementById("txtGood17").value * 1 + document.getElementById("txtBad17").value * 1 }
        function countBaseline18() { document.getElementById("txtScore18").value = document.getElementById("txtGood18").value * 1 + document.getElementById("txtBad18").value * 1 }
        function countBaseline19() { document.getElementById("txtScore19").value = document.getElementById("txtGood19").value * 1 + document.getElementById("txtBad19").value * 1 }
        function countBaseline20() { document.getElementById("txtScore20").value = document.getElementById("txtGood20").value * 1 + document.getElementById("txtBad20").value * 1 }

        function enableField(id) {
            id = id.substring(id.length - 1, id.length);
            if (document.getElementById("txtLimit" + id).value == "") {
                document.getElementById("txtGood" + id).className = "inpTxtDisabled";
                document.getElementById("txtGood" + id).readonly = "true";
                document.getElementById("txtBad" + id).className = "inpTxtDisabled";
                document.getElementById("txtBad" + id).readonly = "true";

                document.getElementById("txtScore" + id).value = "";
                document.getElementById("txtGood" + id).value = "";
                document.getElementById("txtBad" + id).value = "";
            }
            else {
                document.getElementById("txtGood" + id).className = "inpTxt";
                document.getElementById("txtGood" + id).readonly = "false";
                document.getElementById("txtBad" + id).className = "inpTxt";
                document.getElementById("txtBad" + id).readonly = "false";
            }
        }

        function loadDisableField() {
            for (var i = 1; i <= 20; i++) {
                if (document.getElementById("txtLimit" + i).value == "") {
                    document.getElementById("txtGood" + i).className = "inpTxtDisabled";
                    document.getElementById("txtGood" + i).readonly = "true";
                    document.getElementById("txtBad" + i).className = "inpTxtDisabled";
                    document.getElementById("txtBad" + i).readonly = "true";
                }
            }
        }     
    </script>
</head>
<body onload="fLoad(); loadDisableField()">
    <form id="form1" runat="server">
        <input type="hidden" id="numField" runat=server />
    <div>
        <center>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0;margin-top: 5px;">
                <tr>
                    
                    <td colspan="2"  bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Rating Model Code</b></td>
                    <td><asp:Label ID="lblSccCode" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td width="30%"><b>Rating Model Name</b></td>
                    <td><asp:Label ID="lblSccName" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7" id="trStatus" runat="server">
                    <td><b>Action</b></td>
                    <td><asp:DropDownList runat="server" ID="ddlAction" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;">
                <tr>
                    <td width="25%" background="../Images/bg-head.gif" style="color:#fff;"><b>Upper Limit</b></td>
                    <td width="25%" background="../Images/bg-head.gif" style="color:#fff;"><b>Baseline Count</b></td>
                    <td width="25%" background="../Images/bg-head.gif" style="color:#fff;"><b>Good Count</b></td>
                    <td widht="25%" background="../Images/bg-head.gif" style="color:#fff;"><b>Bad Count</b></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit1" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(1)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore1" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood1" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline1()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad1" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline1()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit2" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(2)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore2" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood2" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline2()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad2" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline2()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit3" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(3)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore3" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood3" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline3()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad3" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline3()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit4" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(4)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore4" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood4" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline4()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad4" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline4()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit5" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(5)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore5" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood5" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline5()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad5" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline5()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit6" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(6)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore6" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood6" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline6()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad6" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline6()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit7" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(7)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore7" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood7" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline7()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad7" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline7()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit8" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(8)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore8" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood8" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline8()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad8" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline8()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit9" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(9)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore9" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood9" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline9()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad9" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline9()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit10" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(10)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore10" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood10" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline10()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad10" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline10()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit11" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(11)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore11" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood11" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline11()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad11" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline11()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit12" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(12)" onKeyPress="return fTrapUpperLimit()" onkeyup=" enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore12" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood12" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline12()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad12" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline12()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit13" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(13)" onKeyPress="return fTrapUpperLimit()" onkeyup=" enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore13" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood13" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline13()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad13" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline13()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit14" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(14)" onKeyPress="return fTrapUpperLimit()" onkeyup=" enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore14" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood14" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline14()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad14" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline14()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit15" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(15)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore15" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood15" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline15()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad15" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline15()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit16" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(2)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore16" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood16" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline16()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad16" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline16()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit17" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(17)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore17" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood17" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline17()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad17" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline17()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit18" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(18)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore18" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood18" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline18()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad18" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline18()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:textbox id="txtLimit19" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(19)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:textbox id="txtScore19" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood19" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline19()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad19" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline19()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:textbox id="txtLimit20" runat="server" CssClass="inpTxt"
						onchange="fCheckUpperLimit(20)" onKeyPress="return fTrapUpperLimit()" onkeyup="enableField(this.id);"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:textbox id="txtScore20" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt" Enabled="false"></asp:textbox></td>
                    <td><asp:textbox id="txtGood20" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline20()" CssClass="inpTxt"></asp:textbox></td>
                    <td><asp:textbox id="txtBad20" onKeyPress="return fTrapDefaultScore()" runat="server" OnKeyUp="countBaseline20()" CssClass="inpTxt"></asp:textbox></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 0 0 5px 5px;">
                <tr background="../styles/grid/footer.gif" style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button>&nbsp;
                        <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
