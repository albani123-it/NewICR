<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="advanced_IRB.aspx.vb" Inherits="NewCRM.advanced_IRB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Basel');
        aBc('advanced');
        /** **/

        /** **/
        function OnBeforeEdit(record) {
            fLevelDetail(record.lvl_id);
            return false;
        }

        function OnBeforeAdd(record) {
            fLevelDetail(-1);
            return false;
        }

        fLoadLiteral();
        function fLoadLiteral(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		} 
    </script>
   
<Style type="text/css">
.tdText {
	font:11px Century Gothic;
	color:#333333;
}
.option2{
	font:11px Century Gothic;
	color:#0033cc;
	background-color: #f6f9fc;
	padding-left:4px;
	padding-right:4px;
}
			
.rowEditTable
{
	position: relative;
}
			
.rowEditTable td {
	font-family: Century Gothic;
	font-size: 10px;
	color: #4B555E;
}
			
.ob_iDdlICBC li
{
	width: 150px;
}
			
.ob_gFl .ob_gC, .ob_gFl .ob_gCW
{
    height: auto;
}
            
#MainContent_ContentPlaceHolderInSub_MainContent_grid1_ob_grid1FooterContainer_ob_grid1AddButton
{
    color: blue;
    font-weight: bold;
}       
            
.ob_gCS, .ob_gCS div, .ob_gCS_F, .ob_gCS_F div 
{
        display: none;
}        
td
{
    padding: 1px 5px;
}
a:hover
{
    text-decoration: underline;
    color:#D99E00;
}  			
</style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;">
        <asp:Button id="btnEngine" runat="server" Text="Update Engine" CssClass="inpBtn" style="margin-bottom:20px;"/>
        <asp:Button id="btnRun" runat="server" Text="Run Calculation Engine" CssClass="inpBtn" style="margin-bottom:20px;display:none;"/>
        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Tahoma;font-size: 10px;height: 22px;" background="../Styles/grid/header.gif" >
            <tr style="height:33px;font-weight:bold;">
                <td width="12px" style="padding-left:14px;">&nbsp;</td>
                <td width="30%">Asset Category</td>
                <td width="30%">Description</td>
                <td width="10%">Asset Class</td>
                <td width="10%">Approach</td>
                <td width="10%" style="text-align:center;">Status</td>
                <td width="10%">Action</td>
            </tr>
        </table>
    <%--</div>
    <div style="clear:both; margin:0 auto; padding:0; width:98%; max-height:460px;overflow-y:auto; ">--%>
        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Verdana;font-size: 10px;color:#5E5E70;height: 22px;">
            <%= getAssetList()%>
        </table>
    <%--</div>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;">--%>
        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Century Gothic;font-size: 11px;height: 22px;">
            <tr style="background-color:#F6F5F0;height:33px;font-weight:bold;">
                <td colspan="7" style="height:33px;font-weight:bold;text-align:right;padding: 0 20px;"></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        function fModelDetail(scid, iid) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "advanced_IRB_model.aspx?scid=" + scid + "&iid=" + iid;
            oWindowscroll(l_obj_argument.url, 900, 550);
        }


        function expcoll(a) {

            var path = location.pathname.split('/');

            //alert(location.protocol + '//' + location.host + '/' + location.pathname);
            var pathz = location.protocol + '//' + location.host + '/' + path[1] + '/'

            if (document.getElementById('img' + a).getAttribute('src') == "../Images/ec_minus.png" || document.getElementById('img' + a).getAttribute('src') == pathz + "Images/ec_minus.png") {
                document.getElementById('img' + a).src = pathz + "Images/ec_plus.png";
                PageMethods.GetData(a, onSuccess, onerror);
            }
            else {

                document.getElementById('img' + a).src = pathz + "Images/ec_minus.png";
                PageMethods.GetData(a, onSuccess2, onerror);
            }

        }
        function onSuccess(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {
                document.getElementById('trdet' + res[i + 1]).style.display = "none";
                PageMethods.GetDataModel(res[i + 1], onSuccessModel, onerror);
            }
        }
        function onSuccess2(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {
                document.getElementById('trdet' + res[i + 1]).style.display = "";
                PageMethods.GetDataModel(res[i + 1], onSuccessModel2, onerror);
            }
        }
        function onSuccessModel(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {

                document.getElementById('trdetail' + res[0] + "_" + res[i + 1]).style.display = "none";
            }
        }
        function onSuccessModel2(result) {
            var res = result.split(",");

            for (i = 0; i < res.length - 1; i++) {
                document.getElementById('trdetail' + res[0] + "_" + res[i + 1]).style.display = "";
            }
        }
	</script>
</asp:Content>
