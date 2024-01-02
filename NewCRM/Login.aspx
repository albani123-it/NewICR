<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="NewCRM.Login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="~/Content/Themes1/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
	<link href="~/Content/Themes1/build/css/login.css" rel="stylesheet" />

    <!-- Sweet -->
    <script src="/NewCRM/Content/Themes1/sweet/sweetalert.min.js"></script>
    <link href="~/Content/Themes1/sweet/sweetalert.css" rel="stylesheet" />

    <script type="text/javascript">
    	function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
    </script>

</head>

<body onload="fLoad()">
   <div class="container login-form">
    <section class="container login-form">
		<section>
			<form runat="server" method="post">
				<img src="Images/SkyworxNewICR.png" alt="" class="img-responsive" />
			
				<div class="form-group">
                    <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" required="required" placeholder="Enter User ID"></asp:TextBox>
				</div>
				
				<div class="form-group">
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" required="required" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
				</div>
				<asp:button id="btnLogin" runat="server" Text="Login" class="btn btn-primary btn-block"></asp:button>
			</form>
		</section>
	</section>
	</div>
       <br />  <br />  <br />  <br />  <br />  <br /> <br /><br /><br /><br />

    </body>
</html>
