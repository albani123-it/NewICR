<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="processOffer.aspx.vb" Inherits="NewCRM.processOffer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        	
    function fLoad () {
    <asp:Literal id="liBodyLoadAction" runat="server" />      
            
    }

        function onSave(wpmID,counter,wfId) {
            var strArr = '';
            var hidField = document.getElementById("<%=hidFieldName.ClientID %>").value;
            var res = hidField.split(",");
            var res2 = '';
            var iz = '';
            var iz2 = '';
            var age = '';
            for ( var i = 0; i < res.length; i++) {
                res2 = res[i].split("|");

                if ((res2[1] == 'text') || (res2[1] == 'textarea')) {
                    iz = document.getElementById(res2[0]).value;
                }  else if (res2[1] == 'date') {
                    age = getAge(document.getElementById(res2[0]).value);
                    iz = age + '_' + document.getElementById(res2[0]).value;
                }


                if (i == 0) {
                    strArr += '' + res2[0] + ':'+ iz +'';
                } else {
                    strArr += ',' + res2[0] + ':'+ iz +'';
                }  

                
                if (i == 0) {
                    strArr += '' + res2[0] + ':'+ iz +'';
                } else {
                    strArr += '|' + res2[0] + ':'+ iz +'';
                }  
                             
                
            }
            goToNextWorkflow(wpmID,counter,wfId,strArr)
        }
        
    function goToNextWorkflow(wpmID,counter,wfId,field) {
        $.ajax({
            type: "POST",
            url: "processOffer.aspx/getWorkflow",
            data: '{wpmID:"'+ wpmID +'",counter:"'+ counter +'",wfId:"'+ wfId +'",field:"'+ field +'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessSave, 
            failure: function (response) {
                alert(response.d);
            }
        });
    }
        function OnSuccessSave(response) {
            //var obj = jQuery.parseJSON(response.d);
            alert(response.d);
        }

        function getAge(dateString) {
            var chg = dateString.split("-");


            var today = new Date();
            var birthDate = new Date(chg[2], chg[1] - 1, chg[0]);
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }
            return age;
        }
    </script>
         
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2>Proccess Offer<small> &nbsp;</small></h2>              
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">
                <div class="row">                     
                    <div class="col-md-2">
                        <label class="control-label col-md-12 col-sm-12 col-xs-12">Proccess Type</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlType" name="ddlType" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                            <asp:ListItem Value="-" Text="- Choose Type -"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">&nbsp;</div>
                </div>
                <div class="row">                     
                    <div class="col-md-2">
                        <label id="lblOffer" runat="server" visible="false" class="control-label col-md-12 col-sm-12 col-xs-12">Please Select One</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlOffer" name="ddlOffer" runat="server" class="form-control" Visible="false">                            
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">&nbsp;</div>
                </div>
                <br />
                <div class="row">                     
                    <div class="col-md-2">&nbsp;</div>
                    <div class="col-md-4">&nbsp;</div>
                    <div class="col-md-6"><asp:Button runat="server" ID="btnSubmitOffer" Visible="false" CssClass="btn btn-primary" Text="Proccess" /></div>
                </div>                
                <div class="row">
                        <%=html %>
                        <asp:HiddenField runat="server" ID="hidFieldName" />
                </div>
                
            </div>
        </div>
    </div>
</div>


</asp:Content>
