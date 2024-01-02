<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="productMgmtDet.aspx.vb" Inherits="NewCRM.productMgmtDet" %>

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
        function fCheck(){
            if (document.getElementById("chkRetail").checked){
                document.getElementById("ddlPrdSccRetail").disabled=false;
                document.getElementById("ddlFacNonRetail").disabled=true;
                document.getElementById("ddlFacNonRetail").selectedIndex=0;
            }
            else{
                document.getElementById("ddlFacNonRetail").disabled=false;
                document.getElementById("ddlPrdSccRetail").disabled=true;
                document.getElementById("ddlPrdSccRetail").selectedIndex=0;
            }
            
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
    <input type="hidden" runat="server" id="product_id" />
    
    <div class="modal-body">
        <center>
            
            <table class="table table-striped table-bordered" width="100%">
                <tr>
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Product List</b></td>
                    <td><asp:DropDownList ID="ddlProduct" name="ddlProduct" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Product Name</b></td>
                    <td><asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Product Description</b></td>
                    <td><asp:TextBox ID="txtProductDesc" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec" style="display:none;">
                    <td><b>Micro Rating Model</b></td>
                    <td>
                        <asp:DropDownList ID="ddlPrdSccRetail" name="ddlPrdSccRetail" runat="server" CssClass="inpDdl"></asp:DropDownList>
                        <input type="checkbox" id="chkRetail" name="chkRetail" runat="server" onclick="fCheck();" />Yes
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Facility Rating Model</b></td>
                    <td><asp:DropDownList ID="ddlFacNonRetail" name="ddlFacNonRetail" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec" style="display:none;">
                    <td><b>Credit Limit Formula</b></td>
                    <td><asp:TextBox ID="txtCreditFormula" runat="server" MaxLength="1000" Width="300px" Height="50px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Cut Off (Score) Approved</b></td>
                    <td><asp:TextBox ID="txtCutApp" name="txtCutApp" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Cut Off (Score) Rejected</b></td>
                    <td><asp:TextBox ID="txtCutRej" name="txtCutRej" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td><b>SLA</b></td>
                    <td><asp:TextBox ID="txtSLA" runat="server" CssClass="form-control" value="0"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Activated</b></td>
                    <td><input type="checkbox" id="chkStatus" name="chkStatus" runat="server" /></td>
                </tr>
                <tr style="height:30px">
				    <td colspan="2"  style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" OnClientClick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
