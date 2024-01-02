<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="text_rule.aspx.vb" Inherits="NewCRM.text_rule" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
    </style>
    <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }

		function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function dhanysuramoz(){
            var fields =  document.getElementById("temp1").value;
            var destination = document.getElementById("DestinationControl").value;
            window.opener.popupCallback(fields,destination);
            this.close();
            //alert(1);
                //this.close();
//		    var obj = window.dialogArguments;
//		    obj.returnvalue = document.getElementById("temp1").value;
            
		}
    </script>
</head>
<body MS_POSITIONING="GridLayout" onload="fLoad();" style="background:#A6C3FF;">
    <form id="form1" runat="server">
    <input id="temp1" type="hidden" runat="server" />
    <input type="hidden" id="DestinationControl" name="DestinationControl" runat="server"/>
    <div>
        <%If op = "in" Or op = "not in" Then%>
		<asp:CheckBoxList ID="chkbox" runat="server" Visible="false">
        </asp:CheckBoxList>
		<%Else%>
        <asp:RadioButtonList ID="chkradio" runat="server" Visible="false">
        </asp:RadioButtonList>
        <% End If%>
		<br />
		<asp:button id="btnSave" runat="server" Text="Submit"></asp:button>
    </div>
    </form>
</body>
</html>
