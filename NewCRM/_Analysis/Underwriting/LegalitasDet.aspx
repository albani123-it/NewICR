<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LegalitasDet.aspx.vb" Inherits="NewCRM.LegalitasDet" %>

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
        $(function() {
            $("#txtTglLegalitas").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#txtTglJatuhTempo").datepicker(
            { duration: '',
                yearRange: "-100:+5",
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
    <div>
        <center>
                <table class="table table-striped table-bordered" cellspacing="0" cellpadding="1" width="98%" style="text-align: left; border: thin solid #bcbcbc; border-radius: 5px; margin-top: 5px;">
                <tr>
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="40%"><b>Jenis Ijin</b></td>
                    <td><asp:DropDownList ID="ddlJenisIjin" runat="server" CssClass="form-control" style="max-width:200px;"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>No. Legalitas Usaha</b></td>
                    <td>
                        <asp:TextBox ID="txtNoLegalitas" Style="display:initial" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Tgl. Legalitas Usaha</b></td>
                    <td>
                        <asp:TextBox ID="txtTglLegalitas" runat="server" Width="250px" ReadOnly="true" CssClass="form-control" style="float:none;"></asp:TextBox><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Tgl. Jatuh Tempo</b></td>
                    <td>
                        <asp:TextBox ID="txtTglJatuhTempo" runat="server" Width="250px" ReadOnly="true" CssClass="form-control" style="float:none;"></asp:TextBox><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Perihal</b></td>
                    <td><asp:TextBox ID="txtPerihal" runat="server" TextMode="MultiLine" Rows="7"  Width="260px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>No. Pengesahan Instansi Berwenang</b></td>
                    <td><asp:TextBox ID="txtNoPengesahan" runat="server" CssClass="form-control" Width="250px"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Nama Notaris</b></td>
                    <td><asp:TextBox ID="txtNamaNotaris" runat="server" Width="250px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Keterangan</b></td>
                    <td><asp:TextBox ID="txtKeterangan" runat="server" CssClass="form-control" Width="260px" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
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
