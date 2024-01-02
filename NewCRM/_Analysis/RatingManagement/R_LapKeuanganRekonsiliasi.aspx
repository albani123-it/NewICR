<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="R_LapKeuanganRekonsiliasi.aspx.vb" Inherits="NewCRM.R_LapKeuanganRekonsiliasi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #tblNeraca
        {
            font-weight:bold;
            border-collapse:collapse;
        }
        #tblNeraca tr:nth-child(event)
        {
            background-color: #f6f7f7;
        }
        #tblNeraca tr:nth-child(odd)
        {
            background-color: #ebecec;
        }
        .notif
        {
            margin-bottom:10px;
            border: 1px solid #bcbcbc; 
            border-radius:5px;
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
        a.notif-close {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color:#fff;
            cursor:pointer;
            font-weight:bold;
            width: 1%;
        }
        .aactive
        {
            background-color:transparent;

            border-radius:11px;
            -moz-border-radius:11px;
            -webkit-border-radius:11px;
            color: #fff;
	
	        background-color:#ebebeb;
	        border:thin solid #cccccc;	
        }
        td
        {
            padding-left:5px;
        }
        .tbl
        {
            font-family:"Century Gothic", "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            font-size:11px;
        }
        .spanChild
        {
            margin-left:10px;
        }
        #ui-datepicker-div {display: none;}
    </style>
    <script type="text/javascript">
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
        function fDet(){
            var brw = document.getElementById("<%= brwcode.clientID %>").value;
            var app = document.getElementById("<%= appno.clientID %>").value;
            var str = document.getElementById("<%= sektor.clientID %>").value;
            var rpt = document.getElementById("<%= reporttype.clientID %>").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "R_LapKeuanganDet.aspx?brwcode=" + brw+"&appno=" + app+ "&sektor=" + str + "&report=" + rpt;
            oWindowscroll(l_obj_argument.url, 1000, 600);
            return false;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brwcode" runat="server" />
    <input type="hidden" id="sektor" runat="server" />
    <input type="hidden" id="reporttype" runat="server" />
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
            <div>
                <div class="bodyContent">
                    <% =htmlForm %>
                    <table id="tblNote" class="tbl" cellspacing="0" cellpadding="3" width="100%">
                        <tr style="height: 30px">
                            <td background="../../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                               <%-- <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="inpBtn" OnClientClick="return fDet();">
                                </asp:Button>--%>
                                <input type="button" id='btnNext' value="Lanjut" onclick="fDetail('<%= menuNext %>');" class="inpBtn" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

