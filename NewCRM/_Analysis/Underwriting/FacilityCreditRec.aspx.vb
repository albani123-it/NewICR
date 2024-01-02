Imports System.Drawing
Imports System.IO
Imports System.Security.AccessControl
Imports Microsoft.VisualBasic
Imports System.Net.Mime

Public Class FacilityCreditRec
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected allowDelete As Boolean = True
    Protected allowUpload As Boolean = True
    Protected dataTable As DataTable
    Protected pagename As String = "Rekomendasi Permohonan Kredit"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Protected proses As Integer = 1
    Protected history As Boolean = False

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        lblNotif.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        controlHome()
        If Not IsPostBack Then
            fillDropdown()
            'xtxtJadwalPembayaranPokokKredit.Text = Now.ToString("dd-MM-yyyy")
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
            cekHistory(Session("appno"))
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

    Sub fillDropdown()
        Dim dtFasilitas As DataTable
        dtFasilitas = oRat.getDropDownList("RFPRODUCT")
        ddlJenisKredit.DataSource = dtFasilitas
        ddlJenisKredit.DataTextField = "ldd_desc"
        ddlJenisKredit.DataValueField = "ldd_code"
        ddlJenisKredit.DataBind()

        Dim dtKeputusan As DataTable
        dtKeputusan = oRat.getDropDownList("RFDECISION")
        ddlRecommend.DataSource = dtKeputusan
        ddlRecommend.DataTextField = "ldd_desc"
        ddlRecommend.DataValueField = "ldd_code"
        ddlRecommend.DataBind()

        Dim dtJenisPengajuan As DataTable
        dtJenisPengajuan = oRat.getDropDownList("RFAPPLICATIONTYPE")
        ddlJenisPengajuan.DataSource = dtJenisPengajuan
        ddlJenisPengajuan.DataTextField = "ldd_desc"
        ddlJenisPengajuan.DataValueField = "ldd_code"
        ddlJenisPengajuan.DataBind()

        'ddlJangkaWaktuSP2K.Items.Clear()
        'ddlJangkaWaktuSP2K.Items.Add(New ListItem(30, 30))
        'ddlJangkaWaktuSP2K.Items.Add(New ListItem(60, 60))
        'ddlJangkaWaktuSP2K.Items.Add(New ListItem("Lainnya", "Lainnya"))

        'ddlSkimKMK.Items.Add(New ListItem("Aflopend", "Aflopend"))
        'ddlSkimKMK.Items.Add(New ListItem("RC Terbatas", "RC Terbatas"))

        Dim dtSistemPembayaran As DataTable
        dtSistemPembayaran = oRat.getDropDownList("RFPAYMENTSYS")
        ddlSistemPembayaran.DataSource = dtSistemPembayaran
        ddlSistemPembayaran.DataTextField = "ldd_desc"
        ddlSistemPembayaran.DataValueField = "ldd_code"
        ddlSistemPembayaran.DataBind()

        Dim dtJenisCurr As DataTable
        dtJenisCurr = oRat.getDropDownList("RFCURRENCY")
        ddlJenisValas.DataSource = dtJenisCurr
        ddlJenisValas.DataTextField = "ldd_desc"
        ddlJenisValas.DataValueField = "ldd_code"
        ddlJenisValas.DataBind()

        Dim dtSifat As DataTable
        dtSifat = oRat.getDropDownList("RFSIFATKREDIT")
        ddlSifatKredit.DataSource = dtSifat
        ddlSifatKredit.DataTextField = "ldd_desc"
        ddlSifatKredit.DataValueField = "ldd_code"
        ddlSifatKredit.DataBind()
    End Sub

    Sub controlHome()
        grid2.Visible = False
        filAttch.Visible = False
        btnNext.Visible = False
        txtNilaiValas.ReadOnly = True
        txtNilaiValas.CssClass = "inpTxtDisabled"
        'txtMaxKredit.ReadOnly = True
        'txtMaxKredit.CssClass = "inpTxtDisabled"
        CheckValas()
    End Sub

    Sub CheckValas()
        If ddlJenisValas.SelectedValue = "IDR" Then
            txtMaksimumKreditRec.ReadOnly = False
            txtMaksimumKreditRec.CssClass = "inpTxt"
            txtMaxKredit.CssClass = "inpTxtDisabled"
        ElseIf ddlJenisValas.SelectedValue = "USD" Then
            txtMaksimumKreditRec.ReadOnly = True
            txtMaksimumKreditRec.CssClass = "inpTxtDisabled"
            txtMaxKredit.CssClass = "inpTxt"
        End If
    End Sub


    Sub cekHistory(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkRecommendationHistory(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then

            'If dt.Rows(0).Item("lcr_process") <> proses Then
            history = True
            '    tblInput.Visible = False
            '    btnSave.Visible = False
            '    btnNext.Visible = True
            'End If
        End If

        ds = oApp.checkRecommendationHistoryFile(no, proses)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                grid2.DataSource = ds
                grid2.DataBind()
            Next
            grid2.Visible = True
            filAttch.Visible = True
        End If
    End Sub

    Function bindHistory(ByVal no)
        Dim html As String = ""
        Dim last As Boolean = False
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkRecommendationHistory(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("lcr_process") = proses And i = proses Then
                    last = True
                Else
                    last = False
                End If
            Next
            If last = False Then
                html &= "<fieldset style='margin:20px 0;border-radius:5px;background:#ebecec;'>"
                html &= "<legend><label style='font-weight:bold;'>History</label></legend>"
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("lcr_process") <> proses Then
                        html &= "<div stlye='margin-top:10px;'>"
                        html &= "<table class='tbl' cellspacing='0' cellpadding='3' width='100%' style='text-align:left;border: thin solid #bcbcbc;border-radius: 5px;'>"
                        html &= "<tr>"
                        html &= "<td colspan='4' background='../../Images/bg-head.gif' bgcolor='#ddedf6' style='border-radius: 5px 5px 0 0;font-weight:bold;color:#fff;'>"
                        html &= "<label> PERMOHONAN KREDIT YANG DIREKOMENDASIKAN - User : " & oGen.CheckNullString(dt.Rows(i).Item("usr_name")) & " - User Role : " & oGen.CheckNullString(dt.Rows(i).Item("lu_level_name")) & "</label>"
                        html &= "</td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Jenis Pengajuan</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblJenisPengajuan")) & "</label></td>"
                        html &= "<td width='20%'><b>Suku Bunga</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_sukuBunga")) & " % " & oGen.CheckNullString(dt.Rows(i).Item("lcr_jenisSukuBunga")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Jenis Fasilitas</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblJenisKredit")) & "</label></td>"
                        html &= "<td width='20%'><b>Sifat Kredit</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblSifat")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Sistem Pembayaran</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblPayment")) & "</label></td>"
                        html &= "<td width='20%'><b>Maksimum Tarikan Kredit</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcr_maxTarikanKredit")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Jenis Currency</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblValas")) & "</label></td>"
                        html &= "<td width='20%'><b>Provisi</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_provisi")) & " %</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Nilai Kurs</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_nilaiValas")) & "</label></td>"
                        html &= "<td width='20%'><b>Biaya Administrasi</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcr_biayaAdmin")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Maksimum Kredit (Dalam Valas)</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcr_maxKreditValas")) & "</label></td>"
                        html &= "<td width='20%'><b>Denda Tunggakan</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_denda")) & " %</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Maksimum Kredit (Dalam Rupiah)</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcr_maxKreditRec")) & "</label></td>"
                        html &= "<td width='20%'><b>Usulan</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lbldecision")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Jangka Waktu</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_jangkaWaktuRec")) & " Bulan</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Grace Period</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_gracePeriod")) & " Bulan</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Pendapat</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_pendapat")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Pertimbangan</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcr_pertimbangan")) & "</label></td>"
                        html &= "</tr>"
                        html &= "</table>"
                        html &= "</div>"
                    End If
                Next
                html &= "</fieldset>"
            End If
        End If
        Return html
    End Function

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            CheckReportIPK()
            proses = dtbl.Rows(0).Item("app_position")
            isiData(no)
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
                proses = dtbl.Rows(0).Item("app_position")
                If usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If
            End If
        End If
    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet
        'Dim dt As DataTable
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
            ElseIf dtWorkflow.Rows(j).Item("lwr_field") = "lwr_fac_rating" Or dtWorkflow.Rows(j).Item("lwr_field") = "lwr_brw_rating" Then
                If dtWorkflow.Rows(j).Item("lwr_role") = "HD" Then
                    tblRating.Visible = False
                End If
            End If

        Next
    End Sub

    Sub controlEdit()

    End Sub

    Sub controlRO()
        'txtJenisPengajuan.Enabled = False
        ddlJenisPengajuan.Enabled = False
        ddlJenisPengajuan.CssClass = "inpDdlDisabled"
        ddlJenisKredit.Enabled = False
        ddlJenisKredit.CssClass = "inpDdlDisabled"
        ddlJenisValas.Enabled = False
        ddlJenisValas.CssClass = "inpDdlDisabled"
        txtNilaiValas.Enabled = False
        'ddlJangkaWaktuSP2K.Enabled = False
        'ddlJangkaWaktuSP2K.CssClass = "inpDdlDisabled"
        ddlSistemPembayaran.Enabled = False
        ddlSistemPembayaran.CssClass = "inpDdlDisabled"
        txtMaxKredit.Enabled = False
        txtMaksimumKreditRec.Enabled = False
        txtMaksimumKreditRec.CssClass = "inpDdlDisabled"
        txtJangkaWaktuRec.Enabled = False
        txtGracePeriod.Enabled = False
        txtSukuBunga.Enabled = False
        RadBunga.Enabled = False
        txtProvisi.Enabled = False
        'txtCommitmentFee.Enabled = False
        txtBiayaAdmin.Enabled = False
        txtDenda.Enabled = False
        'txtBebanBungaPerBulan.Enabled = False
        'txtAngsuranPokokPerBulan.Enabled = False
        'txtTotalAngsuranPokokBunga.Enabled = False
        ddlSifatKredit.Enabled = False
        ddlSifatKredit.CssClass = "inpDdlDisabled"
        txtMaxTarikanKredit.Enabled = False
        
        txtPendapat.Enabled = False
        txtPertimbangan.Enabled = False
        'chkRec.Disabled = True
        ddlRecommend.Enabled = False
        ddlRecommend.CssClass = "inpDdlDisabled"
        'txtJadwalPembayaranPokokKredit.Enabled = False
        ' txtProyeksiBunga.Enabled = False

        allowDelete = False
        allowUpload = False
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Sub isiData(ByVal app_no)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oApp.checkCreditRecommendation(app_no, proses)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlJenisPengajuan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_jenisPengajuan"))
            ddlJenisKredit.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_jenisKredit"))
            ddlJenisValas.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_jenisValas"))
            txtNilaiValas.Text = oGen.CheckNullString(dt.Rows(0).Item("lcr_nilaiValas"))
            'ddlJangkaWaktuSP2K.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_jangkaWaktuSP2K"))
            ddlSistemPembayaran.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_sistemPembayaran"))
            txtMaxKredit.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_maxKreditValas"))
            txtMaksimumKreditRec.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_maxKreditRec"))
            txtJangkaWaktuRec.Text = oGen.CheckNullInt(dt.Rows(0).Item("lcr_jangkaWaktuRec"))
            txtGracePeriod.Text = oGen.CheckNullInt(dt.Rows(0).Item("lcr_gracePeriod"))
            CheckValas()

            'If oGen.CheckNullString(dt.Rows(0).Item("lbljadwalPembayaranPokokKredit")) = "01-01-1900" Then
            '    txtJadwalPembayaranPokokKredit.Text = Now.ToString("dd-MM-yyyy")
            'Else
            '    txtJadwalPembayaranPokokKredit.Text = oGen.CheckNullString(dt.Rows(0).Item("lbljadwalPembayaranPokokKredit"))
            'End If

            'txtProyeksiBunga.Text = oGen.CheckNullString(dt.Rows(0).Item("lcr_proyeksiBunga"))
            txtSukuBunga.Text = oGen.CheckNullInt(dt.Rows(0).Item("lcr_sukuBunga"))
            txtProvisi.Text = oGen.CheckNullInt(dt.Rows(0).Item("lcr_provisi"))
            'txtCommitmentFee.Text = oGen.CheckNullString(dt.Rows(0).Item("lcr_commitmentFee"))
            txtBiayaAdmin.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_biayaAdmin"))
            'txtBebanBungaPerBulan.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_bebanBungaPerBulan"))
            'txtAngsuranPokokPerBulan.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_angsuranPokokPerBulan"))
            'txtTotalAngsuranPokokBunga.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_totalAngsuranPokokBunga"))
            ddlSifatKredit.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_sifatKredit"))

            If oGen.CheckNullString(ddlSifatKredit.SelectedItem.Text) = "NON REVOLVING" Then
                txtMaxTarikanKredit.ReadOnly = True
                txtMaxTarikanKredit.CssClass = "inpTxtDisabled"
            End If
            txtMaxTarikanKredit.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcr_maxTarikanKredit"))
            'ddlSkimKMK.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_skimKMK"))
            txtPendapat.Text = oGen.CheckNullString(dt.Rows(0).Item("lcr_pendapat"))
            txtPertimbangan.Text = oGen.CheckNullString(dt.Rows(0).Item("lcr_pertimbangan"))
            RadBunga.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_jenisSukuBunga"))
            txtDenda.Text = oGen.CheckNullInt(dt.Rows(0).Item("lcr_denda"))

            ddlRecommend.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcr_rec"))

            
        End If

        Dim recomApp As Integer = -1
        Dim recomBrw As Integer = -1
        lblRatingApp.Text = "N/A"
        lblRatingBrw.Text = "N/A"

        '===========rating====================
        ds = oApp.checkAppRating(app_no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblRatingApp.Text = oGen.CheckNullString(dt.Rows(0).Item("lfr_rating"))
            If dt.Rows(0).Item("lfr_score") >= dt.Rows(0).Item("appScore") Then
                recomApp = 1
            End If
            If dt.Rows(0).Item("lfr_score") <= dt.Rows(0).Item("rejectScore") Then
                recomApp = 0
            End If
            'Else
            '    lblNotif.Text &= "Data rating fasilitas tidak ada. Silahkan isi rating fasilitas terlebih dahulu. Klik <a href='FacilityRating.aspx?appno=" & app_no & "' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman rating fasilitas.<br/>"
            '    divnotif.Attributes.Add("style", "display:''")
            '    controlRO()
        End If
        ds = oApp.checkBrwRating(app_no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblRatingBrw.Text = oGen.CheckNullString(dt.Rows(0).Item("lbr_rating"))
            If dt.Rows(0).Item("lbr_score") >= dt.Rows(0).Item("appScore") Then
                recomBrw = 1
            End If
            If dt.Rows(0).Item("lbr_score") <= dt.Rows(0).Item("rejectScore") Then
                recomBrw = 0
            End If
            'Else
            '    lblNotif.Text &= "Data rating nasabah tidak ada. Silahkan isi rating nasabah terlebih dahulu. Klik <a href='BorrowerRating.aspx?appno=" & app_no & "' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman rating nasabah.<br/>"
            '    divnotif.Attributes.Add("style", "display:''")
            '    controlRO()
        End If

        If recomApp = 1 And recomBrw = 1 Then
            ddlRecommend.SelectedValue = "True"
        ElseIf recomApp = 0 Or recomBrw = 0 Then
            ddlRecommend.SelectedValue = "False"
        End If
        '===========end rating================

        '===========attachment================
        ds = oApp.checkCreditRecommendationFile(appno.Value, "All", proses)
        grid1.DataSource = ds
        grid1.DataBind()
        '===========end attachment============

        ds = oApp.getDetailOfRequestCredit(app_no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlJenisPengajuan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisPengajuan"))
            ddlJenisKredit.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_fasilitas"))
            lblJenisPengajuanReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lblJenisPengajuan"))
            lblJenisKreditReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lblJenisKredit"))
            lblJenisValasReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lblValas"))
            lblNilaiValasReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kurs"))
            lblSistemPembayaranReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lblPayment"))
            lblMaxKreditReq.Text = oGen.moneyFormatLabel(dt.Rows(0).Item("lfc_nilaiValas"))
            lblNilaiRupiahReq.Text = oGen.moneyFormatLabel(dt.Rows(0).Item("lfc_nilaiRupiah"))
            lblJangkaWaktuReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_jangkaWaktu"))
            lblGracePeriodReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_grace"))
            lblSukuBungaReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_bungaPersen"))
            lblTipeBungaReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisBunga"))
            lblSifatKreditReq.Text = oGen.CheckNullString(dt.Rows(0).Item("lblSifat"))
            'lblTotalAngsuranReq.Text = oGen.moneyFormatLabel(dt.Rows(0).Item("lfc_total"))
        Else
            lblNotif.Text &= "Data permohonan kredit tidak ada. Silahkan isi permohonan kredit terlebih dahulu. Klik <a href='FacilityCredit.aspx?appno=" & app_no & "' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman permohonan kredit.<br/>"
            divnotif.Attributes.Add("style", "display:''")
            req.Visible = False
            controlRO()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblNotif.Text = ""
        ''cekInput()
        If lblNotif.Text = "" Then
            Dim ds As DataSet
            Dim dt As DataTable
            If appno.Value <> "" Then
                Try

                    Dim obj As New AAS.Business.Entity.facilityCredit
                    Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
                    obj.noApp = appno.Value
                    obj.jenisPengajuan = Request.Form(master & "ddlJenisPengajuan")
                    obj.jenisKredit = Request.Form(master & "ddlJenisKredit")
                    obj.jenisValas = Request.Form(master & "ddlJenisValas")
                    obj.nilaiValas = Request.Form(master & "txtNilaiValas")
                    'obj.jangkaWaktuSP2K = ""
                    obj.sistemPembayaran = Request.Form(master & "ddlSistemPembayaran")
                    obj.maxKreditValas = Request.Form(master & "txtMaxKredit").Replace(",", "")
                    obj.maxKreditRec = Request.Form(master & "txtMaksimumKreditRec").Replace(",", "")
                    obj.jangkaWaktuRec = Request.Form(master & "txtJangkaWaktuRec")
                    obj.gracePeriod = Request.Form(master & "txtGracePeriod")
                    'If txtJadwalPembayaranPokokKredit.Text = "" Then
                    '    obj.jadwalPembayaranPokokKredit = "1900-01-01"
                    'Else
                    '    obj.jadwalPembayaranPokokKredit = Mid(txtJadwalPembayaranPokokKredit.Text, 7, 4) + "-" & Mid(txtJadwalPembayaranPokokKredit.Text, 4, 2) + "-" & Mid(txtJadwalPembayaranPokokKredit.Text, 1, 2)
                    'End If
                    'obj.proyeksiBunga = txtProyeksiBunga.Text
                    obj.sukuBunga = Request.Form(master & "txtSukuBunga")
                    obj.jenisSukuBunga = Request.Form(master & "RadBunga")
                    'If rdFlat.Checked = True Then
                    '    obj.jenisSukuBunga = "Flat"
                    'ElseIf rdAnnuitas.Checked = True Then
                    '    obj.jenisSukuBunga = "Annuitas"
                    'ElseIf rdEfektif.Checked = True Then
                    '    obj.jenisSukuBunga = "Efektif"
                    'End If
                    obj.provisi = Request.Form(master & "txtProvisi")
                    'obj.commitmentFee = txtCommitmentFee.Text
                    obj.biayaAdmin = Request.Form(master & "txtBiayaAdmin").Replace(",", "")
                    obj.denda = Request.Form(master & "txtDenda")
                    'obj.bebanBungaPerBulan = txtBebanBungaPerBulan.Text.Replace(",", "")
                    'obj.angsuranPokokPerBulan = txtAngsuranPokokPerBulan.Text.Replace(",", "")
                    'obj.totalAngsuranPokokBunga = txtTotalAngsuranPokokBunga.Text.Replace(",", "")
                    obj.sifatKredit = Request.Form(master & "ddlSifatKredit")
                    obj.maxTarikanKredit = Request.Form(master & "txtMaxTarikanKredit").Replace(",", "")

                    obj.recommendation = Request.Form(master & "ddlRecommend")
                    obj.pendapat = Request.Form(master & "txtPendapat")
                    obj.pertimbangan = Request.Form(master & "txtPertimbangan")
                    'If FileUpload1.HasFile Then
                    '    obj.file = FileUpload1.FileName
                    'Else
                    '    obj.file = ""
                    'End If
                    ds = oApp.checkCreditRecommendation(appno.Value, proses)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update data
                        oApp.updateFacilityCreditRecommendation(obj, proses, usrLogin)
                    Else
                        'insert data
                        oApp.insertFacilityCreditRecommendation(obj, proses, usrLogin)
                    End If
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                Catch ex As Exception
                    lblNotif.Text = ex.Message
                    divnotif.Attributes.Add("style", "display:''")
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lNot As String = ""
        lNot &= oGen.checkInputInaHTML("TextBox", "Maksimum Kredit Yang Diusulkan (Dalam Rupiah)", txtMaksimumKreditRec.Text)
        lNot &= oGen.checkInputInaHTML("TextBox", "Jangka Waktu", txtJangkaWaktuRec.Text)
        'lNot &= oGen.checkInputInaHTML("TextBox", "Suku Bunga", txtSukuBunga.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Jenis Valas", ddlJenisValas.SelectedValue)
        'lNot &= oGen.checkInputInaHTML("DropDownList", "Jangka Waktu SP2K", ddlJangkaWaktuSP2K.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Sifat Kredit", ddlSifatKredit.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Usulan", ddlRecommend.SelectedValue)
        If ddlSifatKredit.SelectedItem.Text = "REVOLVING" Then
            lNot &= oGen.checkInputInaHTML("DropDownList", "Maksinmum Tarikan Kredit", txtMaxTarikanKredit.Text)
        End If
        If txtJangkaWaktuRec.Text = 0 Then
            lNot &= "Jangka Waktu tidak boleh 0.<br/>"
        End If
        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub ddlJenisValas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisValas.SelectedIndexChanged
        Try
            Dim dt As DataTable
            dt = oRat.getDropDownList("RFCURRATE")
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("ldd_code") = ddlJenisValas.SelectedValue Then
                        'txtNilaiValas.Text = dt.Rows(i).Item("ldd_desc")
                        txtNilaiValas.Text = 1
                    End If
                Next
            End If
            If txtNilaiValas.Text = "Pilih" Then
                txtNilaiValas.Text = 0
            End If

            If ddlJenisValas.SelectedValue = "IDR" Then
                txtMaksimumKreditRec.Text = 0
                txtMaxKredit.Text = 0
            ElseIf ddlJenisValas.SelectedValue = "USD" Then
                txtMaksimumKreditRec.Text = 0
                txtMaxKredit.Text = 0
            End If

            If txtNilaiValas.Text = 0 Then
                txtMaxKredit.Text = 0
                'Else
                '    txtMaxKredit.Text = oGen.moneyFormatTextBox(CInt(txtNilaiValas.Text) * txtMaksimumKreditRec.Text)
            End If
        Catch ex As Exception
            lblNotif.Text = ex.Message
            divnotif.Attributes.Add("style", "display:''")
        End Try
    End Sub

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim docname As String = e.CommandArgument.ToString()
        Dim mkFolder As String
        mkFolder = "~/Document Upload/" & appno.Value & "/Recommendation/"
        If File.Exists(Server.MapPath(mkFolder) & docname) Then
            System.IO.File.Delete(Server.MapPath(mkFolder) & docname)
        End If
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oApp.checkCreditRecommendationFile(appno.Value, docname, proses)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                'delete data
                oApp.deleteCreditRecommendationFile(appno.Value, docname, proses)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran berhasil dihapus.');window.location.href=window.location.href", True)
            End If
        Catch ex As Exception
            lblNotif.Text = ex.Message
            divnotif.Attributes.Add("style", "display:''")
        End Try
    End Sub

    Protected Sub download(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim docname As String = e.CommandArgument.ToString()
        Dim mkFolder As String
        mkFolder = "~/Document Upload/" & appno.Value & "/Recommendation/"
        Dim pathes As String = Server.MapPath(mkFolder & docname)
        If File.Exists(pathes) Then
            Response.ContentType = "application/octet-stream"
            Dim cd = New ContentDisposition()
            cd.Inline = False
            cd.FileName = Path.GetFileName(pathes)
            Response.AppendHeader("Content-Disposition", cd.ToString())

            Dim fileData As Byte() = System.IO.File.ReadAllBytes(pathes)
            Response.OutputStream.Write(fileData, 0, fileData.Length)
            Response.[End]()

        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('File cannot be downloaded at this time.');window.location.href=window.location.href", True)
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If

    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        lblNotif.Text = ""
        cekInput()
        If lblNotif.Text = "" Then
            Dim ds As DataSet
            Dim dt As DataTable
            If appno.Value <> "" Then
                Try
                    Dim obj As New AAS.Business.Entity.facilityCredit
                    Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
                    obj.noApp = appno.Value
                    obj.jenisPengajuan = Request.Form(master & "ddlJenisPengajuan")
                    obj.jenisKredit = Request.Form(master & "ddlJenisKredit")
                    obj.jenisValas = Request.Form(master & "ddlJenisValas")
                    obj.nilaiValas = Request.Form(master & "txtNilaiValas")
                    'obj.jangkaWaktuSP2K = ""
                    obj.sistemPembayaran = Request.Form(master & "ddlSistemPembayaran")
                    obj.maxKreditValas = Request.Form(master & "txtMaxKredit").Replace(",", "")
                    obj.maxKreditRec = Request.Form(master & "txtMaksimumKreditRec").Replace(",", "")
                    obj.jangkaWaktuRec = Request.Form(master & "txtJangkaWaktuRec")
                    obj.gracePeriod = Request.Form(master & "txtGracePeriod")
                    'If txtJadwalPembayaranPokokKredit.Text = "" Then
                    '    obj.jadwalPembayaranPokokKredit = "1900-01-01"
                    'Else
                    '    obj.jadwalPembayaranPokokKredit = Mid(txtJadwalPembayaranPokokKredit.Text, 7, 4) + "-" & Mid(txtJadwalPembayaranPokokKredit.Text, 4, 2) + "-" & Mid(txtJadwalPembayaranPokokKredit.Text, 1, 2)
                    'End If
                    'obj.proyeksiBunga = txtProyeksiBunga.Text
                    obj.sukuBunga = Request.Form(master & "txtSukuBunga")
                    obj.jenisSukuBunga = Request.Form(master & "RadBunga")
                    'If rdFlat.Checked = True Then
                    '    obj.jenisSukuBunga = "Flat"
                    'ElseIf rdAnnuitas.Checked = True Then
                    '    obj.jenisSukuBunga = "Annuitas"
                    'ElseIf rdEfektif.Checked = True Then
                    '    obj.jenisSukuBunga = "Efektif"
                    'End If
                    obj.provisi = Request.Form(master & "txtProvisi")
                    'obj.commitmentFee = txtCommitmentFee.Text
                    obj.biayaAdmin = Request.Form(master & "txtBiayaAdmin").Replace(",", "")
                    obj.denda = Request.Form(master & "txtDenda")
                    'obj.bebanBungaPerBulan = txtBebanBungaPerBulan.Text.Replace(",", "")
                    'obj.angsuranPokokPerBulan = txtAngsuranPokokPerBulan.Text.Replace(",", "")
                    'obj.totalAngsuranPokokBunga = txtTotalAngsuranPokokBunga.Text.Replace(",", "")
                    obj.sifatKredit = Request.Form(master & "ddlSifatKredit")
                    obj.maxTarikanKredit = Request.Form(master & "txtMaxTarikanKredit").Replace(",", "")

                    obj.recommendation = Request.Form(master & "ddlRecommend")
                    obj.pendapat = Request.Form(master & "txtPendapat")
                    obj.pertimbangan = Request.Form(master & "txtPertimbangan")

                    ds = oApp.checkCreditRecommendation(appno.Value, proses)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update data
                        oApp.updateFacilityCreditRecommendation(obj, proses, usrLogin)
                    Else
                        'insert data
                        oApp.insertFacilityCreditRecommendation(obj, proses, usrLogin)
                    End If
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran berhasil ditambah.');window.location.href=window.location.href;", True)
                Catch ex As Exception
                    lblNotif.Text = ex.Message
                    divnotif.Attributes.Add("style", "display:''")
                End Try
            End If

            cekInputUpload()
            If lblNotif.Text = "" Then
                Dim mkFolder As String
                mkFolder = "~/Document Upload/" & appno.Value & "/Recommendation/"
                Dim filename As String = ""
                filename = Path.GetFileName(FileUpload1.FileName)

                Try
                    'if directory isn't exist || Server.MapPath("~/Document Upload/")
                    'check folder exists
                    If Not Directory.Exists(Server.MapPath(mkFolder)) Then
                        editpermit()
                        Directory.CreateDirectory(Server.MapPath(mkFolder))
                    End If

                    'check file exists
                    If System.IO.File.Exists(Server.MapPath(mkFolder) & filename) Then
                        liBodyLoadAction.Text = "alert('Tidak bisa upload file. File sudah ada.');window.location.href=window.location.href"
                    Else
                        FileUpload1.SaveAs(Server.MapPath(mkFolder) + filename)


                        If appno.Value <> "" Then
                            Try
                                ds = oApp.checkCreditRecommendationFile(appno.Value, FileUpload1.FileName, proses)
                                dt = ds.Tables(0)
                                If dt.Rows.Count > 0 Then
                                    'update data
                                    oApp.updateCreditRecommendationFile(appno.Value, FileUpload1.FileName, proses)
                                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran dokumen berhasil diubah.');window.location.href=window.location.href;", True)
                                Else
                                    'insert data
                                    oApp.insertCreditRecommendationFile(appno.Value, FileUpload1.FileName, proses)
                                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran dokumen berhasil ditambah.');window.location.href=window.location.href;", True)
                                End If

                            Catch ex As Exception
                                lblNotif.Text = ex.Message
                                divnotif.Attributes.Add("style", "display:''")
                            End Try
                        End If

                    End If



                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message & "');"

                End Try


            End If

        End If
    End Sub

    Sub cekInputUpload()
        Dim lMsg As String = ""

        If FileUpload1.HasFile Then
            'Dim maxLength As Integer = 20971520
            Dim file As HttpPostedFile = DirectCast(FileUpload1.PostedFile, HttpPostedFile)
            Dim filesize As Integer = File.ContentLength
            Dim filename As String = ""

            'folderUrl = mkFolder & "/"

            'Dim path As String = Server.MapPath(folderUrl)
            filename = Path.GetFileName(FileUpload1.FileName)

            If filesize > 20971520 Then
                lMsg = lMsg + "File lebih dari 20 MB.<br/>"
            End If

            If Len(filename) > 5000 Then
                lMsg = lMsg + "Nama file maksimal  5000 karakter.<br/>"
            End If
        Else
            lMsg = lMsg + "File harus diisi.<br/>"
        End If

        If lMsg <> "" Then
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblNotif.Text = ""
        cekInputUploadReport()
        Dim type As String = "IPK"

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

                FileUpload2.SaveAs(Server.MapPath(mkFolder) + filename)

                lblFileUpload.Text = filename

            Catch ex As Exception

                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & ex.Message & ".');", True)

            End Try
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & lblNotif.Text & ".');", True)
        End If
    End Sub

    Sub cekInputUploadReport()
        Dim lMsg As String = ""

        If FileUpload2.HasFile Then
            'Dim maxLength As Integer = 20971520
            Dim file As HttpPostedFile = DirectCast(FileUpload2.PostedFile, HttpPostedFile)
            Dim filesize As Integer = file.ContentLength
            Dim filename As String = ""

            'folderUrl = mkFolder & "/"

            'Dim path As String = Server.MapPath(folderUrl)
            filename = Path.GetFileName(FileUpload2.FileName)

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

    Sub CheckReportIPK()
        Dim path As String
        Dim filename As String
        Dim type As String = "IPK"
        filename = type + "_" + appno.Value + ".pdf"
        path = "~/Document Upload/" & appno.Value & "/" + type + "/" + filename
        If File.Exists(Server.MapPath(path)) Then
            lblFileUpload.Text = filename
        End If

    End Sub
End Class