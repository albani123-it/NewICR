<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LapKeuanganDet.aspx.vb"
    Inherits="NewCRM.LapKeuanganDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../../Scripts/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <script type="text/javascript" src="../../Scripts/timepicker.js"></script>
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script type="text/javascript" src="../../Scripts/idxlibs/idxwidget.js"></script>
    <style type="text/css">
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
        input.fieldname
        {
            float: left;
            display: block;
            margin: 5px 5px 5px 0px;
            width: 180px;
        }
        
        .select2-results ul li
        {
            float: none !important;
        }
        
        .select2-container--default .select2-selection--single
        {
            border: none !important;
        }
        
        .select2-dropdown
        {
            border: none;
            padding-left: 5px;
            padding-bottom: 5px;
        }
        .detail
        {
            background-image: url("../../Images/bg-head.gif");
            background-color: rgb(221, 237, 246);
            color: White;
        }
        
        .coba
        {
            width: 200px !important;
        }
        .inpTxt
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62); /*behavior:url(scripts/PIE.htc);*/
            border-radius: 7px;
            border: thin solid #aaaaaa;
            padding: 1px 4px; /* background: #f5f5f5; */
            color: #2b4c61;
            font-family: century gothic;
            font-size: 12px; /*position:relative;       	*/
        }
        
        .inpDdl
        {
            font-family: Century Gothic;
            font-size: 11px; /* background:#f5f5f5; */
            border-color: #aaaaaa;
            color: #2b4c61;
        }
        function jscalc(a, b)
        {
        }
        
        #tblNeracaForm
        {
            width: 100%;
        }
        
        #tblneraca
        {
            width: 100%;
            border-collapse: collapse;
        }
        #tblneraca tr:nth-child(event)
        {
            background-color: #f6f7f7;
        }
        #tblneraca tr:nth-child(odd)
        {
            background-color: #ebecec;
        }
    </style>
    <script type="text/javascript">
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function jscalc(a, b, c) {
           // console.log(a,b,c);
            idxwidget.lapkeuangancalc(a, b, c);
        }

        function jscalc_pct(a, b) {
            idxwidget.lapkeuangancalc_pct(a, b);
        }

        function isNumberKey(event) {
            var x = idxwidget.isnumber(event)
            if (x) {
                return true;

            } else {
                return false;
            }
        }

        function callFunctionCall(summary)
        {
            var dtArray = summary.split(';');
             for (i = 0; i < dtArray.length-1; i++) { 
                eval(dtArray[i]);
             }
        }

        function callFunctionCallPct(summary)
        {
            var dtArray = summary.split(';');
             for (i = 0; i < dtArray.length-1; i++) { 
                eval(dtArray[i]);
             }
        }

        function checkbalance(){
            var balance ; 
            var check1 =(form1.action).split('&');
            if (check1[3]=='report=Neraca'){
                if(check1[2]=='sektor=4'){
                   balance = $('#X00421').val();
                   if (parseFloat(balance).toFixed(2) != parseFloat("0.00").toFixed(2) ) {
                        alert('Laporan neraca tidak balance');
                        return false;
                    }else if (parseFloat(balance).toFixed(2) != parseFloat("0").toFixed(2) ) {
                        alert('Laporan neraca tidak balance');
                        return false;
                    }
                    else{
                        return true;
                    }
                }else{
                    balance = $('#X00420').val();
                    if (parseFloat(balance).toFixed(2) != parseFloat("0.00").toFixed(2) ) {
                        alert('Laporan neraca tidak balance');
                        return false;
                    }
                    else if (parseFloat(balance).toFixed(2) != parseFloat("0").toFixed(2) ) {
                        alert('Laporan neraca tidak balance');
                        return false;
                    }
                    else{
                     return true;
                    }
                }
            }else{
                return true;
            }
           
        }

        $(document).ready(function(){
            $('#<%= txtDate.ClientID %>').datepicker({
                 duration: '',
                 dateFormat: 'dd-mm-yy',
                 constrainInput: true
           });

           $('.clsInput').blur(function(){
                var summary = $('#inputSummary').val();
                var summaryPct = $('#inputSummaryPct').val();
                callFunctionCall(summary.replace(/'/g, ""));
                callFunctionCallPct(summaryPct.replace(/'/g, ""));
           });
        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblNeracaForm" class="tbl" cellspacing="0" cellpadding="3" width="98%"
            style="text-align: left; border: thin solid #bcbcbc;">
            <tr>
                <td colspan="2" class="detail">
                    <span style="font-size: 14px; font-weight: bolder">Laporan Keuangan</span>
                </td>
            </tr>
            <tr id="rowSummary" style=" display:none">
                <td colspan="2">
                    <input id='inputSummary' runat="server"/>
                </td>
            </tr>
            <tr id="rowSummaryPct" style=" display:none">
                <td colspan="2">
                    <input id='inputSummaryPct' runat="server"/>
                </td>
            </tr>
            <tr>
                <td  style="width: 20%">
                    <b>Status</b>&nbsp;
                </td>
                <td  style="width: 80%">
                    <asp:DropDownList ID="ddlStatus" runat="server"  AutoPostBack="True"  CssClass="inpDdl">
                        <asp:ListItem>Pilih Satu</asp:ListItem>
                        <asp:ListItem>Audited</asp:ListItem>
                        <asp:ListItem>Unaudited</asp:ListItem>
                        <asp:ListItem>Proyeksi</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="rowPeriode" runat="server" style="display:none">
                <td>
                    <b>Periode</b>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddltanggal" name="ddltanggal" AutoPostBack="True" CssClass="inpDdl"
                        runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="rowTgl" runat="server"  style="display:none">
                <td>
                    <b>Tanggal</b>&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" CssClass="inpTxt"></asp:TextBox>
                </td>
            </tr>
            
            <tr id="rowBtn" runat="server" style="display:none">
                <td colspan="2">
                    <asp:Button ID="btnview" runat="server" Text="View Report" CssClass="inpBtn" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <% =html %>
                </td>
            </tr>
            <tr style="height: 30px" colspan="2">
                <td background="../../Styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                    <%--<asp:Button ID="btnsave" runat="server" Text="Simpan" CssClass="inpBtn" />--%>
                    <asp:Button ID="btnsave" runat="server" Text="Simpan" CssClass="inpBtn" OnCommand="SaveData" OnClientClick="javascript: return checkbalance();" />
                    <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="inpBtn" OnClientClick="CloseMe();">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
