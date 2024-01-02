Public Class connection
    Inherits System.Web.UI.Page
    Dim dbCon As New dbConn
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        Dim server As String = txtServerName.Text
        Dim db As String = txtDatabaseName.Text
        Dim user As String = txtUser.Text
        Dim password As String = txtPassword.Text
        Dim maxPoolSize As String = txtMaxPool.Text
        Dim timeOut As String = txtTimeOut.Text

        If dbCon.getConStrDyn(server, db, user, password, maxPoolSize, timeOut) = True Then
            Response.Write("Success")
        Else
            Response.Write("Gagal")
        End If



    End Sub
End Class