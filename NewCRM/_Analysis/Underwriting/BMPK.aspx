<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BMPK.aspx.vb" Inherits="NewCRM.BMPK" %>


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
        function checkModal() {
            var bulan = document.getElementById("<%= ddlBulan.ClientID %>").value;
            var tahun = document.getElementById("<%= ddlTahun.ClientID %>").value;
            var period = bulan + "," + tahun;
            if (bulan != "Pilih" && tahun != "Pilih")
                PageMethods.checkModal(period,OnSuccess,OnError);
        }

        function OnSuccess(result) {
            if(result == "Data tidak ada.") {
                alert(result);
            }
            else {
                var splits = result.split(",");
                document.getElementById("<%= txtModal.ClientID %>").value = formatCurrency(splits[0]);
                document.getElementById("<%= txtInhousePihakKetiga.ClientID %>").value = formatCurrency(splits[1]);
                document.getElementById("<%= txtDebiturTerkait.ClientID %>").value = formatCurrency(splits[0] * 0.1);
                document.getElementById("<%= txtDebiturPihakKetiga.ClientID %>").value = formatCurrency(splits[0] * 0.2);
                document.getElementById("<%= txtDebiturPihakKetigaGrup.ClientID %>").value = formatCurrency(splits[0] * 0.25);
                document.getElementById("<%= txtDebiturBUMN.ClientID %>").value = formatCurrency(splits[0] * 0.3);
                
                var type = document.getElementById("<%= ddlType.ClientID %>").value;
                var total = document.getElementById("<%= txtTotalExposureKredit.ClientID  %>").value.replace(/,/g, "");
                document.getElementById("<%= txtKelonggaranInhouse.ClientID  %>").value = formatCurrency(splits[1] * 1 - total * 1);
                document.getElementById("<%= txtProsentaseKreditToBMPK.ClientID  %>").value = parseFloat(total / splits[1] * 100).toFixed(2);
                if (type == "Debitur Terkait") {
                    document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(splits[0] * 0.1 * 1 - total * 1);
                }
                else if (type == "Debitur Pihak Ketiga") {
                    document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(splits[0] * 0.2 * 1 - total * 1);
                }
                else if (type == "Debitur Pihak Ketiga Grup/Kelompok Usaha") {
                    document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(splits[0] * 0.25 * 1 - total * 1);
                }
                else if (type == "Debitur BUMN") {
                    document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(splits[0] * 0.3 * 1 - total * 1);
                }
            }
        }
        function OnError(result) {
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
                    <table width="100%" border="0">
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
                <table class="tbl" cellspacing="0" cellpadding="2" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;">
                    <tr>
                        <td colspan="2" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="50%">
                            <b>Modal Bank per</b>&nbsp; 
                            <asp:DropDownList ID="ddlBulan" runat="server" CssClass="inpDdl" onchange="checkModal()"></asp:DropDownList>&nbsp; 
                            <asp:DropDownList ID="ddlTahun" runat="server" CssClass="inpDdl" onchange="checkModal()"></asp:DropDownList>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtModal" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" readonly="true"></asp:TextBox>
                        </td>
                    </tr>

                    <script type="text/javascript">
                        function countAllBMPK() {
                            var modal = document.getElementById("<%= txtModal.ClientID  %>").value.replace(/,/g, "");
                            

                            document.getElementById("<%= txtDebiturTerkait.ClientID  %>").value = formatCurrency(Math.round(modal * 10 / 100));
                            document.getElementById("<%= txtDebiturPihakKetiga.ClientID  %>").value = formatCurrency(Math.round(modal * 20 / 100));
                            document.getElementById("<%= txtDebiturPihakKetigaGrup.ClientID  %>").value = formatCurrency(Math.round(modal * 25 / 100));
                            document.getElementById("<%= txtDebiturBUMN.ClientID  %>").value = formatCurrency(Math.round(modal * 30 / 100));
                            var debiturPihakKetigaGrup = document.getElementById("<%= txtDebiturPihakKetigaGrup.ClientID  %>").value.replace(/,/g, "");
                            document.getElementById("<%= txtInhousePihakKetiga.ClientID  %>").value = formatCurrency(Math.round(debiturPihakKetigaGrup * 90 / 100));

                            countTotal();
                            countKelonggaran();
                            countRatio();
                        }
                    </script>

                    <tr bgcolor="#ebecec">
                        <td><b>BMPK Kepada Debitur Terkait - pasal 4 - (10%)</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturTerkait" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>BMPK Kepada Debitur Pihak Ketiga - pasal 11 - (20%)</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturPihakKetiga" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>BMPK Kepada Debitur Pihak Ketiga Grup/Kelompok Usaha - pasal 11 - (25%)</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturPihakKetigaGrup" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>BMPK Kepada Debitur BUMN - pasal 40 - (30%)</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturBUMN" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>

                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>BMPK House Limit</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtInhousePihakKetiga" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Limit Kredit</b>&nbsp;
                            <asp:TextBox ID="txtDebitur1" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturLimit1" runat="server" CssClass="inpTxt" style="float:none;text-align:right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Limit Kredit</b>&nbsp;
                            <asp:TextBox ID="txtDebitur2" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturLimit2" runat="server" CssClass="inpTxt" style="float:none;text-align:right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Limit Kredit</b>&nbsp;
                            <asp:TextBox ID="txtDebitur3" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturLimit3" runat="server" CssClass="inpTxt" style="float:none;text-align:right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td>
                            <b>Limit Kredit</b>&nbsp;
                            <asp:TextBox ID="txtDebitur4" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturLimit4" runat="server" CssClass="inpTxt" style="float:none;text-align:right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td>
                            <b>Limit Kredit</b>&nbsp;
                            <asp:TextBox ID="txtDebitur5" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
                        </td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtDebiturLimit5" runat="server" CssClass="inpTxt" style="float:none;text-align:right;" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);countTotal();"></asp:TextBox>
                        </td>
                    </tr>

                    <script type="text/javascript">
                        function countTotal() {
                            var limit1 = document.getElementById("<%= txtDebiturLimit1.ClientID  %>").value.replace(/,/g, "");
                            var limit2 = document.getElementById("<%= txtDebiturLimit2.ClientID  %>").value.replace(/,/g, "");
                            var limit3 = document.getElementById("<%= txtDebiturLimit3.ClientID  %>").value.replace(/,/g, "");
                            var limit4 = document.getElementById("<%= txtDebiturLimit4.ClientID  %>").value.replace(/,/g, "");
                            var limit5 = document.getElementById("<%= txtDebiturLimit5.ClientID  %>").value.replace(/,/g, "");

                            document.getElementById("<%= txtTotalExposureKredit.ClientID  %>").value = formatCurrency(limit1 * 1 + limit2 * 1 + limit3 * 1 + limit4 * 1 + limit5 * 1);
                            
                            countKelonggaran();
                            countRatio();
                        }
                    </script>

                    <tr bgcolor="#ebecec">
                        <td><b>Total Eksposure Kredit</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtTotalExposureKredit" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>

                    <tr bgcolor="#f6f7f7">
                        <td><b>Type BMPK</b></td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="inpDdl" style="margin-left:23px;" onchange="countKelonggaran();">
                                <asp:ListItem Value="Pilih">Pilih</asp:ListItem>
                                <asp:ListItem Value="Debitur Terkait">Debitur Terkait</asp:ListItem>
                                <asp:ListItem Value="Debitur Pihak Ketiga">Debitur Pihak Ketiga</asp:ListItem>
                                <asp:ListItem Value="Debitur Pihak Ketiga Grup/Kelompok Usaha">Debitur Pihak Ketiga Grup/Kelompok Usaha</asp:ListItem>
                                <asp:ListItem Value="Debitur BUMN">Debitur BUMN</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <script type="text/javascript">
                        function countKelonggaran() {
                            var type = document.getElementById("<%= ddlType.ClientID %>").value;
                            var debiturPihakKetigaGrup = document.getElementById("<%= txtDebiturPihakKetigaGrup.ClientID  %>").value.replace(/,/g, "");
                            var modal = document.getElementById("<%= txtModal.ClientID  %>").value.replace(/,/g, "");
                            var debiturInhouse = document.getElementById("<%= txtInhousePihakKetiga.ClientID  %>").value.replace(/,/g, "");
                            var total = document.getElementById("<%= txtTotalExposureKredit.ClientID  %>").value.replace(/,/g, "");

                            if (type == "Debitur Terkait") {
                                document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(modal * 0.1 * 1 - total * 1);
                            }
                            else if (type == "Debitur Pihak Ketiga") {
                                document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(modal * 0.2 * 1 - total * 1);
                            }
                            else if (type == "Debitur Pihak Ketiga Grup/Kelompok Usaha") {
                                document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(modal * 0.25 * 1 - total * 1);
                            }
                            else if (type == "Debitur BUMN") {
                                document.getElementById("<%= txtKelonggaranBMPK.ClientID  %>").value = formatCurrency(modal * 0.3 * 1 - total * 1);
                            }
                            document.getElementById("<%= txtKelonggaranInhouse.ClientID  %>").value = formatCurrency(debiturInhouse * 1 - total * 1);
                        }
                    </script>

                    <tr bgcolor="#ebecec">
                        <td><b>Kelonggaran BMPK</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtKelonggaranBMPK" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kelonggaran House Limit BMPK House Limit</b></td>
                        <td>
                            Rp.&nbsp;
                            <asp:TextBox ID="txtKelonggaranInhouse" runat="server" CssClass="inpTxtDisabled" style="float:none;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Persentase Kredit Terhadap BMPK</b></td>
                        <td>
                            <asp:TextBox ID="txtProsentaseKreditToBMPK" runat="server" CssClass="inpTxtDisabled" style="float:none;margin-left:23px;text-align:right;" value="0" ReadOnly="true"></asp:TextBox>
                            &nbsp;%&nbsp;&nbsp;
                            <span id="spNotif" style="color:Red;display:none;"></span>
                        </td>
                    </tr>

                    <script type="text/javascript">
                        function countRatio() {
                            var debiturInhouse = document.getElementById("<%= txtInhousePihakKetiga.ClientID  %>").value.replace(/,/g, "");
                            var total = document.getElementById("<%= txtTotalExposureKredit.ClientID  %>").value.replace(/,/g, "");

                            document.getElementById("<%= txtProsentaseKreditToBMPK.ClientID  %>").value = parseFloat(total / debiturInhouse * 100).toFixed(2);
                            var persen = parseFloat(total / debiturInhouse * 100).toFixed(2);
                            if (persen > 100) {
                                document.getElementById("<%= txtProsentaseKreditToBMPK.clientID %>").innerHTML = "Persentase Kredit Terhadap BMPK tidak boleh lebih dari 100%";
                                document.getElementById("<%= txtProsentaseKreditToBMPK.clientID %>").style.display = "block";
                            }
                        }
                    </script>

                    <tr bgcolor="#f6f7f7">
                        <td><b>Kesimpulan</b></td>
                        <td><asp:TextBox ID="txtKesimpulan" runat="server" CssClass="inpTxt" TextMode="MultiLine" style="width:400px;margin-left:23px;" Rows="5"></asp:TextBox></td>
                    </tr>
                    <tr style="height:30px">
				        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:button id="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn"></asp:button>
                            <asp:button id="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:button>
                            <%--<span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>--%>
                        </td>
			        </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>