<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="borrowerMgmtDet.aspx.vb" Inherits="NewCRM.borrowerMgmtDet" %>

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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45)
                return false;
            return true;
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="brw_id" />
    <div class="modal-body">
        <center>
            
                <table class="table table-striped table-bordered">
                <tr>
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Borrower Type</b></td>
                    <td><asp:TextBox ID="txtBrwType" name="txtBrwType" runat="server" CssClass="form-control" Style="width: 200px;"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Sector</b></td>
                    <td><asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Borrower Rating Model</b></td>
                    <td><asp:DropDownList ID="ddlBrwScc" name="ddlBrwScc" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Cut Off (Score) Approved</b></td>
                    <td><asp:TextBox ID="txtCutApp" name="txtCutApp" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Cut Off (Score) Rejected</b></td>
                    <td><asp:TextBox ID="txtCutRej" name="txtCutRej" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Activated</b></td>
                    <td><input type="checkbox" id="chkStatus" name="chkStatus" runat="server" /></td>
                </tr>
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
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
