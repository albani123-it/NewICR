Public Class callMemoDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            txtTgl.Text = Now.ToString("dd-MM-yyyy")
        End If
        If Request.QueryString("id") IsNot Nothing Then
            If Request.QueryString("id") > 0 And Not IsPostBack Then
                isiData(Request.QueryString("id"))
            End If
        End If
        If Request.QueryString("appno") IsNot Nothing Then
            appno.Value = Request.QueryString("appno")
        End If
        If Request.QueryString("status") = 0 Then
            btnSave.Enabled = False
            controlRO()
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

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDetailOfCallMemo(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            txtTgl.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_tgl"))
            txtNoTelp.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_noTelp"))
            txtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_namaPerusahaan"))
            txtNoTelpLain.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_noTelpLain"))
            txtBentukCall.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_bentukCall"))
            txtPenjabatYangDiCall.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_yangDiCall"))
            txtPenjabatYangMelakukanCall.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_yangMelakukanCall"))
            txtTujuanCall.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_tujuanCall"))
            txtHasil.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_hasil"))
            txtTindakLanjut.Text = oGen.CheckNullString(dt.Rows(0).Item("lcm_tindakLanjut"))
        End If
    End Sub

    Sub controlRO()
        txtTgl.Enabled = False
        txtNoTelp.Enabled = False
        txtNama.Enabled = False
        txtNoTelpLain.Enabled = False
        txtBentukCall.Enabled = False
        txtPenjabatYangDiCall.Enabled = False
        txtPenjabatYangMelakukanCall.Enabled = False
        txtTujuanCall.Enabled = False
        txtHasil.Enabled = False
        txtTindakLanjut.Enabled = False

        'txtTgl.CssClass = "inpDisabled"
        'txtNoTelp.CssClass = "inpDisabled"
        'txtNama.CssClass = "inpDisabled"
        'txtNoTelpLain.CssClass = "inpDisabled"
        'txtBentukCall.CssClass = "inpDisabled"
        'txtPenjabatYangDiCall.CssClass = "inpDisabled"
        'txtPenjabatYangMelakukanCall.CssClass = "inpDisabled"
        'txtTujuanCall.CssClass = "inpDisabled"
        'txtHasil.CssClass = "inpDisabled"
        'txtTindakLanjut.CssClass = "inpDisabled"

        btnSave.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If appno.Value <> "" Then
                If Request.QueryString("id") <> -1 Then
                    Try
                        'update 
                        oApp.updateCallMemo(Request, appno.Value, Request.QueryString("id"), usrLogin)
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                Else
                    Try
                        'insert
                        oApp.insertCallMemo(Request, appno.Value, usrLogin)
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                End If

            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal", txtTgl.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "No Telepon", txtNoTelp.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "No Telepon Lain Yang Di Call", txtNoTelpLain.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Bentuk Call", txtBentukCall.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Penjabat Yang Di Call", txtPenjabatYangDiCall.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Penjabat Yang Melakukan Call", txtPenjabatYangMelakukanCall.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tujuan Call", txtTujuanCall.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Hasil", txtHasil.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tindak Lanjut Yang Harus Dilakukan", txtTindakLanjut.Text)

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class