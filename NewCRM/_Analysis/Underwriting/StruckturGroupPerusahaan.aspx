<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="StruckturGroupPerusahaan.aspx.vb" Inherits="NewCRM.StruckturGroupPerusahaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
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
        
        #tblSubgroup_dtl
        {
            width: 100%;
            border-collapse: collapse;
        }
        #tblSubgroup_dtl tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblSubgroup_dtl tr:nth-child(odd)
        {
            background: #ebecec;
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
    </style>
    <script type="text/javascript">
        aMenu("Analysis");
        aBc("TacticalAccountPlanning");

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

    </script>
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
                            <td width="20%"><span><b>No. Aplikasi</b></span></td>
                            <td><asp:label ID="lblAppNo_header" runat="server"></asp:label></td>
                        </tr>
                        <tr>
                            <td><span><b>Nama Pemohon</b></span></td>
                            <td><asp:label ID="lblBrwName_header" runat="server"></asp:label></td>
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
                 <table id="tblFillData" class="table table-striped table-bordered" cellspacing="0" cellpadding="3">
                    <tr>
                        <td colspan="4" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>
                     <tr>
                        <td colspan="4" class="detail">
                            <span style="font-size: 14px; font-weight: bolder">Struktur Group Perusahaan</span>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <b>Group ID</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgroupID" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Kode Group</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGroupCode" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Nama Group</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                            <%--<span style="color: Red; font-size: 11pt; font-weight: bold;">&nbsp;*</span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Keterangan</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGroupDesc" runat="server" CssClass="form-control" TextMode="MultiLine"
                                Rows="3" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <b>Foto</b>
                        </td>
                        <td colspan="4">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false">
                                <Columns>
                                    <asp:ImageField DataImageUrlField="Value" ControlStyle-Height="400" ControlStyle-Width="400" />
                                </Columns>
                            </asp:GridView>
                            <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp; <br />
                            <asp:Button ID="btnUpload" Text="Upload" runat="server" CssClass="btn btn-default" />
                            <br />
                            <span>Ukuran File maksimum 200 KB.</span><br />
                            <input type="hidden" id="textFile" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="detail">
                            <span style="font-size: 14px; font-weight: bolder">Detail</span>
                        </td>
                    </tr>
                    <tr id="subgroup_dtl" runat="server">
                        <td colspan="3" width="100%">
                            <table width="98%" id="tblSubgroup_dtl" class="tbl">
                                <tr>
                                    <td width="2%">
                                        &nbsp;
                                    </td>
                                    <td width="1%">
                                        &nbsp;
                                    </td>
                                    <td width="17%">
                                        <b>Kode Sub Group</b>
                                    </td>
                                    <td width="20%">
                                        <b>Nama Sub Group</b>
                                    </td>
                                    <td width="60%">
                                        <b>Keterangan</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubGroupId1" runat="server" CssClass="form-control" Style="display: none">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubGroupCode1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubGroupName1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubgroupDesc1" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Rows="3" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" ClientIDMode="Predictable">
                                    <ItemTemplate>
                                        <tr>
                                            <td width="2%" valign='top'>
                                                <div>
                                                    <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="../../images/false.gif" />&nbsp;
                                                </div>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubGroupId" runat="server" CssClass="form-control" Style="display: none">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubGroupCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubgroupDesc" runat="server" CssClass="form-control" TextMode="MultiLine"
                                            Rows="3" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="5" style="padding-left: 0;">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="btn btn-info"
                                            Style="float: left;" />
                                    </td>
                                </tr>
                                <tr colspan="5" style="height: 30px">
                                    <td background="../../styles/btn_bank/footer.gif" colspan="5" style="border-radius: 0 0 5px 5px;">
                                        <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn btn-primary"></asp:Button>&nbsp;
                                        <input type="button" value="Lanjut" onclick="fDetail('<%= menuNext %>');" class="btn btn-danger" />
                                        &nbsp; <span style="color: Red; font-size: 11pt;
                                                font-weight: bold;">* <span style="font-size: 13px;">Harus Diisi</span></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
