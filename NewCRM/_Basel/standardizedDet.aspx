<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="standardizedDet.aspx.vb" Inherits="NewCRM.standardizedDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <div>
        <center>
            <table class="table table-striped table-bordered">
                <tr>
                    
                 <td colspan="2"  bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="20%"><b>Asset Category</b></td>
                    <td><asp:Label ID="lblAssetCat" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Asset Sub Category</b></td>
                    <td><asp:Label ID="lblAssetSubCat" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Model Name</b></td>
                    <td><asp:Label ID="lblModelName" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:Label ID="lblDesc" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Credit Conversion Factor (CCF)</b></td>
                    <td><asp:TextBox runat="server" ID="txtCCF" Width="250px" CssClass="form-control"></asp:TextBox></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Exposure Class</b></td>
                    <td><asp:DropDownList ID="ddlExposure" runat="server" Width="250px" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Data Source</b></td>
                    <td><asp:Label ID="lblDataSource" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Activated</b></td>
                    <td><input type="checkbox" id="chkStatus" name="chkStatus" runat="server" /></td>
                </tr>
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
        </center>
    </div>
    </form>
</body>
</html>
