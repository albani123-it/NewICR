Public Class systems
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("-", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Page System", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With
            Catch ex As Exception
                Session.Abandon()
                Response.Redirect("../Login")
            End Try
        End If
    End Sub

End Class