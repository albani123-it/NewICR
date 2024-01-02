<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LKN.aspx.vb" Inherits="NewCRM.LKN" %>

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
                window.location.href = url +"?appno=" + appno;
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
    <input type="hidden" id="status" value="1" runat="server" />
    <div style="clear:both; margin:0 auto; padding:0; width:100%">
        <div id='cssmenu'>
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
                                    <obout:Column id="Column3" runat="server" DataField="lsl_app_no" Visible="false" />
                                    <obout:Column id="Column2" runat="server" DataField="lsl_tanggal" HeaderText="Tanggal" ShowFilterCriterias="false" Width="12%">
                                        <TemplateSettings RowEditTemplateControlId="TxtTanggal" RowEditTemplateControlPropertyName="value" FilterTemplateId="TanggalFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column4" runat="server" DataField="lsl_nama" HeaderText="Nama Nasabah" ShowFilterCriterias="false" Width="15%">
                                        <TemplateSettings RowEditTemplateControlId="TxtNama" RowEditTemplateControlPropertyName="value" FilterTemplateId="NamaFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column5" runat="server" DataField="lsl_bentuk" HeaderText="Bentuk Kontak" ShowFilterCriterias="false" Width="15%">
                                        <TemplateSettings RowEditTemplateControlId="TxtBentuk" RowEditTemplateControlPropertyName="value" FilterTemplateId="BentukFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column6" runat="server" DataField="lsl_tujuan" HeaderText="Tujuan Kontak" ShowFilterCriterias="false" Width="15%">
                                        <TemplateSettings RowEditTemplateControlId="TxtTujuan" RowEditTemplateControlPropertyName="value" FilterTemplateId="TujuanFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column9" runat="server" DataField="lsl_hasil" HeaderText="Hasil Kontak" ShowFilterCriterias="false" Width="25%">
                                        <TemplateSettings RowEditTemplateControlId="TxtHasil" RowEditTemplateControlPropertyName="value" FilterTemplateId="HasilFilter" />
                                        <FilterOptions>
                                            <obout:FilterOption IsDefault="true" Type="Contains" />
                                        </FilterOptions>
                                    </obout:Column>
                                    <obout:Column id="Column7" runat="server" HeaderText="Action" Width="8%" AllowEdit="true"></obout:Column>
                                    <obout:Column id="Column8" runat="server" Width="10%">
                                        <TemplateSettings TemplateID = "deleteTemplate" />
                                    </obout:Column>
                                </Columns>
                                <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                                <Templates>
                                     <obout:GridTemplate runat="server" ID="deleteTemplate" >
                                        <Template>
                                            <%If allowDelete = True Then%>
                                                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Apakah Anda yakin ingin menghapus?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                                            <%End If%>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="TanggalFilter" ControlID="Tanggal">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Tanggal" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="NamaFilter" ControlID="Nama">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Nama" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="BentukFilter" ControlID="Bentuk">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Bentuk" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="TujuanFilter" ControlID="Tujuan">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Tujuan" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="HasilFilter" ControlID="Hasil">
                                        <Template>
                                            <obout:OboutTextBox runat="server" ID="Hasil" Width="100%">
                                                <ClientSideEvents OnKeyUp="applyFilter" />
                                            </obout:OboutTextBox>
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
            var brw = document.getElementById("<%= appno.clientID %>").value;
            var stat = document.getElementById("<%= status.clientID %>").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "LKNDet.aspx?id=" + id + "&appno=" + brw + "&status=" + stat;
            oWindowscroll(l_obj_argument.url, 820, 500);
        }
        /* -- */
    </script>
</asp:Content>
