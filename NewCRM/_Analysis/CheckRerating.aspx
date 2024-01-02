<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CheckRerating.aspx.vb" Inherits="NewCRM.CheckRerating" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Analysis');
        aBc('check_rerating');
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
    <div style="margin:auto;width:98%;">
        <div>
        <obout:Grid id="grid1" runat="server" Width="100%" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="15"
			    AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
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
                <obout:Column ID="Column3" DataField="app_no" HeaderText="Nomor Aplikasi" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtAppNo" RowEditTemplateControlPropertyName="value" FilterTemplateId="AppNoFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column8" DataField="lsb_namaPemohon" HeaderText="Nama Borrower" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtNamaBorrower" RowEditTemplateControlPropertyName="value" FilterTemplateId="NamaBorrowerFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column20" DataField="prd_name" HeaderText="Nama Produk" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtNamaProduk" RowEditTemplateControlPropertyName="value" FilterTemplateId="NamaProdukFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column4" DataField="rating_terakhir" HeaderText="Tanggal Rating"  runat="server" ShowFilterCriterias="false" Width="12%" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtTglRating" RowEditTemplateControlPropertyName="value" FilterTemplateId="TglRatingFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>

                <obout:Column ID="Column18" DataField="usr_name" HeaderText="User yang merating" runat="server" ShowFilterCriterias="false" Width="10%" Wrap="true">
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
                <obout:Column ID="Column6" HeaderText="Action" runat="server" Width="10%">
                    <TemplateSettings TemplateID="viewBtnTemplate" />
                </obout:Column>
            </Columns>
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
            <Templates>
                <obout:GridTemplate runat="server" ID="viewBtnTemplate">
                    <Template>
                        <a class="ob_gALF" href="javascript:detail('<%# Container.DataItem("app_no") %>','<%# Container.DataItem("app_brw_code") %>','<%# Container.DataItem("app_workflow") %>');">Detail</a>&nbsp;
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
                <obout:GridTemplate runat="server" ID="NamaBorrowerFilter" ControlID="NamaBorrower">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="NamaBorrower" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="NamaProdukFilter" ControlID="NamaProduk">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="NamaProduk" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="TglRatingFilter" ControlID="TglRating">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="TglRating" Width="100%">
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
                
                <obout:GridTemplate runat="server" ID="AppStatusFilter" ControlID="AppStatus">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="AppStatus" Width="100%">
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
            detail("-1", "-1", "");
            return false;
        }

        function detail(id) {
            window.location.href = "Underwriting/General.aspx?appno=" + id;
        }
    </script>
</asp:Content>