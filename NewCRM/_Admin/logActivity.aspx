<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="logActivity.aspx.vb" Inherits="NewCRM.logActivity" %>
<%@ Register assembly="obout_Grid_NET" namespace="Obout.Grid" tagprefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
               
    function fLoad(){
		<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
	}
    fLoad();

    function formatDate(date) {
        var monthNames = [
          "January", "February", "March",
          "April", "May", "June", "July",
          "August", "September", "October",
          "November", "December"
        ];

        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();

        return day + ' ' + monthNames[monthIndex] + ' ' + year;
    }

    function logDetail(userid,dateIn) {
        var tgl = new Date(dateIn);
        var yyyy = tgl.getFullYear();
        var mm = tgl.getMonth()+1;
        var dd = tgl.getDate();
        if (mm.toString().length < 2) {
            mm = '0' + mm;
        }
        if (dd.toString().length < 2) {
            dd = '0' + dd;
        }
        var fulldate = dd + '-' + mm + '-' + yyyy;
        
        document.getElementById("spnAction").innerHTML = userid + ' - ' + formatDate(tgl); 
        
        $('#logDetailModal').modal('toggle');
        detailActivityLog(userid,fulldate);
            
    }

    function detailActivityLog(userid,actDate) {
        $.ajax({
            type: "POST",
            url: "logActivity.aspx/getDetailActivityLog",
            data: "{userid:'"+ userid +"',datelog:'"+ actDate +"'}",
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

        var trHTML = '';
        var det = '';
        $('#tblDetailActivityLog').find('tbody').remove();
        $.each(obj, function (i, item) {
            trHTML += '<tr data-toggle="collapse" data-target="#text-1" class="accordion-toggle collapsed"><td style="width: 5%;">' + item.log_time + '</td><td style="width:30%;">Browser : ' + item.log_browser + '<br />Client IP : ' + item.client_ip + '</td>';
            trHTML +='<td style="width: 30%;"><p style="width: 70%;word-wrap: break-word;">' + item.log_activity + '</p></td><td style="width: 15%;">' + item.log_page_url + '</td>';
            trHTML +='<td style="width: 15%;">' + item.log_query_string + '</td>';
            trHTML +='<td style="width: 5%;">'
            if (item.log_code != '-') {
                trHTML +="<a data-toggle='collapse' href='#collapse_"+ item.log_id +"' aria-expanded='false' aria-controls='collapseExample' onClick='return test("+ item.log_id + ")'> view</a>";
            }            
            trHTML +="</td></tr>";

            //if (item.log_code != '-') {
                trHTML +='<tr>';
                trHTML +='<td colspan="6" class="hiddenRow">';
                trHTML +='<div class="accordian-body collapse clearfix" id="collapse_'+ item.log_id +'">';

                if (item.log_code != '-') {                   
                    trHTML += '<table style="width:98%" id="tbl_'+ item.log_id + '"></table>';
                }
                
                trHTML +='</div>'
                trHTML +='</td>'
                trHTML +='</tr>'
            //}
        });
        $('#tblDetailActivityLog').append(trHTML);
    }    

    function test(oVal) {
        $.ajax({
            type: "POST",
            url: "logActivity.aspx/getViewDetailActivityLog",
            data: "{logid:'"+ oVal +"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessView, 
            failure: function (response) {
                alert(response.d);
            }
        });        
    }
   
    function OnSuccessView(response) {

        var obj = jQuery.parseJSON(response.d);
        var html = "";
        
        $('#tbl_'+ obj[0].log_id).find('tbody').remove();
        if (obj[0].code == 'ua') {
            html += "<tr><td>Field Name</td><td>User "+ obj[0].log_action_mode +" Detail</td></tr>";
            html += "<tr><td>User ID</td><td>"+ obj[0].usr_userid +"</td></tr>";
            html += "<tr><td>Name</td><td>"+ obj[0].usr_name +"</td></tr>";
            html += "<tr><td>NIP</td><td>"+ obj[0].usr_nip +"</td></tr>";
            html += "<tr><td>Access Level</td><td>"+ obj[0].usr_access_level +"</td></tr>";
            html += "<tr><td>Lock Status</td><td>"+ obj[0].usr_status_name +"</td></tr>";
            html += "<tr><td>Operating System</td><td>"+ obj[0].log_operating_system +"</td></tr>";
        } else if (obj[0].code == 'users') {
            html += "<tr><td>Field Name</td><td>Before</td><td>After</td></tr>";
            html += "<tr><td>User ID</td><td>"+ obj[0].usr_userid +"</td><td>"+ obj[1].usr_userid +"</td></tr>";
            html += "<tr><td>Name</td><td>"+ obj[0].usr_name +"</td><td>"+ obj[1].usr_name +"</td></tr>";
            html += "<tr><td>NIP</td><td>"+ obj[0].usr_nip +"</td><td>"+ obj[1].usr_nip +"</td></tr>";
            html += "<tr><td>Access Level</td><td>"+ obj[0].usr_access_level +"</td><td>"+ obj[1].usr_access_level +"</td></tr>";
            html += "<tr><td>Lock Status</td><td>"+ obj[0].usr_status_name +"</td><td>"+ obj[1].usr_status_name +"</td></tr>";
        }
        $('#tbl_'+ obj[0].log_id).append(html);
              

        
    }
    
</script>
<style>
    .hiddenRow {
   padding: 0 !important;
}
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    
    <div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2>Activity Log <small>&nbsp;</small></h2>              
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">
                <div class="row">
                    <div class="well" style="overflow: auto">
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                        <input type="text" class="form-control has-feedback-left col-md-3 col-sm-3 col-xs-6" id="txtStartDate" placeholder="Start Date" runat="server" />
                        <i class="fa fa-calendar form-control-feedback left" aria-hidden="true"></i>                
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                        <input type="text" class="form-control has-feedback-left col-md-3 col-sm-3 col-xs-6" id="txtEndDate" placeholder="End Date" runat="server" />
                        <i class="fa fa-calendar form-control-feedback left" aria-hidden="true"></i>                                  
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6 form-group has-feedback">
                            <button type="submit" class="btn btn-info" runat="server" id="btnSearch">
                              <span class="glyphicon glyphicon-search"></span> Search
                            </button>                
                        </div>
                    </div>
                </div>
                <div class="row">
                    <obout:Grid id="grdListActivityLog" Width="100%" runat="server" CallbackMode="true" serialize="true" 
		            AutoGenerateColumns="false" EnableRecordHover="true" 
                    AllowPageSizeSelection="false" AllowFiltering="false" AllowRecordSelection="false"
                    AllowAddingRecords="false" AllowColumnResizing="false" AllowPaging="true" AllowSorting="false" 
                    FolderStyle="~\Content\Themes1\Grid">
                        <Columns>
                            <obout:Column ID="Column3" DataField="log_userid" Visible="true" HeaderText="User ID" runat="server" Wrap="true" />
                            <obout:Column ID="Column2" DataField="log_date" Visible="true" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Activity Date" runat="server" />
                            <obout:Column ID="Column4" DataField="log_start" Visible="true" HeaderText="Start Activity" runat="server" Wrap="true" />
                            <obout:Column ID="Column7" HeaderText="Action" runat="server" Width="16%">
                                <TemplateSettings  TemplateID="viewTemplate" />
                            </obout:Column>
                        </Columns>
                        <Templates>
                            <obout:GridTemplate runat="server" ID="viewTemplate">
                                    <Template>
                                        <a href="javascript:logDetail('<%# Container.DataItem("log_userid") %>','<%# Container.DataItem("log_date") %>');">View Log Activity</a>
                                    </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>
                </div>
            </div>
        </div>
    </div>
    </div>


    


    <div class="modal fade bs-example-modal-lg" id="logDetailModal" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="logDetailModalLabel">
    <div class="modal-dialog  modal-lg" role="document">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Log Activity Detail | <small> <span id="spnAction" /></small></h4>
                </div>

                <div class="modal-body">
                    <div class="container">
                    <table id="tblDetailActivityLog" class="table table-striped" style="width:100%;font-size:12px;">
                        <thead>
                            <tr>
                            <th style="width:5%">Time</th>
                            <th style="width:20%">User Agent</th>
                            <th style="width:35%">Activity</th>
                            <th style="width:15%">Page Url</th>
                            <th style="width:20%">String</th>
                            <th style="width:5%">Action</th>
                            </tr>
                        </thead>
        
                    </table>
                    </div>
                </div>

          </div>
    </div>
</div>

    <script>
    $(function () {
        $('#<%= txtStartDate.ClientID %>').datepicker({
            calender_style: "picker_4",
            duration: '',
            yearRange: "-5:+0",
            dateFormat: 'dd-mm-yy',
            constrainInput: true,
            changeMonth: true,
            changeYear: true
        });
         $('#<%= txtEndDate.ClientID %>').datepicker({
            calender_style: "picker_4",
            duration: '',
            yearRange: "-5:+0",
            dateFormat: 'dd-mm-yy',
            constrainInput: true,
            changeMonth: true,
            changeYear: true
        });
    });

        

</script>

</asp:Content>
