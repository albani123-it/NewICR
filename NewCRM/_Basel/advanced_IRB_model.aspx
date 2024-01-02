<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="advanced_IRB_model.aspx.vb" Inherits="NewCRM.advanced_IRB_model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>

<html>
  <head runat="server">
		<title>Skyworx - Internal Credit Rating</title>
        <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
        <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
        <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
	    <style>
		    .bk{
			    height: 16px;
			    cursor: pointer;
			    border: none;
			    background-color: Transparent;
		    }
		    td
            {
        	    padding-left: 5px;
            }
        
            input
            {
        	    font-family: "Century Gothic";
            }
		</style>		
		<script type="text/javascript" language="javascript">
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
		        fDetail(record.mod_ast_id, record.mod_id, record.pol_id);
		        return false;
		    }

		    function OnBeforeAdd(record) {
		        fDetail(document.getElementById("astid").value, document.getElementById("moid").value, -1);
		        return false;
		    }
		    function fDetail(astid, moid, polid) {
		        var l_obj_argument = new Object();
		        l_obj_argument.url = "PDPoolDet.aspx?astid=" + astid + "&moid=" + moid + "&polid=" + polid;
		        oWindow(l_obj_argument.url, 550, 350);
		    }

		    function OnBeforeDelete2(record) {
		        if (confirm("Are you sure you want to delete?") == false) {
		            return false;
		        }
		        return true;
		    }
		    function OnBeforeEdit2(record) {
		        fDetail2(record.mod_ast_id, record.mod_id, record.pol_id);
		        return false;
		    }

		    function OnBeforeAdd2(record) {
		        fDetail2(document.getElementById("astid").value, document.getElementById("moid").value, -1);
		        return false;
		    }
		    function fDetail2(astid, moid, polid) {
		        var l_obj_argument = new Object();
		        l_obj_argument.url = "LGDPoolDet.aspx?astid=" + astid + "&moid=" + moid + "&polid=" + polid;
		        oWindow(l_obj_argument.url, 550, 350);
		    }

		    function OnBeforeDelete3(record) {
		        if (confirm("Are you sure you want to delete?") == false) {
		            return false;
		        }
		        return true;
		    }
		    function OnBeforeEdit3(record) {
		        fDetail3(record.mod_ast_id, record.mod_id, record.pol_id);
		        return false;
		    }

		    function OnBeforeAdd3(record) {
		        fDetail3(document.getElementById("astid").value, document.getElementById("moid").value, -1);
		        return false;
		    }
		    function fDetail3(astid, moid, polid) {
		        var l_obj_argument = new Object();
		        l_obj_argument.url = "CCFPoolDet.aspx?astid=" + astid + "&moid=" + moid + "&polid=" + polid;
		        oWindow(l_obj_argument.url, 550, 350);
		    }

		    function panggil() {
		        PageMethods.rekol();
		    }

		    function fPageRule(scid, typid, baselid) {
//		        alert(scid + " , " + typid + " , " + baselid);
		        var l_obj_argument = new Object();
		        l_obj_argument.url = "../_Parameter/ruleDet.aspx?sci=" + scid + "&typid=" + typid + "&baselid=" + baselid;
		        window.open(l_obj_argument.url, 800, 550);
		    }
		    function reloadOboutGrid() {
		        grid1.refresh();
		        grid2.refresh();
		        grid3.refresh();
		    }
		</script>		
		<script type="text/javascript" language="javascript">
		var gRange	
			
		function fAddRule(scid){
		    var l_obj_argument = new Object();
		    l_obj_argument.url = "asset/rule.aspx?scid=" + scid;
		    oWindowscroll(l_obj_argument.url, 800, 550);
		}
		
		function CloseMe() {	
			window.returnValue = window.dialogArguments;
			event.returnValue = false;
			window.close();
		}		
		
		function fShowField(id){
			//fKeyTrap();
			document.getElementById('idHidden').value = id
			gRange = document.selection.createRange()
			lRect = gRange.getBoundingClientRect()
			var lObj
			if (event.ctrlKey && event.keyCode==70){
				lObj=document.Form1.lstField
			} else if (event.ctrlKey && event.keyCode==79){
				lObj=document.Form1.lstOperator
			}
			if (typeof(lObj)!="undefined"){
				lObj.style.visibility="visible"
				lObj.style.left=lRect.left;
				lObj.style.top=lRect.top;
				lObj.focus();
			}
		}
		function fShowGBField(id){
			//fKeyTrap();
			document.getElementById('idHidden').value = id
			gRange = document.selection.createRange()
			lRect = gRange.getBoundingClientRect()
			var lObj
			if (event.ctrlKey && event.keyCode==70){
				lObj=document.Form1.lstGBField
			} else if (event.ctrlKey && event.keyCode==79){
				lObj=document.Form1.lstOperator
			}
			if (typeof(lObj)!="undefined"){
				lObj.style.visibility="visible"
				lObj.style.left=lRect.left;
				lObj.style.top=lRect.top;
				lObj.focus();
			}
		}		
		function fKeyTrap(){			
			var ctrlPressed;
			ctrlPressed =  window.event.keyCode;
			alert(ctrlPressed);
		}
		
		function fHideField(pObj){
			pObj.style.visibility="hidden"
		}
		
		function fSelect(pObj){
			var id = document.getElementById('idHidden').value;
			if (pObj.selectedIndex>-1)
				gRange.text=pObj.options[pObj.selectedIndex].value
			pObj.style.visibility="hidden"
			document.getElementById(id).focus()
		}
		
		function fSelectKeyDown(pObj){
			var id = document.getElementById('idHidden').value;
			if (event.keyCode==13) fSelect(pObj);
			else if(event.keyCode==27) {
				pObj.style.visibility="hidden";
			document.getElementById(id).focus()
			}
		}
		function fTrapInteger(){
		    keyC = event.keyCode;
		    if (keyC >= 48 && keyC <= 57){
		        return true;
		    }else{
		        return false;
		    }		    
		}		
		
		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
	    function checkParent() {
	        var chknew = document.getElementById('chkNew').checked;
	        if(chknew){
	            document.getElementById('ddlCatID').disabled = true;
	        }else{
	            document.getElementById('ddlCatID').disabled= false;
	        }
	    }
	    
	    function add_data(dt){
	        document.getElementById("txt_rule_1").value = dt;
	    }
	    
	     $(document).ready(function(){
	        $("#tabs li").click(function() {
		        $("#tabs li").removeClass('active');
		        $(this).addClass("active");
		        $(".tab_content").hide();
		        var selected_tab = $(this).find("a").attr("href");
		        $(selected_tab).fadeIn();
		        return false;
	        });
        });
        
  	    function fDeletePool(scid,iid,pid){
	        if(confirm("are you sure to delete this pooling?")){
			    var l_obj_argument = new Object();
    		    l_obj_argument.url = "advanced_IRB_model.aspx?scid=" + scid + "&iid=" + iid + "&pid=" + pid;
                window.location=l_obj_argument.url;   
	        }
	    }
	    
        function checkMaturity(){
	        var val = document.getElementById("chkFieldMaturity").checked;
	        if(val){
	            document.getElementById("txtMaturity").disabled = true;
	            document.getElementById("ddlMaturity").style.visibility = 'visible'; 
	        }else{
	            document.getElementById("ddlMaturity").style.visibility = 'hidden';
	            document.getElementById("txtMaturity").disabled = false;
	        }	        
	    }

        </script>
		
<style type="text/css">
    #tabs_wrapper {
	width: 99%;
}
#tabs_container {
	
}
#tabs {
	list-style: none;
	padding: 5px 0 4px 0;
	margin: 0 0 0 0px;
}
#tabs li {
	display: inline;
	float:left;
	margin:0;
	margin-right:3px;
}
#tabs li a {
	border: 1px solid #ccc;
	color: #000;
	padding: 4px 6px;
	text-decoration: none;
	background-color: #eeeeee;
	border-bottom: none;
	outline: none;
	/*border-radius: 5px 5px 0 0;*/
	-moz-border-radius: 5px 5px 0 0;
	-webkit-border-top-left-radius: 5px;
	-webkit-border-top-right-radius: 5px;
}
#tabs li a:hover {
	background-color: #dddddd;
	padding: 4px 6px;
}
#tabs li.active a {
	border-bottom: 1px solid #fff;
	background-color: #fff;
	font-weight : bold;
	padding: 4px 6px 5px 6px;
	border-bottom: none;
}
#tabs li.active a:hover {
	background-color: #eeeeee;
	padding: 4px 6px 5px 6px;
	border-bottom: none;
}

#tabs li a.icon_accept {
	background-image: url(accept.png);
	background-position: 5px;
	background-repeat: no-repeat;
	padding-left: 24px;
}
#tabs li a.icon_accept:hover {
	padding-left: 24px;
}

#tabs_content_container {
	/*border-left: 1px solid #ccc;
        border-right: 1px solid #ccc;
        border-bottom: 1px solid #ccc;*/
        border-top: none;
        
        /*background-color:White;*/
        width: 100%;
}
.tab_content {
	display: none;
}
</style>
		
</head>
	<body onload="fLoad();">
		<form name="FormModel" id="FormModel" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
			<input type="hidden" id="txtId" runat="server" name="txtId" />
			<input type="hidden" id="txtAssetId" runat="server" name="txtAssetId" />
			<input type="hidden" id="ddlApproach" runat="server" value="" name="ddlApproach" />
            <div>
                <center>
             
                <table class="table table-striped table-bordered">
			    <tr>
				    
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
			    </tr>
				<tr bgcolor="#f6f7f7">
					<td style="width:30%"><b>Asset Category</b></td>
					<td><asp:Label ID="txtCat" runat="server" Text=""></asp:Label></td>
				</tr>
                <tr valign="top" bgColor="#ebecec">
					<td><b>Asset Sub Category</b></td>
					<td><asp:Label ID="txtSubCat" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr bgColor="#f6f7f7">
					<td><b>Model Name</b></td>
					<td><asp:label ID="txtName" name="txtName" runat="server" Text=""></asp:label></td>
				</tr>
				<tr valign="top" bgColor="#ebecec">
					<td><b>Description</b></td>
					<td><asp:label ID="txtDescription" name="txtDescription" runat="server" Text=""></asp:label></td>
				</tr>
                <tr valign="top" bgColor="#f6f7f7">
					<td><b>Data Source</b></td>
					<td><asp:Label ID="lblDataRule" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr valign="top" bgColor="#ebecec">
					<td><b>Approach Type</b></td>
					<td><asp:DropDownList ID="ddlApproachType" runat="server" Width="300px" CssClass="form-control"></asp:dropdownlist></td>
				</tr>
				<tr style="display:none" bgColor="#f6f7f7">
					<td><b>Hurdle Rate</b></td>
					<td>
                        <asp:TextBox name="txtHurdle" ID="txtHurdle" MaxLength="6" class="form-control" Width="250px" runat="server"></asp:TextBox>
					</td>
				</tr>
                <tr style="display:none;" bgColor="#ebecec">
					<td><b>Internal Rating</b></td>
					<td>
					<div><input type="hidden" value="0" name="txtIDPD" id="txtIDPD" runat="server" />
					    <span style="width:100px;">PD Rating </span>: 
                                <asp:DropDownList ID="txtInternalPD" class="form-control" DataValueField="scc_code"
                                    DataTextField="scc_name" runat="server" Style="margin-left: 20px; min-width: 150px" /></div>
					            <div><input type="hidden" value="0" name="txtIDLGD" id="txtIDLGD" runat="server" />
					                <span style="width:100px;">LGD Rating </span>: 
                                    <asp:DropDownList ID="txtInternalLGD" class="form-control" DataValueField="scc_code"
                                        DataTextField="scc_name" runat="server" Style="margin-left: 20px; min-width: 150px" /></div>
					<div><input type="hidden" value="0" name="txtIDEAD" id="txtIDEAD" runat="server" />
					    <span style="width:100px;">EAD Rating </span>: <asp:Dropdownlist id="txtInternalEAD" class="inpDdl" DataValueField = "scc_code" 
                            DataTextField = "scc_name" runat="server" style="margin-left: 20px;min-width:150px" /></div>
					</td>
				</tr>
                <tr bgColor="#f6f7f7">
					<td><b>Asset Class</b></td>
					<td><asp:DropDownList ID="txtAssetClass" class="form-control" Width="300px" DataValueField="cat_class"
                          DataTextField="cat_class" runat="server">
                          <asp:ListItem Value="Pemerintah" Text="Pemerintah"></asp:ListItem>   
                          <asp:ListItem Value="Korporasi" Text="Korporasi"></asp:ListItem>   
                          <asp:ListItem Value="Bank" Text="Bank"></asp:ListItem>   
                          <asp:ListItem Value="Retail" Text="Retail"></asp:ListItem>   
                         </asp:DropDownList></td>
				</tr>
				<tr bgColor="#ebecec">
					<td><b>Probability of Default (PD)</b></td>
					<td style="padding:0;">
					    <table style="PADDING-RIGHT: 5px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"cellSpacing="1" cellPadding="0" width="100%">
					        <tr>
					            <td colspan="2" style="padding:0;"><input type="radio" runat="server" name="rdPD" id="rdPDText" value="Pool" onclick="checkPD('PDText');" checked /> PD Pool Variable</td>
					        </tr>
					        <tr>
					            <td style="width:25%;padding:0;"><input type="radio" runat="server" name="rdPD" id="rdPDField" value="Field" onclick="checkPD('ddlPD');" /> Use Field</td>
					            <td><asp:DropDownList Style="visibility: hidden; float: left;" class="form-control" ID="ddlPD" runat="server" Width="250px" /></td>
					        </tr>
					     </table>
					</td>
				</tr>
				<tr bgColor="#f6f7f7">
					<td><b>Exposure At Default (EAD)</b></td>
					<td style="padding:0;">
					    <table style="PADDING-RIGHT: 5px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"cellSpacing="1" cellPadding="0" width="100%">
					        <tr>
					            <td colspan="2" style="padding:0;"><input type="radio" runat="server" name="rdEAD" id="rdEADText" value="Fixed" checked onclick="checkEAD('txtEADValue');" />   
                                    EAD Fixed/Variable Exposure</td>
					            <%--<td><input type="text" style="visibility:hidden;" value="0" name="txtEAD" id="txtEADValue" runat="server" /></td>--%>
					        </tr>
					        <tr>
					            <td style="width:25%;padding:0;"><input type="radio" runat="server" name="rdEAD" id="rdEADField" value="Field" onclick="checkEAD('ddlEAD');" /> Use Field</td>
					            <td><asp:DropDownList Style="visibility: hidden; float: left;" class="form-control" Width="250px" ID="ddlEAD" runat="server" /></td>
					        </tr>
					     </table>
					</td>
				</tr>
				<tr bgColor="#ebecec">
					<td><b>Maturity</b></td>
					<td style="padding:0;">
					    <table style="PADDING-RIGHT: 5px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"cellSpacing="1" cellPadding="0" width="100%">
					        <tr>
					            <td style="width:25%;padding:0;"><input type="radio" runat="server" name="rdMaturity" id="rdMaturityText" value="Fixed" checked onclick="checkMaturity('txtMaturity');" /> Fixed Value</td>
					            <td><input type="text" style="visibility: hidden;width:250px" class="form-control" value="0" name="txtMaturity" id="txtMaturity" runat="server" /></td>
					        </tr>
					        <tr>
					            <td style="padding:0;"><input type="radio" runat="server" name="rdMaturity" id="rdMaturityField" value="Field" onclick="checkMaturity('ddlMaturity');" /> Use Field</td>
					            <td><asp:DropDownList Style="visibility: hidden; float: left;" Width="250px" class="form-control" ID="ddlMaturity" runat="server" /></td>
					        </tr>
					     </table>
					</td>
				</tr>
                <tr bgcolor="#f6f7f7" id="tr1">
                    <td><b>Minimum Effective Maturity</b></td>
                    <td><input type="text" id="txtEffMaturity" name="txtEffMaturity" style="width:350px" class="form-control" runat="server" /></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Activated</b></td>
                    <td>
                        <input type="checkbox" id="chkStatus" name="chkStatus" runat="server" />
                        <span style="color:Red;font-weight:bold" visible="false" id="lblStatus" runat="server">Can't active this model.</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" bgcolor="#2A3F54">&nbsp;</td>
                </tr>
                </table>
                <table width="98%" style="border: thin solid #bcbcbc;">
                
				<tr bgColor="#ebecec">
					<td style="padding:0;">
						<div id="tabs_wrapper" style="padding-left:5px;">
					        <div id="tabs_container">
					            <ul id="tabs">
			                        <li class="active"><a id="tab1link" href="#tab1">Probability of Default</a></li>
			                        <li><a id="tab2link" href="#tab2">Loss Given Default</a></li>
			                        <li><a id="tab3link" href="#tab3">Credit Conversion Factor</a></li>
		                        </ul>
		                    </div>
		                    <div id="tabs_content_container">					    
					            <div id="tab1" style="display:block; width:100%;" class="tab_content">
                                    <obout:Grid ID="grid1" runat="server" Width="100%" Serialize="true" AllowPaging="false" PageSize="20"
                                    AutoGenerateColumns="False" EnableRecordHover="true" AllowPageSizeSelection="False"
                                     AllowRecordSelection="False" AllowSorting="true" CallbackMode="true" AllowAddingRecords="true"
                                    FolderStyle="~/styles/grid">
                                   <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnBeforeClientEdit="OnBeforeEdit" OnBeforeClientAdd="OnBeforeAdd" />
                                   <Columns>
                                        <obout:Column ID="Column1" DataField="mod_ast_id" Visible="false" runat="server" />
                                        <obout:Column ID="Column2" DataField="mod_id" Visible="false" runat="server" />
                                        <obout:Column ID="Column3" DataField="pol_id" Visible="false" runat="server" />
				                        <obout:Column ID="Column4" DataField="pol_name" HeaderText="Name" runat="server" Width="20%"></obout:Column>

                                        <obout:Column ID="Column5" DataField="rul_name" HeaderText="Rule" runat="server" Wrap="true" Width="20%"></obout:Column>

                                        <obout:Column ID="Column6" DataField="pol_rule_rate" HeaderText="Rating" runat="server" Width="8%"></obout:Column> 
                                                                                   
                                        <obout:Column ID="Column7" DataField="pol_pd" HeaderText="PD Value" runat="server" Wrap="true" Width="10%"></obout:Column>
                            
                                        <obout:Column ID="Column8" DataField="pol_pd_baseline" HeaderText="Distribution Baseline" runat="server" Wrap="true" Width="15%"></obout:Column>
                                                         
                                        <obout:Column ID="Column9" HeaderText="Action" Width="8%" AllowEdit="true" runat="server" />
                                        <obout:Column ID="Column10" Width="10%" runat="server" >
                                            <TemplateSettings TemplateID = "deleteTemplate" />
                                        </obout:Column>
			                            </Columns>  
                                        <Templates>
                                             <obout:GridTemplate runat="server" ID="deleteTemplate" >
                                                <Template>
                                                    <%  If delPool = True Then%>
                                                            <asp:LinkButton ID="zzz" runat="server" OnCommand="deletePD" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton> 
                                                    <%  End If%>
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
		                            </obout:Grid>
						        </div>
			                    <div id="tab2" class="tab_content" style="width:100%">
					                <obout:Grid ID="grid2" runat="server" Width="100%" Serialize="true" AllowPaging="false" PageSize="20"
                                    AutoGenerateColumns="False" EnableRecordHover="true" AllowPageSizeSelection="False"
                                     AllowRecordSelection="False" AllowSorting="true" CallbackMode="true" AllowAddingRecords="true"
                                    FolderStyle="~/styles/grid">
                                   <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete2" OnBeforeClientEdit="OnBeforeEdit2" OnBeforeClientAdd="OnBeforeAdd2" />
                                   <Columns>
                                        <obout:Column ID="Column11" DataField="mod_ast_id" Visible="false" runat="server" />
                                        <obout:Column ID="Column12" DataField="mod_id" Visible="false" runat="server" />
                                        <obout:Column ID="Column13" DataField="pol_id" Visible="false" runat="server" />
				                        <obout:Column ID="Column14" DataField="pol_name" HeaderText="Name" runat="server" Width="20%"></obout:Column>

                                        <obout:Column ID="Column15" DataField="rul_name" HeaderText="Rule" runat="server" Wrap="true" Width="20%"></obout:Column>

                                        <obout:Column ID="Column16" DataField="pol_rule_rate" HeaderText="Rating" runat="server" Width="8%"></obout:Column> 
                                                                                   
                                        <obout:Column ID="Column17" DataField="pol_lgd" HeaderText="LGD Value" runat="server" Wrap="true" Width="10%"></obout:Column>
                                        <obout:Column ID="Column18" HeaderText="Action" Width="8%" AllowEdit="true" runat="server" />
                                        <obout:Column ID="Column19" Width="10%" runat="server" >
                                            <TemplateSettings TemplateID = "deleteTemplate2" />
                                        </obout:Column>
			                            </Columns>  
                                        <Templates>
                                             <obout:GridTemplate runat="server" ID="deleteTemplate2" >
                                                <Template>
                                                    <%  If delPool = True Then%>
                                                         <asp:LinkButton ID="zzz2" runat="server" OnCommand="deleteLGD" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton> 
                                                    <%  End If%>
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
		                            </obout:Grid>
						        </div>
	                            <div id="tab3" class="tab_content" style="width:100%">
				                    <obout:Grid ID="grid3" runat="server" Width="100%" Serialize="true" AllowPaging="false" PageSize="20"
                                    AutoGenerateColumns="False" EnableRecordHover="true" AllowPageSizeSelection="False"
                                     AllowRecordSelection="False" AllowSorting="true" CallbackMode="true" AllowAddingRecords="true"
                                    FolderStyle="~/styles/grid">
                                   <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete3" OnBeforeClientEdit="OnBeforeEdit3" OnBeforeClientAdd="OnBeforeAdd3" />
                                   <Columns>
                                        <obout:Column ID="Column20" DataField="mod_ast_id" Visible="false" runat="server" />
                                        <obout:Column ID="Column21" DataField="mod_id" Visible="false" runat="server" />
                                        <obout:Column ID="Column22" DataField="pol_id" Visible="false" runat="server" />
				                        <obout:Column ID="Column23" DataField="pol_name" HeaderText="Name" runat="server" Width="20%"></obout:Column>

                                        <obout:Column ID="Column24" DataField="rul_name" HeaderText="Rule" runat="server" Wrap="true" Width="20%"></obout:Column>

                                        <obout:Column ID="Column25" DataField="pol_rule_rate" HeaderText="Rating" runat="server" Width="8%"></obout:Column> 
                                                                                   
                                        <obout:Column ID="Column26" DataField="pol_ead" HeaderText="EAD Value" runat="server" Wrap="true" Width="10%"></obout:Column>
                                        <obout:Column ID="Column27" HeaderText="Action" Width="8%" AllowEdit="true" runat="server" />
                                        <obout:Column ID="Column28" Width="10%" runat="server" >
                                            <TemplateSettings TemplateID = "deleteTemplate3" />
                                        </obout:Column>
			                            </Columns>  
                                        <Templates>
                                             <obout:GridTemplate runat="server" ID="deleteTemplate3" >
                                                <Template>
                                                    <%  If delPool = True Then%>
                                                         <asp:LinkButton ID="zzz3" runat="server" OnCommand="deleteCCF" OnClientClick="return confirm('Are you certain want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton> 
                                                    <%  End If%>
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
		                            </obout:Grid>
						        </div>
					        </div>
					     </div>
					</td>
				</tr>
			</table>

			<table cellSpacing="0" cellPadding="1" width="98%" style="text-align:left;border-radius:0 0 5px 5px;border: thin solid #bcbcbc;margin-bottom:10px;">
				<tr style="height:30px">
					<td >
						<asp:button id="btnSaveDetail" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>
                        <asp:button id="btnClose" runat="server" CssClass="btn btn-default" Text="Cancel" CausesValidation="False" onclientclick="CloseMe()"></asp:button>
					</td>
				</tr>
			</table>
            </center>
            </div>
		</form>
		<script type="text/javascript">
		    function checkMaturity(param) {
		        if (param == "txtMaturity") {
		            document.getElementById("ddlMaturity").style.visibility = 'hidden';
		        } else if (param == "ddlMaturity") {
		            document.getElementById("txtMaturity").style.visibility = 'hidden';
		        }
		        document.getElementById(param).style.visibility = 'visible';
		    }

		    function checkEAD(param) {
		        if (param == "txtEADValue") {
		            document.getElementById("ddlEAD").style.visibility = 'hidden';
		            document.getElementById("tab3").style.visibility = 'visible';
		            document.getElementById("tab3link").style.display = '';
		        } else if (param == "ddlEAD") {
		            //document.getElementById("txtEADValue").style.visibility = 'hidden';
		            document.getElementById("tab3").style.visibility = 'hidden';
		            document.getElementById(param).style.visibility = 'visible';
		            document.getElementById("tab3link").style.display = 'none';
		        }
		    }

		    function checkPD(param) {
		        if (param == "PDText") {
		            document.getElementById("ddlPD").style.visibility = 'hidden'
		            document.getElementById("tab1").style.visibility = 'visible';
		            document.getElementById("tab1link").style.display = '';
		        } else if (param == "ddlPD") {
		            document.getElementById("tab1").style.visibility = 'hidden';
		            document.getElementById(param).style.visibility = 'visible';
		            document.getElementById("tab1link").style.display = 'none';
		        }
		    }

		    function fCheckDetail() {

		        //FOR PD
		        if (document.getElementById("rdPDText").checked) {
		            checkPD('PDText');
		        } else if (document.getElementById("rdPDField").checked) {
		            checkPD('ddlPD');
		        }
		        //FOR EAD
		        if (document.getElementById("rdEADText").checked) {
		            checkEAD('txtEADValue');
		        } else if (document.getElementById("rdEADField").checked) {
		            checkEAD('ddlEAD');
		        }
		        //FOR Maturity
		        if (document.getElementById("rdMaturityText").checked) {
		            checkMaturity('txtMaturity');
		        } else if (document.getElementById("rdMaturityField").checked) {
		            checkMaturity('ddlMaturity');
		        }
		    }
		    fCheckDetail();
		</script>
	</body>
</html>
