<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ratingMgmt.aspx.vb" Inherits="NewCRM.ratingMgmt" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        
        var menu = "<%=PageType%>";
        if (menu == "Borrower") {
            aBc("borrowerRerating");
        }
        if (menu == "Facility") {
            aBc("facilityRerating");
        }
        
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
            
		}
        fAction();
    </script>
    <style type="text/css">
        .lblSearch
        {
            float:left;
            width:100px;
            height:16px;
            margin-top: 4px;
        }
        .rw
        {
            float:left;
            width:25%;
        }
        .clm
        {
            width:100%;
            height:27px;
        }
    </style>
    <div style="clear:both; margin:0 auto; padding:0; width:98%">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
        <fieldset style="width:98.2%; border-radius:5px; background:#eeeeee;">
            <legend style="font-weight:bold">
                <asp:Label ID="TxtLegend" runat="server" Text=""></asp:Label>
            </legend>
           
            <div>
                <div class="clm">
                <% If PageType = "Borrower" Then%>
                    <div class="rw">
                        <asp:Label ID="LblBorrowerCode" runat="server" Text="Kode Borrower" CssClass="lblSearch"></asp:Label>
                        <input type="text" runat="server" class="inpTxt" id="txtBorrowerCode" style="width:180px" />
                    </div>
                <% End If%>
                <% If PageType = "Facility" Then%>
                    <div class="rw">
                        <asp:Label ID="LblApp" runat="server" Text="No. Aplikasi" CssClass="lblSearch"></asp:Label>
                        <input type="text" runat="server" class="inpTxt" id="txtApp" style="width:180px" />
                    </div>
                <% End If%>
                <% If PageType = "Borrower" Then%>
                    <div class="rw">
                        <asp:Label ID="LblNama" runat="server" Text="Nama" CssClass="lblSearch"></asp:Label>
                        <input type="text" runat="server" id="txtNama" class="inpTxt" style="width:180px" />
                    </div>
                <% End If%>
                </div>
                <div class="clm">
                <% If PageType = "Borrower" Then%>
                    <div class="rw">
                        <asp:Label ID="LblNPWP" runat="server" Text="NPWP" CssClass="lblSearch"></asp:Label>
                        <input type="text" runat="server" id="txtNPWP" class="inpTxt" style="width:180px" />
                    </div>
                <% End If%>
                <% If PageType = "Facility" Then%>
                    <div class="rw">
                        <asp:Label ID="LblRek" runat="server" Text="No. Rekening" CssClass="lblSearch" style="display:none;"></asp:Label>
                        <input type="text" runat="server" id="txtRek" class="inpTxt" style="width:180px;display:none;" />
                    </div>
                <% End If%>
                <% If PageType = "Borrower" Then%>
                    <div class="rw">
                        <asp:Label ID="LblCIF" runat="server" Text="No. CIF" CssClass="lblSearch"></asp:Label>
                        <input type="text" runat="server" id="txtCIF" class="inpTxt" style="width:180px" />
                    </div>
                <% End If%>
                </div>
                <div class="clm"></div>
                <div class="clm"><asp:button id="btnSearch" runat="server" Text="Search" CssClass="inpBtn"></asp:button></div>
            </div>
        </fieldset>
        <br />
        <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" AllowPaging="true" PageSize="12" AllowColumnResizing="false"
			    AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowSorting="false" AllowAddingRecords="false"
                AllowPageSizeSelection="false" AllowFiltering="false" AllowRecordSelection="false" FolderStyle="~/styles/grid">
            <Columns>
                <obout:Column ID="Column1" DataField="app_id" Visible="false" runat="server"/>
                <obout:Column ID="Column2" DataField="app_brw_code" HeaderText="Kode Borrower" runat="server" Width="10%" />
                <obout:Column ID="Column3" DataField="app_no" HeaderText = "No. Aplikasi" Width="10%" runat="server"/>
                <obout:Column ID="Column4" DataField="brw_type" HeaderText="Borrower Type" runat="server"/>
                <obout:Column ID="Column5" DataField="prd_name" HeaderText="Product Type" runat="server"/>
                <obout:Column ID="Column6" DataField="lsb_cif" HeaderText="CIF" runat="server" Width="10%" />
                <obout:Column ID="Column7" DataField="lsb_npwp" HeaderText="NPWP" runat="server" Width="10%" />
                <obout:Column ID="Column8" DataField="lsb_namaPemohon" HeaderText="Nama Debitur" runat="server" Width="20%" />
                <obout:Column ID="Column9" DataField="lfc_nilaiRupiah" HeaderText="Limit (IDR)" DataFormatString="{0:N2}" runat="server" Width="10%" />
                <obout:Column ID="Column10" DataField="lfc_fasilitas" HeaderText="Jenis Fasilitas" runat="server" Width="20%" />
                <obout:Column ID="Column11" HeaderText="Action" runat="server" Width="10%">
                    <TemplateSettings TemplateID="viewBtnTemplate" />
                </obout:Column>
            </Columns>
            <Templates>
                <obout:GridTemplate runat="server" ID="viewBtnTemplate">
                    <Template>
                        <% If PageType = "Borrower" Then%>
                            <a class="ob_gALF" href="javascript:detail('<%# Container.DataItem("app_brw_code")%>','Borrower');">Detail</a>
                         <% End If%>
                         <% If PageType = "Facility" Then%>
                            <a class="ob_gALF" href="javascript:detail('<%# Container.DataItem("app_no")%>','Facility');">Detail</a>
                         <% End If%>
                    </Template>
                </obout:GridTemplate>
            </Templates>
        </obout:Grid>

        
    </div>
    <script type="text/javascript">

        function detail(id, type) {
            if (type == "Borrower")
                window.location.href = 'R_Borrower.aspx?brwcode=' + id + '&typ=' + type;
            else if (type == "Facility")
                window.location.href = 'R_Borrower.aspx?appno=' + id + '&typ=' + type;
        }
    </script>
</asp:Content>