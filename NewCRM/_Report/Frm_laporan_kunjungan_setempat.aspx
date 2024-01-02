<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_laporan_kunjungan_setempat.aspx.vb" Inherits="NewCRM.Frm_laporan_kunjungan_setempat" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderInSubSub" runat="server">--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
 <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
  <script src="../Scripts/textarea/tinymce/jscripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
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

        //       // di panggil 
        tinyMCE.init({
            // General options 
            mode: "textareas",
            theme: "advanced",
            width: "100%",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave",
            setup: function (ed) {
                ed.onKeyPress.add(
function (ed, evt) {
}
);
            },
            // Theme options 
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,table",
            theme_advanced_buttons2: "styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons3: "cut,copy,paste,|,bullist,numlist,|,undo,redo,|,insertdate,inserttime,|,forecolor,backcolor",

            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,
            // Example content CSS (should be your site CSS) 
            //            content_css: "css/content.css",
            // Drop lists for link/image/media/template dialogs 
            template_external_list_url: "lists/template_list.js",
            external_link_list_url: "lists/link_list.js",
            external_image_list_url: "lists/image_list.js",
            media_external_list_url: "lists/media_list.js",
            // Style formats 
            style_formats: [
{ title: 'Bold text', inline: 'b' },
{ title: 'Red text', inline: 'span', styles: { color: '#ff0000'} },
{ title: 'Red header', block: 'h1', styles: { color: '#ff0000'} },
{ title: 'Example 1', inline: 'span', classes: 'example1' },
{ title: 'Example 2', inline: 'span', classes: 'example2' },
{ title: 'Table styles' },
{ title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
],
            // Replace values for the template plugin 
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });

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
        .inpBtn
        {}
    </style>
    </head>
<body  >
 <form id="form1" runat="server">
  
    <div  style="clear:both; margin:0 auto; padding:0; width:98%;" >
        <div align="center">
            <div style="text-align:center;">
            <asp:Label ID="lbljudul1" runat="server" style="font-weight:bold;font-size:14px;width:89%;display: inline;;margin: 10px 0;line-height:30px"><u>FORMULIR LAPORAN KUNJUNGAN SETEMPAT (FKS)</u></asp:Label>
            </div>
            <br />
            <div style="text-align:center; width:80%">
            <div align="center">
              <%= htmlform%></div>
              <div style="text-align: center;">
                <div style="width: 80%;">
                    <asp:Literal ID="litSignature" runat="server"></asp:Literal>
                </div>
            </div>
            </div>
                 
            <div style="margin-left:75%">
                <%--<asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
                <asp:Button Text="Export" ID="btnExport" CssClass="inpBtn" runat="server" 
                    Visible="false" Width="65px" />
                <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="false" OnClientClick="ex();return false;" />     
            </div>
             </div>
            </div>
    </form>
</body>
</html>