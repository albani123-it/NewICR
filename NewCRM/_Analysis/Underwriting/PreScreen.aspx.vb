Public Class PreScreen
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oGlo As New lglobalSetting
    Dim cs As ClientScriptManager = Page.ClientScript

    Protected exists As Boolean = False
    Dim read As Boolean = False
    Protected dataTable As DataTable
    Protected pagename As String = "Pre Screening"
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

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                isiData(dtbl.Rows(0).Item("app_brw_code"))
                brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
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

    Sub isiData(ByVal brw)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkPreScreening(brw)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            exists = True
            txtAlasan.Text = oGen.CheckNullString(dt.Rows(0).Item("lpr_reason"))
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
            End If

        Next
    End Sub

    Sub controlHome()
        btnNext.Visible = False
    End Sub

    Sub controlEdit()

    End Sub

    Sub controlRO()
        read = True
        txtAlasan.ReadOnly = True
        txtAlasan.CssClass = "inpTxtDisabled"
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Function fillBorrowerType()
        Dim html As String = ""
        brwType.Value = ""
        html &= "<td><b>Debitur</b></td>"



        Dim dt As DataTable

        dt = oGlo.getListGloPreScreening
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                html &= "<td>"
                If i = 1 Then
                    If read = True Then
                        html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' checked onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                    Else
                        html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' checked onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                    End If
                    brwType.Value = dt.Rows(i).Item("id")
                    minScore.Value = dt.Rows(i).Item("min_score")
                Else
                    If read = True Then
                        html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                    Else
                        html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                    End If
                End If
                html &= "<span>" & dt.Rows(i).Item("brw_type") & "</span>"

                html &= "</td>"
            Next
        Else
            html &= "<td></td>"
            html &= "<td></td>"
        End If
        Return html
    End Function

    Function fillBorrowerTypeExists()
        Dim html As String = ""
        brwType.Value = ""
        html &= "<td><b>Debitur</b></td>"



        Dim dt As DataTable

        dt = oGlo.getListGloPreScreening
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                html &= "<td>"

                If brw_code.Value <> "" Then
                    Dim dsEx As DataSet
                    Dim dtEx As DataTable
                    dsEx = oApp.getPreScreeningExists(brw_code.Value)
                    dtEx = dsEx.Tables(0)

                    For j As Integer = 0 To dtEx.Rows.Count - 1
                        If dt.Rows(i).Item("id") = dtEx.Rows(j).Item("lpr_brw_type") Then
                            If read = True Then
                                html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' checked onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                            Else
                                html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' checked onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                            End If
                            brwType.Value = dt.Rows(i).Item("id")
                            minScore.Value = dt.Rows(i).Item("min_score")
                        Else
                            If read = True Then
                                html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                            Else
                                html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                            End If
                        End If
                    Next

                Else
                    If i = 0 Then
                        If read = True Then
                            html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' checked onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                        Else
                            html &= "<input type='radio' readonly='true' name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' checked onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                        End If
                        brwType.Value = dt.Rows(i).Item("id")
                    Else
                        If read = True Then
                            html &= "<input type='radio' disabled name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                        Else
                            html &= "<input type='radio' readonly='true' name='chkDebt' id='chk_" & dt.Rows(i).Item("id") & "' value='" & dt.Rows(i).Item("min_score") & "' onclick=checkApproval('" & dt.Rows(i).Item("id") & "') />"
                        End If
                    End If
                End If
                
                
                html &= "<span>" & dt.Rows(i).Item("brw_type") & "</span>"

                html &= "</td>"
            Next
        Else
            html &= "<td></td>"
            html &= "<td></td>"
        End If
        Return html
    End Function

    Function fillPreScreen()
        Dim html As String = ""
        Dim total As Integer = 0
        Dim totalEx As Integer = 0

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getPreScreeningCat
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                html &= "<tr style='background-color:rgb(239, 237, 226);'><td colspan='3'><b>" & dt.Rows(i).Item("lps_cat") & "</b></td>"

                Dim dsPr As DataSet
                Dim dtPr As DataTable
                dsPr = oApp.getPreScreeningByCat(dt.Rows(i).Item("lps_cat"))
                dtPr = dsPr.Tables(0)
                Dim index As Integer = 0
                For j As Integer = 0 To dtPr.Rows.Count - 1
                    If index Mod 2 = 0 Then
                        html &= "<tr bgcolor='#ebecec'><td width='80%' style='padding-left:20px;'><b>" & dtPr.Rows(j).Item("lps_name") & "</b></td>"
                    Else
                        html &= "<tr bgcolor='#f6f7f7'><td width='80%' style='padding-left:20px;'><b>" & dtPr.Rows(j).Item("lps_name") & "</b></td>"
                    End If

                    Dim dsPrDetail As DataSet
                    Dim dtPrDetail As DataTable
                    dsPrDetail = oApp.getPreScreeningDetail(dtPr.Rows(j).Item("lps_field"))
                    dtPrDetail = dsPrDetail.Tables(0)


                    For k As Integer = 0 To dtPrDetail.Rows.Count - 1
                        html &= "<td>"
                        If k = 0 Then
                            If read = True Then
                                html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' checked onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                            Else
                                html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' checked onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                            End If

                            html &= "<input type='hidden' id='hid_" & dtPr.Rows(j).Item("lps_field") & "' name='hid_" & dtPr.Rows(j).Item("lps_field") & "' value='" & dtPrDetail.Rows(k).Item("lpd_weight") & "' />"
                            totalEx = totalEx + dtPrDetail.Rows(k).Item("lpd_weight")
                        Else
                            If read = True Then
                                html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                            Else
                                html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                            End If
                        End If
                        total = total + dtPrDetail.Rows(k).Item("lpd_weight")
                        html &= "<span>" & dtPrDetail.Rows(k).Item("lpd_value") & "</span>"
                        html &= "</td>"
                    Next
                    html &= "</tr>"
                    index = index + 1
                Next

            Next
        End If
        totalScore.Value = total
        totalScoreMove.Value = totalEx
        txtTotalSkor.Text = String.Format("{0:N2}", totalEx / total * 100)

        If CDbl(totalEx / total * 100) > minScore.Value Then
            decision.Value = 1
            rdDecisionYes.Checked = True
        Else
            decision.Value = 0
            rdDecisionNo.Checked = True
        End If
        Return html
    End Function

    Function fillPreScreenExists()
        Dim html As String = ""
        Dim total As Integer = 0
        Dim totalEx As Integer = 0

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getPreScreeningCat
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                html &= "<tr style='background-color:rgb(239, 237, 226);'><td colspan='3'><b>" & dt.Rows(i).Item("lps_cat") & "</b></td>"

                Dim dsPr As DataSet
                Dim dtPr As DataTable
                dsPr = oApp.getPreScreeningByCat(dt.Rows(i).Item("lps_cat"))
                dtPr = dsPr.Tables(0)
                Dim index As Integer = 0
                For j As Integer = 0 To dtPr.Rows.Count - 1
                    If index Mod 2 = 0 Then
                        html &= "<tr bgcolor='#ebecec'><td width='80%' style='padding-left:20px;'><b>" & dtPr.Rows(j).Item("lps_name") & "</b></td>"
                    Else
                        html &= "<tr bgcolor='#f6f7f7'><td width='80%' style='padding-left:20px;'><b>" & dtPr.Rows(j).Item("lps_name") & "</b></td>"
                    End If

                    Dim dsPrDetail As DataSet
                    Dim dtPrDetail As DataTable
                    dsPrDetail = oApp.getPreScreeningDetail(dtPr.Rows(j).Item("lps_field"))
                    dtPrDetail = dsPrDetail.Tables(0)


                    For k As Integer = 0 To dtPrDetail.Rows.Count - 1
                        html &= "<td>"
                        If brw_code.Value <> "" Then
                            Dim dsPrDetailEx As DataSet
                            Dim dtPrDetailEx As DataTable
                            dsPrDetailEx = oApp.getPreScreeningDetailExists(brw_code.Value, dtPr.Rows(j).Item("lps_field"))
                            dtPrDetailEx = dsPrDetailEx.Tables(0)
                            For l As Integer = 0 To dtPrDetailEx.Rows.Count - 1
                                If dtPrDetail.Rows(k).Item("lpd_parent") = dtPrDetailEx.Rows(l).Item("lpr_field") Then
                                    If dtPrDetail.Rows(k).Item("lpd_value") = dtPrDetailEx.Rows(l).Item("lpr_value") Then
                                        If read = True Then
                                            html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' checked onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                        Else
                                            html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' checked onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                        End If
                                        totalEx = totalEx + dtPrDetailEx.Rows(l).Item("lpr_weight")
                                        html &= "<input type='hidden' id='hid_" & dtPr.Rows(j).Item("lps_field") & "' name='hid_" & dtPr.Rows(j).Item("lps_field") & "' value='" & dtPrDetail.Rows(k).Item("lpd_weight") & "' />"
                                    Else
                                        If k = 0 Then
                                            If read = True Then
                                                html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                            Else
                                                html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                            End If

                                        Else
                                            If read = True Then
                                                html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                            Else
                                                html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                            End If
                                        End If
                                    End If
                                    total = total + dtPrDetail.Rows(k).Item("lpd_weight")
                                Else
                                    If k = 0 Then
                                        If read = True Then
                                            html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' checked onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                        Else
                                            html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' checked onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                        End If

                                        html &= "<input type='hidden' id='hid_" & dtPr.Rows(j).Item("lps_field") & "' name='hid_" & dtPr.Rows(j).Item("lps_field") & "' value='" & dtPrDetail.Rows(k).Item("lpd_weight") & "' />"
                                        total = total + dtPrDetail.Rows(k).Item("lpd_weight")
                                    Else
                                        If read = True Then
                                            html &= "<input type='radio' disabled value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                        Else
                                            html &= "<input type='radio' value='" & dtPrDetail.Rows(k).Item("lpd_value") & "' name='rd_" & dtPr.Rows(j).Item("lps_field") & "' id='rd_" & dtPr.Rows(j).Item("lps_field") & "_" & dtPrDetail.Rows(k).Item("lpd_value") & "' onclick=checkScore('" & dtPr.Rows(j).Item("lps_field") & "','" & dtPrDetail.Rows(k).Item("lpd_weight") & "'); />"
                                        End If
                                    End If
                                End If

                            Next
                        End If



                        html &= "<span>" & dtPrDetail.Rows(k).Item("lpd_value") & "</span>"
                        html &= "</td>"
                    Next
                    html &= "</tr>"
                    index = index + 1
                Next

            Next
        End If
        totalScore.Value = total
        totalScoreMove.Value = totalEx
        txtTotalSkor.Text = String.Format("{0:N2}", totalEx / total * 100)

        If CDbl(totalEx / total * 100) > minScore.Value Then
            decision.Value = 1
            rdDecisionYes.Checked = True
        Else
            decision.Value = 0
            rdDecisionNo.Checked = True
        End If
        Return html
    End Function

    Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnSave.Click
        If brw_code.Value <> "" And brwType.Value <> "" And appno.Value <> "" Then
            Dim total As Integer = totalScoreMove.Value / totalScore.Value * 100
            oApp.insertUpdatePreScreening(appno.Value, brw_code.Value, brwType.Value, total, decision.Value, txtAlasan.Text, usrLogin)

            Dim ds As DataSet
            Dim dt As DataTable
            ds = oApp.getPreScreening()
            dt = ds.Tables(0)
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim value As String = Request.Form("rd_" & dt.Rows(i).Item("lps_field"))
                Dim weight As String = Request.Form("hid_" & dt.Rows(i).Item("lps_field"))

                oApp.insertUpdatePreScreeningDetail(brw_code.Value, dt.Rows(i).Item("lps_field"), value, weight, usrLogin)

            Next
            'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil disimpan.');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
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