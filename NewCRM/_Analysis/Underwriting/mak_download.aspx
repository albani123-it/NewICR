<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="mak_download.aspx.vb" Inherits="NewCRM.mak_download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MAK</title>
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
            var record = arrSelectedRecords[0];
//            alert(record.usr_usersid);
             window.opener.popupCallback(record.usr_usersid,record.usr_name);
             this.close();
	    }
    </script>
    
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="clear:both; margin:0 auto; padding:0; width:98%">
        <center>
            <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
                <tr>
                    <td>
                        <legend><b>MAK</b></legend>
                        <obout:Grid id="grid2" runat="server"  pagesize="15" KeepSelectedRecords="true"
			        AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowMultiRecordSelection="false" AllowRecordSelection="true"
                    AllowPageSizeSelection="false" AllowFiltering="false" AllowAddingRecords="false"  AllowColumnResizing="false"
                    FolderStyle="~/styles/grid">
                    <ClientSideEvents OnClientSelect="onRecordSelect" />
                            <Columns>
                                <obout:Column ID="Column5" DataField="lmakf_id" Visible="false" runat="server"/>
                                <obout:Column ID="Column6" DataField="lmakf_app_no" Visible="false" runat="server"/>
                                <obout:Column ID="Column7" DataField="lmakf_file" HeaderText="Nama Dokumen" runat="server" ShowFilterCriterias="false" Width="70%">
                                    <TemplateSettings RowEditTemplateControlId="txtDoc2" RowEditTemplateControlPropertyName="value" FilterTemplateId="Doc2Filter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column8" DataField="usr_name" HeaderText="Dilampirkan Oleh" runat="server" ShowFilterCriterias="false" Width="25%">
                                    <TemplateSettings RowEditTemplateControlId="txtUpdateBy" RowEditTemplateControlPropertyName="value" FilterTemplateId="UpdateByFilter" />
                                    <FilterOptions>
                                        <obout:FilterOption IsDefault="true" Type="Contains" />
                                    </FilterOptions>
                                </obout:Column>
                                <obout:Column ID="Column9" HeaderText="Action" runat="server" Width="20%">
                                    <TemplateSettings TemplateID="downloadTemplate" />
                                </obout:Column>
                            </Columns>
                            <FilteringSettings InitialState="Visible" FilterPosition="Top" />
                            <Templates>
                                <obout:GridTemplate runat="server" ID="downloadTemplate">
                                    <Template>
                                        <asp:LinkButton ID="LinkButton1" OnCommand="download" runat="server" class='ob_gAL' CommandArgument='<%# Container.DataItem("lrf_file") %>' >  Download </asp:LinkButton>&nbsp;&nbsp;    
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="Doc2Filter" ControlID="Doc">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="Doc2" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter2" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="UpdateByFilter" ControlID="Doc">
                                    <Template>
                                        <obout:OboutTextBox runat="server" ID="UpdateBy" Width="100%">
                                            <ClientSideEvents OnKeyUp="applyFilter2" />
                                        </obout:OboutTextBox>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="tplRemoveFilter2">
                                    <Template>
                                        <a onclick="grid2.removeFilter()" class="ob_gALF" style="cursor:pointer">Clear Filter</a>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
                        <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter2"/>
                        </obout:Grid>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
