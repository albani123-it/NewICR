Public Class IndividualShareholdersDet
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
            If Request.QueryString("sisa") <> "" Then
                sisa.Value = Request.QueryString("sisa")
            End If
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
        dt = oRat.getDropDownList("RFSEX")
        ddlJenisKelamin.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlJenisKelamin.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDetailOfIndividualShareholders(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            brw_code.Value = oGen.CheckNullString(dt.Rows(0).Item("lis_brw_code"))
            txtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_nama"))
            txtGelarD.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_gelarD"))
            txtGelarB.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_gelarB"))
            txtJabatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_jabatan"))
            txtKepemilikanSaham.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_kepemilikanSaham"))
            txtJumlahLembarSaham.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lis_jumlahLembarSaham"))
            txtTempatLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_tempatLahir"))
            txtTglLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_tglLahir"))
            txtNoKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_noKTP"))
            txtTglKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_tglKTP"))
            txtExpKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_expKTP"))
            txtNPWP.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_npwp"))
            ddlJenisKelamin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lis_jenisKelamin"))
            txtAlamatRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_alamat"))
            txtKodePos.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_kodePos"))
            txtKelurahan.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_kel"))
            txtKecamatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_kec"))
            txtKabupaten.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_kab"))
            txtPropinsi.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_prop"))
            txtKodeAreaTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_kodeAreaTlp"))
            txtTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_tlp"))
            txtExtTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_extTlp"))
            txtNoHP.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_mobile"))
            txtNamaIbu.Text = oGen.CheckNullString(dt.Rows(0).Item("lis_namaGadisIbu"))
            If Request.QueryString("sisa") <> "" Then
                sisa.Value = CDbl(Request.QueryString("sisa")) + CDbl(oGen.CheckNullString(dt.Rows(0).Item("lis_kepemilikanSaham")))
            End If
        End If
    End Sub

    Sub controlRO()
        txtNama.ReadOnly = True
        txtGelarD.ReadOnly = True
        txtGelarB.ReadOnly = True
        txtJabatan.ReadOnly = True
        txtKepemilikanSaham.ReadOnly = True
        txtJumlahLembarSaham.ReadOnly = True
        txtTempatLahir.ReadOnly = True
        txtTglLahir.ReadOnly = True
        txtNoKTP.ReadOnly = True
        txtTglKTP.ReadOnly = True
        txtExpKTP.ReadOnly = True
        txtNPWP.ReadOnly = True
        ddlJenisKelamin.Enabled = False
        txtAlamatRumah.ReadOnly = True
        txtKodePos.ReadOnly = True
        txtKelurahan.ReadOnly = True
        txtKecamatan.ReadOnly = True
        txtKabupaten.ReadOnly = True
        txtPropinsi.ReadOnly = True
        txtKodeAreaTlp.ReadOnly = True
        txtTlp.ReadOnly = True
        txtExtTlp.ReadOnly = True
        txtNoHP.ReadOnly = True
        txtNamaIbu.ReadOnly = True

        txtNama.CssClass = "inpTxtDisabled"
        txtGelarD.CssClass = "inpTxtDisabled"
        txtGelarB.CssClass = "inpTxtDisabled"
        txtJabatan.CssClass = "inpTxtDisabled"
        txtKepemilikanSaham.CssClass = "inpTxtDisabled"
        txtJumlahLembarSaham.CssClass = "inpTxtDisabled"
        txtTempatLahir.CssClass = "inpTxtDisabled"
        txtTglLahir.CssClass = "inpTxtDisabled"
        txtNoKTP.CssClass = "inpTxtDisabled"
        txtTglKTP.CssClass = "inpTxtDisabled"
        txtExpKTP.CssClass = "inpTxtDisabled"
        txtNPWP.CssClass = "inpTxtDisabled"
        ddlJenisKelamin.CssClass = "inpDdlDisabled"
        txtAlamatRumah.CssClass = "inpTxtDisabled"
        txtKodePos.CssClass = "inpTxtDisabled"
        txtKelurahan.CssClass = "inpTxtDisabled"
        txtKecamatan.CssClass = "inpTxtDisabled"
        txtKabupaten.CssClass = "inpTxtDisabled"
        txtPropinsi.CssClass = "inpTxtDisabled"
        txtKodeAreaTlp.CssClass = "inpTxtDisabled"
        txtTlp.CssClass = "inpTxtDisabled"
        txtExtTlp.CssClass = "inpTxtDisabled"
        txtNoHP.CssClass = "inpTxtDisabled"
        txtNamaIbu.CssClass = "inpTxtDisabled"
        btnSave.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If brw_code.Value <> "" Then
                If Request.QueryString("id") <> -1 Then
                    'update data
                    Try
                        oApp.updateIndividualShareholders(Request, brw_code.Value, Request.QueryString("id"), usrLogin)
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                Else
                    'insert data
                    Try
                        oApp.insertIndividualShareholders(Request, brw_code.Value, usrLogin)
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
        If CDbl(txtKepemilikanSaham.Text) > 100 Then
            lMsg &= "Jumlah Kepemilikan Saham tidak boleh lebih dari 100%.\n"
        End If

        If CDbl(txtKepemilikanSaham.Text) > CDbl(sisa.Value) Then
            lMsg &= "Jumlah Saham Perorangan dan Badan tidak boleh lebih dari 100%.\n"
        End If

        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Kepemilikan Saham", txtKepemilikanSaham.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Jumlah Lembar Saham", txtJumlahLembarSaham.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tempat Lahir", txtTempatLahir.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tgl. Lahir", Request.Form("txtTglLahir"))
        lMsg &= oGen.checkInputInaJavascript("TextBox", "No. KTP", txtNoKTP.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "NPWP", txtNPWP.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Alamat Rumah", txtAlamatRumah.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "No. Handphone", txtNoHP.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Gadis Ibu Kandung", txtNamaIbu.Text)

        If Request.Form("txtTglKTP") <> "" And Request.Form("txtExpKTP") <> "" Then
            Dim tglExp As String = Mid(Request.Form("txtExpKTP"), 7, 4) + "-" & Mid(Request.Form("txtExpKTP"), 4, 2) + "-" & Mid(Request.Form("txtExpKTP"), 1, 2)
            Dim tgl As String = Mid(Request.Form("txtTglKTP"), 7, 4) + "-" & Mid(Request.Form("txtTglKTP"), 4, 2) + "-" & Mid(Request.Form("txtTglKTP"), 1, 2)
            Dim result = DateTime.Compare(tglExp, tgl)
            If result < 0 Then
                lMsg &= "Tgl. Kadaluarsa KTP tidak boleh kurang dari Tgl. Pembuatan KTP."
            End If
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class