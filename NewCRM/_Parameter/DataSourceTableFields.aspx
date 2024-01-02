<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DataSourceTableFields.aspx.vb" Inherits="NewCRM.DataSourceTableFields" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function getSelectValues(select,src) {
          var result = [];
          var options = select && select.options;
          var opt;

          for (var i=0, iLen=options.length; i<iLen; i++) {
            opt = options[i];

            if (opt.selected) {
                if (src == "operator")
                    result.push( opt.value || opt.text);
                else
                    result.push("[" + opt.value + "]" || opt.text);
            }
          }
          return result;
        }
        function PasteMe()
         {
            var source = document.getElementById("SourceTable").value;
            var fields =  getSelectValues(document.getElementById('lstParameters'), source);

            var list = document.getElementById('lstParameters');
            var indx = list.selectedIndex;
            var rule = list[indx].value;
            if (rule = "") 
            {
                alert('Please select an item');
            }
            else
            {
                var destination =  document.getElementById("DestinationControl").value;
                
                if (source == "operator"){
                    window.opener.popupCallback(fields,destination,"operator");
                }
                else{
                    window.opener.popupCallback(fields,destination,"");
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
				    
                    <td colspan="2" bgcolor="#2A3F54" style="border-radius: 10px 10px 0 0">
                        &nbsp;</td>
			    </tr>
            
                    <tr bgcolor="#F6F5F0">
                   <td class="style3"><p>Select 
                       <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></p>
                    </td> 
                </tr>
               
     
                 <tr bgcolor="#efefef">
                
                <td>
                
                        <asp:ListBox ID="lstParameters"  ondblclick="PasteMe();" runat="server" SelectionMode="Multiple"
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
