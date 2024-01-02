<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="all.aspx.vb" Inherits="NewCRM.all" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">      
        
        .ob_gFPT
        {
        	display:none;
        }
       
</style>
<script type="text/javascript">
        aMenu('Report');
        aBc('report_all');
     
		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}            
    </script>


<div style="clear:both; margin:0 auto; padding:0; width:98%">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" ProcessingMode="Remote" Width="100%" Height="600px" 
            BackColor="#d9e5ff" BorderStyle="Solid" BorderColor="Black" BorderWidth="1">
            <ServerReport 
                ReportPath="/basel_dki/all" />
        </rsweb:ReportViewer>
</div>

</asp:Content>
