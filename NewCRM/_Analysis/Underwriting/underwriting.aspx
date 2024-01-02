<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="underwriting.aspx.vb" Inherits="NewCRM.underwriting" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aBc("underwriting");
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
            position:relative;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
    <div style="margin:auto;width:98%;">
        <div>
        <obout:Grid id="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="20"
			    AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="true"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column ID="Column1" DataField="app_id" Visible="false" runat="server"/>
                <obout:Column ID="Column9" DataField="app_workflow" Visible="false" runat="server"/>
                <obout:Column ID="Column10" DataField="app_position" Visible="false" runat="server"/>
                <obout:Column ID="Column2" DataField="app_brw_code" HeaderText="Kode Borrower" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtBrwCode" RowEditTemplateControlPropertyName="value" FilterTemplateId="BrwCodeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column3" DataField="app_no" HeaderText="Nomor Aplikasi" runat="server" ShowFilterCriterias="false" Width="8%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtAppNo" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppNoFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column11" DataField="lsb_namaPemohon" HeaderText="Nama Pemohon" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtNamaPemohon" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppNamaPemohonFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column4" DataField="app_date" HeaderText="Tanggal Aplikasi" DataFormatString="{0:dd-MMM-yyyy}" runat="server" ShowFilterCriterias="false" Width="8%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtAppDate" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppDateFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column19" DataField="brw_type" HeaderText="Borrower Type" runat="server" ShowFilterCriterias="false" Width="8%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtAppBrwType" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppBrwTypeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column20" DataField="prd_name" HeaderText="Product Type" runat="server" ShowFilterCriterias="false" Width="8%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtAppPrdType" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppPrdTypeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column7" DataField="lsw_name" HeaderText="Workflow" runat="server" ShowFilterCriterias="false" Width="10%" Visible="false">
                    <TemplateSettings RowEditTemplateControlId="txtWorkflow" RowEditTemplateControlPropertyName="value" FilterTemplateId="WorkflowFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column8" DataField="lwp_name" HeaderText="Proses" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtWorkflowPosisi" RowEditTemplateControlPropertyName="value" FilterTemplateId="WorkflowPosisiFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column18" DataField="usr_name" HeaderText="User" runat="server" ShowFilterCriterias="false" Width="8%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtUser" RowEditTemplateControlPropertyName="value" FilterTemplateId="UserFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column5" DataField="app_is_open" HeaderText="Status" runat="server" ShowFilterCriterias="false" Width="5%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtAppStatus" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppStatusFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column6" HeaderText="Action" runat="server" Width="12%">
                    <TemplateSettings TemplateID="viewBtnTemplate" />
                </obout:Column>
            </Columns>
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
            <Templates>
                <obout:GridTemplate runat="server" ID="viewBtnTemplate">
                    <Template>
                        <%  If allowDetail = True Then%>
                        <a class="ob_gALF" href="javascript:detail('<%# Container.DataItem("app_no") %>','<%# Container.DataItem("app_brw_code") %>');">Detail</a>&nbsp;&nbsp;
                        <%End If%>
                         <%  If allowLogs = True Then%>
                       <a class="ob_gALF" href="javascript:fSetting('<%# Container.DataItem("app_no") %>');">Setting</a>&nbsp;&nbsp;
                        <%End If%>
                        <a class="ob_gALF" href="javascript:fMonitoring('<%# Container.DataItem("app_no") %>');">Monitoring</a>
                       
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="BrwCodeFilter" ControlID="BrwCode">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="BrwCode" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AppNoFilter" ControlID="AppNo">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppNo" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AppNamaPemohonFilter" ControlID="AppNamaPemohon">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppNamaPemohon" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AppDateFilter" ControlID="AppDate">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppDate" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AppBrwTypeFilter" ControlID="AppBrwType">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppBrwType" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AppPrdTypeFilter" ControlID="AppPrdType">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppPrdType" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AppStatusFilter" ControlID="AppStatus">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppStatus" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                 <obout:GridTemplate runat="server" ID="WorkflowPosisiFilter" ControlID="AppProses">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppProses" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="UserFilter" ControlID="AppUser">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppUser" Width="100%">
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

        function applyFilter2() {
            if (applyFilterTimeout) {
                window.clearTimeout(applyFilterTimeout);
            }
            applyFilterTimeout = window.setTimeout(doFiltering2, 500);
        }

        function doFiltering2() {
            grid2.filter();
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
            fAdd("-1","-1","");
            return false;
        }
        function fAdd(id, brwcode){
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
        function fLogs(id) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "activitiesLog.aspx?appno=" + id;
            oWindowscroll(l_obj_argument.url, 1000, 600);
        }
        function fMonitoring(id) {
            PageMethods.encrpytAppNo(id, OnSuccess, OnFailure);
        }
        function OnSuccess(response) {
            window.location.href = "underwritingMon.aspx?appno=" + response;
        }
        function OnFailure(response) {
        }
    </script>
</asp:Content>