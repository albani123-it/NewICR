<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CorporateShareholdersDet.aspx.vb" Inherits="NewCRM.CorporateShareholdersDet" %>

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
            $("#txtTglAkteKTP").datepicker(
            { duration: '',
                yearRange: "-100:+0",
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
            <table class="table table-striped table-bordered" cellspacing="0" cellpadding="3" width="98%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px; margin-top: 5px; margin-bottom: 5px;">
                <tr>
                    <td colspan="4" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="18%"><b>Tipe Badan</b></td>
                    <td width="26%">
                        <asp:DropDownList ID="ddlTipeBadan" runat="server"  AutoPostBack="True" CssClass="form-control" Width="230px" style="display:inline"></asp:DropDownList>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td width="18%"><b>Alamat</b></td>
                    <td width="38%">
                        <asp:TextBox ID="txtAlamat" runat="server" CssClass="form-control" Style="display: inline" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                        <span id="infoAlamat" runat="server" style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Nama</b></td>
                    <td>
                        <asp:TextBox ID="txtNama" runat="server" CssClass="form-control" Width="230px" Style="display: inline"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Kode Pos</b></td>
                    <td><asp:TextBox ID="txtKodePos" runat="server" CssClass="form-control" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>No. Akte Pendirian/No. KTP</b></td>
                    <td>
                        <asp:TextBox ID="txtNoAkteKTP" runat="server" Width="250px" CssClass="form-control" Style="display: inline"></asp:TextBox>
                        <span runat="server" id="infoAkteNo" style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Kelurahan</b></td>
                    <td><asp:TextBox ID="txtKelurahan" Width="250px" runat="server" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Tgl. Akte Pendirian/Tgl. KTP</b></td>
                    <td>
                        <asp:TextBox ID="txtTglAkteKTP" runat="server" Width="250px" CssClass="form-control" ReadOnly="true" style="float:none;display:inline"></asp:TextBox>
                        <span runat="server" id="infoAkteTgl" style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                    <td><b>Kecamatan</b></td>
                    <td><asp:TextBox ID="txtKecamatan" runat="server" Width="250px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>NPWP</b></td>
                    <td>
                        <asp:TextBox ID="txtNPWP" runat="server" Width="250px" CssClass="form-control" Style="display:inline"></asp:TextBox>
                        <span runat="server" id="infoAkteNPWP" style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Kotamadya/Kabupaten</b></td>
                    <td><asp:TextBox ID="txtKabupaten" runat="server" Width="250px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Kepemilikan Saham (%)</b></td>
                    <td>
                        <asp:TextBox ID="txtKepemilikanSaham" runat="server" Width="230px" Style="display:inline" CssClass="form-control" onkeypress="return isNumberKey(event)">0</asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>Propinsi</b></td>
                    <td><asp:TextBox ID="txtPropinsi" runat="server" Width="250px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Jumlah Lembar Saham</b></td>
                    <td>
                        <asp:TextBox ID="txtJumlahLembarSaham" runat="server" Width="230px" CssClass="form-control" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value)" Style="display:inline">0</asp:textbox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                    <td><b>No. Telepon</b></td>
                    <td>
                        <asp:TextBox ID="txtKodeAreaTlp" runat="server" CssClass="form-control" Width="50px" Style="margin-right: 3px; float: none; margin-bottom: 3px;display:inline" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <asp:TextBox ID="txtTlp" runat="server" CssClass="form-control" Width="170px" Style="float: none; margin-right: 3px; margin-bottom: 3px;display:inline" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                        <asp:TextBox ID="txtExtTlp" runat="server" CssClass="form-control" Width="50px" style="float:none;margin-bottom:3px;display:inline" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Simpan" CssClass="btn btn-primary"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Batal" CausesValidation="False" CssClass="btn btn-danger" onclientclick="CloseMe()"></asp:button>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
