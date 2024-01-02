<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_counter_party_CR.aspx.vb" Inherits="NewCRM.glo_counter_party_CR" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function OnBeforeDelete(record) {
            if (confirm("Are you sure you want to delete?") == false) {
                return false;
            }
            return true;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto;clear:both;width:98%;">
        <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0;margin-top: 5px;">
            <tr>
                <td background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;height:auto;">&nbsp;</td>
            </tr>
        </table>
         <obout:Grid ID="gridGloCounterPartyCreditRisk" runat="server" Width="100%" 
                    AutoGenerateColumns="False" EnableRecordHover="true" AllowSorting="False" 
                    AllowPageSizeSelection="False" AllowRecordSelection="False" AllowAddingRecords="true"  
                    OnRebind="RebindGrid" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord"
                    FolderStyle="~/styles/btn_bank">
                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                    <Columns>
                        <obout:Column ID="Column1" DataField="gccr_id" ReadOnly="true" Visible="false" runat="server" />

			            <obout:Column ID="Column2" DataField="gccr_residual_maturity" HeaderText="Residual Maturity (Upper Limit) in Months" runat="server" Width="20%" Wrap="true" >  
                            <TemplateSettings EditTemplateId="tplEditResidualMaturity" />
                        </obout:Column>

                        <obout:Column ID="Column3" DataField="gccr_interest_rate" HeaderText="Interest Rate (%)" runat="server" Width="15%">
                            <TemplateSettings EditTemplateId="tplEditInterestRate" /> 
                        </obout:Column>

                        <obout:Column ID="Column4" DataField="gccr_exchange_rate_and_gold" HeaderText="Exchange Rate and Gold (%)" runat="server" Width="15%" Wrap="true">
                            <TemplateSettings EditTemplateId="tplEditExchangeRateAndGold" /> 
                        </obout:Column> 
                                                                                   
                        <obout:Column ID="Column5" DataField="gccr_stock" HeaderText="Stock (%)" runat="server" Width="15%">
                            <TemplateSettings EditTemplateId="tplEditStock" /> 
                        </obout:Column> 
                        
                        <obout:Column ID="Column6" DataField="gccr_metal_beside_gold" HeaderText="Metal Beside Gold (%)" runat="server" Width="16%">
                            <TemplateSettings EditTemplateId="tplEditMetalBesideGold" /> 
                        </obout:Column>                             
                            
                        <obout:Column ID="Column8" DataField="gccr_other" HeaderText="Other (%)" runat="server" Width="15%">
                            <TemplateSettings EditTemplateId="tplEditOther" /> 
                        </obout:Column> 

                        <obout:Column ID="Column7" HeaderText="Action" Width="150" AllowEdit="true" AllowDelete="true" runat="server" />
            </Columns>	    
		      
              <Templates>
                    <obout:GridTemplate runat="server" ID="tplEditResidualMaturity" ControlID="txtResidualMaturity">
                        <Template>
                            <input type="text" id="txtResidualMaturity" name="txtResidualMaturity" class="inpTxt" size="15"/>
                        </Template>
                    </obout:GridTemplate>
                    
                    <obout:GridTemplate runat="server" ID="tplEditInterestRate" ControlID="txtInterestRate">
                        <Template>
                            <input type="text" id="txtInterestRate" name="txtInterestRate" class="inpTxt" size="15"/>
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditExchangeRateAndGold" ControlID="txtExchangeRateAndGold">
                        <Template>
                            <input type="text" id="txtExchangeRateAndGold" name="txtExchangeRateAndGold" class="inpTxt" size="15" />
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditStock" ControlID="txtStock">
                        <Template>
                            <input type="text" id="txtStock" name="txtStock" class="inpTxt" size="15" />
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditMetalBesideGold" ControlID="txtMetalBesideGold">
                        <Template>
                            <input type="text" id="txtMetalBesideGold" name="txtMetalBesideGold" class="inpTxt" size="15" />
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditOther" ControlID="txtOther">
                        <Template>
                            <input type="text" id="txtOther" name="txtOther" class="inpTxt" size="15" />
                        </Template>
                    </obout:GridTemplate>
              </Templates>
            </obout:Grid>	
    </div>
    </form>
</body>
</html>
