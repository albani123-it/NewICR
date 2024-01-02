<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Legalitas.aspx.vb" Inherits="NewCRM.Legalitas" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        aBc("General");
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
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
        </script>
    <style type="text/css">
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
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <input type="hidden" id="status" runat="server" value="1" />
    <div style="clear:both; margin:0 auto; padding:0; width:100%">
        <div id='cssmenu' style="margin-bottom:20px;">
            <ul style="float:left;margin-right:10px;">
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
                <table class="tbl" cellspacing="0" cellpadding="2" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;">
                    <tr>
                        <td background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td style="padding:0;">
                <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" 
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
                    <Columns>
                        <obout:Column id="Column1" runat="server" DataField="lsl_id" Visible="false" />
                        <obout:Column id="Column2" runat="server" DataField="lblJenisIjin" HeaderText="Jenis Ijin" ShowFilterCriterias="false" Width="15%">
                            <TemplateSettings RowEditTemplateControlId="txtJenisIjin" RowEditTemplateControlPropertyName="value" FilterTemplateId="JenisIjinFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column id="Column3" runat="server" DataField="lsl_noLegalitas" HeaderText="No. Legalitas Usaha" ShowFilterCriterias="false" Width="15%">
                            <TemplateSettings RowEditTemplateControlId="txtNoLegal" RowEditTemplateControlPropertyName="value" FilterTemplateId="NoLegalFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column id="Column4" runat="server" DataField="lsl_tglLegalitas" HeaderText="Tgl. Legalitas Usaha" ShowFilterCriterias="false" Width="13%">
                            <TemplateSettings RowEditTemplateControlId="txtTglLegalitas" RowEditTemplateControlPropertyName="value" FilterTemplateId="TglLegalitasFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column id="Column5" runat="server" DataField="lsl_tglJatuhTempo" HeaderText="Tgl. Jatuh Tempo" ShowFilterCriterias="false" Width="12%">
                            <TemplateSettings RowEditTemplateControlId="txtTglJatuhTempo" RowEditTemplateControlPropertyName="value" FilterTemplateId="TglJatuhTempoFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column id="Column6" runat="server" DataField="lsl_keterangan" HeaderText="Keterangan" ShowFilterCriterias="false" Width="25%">
                            <TemplateSettings RowEditTemplateControlId="txtKeterangan" RowEditTemplateControlPropertyName="value" FilterTemplateId="KeteranganFilter" />
                            <FilterOptions>
                                <obout:FilterOption IsDefault="true" Type="Contains" />
                            </FilterOptions>
                        </obout:Column>
                        <obout:Column id="Column7" runat="server" HeaderText="Action" Width="8%" AllowEdit="true"></obout:Column>
                        <obout:Column id="Column8" runat="server" Width="12%">
                            <TemplateSettings TemplateID = "deleteTemplate" />
                        </obout:Column>
                    </Columns>
                    <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                    <Templates>
                         <obout:GridTemplate runat="server" ID="deleteTemplate" >
                            <Template>
                                <% If allowDelete = True Then%>
                                <asp:LinkButton ID="zzz" OnCommand="delete" runat="server" OnClientClick="return confirm('Apakah Anda yakin ingin menghapus?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                                <%End If%>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="JenisIjinFilter" ControlID="JenisIjin">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="JenisIjin" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="NoLegalFilter" ControlID="NoLegalitas">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="NoLegalitas" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="TglLegalitasFilter" ControlID="TglLegalitas">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="TglLegalitas" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="TglJatuhTempoFilter" ControlID="TglJatuhTempo">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="TglJatuhTempo" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="KeteranganFilter" ControlID="Keterangan">
                            <Template>
                                <obout:OboutTextBox runat="server" ID="Keterangan" Width="100%">
                                    <ClientSideEvents OnKeyUp="applyFilter" />
                                </obout:OboutTextBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="tplRemoveFilter">
                            <Template>
                                <a onclick="grid1.removeFilter()" class="ob_gALF" style="cursor:pointer">Clear Filter</a>
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                    <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter"/>
                </obout:Grid>
                        </td>
                    </tr>
                    <tr style="height:30px">
				        <td background="../../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                            <input type="button" value="Lanjut" onclick="fDetail('<%= menuNext %>');" class="inpBtn" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        /* -- */
        var applyFilterTimeout = null;

        function applyFilter() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeout);
            }
            applyFilterTimeout = window.setTimeout(doFiltering, 500);
        }

        function doFiltering() {
            grid1.filter();
        }
        /* -- */

        /* -- */
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
            fEdit(record.lsl_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            var brw = document.getElementById("<%= brw_code.clientID %>").value;
            var stat = document.getElementById("<%= status.clientID %>").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "LegalitasDet.aspx?id=" + id + "&brwcode=" + brw + "&status=" + stat ;
            oWindowscroll(l_obj_argument.url, 740, 350);
        }
        /* -- */
    </script>
</asp:Content>