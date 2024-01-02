<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_Tectical_Account_Planning.aspx.vb" Inherits="NewCRM.Frm_Tectical_Account_Planning" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
<title></title>

<style>

/* 1 column: 320px */
.autowide {
  margin: 0 auto;
  width: 98%;
}
.header-content {
    position: absolute;
    bottom: 0;
    left: 0;
	width:98%;
  }
  
.autowide .module {
  height:150px;
  border-radius: .25rem;
  margin-bottom: 1rem;
  position:relative;
}

.autowide .module p {
  padding: .25rem .75rem;
}

/* 2 columns: 600px */
@media only screen and (min-width: 600px) {
  .autowide .module {
		float: left;
		margin-right: 2.564102564102564%;
		width: 48.717948717948715%;
	}
	.autowide .module:nth-child(2n+0) {
		margin-right: 0;
	}
}

/* 3 columns: 768px */
@media only screen and (min-width: 768px) {
  .autowide .module {
		width: 31.623931623931625%;
	}
	.autowide .module:nth-child(2n+0) {
		margin-right: 2.564102564102564%;
	}
	.autowide .module:nth-child(3n+0) {
		margin-right: 0;
	}
}

/* 4 columns: 992px and up */
@media only screen and (min-width: 992px) {
  .autowide .module {
		width: 23.076923076923077%;
	}
	.autowide .module:nth-child(3n+0) {
		margin-right: 2.564102564102564%;
	}
	.autowide .module:nth-child(4n+0) {
		margin-right: 0;
	}
}
</style>
<script type="text/javascript">
    //    aMenu('Report');

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
<body onload="fLoad()">
 <form id="form1" runat="server">
        <table style="clear:both; margin:0 auto; padding:0; width:80%;text-align:center;" id='tblrpt' runat="server">
        <tr>
            <td style="text-align:center;">
            <asp:Label ID="lbljudul1" runat="server" style="font-weight:bold;font-size:15px;width:99%;">TACTICAL ACCOUNT PLANNING (TAP)</asp:Label>
            </td>
            </tr>
            <tr>
            <td style="width:80%;text-align:center"><asp:Literal ID="littap" runat="server"></asp:Literal> </td>
            </tr>
            <tr>
            <td style="width:80%;float:left;"><asp:Literal ID="Littap_A" runat="server"></asp:Literal>    <br /></td>
            </tr>
          
            <tr>
            <td style="text-align:left;width:80%">
            <asp:Label ID="lblMsgAtas" runat="server" style="font-weight:bold;font-size:14px;width:99%;float:left;">2.	Wallet Share</asp:Label>
            <asp:Literal ID="littap1" runat="server"></asp:Literal><br />
            </td>
            </tr>
            <tr>
            <td style="text-align:left;width:80%">
             <asp:Label ID="lblMsgAta_B" runat="server" style="font-weight:bold;font-size:14px;width:99%;float:left;">3.	Action Plan</asp:Label>
            <asp:Literal ID="littap2" runat="server"></asp:Literal><br />
            </td>
            </tr>
            <tr>
            <td style="text-align:left;width:80%">
            <asp:Literal ID="littap4" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left;width:80%">
            <asp:Literal ID="litSignature" runat="server"></asp:Literal>
            </td>
            </tr>
            </table>
                
           <%-- <br />
            <div style="text-align:center;">
            <div style="width:80%;float:left;"><asp:Literal ID="littap3" runat="server"></asp:Literal></div>
            </div>--%>
           <div style="margin-left:85%">
           <%-- <asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
            <asp:Button Text="Export" ID="btnExport" CssClass="inpBtn" runat="server" Visible="false" />
            <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="false" OnClientClick="ex();return false;" />     
            </div>
    </form>
</body>
</html>