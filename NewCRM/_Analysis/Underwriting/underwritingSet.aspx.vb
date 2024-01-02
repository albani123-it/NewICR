Public Class underwritingSet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim stat As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Not IsPostBack Then
            fillDdl()
        End If

        If Request.QueryString("appno") IsNot Nothing Then
            appno.Value = Request.QueryString("appno")
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

    Sub fillDdl()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getListOfBorrowerType()
        dt = ds.Tables(0)
        ddlBrwType.Items.Clear()
        ddlBrwType.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlBrwType.Items.Add(New ListItem(dt.Rows(i).Item("brw_type"), dt.Rows(i).Item("brw_id")))
            Next
        End If

        ds = oApp.getListOfProduct()
        dt = ds.Tables(0)
        ddlPrdType.Items.Clear()
        ddlPrdType.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlPrdType.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
            Next
        End If
    End Sub

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkApp(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlBrwType.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("app_brw_type"))
            ddlPrdType.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("app_prd_type"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If appno.Value <> "" Then
                Try
                    oApp.updateAppModel(appno.Value, ddlBrwType.SelectedValue, ddlPrdType.SelectedValue, usrLogin)
                    'liBodyLoadAction.Text = "alert('Data berhasil diubah');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If ddlBrwType.SelectedValue = "Pilih" Then
            lMsg = "Borrower Type harus dipilih"
        End If
        If ddlPrdType.SelectedValue = "Pilih" Then
            lMsg &= "Product Type harus dipilih"
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class