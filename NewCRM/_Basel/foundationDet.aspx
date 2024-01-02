<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="foundationDet.aspx.vb" Inherits="NewCRM.foundationDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
    </style>
    <script type="text/javascript">
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function oWindow(url, w, h) {
            var width = w;
            var height = h;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open(url, "subFire", windowFeatures);
        }
        function OnBeforeDelete(record) {
            if (confirm("Are you sure you want to delete?") == false) {
                return false;
            }
            return true;
        }
        function OnBeforeEdit(record) {
            fDetail(record.mod_ast_id,record.mod_id,record.pol_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fDetail(document.getElementById("astid").value,document.getElementById("moid").value,-1);
            return false;
        }
        function fDetail(astid,moid,polid) {    
            var l_obj_argument = new Object();
            l_obj_argument.url = "PDPoolDet.aspx?astid=" + astid + "&moid=" + moid + "&polid=" + polid;
            oWindow(l_obj_argument.url, 550, 350);
        } 
        window.onload = function () {
            var scrollY = document.getElementById("scrollY").value
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                input.value = scrollY;
            }
        };
        function oWindow(url, w, h) {
            var width = w;
            var height = h;
            var left = parseInt((screen.availWidth / 2) - (width / 2));
            var top = parseInt((screen.availHeight / 2) - (height / 2));
            var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
            myWindow = window.open(url, "subFire", windowFeatures);
        }
        function fRuleAdd(){
            var id = document.getElementById("dataid").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "../_Parameter/ruleDet.aspx?scid=-1&data=" + id;
            oWindow(l_obj_argument.url, 1000, 600);
        }
        function fDeletePool(scid,moid,pid,astid){
	        if(confirm("Are you sure want to delete this pooling?")){
			    var l_obj_argument = new Object();
    		    l_obj_argument.url = "foundationDet.aspx?scid=" + scid + "&moid=" + moid + "&pid=" + pid + "&astid=" + astid;
                window.location=l_obj_argument.url;
	        }
	    }
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
        
        function reloadOboutGrid(){
            grid1.refresh();
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
    <input type="hidden" id="scrollY" name="scrollY" runat="server" />
    <input type="hidden" id="dataid" runat="server" />
       <div class="modal-body">
        <center>
            <table class="table table-striped table-bordered">
                <tr>
                    
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Asset Category</b></td>
                    <td><asp:Label ID="lblAssetCat" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Asset Sub Category</b></td>
                    <td><asp:Label ID="lblAssetSubCat" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Model Name</b></td>
                    <td><asp:Label ID="lblModelName" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Description</b></td>
                    <td><asp:Label ID="lblDesc" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Data Source</b></td>
                    <td><asp:Label ID="lblRule" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Approach Type</b></td>
                    <td><asp:DropDownList runat="server" Width="250px" ID="ddlApproach" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Asset Class</b></td>
                    <td><asp:DropDownList ID="ddlAssetClass" runat="server" CssClass="form-control" Width="250px">
                        <asp:ListItem Value="Pemerintah" Text="Pemerintah"></asp:ListItem>   
                        <asp:ListItem Value="Korporasi" Text="Korporasi"></asp:ListItem>   
                        <asp:ListItem Value="Bank" Text="Bank"></asp:ListItem>   
                        <asp:ListItem Value="Retail" Text="Retail"></asp:ListItem>
                    </asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Probabilty of Default (PD)</b></td>
                    <td style="padding:0;">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="padding:0;">
                                    <input type="radio" runat="server" id="rdPDText" name="rdPD" checked onclick="checkPD(1)" />
                                    <span>PD Pool Variable</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" style="padding:0;">
                                    <input type="radio" runat="server" id="rdPDField" name="rdPD" onclick="checkPD(2)" />
                                    <span>Use Field</span>
                                </td>
                                <td><asp:DropDownList ID="ddlPDField" runat="server" Width="250px" CssClass="form-control" Style="display: none;"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Loss Given Default (LGD)</b></td>
                    <td style="padding:0;">
                        <table width="100%">
                            <tr>
                                <td width="25%" style="padding:0;">
                                    <input type="radio" runat="server" id="rdLGDFixed" name="rdLGD" checked onclick="checkLGD(1)" />
                                    <span>Fixed Value</span>
                                </td>
                                <td><input type="text" runat="server" style="width:300px" id="txtLGDFixed" class="form-control"  /></td>
                            </tr>
                            <tr>
                                <td style="padding:0;">
                                    <input type="radio" runat="server" id="rdLGDFormula" name="rdLGD" onclick="checkLGD(2)" />
                                    <span>Eligible IRB Collateral</span>
                                </td>
                                <td><input type="text" runat="server" id="txtLGDFormula" size="5" class="form-control" value="0" style="display: none;" />
                                    <span id="spFormula" style="display:none;">&nbsp;% x E*/E</span>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding:0;">
                                    <input type="radio" runat="server" id="rdLGDField" name="rdLGD" onclick="checkLGD(3)" />
                                    <span>Use Field</span>
                                </td>
                                <td><asp:DropDownList ID="ddlLGDField" runat="server" Width="250px" CssClass="form-control" Style="display: none;"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Exposure At Default (EAD)</b></td>
                    <td style="padding:0;">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="padding:0;">
                                    <input type="radio" runat="server" id="rdEADFixed" name="rdEAD" checked onclick="checkEAD(1)" />
                                    <span>EAD Fixed / Variable Exposure</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" style="padding:0;">
                                    
                                    <input type="radio" runat="server" id="rdEADField" width="300px" name="rdEAD" onclick="checkEAD(2)" />
                                    <span>Use Field</span>
                                </td>
                                <td><asp:DropDownList ID="ddlEADField" Width="250px" runat="server" CssClass="form-control" Style="display: none;"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7" id="trCCF">
                    <td><b>Credit Conversion Factor (CCF)</b></td>
                    <td><input type="text" style="width:300px" id="txtCCF" name="txtCCF" class="form-control" runat="server" /></td>
                </tr>
                <tr bgcolor="#ebecec" id="trMaturity">
                    <td><b>Maturity</b></td>
                    <td style="padding:0;">
                        <table width="100%">
                            <tr>
                                <td style="padding:0;">
                                    <input type="radio" runat="server" id="rdMaturityFixed" name="rdMaturity" checked onclick="checkMaturity(1)" />
                                    <span>Fixed Value</span>
                                </td>
                                <td><input type="text" runat="server" id="txtMaturityFixed" size="5"  class="form-control" style="display: none;width:250px" /></td>
                            </tr>
                            <tr>
                                <td width="25%" style="padding:0;">
                                    <input type="radio" runat="server" id="rdMaturityField" name="rdMaturity" onclick="checkMaturity(2)" />
                                    <span>Use Field</span>
                                </td>
                                <td><asp:DropDownList ID="ddlMaturityField" Width="250px" runat="server" CssClass="form-control" Style="display: none;"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7" id="tr1" style="display:none;">
                    <td><b>Minimum Effective Maturity</b></td>
                    <td><input type="text" id="txtEffMaturity" name="txtEffMaturity" class="form-control" runat="server" /></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Activated</b></td>
                    <td>
                        <input type="checkbox" id="chkStatus" name="chkStatus" runat="server" />
                        <span style="color:Red;font-weight:bold" visible="false" id="lblStatus" runat="server">Can't active this model.</span>
                    </td>
                </tr>
            </table>
            <div id="pdPooling" runat="server" style="width:98%;">
                <table cellspacing="0" cellpadding="1" width="100%" style="text-align:left;border: thin solid #bcbcbc;">
                    <tr>
                        
                        <td bgcolor="#2A3F54" style="color: #fff;"><b>PD Pooling :</b></td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" style="text-align:left;border: thin solid #ccc;font-family: Verdana;font-size: 10px;color:#5E5E70;height: 22px;background-color:#F6F5F0;">
                    <tr>
                        
                     <td bgcolor="#2A3F54" style="display: none;">
                            <asp:Button ID="btnNew" runat="server" Text="New Rule" CssClass="btn btn-info" OnClientClick="OnBeforeAdd()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left:0;">
                            <obout:Grid ID="grid1" runat="server" Width="100%" Serialize="true" AllowPaging="false" PageSize="20"
                        AutoGenerateColumns="False" EnableRecordHover="true" AllowPageSizeSelection="False"
                         AllowRecordSelection="False" AllowSorting="true" CallbackMode="true" AllowAddingRecords="true"
                        FolderStyle="~/styles/grid">
                       <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
                       <Columns>
                            <obout:Column ID="Column1" DataField="mod_ast_id" Visible="false" runat="server" />
                            <obout:Column ID="Column8" DataField="mod_id" Visible="false" runat="server" />
                            <obout:Column ID="Column9" DataField="pol_id" Visible="false" runat="server" />
				            <obout:Column ID="Column2" DataField="pol_name" HeaderText="Name" runat="server" Width="20%"></obout:Column>

                            <obout:Column ID="Column3" DataField="rul_name" HeaderText="Rule" runat="server" Wrap="true" Width="20%"></obout:Column>

                            <obout:Column ID="Column4" DataField="pol_rule_rate" HeaderText="Rating" runat="server" Width="8%"></obout:Column> 
                                                                                   
                            <obout:Column ID="Column5" DataField="pol_pd" HeaderText="PD Value" runat="server" Wrap="true" Width="10%"></obout:Column>
                            
                            <obout:Column ID="Column6" DataField="pol_pd_baseline" HeaderText="Distribution Baseline" runat="server" Wrap="true" Width="15%"></obout:Column>
                                                         
                            <obout:Column ID="Column7" HeaderText="Action" Width="8%" AllowEdit="true" runat="server" />
                            <obout:Column ID="Column10" Width="10%" runat="server" >
                                <TemplateSettings TemplateID = "deleteTemplate" />
                            </obout:Column>
			                </Columns>  
                            <Templates>
                                 <obout:GridTemplate runat="server" ID="deleteTemplate" >
                                    <Template>
                                        <%  If delPool = True Then%>
                                                <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton> 
                                        <%  End If%>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
		                </obout:Grid>
                        </td>
                    </tr>
                </table>
            </div>
            <table cellspacing="0" cellpadding="1" width="98%" style="text-align:left;border: thin solid #bcbcbc;margin-bottom:20px;">
                <tr style="height:30px">
				    <td background="../styles/grid/footer.gif"  colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="inpBtn"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
    <script type="text/javascript">
        function checkPD(par) {
            if (par == 1) {
                document.getElementById("ddlPDField").style.display = "none";
                document.getElementById("pdPooling").style.display = "";
            }
            else if (par == 2) {

                document.getElementById("ddlPDField").style.display = "";
                document.getElementById("pdPooling").style.display = "none";
            }
        }
        function checkLGD(par) {
            if (par == 1) {
                document.getElementById("ddlLGDField").style.display = "none";
                document.getElementById("txtLGDFormula").style.display = "none";
                document.getElementById("spFormula").style.display = "none";
                document.getElementById("txtLGDFixed").style.display = "";
            }
            else if (par == 2) {
                document.getElementById("ddlLGDField").style.display = "none";
                document.getElementById("txtLGDFormula").style.display = "none";
                document.getElementById("spFormula").style.display = "none";
                document.getElementById("txtLGDFixed").style.display = "none";
            }
            else if (par == 3) {
                document.getElementById("ddlLGDField").style.display = "";
                document.getElementById("txtLGDFormula").style.display = "none";
                document.getElementById("spFormula").style.display = "none";
                document.getElementById("txtLGDFixed").style.display = "none";
            }
        }
        function checkEAD(par) {
            if (par == 1) {
                document.getElementById("ddlEADField").style.display = "none";
                document.getElementById("txtCCF").style.display = "";
//                document.getElementById("trCCF").style.display = "";
//                document.getElementById("trMaturity").style.backgroundColor = "#ebecec";
            }
            else if (par == 2) {
                document.getElementById("ddlEADField").style.display = "";
                document.getElementById("txtCCF").style.display = "none";
//                document.getElementById("trCCF").style.display = "none";
//                document.getElementById("trMaturity").style.backgroundColor = "#f6f7f7";
            }
        }
        function checkMaturity(par) {
            if (par == 1) {
                document.getElementById("ddlMaturityField").style.display = "none";
                document.getElementById("txtMaturityFixed").style.display = "";
            }
            else if (par == 2) {
                document.getElementById("ddlMaturityField").style.display = "";
                document.getElementById("txtMaturityFixed").style.display = "none";
            }
        }
        function fCheckDetail() {
            // PD
            if (document.getElementById("rdPDText").checked) {
                checkPD(1);
            }
            else if (document.getElementById("rdPDField").checked) {
                checkPD(2);
            }
            // LGD
            if (document.getElementById("rdLGDFixed").checked) {
                checkLGD(1);
            }
            else if (document.getElementById("rdLGDFormula").checked) {
                checkLGD(2);
            }
            else if (document.getElementById("rdLGDField").checked) {
                checkLGD(3);
            }
            // EAD
            if (document.getElementById("rdEADFixed").checked) {
                checkEAD(1);
            }
            else if (document.getElementById("rdEADField").checked) {
                checkEAD(2);
            }
            // Maturity
            if (document.getElementById("rdMaturityFixed").checked) {
                checkMaturity(1);
            }
            else if (document.getElementById("rdMaturityField").checked) {
                checkMaturity(2);
            }
        }
        fCheckDetail();
    </script>
</body>
</html>
