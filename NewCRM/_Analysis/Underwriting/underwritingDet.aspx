<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="underwritingDet.aspx.vb" Inherits="NewCRM.underwritingDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.js"></script>
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
        .labelFound
        {
            color:green;
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
            if ($("#RadNew").attr("checked") == "checked") {
                $('#ddlJenisPengajuan').attr("disabled", true);
                $("#ddlJenisPengajuan").addClass("inpDdlDisabled");
                $("#ddlJenisPengajuan").val("B1");
            }
            else {
                $('#ddlJenisPengajuan').attr("disabled", false);
                $("#ddlJenisPengajuan").addClass("inpDdl");
            }
		}
        function fCheck(){
            if (document.getElementById("chkRetail").checked){
                document.getElementById("ddlRetail").disabled=false;
                document.getElementById("ddlRetail").style.backgroundColor = "#fff";
                document.getElementById("ddlBrwType").disabled=true;
                document.getElementById("ddlBrwType").style.backgroundColor = "#ebecec";
                document.getElementById("ddlBrwType").selectedIndex=0;
                document.getElementById("ddlPrdType").disabled=true;
                document.getElementById("ddlPrdType").style.backgroundColor = "#ebecec";
                document.getElementById("ddlPrdType").selectedIndex=0;
            }
            else{
                document.getElementById("ddlBrwType").disabled=false;
                document.getElementById("ddlBrwType").style.backgroundColor = "#fff";
                document.getElementById("ddlPrdType").disabled=false;
                document.getElementById("ddlPrdType").style.backgroundColor = "#fff";
                document.getElementById("ddlRetail").disabled=true;
                document.getElementById("ddlRetail").style.backgroundColor = "#ebecec";
                document.getElementById("ddlRetail").selectedIndex=0;
            }
            
        }
        function fCheck2(){
            if (document.getElementById("chkRetailEx").checked){
                document.getElementById("ddlRetailEx").disabled=false;
                document.getElementById("ddlRetailEx").style.backgroundColor = "#fff";
                document.getElementById("ddlBrwTypEx").disabled=true;
                document.getElementById("ddlBrwTypEx").style.backgroundColor = "#ebecec";
                document.getElementById("ddlBrwTypEx").selectedIndex=0;
                document.getElementById("ddlPrdTypEx").disabled=true;
                document.getElementById("ddlPrdTypEx").style.backgroundColor = "#ebecec";
                document.getElementById("ddlPrdTypEx").selectedIndex=0;
            }
            else{
                document.getElementById("ddlBrwTypEx").disabled=false;
                document.getElementById("ddlBrwTypEx").style.backgroundColor = "#fff";
                document.getElementById("ddlPrdTypEx").disabled=false;
                document.getElementById("ddlPrdTypEx").style.backgroundColor = "#fff";
                document.getElementById("ddlRetailEx").disabled=true;
                document.getElementById("ddlRetailEx").style.backgroundColor = "#ebecec";
                document.getElementById("ddlRetailEx").selectedIndex=0;
            }
            
        }
        $(document).ready(function(){
            $("#ddlJenisPengajuan").change(function() {
                if (($("#ddlJenisPengajuan option:selected").val() != "B1") || ($("#ddlJenisPengajuan option:selected").text() != "Baru")){
                    $("#btnSearchApp").show();
                    $("#txtNoAppEx").show();
                }
                else{
                    $("#btnSearchApp").hide();
                    $("#txtNoAppEx").hide();
                }
                document.getElementById("<%= LblError.ClientID %>").innerHTML = "";
                $('#lblBrwCode').attr('readonly', false);
            });

            $("#btnSearchApp").click(function () {
                var appcode = document.getElementById("<%= txtNoAppEx.ClientID %>").value;
                var cif = document.getElementById("<%= lblBrwCode.clientID %>").value;
                var mix = appcode + "," + cif;
                PageMethods.checkApps(mix,OnSuccessApps,OnFailureApps);    
            }); 

            function OnSuccessApps(response) {
                if(response == "No. Aplikasi tidak ada.")
                {
                    document.getElementById("<%= LblErrorApps.ClientID %>").innerHTML = "No. Aplikasi tidak ada.";
                    document.getElementById("<%= ddlPrdType.ClientID %>").selectedIndex = 0;
                    $("#LblErrorApps").removeClass("labelFound");
                }
                else
                {
                    var splits = response.split(",");
                    document.getElementById("<%= LblErrorApps.ClientID %>").innerHTML = "No. Aplikasi ditemukan.";
                    $("#LblErrorApps").addClass("labelFound");
                    document.getElementById("<%= ddlPrdType.ClientID %>").value = splits[1];     
                }
            }

            function  OnFailureApps(response) {
                document.getElementById("<%= LblErrorApps.ClientID %>").innerHTML = "No. Aplikasi tidak ada.";
                document.getElementById("<%= ddlPrdType.ClientID %>").selectedIndex = 0;
                $("#LblErrorApps").removeClass("labelFound");
            }
             
            $("#RadExist").click(function() {
                //document.getElementById("<%= lblKets.clientID %>").innerText = "No. CIF";
                $("#BtnSearch").show();
                $("#lblBrwCode").val("");
                document.getElementById("<%= LblError.ClientID %>").innerHTML = "";
                $('#lblBrwCode').attr('readonly', false);
                $("#lblBrwCode").removeClass("inpTxtDisabled");
                $("#lblBrwCode").addClass("inpTxt");

                $('#ddlJenisPengajuan').attr("disabled", false);
                $("#ddlJenisPengajuan").removeClass("inpDdlDisabled");
                $("#ddlJenisPengajuan").addClass("inpDdl");
                $("#btnSearchApp").hide();
                $("#txtNoAppEx").hide();
            });

            $("#RadNew").click(function() {
                //document.getElementById("lblKets").innerText = "Kode Borrower";
                $("#BtnSearch").hide();
                $('#lblBrwCode').attr('readonly', true);
                $("#lblBrwCode").addClass("inpTxtDisabled");
                $("#lblBrwCode").removeClass("inpTxt");
                $('#ddlBrwType').removeAttr('disabled');
                $('#ddlPrdType').removeAttr('disabled');

                
                $("#ddlJenisPengajuan").val("B1");
                $('#ddlJenisPengajuan').attr("disabled", true);
                $("#ddlJenisPengajuan").addClass("inpDdlDisabled");

                $("#btnSearchApp").hide();
                $("#txtNoAppEx").hide();
                document.getElementById("<%= ddlBrwType.ClientID %>").selectedIndex = 0;
                document.getElementById("<%= ddlPrdType.ClientID %>").selectedIndex = 0;
                //getBorrowerCodeNew();
            }); 

            function getBorrowerCodeNew() {
                PageMethods.getBorrowerCodeNew("New",OnSuccessNew,OnErrorNew);
            }

            function OnSuccessNew(result) {
                document.getElementById("<%= lblBrwCode.ClientID %>").value = result;
            }
            function OnErrorNew(result) {
            }
            
            $("#BtnSearch").click(function () {
                var code = document.getElementById("<%= lblBrwCode.ClientID %>").value;
                checkBrwCodes(code);    
            }); 

            function checkBrwCodes(code) {
                PageMethods.checkBrwCode(code,OnSuccess,OnFailure);
            }

            function OnSuccess(response) {
                if(response == "No. NPWP tidak ada.")
                {
                    document.getElementById("<%= LblError.ClientID %>").innerHTML = "No. NPWP tidak ada.";
                    document.getElementById("<%= lblNamaPT.ClientID %>").innerHTML = "";
                    document.getElementById("<%= ddlBrwType.ClientID %>").selectedIndex = 0;
                    document.getElementById("<%= ddlPrdType.ClientID %>").selectedIndex = 0;
                }
                else
                {
                    var splits = response.split(",");
                    document.getElementById("<%= lblBrwCode.ClientID %>").value = splits[0]; 
                    document.getElementById("<%= LblError.ClientID %>").innerHTML = "";
                    document.getElementById("<%= lblNamaPT.ClientID %>").innerHTML = splits[3];
                    document.getElementById("<%= ddlBrwType.ClientID %>").value = splits[1];
                    document.getElementById("<%= ddlPrdType.ClientID %>").value = splits[2];     
                }
            }

            function  OnFailure(response) {
                document.getElementById("<%= LblError.ClientID %>").innerHTML = "No. CIF tidak ada.";
            }
        });
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
    <div style="margin:0;padding:0;">
        <center>
            <table id="tblNew" runat="server" cellspacing="0" cellpadding="3" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td width="30%"><b>Workflow</b></td>
                    <td><asp:DropDownList ID="ddlWorkflow" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;display:none;">
                    <td><b>No. Aplikasi</b></td>
                    <td><asp:Label ID="lblAppno" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="text-align:left;">
                    <td width="30%"><b>Borrower</b></td>
                    <td width="70%">
                        <label><input type="radio" id="RadNew" name="RadBorrower" runat="server" checked />New</label>
                        <label><input type="radio" id="RadExist" name="RadBorrower" runat="server" />Existing</label>
                    </td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;">
                    <td><b><label id="lblKets" runat="server">No. NPWP</label></b></td>
                    <td>
                        <asp:TextBox ID="lblBrwCode" CssClass="inpTxtDisabled" runat="server" ReadOnly="true"></asp:TextBox>
                        &nbsp;&nbsp;
                        <input type="button" ID="BtnSearch" runat="server" class="inpBtn" value="Cari" style="display:none;" />
                        &nbsp;&nbsp;
                        <asp:Label ID="LblError" Text="" runat="server" style="color:Red;" />
                        <asp:Label ID="lblNamaPT" Text="" runat="server" />
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7" style="text-align:left;">
                    <td><b>Jenis Pengajuan</b></td>
                    <td>
                        <asp:DropDownList ID="ddlJenisPengajuan" runat="server" CssClass="inpDdl"></asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="txtNoAppEx" CssClass="inpTxt" runat="server" style="float:none;display:none;" placeholder="No. Aplikasi"></asp:TextBox>
                        &nbsp;&nbsp;
                        <input type="button" ID="btnSearchApp" runat="server" class="inpBtn" value="Cari" style="display:none;" />
                        &nbsp;&nbsp;
                        <asp:Label ID="lblErrorApps" Text="" runat="server" style="color:Red;" />                       
                    </td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;">
                    <td><b>Borrower Type</b></td>
                    <td><asp:DropDownList ID="ddlBrwType" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="text-align:left;">
                    <td><b>Product Type</b></td>
                    <td><asp:DropDownList ID="ddlPrdType" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;display:none;">
                    <td><b>Product Micro Type</b></td>
                    <td>
                        <asp:DropDownList ID="ddlRetail" runat="server" CssClass="inpDdl" Enabled="false" style="background-color:#ebecec;"></asp:DropDownList>
                        <input type="checkbox" id="chkRetail" name="chkRetail" runat="server" onclick="fCheck();" />Yes
                    </td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Simpan" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Batal" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                    </td>
			    </tr>
            </table>
            <table id="tblExists" runat="server" cellspacing="0" cellpadding="3" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;display:none;">
                <tr>
                    <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7" style="display:none;">
                    <td width="30%"><b>Workflow</b></td>
                    <td><asp:DropDownList ID="ddlWorkflowEx" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;">
                    <td><b>No. Aplikasi</b></td>
                    <td><asp:Label ID="lblAppNoEx" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="text-align:left;">
                    <td><b>Kode Borrower</b></td>
                    <td>
                        <asp:TextBox ID="lblBrwCodeEx" CssClass="inpTxt" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr bgcolor="#ebecec" style="text-align:left;">
                    <td><b>Borrower Type</b></td>
                    <td><asp:DropDownList ID="ddlBrwTypEx" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7" style="text-align:left;">
                    <td><b>Product Type</b></td>
                    <td><asp:DropDownList ID="ddlPrdTypEx" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                </tr>
                <tr id="RowMicro" bgcolor="#ebecec" style="text-align:left;display:none;">
                    <td><b>Product Micro Type</b></td>
                    <td>
                        <asp:DropDownList ID="ddlRetailEx" runat="server" CssClass="inpDdl" Enabled="false" style="background-color:#ebecec;"></asp:DropDownList>
                        <input type="checkbox" id="chkRetailEx" name="chkRetailEx" runat="server" onclick="fCheck2();" />Yes
                    </td>
                </tr>
                <tr style="height:30px">
				    <td background="../../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSaveEx" runat="server" Text="Simpan" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancelEx" runat="server" Text="Batal" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>&nbsp;
                        <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
