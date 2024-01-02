<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="productOffer.aspx.vb" Inherits="NewCRM.productOffer" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">


    /** **/
    function OnBeforeEdit(record) {
    fEdit(record.po_id);
        return false;
    }

    function OnBeforeAdd(record) {
        fEdit(-1);
        return false;
    }

    function fEdit(id) {
        var l_obj_argument = new Object();
        l_obj_argument.url = "productOfferDet.aspx?productOffer_id=" + (id);
        oWindow(l_obj_argument.url, 500, 250);
    }

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
        if (confirm("Are you sure you want to delete?") == false) {
            return false;
        }
        return true;
    }
    function OnDelete(record) {
        alert("Data has been deleted successfully.")
    }
    /* -- */

    </script>

    <div style="clear:both; margin:0 auto; padding:0; width:98%;margin-bottom:20px;">
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" PageSize="20"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
                AllowPageSizeSelection="false" AllowFiltering="true" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
            <Columns>
                <obout:Column id="Column1" runat="server" DataField="po_id" Visible="false"></obout:Column>
                <obout:Column id="Column2" runat="server" DataField="po_offering_code" HeaderText="Offering Code" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtOfferingCode" RowEditTemplateControlPropertyName="value" FilterTemplateId="OfferingCodeFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column3" runat="server" DataField="category_name" HeaderText="Category Name" Width="15%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtCategoryName" RowEditTemplateControlPropertyName="value" FilterTemplateId="CategoryNameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column4" runat="server" DataField="po_product" HeaderText="Product" Width="20%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtProduct" RowEditTemplateControlPropertyName="value" FilterTemplateId="ProductFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column5" runat="server" DataField="type_name" HeaderText="Type Name" Width="20%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtTypeName" RowEditTemplateControlPropertyName="value" FilterTemplateId="TypeNameFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column6" runat="server" DataField="po_sla" HeaderText="SLA" Width="20%" ShowFilterCriterias="false" Wrap="true">
                    <TemplateSettings RowEditTemplateControlId="txtSLA" RowEditTemplateControlPropertyName="value" FilterTemplateId="SLAFilter" />
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column id="Column8" runat="server" HeaderText="" Width="5%"></obout:Column>
                <obout:Column id="Column7" runat="server" HeaderText="Action" Width="5%" AllowEdit="true"></obout:Column>
                <obout:Column id="Column9" runat="server" Width="5%">
                    <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>
            </Columns>
            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
            <Templates>
                 <obout:GridTemplate runat="server" ID="deleteTemplate" >
                    <Template>
                        <%  If AllowDelete = True Then%>
                                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                        <%  End If%>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="OfferingCodeFilter" ControlID="offeringCode">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="offeringCode" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="CategoryNameFilter" ControlID="CategoryName">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="CategoryName" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="ProductFilter" ControlID="product">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="product" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="TypeNameFilter" ControlID="TypeName">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="TypeName" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="SLAFilter" ControlID="sla">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="sla" Width="100%">
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
</asp:Content>
