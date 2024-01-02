<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IkhtisarPengusulanKredit.aspx.vb"
    Inherits="NewCRM.IkhtisarPengusulanKredit" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.hotkeys.js"></script>
     <script src="../Scripts/textarea/tinymce/jscripts/tiny_mce/tiny_mce_src.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        tinyMCE.init({
            mode: "specific_textareas",
            editor_selector: "myTextEditor",
            theme: "advanced",
            plugins: "safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,blockquote,|,undo,redo,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: false
        });
    </script>--%>
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
</head>
<body>
    <form id="form1" runat="server">
    <table style="clear: both; margin: 0 auto; padding: 0; width: 80%; text-align: center">
        <tr>
            <td style="text-align: center;">
                <asp:Label ID="lbljudul1" runat="server" Style="font-weight: bold; font-size: 14px;
                    width: 99%;">IKHTISAR PENGUSULAN KREDIT </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litIPK1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litIPK2" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litIPK3" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litIPK4" runat="server"></asp:Literal>
                <asp:Literal ID="litPerofilRisiko" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litIPK5" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; border: 1px solid #000;">
                <asp:Literal ID="litIPK6" runat="server"></asp:Literal>
                <div style="width: 100%">
                    <asp:Label ID="lblAudtor" runat="server" Style="height:25px;font-size: 12px; padding-left: 4px;
                        font-style: italic; text-align: center;">Audit Report</asp:Label>
                    <input type="text" name="txtaudit" id="txtaudit" runat="server" style="width: 200px;
                        border-bottom-style: solid; border-bottom-color: inherit; border-bottom-width: 1px;
                        margin-left: 431px; padding-left: 4px;" class="inpTxt2" />
                </div>
                <div style="width: 100%;">
                    <asp:Label ID="lblappraisal" runat="server" Style="height:25px;font-size: 12px; padding-left: 4px;
                        font-style: italic; text-align: center">Appraisel Report</asp:Label>
                    <input type="text" name="txtAppraisel" id="txtAppraisel" runat="server" style="width: 200px;
                        border-bottom-style: solid; border-bottom-color: inherit; border-bottom-width: 1px;
                        margin-left: 408px; padding-left: 4px;" class="inpTxt2" />
                </div>
                <div style="width: 100%;">
                    <asp:Label ID="lblmargin" runat="server" Style="height:25px;font-size: 12px; padding-left: 4px;
                        font-style: italic; text-align: center">Marginal Deposit</asp:Label>
                    <input type="text" name="txtmarginal" id="txtmarginal" runat="server" style="width: 200px;
                        border-bottom-style: solid; border-bottom-color: inherit; border-bottom-width: 1px;
                        margin-left: 406px; padding-left: 4px;" class="inpTxt2" />
                </div>
                <div style="width: 100%;">
                    <asp:Label ID="lblother" runat="server" Style="height:25px;font-size: 12px; padding-left: 4px;
                        font-style: italic; text-align: center">Other</asp:Label>
                    <input type="text" name="txtother" id="txtother" runat="server" style="width: 200px;
                        border-bottom-style: solid; border-bottom-color: inherit; border-bottom-width: 1px;
                        margin-left: 471px; padding-left: 4px;" class="inpTxt2" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; border: 1px solid #000">
                <asp:Literal ID="litIPK7" runat="server"></asp:Literal>
                <div style="width: 100%;">
                    <asp:TextBox ID="txtpejabat" name="txtpejabat" runat="server" TextMode="MultiLine"
                        Rows="3" Width="25%" CssClass="inpTxt" Style="margin-left: 7px"> </asp:TextBox>
                    <asp:TextBox ID="txtkomite" name="txtkomite" runat="server" TextMode="MultiLine"
                        Rows="3" Width="25%" CssClass="inpTxt" Style="margin-left: 232px"> </asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litIPK8" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblKMK1" runat="server" Style="height:25px;font-size: 14px; font-weight: bold;
                    text-align: center;">A.	KETENTUAN KMK I</asp:Label>
                <asp:Literal ID="litKetentuanKMK1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="litKetentuanKMKAgunan" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="litKetentuanKMKFixed" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="litKetentuanKMKFixedTunai" runat="server"></asp:Literal>
            </td>
        </tr>
        <%-- <tr>
            <td style="text-align:left;">
            <asp:Label ID="lblKMK2" runat="server" style="font-size:14px;font-weight:bold;text-align:center;">B.	KETENTUAN KMK II</asp:Label>
             <asp:Literal ID="litKetentuanKMK2" runat="server"></asp:Literal>
            </td>
            </tr>--%>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblSPPK" runat="server" Style="height:25px;font-size: 14px; font-weight: bold;
                    text-align: center;">C.	SYARAT PENANDATANGANAN PERJANJIAN KREDIT</asp:Label>
                <asp:TextBox ID="txtPerjanjianKredit" name="txtPerjanjianKredit" runat="server" TextMode="MultiLine"
                    Rows="10" Width="98%" CssClass="myTextEditor" Style="margin-left: 18px;"> </asp:TextBox>
                <%--<textarea id=""   runat="server" rows="2" cols="185"  class="iWdw"></textarea>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblkmk" runat="server" Style="height:25px;font-size: 14px; font-weight: bold;
                    text-align: center;">D.	SYARAT EFEKTIF KMK I</asp:Label>
                <asp:TextBox ID="txtEfektifKMK1" name="txtEfektifKMK1" runat="server" TextMode="MultiLine"
                    Rows="10" Width="98%" CssClass="myTextEditor" Style="margin-left: 18px;"> </asp:TextBox>
                <%--<textarea id="txtEfektifKMK1" name="txtEfektifKMK1"  runat="server" rows="2" cols="185"  class="iWdw"></textarea>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblpenariknKMK" runat="server" Style="height:25px;font-size: 14px; font-weight: bold;
                    text-align: center;">E.	SYARAT PENARIKAN KMK II </asp:Label>
                <asp:TextBox ID="txtPenarikanKMK2" name="txtPenarikanKMK2" runat="server" TextMode="MultiLine"
                    Rows="10" Width="98%" CssClass="myTextEditor" Style="margin-left: 18px;"> </asp:TextBox>
                <%--<textarea id="txtPenarikanKMK2" name="txtPenarikanKMK2"  runat="server" rows="3" cols="185"  class="iWdw"></textarea>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblsyaratSwitchable" runat="server" Style="height:25px;font-size: 14px; font-weight: bold;
                    text-align: center;">F.	SYARAT SWITCHABLE  FASILITAS KMK </asp:Label>
                <asp:TextBox ID="txtFasilitasKMK" name="txtFasilitasKMK" runat="server" TextMode="MultiLine"
                    Rows="10" Width="98%" CssClass="myTextEditor" Style="margin-left: 18px;"> </asp:TextBox>
                <%--<textarea id="txtFasilitasKMK" name="txtFasilitasKMK"  runat="server" rows="3" cols="185"  class="iWdw"></textarea>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Label ID="lblSyarat" runat="server" Style="height:25px;font-size: 14px; font-weight: bold;
                    text-align: center;">G.	SYARAT LAINNYA</asp:Label>
                <asp:TextBox ID="txtSyaratLainnya" name="txtSyaratLainnya" runat="server" TextMode="MultiLine"
                    Rows="10" Width="98%" CssClass="myTextEditor" Style="margin-left: 18px;"> </asp:TextBox>
                <%--<textarea id="txtSyaratLainnya" name="txtSyaratLainnya"  runat="server" rows="5" cols="185"  class="iWdw"></textarea>--%>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Literal ID="litSignature" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <div style="margin-left: 82%">
        <%-- <asp:DropDownList ID="ddlExportType" runat="server" CssClass="inpDdl" style="height:22px;margin:0 5px;" visible="false"></asp:DropDownList>--%>
        <asp:Button Text="Save & Export" ID="btnExport" CssClass="inpBtn" runat="server"
            Visible="false" />
        <asp:Button Text="Export Word or Excel" ID="btnExport2" CssClass="inpBtn" runat="server"
            Visible="false" OnClientClick="ex();return false;" />
    </div>
    </form>
</body>
</html>
