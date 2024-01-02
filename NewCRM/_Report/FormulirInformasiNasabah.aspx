<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FormulirInformasiNasabah.aspx.vb"
    Inherits="NewCRM.FormulirInformasiNasabah" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.hotkeys.js"></script>
     <script src="../Scripts/textarea/tinymce/jscripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
    <script type="text/javascript">
        function printEx() {
            var doc = new jsPDF();
            doc.fromHTML($('#content').get(0), 15, 15, {
                'width': 500,
                'elementHandlers': specialElementHandlers
            });
            doc.save('sample-file.pdf');
        }

        // di panggil 
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
    <style>
        *
        {
            margin: 0;
            padding: 0;
        }
        
        .tree ul
        {
            padding-top: 20px;
            position: relative;
            transition: all 0.5s;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
        }
        
        .tree li
        {
            float: left;
            text-align: center;
            list-style-type: none;
            position: relative;
            padding: 20px 5px 0 5px;
            transition: all 0.5s;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
        }
        
        /*We will use ::before and ::after to draw the connectors*/
        
        .tree li::before, .tree li::after
        {
            content: '';
            position: absolute;
            top: 0;
            right: 50%;
            border-top: 1px solid #ccc;
            width: 50%;
            height: 20px;
        }
        .tree li::after
        {
            right: auto;
            left: 50%;
            border-left: 1px solid #ccc;
        }
        
        /*We need to remove left-right connectors from elements without 
        any siblings*/
        .tree li:only-child::after, .tree li:only-child::before
        {
            display: none;
        }
        
        /*Remove space from the top of single children*/
        .tree li:only-child
        {
            padding-top: 0;
        }
        
        /*Remove left connector from first child and 
        right connector from last child*/
        .tree li:first-child::before, .tree li:last-child::after
        {
            border: 0 none;
        }
        /*Adding back the vertical connector to the last nodes*/
        .tree li:last-child::before
        {
            border-right: 1px solid #ccc;
            border-radius: 0 5px 0 0;
            -webkit-border-radius: 0 5px 0 0;
            -moz-border-radius: 0 5px 0 0;
        }
        .tree li:first-child::after
        {
            border-radius: 5px 0 0 0;
            -webkit-border-radius: 5px 0 0 0;
            -moz-border-radius: 5px 0 0 0;
        }
        
        /*Time to add downward connectors from parents*/
        .tree ul ul::before
        {
            content: '';
            position: absolute;
            top: 0;
            left: 50%;
            border-left: 1px solid #ccc;
            width: 0;
            height: 20px;
        }
        
        .tree li a
        {
            border: 1px solid #ccc;
            padding: 20px 30px;
            text-decoration: none;
            color: #666;
            font-family: arial, verdana, tahoma;
            font-size: 15px;
            display: inline-block;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            transition: all 0.5s;
            -webkit-transition: all 0.5s;
            -moz-transition: all 0.5s;
        }
        
        .tree li a:hover, .tree li a:hover + ul li a
        {
            background: #c8e4f8;
            color: #000;
            border: 1px solid #94a0b4;
        }
        
        /*Connector styles on hover*/
        .tree li a:hover + ul li::after, .tree li a:hover + ul li::before, .tree li a:hover + ul::before, .tree li a:hover + ul ul::before
        {
            border-color: #94a0b4;
        }
        
        .style1
        {
            height: 52px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="clear: both; margin: 0 auto; padding: 0; width: 98%">
        <div>
            <table runat="server" id="tblc" style="border: 1px solid #000; clear: both; margin: 0 auto;
                padding: 0; width: 80%;">
                <tr>
                    <td>
                        <div style="text-align: center;">
                            <asp:Label ID="lbljudul" runat="server" Style="height:30px;font-weight: bold; font-size: 14px;
                                width: 99%;">FORMULIR INFORMASI NASABAH</asp:Label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Literal ID="ltrlFIN" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%; font-size: 13px; font-weight: bold;">
                        <strong>1. LATAR BELAKANG USAHA DEBITUR</strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:TextBox ID="txaLatarBelakang" name="txaLatarBelakang" runat="server" TextMode="MultiLine"
                            Rows="11" Width="98%" Style="text-align: justify;" CssClass="myTextEditor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%; font-size: 13px; font-weight: bold;">
                        <span><strong>2. LEGALITAS / PERIZINAN USAHA</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <span style="height:30px;margin-left: 12px; font-size: 13px; font-weight: bold;"><strong>Legalitas
                            Usaha</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%;" align="center">
                        <asp:Literal ID="ltrlLegalitasUsaha" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <span style="height:30px;margin-left: 13px; font-size: 12px; font-weight: bold;"><strong>Legalitas
                            Permohonan Kredit</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:TextBox ID="txaLegalitasUsaha" name="txaLegalitasUsaha" runat="server" Style="text-align: justify"
                            TextMode="MultiLine" Rows="11" Width="98%" CssClass="myTextEditor"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%; font-size: 13px; font-weight: bold;">
                        <span><strong>3. KEPEMILIKAN PERUSAHAAN</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <span style="height:30px;margin-left: 12px; font-size: 13px;">3.1 Pemegang Saham</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:TextBox ID="txtpemegangsaham" name="txtpemegangsaham" runat="server" Style="text-align: justify"
                            TextMode="MultiLine" Rows="2" Width="98%" CssClass="myTextEditor"> </asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Literal ID="ltrlKepemilikanPerusahaan" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%;">
                        <span style="margin-left: 12px; font-size: 13px;">3.2 Key Person</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <asp:Literal ID="litkeyperson" runat="server"></asp:Literal>
                        <%--<asp:Label runat="server" ID="keyPersonName" name="keyPersonName" style="margin-left:30px;" text="Name"></asp:Label> &nbsp; (<asp:Label runat="server" ID="keyPersonJabatan" name="keyPersonJabatan" Text="Position"></asp:Label>)--%>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%; font-size: 13px; font-weight: bold">
                        <span><strong>4. PENGURUS DAN PENILAIAN MANAJEMEN</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%;">
                        <span style="margin-left: 12px; font-size: 13px;">4.1 Susunan Pengurus Perusahaan</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:TextBox ID="txtsusunanpengurus" runat="server" TextMode="MultiLine" Style="text-align: justify"
                            Rows="2" Width="98%" CssClass="myTextEditor"> </asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Literal ID="ltrlPengurusdanPenilaianManajemen" runat="server"></asp:Literal><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:TextBox ID="txtpengursumanagemen" runat="server" TextMode="MultiLine" Style="text-align: justify"
                            Rows="2" Width="98%" CssClass="myTextEditor"> </asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Literal ID="ltrlProfilManajemenPerusahaan" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="height:30px;width: 80%;">
                        <span style="margin-left: 12px; font-size: 13px">4.2 Penilaian Manajemen</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%;" align="center">
                        <asp:TextBox ID="txaPenilaianManajemen" name="txaPenilaianManajemen" Style="text-align: justify"
                            runat="server" TextMode="MultiLine" Rows="6" Width="98%" CssClass="myTextEditor"> </asp:TextBox></span>
                    </td>
                </tr>

                     <tr>
                    <td style="width: 80%;" align="center">
                        <asp:Literal ID="litSignature" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div style="margin-left: 83%">
                <asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" Style="height: 22px;
                    margin: 0 5px;" Visible="false">
                </asp:DropDownList>
                <asp:Button Text="Save & Export" ID="btnExport" CssClass="inpBtn" Style="margin-right: 15px"
                    runat="server" Visible="true" />
                <%--<asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server" Visible="true" OnClientClick="ex();return false;" />--%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
