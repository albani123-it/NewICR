<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Proyeksi_PL.aspx.vb" Inherits="NewCRM.Proyeksi_PL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="../../Scripts/tableHeadFixer.js"></script>
    <script type="text/javascript" src="../../Scripts/idxlibs/idxwidget.js"></script>
    <script type="text/javascript">
        aMenu("Analysis");
        aBc("Kertas Kerja");
        
        $(document).ready(function () {
            $("#fixedHeader").tableHeadFixer({ "left": 2 });
		});

        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
        }

        function jsCalcTotalPerYear(period, code) {
            var total = 0;
            var temp;
            var val;
            var temp_val = "";
            for (var i = 1; i <= 12; i++){
                if (i < 10){
                    temp = period.substring(0,4) + "0" + i;
                }
                else{
                    temp = period.substring(0,4) + i;
                }
                val = document.getElementById("col_" + temp + "_" + code).value.replace(/\$|\,/g, '');
                temp_val = temp_val + document.getElementById("col_" + temp + "_" + code).value.replace(/\$|\,/g, '');
                
                total = total * 1 + val * 1;
            }
            total = formatCurrency(total);
            
            if (temp_val == "")
                document.getElementById("col_total_" + period.substring(0,4) + "_" + code).value = "";
            else
                document.getElementById("col_total_" + period.substring(0,4) + "_" + code).value = total;
        }
        function jsProyeksiPerPeriod(code,period,a, b, c) {
            idxwidget.lapkeuangancalc_proyeksi(code, a, b, c);
            jsCalcTotalPerYear(period, b);
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
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
        function updateData(period, code){
            var brw = document.getElementById("<%= txtBrwCode.ClientID %>").value;
            var values = document.getElementById("col_" + period + "_" + code).value.replace(/,/g, "");
            var val = brw + "," + period + "," + code + "," + values;
            //alert(val);
            //PageMethods.updateData(val,OnSuccess,OnFailure);
            //document.getElementById("col_" + period + "_" + code).value = ;
        }
        function OnSuccess(response){
            alert(response);
        }
        function OnFailure(response){
        }
    </script>
    <style type="text/css">
        #tblFix {
		    height: 500px;
		}
        #fixedHeader {
		    width: 1800px !important;
		}
        .notif
        {
            margin-bottom:10px;
            border: 1px solid #bcbcbc; 
            border-radius:5px;
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
        a.notif-close {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color:#fff;
            cursor:pointer;
            font-weight:bold;
            width: 1%;
        }
        .aactive
        {
            background-color:transparent;

            border-radius:11px;
            -moz-border-radius:11px;
            -webkit-border-radius:11px;
            color: #fff;
	
	        background-color:#ebebeb;
	        border:thin solid #cccccc;	
        }
        
        .tbl
        {
            font-family:"Century Gothic", "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            font-size:11px;
        }
        td
        {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border: 1px solid #ddd;
        }
        th
        {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border: 1px solid #ddd;
        }
        .spanChild
        {
            margin-left:10px;
        }
        #ui-datepicker-div {display: none;}
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <div style="clear:both; margin:0 auto; padding:0; width:100%">
        <div style="width:98%;margin:0 auto; padding-bottom:20px;">
            <span>Year:</span>
            <asp:DropDownList ID="ddlStartYear" runat="server" CssClass="inpDdl"></asp:DropDownList>
            <span>To</span>
            <asp:DropDownList ID="ddlEndYear" runat="server" CssClass="inpDdl"></asp:DropDownList>
            <span>Kode Borrower</span>
            <asp:TextBox ID="txtBrwCode" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
            <span>Sektor</span>
            <asp:TextBox ID="txtSector" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>
            <asp:button id="btnView" runat="server" Text="Generate" CssClass="inpBtn"></asp:button>
        </div>
        <div id="tblFix" style="width:98%;margin:0 auto; padding:0;">
            <asp:Literal ID="litBody" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>