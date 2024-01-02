Imports System.Drawing
Imports System.IO
Imports System.Security.AccessControl
Imports Microsoft.VisualBasic
Imports System.Net.Mime

Public Class Submit
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oUsr As New lUsers
    Dim oAppF As New lAppsFlow
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Submit Aplikasi"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Protected brwcode As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))

        cekUserAuthentication()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If
        If Session("appno") IsNot Nothing Then
            cekSector(Session("appno"))
            appno.Value = Session("appno")
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
            cekWorkflow(Session("appno"))
            If Not IsPostBack Then
                cekApp(Session("appno"))
            End If
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
            brwcode = dt.Rows(0).Item("app_brw_code")
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

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim dset2 As DataSet
        Dim dtbl2 As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            lblAppNo_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("app_no"))
            lblBrwName_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("lsb_namaPemohon"))

            isiDataAction(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"))
            isiData(no, dtbl.Rows(0).Item("app_brw_code"))
            dset2 = oApp.getRM_inAppFlow(no)
            dtbl2 = dset2.Tables(0)
            Dim dtTable(6) As String

            'dtTable = oUsr.getUserDetail(dtbl.Rows(0).Item("app_usr"))
            dtTable = oUsr.getUserDetail(usrLogin)
            With dtTable
                If .Length > 0 Then
                    lblDefaultSupervisor.Text = dtTable(7)
                    If dtbl.Rows(0).Item("app_position") = 7 Then
                        hdAssign.Value = 0
                        txtAssignTo.Text = ""
                    ElseIf dtbl.Rows(0).Item("app_position") = 5 Then
                        hdAssign.Value = dset2.Tables(0).Rows(0).Item("lwu_usr_before").ToString()
                        txtAssignTo.Text = dset2.Tables(0).Rows(0).Item("lwu_usr_before").ToString()
                    ElseIf dtbl.Rows(0).Item("app_position") = 9 Then

                        If usrLogin = dtbl.Rows(0).Item("app_usr").ToString() Then
                            For i = 0 To dset2.Tables(0).Rows.Count() - 1
                                If (dtbl2.Rows(i).Item("lwu_workflow_id_next").ToString() = dtbl.Rows(0).Item("app_position").ToString()) Then
                                    hdAssign.Value = dset2.Tables(0).Rows(i).Item("lwu_usr_before").ToString()
                                    txtAssignTo.Text = dset2.Tables(0).Rows(i).Item("lwu_usr_before").ToString()
                                End If

                            Next
                        End If
                       
                    Else
                        hdAssign.Value = dtTable(6)
                        txtAssignTo.Text = dtTable(6)
                    End If

                End If
            End With

            isiDataAssign(no, dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_usr"))
            usrid.Value = dtbl.Rows(0).Item("app_usr")
        End If
    End Sub

    Sub cekWorkflow(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim ds As DataSet
        Dim dt As DataTable
        Dim ds2 As DataSet
        Dim dt2 As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            Dim cond As Boolean = False
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                execWorkflow(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_convent"))
                If dtbl.Rows(0).Item("app_position") = 9 Then
                    Dim checkApproval As DataSet = oApp.CheckApproval(no)

                    ds2 = oApp.getLastWorkflowUsr(no)
                    dt2 = ds2.Tables(0)

                    If usrLogin + "-" + dtbl.Rows(0).Item("app_position").ToString() = dtbl.Rows(0).Item("app_usr") And dt2.Rows(dt2.Rows.Count - 1).Item("lwu_workflow_proses_id") = 5 Then
                        ds = oApp.getStatusProc_App(no)
                        dt = ds.Tables(0)
                        If dtbl.Rows(0).Item("app_position") = 9 And dt.Rows.Count > 0 Then
                            controlRO()
                        End If

                    ElseIf usrLogin + "-" + dtbl.Rows(0).Item("app_position").ToString() <> dtbl.Rows(0).Item("app_usr") Then
                        controlRO()


                        'If checkApproval.Tables(0).Rows(0).Item("isreject") = 0 Then
                        '    controlRO()
                        'End If

                        'If 
                        
                    ElseIf usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                        controlRO()
                    End If
                ElseIf usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If

            End If
        End If
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

    Sub isiDataAction(ByVal workflow, ByVal posisi)
        'Dim workflowName As String = ""
        Dim dt2 As DataTable

        dt2 = oApp.getActionOfWorkflowProcess(workflow, posisi)
        ddlSubmit.Items.Clear()
        Dim cond As Boolean = False
        For i As Integer = 0 To dt2.Rows.Count - 1
            If Not IsDBNull(dt2.Rows(i).Item("lwd_var")) And oGen.CheckNullString(dt2.Rows(i).Item("lwd_var")) <> "" Then
                Dim dsCh As DataSet
                Dim dtCh As DataTable
                'dsCh = oApp.checkActionOfWorkflowProcessByVariable(appno.Value, dt2.Rows(i).Item("lwd_var"), dt2.Rows(i).Item("lwd_cond"), dt2.Rows(i).Item("lwd_value"))
                dsCh = oApp.checkActionOfWorkflowProcessBykre_rec_info(appno.Value, dt2.Rows(i).Item("lwd_var"), dt2.Rows(i).Item("lwd_cond"), dt2.Rows(i).Item("lwd_value"))
                dtCh = dsCh.Tables(0)
                If dtCh.Rows.Count > 0 Then
                    cond = True
                End If
            Else
                cond = True
            End If
            If cond = True Then
                ddlSubmit.Items.Add(New ListItem(dt2.Rows(i).Item("lwd_action"), dt2.Rows(i).Item("lwd_next")))
                cond = False
            End If
        Next
    End Sub

    Sub controlEdit()

    End Sub

    Sub controlRO()
        ddlSubmit.Visible = False
        btnSave.Visible = False
    End Sub
    Sub controlROSpecial()
        ddlSubmit.Visible = True
        btnSave.Visible = True
    End Sub

    Sub isiDataAssign(ByVal no, ByVal workflow, ByVal posisi, ByVal usr)
        If ddlSubmit.Items.Count > 0 Then
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oApp.getAppUserByWorkflow(no, workflow, posisi, ddlSubmit.SelectedValue)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                hdAssign.Value = dt.Rows(0).Item("lwu_usr")
                txtAssignTo.Text = dt.Rows(0).Item("usr_name")
                lblPrevAction.Text = "Previous Action"
                lblPrevAction.Visible = True
                lblPrevActionValue.Text = dt.Rows(0).Item("lwd_action")
                lblPrevActionValue.Visible = True
                lblPrevSupervisor.Text = "Previous Assign To"
                lblPrevSupervisor.Visible = True
                lblPrevSupervisorValue.Text = dt.Rows(0).Item("usr_name")
                lblPrevSupervisorValue.Visible = True
            Else
                Dim ds2 As DataSet
                Dim dt2 As DataTable
                ds2 = oApp.getAppUserBeforeByWorkflow(no, workflow, posisi, ddlSubmit.SelectedValue, usr)
                dt2 = ds2.Tables(0)
                If dt2.Rows.Count > 0 Then
                    hdAssign.Value = dt2.Rows(0).Item("lwu_usr_before")

                    Dim dtTable(6) As String

                    dtTable = oUsr.getUserDetail(dt2.Rows(0).Item("lwu_usr_before"))
                    With dtTable
                        If .Length > 0 Then
                            txtAssignTo.Text = dtTable(1)
                        End If
                    End With
                
                End If
            End If
        End If
    End Sub

    Sub isiData(ByVal app_no, ByVal brwcode)
        lblNotif.Text = ""
        
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.reviewApp(app_no, brwcode)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("app_position") = 0 And dt.Rows(0).Item("app_is_open") = False Then
                ddlSubmit.Visible = False
                btnSave.Visible = False
            End If
            lblAppNo.Text = oGen.CheckNullString(dt.Rows(0).Item("app_no"))
            lblAppDate.Text = oGen.CheckNullString(dt.Rows(0).Item("lbl_date"))
            lblRM.Text = oGen.CheckNullString(dt.Rows(0).Item("lsg_RM"))
            Dim dset As DataSet
            Dim dtbl As DataTable
            dset = oUsr.getUserBranchParent(oGen.CheckNullString(dt.Rows(0).Item("lsg_spoke")))
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                lblKanwil.Text = dtbl.Rows(0).Item("NAME")
            End If
            lblSpokeCapem.Text = oGen.CheckNullString(dt.Rows(0).Item("lblCapem"))
            lblKantorCabang.Text = oGen.CheckNullString(dt.Rows(0).Item("lblKantorCabang"))
            'lblSegment.Text = oGen.CheckNullString(dt.Rows(0).Item("lblSegment"))
            lblBrwCode.Text = brwcode
            lblCIF.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_cif"))
            lblNamaDebitur.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaPemohon"))
            lblNoTelp.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_kodeAreaTlpRmh")) & "-" & oGen.CheckNullString(dt.Rows(0).Item("lsb_tlpRmh"))
            lblAlamat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_alamatRmh"))
            lblSektorEkonomi.Text = oGen.CheckNullString(dt.Rows(0).Item("lblSektor"))
            lblSubSektorEkonomi.Text = oGen.CheckNullString(dt.Rows(0).Item("lblSubSektor"))
            lblBidangUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lblBidangUsaha"))
            lblGrupUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_grupUsaha"))
            'lblGrupUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lblGrupUsaha"))
            'lblOmzet.Text = oGen.moneyFormatLabel(dt.Rows(0).Item("lsg_omzet"))

            '===================request==============================
            Dim ds2 As DataSet
            Dim dt2 As DataTable
            ds2 = oApp.getDetailOfRequestCredit(app_no)
            dt2 = ds2.Tables(0)
            If dt2.Rows.Count > 0 Then
                lblJenisPengajuanReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lblJenisPengajuan"))
                lblJenisKreditReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lblJenisKredit"))
                lblJenisValasReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lblValas"))
                lblNilaiValasReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lfc_kurs"))
                lblSistemPembayaranReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lblPayment"))
                lblMaxKreditReq.Text = oGen.moneyFormatLabel(dt2.Rows(0).Item("lfc_nilaiValas"))
                lblNilaiRupiahReq.Text = oGen.moneyFormatLabel(dt2.Rows(0).Item("lfc_nilaiRupiah"))
                lblJangkaWaktuReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lfc_jangkaWaktu"))
                lblGracePeriodReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lfc_grace"))
                lblSukuBungaReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lfc_bungaPersen"))
                lblTipeBungaReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lfc_jenisBunga"))
                lblSifatKreditReq.Text = oGen.CheckNullString(dt2.Rows(0).Item("lblSifat"))
                'lblTotalAngsuranReq.Text = oGen.moneyFormatLabel(dt.Rows(0).Item("lfc_total"))
            Else
                lblNotif.Text &= "Data permohonan kredit tidak ada. Silahkan isi permohonan kredit terlebih dahulu. Klik <a href='FacilityCredit.aspx?appno=" & app_no & "' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman permohonan kredit.<br/>"
                divnotif.Attributes.Add("style", "display:''")
                lblJenisPengajuanReq.Text = "N/A"
                lblJenisKreditReq.Text = "N/A"
                lblJenisValasReq.Text = "N/A"
                lblNilaiValasReq.Text = "N/A"
                lblSistemPembayaranReq.Text = "N/A"
                lblMaxKreditReq.Text = "N/A"
                lblNilaiRupiahReq.Text = "N/A"
                lblJangkaWaktuReq.Text = "N/A"
                lblGracePeriodReq.Text = "N/A"
                lblSukuBungaReq.Text = "N/A"
                lblTipeBungaReq.Text = "N/A"
                lblSifatKreditReq.Text = "N/A"
                'lblTotalAngsuranReq.Text = "N/A"
                tblRequest.Visible = False
                'controlRO()
            End If
            '===================end request==============================

            '===================notes==============================
            ds2 = oApp.checkNotes(app_no)
            dt2 = ds2.Tables(0)
            If dt2.Rows.Count > 0 Then
                grid1.DataSource = dt2
                grid1.DataBind()
            End If
            ''===================end notes==============================

        Else
            lblNotif.Text &= "Data aplikasi tidak ada. Klik <a href='Underwriting.aspx' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman underwriting.<br/>"
            divnotif.Attributes.Add("style", "display:''")
            tblRequest.Visible = False
            controlRO()
        End If

    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnSave.Click
        'cek data financial year - 1
        'cek data facility
        'cek data borrower
        'cek data pengajuan kredit
        lblNotif.Text = ""
        'cekInput()

        Dim check As String
        check = checkSaham()
        Try
            If check = "" Then
                'If appno.Value <> "" Then
                'End If
                If hdAssign.Value <> "0" Then
                    'update app info
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkApp(appno.Value)
                    dt = ds.Tables(0)
                    Dim ds2 As DataSet
                    Dim dt2 As DataTable
                    ds2 = oAppF.getListOfAppFlow(appno.Value)
                    dt2 = ds2.Tables(0)
                    If dt.Rows.Count Then
                        If ddlSubmit.SelectedValue = 10 Then
                            Dim ca As String = ""

                            ca = dt2.Rows(dt2.Rows.Count() - 1).Item("lwu_usr_before").ToString()
                            'oApp.ResetApprovalHead(appno.Value)
                            For i As Integer = 0 To dt2.Rows.Count() - 1
                                oApp.submitAllHead(appno.Value, dt2.Rows(i).Item("app_brw_code"), lblBrwName_header.Text, ca, dt2.Rows(i).Item("lwu_usr"), usrLogin, dt2.Rows(i).Item("lwu_workflow_id_next"))
                            Next
                            'oApp.submitApp(appno.Value, ddlSubmit.SelectedValue, usrLogin, hdAssign.Value, txtNotes.Text)

                        ElseIf ddlSubmit.SelectedValue = 11 Then
                            Dim ca As String = ""

                            ca = dt2.Rows(dt2.Rows.Count() - 1).Item("lwu_usr_before").ToString()
                            'oApp.ResetApprovalHead(appno.Value)
                            For i As Integer = 0 To dt2.Rows.Count() - 1
                                oApp.submitAllDivHead(appno.Value, dt2.Rows(i).Item("app_brw_code"), lblBrwName_header.Text, ca, dt2.Rows(i).Item("lwu_usr"), usrLogin, dt2.Rows(i).Item("lwu_workflow_id_next"))
                            Next
                            'oApp.submitApp(appno.Value, ddlSubmit.SelectedValue, usrLogin, hdAssign.Value, txtNotes.Text)
                        Else

                            oApp.submitApp(appno.Value, ddlSubmit.SelectedValue, usrLogin, hdAssign.Value, txtNotes.Text)
                        End If
                        'oApp.submitApp(appno.Value, ddlSubmit.SelectedValue, usrLogin, hdAssign.Value, txtNotes.Text)

                    End If
                    liBodyLoadAction.Text = "alert('Aplikasi berhasil disubmit.');window.location.href='Underwriting.aspx'"
                Else
                    If ddlSubmit.SelectedValue <> 0 Or ddlSubmit.SelectedValue <> 9999 Then
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Assign To harus diisi.\n');", True)
                    End If
                End If
            Else
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + check + "');", True)
            End If
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        End Try
    End Sub

    Sub cekInput()
        Dim lNot As String = ""
        lNot &= oGen.checkInputInaHTML("TextBox", "Assign To", hdAssign.Value)
        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub ddlSubmit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubmit.SelectedIndexChanged
        hdAssign.Value = "0"
        txtAssignTo.Text = ""

        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            Dim dtTable(6) As String

            dtTable = oUsr.getUserDetail(dtbl.Rows(0).Item("app_usr"))
            With dtTable
                If .Length > 0 Then

                    lblDefaultSupervisor.Text = dtTable(7)
                    hdAssign.Value = dtTable(6)
                    txtAssignTo.Text = dtTable(7)
                End If
            End With

            Dim ds As DataSet
            Dim dt As DataTable
            If ddlSubmit.SelectedValue = 0 Or ddlSubmit.SelectedValue = 9999 Then
                txtAssignTo.Visible = False
                'btnBrowse.Attributes.Add("style", "display:none")
                btnBrowse.Visible = False
                hdAssign.Value = "0"
                txtAssignTo.Text = ""
            ElseIf ddlSubmit.SelectedValue = 10 Then
                'txtAssignTo.Visible = False
                'btnBrowse.Attributes.Add("style", "display:none")
                btnBrowse.Visible = False
                hdAssign.Value = "Process Approval All Head"
                txtAssignTo.Text = "Process Approval All Head"
            ElseIf ddlSubmit.SelectedValue = 11 Then
                'txtAssignTo.Visible = False
                'btnBrowse.Attributes.Add("style", "display:none")
                btnBrowse.Visible = False
                hdAssign.Value = "Process Approval All Div Head"
                txtAssignTo.Text = "Process Approval All Div Head"
            Else
                txtAssignTo.Visible = True
                btnBrowse.Visible = True
                ds = oApp.getAppUserByWorkflow(appno.Value, dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), ddlSubmit.SelectedValue)
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    hdAssign.Value = dt.Rows(0).Item("lwu_usr")
                    txtAssignTo.Text = dt.Rows(0).Item("usr_name")
                    lblPrevAction.Text = "Previous Action"
                    lblPrevAction.Visible = True
                    lblPrevActionValue.Text = dt.Rows(0).Item("lwd_action")
                    lblPrevActionValue.Visible = True
                    lblPrevSupervisor.Text = "Previous Assign To"
                    lblPrevSupervisor.Visible = True
                    lblPrevSupervisorValue.Text = dt.Rows(0).Item("usr_name")
                    lblPrevSupervisorValue.Visible = True
                Else
                    Dim ds2 As DataSet
                    Dim dt2 As DataTable
                    ds2 = oApp.getAppUserBeforeByWorkflow(appno.Value, dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), ddlSubmit.SelectedValue, dtbl.Rows(0).Item("app_usr"))
                    dt2 = ds2.Tables(0)
                    If dt2.Rows.Count > 0 Then
                        hdAssign.Value = dt2.Rows(0).Item("lwu_usr_before")

                        dtTable = oUsr.getUserDetail(dt2.Rows(0).Item("lwu_usr_before"))
                        With dtTable
                            If .Length > 0 Then
                                txtAssignTo.Text = dtTable(1)
                            End If
                        End With
                        'Else
                        '    hdAssign.Value = 0
                        '    txtAssignTo.Text = ""
                    End If
                End If
            End If

        End If

    End Sub

    Function checkSaham()
        Dim text As String = ""
        Dim dtbl As DataTable
        dtbl = oApp.checkTotalSaham(brwcode)
        If dtbl.Rows.Count > 0 Then
            If Convert.ToDecimal(dtbl.Rows(0).Item("totalSaham").ToString()) < 100 Then
                text = "Total Saham kurang dari 100%, total saham harus 100%."
            End If
        End If

        Return text

    End Function


End Class