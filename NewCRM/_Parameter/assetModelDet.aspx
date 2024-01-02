<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="assetModelDet.aspx.vb" Inherits="NewCRM.assetModelDet" %>

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
        function checkParent(){
            var chk = document.getElementById("chkParentCat").checked;
            if(chk){
                document.getElementById("ddlCategoryGroup").disabled = true;
                document.getElementById("ruleCondition").style.display = "none";
                //document.getElementById("ddlCategoryGroup").value = "Select One";
            }
            else{
                document.getElementById("ddlCategoryGroup").disabled = false;
                document.getElementById("ruleCondition").style.display = "";
            }
        }

		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}            
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
      <div class="modal-body">
        <center>
            
            <table class="table table-striped table-bordered">
                <tr>
                    
                <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="120px"><b>Category</b></td>
                    <td><asp:Label runat="server" ID="lblCategory"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Sub Category</b></td>
                    <td><asp:Label runat="server" ID="lblSubCategory"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Model Name</b></td>
                    <td><asp:TextBox ID="txtModelName" runat="server" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:TextBox ID="txtDescription" runat="server" MaxLength="1000"  Height="50px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Approach</b></td>
                    <td><asp:DropDownList ID="ddlApproach" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">Select One</asp:ListItem>
                            <asp:ListItem Value="3">Advanced IRB</asp:ListItem>
                            <asp:ListItem Value="2">Foundation IRB</asp:ListItem>
                           <%-- <asp:ListItem Value="1">Standardized</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Data Source</b></td>
                    <td><asp:Label runat="server" ID="lblDataSource"></asp:Label></td>
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
