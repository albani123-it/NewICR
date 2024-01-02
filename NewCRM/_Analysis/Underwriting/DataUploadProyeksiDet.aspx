<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataUploadProyeksiDet.aspx.vb" Inherits="NewCRM.DataUploadProyeksiDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
     <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }

        function fLoad() {
            <asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
        }
    </script>
</head>
<body onload="fLoad()">
    <form id="form1" runat="server">
    <div>
    <center>
        <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
            <tr>
                <td colspan="2" background="../Images/bg-head.gif" bgcolor="#007cc2" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
            </tr>            
            <tr bgcolor="#ebecec">
                <td><b>Select File</b></td>
                <td>
                    <div>
                        <table>
                             <tr>
                                <td>
                                  <asp:Label runat="server" ID="lblFileExcel"></asp:Label>
                                  <asp:FileUpload ID="FileUploadXLS" runat="server" />
                                  <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                </td>
                             </tr>
                              <tr runat="server" id="TRWord">
                                <td> 
                                   &nbsp; The maximum file size is 20MB.
                                </td>
                             </tr>
                        </table>
                   </div>
                </td>                
            </tr>
            <tr runat="server" id="TRSheet" style="display:none;">
                <td><b>Select Sheet</b></td>
                <td>
                    <asp:DropDownList ID="ddlSheets" runat="server" AppendDataBoundItems = "true"></asp:DropDownList>
                </td>
            </tr>
            <%--<tr bgcolor="#f6f7f7">
                <td width="30%"><b>Table Name</b></td>
                <td width="68%">
                    <asp:textbox ID="dataupld_name" name="dataupld_name" style="width: 250px;" runat="server" CssClass="inpTxt"></asp:textbox>
                </td>
            </tr>--%>
            <tr style="height:30px">
				<td background="../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                    <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button>
					<asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                </td>
			</tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
