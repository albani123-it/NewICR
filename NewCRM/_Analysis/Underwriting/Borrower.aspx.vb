Imports System.IO
Imports System.Security.AccessControl

Public Class Borrower
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Informasi Nasabah"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
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
        If Session("appno") IsNot Nothing Then
            cekSector(Session("appno"))
            appno.Value = Session("appno")
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
            If Not IsPostBack Then
                cekApp(Session("appno"))
            End If
            cekWorkflow(Session("appno"))
            executeJavascript()
        Else
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
        End If
    End Sub

    Sub executeJavascript()
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    url = dataTable.Rows(i).Item("lsm_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "a_aBc('" & url & "','" & pagename & "','" & appno.Value & "');"
        Dim javascript = "<script type='text/javascript'>" & script & "</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
    End Sub

    Sub cekSector(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerSector(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            sector = dt.Rows(0).Item("brw_sec")
        End If
    End Sub

    Sub getMenuNext()
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            Dim dt As DataTable
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    dt = oApp.getNextMenuByWorkflowRole(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dataTable.Rows(i).Item("lsm_id"), sector)
                    If dt.Rows.Count > 0 Then
                        menuNext = dt.Rows(0).Item("lsm_link")
                    Else
                        Dim dt2 As DataTable
                        dt2 = oApp.getDetailOfMenu(dataTable.Rows(i).Item("lsm_id"))
                        If dt2.Rows.Count > 0 Then
                            menuNext = dt2.Rows(0).Item("lsm_link")
                        End If
                    End If
                End If
            Next
        Else
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    Dim dt2 As DataTable
                    dt2 = oApp.getDetailOfMenu(dataTable.Rows(i).Item("lsm_id"))
                    If dt2.Rows.Count > 0 Then
                        menuNext = dt2.Rows(0).Item("lsm_link")
                    End If
                End If
            Next
        End If

    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        dt = oApp.getChildOfMenu(id, sector)
        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lsm_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        dt = oApp.getChildOfMenu(id, sector)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To dtWorkflow.Rows.Count - 1
                    If dtWorkflow.Rows(j).Item("lwr_field") = dt.Rows(i).Item("lsm_field") Then
                        If dtWorkflow.Rows(j).Item("lwr_role") = "HD" Then
                            count = count + 1
                        End If
                    End If
                Next
            Next
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lsm_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

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

    Sub controlHome()
        btnNext.Visible = False
    End Sub

    Sub fillDdl()
        Dim dt As DataTable
        'dt = oRat.getDropDownList("RFJOB")
        'ddlPekerjaan.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlPekerjaan.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        'dt = oRat.getDropDownList("RFCOMPTYPE")
        'ddlJenisDebitur.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlJenisDebitur.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        'dt = oRat.getDropDownList("RFMARITAL")
        'ddlStatusPerkawinan.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlStatusPerkawinan.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        'dt = oRat.getDropDownList("RFSEX")
        'ddlJenisKelamin.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlJenisKelamin.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        dt = oRat.getDropDownList("RFBUSINESSSECTOR")
        ddlSektorEkonomi.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlSektorEkonomi.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

        'dt = oRat.getDropDownList("RFBUSINESSSUBSECTOR")
        'ddlSubSektor.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlSubSektor.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        dt = oRat.getDropDownList("BRWBUSINESSSECTOR")
        ddlBidangUsaha.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlBidangUsaha.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

        'dt = oRat.getDropDownList("BRWGROUP")
        'ddlGrupUsaha.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlGrupUsaha.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If
    End Sub

    Sub fillTahunHubBank()
        Dim year As String = Now.ToString("yyyy")
        Dim x As Integer = CInt(year)
        ddlTahunHubunganDenganBTN.Items.Add(New ListItem("Pilih", "Pilih"))
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

    Function fillBrwCode()
        Dim brw As String = ""
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getCounter("Borrower")
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            brw = dt.Rows(0).Item("lsc_brw_code")
        End If
        Return brw
    End Function

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            CheckReportFIN()
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Or dtbl.Rows(0).Item("app_brw_code") = "" Then
                Dim brw As String = ""
                brw = fillBrwCode()
                isiData(brw, no)
            Else
                brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
                lblBrwCode.Text = dtbl.Rows(0).Item("app_brw_code")
                isiData(dtbl.Rows(0).Item("app_brw_code"), no)
            End If
            lblAppNo_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("app_no"))
            lblBrwName_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("lsb_namaPemohon"))
        End If
    End Sub

    Sub cekWorkflow(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                execWorkflow(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_convent"))
                If usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If
            End If
        End If
    End Sub

    Sub isiData(ByVal brwcode, ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrower(brwcode)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlJenisDebitur.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_jenisDebitur"))
            txtAcctno.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_acctno"))
            'If ddlJenisDebitur.SelectedValue = "2" Or ddlJenisDebitur.SelectedItem.Text = "Lembaga" Then
            '    txtGelarDepan.Enabled = False
            '    txtGelarBelakang.Enabled = False
            '    txtStatusGelar.Enabled = False
            '    txtKetStatusGelar.Enabled = False
            '    ddlJenisKelamin.Enabled = False
            '    ddlPekerjaan.Enabled = False
            '    ddlStatusPerkawinan.Enabled = False
            '    txtTempatLahir.Enabled = False
            '    txtNamaGadisIbuKandung.Enabled = False
            '    txtGelarDepan.Text = ""
            '    txtGelarBelakang.Text = ""
            '    txtStatusGelar.Text = ""
            '    txtKetStatusGelar.Text = ""
            '    ddlJenisKelamin.SelectedValue = "Pilih"
            '    ddlPekerjaan.SelectedValue = "Pilih"
            '    ddlStatusPerkawinan.SelectedValue = "Pilih"
            '    txtTempatLahir.Text = ""
            '    txtNamaGadisIbuKandung.Text = ""
            'End If
            'ddlJenisKelamin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_jenisKelamin"))
            txtCIF.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_cif"))
            'ddlPekerjaan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_pekerjaan"))
            txtNamaPemohon.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaPemohon"))
            'ddlStatusPerkawinan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_statusKawin"))
            'txtGelarDepan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_gelarD"))
            'txtGelarBelakang.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_gelarB"))
            txtNPWP.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_npwp"))
            txtTglLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tglLahir"))
            'txtStatusGelar.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_statusGelar"))
            'txtTempatLahir.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_tempatLahir"))
            'txtKetStatusGelar.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_ketStatusGelar"))
            'txtNamaGadisIbuKandung.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaGadisIbu"))
            txtNoKTPAkte.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_noKTPAkte"))
            txtDeskripsi.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_deskripsi"))
            ddlSektorEkonomi.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_sektorEko"))
            'ddlSubSektor.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_subSektorEko"))
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
            'txtSandiLokasiBI.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_sandiBI"))
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
            'ddlGrupUsaha.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsb_grupUsaha"))
            txtGrupUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_grupUsaha"))
        End If
        'ds = oApp.checkFacilityKreditInfoFromBrw(no)
        'dt = ds.Tables(0)
        'If dt.Rows.Count > 0 Then
        '    txtNamaProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_nama"))
        '    txtAlamatProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_alamat"))
        '    txtKodePosProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kodePos"))
        '    txtKelurahanProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kelurahan"))
        '    txtKecamatanProyekUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kecamatan"))
        '    txtKotamadyaKabupatenUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kecamatan"))
        '    txtPropinsiUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_propinsi"))
        '    txtKodeAreaTlpUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kodeAreaTlp"))
        '    txtTlpUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_tlp"))
        '    txtExtTlpUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_extTlp"))
        '    txtKodeAreaTlpUsaha2.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kodeAreaTlp2"))
        '    txtTlpUsaha2.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_tlp2"))
        '    txtExtTlpUsaha2.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_extTlp2"))
        '    txtKodeAreaTlpUsaha3.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kodeAreaTlp3"))
        '    txtTlpUsaha3.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_tlp3"))
        '    txtExtTlpUsaha3.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_extTlp3"))
        '    txtKodeAreaFaxUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kodeAreaFax"))
        '    txtFaxUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_fax"))
        '    txtExtFaxUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_extFax"))
        '    txtLamaUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_lamaUsaha"))
        'End If

    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet
        ds = oApp.checkWorkflowRole(workflow, posisi, sector)
        dtWorkflow = ds.Tables(0)
        For j As Integer = 0 To dtWorkflow.Rows.Count - 1
            If dtWorkflow.Rows(j).Item("lsm_name") = pagename Then
                If dtWorkflow.Rows(j).Item("lwr_role") = "RO" Then
                    controlRO()
                Else
                    If Not convent Then
                        If dtWorkflow.Rows(j).Item("lsm_edit") = 0 Then
                            controlRO()
                        ElseIf dtWorkflow.Rows(j).Item("lsm_edit") = 2 Then
                            controlEdit()
                        End If
                    End If
                End If
            End If

        Next
    End Sub

    Sub controlEdit()

    End Sub

    Sub controlRO()
        chkAlamatRumah.Disabled = True
        chkAlamatKantorKTP.Disabled = True
        chkAlamatUsaha.Disabled = True
        ddlJenisDebitur.Enabled = False
        ddlJenisDebitur.CssClass = "inpDdlDisabled"
        'ddlJenisKelamin.Enabled = False
        'ddlJenisKelamin.CssClass = "inpDdlDisabled"
        txtCIF.Enabled = False
        txtAcctno.Enabled = False
        btnLook.Enabled = False
        'ddlPekerjaan.Enabled = False
        'ddlPekerjaan.CssClass = "inpDdlDisabled"
        txtNamaPemohon.Enabled = False
        'ddlStatusPerkawinan.Enabled = False
        'ddlStatusPerkawinan.CssClass = "inpDdlDisabled"
        'txtGelarDepan.Enabled = False
        'txtGelarBelakang.Enabled = False
        txtNPWP.Enabled = False
        txtTglLahir.Enabled = False
        'txtStatusGelar.Enabled = False
        'txtTempatLahir.Enabled = False
        'txtKetStatusGelar.Enabled = False
        'txtNamaGadisIbuKandung.Enabled = False
        txtNoKTPAkte.Enabled = False
        txtDeskripsi.Enabled = False
        ddlSektorEkonomi.Enabled = False
        ddlSektorEkonomi.CssClass = "inpDdlDisabled"
        'ddlSubSektor.Enabled = False
        'ddlSubSektor.CssClass = "inpDdlDisabled"
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
        'txtBulanHubunganDenganBTN.Enabled = False
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
        'ddlGrupUsaha.Enabled = False
        'ddlGrupUsaha.CssClass = "inpDdlDisabled"
        txtGrupUsaha.CssClass = "inpTxtDisabled"
        txtGrupUsaha.ReadOnly = True

        txtAlamatSurat.Enabled = False
        txtKodePosSurat.Enabled = False
        txtKelurahanSurat.Enabled = False
        txtKecamatanSurat.Enabled = False
        txtKotamadyaKabupatenSurat.Enabled = False
        'txtSandiLokasiBI.Enabled = False
        txtPropinsiSurat.Enabled = False
        txtKodeAreaTlpSurat.Enabled = False
        txtTlpSurat.Enabled = False
        txtExtTlpSurat.Enabled = False
        txtEmailSurat.Enabled = False
        btnSave.Visible = False
        btnNext.Visible = True
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
            'obj.grupUsaha = ddlGrupUsaha.SelectedValue
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
                'Dim dset As DataSet
                'Dim dtbl As DataTable
                'dset = oApp.checkApp(appno.Value)
                'dtbl = dset.Tables(0)
                'If dtbl.Rows.Count > 0 Then
                '    oApp.updateAppInfo(appno.Value, brw_code.Value, usrLogin)
                'End If
                'ds = oRat.checkFacilityCredit(appno.Value)
                'dt = ds.Tables(0)
                'If dt.Rows.Count > 0 Then
                '    'update data
                '    oApp.updateFacilityKreditInfoFromBrw(obj, appno.Value)
                'Else
                '    'insert data
                '    oApp.insertFacilityKreditInfoFromBrw(obj, appno.Value)
                'End If
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
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
                If txtMenjadiDebiturSejak.Text <> "" Then
                    Dim HubDenganDKI As String = "01-" & temp_month & "-" & ddlTahunHubunganDenganBTN.SelectedValue
                    Dim menjadiDebitur As String = Request.Form(txtMenjadiDebiturSejak.UniqueID).Substring(0, 2) & "-" & Request.Form(txtMenjadiDebiturSejak.UniqueID).Substring(3, 2) & "-" & Request.Form(txtMenjadiDebiturSejak.UniqueID).Substring(6, 4)
                    lNot &= oGen.checkDateParInHtml("more than", "Menjadi Debitur DKI Sejak", "Hubungan dengan Bank DKI Sejak", menjadiDebitur, HubDenganDKI)
                    lNot &= oGen.checkDateParInHtml("more", "Tanggal Permohonan Debitur", "Tanggal Penerimaan Berkas", txtTglPermohonanDebitur.Text, txtTglPenerimaanBerkas.Text)
                End If
            End If

        Catch ex As Exception
            lNot &= "&bull;&nbsp;" & ex.Message & "<br/>"
        End Try


        'Dim ds As DataSet
        'Dim dt As DataTable

        'If txtCIF.Text <> "" Then
        '    ds = oApp.checkBorrowerByCIF(txtCIF.Text)
        '    dt = ds.Tables(0)
        '    If dt.Rows.Count > 0 Then
        '        lNot &= "No. CIF sudah ada.<br/>"
        '    End If
        'End If



        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
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

    Private Sub btnLook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLook.Click
        If txtCIF.Text = "" Then
            liBodyLoadAction.Text = "alert('No. CIF harus diisi.');"
        Else
            'Dim ds As DataSet
            'Dim dt As DataTable
            'ds = oApp.checkBorrowerByCIF(txtCIF.Text)
            'dt = ds.Tables(0)
            'If dt.Rows.Count > 0 Then
            '    isiData(dt.Rows(0).Item("lsb_brw_code"))
            'Else
            '    liBodyLoadAction.Text = "alert('Data Nasabah tidak ditemukan.');"
            'End If

        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If
    End Sub

    'Private Sub ddlJenisDebitur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisDebitur.SelectedIndexChanged
    '    If ddlJenisDebitur.SelectedValue = 2 Or ddlJenisDebitur.SelectedItem.Text = "Lembaga" Then
    '        txtGelarDepan.Enabled = False
    '        txtGelarBelakang.Enabled = False
    '        txtStatusGelar.Enabled = False
    '        txtKetStatusGelar.Enabled = False
    '        ddlJenisKelamin.Enabled = False
    '        ddlPekerjaan.Enabled = False
    '        ddlStatusPerkawinan.Enabled = False
    '        txtTempatLahir.Enabled = False
    '        txtNamaGadisIbuKandung.Enabled = False

    '        txtGelarDepan.Text = ""
    '        txtGelarBelakang.Text = ""
    '        txtStatusGelar.Text = ""
    '        txtKetStatusGelar.Text = ""
    '        ddlJenisKelamin.SelectedValue = "Pilih"
    '        ddlPekerjaan.SelectedValue = "Pilih"
    '        ddlStatusPerkawinan.SelectedValue = "Pilih"
    '        txtTempatLahir.Text = ""
    '        txtNamaGadisIbuKandung.Text = ""
    '    Else
    '        txtGelarDepan.Enabled = True
    '        txtGelarBelakang.Enabled = True
    '        txtStatusGelar.Enabled = True
    '        txtKetStatusGelar.Enabled = True
    '        ddlJenisKelamin.Enabled = True
    '        ddlPekerjaan.Enabled = True
    '        ddlStatusPerkawinan.Enabled = True
    '        txtTempatLahir.Enabled = True
    '        txtNamaGadisIbuKandung.Enabled = True
    '        isiData(brw_code.Value, appno.Value)
    '    End If
    'End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblNotif.Text = ""
        cekInputUpload()
        Dim type As String = "FIN"

        If lblNotif.Text = "" Then
            Dim mkFolder As String
            mkFolder = "~/Document Upload/" & appno.Value & "/" + type + "/"
            Dim filename As String = ""
            filename = type + "_" + appno.Value + ".pdf"
            Try
                'check folder exists
                If Not Directory.Exists(Server.MapPath(mkFolder)) Then
                    editpermit()
                    Directory.CreateDirectory(Server.MapPath(mkFolder))
                End If

                'check file exists
                If System.IO.File.Exists(Server.MapPath(mkFolder) & filename) Then
                    System.IO.File.Delete(filename)
                End If

                FileUpload1.SaveAs(Server.MapPath(mkFolder) + filename)

                lblFileUpload.Text = filename

            Catch ex As Exception

                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & ex.Message & ".');", True)

            End Try
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & lblNotif.Text & ".');", True)
        End If
    End Sub

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Sub CheckReportFIN()
        Dim path As String
        Dim filename As String
        Dim type As String = "FIN"
        filename = type + "_" + appno.Value + ".pdf"
        path = "~/Document Upload/" & appno.Value & "/" + type + "/" + filename
        If File.Exists(Server.MapPath(path)) Then
            lblFileUpload.Text = filename
        End If

    End Sub


    Sub cekInputUpload()
        Dim lMsg As String = ""

        If FileUpload1.HasFile Then
            'Dim maxLength As Integer = 20971520
            Dim file As HttpPostedFile = DirectCast(FileUpload1.PostedFile, HttpPostedFile)
            Dim filesize As Integer = file.ContentLength
            Dim filename As String = ""

            'folderUrl = mkFolder & "/"

            'Dim path As String = Server.MapPath(folderUrl)
            filename = Path.GetFileName(FileUpload1.FileName)

            Dim _filename = filename.Split(".")

            filename = _filename(_filename.Length - 1)

            If filesize > 20971520 Then
                lMsg = lMsg + "File lebih dari 20 MB.<br/>"
            End If

            If filename.ToLower() <> "pdf" Then
                lMsg = lMsg + "Bukan File PDF"
            End If
        Else
            lMsg = lMsg + "Pilih file dahulu diisi"
        End If

        If lMsg <> "" Then
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub
End Class