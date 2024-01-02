<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_non_rat_based_det.aspx.vb" Inherits="NewCRM.glo_non_rat_based_det" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
            function OnBeforeDelete(record) {
                if (confirm("Are you sure you want to delete?") == false) {
                    return false;
                }
                return true;
            }

            function fLoad(){
			    <asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		    }
		    
		    function vText(mode) {
		        var grid = document.getElementById("grd");
		        if (mode == "hide") {
		            grid.style.display="none";
		            document.getElementById("bbt").style.display="table-row";
                    document.getElementById("bbt").style.color="rgb(0, 0, 0)";
                    document.getElementById("bbt").style.backgroundImage="none";
                    document.getElementById("bbt").style.backgroundColor="rgb(246, 245, 240)";
		        } else {
		            grid.style.display="table-row";
		            document.getElementById("bbt").style.display="none";
		        }
		    }   

		    function CloseMe() {		
			window.returnValue = window.dialogArguments;
			event.returnValue = false;
			window.close();
		}	
		</script>
</head>
<body onload="fLoad()">
    <form id="form1" runat="server">
    <center>
    <div>
        <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-top: 5px;">
			<tr>
				<td colspan="2" background="../Images/bg-head.gif" bgcolor="#C9961A" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
			</tr>
			<tr style="color: rgb(0, 0, 0); background-color: rgb(246, 245, 240);">
				<td width="30%"><b>Jenis Tagihan</b></td>
				<td>
					<asp:TextBox runat="server" ID="txtJenisTagihan" CssClass="inpTxt" TextMode="MultiLine" Width="300px" Height="50px"></asp:TextBox>
					<asp:HiddenField runat="server" ID="mode" />  
					<asp:HiddenField runat="server" ID="hidGNRID" />    
				</td>
			</tr>
			<tr id="typ" runat="server" style="color: rgb(0, 0, 0); background-image: none; background-color: rgb(239, 237, 226);">
				<td><b>Parameter Type</b></td>
				<td>
					<asp:RadioButton runat="server" ID="rdoSingle" Checked="true" onclick = "vText('hide')" GroupName="TypeParameter" Text="Single" />
					<asp:RadioButton runat="server" ID="rdoMultiple" GroupName="TypeParameter"  onclick = "vText('show')" Text="Multiple" />
				</td>
			</tr>
			<tr id="bbt" runat="server" style="color: rgb(0, 0, 0); background-color: rgb(246, 245, 240);">            
				<td><b>Risk Weight</b></td>
				<td>
					<asp:TextBox runat="server" ID="txtBobotResiko" CssClass="inpTxt"></asp:TextBox>
				</td>
			</tr>
					    
			<tr id="grd" runat="server" bgcolor="#ababab">
				<td colspan="2">	
                    <obout:Grid ID="gridGlobalRatingBasedDetail" runat="server" Serialize="true" Width="100%" PageSize="5" AutoGenerateColumns="false"
                    EnableRecordHover="true" AllowSorting="False" CallbackMode="true" AllowPageSizeSelection="false"
                    OnRebind="RebindGrid" OnInsertCommand="InsertRecord" OnUpdateCommand="UpdateRecord" OnDeleteCommand="DeleteRecord"
                    FolderStyle="~/styles/btn_bank">   
                    <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" /> 
                    <Columns>
                        <obout:Column ID="Column1" DataField="gnd_id" ReadOnly="true" Visible="false" runat="server" />
				        <obout:Column ID="Column2" DataField="gnd_billing_type" HeaderText="JENIS TAGIHAN" Width="50%" runat="server">
                            <TemplateSettings EditTemplateId="tplEditJenisTagihan" />
                        </obout:Column>
                        <obout:Column ID="Column3" DataField="gnd_rwa" HeaderText="RISK WEIGHT" runat="server" Width="30%">
                            <TemplateSettings EditTemplateId="tplEditRiskWeight" />
                        </obout:Column>
                        <obout:Column ID="Column4" HeaderText="Action" Width="15%" AllowEdit="true" AllowDelete="true" runat="server" />
                    </Columns>   
                    
                    <Templates>
                        <obout:GridTemplate runat="server" ID="tplEditJenisTagihan" ControlID="txtEditJenisTagihan">
                            <Template>
                                <input type="text" id="txtEditJenisTagihan" name="txtEditJenisTagihan" class="inpTxt" size="50" />
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
            <tr background="../styles/btn_bank/footer.gif" style="height:30px">
				<td colspan="2" style="border-radius: 0 0 5px 5px;">
                    <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button>
					<asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                </td>
			</tr>
	</table>
    </div>
    </center>
    </form>
</body>
</html>
