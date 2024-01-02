<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="R_AspekRPC.aspx.vb" Inherits="NewCRM.R_AspekRPC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        aMenu("Analysis");
        aBc("RatingNonFinancial");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno + "&typ=Facility";
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
//        function pilBankRating(parent){
//            var value = document.getElementById("selBankDet_"+ parent).value;
//            
//            var before =  document.getElementById("hid_"+ parent).value;
//            
//            document.getElementById("part_"+ parent + value).style.display = '';
//            document.getElementById("part_"+ parent + before).style.display = 'none';
//            document.getElementById("hid_"+ parent).value = value;
//            //return false;
//        }
    </script>
    <style type="text/css">
        .notif
        {
            margin-bottom:10px;
            border: 1px solid #bcbcbc; 
            border-radius:5px;
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
        a.notif-close {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color:#fff;
            cursor:pointer;
            font-weight:bold;
            width: 1%;
        }
        .aactive
        {
            background-color:transparent;

            border-radius:11px;
            -moz-border-radius:11px;
            -webkit-border-radius:11px;
            color: #fff;
	
	        background-color:#ebebeb;
	        border:thin solid #cccccc;	
        }
        td
        {
            padding-left:5px;
        }
        .tbl
        {
            font-family:"Century Gothic", "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            font-size:11px;
        }
        .spanChild
        {
            margin-left:10px;
        }
        #ui-datepicker-div {display: none;}
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <div style="clear:both; margin:0 auto; padding:0; width:100%">
        <div id='cssmenu'>
            <ul style="float:left;margin-right:10px;">
                <%  Dim html As String = ""
                   If dataTable.Rows.Count > 0 Then
                       For i As Integer = 0 To dataTable.Rows.Count - 1
                           If dataTable.Rows(i).Item("lmr_parent") = 0 And dataTable.Rows(i).Item("lmr_link") <> "#" Then
                               If dataTable.Rows(i).Item("lmr_name") = pagename Then
                                   html &= "<li id='m" & dataTable.Rows(i).Item("lmr_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                               Else
                                   html &= "<li id='m" & dataTable.Rows(i).Item("lmr_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                               End If
                           ElseIf dataTable.Rows(i).Item("lmr_link") = "#" Then
                               html &= checkChildHD(dataTable.Rows(i).Item("lmr_position"), i)
                               html &= getMenuChild(dataTable.Rows(i).Item("lmr_position"))
                           Else
                               If dataTable.Rows(i).Item("lmr_name") = pagename Then
                                   html &= "<li id='c" & dataTable.Rows(i).Item("lmr_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                               Else
                                   html &= "<li id='c" & dataTable.Rows(i).Item("lmr_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                               End If
                           End If
                       Next
                   End If
                %>
                <%=html %>
            </ul>
            <div class="bodyContent">
                <div id="divnotif" runat="server" class="notif" style="display:none;">
                    <div class="notif-header" width="100%">
                        <table width="100%">
                            <tr>
                                <td><span><b>Notifikasi</b></span></td>
                                <td align="right"><a class="notif-close" onclick="document.getElementById('<%=divnotif.clientID %>').style.display='none'" title="Close">X</a></td>
                            </tr>
                        </table>
                    </div>
                    <div class="notif-body">
                        <asp:Label ID="lblNotif" runat="server" style="color:Red;"></asp:Label>
                    </div>
                </div>
                <table class="tbl" cellspacing="0" cellpadding="3" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;">
                    <tr>
                        <td colspan="3" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>

                    <%  If exists = False Then%>
                            <%=fillAspek()%>
                    <%Else%>
                            <%=fillAspekExists()%>
                    <%End If%>
                    </table>
                <table id="Table1" class="tbl" runat="server" cellspacing="0" cellpadding="3" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 0 0 5px 5px;">
                    <tr>
                        <td colspan="3" background="../../Images/bg-head.gif" bgcolor="#ddedf6">&nbsp;</td>
                    </tr>
                    <%--Isian--%>
                    <tr id="TrShareCalonDebitur" bgcolor='#ebecec'>
                        <td width='30%'><b>Share Calon debitur terhadap modal usaha atau RAB</b></td>
                        <td width='60%'>
                            <asp:TextBox ID="txtShareCalonDebitur" runat="server" CssClass="inpTxt" style="float:none" onkeypress="return isNumberKey(event)" value="0"></asp:TextBox>
                            &nbsp;% &nbsp; (Diisi hanya untuk produk KI, KMK, KYG)
                        </td>
                    </tr>
                    <tr id="TrBcr" bgcolor='#f6f7f7'>
                        <td width='30%'>
                            <asp:Label id="lblBCR" runat="server"><b>Benefit Cost Ratio (BCR) atau Profitability Index (PI)</b></asp:Label>
                        </td>
                        <td width='60%'>
                            <asp:TextBox ID="txtBCR" runat="server" CssClass="inpTxt" style="float:none" value="0"></asp:TextBox>
                            &nbsp;x &nbsp; (Diisi hanya untuk produk KI)
                        </td>
                    </tr>
                    <tr id="TrRasioJumlahKredit" bgcolor='#ebecec'>
                        <td width='30%'><b>Rasio Jumlah Kredit Yang Diajukan Terhadap Nilai Proyek</b></td>
                        <td width='60%'>
                            <asp:TextBox ID="txtRasioJumlahKreditToNilaiProyek" runat="server" ReadOnly="true" CssClass="inpTxtDisabled" style="float:none" value="0"></asp:TextBox>
                            &nbsp;% &nbsp; (Hanya untuk produk KI, KMK, KYG, KMK Kontraktor)
                        </td>
                    </tr>
                    <tr id="TrNetIncome" bgcolor='#f6f7f7'>
                        <td width='30%'><b>Net Income To Total Kewajiban (Termasuk Pinjaman Baru)</b></td>
                        <td width='60%'>
                            <asp:TextBox ID="txtNetIncomeToTotalKewajiban" runat="server" ReadOnly="true" CssClass="inpTxtDisabled" style="float:none" onkeypress="return isNumberKey(event)" value="0"></asp:TextBox>
                            &nbsp;% &nbsp; (Hanya untuk produk KI, KMK, KYG, KMK Kontraktor)
                        </td>
                    </tr>

                    <tr id="TrRasioTotal" bgcolor='#ebecec'>
                        <td width='30%'><b>Rasio Total Penjualan Dengan Tarikan Pinjaman</b>
                        </td>
                        <td width='60%'>
                            <asp:TextBox ID="txtRasioTotalPinjaman" runat="server" CssClass="inpTxt" style="float:none" onkeypress="return isNumberKey(event)" value="0"></asp:TextBox>
                            &nbsp;% &nbsp; (Diisi hanya untuk produk KI, KMK, KYG, KMK Kontraktor)
                        </td>
                    </tr>

                    <tr id="TrNetPresent" bgcolor='#f6f7f7'>
                        <td width='30%'><b>Net Present Value (NPV)</b></td>
                        <td width='60%'>
                           <asp:TextBox ID="txtNPV" runat="server" CssClass="inpTxt" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);" style="float:none;"></asp:TextBox>
                           &nbsp;x &nbsp; (Diisi hanya untuk produk KI)
                        </td>
                    </tr>

                    <tr id="TrProyeksi" bgcolor='#ebecec'>
                        <td width='30%'><b>Proyeksi Cash Flow Menunjukkan Keuntungan</b></td>
                        <td width='60%'>
                            <asp:TextBox ID="txtProjectedCashFlow" runat="server" CssClass="inpTxt" style="float:none" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);" value="0"></asp:TextBox>
                            &nbsp;Tahun &nbsp; (Diisi hanya untuk produk KI, KMK, KYG, KMK Kontraktor)
                        </td>
                    </tr>
                    <tr style="height:30px">
				        <td background="../../styles/grid/footer.gif" colspan="3" style="border-radius: 0 0 5px 5px;">
                            <asp:button id="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn"></asp:button>
                            <asp:button id="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:button>
                        </td>
			        </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
