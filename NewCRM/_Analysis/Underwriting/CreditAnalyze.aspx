<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreditAnalyze.aspx.vb"
    Inherits="NewCRM.CreditAnalyze" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/textarea/tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        aBc("General");
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
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
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,|,bullist,numlist,|,undo,redo,|,insertdate,inserttime,|,forecolor,backcolor",

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
        .notif
        {
            margin-bottom: 10px;
            border: 1px solid #bcbcbc;
            border-radius: 5px;
        }
        .notif-header
        {
            border-top-right-radius: 5px;
            border-top-left-radius: 5px;
            font-size: 12px;
            background: #007cc2;
            color: #fff;
        }
        .notif-body
        {
            border-bottom-right-radius: 5px;
            border-bottom-left-radius: 5px;
            background: #ebecec;
            padding: 5px;
            font-size: 12px;
        }
        a.notif-close
        {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            width: 1%;
        }
        .aactive
        {
            background-color: transparent;
            border-radius: 11px;
            -moz-border-radius: 11px;
            -webkit-border-radius: 11px;
            color: #fff;
            background-color: #ebebeb;
            border: thin solid #cccccc;
        }
        td
        {
            padding-left: 5px;
        }
        .tbl
        {
            font-family: "Century Gothic" , "Helvetica Neue" , "Lucida Grande" , "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
            font-size: 11px;
        }
        .spanChild
        {
            margin-left: 10px;
        }
        .tb-header
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
            text-transform: uppercase;
            font-weight: bold;
        }
        #ui-datepicker-div
        {
            display: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 100%">
        <div id='cssmenu' style="margin-bottom: 20px;">
            <ul style="float: left; margin-right: 10px;">
                <%  Dim html As String = ""
                    If dataTable.Rows.Count > 0 Then
                        For i As Integer = 0 To dataTable.Rows.Count - 1
                            If Session("appno") IsNot Nothing Then
                                If dtWorkflow.Rows.Count > 0 Then
                                    For j As Integer = 0 To dtWorkflow.Rows.Count - 1
                                        If dataTable.Rows(i).Item("lsm_field") = dtWorkflow.Rows(j).Item("lwr_field") Then
                                            If dtWorkflow.Rows(j).Item("lwr_role") <> "HD" Then
                                                If dataTable.Rows(i).Item("lsm_parent") = 0 Then
                                                    If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                                        html &= "<li><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                    Else
                                                        html &= "<li><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                    End If
                                                    
                                                Else
                                                    If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                                        html &= "<li id='c" & dataTable.Rows(i).Item("lsm_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                    Else
                                                        html &= "<li id='c" & dataTable.Rows(i).Item("lsm_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                    End If
                                                End If
                                            Else
                                                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                                    If appno.Value <> "" Then
                                                        Response.Redirect(menuNext & "?appno=" & appno.Value)
                                                    Else
                                                        Response.Redirect(menuNext)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                    If dataTable.Rows(i).Item("lsm_field") = "parent" Then
                                        html &= checkChildHD(dataTable.Rows(i).Item("lsm_position"), i)
                                        
                                        html &= getMenuChild(dataTable.Rows(i).Item("lsm_position"))
                                    End If
                                Else
                                    If dataTable.Rows(i).Item("lsm_parent") = 0 Then
                                        If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                            html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        Else
                                            html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        End If
                                        
                                        If dataTable.Rows(i).Item("lsm_field") = "parent" Then
                                            html &= getMenuChild(dataTable.Rows(i).Item("lsm_id"))
                                        End If
                                    Else
                                        If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                            html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        Else
                                            html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        End If
                                    End If
                                End If
                            Else
                                If dataTable.Rows(i).Item("lsm_parent") = 0 Then
                                    If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                        html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    Else
                                        html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    End If
                                        
                                    If dataTable.Rows(i).Item("lsm_field") = "parent" Then
                                        html &= getMenuChild(dataTable.Rows(i).Item("lsm_id"))
                                    End If
                                Else
                                    If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                        html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    Else
                                        html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    End If
                                End If
                            End If
                        Next
                    End If
                %>
                <%=html %>
            </ul>
            <div class="bodyContent">
                <div class="cont-header" stlye="width:100%;">
                    <table width="100%" border="0">
                        <tr>
                            <td width="20%">
                                <span><b>No. Aplikasi</b></span>
                            </td>
                            <td>
                                <asp:Label ID="lblAppNo_header" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><b>Nama Pemohon</b></span>
                            </td>
                            <td>
                                <asp:Label ID="lblBrwName_header" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
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
                <div>
                    <table class="tbl" cellspacing="0" cellpadding="3" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0; ">
                        <tr>
                            <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#ddedf6" class="tb-header">DATA PEMOHON</td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>Upload File Report MAK</b>
                            </td>
                            <td width="70%">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <br />
                                <span>Hanya File PDF yang boleh di upload.</span><br />
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>&nbsp;</b>
                            </td>
                            <td>
                               <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>&nbsp;</b>
                            </td>
                            <td>
                               <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="inpBtn"></asp:Button>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn">
                            </asp:Button>
                            <asp:Button ID="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:Button>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">* <span style="font-size: 13px;">
                                Harus Diisi</span></span>
                        </td>
                    </tr>
                    </table>
                    
                </div>
                <div style="display:none;"> 
                <table class="tbl" cellspacing="0" cellpadding="3" width="100%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px 5px 0 0;">
                    <tr>
                        <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#ddedf6" class="tb-header">
                            DATA PEMOHON
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="30%">
                            <b>Nama Perusahaan</b>
                        </td>
                        <td>
                            <asp:Label ID="lblNamaPerusahaan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Grup Perusahaan</b>
                        </td>
                        <td>
                            <asp:Label ID="lblGrupPerusahaan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Alamat Perusahaan</b>
                        </td>
                        <td>
                            <asp:Label ID="lblAlamatPerusahaan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Bidang Usaha</b>
                        </td>
                        <td>
                            <asp:Label ID="lblBidangUsaha" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Jenis PAK</b>
                        </td>
                        <td>
                            <asp:Label ID="lblJenisPAK" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Tujuan PAK</b>
                        </td>
                        <td>
                            <asp:Label ID="lblTujuanPAK" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="tbl" cellspacing="0" cellpadding="2" width="100%" style="text-align: left;
                    border: thin solid #bcbcbc; border-radius: 0 0 5px 5px;">
                    <tr>
                        <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#ddedf6" class="tb-header">
                            ANALISA KREDIT
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="30%">
                            <b>Aspek Umum dan Manajemen</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAspekUmumMgmt" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Hubungan dengan Bank</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHubunganBank" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Aspek Pemasaran</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAspekPemasaran" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Aspek Teknis dan Produksi/Pembelian</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAspekTeknisProduksiPembelian" runat="server" CssClass="inpTxt"
                                TextMode="MultiLine" Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Aspek Keuangan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAspekKeuangan" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Kelemahan dan Risiko</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKelemahanDanRisiko" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Analisa Kebutuhan Kredit</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnalisaKebutuhanKredit" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Evaluasi Jaminan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEvaluasiJaminan" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Kesimpulan dan Pendapat Hasil Analisa Kredit</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKesimpulan" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr style="height: 30px">
                        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn">
                            </asp:Button>
                            <asp:Button ID="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:Button>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">* <span style="font-size: 13px;">
                                Harus Diisi</span></span>
                        </td>
                    </tr>--%>
                </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
