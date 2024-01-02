<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="user.aspx.vb" Inherits="NewCRM.user" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   

 <script type="text/javascript">
     /** **/

     function OnBeforeEdit(record) { 
         $('#userModal').modal('toggle');
         document.getElementById("spnAction").innerHTML = "Edit";
         getEditUser(record.usr_usersid);
         return false;
     }

     function OnBeforeDelete(record) { 
         swal({
             title: "Are you certain you want to delete?",
             text: "",
             type: "warning",
             showCancelButton: true,
             confirmButtonClass: "btn-danger",
             confirmButtonText: "Delete",
             cancelButtonText: "Cancel",
             closeOnConfirm: false,
             closeOnCancel: false
         },
         function(isConfirm) {
             if (isConfirm) {
                 getDeleteUser(record.usr_userid);
             } else {
                 swal({
                     title: 'Cancelled',
                     text: '',
                     type: 'error',
                     timer: 800,
                     showConfirmButton: false,
                 })
             }
         });         
     }

     function getDeleteUser(val) {
         $.ajax({
             type: "POST",
             url: "user.aspx/getDeleteUser",
             data: '{userid:"'+ val +'"}',
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: OnSuccessDeleteUser, 
             failure: function (response) {
                 alert(response.d);
             }
         });
     }

     function OnSuccessDeleteUser(response) { 
         var obj = response.d;    
         alert(obj);
         if (obj == "True") {
             sweetURL("Data has been deleted successfully","","success",window.location.href);         
         } else {
             sweet("Delete failed","You cannot delete your self.","error");         
         }
         
     }

        function OnBeforeAdd() {            
            $('#userModal').modal('toggle');
            document.getElementById("spnAction").innerHTML = "Add New";
            clearForm();
            return false;
        }
     /** **/ 

        function clearForm() {
            document.getElementById("<%=txtUserID.ClientID %>").readOnly = false;
            document.getElementById("<%=hidID.ClientID %>").value = "";
            document.getElementById("<%=txtUserID.ClientID %>").value = "";
            document.getElementById("<%=txtUserName.ClientID %>").value = "";
            document.getElementById("<%=txtNIP.ClientID %>").value = "";
            document.getElementById("<%=txtEmail.ClientID %>").value = "";
            document.getElementById("<%=ddlBranch.ClientID %>").selectedIndex = "";
           <%-- document.getElementById("<%=lblRegion.ClientID %>").innerHTML = "";
            document.getElementById("<%=lblArea.ClientID %>").innerHTML = "";--%>
            document.getElementById("<%=ddlSupervisor.ClientID %>").value = "";
           <%-- document.getElementById("<%=ddlPosition.ClientID %>").value = "";--%>
            document.getElementById("<%=ddlLevel.ClientID %>").value = "";
            document.getElementById("<%=txtEfectiveDate.ClientID %>").value = "";
            document.getElementById("<%=chkStatus.ClientID %>").checked = true;
        }
     
        function getEditUser(val) {
        $.ajax({
            type: "POST",
            url: "user.aspx/getDetailUser",
            data: '{userid:"'+ val +'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessEditUser, 
            failure: function (response) {
                alert(response.d);
            }
        });
    }
                
        function OnSuccessEditUser(response) { 
       
        var obj = jQuery.parseJSON(response.d);
        if (obj != '') {
            document.getElementById("<%=txtUserID.ClientID %>").readOnly = true;
            document.getElementById("<%=hidID.ClientID %>").value = obj[0].usr_usersid;
            document.getElementById("<%=txtUserID.ClientID %>").value = obj[0].usr_usersid;
            document.getElementById("<%=txtUserName.ClientID %>").value = obj[0].usr_name;
            document.getElementById("<%=txtNIP.ClientID %>").value = obj[0].usr_nip;
            document.getElementById("<%=txtEmail.ClientID %>").value = obj[0].usr_email;
            document.getElementById("<%=ddlBranch.ClientID %>").value = obj[0].usr_branch_code;
          <%--  document.getElementById("<%=lblRegion.ClientID %>").innerHTML = obj[0].REGION;
            document.getElementById("<%=lblArea.ClientID %>").innerHTML = obj[0].AREA;--%>
            document.getElementById("<%=ddlSupervisor.ClientID %>").value = obj[0].usr_supervisor;
            <%--document.getElementById("<%=ddlPosition.ClientID %>").value = obj[0].usr_position;--%>
            document.getElementById("<%=ddlLevel.ClientID %>").value = obj[0].usr_access_level;
            document.getElementById("<%=txtEfectiveDate.ClientID %>").value = parseJsonDate(obj[0].usr_efective_date);
             document.getElementById("<%=chkStatus.ClientID %>").checked = true;
        } else {
            document.getElementById("<%=txtUserID.ClientID %>").value = "";
            document.getElementById("<%=txtUserName.ClientID %>").value = "";
        }
        
        
    }

     function parseJsonDate(jsonDateString){
         var value = new Date
            (
                 parseInt(jsonDateString.replace(/(^.*\()|([+-].*$)/g, ''))
            );

         var dd = value.getDate();
         var mm = value.getMonth()+1;
         var yyyy = value.getFullYear();

         if(dd<10){
             dd='0'+dd;
         } 
         if(mm<10){
             mm='0'+mm;
         } 
         var tgl = dd+'-'+mm+'-'+yyyy;
         

         return tgl;
     }

        	
    function fLoad(){
        <asp:Literal id="liBodyLoadAction" runat="server" />
     
        }

    function getRegion(val) {
        $.ajax({
            type: "POST",
            url: "user.aspx/getRegionForLabel",
            data: '{value:"'+ val +'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess, 
            failure: function (response) {
                alert(response.d);
            }
        });
    }
                
    function OnSuccess(response) {   
        var obj = jQuery.parseJSON(response.d);
        if (obj != '') {
           <%-- document.getElementById("<%=lblRegion.ClientID %>").innerHTML = obj[0].REGION;
            document.getElementById("<%=lblArea.ClientID %>").innerHTML = obj[0].AREA;--%>
        } else {
           <%-- document.getElementById("<%=lblRegion.ClientID %>").innerHTML = "";
            document.getElementById("<%=lblArea.ClientID %>").innerHTML = "";--%>
        }
        
        
    }    


    </script>


    <style type="text/css">		
						
            .ob_gFl .ob_gC, .ob_gFl .ob_gCW
            {
            	height: auto;
            }
	</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    
    <div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2>Users Management<small>Users</small></h2>              
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">
            <obout:Grid id="grdUser" runat="server" CallbackMode="true" Serialize="true" pagesize="15"
			AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
            AllowPageSizeSelection="false" AllowFiltering="true" FolderStyle="~\Content\Themes1\Grid">	
            <ClientSideEvents OnBeforeClientAdd="OnBeforeAdd" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientDelete="OnBeforeDelete" />		
            <Columns>
                <obout:Column ID="Column1" DataField="usr_usersid" HeaderText="User ID" runat="server" ShowFilterCriterias="false" Width="15%">				   
				    <TemplateSettings RowEditTemplateControlId="txtusrID" RowEditTemplateControlPropertyName="value" FilterTemplateId="UseridFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column2" DataField="usr_name" HeaderText="Name" runat="server" ShowFilterCriterias="false" Width="20%">
                    <TemplateSettings RowEditTemplateControlId="txtNM" RowEditTemplateControlPropertyName="value" FilterTemplateId="NameFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>               
                </obout:Column>
                <obout:Column ID="Column3" DataField="usr_nip" HeaderText="NIP" runat="server" ShowFilterCriterias="false" Width="8%">
                    <TemplateSettings RowEditTemplateControlId="txtNP" RowEditTemplateControlPropertyName="value" FilterTemplateId="NIPFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>             
                </obout:Column>
                <obout:Column ID="Column4" DataField="lvl_name" HeaderText="Access Level" runat="server" ShowFilterCriterias="false" Width="15%">
                    <TemplateSettings RowEditTemplateControlId="txtLVL" RowEditTemplateControlPropertyName="value" FilterTemplateId="LevelFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>                          
                </obout:Column>
                <obout:Column Visible="false" ID="Column5" DataField="branch_name" HeaderText="Branch" runat="server" ShowFilterCriterias="false" Width="20%">
                    <TemplateSettings RowEditTemplateControlId="txtBR" RowEditTemplateControlPropertyName="value" FilterTemplateId="BranchFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>                                         
                </obout:Column>
                <obout:Column ID="Column6" DataField="usr_status_format" HeaderText="Status" runat="server" ShowFilterCriterias="false" Width="8%">
                    <TemplateSettings RowEditTemplateControlId="txtST" RowEditTemplateControlPropertyName="value" FilterTemplateId="StatusFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>              
                </obout:Column>                
                <obout:Column ID="Column7" HeaderText="Action" AllowEdit="true" AllowDelete="true" runat="server" Width="10%"/>                
                <obout:Column ID="Column8" DataField="usr_en" Visible="false" HeaderText="User ID en" runat="server"/>
			</Columns>

            <FilteringSettings InitialState="Visible" FilterPosition="Top" />

            <Templates>
                
                <obout:GridTemplate runat="server" ID="UseridFilter" ControlID="uid">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="uid" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="NameFilter" ControlID="nm">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="nm" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                 <obout:GridTemplate runat="server" ID="NIPFilter" ControlID="nip">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="nip" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="LevelFilter" ControlID="lvl">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="lvl" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="StatusFilter" ControlID="stts">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="stts" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
            </Templates>
                <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter" />
		</obout:Grid>
            </div>
        </div>
    </div>
</div>

    <div class="modal fade" id="userModal" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="userModalLabel">
    <div class="modal-dialog" role="document">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">User Management | <small> <span id="spnAction" /></small></h4>
                </div>

                <div class="modal-body"> 
                    <input type="hidden" id="hidID" name="hidID" runat="server" />
                    <table class="table table-striped table-bordered" width="100%">                
                        <tr>
                            <td>
                                <b>User ID</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserID" name="txtUserID"  runat="server" CssClass="form-control" required="required" placeholder="User ID"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Name</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" name="txtUserName" runat="server" class="form-control" required="required" placeholder="Name"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>NIP</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNIP" name="txtNIP" class="form-control" placeholder="NIP" required="required" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Email</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" name="txtEmail"  class="form-control" required="required" placeholder="E-Mail" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Branch</b>
                            </td>
                            <td>
                                <select id="ddlBranch" runat="server" class="form-control" required="required" onchange="getRegion(this.value)"></select>                                
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                Region
                            </td>
                            <td>
                                <asp:Label ID="lblRegion" runat="server" name="lblRegion"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                Area
                            </td>
                            <td>
                                <asp:Label ID="lblArea" runat="server" name="lblArea"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Name Supervisor</b>
                            </td>
                            <td>
                                <select id="ddlSupervisor" name="ddlSupervisor" runat="server" class="form-control"></select>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                <b>Position</b>
                            </td>
                            <td>
                                <select id="ddlPosition" name="ddlPosition" runat="server" class="form-control" required="required"></select>
                            </td>
                        </tr>
               
                        <tr>
                            <td>
                                <b>Access Level</b>
                            </td>
                            <td>
                                <select id="ddlLevel" name="ddlLevel" runat="server" class="form-control" required="required"></select>
                            </td>
                        </tr>
                
                        <tr style="display:none;">
                            <td>
                                <b>Efective Date</b>
                            </td>
                            <td>
                                <input type="text" runat="server" readonly="readonly" required="required" name="txtEfectiveDate" id="txtEfectiveDate" class="form-control" style="width:230px" />
                            </td>
                        </tr>
                        <tr id="trStat" runat="server" bgcolor="#ebecec">
                            <td>
                                <b>Status</b>
                            </td>
                            <td>
                                <input type="checkbox" class="" id="chkStatus" name="chkStatus" runat="server" />
                            </td>
                        </tr>  
                        <tr >
                        <td>
                            <b>Position for Analysis Flow</b>
                        </td>
                        <td>
                        <% If exists = True Then%>
                        <%=fillListExisting() %>
                    
                        <%Else %>
                            <%=fillList() %> 
                        <% End If%>
                        </td>
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
            grdUser.filter();
        }
        /* -- */

       $(function () {
        $('#<%= txtEfectiveDate.ClientID %>').datepicker({
            calender_style: "picker_4",
            duration: '',
            yearRange: "-50:+0",
            dateFormat: 'dd-mm-yy',
            constrainInput: true,
            changeMonth: true,
            changeYear: true
        });
       });    
	</script>

    

   
</asp:Content>
