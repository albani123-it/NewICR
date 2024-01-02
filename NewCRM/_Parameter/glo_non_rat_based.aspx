<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="glo_non_rat_based.aspx.vb" Inherits="NewCRM.glo_non_rat_based" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
	    .ob_gC, .ob_gHContWG .ob_gH .ob_gCW {
            background-image: url(../Styles/btn_bank/header.gif);
            height: 33px;
            font-family: Tahoma;
            font-size: 10px;
            font-weight: bold;
            color: #242500;
            cursor: pointer;
            text-align:left;
        }
        
        .ob_gFCont {
            background-color: #F6F5F0;
            padding: 0px;
            margin-top: 0px;
            border: 0px;
            height: 27px;
            padding-left: 20px;
            padding-top: 0px;
            padding-bottom: 0px;
            vertical-align: middle;
            position: relative;
            cursor: default;
            font-family: Verdana;
            font-size: 10px;
            color: #5E5E70;
            white-space: nowrap;
            z-index: 0;
            text-align: left;
        }
        a {
            font-family: Verdana;
            font-size: 10px;
            color: #1F4697;
            font-weight: bold;
            text-decoration: none;
        }
        a:hover
        {
	        font-family: Verdana;
	        font-size: 10px;
	        color:#5E5E70;
	        font-weight: bold;
	        text-decoration:underline;
}
        td 
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: normal;
            text-align: left;
            vertical-align: middle;
            color: #5E5E70;
            height:23px;
        }
    </style>

    <script type="text/javascript">
            function oWindow(url, w, h) {
                var width = w;
                var height = h;
                var left = parseInt((screen.availWidth / 2) - (width / 2));
                var top = parseInt((screen.availHeight / 2) - (height / 2));
                var windowFeatures = "width=" + width + ",height=" + height + ",status,scrollbars,resizable=no,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                myWindow = window.open(url, "subFire", windowFeatures);
            }
		    function fEdit(gnr_id) {
		        var l_obj_argument = new Object();
		        l_obj_argument.url = "glo_non_rat_based_det.aspx?gnrID=" + gnr_id;
                oWindow(l_obj_argument.url, 800, 480);
                //window.open(l_obj_argument.url,600,480);	     
		    }
		    
		    function fDelete(gnr_id) {
		        var result = confirm("Want to delete?");
                if (result) {
                    var l_obj_argument = new Object();
		        l_obj_argument.url = document.URL + "&gnrID="+gnr_id;
		        location.href=l_obj_argument.url;
                }		    
		     }
		
		    function fGlobalNew() {
                var l_obj_argument = new Object();
		        l_obj_argument.url = "glo_non_rat_based_det.aspx";
                window.open(l_obj_argument.url,10,20);
//                oWindow(l_obj_argument.url, 100, 180);
		    }

		    function CloseMe() {		
			window.returnValue = window.dialogArguments;
			event.returnValue = false;
			window.close();
			
		}	
		
		    function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
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
            <table width="100%" cellspacing="0" style="text-align:left;border: thin solid #ccc;font-family: Century Gothic;font-size: 11px;height: 22px;" background="../Styles/btn_bank/header.gif" >
                <tr style="height:33px;font-weight:bold;">
                    <td width="75%" class="ob_gC">Jenis Tagihan</td>
                    <td width="15%" class="ob_gC">Risk Weight</td>
                    <td width="15%" class="ob_gC">Action</td>
                </tr>
            </table>
            <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Verdana;font-size: 10px;color:#5E5E70;height: 22px;">
							       
	                                <%  
	                                    Dim htmlView As String = "" 
	                                    Try
	                                        
	                                    
	                                        If dtParent.Rows.Count > 0 Then
	                                            For i As Integer = 1 To dtParent.Rows.Count
	                                                If i Mod 2 = 1 Then
	                                                    htmlView += "<tr style='color: rgb(0, 0, 0); background-image: none; background-color: rgb(246, 245, 240);'> "
	                                                Else
	                                                    htmlView += "<tr style='color: rgb(0, 0, 0); background-image: none; background-color: rgb(239, 237, 226);'> "
	                                                End If
	                                            
	                                                htmlView += "<td colspan='2'><input type='hidden' id='hidGnrID' text='" & dtParent.Rows(i - 1).Item("gnr_id") & "'>" & dtParent.Rows(i - 1).Item("gnr_billing_type") & "</td>"
	                                                htmlView += "<td align='center'>" & dtParent.Rows(i - 1).Item("gnr_rwa") & "</td>"
	                                                htmlView += "<td><a href='#' onClick='fEdit(" & dtParent.Rows(i - 1).Item("gnr_id") & ")'>Edit</a> | <a href='#' onClick='fDelete(" & dtParent.Rows(i - 1).Item("gnr_id") & ")'>Delete</a></td>"
	                                                htmlView += "</tr>"
	                                                Try
	                                                    getChild(dtParent.Rows(i - 1).Item("gnr_id"))
	                                                    If dtChild.Rows.Count > 0 Then
	                                                        For u As Integer = 1 To dtChild.Rows.Count
	                                                            If i Mod 2 = 1 Then
	                                                                htmlView += "<tr style='color: rgb(0, 0, 0); background-image: none; background-color: rgb(246, 245, 240);'> "
	                                                            Else
	                                                                htmlView += "<tr style='color: rgb(0, 0, 0); background-image: none; background-color: rgb(239, 237, 226);'> "
	                                                            End If
	                                                            'htmlView += "<tr class='ob_gRA' style='color: rgb(0, 0, 0); background-image: none; background-color: rgb(243, 233, 208);'> "
	                                                            htmlView += "<td width='30%'><input type='hidden' id='hidGndID' text='" & dtChild.Rows(u - 1).Item("gnd_id") & "'></td>"
	                                                            htmlView += "<td width='45%'>" & dtChild.Rows(u - 1).Item("gnd_billing_Type") & "</td>"
	                                                            htmlView += "<td width='15%'>" & dtChild.Rows(u - 1).Item("gnd_rwa") & "</td>"
	                                                            htmlView += "<td width='10%'>&nbsp;</td>"
	                                                            htmlView += "</tr>"
	                                                        Next
	                                                    End If
	                                                Catch ex As Exception

	                                                End Try
	                                            
	                                            Next
	                                        End If
	                                        
	                                    Catch ex As Exception

	                                    End Try
	                                    
	                                    %>
	                                    
	                                    <%=htmlView%>                              
	                                    
            </table>
            <table width="100%" cellspacing="0" style="border: thin solid #ccc;font-family: Century Gothic;font-size: 11px;height: 22px;">
                <tr style="background-color:#F6F5F0;height:33px;font-weight:bold;">
                    <td colspan="7" style="height:33px;font-weight:bold;text-align:right;padding: 0 20px;"><a href="javascript:fGlobalNew();"><span style="color:#D99E00;">Add New</span></a></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
