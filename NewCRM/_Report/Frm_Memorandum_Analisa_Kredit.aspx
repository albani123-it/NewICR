<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_Memorandum_Analisa_Kredit.aspx.vb" Inherits="NewCRM.Frm_Memorandum_Analisa_Kredit" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
 <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
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
 <body>
 <form id="form1" runat="server">
    <div  style="clear:both; margin:0 auto; padding:0; width:80%">
        <div>
            <div style="text-align:center;">
            <asp:Label ID="lbljudul" runat="server" style="font-weight:bold;font-size:14px;width:99%;">MEMORANDUM ANALISA KREDIT (MAK)</asp:Label>
            </div>
            <br />
           <table style="clear:both; margin:0 auto; padding:0; width:100%;text-align:center;">
            <tr>
            <td style="width:80%;"><asp:Literal ID="litMak" runat="server"></asp:Literal></td>
            </tr>
            <tr>
            <td style="width:80%;"><asp:Literal ID="litSignature" runat="server"></asp:Literal></td>
            </tr>
            </table>
            <div style="text-align:right;">
                <%--<asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
                <asp:Button Text="Export" ID="btnExport" CssClass="inpBtn" runat="server" Visible="false" />
                <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="false" OnClientClick="ex();return false;" />     
            </div>
    </div>
    </div> 
   </form>
    </body>
    </html>
