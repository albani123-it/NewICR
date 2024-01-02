<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="AsumsiRating.aspx.vb" Inherits="NewCRM.AsumsiRating" %>

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
        
        #tblAsumsiRating
        {
            width: 100%;
        }
        
        #btnAddNew
        {
            display: block;
        }
        
        #tblAsumsiInput
        {
            width: 100%;
            display: none;
            border-collapse: collapse;
        }
        
        #tblAsumsiInput tr:nth-child(even)
        {
            background: #f6f7f7;
        }
        #tblAsumsiInput tr:nth-child(odd)
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
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
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
              $('#tblAsumsiInput').css('display','block');
              $('#btnAddNew').css('display','none');

              $('#<%=txtIdDtl.clientID %>').val(record.lpard_id);
              $('#<%=txtAsumsi.clientID %>').val(record.lpard_asumsi);
              $('#<%=ddlRincian1.clientID %>').val(record.lpard_rincian);

            return false;
        }
        function clearData(){
            $('#<%=txtIdDtl.clientID %>').val('0');
            $('#<%=txtAsumsi.clientID %>').val('')
        }

       
        $(document).ready(function(){
            $('#<%=btnAddNew.clientID %>').click(function(){
                $('#tblAsumsiInput').css('display','block');
                $('#btnAddNew').css('display','none');
                clearData();
            });

            $('#btnCanceled').click(function(){
                clearData();
                $('#tblAsumsiInput').css('display','none');
                $('#<%=btnAddNew.clientID %>').css('display','block');
            });

        });

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="True" />
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brwcode" runat="server" />
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
                <div id="divData">
                    <table id="tblAsumsiRating" class="tbl" cellspacing="0" cellpadding="3">
                        <tr>
                            <td colspan="2" class="detail" style="border-radius: 5px 5px 0 0;">
                                <span style="font-size: 14px; font-weight: bolder">Perhitungan Deskripsi Rating</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="display: none">
                                <asp:TextBox ID="txtIdRating" runat="server" CssClass="inpTxt">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <b>Periode Rating</b>
                            </td>
                            <td width="80%">
                                <asp:DropDownList ID="ddlPeriodeRating" runat="server" CssClass="inpDdl" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <b>Customer Rating</b>
                            </td>
                            <td width="70%">
                                <asp:TextBox ID="lblCustRating" runat="server" CssClass="inpTxt" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Fasility Rating</b>
                            </td>
                            <td>
                                <asp:TextBox ID="lblFacRating" runat="server" CssClass="inpTxt" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="rowAsumsi" runat="server" visible="false">
                            <td colspan="3" class="detail">
                                <span style="font-size: 14px; font-weight: bolder">Detail Deskripsi Rating</span>
                            </td>
                        </tr>
                        <tr id="dtlAsumsi" runat="server" visible="false">
                            <td colspan="3" style="padding:0;">
                                <table id="tblAsumsiDtl" width="100%" class="tbl" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td style="padding:0;">
                                            <obout:Grid ID="grid1" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowAddingRecords="false"
                                                AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                                <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" />
                                                <Columns>
                                                    <obout:Column ID="Column1" runat="server" DataField="lpard_id" Visible="false" />
                                                    <obout:Column ID="Column2" runat="server" DataField="lpard_rincian" Visible="false" />
                                                    <obout:Column ID="Column5" runat="server" DataField="rinciangroup" HeaderText="Group Rincian"
                                                        ShowFilterCriterias="false" Width="15%">
                                                        <TemplateSettings RowEditTemplateControlId="txtGroupRincian" RowEditTemplateControlPropertyName="value"
                                                            FilterTemplateId="GroupRincianFilter" />
                                                        <FilterOptions>
                                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                                        </FilterOptions>
                                                    </obout:Column>
                                                    <obout:Column ID="Column3" runat="server" DataField="rincian" HeaderText="Rincian"
                                                        ShowFilterCriterias="false" Width="15%">
                                                        <TemplateSettings RowEditTemplateControlId="txtRincian" RowEditTemplateControlPropertyName="value"
                                                            FilterTemplateId="RincianFilter" />
                                                        <FilterOptions>
                                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                                        </FilterOptions>
                                                    </obout:Column>
                                                    <obout:Column ID="Column4" runat="server" DataField="lpard_asumsi" HeaderText="Deskripsi"
                                                        ShowFilterCriterias="false" Width="15%">
                                                        <TemplateSettings RowEditTemplateControlId="txtAsumsi" RowEditTemplateControlPropertyName="value"
                                                            FilterTemplateId="AsumsiFilter" />
                                                        <FilterOptions>
                                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                                        </FilterOptions>
                                                    </obout:Column>
                                                    <obout:Column ID="Column7" runat="server" HeaderText="Action" Width="5%" AllowEdit="true">
                                                    </obout:Column>
                                                    <obout:Column ID="Column8" runat="server" Width="5%">
                                                        <TemplateSettings TemplateId="deleteTemplate" />
                                                    </obout:Column>
                                                </Columns>
                                                <Templates>
                                                    <obout:GridTemplate runat="server" ID="deleteTemplate">
                                                        <Template>
                                                            <asp:LinkButton ID="delDtl" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain you want to delete?');"
                                                                CommandArgument='<%# Container.PageRecordIndex %>' class='ob_gAL'>  Delete  </asp:LinkButton>
                                                        </Template>
                                                    </obout:GridTemplate>
                                                </Templates>
                                            </obout:Grid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="button" runat="server" id="btnAddNew" value="Tambah Baru" class="inpBtn" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:0;">
                                            <table width="100%" id="tblAsumsiInput">
                                                <tr>
                                                    <td colspan="2" style="display: none">
                                                        <asp:TextBox ID="txtIdDtl" runat="server" CssClass="inpTxt">0</asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        <b>Rincian</b>
                                                    </td>
                                                    <td width="70%">
                                                        <b>Deskripsi</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <customControl:GroupDropDownList ID="ddlRincian1" runat="server" CssClass="inpDdl">
                                                        </customControl:GroupDropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAsumsi" runat="server" CssClass="inpTxt" Style="float: none;
                                                            width: 400px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnSaved" runat="server" Text="Simpan" CssClass="inpBtn" />
                                                        &nbsp;
                                                        <input type="button" id="btnCanceled" value="Batal" class="inpBtn" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td background="../../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;">
                                <asp:Button ID="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
