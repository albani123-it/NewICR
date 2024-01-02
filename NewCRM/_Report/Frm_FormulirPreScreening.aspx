<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_FormulirPreScreening.aspx.vb" Inherits="NewCRM.Frm_FormulirPreScreening" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<title></title>
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
        .inpBtn
        {
            margin-left: 0px;
        }
    </style>
 </head>
 <body onload="fLoad()">
 <form id="form1" runat="server">
    <div  style="clear:both; margin:0 auto; padding:0; width:98%;margin-left:3cm" id="bodyC" runat="server" >
        <div>
        <div style="text-align:center;">
        <asp:Label ID="lbljudul" runat="server" style="font-weight:bold;font-size:16px;width:79%;margin-right: 6cm;">FORMULIR PRE SCREENING (FPS) </asp:Label>
        </div>
        <br />
        <div style="text-align:center;">
        <div style="width:79%;"><asp:Literal ID="litFPS" runat="server"></asp:Literal></div>
         <div style="width:79%;"><asp:Literal ID="litKeyperson" runat="server"></asp:Literal></div>
        </div>
         <br />
        <div style="text-align:center;">
        <div style="width:79%;"><asp:Literal ID="litFPS_A" runat="server"></asp:Literal></div>
        </div>
         <br />
        <div style="text-align:center;">
        <div style="width:79%;"><asp:Literal ID="litFPS_B" runat="server"></asp:Literal></div>
        </div>
         <br />
        <div style="text-align:center;">
        <div style="width:79%;">
            <asp:Literal ID="litFPS_Alasan" runat="server"></asp:Literal>        
        </div>
        <br />
        </div>

        <div style="text-align:center;">
        <div style="width:80%;">
            <asp:Literal ID="litSignature" runat="server"></asp:Literal>       
        </div>
        <br />
        </div>
       
</div>
</div> 
 <div style="margin-left:29cm">
           <%-- <asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
            <asp:Button Text="Export" ID="btnExport" CssClass="inpBtn" runat="server" 
                Visible="false" Height="25px" Width="58px" />
            <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="false" OnClientClick="ex();return false;" />     
        </div>
</form>
</body>
</html>