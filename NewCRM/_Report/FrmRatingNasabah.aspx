<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmRatingNasabah.aspx.vb" Inherits="NewCRM.FrmRatingNasabah" EnableEventValidation="false" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%--<script src="../Scripts/tinymce/jscripts/tiny_mce/tiny_mce.js" type="text/javascript"></script>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<title></title>
    <script type="text/javascript">
        function printEx() {
            alert(10);
            var doc = new jsPDF();
            doc.fromHTML($('#content').get(0), 15, 15, {
                'width': 500,
                'elementHandlers': specialElementHandlers
            });
            doc.save('sample-file.pdf');
        }
       function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}  
        
    </script>
    <style type="text/css">
        .tblHead
        {
            background: url(../Styles/btn_bank/header.gif);
            height: 33px;
            font-family: Tahoma;
            font-size: 10px;
            font-weight: bold;
            color: #242500;
            text-align: center;
            vertical-align: middle;
            padding:5px;
        }
         
    </style>
    </head>
 <body onload="fLoad()">
 <form id="form1" runat="server">
    <div  >
        <div >
        <table id="tblFrn" runat="server" style="clear:both; margin:0 auto; padding:0; width:80%;text-align:center;" >
            <tr>
            <td style="text-align:center;">
            <asp:Label ID="lbljudul1" runat="server" style="font-weight:bold;font-size:14px;">FORM RATING NASABAH (FRN)</asp:Label></td>
            
            </tr>
            <tr>
            <td>
            <asp:Label ID="lbljudul2" runat="server" style="font-size:14px;"></asp:Label>
            </td>
            </tr>
            <tr>
            <td style="text-align:left;width:80%">
            <textarea id="txtmsg1" name="txtmsg1"  runat="server" rows="3" cols="180" class="iWdw"></textarea>
            <asp:Literal ID="litScore" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left;width:80%">
            <textarea id="txtmsg2" name="txtmsg2"  runat="server" rows="3" cols="180" class="iWdw"></textarea>
            <asp:Literal ID="litScore1" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left;width:80%">
            <asp:Literal ID="litSignature" runat="server"></asp:Literal>
            </td>
            </tr>
           </table>
            
           <div style="margin-left:84%">
                <%--<asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
                <asp:Button Text=" Save & Export" ID="btnExport" CssClass="inpBtn" runat="server" Visible="false" />
                <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="false" OnClientClick="ex();return false;" />     
            </div>
    
    </div>
    </div> 
    </form>
    </body>
    </html>

