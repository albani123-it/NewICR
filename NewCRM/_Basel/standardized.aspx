<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="standardized.aspx.vb" Inherits="NewCRM.standardized" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        aMenu('Basel');
        aBc('standardized');
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

        
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;">
        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Tahoma;font-size: 10px;height: 22px;" background="../Styles/btn_bank/header.gif" >
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
    </div>
    <div style="clear:both; margin:0 auto; padding:0; width:98%; max-height:460px;overflow-y:auto; ">
        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Verdana;font-size: 10px;color:#5E5E70;height: 22px;">
            <%= getAssetList()%>
        </table>
    </div>
    <div style="clear:both; margin:0 auto; padding:0; width:98%;">
        <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Century Gothic;font-size: 11px;height: 22px;">
            <tr style="background-color:#F6F5F0;height:33px;font-weight:bold;">
                <td colspan="7" style="height:33px;font-weight:bold;text-align:right;padding: 0 20px;"></td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        function fModelDetail(scid, moid) {
            var l_obj_argument = new Object();
            l_obj_argument.url = "standardizedDet.aspx?astid=" + scid + "&moid=" + moid;
            oWindowscroll(l_obj_argument.url, 800, 350);
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