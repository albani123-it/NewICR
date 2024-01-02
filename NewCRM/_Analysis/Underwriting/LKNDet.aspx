<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LKNDet.aspx.vb" Inherits="NewCRM.LKNDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="../../Scripts/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <%--<script type="text/javascript" src="../../Scripts/tinymce/jscripts/tiny_mce/tiny_mce.js"></script>--%>
    <style type="text/css">
        td
        {
            padding-left: 5px;
        }
        
        input
        {
            font-family: "Century Gothic";
        }
        .inpTxt
        {
            width: 120px;
        }
        #ui-datepicker-div
        {
            display: none;
        }
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
        }
        
        #tblLKNDtl
        {
            width: 98%;
            text-align: left;
            border: thin solid #bcbcbc;
            border-radius: 5px;
        }
        
        #tblLKNDtl tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblLKNDtl tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        #btnAddNew
        {
            display: block;
        }
        
        #tblFotoLKNInput
        {
            width: 100%;
            display: none;
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
        }
        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num);
        }

        function OnBeforeDelete(record) {
            if (confirm("Apakah Anda yakin ingin menghapus?") == false) {
                return false;
            }
            return true;
        }
        function OnDelete(record) {
            alert("Data berhasil dihapus.");
        }
        function OnBeforeEdit(record) {
//            PageMethods.getDataWalledShareById(record.lsld_id, Succ, Fail);
            $('#txtIdDtl').val(record.lsld_id);
            $('#txtFilename').val(record.lsld_foto_kunjungan);
            $('#txtInfoFoto').val(record.lsld_keterangan_foto);

            $('#tblFotoLKNInput').css('display','block');
            $('#btnAddNew').css('display','none');
            return false;
        }

        function clearData(){
            $('#txtIdDtl').val(0)
            $('#txtFilename').val('')
            $('#txtInfoFoto').val('')
        }

        function callReadOnly(){
//            console.log('callfunction');
             $('#btnAddNew').prop( "disabled", true );
        }

        $(document).ready(function(){
            $("#InpTanggal").datepicker(
            { duration: '',
                yearRange: "-100:+20",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });

             $('#btnAddNew').click(function(){
                $('#tblFotoLKNInput').css('display','block');
                $('#btnAddNew').css('display','none');
                clearData();
            });
        });

        function test(){
        console.log("text");
        }

        tinyMCE.init({
            // General options 
            mode: "textareas",
            theme: "advanced",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave",
            setup: function (ed) {
                ed.onKeyPress.add(
function (ed, evt) {
}
);
            },
            // Theme options 
            theme_advanced_buttons1: "bold,italic,underline,strikethrough",
            theme_advanced_buttons2: "cut,copy,paste,|,bullist,numlist,|,undo,redo",

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
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="appno" runat="server" />
    <div>
        <center>
            <div id="divnotif" runat="server" class="notif" style="display: none;">
                <div class="notif-header" width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <span><b>Notifikasi</b></span>
                            </td>
                            <td align="right">
                                <a class="notif-close" onclick="document.getElementById('<%=divnotif.clientID %>').style.display='none'"
                                    title="Close">X</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="notif-body">
                    <asp:Label ID="lblNotif" runat="server" Style="color: Red;"></asp:Label>
                </div>
            </div>
            <table id="tblLKNDtl" class="tbl" cellspacing="0" cellpadding="3">
                <tr>
                    <td colspan="4" class="detail" style="border-radius: 5px 5px 0 0;">
                        <span style="font-size: 14px; font-weight: bolder">Laporan Kunjungan Setempat Detail</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="display: none">
                        <asp:TextBox ID="txtIdLkn" runat="server" CssClass="inpTxt">-</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <b>Nama Yang Di Kontak</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="InpNama" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                    <td width="20%">
                        <b>Tanggal Kontak</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="InpTanggal" runat="server" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Bentuk Pelaksanaan Kontak</b>
                    </td>
                    <td>
                        <asp:TextBox ID="InpBentuk" runat="server" CssClass="inpTxt"></asp:TextBox>
                    </td>
                    <td>
                        <b>Jabatan Yang Di Kontak</b>
                    </td>
                    <td>
                        <asp:TextBox ID="InpJabatan" runat="server" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Tujuan Kontak</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="InpTujuan" runat="server"  Width="400px" Rows="3" TextMode="multiline" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Hasil Kontak</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="InpHasil" runat="server" Rows="3" TextMode="multiline" Width="400px" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Tindak Lanjut</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="InpTindak" runat="server" Rows="3" TextMode="multiline" Width="400px" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Alamat</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtAlamat" runat="server" Rows="3" TextMode="multiline" Width="400px"
                            CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Hasil Kunjungan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtHasilKunjungan" runat="server" Rows="3" TextMode="multiline"
                            Width="400px" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="detail" style="">
                        <span style="font-size: 14px; font-weight: bolder">Foto Kunjungan</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="width: 100%">
                        <table id="tblFotoLKN" width="100%" class="tbl" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <obout:Grid ID="grid1" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                        EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowAddingRecords="false"
                                        AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" />
                                        <Columns>
                                            <obout:Column ID="Column1" runat="server" DataField="lsld_id" Visible="false" />
                                            <obout:Column ID="Column2" runat="server" DataField="lsld_lsl_id" Visible="false" />
                                            <obout:Column ID="Column3" runat="server" DataField="lsld_foto_kunjungan" HeaderText="Foto Kunjungan"
                                                ShowFilterCriterias="false" Width="15%">
                                                <TemplateSettings RowEditTemplateControlId="txtFotoKunjungan" RowEditTemplateControlPropertyName="value"
                                                    FilterTemplateId="FotoKunjungan" />
                                                <FilterOptions>
                                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                                </FilterOptions>
                                            </obout:Column>
                                            <obout:Column ID="Column4" runat="server" DataField="lsld_keterangan_foto" HeaderText="Keterangan Foto"
                                                ShowFilterCriterias="false" Width="15%">
                                                <TemplateSettings RowEditTemplateControlId="txtKeteranganFoto" RowEditTemplateControlPropertyName="value"
                                                    FilterTemplateId="KeteranganFoto" />
                                                <FilterOptions>
                                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                                </FilterOptions>
                                            </obout:Column>
                                            <obout:Column ID="Column7" runat="server" HeaderText="Action" Width="5%" AllowEdit="true">
                                            </obout:Column>
                                            <obout:Column ID="Column8" runat="server" Width="10%">
                                                <TemplateSettings TemplateId="deleteTemplate" />
                                            </obout:Column>
                                        </Columns>
                                        <Templates>
                                            <obout:GridTemplate runat="server" ID="deleteTemplate">
                                                <Template>
                                                 <% If allowDelete = True Then%>
                                                    <asp:LinkButton ID="delDtl" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain you want to delete?');"
                                                        CommandArgument='<%# Container.PageRecordIndex %>' class='ob_gAL'>  Delete  </asp:LinkButton>
                                                 <%End If%>
                                                 &nbsp;
                                                 <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="download" CommandArgument='<%# Container.DataItem("lsld_foto_kunjungan") %>' class='ob_gAL'>  Download  </asp:LinkButton>
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
                                    </obout:Grid>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" id="btnAddNew" value="Tambah Baru" class="inpBtn" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" id="tblFotoLKNInput">
                                        <tr>
                                            <td colspan="3" style="display: none">
                                                <asp:TextBox ID="txtIdDtl" runat="server" CssClass="inpTxt">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%">
                                                <b>Foto Kunjungan</b>
                                            </td>
                                            <td width="80%">
                                                <asp:Image />
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                <br />
                                                <span>Ukuran File maksimum 200 KB.</span><br />
                                            </td>
                                            <td width="5%" style="display: none">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Nama File</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFilename" Width="200px" ReadOnly="true" runat="server" CssClass="inpTxt"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Keterangan Foto</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInfoFoto" runat="server" Width="200px" CssClass="inpTxt"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>&nbsp;</b>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" Text="Simpan & Upload" runat="server" CssClass="inpBtn" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr style="height: 30px">
                    <td background="../../styles/btn_bank/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="inpBtn" di></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Closed" CssClass="inpBtn" OnClientClick="CloseMe()"
                            CausesValidation="false"></asp:Button>&nbsp; <span style="color: Red; font-size: 11pt;
                                font-weight: bold;">* <span style="font-size: 13px;">Harus Diisi</span></span>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
