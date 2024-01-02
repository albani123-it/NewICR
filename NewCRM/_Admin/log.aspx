<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="log.aspx.vb" Inherits="NewCRM.log" %>

<%@ Register assembly="obout_Grid_NET" namespace="Obout.Grid" tagprefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Admin');
        aBc('log');
    </script>
    <style type="text/css">
        input[type=text]
        {
            height: 16px;
            width: 150px;
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 2px 5px;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 12px;      	
        }
        
        div
        {
        	color: #2b4c61;
        }
        
        .ob_gFPT
        {
        	display:none;
        }
        
        #ui-datepicker-div
        {
        	display: none;
        }
        
        input
        {
        	font-family: Century Gothic;
        	font-size: 12px;
        }
        
        .ob_gNRM .ob_gCc1 
        {
        	z-index: 1;
        }
        
            .ob_gCS, .ob_gCS div, .ob_gCS_F, .ob_gCS_F div 
            {
                  display: none;
            }  
         
                           
    </style>
    <div style="margin: 0 10px;">
    <fieldset style="width:98%; border-radius:5px; background:#eeeeee;">
        <div style="margin-bottom: 30px;">
            <span style="float:left;">
                Log Mode :  <asp:DropDownList ID="optionMode" runat="server"  class="inpDdl"></asp:DropDownList>
                From : <input type="text" style="width:121px" runat="server" readonly="readonly" name="txtFrom" id="txtFrom" value="" />
                    <script type='text/javascript'>
                        $(function () {
                            $('#<%= txtFrom.ClientID %>').datepicker({
                                duration: '',
                                dateFormat: 'dd-mm-yy',
                                yearRange: "-5:+0",
                                constrainInput: true,
                                changeMonth: true,
                                changeYear: true
                            });
                        });
                    </script>
                To : <input type="text" style="width:121px" runat="server" readonly="readonly" name="txtTo" id="txtTo" value="" />
                    <script type='text/javascript'>
                        $(function () {
                            $('#<%= txtTo.ClientID %>').datepicker({
                                duration: '',
                                dateFormat: 'dd-mm-yy',
                                yearRange: "-5:+0",
                                constrainInput: true,
                                changeMonth: true,
                                changeYear: true
                            });
                        });
                    </script> 
                <asp:Button ID="btnSearch" runat="server" Text="Apply Filter" />
            </span>

            <span style="float:right">
                <obout:OboutButton ID="OboutButton2" runat="server" Text="Export all pages to CSV" OnClick="OboutButton2_Click" />
            </span>
        </div>

        <div>
            <obout:Grid id="grid1" runat="server" CallbackMode="false" Serialize="true" PageSize="20" 
            AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowPageSizeSelection="false" AllowFiltering="false" AllowRecordSelection="false" 
            FolderStyle="~/styles/grid" AllowAddingRecords="False" AllowSorting="False">
                <Columns>
                    <obout:Column ID="Column3" DataField="strlog" Visible="true" HeaderText="Log Records" runat="server" Width="90%" />
                    <obout:Column ID="Column7" HeaderText="Action" runat="server" Width="10%">
                        <TemplateSettings  TemplateID="viewBtnTemplate" />
                    </obout:Column>
                </Columns>  
                <Templates>
                    <obout:GridTemplate runat="server" ID="viewBtnTemplate">
                        <Template>
                            <a class="ob_gAL" href="javascript:detail('<%# Container.DataItem("log_id") %>','<%# Container.DataItem("code") %>');"/>Detail</a>
                        </Template>
                    </obout:GridTemplate>
                </Templates>  
            </obout:Grid>                    
        </div>
    </fieldset>
    </div>
    <script type="text/javascript">
        function detail(csv,code) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "LogDet.aspx?id=" + csv + '&code=' + code;
            var w = 1000;
            var h = 600;
            if(code=="users" || code=="ua" || code=="offering"){
                h = 250;
            }
            oWindowscroll(l_obj_argument.url, w, h);
            document.Form1.is_refresh.value = "true"
            document.Form1.submit();
        }
        fLoadLiteral();
        function fLoadLiteral(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}   
    </script>
</asp:Content>