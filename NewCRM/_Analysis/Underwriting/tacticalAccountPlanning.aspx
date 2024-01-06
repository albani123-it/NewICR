<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="tacticalAccountPlanning.aspx.vb" Inherits="NewCRM.tacticalAccountPlanning" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.SuperForm" Assembly="obout_SuperForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <style type="text/css">
        .cont-header
        {
            background: #ebecec;
            margin-bottom: 10px;
            border: 1px solid #bcbcbc;
            border-radius: 5px;
            font-size: 14px;
        }
        .tdText
        {
            font: 11px Verdana;
            color: #333333;
        }
        .option2
        {
            font: 11px Verdana;
            color: #0033cc;
            padding-left: 4px;
            padding-right: 4px;
        }
        
        
        .ob_fC table td
        {
            white-space: normal !important;
        }
        
        .command-row .ob_fRwF
        {
            padding-left: 200px !important;
        }
        
        .ob_gRETpl #ob_iTSuperForm1_AdditionalInformationContainer
        {
            height: 160px !important;
        }
        
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
        }
        .notif
        {
            margin-bottom: 10px;
            border: 1px solid #bcbcbc;
            border-radius: 5px;
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
        a.notif-close
        {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            width: 1%;
        }
        .aactive
        {
            background-color: transparent;
            border-radius: 11px;
            -moz-border-radius: 11px;
            -webkit-border-radius: 11px;
            color: #fff;
            background-color: #ebebeb;
            border: thin solid #cccccc;
        }
        td
        {
            padding-left: 5px;
        }
        .tbl
        {
            font-family: "Century Gothic" , "Helvetica Neue" , "Lucida Grande" , "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
            font-size: 11px;
        }
        .spanChild
        {
            margin-left: 10px;
        }
        #ui-datepicker-div
        {
            display: none;
        }
        
        #tblFillData
        {
            width: 100%;
            text-align: left;
            border: thin solid #bcbcbc;
            border-radius: 5px;
        }
        
        #tblFillData tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblFillData tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        textarea
        {
            resize: none;
        }
        
        #tblNote
        {
            width: 100%;
            text-align: left;
            border: thin solid #bcbcbc;
            border-radius: 5px;
        }
        
        #tblNote tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblNote tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        #tblTapDtl1
        {
            width: 100%;
            text-align: left;
        }
        #tblTapDtl2
        {
            width: 100%;
            text-align: left;
        }
        
        #tblTapDtl1 tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblTapDtl1 tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        #tblTapDtl2
        {
            width: 100%;
            text-align: left;
        }
        
        #tblTapDtl2 tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblTapDtl2 tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        .tblDetails
        {
            border-collapse: collapse;
            width: 100%;
        }
        
        .tblDetails tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        .tblDetails tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
        #btnAddNewWS
        {
            display: block;
        }
        
        #tblWalledShare
        {
            width: 100%;
            display: none;
        }
        
        #btnAddNewAP
        {
            display: block;
        }
        
        #tblActionPlan
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        aMenu("Analysis");
        aBc("TacticalAccountPlanning");
        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
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
            PageMethods.getDataWalledShareById(record.ltapw_id, Succ, Fail);
            return false;
        }

        function OnBeforeEditAP(record) {
            PageMethods.getDataAPById(record.ltapa_id, SuccAP1, FailAP1);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
//            console.log('test');
            return false;
        }

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

        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
        }

        function callReadOnly(){
//            console.log('callfunction');
             $('#btnAddNewWS').prop( "disabled", true );
             $('#btnAddNewAP').prop( "disabled", true );
        }

        function hitung1(){
            var var1 = document.getElementById("<%=txtTotalBank1.clientID %>").value;
            var var2 = document.getElementById("<%=txtBdki1.clientID %>").value;

            if ((formatCurrency(var1).replace(/,/g,"")) == 0) {
                document.getElementById("<%=txtPersentaseBdki1.clientID %>").value=0

            }else if ((formatCurrency(var2).replace(/,/g,"")) == 0 ){
                document.getElementById("<%=txtPersentaseBdki1.clientID %>").value=0
            }else{
                document.getElementById("<%=txtPersentaseBdki1.clientID %>").value = ((formatCurrency(var2).replace(/,/g,""))/ (formatCurrency(var1).replace(/,/g,"")) * 100).toFixed(2) ;
            }
        }

        function hitung2(){
            var var1 = document.getElementById("<%=txtTotalBank2.clientID %>").value;
            var var2 = document.getElementById("<%=txtBdki2.clientID %>").value;

            if ((formatCurrency(var1).replace(/,/g,"")) == 0) {
                document.getElementById("<%=txtPersentaseBdki2.clientID %>").value=0

            }else if ((formatCurrency(var2).replace(/,/g,"")) == 0 ){
                document.getElementById("<%=txtPersentaseBdki2.clientID %>").value=0
            }else{
                document.getElementById("<%=txtPersentaseBdki2.clientID %>").value = ((formatCurrency(var2).replace(/,/g,""))/ (formatCurrency(var1).replace(/,/g,"")) * 100).toFixed(2) ;
            }
        }

        function clearData(){
            var currentYear = (new Date).getFullYear();
             $('#<%=txtIdWS.clientID %>').val(0);
             $('#<%=ddlKategoriProduk.clientID %>').val('funding');
             $('#<%=txtNamaProduk.clientID %>').val('');
             $('#<%=ddlPeriode1.clientID %>').val(currentYear);
             $('#<%=txtTotalBank1.clientID %>').val('0');
             $('#<%=txtBdki1.clientID %>').val('0');
             $('#<%=txtRates1.clientID %>').val('0');
             $('#<%=txtRev1.clientID %>').val('0');
             $('#<%=txtPersentaseBdki1.clientID %>').val('0');
             $('#<%=ddlPeriode2.clientID %>').val(currentYear);
             $('#<%=txtTotalBank2.clientID %>').val('0');
             $('#<%=txtBdki2.clientID %>').val('0');
             $('#<%=txtRates2.clientID %>').val('0');
             $('#<%=txtRev2.clientID %>').val('0');
             $('#<%=txtPersentaseBdki2.clientID %>').val('0');
        }

        function clearDataAP(){
            var currentYear = (new Date).getFullYear();
             $('#<%=txtIdAP.clientID %>').val(0);
             $('#<%=ddlThnMulai.clientID %>').val(currentYear);
             $('#<%=ddlThnSelesai.clientID %>').val(currentYear);
             $('#<%=txtResponsible.clientID %>').val('');
             $('#<%=txtActivity.clientID %>').val('');
             $('#<%=chkProspek.clientID %>').attr('checked',false)
        }



        function Succ(result){
            var data = JSON.parse(result);
            $('#<%=txtIdWS.clientID %>').val(data[0].ltapw_id);
            $('#<%=ddlKategoriProduk.clientID %>').val(data[0].ltapw_kategori_produk);
            $('#<%=txtNamaProduk.clientID %>').val(data[0].ltapw_nama_produk);
            $('#<%=ddlPeriode1.clientID %>').val(data[0].ltapw_periode1);
            $('#<%=txtTotalBank1.clientID %>').val(formatCurrency(data[0].ltapw_total_bank1));
            $('#<%=txtBdki1.clientID %>').val(formatCurrency(data[0].ltapw_bdki1));
            $('#<%=txtRates1.clientID %>').val(formatCurrency(data[0].ltapw_rate1));
            $('#<%=txtRev1.clientID %>').val(formatCurrency(data[0].ltapw_revenue1));
            $('#<%=txtPersentaseBdki1.clientID %>').val(data[0].ltapw_persen_bdki1);
            $('#<%=ddlPeriode2.clientID %>').val(data[0].ltapw_periode2);
            $('#<%=txtTotalBank2.clientID %>').val(formatCurrency(data[0].ltapw_total_bank2));
            $('#<%=txtBdki2.clientID %>').val(formatCurrency(data[0].ltapw_bdki2));
            $('#<%=txtRates2.clientID %>').val(formatCurrency(data[0].ltapw_rate2));
            $('#<%=txtRev2.clientID %>').val(formatCurrency(data[0].ltapw_revenue2));
            $('#<%=txtPersentaseBdki2.clientID %>').val(data[0].ltapw_persen_bdki2);
            $('#tblWalledShare').css('display','block');
            $('#btnAddNewWS').css('display','none');
        }

        function Fail(result){
        }

        function Succ1(result){
            var data = JSON.parse(result);
            var kode=$('#<%=txtTapKode.clientID %>').val();
            if (data.length > 0) {
                $('#tblActionPlan').css('display','block');
                $('#btnAddNewAP').css('display','none');
            }else{
                alert('Wallet Share belum diinput');
                return;
            }
        }
        function Fail1(result){
        }

        function SuccAP1(result){
            var data = JSON.parse(result);
            console.log(data[0].ltapa_prospek );
            $('#<%=txtIdAP.clientID %>').val(data[0].ltapa_id);
            $('#<%=ddlNamaProduk.clientID %>').val(data[0].ltapa_nama_produk);
            $('#<%=ddlThnMulai.clientID %>').val(data[0].ltapa_tahun_mulai);
            $('#<%=ddlThnSelesai.clientID %>').val(data[0].ltapa_tahun_selesai);
            $('#<%=txtResponsible.clientID %>').val(data[0].ltapa_responsible);
            $('#<%=txtActivity.clientID %>').val(data[0].ltapa_activity);
            if (data[0].ltapa_prospek == 1) {
                $('#<%=chkProspek.clientID %>').attr('checked',true)
            }else{
                $('#<%=chkProspek.clientID %>').attr('checked',false)
            }
            $('#tblActionPlan').css('display','block');
            $('#btnAddNewAP').css('display','none');
        }

        function FailAP1(result){
        }

        
        $(document).ready(function(){
//        console.log('<%= menuNext %>');
         $("[data-name=date] ").datepicker(
            { 
                duration: '',
                yearRange: "-100:+20",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });

            $('#btnAddNewWS').click(function(){
                $('#tblWalledShare').css('display','block');
                $('#btnAddNewWS').css('display','none');
                clearData();
            });

            $('#btnCanceledWS').click(function(){
                $('#tblWalledShare').css('display','none');
                $('#btnAddNewWS').css('display','block');
                clearData();
            });

            $('#btnAddNewAP').click(function(){
                clearDataAP();
                $('#tblActionPlan').css('display','block');
                $('#btnAddNewAP').css('display','none');
            });

            $('#btnCanceledAP').click(function(){
                clearDataAP();
                $('#tblActionPlan').css('display','none');
                $('#btnAddNewAP').css('display','block');
            });

        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 100%">
        <div id='cssmenu'>
            <ul style="float: left; margin-right: 10px;">
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
                            <td width="20%">
                                <span><b>No. Aplikasi</b></span>
                            </td>
                            <td>
                                <asp:Label ID="lblAppNo_header" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><b>Nama Pemohon</b></span>
                            </td>
                            <td>
                                <asp:Label ID="lblBrwName_header" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
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
                
                    <table id="tblFillData" class="table table-striped table-bordered" width="100%">
                    <tr >
                        <td colspan="4" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="display: none">
                            <asp:TextBox ID="txtTapKode" runat="server" CssClass="form-control">-</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="detail" style="border-radius: 5px 5px 0 0;">
                            <span style="font-size: 14px; font-weight: bolder">Tactical Account Planning</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Tanggal</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTanggal" Width="250px" data-name="date" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Unit Pengusul</b>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtUnitPengusul" runat="server" CssClass="inpTxt" Width="300px"></asp:TextBox>--%>
                             <asp:DropDownList ID="ddlUnitPengusul" Width="250px" name="ddlUnitPengusul" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Funding</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFunding" runat="server" CssClass="form-control" TextMode="MultiLine"
                                Rows="3" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Lending</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLending" runat="server" CssClass="form-control" TextMode="MultiLine"
                            Rows="3" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Services</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtServices" runat="server" CssClass="form-control" TextMode="MultiLine"
                            Rows="3" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <fieldset>
                    <legend style="font-weight: bold">Wallet Share</legend>
                    <input runat="server" id="tempCodeWS" placeholder="TempCodeWS" type="hidden" />
                    <table id="tblTapDtl1" class="tbl" cellspacing="0" cellpadding="3">
                        <tr>
                            <td width="100%">
                                <obout:Grid ID="grid1" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                    EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowAddingRecords="false"
                                    AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" />
                                    <Columns>
                                        <obout:Column ID="Column1" runat="server" DataField="ltapw_id" Visible="false" />
                                        <obout:Column ID="Column2" runat="server" DataField="ltapw_ltap_kode" Visible="false" />
                                        <obout:Column ID="Column3" runat="server" DataField="ltapw_kategori_produk" HeaderText="Ketegori Produk"
                                            ShowFilterCriterias="false" Width="15%">
                                            <TemplateSettings RowEditTemplateControlId="txtNama" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="NamaFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column4" runat="server" DataField="ltapw_nama_produk" HeaderText="Nama Produk"
                                            ShowFilterCriterias="false" Width="15%">
                                            <TemplateSettings RowEditTemplateControlId="txtNamaProduk" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="NamaProdukFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column5" runat="server" DataField="ltapw_periode1" HeaderText="Periode I"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtPeriode1" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="Periode1Filter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column6" runat="server" DataField="ltapw_persen_bdki1" HeaderText="Persen BANK I"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtPersenBDKI1" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="PersenBDKI1" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column9" runat="server" DataField="ltapw_periode2" HeaderText="Periode II"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtPeriode2" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="Periode2Filter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column10" runat="server" DataField="ltapw_persen_bdki2" HeaderText="Persen BANK II"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtPersenBDKI2" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="PersenBDKI2Filter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column7" runat="server" HeaderText="Action" Width="5%" AllowEdit="true">
                                        </obout:Column>
                                        <obout:Column ID="Column8" runat="server" Width="5%">
                                            <TemplateSettings TemplateId="deleteTemplateWS" />
                                        </obout:Column>
                                    </Columns>
                                    <Templates>
                                        <obout:GridTemplate runat="server" ID="deleteTemplateWS">
                                            <Template>
                                                <% If allowDelete = True Then%>
                                                <asp:LinkButton ID="delDtlWS" runat="server" OnCommand="deleteWS" OnClientClick="return confirm('Are you certain you want to delete?');"
                                                    CommandArgument='<%# Container.PageRecordIndex %>' class='ob_gAL'>  Delete  </asp:LinkButton>
                                                <%End If%>
                                            </Template>
                                        </obout:GridTemplate>
                                    </Templates>
                                </obout:Grid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="button" id="btnAddNewWS" value="Tambah WS Baru" class="btn btn-info" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="table table-striped table-bordered" id="tblWalledShare" >
                                    <tr>
                                        <td colspan="6" style="display: none">
                                            <asp:TextBox ID="txtIdWS" runat="server" CssClass="inpTxt">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%">
                                            Ketegori Kebutuhan Produk
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList ID="ddlKategoriProduk" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="12%">
                                            Periode I
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList Width="150px" ID="ddlPeriode1" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="12%">
                                            Periode II
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList Width="150px" ID="ddlPeriode2" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nama Produk
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNamaProduk" runat="server" CssClass="form-control" Width="180px"></asp:TextBox>
                                        </td>
                                        <td>
                                            Total Bank I
                                        </td>
                                        <td>
                                                <asp:TextBox Width="220px" ID="txtTotalBank1" runat="server" Style="text-align: right; display:inline" CssClass="form-control"
                                                onkeyup="hitung1();this.value=formatCurrency(this.value)">0</asp:TextBox>
                                                 <span style="color:Red;font-size:8pt;">&nbsp;(Rp)</span>
                                        </td>
                                        <td>
                                            Total Bank II
                                        </td>
                                        <td>
                                            
                                            <asp:TextBox ID="txtTotalBank2" Width="220px" runat="server" Style="text-align: right;display:inline" CssClass="form-control"
                                            onkeyup="hitung2();this.value=formatCurrency(this.value)">0</asp:TextBox>
                                            <span style="color:Red;font-size:8pt;">&nbsp;(Rp)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            BANK I
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBdki1" Width="220px" runat="server" Style="text-align: right; display: inline" CssClass="form-control"
                                            onkeyup="hitung1();this.value=formatCurrency(this.value)">0</asp:TextBox>
                                            <span style="color:Red;font-size:8pt;">&nbsp;(Rp)</span>
                                        </td>
                                        <td>
                                            BANK II
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBdki2" runat="server" Width="220px" Style="text-align: right;display:inline" CssClass="form-control"
                                            onkeyup="hitung2();this.value=formatCurrency(this.value)">0</asp:TextBox>
                                                <span style="color:Red;font-size:8pt;">&nbsp;(Rp)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Rate I
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRates1" Width="220px" runat="server" Style="text-align: right;display:inline" CssClass="form-control">0</asp:TextBox>
                                             <span style="color:Red;font-size:8pt;">&nbsp;(%)</span>
                                        </td>
                                        <td>
                                            Rate II
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRates2" Width="220px" runat="server" Style="text-align: right;display:inline" CssClass="form-control">0</asp:TextBox>
                                             <span style="color:Red;font-size:8pt;">&nbsp;(%)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Revenue I
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRev1" runat="server" Style="text-align: right;display:inline" Width="220px" CssClass="form-control"
                                                onkeyup="this.value=formatCurrency(this.value)">0</asp:TextBox>
                                                 <span style="color:Red;font-size:8pt;">&nbsp;(Rp)</span>
                                        </td>
                                        <td>
                                            Revenue II
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtRev2" runat="server" Style="text-align: right; display: inline" Width="220px" CssClass="form-control"
                                                onkeyup="this.value=formatCurrency(this.value)">0</asp:TextBox>
                                                 <span style="color:Red;font-size:8pt;">&nbsp;(Rp)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Presentase BANK I
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPersentaseBdki1" runat="server" Style="text-align: right; display: inline" Width="220px" CssClass="form-control"
                                                    ReadOnly="true" onkeyup="hitung1();this.value=formatCurrency(this.value)">0</asp:TextBox>
                                                 <span style="color:Red;font-size:8pt;">&nbsp;(%)</span>
                                        </td>
                                        <td>
                                            Presentase BANK II
                                        </td>
                                        <td>
                                               <asp:TextBox ID="txtPersentaseBdki2" runat="server" Style="text-align: right; display: inline" Width="220px" CssClass="form-control"
                                                        ReadOnly="true" onkeyup="hitung2();this.value=formatCurrency(this.value)">0</asp:TextBox>
                                                 <span style="color:Red;font-size:8pt;">&nbsp;(%)</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnSavedWS" OnClick="btnSavedWS_Click" runat="server" Text="Simpan"
                                                CssClass="btn btn-primary" />
                                            &nbsp;
                                            <input type="button" id="btnCanceledWS" value="Batal" class="btn btn-default" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend style="font-weight: bold">Action Plan</legend>
                    <input runat="server" id="tempCodeAP" placeholder="tempCodeAP" type="hidden" />
                    <table id="tblTapDtl2" class="tbl" cellspacing="0" cellpadding="3">
                        <tr>
                            <td width="100%">
                                <obout:Grid ID="grid2" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                    EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowAddingRecords="false"
                                    AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEditAP" />
                                    <Columns>
                                        <obout:Column ID="Column11" runat="server" DataField="ltapa_id" Visible="false" />
                                        <obout:Column ID="Column12" runat="server" DataField="ltapa_ltap_kode" Visible="false" />
                                        <obout:Column ID="Column13" runat="server" DataField="ltapa_nama_produk" HeaderText="Nama Produk"
                                            ShowFilterCriterias="false" Width="15%">
                                            <TemplateSettings RowEditTemplateControlId="txtNamaProduk" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="NamaProdukFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column14" runat="server" DataField="ltapa_tahun_mulai" HeaderText="Tahun Mulai"
                                            ShowFilterCriterias="false" Width="15%">
                                            <TemplateSettings RowEditTemplateControlId="txtTahunMulai" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="TahunMulaiFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column15" runat="server" DataField="ltapa_tahun_selesai" HeaderText="Tahun Selesai"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtTahunSelesai" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="TahunSelesaiFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column16" runat="server" DataField="ltapa_responsible" HeaderText="Responsible"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtResponsible" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="ResponsibleFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column17" runat="server" DataField="ltapa_activity" HeaderText="Activity"
                                            ShowFilterCriterias="false" Width="10%">
                                            <TemplateSettings RowEditTemplateControlId="txtActivity" RowEditTemplateControlPropertyName="value"
                                                FilterTemplateId="ActivityFilter" />
                                            <FilterOptions>
                                                <obout:FilterOption IsDefault="true" Type="Contains" />
                                            </FilterOptions>
                                        </obout:Column>
                                        <obout:Column ID="Column19" runat="server" HeaderText="Action" Width="5%" AllowEdit="true">
                                        </obout:Column>
                                        <obout:Column ID="Column20" runat="server" Width="5%">
                                            <TemplateSettings TemplateId="deleteTemplateAP" />
                                        </obout:Column>
                                    </Columns>

                                    <Templates>
                                        <obout:GridTemplate runat="server" ID="deleteTemplateAP">
                                            <Template>
                                                <% If allowDelete = True Then%>
                                                <asp:LinkButton ID="delDtlAP" runat="server" OnCommand="deleteAP" OnClientClick="return confirm('Are you certain you want to delete?');"
                                                    CommandArgument='<%# Container.PageRecordIndex %>' class='ob_gAL'>  Delete  </asp:LinkButton>
                                                <%End If%>
                                            </Template>
                                        </obout:GridTemplate>
                                    </Templates>

                                </obout:Grid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="button" id="btnAddNewAP" value="Tambah AP Baru" class="btn btn-info" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                <table class="table table-striped table-bordered" id="tblActionPlan">
                                    <tr>
                                        <td style="display: none">
                                            <asp:TextBox ID="txtIdAP" runat="server" CssClass="form-control">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            Nama Produk
                                        </td>
                                        <td width="10%">
                                            Tahun Mulai
                                        </td>
                                        <td width="10%">
                                            Tahun Selesai
                                        </td>
                                        <td width="20%">
                                            Responsible
                                        </td>
                                        <td width="10%">
                                            Activity
                                        </td>
                                        <td width="10%">
                                            Prospect
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlNamaProduk" runat="server" CssClass="form-control" Visible=false>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtNamaProduk1" runat="server" CssClass="form-control" Width="180px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlThnMulai" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlThnSelesai" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtResponsible" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtActivity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <input type="checkbox" runat="server" id="chkProspek" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Button ID="btnSavedAP" OnClick="btnSavedAP_Click" runat="server" Text="Simpan"
                                                CssClass="btn btn-primary" />
                                            &nbsp;
                                            <input type="button" id="btnCanceledAP" value="Batal" class="btn btn-default" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table id="tblNote" class="tbl" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <b>Catatan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCatatan" runat="server" CssClass="form-control" TextMode="MultiLine"
                                Rows="3" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn btn-primary"></asp:Button>
                            <input type="button" value="Lanjut" onclick="fDetail('<%= menuNext %>');" class="btn btn-danger" />
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">* <span style="font-size: 13px;">
                                Harus Diisi</span></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
    