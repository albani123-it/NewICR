<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_financial_det.aspx.vb" Inherits="NewCRM.glo_financial_det" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.hotkeys.js"></script>
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
        .inpTxtFormula
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 1px 4px;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 11px;
            float:left;
        }

        .inpTxtFormulaDisabled
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 1px 4px;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 11px;
            float:left;
            background-color:#ebebe4;
            pointer-events: none;
            cursor: default;
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
        $(function () {
            $(".inpTxtFormula").bind('keydown', 'alt+f', function () {
                var control = window.event.srcElement.id;

                var left = (screen.width / 2) - (330 / 2);
                var top = (screen.height / 2) - (318 / 2);
                var url = "glo_financial_value.aspx?src=" + control;
                oWindowscroll(url,800,550);
            })
            .bind('keydown', 'alt+o', function () {
                var control = window.event.srcElement.id;
                var source = "operator"
                var left = (screen.width / 2) - (330 / 2);
                var top = (screen.height / 2) - (318 / 2);
                var url = "calc_operator.aspx?typ=sql&destination=" + control;
                window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);
            });
            $(".inpTxtFormulaDisabled").bind('keydown', 'alt+f', function () {
                var control = window.event.srcElement.id;

                var left = (screen.width / 2) - (330 / 2);
                var top = (screen.height / 2) - (318 / 2);
                var url = "glo_financial_value.aspx?src=" + control;
                oWindowscroll(url,800,550);
            })
            .bind('keydown', 'alt+o', function () {
                var control = window.event.srcElement.id;
                var source = "operator"
                var left = (screen.width / 2) - (330 / 2);
                var top = (screen.height / 2) - (318 / 2);
                var url = "calc_operator.aspx?typ=javascript&destination=" + control;
                window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);
            });
        });
        function popupCallback(data,destination)
        {
            document.getElementById(destination).value += data;
        }
        function fCheckCondition(){
            var val = $('#rdConditional input[type=radio]:checked').val();
            if (val == 'Y'){
                document.getElementById("txtCondition").readOnly = false;
                document.getElementById("txtConditionValue").readOnly = false;
                document.getElementById("txtCondition").className = "inpTxtFormula";
                document.getElementById("txtConditionValue").className = "inpTxtFormula";
            }
            else{
                document.getElementById("txtCondition").readOnly = false;
                document.getElementById("txtConditionValue").readOnly = false;
                document.getElementById("txtCondition").className = "inpTxtFormulaDisabled";
                document.getElementById("txtConditionValue").className = "inpTxtFormulaDisabled";
            }
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <div>
        <center>
            <table cellspacing="0" cellpadding="3" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Code</b></td>
                    <td><asp:TextBox ID="txtCode" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:TextBox ID="txtDescription" runat="server" CssClass="inpTxt" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Inputable</b></td>
                    <td>
                        <asp:RadioButtonList ID="rdInputable" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Selected>Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Order</b></td>
                    <td><asp:TextBox ID="txtOrder" runat="server" CssClass="inpTxt" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Proyeksi</b></td>
                    <td>
                        <asp:RadioButtonList ID="rdProyeksi" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="Y" Selected>Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>SQL Formula</b></td>
                    <td><asp:TextBox ID="txtSQLFormula" runat="server" CssClass="inpTxtFormula" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>SQL Formula Proyeksi</b></td>
                    <td><asp:TextBox ID="txtSQLFormulaProyeksi" runat="server" CssClass="inpTxtFormula" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Javascript Formula</b></td>
                    <td><asp:TextBox ID="txtJsFormula" runat="server" CssClass="inpTxtFormula" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Javascript Percentage Formula</b></td>
                    <td><asp:TextBox ID="txtJsPercentageFormula" runat="server" CssClass="inpTxtFormula" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Notes</b></td>
                    <td><asp:TextBox ID="txtNotes" runat="server" CssClass="inpTxt" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Conditional</b></td>
                    <td>
                        <asp:RadioButtonList ID="rdConditional" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" onchange="fCheckCondition();">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N" Selected>No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Condition</b></td>
                    <td><asp:TextBox ID="txtCondition" runat="server" CssClass="inpTxtFormulaDisabled" TextMode="MultiLine" Rows="3" Width="300px" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Condition Value</b></td>
                    <td><asp:TextBox ID="txtConditionValue" runat="server" CssClass="inpTxtFormulaDisabled" TextMode="MultiLine" Rows="3" Width="300px" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Report</b></td>
                    <td><asp:DropDownList ID="ddlReport" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td style="vertical-align:top;"><b>Sector</b></td>
                    <td style="padding-left:0;">
                        <%= fillSector()%>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Font Style</b></td>
                    <td><asp:DropDownList ID="ddlFontStyle" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Identasi</b></td>
                    <td><asp:TextBox ID="txtIdentasi" runat="server" CssClass="inpTxt" value="0" TextMode="Number" min="0" max="10"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Color</b></td>
                    <td><asp:TextBox ID="txtColor" runat="server" CssClass="inpTxt" ></asp:TextBox></td>
                </tr>
                <tr style="height:30px">
				    <td colspan="2" background="../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn" OnClick="btnSave_Click" OnClientClick="return confirm('Do you really want to proceed?');"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
