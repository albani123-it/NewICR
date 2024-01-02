Public Class KeyPersonDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oGen As New lGeneral
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

        If IsPostBack Then
            txtTglLahir.Text = Request.Form("txtTglLahir").ToString()
            'txtTglKTP.Text = Request.Form("txtTglKTP").ToString()
            'txtExpKTP.Text = Request.Form("txtExpKTP").ToString()
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
        'dt = oRat.getDropDownList("RFSEX")
        'ddlJenisKelamin.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlJenisKelamin.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        'dt = oRat.getDropDownList("RFMARITAL")
        'ddlStatusKawin.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlStatusKawin.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        'dt = oRat.getDropDownList("RFNATION")
        'ddlKebangsaan.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlKebangsaan.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If
        Dim dtJenis As New DataTable
        dtJenis.Columns.Add("Value")

        Dim dtControled As DataTable
        dtControled = oRat.getDropDownList("RFSEX")
        ddlGender.DataSource = dtControled
        ddlGender.DataTextField = "ldd_desc"
        ddlGender.DataValueField = "ldd_code"
        ddlGender.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDetailOfKeyPerson(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            brw_code.Value = oGen.CheckNullString(dt.Rows(0).Item("lsk_brw_code"))
            'txtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_nama"))
            ''txtGelarD.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_gelarD"))
            ''txtGelarB.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_gelarB"))
            'txtJabatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_jabatan"))
            'txtTempatLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_tempatLahir"))
            'txtTglLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_tglLahir"))
            'txtNoKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_noKTP"))
            'txtTglKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_tglKTP"))
            'txtExpKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_expKTP"))
            'txtNPWP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_npwp"))
            'ddlStatusKawin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsk_statusKawin"))
            'ddlKebangsaan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsk_kebangsaan"))
            'ddlJenisKelamin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsk_jenisKelamin"))
            'txtAlamatRmh.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_alamat"))
            'txtAlamatUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_alamatUsaha"))
            ''txtKodePos.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_kodePos"))
            ''txtKelurahan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_kel"))
            ''txtKecamatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_kec"))
            ''txtKabupaten.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_kab"))
            ''txtPropinsi.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_prop"))
            'txtKodeAreaTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_kodeAreaTlp"))
            'txtTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_tlp"))
            'txtExtTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_extTlp"))
            'txtNoHP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_mobile"))
            ''txtNamaIbu.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_namaGadisIbu"))
            txtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_nama"))
            txtTgllahir.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsk_tgl_lahir")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsk_tgl_lahir")))
            ddlGender.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsk_jenis_kelamin"))
            txtNamaPerusahaan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_nama_perusahaan"))
            txtNamaJabatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_nama_jabatan"))
            txtHUbunganAntarPengurus.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_hub_antar_pengurus"))
            txtAlamat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_alamat"))
            txtMasaKerjaDari.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsk_masa_kerja_awal")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsk_masa_kerja_awal")))
            txtSampaiTgl.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsk_masa_kerja_akhir")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsk_masa_kerja_akhir")))
            txtRiwayat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsk_riwayat"))
        End If
    End Sub

    Sub controlRO()
        'txtNama.Enabled = False
        'txtGelarD.Enabled = False
        'txtGelarB.Enabled = False
        'txtJabatan.Enabled = False
        'txtTempatLahir.Enabled = False
        'txtTglLahir.Enabled = False
        'txtNoKTP.Enabled = False
        'txtTglKTP.Enabled = False
        'txtExpKTP.Enabled = False
        'txtNPWP.Enabled = False
        'ddlStatusKawin.Enabled = False
        'ddlStatusKawin.CssClass = "inpDdlDisabled"
        'ddlKebangsaan.Enabled = False
        'ddlKebangsaan.CssClass = "inpDdlDisabled"
        'ddlJenisKelamin.Enabled = False
        'ddlJenisKelamin.CssClass = "inpDdlDisabled"
        'txtAlamatRmh.Enabled = False
        'txtAlamatUsaha.Enabled = False
        ''txtKodePos.Enabled = False
        ''txtKelurahan.Enabled = False
        ''txtKecamatan.Enabled = False
        ''txtKabupaten.Enabled = False
        ''txtPropinsi.Enabled = False
        'txtKodeAreaTlp.Enabled = False
        'txtTlp.Enabled = False
        'txtExtTlp.Enabled = False
        'txtNoHP.Enabled = False
        'txtNamaIbu.Enabled = False
        txtNama.Enabled = False
        txtTgllahir.Enabled = False
        ddlGender.Enabled = False
        ddlGender.CssClass = "inpDdlDisabled"
        txtNamaPerusahaan.Enabled = False
        txtNamaJabatan.Enabled = False
        txtHUbunganAntarPengurus.Enabled = False
        txtAlamat.Enabled = False
        txtMasaKerjaDari.Enabled = False
        txtSampaiTgl.Enabled = False
        txtRiwayat.Enabled = False
        btnSave.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If brw_code.Value IsNot Nothing Then
                If Request.QueryString("id") <> -1 Then
                    'update data
                    Try
                        oApp.updateKeyPersonInfo(Request, brw_code.Value, Request.QueryString("id"), usrLogin)
                        'liBodyLoadAction.Text = "alert('Data berhasil diubah.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                Else
                    'insert data
                    Try
                        oApp.insertKeyPersonInfo(Request, brw_code.Value, usrLogin)
                        'liBodyLoadAction.Text = "alert('Data berhasil ditambah.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                End If
            End If
        End If
    End Sub

    Sub cekInput()
        'Dim lNot As String = ""

        'lNot &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
        'lNot &= oGen.checkInputInaJavascript("TextBox", "Tempat Lahir", txtTempatLahir.Text)
        'lNot &= oGen.checkInputInaJavascript("TextBox", "Tgl. Lahir", Request.Form("txtTglLahir"))
        'lNot &= oGen.checkInputInaJavascript("TextBox", "No. KTP", txtNoKTP.Text)
        'lNot &= oGen.checkInputInaJavascript("TextBox", "NPWP", txtNPWP.Text)
        'lNot &= oGen.checkInputInaJavascript("TextBox", "Alamat Kantor", txtAlamatRmh.Text)
        'lNot &= oGen.checkInputInaJavascript("TextBox", "Alamat Usaha", txtAlamatUsaha.Text)
        'lNot &= oGen.checkInputInaJavascript("TextBox", "No. Handphone", txtNoHP.Text)
        Dim lMsg As String = ""
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal Lahir", Request.Form("txtTgllahir"))
        lMsg &= oGen.checkInputInaJavascript("DropDownList", "Jenis Kelamin", ddlGender.SelectedValue)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Alamat", txtAlamat.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Jabatan", txtNamaJabatan.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Hubungan Antar Pengurus", txtHUbunganAntarPengurus.Text)

        'If Request.Form("txtTglKTP") <> "" And Request.Form("txtExpKTP") <> "" Then
        '    Dim tglExp As String = Mid(Request.Form("txtExpKTP"), 7, 4) + "-" & Mid(Request.Form("txtExpKTP"), 4, 2) + "-" & Mid(Request.Form("txtExpKTP"), 1, 2)
        '    Dim tgl As String = Mid(Request.Form("txtTglKTP"), 7, 4) + "-" & Mid(Request.Form("txtTglKTP"), 4, 2) + "-" & Mid(Request.Form("txtTglKTP"), 1, 2)
        '    Dim result = DateTime.Compare(tglExp, tgl)
        '    If result < 0 Then
        '        lNot &= "Tgl. Kadaluarsa KTP tidak boleh kurang dari Tgl. Pembuatan KTP."
        '    End If
        'End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

End Class