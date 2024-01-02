<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_FormulirAnalisaKeuangan.aspx.vb"
    Inherits="NewCRM.Frm_FormulirAnalisaKeuangan" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/textarea/tinymce/jscripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/idxlibs/idxwidget.js"></script>
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
          // di panggil 
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

         function formatCurrency(num) {
        num = num.toString().replace(/\$|\,/g, '');
        //        console.log(num);
        if (num == "-") {

            return num;
        }
        else if (num == "") {
            return num;
        }
        else {
            if (isNaN(num))
                num = "";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num);
        }
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
        
        #txtopini
        {
            margin-left: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="clear: both; margin: 0 auto; padding: 0; width: 80%; text-align: center"
        id="tblRpt" runat="server">
        <tr>
            <td style="text-align: center;">
                <asp:Label ID="lbljudul1" runat="server" Style="font-weight: bold; font-size: 14px;
                    width: 99%;">FORMULIR ANALISA KEUANGAN (FAK)</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblheader1" runat="server" Style="font-size: 14px; font-weight: bold;">A. Analisa Keuangan Historis</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; background-color: #F6F5F0; border: 1px solid #000;">
                <asp:Literal ID="litkeungan1" runat="server"></asp:Literal>
                <asp:Label ID="lblAudtor" runat="server" Style="font-size: 12px; font-weight: bold;
                    text-align: center; width: 32%;">Auditor</asp:Label>
                <input type="text" name="txtauditor" id="txtauditor" runat="server" style="width: 301px;
                    margin-left: 6px; background: White;" class="inpTxt2" />
                <div>
                    <asp:Label ID="lblopini" runat="server" valign="top" Style="font-size: 12px; font-weight: bold;">Opini</asp:Label>
                    <textarea id="txtopini" name="txtopini" runat="server" rows="3" cols="40"></textarea></div>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:Label ID="Label1" runat="server" Style="font-size: 12px; font-weight: bold;
                    text-align: center;">IKHTISAR KEUANGAN</asp:Label>
                <asp:Literal ID="litkeungan2" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <textarea id="txtmsg1" name="txtmsg1" runat="server" rows="7" cols="185" class="iWdw"></textarea>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <textarea id="txtmsg2" name="txtmsg2" runat="server" rows="2" cols="185" class="iWdw"></textarea>
                <asp:Literal ID="litkeungan3" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <textarea id="txtmsg3" name="txtmsg3" runat="server" style="text-align: justify"
                    rows="5" cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblheader2" runat="server" Style="font-size: 14px; font-weight: bold;">Pernyataan Laba Rugi</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:Label ID="lblpernyataanlaba" runat="server" Style="font-size: 12px; width: 99%;
                    font-weight: bold; text-align: center;">PERNYATAAN LABA/RUGI</asp:Label>
                <asp:Literal ID="litkeungan4" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <textarea id="txtmsg4" name="txtmsg4" runat="server" style="text-align: justify"
                    rows="9" cols="185" class="iWdw"></textarea><br />
                <br />
                <%-- <asp:Literal ID="litkeungan5" runat="server"></asp:Literal>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblheader3" runat="server" Style="font-size: 14px; font-weight: bold;">Laporan Posisi Keuangan</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblneraca" runat="server" Style="font-size: 12px; font-weight: bold;">NERACA</asp:Label>
                <asp:Literal ID="litkeungan6" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <textarea id="txtmsg5" name="txtmsg5" runat="server" rows="8" cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblheader5" runat="server" Style="font-size: 14px; font-weight: bold;">Rekonsiliasi Aktiva Tetap</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblheader6" runat="server" Style="font-size: 12px; font-weight: bold;
                    text-align: center">REKONSILIASI HARTA TETAP</asp:Label>
                <asp:Literal ID="litkeungan7" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <textarea id="txtmsg6" name="txtmsg6" runat="server" rows="4" cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblheader8" runat="server" Style="font-size: 14px; font-weight: bold;">a. Arus Kas</asp:Label>
                <asp:Literal ID="litkeungan8" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <textarea id="txtmsg7" name="txtmsg7" runat="server" rows="7 " cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblheader9" runat="server" Style="font-size: 14px; font-weight: bold;"><u>Proyeksi Keuangan</u></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblheader10" runat="server" Style="font-size: 14px; font-weight: bold;
                    margin-left: 1cm"> </asp:Label>
                <asp:Literal ID="litkeungan9" runat="server"></asp:Literal><br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblheader11" runat="server" Style="font-size: 14px; font-weight: bold;">Proyeksi Ikhtisar Keuangan</asp:Label>
                <textarea id="txtmsg8" name="txtmsg8" runat="server" rows="2" cols="185" class="iWdw"></textarea>
                <asp:Literal ID="litkeungan10" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <textarea id="txtmsg9" name="txtmsg9" runat="server" rows="10" cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblheader12" runat="server" Style="font-size: 14px; font-weight: bold;">Proyeksi Laba Rugi</asp:Label>
                <asp:Literal ID="litkeungan11" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <textarea id="txtmsg10" name="txtmsg10" runat="server" rows="7" cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblheader13" runat="server" Style="font-size: 14px; font-weight: bold;">Proyeksi Laporan Posisi Keuangan </asp:Label>
                <asp:Literal ID="litkeungan12" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <textarea id="txtmsg11" name="txtmsg11" runat="server" rows="6" cols="185" class="iWdw"></textarea><br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 90%">
                <asp:Label ID="lblheader14" runat="server" Style="font-size: 14px; font-weight: bold;">Proyeksi Cash Flow</asp:Label>
                <asp:Literal ID="litkeungan13" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <textarea id="txtmsg12" name="txtmsg12" runat="server" rows="5" cols="185" class="iWdw"></textarea>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Literal ID="litSignature" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <div style="margin-left: 85%">
        <%-- <asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
        <asp:Button Text="Save & Export" ID="btnExport" CssClass="inpBtn" runat="server"
            Visible="false" />
        <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server"
            Visible="false" OnClientClick="ex();return false;" />
    </div>
    <div id="editor">
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        $("tbody").find(".valratio").each(function () {
            var valData = $(this).text();
            if (valData === "n/a") {
                valData = "";
            } else {
                valData = idxwidget.convertmoneywithdigits(valData, 2);
            }
            $(this).text(valData);
        });
        //        $.each(yourArray, function (key, value) {
        //            (value + "").replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
        //            console.log((value + "").replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
        //        });
    });

    function convertmoneywithdigits(data1, data2) {
        var valData = parseFloat(data1).toFixed(data2);
        valData = (valData + "").replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
        return valData;
    }

</script>
