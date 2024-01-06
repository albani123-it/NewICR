<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="callMemoDet.aspx.vb" Inherits="NewCRM.callMemoDet" %>

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
        .inpTxtDisabled
        {
            width: 120px;
        }
        #ui-datepicker-div
        {
            display: none;
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
        $(function() {
            $("#InpTanggal").datepicker(
            { duration: '',
                yearRange: "-100:+20",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
        }

        function isNumberKeyForTlp(evt) {        
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 43)
                return false;

            return true;
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="appno" runat="server" />
    <div>
        <center>
            
            <table class="table table-striped table-bordered" cellspacing="0" cellpadding="2" width="98%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px; margin-bottom: 10px;">
                <tr>
                    
                        <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">
                        &nbsp;
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%">
                        <b>Tanggal</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTgl"  Width="250px" runat="server" CssClass="form-control" ReadOnly="true"
                        Style="float: none; display: inline"></asp:TextBox><br />
                        <span style="color: Red; margin-top: 2px; float: left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td>
                        <b>No. Telepon</b>
                    </td>
                    <td>
                        
                        <asp:TextBox ID="txtNoTelp" Width="250px" runat="server" CssClass="form-control" onkeypress="return isNumberKeyForTlp(event)" Style="display: inline"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Nama Perusahaan</b>
                    </td>
                    <td>
                            <asp:TextBox ID="txtNama" runat="server" CssClass="form-control" Width="250px" Height="20px"
                            TextMode="MultiLine" Style="display: inline"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td colspan="2">
                        <b>(Pihak Lain Yang Di Call)</b>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>No. Telepon</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoTelpLain" style="display:inline" Width="250px"  runat="server" CssClass="form-control" onkeypress="return isNumberKeyForTlp(event)"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td>
                        <b>Bentuk Call</b>
                    </td>
                    <td>
                         <asp:TextBox ID="txtBentukCall" runat="server" CssClass="form-control" Width="250px" Height="20px"
                            TextMode="MultiLine" Style="display: inline"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Penjabat Yang Di Call</b>
                    </td>
                    <td>
                         <asp:TextBox ID="txtPenjabatYangDiCall" runat="server" Style="display: inline" CssClass="form-control" Width="450px"
                                Height="20px" TextMode="MultiLine"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td>
                        <b>Penjabat Yang Melakukan Call</b>
                    </td>
                    <td>
                            <asp:TextBox ID="txtPenjabatYangMelakukanCall" runat="server" CssClass="form-control" Width="450px"
                            Height="20px" TextMode="MultiLine" Style="display: inline"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Tujuan Call</b>
                    </td>
                    <td>
                           <asp:TextBox ID="txtTujuanCall" runat="server" CssClass="form-control" Width="450px" Height="40px"
                                TextMode="MultiLine" Style="display: inline"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td>
                        <b>Hasil</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHasil" runat="server" CssClass="form-control" Width="450px" Height="50px"
                                    TextMode="MultiLine" Style="display: inline"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td>
                        <b>Tindak Lanjut Yang Harus Dilakukan</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTindakLanjut" runat="server" CssClass="form-control" Width="450px"
                                        Height="50px" Style="display: inline" TextMode="MultiLine"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn btn-primary" ></asp:Button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn btn-danger" OnClientClick="CloseMe();">
                        </asp:Button>&nbsp; <span style="color: Red; font-size: 11pt; font-weight: bold;">*
                            <span style="font-size: 13px;">Harus Diisi</span></span>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
