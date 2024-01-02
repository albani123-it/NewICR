<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_financial_value.aspx.vb" Inherits="NewCRM.glo_financial_value" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ob_gCS, .ob_gCS div, .ob_gCS_F, .ob_gCS_F div 
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function onRecordSelect(arrSelectedRecords) {
            var src = document.getElementById("DestinationControl").value;
            var record = arrSelectedRecords[0];
             window.opener.popupCallback(record.acc_code,src);
             this.close();
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
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="DestinationControl" name="DestinationControl" runat="server"/>
    <div style="clear:both; margin:0 auto; padding:0; width:98%">
        <center>
            <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td>
                    <obout:Grid id="grid1" runat="server"  pagesize="15" KeepSelectedRecords="true"
			        AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowMultiRecordSelection="false" AllowRecordSelection="true"
                    AllowPageSizeSelection="false" AllowFiltering="true" AllowAddingRecords="false"  AllowColumnResizing="false"
                    FolderStyle="~/styles/grid">
                    <ClientSideEvents OnClientSelect="onRecordSelect" />
                        <Columns>
                            <obout:Column ID="Column1" DataField="acc_code" Visible="true" HeaderText="User ID" runat="server" ShowFilterCriterias="false" Width="20%">
				                <TemplateSettings RowEditTemplateControlId="txtCode" RowEditTemplateControlPropertyName="value" FilterTemplateId="CodeFilter"/>
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                </FilterOptions>
				            </obout:Column>
                            <obout:Column ID="Column2" DataField="acc_desc" HeaderText="Name" runat="server" ShowFilterCriterias="false" Width="30%">
                                <TemplateSettings RowEditTemplateControlId="txtDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="DescFilter" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                </FilterOptions>                
                            </obout:Column>
                            <obout:Column ID="Column3" DataField="acc_rpt" HeaderText="User Role" runat="server" ShowFilterCriterias="false" Width="35%">
                                <TemplateSettings RowEditTemplateControlId="txtReport" RowEditTemplateControlPropertyName="value" FilterTemplateId="ReportFilter" />
                                <FilterOptions>
                                    <obout:FilterOption IsDefault="true" Type="Contains" />
                                </FilterOptions>                
                            </obout:Column>
			            </Columns>
			
                        <FilteringSettings InitialState="Visible" FilterPosition="Top" />

			            <Templates>
                            <obout:GridTemplate runat="server" ID="CodeFilter" ControlID="CodeF">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="CodeF" Width="100%">
                                        <ClientSideEvents OnKeyUp="applyFilter" />
                                    </obout:OboutTextBox>
                                </Template>
                            </obout:GridTemplate>
                            <obout:GridTemplate runat="server" ID="DescFilter" ControlID="DescF">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="DescF" Width="100%">
                                        <ClientSideEvents OnKeyUp="applyFilter" />
                                    </obout:OboutTextBox>
                                </Template>
                            </obout:GridTemplate>
                            <obout:GridTemplate runat="server" ID="ReportFilter" ControlID="ReportF">
                                <Template>
                                    <obout:OboutTextBox runat="server" ID="ReportF" Width="100%">
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
				    <td background="../styles/grid/footer.gif" colspan="2" style="border-radius: 0 0 5px 5px;"></td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
