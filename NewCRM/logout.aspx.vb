Public Class logout
    Inherits System.Web.UI.Page
    Dim lUsr As New lUsers
    Dim llog As New lLog
    Dim baseUrl As String = VirtualPathUtility.ToAbsolute("~/")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        logoutProcess()
    End Sub

    Private Function logoutProcess()
        FormsAuthentication.SignOut()
        If Session("UserLoginID") <> "" Then

            lUsr.insertLoginLog(Session("UserLoginID"), "Logout")

            'user track
            With New lLog
                Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                .log_user_tracking("ua", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Logout by User", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
            End With

            Session.Abandon()
            liBodyLoadAction.Text &= "window.location='" & baseUrl & "login';"
        Else
            If Request.Cookies("crmcookie") IsNot Nothing Then

                Dim userlogin As String
                If (Request.Cookies("crmcookie")("user_login") IsNot Nothing) Then
                    userlogin = Request.Cookies("crmcookie")("user_login")
                    lUsr.insertLoginLog(userlogin, "Logout")

                    'user track
                    With New lLog
                        Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                        .log_user_tracking("ua", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "User session time out", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                    End With

                    liBodyLoadAction.Text &= "window.location='" & baseUrl & "login';"
                Else
                    liBodyLoadAction.Text &= "window.location='" & baseUrl & "login';"
                End If
            Else
                liBodyLoadAction.Text &= "window.location='" & baseUrl & "login';"
            End If
        End If
    End Function

End Class