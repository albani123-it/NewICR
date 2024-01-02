<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Submit.aspx.vb"
    Inherits="NewCRM.Submit" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aBc("General");
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
        }
        fAction();
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
        function fBrowse(){
            var e = document.getElementById("<%= ddlSubmit.clientID %>");           
            var position = e.options[e.selectedIndex].value;
             
            oWindowscroll("../../_Admin/userRefByPosition.aspx?position=" + position,800,400);
        }
        function popupCallback(value, value2) {
            
            var usrid = document.getElementById("<%=usrid.clientID %>").value;
            if (usrid == value){
                alert("Cannot assign to yourself.");
            }
            else{
                document.getElementById("<%=hdAssign.clientID %>").value = value;
                document.getElementById("<%=txtAssignTo.clientID %>").value = value2;
            }
            
            return false;
        }
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
        #ui-datepicker-div
        {
            display: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="usrid" runat="server" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 100%">
        <div id='cssmenu'>
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
                <table class="tbl" cellspacing="0" cellpadding="3" width="100%" style="text-align: left;
                    border: thin solid #bcbcbc; border-radius: 5px 5px 0 0;">
                    <tr>
                        <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;
                            font-weight: bold; color: #fff;">
                            INFORMASI APLIKASI
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="20%">
                            <b>No. Aplikasi</b>
                        </td>
                        <td width="30%">
                            <asp:Label ID="lblAppNo" runat="server"></asp:Label>
                        </td>
                        <td width="20%">
                            <b>Nama Debitur</b>
                        </td>
                        <td width="30%">
                            <asp:Label ID="lblNamaDebitur" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Tgl. Aplikasi</b>
                        </td>
                        <td>
                            <asp:Label ID="lblAppDate" runat="server"></asp:Label><br />
                            <span style="color: Red; margin-top: 2px; float: left;">(dd-mm-yyyy)</span>
                        </td>
                        <td>
                            <b>No. Telp</b>
                        </td>
                        <td>
                            <asp:Label ID="lblNoTelp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Kanwil</b>
                        </td>
                        <td>
                            <asp:Label ID="lblKanwil" runat="server"></asp:Label>
                        </td>
                        <td>
                            <b>Alamat</b>
                        </td>
                        <td>
                            <asp:Label ID="lblAlamat" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Spoke/Capem</b>
                        </td>
                        <td>
                            <asp:Label ID="lblSpokeCapem" runat="server"></asp:Label>
                        </td>
                        <td>
                            <b>Bidang Usaha</b>
                        </td>
                        <td>
                            <asp:Label ID="lblBidangUsaha" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>RM/Analist</b>
                        </td>
                        <td>
                            <asp:Label ID="lblRM" runat="server"></asp:Label>
                        </td>
                        <td>
                            <b>Sektor Ekonomi</b>
                        </td>
                        <td>
                            <asp:Label ID="lblSektorEkonomi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Kantor Cabang Pembukuan</b>
                        </td>
                        <td>
                            <asp:Label ID="lblKantorCabang" runat="server"></asp:Label>
                        </td>
                        <td>
                            <b>Sub Sektor Ekonomi</b>
                        </td>
                        <td>
                            <asp:Label ID="lblSubSektorEkonomi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Kode Borrower</b>
                        </td>
                        <td>
                            <asp:Label ID="lblBrwCode" runat="server"></asp:Label>
                        </td>
                        <td>
                            <b>Grup Usaha</b>
                        </td>
                        <td>
                            <asp:Label ID="lblGrupUsaha" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>CIF</b>
                        </td>
                        <td>
                            <asp:Label ID="lblCIF" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table id="tblRequest" runat="server" class="tbl" cellspacing="0" cellpadding="3"
                    width="100%" style="text-align: left; border: thin solid #bcbcbc;">
                    <tr>
                        <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="color: #fff;
                            font-weight: bold;">
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
                <table class="tbl" cellspacing="0" cellpadding="3" width="100%" style="text-align: left;
                    border: thin solid #bcbcbc; border-radius: 0 0 5px 5px;">
                    <tr>
                        <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="color: #fff;
                            font-weight: bold;">
                            SUBMIT TO
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="20%">
                            <b>Supervisor Anda</b>
                        </td>
                        <td width="30%">
                            <asp:Label ID="lblDefaultSupervisor" runat="server"></asp:Label>
                        </td>
                        <td width="30%">
                            <b>
                                <asp:Label ID="lblPrevAction" runat="server" Visible="false"></asp:Label></b>
                        </td>
                        <td width="30%">
                            <asp:Label ID="lblPrevActionValue" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Assign To</b>
                        </td>
                        <td>
                            <div id="dvAssignTo1" runat="server" style="display:block;">
                                <asp:TextBox ID="txtAssignTo" ReadOnly="true" runat="server" CssClass="inpTxtDisabled"
                                    Style="width: 200px; margin-right: 5px; height: 15px;"></asp:TextBox>
                                <input type="hidden" value="0" id="hdAssign" runat="server" />
                                <input type="button" id="btnBrowse" runat="server" value="Browse" onclick="fBrowse();"
                                    class="inpBtn" />
                            </div>
                            <div id="dvAssignTo2" runat="server" style="display:none;">
                                <asp:TextBox ID="txtAssignToAll" ReadOnly="true" runat="server" CssClass="inpTxtDisabled"
                                    Style="width: 200px; margin-right: 5px; height: 15px;"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="lblPrevSupervisor" runat="server" Visible="false"></asp:Label></b>
                        </td>
                        <td>
                            <asp:Label ID="lblPrevSupervisorValue" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Notes</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                                Rows="4" Style="width: 600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td colspan="4">
                            <obout:Grid ID="grid1" runat="server" CallbackMode="true" Serialize="true" AllowSorting="false"
                                AllowPaging="false" AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                                AllowAddingRecords="false" AllowFiltering="false" AllowPageSizeSelection="false"
                                AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                <Columns>
                                    <obout:Column ID="Column1" runat="server" DataField="lon_id" Visible="false" />
                                    <obout:Column ID="Column2" runat="server" DataField="lon_desc" HeaderText="Notes"
                                        Width="50%" Wrap="true" />
                                    <obout:Column ID="Column3" runat="server" DataField="lon_update_by" HeaderText="Update By"
                                        Width="20%" />
                                    <obout:Column ID="Column4" runat="server" DataField="lon_update_date" DataFormatString="{0:dd-MMM-yyyy}"
                                        HeaderText="Update Date" Width="20%" />
                                </Columns>
                            </obout:Grid>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:DropDownList ID="ddlSubmit" runat="server" CssClass="inpDdl" Style="height: 20px;"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="inpBtn" OnClick="btnSave_Click"
                                OnClientClick="return checkSubmit();"></asp:Button>
                        </td>
                    </tr>
                </table>
                <fieldset style="margin-top: 10px; border-radius: 5px; background: #ebecec; display: none;">
                    <legend><b>Workflow Proses</b></legend>
                    <asp:Label ID="lblProcess" runat="server" Style="color: Red;"></asp:Label>
                </fieldset>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function checkSubmit() {
            var action = document.getElementById("<%= ddlSubmit.clientID %>").value;
//            var assign = document.getElementById("<%= txtAssignTo.clientID %>").value;

            if (action == 0) {
                return confirm("Apakah anda yakin untuk menolak aplikasi ini?");
            }
//            else if (assign == 0) {
//                return confirm("Assign To must be field. Please input Assign To\n");
//            }
            else
                return true;
        }
        /* -- */
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
