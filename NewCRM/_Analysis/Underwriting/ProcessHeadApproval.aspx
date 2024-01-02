<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ProcessHeadApproval.aspx.vb" Inherits="NewCRM.ProcessHeadApproval" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aBc("Process Head Approval");
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
    </script>
    <style type="text/css">
        .txtInput
        {
            height: 16px;
            width: 150px;
            padding: 2px 5px;
            background: #f5f5f5;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 12px;
            position: relative;
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 1px 4px;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 11px;
            float: left;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="True" />
    <div style="margin: auto; width: 98%;">
        <div>
                        <obout:Grid ID="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true"
                AllowPaging="true" PageSize="20" AutoGenerateColumns="false" EnableRecordHover="true"
                 AllowPageSizeSelection="false" AllowFiltering="true" AllowAddingRecords=false
                AllowRecordSelection="false" FolderStyle="~/styles/grid">
                <Columns>
                    <obout:Column ID="Column1" DataField="id" Visible="false" runat="server" />
                    <obout:Column ID="Column10" DataField="brw_code" Visible="false" runat="server" />
                    <obout:Column ID="Column2" DataField="no_app" HeaderText="Nomor Aplikasi" runat="server"
                        ShowFilterCriterias="false" Width="10%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtAppNo" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="AppNoFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column3" DataField="nama_debitur" HeaderText="Nama Debitur" runat="server"
                        ShowFilterCriterias="false" Width="20%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtNamaDebitur" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="NamaDebiturFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column4" DataField="nama_CA" HeaderText="Nama CA" runat="server"
                        ShowFilterCriterias="false" Width="10%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtNamaCA" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="NamaCAFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column5" DataField="nama_RM" HeaderText="Nama RM" runat="server"
                        ShowFilterCriterias="false" Width="10%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtNamaRM" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="NamaRMFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column7" DataField="usr_head" HeaderText="User Head" runat="server"
                        ShowFilterCriterias="false" Width="10%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtNamaUsrHead" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="NamaUsrHeadFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column6" DataField="komentar" HeaderText="Komentar" runat="server"
                        ShowFilterCriterias="false" Width="20%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtKomentar" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="KomentarFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column8" DataField="status" HeaderText="Status" runat="server"
                        ShowFilterCriterias="false" Width="10%" Wrap="true">
                        <TemplateSettings RowEditTemplateControlId="txtStatus" RowEditTemplateControlPropertyName="value"
                            FilterTemplateId="StatusFilter" />
                        <FilterOptions>
                            <obout:FilterOption IsDefault="true" Type="Contains" />
                        </FilterOptions>
                    </obout:Column>
                    <obout:Column ID="Column9" HeaderText="Action" runat="server" Width="10%">
                        <TemplateSettings TemplateId="viewBtnTemplate" />
                    </obout:Column>
                </Columns>
                <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                <Templates>
                    <obout:GridTemplate runat="server" ID="viewBtnTemplate">
                        <Template>
                       
                        

                            <%# IIf(Container.DataItem("usr_head") = usrLogin, "<a class='ob_gALF' href='javascript:fProcess(" & Container.DataItem("id") & ")'>Process</a>&nbsp;<span class='ob_gALF'>| </span>", "")%> 
                            
                            
                            
                            <%--<%  If allowDetail = True Then%>--%>
                            
                            <a class="ob_gALF" href="javascript:detail('<%# Container.DataItem("no_app") %>','<%# Container.DataItem("brw_code") %>');">
                                Detail</a>&nbsp;&nbsp; <a class="ob_gALF" href="javascript:fSetting('<%# Container.DataItem("no_app") %>');">                                   
                            <%--<%End If%>--%>
                            <%--<a class="ob_gALF" href="javascript:fMonitoring('<%# Container.DataItem("no_app") %>');">
                                Monitoring</a>--%>
                            <%--<%  If allowLogs = True Then%>
                        <a class="ob_gALF" href="javascript:fLogs('<%# Container.DataItem("app_no") %>');">Log</a>&nbsp;&nbsp;
                        <%End If%>--%>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="AppNoFilter" ControlID="AppNo">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="AppNo" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="NamaDebiturFilter" ControlID="NmDebitur">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="NmDebitur" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="NamaCAFilter" ControlID="NmCA">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="NmCA" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="NamaRMFilter" ControlID="NMRM">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="NMRM" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="NamaUsrHeadFilter" ControlID="NmUsrHead">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="NmUsrHead" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="KomentarFilter" ControlID="Komentar">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="Komentar" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="StatusFilter" ControlID="Status">
                        <Template>
                            <obout:OboutTextBox runat="server" ID="Status" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                            </obout:OboutTextBox>
                        </Template>
                    </obout:GridTemplate>
                    <obout:GridTemplate runat="server" ID="tplRemoveFilter">
                        <Template>
                            <a onclick="grid1.removeFilter()" class="ob_gALF" style="cursor: pointer">Clear Filter</a>
                        </Template>
                    </obout:GridTemplate>
                </Templates>
                <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter" />
            </obout:Grid>
        </div>
    </div>

    <script type="text/javascript">
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

        function OnBeforeDelete(record) {
            if (confirm("Apakah Anda yakin ingin menghapus?") == false) {
                return false;
            }
            return true;
        }
        function OnDelete(record) {
            alert("Data berhasil dihapus.");
        }
        function OnBeforeAdd(record) {
            fAdd("-1", "-1", "");
            return false;
        }
        function fAdd(id, brwcode) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "UnderwritingDet.aspx?appno=" + id + "&brw_code=" + brwcode;
            oWindow(l_obj_argument.url, 740, 350);
        }

        function detail(id, brwcode, workflow) {
            PageMethods.checkAppModel(id, OnSuccessApps, OnFailure);
            //            if (workflow == "") {
            //                var l_obj_argument = new Object();
            //                l_obj_argument.url = "UnderwritingDet.aspx?appno=" + id + "&brw_code=" + brwcode;
            //                oWindow(l_obj_argument.url, 740, 350);
            //            }
            //            else
            //                window.location.href = "General.aspx?appno=" + id;
        }

        function OnSuccessApps(response) {
            var splits = response.split(",");
            if (splits[1] == "" || splits[2] == "") {
                alert("Mohon untuk melengkapi model rating terlebih dahulu.");
            }
            else {
                window.location.href = "General.aspx?appno=" + splits[0];
            }
        }
        function fSetting(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "underwritingSet.aspx?appno=" + id;
            oWindow(l_obj_argument.url, 440, 250);
        }

        function fMonitoring(id) {
            
            PageMethods.encrpytAppNo(id, OnSuccess, OnFailure);
        }
        function OnSuccess(response) {
            window.location.href = "underwritingMon.aspx?appno=" + response;
        }
        function OnFailure(response) {
        }

        function fProcess(id) {
            
            fProc(id);
            return false;
        }

        function fProc(id) {
            
            var l_obj_argument = new Object();
            l_obj_argument.url = "ProcessHeadApprovalDet.aspx?id=" + id;
            oWindowscroll(l_obj_argument.url, 740, 350);
        }
    </script>
</asp:Content>
