<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="analysisManufacturer.aspx.vb" Inherits="NewCRM.analysisManufacturer" %>


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
                    <table width="100%" border="0" class="table table-striped table-bordered">
                        <tr>
                            <td width="20%"><span><b>No. Aplikasi</b></span></td>
                            <td><asp:label ID="lblAppNo_header" runat="server"></asp:label></td>
                        </tr>
                        <tr>
                            <td><span><b>Nama Pemohon</b></span></td>
                            <td><asp:label ID="lblBrwName_header" runat="server"></asp:label></td>
                        </tr>
                    </table>
                </div>
                <div id="divnotif" runat="server" class="notif" style="display:none;">
                    <div class="notif-header" width="100%">
                        <table width="100%" class="table table-striped table-bordered">
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
                <table class="table table-striped table-bordered" cellspacing="0" cellpadding="2" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;">
                    <tr>
                        <td colspan="3" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;font-weight:bold;color:#fff;">PROYEKSI KEBUTUHAN MODAL KERJA</td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="30%"><b>CoGS</b></td>
                        <td width="20%"></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtCOGS" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0" onkeyup="countProyeksiKebutuhanKMK();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>SGA</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtSGA" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0" onkeyup="countProyeksiKebutuhanKMK();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Sales</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtSales" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0" onkeyup="countProyeksiKebutuhanKMK();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>ARTO</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtARTO" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0" onkeyup="countTC();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>ITO</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtITO" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0" onkeyup="countTC();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>APTO</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtAPTO" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0" onkeyup="countTC();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>TC</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtTC" runat="server" CssClass="inpTxtDisabled form-control" Width="250px" ReadOnly="true"  style="float:none;text-align: right;display:inline" value="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Proyeksi Kebutuhan KMK</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtProyeksiKebutuhanKMK" runat="server" CssClass="inpTxtDisabled form-control" Width="250px" ReadOnly="true" style="float:none;text-align: right;display:inline" value="0"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" bgcolor="#2A3F54" style="color:#fff;font-weight:bold;">PERHITUNGAN MAKSIMAL PEMBIAYAAN KMK (PROGRESS PAYMENT)</td>
                    </tr>

                    <tr bgcolor="#f6f7f7">
                        <td><b>Porsi Pembiayaan</b></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtPorsiPembiayaan" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;margin-left:23px;text-align: right;display:inline" value="0" onkeyup="countMaxKMK();"></asp:TextBox>
                            &nbsp; %
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Maksimal Kebutuhan KMK</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtMaxKebutuhanKMK" runat="server" CssClass="inpTxtDisabled form-control" Width="250px" ReadOnly="true" style="float:none;text-align: right;display:inline" value="0"></asp:TextBox>
                        </td>
                    </tr>

                    <script type="text/javascript">
                       

                        function countTC() {
                           
                            var ProyeksiARTO = document.getElementById("<%= txtARTO.ClientID  %>").value.replace(/,/g, "");
                            var ProyeksiITO = document.getElementById("<%= txtITO.ClientID  %>").value.replace(/,/g, "");
                            var ProyeksiAPTO = document.getElementById("<%= txtAPTO.ClientID  %>").value.replace(/,/g, "");

                            
                            document.getElementById("<%= txtTC.ClientID  %>").value = formatCurrency(Math.round((ProyeksiARTO * 1) + (ProyeksiITO * 1)) - (ProyeksiAPTO * 1));
                            countProyeksiKebutuhanKMK();
                        }

                        function countProyeksiKebutuhanKMK() {
                            var ProyeksiTC = document.getElementById("<%= txtTC.ClientID  %>").value.replace(/,/g, "");
                            var ProyeksiCOGS = document.getElementById("<%= txtCOGS.ClientID  %>").value.replace(/,/g, "");
                            var ProyeksiSGA = document.getElementById("<%= txtSGA.ClientID  %>").value.replace(/,/g, "");
                            var ProyeksiSales = document.getElementById("<%= txtSales.ClientID  %>").value.replace(/,/g, "");
                           

                            //document.getElementById("<%= txtProyeksiKebutuhanKMK.ClientID  %>").value = formatCurrency(Math.round(((ProyeksiCOGS + ProyeksiSGA) / ProyeksiSales) * (ProyeksiSales / 360) * ProyeksiTC));

                            var kebutuhanKMK1 = ((Math.round(ProyeksiCOGS) + Math.round(ProyeksiSGA)) / Math.round(ProyeksiSales)) * (Math.round(ProyeksiSales) / 360) * Math.round(ProyeksiTC);
                            //var kebutuhanKMK1 = Math.round(ProyeksiCOGS) + Math.round(ProyeksiSGA);
                            document.getElementById("<%= txtProyeksiKebutuhanKMK.ClientID  %>").value = formatCurrency(kebutuhanKMK1);

                        }

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
                            <asp:TextBox ID="txtKMKBankLainField1" runat="server" CssClass="inpTxt form-control" Width="250px" style="width:300px;float:none;display:inline" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue1" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField2" runat="server" CssClass="inpTxt form-control" Width="250px" style="width:300px;float:none;display:inline" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue2" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField3" runat="server" CssClass="inpTxt form-control" Width="250px" style="width:300px;float:none;display:inline" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue3" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField4" runat="server" CssClass="inpTxt form-control" Width="250px" style="width:300px;float:none;display:inline" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue4" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td colspan="2">
                            <b>KMK Bank Lain</b>&nbsp;
                            <asp:TextBox ID="txtKMKBankLainField5" runat="server" CssClass="inpTxt form-control" Width="250px" style="width:300px;float:none;display:inline" onkeyup="fillBankLain();"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKMKBankLainValue5" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countKebutuhanFasilitasKMK();countKebutuhanFasilitasKMKPRK();fillBankLain();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kebutuhan Fasilitas KMK</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtKebutuhanFasilitasKMK" runat="server" CssClass="inpTxtDisabled form-control" Width="250px" ReadOnly="true" style="float:none;text-align: right;display:inline" value="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Pembiayaan Bank DKI</b></td>
                        <td></td>
                        <td>
                            Rp.&nbsp
                            <asp:TextBox ID="txtPembiayaanBankKMK" runat="server" CssClass="inpTxt form-control" Width="250px" style="float:none;text-align: right;display:inline" value="0"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);checkPembiayaan();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height:30px">
				        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:button id="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="btn btn-primary"></asp:button>
                            <asp:button id="btnNext" runat="server" Text="Lanjut" CssClass="btn btn-primary"></asp:button>
                            <%--<span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>--%>
                        </td>
			        </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>