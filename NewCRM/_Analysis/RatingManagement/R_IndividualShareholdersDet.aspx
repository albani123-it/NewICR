<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="R_IndividualShareholdersDet.aspx.vb" Inherits="NewCRM.R_IndividualShareholdersDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Skyworx - Internal Credit Rating</title>
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
        #ui-datepicker-div {display: none;}
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
            $("#txtTglLahir").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#txtTglKTP").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#txtExpKTP").datepicker(
            { duration: '',
                yearRange: "-10:+50",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="brw_code" runat="server" />
    <input type="hidden" id="sisa" runat="server" />
    <div>
        <center>
            <table cellspacing="0" cellpadding="3" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;margin-bottom:5px;">
                <tr>
                    <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="18%"><b>Nama</b></td>
                    <td width="26%">
                        <asp:TextBox ID="txtNama" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td width="18%"><b>Alamat Rumah</b></td>
                    <td width="38%">
                        <asp:TextBox ID="txtAlamatRumah" runat="server" CssClass="inpTxt" Width="200px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Gelar Depan</b></td>
                    <td><asp:TextBox ID="txtGelarD" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                    <td><b>Kode Pos</b></td>
                    <td><asp:TextBox ID="txtKodePos" runat="server" CssClass="inpTxt" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Gelar Belakang</b></td>
                    <td><asp:TextBox ID="txtGelarB" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                    <td><b>Kelurahan</b></td>
                    <td><asp:TextBox ID="txtKelurahan" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Jabatan</b></td>
                    <td><asp:TextBox ID="txtJabatan" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                    <td><b>Kecamatan</b></td>
                    <td><asp:TextBox ID="txtKecamatan" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Kepemilikan Saham (%)</b></td>
                    <td>
                        <asp:TextBox ID="txtKepemilikanSaham" runat="server" CssClass="inpTxt" value="0" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Kotamadya/Kabupaten</b></td>
                    <td><asp:TextBox ID="txtKabupaten" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Jumlah Lembar Saham</b></td>
                    <td>
                        <asp:textbox ID="txtJumlahLembarSaham" runat="server" class="inpTxt" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)"></asp:textbox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Propinsi</b></td>
                    <td><asp:TextBox ID="txtPropinsi" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Tempat Lahir</b></td>
                    <td>
                        <asp:TextBox ID="txtTempatLahir" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>No. Telepon Rumah</b></td>
                    <td>
                        <asp:TextBox ID="txtKodeAreaTlp" runat="server" CssClass="inpTxt" Width="50px" style="margin-right:3px;float:none;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <asp:TextBox ID="txtTlp" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                        <asp:TextBox ID="txtExtTlp" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Tgl. Lahir</b></td>
                    <td>
                        <asp:TextBox ID="txtTglLahir" runat="server" CssClass="inpTxt" style="float:none;" ReadOnly="true">
                        </asp:TextBox><span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                    <td><b>No. Handphone</b></td>
                    <td>
                        <asp:TextBox ID="txtNoHP" runat="server" CssClass="inpTxt" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>No. KTP</b></td>
                    <td>
                        <asp:TextBox ID="txtNoKTP" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Nama Gadis Ibu Kandung</b></td>
                    <td>
                        <asp:TextBox ID="txtNamaIbu" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <%--<span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>--%>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Tgl. Pembuatan KTP</b></td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTglKTP" runat="server" CssClass="inpTxt" style="float:none;" ReadOnly="true"></asp:TextBox><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Tgl. Kadaluarsa KTP</b></td>
                    <td colspan="3">
                        <asp:TextBox ID="txtExpKTP" runat="server" CssClass="inpTxt" style="float:none;" ReadOnly="true"></asp:TextBox><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>NPWP</b></td>
                    <td colspan="3">
                        <asp:TextBox ID="txtNPWP" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Jenis Kelamin</b></td>
                    <td colspan="3"><asp:DropDownList ID="ddlJenisKelamin" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Simpan" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Batal" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
