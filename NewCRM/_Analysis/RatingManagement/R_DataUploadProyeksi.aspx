<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="R_DataUploadProyeksi.aspx.vb" Inherits="NewCRM.R_DataUploadProyeksi" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
    function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
    </script>
    <style type="text/css">
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
        .tdLink
        {
            color: #333333;
            font: 10px Century Gothic;
        }
        .tdLink:hover
        {
            text-decoration: underline;
        }
        
        .ob_gCS, .ob_gCS div, .ob_gCS_F, .ob_gCS_F div
        {
            display: none;
        }
        
        #MainContent_ContentPlaceHolderInSub_MainContent_grid1_ob_grid1FooterContainer_ob_grid1AddButton
        {
            color: blue;
            font-weight: bold;
        }
        
        .no-sort .ob_gHSI
        {
            display: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <input type="hidden" id="status" runat="server" value="1" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 100%">
        <div id='cssmenu' style="margin-bottom: 20px;">
            <ul style="float: left; margin-right: 10px;">
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
                <div style="clear: both; margin: 0 auto; padding: 0; width: 98%">
                    <obout:Grid ID="grdDataUpload" runat="server" CallbackMode="true" Serialize="true"
                        PageSize="20" AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                        AllowRecordSelection="false" AllowColumnResizing="true" AllowPageSizeSelection="false"
                        FolderStyle="~/styles/grid" AllowSorting="true" AllowFiltering="false">
                        <ClientSideEvents OnBeforeClientAdd="OnBeforeAdd" />
                        <Columns>
                            <obout:Column ID="column1" DataField="dtu_file_location" HeaderText="Excel Name"
                                Wrap="true" Width="15%">
                            </obout:Column>
                            <obout:Column ID="column2" DataField="dtu_sheet_name" HeaderText="Sheet Name" Wrap="true"
                                Width="15%">
                            </obout:Column>
                            <obout:Column ID="column3" DataField="dtu_row_count" HeaderText="Row Count" Wrap="true"
                                Width="15%">
                            </obout:Column>
                            <obout:Column ID="column4" DataField="dtu_upload_date" HeaderText="Upload Date" Wrap="true"
                                Width="15%">
                            </obout:Column>
                            <obout:Column ID="column5" DataField="dtu_upload_usr" HeaderText="Upload By User"
                                Wrap="true" Width="15%">
                            </obout:Column>
                            <%--<obout:Column id="column6" HeaderText="Action" Wrap="true"></obout:Column>--%>
                        </Columns>
                    </obout:Grid>
                </div>
                <center>
                    <table id="tblNote" class="tbl" cellspacing="0" cellpadding="3" width="98%">
                        <tr style="height: 30px">
                            <td background="../../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                                <input type="button" id='btnNext' value="Lanjut" onclick="fDetail('<%= menuNext %>');"
                                    class="inpBtn" />
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <script type="text/javascript">

                function fDetail(url) {
                    var brwcode = document.getElementById("<%=brw_code.clientID %>").value;
                    if (brwcode == "")
                        window.location.href = url;
                    else
                        window.location.href = url + "?brwcode=" + brwcode + "&typ=Borrower";
                }
                function OnBeforeAdd() {
                    getModalForm();
                    return false;
                }

                function getModalForm() {
                    var appno = document.getElementById("<%=appno.clientID %>").value;
                    var l_obj_argument = new Object();
                    l_obj_argument.url = "R_DataUploadProyeksiDet.aspx?appno=" + appno;
                    oWindow(l_obj_argument.url, 500, 185);
                }
            </script>
</asp:Content>
