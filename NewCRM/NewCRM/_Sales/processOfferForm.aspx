<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="processOfferForm.aspx.vb" Inherits="NewCRM.processOfferForm" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">        	
    function fLoad(){
        <asp:Literal id="liBodyLoadAction" runat="server" />
        }
</script>

<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2><span id="spnTitle" runat="server">Perumahan Rakyat (KPR)</span></h2>              
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">
                <input type="hidden" id="hidWadWacId" runat="server" />
                <input type="hidden" id="hidProcessId" runat="server" />                

                <%=html %>
                <%--<div class="ln_solid"></div>--%>
                <div class="col-md-12 col-sm-12 col-xs-12 form-group has-feedback">
                <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-6" align="right">
                    <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-default" UseSubmitBehavior="false" Text="Cancel" />
                    <asp:Button runat="server" ID="btnSaD" CssClass="btn btn-info" Text="Saved as Draft" />
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="Submit" />
                </div>
                </div>
                
                
            </div>
        </div>
    </div>
</div>

    <%--<script>
    $(function () {
        $('#wff_birth_date').datepicker({
            calender_style: "picker_4",
            duration: '',
            yearRange: "-50:+0",
            dateFormat: 'dd-mm-yy',
            constrainInput: true,
            changeMonth: true,
            changeYear: true
        });
    });

   
</script>--%>
</asp:Content>
