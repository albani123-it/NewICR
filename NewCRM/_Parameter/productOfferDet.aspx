<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="productOfferDet.aspx.vb"
    Inherits="NewCRM.productOfferDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        
        #tblProductOffering
        {
            width: 98%;
            text-align: left;
            border: thin solid #bcbcbc;
            border-radius: 5px;
            margin-top: 5px;
        }
        
        #tblProductOffering tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        
        #tblProductOffering tr:nth-child(odd)
        {
            background: #ebecec;
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
<body  onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="prod_id" />
    <div>
        <center>
            <table id="tblProductOffering" cellspacing="0" cellpadding="1">
                <tr>
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <b>Offering Code</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfferingCode" runat="server" CssClass="inpTxt" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="30%">
                        <b>Category</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategory" name="ddlCategory" runat="server" CssClass="inpDdl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Product</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProduct" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                            Rows="3" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Type</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" name="ddlType" runat="server" CssClass="inpDdl">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>SLA</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSLA" runat="server" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td colspan="2" background="../styles/btn_bank/footer.gif" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                            CssClass="inpBtn" OnClientClick="CloseMe()"></asp:Button>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
