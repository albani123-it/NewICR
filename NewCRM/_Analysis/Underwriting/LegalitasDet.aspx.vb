Public Class LegalitasDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript

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

        If Request.QueryString("id") IsNot Nothing Then
            If Request.QueryString("id") > 0 And Not IsPostBack Then
                isiData(Request.QueryString("id"))
            End If
        End If
        If Request.QueryString("brwcode") IsNot Nothing Then
            brw_code.Value = Request.QueryString("brwcode")
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

    Sub fillDdl()
        Dim dt As DataTable
        dt = oRat.getDropDownList("RFLEGALLETTER")
        ddlJenisIjin.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlJenisIjin.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDetailOfLegalitas(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            brw_code.Value = oGen.CheckNullString(dt.Rows(0).Item("lsl_brw_code"))
            ddlJenisIjin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsl_jenisIjin"))
            txtNoLegalitas.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_noLegalitas"))
            txtTglLegalitas.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_tglLegalitas"))
            txtTglJatuhTempo.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_tglJatuhTempo"))
            txtPerihal.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_perihal"))
            txtNoPengesahan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_noPengesahan"))
            txtNamaNotaris.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_namaNotaris"))
            txtKeterangan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_keterangan"))
        End If
    End Sub

    Sub controlRO()
        ddlJenisIjin.Enabled = False
        ddlJenisIjin.CssClass = "inpDdlDisabled"
        txtNoLegalitas.Enabled = False
        txtTglLegalitas.Enabled = False
        txtTglJatuhTempo.Enabled = False
        txtPerihal.Enabled = False
        txtNoPengesahan.Enabled = False
        txtNamaNotaris.Enabled = False
        txtKeterangan.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If brw_code.Value IsNot Nothing Then
                If Request.QueryString("id") <> -1 Then
                    'update data
                    Try
                        oApp.updateLegalitasInfo(Request, brw_code.Value, Request.QueryString("id"), usrLogin)
                        'liBodyLoadAction.Text = "alert('Data berhasil diubah.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                Else
                    'insert data
                    Try
                        oApp.insertLegalitasInfo(Request, brw_code.Value, usrLogin)
                        'liBodyLoadAction.Text = "alert('Data berhasil ditambah.');window.opener.location.href = window.opener.location.href;parent.window.close();"
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
        If txtNoLegalitas.Text = "" Then
            lMsg &= "No. Legalitas Usaha harus diisi.\n"
        End If
        If Request.Form("txtTglLegalitas") <> "" And Request.Form("txtTglJatuhTempo") <> "" Then
            Dim tglExp As String = Mid(Request.Form("txtTglJatuhTempo"), 7, 4) + "-" & Mid(Request.Form("txtTglJatuhTempo"), 4, 2) + "-" & Mid(Request.Form("txtTglJatuhTempo"), 1, 2)
            Dim tgl As String = Mid(Request.Form("txtTglLegalitas"), 7, 4) + "-" & Mid(Request.Form("txtTglLegalitas"), 4, 2) + "-" & Mid(Request.Form("txtTglLegalitas"), 1, 2)
            Dim result = DateTime.Compare(tglExp, tgl)
            If result < 0 Then
                lMsg &= "Tgl. Jatuh Tempo tidak boleh kurang dari Tgl. Legalitas Usaha."
            End If
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class