Public Class General
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim oUsr As New lUsers
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Informasi Umum"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""

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
            fillDdl()
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
        Dim dtTable(9) As String

        dtTable = oUsr.getUserDetail(usrLogin)

        With dtTable
            If .Length > 0 Then

                hdCapem.Value = dtTable(9)
                txtSpokeCapem.Text = dtTable(10)
                'If dtTable(9) <> "HEAD" Then
                Dim ds As DataSet
                Dim dt As DataTable
                ds = oUsr.getUserBranchParent(dtTable(9))
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    hdKanwil.Value = dt.Rows(0).Item("CODE")
                    txtKantorPusatKanwil.Text = dt.Rows(0).Item("NAME")
                Else
                    hdKanwil.Value = ""
                    txtKantorPusatKanwil.Text = ""
                End If
                'Else
                '    hdKanwil.Value = dtTable(9)
                '    txtKantorPusatKanwil.Text = dtTable(10)
                'End If

            End If
        End With

        btnNext.Visible = False
    End Sub

    Sub fillDdl()
        Dim dt As DataTable

        'dt = oRat.getDropDownList("RFCHANNEL")
        'ddlChannels.Items.Clear()
        'If dt.Rows.Count > 0 Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        ddlChannels.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
        '    Next
        'End If

        dt = oRat.getDropDownList("VW_REF_RFBRANCH")
        ddlKantorCabang.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlKantorCabang.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

        dt = oRat.getDropDownList("RFUNIT")
        ddlUnitPengelola.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlUnitPengelola.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

        dt = oRat.getDropDownList("RFBUSINESSUNIT")
        ddlSegment.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlSegment.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If

        dt = oRat.getDropDownList("RFAPPLICATIONTYPE")
        ddlJenisPengajuan.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlJenisPengajuan.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If
    End Sub

    Sub cekApp(ByVal appno)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            brwcode.Value = dtbl.Rows(0).Item("app_brw_code")
            txtNoAplikasi.Text = appno
            txtTglAplikasi.Text = dtbl.Rows(0).Item("app_date")
            txtRMAnalist.Text = dtbl.Rows(0).Item("app_usr")
            isiData(appno)
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

    Sub isiData(ByVal appno)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oApp.checkGeneral(appno)
        dt = ds.Tables(0)
        'txtTglAplikasi.Text = Now.ToString("dd-MM-yyyy")
        If dt.Rows.Count > 0 Then
            Dim dset As DataSet
            Dim dtbl As DataTable
            dset = oUsr.getUserBranchParent(oGen.CheckNullString(dt.Rows(0).Item("lsg_spoke")))
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                hdKanwil.Value = dtbl.Rows(0).Item("CODE")
                txtKantorPusatKanwil.Text = dtbl.Rows(0).Item("NAME")
            End If
            'txtKantorPusatKanwil.Text = oGen.CheckNullString(dt.Rows(0).Item("lblKantorPusat"))
            'hdKanwil.Value = oGen.CheckNullString(dt.Rows(0).Item("lsg_kantorPusat"))
            txtSpokeCapem.Text = oGen.CheckNullString(dt.Rows(0).Item("lblCapem"))
            hdCapem.Value = oGen.CheckNullString(dt.Rows(0).Item("lsg_spoke"))
            txtRMAnalist.Text = oGen.CheckNullString(dt.Rows(0).Item("lsg_RM"))
            txtNoAplikasi.Text = oGen.CheckNullString(dt.Rows(0).Item("lsg_noApp"))
            txtTglAplikasi.Text = oGen.CheckNullString(dt.Rows(0).Item("lsg_tglApp"))
            ddlSegment.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsg_segment"))
            ddlUnitPengelola.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsg_unitPengelola"))
            'ddlJenisAnalisaKeuangan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsg_jenisKeuangan"))
            ddlKantorCabang.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsg_kantorCabang"))
        End If

        'Dim ds1 As DataSet
        'Dim dt1 As DataTable
        'ds1 = oApp.checkPreScreening(brwcode.Value)
        'dt1 = ds1.Tables(0)

        ds = oRat.checkFacilityCredit(appno)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            If dt.Rows.Count > 0 Then
                ddlJenisPengajuan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisPengajuan"))
                txtLimit.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiRupiah"))
            End If
        Else
            ddlJenisPengajuan.SelectedValue = "B1"
            txtLimit.Text = "0"
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

    Sub controlEdit()
        txtKantorPusatKanwil.Enabled = False
        txtSpokeCapem.Enabled = False
        txtRMAnalist.Enabled = False
        'txtOmzetPerTahun.Enabled = False
        txtNoAplikasi.Enabled = False
        txtTglAplikasi.Enabled = False
        txtLimit.Enabled = False
        'ddlProgram.Enabled = False
        'ddlProgram.CssClass = "inpDdlDisabled"
        'ddlChannels.Enabled = False
        'ddlChannels.CssClass = "inpDdlDisabled"
        ddlSegment.Enabled = False
        ddlSegment.CssClass = "inpDdlDisabled"
        ddlJenisPengajuan.Enabled = False
        ddlJenisPengajuan.CssClass = "inpDdlDisabled"
        ddlUnitPengelola.Enabled = False
        ddlUnitPengelola.CssClass = "inpDdlDisabled"
        'ddlJenisAnalisaKeuangan.Enabled = False
        'ddlJenisAnalisaKeuangan.CssClass = "inpDdlDisabled"
        ddlKantorCabang.Enabled = False
        ddlKantorCabang.CssClass = "inpDdlDisabled"
    End Sub

    Sub controlRO()
        txtKantorPusatKanwil.Enabled = False
        txtSpokeCapem.Enabled = False
        txtRMAnalist.Enabled = False
        'txtOmzetPerTahun.Enabled = False
        txtNoAplikasi.Enabled = False
        txtTglAplikasi.Enabled = False
        txtLimit.Enabled = False
        'ddlProgram.Enabled = False
        'ddlProgram.CssClass = "inpDdlDisabled"
        'ddlChannels.Enabled = False
        'ddlChannels.CssClass = "inpDdlDisabled"
        ddlSegment.Enabled = False
        ddlSegment.CssClass = "inpDdlDisabled"
        ddlUnitPengelola.Enabled = False
        ddlUnitPengelola.CssClass = "inpDdlDisabled"
        ddlJenisPengajuan.Enabled = False
        ddlJenisPengajuan.CssClass = "inpDdlDisabled"
        'ddlJenisAnalisaKeuangan.Enabled = False
        'ddlJenisAnalisaKeuangan.CssClass = "inpDdlDisabled"
        ddlKantorCabang.Enabled = False
        ddlKantorCabang.CssClass = "inpDdlDisabled"
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            Dim obj As New AAS.Business.Entity.general
            obj.noApp = appno.Value
            obj.brwcode = brwcode.Value
            obj.tglApp = Mid(txtTglAplikasi.Text, 7, 4) + "-" & Mid(txtTglAplikasi.Text, 4, 2) + "-" & Mid(txtTglAplikasi.Text, 1, 2)
            obj.kantorPusat = hdKanwil.Value
            obj.spoke = hdCapem.Value
            obj.rm = txtRMAnalist.Text
            'obj.omzet = txtOmzetPerTahun.Text.Replace(",", "")
            'obj.program = ddlProgram.SelectedValue
            'obj.channels = ddlChannels.Text
            obj.segment = ddlSegment.SelectedValue
            obj.unitPengelola = ddlUnitPengelola.SelectedValue
            obj.jenisPengajuan = ddlJenisPengajuan.SelectedValue
            obj.limit = txtLimit.Text.Replace(",", "")
            'obj.jenisKeuangan = ddlJenisAnalisaKeuangan.SelectedValue
            obj.kantorCabang = ddlKantorCabang.SelectedValue
            Try
                If appno.Value <> "" Then
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkGeneral(txtNoAplikasi.Text)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update data
                        oApp.updateGeneralInfo(obj, usrLogin)
                    Else
                        'insert data
                        oApp.insertGeneralInfo(obj, usrLogin)
                    End If
                    'Dim dset As DataSet
                    'Dim dtbl As DataTable
                    'dset = oApp.checkApp(txtNoAplikasi.Text)
                    'dtbl = dset.Tables(0)
                    'If dtbl.Rows.Count > 0 Then
                    '    oApp.updateAppInfo(txtNoAplikasi.Text, "", usrLogin)
                    'End If
                    Session("appno") = txtNoAplikasi.Text
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                End If

            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        Dim lNot As String = ""

        lNot &= oGen.checkInputInaHTML("DropDownList", "Segmen", ddlSegment.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Unit Pengelola", ddlSegment.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Jenis Pengajuan", ddlJenisPengajuan.SelectedValue)
        lNot &= oGen.checkInputInaHTML("TextBox", "Limit Kredit", txtLimit.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Kantor Cabang", ddlKantorCabang.SelectedValue)

        If lNot <> "" Then
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If

    End Sub
End Class