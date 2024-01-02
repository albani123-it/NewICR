<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_FormulirInfoAgunan.aspx.vb" Inherits="NewCRM.Frm_FormulirInfoAgunan"
    EnableEventValidation="false" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/textarea/tinymce/jscripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>

   

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
        
//       // di panggil 
        tinyMCE.init({
            // General options 
            mode: "textareas",
            theme: "advanced",
            width : "100%",
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
    <table id="tblFia" style="clear:both; margin:0 auto; padding:0; width:80%; text-align:center" runat="server" >
    
            <tr>
            <td style="text-align:center;">
            <input type="hidden" id="appno" runat="server" />
            <asp:Label ID="lbljudul1" runat="server" style="height:25px;font-weight:bold;font-size:14px;"><u>FORMULIR  INFORMASI AGUNAN (FIA)</u></asp:Label></td>
            </tr>
            <tr>
            <td style="text-align:left;">
           <asp:Literal ID="littgl" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
             <td style="text-align:left" >
            <asp:Label ID="lblheader1" runat="server" style="height:25px;font-size:13px;font-weight:bold;">1.	LAPORAN PENILAIAN AGUNAN PERUSAHAAN INDEPENDEN :</asp:Label>
            <asp:Literal ID="litagunan1" runat="server"></asp:Literal>
            <textarea id="txtmsg2" name="txtmsg2"  runat="server" rows="4" style="text-align:justify" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left;">
            <asp:Label ID="lblheader2" runat="server" style="height:25px;font-size:13px;font-weight:bold;">2.	ANALISA AGUNAN / JAMINAN :</asp:Label>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <asp:Label ID="lblket1" runat="server" style="height:25px;font-size:12px;font-weight:bold;">a.	Rincian Agunan</asp:Label>
            <textarea id="txtmsg1" runat="server" rows="4" cols="185"  class="iWdw"></textarea>
            <asp:Literal ID="littextarea" runat="server"></asp:Literal>
            <asp:Literal ID="litagunan2" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            
            </td>
            </tr>
            <tr>
             <td style="text-align:left">
             <textarea id="txtmsg3" visible="false" name="txtmsg3"  runat="server" rows="4" style="text-align:justify" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
              <asp:Label ID="lblket2" visible="false" runat="server" style="height:25px;font-size:12px;font-weight:bold;">b.Lokasi Agunan</asp:Label>
            <textarea id="txtmsg4" name="txtmsg4"  runat="server" style="text-align:justify" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <asp:Literal  visible="false" ID="litagunan3" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
             <asp:Label ID="lblket3"  visible="false" runat="server" style="height:25px;font-size:12px;font-weight:bold;">c.	Penukaran Agunan</asp:Label>
            <textarea id="txtmsg5"  visible="false" name="txtmsg5" style="text-align:justify" runat="server" rows="4" cols="185" class="iWdw"></textarea>
            <asp:Literal  visible="false" ID="litagunan4" runat="server"></asp:Literal>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <textarea  visible="false" id="txtmsg6" name="txtmsg6" style="text-align:justify" runat="server" rows="7" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <textarea  visible="false" id="txtmsg7" name="txtmsg7" style="text-align:justify" runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <textarea  visible="false" id="txtmsg8" name="txtmsg8" style="text-align:justify" runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
             <asp:Label ID="lblheader3" runat="server" style="height:25px;font-size:13px;font-weight:bold;">3.	PENGIKATAN AGUNAN & PENUTUPAN ASURANSI :</asp:Label>
            <textarea id="txtmsg9" name="txtmsg9" style="text-align:justify" runat="server" rows="4" cols="185" class="iWdw"></textarea>
            <asp:Literal ID="litagunan5" runat="server"></asp:Literal>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <textarea id="txtmsg10"  visible="false" name="txtmsg10" style="text-align:justify" runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <textarea id="txtmsg11" visible="false" name="txtmsg11" style="text-align:justify" runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <textarea id="txtmsg12" visible="false" name="txtmsg12" style="text-align:justify" runat="server" rows="4" cols="185" class="iWdw"></textarea>
            <asp:Literal ID="litagunan6" runat="server"></asp:Literal>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <textarea id="txtmsg13"  visible="false" name="txtmsg13" style="text-align:justify" runat="server" rows="3" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <asp:Label ID="lblheader4" runat="server" style="height:25px;font-size:13px;font-weight:bold;">4.	SECURITY COVERAGE RATIO</asp:Label> 
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <asp:Label ID="lblket4" runat="server" style="height:25px;font-size:13px;font-weight:bold;">Security Coverage Ratio awal</asp:Label>
            <asp:Literal ID="litagunan7" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <textarea id="txtmsg14" name="txtmsg14" style="text-align:justify" runat="server" rows="3" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <asp:Label ID="lblket5" runat="server" style="height:25px;font-size:13px;font-weight:bold;">Security Coverage Ratio setelah penukaran agunan</asp:Label>
            <asp:Literal ID="litagunan8" runat="server"></asp:Literal>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <asp:Label ID="lblket6" runat="server" style="height:25px;font-size:12px;">Keterangan :</asp:Label>
            <textarea id="txtmsg15" name="txtmsg15" style="text-align:justify"  runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
             <td>
             <textarea id="txtmsg16"  visible="false" name="txtmsg16" style="text-align:justify" runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
             <tr>
            <td style="text-align:left">
            <asp:Label ID="lblket7"  visible="false" runat="server" style="height:25px;font-size:13px;font-weight:bold;">5.	ISU TERKAIT AGUNAN</asp:Label>
            <textarea id="txtmsg17"  visible="false" name="txtmsg17"  runat="server" rows="8" cols="185" class="iWdw"></textarea>
            </td>
            </tr>
            <tr>
            <td style="text-align:left">
            <asp:Label ID="lblket8"  visible="false" runat="server" style="height:25px;font-size:13px;font-weight:bold;">6.	MEKANISME PERUBAHAN AGUNAN </asp:Label>
            <textarea id="txtmsg18"  visible="false" name="txtmsg18"  runat="server" rows="10" cols="185" class="iWdw"></textarea>
            </td>
            </tr>

            <tr>
            <td style="text-align:left">
            <asp:Literal ID="litSignature" runat="server"></asp:Literal>
            </td>
            </tr>
</table>
<div style="margin-left:85%">
    <%--<asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
    <asp:Button Text="Save & Export" ID="btnExport" CssClass="inpBtn" runat="server" Visible="false" />
    <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="false" OnClientClick="ex();return false;" />     
</div>
    </form>
</body>
</html>
