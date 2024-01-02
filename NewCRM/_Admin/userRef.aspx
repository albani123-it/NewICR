<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="userRef.aspx.vb" Inherits="NewCRM.userRef" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script>
         function search() {
             var val = document.getElementById("txtSearch");
             $.ajax({
                 type: "POST",
                 url: "userRef.aspx/getOrganizationChart",
                 data: '{userid:"' + val.value + '"}',
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
             var config;
             var columns = {};
             var c;

             if (obj != '') {
                 for (var i = 0; i < Object.keys(obj).length; i++) {
                     
                     if (i == 0) {
                         config = {
                             container: "#basic-example",

                             connectors: {
                                 type: 'step'
                             },
                             node: {
                                 HTMLclass: 'nodeExample1'
                             }
                         },
                         columns[obj[i].usr_user_id] = {
                             text: {
                                 name: obj[i].usr_name,
                                 title: obj[i].usr_access_level,
                                 contact: "Tel: 01 213 123 134",
                             },
                             image: "../img/IbaY.jpg"
                         }, ""
                         
                         var zz = [config];
                         zz.push(columns[obj[i].usr_user_id]);
                     } else {
                         columns[obj[i].usr_user_id] = {
                             parent: columns[obj[i].usr_supervisor_id],
                             stackChildren: true,
                             text: {
                                 name: obj[i].usr_name,
                                 title: obj[i].usr_access_level,
                                 contact: "Tel: 01 213 123 134",
                             },
                             image: "../img/IbaY.jpg"
                         }, ""
                         zz.push(columns[obj[i].usr_user_id]);
                     }      
              }
             }
             chart_config = zz;
             new Treant(chart_config);
             
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2>Organization Chart<small> User Reference</small></h2>              
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">
                <div class="row">
                    <div class="col-md-4">&nbsp;</div>
                    <div class="col-md-4">&nbsp;</div>
                    <div class="col-md-4" align="right">
                        <div class="input-group add-on">
                            <input class="form-control" placeholder="Search..." name="txtSearch" id="txtSearch" type="text" />
                            <div class="input-group-btn">
                            <button class="btn btn-info" type="button" onclick="search()"><i class="glyphicon glyphicon-search"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="chartOrganization col-md-12 col-sm-12 col-xs-12" id="basic-example"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="panel panel-default">
      <div class="panel-body">
            
      </div>
    </div>--%>
    
    <%--<script>
		var config = {
        container: "#basic-example",
        
        connectors: {
            type: 'step'
        },
        node: {
            HTMLclass: 'nodeExample1'
        }
    },
    crm_iz = {
        stackChildren: true,
        text: {
            name: "crm_iz",
            title: "Chief executive officer",
            contact: "Tel: 01 213 123 134",
        },
        image: "../img/IbaY.jpg"
    },

    crm_admin = {
        parent: crm_iz,
        text:{
            name: "crm_admin",
            title: "Chief Technology Officer",
        },
        stackChildren: true,
        image: "../img/IbaY.jpg"
    },
    d = {
        parent: crm_admin,
        text:{
            name: "d",
            title: "Chief Business Officer",
        },
        stackChildren: true,
        image: "../img/IbaY.jpg"
    },
    c = {
        parent: crm_admin,
        stackChildren: true,
        text:{
            name: "c",
            title: "Chief accounting officer",
            contact: "Tel: 01 213 123 134",
        },
        image: "../img/IbaY.jpg"
    },
    e = {
        parent: c,
        stackChildren: true,
        text:{
            name: "e",
            title: "Chief Information Security Officer"
        },
        image: "../img/IbaY.jpg"
    },
    

    chart_config = [
        config,
        crm_iz,
        crm_admin,
        d,
        c,
        e
    ];
    alert(chart_config);
    new Treant(chart_config);
	</script>--%>

    </div>
</asp:Content>
