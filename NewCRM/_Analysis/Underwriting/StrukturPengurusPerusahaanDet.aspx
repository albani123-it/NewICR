<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="StrukturPengurusPerusahaanDet.aspx.vb"
    Inherits="NewCRM.StrukturPengurusPerusahaanDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <link type="text/css" href="../../Scripts/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <style type="text/css">
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
        }
        
        #tblStrukturPengurus
        {
            width: 98%;
            text-align: left;
            border: thin solid #bcbcbc;
            border-radius: 5px;
        }
        
        #tblStrukturPengurus tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblStrukturPengurus tr:nth-child(odd)
        {
            background: #ebecec;
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

        $(function() {
            $("#txtMasaKerjaDari").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });

            $("#txtSampaiTgl").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
         
            $("#txtTgllahir").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                maxDate: 0,
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
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
            <table id="tblStrukturPengurus" class="tbl" cellspacing="0" cellpadding="3">
                <tr>
                    <td colspan="4" class="detail" style="border-radius: 5px 5px 0 0;">
                        <span style="font-size: 14px; font-weight: bolder">Struktur Pengurus Perusahaan Detail</span>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <b>Nama</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtNama" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                    <td width="20%">
                        <b>Tanggal Lahir</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtTgllahir" ReadOnly="true" data-name="date" runat="server" CssClass="inpTxt"></asp:TextBox><span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Jenis Kelamin</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="inpDdl">
                        </asp:DropDownList><span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                    <td>
                        <b>Key Person</b>
                    </td>
                    <td>
                        <input type="checkbox" id="chkKeyPerson" name="chkKeyPerson" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Alamat</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtAlamat" runat="server" Rows="3" TextMode="multiline" Width="200px"
                            CssClass="inpTxt"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr runat="server" id="rowNamaPerusahaan" style="display:none;">
                    <td>
                        <b>Nama Perusahaan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNamaPerusahaan" runat="server" Width="200px" CssClass="inpTxt"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Nama Jabatan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNamaJabatan" runat="server" Width="200px" CssClass="inpTxt"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Hubungan Antar Pengurus</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtHUbunganAntarPengurus" runat="server" Width="200px" CssClass="inpTxt"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Masa Kerja Dari</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMasaKerjaDari"  ReadOnly="true" data-name="date" runat="server" CssClass="inpTxt"></asp:TextBox>
                        &nbsp; &nbsp;&nbsp;<b>Sampai</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtSampaiTgl"  ReadOnly="true" data-name="date" runat="server" CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <b>Riwayat Pekerjaan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRiwayat" runat="server" Rows="3" TextMode="multiline" Width="200px"
                            CssClass="inpTxt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <b>Foto</b>
                    </td>
                    <td colspan="4">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false">
                            <Columns>
                                <asp:ImageField DataImageUrlField="Value" ControlStyle-Height="100" ControlStyle-Width="100" />
                            </Columns>
                        </asp:GridView>
                        <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                        <asp:Button ID="btnAdd" Text="Upload" runat="server" CssClass="inpBtn" />
                        <br />
                        <span>Ukuran File maksimum 200 KB.</span><br /><input type="hidden" id="textFile" runat="server" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="inpBtn"></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="inpBtn" OnClientClick="CloseMe()"
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
