Imports System.Drawing
Imports System.IO
Imports System.Security.AccessControl
Imports Microsoft.VisualBasic
Imports System.Net.Mime

Public Class FacilityCreditFin
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Protected allowDelete As Boolean = True
    Protected allowUpload As Boolean = True
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Persetujuan Permohonan Kredit"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Protected history As Boolean = False
    Dim proses As Integer = 1

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

    Sub cekHistory(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkFinalHistory(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            history = True
        End If

        ds = oApp.checkFinalHistoryFile(no, proses)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                grid1.DataSource = ds
                grid1.DataBind()
            Next
            grid1.Visible = True
            filAttch.Visible = True
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

    Sub fillDropdown()
        Dim dtFasilitas As DataTable
        dtFasilitas = oRat.getDropDownList("RFPRODUCT")
        ddlJenisKredit.DataSource = dtFasilitas
        ddlJenisKredit.DataTextField = "ldd_desc"
        ddlJenisKredit.DataValueField = "ldd_code"
        ddlJenisKredit.DataBind()

        Dim dtKeputusan As DataTable
        dtKeputusan = oRat.getDropDownList("RFDECISION")
        ddlKeputusan.DataSource = dtKeputusan
        ddlKeputusan.DataTextField = "ldd_desc"
        ddlKeputusan.DataValueField = "ldd_code"
        ddlKeputusan.DataBind()

        'ddlKeputusan.Items.Clear()
        'ddlKeputusan.Items.Add(New ListItem("Pilih", "Pilih"))
        'ddlKeputusan.Items.Add(New ListItem("Direkomendasikan untuk ditolak", "False"))
        'ddlKeputusan.Items.Add(New ListItem("Direkomendasikan untuk diterima", "True"))

        Dim dtJenisPengajuan As DataTable
        dtJenisPengajuan = oRat.getDropDownList("RFAPPLICATIONTYPE")
        ddlJenisPengajuan.DataSource = dtJenisPengajuan
        ddlJenisPengajuan.DataTextField = "ldd_desc"
        ddlJenisPengajuan.DataValueField = "ldd_code"
        ddlJenisPengajuan.DataBind()

        Dim dtSistemPembayaran As DataTable
        dtSistemPembayaran = oRat.getDropDownList("RFPAYMENTSYS")
        ddlSistemPembayaranFin.DataSource = dtSistemPembayaran
        ddlSistemPembayaranFin.DataTextField = "ldd_desc"
        ddlSistemPembayaranFin.DataValueField = "ldd_code"
        ddlSistemPembayaranFin.DataBind()

        Dim dtJenisCurr As DataTable
        dtJenisCurr = oRat.getDropDownList("RFCURRENCY")
        ddlJenisValasFin.DataSource = dtJenisCurr
        ddlJenisValasFin.DataTextField = "ldd_desc"
        ddlJenisValasFin.DataValueField = "ldd_code"
        ddlJenisValasFin.DataBind()

        Dim dtSifat As DataTable
        dtSifat = oRat.getDropDownList("RFSIFATKREDIT")
        ddlSifatKreditFin.DataSource = dtSifat
        ddlSifatKreditFin.DataTextField = "ldd_desc"
        ddlSifatKreditFin.DataValueField = "ldd_code"
        ddlSifatKreditFin.DataBind()
    End Sub

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
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
                proses = dtbl.Rows(0).Item("app_position")
                execWorkflow(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_convent"))
                If usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If
            End If
        End If
    End Sub

    Sub controlHome()
        grid1.Visible = False
        filAttch.Visible = False
        btnNext.Visible = False
        txtNilaiValasFin.ReadOnly = True
        txtNilaiValasFin.CssClass = "inpTxtDisabled"
        txtMaxKreditDisetujuiFinValas.ReadOnly = True
        txtMaxKreditDisetujuiFinValas.CssClass = "inpTxtDisabled"
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
        ddlJenisPengajuan.Enabled = False
        ddlJenisPengajuan.CssClass = "inpDdlDisabled"
        ddlJenisKredit.Enabled = False
        ddlJenisKredit.CssClass = "inpDdlDisabled"
        ddlJenisValasFin.Enabled = False
        ddlJenisValasFin.CssClass = "inpDdlDisabled"
        txtNilaiValasFin.Enabled = False

        ddlSistemPembayaranFin.Enabled = False

        txtMaxKreditDisetujuiFin.Enabled = False
        txtJangkaWaktuFin.Enabled = False
        txtGracePeriodFin.Enabled = False
        txtSukuBungaFin.Enabled = False
        RadBunga.Enabled = False
        
        'txtBebanBungaPerBulanFin.Enabled = False
        'txtAngsuranPokokPerBulanFin.Enabled = False
        'txtTotalAngsuranPokokBungaFin.Enabled = False

        ddlSifatKreditFin.Enabled = False
        ddlSifatKreditFin.CssClass = "inpDdlDisabled"
        txtMaxTarikanKreditFin.Enabled = False
        txtAlasanFin.Enabled = False
        allowDelete = False
        allowUpload = False
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Function bindHistory(ByVal no)
        Dim html As String = ""
        Dim last As Boolean = False
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkFinalHistory(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("lcf_process") = proses And i = proses Then
                    last = True
                Else
                    last = False
                End If
            Next
            If last = False Then
                html &= "<fieldset style='margin:20px 0;border-radius:5px;background:#ebecec;'>"
                html &= "<legend><label style='font-weight:bold;'>History</label></legend>"
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("lcf_process") <> proses Then
                        html &= "<div stlye='margin-top:10px;'>"
                        html &= "<table class='tbl' cellspacing='0' cellpadding='3' width='100%' style='text-align:left;border: thin solid #bcbcbc;border-radius: 5px;'>"
                        html &= "<tr>"
                        html &= "<td colspan='4' background='../../Images/bg-head.gif' bgcolor='#ddedf6' style='border-radius: 5px 5px 0 0;font-weight:bold;color:#fff;'>"
                        html &= "<label> PERMOHONAN KREDIT YANG DISETUJUI - User : " & oGen.CheckNullString(dt.Rows(i).Item("usr_name")) & " - User Role : " & oGen.CheckNullString(dt.Rows(i).Item("lu_level_name")) & "</label>"
                        html &= "</td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Jenis Pengajuan</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblJenisPengajuan")) & "</label></td>"
                        html &= "<td width='20%'><b>Suku Bunga</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_sukuBunga")) & " % " & oGen.CheckNullString(dt.Rows(i).Item("lcf_jenisSukuBunga")) & "</label></td>"
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
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcf_maxTarikanKredit")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Jenis Currency</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lblValas")) & "</label></td>"
                        html &= "<td width='20%'><b>Provisi</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_provisi")) & " %</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Nilai Kurs</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_nilaiValas")) & "</label></td>"
                        html &= "<td width='20%'><b>Biaya Administrasi</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcf_bebanAdministrasi")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Maksimum Kredit (Dalam Valas)</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcf_maxKreditValas")) & "</label></td>"
                        html &= "<td width='20%'><b>Denda Tunggakan</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_denda")) & " %</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Maksimum Kredit (Dalam Rupiah)</b></td>"
                        html &= "<td width='30%'><label>" & oGen.moneyFormatLabel(dt.Rows(i).Item("lcf_maxKredit")) & "</label></td>"
                        html &= "<td width='20%'><b>Keputusan</b></td>"
                        html &= "<td width='30%'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lbldecision")) & "</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Jangka Waktu</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_jangkaWaktu")) & " Bulan</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#f6f7f7'>"
                        html &= "<td width='20%'><b>Grace Period</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_gracePeriod")) & " Bulan</label></td>"
                        html &= "</tr>"
                        html &= "<tr bgcolor='#ebecec'>"
                        html &= "<td width='20%'><b>Alasan</b></td>"
                        html &= "<td width='30%' colspan='3'><label>" & oGen.CheckNullString(dt.Rows(i).Item("lcf_alasan")) & "</label></td>"
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

    Sub isiData(ByVal app_no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkCreditFinal(app_no, proses)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlJenisPengajuan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_jenisPengajuan"))
            ddlJenisKredit.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_JenisKredit"))
            ddlJenisValasFin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_jenisValas"))
            txtNilaiValasFin.Text = oGen.CheckNullString(dt.Rows(0).Item("lcf_nilaiValas"))

            ddlSistemPembayaranFin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_sistemPembayaran"))
            txtMaxKreditDisetujuiFinValas.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcf_maxKreditValas"))
            txtMaxKreditDisetujuiFin.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcf_maxKredit"))
            txtJangkaWaktuFin.Text = oGen.CheckNullString(dt.Rows(0).Item("lcf_jangkaWaktu"))
            txtGracePeriodFin.Text = oGen.CheckNullString(dt.Rows(0).Item("lcf_gracePeriod"))
            txtSukuBungaFin.Text = oGen.CheckNullString(dt.Rows(0).Item("lcf_sukuBunga"))

            'txtBebanBungaPerBulanFin.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcf_bebanBungaPerBulan"))
            'txtAngsuranPokokPerBulanFin.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcf_angsuranPokokPerBulan"))
            'txtTotalAngsuranPokokBungaFin.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcf_totalAngsuran"))
            ddlSifatKreditFin.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_sifatKredit"))
            If ddlSifatKreditFin.SelectedItem.Text = "NON REVOLVING" Then
                txtMaxTarikanKreditFin.ReadOnly = True
                txtMaxTarikanKreditFin.CssClass = "inpTxtDisabled"
            End If
            txtMaxTarikanKreditFin.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcf_maxTarikanKredit"))
            txtAlasanFin.Text = oGen.CheckNullString(dt.Rows(0).Item("lcf_alasan"))
            ddlKeputusan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_keputusan"))
            RadBunga.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcf_jenisSukuBunga"))
            
        End If

        '===========attachment============
        ds = oApp.checkCreditFinalFile(appno.Value, "All", proses)
        grid2.DataSource = ds
        grid2.DataBind()
        '===========end attachment============

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
            ddlKeputusan.SelectedValue = "True"
        ElseIf recomApp = 0 Or recomBrw = 0 Then
            ddlKeputusan.SelectedValue = "False"
        End If
        '===========end rating================

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

        cekInput()
        If lblNotif.Text = "" Then

            If appno.Value <> "" Then
                Try
                    Dim obj As New AAS.Business.Entity.facilityCredit
                    Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
                    obj.noApp = appno.Value
                    obj.jenisPengajuan = Request.Form(master & "ddlJenisPengajuan")
                    obj.jenisKredit = Request.Form(master & "ddlJenisKredit")
                    obj.jenisValas = Request.Form(master & "ddlJenisValasFin")
                    obj.nilaiValas = Request.Form(master & "txtNilaiValasFin")
                    obj.sistemPembayaran = Request.Form(master & "ddlSistemPembayaranFin")
                    obj.maxKreditValas = Request.Form(master & "txtMaxKreditDisetujuiFinValas").Replace(",", "")
                    obj.maxKreditFin = Request.Form(master & "txtMaxKreditDisetujuiFin").Replace(",", "")
                    obj.jangkaWaktuFin = Request.Form(master & "txtJangkaWaktuFin")
                    obj.gracePeriod = Request.Form(master & "txtGracePeriodFin")
                    obj.sukuBunga = Request.Form(master & "txtSukuBungaFin")
                    obj.jenisSukuBunga = Request.Form(master & "RadBunga")
                    obj.provisi = Request.Form(master & "txtProvisi")
                    obj.denda = Request.Form(master & "txtDenda")
                    obj.biayaAdmin = Request.Form(master & "txtBiayaAdmin").Replace(",", "")
                    obj.sifatKredit = Request.Form(master & "ddlSifatKreditFin")
                    obj.maxTarikanKredit = Request.Form(master & "txtMaxTarikanKreditFin").Replace(",", "")
                    obj.alasan = Request.Form(master & "txtAlasanFin")
                    obj.keputusan = Request.Form(master & "ddlKeputusan")

                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkCreditFinal(appno.Value, proses)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update
                        oApp.updateFacilityCreditFinal(obj, proses, usrLogin)
                    Else
                        'insert
                        oApp.insertFacilityCreditFinal(obj, proses, usrLogin)
                    End If
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil disimpan.');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                Catch ex As Exception
                    lblNotif.Text = ex.Message
                    divnotif.Attributes.Add("style", "display:''")
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lNot As String = ""
        lNot &= oGen.checkInputInaHTML("TextBox", "Maksimum Kredit (Dalam Rupiah)", txtMaxKreditDisetujuiFin.Text)
        'lNot &= oGen.checkInputInaHTML("TextBox", "Suku Bunga", txtSukuBungaFin.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Jenis Valas", ddlJenisValasFin.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Sifat Kredit", ddlSifatKreditFin.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Keputusan", ddlKeputusan.SelectedValue)
        If ddlSifatKreditFin.SelectedItem.Text = "REVOLVING" Then
            lNot &= oGen.checkInputInaHTML("DropDownList", "Maksinmum Tarikan Kredit", txtMaxTarikanKreditFin.Text)
        End If
        If txtJangkaWaktuFin.Text = 0 Then
            lNot &= "Jangka Waktu tidak boleh 0.<br/>"
        End If
        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub ddlJenisValasFin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisValasFin.SelectedIndexChanged
        Try
            Dim dt As DataTable
            dt = oRat.getDropDownList("RFCURRATE")
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("ldd_code") = ddlJenisValasFin.SelectedValue Then
                        txtNilaiValasFin.Text = dt.Rows(i).Item("ldd_desc")
                    End If
                Next
            End If
            If txtNilaiValasFin.Text = "Pilih" Then
                txtNilaiValasFin.Text = 0
            End If
            If txtNilaiValasFin.Text = 0 Then
                txtMaxKreditDisetujuiFinValas.Text = 0
            Else
                txtMaxKreditDisetujuiFinValas.Text = oGen.moneyFormatTextBox(CInt(txtNilaiValasFin.Text) * txtMaxKreditDisetujuiFin.Text)
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
        mkFolder = "~/Document Upload/" & appno.Value & "/Final/"
        If File.Exists(Server.MapPath(mkFolder) & docname) Then
            System.IO.File.Delete(Server.MapPath(mkFolder) & docname)
        End If
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oApp.checkCreditFinalFile(appno.Value, docname, proses)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                'delete data
                oApp.deleteCreditFinalFile(appno.Value, docname)
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
        mkFolder = "~/Document Upload/" & appno.Value & "/Final/"
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
        cekInput()
        If lblNotif.Text = "" Then
            If appno.Value <> "" Then
                Try
                    Dim obj As New AAS.Business.Entity.facilityCredit
                    Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
                    obj.noApp = appno.Value
                    obj.jenisPengajuan = Request.Form(master & "ddlJenisPengajuan")
                    obj.jenisKredit = Request.Form(master & "ddlJenisKredit")
                    obj.jenisValas = Request.Form(master & "ddlJenisValasFin")
                    obj.nilaiValas = Request.Form(master & "txtNilaiValasFin")
                    obj.sistemPembayaran = Request.Form(master & "ddlSistemPembayaranFin")
                    obj.maxKreditValas = Request.Form(master & "txtMaxKreditDisetujuiFinValas").Replace(",", "")
                    obj.maxKreditFin = Request.Form(master & "txtMaxKreditDisetujuiFin").Replace(",", "")
                    obj.jangkaWaktuFin = Request.Form(master & "txtJangkaWaktuFin")
                    obj.gracePeriod = Request.Form(master & "txtGracePeriodFin")
                    obj.sukuBunga = Request.Form(master & "txtSukuBungaFin")
                    obj.jenisSukuBunga = Request.Form(master & "RadBunga")
                    obj.provisi = Request.Form(master & "txtProvisi")
                    obj.denda = Request.Form(master & "txtDenda")
                    obj.biayaAdmin = Request.Form(master & "txtBiayaAdmin").Replace(",", "")
                    obj.sifatKredit = Request.Form(master & "ddlSifatKreditFin")
                    obj.maxTarikanKredit = Request.Form(master & "txtMaxTarikanKreditFin").Replace(",", "")
                    obj.alasan = Request.Form(master & "txtAlasanFin")
                    obj.keputusan = Request.Form(master & "ddlKeputusan")

                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkCreditFinal(appno.Value, proses)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update
                        oApp.updateFacilityCreditFinal(obj, proses, usrLogin)
                    Else
                        'insert
                        oApp.insertFacilityCreditFinal(obj, proses, usrLogin)
                    End If
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                Catch ex As Exception
                    lblNotif.Text = ex.Message
                    divnotif.Attributes.Add("style", "display:''")
                End Try
            End If

            cekInputUpload()
            If lblNotif.Text = "" Then
                Dim mkFolder As String
                mkFolder = "~/Document Upload/" & appno.Value & "/Final/"
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

                        Dim ds As DataSet
                        Dim dt As DataTable
                        If appno.Value <> "" Then
                            Try
                                ds = oApp.checkCreditFinalFile(appno.Value, FileUpload1.FileName, proses)
                                dt = ds.Tables(0)
                                If dt.Rows.Count > 0 Then
                                    'update data
                                    oApp.updateCreditFinalFile(appno.Value, FileUpload1.FileName)
                                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran dokumen berhasil diubah.');window.location.href=window.location.href;", True)
                                Else
                                    'insert data
                                    oApp.insertCreditFinalFile(appno.Value, FileUpload1.FileName)
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
End Class