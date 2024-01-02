<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_rat_based.aspx.vb" Inherits="NewCRM.glo_rat_based" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/BTN/Scripts/jquery-1.9.1.js"></script>
    <script language="javascript" type="text/javascript">

        function OnBeforeInsert(record) {

            if (document.getElementById("ddlAssetClass").value == "") {
                alert("ddl Asset Class kosong");
                document.getElementById("ddlAssetClass").focus();
            }

            else if (document.getElementById("ddlAgency").value == "") {
                alert("ddl Agency kosong");
                document.getElementById("ddlAgency").focus();
            }

            else if (document.getElementById("ddlExternalRating").value == "") {
                alert("ddl External kosong");
                document.getElementById("ddlExternalRating").focus();
            }

            else if (document.getElementById("txtRiskWeight").value == "") {
                alert("Risk Weight kosong");
                document.getElementById("txtRiskWeight").focus();
            }

        }

        function OnBeforeDelete(record) {
            if (confirm("Are you sure you want to delete?") == false) {
                return false;
            }
            return true;
        }

        function loadExtRating(record) {
            var Agency = document.getElementById("ddlAgency").value;

            PageMethods.getExternalRating(Agency, Succ, Fail);
            PageMethods.pickExternalRating(record.glr_id, SuccLoad, FailLoad);
        }

        function SuccLoad(result) {
            $('#ddlExternalRating').val(result);
        }

        function FailLoad(result) {
        }

        function getval(data) {
            PageMethods.getExternalRating(data.value, Succ, Fail);
        }

        function Succ(result) {
            $('#ddlExternalRating').empty();
            $('#ddlExternalRating').append(result);
        }

        function Fail(result) {
        }


    </script>

</head>
<body>
   <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
   <div style="margin: 0 auto;padding: 0;width:98%;">				
		<table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
            <tr>
                <td background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
            </tr>
            <tr>
			    <td>
                    <obout:Grid ID="gridGloRatBased" runat="server" Width="100%" PageSize="15" Serialize="true" 
                        AutoGenerateColumns="False" EnableRecordHover="true" AllowSorting="False" CallbackMode="true"
                        AllowPageSizeSelection="False" AllowRecordSelection="False"
                        OnRebind="RebindGrid" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord"
                        FolderStyle="~/styles/btn_bank">
                    <ClientSideEvents OnBeforeClientInsert="OnBeforeInsert" OnBeforeClientDelete="OnBeforeDelete" OnClientEdit="loadExtRating" />
                       <Columns>
                            <obout:Column ID="Column1" DataField="glr_id" ReadOnly="true" Visible="false" runat="server" />

				            <obout:Column ID="Column2" DataField="glr_asset_class" HeaderText="Asset Class" runat="server" Width="20%">  
                                <TemplateSettings EditTemplateId="tplEditAssetClass" TemplateId="tplAssetClass" />
                            </obout:Column>

                            <obout:Column ID="Column3" DataField="glr_agency" HeaderText="Rating Agency" runat="server" Width="20%">                                                        
                                <TemplateSettings EditTemplateId="tplEditAgency" />
                            </obout:Column>

                            <obout:Column ID="Column4" DataField="glr_external_rating" HeaderText="External Rating" runat="server" Width="20%">
                                 <TemplateSettings EditTemplateId="tplEditExternalRating" />
                            </obout:Column> 
                                                                                   
                            <obout:Column ID="Column5" DataField="glr_rwa" HeaderText="Risk Weight" runat="server" Width="20%">
                                <TemplateSettings EditTemplateId="tplEditRiskWeight" />
                            </obout:Column>                             
                            <obout:Column ID="Column6" HeaderText="Action" Width="150" AllowEdit="true" AllowDelete="true" runat="server" />
			            </Columns>	

                        <Templates>
                    
                            <obout:GridTemplate ID="tplAssetClass" runat="server">
				                <Template>
                                    <%# Container.DataItem("glr_asset_class")%>
				                </Template>
				            </obout:GridTemplate>

                            <obout:GridTemplate runat="server" ID="tplEditAssetClass" ControlID="ddlAssetClass">
                                <Template>
                                    <select class="inpDdl" id="ddlAssetClass" name="cboAssetClass">
                                        <option value="Korporasi" selected="selected">Korporasi</option>
                                        <option value="MDB Tertentu" selected="selected">MDB Tertentu</option>
                                        <option value="MDB" selected="selected">MDB</option>
                                        <option value="Pemerintah" selected="selected">Pemerintah</option>
                                        <option value="Bank (Jangka Pendek)" selected="selected">Bank (Jangka Pendek)</option>
                                        <option value="Bank" selected="selected">Bank</option>
                                        <option value="PSE" selected="selected">PSE</option>
                                        <option value="Pemerintah Negara Lain" selected="selected">Pemerintah Negara Lain</option>
                                       <%-- <%= fillAssetClass%>--%>
                                    </select>
                                </Template>
                            </obout:GridTemplate>


                            <obout:GridTemplate runat="server" ID="tplAgency" >
				                <Template>
				                    <%# Container.DataItem("glr_agency")%>			      		       
				                </Template>
				            </obout:GridTemplate>
				
				            <obout:GridTemplate runat="server" ID="tplEditAgency" ControlID="ddlAgency" ControlPropertyName="value">
				                <Template>
                                    <select class="inpDdl" id="ddlAgency" onchange="getval(this)" name="ddlAgency">
                                        <%= fillRatingAgency%>
                                    </select>
				                </Template>
				            </obout:GridTemplate>

                            <obout:GridTemplate runat="server" ID="tplEditExternalRating" ControlID="ddlExternalRating">
                                <Template>
                                    <select class="inpDdl" id="ddlExternalRating" name="ddlExternalRating">
                                        <%= fillExternalRating%>
                                    </select>
                                </Template>
                            </obout:GridTemplate>

                            <obout:GridTemplate runat="server" ID="tplEditRiskWeight" ControlID="txtRiskWeight">
                                <Template>
                                    <input type="text" id="txtRiskWeight" name="txtRiskWeight" class="inpTxt" />
                                </Template>
                            </obout:GridTemplate>
                        </Templates>
                    </obout:Grid>		                
                </td>
		    </tr>
		</table>  
        
    </div>                            
    </form>

</body>
</html>
