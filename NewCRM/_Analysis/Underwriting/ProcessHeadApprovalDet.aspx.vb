Public Class ProcessHeadApprovalDet
    Inherits System.Web.UI.Page
    Private lP As New lProcessApproval
    Private oApp As New lApps

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        'cekUserAuthentication()
        CheckAvailableApproval()
    End Sub

    Protected Sub btnApproval_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproval.Click
        Dim komentar = txtComentar.Text

        Dim id = Convert.ToInt64(Request.QueryString("id"))

        cekInput()
        If liBodyLoadAction.Text = "" Then
            Try
                lP.approval(id, usrLogin, komentar)
                'oApp.submitApp(app_no, 9999, usrLogin, "9999", komentar)
                liBodyLoadAction.Text = "alert('Applicatioan has been Approved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
        
    End Sub

    Protected Sub btnReject_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click
        Dim komentar = txtComentar.Text
        Dim id = Request.QueryString("id")

        cekInput()
        If liBodyLoadAction.Text = "" Then
            Try
                lP.reject(usrLogin, komentar, id)
                liBodyLoadAction.Text = "alert('Applicatioan has been rejected.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
        
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und" Then
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

    Sub cekInput()
        Dim lMsg As String = ""
        If txtComentar.Text = "" Then
            lMsg &= "Komentar harus diisi.\n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Sub CheckAvailableApproval()
        Dim id = Request.QueryString("id")
        Dim dt As DataTable
        dt = lP.CheckAvailableApproval(id)

        If dt.Rows(0).Item("available") = 0 Then
            txtComentar.Text = dt.Rows(0).Item("komentar").ToString()
            txtComentar.ReadOnly = True
            txtComentar.Enabled = False
            btnApproval.Visible = False
            btnReject.Visible = False

        End If


    End Sub


End Class