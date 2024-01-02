<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ruleDetValues.aspx.vb" Inherits="NewCRM.ruleDetValues" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.hotkeys.js"></script>
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
        .style1
        {
            width: 22%;
            font-family: "Century Gothic";
        }
        .style2
        {
            width: 60%;
            font-family: "Century Gothic";
        }
       
        .style3
        {
            width: 11%;
            font-family: "Century Gothic";
        }
       
       .liststyle
       {
            font-family: "Century Gothic";
            width:300px; 
            height:200px;
       }
       p
       {
           font-family: "Century Gothic";
          font-size:16px;
         font-weight:bold;
       }
    </style>
    <script type="text/javascript">
        function CloseMe()
         {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }	
        
        function PasteMe()
        {
            
            var list = document.getElementById('lstParameters');

            var indx = list.selectedIndex;
            
            
            if (indx == -1){
                alert('Please select an item');
            }
            else{
                var fields = list.value;
                var destination =  document.getElementById("DestinationControl").value;
                var source = document.getElementById("SourceTable").value;
                if (source == "operator"){
                    window.opener.popupCallbackField(fields,destination,"operator");
                }
                else{
                    window.opener.popupCallbackField(fields,destination,"");
                }
                this.close();
            }
        }

     	function fLoad()
        {
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		} 
        
    </script>
</head>
<body onload="fLoad()">
    <form id="form1" runat="server">
        <input type="hidden" id="SourceTable" name="SourceTable" runat="server"/>
        <input type="hidden" id="DestinationControl" name="DestinationControl" runat="server"/>
    <div>
        <center>
            <table cellspacing="1" cellpadding="1" width="98%" style="text-align:left">
			    <tr>
				    
                        <td colspan="2"  bgcolor="#2A3F54" style="border-radius: 10px 10px 0 0">
                        &nbsp;</td>
			    </tr>
            
                    <tr bgcolor="#F6F5F0">
                   <td class="style3"><p>Select 
                       <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></p>
                    </td> 
                </tr>
               
     
                 <tr bgcolor="#efefef">
                
                <td>
                
                        <asp:ListBox ID="lstParameters"  ondblclick="PasteMe();" runat="server" SelectionMode="Single"
                         Rows="5" class="liststyle">
                        </asp:ListBox >
                </td>
                </tr>
                <tr>
				    <td colspan="2" background="../Images/bg-head.gif" style="padding-left:10px">
                        <asp:button id="btnSave" runat="server" Text="Select" CssClass="inpBtn" onclientclick="PasteMe();"></asp:button>&nbsp;
					    <asp:button id="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="inpBtn" onclientclick="CloseMe()"></asp:button>
                    </td>
			    </tr>
     
            </table>
       
        </center>
    </div>
    </form>
</body>
</html>
