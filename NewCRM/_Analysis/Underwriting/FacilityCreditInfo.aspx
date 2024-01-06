<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FacilityCreditInfo.aspx.vb" Inherits="NewCRM.FacilityCreditInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
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
            if (num == "-"){
                return num;
            }
            else if (num == "0-"){
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
        
//        function fillNilai()
//        {
//            var a = document.getElementById("<%= TxtNilaiRp.ClientID  %>").value.replace(/,/g, "");
//            var b = document.getElementById("<%= TxtKurs.ClientID  %>").value.replace(/,/g, "");
//            var total = Math.floor(a / b);
//            
//            if (isFinite(total)){
//                document.getElementById("<%= TxtNilaiValas.ClientID  %>").value = formatCurrency(total);
//            }
//            else{
//                document.getElementById("<%= TxtNilaiValas.ClientID  %>").value = 0;
//            }
//        }

        $(document).ready(function(){
            $("[data-name=date] ").datepicker(
            { 
                duration: '',
                yearRange: "-100:+20",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
        });

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
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color:White;
        }
        #ui-datepicker-div {display: none;}
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
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
                    <table class="table table-striped table-bordered" cellspacing="0" cellpadding="3" width="98%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px;">
                <tr>
                    <td colspan="4" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="20%">
                        <b>Jenis Pengajuan</b>
                    </td>
                    <td width="30%">
                        <asp:DropDownList ID="DdlJenisPengajuan" runat="server" CssClass="form-control" Width="250px" style="display:inline" >
                        </asp:DropDownList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td width="20%">
                        <b>Bunga</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="TxtBunga" Width="20%" runat="server" Value="0" CssClass="form-control" style="float:left;text-align:right;" onkeypress="return isNumberKey(event)" onkeyup="fillNilai();"></asp:TextBox>&nbsp %
                        <br /> <br /> <br />
                        <asp:RadioButtonList ID="RadBunga" Height="10px" RepeatDirection="Horizontal" runat="server" style="float:left">
                            <asp:ListItem Value="Flat" Selected="True">Flat</asp:ListItem>
                            <asp:ListItem Value="Annuitas">Annuitas</asp:ListItem>
                            <asp:ListItem Value="Efektif">Efektif</asp:ListItem>
                        </asp:RadioButtonList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>

                <tr bgcolor="#ebecec">
                    <td>
                        <b>Jenis Fasilitas</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlFasilitas" runat="server" CssClass="form-control" style="width:250px;display:inline">
                        </asp:DropDownList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Nilai Proyek/Usaha</b></td>
                    <td>
                        <asp:TextBox ID="txtNilaiProyek" runat="server" CssClass="form-control" Width="250px" style="text-align:right;display:inline" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);" value="0"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    
                </tr>
                    
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Sistem Pembayaran</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlSitemPembayaran" runat="server" CssClass="form-control" style="width:250px;">
                        </asp:DropDownList>
                        <%--<span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>--%>
                    </td>
                    <td>
                        <b>Peruntukan Kredit/Fasilitas</b>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPeruntukan" Rows="3" TextMode="multiline" Width="250px" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>

                <tr bgcolor="#ebecec">
                    <td><b>Jenis Currency</b></td>
                    <td>
                        <asp:DropDownList ID="DdlJenisCurr" runat="server" CssClass="form-control" AutoPostBack="true" style="width:250px;display:inline">
                        </asp:DropDownList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Availibility Periode</b></td>
                    <td>
                        <%--<asp:TextBox ID="txtAvailibilityPeriode" data-name="date" runat="server" CssClass="inpTxt"></asp:TextBox>
                            <span style="color: Red; margin-top: 2px; float: left;">(dd-mm-yyyy)</span>--%>
                        <%--Tahun&nbsp;<asp:DropDownList ID="ddlTahunAvailibility" runat="server" CssClass="inpDdl"></asp:DropDownList>
                        Bulan&nbsp;<asp:DropDownList ID="ddlBulanAvailibility" runat="server" CssClass="inpDdl"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtAvailibilityPeriode" runat="server" Width="20%" MaxLength="2" CssClass="form-control" Value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);fillNilai();" Style="display:inline"></asp:TextBox>&nbsp Bulan
                    </td>
                </tr>

                <tr bgcolor="#f6f7f7">
                    <td><b>Nilai Kurs</b></td>
                    <td><asp:TextBox ID="TxtKurs" runat="server" value="0" Width="250px" class="form-control" style="text-align:right;" onkeypress="return isNumberKey(event)" onkeyup="fillNilai();"></asp:TextBox></td>
                    <td><b>Sifat Kredit</b></td>
                        <td>
                            <asp:DropDownList ID="DdlSifat" runat="server" CssClass="form-control" Width="250px" style="display:inline" onchange="checkSifatKredit();"></asp:DropDownList>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                        </td>
                </tr>

                <tr bgcolor="#ebecec">
                    <td><b>Nilai Permohonan (dalam Valas)</b></td>
                    <td><asp:TextBox ID="TxtNilaiValas" runat="server" Width="250px" CssClass="form-control" value="0" style="text-align:right;" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value); fillNilai();"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                </tr>

                <tr bgcolor="#f6f7f7">
                    <td><b>Nilai Permohonan (dalam Rupiah)</b></td>
                    <td>
                        <asp:TextBox ID="TxtNilaiRp" runat="server" Width="250px" CssClass="form-control" value="0" style="text-align:right;display:inline;width:250px"  onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value); fillNilai();" ></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td></td>
                    <td></td>
                </tr>

                <tr bgcolor="#ebecec">
                    <td><b>Jangka Waktu Kredit / Fasilitas</b></td>
                    <td><asp:TextBox ID="TxtJangkaWaktu" runat="server" Width="20%" CssClass="form-control" MaxLength="3" Value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);fillNilai();" Style="display: inline"></asp:TextBox>&nbsp Bulan</td>
                    <td></td>
                    <td></td>
                </tr>

                <tr bgcolor="#f6f7f7">
                    <td><b>Grace Period</b></td>
                    <td><asp:TextBox ID="TxtGrace" runat="server" Width="20%" MaxLength="2" CssClass="form-control" Value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);fillNilai();" Style="display: inline"></asp:TextBox>&nbsp Bulan</td>
                    <td></td>
                    <td></td>
                </tr>


                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td>
                        <b>Total Angsuran/Bunga</b>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTotal" runat="server" CssClass="form-control" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value); fillNilai();" Style="display:inline"></asp:TextBox>&nbsp Per Bulan
                    </td>
                    <td></td>
                    <td></td>
                </tr>

                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="btn btn-primary"></asp:button>
                        <asp:button id="btnNext" runat="server" Text="Lanjut" CssClass="btn btn-danger"></asp:button>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                    </td>
			    </tr>
            </table>
            </div>
        </div>
    </div>
</asp:Content>
