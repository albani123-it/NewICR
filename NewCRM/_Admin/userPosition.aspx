<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="userPosition.aspx.vb" Inherits="NewCRM.userPosition" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        /** **/
        function OnBeforeEdit(record) { 
            $('#userPositionModal').modal('toggle');
            document.getElementById("spnAction").innerHTML = "Edit";
            getEditUserPosition(record.usp_value);
            return false;
        }

        function OnBeforeAdd() {            
            $('#userPositionModal').modal('toggle');
            document.getElementById("spnAction").innerHTML = "Add New";
            clearForm();
            return false;
        }

        function clearForm(){
            document.getElementById("<%=txtCode.ClientID %>").readOnly = false;
            document.getElementById("<%=hidID.ClientID %>").value = "";
            document.getElementById("<%=txtCode.ClientID %>").value = "";
            document.getElementById("<%=txtValue.ClientID %>").value = "";
            document.getElementById("<%=hidID.ClientID %>").value = "";
            document.getElementById("<%=txtValue.ClientID %>").value = "";
            document.getElementById("<%=txtDesc.ClientID %>").value ="";
        }

        function OnSuccessEditUserPosition(response) {   
            clearForm();
            var obj = jQuery.parseJSON(response.d);
            if (obj != '') {

                if (obj[0].usp_status == true){
            
                    document.getElementById("<%=txtCode.ClientID %>").readOnly = true;
                    document.getElementById("<%=hidID.ClientID %>").value = obj[0].usp_value;
                    document.getElementById("<%=txtCode.ClientID %>").value = obj[0].usp_code;
                } else {
                    document.getElementById("<%=txtCode.ClientID %>").readOnly = false;
                    document.getElementById("<%=txtCode.ClientID %>").value = obj[0].usp_code;
                    document.getElementById("<%=hidID.ClientID %>").value = obj[0].usp_value;
                }

                if (obj[0].usp_status == true){
                    document.getElementById("<%=txtValue.ClientID %>").readOnly = true;
                    document.getElementById("<%=hidID.ClientID %>").value = obj[0].usp_value;
                    document.getElementById("<%=txtValue.ClientID %>").value = obj[0].usp_value;
                }else {
                    document.getElementById("<%=txtValue.ClientID %>").readOnly = false;
                    document.getElementById("<%=txtValue.ClientID %>").value = obj[0].usp_value;
                }
                document.getElementById("<%=txtDesc.ClientID %>").value = obj[0].usp_description;
                
            }

         else {
                document.getElementById("<%=txtCode.ClientID %>").value = "";
                document.getElementById("<%=txtValue.ClientID %>").value = "";
            
        }
        
        
        }

        function getEditUserPosition(val) {
            $.ajax({
                type: "POST",
                url: "userPosition.aspx/getEditUserPosition",
                data: '{usp_value:"'+ val +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessEditUserPosition, 
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function OnSuccess(data) {  
            var obj = data;
            alert(data);
        }

       
               /** **/
    </script>

    <script type="text/javascript">	
        

        

        fLoad();
        	
		function fLoad(){
		    <asp:Literal id="liBodyLoadAction" runat="server" />
           
		}
		</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2>User Position<small>&nbsp;</small></h2>              
            
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">                
                <obout:Grid id="grdUserPosition" runat="server" CallbackMode="true" Serialize="true" 
                AutoGenerateColumns="false" Width="100%" AllowPageSizeSelection="false" 
                AllowFiltering="false" OnRebind="ReBinGrid" FolderStyle="~\Content\Themes1\Grid" style="color: #000000; background-color: #000000">
                <ClientSideEvents OnBeforeClientAdd="OnBeforeAdd" OnBeforeClientEdit="OnBeforeEdit" />
                <Columns>
                <obout:Column ID="Column2" DataField="usp_code"  HeaderText="Code" runat="server" ShowFilterCriterias="false" Width="8%">				   
				</obout:Column>
                <obout:Column ID="Column3" DataField="usp_value" Visible="true" HeaderText="Value" runat="server" ShowFilterCriterias="false" Width="8%">
                                   
                </obout:Column>
                <obout:Column ID="Column4" DataField="usp_description" HeaderText="Description" runat="server" ShowFilterCriterias="false" Width="24%">
                                  
                </obout:Column>
                <obout:Column ID="Column5" DataField="usp_counter" HeaderText="Counter" runat="server" ShowFilterCriterias="false" Width="15%">
                                  
                </obout:Column>
                
                <obout:Column ID="Column1" HeaderText="Action" AllowEdit="true" runat="server" Width="7%"/>
                <obout:Column ID="Column9" HeaderText="" Width="6%" runat="server">
			        <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>

                <obout:Column ID="Column8" DataField="usr_en" Visible="false" HeaderText="User ID en" runat="server"/>
                </Columns>

                			
                <Templates>
                <obout:GridTemplate runat="server" ID="deleteTemplate" >
                <Template>
                  
                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain you want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                    
                </Template>
                </obout:GridTemplate>            
                </Templates>
                </obout:Grid>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="userPositionModal" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="userPositionModalLabel">
    <div class="modal-dialog modal-lg" role="document">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">User Positiion | <small><span id="spnAction" /></small></h4>
                </div>

                <div class="modal-body">  
               <%--     <asp:Literal id="litModal" runat="server" />--%>
                    <input type="hidden" id="hidID" name="hidID" runat="server">
                    <asp:HiddenField ID="hidLevelCode" runat="server" />
                    <table id="tbl1" class="table table-striped table-bordered" style="text-align:left;" cellSpacing="1" cellPadding="0" width="98%">
                        <tr>
					        <td style="WIDTH: 148px"><b> Code</b></td>
					        <td><asp:textbox id="txtCode" name="txtCode" required="required" runat="server" MaxLength="50" Width="350" CssClass="form-control" placeholder="Code"></asp:textbox></td>
				        </tr>
				        <tr>
					        <td style="WIDTH: 148px"><b>Value</b></td>
					        <td><asp:textbox id="txtValue" name="txtValue" required="required" runat="server" MaxLength="50" Width="350" CssClass="form-control" placeholder="Value"></asp:textbox></td>
				        </tr>
				        <tr>
					        <td style="WIDTH: 148px"><b>Description</b></td>
					        <td><asp:textbox id="txtDesc" name="txtDesc" required="required" runat="server" MaxLength="50" Width="350" CssClass="form-control" placeholder="Description"></asp:textbox></td>
				        </tr>

                       
				       
                    </table>

                </div>

             <div class="modal-footer">
                <button type="submit" class="btn btn-default" data-dismiss="modal" aria-label="Close" style="margin-bottom:0px">Cancel</button>
                <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>
             </div>

          </div>
    </div>
</div>

</asp:Content>
