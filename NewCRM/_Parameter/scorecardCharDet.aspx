<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="scorecardCharDet.aspx.vb" Inherits="NewCRM.scorecardCharDet" %>

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
            document.getElementById(destination).value = data
        }
        $(function () {
            $(".inpLimit").bind('keydown', 'alt+f', function () {
                var dd = document.getElementById("numField").value;
                var val = document.getElementById("valField").value;
                var control = window.event.srcElement.id;
                var row = control.substring(control.lastIndexOf("_") + 1);
                var l_obj_argument = new Object();
                if (dd == "inte") {
                    l_obj_argument.url = "scorecardCharDetValues.aspx?destination=" + row + "&f=" + val;
                    oWindowscroll(l_obj_argument.url, 810, 350)
                }
            });
        });

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
        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-") {
                return num;
            }
            else if (num == "0-") {
                return "-"
            }
            else {
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
        $(document).ready(function () {
            $(".hidden").css("display", "none");
        })

    </script>
</head>
<body onload="fLoad();hidenGoodBad();">
    <form id="form1" runat="server">
        <input type="hidden" id="idHidden" runat="server" />
	    <input type="hidden" id="numField" runat="server" />
        <input type="hidden" id="valField" runat="server" />
    <div >
        <center>
            <table class="table table-striped table-bordered">
                <tr>
                    
                    <td colspan="2"  bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Field</b></td>
                    <td><asp:DropDownList ID="ddlSCHField" runat="server" CssClass="form-control" AutoPostBack="true" Style="width: 300px; overflow: auto;"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Default Score</b></td>
                    <td><asp:TextBox ID="txtDefaultScore" Width="200px" runat="server" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Koefisien</b></td>
                    <td><asp:TextBox ID="txtKoefisien" Width="200px" runat="server" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec" id="trStatus" runat="server">
                    <td><b>Status</b></td>
                    <td><asp:Label runat="server" ID="lblStatus"></asp:Label></td>
                </tr>
            </table>
            
                <table class="table table-striped table-bordered" width="100%" >

                    <tr bgcolor="#2A3F54">
                    <td width="50%" ><b><asp:Label ID="lblAttr" runat="server"></asp:Label></b></td>
                    <td width="50%" ><b>Score</b></td>
                    <td width="20%" style="color:#fff;" class="hidden"><b>Count</b></td>
                    <td width="20%" style="color:#fff;" class="hidden"><b>Good Count</b></td>
                    <td width="20%" style="color:#fff;" class="hidden"><b>Bad Count</b></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit1" runat="server" CssClass="form-control" Width="120px"
                     onchange="fCheckUpperLimit(1)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore1" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount1" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood1" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad1" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit2" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(2)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore2" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount2" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood2" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad2" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit3" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(3)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore3" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount3" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood3" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad3" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit4" runat="server" CssClass="form-control" Width="120px"
                        onchange="fCheckUpperLimit(4)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore4" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount4" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood4" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad4" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit5" runat="server" CssClass="form-control" Width="120px"
                      onchange="fCheckUpperLimit(5)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore5" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount5" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood5" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad5" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit6" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(6)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore6" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount6" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood6" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad6" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit7" runat="server" CssClass="form-control" Width="120px"
                      onchange="fCheckUpperLimit(7)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore7" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount7" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood7" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad7" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit8" runat="server" CssClass="form-control" Width="120px"
                        onchange="fCheckUpperLimit(8)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore8" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount8" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood8" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad8" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit9" runat="server" CssClass="form-control" Width="120px"
                      onchange="fCheckUpperLimit(9)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore9" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount9" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood9" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad9" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit10" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(10)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore10" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount10" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood10" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad10" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit11" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(11)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore11" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount11" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood11" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad11" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit12" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(12)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore12" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount12" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood12" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad12" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit13" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(13)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore13" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount13" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood13" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad13" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit14" runat="server" CssClass="form-control" Width="120px"
                      nchange="fCheckUpperLimit(14)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore14" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtCount14" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtGood14" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                    <td class="hidden"><asp:textbox id="txtBad14" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="inpTxt"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit15" runat="server" CssClass="form-control" Width="120px"
                      nchange="fCheckUpperLimit(15)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore15" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount15" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood15" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad15" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit16" runat="server" CssClass="form-control" Width="120px"
                      onchange="fCheckUpperLimit(2)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore16" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount16" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood16" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad16" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit17" runat="server" CssClass="form-control" Width="120px"
                      onchange="fCheckUpperLimit(17)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore17" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount17" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood17" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad17" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit18" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(18)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore18" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount18" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood18" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad18" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit19" runat="server" CssClass="form-control" Width="120px"
                      onchange="fCheckUpperLimit(19)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore19" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount19" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood19" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad19" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit20" runat="server" CssClass="form-control" Width="120px"
                       onchange="fCheckUpperLimit(20)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore20" onKeyPress="return fTrapDefaultScore()" runat="server" Width="200px" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtCount20" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtGood20" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td class="hidden"><asp:TextBox ID="txtBad20" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                </tr>
                <tr style="height: 30px">
                    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:Button>&nbsp;
                        <asp:Button ID="btnApp" runat="server" Text="Approve" CssClass="btn btn-dark"></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" OnClientClick="CloseMe()"></asp:Button>
                    </td>
                </tr>
            </table>
            
        </center>
    </div>
    </form>
</body>
</html>
