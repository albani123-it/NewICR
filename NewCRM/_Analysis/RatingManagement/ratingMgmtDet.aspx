<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ratingMgmtDet.aspx.vb" Inherits="Basel2BTN.ratingMgmtDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <link type="text/css" href="../../Scripts/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
        .inpTxt
        {
            width: 120px;
        }
        #ui-datepicker-div {display: none;}
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
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <div>
        <center>
            
            <table id="tblExists" runat="server" cellspacing="0" cellpadding="3" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;">
                    <td><b>Product Type</b></td>
                    <td><asp:DropDownList ID="ddlPrdTypEx" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td><b>Product Micro Type</b></td>
                    <td>
                        <asp:DropDownList ID="ddlRetailEx" runat="server" CssClass="inpDdl" Enabled="false" style="background-color:#ebecec;"></asp:DropDownList>
                        <input type="checkbox" id="chkRetailEx" name="chkRetailEx" runat="server"  />Yes
                    </td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/btn_bank/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSaveEx" runat="server" Text="Simpan" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancelEx" runat="server" Text="Batal" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                    </td>
			    </tr>
            </table>
            <table id="tblExists2" runat="server" cellspacing="0" cellpadding="3" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7" style="text-align:left;">
                    <td><b>Borrower Type</b></td>
                    <td><asp:DropDownList ID="ddlBrwTypEx" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/btn_bank/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSaveEx2" runat="server" Text="Simpan" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancelEx2" runat="server" Text="Batal" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
