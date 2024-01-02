<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="R_ExtRatingInfo.aspx.vb" Inherits="NewCRM.R_ExtRatingInfo" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="customControl" Namespace="GroupDropDownList" Assembly="GroupDropDownList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
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
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
        }
        #ui-datepicker-div
        {
            display: none;
        }
        
        #tblExtInfo
        {
            width: 100%;
            text-align: left;
            border: thin solid #bcbcbc;
            border-radius: 5px;
        }
        
        #tblExtInfo tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblExtInfo tr:nth-child(odd)
        {
            background: #ebecec;
        }
        #tblAsumsi_dtl
        {
            width: 100%;
            border-collapse: collapse;
        }
        
        #tblAsumsi_dtl tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblAsumsi_dtl tr:nth-child(odd)
        {
            background: #ebecec;
        }
        
    </style>
    <script type="text/javascript">
        aBc("General");
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function fDetail(url){
            var brwcode = document.getElementById("<%=brwcode.clientID %>").value;
            if (brwcode == "")
                window.location.href = url;
            else
                window.location.href = url +"?brwcode=" + brwcode + "&typ=Borrower";
        }
        fAction();
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
        function fChange(){
            var choose = document.getElementById("<%=ddlHaveExtRat.ClientID %>").value;
            
            if (choose == "Ya"){
                document.getElementById("<%=ratAgency.clientID %>").style.display = '';
                document.getElementById("<%=corType.clientID %>").style.display = '';
                document.getElementById("<%=rating.clientID %>").style.display = '';
                document.getElementById("<%=ratingDate.clientID %>").style.display = '';
                document.getElementById("<%=desc.clientID %>").style.display = '';
                document.getElementById("<%=btnNext.clientID %>").style.display = 'none';
                document.getElementById("<%=btnSave.clientID %>").style.display = '';
//                document.getElementById("<%=data_asumsi.clientID %>").style.display = '';
//                document.getElementById("<%=data_asumsi_dtl.clientID %>").style.display = '';
            }
            else{
                document.getElementById("<%=ratAgency.clientID %>").style.display = 'none';
                document.getElementById("<%=corType.clientID %>").style.display = 'none';
                document.getElementById("<%=rating.clientID %>").style.display = 'none';
                document.getElementById("<%=ratingDate.clientID %>").style.display = 'none'; 
                document.getElementById("<%=desc.clientID %>").style.display = 'none';   
                document.getElementById("<%=btnSave.clientID %>").style.display = 'none';            
                document.getElementById("<%=btnNext.clientID %>").style.display = '';
//                document.getElementById("<%=data_asumsi.clientID %>").style.display = 'none';
//                document.getElementById("<%=data_asumsi_dtl.clientID %>").style.display = 'none';
            }
        }
        $(function() {
            $("#<%=InpTglRating.clientID %>").datepicker(
            {   duration: '',
                yearRange: "-10:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate : new Date
            });

            getValExt(document.getElementById("<%=ddlHaveExtRat.clientID %>").value);
        });

        function getValExt(val){
            if(val == 'Ya'){
                var Agency = document.getElementById("<%=InpRatingAgency.ClientID %>").value;
                var Rating = document.getElementById("<%=InpRating.ClientID %>").value;
                PageMethods.getExternalDesc(Rating,Agency, Succ, Fail);  
            }
        }

        function getval(data) {
            //alert(data.value);
            var Agency = document.getElementById("<%=InpRatingAgency.ClientID %>").value;       
            PageMethods.getExternalDesc(data.value,Agency, Succ, Fail);
        }

        function Succ(result) {
               document.getElementById("<%=txtDesc.ClientID %>").innerHTML = result;
        }

        function Fail(result) {
        }
        
        function test(){
        console.log("text");
        }


    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="True" />
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brwcode" runat="server" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 100%">
        <div id='cssmenu'>
            <ul style="float:left;margin-right:10px;">
                <%  Dim html As String = ""
                    If dataTable.Rows.Count > 0 Then
                        For i As Integer = 0 To dataTable.Rows.Count - 1
                            If dataTable.Rows(i).Item("lmr_parent") = 0 And dataTable.Rows(i).Item("lmr_link") <> "#" Then
                                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                                    html &= "<li id='m" & dataTable.Rows(i).Item("lmr_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                                Else
                                    html &= "<li id='m" & dataTable.Rows(i).Item("lmr_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                                End If
                            ElseIf dataTable.Rows(i).Item("lmr_link") = "#" Then
                                html &= checkChildHD(dataTable.Rows(i).Item("lmr_position"), i)
                                html &= getMenuChild(dataTable.Rows(i).Item("lmr_position"))
                            Else
                                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                                    html &= "<li id='c" & dataTable.Rows(i).Item("lmr_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                                Else
                                    html &= "<li id='c" & dataTable.Rows(i).Item("lmr_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lmr_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lmr_name") & "</span></a></li>"
                                End If
                            End If
                        Next
                    End If
                %>
                <%=html %>
            </ul>
            <div class="bodyContent">
                <div id="divnotif" runat="server" class="notif" style="display: none;" width="100%">
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
                <table id="tblExtInfo" class="tbl" cellspacing="0" cellpadding="3">
                    <tr>
                        <td colspan="3" class="detail" style="border-radius: 5px 5px 0 0;">
                            <b>&nbsp</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" colspan="2">
                            <b>Apakah memiliki rating external?</b>
                        </td>
                        <td width="80%">
                            <asp:DropDownList ID="ddlHaveExtRat" runat="server" CssClass="inpDdl" onchange="fChange();">
                                <asp:ListItem Value="Ya">Ya</asp:ListItem>
                                <asp:ListItem Value="Tidak">Tidak</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="ratAgency" runat="server">
                        <td width="20%" colspan="2">
                            <b>Rating Agency</b>
                        </td>
                        <td width="80%">
                            <asp:DropDownList ID="InpRatingAgency" runat="server" CssClass="inpDdl" AutoPostBack="True">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="corType" runat="server">
                        <td colspan="2">
                            <b>Corporate Type</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="InpCorp" runat="server" CssClass="inpDdl">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="rating" runat="server">
                        <td colspan="2">
                            <b>Rating</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="InpRating" runat="server" onchange="getval(this)" CssClass="inpDdl">
                            </asp:DropDownList>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                            <input type="hidden" id="InpRatingHid" runat="server" />
                        </td>
                    </tr>
                    <tr id="desc" runat="server">
                        <td colspan="2">
                            <b>Rating Description</b>
                        </td>
                        <td id="TxtDesc" style="font-weight: bold">
                        </td>
                    </tr>
                    <tr id="ratingDate" runat="server">
                        <td colspan="2">
                            <b>Tgl. Rating</b>
                        </td>
                        <td>
                            <asp:TextBox ID="InpTglRating" runat="server" CssClass="inpTxt" Style="float: none"></asp:TextBox>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr id="data_asumsi" runat="server" visible="false">
                        <td colspan="3" class="detail">
                            <span style="font-size: 14px; font-weight: bolder">Detail</span>
                        </td>
                    </tr>
                    <tr id="data_asumsi_dtl" runat="server" visible="false">
                        <td colspan="3" width="100%">
                            <table width="98%" id="tblAsumsi_dtl" class="tbl">
                                <tr>
                                    <td width="2%" valign='top'>
                                        &nbsp;
                                    </td>
                                    <td width="1%" valign='top'>
                                        &nbsp;
                                    </td>
                                    <td width="17%">
                                        <b>Rincian</b>
                                    </td>
                                    <td width="80%">
                                        <b>Asumsi</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtId1" runat="server" CssClass="inpTxt" Visible="false">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <customControl:GroupDropDownList ID="ddlRincian1" runat="server" CssClass="inpDdl">
                                        </customControl:GroupDropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAsumsi1" runat="server" CssClass="inpTxt" Style="float: none"></asp:TextBox>
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" ClientIDMode="Predictable">
                                    <ItemTemplate>
                                        <tr>
                                            <td width="2%" valign='top'>
                                                <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="../../images/false.gif" />&nbsp;
                                            </td>
                                            <td  width="1%" >
                                                <asp:TextBox ID="txtId" runat="server" CssClass="inpTxt" Visible="false">0</asp:TextBox>
                                            </td>
                                            <td width="17%">
                                                <customControl:GroupDropDownList ID="ddlRincian" runat="server" CssClass="inpDdl"
                                                    AutoPostBack="false">
                                                </customControl:GroupDropDownList>
                                            </td>
                                            <td width="80%">
                                                <asp:TextBox ID="txtAsumsi" runat="server" CssClass="inpTxt" Style="float: none"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="4" style="padding-left: 0;">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="inpBtn"
                                            Style="float: left;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td background="../../styles/btn_bank/footer.gif" colspan="3" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan " CssClass="inpBtn">
                            </asp:Button>
                            <asp:Button ID="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:Button>
                            <span style="color: Red; font-size: 11pt; font-weight: bold;">* <span style="font-size: 13px;">
                                Harus Diisi</span></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

