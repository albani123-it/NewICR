<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ruleDet.aspx.vb" Inherits="NewCRM.ruleDet" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .bk{
		    height: 16px;
			cursor: pointer;
			border: none;
			background-color: Transparent;
		}
		#ptrchg
        {
            color: #FF0000;
        }
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
        .selHidden
        {
            position: absolute;
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9px;
            visibility: hidden;
        }
    </style>
    <script type="text/javascript">
        
        function ruleType(tipe){
            PageMethods.GetRule(tipe, onSuccess, onerror);
            return false;
        }
        function onSuccess(result){
            var a = document.getElementById("ddlParam_1.1");
            a.innerHTML = "";
            a.innerHTML = result;
            return false;
        }
        function onerror(){
            return false;
        }
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function oWindowscroll(url, w, h) {
                    var width = w;
                    var height = h;
                    var left = parseInt((screen.availWidth / 2) - (width / 2));
                    var top = parseInt((screen.availHeight / 2) - (height / 2));
                    var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                    myWindow = window.open(url, "_blank", windowFeatures);
                    myWindow.focus();
                }
            var gRange;
            var titip = "";

            function popupCallback(data, destination) {
                document.getElementById("ddlInput_" + destination).value = data;
            }

            function fOpenText(scid,op,idz,valsave) {
                var szUrl;
                var szFeatures;
                var l_obj_argument = new Object();
                //l_obj_argument.url = "text_rule.aspx?scid=" + scid + "&idz=" + idz + "&op=" + op;  
                l_obj_argument.url = "text_rule.aspx?scid=" + scid + "&idz=" + idz + "&op=" + op;  
                
                saveValue();

                oWindowscroll(l_obj_argument.url, 350, 450);
//                szUrl = "../modal.html";
//                szFeatures = 'dialogWidth:250px; dialogHeight:250px; status:0; help:0';
////                 window.showModalDialog(szUrl, l_obj_argument, szFeatures);              
//                if (l_obj_argument.returnvalue != "") {
//                    
//                    document.getElementById("ddlInput_" + idz).value = l_obj_argument.returnvalue;
//                }
//                else{
//                    //alert("returnvalue-nya kosong");
//                    document.getElementById("ddlInput_" + idz).value = "";
//                }
                if (window.event) {//IE
	                window.event.returnValue = false;
	            }
	            else {
	                return false;
	            }
            }            

//            function fShowField() {
//                gRange = document.selection.createRange();
//                lRect = gRange.getBoundingClientRect();
//                var lObj
//                if (event.ctrlKey && event.keyCode == 70) {
//                    lObj = document.Form1.lstField;
//                } else if (event.ctrlKey && event.keyCode == 79) {
//                    lObj = document.Form1.lstOperator;
//                }
//                if (typeof (lObj) != "undefined") {
//                    lObj.style.visibility = "visible";
//                    lObj.style.left = lRect.left;
//                    lObj.style.top = lRect.top;
//                    lObj.focus();
//                }
//            }
            
            function fHideField(pObj) {
                pObj.style.visibility = "hidden"
            }

            function fSelect(pObj) {
                if (pObj.selectedIndex > -1)
                    gRange.text = pObj.options[pObj.selectedIndex].value
                pObj.style.visibility = "hidden"
                //document.Form1.txtRule.focus()
                document.Form1.rlSQL.focus()
            }

            function fSelectKeyDown(pObj) {
                if (event.keyCode == 13) fSelect(pObj);
                else if (event.keyCode == 27) {
                    pObj.style.visibility = "hidden";
                    //document.Form1.txtRule.focus();
                    document.Form1.rlSQL.focus();
                }
            }
		</script>
    <script type="text/javascript">
        var par = "";
        par = "<%=ddlparam%>";
        var scidGo = "";
        scidGo = "<%=scidGo%>";
        var browsers = ""
        browsers = "<%= tipeBrowser %>";

        function kurang(id, idparent) {

            if ($(document.getElementById(idparent)).children('div').length == 1) {
                //document.getElementById('min' + '_' + id).disabled = true;
                return false;
            }
            else {
                var idnew = id + "";
                var idnew2 = idnew.replace(/\./g, "\\.");
                $('#' + idnew2).remove();
            }
        }

        function tambahAn(idz) {
	        var op = "";
	        var wall = "";
	        var p = "";
            
	        var idnew = idz + "";

	        var id = idnew.replace(/\./g, "\\.");
	        
	        b = id + "." + (($('#' + id).children('div').filter(':last-child').attr('id').substring($('#' + id).children('div').filter(':last-child').attr('id').lastIndexOf('.') + 1)) * 1 + 1);
	        
	        if (id.indexOf('.') == '-1') {
	            wall = 'style="margin-left:35px;clear:both;padding-top:10px;margin-bottom:10px;"';
	            wall2 = 'style="margin-left:18px;clear:both;padding-top:10px;margin-bottom:10px;"';
	        }
	        else {
	            wall = 'style="margin-left:55px;clear:both;padding-top:10px;margin-bottom:10px;"';
	            wall2 = 'style="margin-left:55px;clear:both;padding-top:10px;margin-bottom:10px;"';
	        }

	        var anew = b + "";
	        var a = anew.replace(/\\./g, "\.");
	        var b = a;
	        
	        p = p + par;
	        p = p + '</select>';

	        var sp = '<select id="ddlParam_' + b + '" onChange="checkParam(\'' + b + '\',this.value);" style="margin-right:10px;float:left;" class="inpDdl">';
	        //var sp = '<select id="ddlParam_' + b + '" style="margin-right:10px;float:left;" class="inpDdl">';
	        sp = sp + p;

	        var tp = '<select id="SW_' + b + '" onChange="checkParam(\'' + b + '\',this.value);" style="margin-right:10px;display:none;float:left" class="inpDdl">';
	        //var tp = '<select id="SW_' + b + '" style="margin-right:10px;display:none;float:left" class="inpDdl">';
	        tp = tp + p;

	        var so = '<select id="ddlOperant_' + b + '" onChange="getOperant(\'' + b + '\',this.value);" style="margin-right:10px;float:left;" class="inpDdl">';
	        so = so + '<option value="startswith">Starts With</option>';
	        so = so + '<option value="endswith">Ends With</option>';
	        so = so + '<option value="eq">Matches Other Field</option>';
	        so = so + '<option value="not eq">Not Match Other Field</option>';
	        so = so + '<option value="&lt;"> &lt; </option>';
	        so = so + '<option value="&gt;"> &gt; </option>';
	        so = so + '<option value="&lt;="> &lt;= </option>';
	        so = so + '<option value="&gt;="> &gt;= </option>';
	        so = so + '<option value="between">Between</option>';
	        so = so + '<option value="is null">Is Null</option>';
	        so = so + '<option value="is not null">Not Null</option>';
	        so = so + '<option value="like">Like</option>';
	        so = so + '<option value="equal">Equal</option>';
	        so = so + '<option value="notequal">Not Equal</option>';
	        so = so + '<option value="not like">Not Like</option>';
	        so = so + '<option value="in">In</option>';
	        so = so + '<option value="not in">Not In</option>';
	        so = so + '</select>';

	        var tb = '<input type="text" id="ddlInput_' + b + '" style="margin-right:5px;" class="inpTxt" /><input style="display:none" id="ddlInputText_' + b + '" type="image" src="../images/help.png" class="bk" onClick="saveValue(); fOpenText(document.getElementById(\'ddlParam_' + b + '\').value,document.getElementById(\'ddlOperant_' + b + '\').value,\'' + b + '\',document.getElementById(\'valSave\').value); return false;" /><span style="display:none; margin-right:10px" id="BW_' + b + '"><input type="text" id="ddlInputBW1_' + b + '" class="inpTxt" /><label style="float:left;">&nbsp;and&nbsp;</label><input type="text" id="ddlInputBW2_' + b + '" class="inpTxt" /></span><!--span style="display:none; margin-right:10px" id="IN_' + b + '">(<input type="text" id="ddlIN_' + b + '" />)</span-->' + tp;

	        $('#' + id).append('<div ' + wall + ' id=' + b + '><input style="margin-right:10px;float:left;" type="image" src="../images/ico_cross_circle.png" class="bk" onClick="return kurang(\'' + b + '\',\'' + id + '\')" />' + op + sp + so + tb + '<div style="display:none" id="ket_' + b + '"><div style="margin-left:18px; color:#bfd4ff">Integer</div></div><div id="ketr_' + b + '"><div style="margin-left:18px; color:#bfd4ff">Character</div></div></div>');
	        if (window.event) {//IE
	            window.event.returnValue = false;
	        }
	        else {
	            return false;
	        }
        }

        function tambahA(idz) {
	        var op = "";
	        var wall = "";
	        var p = "";

	        var idnew = idz + "";

	        var id = idnew.replace(/\./g, "\\.");

	        b = id + "." + (($('#' + id).children('div').filter(':last-child').attr('id').substring($('#' + id).children('div').filter(':last-child').attr('id').lastIndexOf('.') + 1)) * 1 + 1);
	        ba = b + ".1";

	        if (id.indexOf('.') == '-1') {
	            wall = 'style="margin-left:35px;clear:both;padding-top:10px;width:100%;margin-bottom:10px;"';
	            wall2 = 'style="margin-left:55px;clear:both;padding-top:10px;width:100%;margin-bottom:10px;"';
	        }
	        else {
	            wall = 'style="margin-left:55px;clear:both;padding-top:10px;width:100%;margin-bottom:10px;"';
	            wall2 = 'style="margin-left:55px;clear:both;padding-top:10px;width:100%;margin-bottom:10px;"';
	        }

	        var anew = b + "";
	        var a = anew.replace(/\\./g, "\.");
	        var b = a;

	        var cnew = ba + "";
	        var c = cnew.replace(/\\./g, "\.");
	        var ba = c;

	        op = "<select id='op_" + b + "' style='margin-right:10px;float:left;' class='inpDdl'><option value='AND'>AND</option><option value='OR'>OR</option></select>";

	        p = p + par;
	        p = p + '</select>';

	        var sp = '<select id="ddlParam_' + ba + '" onChange="checkParam(\'' + ba + '\',this.value);" style="margin-right:10px;float:left;" class="inpDdl">';
	        //var sp = '<select id="ddlParam_' + ba + '" style="margin-right:10px;float:left;" class="inpDdl">';
	        sp = sp + p;

	        var tp = '<select id="SW_' + ba + '" onChange="checkParam(\'' + ba + '\',this.value);" style="margin-right:10px;display:none;float:left;" class="inpDdl">';
	        //var tp = '<select id="SW_' + ba + '" style="margin-right:10px;display:none;float:left;" class="inpDdl">';
	        tp = tp + p;

	        var so = '<select id="ddlOperant_' + ba + '" onChange="getOperant(\'' + ba + '\',this.value);" style="margin-right:10px;float:left;" class="inpDdl">';
	        so = so + '<option value="startswith">Starts With</option>';
	        so = so + '<option value="endswith">Ends With</option>';
	        so = so + '<option value="eq">Matches Other Field</option>';
	        so = so + '<option value="not eq">Not Match Other Field</option>';
	        so = so + '<option value="&lt;"> &lt; </option>';
	        so = so + '<option value="&gt;"> &gt; </option>';
	        so = so + '<option value="&lt;="> &lt;= </option>';
	        so = so + '<option value="&gt;="> &gt;= </option>';
	        so = so + '<option value="between">Between</option>';
	        so = so + '<option value="is null">Is Null</option>';
	        so = so + '<option value="is not null">Not Null</option>';
	        so = so + '<option value="like">Like</option>';
	        so = so + '<option value="equal">Equal</option>';
	        so = so + '<option value="notequal">Not Equal</option>';
	        so = so + '<option value="not like">Not Like</option>';
	        so = so + '<option value="in">In</option>';
	        so = so + '<option value="not in">Not In</option>';
	        so = so + '</select>';

	        var tb = '<input type="text" id="ddlInput_' + ba + '" style="margin-right:5px;" class="inpTxt" /><input style="display:none" id="ddlInputText_' + ba + '" type="image" src="../images/help.png" class="bk" onClick="saveValue(); fOpenText(document.getElementById(\'ddlParam_' + ba + '\').value,document.getElementById(\'ddlOperant_' + ba + '\').value,\'' + ba + '\',document.getElementById(\'valSave\').value); return false;" /><span style="display:none; margin-right:10px" id="BW_' + ba + '"><input type="text" id="ddlInputBW1_' + ba + '" class="inpTxt" /><label style="float:left;">&nbsp;and&nbsp;</label><input type="text" id="ddlInputBW2_' + ba + '" class="inpTxt" /></span><!--span style="display:none; margin-right:10px" id="IN_' + ba + '">(<input type="text" id="ddlIN_' + ba + '" />)</span-->' + tp;

	        $('#' + id).append('<div ' + wall + ' id=' + b + '><input style="float:left;margin-right:10px;" type="image" src="../images/ico_cross_circle.png" class="bk" onClick="return kurang(\'' + b + '\',\'' + id + '\')" />' + op + '<input type="image" class="bk" src="../images/icon_plus.png" onclick="tambahAn(\'' + b + '\'); return false;" />&nbsp;<input type="image" class="bk" src="../images/new_row.png" onClick="tambahA(\'' + b + '\'); return false;" /><div ' + wall2 + ' id=' + ba + '><input style="margin-right:10px;float:left;" type="image" src="../images/ico_cross_circle.png" class="bk" onClick="return kurang(\'' + ba + '\',\'' + b + '\')" />' + sp + so + tb + '<div style="display:none" id="ket_' + ba + '"><div style="margin-left:18px; color:#bfd4ff">Integer</div></div><div id="ketr_' + ba + '"><div style="margin-left:18px; color:#bfd4ff">Character</div></div></div></div>');
	        
            if (window.event) {//IE
	            window.event.returnValue = false;
	        }
	        else{ 
	            return false;
            }
        }

	    function getOperant(id, val) {
	        if (val == 'startswith' || val == 'endswith' || val == 'in' || val == 'not in' || val == '&lt;' || val == '<' || val == '&gt;' || val == '>' || val == '&lt;=' || val == '<=' || val == '&gt;=' || val == '>=' || val == 'like' || val == 'equal' || val == 'notequal' || val == 'not like') {
	            document.getElementById("ddlInput_" + id).style.display = 'inline';
	            document.getElementById("SW_" + id).style.display = 'none';
	            document.getElementById("BW_" + id).style.display = 'none';
	            if (document.getElementById('ketr_' + id).style.display == 'block') {
	                document.getElementById('ddlInputText_' + id).   style.display = 'inline';
	            }
	        }
	        else if (val == 'eq' || val == 'not eq') {
	            document.getElementById("ddlInput_" + id).style.display = 'none';
	            document.getElementById("SW_" + id).style.display = 'inline';
	            document.getElementById("BW_" + id).style.display = 'none';
	            document.getElementById('ddlInputText_' + id).style.display = 'none';
	        }
	        else if (val == 'between') {
	            document.getElementById("ddlInput_" + id).style.display = 'none';
	            document.getElementById("SW_" + id).style.display = 'none';
	            document.getElementById("BW_" + id).style.display = 'inline';
	            document.getElementById("BW_" + id).style.marginRight = '10px';
	            document.getElementById('ddlInputText_' + id).style.display = 'none';
	        }
	        else if (val == 'is null' || val == 'is not null') {
	            document.getElementById("ddlInput_" + id).style.display = 'none';
	            document.getElementById("BW_" + id).style.display = 'none';
	            document.getElementById("SW_" + id).style.display = 'none';
	            document.getElementById('ddlInputText_' + id).style.display = 'none';
	        }
	    }

	    function checkParam(id, val) {
	        if (val.substring(val.lastIndexOf('|') + 1) == '2') {
	            document.getElementById('ket_' + id).style.display = 'none';
	            if ((browsers == "InternetExplorer") || (browsers == "IE")) {
	                document.getElementById('ketr_' + id).style.display = 'block';
	            }
	            else {
	                document.getElementById('ketr_' + id).style.display = 'inline';
	            }
	            if (document.getElementById('ddlParam_' + id).value != 'ACC_NO|2' && document.getElementById('ddlParam_' + id).value != 'CIF_NO|2' && document.getElementById('ddlParam_' + id).value != 'ZIP_CD|2') {
	                document.getElementById('ddlInputText_' + id).style.display = 'inline';
	            }
	            else {
	                document.getElementById('ddlInputText_' + id).style.display = 'none';
	            }
	        }
	        else {
	            document.getElementById('ketr_' + id).style.display = 'none';
	            if ((browsers == "InternetExplorer") || (browsers == "IE")) {
	                document.getElementById('ket_' + id).style.display = 'block';
	            }
	            else {
	                document.getElementById('ket_' + id).style.display = 'inline';
	            }
	            document.getElementById('ddlInputText_' + id).style.display = 'none';
	        }
	    }

	    function cetak() {
	        var msg = "";
	        var oper = "";
	        var lgt = $('#1').children('div').length;
	        /*
	        if (lgt == 1) {
	        msg = "Query-nya salah nih, Pak..";
	        //document.getElementById('cetak').innerHTML = msg;
	        document.getElementById('rlSQL').innerHTML = msg;
	        return;
	        }
	        */
	        /*ALBERT EDIT - KATA JO, BOLEH 1 CHILD*/
	        var i = 1;
	        $('#1').children().each(
				function (index) {
				    if ($(this).attr('id') != undefined) { //yang tidak ada id, tidak perlu diambil
				        if ($(this).attr('id').indexOf('op') != '-1') { //yang merupakan operator and/or dicetak terpisah
				            oper = $(this).val() + " ";
				        }
				        else {

				            if ($(this).children().closest('select').val() == 'AND' || $(this).children().closest('select').val() == 'OR') { //anak yang punya operator and/or
				                msg = msg + "(" + cetakAnak($(this).attr('id')) + ") ";
				            }
				            else {
				                //msg = msg + $(this).attr('id') + " ";
				                msg = msg + cetakIsi($(this).attr('id')); +" ";
				            }
				            if (i != lgt) {
				                msg = msg + oper;
				            }
				            i++;
				        }
				    }
				}
			);

	        if (msg.substring(0, 2) == 'AN' || msg.substring(0, 2) == 'OR') msg = "Query Failed, Please check..";

	        //if (document.getElementById("chkVR").checked == true) {
	        /*if (lgt == 1) {
	        msg = "Query-nya salah nih, Pak..";
	        //document.getElementById('cetak').innerHTML = msg;
	        document.getElementById('rlSQL').innerHTML = msg;
	        return;
	        } else {
	        document.getElementById('rlSQL').innerHTML = msg;
	        }*/
	        /*ALBERT EDIT - KATA JO, BOLEH 1 CHILD*/
	        //document.getElementById('rlSQL').innerHTML = msg;	            
	        //}
	        //else {
	        //document.getElementById('rlSQL').innerHTML = "";
	        //} /*ALBERT EDIT*/
	        document.getElementById('rlSQL').innerHTML = msg;
	        document.getElementById('ptrchg').style.display = "none";
	        //saveValue();
	    }

	    function cetakBanding() {
	        var msg = "";
	        var oper = "";
	        var lgt = $('#1').children('div').length;

	        var i = 1;
	        $('#1').children().each(
				function (index) {
				    if ($(this).attr('id') != undefined) { //yang tidak ada id, tidak perlu diambil
				        if ($(this).attr('id').indexOf('op') != '-1') { //yang merupakan operator and/or dicetak terpisah
				            oper = $(this).val() + " ";
				        }
				        else {

				            if ($(this).children().closest('select').val() == 'AND' || $(this).children().closest('select').val() == 'OR') { //anak yang punya operator and/or

				                msg = msg + "(" + cetakAnak($(this).attr('id')) + ") ";
				            }
				            else {
				                //msg = msg + $(this).attr('id') + " ";
				                msg = msg + cetakIsi($(this).attr('id')); +" ";
				            }
				            if (i != lgt) {
				                msg = msg + oper;
				            }
				            i++;
				        }
				    }
				}
			);

	        if (msg.substring(0, 2) == 'AN' || msg.substring(0, 2) == 'OR') msg = "Query Failed, Please check..";
	        
	        //document.getElementById('rlSQL').innerHTML = msg;
	        //document.getElementById('ptrchg').style.display = "none";
	        if (document.getElementById('rlSQL').value != msg) {
	            document.getElementById("ptrchg").style.display = "inline";
	        }
	        else document.getElementById("ptrchg").style.display = "none";
	    }

	    function cetakAnak(id) {
	        var oper;
	        var msg = "";
	        var lgt;
	        var i = 1;

	        var idnew = id + "";

	        id = idnew.replace(/\./g, "\\.");

	        /*if ($('#' + id).children('div').length == '1') {
	        msg = "Query-nya yakin sudah benar, Pak?";
	        return msg;
	        }*/
	        /*ALBERT EDIT - KATA JO, BOLEH 1 CHILD*/

	        var lgt = $('#' + id).children('div').length;
	        
	        $('#' + id).children().each(
				function (index) {
				    if ($(this).attr('id') != undefined) {
				        if ($(this).attr('id').indexOf('op') != -1) {
				            oper = $(this).val() + " ";
				        }
				        else {
				            if ($(this).children().closest('select').val() == 'AND' || $(this).children().closest('select').val() == 'OR') { //anak yang punya operator and/or

				                msg = msg + "(" + cetakAnak($(this).attr('id')) + ") ";
				            }
				            else {
				                msg = msg + cetakIsi($(this).attr('id')); +" ";
				            }
				            if (i != lgt) {
				                msg = msg + oper;
				            }
				            i++;
				        }
				    }
				}
			);
	        return msg;
	    }

	    function cetakIsi(id) {
	        var i;
	        var j = 1;
	        var temp = "";
	        var msg = "";
	        var tmp = 0;

	        var idnew = id + "";
	        id = idnew.replace(/\./g, "\\.");
	        $('#' + id).children().each(
				function (index) {
				    if ((browsers == "InternetExplorer") || (browsers == "IE")) {
				        if ($(this).attr('id') != undefined && $(this).attr('id').indexOf('Text') == '-1' && $(this).css('display') == 'inline' && ($(this).attr('id') != "ket_" + id || $(this).attr('id') != "ketr_" + id)) {

				            if ($(this).children('input').length >= 1) {
				                var cek = $(this).children('input').length;
				                $(this).children('input').each(
								function (index) {
								    if (j != cek) {
								        temp = temp + $(this).val() + " AND ";
								    }
								    else {
								        temp = temp + $(this).val() + " ";
								    }
								    j++;
								}
							);
				            }

				            if ($(this).val() == 'endswith') {
				                msg = msg + 'like' + " '%";
				                tmp = 5;
				            }
				            else if ($(this).val() == 'startswith') {
				                msg = msg + 'like' + " ";
				                tmp = 4;
				            }
				            else if ($(this).val() == 'equal') {
				                msg = msg + "= '";
				                tmp = 6;
				            }
				            else if ($(this).val() == 'notequal') {
				                msg = msg + "!= '";
				                tmp = 7;
				            }
				            else if ($(this).val() == 'eq') {
				                msg = msg + "= ";
				            }
				            else if ($(this).val() == 'not eq') {
				                msg = msg + "!= ";
				            }
				            else if ($(this).val() == 'between') {
				                msg = msg + $(this).val();
				                tmp = 3;
				            }
				            else if ($(this).val() == 'like' || $(this).val() == 'not like') {
				                msg = msg + $(this).val() + " '%";
				                tmp = 1;
				            }
				            else if ($(this).val() == 'in' || $(this).val() == 'not in') {
				                msg = msg + $(this).val() + " (";
				                tmp = 2;
				            }
				            else {
				                if (tmp == 1 || tmp == 5) {
				                    msg = msg + $(this).val();
				                }
				                else if (tmp == 6 || tmp == 7) {
				                    msg = msg + $(this).val() + "' ";
				                }
				                else if (tmp == 4) {
				                    msg = msg + "'" + $(this).val();
				                }
				                else {
				                    if ($(this).val().indexOf('|') != '-1') {
				                        msg = msg + $(this).val().substring(0, $(this).val().indexOf('|')) + " ";
				                    }
				                    else {
				                        msg = msg + $(this).val() + " ";
				                    }
				                }
				            }
				        }
				    }
				    else {
				        if ($(this).attr('id') != undefined && $(this).attr('id').indexOf('Text') == '-1' && $(this).css('display') == 'block' && ($(this).attr('id') != "ket_" + id || $(this).attr('id') != "ketr_" + id)) {
				            if ($(this).children('input').length >= 1) {
				                var cek = $(this).children('input').length;
				                $(this).children('input').each(
								function (index) {
								    if (j != cek) {
								        temp = temp + $(this).val() + " AND ";
								    }
								    else {
								        temp = temp + $(this).val() + " ";
								    }
								    j++;
								}
							);
				            }

				            if ($(this).val() == 'endswith') {
				                msg = msg + 'like' + " '%";
				                tmp = 5;
				            }
				            else if ($(this).val() == 'startswith') {
				                msg = msg + 'like' + " ";
				                tmp = 4;
				            }
				            else if ($(this).val() == 'equal') {
				                msg = msg + "= '";
				                tmp = 6;
				            }
				            else if ($(this).val() == 'notequal') {
				                msg = msg + "!= '";
				                tmp = 7;
				            }
				            else if ($(this).val() == 'eq') {
				                msg = msg + "= ";
				            }
				            else if ($(this).val() == 'not eq') {
				                msg = msg + "!= ";
				            }
				            else if ($(this).val() == 'between') {
				                msg = msg + $(this).val();
				                tmp = 3;
				            }
				            else if ($(this).val() == 'like' || $(this).val() == 'not like') {
				                msg = msg + $(this).val() + " '%";
				                tmp = 1;
				            }
				            else if ($(this).val() == 'in' || $(this).val() == 'not in') {
				                msg = msg + $(this).val() + " (";
				                tmp = 2;
				            }
				            else {
				                if (tmp == 1 || tmp == 5) {
				                    msg = msg + $(this).val();
				                }
				                else if (tmp == 6 || tmp == 7) {
				                    msg = msg + $(this).val() + "' ";
				                }
				                else if (tmp == 4) {
				                    msg = msg + "'" + $(this).val();
				                }
				                else {
				                    if ($(this).val().indexOf('|') != '-1') {
				                        msg = msg + $(this).val().substring(0, $(this).val().indexOf('|')) + " ";
				                    }
				                    else {
				                        msg = msg + $(this).val() + " ";
				                    }
				                }
				            }
				        }
				    } //browser
				}
			);
	        msg = msg + temp;
	        if (tmp == 1 || tmp == 4) {
	            msg = msg + "%' ";
	        }
	        else if (tmp == 2) {
	            msg = msg + ") ";
	        }
	        else if (tmp == 3) {
	            msg = "(" + msg + ") ";
	        }
	        else if (tmp == 5) {
	            msg = msg + "' ";
	        }

	        return msg;
	    }

	    function saveValue() {
	        var msgs = "";
	        var i = 0;

	        /***********************************************************************************************************************************/

	        var msg2 = "";

	        msg2 = $('.isi').html();                               /* sama seperti outerHTML */
	        //$('#isi2').append($('#1').clone()).remove().html();    /* belum terlalu paham */
	        //msg2 = $('#1')[0].outerHTML;                         /* sama seperti html() */ 

	        /***********************************************************************************************************************************/
	        
	        $('#1').find('*').each(
				function (index) {
				    if ($(this).attr('id') != undefined && $(this).css('display') == 'inline' && $(this)[0].tagName != 'IMG' && $(this).attr('class') != 'bk' && $(this).val() != "") {
				        //msgs = msgs + $(this).attr('id') + " " + $(this).val() + " ";
				        msgs = msgs + "document.getElementById('" + $(this).attr('id') + "').value='" + $(this).val() + "';";
				        i++;
				    }
				}
			);

	        //msg2 = krEncodeEntities(msg2)
	        //msgs = krEncodeEntities(msgs)
	        document.getElementById("valSave").value = msg2;
	    }

	    function chgPtrUp() {
	        var a = document.getElementById("valPtr").value;
	        var b = document.getElementById("rlSQL").value;
	        if (a != b) {
	            document.getElementById("ptrchg").style.display = "inline";
	        }
	        return false;
	    }

	    function chgPtrDown() {
	        document.getElementById("valPtr").value = document.getElementById("rlSQL").value;
	    }

	    function cekVis() {
	        var a = cetak();

	    }
    </script>
</head>
<body onload="fLoad(); cetakBanding();">
    <form id="Form1" name="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" name="ruleId" id="ruleId" runat="server" />
    <%--<div>--%>
        <center>
            
            <table class="table table-striped table-bordered">
                <tr>
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="120px"><b>Master Data</b></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlRuleData" Width="300px" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td width="120px"><b>Rule Name</b></td>
                    <td><asp:TextBox ID="txtRuleName" runat="server" CssClass="form-control" Style="width: 300px;"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="120px"><b>Description</b></td>
                    <td><asp:TextBox ID="txtDescription" runat="server" MaxLength="1000" Width="300px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td colspan="2"><b>Visual Rule Tool</b></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td colspan="2" style="padding-right:5px;">
                        <table width="100%" style="border: thin dashed #D99E00;height:200px;margin-bottom:10px;">
                            <tr style="height:200px;">
                                <td style="vertical-align:top;padding-left:0;">
                                <div class="isi">
                                <%
                                    If scidGo = "" Or (scidGo <> "" And IO.File.Exists(Server.MapPath("text\") & scidGo & ".txt") = False) Then
                                        Dim kertas As String = "" ', kertas2 As String = ""
                                        Dim tanda As Integer
                                        Dim oFil As System.IO.File
                                        Dim oRea As System.IO.StreamReader
                                        Dim contentFile
                                        kertas = Server.MapPath("text\") & ipadd & "temp.txt"
                                        'kertas2 = Server.MapPath("text\") & ipadd & "_textrule.txt"
        
                                        If oFil.Exists(kertas) Then
                                            tanda = 1
                                            'ElseIf oFil.Exists(kertas2) Then
                                            'tanda = 2
                                            'kertas = kertas2
                                        End If
        
                                        'If tanda = 1 Or tanda = 2 Then
                                        If tanda = 1 Then
                                            oRea = oFil.OpenText(kertas)
                                            contentFile = oRea.ReadToEnd
                                        End If
        
                                        If contentFile = "" Then
                                            tanda = 3
                                        End If
        
                                        If tanda <> 3 Then
                                            Response.Write(contentFile)
                                            oRea.Close()
                                        Else
        
                                %>
                                <div id="1" style="margin:0; background-color:#bfd4ff; overflow:auto; height:300px; width:100%;">
                                    <select id='op_1' style='margin-right:10px;float:left;' class="inpDdl">
                                        <option value="AND">AND</option>
                                        <option value="OR">OR</option>
                                    </select>
                                    <input type="image" src="../images/icon_plus.png" class="bk" onclick="tambahAn(1); return false;" style="margin:0; padding:0;"/>
                                    <input type="image" src="../images/new_row.png" class="bk" onclick="tambahA(1); return false;" style="margin:0; padding:0;"/>
                                    <div id="1.1" style="margin-left:35px;margin-top:10px;height:13px;clear:both;margin-bottom:10px;">
                                        <input type="image" src="../images/ico_cross_circle.png" class="bk" onclick="return kurang(1.1,1);" style="float:left;margin-right:10px;"/>
                                        <select id="ddlParam_1.1" onchange="checkParam(1.1,this.value);" style="float:left;margin-right:10px;" class="inpDdl">
                                            <%= ddlparam%>
                                        </select>
                                        <select id="ddlOperant_1.1" onchange="getOperant(1.1, this.value);" style="float:left;margin-right:10px;" class="inpDdl">
		                                    <option value="startswith">Starts With</option>
		                                    <option value="endswith">Ends With</option>
		                                    <option value="eq">Matches Other Field</option>
		                                    <option value="not eq">Not Match Other Field</option>
		                                    <option value="&lt;"> &lt; </option>
		                                    <option value="&gt;"> &gt; </option>
		                                    <option value="&lt;="> &lt;= </option>
		                                    <option value="&gt;="> &gt;= </option>
		                                    <option value="between">Between</option>
		                                    <option value="is null">Is Null</option>
		                                    <option value="is not null">Not Null</option>
		                                    <option value="like">Like</option>
		                                    <option value="equal">Equal</option>
		                                    <option value="notequal">Not Equal</option>
		                                    <option value="not like">Not Like</option>
		                                    <option value="in">In</option>
		                                    <option value="not in">Not In</option>
	                                    </select>
                                        <div id="tempatnitip" style="display:none"></div>
                                        <input type="text" id="ddlInput_1.1" style="margin-right:5px;" class="inpTxt" />
                                        <input style="display:none;" id="ddlInputText_1.1" type="image" src="../images/help.png" class="bk" onClick="saveValue();fOpenText(document.getElementById('ddlParam_1.1').value,document.getElementById('ddlOperant_1.1').value,'1.1',document.getElementById('valSave').value); return false;" />
                                        <span style="display:none; margin-top:10px;" id="BW_1.1"><input type="text" id="ddlInputBW1_1.1" class="inpTxt" /><label style="float:left;">&nbsp;and&nbsp;</label><input type="text" id="ddlInputBW2_1.1" class="inpTxt" /></span>
                                        <select id="SW_1.1" onChange="checkParam(1.1,this.value);" style="margin-right:10px;display:none;" class="inpDdl">
                                            <%=ddlparam%>
	                                    </select>
                                        <div style="display:none" id="ket_1.1">
                                            <div style="margin-left:18px; color:white">Integer</div>
                                        </div>
                                        <div id="ketr_1.1">
                                            <div style="margin-left:18px; color:white">Character</div>
                                        </div>
                                        <%--<label id="ket_1.1" style="color: white; display: inline;">
                                            Integer
                                        </label>
                                        <label id="ketr_1.1" style="color: white; display: none;">
                                            Character
                                        </label>--%>
                                    </div>
                                </div>
                                </div>
                                <%
                                End If
                            Else
                                Dim oFile As System.IO.File
                                Dim oRead As System.IO.StreamReader
                                Dim contentFile
                                If Not IO.File.Exists(Server.MapPath("text\") & scidGo & "temp.txt") Then
                                    oRead = oFile.OpenText(Server.MapPath("text\") & scidGo & ".txt")
                                Else
                                    oRead = oFile.OpenText(Server.MapPath("text\") & scidGo & "temp.txt")
                                End If
                                contentFile = oRead.ReadToEnd
                                Response.Write(contentFile)
                                oRead.Close()
                            End If
                            %>
                            
                            <div id="isi2"><asp:HiddenField ID="valSave" runat="server" /><asp:HiddenField ID="valSave2" runat="server" /></div>
                            <div id="cetak"></div>
                                </td>
                            </tr>
                            <tr>
			                    <td width="100%" bgcolor="#ababab">
			                        <!--input id="chkVR" type="checkbox" onclick="cetak()"/--> <!--Apply Visual Rule-->
			                        <input id="chkVR" type="button" value="Generate Rule" OnClick="cetak();saveValue();" class="inpBtn" />
			                        <asp:HiddenField ID="valPtr" value="0" runat="server" />
                                    <span id="ptrchg" style="display:none">&nbsp;SQL Script doesn&#39;t match Visual Rule</span>
                                 </td>
		                    </tr>
                            <tr>
                                <td style="padding:0;">
                                    <div id="genSQL" style="text-align:left;">
                                        
                                    <asp:TextBox ID="rlSQL" name="rlSQL" onkeydown="chgPtrDown();" onkeyup="chgPtrUp();" runat="server" MaxLength="1000" Width="98%" Height="90px" TextMode="MultiLine"
                                    CssClass="form-control"></asp:textbox>
	                                    <br />
                                        Browse field (Alt + f) | Browse operator (Alt + o)
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr  style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="saveValue();"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" onclientclick="CloseMe();"></asp:button>
                    </td>
			    </tr>
            </table>
            
        </center>
        <asp:ListBox runat="server" id="lstField" class="selHidden" onclick="fSelect(this)" onkeydown="fSelectKeyDown(this)" onblur="fHideField(this)"></asp:ListBox>		
			<select id="lstOperator" style="VISIBILITY:hidden;POSITION:absolute;" onclick="fSelect(this)" onkeydown="fSelectKeyDown(this)" onblur="fHideField(this)" size="4" class="inpDdl">
				<option value="And">And</option>
				<option value="or">Or</option>
				<option value="(">(</option>
				<option value=")">)</option> <OPTION value== 
                    DESIGNTIMESP="17350">=</OPTION>
				<option value="<>">&lt;&gt;</option>
				<option value=">">&gt;</option>
				<option value="&gt;=">&gt;=</option>
				<option value="<">&lt;</option>
				<option value="&lt;=">&lt;=</option>
			</select>
    <%--</div>--%>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#rlSQL").bind('keydown', 'alt+f', function () {
                var e = document.getElementById("ddlRuleData");
                var fieldText = e.options[e.selectedIndex].text;
                var left = (screen.width / 2) - (330 / 2);
                var top = (screen.height / 2) - (318 / 2);
                var url = "ruleDetValues.aspx?f=" + fieldText + "&destination=rlSQL&source=field";
                var l_obj_argument = new Object();
                window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);

            })
            .bind('keydown', 'alt+o', function () {
                var e = document.getElementById("ddlRuleData");
                var fieldText = e.options[e.selectedIndex].text;
                var left = (screen.width / 2) - (330 / 2);
                var top = (screen.height / 2) - (318 / 2);
                var url = "ruleDetValues.aspx?f=" + fieldText + "&destination=rlSQL&source=operator";
                var l_obj_argument = new Object();
                window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);
            });
        });
        function popupCallbackField(data, destination,mode) {
            var words = document.getElementById(destination).value;
            var last = words.substring(words.length - 1, words.length);

            if (mode == "operator") {
                document.getElementById(destination).value += " " + data + " ";
            }
            else {
                document.getElementById(destination).value += data;                
            }
        }
    </script>
</body>
</html>
