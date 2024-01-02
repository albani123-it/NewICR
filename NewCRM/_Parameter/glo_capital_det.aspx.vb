Public Class glo_capital_det
    Inherits System.Web.UI.Page
    Dim oGen As New lGeneral
    Dim oGlo As New lglobalSettings
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        liBodyLoadAction.Text = ""
        If Not IsPostBack Then
            fillDdl()
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            If Request.QueryString("id") <> -1 And Not IsPostBack Then
                isiData(Request.QueryString("id"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_capital" Then
                    tf = True
                End If
            Next
            If Not tf Then
                Session.Abandon()
                Response.Redirect("../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub fillDdl()
        ddlMonth.Items.Add(New ListItem("Select One", "Select One"))
        ddlMonth.Items.Add(New ListItem("Januari", "1"))
        ddlMonth.Items.Add(New ListItem("Februari", "2"))
        ddlMonth.Items.Add(New ListItem("Maret", "3"))
        ddlMonth.Items.Add(New ListItem("April", "4"))
        ddlMonth.Items.Add(New ListItem("Mei", "5"))
        ddlMonth.Items.Add(New ListItem("Juni", "6"))
        ddlMonth.Items.Add(New ListItem("Juli", "7"))
        ddlMonth.Items.Add(New ListItem("Agustus", "8"))
        ddlMonth.Items.Add(New ListItem("September", "9"))
        ddlMonth.Items.Add(New ListItem("Oktober", "10"))
        ddlMonth.Items.Add(New ListItem("November", "11"))
        ddlMonth.Items.Add(New ListItem("Desember", "12"))
        'ddlBulan.SelectedValue = "12"

        ddlYear.Items.Clear()
        Dim year As String = Now.ToString("yyyy")
        Dim x As Integer = CInt(year)
        ddlYear.Items.Add(New ListItem("Select One", "Select One"))
        While x <> 1960
            ddlYear.Items.Add(New ListItem(x.ToString, x.ToString))
            x = x - 1
        End While
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oGlo.getDetailOfGlobalCapital(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlMonth.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("cap_month"))
            ddlYear.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("cap_year"))
            txtCapital.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("cap_value"))
            txtHouseLimit.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("cap_house"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            Dim ds As DataSet
            Dim dt As DataTable

            Try
                If Request.QueryString("id") <> -1 Then
                    'ds = oGlo.getDetailOfGlobalCapitalByPeriod(Request.Form("ddlMonth"), Request.Form("ddlYear"))
                    'dt = ds.Tables(0)
                    'If dt.Rows.Count > 0 Then
                    '    liBodyLoadAction.Text = "alert('Data has been failed to add. Please choose another month or year.\n');"
                    'Else
                    'oGlo.updateGlobalCapital(Request, Request.QueryString("id"), usrLogin)
                    oGlo.updateGlobalCapital(Request, Request.QueryString("id"), usrLogin)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Bank Capital Updated")
                    objLog.insertLog("glo_bank_capital", logs)
                    liBodyLoadAction.Text = "alert('Data has been updated successfully.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    'End If
                Else
                    ds = oGlo.getDetailOfGlobalCapitalByPeriod(Request.Form("ddlMonth"), Request.Form("ddlYear"))
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        liBodyLoadAction.Text = "alert('Data has been failed to add. Please choose another month or year.\n');"
                    Else
                        'oGlo.insertGlobalCapital(Request, usrLogin)
                        oGlo.insertGlobalCapital(Request, usrLogin)
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Bank Capital Creation")
                        objLog.insertLog("glo_bank_capital", logs)
                        liBodyLoadAction.Text = "alert('Data has been added successfully.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    End If
                End If

            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If ddlMonth.SelectedValue = "Pilih" Then
            lMsg &= "Please choose month.\n"
        End If
        If ddlYear.SelectedValue = "Pilih" Then
            lMsg &= "Please choose year.\n"
        End If
        If txtCapital.Text = "" Then
            lMsg &= "Please fill capital.\n"
        End If
        If txtHouseLimit.Text = "" Then
            lMsg &= "Please fill house limit.\n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class