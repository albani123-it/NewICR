<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_pre_screening.aspx.vb" Inherits="NewCRM.glo_pre_screening" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function OnBeforeInsert(record) {
            var msg = "";
            if (document.getElementById("ddlAssetClass").value == "") {
                msg += "Asset Class is empty.\n";
                document.getElementById("ddlAssetClass").focus();
            }

            else if (document.getElementById("ddlAgency").value == "") {
                msg += "Agency is empty.\n";
                document.getElementById("ddlAgency").focus();
            }

            else if (document.getElementById("ddlExternalRating").value == "") {
                msg += "External Rating is empty.\n";
                document.getElementById("ddlExternalRating").focus();
            }

            else if (document.getElementById("txtRiskWeight").value == "") {
                msg += "Risk Weight is empty.\n";
                document.getElementById("txtRiskWeight").focus();
            }
            alert(msg);
        }

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
            <table cellSpacing="0" cellPadding="0" width="100%" style="border: thin solid #ccc;">
                <tr>
					<td>
                        <obout:Grid ID="grid1" runat="server" Width="100%" PageSize="15" Serialize="true" 
                            AutoGenerateColumns="False" EnableRecordHover="true" AllowSorting="False" CallbackMode="true"
                            AllowPageSizeSelection="False" AllowRecordSelection="False" AllowAddingRecords="false" AllowPaging="false" 
                            OnRebind="RebindGrid" OnUpdateCommand="UpdateRecord" 
                            FolderStyle="~/styles/grid">
                            <Columns>
                                <obout:Column ID="Column1" DataField="ID" ReadOnly="true" Visible="false" runat="server" />
                                <obout:Column ID="Column2" DataField="brw_type" HeaderText="Borrower Type" runat="server" Width="30%" readonly="true"></obout:Column>
                                <obout:Column ID="Column3" DataField="min_score" HeaderText="Minimal Score (%)" runat="server" Width="50%">                                                        
                                    <TemplateSettings EditTemplateId="tplEditMinScore" />
                                </obout:Column>
                                <obout:Column ID="Column4" HeaderText="Action" Width="150" AllowEdit="true" AllowDelete="false" runat="server" />
                            </Columns>
                            <Templates>
                                <obout:GridTemplate runat="server" ID="tplEditMinScore" ControlID="txtMinScore">
                                    <Template>
                                        <input type="text" id="txtMinScore" name="txtMinScore" class="inpTxt" />
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
