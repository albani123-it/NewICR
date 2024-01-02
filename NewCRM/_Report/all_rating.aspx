<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="all_rating.aspx.vb" Inherits="NewCRM.all_rating" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        aMenu('Report');
        aBc('report_rating');
     
		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fLoad();
        $(function() {
            $("#<%=txtStartDate.clientID %>").datepicker(
            { duration: '',
                yearRange: "-5:+5",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            
            $("#<%=txtEndDate.clientID %>").datepicker(
            { duration: '',
                yearRange: "-5:+5",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
    <style type="text/css">      
       #ui-datepicker-div {display: none;}
    </style>

<div style="clear:both; margin:0 auto; padding:0; width:98%">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div style="border: 1px solid #000; width:100%;margin-bottom:20px;height:50px;background:#D9E5FF;">
        
            <div style="float:left;padding:10px;">
                <span><b>Type </b></span>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="inpDdl" style="float:none;width:100px;height:20px;">
                    <asp:ListItem Value="Select One">Select One</asp:ListItem>
                    <asp:ListItem Value="Borrower">Borrower</asp:ListItem>
                    <asp:ListItem Value="Facility">Facility</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div style="float:left;padding:10px;">
                <span><b>Start Date </b></span>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="inpTxt" style="float:none;height:15px;"></asp:TextBox>    
            </div>

            <div style="float:left;padding:10px;">
                <span><b>End Date </b></span>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="inpTxt" style="float:none;height:15px;"></asp:TextBox>
            </div>

            <div style="float:right;padding:10px;">
                <asp:Button ID="btnView" runat="server" CssClass="inpBtn" Text="View Report" />
            </div>
        
    </div>
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" style="display:none;"
            Font-Size="8pt" ProcessingMode="Remote" Width="100%" Height="600px" 
            BackColor="#d9e5ff" BorderStyle="Solid" BorderColor="Black" BorderWidth="1">
            <ServerReport 
                ReportPath="/basel_dki/borrower_rating" />
       </rsweb:ReportViewer>

       <rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" style="display:none;"
            Font-Size="8pt" ProcessingMode="Remote" Width="100%" Height="600px" 
            BackColor="#d9e5ff" BorderStyle="Solid" BorderColor="Black" BorderWidth="1">
            <ServerReport 
                ReportPath="/basel_dki/facility_rating" />
       </rsweb:ReportViewer>
</div>
</asp:Content>
