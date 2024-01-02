Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim usrlogin As String = Session("UserLoginID")
        If usrlogin = "" Then
            FormsAuthentication.SignOut()
            Response.Redirect("login")
        End If
    End Sub
End Class