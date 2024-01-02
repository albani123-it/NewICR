<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="wsUsers.aspx.vb" Inherits="NewCRM.wsUsers" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
     /** **/
  
     function OnBeforeEdit(record) { 
         $('#userModal').modal('toggle');
         document.getElementById("spnAction").innerHTML = "Edit";
         getEditUser(record.uws_userid);
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
                 getDeleteUser(record.uws_userid);
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
             url: "wsUsers.aspx/getDeleteWUser",
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
            document.getElementById("<%=txtDesc.ClientID %>").value = "";
            document.getElementById("<%=chkActived.ClientID %>").checked = true;
            document.getElementById("<%=chkPrivilage.ClientID %>").checked = true;
        }
     
        function getEditUser(val) {
        $.ajax({
            type: "POST",
            url: "wsUsers.aspx/getDetailUser",
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
            document.getElementById("<%=hidID.ClientID %>").value = obj[0].uws_userid;
            document.getElementById("<%=txtUserID.ClientID %>").value = obj[0].uws_userid;
            document.getElementById("<%=txtUserName.ClientID %>").value = obj[0].uws_name;
            document.getElementById("<%=txtDesc.ClientID %>").value = obj[0].uws_description;
            document.getElementById("<%=chkActived.ClientID %>").checked = true;
            document.getElementById("<%=chkPrivilage.ClientID %>").checked = true;
        }
        
        
    }

     //function parseJsonDate(jsonDateString){
     //    var value = new Date
     //       (
     //            parseInt(jsonDateString.replace(/(^.*\()|([+-].*$)/g, ''))
     //       );

     //    var dd = value.getDate();
     //    var mm = value.getMonth()+1;
     //    var yyyy = value.getFullYear();

     //    if(dd<10){
     //        dd='0'+dd;
     //    } 
     //    if(mm<10){
     //        mm='0'+mm;
     //    } 
     //    var tgl = dd+'-'+mm+'-'+yyyy;
         

     //    return tgl;
     //}
    function fLoad() {
        <asp:Literal id="liBodyLoadAction" runat="server" />

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
            <h2>WS Users</h2>              
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">
            <obout:Grid id="grdWsUser" runat="server" CallbackMode="true" Serialize="true" pagesize="15"
			AutoGenerateColumns="false" EnableRecordHover="true" Width="100%"
            AllowPageSizeSelection="false" AllowFiltering="true" FolderStyle="~\Content\Themes1\Grid">	
            <ClientSideEvents OnBeforeClientAdd="OnBeforeAdd" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientDelete="OnBeforeDelete" />		
            <Columns>
                <obout:Column ID="Column1" DataField="uws_userid" HeaderText="User ID" runat="server" ShowFilterCriterias="false" Width="15%">				   
				    <TemplateSettings RowEditTemplateControlId="txtusrID" RowEditTemplateControlPropertyName="value" FilterTemplateId="UseridFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>
                </obout:Column>
                <obout:Column ID="Column2" DataField="uws_name" HeaderText="User Name" runat="server" ShowFilterCriterias="false" Width="15%">
                    <TemplateSettings RowEditTemplateControlId="txtNM" RowEditTemplateControlPropertyName="value" FilterTemplateId="NameFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>               
                </obout:Column>
                <obout:Column ID="Column3" DataField="uws_description" HeaderText="Description" runat="server" ShowFilterCriterias="false" Width="20%">
                    <TemplateSettings RowEditTemplateControlId="txtDesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="DescFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>             
                </obout:Column>
                <obout:Column ID="Column4" DataField="uws_status" HeaderText="Status" runat="server" ShowFilterCriterias="false" Width="10%">
                    <TemplateSettings RowEditTemplateControlId="txtST" RowEditTemplateControlPropertyName="value" FilterTemplateId="StatusFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>                          
                </obout:Column>
                <obout:Column  ID="Column5" DataField="uws_authkey" HeaderText="Autentication" runat="server" ShowFilterCriterias="false" Width="15%">
                    <TemplateSettings RowEditTemplateControlId="txtAuten" RowEditTemplateControlPropertyName="value" FilterTemplateId="AutenFilter"/>
                    <FilterOptions>
                        <obout:FilterOption IsDefault="true" Type="Contains" />
                    </FilterOptions>                                         
                </obout:Column>
                <obout:Column ID="Column6" DataField="lws_userlevel" HeaderText="Access Level" runat="server" ShowFilterCriterias="false" Width="10%">
                    <TemplateSettings RowEditTemplateControlId="txtAL" RowEditTemplateControlPropertyName="value" FilterTemplateId="LvlFilter"/>
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
                 <obout:GridTemplate runat="server" ID="DescFilter" ControlID="desc">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="desc" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="StatusFilter" ControlID="sts">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="sts" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="AutenFilter" ControlID="auten">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="auten" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
                <obout:GridTemplate runat="server" ID="LvlFilter" ControlID="lvl">
                    <Template>
                        <obout:OboutTextBox runat="server" ID="lvl" Width="100%">
                            <ClientSideEvents OnKeyUp="applyFilter" />
                        </obout:OboutTextBox>
                    </Template>
                </obout:GridTemplate>
            </Templates>
            <TemplateSettings FilterRemoveButton_TemplateId="tplRemoveFilter"/>
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
                        <b>Description</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesc" name="txtDesc" TextMode="multiline" class="form-control" placeholder="Description" required="required" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="tractive" runat="server" bgcolor="#ebecec">
                    <td>
                        <b>Activated</b>
                    </td>
                    <td>
                        <input type="checkbox" class="" id="chkActived" name="chkActived" checked="checked" runat="server" />
                    </td>
                </tr>                
                <tr id="trprive" runat="server" bgcolor="#ebecec">
                    <td>
                        <b>Privilage</b>
                    </td>
                    <td>
                        <input type="checkbox" class="" id="chkPrivilage" name="chkPrivilage" checked="checked" runat="server" />&nbsp;Tasklist
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
            grdWsUser.filter();
        }
        /* -- */   
	</script>


</asp:Content>