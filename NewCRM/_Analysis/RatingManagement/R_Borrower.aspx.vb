Public Class R_Borrower
    Inherits System.Web.UI.Page
    Dim oRat As New lRating
    Dim oRerat As New lRerating
    Dim oGen As New lGeneral
    Dim oBrat As New lBRating
    Dim oApp As New lApps
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Informasi Nasabah"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected app_no As String = ""
    Protected product As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        controlHome()
        If Not IsPostBack Then
            fillDdl()
            fillTahunHubBank()
            fillBulanHubBank()
        End If

        If IsPostBack Then
            txtTglLahir.Text = Request.Form(txtTglLahir.UniqueID).ToString()
            txtTglPembuatanKTPAkte.Text = Request.Form(txtTglPembuatanKTPAkte.UniqueID).ToString()
            txtMenjadiDebiturSejak.Text = Request.Form(txtMenjadiDebiturSejak.UniqueID).ToString()
            txtTglPermohonanDebitur.Text = Request.Form(txtTglPermohonanDebitur.UniqueID).ToString()
            txtTglPenerimaanBerkas.Text = Request.Form(txtTglPenerimaanBerkas.UniqueID).ToString()
        End If

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If
        If Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
            brw_code.Value = Request.QueryString("brwcode")
        End If

        If Session("appno") IsNot Nothing Then
            'cek data hanya untuk diakses dari facility rerating, 
            'karena halaman ini ditampilkan juga di facility rereating. sisanya langsung isi data saja tidak perlu cekData.
            If Not IsPostBack Then
                cekData(Session("appno"))
            End If
            appno.Value = Session("appno")

        ElseIf Session("brwcode") IsNot Nothing Then
            If Not IsPostBack Then
                isiData(Session("brwcode"))
            End If
            brw_code.Value = Session("brwcode")
        End If

        type.Value = Request.QueryString("typ")

        If Request.QueryString("typ") IsNot Nothing Then
            If Request.QueryString("typ") = "Borrower" Then
                dataTable = oRerat.getListOfMenu("Borrower")
                Session("type") = "Borrower"
            Else
                dataTable = oRerat.getListOfMenu("Facility")
                Session("type") = "Facility"
                controlRO()
            End If
            getMenuNext(Request.QueryString("typ"))
            executeJavascript(Request.QueryString("typ"))
        Else
            Response.Redirect("ratingMgmt.aspx" + "?typ=" + Session("type"))

        End If
    End Sub

    Sub executeJavascript(ByVal typ)
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        If typ = "Facility" Then
            dset = oApp.checkApp(appno.Value)
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dataTable.Rows.Count - 1
                    If dataTable.Rows(i).Item("lmr_name") = pagename Then
                        url = dataTable.Rows(i).Item("lmr_link")
                    End If
                Next
            End If
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & appno.Value & "', 'Facility');"
            Dim javascript = "<script type='text/javascript'>" & script & "</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
        End If
        If typ = "Borrower" Then
            dset = oApp.checkAppByBrwCode(brw_code.Value)
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dataTable.Rows.Count - 1
                    If dataTable.Rows(i).Item("lmr_name") = pagename Then
                        url = dataTable.Rows(i).Item("lmr_link")
                    End If
                Next
            End If
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & brw_code.Value & "', 'Borrower');"
            Dim javascript = "<script type='text/javascript'>" & script & "</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
        End If
    End Sub

    Sub getMenuNext(ByVal typ)
        For i As Integer = 0 To dataTable.Rows.Count - 1
            If dataTable.Rows(i).Item("lmr_name") = pagename Then
                Dim dt2 As DataTable
                dt2 = oRerat.getNextMenu(dataTable.Rows(i).Item("lmr_id"), typ)
                If dt2.Rows.Count > 0 Then
                    menuNext = dt2.Rows(0).Item("lmr_link")
                End If
            End If
        Next
    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        Dim typ = Request.QueryString("typ")

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lmr_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        Dim typ = Request.QueryString("typ")

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lmr_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_rat" Then
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

    Sub controlHome()

    End Sub

    Sub fillDdl()
        Dim dt As DataTable
        dt = oRat.getDropDownList("RFBUSINESSSECTOR")
        ddlSektorEkonomi.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlSektorEkonomi.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

        dt = oRat.getDropDownList("BRWBUSINESSSECTOR")
        ddlBidangUsaha.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlBidangUsaha.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

    End Sub

    Sub fillTahunHubBank()
        Dim year As String = Now.ToString("yyyy")
        Dim x As Integer = CInt(year)
        While (x > 1980)
            ddlTahunHubunganDenganBTN.Items.Add(New ListItem(x.ToString, x.ToString))
            x = x - 1
        End While
    End Sub

    Sub fillBulanHubBank()

        Dim month As Integer = 1
        ddlBulanHubunganDenganBTN.Items.Add(New ListItem("Pilih", "Pilih"))
        While (month <= 12)
            ddlBulanHubunganDenganBTN.Items.Add(New ListItem(month.ToString, month.ToString))
            month = month + 1
        End While
    End Sub

    Sub cekData(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oBrat.getBrwCodeByAppno(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then

            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Or dtbl.Rows(0).Item("app_brw_code") = "" Then
                Response.Redirect("ratingMgmt.aspx" + "?typ=" + Session("type"))
            Else
                brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
                lblBrwCode.Text = dtbl.Rows(0).Item("app_brw_code")
                isiData(dtbl.Rows(0).Item("app_brw_code"))
            End If
            lblAppNo_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("app_no"))
            lblBrwName_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("lsb_namaPemohon"))
        Else
            Response.Redirect("ratingMgmt.aspx" + "?typ=" + Session("type"))
        End If
    End Sub

    Sub isiData(ByVal brwcode)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrower(brwcode)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlJenisDebitur.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_jenisDebitur"))
            txtAcctno.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_acctno"))
            txtCIF.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_cif"))
            txtNamaPemohon.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaPemohon"))
            txtNPWP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_npwp"))
            txtTglLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tglLahir"))
            txtNoKTPAkte.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_noKTPAkte"))
            txtDeskripsi.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_deskripsi"))
            ddlSektorEkonomi.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_sektorEko"))
            txtTglPembuatanKTPAkte.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tglKTPAkte"))
            txtDaerahPemasaran.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_daerahPemasaran"))
            txtAlamatRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_alamatRmh"))
            txtSasaranPembeli.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_sasaranPembeli"))
            txtKodePosRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodePosRmh"))
            txtJumlahKaryawan.Text = oGen.moneyNumericLabel(dt.Rows(0).Item("lsb_jmlKaryawan"))
            txtKelurahanRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kelRmh"))
            txtMenjadiDebiturSejak.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_debSejak"))
            txtKecamatanRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kecRmh"))
            ddlTahunHubunganDenganBTN.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_thnHubBank"))
            ddlBulanHubunganDenganBTN.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_blnHubBank"))
            txtKotamadyaKabupatenKantor.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kabRmh"))
            txtNoPermohonanDebitur.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_noPermohonanDeb"))
            txtPropinsiRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_propRmh"))
            txtTglPermohonanDebitur.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tglPermohonanDeb"))
            txtKodeAreaTlpRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpRmh"))
            txtTlpRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpRmh"))
            txtExtTlpRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extTlpRmh"))
            txtKodeAreaTlpRumah2.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpRmh2"))
            txtTlpRumah2.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpRmh2"))
            txtExtTlpRumah2.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extTlpRmh2"))
            txtKodeAreaTlpRumah3.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpRmh3"))
            txtTlpRumah3.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpRmh3"))
            txtExtTlpRumah3.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extTlpRmh3"))
            txtTglPenerimaanBerkas.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tglPenerimaanBerkas"))
            txtKodeAreaFaxRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaFaxRmh"))
            txtFaxRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_faxRmh"))
            txtExtFaxRumah.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extFaxRmh"))

            txtAlamatSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_alamatSrt"))
            txtKodePosSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodePosSrt"))
            txtKelurahanSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kelSrt"))
            txtKecamatanSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kecSrt"))
            txtKotamadyaKabupatenSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kabSrt"))
            txtPropinsiSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_propSrt"))
            txtKodeAreaTlpSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpSrt"))
            txtTlpSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpSrt"))
            txtExtTlpSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extTlpSrt"))
            txtEmailSurat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_emailSrt"))

            txtNamaProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaUsaha"))
            txtAlamatProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_alamatUsaha"))
            txtKodePosProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodePosUsaha"))
            txtKelurahanProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kelUsaha"))
            txtKecamatanProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kecUsaha"))
            txtKotamadyaKabupatenUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kabUsaha"))
            txtPropinsiUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_propUsaha"))
            txtKodeAreaTlpUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpUsaha"))
            txtTlpUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpUsaha"))
            txtExtTlpUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extTlpUsaha"))
            txtKodeAreaTlpUsaha2.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpUsaha2"))
            txtTlpUsaha2.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpUsaha2"))
            txtExtTlpUsaha2.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extTlpUsaha2"))
            txtKodeAreaTlpUsaha3.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpUsaha3"))
            txtTlpUsaha3.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpUsaha3"))
            txtExtTlpUsaha3.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpUsaha3"))
            txtKodeAreaFaxUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaFaxUsaha"))
            txtFaxUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_faxUsaha"))
            txtExtFaxUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_extFaxUsaha"))
            txtLamaUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_lamaUsaha"))
            ddlBidangUsaha.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_bidangUsaha"))
            txtGrupUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_grupUsaha"))
        End If
    End Sub

    Sub controlRO()
        chkAlamatRumah.Disabled = True
        chkAlamatKantorKTP.Disabled = True
        chkAlamatUsaha.Disabled = True
        ddlJenisDebitur.Enabled = False
        ddlJenisDebitur.CssClass = "inpDdlDisabled"
        txtCIF.Enabled = False
        txtAcctno.Enabled = False
        btnLook.Enabled = False
        txtNamaPemohon.Enabled = False
        txtNPWP.Enabled = False
        txtTglLahir.Enabled = False
        txtNoKTPAkte.Enabled = False
        txtDeskripsi.Enabled = False
        ddlSektorEkonomi.Enabled = False
        ddlSektorEkonomi.CssClass = "inpDdlDisabled"
        txtTglPembuatanKTPAkte.Enabled = False
        txtDaerahPemasaran.Enabled = False
        txtAlamatRumah.Enabled = False
        txtSasaranPembeli.Enabled = False
        txtKodePosRumah.Enabled = False
        txtJumlahKaryawan.Enabled = False
        txtKelurahanRumah.Enabled = False
        txtMenjadiDebiturSejak.Enabled = False
        txtKecamatanRumah.Enabled = False
        ddlTahunHubunganDenganBTN.Enabled = False
        ddlTahunHubunganDenganBTN.CssClass = "inpDdlDisabled"
        ddlBulanHubunganDenganBTN.CssClass = "inpDdlDisabled"
        txtKotamadyaKabupatenKantor.Enabled = False
        txtNoPermohonanDebitur.Enabled = False
        txtPropinsiRumah.Enabled = False
        txtTglPermohonanDebitur.Enabled = False
        txtKodeAreaTlpRumah.Enabled = False
        txtTlpRumah.Enabled = False
        txtExtTlpRumah.Enabled = False
        txtKodeAreaTlpRumah2.Enabled = False
        txtTlpRumah2.Enabled = False
        txtExtTlpRumah2.Enabled = False
        txtKodeAreaTlpRumah3.Enabled = False
        txtTlpRumah3.Enabled = False
        txtExtTlpRumah3.Enabled = False
        txtTglPenerimaanBerkas.Enabled = False
        txtKodeAreaFaxRumah.Enabled = False
        txtFaxRumah.Enabled = False
        txtExtFaxRumah.Enabled = False
        txtNamaProyekUsaha.Enabled = False
        txtAlamatProyekUsaha.Enabled = False
        txtKodePosProyekUsaha.Enabled = False
        txtKelurahanProyekUsaha.Enabled = False
        txtKecamatanProyekUsaha.Enabled = False
        txtKotamadyaKabupatenUsaha.Enabled = False
        txtPropinsiUsaha.Enabled = False
        txtKodeAreaTlpUsaha.Enabled = False
        txtTlpUsaha.Enabled = False
        txtExtTlpUsaha.Enabled = False
        txtKodeAreaTlpUsaha2.Enabled = False
        txtTlpUsaha2.Enabled = False
        txtExtTlpUsaha2.Enabled = False
        txtKodeAreaTlpUsaha3.Enabled = False
        txtTlpUsaha3.Enabled = False
        txtExtTlpUsaha3.Enabled = False
        txtKodeAreaFaxUsaha.Enabled = False
        txtFaxUsaha.Enabled = False
        txtExtFaxUsaha.Enabled = False
        txtLamaUsaha.Enabled = False
        ddlBidangUsaha.Enabled = False
        ddlBidangUsaha.CssClass = "inpDdlDisabled"
        txtGrupUsaha.CssClass = "inpTxtDisabled"
        txtGrupUsaha.ReadOnly = True

        txtAlamatSurat.Enabled = False
        txtKodePosSurat.Enabled = False
        txtKelurahanSurat.Enabled = False
        txtKecamatanSurat.Enabled = False
        txtKotamadyaKabupatenSurat.Enabled = False
        txtPropinsiSurat.Enabled = False
        txtKodeAreaTlpSurat.Enabled = False
        txtTlpSurat.Enabled = False
        txtExtTlpSurat.Enabled = False
        txtEmailSurat.Enabled = False
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Sub cekInput()
        lblNotif.Text = ""
        Dim lNot As String = ""

        Try
            lNot &= oGen.checkInputInaHTML("DropDownList", "Jenis Debitur", ddlJenisDebitur.SelectedValue)
            'lNot &= oGen.checkInputInaHTML("TextBox", "No. CIF", txtCIF.Text)
            lNot &= oGen.checkInputInaHTML("TextBox", "Nama Pemohon", txtNamaPemohon.Text)
            lNot &= oGen.checkInputInaHTML("TextBox", "Berdiri Sejak", Request.Form(txtTglLahir.UniqueID).ToString())
            'lNot &= oGen.checkInputInaHTML("TextBox", "Tempat Lahir", txtTempatLahir.Text)
            'lNot &= oGen.checkInputInaHTML("TextBox", "Nama Gadis Ibu Kandung", txtNamaGadisIbuKandung.Text)
            lNot &= oGen.checkInputInaHTML("TextBox", "No. Akte Pendirian", txtNoKTPAkte.Text)
            lNot &= oGen.checkInputInaHTML("TextBox", "Tgl. Pembuatan Akte Pendirian", Request.Form(txtTglPembuatanKTPAkte.UniqueID).ToString())
            lNot &= oGen.checkInputInaHTML("DropDownList", "Sektor Ekonomi LBU", ddlSektorEkonomi.SelectedValue)
            'lNot &= oGen.checkInputInaHTML("TextBox", "Menjadi Debitur DKI Sejak", Request.Form(txtMenjadiDebiturSejak.UniqueID).ToString())
            lNot &= oGen.checkInputInaHTML("DropDownList", "Tahun Hubungan dengan Bank DKI", ddlTahunHubunganDenganBTN.SelectedValue)
            lNot &= oGen.checkInputInaHTML("DropDownList", "Bulan Hubungan dengan Bank DKI", ddlBulanHubunganDenganBTN.SelectedValue)
            lNot &= oGen.checkInputInaHTML("TextBox", "Tanggal Permohonan Debitur", Request.Form(txtTglPermohonanDebitur.UniqueID).ToString())
            lNot &= oGen.checkInputInaHTML("TextBox", "Tanggal Penerimaan Berkas", Request.Form(txtTglPenerimaanBerkas.UniqueID).ToString())
            lNot &= oGen.checkInputInaHTML("TextBox", "No. Permohonan Debitur", txtNoPermohonanDebitur.Text)
            lNot &= oGen.checkInputInaHTML("TextBox", "Kode Area No. Telepon Kantor", txtKodeAreaTlpRumah.Text)
            lNot &= oGen.checkInputInaHTML("TextBox", "No. Telepon Kantor", txtTlpRumah.Text)

            If ddlBulanHubunganDenganBTN.SelectedValue <> "Pilih" And ddlTahunHubunganDenganBTN.SelectedValue <> "Pilih" Then
                Dim temp_month As String = ddlTahunHubunganDenganBTN.SelectedValue
                If ddlBulanHubunganDenganBTN.SelectedValue < 10 Then
                    temp_month = "0" & ddlBulanHubunganDenganBTN.SelectedValue
                End If
                Dim HubDenganDKI As String = "01-" & temp_month & "-" & ddlTahunHubunganDenganBTN.SelectedValue
                Dim menjadiDebitur As String = Request.Form(txtMenjadiDebiturSejak.UniqueID).Substring(0, 2) & "-" & Request.Form(txtMenjadiDebiturSejak.UniqueID).Substring(3, 2) & "-" & Request.Form(txtMenjadiDebiturSejak.UniqueID).Substring(6, 4)
                lNot &= oGen.checkDateParInHtml("more than", "Menjadi Debitur DKI Sejak", "Hubungan dengan Bank DKI Sejak", menjadiDebitur, HubDenganDKI)
                lNot &= oGen.checkDateParInHtml("more", "Tanggal Permohonan Debitur", "Tanggal Penerimaan Berkas", txtTglPermohonanDebitur.Text, txtTglPenerimaanBerkas.Text)
            End If

        Catch ex As Exception
            lNot &= "&bull;&nbsp;" & ex.Message & "<br/>"
        End Try

        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            Dim obj As New AAS.Business.Entity.borrower
            obj.acctno = txtAcctno.Text
            obj.brwcode = brw_code.Value
            obj.jenisDebitur = ddlJenisDebitur.SelectedValue
            obj.jenisKelamin = ""
            obj.noCIF = txtCIF.Text
            obj.pekerjaan = ""
            obj.namaPemohon = txtNamaPemohon.Text
            obj.statusPerkawinan = ""
            obj.gelarDepan = ""
            obj.gelarBelakang = ""
            obj.NPWPPemohon = txtNPWP.Text
            If Request.Form(txtTglLahir.UniqueID).ToString() = "" Then
                obj.tglLahir = "1900-01-01"
            Else
                obj.tglLahir = Mid(Request.Form(txtTglLahir.UniqueID).ToString(), 7, 4) + "-" & Mid(Request.Form(txtTglLahir.UniqueID).ToString(), 4, 2) + "-" & Mid(Request.Form(txtTglLahir.UniqueID).ToString(), 1, 2)
            End If
            obj.statusGelar = ""
            obj.tempatLahir = ""
            obj.ketStatusGelar = ""
            obj.namaGadisIbuKandung = ""
            obj.noKTPAkte = txtNoKTPAkte.Text
            obj.deskripsi = txtDeskripsi.Text
            If Request.Form(txtTglPembuatanKTPAkte.UniqueID) = "" Then
                obj.tglPembuatanKTPAkte = "1900-01-01"
            Else
                obj.tglPembuatanKTPAkte = Mid(Request.Form(txtTglPembuatanKTPAkte.UniqueID), 7, 4) + "-" & Mid(Request.Form(txtTglPembuatanKTPAkte.UniqueID), 4, 2) + "-" & Mid(Request.Form(txtTglPembuatanKTPAkte.UniqueID), 1, 2)
            End If
            obj.daerahPemasaran = txtDaerahPemasaran.Text
            obj.alamatRumah = txtAlamatRumah.Text
            obj.sektorEkonomi = ddlSektorEkonomi.SelectedValue
            'obj.subSektorEkonomi = ddlSubSektor.SelectedValue
            obj.kodePosRumah = txtKodePosRumah.Text
            obj.sasaranPembeli = txtSasaranPembeli.Text
            obj.jumlahKaryawan = txtJumlahKaryawan.Text.Replace(",", "")
            obj.kelurahanRumah = txtKelurahanRumah.Text
            If Request.Form(txtMenjadiDebiturSejak.UniqueID) = "" Then
                obj.menjadiDebiturSejak = "1900-01-01"
            Else
                obj.menjadiDebiturSejak = Mid(Request.Form(txtMenjadiDebiturSejak.UniqueID), 7, 4) + "-" & Mid(Request.Form(txtMenjadiDebiturSejak.UniqueID), 4, 2) + "-" & Mid(Request.Form(txtMenjadiDebiturSejak.UniqueID), 1, 2)
            End If
            obj.kecamatanRumah = txtKecamatanRumah.Text
            obj.tahunHubunganDenganBTN = ddlTahunHubunganDenganBTN.SelectedValue
            obj.bulanHubunganDenganBTN = ddlBulanHubunganDenganBTN.SelectedValue
            obj.kotamadyaKabupatenKantor = txtKotamadyaKabupatenKantor.Text
            obj.noPermohonanDebitur = txtNoPermohonanDebitur.Text
            obj.propinsiRumah = txtPropinsiRumah.Text
            If Request.Form(txtTglPermohonanDebitur.UniqueID) = "" Then
                obj.tglPermohonanDebitur = "1900-01-01"
            Else
                obj.tglPermohonanDebitur = Mid(Request.Form(txtTglPermohonanDebitur.UniqueID), 7, 4) + "-" & Mid(Request.Form(txtTglPermohonanDebitur.UniqueID), 4, 2) + "-" & Mid(Request.Form(txtTglPermohonanDebitur.UniqueID), 1, 2)
            End If
            obj.noKodeAreaTlpRumah = txtKodeAreaTlpRumah.Text
            obj.noTlpRumah = txtTlpRumah.Text
            obj.noExtTlpRumah = txtExtTlpRumah.Text
            obj.noKodeAreaTlpRumah2 = txtKodeAreaTlpRumah2.Text
            obj.noTlpRumah2 = txtTlpRumah2.Text
            obj.noExtTlpRumah2 = txtExtTlpRumah2.Text
            obj.noKodeAreaTlpRumah3 = txtKodeAreaTlpRumah3.Text
            obj.noTlpRumah3 = txtTlpRumah3.Text
            obj.noExtTlpRumah3 = txtExtTlpRumah3.Text
            If Request.Form(txtTglPenerimaanBerkas.UniqueID) = "" Then
                obj.tglPenerimaanBerkas = "1900-01-01"
            Else
                obj.tglPenerimaanBerkas = Mid(Request.Form(txtTglPenerimaanBerkas.UniqueID), 7, 4) + "-" & Mid(Request.Form(txtTglPenerimaanBerkas.UniqueID), 4, 2) + "-" & Mid(Request.Form(txtTglPenerimaanBerkas.UniqueID), 1, 2)
            End If
            obj.noKodeAreaFaxRumah = txtKodeAreaFaxRumah.Text
            obj.noTlpFaxRumah = txtFaxRumah.Text
            obj.noExtFaxRumah = txtExtFaxRumah.Text
            obj.namaUsaha = txtNamaProyekUsaha.Text
            obj.alamatUsaha = txtAlamatProyekUsaha.Text
            obj.kodePosUsaha = txtKodePosProyekUsaha.Text
            obj.kelurahanUsaha = txtKelurahanProyekUsaha.Text
            obj.kecamatanUsaha = txtKecamatanProyekUsaha.Text
            obj.kotamadyaKabupatenUsaha = txtKotamadyaKabupatenUsaha.Text
            obj.propinsiUsaha = txtPropinsiUsaha.Text
            obj.noKodeAreaTlpUsaha = txtKodeAreaTlpUsaha.Text
            obj.noTlpUsaha = txtTlpUsaha.Text
            obj.noExtTlpUsaha = txtExtTlpUsaha.Text
            obj.noKodeAreaTlpUsaha2 = txtKodeAreaTlpUsaha2.Text
            obj.noTlpUsaha2 = txtTlpUsaha2.Text
            obj.noExtTlpUsaha2 = txtExtTlpUsaha2.Text
            obj.noKodeAreaTlpUsaha3 = txtKodeAreaTlpUsaha3.Text
            obj.noTlpUsaha3 = txtTlpUsaha3.Text
            obj.noExtTlpUsaha3 = txtExtTlpUsaha3.Text
            obj.noKodeAreaFaxUsaha = txtKodeAreaFaxUsaha.Text
            obj.noTlpFaxUsaha = txtFaxUsaha.Text
            obj.noExtFaxUsaha = txtExtFaxUsaha.Text
            obj.lamaUsaha = txtLamaUsaha.Text
            obj.bidangUsaha = ddlBidangUsaha.SelectedValue
            obj.grupUsaha = txtGrupUsaha.Text
            obj.alamatSurat = txtAlamatSurat.Text
            obj.kodePosSurat = txtKodePosSurat.Text
            obj.kelurahanSurat = txtKelurahanSurat.Text
            obj.kecamatanSurat = txtKecamatanSurat.Text
            obj.kotamadyaKabupatenSurat = txtKotamadyaKabupatenSurat.Text
            obj.sandiLokasiBI = ""
            obj.propinsiSurat = txtPropinsiSurat.Text
            obj.noKodeAreaTlpSurat = txtKodeAreaTlpSurat.Text
            obj.noTlpSurat = txtTlpSurat.Text
            obj.noExtTlpSurat = txtExtTlpSurat.Text
            obj.emailSurat = txtEmailSurat.Text
            Try
                lblNotif.Text = ""
                Dim ds As DataSet
                Dim dt As DataTable
                Dim dset As DataSet
                Dim dtbl As DataTable

                ds = oApp.checkBorrower(brw_code.Value)
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    'update data
                    'oApp.updateBorrowerInfo(obj, usrLogin)
                    dset = oApp.checkCifNo(appno.Value, txtCIF.Text)
                    dtbl = dset.Tables(0)

                    If dtbl.Rows.Count = 0 Then
                        lblNotif.Text = checkCif()
                    End If

                    dset = oApp.checkAcctNo(appno.Value, txtAcctno.Text)
                    dtbl = dset.Tables(0)

                    If dtbl.Rows.Count = 0 Then
                        lblNotif.Text = checkAcctno()
                    End If

                    If lblNotif.Text = "" Then
                        oApp.updateBorrowerInfo(obj, usrLogin)
                    Else
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + lblNotif.Text + "');", True)
                        txtCIF.Focus()
                    End If
                Else
                    'insert data
                    'oApp.insertBorrowerInfo(obj, usrLogin)

                    lblNotif.Text = checkCif()
                    If lblNotif.Text = "" Then
                        oApp.insertBorrowerInfo(obj, usrLogin)
                    Else
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + lblNotif.Text + "');", True)
                        txtCIF.Focus()
                    End If
                End If
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
    End Sub

    Function checkAcctno()
        Dim lNot As String = ""
        Dim ds As DataSet
        Dim dt As DataTable
        If txtAcctno.Text <> "" Then
            ds = oApp.checkBorrowerByAcctno(txtAcctno.Text)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                lNot &= "&bull;&nbsp;" & "No. Rekening sudah ada.<br/>"
            End If
        End If
        Return lNot
    End Function

    Function checkCif()
        Dim info As String
        info = ""
        Dim ds As DataSet
        Dim dt As DataTable

        If txtCIF.Text <> "" Then
            ds = oApp.checkBorrowerByCIF(txtCIF.Text)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                info &= "&bull;&nbsp;" & "No. CIF sudah ada.<br/>"
            End If
        End If
        Return info
    End Function
End Class