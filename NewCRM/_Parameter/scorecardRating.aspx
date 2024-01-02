<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="scorecardRating.aspx.vb" Inherits="NewCRM.scorecardRating" %>

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
//            keyC = event.keyCode;
//            if (keyC == 46 || keyC == 45 || (keyC >= 48 && keyC <= 57)) {
//                return true;
//            } else {
//                return false;
//            }
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
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
        <input type="hidden" id="numField" runat="server" />
        <div class="modal-body">
        <center>
            
                <table class="table table-striped table-bordered">
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
                    <td><asp:DropDownList runat="server" Width="300px" ID="ddlAction" CssClass="form-control"></asp:DropDownList></td>
                </tr>
            </table>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;">
                <tr>
                    
                <td width="15%" bgcolor="#2A3F54" style="color: #fff;"><b>Upper Limit</b></td>
                    
                <td width="15%" bgcolor="#2A3F54" style="color: #fff;"><b>Rating</b></td>
                    
                <td width="25%" bgcolor="#2A3F54" style="color: #fff;"><b>Category</b></td>
                    
                <td widht="45%" bgcolor="#2A3F54" style="color: #fff;"><b>Description</b></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    
                <td ><asp:TextBox ID="txtLimit1"  runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(1)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore1" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori1" runat="server" TextMode="MultiLine" Rows="2" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc1" runat="server" TextMode="MultiLine" Rows="2" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit2" runat="server" CssClass="form-control" Width="80px"
                      onchange="fCheckUpperLimit(2)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore2" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori2" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc2" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit3" runat="server" CssClass="form-control" Width="80px"
                              onchange="fCheckUpperLimit(3)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore3" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori3" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc3" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit4" runat="server" CssClass="form-control" Width="80px"
                      onchange="fCheckUpperLimit(4)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore4" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori4" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc4" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit5" runat="server" CssClass="form-control" Width="80px"
                      onchange="fCheckUpperLimit(5)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore5" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori5" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc5" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit6" runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(6)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore6" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori6" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc6" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit7" runat="server" CssClass="form-control" Width="80px"
                                onchange="fCheckUpperLimit(7)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore7" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori7" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc7" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit8" runat="server" CssClass="form-control" Width="80px"
                       onchange="fCheckUpperLimit(8)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore8" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori8" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc8" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit9" runat="server" CssClass="form-control" Width="80px"
                      onchange="fCheckUpperLimit(9)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore9" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori9" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc9" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit10" runat="server" CssClass="form-control" Width="80px"
                         onchange="fCheckUpperLimit(10)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore10" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori10" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc10" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit11" runat="server" CssClass="form-control" Width="80px"
                         onchange="fCheckUpperLimit(11)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore11" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori11" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc11" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit12" runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(12)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore12" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori12" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc12" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit13" runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(13)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore13" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori13" runat="server" TextMode="MultiLine" Rows="3" style="width:200px;" CssClass="inpTxt"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc13" runat="server" TextMode="MultiLine" Rows="3" style="width:400px;" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit14" runat="server" CssClass="form-control" Width="80px"
                     onchange="fCheckUpperLimit(14)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore14" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori14" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc14" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit15" runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(15)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore15" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori15" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc15" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit16" runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(2)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore16" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori16" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc16" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit17" runat="server" CssClass="form-control" Width="80px"
                       onchange="fCheckUpperLimit(17)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore17" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori17" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc17" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit18" runat="server" CssClass="form-control" Width="80px"
                         onchange="fCheckUpperLimit(18)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore18" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori18" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc18" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><asp:TextBox ID="txtLimit19" runat="server" CssClass="form-control" Width="80px"
                       onchange="fCheckUpperLimit(19)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="txtScore19" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori19" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Style="width: 200px;"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc19" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><asp:TextBox ID="txtLimit20" runat="server" CssClass="form-control" Width="80px"
                        onchange="fCheckUpperLimit(20)" onKeyPress="return fTrapUpperLimit()"></asp:textbox>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td><asp:TextBox ID="txtScore20" onKeyPress="return fTrapDefaultScore()" runat="server" CssClass="form-control"></asp:textbox></td>
                    <td><asp:TextBox ID="txtKategori20" runat="server" TextMode="MultiLine" Rows="3" Style="width: 200px;" CssClass="form-control"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtDesc20" runat="server" TextMode="MultiLine" Rows="3" Style="width: 400px;" CssClass="form-control"></asp:TextBox></td>
                </tr>

            </table>
            
            <table class="table table-striped table-bordered">
                <tr style="height:30px">
				    <td  colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
