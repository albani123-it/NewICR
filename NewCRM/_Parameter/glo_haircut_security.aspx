<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_haircut_security.aspx.vb" Inherits="NewCRM.glo_haircut_security" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="/BTN/Scripts/jquery-1.9.1.js"></script>
       <script language="javascript" type="text/javascript">
           function OnBeforeDelete(record) {
               if (confirm("Are you sure you want to delete?") == false) {
                   return false;
               }
               return true;
           }

           function loadExtRating() {
               var Agency = document.getElementById("ddlAgency").value;
               PageMethods.getExternalRating(Agency, Succ, Fail);
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

           function isNumberKeyDueDate(evt) {
               var charCode = (evt.which) ? evt.which : event.keyCode;
               if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                   return false;
               return true;
           }

           function isNumberKey(evt) {
               var charCode = (evt.which) ? evt.which : event.keyCode;
               if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                   return false;
               return true;
           }
       </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="True" />
        <div style="margin: 0 auto;clear:both;width:98%;">
            <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0;margin-top: 5px;">
                    <tr>
                        <td background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;height:auto;">&nbsp;</td>
                    </tr>
            </table>
		    <table cellSpacing="0" cellPadding="0" width="100%" style="border: thin solid #ccc;">
				<tr>
					<td>
                        <obout:Grid ID="gridGloHaircutSecurity" runat="server" Width="100%" Serialize="true"
                        AutoGenerateColumns="False" EnableRecordHover="true" AllowPageSizeSelection="False"
                         AllowRecordSelection="False" AllowSorting="False" CallbackMode="true"
                         OnRebind="RebindGrid" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord"
                        FolderStyle="~/styles/btn_bank">
                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientEdit="loadExtRating" />
                       <Columns>
                            <obout:Column ID="Column1" DataField="glh_id" Visible="false" runat="server" />

				            <obout:Column ID="Column2" DataField="glh_agency" HeaderText="Rating Agency" runat="server" Width="20%">
                                <TemplateSettings EditTemplateId="tplEditRatingAgency" />
                            </obout:Column>

                            <obout:Column ID="Column3" DataField="glh_external_rating" HeaderText="Peringkat Efek/Surat Berharga" runat="server" Wrap="true" Width="20%">
                                <TemplateSettings EditTemplateId="tplEditExternalRating" />
                            </obout:Column>

                            <obout:Column ID="Column4" DataField="glh_due_date" HeaderText="Sisa Jatuh Tempo" runat="server" Width="20%">
                                <TemplateSettings EditTemplateId="tplEditDueDate" />
                            </obout:Column> 
                                                                                   
                            <obout:Column ID="Column5" DataField="glh_bank_provider" HeaderText="Diterbitkan Pemerintah dan Bank" runat="server" Wrap="true" Width="20%">
                                <TemplateSettings EditTemplateId="tplEditBankProvider" />
                            </obout:Column>
                            
                            <obout:Column ID="Column6" DataField="glh_other_provider" HeaderText="Diterbitkan Oleh Pihak Lainnya" runat="server" Wrap="true" Width="20%">
                                <TemplateSettings EditTemplateId="tplEditOtherProvider" />
                            </obout:Column>
                                                         
                            <obout:Column ID="Column7" HeaderText="Action" Width="150" AllowEdit="true" AllowDelete="true" runat="server" />
			    </Columns>  
                
                <Templates>
                     <obout:GridTemplate runat="server" ID="tplEditRatingAgency" ControlID="ddlRatingAgency">
                        <Template>
                            <select class="inpDdl" id="ddlRatingAgency" onchange="getval(this)" name="ddlRatingAgency">
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

                 <obout:GridTemplate runat="server" ID="tplEditDueDate" ControlID="txtDueDate">
                        <Template>
                            <input type="text" id="txtDueDate" name="txtDueDate" class="inpTxt" onkeypress="return isNumberKeyDueDate(event)"/>
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditBankProvider" ControlID="txtBankProvider">
                        <Template>
                            <input type="text" id="txtBankProvider" name="txtBankProvider" class="inpTxt" onkeypress="return isNumberKey(event)"/>
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditOtherProvider" ControlID="txtOtherProvider">
                        <Template>
                            <input type="text" id="txtOtherProvider" name="txtOtherProvider" class="inpTxt" onkeypress="return isNumberKey(event)"/>
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
