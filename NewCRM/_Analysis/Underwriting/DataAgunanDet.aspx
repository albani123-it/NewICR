<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataAgunanDet.aspx.vb"
    Inherits="NewCRM.DataAgunanDet" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
  
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <link type="text/css" href="../../Scripts/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
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
        
        select
        {
            width: 250px;
        }
        
        .twister tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        .twister tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        #tblFotoLKNInput
        {
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript">
        function CloseMe() {
            clearData();
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 45)
                return false;

            return true;
        }
        function formatCurrency(num)
        {    
            console.log(num.toLocaleString());
            num = num.toString().replace(/\$|\,/g, '');             
             var temp1=num;
             var temp2;
             var x = temp1.split('.');
             if (x.length > 1) 
             {
                num=x[0];
                temp2 = x[1];
                console.log(temp2);
             }
             
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
//            return (((sign) ? '' : '-') + num );
                if (x.length > 1) {
                return (((sign) ? '' : '-') + num + '.'+ temp2);
                }else{
                return (((sign) ? '' : '-') + num );
                }
            }

            //return num.toLocaleString();
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
            $('#txtFotoId').val(record.ldad_id);
            $('#txtFilename').val(record.ldad_image_nama);
            $('#txtInfoFoto').val(record.ldad_image_keterangan);

            $('#rowUploadImage').attr("style", "display:''");
            $('#btnAddNew').attr("style", "display:none");
            return false;
        }

        function clearData(){
            $('#txtFotoId').val(0)
            $('#txtFilename').val('')
            $('#txtInfoFoto').val('')
        }
        
        function callReadOnly(){
//            console.log('callfunction');
             $('#btnAddNew').prop( "disabled", true );
        }

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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="app_no" runat="server" />
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
            <div>
                <table id="tblFillDataHeader" class="tbl twister" cellspacing="0" cellpadding="3"
                    width="98%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px 5px 0px 0px;">
                    <tr>
                        <td colspan="4"  bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;"
                            class="detail">
                            <b>Data Agunan Detail</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="detail" style="display: none">
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <b>Tipe Agunan</b>
                        </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlTipeAgunan" runat="server" CssClass="inpDdl" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="30%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="detail">
                            <b>Header</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Tanggal</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTglAgunan" data-name="date" runat="server" CssClass="inpTxt"
                                Style="float: none;"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span><br />
                            <span style="color: Red; margin-top: 2px; float: left;">(dd-mm-yyyy)</span>
                        </td>
                        <td>
                            <b>Keterangan Lain</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeterangan" runat="server" Rows="3" TextMode="multiline" Width="200px"
                                CssClass="inpTxt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Jenis Agunan</b>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlAgunan" runat="server" CssClass="inpDdl" AutoPostBack="true"
                                OnSelectedIndexChanged="DdlAgunanOnSelectedIndexChanged">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Jenis Pengikatan</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPengikatan" runat="server" CssClass="inpDdl">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Deskripsi</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDeskripsi" runat="server" Rows="3" TextMode="multiline" Width="200px"
                                CssClass="inpTxt"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr style='display: none'>
                        <td>
                            <b>Klasifikasi Agunan</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlKlasAgunan" runat="server" CssClass="inpDdl">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                        <td>
                            <b>Kontrol Agunan</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlControled" runat="server" CssClass="inpDdl">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rowDtl" runat="server" visible="True">
                        <td colspan="4" class="detail">
                            <b>Detail</b>
                        </td>
                    </tr>
                    <tr id="rowStsKepemilikan" runat="server" visible="True">
                        <td>
                            <b>Status Kepemilikan</b>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="inpDdl">
                            </asp:DropDownList>
                           <%-- <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowBuktiKemungkinan" runat="server" visible="True">
                        <td>
                            <b>Bukti Kepemilikan</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtBuktiKepemilikan" runat="server" Rows="3" TextMode="multiline"
                                Width="200px" CssClass="inpTxt"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowLokasiAgunan" runat="server" visible="True">
                        <td>
                            <b>Lokasi Agunan</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLokasiAgunan" runat="server" Rows="3" TextMode="multiline" Width="200px"
                                CssClass="inpTxt"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowNamaPemilik" runat="server" visible="True">
                        <td>
                            <b>Nama Pemilik</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNamaPemilik" runat="server" CssClass="inpTxt" Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowAppraisal" runat="server" visible="True">
                        <td>
                            <b>Appraisal</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAppraisal" runat="server" Width="200px" CssClass="inpTxt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="rowNamaAppraisal" runat="server" visible="True">
                        <td>
                            <b>No Appraisal</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNoAppraisal" runat="server" Width="200px" CssClass="inpTxt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="rowTglAppraisal" runat="server" visible="True">
                        <td>
                            <b>Tanggal Appraisal</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtTglAppraisal" data-name="date" runat="server" CssClass="inpTxt"></asp:TextBox>
                            <span style="color: Red; margin-top: 2px; float: left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr id="rowNilai" runat="server" visible="True">
                        <td colspan="4" class="detail">
                            <b>Nilai</b>
                        </td>
                    </tr>
                    <tr class="notformltfinance">
                        <td colspan="4" style="display: none">
                            <asp:TextBox ID="txtIdDtl" runat="server" CssClass="inpTxt">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr id="rowNilaiPasar" runat="server" visible="True">
                        <td>
                            <b>Nilai Pasar</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNilaiPasar" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rowNilaiLikuidasi" runat="server" visible="True">
                        <td>
                            <b>Nilai Likuidasi</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNilaiLikuidasi" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowNilaiPengikatan" runat="server" visible="True">
                        <td>
                            <asp:Label ID="lblNilaiPengikatan" runat="server"><b>Nilai Pengikatan</b></asp:Label>
                        </td>
                        <td colspan="3">
                            <%--<asp:TextBox ID="txtNilaiPengikatan" style="text-align:right" runat="server" CssClass="inpTxt" value="0" onkeypress="return isNumberKey(event)"
                                onkeyup="this.value=formatCurrency(this.value)" Width="200px"></asp:TextBox>--%>
                            <asp:TextBox ID="txtNilaiPengikatan" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rowAsuransiPenjamin" runat="server" visible="True">
                        <td>
                            <b>Asuransi Penjamin</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAsuransiPenjamin" runat="server" CssClass="inpTxt" Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowNilaiAsuransi" runat="server" visible="True">
                        <td>
                            <b>Nilai Asuransi</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtNilaiAsuransi" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowLuasTB" runat="server" visible="false">
                        <td>
                            <b>Luas Tanah</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLuasTanah" Style="text-align: right" Width="75px" value="0" runat="server"
                                CssClass="inpTxt"></asp:TextBox>&nbsp m<sup>2</sup>(persegi)
                        </td>
                        <td>
                            <b>Luas Bangunan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLuasBangunan" Style="text-align: right" Width="75px" value="0"
                                runat="server" CssClass="inpTxt"></asp:TextBox>&nbsp m<sup>2</sup>(persegi)
                        </td>
                    </tr>
                    <tr id="rowNilaiPasarTB" runat="server" visible="false">
                        <td>
                            <b>Nilai Pasar</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiPasarLT" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                        <td>
                            <b>Nilai Pasar</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiPasarLB" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rowNilaiLikuidTB" runat="server" visible="false">
                        <td>
                            <b>Nilai Likuidasi</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiLikuidasiLT" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                        <td>
                            <b>Nilai Likuidasi</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiLikuidasiLB" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowNilaiPengikatanTB" runat="server" visible="false">
                        <td>
                            <b>Nilai Pengikatan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiPengikatanLT" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                        <td>
                            <b>Nilai Pengikatan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiPengikatanLB" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rowAsuransiPenjaminTB" runat="server" visible="false">
                        <td>
                            <b>Asuransi Penjamin</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAsuransiPenjaminLT" runat="server" CssClass="inpTxt" Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                        <td>
                            <b>Asuransi Penjamin</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAsuransiPenjaminLB" runat="server" CssClass="inpTxt" Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowNilaiAsuransiTB" runat="server" visible="false">
                        <td>
                            <b>Nilai Asuransi</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiAsuransiLT" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                        <td>
                            <b>Nilai Asuransi</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNilaiAsuransiLB" Style="text-align: right" runat="server" CssClass="inpTxt"
                                value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"
                                Width="200px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr id="rowHdrPenarikan" runat="server" visible="false">
                        <td colspan="4" class="detail">
                            <b>Penarikan Agunan</b>
                        </td>
                    </tr>
                    <tr id="rowPenarikanAgunan" runat="server" visible="false">
                        <td>
                            <b>Penarikan Agunan</b>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkPenarikanAgunan" runat="server" AutoPostBack="true" OnCheckedChanged="ChckedChanged" />&nbsp;Ya
                        </td>
                    </tr>
                    <tr id="rowKeteranganPenarikan" runat="server" visible="false">
                        <td>
                            <b>Keterangan Penarikan</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtKetPenarikan" runat="server" Rows="3" TextMode="multiline" Width="200px"
                                CssClass="inpTxt"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rowTglPenarikan" runat="server" visible="false">
                        <td>
                            <b>Tanggal Penarikan</b>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtTanggal" data-name="date" runat="server" CssClass="inpTxt"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span><br />
                            <span style="color: Red; margin-top: 2px; float: left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr id="rowHeaderFotoAgunan" runat="server">
                        <td colspan="4" class="detail">
                            <b>Foto Agunan</b>
                        </td>
                    </tr>
                    <tr id="rowTblFotoAgunan" runat="server">
                        <td colspan="4">
                            <obout:Grid ID="grid1" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowAddingRecords="false"
                                AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" />
                                <Columns>
                                    <obout:Column ID="Column1" runat="server" DataField="ldad_id" Visible="false" />
                                    <obout:Column ID="Column2" runat="server" DataField="lda_id" Visible="false" />
                                    <obout:Column ID="Column3" runat="server" DataField="ldad_image_nama" HeaderText="Foto Agunan"
                                        ShowFilterCriterias="false" Width="15%">
                                        <TemplateSettings RowEditTemplateControlId="txtFotoKunjungan" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="FotoKunjungan" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column ID="Column4" runat="server" DataField="ldad_image_keterangan" HeaderText="Keterangan Foto"
                                        ShowFilterCriterias="false" Width="15%">
                                        <TemplateSettings RowEditTemplateControlId="txtKeteranganFoto" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="KeteranganFoto" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column ID="Column7" runat="server" HeaderText="Action" Width="5%" AllowEdit="true">
                                    </obout:Column>
                                    <obout:Column ID="Column8" runat="server" Width="5%">
                                        <TemplateSettings TemplateId="deleteTemplate" />
                                    </obout:Column>
                                </Columns>
                                <Templates>
                                    <obout:GridTemplate runat="server" ID="deleteTemplate">
                                        <Template>
                                            <%--<asp:LinkButton ID="delDtl" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain you want to delete?');"
                                                        CommandArgument='<%# Container.PageRecordIndex %>' class='ob_gAL'>  Delete  </asp:LinkButton>--%>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="download" CommandArgument='<%# Container.DataItem("ldad_image_nama") %>'
                                                class='ob_gAL'>  Download  </asp:LinkButton>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                            </obout:Grid>
                        </td>
                    </tr>
                    <tr id="rowBtnAddNew" runat="server">
                        <td colspan="4">
                            <asp:Button ID="btnAddNew" runat="server" Text="Tambah Foto" CssClass="inpBtn"></asp:Button>
                        </td>
                    </tr>
                    <tr id="rowUploadImage" runat="server" style="display: none">
                        <td colspan="4">
                            <table width="100%" id="tblFotoLKNInput">
                                <tr>
                                    <td colspan="3" style="display: none">
                                        <asp:TextBox ID="txtFotoId" runat="server" CssClass="inpTxt">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%">
                                        <b>Foto Kunjungan</b>
                                    </td>
                                    <td width="75%">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <br />
                                        <span>Ukuran File maksimum 20MB.</span><br />
                                    </td>
                                    <td width="5%">
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
                                        <asp:Button ID="btnAdd" Text="Upload" runat="server" CssClass="inpBtn" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="footerData">
                <table class="tbl" cellspacing="0" cellpadding="3" width="98%" style="text-align: left;
                    border: thin solid #bcbcbc; border-radius: 0px 0px 5px 5px;">
                    <tr style="height: 30px">
                        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="inpBtn"></asp:Button>&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="inpBtn" OnClientClick="CloseMe()"
                                CausesValidation="false"></asp:Button>&nbsp; <span style="color: Red; font-size: 11pt;
                                    font-weight: bold;">* <span style="font-size: 13px;">Harus Diisi</span></span>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
