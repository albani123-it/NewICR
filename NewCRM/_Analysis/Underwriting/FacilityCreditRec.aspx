<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FacilityCreditRec.aspx.vb"
    Inherits="NewCRM.FacilityCreditRec" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
            }
            else{
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

//        function fillNilai(){
//            var a = document.getElementById("<%= txtMaksimumKreditRec.ClientID  %>").value.replace(/,/g,"");
//            var b = document.getElementById("<%= txtNilaiValas.ClientID  %>").value.replace(/,/g,"");
//            
//            var total = Math.floor(a / b);
//            if (isFinite(total)){
//                document.getElementById("<%= txtMaxKredit.ClientID  %>").value = formatCurrency(total);
//            }
//            else{
//                document.getElementById("<%= txtMaxKredit.ClientID  %>").value = 0;
//            }
////            checkSifatKredit();
//        }

//       
    </script>
    <style type="text/css">
        .inpTxt
        {
            width: 120px;
        }
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
        #ui-datepicker-div
        {
            display: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
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
                        <asp:Label ID="lblNotif" runat="server" Style="color: Red;" value=""></asp:Label>
                    </div>
                </div>
                <div id="uploadipk">
                <table id="Table1" runat="server" class="tbl" cellspacing="0" cellpadding="3" width="100%"
                        style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px; margin-bottom: 20px;">
                        <tr>
                            <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;
                                color: #fff; font-weight: bold;">
                                PERMOHONAN KREDIT YANG DIMINTA
                            </td>
                        </tr>
                         <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>Upload File Report IPK</b>
                            </td>
                            <td width="70%">
                                <asp:FileUpload ID="FileUpload2" runat="server" />
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
                        <%--<td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn">
                            </asp:Button>
                            <asp:Button ID="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:Button>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">* <span style="font-size: 13px;">
                                Harus Diisi</span></span>
                        </td>--%>
                    </tr>
                    </table>
                </div>
                <div style="display: block;">
                    <table id="req" runat="server" class="tbl" cellspacing="0" cellpadding="3" width="100%"
                        style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px; margin-bottom: 20px;">
                        <tr>
                            <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;
                                color: #fff; font-weight: bold;">
                                PERMOHONAN KREDIT YANG DIMINTA
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="20%">
                                <b>Jenis Pengajuan</b>
                            </td>
                            <td width="30%">
                                <asp:Label ID="lblJenisPengajuanReq" runat="server"></asp:Label>
                            </td>
                            <td width="20%">
                                <b>Grace Period</b>
                            </td>
                            <td width="30%">
                                <asp:Label ID="lblGracePeriodReq" runat="server"></asp:Label>&nbsp;Bulan
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Jenis Kredit</b>
                            </td>
                            <td>
                                <asp:Label ID="lblJenisKreditReq" runat="server"></asp:Label>
                            </td>
                            <td>
                                <b>Suku Bunga</b>
                            </td>
                            <td>
                                <asp:Label ID="lblSukuBungaReq" runat="server"></asp:Label>&nbsp;%
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Jenis Valas</b>
                            </td>
                            <td>
                                <asp:Label ID="lblJenisValasReq" runat="server"></asp:Label>
                            </td>
                            <td>
                                <b>Tipe Bunga</b>
                            </td>
                            <td>
                                <asp:Label ID="lblTipeBungaReq" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Nilai Valas</b>
                            </td>
                            <td>
                                <asp:Label ID="lblNilaiValasReq" runat="server"></asp:Label>
                            </td>
                            <%--<td><b>Total Angsuran Pokok + Bunga</b></td>
                        <td><asp:Label ID="lblTotalAngsuranReq" runat="server"></asp:Label></td>--%>
                            <td>
                                <b>Sifat Kredit</b>
                            </td>
                            <td>
                                <asp:Label ID="lblSifatKreditReq" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Nilai Permohonan (Dalam Valas)</b>
                            </td>
                            <td>
                                <asp:Label ID="lblMaxKreditReq" runat="server"></asp:Label>
                            </td>
                            <td>
                                <b>Jangka Waktu</b>
                            </td>
                            <td>
                                <asp:Label ID="lblJangkaWaktuReq" runat="server"></asp:Label>&nbsp;Bulan
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Nilai Permohonan (Dalam Rupiah)</b>
                            </td>
                            <td>
                                <asp:Label ID="lblNilaiRupiahReq" runat="server"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Sistem Pembayaran</b>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblSistemPembayaranReq" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="tblRating" runat="server" class="tbl" cellspacing="0" cellpadding="3"
                        width="100%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;
                        margin-bottom: 20px;">
                        <tr>
                            <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;
                                font-weight: bold; color: #fff;">
                                INFORMASI RATING
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="20%">
                                <b>Rating Nasabah</b>
                            </td>
                            <td width="30%">
                                <asp:Label ID="lblRatingBrw" runat="server"></asp:Label>
                            </td>
                            <td width="20%">
                                <b>Rating Fasilitas</b>
                            </td>
                            <td width="30%">
                                <asp:Label ID="lblRatingApp" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <%  If history = True And Session("appno") IsNot Nothing Then%>
                    <%= bindHistory(Session("appno"))%>
                    <fieldset id='filAttch' runat="server" style='margin-top: 10px; margin-bottom: 20px;
                        border-radius: 5px; background: #ebecec;'>
                        <legend><b>Lampiran History</b></legend>
                        <obout:Grid ID="grid2" runat="server" Width="100%" CallbackMode="true" Serialize="true"
                            AllowPaging="true" PageSize="10" AutoGenerateColumns="false" EnableRecordHover="true"
                            AllowAddingRecords="false" AllowPageSizeSelection="false" AllowFiltering="true"
                            AllowRecordSelection="false" FolderStyle="~/styles/grid">
                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                            <Columns>
                                <obout:Column ID="Column5" DataField="lrf_id" Visible="false" runat="server" />
                                <obout:Column ID="Column6" DataField="lrf_app_no" Visible="false" runat="server" />
                                <obout:Column ID="Column7" DataField="lrf_file" HeaderText="Nama Dokumen" runat="server"
                                    ShowFilterCriterias="false" Width="70%">
                                    <TemplateSettings RowEditTemplateControlId="txtDoc2" RowEditTemplateControlPropertyName="value"
                                        FilterTemplateId="Doc2Filter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column8" DataField="usr_name" HeaderText="Dilampirkan Oleh" runat="server"
                                    ShowFilterCriterias="false" Width="25%">
                                    <TemplateSettings RowEditTemplateControlId="txtUpdateBy" RowEditTemplateControlPropertyName="value"
                                        FilterTemplateId="UpdateByFilter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column9" HeaderText="Action" runat="server" Width="20%">
                                    <TemplateSettings TemplateId="downloadTemplate" />
                                </obout:Column>
                            </Columns>
                            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                            <Templates>
                                <obout:GridTemplate runat="server" ID="downloadTemplate">
                                    <Template>
                                        <asp:LinkButton ID="LinkButton1" OnCommand="download" runat="server" class='ob_gAL'
                                            CommandArgument='<%# Container.DataItem("lrf_file") %>'>  Download </asp:LinkButton>&nbsp;&nbsp;
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="Doc2Filter" ControlID="Doc">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="Doc2" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter2" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="UpdateByFilter" ControlID="Doc">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="UpdateBy" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter2" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="tplRemoveFilter2">
                                    <Template>
                                        <a onclick="grid2.removeFilter()" class="ob_gALF" style="cursor: pointer">Clear Filter</a>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
                            <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter2" />
                        </obout:Grid>
                    </fieldset>
                    <%End If%>
                    <table class="tbl" id="tblInput" runat="server" cellspacing="0" cellpadding="3" width="100%"
                        style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                        <tr>
                            <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;
                                font-weight: bold; color: #fff;">
                                PERMOHONAN KREDIT YANG DIREKOMENDASIKAN
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="20%">
                                <b>Jenis Pengajuan</b>
                            </td>
                            <td width="30%">
                                <asp:DropDownList ID="ddlJenisPengajuan" runat="server" CssClass="inpDdl">
                                </asp:DropDownList>
                            </td>
                            <td width="20%">
                                <b>Suku Bunga</b>
                            </td>
                            <td width="30%" valign="top">
                                <asp:TextBox ID="txtSukuBunga" runat="server" CssClass="inpTxt" value="0" Style="float: none;
                                    margin-bottom: 5px; width: 30px; text-align: right;" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                &nbsp; %
                                <br />
                                <br />
                                <asp:RadioButtonList ID="RadBunga" Height="10px" RepeatDirection="Horizontal" runat="server"
                                    Style="float: left">
                                    <asp:ListItem Value="Flat" Selected="True">Flat</asp:ListItem>
                                    <asp:ListItem Value="Annuitas">Annuitas</asp:ListItem>
                                    <asp:ListItem Value="Efektif">Efektif</asp:ListItem>
                                </asp:RadioButtonList>
                                <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Jenis Fasilitas</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJenisKredit" runat="server" CssClass="inpDdl">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Sifat Kredit</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSifatKredit" runat="server" CssClass="inpDdl" onchange="checkSifatKredit();">
                                </asp:DropDownList>
                                <span style="color: Red; font-size: 11pt; font-weight: bold;">*</span>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Sistem Pembayaran</b>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSistemPembayaran" CssClass="inpDdl">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Maksimum Tarikan Kredit</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaxTarikanKredit" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>
                                &nbsp;<span style="color: Red; font-size: 11pt; font-weight: bold;">*</span>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Jenis Currency</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJenisValas" runat="server" CssClass="inpDdl" AutoPostBack="true">
                                </asp:DropDownList>
                                <span style="color: Red; font-size: 11pt; font-weight: bold;">*</span>
                            </td>
                            <td>
                                <b>Provisi</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProvisi" runat="server" CssClass="inpTxt" Style="float: none;
                                    width: 30px; text-align: right;" value="0" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Nilai Kurs</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNilaiValas" runat="server" CssClass="inpTxt"></asp:TextBox>
                            </td>
                            <td>
                                <b>Biaya Administrasi</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBiayaAdmin" runat="server" CssClass="inpTxt" Style="text-align: right;"
                                    value="0" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Maksimum Kredit (Dalam Valas)</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaxKredit" runat="server" CssClass="inpTxt" value="0" Style="width: 120px;
                                    text-align: right;" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>
                            </td>
                            <td>
                                <b>Denda Tunggakan</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDenda" runat="server" CssClass="inpTxt" Style="float: none; width: 30px;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr> 
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Maksimum Kredit (Dalam Rupiah)</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMaksimumKreditRec" runat="server" CssClass="inpTxt" Style="float: none; width: 30px;text-align: right;"
                                    value="0" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);fillNilai();"></asp:TextBox>
                                &nbsp;<span style="color: Red; font-size: 11pt; font-weight: bold;">*</span>
                            </td>
                            <td>
                                <b>Usulan</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRecommend" runat="server" CssClass="inpDdl">
                                </asp:DropDownList>
                                <span style="color: Red; font-size: 11pt; font-weight: bold;">*</span>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Jangka Waktu</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJangkaWaktuRec" runat="server" CssClass="inpTxt" value="0" MaxLength="3"
                                    Style="width: 30px; float: none;" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>&nbsp;Bulan
                                &nbsp;<span style="color: Red; font-size: 11pt; font-weight: bold;">*</span>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Grace Period</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGracePeriod" runat="server" CssClass="inpTxt" value="0" MaxLength="2"
                                    Style="width: 30px; float: none;" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>&nbsp;Bulan
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <%--<tr bgcolor="#ebecec" style="display:none">
                        <td><b>Jadwal Pembayaran Pokok Kredit</b></td>
                        <td>
                            <asp:TextBox ID="txtJadwalPembayaranPokokKredit" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b style="display:none">Proyeksi Bunga</b></td>
                        <td>
                            <asp:TextBox ID="txtProyeksiBunga" runat="server" CssClass="inpTxt" style="display:none" value="0"></asp:TextBox>   
                        </td>
                        <td></td>
                        <td></td>
                    </tr>--%>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Pendapat</b>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPendapat" runat="server" CssClass="inpTxt" Width="750px" Height="50px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Pertimbangan</b>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPertimbangan" runat="server" CssClass="inpTxt" Width="750px"
                                    Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Lampiran Dokumen</b>
                            </td>
                            <td colspan="3">
                                <%  If allowUpload = True Then%>
                                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                                <asp:Button ID="btnAdd" Text="Add" runat="server" CssClass="inpBtn" />
                                <br />
                                <span>Ukuran File maksimum 20MB.</span><br />
                                <%Else%>
                                &nbsp;
                                <%End If%>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                <div id="divFileUpload" runat="server">
                                    <obout:Grid ID="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true"
                                        AllowPaging="true" PageSize="10" AutoGenerateColumns="false" EnableRecordHover="true"
                                        AllowAddingRecords="false" AllowPageSizeSelection="false" AllowFiltering="true"
                                        AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                                        <Columns>
                                            <obout:Column ID="Column1" DataField="lrf_id" Visible="false" runat="server" />
                                            <obout:Column ID="Column4" DataField="lrf_app_no" Visible="false" runat="server" />
                                            <obout:Column ID="Column2" DataField="lrf_file" HeaderText="Nama Dokumen" runat="server"
                                                ShowFilterCriterias="false" Width="80%">
                                                <TemplateSettings RowEditTemplateControlId="txtDoc" RowEditTemplateControlPropertyName="value"
                                                    FilterTemplateId="DocFilter" />
                                                <FilterOptions>
                                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                                </FilterOptions>
                                            </obout:Column>
                                            <obout:Column ID="Column3" HeaderText="Action" runat="server" Width="20%">
                                                <TemplateSettings TemplateId="deleteTemplate" />
                                            </obout:Column>
                                        </Columns>
                                        <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                                        <Templates>
                                            <obout:GridTemplate runat="server" ID="deleteTemplate">
                                                <Template>
                                                    <asp:LinkButton ID="LinkButton1" OnCommand="download" runat="server" class='ob_gAL'
                                                        CommandArgument='<%# Container.DataItem("lrf_file") %>'>  Download </asp:LinkButton>&nbsp;&nbsp;
                                                    <%  If allowDelete = True Then%>
                                                    <asp:LinkButton ID="zzz" OnCommand="delete" runat="server" OnClientClick="return confirm('Apakah anda yakin ingin menghapus?');return false;"
                                                        class='ob_gAL' CommandArgument='<%# Container.DataItem("lrf_file") %>'>  Delete  </asp:LinkButton>
                                                    <%End If%>
                                                </Template>
                                            </obout:GridTemplate>
                                            <obout:GridTemplate runat="server" ID="DocFilter" ControlID="Doc">
                                                <Template>
                                                    <obout:OboutTextBox runat="server" ID="Doc" Width="100%">
                                                        <ClientSideEvents OnKeyUp="applyFilter" />
                                                    </obout:OboutTextBox>
                                                </Template>
                                            </obout:GridTemplate>
                                            <obout:GridTemplate runat="server" ID="tplRemoveFilter">
                                                <Template>
                                                    <a onclick="grid1.removeFilter()" class="ob_gALF" style="cursor: pointer">Clear Filter</a>
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
                                        <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter" />
                                    </obout:Grid>
                                </div>
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
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var applyFilterTimeout = null;

        function applyFilter() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeout);
            }
            applyFilterTimeout = window.setTimeout(doFiltering, 500);
        }

        function doFiltering() {
            grid1.filter();
        }

        function applyFilter2() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeout);
            }
            applyFilterTimeout = window.setTimeout(doFiltering2, 500);
        }

        function doFiltering2() {
            grid2.filter();
        }

        /* -- */

        function OnBeforeDelete(record) {
            if (confirm("Apakah Anda yakin ingin menghapus?") == false) {
                return false;
            }
            return true;
        }
        function OnDelete(record) {
            alert("Data berhasil dihapus.");
        }
    </script>
</asp:Content>
