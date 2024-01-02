<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Memorandum_Bisnis_Kredit.aspx.vb"
    Inherits="NewCRM.Memorandum_Bisnis_Kredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
            padding: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="clear: both; margin:0 auto;  padding: 10px; width: 80%">
        <div>
            <div style="text-align: center;">
                <asp:Label ID="lbljudul" runat="server" Style="font-weight: bold; font-size: 14px;
                    width:80%;">MEMORANDUM BISNIS KREDIT (MBK)</asp:Label>
            </div>
            <br />
            <table style="clear: both;padding: 10px; width: 100%; text-align: center;">
                <tr>
                    <td style="width: 80%;">
                        <asp:Literal ID="litMbk" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <asp:Literal ID="litSignature" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div style="text-align: right;">
                <%--<asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
                <asp:Button Text="Export" ID="btnExport" CssClass="inpBtn" runat="server" Visible="false" />
                <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server"
                    Visible="false" OnClientClick="ex();return false;" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
