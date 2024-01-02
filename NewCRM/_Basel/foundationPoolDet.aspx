<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="foundationPoolDet.aspx.vb" Inherits="NewCRM.foundationPoolDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <title></title>
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
        function reloadParentWindow(){
            window.opener.reloadOboutGrid();
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <div>
        <center>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius:  5px 5px 0 0;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Pool Name</b></td>
                    <td><asp:TextBox ID="txtPoolName" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Rule</b></td>
                    <td><asp:DropDownList ID="ddlRule" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Rating</b></td>
                    <td><asp:DropDownList ID="ddlRating" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>PD Value</b></td>
                    <td><asp:TextBox ID="txtPDValue" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Distribution Baseline</b></td>
                    <td><asp:TextBox ID="txtDistributionBaseline" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr style="height:30px">
				    <td background="../styles/btn_bank/footer.gif"  colspan="2" style="border-radius: 0 0 5px 5px;">
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
