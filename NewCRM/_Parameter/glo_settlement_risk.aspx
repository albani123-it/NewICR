<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_settlement_risk.aspx.vb" Inherits="NewCRM.glo_settlement_risk" %>
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
          <obout:Grid ID="gridGloSettlementRisk" runat="server" Width="100%" PageSize="15" Serialize="true" CallbackMode="true"
                        AutoGenerateColumns="False" EnableRecordHover="true" AllowSorting="False"
                        AllowPageSizeSelection="False" AllowRecordSelection="False"
                        OnRebind="RebindGrid" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord"
                        FolderStyle="~/styles/btn_bank">
                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                       <Columns>
                            <obout:Column ID="Column1" DataField="glsr_id" ReadOnly="true" Visible="false" runat="server" />

				            <obout:Column ID="Column2" DataField="glsr_settlement_date" HeaderText="Settlement Date (Upper Limit) in Days" runat="server" Width="50%" >
                                <TemplateSettings EditTemplateId="tplEditSettlementDate" /> 
                            </obout:Column>

                            <obout:Column ID="Column3" DataField="glsr_percentage" HeaderText="Percentage" runat="server" Width="40%">  
                                <TemplateSettings EditTemplateId="tplEditPercentage" />
                            </obout:Column>

                            <obout:Column ID="Column6" HeaderText="Action" Width="150" AllowEdit="true" AllowDelete="true" runat="server" />
			    </Columns>	    
		
                <Templates>
                    <obout:GridTemplate runat="server" ID="tplEditSettlementDate" ControlID="txtSettlementDate">
                        <Template>
                            <input type="text" id="txtSettlementDate" name="txtSettlementDate" class="inpTxt" />
                        </Template>
                    </obout:GridTemplate>

                    <obout:GridTemplate runat="server" ID="tplEditPercentage" ControlID="txtPercentage">
                        <Template>
                            <input type="text" id="txtPercentage" name="txtPercentage" class="inpTxt" />
                        </Template>
                    </obout:GridTemplate>
                </Templates>
		    </obout:Grid>		                
    </div>
    </form>
</body>
</html>
