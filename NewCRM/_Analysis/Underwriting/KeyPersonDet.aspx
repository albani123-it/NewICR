<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="KeyPersonDet.aspx.vb" Inherits="NewCRM.KeyPersonDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
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
        #ui-datepicker-div {display: none;}
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
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
    <input type="hidden" id="brw_code" runat="server" />
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
             <table class="table table-striped table-bordered" cellspacing="0" cellpadding="2" width="98%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px; margin-bottom: 10px;">
                <tr>
                    <td colspan="4" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="20%">
                        <b>Nama</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtNama" runat="server" CssClass="form-control" Width="250px" Style="display:inline"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                    <td width="20%">
                        <b>Tanggal Lahir</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtTgllahir" ReadOnly="true" data-name="date" runat="server" CssClass="form-control" Width="250px" Style="display: inline"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td>
                        <b>Jenis Kelamin</b>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" Width="250px" Style="display: inline">
                        </asp:DropDownList><span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp; *</span>
                    </td>
                    
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Alamat</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtAlamat" runat="server" Rows="3" TextMode="multiline" Width="250px"
                        CssClass="form-control" Style="display: inline"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr runat="server" id="rowNamaPerusahaan" style="display:none;" bgcolor="#ebecec">
                    <td>
                        <b>Nama Perusahaan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNamaPerusahaan" runat="server" Width="250px" CssClass="form-control" Style="display:inline"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Nama Jabatan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNamaJabatan" runat="server" Width="250px" CssClass="form-control" Style="display:inline"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td>
                        <b>Hubungan Antar Pengurus</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtHUbunganAntarPengurus" runat="server" Width="250px" CssClass="form-control" Style="display:inline"></asp:TextBox>
                        <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Masa Kerja Dari</b>
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtMasaKerjaDari"  ReadOnly="true" data-name="date" runat="server" CssClass="form-control"  Width="250px" Style="display:inline"></asp:TextBox>
                        &nbsp; &nbsp;&nbsp;<b>Sampai</b>  &nbsp; &nbsp;&nbsp;
                        <asp:TextBox ID="txtSampaiTgl" ReadOnly="true" data-name="date" runat="server" CssClass="form-control" Width="250px" Style="display: inline"></asp:TextBox>
                    </td>
                </tr>
                 <tr bgcolor="#ebecec">
                    <td>
                        <b>Riwayat Pekerjaan</b>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRiwayat" runat="server" Rows="3" TextMode="multiline" Width="250px"
                            CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn btn-primary"></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn btn-danger" OnClientClick="CloseMe()"
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
