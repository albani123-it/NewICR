<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="R_DataAgunan.aspx.vb" Inherits="NewCRM.R_DataAgunan" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                window.location.href = url +"?appno=" + appno + "&typ=Facility";
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
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="status" value="1" runat="server" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 100%">
        <div id='cssmenu'>
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
                <table class="tbl" cellspacing="0" cellpadding="2" width="100%" style="text-align: left;
                    border: thin solid #bcbcbc; border-radius: 5px;">
                    <tr>
                        <td background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td style="padding: 0;">
                            <obout:Grid ID="GridAgunan" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowFiltering="true"
                                AllowRecordSelection="false" FolderStyle="~/styles/grid">
                                <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit"
                                    OnBeforeClientAdd="OnBeforeAdd" />
                                <Columns>
                                    <obout:Column ID="Column1" runat="server" DataField="lda_id" Visible="false" />
                                    <obout:Column ID="Column3" runat="server" DataField="appno" Visible="false" />
                                    <obout:Column ID="Column9" runat="server" DataField="type_agunan" HeaderText="Type Agunan"
                                        ShowFilterCriterias="false" Width="15%" Wrap="true">
                                        <TemplateSettings RowEditTemplateControlId="txtTypeAgunan" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="TypeAgunanFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column ID="Column2" runat="server" DataField="jenis_agunan" HeaderText="Jenis Agunan"
                                        ShowFilterCriterias="false" Width="15%" Wrap="true">
                                        <TemplateSettings RowEditTemplateControlId="txtJenisAgunan" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="JenisAgunanFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column ID="Column4" runat="server" DataField="lokasi_agunan" HeaderText="Lokasi Agunan"
                                        ShowFilterCriterias="false" Width="15%" Wrap="true">
                                        <TemplateSettings RowEditTemplateControlId="txtLokasiAgunan" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="LokasiAgunanFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column ID="Column5" runat="server" DataField="kelas_agunan" HeaderText="Kelas Agunan"
                                        ShowFilterCriterias="false" Width="15%" Wrap="true">
                                        <TemplateSettings RowEditTemplateControlId="txtKelasAgunan" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="KelasAgunanFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column ID="Column6" runat="server" DataField="flag_agunan" HeaderText="Flag"
                                        ShowFilterCriterias="false" Width="5%" Wrap="true">
                                        <TemplateSettings RowEditTemplateControlId="txtFlagAgunan" RowEditTemplateControlPropertyName="value"
                                            FilterTemplateId="FlagAgunanFilter" />
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
                                <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                                <Templates>
                                    <obout:GridTemplate runat="server" ID="deleteTemplate">
                                        <Template>
                                            <%If allowDelete = True Then%>
                                            <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Apakah Anda yakin ingin menghapus?');"
                                                CommandArgument='<%# Container.PageRecordIndex %>' class='ob_gAL'>  Delete  </asp:LinkButton>
                                            <%End If%>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="JenisAgunanFilter" ControlID="Agunan">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Agunan" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="txtLokasiAgunan" ControlID="Lokasi">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Lokasi" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="KelasAgunanFilter" ControlID="Status">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Status" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="FlagAgunanFilter" ControlID="Kelas">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Kelas" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                                <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter" />
                            </obout:Grid>
                        </td>
                    </tr>
                    <tr style="height: 30px">
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
            fEdit(record.lda_id, 'EDIT');
            return false;
        }

        function OnBeforeAdd(record) {
            fEdit(-1, 'NEW');
            return false;
        }

        function fEdit(id, code) {
            var appno = document.getElementById("<%= appno.clientID %>").value;
            var stat = document.getElementById("<%= status.clientID %>").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "R_DataAgunanDet.aspx?id=" + id + "&appno=" + appno + "&status=" + stat + "&code=" + code;
            oWindowscroll(l_obj_argument.url, 1000, 590);
        }

        /* -- */
    </script>
</asp:Content>
