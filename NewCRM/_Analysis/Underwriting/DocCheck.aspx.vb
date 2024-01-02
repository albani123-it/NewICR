Public Class DocCheck
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected exists As Boolean = False
    Dim read As Boolean = False
    Protected dataTable As DataTable
    Protected pagename As String = "Document Checking"
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
                isiData(no)
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

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkDocumentChecking(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            exists = True
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
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Function fillTemplate()
        Dim html As String = ""
        Dim total As Integer = 0

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDocumentCheckingCat
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("ldc_cat_has_child") = 0 Then
                    Dim dsDC As DataSet
                    Dim dtDC As DataTable
                    dsDC = oApp.getDocumentCheckingByCat(dt.Rows(i).Item("ldc_cat"))
                    dtDC = dsDC.Tables(0)
                    'html &= "<tr style='background-color:rgb(239, 237, 226);'>"
                    'html &= "<td width='50%'><b>" & dtDC.Rows(i).Item("ldc_name") & "</b></td>"
                    'html &= "<td width='20%'><input type='checkbox' id='chk_" & dtDC.Rows(i).Item("ldc_id") & "' /></td>"
                    'html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(i).Item("ldc_id") & "' width='300px' rows='3'></textarea></td>"
                    'html &= "</tr>"
                    For j As Integer = 0 To dtDC.Rows.Count - 1
                        html &= "<tr bgcolor='#f6f7f7'>"

                        html &= "<td width='60%'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"

                        If read = True Then
                            html &= "<td width='10%'><input type='checkbox' disabled id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                        Else
                            html &= "<td width='10%'><input type='checkbox' id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                        End If

                        If dtDC.Rows(j).Item("ldc_is_required") = True Then
                            html &= "&nbsp;<span style='color:Red;font-size:11pt;font-weight:bold;'>*</span>"
                        End If
                        html &= "</td>"

                        If read = True Then
                            html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxtDisabled' readonly='true'></textarea></td>"
                        Else
                            html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxt'></textarea></td>"
                        End If
                        html &= "</tr>"
                    Next
                Else
                    Dim index As Integer = 0

                    Dim dsDC As DataSet
                    Dim dtDC As DataTable
                    dsDC = oApp.getDocumentCheckingByCat(dt.Rows(i).Item("ldc_cat"))
                    dtDC = dsDC.Tables(0)

                    html &= "<tr style='background-color:rgb(239, 237, 226);'><td colspan='3'><b>" & dt.Rows(i).Item("ldc_cat") & "</b></td>"

                    For j As Integer = 0 To dtDC.Rows.Count - 1
                        If index Mod 2 = 0 Then
                            html &= "<tr bgcolor='#ebecec'>"
                        Else
                            html &= "<tr bgcolor='#f6f7f7'>"
                        End If

                        html &= "<td width='60%' style='padding-left:20px;'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"

                        If read = True Then
                            html &= "<td width='10%'><input type='checkbox' disabled id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                        Else
                            html &= "<td width='10%'><input type='checkbox' id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                        End If

                        html &= "<span>Ada</span>"

                        If dtDC.Rows(j).Item("ldc_is_required") = True Then
                            html &= "&nbsp;<span style='color:Red;font-size:11pt;font-weight:bold;'>*</span>"
                        End If
                        html &= "</td>"
                        'html &= "<td width='50%'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"
                        If read = True Then
                            html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxtDisabled' readonly='true'></textarea></td>"
                        Else
                            html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxt'></textarea></td>"
                        End If

                        html &= "</tr>"
                        index = index + 1
                    Next
                End If

            Next
        End If

        Return html
    End Function

    Function fillTemplateExists()
        Dim html As String = ""
        Dim total As Integer = 0

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDocumentCheckingCat
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("ldc_cat_has_child") = 0 Then
                    Dim dsDC As DataSet
                    Dim dtDC As DataTable
                    dsDC = oApp.getDocumentCheckingByCat(dt.Rows(i).Item("ldc_cat"))
                    dtDC = dsDC.Tables(0)

                    For j As Integer = 0 To dtDC.Rows.Count - 1
                        Dim dsEx As DataSet
                        Dim dtEx As DataTable
                        dsEx = oApp.getDocumentCheckingExists(appno.Value, dtDC.Rows(j).Item("ldc_id"))
                        dtEx = dsEx.Tables(0)
                        For k As Integer = 0 To dtEx.Rows.Count - 1
                            If dtDC.Rows(j).Item("ldc_id") = dtEx.Rows(k).Item("ldi_doc_id") Then
                                html &= "<tr bgcolor='#f6f7f7'>"

                                html &= "<td width='60%'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"
                                If dtEx.Rows(k).Item("ldi_value") = True Then
                                    If read = True Then
                                        html &= "<td width='10%'><input type='checkbox' disabled checked id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                                    Else
                                        html &= "<td width='10%'><input type='checkbox' checked id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                                    End If
                                Else
                                    If read = True Then
                                        html &= "<td width='10%'><input type='checkbox' disabled id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                                    Else
                                        html &= "<td width='10%'><input type='checkbox' id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                                    End If
                                End If

                                If dtDC.Rows(j).Item("ldc_is_required") = True Then
                                    html &= "&nbsp;<span style='color:Red;font-size:11pt;font-weight:bold;'>*</span>"
                                End If
                                html &= "</td>"

                                If read = True Then
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxtDisabled' readonly='true'>" & dtEx.Rows(k).Item("ldi_desc") & "</textarea></td>"
                                Else
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxt'>" & dtEx.Rows(k).Item("ldi_desc") & "</textarea></td>"
                                End If

                                html &= "</tr>"
                            Else
                                html &= "<tr bgcolor='#f6f7f7'>"

                                html &= "<td width='60%'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"
                                If read = True Then
                                    html &= "<td width='10%'><input type='checkbox' disabled id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                                Else
                                    html &= "<td width='10%'><input type='checkbox' id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' /><span>Ada</span>"
                                End If

                                If dtDC.Rows(j).Item("ldc_is_required") = True Then
                                    html &= "&nbsp;<span style='color:Red;font-size:11pt;font-weight:bold;'>*</span>"
                                End If
                                html &= "</td>"

                                If read = True Then
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxtDisabled' readonly='true'></textarea></td>"
                                Else
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxt'></textarea></td>"
                                End If

                                html &= "</tr>"
                            End If
                        Next
                    Next
                Else
                    Dim index As Integer = 0

                    Dim dsDC As DataSet
                    Dim dtDC As DataTable
                    dsDC = oApp.getDocumentCheckingByCat(dt.Rows(i).Item("ldc_cat"))
                    dtDC = dsDC.Tables(0)

                    html &= "<tr style='background-color:rgb(239, 237, 226);'><td colspan='3'><b>" & dt.Rows(i).Item("ldc_cat") & "</b></td>"

                    For j As Integer = 0 To dtDC.Rows.Count - 1
                        If index Mod 2 = 0 Then
                            html &= "<tr bgcolor='#ebecec'>"
                        Else
                            html &= "<tr bgcolor='#f6f7f7'>"
                        End If
                        Dim dsEx As DataSet
                        Dim dtEx As DataTable
                        dsEx = oApp.getDocumentCheckingExists(appno.Value, dtDC.Rows(j).Item("ldc_id"))
                        dtEx = dsEx.Tables(0)
                        For k As Integer = 0 To dtEx.Rows.Count - 1
                            If dtDC.Rows(j).Item("ldc_id") = dtEx.Rows(k).Item("ldi_doc_id") Then
                                html &= "<td width='60%' style='padding-left:20px;'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"
                                html &= "<td width='10%'>"
                                If dtEx.Rows(k).Item("ldi_value") = True Then
                                    If read = True Then
                                        html &= "<input type='checkbox' disabled checked id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                                    Else
                                        html &= "<input type='checkbox' checked id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                                    End If
                                Else
                                    If read = True Then
                                        html &= "<input type='checkbox' disabled id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                                    Else
                                        html &= "<input type='checkbox' id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                                    End If
                                End If

                                html &= "<span>Ada</span>"

                                If dtDC.Rows(j).Item("ldc_is_required") = True Then
                                    html &= "&nbsp;<span style='color:Red;font-size:11pt;font-weight:bold;'>*</span>"
                                End If
                                html &= "</td>"

                                If read = True Then
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxtDisabled' readonly='true'>" & dtEx.Rows(k).Item("ldi_desc") & "</textarea></td>"
                                Else
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxt'>" & dtEx.Rows(k).Item("ldi_desc") & "</textarea></td>"
                                End If
                            Else
                                html &= "<td width='60%' style='padding-left:20px;'><b>" & dtDC.Rows(j).Item("ldc_name") & "</b></td>"
                                html &= "<td width='10%'>"
                                If read = True Then
                                    html &= "<input type='checkbox' disabled id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                                Else
                                    html &= "<input type='checkbox' id='chk_" & dtDC.Rows(j).Item("ldc_id") & "' name='chk_" & dtDC.Rows(j).Item("ldc_id") & "' />"
                                End If
                                html &= "<span>Ada</span>"

                                If dtDC.Rows(j).Item("ldc_is_required") = True Then
                                    html &= "&nbsp;<span style='color:Red;font-size:11pt;font-weight:bold;'>*</span>"
                                End If
                                html &= "</td>"

                                If read = True Then
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxtDisabled' readonly='true'></textarea></td>"
                                Else
                                    html &= "<td width='30%'><textarea id='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' name='txtKet_" & dtDC.Rows(j).Item("ldc_id") & "' style='width:300px;' rows='3' class='inpTxt'></textarea></td>"
                                End If
                            End If
                        Next

                        html &= "</tr>"
                        index = index + 1
                    Next
                End If

            Next
        End If

        Return html
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If brw_code.Value <> "" Then
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oApp.getDocumentChecking
            dt = ds.Tables(0)

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim value As String = Request.Form("chk_" & dt.Rows(i).Item("ldc_id"))
                Dim desc As String = Request.Form("txtKet_" & dt.Rows(i).Item("ldc_id"))
                oApp.insertUpdateDocumentChecking(dt.Rows(i).Item("ldc_id"), appno.Value, dt.Rows(i).Item("ldc_name"), value, desc, usrLogin)
            Next
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