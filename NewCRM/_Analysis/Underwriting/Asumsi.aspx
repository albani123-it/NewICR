<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Asumsi.aspx.vb"
    Inherits="NewCRM.Asumsi" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/tableHeadFixer.js"></script>
    <script type="text/javascript">
        aMenu("Analysis");
        aBc("Kertas Kerja");

        $(document).ready(function () {
            $("#tblAsmDist").tableHeadFixer({ "left": 3 });
		});

        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
        
        function isNumber(n) {
          return !isNaN(parseFloat(n)) && isFinite(n);
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 45)
                return false;
            return true;
        }
        
        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
            }
            else if(num == "0-")
            {
                return "-";
            }
            else if (num == ""){
                return "";
            }
            else{
                if (isNaN(num))
                    num = "";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                num = Math.floor(num / 100).toString();
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num);
            }
        }

        function fHide(id,img_id){
            var path = location.pathname.split('/');
            var pathz = location.protocol + '//' + location.host + '/' + path[1] + '/';

            if (document.getElementById(img_id).getAttribute('src') == "../../Images/ec_minus.png" || document.getElementById(img_id).getAttribute('src') == pathz + "Images/ec_minus.png") {
                document.getElementById(img_id).src = pathz + "Images/ec_plus.png";
                document.getElementById(img_id).setAttribute("alt", "Plus");
                document.getElementById(id).style.display = "none";
            }
            else {

                document.getElementById(img_id).src = pathz + "Images/ec_minus.png";
                document.getElementById(img_id).setAttribute("alt", "Minus");
                document.getElementById(id).style.display = "";
            }
            
        }

        function calcAsumsiYearly(code,period_proyeksi,max_his,src){
            var rowHis = document.getElementById("<%= hdRowHis.clientID %>").value;
            var startYear = document.getElementById("<%= ddlStartYear.clientID %>").value;
            var endYear = document.getElementById("<%= ddlEndYear.clientID %>").value;
            var year_select = document.getElementById("selAsmYearly_" + code + '_' + period_proyeksi).value;
            
            var data,res, growth, growth_pos, amount, amount_pos, year, data_period;

            if (parseInt(year_select) > parseInt(max_his)){
                data = document.getElementById("txtAsmYearlyAmount_" + code + '_' + year_select).value.replace(/,/g, "");
            }
            else{
                if (rowHis > 0){
                    data = document.getElementById("hdHis_" + code + '_' + year_select).value.replace(/,/g, "");
                }
            }

            if (src == "Growth"){
                growth = document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value.replace(/,/g, "");
                if (data != ""){
                    if (growth != "" && growth_pos != ""){
                        res = growth * data / 100;
                        document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value = formatCurrency(data * 1 + res * 1);
                    }
                    else{
                        document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value = "";
                    }
                }
            }
            if (src == "Amount"){
                amount = document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value.replace(/,/g, "");
                if (rowHis > 0){
                    if (data != "" && parseInt(data) != 0){
                        if (amount != ""){
                            res = (amount * 1 - data * 1) / data * 100;
                            document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value = parseFloat(res).toFixed(2);
                        }
                        else{
                            document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value = "";
                        }
                    }
                }
                else {
                    if (period_proyeksi != startYear){
                        if (data != ""){
                            if (amount != ""){
                                res = (amount * 1 - data * 1) / data * 100;
                                document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value = parseFloat(res).toFixed(2);
                            }
                            else{
                                document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value = "";
                            }
                        }
                    }
                    else{
                        document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value = "n/a";
                    }
                }
                
            }

            monthDist(code, period_proyeksi);
            data = document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value.replace(/,/g, "");

            //loop cek efek
            for (var i = startYear; i <= endYear; i++){
                //cek ke yang lain
                year = document.getElementById("selAsmYearly_" + code + '_' + i).value;
                //kalo sama year selectnya
                if (year == period_proyeksi){
                    if (src == "Growth"){
                        growth = document.getElementById("txtAsmYearlyGrowth_" + code + '_' + period_proyeksi).value.replace(/,/g, "");
                        growth_pos = document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value.replace(/,/g, "");
                        
                        if (data != ""){
                            if (growth != ""){
                                res = growth * data / 100;
                                //document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value = formatCurrency(data * 1 + res * 1);
                                data_period = data * 1 + res * 1;
                                if (growth_pos != ""){
                                    res = growth_pos * data_period / 100;
                                    document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = formatCurrency(data_period * 1 + res * 1);
                                }
                                else{
                                    document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = "";
                                }
                                
                            }
                            else{
                                document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = "";
                            }
                        }
                    }
                    if (src == "Amount"){
                        data_period = document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value.replace(/,/g, "");
                        amount = document.getElementById("txtAsmYearlyAmount_" + code + '_' + period_proyeksi).value.replace(/,/g, "");
                        amount_pos = document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value.replace(/,/g, "");
                        growth_pos = document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value.replace(/,/g, "");

                        if (rowHis > 0){
                            if (data != ""){
                                if (amount != ""){
                                    if (growth_pos != ""){
                                        res = growth_pos * data_period / 100;
                                        document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = formatCurrency(data_period * 1 + res * 1);
                                    }
                                    else if (amount_pos != "" && growth_pos == ""){
                                        res = (amount_pos * 1 - data_period * 1) / data_period * 100;
                                        document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value = parseFloat(res).toFixed(2);
                                    }
                                    else{
                                        document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value = "";
                                        document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = "";
                                    }
                                }
                                else{
                                    document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value = "";
                                    document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = "";
                                }
                            }
                        }
                        else if (rowHis == 0 ){
                            if (data != ""){
                                if (amount != ""){
                                    if (growth_pos != ""){
                                        res = growth_pos * data_period / 100;
                                        document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = formatCurrency(data_period * 1 + res * 1);
                                    }
                                    else if (amount_pos != "" && growth_pos == ""){
                                        res = (amount_pos * 1 - data_period * 1) / data_period * 100;
                                        document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value = parseFloat(res).toFixed(2);
                                    }
                                    else{
                                        document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value = "";
                                        document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = "";
                                    }
                                }
                                else{
                                    document.getElementById("txtAsmYearlyGrowth_" + code + '_' + i).value = "";
                                    document.getElementById("txtAsmYearlyAmount_" + code + '_' + i).value = "";
                                }
                            }
                        }
                    }
                    monthDist(code, i);                   
                }
                //end kalo sama year selectnya
            }
            //end loop cek efek
        }

        function monthDist(code,period){
            var param = document.getElementById("selParam_" + code + "_" + period).value;
            var val = document.getElementById("txtAsmYearlyAmount_" + code + "_" + period).value;
            document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value = val;           
            var total = val.replace(/,/g, "");

            if (param == "P2"){
                for (var i = 1; i <= 12; i++){
                    document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value = parseFloat(100/12).toFixed(2);
                    document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).style.readonly = true;
                    document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).className = "inpTxtNumDisabled";
                    document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "100.00";
                    
                    var growth = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value.replace(/,/g, "");

                    if (total != ""){
                        var tmp = total / 12;
                        document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = formatCurrency(tmp);
                    }
                }
            }
            else if (param == "P3" || param == "P4" || param == "P5"){
                var brwcode = document.getElementById("<%= brw_code.clientID %>").value;
                
                document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "";
                document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value = document.getElementById("txtAsmYearlyAmount_" + code + "_" + period).value;

                var totalAmt = document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value.replace(/,/g, "");
                for (var i = 1; i <= 12; i++){
                    var str = brwcode + ',' + code + ',' + period + ',' + i + "," + param;
                    PageMethods.getDistribution(str,OnSuccess,OnFailure);

                    var grw = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value.replace(/,/g, "");
                    if (grw != ""){
                        document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = formatCurrency(grw * totalAmt / 100);
                    }
                }
               
               setTimeout(function(){
               var total_grw = document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value.replace(/,/g, "");
                var last_year = period * 1 - 1;
                var check = document.getElementById("selParam_" + code + "_" + last_year);

                if (check != null ) {
                var param_last_year = document.getElementById("selParam_" + code + "_" + last_year).value;
                    if (total_grw == ""){
                        for (var i = 1; i <= 12; i++){
                            var grw = document.getElementById("txtAsmDistGrowth_" + code + "_" + last_year + "_" + i).value.replace(/,/g, "");
                            document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value = document.getElementById("txtAsmDistGrowth_" + code + "_" + last_year + "_" + i).value.replace(/,/g, "");
                            if (grw != "" && param_last_year != "P2"){
                                document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = formatCurrency(grw * totalAmt / 100);
                                total_grw = total_grw * 1 + parseFloat(grw).toFixed(2) * 1;
                            }
                            else if (grw != "" && param_last_year == "P2"){
                                document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = formatCurrency(totalAmt / 12);
                            }
                        }
                        document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = document.getElementById("txtAsmDistGrowthTot_" + code + "_" + last_year).value.replace(/,/g, "");
                    }     
                }  else{
                    document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "";
                    for (var i = 1; i <= 12; i++){
                            document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value ="";
                            document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value="";
                        }
                }                
               },1000);             
            }
            else{
                for (var i = 1; i <= 12; i++){
                    document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value = "";
                    document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = "";
                    enabledTextbox("txtAsmDistGrowth_" + code + "_" + period + "_" + i);
                    enabledTextbox("txtAsmDistAmount_" + code + "_" + period + "_" + i);
                }
                document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "";
            }
        }

        function growthCalc(code,period,month){
            var total = 100;
            document.getElementById("selParam_" + code + "_" + period).value = "P1";
            var total_amount_yearly = document.getElementById("txtAsmYearlyAmount_" + code + "_" + period).value.replace(/,/g, "");
            var growth = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + month).value.replace(/,/g, "");
            var total_amount_dist = document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value.replace(/,/g, "");
            
            var total_growth_tmp = 0, val, index = 0;
            if (total_amount_dist != 0 && total_amount_dist != ""){
                
                for (var i = 1; i <= 12; i++){
                    val = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value.replace(/,/g, "");

                    if (val != ""){
                        document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = formatCurrency(val * total_amount_dist / 100);
                        total_growth_tmp = total_growth_tmp * 1 + val * 1;
                    }
                    else{
                        index++;
                        document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = "";
                    }
                }

                if (total_growth_tmp != 0)
                    document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = Math.round(total_growth_tmp).toFixed(2);
                else if (index == 12){
                    document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "";
                }
            }
            else if (total_amount_yearly != ""){
                document.getElementById("txtAsmDistAmount_" + code + "_" + period).value = formatCurrency(total_amount_yearly);
                total_amount_dist = total_amount_yearly;
                for (var i = 1; i <= 12; i++){
                    val = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value.replace(/,/g, "");

                    if (val != ""){
                        document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = formatCurrency(val * total_amount_dist / 100);
                        total_growth_tmp = total_growth_tmp * 1 + val * 1;
                    }
                    else{
                        index++;
                        document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value = "";
                    }
                }

                if (total_growth_tmp != 0)
                    document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = Math.round(total_growth_tmp).toFixed(2);
                else if (index == 12){
                    document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "";
                }
            }
            
        }

        function amountCalc(code,period,month){
            document.getElementById("selParam_" + code + "_" + period).value = "P1";
            var total = document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value.replace(/,/g, "");
            var amount = document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + month).value.replace(/,/g, "");

            if (amount != "" && (parseInt(total) != 0 && total != "") ){
                var tmp = amount / total * 100;
                document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + month).value = parseFloat(tmp).toFixed(2);
            }
            else{
                document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + month).value = "";
            }
            
            
            var  val, total_tmp = 0;
            for (var i = 1; i <= 12; i++){
                val = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value.replace(/,/g, "");
                total_tmp = total_tmp * 1 + document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value.replace(/,/g, "") * 1;
                
                //document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value = "";
                enabledTextbox("txtAsmDistGrowth_" + code + "_" + period + "_" + i);
                enabledTextbox("txtAsmDistAmount_" + code + "_" + period + "_" + i);
            }
            
            if (total_tmp != 0){
                document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value = formatCurrency(total_tmp);
            }
            else{
                document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value = "";
            }

            var grw = "", total_growth_tmp = 0, index = 0; 
            val = "";

            for (var i = 1; i <= 12; i++){
                val = document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value.replace(/,/g, "");
                if (total_tmp != 0){
                    grw = val / total_tmp * 100;                    
                }
                
                if (grw != ""){
                    total_growth_tmp = total_growth_tmp * 1 + grw * 1;
                    document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + i).value = parseFloat(grw).toFixed(2);
                }
                else{
                    index++;
                }
                
            }
            
            if (total_growth_tmp != 0){
                document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = Math.round(total_growth_tmp).toFixed(2);
            }
            else if (index == 12){
                document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = "";
            }
        }

        function enabledTextbox(code){
            document.getElementById(code).readonly = false;
            document.getElementById(code).className = "inpTxtNum";
        }

        function OnSuccess(response){
            var code = response[1];
            var period = response[2];
            var month = response[3];
            
            var totalGrw = "", totalAmt = "";
            var totalPct ="";
            
            if (isNumber(response[5]) != false){
                totalGrw = document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value * 1 + parseFloat(response[5]).toFixed(2) * 1;
//                console.log('period:'+period,totalGrw,' : ',document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value * 1,' - ',parseFloat(response[5]).toFixed(2) * 1);
                document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + month).value = parseFloat(response[5]).toFixed(2);
                if (totalGrw.toFixed(2)=="99.96") {
                    totalPct='100.00';    
                }
                else{
                    totalPct=totalGrw.toFixed(2);
                }
                document.getElementById("txtAsmDistGrowthTot_" + code + "_" + period).value = totalPct;
            }
//            if (response[5] != ""){
//                totalAmt = document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value.replace(/,/g, "") * 1 + response[5] * 1;
//                document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + month).value = formatCurrency(response[5]);
//                document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value = formatCurrency(totalAmt);
//            }
        }
        function OnFailure(response){
        }
//        function growthCalcByAmount(code,period,month){
//            var total = document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value.replace(/,/g, "");
//            var growth = document.getElementById("txtAsmDistGrowth_" + code + "_" + period + "_" + month).value.replace(/,/g, "");
//            var total_tmp;
//            var tmp;
//            for (var i = 1; i <= 12; i++){
//                total_tmp = total_tmp + document.getElementById("txtAsmDistAmount_" + code + "_" + period + "_" + i).value.replace(/,/g, "");
//            }
//            document.getElementById("txtAsmDistAmountTot_" + code + "_" + period).value = formatCurrency(total_tmp);
//        }
//        function fGenerate(){
//            var brwcode = document.getElementById("<%= brw_code.clientID %>").value;
//            var start = document.getElementById("<%= ddlStartYear.clientID %>").value;
//            var end = document.getElementById("<%= ddlEndYear.clientID %>").value;
//            var asumsi = document.getElementById("<%= ddlAsumsi.clientID %>").value;
//            var str = brwcode + "," + start + "," + end + "," + asumsi;
//            
//            PageMethods.generateFormHistoris(str,OnSuccessHistoris,OnFailure);
//            //PageMethods.generateFormAverage(str,OnSuccessAverage,OnFailure);

//            //var rowHis = document.getElementById("hdRowHis").value;alert(rowHis);
//            str = str + ",3"; //+ rowHis;
//            
//            //PageMethods.generateFormAsumsiYearly(str,OnSuccessAsumsiYearly,OnFailure);
//            return false;
//        }
//        function OnSuccessHistoris(response){
//            document.getElementById("historis_content").innerHTML = response;
//            return false;
//        }
//        function OnSuccessAverage(response){
//            document.getElementById("average_content").innerHTML = response;
//            return false;
//        }
//        function OnSuccessAsumsiYearly(response){
//            document.getElementById("asumsi_yearly_content").innerHTML = response;
//            return false;
//        }
//        function checkApp(){
//            var appno = document.getElementById("<%= txtAppNo.clientID %>").value;
//            PageMethods.checkApp(appno,OnSuccess,OnFailure);
//            return false;
//        }
//        function OnSuccess(response){
//            var splits = response.split(",");
//            document.getElementById("<%= brw_code.clientID %>").value = splits[0];
//            document.getElementById("<%= txtNamaPemohon.clientID %>").value = splits[1];
//            document.getElementById("<%= brw_sector.clientID %>").value = splits[2];
//            document.getElementById("<%= txtSector.clientID %>").value = splits[3];
//            
//            PageMethods.checkListAsumsi(splits[2],OnSuccess2,OnFailure);
//        }
//        function OnSuccess2(response){
//            document.getElementById("<%= ddlAsumsi.clientID %>").innerHTML = "";
//            var select = document.getElementById("<%= ddlAsumsi.clientID %>");
//            for (var i = 1; i < response.length; i++) {
//                var splits = response[i].split(",");
//                //document.getElementById("<%= ddlAsumsi.clientID %>").append = "<option value='" + splits[0] + "'>" + splits[1] + "</option>";
//                var opt = document.createElement('option');
//                opt.value = splits[0];
//                opt.innerHTML = splits[1];;
//                select.appendChild(opt);
//            }
//        }
//        
//        
    </script>
    <style type="text/css">
        .notif
        {
            margin-bottom: 10px;
            border: 1px solid #bcbcbc;
            border-radius: 5px;
        }
        .notif-header
        {
            border-top-right-radius: 5px;
            border-top-left-radius: 5px;
            font-size: 12px;
            background: #007cc2;
            color: #fff;
        }
        .notif-body
        {
            border-bottom-right-radius: 5px;
            border-bottom-left-radius: 5px;
            background: #ebecec;
            padding: 5px;
            font-size: 12px;
        }
        a.notif-close
        {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            width: 1%;
        }
        .aactive
        {
            background-color: transparent;
            border-radius: 11px;
            -moz-border-radius: 11px;
            -webkit-border-radius: 11px;
            color: #fff;
            background-color: #ebebeb;
            border: thin solid #cccccc;
        }
        
        .tbl
        {
            font-family: "Century Gothic" , "Helvetica Neue" , "Lucida Grande" , "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
            font-size: 11px;
        }
        
        .spanChild
        {
            margin-left: 10px;
        }
        #ui-datepicker-div
        {
            display: none;
        }
        
        .fieldset_content
        {
            margin-top: 10px;
            border-radius: 5px;
            background: #D8E9FA;
        }
        #div_content
        {
            width: 100%;
            margin-top: 10px;
        }
        #historis_info_content
        {
            width: 100%;
            margin-top: 10px;
        }
        #historis_content
        {
            width: 45%;
            margin: 0 3% 0 0;
            position: relative;
            float: left;
        }
        #average_content
        {
            width: 45%;
            margin: 0 0 0 3%;
            position: relative;
            float: left;
        }
        #asumsi_yearly_content
        {
            width: 100%;
        }
        #asumsi_dist_content
        {
            width: 100%;
            margin: 0 auto;
            padding: 0;
        }
        #tblAsmDist
        {
            width: 1800px !important;
            margin-top: 10px;
        }
        td .tdAsmDist
        {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border: 1px solid #ddd;
        }
        th .thAsmDist
        {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border: 1px solid #ddd;
        }
        .inpTxtNum
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 2px 4px;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 13px;
            float: left;
            width: 150px;
        }
        .inpTxtNumDisabled
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 2px 4px;
            color: #2b4c61;
            font-family: century gothic;
            font-size: 13px;
            float: left;
            background-color: #ebebe4;
            pointer-events: none;
            cursor: default;
            width: 150px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <input type="hidden" id="brw_sector" runat="server" />
    <input type="hidden" id="hdRowHis" runat="server" />
    <div style="clear: both; margin: 0 auto; padding: 0; width: 98%; margin-bottom: 20px;">
        <table cellspacing="0" cellpadding="3" width="60%" style="text-align: left; border: thin solid #bcbcbc;
            border-radius: 5px; margin-top: 5px;">
            <tr>
                <td colspan="2" background="../../Images/bg-head.gif" style="border-radius: 5px 5px 0 0;">
                    &nbsp;
                </td>
            </tr>
            <tr bgcolor="#f6f7f7">
                <td width="30%">
                    <b>No Aplikasi</b>
                </td>
                <td>
                    <asp:TextBox ID="txtAppNo" runat="server" CssClass="inpTxt"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#ebecec">
                <td>
                    <b>Nama Pemohon</b>
                </td>
                <td>
                    <asp:TextBox ID="txtNamaPemohon" runat="server" CssClass="inpTxt" TextMode="MultiLine"
                        Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#f6f7f7">
                <td>
                    <b>Sector</b>
                </td>
                <td>
                    <asp:TextBox ID="txtSector" runat="server" CssClass="inpTxt"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#ebecec">
                <td>
                    <b>Asumsi</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAsumsi" runat="server" CssClass="inpDdl">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr bgcolor="#f6f7f7">
                <td>
                    <b>Proyeksi Tahun</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStartYear" runat="server" CssClass="inpDdl" Style="float: none;">
                    </asp:DropDownList>
                    <span>&nbsp; To &nbsp;</span>
                    <asp:DropDownList ID="ddlEndYear" runat="server" CssClass="inpDdl" Style="float: none;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 30px">
                <td colspan="2" background="../../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="inpBtn"></asp:Button>
                </td>
            </tr>
        </table>
        <div id="div_content">
            <asp:Literal ID="litHistory" runat="server"></asp:Literal>
            <asp:Literal ID="litAsumsiYearly" runat="server"></asp:Literal>
            <asp:Literal ID="litAsumsiDistribution" runat="server"></asp:Literal>
        </div>
        <div style="margin-top: 10px;">
            <asp:Button ID="btnSave" runat="server" Text="Save Data" CssClass="inpBtn" Visible="false">
            </asp:Button>
        </div>
    </div>
</asp:Content>
