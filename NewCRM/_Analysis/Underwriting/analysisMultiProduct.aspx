<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="analysisMultiProduct.aspx.vb"
    Inherits="NewCRM.analysisMultiProduct" %>

<%--<%@ Import Namespace="System.Data" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu("Analysis");
        aBc("Kertas Kerja");
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
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 45)
                return false;
            return true;
        }
        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
            }
            else if(num == "0-")
            {
                return "-"
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
    <input type="hidden" id="brw_code" runat="server" />
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
                        <asp:Label ID="lblNotif" runat="server" Style="color: Red;"></asp:Label>
                    </div>
                </div>
                <table id="tblMultiProduct" class="tbl" cellspacing="0" cellpadding="2" width="100%"
                    style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                    <tr>
                        <td colspan="3" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;
                            font-weight: bold; color: #fff;">
                            PROYEKSI KEBUTUHAN MODAL KERJA
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Nilai proyek / Nilai investasi</b>
                        </td>
                        <td>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtNilaiProyek" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="1,000,000,000"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" background="../../Images/bg-head.gif" style="color: #fff; font-weight: bold;">
                            PERHITUNGAN MAKSIMAL PEMBIAYAAN KI (PROGRESS PAYMENT)
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Porsi Pembiayaan</b>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPorsiPembiayaan" runat="server" CssClass="inpTxt" Style="float: none;
                                margin-left: 23px; text-align: right;" value="0" onkeyup="countMaxKMK();"></asp:TextBox>
                            &nbsp; %
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Maksimal Kebutuhan KMK</b>
                        </td>
                        <td>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtMaxKebutuhanKMK" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                Style="float: none; text-align: right;" value="0"></asp:TextBox>
                        </td>
                    </tr>
                    <script type="text/javascript">
                        function countMaxKMK() {
                            var porsiPembiayaan = document.getElementById("<%= txtPorsiPembiayaan.ClientID  %>").value.replace(/,/g, "");
                            var proyeksiKebutuhanKMK = document.getElementById("<%= txtNilaiProyek.ClientID  %>").value.replace(/,/g, "");

                            document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value = formatCurrency(Math.round(porsiPembiayaan / 100 * proyeksiKebutuhanKMK));
                            countKebutuhanFasilitasKMK();
                        }
                        function countKebutuhanFasilitasKMK() {
                            var maxKMK = document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value.replace(/,/g, "");
                            var bankLain1 = document.getElementById("<%= txtKMKBankLainValue1.ClientID  %>").value.replace(/,/g, "");
                            var bankLain2 = document.getElementById("<%= txtKMKBankLainValue2.ClientID  %>").value.replace(/,/g, "");
                            var bankLain3 = document.getElementById("<%= txtKMKBankLainValue3.ClientID  %>").value.replace(/,/g, "");
                            var bankLain4 = document.getElementById("<%= txtKMKBankLainValue4.ClientID  %>").value.replace(/,/g, "");
                            var bankLain5 = document.getElementById("<%= txtKMKBankLainValue5.ClientID  %>").value.replace(/,/g, "");

                            var kebutuhanKMK = Math.round((maxKMK * 1) - (bankLain1 * 1 + bankLain2 * 1 + bankLain3 * 1 + bankLain4 * 1 + bankLain5 * 1));

                            document.getElementById("<%= txtKebutuhanFasilitasKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK);
                            checkPembiayaan();
                        }
                        function checkPembiayaan() {
                            var kebutuhanKMK = document.getElementById("<%= txtKebutuhanFasilitasKMK.ClientID  %>").value.replace(/,/g, "");
                            var pembiayaanBank = document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value.replace(/,/g, "");

                            if ((parseInt(pembiayaanBank) > parseInt(kebutuhanKMK)) & parseInt(kebutuhanKMK) > 0) {
                                document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK);
                            }
                            else if (parseInt(kebutuhanKMK) < 0) {
                                document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value = 0;
                            }
                        }
                    </script>
                    <tr bgcolor="#f6f7f7">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField1" runat="server" CssClass="inpTxt" Style="width: 300px;
                                float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue1" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField2" runat="server" CssClass="inpTxt" Style="width: 300px;
                                float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue2" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField3" runat="server" CssClass="inpTxt" Style="width: 300px;
                                float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue3" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField4" runat="server" CssClass="inpTxt" Style="width: 300px;
                                float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue4" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField5" runat="server" CssClass="inpTxt" Style="width: 300px;
                                float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue5" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Kebutuhan Fasilitas KI</b>
                        </td>
                        <td>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKebutuhanFasilitasKMK" runat="server" CssClass="inpTxtDisabled"
                                ReadOnly="true" Style="float: none; text-align: right;" value="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Pembiayaan Bank DKI</b>
                        </td>
                        <td>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtPembiayaanBankKMK" runat="server" CssClass="inpTxt" Style="float: none;
                                text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaan();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <div id="divMultiFinance" runat="server" visible="false">
                    <table id="tblMultifinance" class="tbl" cellspacing="0" cellpadding="2" width="100%"
                        style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;
                                font-weight: bold; color: #fff;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>Target Booking/Penjualan</b>
                            </td>
                            <td width="20%">
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtTargetBookingToPenjualan" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Realisasi Booking/Penjualan</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtRealisasiBookingToPenjualan" runat="server" CssClass="inpTxt"
                                    Style="float: none; text-align: right;" value="0" onkeypress="return isNumberKey(event)"
                                    onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Kelonggaran Tarik Bank Lain</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtKelonggaranTarikBankLain" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Rencana Obligasi</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtRencanaObligasi" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Total</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtTotal" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Maksimal Pembiayaan Bank</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtPembiayaanBank" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaan();"></asp:TextBox>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countTotal() {

                                var target = document.getElementById("<%= txtTargetBookingToPenjualan.ClientID  %>").value.replace(/,/g, "");
                                var realisasi = document.getElementById("<%= txtRealisasiBookingToPenjualan.ClientID  %>").value.replace(/,/g, "");
                                var kelonggaran = document.getElementById("<%= txtKelonggaranTarikBankLain.ClientID  %>").value.replace(/,/g, "");
                                var rencana = document.getElementById("<%= txtRencanaObligasi.ClientID  %>").value.replace(/,/g, "");


                                var total = Math.round(target * 1 - realisasi * 1 - kelonggaran * 1 - rencana * 1);

                                document.getElementById("<%= txtTotal.ClientID  %>").value = formatCurrency(total);
                                checkPembiayaan();
                            }
                            function checkPembiayaan() {
                                var total = document.getElementById("<%= txtTotal.ClientID  %>").value.replace(/,/g, "");
                                var pembiayaanBank = document.getElementById("<%= txtPembiayaanBank.ClientID  %>").value.replace(/,/g, "");

                                if ((parseInt(pembiayaanBank) > parseInt(total)) & parseInt(total) > 0) {
                                    document.getElementById("<%= txtPembiayaanBank.ClientID  %>").value = formatCurrency(total);
                                }
                                else if (parseInt(total) < 0) {
                                    document.getElementById("<%= txtPembiayaanBank.ClientID  %>").value = 0;
                                }
                            }
                        </script>
                        <tr style="height: 30px">
                            <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divManufacturer" runat="server" visible="false">
                    <table id="tblManufacturer" visible="false" class="tbl" cellspacing="0" cellpadding="2"
                        width="100%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;
                                font-weight: bold; color: #fff;">
                                PROYEKSI KEBUTUHAN MODAL KERJA 
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>CoGS</b>
                            </td>
                            <td width="20%">
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtCOGS" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>SGA</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtSGA" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Sales</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtSales" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>ARTO</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtARTO" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>ITO</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtITO" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>APTO</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtAPTO" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>TC</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtTC" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Proyeksi Kebutuhan KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtProyeksiKebutuhanKMK" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="1,000,000,000"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="color: #fff; font-weight: bold;">
                                PERHITUNGAN MAKSIMAL PEMBIAYAAN KMK (PROGRESS PAYMENT)
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Porsi Pembiayaan</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="inpTxt" Style="float: none; margin-left: 23px;
                                    text-align: right;" value="0" onkeyup="countMaxKMK();"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Maksimal Kebutuhan KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countMaxKMK() {
                                var porsiPembiayaan = document.getElementById("<%= txtPorsiPembiayaan.ClientID  %>").value.replace(/,/g, "");
                                var proyeksiKebutuhanKMK = document.getElementById("<%= txtProyeksiKebutuhanKMK.ClientID  %>").value.replace(/,/g, "");


                                document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value = formatCurrency(Math.round(porsiPembiayaan / 100 * proyeksiKebutuhanKMK));
                                countKebutuhanFasilitasKMK();
                            }
                            function countKebutuhanFasilitasKMK() {
                                var maxKMK = document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value.replace(/,/g, "");
                                var bankLain1 = document.getElementById("<%= txtKMKBankLainValue1.ClientID  %>").value.replace(/,/g, "");
                                var bankLain2 = document.getElementById("<%= txtKMKBankLainValue2.ClientID  %>").value.replace(/,/g, "");
                                var bankLain3 = document.getElementById("<%= txtKMKBankLainValue3.ClientID  %>").value.replace(/,/g, "");
                                var bankLain4 = document.getElementById("<%= txtKMKBankLainValue4.ClientID  %>").value.replace(/,/g, "");
                                var bankLain5 = document.getElementById("<%= txtKMKBankLainValue5.ClientID  %>").value.replace(/,/g, "");

                                var kebutuhanKMK = Math.round((maxKMK * 1) - (bankLain1 * 1 + bankLain2 * 1 + bankLain3 * 1 + bankLain4 * 1 + bankLain5 * 1));

                                document.getElementById("<%= txtKebutuhanFasilitasKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK);
                                checkPembiayaan();
                            }
                            function checkPembiayaan() {
                                var kebutuhanKMK = document.getElementById("<%= txtKebutuhanFasilitasKMK.ClientID  %>").value.replace(/,/g, "");
                                var pembiayaanBank = document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value.replace(/,/g, "");

                                if ((parseInt(pembiayaanBank) > parseInt(kebutuhanKMK)) & parseInt(kebutuhanKMK) > 0) {
                                    document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK);
                                }
                                else if (parseInt(kebutuhanKMK) < 0) {
                                    document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value = 0;
                                }
                            }
                        </script>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="inpTxt" Style="float: none; text-align: right;"
                                    value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="inpTxt" Style="float: none; text-align: right;"
                                    value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox7" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox8" runat="server" CssClass="inpTxt" Style="float: none; text-align: right;"
                                    value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox9" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox10" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox11" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Kebutuhan Fasilitas KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox13" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Pembiayaan Bank DKI</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox14" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaan();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divConstruction" runat="server" visible="false">
                    <table id="tblConstruction" visible="false" class="tbl" cellspacing="0" cellpadding="2"
                        width="100%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;">
                                &nbsp;
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countNilaiProyekDiaksep() {
                                var rencanaProyek = document.getElementById("<%= txtRencanaProyek.ClientID  %>").value.replace(/,/g, "");
                                var akseptasiBank = document.getElementById("<%= txtAkseptasiBank.ClientID  %>").value.replace(/,/g, "");

                                document.getElementById("<%= txtNilaiProyekDiaksep.ClientID  %>").value = formatCurrency(Math.round(rencanaProyek * akseptasiBank / 100));
                                document.getElementById("<%= txtAkseptasiBank2.ClientID  %>").value = document.getElementById("<%= txtAkseptasiBank.ClientID  %>").value;
                                countUangMuka();
                                countProjectCost();
                            }
                        </script>
                        <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>Rencana Proyek Konstruksi</b>
                            </td>
                            <td width="20%">
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtRencanaProyek" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countNilaiProyekDiaksep();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Akseptasi Bank</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAkseptasiBank" runat="server" CssClass="inpTxt" Style="float: none;
                                    margin-left: 23px; text-align: right;" value="0" onkeyup="countNilaiProyekDiaksep();countMaxPembiayaan();"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Nilai Proyek diaksep</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtNilaiProyekDiaksep" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Laba</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLaba" runat="server" CssClass="inpTxt" Style="float: none; margin-left: 23px;
                                    text-align: right;" value="0" onkeyup="countProjectCost();"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Pajak</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPajak" runat="server" CssClass="inpTxt" Style="float: none; margin-left: 23px;
                                    text-align: right;" value="0" onkeyup="countProjectCost();"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countUangMuka() {
                                var nilaiProyekDiaksep = document.getElementById("<%= txtNilaiProyekDiaksep.ClientID  %>").value.replace(/,/g, "");
                                var persenUangMuka = document.getElementById("<%= txtUangMukaPercent.ClientID  %>").value.replace(/,/g, "");

                                document.getElementById("<%= txtUangMuka.ClientID  %>").value = formatCurrency(Math.round(nilaiProyekDiaksep * persenUangMuka / 100));

                                countNilaiUangMukaBersih();
                                countMaxKMK();
                            }
                        </script>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Uang Muka</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUangMukaPercent" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeyup="countUangMuka();"></asp:TextBox>
                                &nbsp; %
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtUangMuka" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0" onkeyup="countUangMuka();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Jenis Kontrak</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdJenisKontrak" RepeatDirection="Horizontal" Style="margin-bottom: -5px;
                                    margin-left: -12px" runat="server" onclick="fillPorsiPembiayaan();">
                                    <asp:ListItem Value="Progress Payment" Selected="True">Progress Payment</asp:ListItem>
                                    <asp:ListItem Value="Turnkey Project & Monthly Payment">Turnkey Project & Monthly Payment</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function fillPorsiPembiayaan() {
                                var rad = document.getElementById('<%=rdJenisKontrak.ClientID %>');
                                var radio = rad.getElementsByTagName("input");
                                //                            var jenisKontrak = "";
                                //                            for (var i = 0; i < radio.length; i++) {
                                //                                if (radio[i].checked)
                                //                                {
                                //                                    jenisKontrak = radio[i].value;
                                //                                    //break;
                                //                                }
                                //                            }
                                //alert(jenisKontrak);
                                //if (jenisKontrak.toString = "Progress Payment") {
                                if (radio[0].checked) {
                                    document.getElementById('<%=txtPorsiPembiayaan.ClientID %>').value = "40";
                                }
                                else {
                                    document.getElementById('<%=txtPorsiPembiayaan.ClientID %>').value = "70";
                                }
                                countMaxKMK();
                            }
                        </script>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Porsi Pembiayaan</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox15" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; margin-left: 23px; text-align: right;" value="40"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Project Cost</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtProjectCost" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countProjectCost() {
                                var nilaiProyekDiaksep = document.getElementById("<%= txtNilaiProyekDiaksep.ClientID  %>").value.replace(/,/g, "");
                                var laba = document.getElementById("<%= txtLaba.ClientID  %>").value.replace(/,/g, "");
                                var pajak = document.getElementById("<%= txtPajak.ClientID  %>").value.replace(/,/g, "");

                                document.getElementById("<%= txtProjectCost.ClientID  %>").value = formatCurrency(Math.round(nilaiProyekDiaksep * 1 - (nilaiProyekDiaksep * laba / 100) - (nilaiProyekDiaksep * pajak / 100)));

                                countNilaiUangMukaBersih();
                                countMaxKMK();
                            }
                            function countNilaiUangMukaBersih() {
                                var projectCost = document.getElementById("<%= txtProjectCost.ClientID  %>").value.replace(/,/g, "");
                                var uangMuka = document.getElementById("<%= txtUangMuka.ClientID  %>").value.replace(/,/g, "");

                                document.getElementById("<%= txtNilaiProyekBersih.ClientID  %>").value = formatCurrency(Math.round(projectCost * 1 - uangMuka * 1));
                            }
                        </script>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Nilai Proyek Bersih</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtNilaiProyekBersih" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="color: #fff; font-weight: bold;">
                                PERHITUNGAN MAKSIMAL PEMBIAYAAN KMK (PROGRESS PAYMENT)
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Maksimal Kebutuhan KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox16" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countMaxKMK() {
                                var porsiPembiayaan = document.getElementById("<%= txtPorsiPembiayaan.ClientID  %>").value.replace(/,/g, "");
                                var projectCost = document.getElementById("<%= txtProjectCost.ClientID  %>").value.replace(/,/g, "");
                                var uangMuka = document.getElementById("<%= txtUangMuka.ClientID  %>").value.replace(/,/g, "");

                                document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value = formatCurrency(Math.round((porsiPembiayaan / 100 * projectCost) - (uangMuka * 1)));
                                countKebutuhanFasilitasKMK();
                            }
                            function countKebutuhanFasilitasKMK() {
                                var maxKMK = document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value.replace(/,/g, "");
                                var bankLain1 = document.getElementById("<%= txtKMKBankLainValue1.ClientID  %>").value.replace(/,/g, "");
                                var bankLain2 = document.getElementById("<%= txtKMKBankLainValue2.ClientID  %>").value.replace(/,/g, "");
                                var bankLain3 = document.getElementById("<%= txtKMKBankLainValue3.ClientID  %>").value.replace(/,/g, "");
                                var bankLain4 = document.getElementById("<%= txtKMKBankLainValue4.ClientID  %>").value.replace(/,/g, "");
                                var bankLain5 = document.getElementById("<%= txtKMKBankLainValue5.ClientID  %>").value.replace(/,/g, "");

                                var kebutuhanKMK = Math.round((maxKMK * 1) - (bankLain1 * 1 + bankLain2 * 1 + bankLain3 * 1 + bankLain4 * 1 + bankLain5 * 1));

                                document.getElementById("<%= txtKebutuhanFasilitasKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK);
                                checkPembiayaan();
                            }
                            function fillBankLain() {
                                document.getElementById("<%= txtKMKBankLain2Value1.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainValue1.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Value2.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainValue2.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Value3.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainValue3.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Value4.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainValue4.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Value5.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainValue5.ClientID  %>").value;

                                document.getElementById("<%= txtKMKBankLain2Field1.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainField1.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Field2.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainField2.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Field3.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainField3.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Field4.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainField4.ClientID  %>").value;
                                document.getElementById("<%= txtKMKBankLain2Field5.ClientID  %>").value = document.getElementById("<%= txtKMKBankLainField5.ClientID  %>").value;
                            }
                            function checkPembiayaan() {
                                var kebutuhanKMK = document.getElementById("<%= txtKebutuhanFasilitasKMK.ClientID  %>").value.replace(/,/g, "");
                                var pembiayaanBank = document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value.replace(/,/g, "");

                                if ((parseInt(pembiayaanBank) > parseInt(kebutuhanKMK)) & parseInt(kebutuhanKMK) > 0) {
                                    document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK);
                                }
                                else if (parseInt(kebutuhanKMK) < 0) {
                                    document.getElementById("<%= txtPembiayaanBankKMK.ClientID  %>").value = 0;
                                }
                            }
                        </script>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox17" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox18" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox19" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox20" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox21" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox22" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox23" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox24" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="TextBox25" runat="server" CssClass="inpTxt" Style="width: 300px;
                                    float: none;" onkeyup="fillBankLain();"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox26" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Kebutuhan Fasilitas KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox27" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Pembiayaan Bank DKI</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="TextBox28" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaan();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="color: #fff; font-weight: bold;">
                                KEBUTUHAN KMK PRK
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countMaxPembiayaan() {
                                var sga = document.getElementById("<%= txtSGA.ClientID  %>").value.replace(/,/g, "");
                                var akseptasiBank = document.getElementById("<%= txtAkseptasiBank.ClientID  %>").value.replace(/,/g, "");

                                var maxPembiayaan = Math.round(sga * akseptasiBank / 100);

                                document.getElementById("<%= txtMaxPembiayaan.ClientID  %>").value = formatCurrency(maxPembiayaan);

                                countKebutuhanFasilitasKMKPRK();
                                countMaxKMK();
                            }
                            function countKebutuhanFasilitasKMKPRK() {
                                var maxPembiayaan = document.getElementById("<%= txtMaxPembiayaan.ClientID  %>").value.replace(/,/g, "");
                                var bankLain1 = document.getElementById("<%= txtKMKBankLainValue1.ClientID  %>").value.replace(/,/g, "");
                                var bankLain2 = document.getElementById("<%= txtKMKBankLainValue2.ClientID  %>").value.replace(/,/g, "");
                                var bankLain3 = document.getElementById("<%= txtKMKBankLainValue3.ClientID  %>").value.replace(/,/g, "");
                                var bankLain4 = document.getElementById("<%= txtKMKBankLainValue4.ClientID  %>").value.replace(/,/g, "");
                                var bankLain5 = document.getElementById("<%= txtKMKBankLainValue5.ClientID  %>").value.replace(/,/g, "");

                                var kebutuhanKMKPRK = Math.round((maxPembiayaan * 1) - (bankLain1 * 1 + bankLain2 * 1 + bankLain3 * 1 + bankLain4 * 1 + bankLain5 * 1));

                                document.getElementById("<%= txtKebutuhanFasilitasKMKPRK.ClientID  %>").value = formatCurrency(kebutuhanKMKPRK);
                                checkPembiayaanPRK();
                            }
                            function checkPembiayaanPRK() {
                                var kebutuhanKMKPRK = document.getElementById("<%= txtKebutuhanFasilitasKMKPRK.ClientID  %>").value.replace(/,/g, "");
                                var pembiayaanBankPRK = document.getElementById("<%= txtPembiayaanBankPRK.ClientID  %>").value.replace(/,/g, "");

                                if ((parseInt(pembiayaanBankPRK) > parseInt(kebutuhanKMKPRK)) && parseInt(kebutuhanKMKPRK) > 0) {
                                    document.getElementById("<%= txtPembiayaanBankPRK.ClientID  %>").value = formatCurrency(kebutuhanKMKPRK);
                                }
                                else if (parseInt(kebutuhanKMKPRK) < 0) {
                                    document.getElementById("<%= txtPembiayaanBankPRK.ClientID  %>").value = 0;
                                }
                            }
                        </script>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>SGA</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="textSGA" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="26,455,000,000"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Akseptasi Bank</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAkseptasiBank2" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; margin-left: 23px; text-align: right;" value="0"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Maksimal Pembiayaan</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtMaxPembiayaan" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="txtKMKBankLain2Field1" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="width: 300px; float: none;"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtKMKBankLain2Value1" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0" onkeypress="return isNumberKey(event)"
                                    onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMKPRK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="txtKMKBankLain2Field2" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="width: 300px; float: none;"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtKMKBankLain2Value2" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0" onkeypress="return isNumberKey(event)"
                                    onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMKPRK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="txtKMKBankLain2Field3" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="width: 300px; float: none;"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtKMKBankLain2Value3" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0" onkeypress="return isNumberKey(event)"
                                    onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMKPRK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="txtKMKBankLain2Field4" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="width: 300px; float: none;"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtKMKBankLain2Value4" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0" onkeypress="return isNumberKey(event)"
                                    onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMKPRK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td colspan="2">
                                <b>KMK Bank Lain</b>&nbsp;
                                <asp:TextBox ID="txtKMKBankLain2Field5" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="width: 300px; float: none;"></asp:TextBox>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtKMKBankLain2Value5" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0" onkeypress="return isNumberKey(event)"
                                    onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMKPRK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Kebutuhan Fasilitas KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtKebutuhanFasilitasKMKPRK" runat="server" CssClass="inpTxtDisabled"
                                    ReadOnly="true" Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Pembiayaan Bank DKI</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp
                                <asp:TextBox ID="txtPembiayaanBankPRK" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaanPRK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divCashColl" runat="server" visible="false">
                    <table id="tblCashColl" visible="false" class="tbl" cellspacing="0" cellpadding="2"
                        width="100%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                        <tr>
                            <td colspan="3" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;
                                font-weight: bold; color: #fff;">
                                &nbsp; 
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>Nominal Deposito</b>
                            </td>
                            <td width="20%">
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="txtNominalDeposito" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanKMK();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Porsi Pembiayaan</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox30" runat="server" CssClass="inpTxt" Style="float: none;
                                    margin-left: 23px; text-align: right;" value="0" onkeyup="countKebutuhanKMK();"></asp:TextBox>
                                &nbsp; %
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>Maksimal Kebutuhan KMK</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="TextBox31" runat="server" CssClass="inpTxtDisabled" ReadOnly="true"
                                    Style="float: none; text-align: right;" value="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>Pembiayaan Bank DKI</b>
                            </td>
                            <td>
                            </td>
                            <td>
                                Rp.&nbsp;
                                <asp:TextBox ID="TextBox32" runat="server" CssClass="inpTxt" Style="float: none;
                                    text-align: right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaan();"></asp:TextBox>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            function countKebutuhanKMK() {

                                var deposito = document.getElementById("<%= txtNominalDeposito.ClientID  %>").value.replace(/,/g, "");
                                var porsi = document.getElementById("<%= txtPorsiPembiayaan.ClientID  %>").value.replace(/,/g, "");

                                var maxKMK = Math.round(deposito * porsi / 100);

                                document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value = formatCurrency(maxKMK);
                                checkPembiayaan();
                            }
                            function checkPembiayaan() {
                                var maxKMK = document.getElementById("<%= txtMaxKebutuhanKMK.ClientID  %>").value.replace(/,/g, "");
                                var pembiayaanBank = document.getElementById("<%= txtPembiayaanBank.ClientID  %>").value.replace(/,/g, "");

                                if ((parseInt(pembiayaanBank) > parseInt(maxKMK)) & parseInt(maxKMK) > 0) {
                                    document.getElementById("<%= txtPembiayaanBank.ClientID  %>").value = formatCurrency(maxKMK);
                                }
                                else if (parseInt(maxKMK) < 0) {
                                    document.getElementById("<%= txtPembiayaanBank.ClientID  %>").value = 0;
                                }
                            }
                        </script>
                    </table>
                </div>
                
                <div>
                <table id="Table1" visible="false" class="tbl" cellspacing="0" cellpadding="2"
                        width="100%" style="text-align: left;  border-radius: 5px;">
                         <tr style="height: 30px">
                            <td style="border-radius: 0 0 5px 5px;">
                                <asp:Button ID="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn">
                                </asp:Button>
                                <asp:Button ID="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
