<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GlobalSetting.aspx.vb" Inherits="NewCRM.GlobalSetting" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <style type="text/css">      
        
        .ob_gFPT
        {
        	display:none;
        }
       
</style>
<script type="text/javascript">
       
        function callPopUp(iz) {
            var szWidth;
			var szHeight;
            var l_obj_argument = new Object();

			if (iz == 1) {
			    l_obj_argument.url = "glo_rat_based.aspx?glsid=" + iz;
			    szWidth = 900;
			    szHeight = 480;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			} 
            else if (iz == 2) {
			    l_obj_argument.url = "glo_non_rat_based.aspx?glsid=" + iz;
			    szWidth = 1000;
			    szHeight = 550;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			} 
            else if (iz == 3) {
			    l_obj_argument.url = "glo_haircut_security.aspx?glsid=" + iz;
			    szWidth = 1100;
			    szHeight = 400;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			} 
            else if (iz == 4) {
			    l_obj_argument.url = "glo_eligible_IRB.aspx?glsid=" + iz;
			    szWidth = 900;
			    szHeight = 400;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			} 
            else if (iz == 5) {
			    l_obj_argument.url = "glo_settlement_risk.aspx?glsid=" + iz;
			    szWidth = 550;
			    szHeight = 300;
			} 
            else if (iz == 6) {
			    l_obj_argument.url = "glo_counter_party_CR.aspx?glsid=" + iz;
			    szWidth = 1000;
			    szHeight = 400;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			}    
            else if (iz == 7) {
			    l_obj_argument.url = "glo_pre_screening.aspx?glsid=" + iz;
			    szWidth = 1000;
			    szHeight = 400;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			}  
            else if (iz == 9) {
			    l_obj_argument.url = "glo_capital.aspx?glsid=" + iz;
			    szWidth = 1000;
			    szHeight = 400;
                oWindowscroll(l_obj_argument.url, szWidth, szHeight);
			}
            else if (iz == 10) {
			    window.location.href = "glo_picklist.aspx";
			}
            else if (iz == 11) {
			    window.location.href = "glo_financial.aspx";
			}
            return false;
                    
        }

		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}            
    </script>


<div style="clear:both; margin:0 auto; padding:0; width:98%">
     <obout:Grid id="GloSetGrid" runat="server" Serialize="true" pagesize="15" 
         AllowSorting="False" AllowRecordSelection="False"
			AutoGenerateColumns="False" EnableRecordHover="true" Width="100%" AllowPaging="False"
            AllowPageSizeSelection="False" AllowAddingRecords="False"
            FolderStyle="~/styles/grid">
            
            <Columns>
                <obout:Column ID="Column1" DataField="gls_id" Visible="false" HeaderText="ID" runat="server" />				   
                <obout:Column ID="Column2" DataField="gls_parameter" HeaderText="Parameter" runat="server" Width="50%" />
                <obout:Column ID="Column3" DataField="gls_last_update" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Last Update" runat="server" Width="15%" />
                <obout:Column ID="Column4" DataField="gls_last_update_by" HeaderText="Last Update By" runat="server" Width="15%" />
                <obout:Column ID="Column5" HeaderText="Action" runat="server" Width="20%">
                    <TemplateSettings TemplateID = "manageTemplate" />
                </obout:Column>                                
			</Columns>			
            <Templates>
                 <obout:GridTemplate runat="server" ID="manageTemplate" >
                    <Template>
                        <%--<asp:LinkButton ID="mng" runat="server" OnCommand="manage" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Manage  </asp:LinkButton>--%>
                        <a class="ob_gALF" href="javascript:callPopUp('<%# Container.DataItem("gls_id") %>');">Manage</a>
                    </Template>
                 </obout:GridTemplate> 
            </Templates>
		</obout:Grid>
</div>
</asp:Content>
