<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="connection.aspx.vb" Inherits="NewCRM.connection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID ="txtServerName" />
        <asp:TextBox runat="server" ID ="txtDatabaseName" />
        <asp:TextBox runat="server" ID ="txtUser" />
        <asp:TextBox runat="server" ID ="txtPassword" />
        <asp:TextBox runat="server" ID ="txtMaxPool" />
        <asp:TextBox runat="server" ID ="txtTimeOut" />
        <asp:Button runat="server" ID="btnTestConnection" Text="Test Connection" />        
    </div>
    </form>
</body>
</html>
