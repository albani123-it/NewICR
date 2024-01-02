Public Class assignment
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oAssign As New lAssignment
    Dim oUsr As New lUsers
    Dim appno As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim stat As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Request.QueryString("appno") IsNot Nothing Then
            appno = Request.QueryString("appno")
            txtAppno.Text = Request.QueryString("appno")
            If Not IsPostBack Then
                isiData(Request.QueryString("appno"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_tracking" Then
                    tf = True
                End If
            Next

            If Not tf Then
                Session.Abandon()
                Response.Redirect("../../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../../Login.aspx")
        End Try

    End Sub

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkApp(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            hdUser.Value = dt.Rows(0).Item("app_usr")
            hdPosition.Value = dt.Rows(0).Item("app_position")
            txtUser.Text = ""

            Dim dtTable(6) As String

            dtTable = oUsr.getUserDetail(dt.Rows(0).Item("app_usr"))
            With dtTable
                If .Length > 0 Then
                    hdUser.Value = dtTable(0)
                    txtUser.Text = dtTable(1)
                End If
            End With
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            Try
                oAssign.updateAppUser(appno, hdUser.Value, usrLogin)
                liBodyLoadAction.Text = "alert('Data berhasil diubah');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace(",", "") & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        If hdUser.Value = "" Or hdUser.Value = "0" Then
            liBodyLoadAction.Text = "alert('Please input user');"
        End If
    End Sub
End Class