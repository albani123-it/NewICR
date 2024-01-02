<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="R_CorporateShareholders.aspx.vb" Inherits="NewCRM.R_CorporateShareholders" %>

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
            var brwcode = document.getElementById("<%=brw_code.clientID %>").value;
            if (brwcode == "")
                window.location.href = url;
            else
                window.location.href = url +"?brwcode=" + brwcode + "&typ=Borrower";
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
        #ui-datepicker-div {display: none;}
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <input type="hidden" id="sisa" value="0" runat="server" />
    <input type="hidden" id="status" value="1" runat="server" />
    <div style="clear:both; margin:0 auto; padding:0; width:100%">
        <div id='cssmenu' style="margin-bottom:20px;">
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
                <div id="divnotif" runat="server" class="notif" style="display:none;">
                    <div class="notif-header" width="100%">
                        <table width="100%">
                            <tr>
                                <td><span><b>Notifikasi</b></span></td>
                                <td align="right"><a class="notif-close" onclick="document.getElementById('<%=divnotif.clientID %>').style.display='none'" title="Close">X</a></td>
                            </tr>
                        </table>
                    </div>
                    <div class="notif-body">
                        <asp:Label ID="lblNotif" runat="server" style="color:Red;"></asp:Label>
                    </div>
                </div>
                <table class="tbl" cellspacing="0" cellpadding="2" width="100%" style="font-weight:bold;text-align:right;border: thin solid #bcbcbc;border-radius: 5px;">
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
                                    <obout:Column id="Column1" runat="server" DataField="lcs_id" Visible="false" />
                                    <obout:Column id="Column2" runat="server" DataField="lcs_nama" HeaderText="Nama" ShowFilterCriterias="false" Width="13%">
                                        <TemplateSettings RowEditTemplateControlId="txtNama" RowEditTemplateControlPropertyName="value" FilterTemplateId="NamaFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column3" runat="server" DataField="lcs_noAkteKTP" HeaderText="No. Akte Pendirian" ShowFilterCriterias="false" Width="13%">
                                        <TemplateSettings RowEditTemplateControlId="txtNoAkte" RowEditTemplateControlPropertyName="value" FilterTemplateId="NoAkteFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column4" runat="server" DataField="lcs_tglAkteKTP" HeaderText="Tgl. Akte Pendirian" ShowFilterCriterias="false" Width="13%">
                                        <TemplateSettings RowEditTemplateControlId="txtTglAkte" RowEditTemplateControlPropertyName="value" FilterTemplateId="TglAkteFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column5" runat="server" DataField="lcs_npwp" HeaderText="NPWP" ShowFilterCriterias="false" Width="13%">
                                        <TemplateSettings RowEditTemplateControlId="txtNPWP" RowEditTemplateControlPropertyName="value" FilterTemplateId="NPWPFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column6" runat="server" DataField="lcs_kepemilikanSaham" HeaderText="Kepemilikan Saham (%)" AllowFilter="false" Width="15%"></obout:Column>
                                    <obout:Column id="Column7" runat="server" DataField="lcs_jumlahLembarSaham" HeaderText="Jumlah Lembar Saham" AllowFilter="false" Width="14%" DataFormatString="{0:N0}"></obout:Column>
                                    <obout:Column id="Column8" runat="server" HeaderText="Action" Width="5%" AllowEdit="true"></obout:Column>
                                    <obout:Column id="Column9" runat="server" Width="10%">
                                        <TemplateSettings TemplateID = "deleteTemplate" />
                                    </obout:Column>
                                </Columns>
                                <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                                <Templates>
                                     <obout:GridTemplate runat="server" ID="deleteTemplate" >
                                        <Template>
                                            <%If allowDelete = True Then%>
                                                <asp:LinkButton ID="zzz" OnCommand="delete" runat="server" OnClientClick="return confirm('Apakah Anda yakin ingin menghapus?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                                            <%End If%>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="NamaFilter" ControlID="Nama">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Nama" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="NoAkteFilter" ControlID="NoAkte">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="NoAkte" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="TglAkteFilter" ControlID="TglAkte">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="TglAkte" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="NPWPFilter" ControlID="NPWP">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="NPWP" Width="100%">
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
				        <td align="left" background="../../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                            <input type="button" value="Lanjut" onclick="fDetail('<%= menuNext %>');" class="inpBtn" />
                        </td>
                    </tr>
                <//table>
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
            fEdit(record.lcs_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1);
            return false;
        }

        function fEdit(id) {
            var brw = document.getElementById("<%= brw_code.clientID %>").value;
            var sisa = 100 - document.getElementById("<%= sisa.clientID %>").value;
            var stat = document.getElementById("<%= status.clientID %>").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "R_CorporateShareholdersDet.aspx?id=" + id + "&brwcode=" + brw + "&sisa=" + sisa + "&status=" + stat;
            oWindowscroll(l_obj_argument.url, 840, 350);
        }
        /* -- */
    </script>
</asp:Content>
