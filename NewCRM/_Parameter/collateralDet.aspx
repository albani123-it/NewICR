<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="collateralDet.aspx.vb" Inherits="NewCRM.collateralDet" %>

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
    <input type="hidden" runat="server" id="collateral_id" />
       <div class="modal-body">
        <center>
            
        <table class="table table-striped table-bordered">
                <tr>
                     <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Collateral Type</b></td>
                    <td><asp:TextBox ID="txtCollType" name="CollType" runat="server" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Collateral Class</b></td>
                    <td><asp:DropDownList ID="ddlCollClass" name="CollClass" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Description</b></td>
                    <td><asp:TextBox ID="txtCollDesc" name="CollDesc" runat="server" MaxLength="1000"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Data Source</b></td>
                    <td><asp:DropDownList ID="ddlCollSource" name="CollSource" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Activated</b></td>
                    <td><input type="checkbox" id="chkStatus" name="chkStatus" runat="server" /></td>
                </tr>
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="this.disabled='true';" UseSubmitBehavior="false"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
