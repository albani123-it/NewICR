﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CCFPoolDet.aspx.vb" Inherits="NewCRM.CCFPoolDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <title>Skyworx - Internal Credit Rating</title>
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
            
            <table class="table table-striped table-bordered">
                <tr>
                    
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Pool Name</b></td>
                    <td><asp:TextBox ID="txtPoolName" runat="server" CssClass="form-control" Style="width: 200px;"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Rule</b></td>
                    <td><asp:DropDownList ID="ddlRule" runat="server" Width="250px" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Rating</b></td>
                    <td><asp:DropDownList ID="ddlRating" runat="server" Width="250px" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>CCF Value</b></td>
                    <td><asp:TextBox ID="txtCCFValue" runat="server" Width="200px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td width="30%"><b>Distribution Baseline</b></td>
                    <td><asp:TextBox ID="txtDistributionBaseline" runat="server" Width="200px" CssClass="form-control"></asp:TextBox></td>
                </tr>
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