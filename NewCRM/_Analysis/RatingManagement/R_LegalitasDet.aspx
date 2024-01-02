<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="R_LegalitasDet.aspx.vb" Inherits="NewCRM.R_LegalitasDet" %>

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
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="40%"><b>Jenis Ijin</b></td>
                    <td><asp:DropDownList ID="ddlJenisIjin" runat="server" CssClass="inpDdl" style="max-width:200px;"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>No. Legalitas Usaha</b></td>
                    <td>
                        <asp:TextBox ID="txtNoLegalitas" runat="server" CssClass="inpTxt"></asp:TextBox>
                        <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Tgl. Legalitas Usaha</b></td>
                    <td>
                        <asp:TextBox ID="txtTglLegalitas" runat="server" ReadOnly="true" CssClass="inpTxt" style="float:none;"></asp:TextBox><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Tgl. Jatuh Tempo</b></td>
                    <td>
                        <asp:TextBox ID="txtTglJatuhTempo" runat="server" ReadOnly="true" CssClass="inpTxt" style="float:none;"></asp:TextBox><br />
                        <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Perihal</b></td>
                    <td><asp:TextBox ID="txtPerihal" runat="server" TextMode="MultiLine" Rows="7"  Width="260px" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>No. Pengesahan Instansi Berwenang</b></td>
                    <td><asp:TextBox ID="txtNoPengesahan" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Nama Notaris</b></td>
                    <td><asp:TextBox ID="txtNamaNotaris" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Keterangan</b></td>
                    <td><asp:TextBox ID="txtKeterangan" runat="server" CssClass="inpTxt" Width="260px" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
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
