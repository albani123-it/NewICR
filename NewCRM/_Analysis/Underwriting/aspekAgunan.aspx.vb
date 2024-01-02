Public Class aspekAgunan
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected exists As Boolean = False
    Dim read As Boolean = False
    Protected product As String = ""
    Protected sector As String = ""
    Protected dataTable As DataTable
    Protected pagename As String = "Aspek Agunan"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""

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
            cekProduct(Session("appno"))
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

    Sub cekProduct(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkProductType(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            product = dt.Rows(0).Item("prd_product")
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

    Sub controlEdit()

    End Sub

    Sub controlRO()
        read = True
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkAppAspek(no, "Aspek Agunan")
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            exists = True
        End If
    End Sub

    Function fillAspekExists()
        Dim html As String = ""

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getAspekAgunan(Request.QueryString("appno"))
        dt = ds.Tables(0)
        Dim index As Integer = 0
        If dt.Rows.Count > 0 Then
            'loop for question (fieldnya)
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dsP As DataSet
                Dim dtP As DataTable
                dsP = oApp.getBankRatingProduct(product)
                dtP = dsP.Tables(0)
                For tmp As Integer = 0 To dtP.Rows.Count - 1
                    If dt.Rows(i).Item("lbr_field") = dtP.Rows(tmp).Item("lbp_lbr_field") = True Then
                        If index Mod 2 = 0 Then
                            html &= "<tr bgcolor='#f6f7f7'><td width='30%'><b>" & dt.Rows(i).Item("lbr_name") & "</b></td>"
                        Else
                            html &= "<tr bgcolor='#ebecec'><td width='30%'><b>" & dt.Rows(i).Item("lbr_name") & "</b></td>"
                        End If

                        If read = True Then
                            'html &= "<td><select disabled='true' id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' onchange='return pilBankRating(" & dt.Rows(i).Item("lbr_id") & ")' class='inpDdl'>"
                            html &= "<td><select disabled='true' id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' class='inpDdlDisabled'>"
                        Else
                            'html &= "<td><select id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' onchange='return pilBankRating(" & dt.Rows(i).Item("lbr_id") & ")' class='inpDdl'>"
                            html &= "<td><select id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' class='inpDdl'>"
                        End If


                        Dim ds2 As DataSet 'get choice for all answer (detail) by id question [A,B,C,D,E]
                        Dim dt2 As DataTable
                        ds2 = oApp.getBankRatingDetail(dt.Rows(i).Item("lbr_id"))
                        dt2 = ds2.Tables(0)

                        html &= "<option value='-'>-</option>" 'strip
                        'loop for choice answer (value) [Selected DropDownList]
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            If appno.Value <> "" Then
                                Dim ds3 As DataSet
                                Dim dt3 As DataTable
                                'check if borrower has data for the answer
                                ds3 = oApp.getDetailOfAppAspek(appno.Value, dt2.Rows(j).Item("lbd_parent"))
                                dt3 = ds3.Tables(0)
                                If dt3.Rows.Count > 0 Then
                                    'selected the choice with data for the answer
                                    If dt2.Rows(j).Item("lbd_value") = dt3.Rows(0).Item("la_value") Then
                                        html &= "<option value='" & dt2.Rows(j).Item("lbd_value") & "' selected>" & dt2.Rows(j).Item("lbd_value") & "</option>"
                                    Else
                                        html &= "<option value='" & dt2.Rows(j).Item("lbd_value") & "'>" & dt2.Rows(j).Item("lbd_value") & "</option>"
                                    End If
                                Else
                                    'default
                                    html &= "<option value='" & dt2.Rows(j).Item("lbd_value") & "'>" & dt2.Rows(j).Item("lbd_value") & "</option>"
                                End If
                            End If
                        Next
                        html &= "</select>"

                        Dim ds5 As DataSet
                        Dim dt5 As DataTable
                        ds5 = oApp.getDetailOfAppAspek(appno.Value, dt.Rows(i).Item("lbr_id"))
                        dt5 = ds5.Tables(0)
                        If dt5.Rows.Count > 0 Then
                            html &= "<input type='hidden' id='hid_" & dt.Rows(i).Item("lbr_id") & "' value='" & dt5.Rows(0).Item("la_value") & "'/>"
                            html &= "</td></td>"

                            html &= "<td><table border='0' style='border-collapse:collapse;><tr height='60'>"
                            html &= "<td width='615' >"
                            'loop for the choice answer (detail answer)
                            For k As Integer = 0 To dt2.Rows.Count - 1
                                html &= "<table style='font-weight:bold;'><tr>"
                                html &= "<td style='vertical-align:top;'>" & dt2.Rows(k).Item("lbd_value") & "</td>"
                                html &= "<td>" & dt2.Rows(k).Item("lbd_desc") & "</td>"
                                html &= "</tr></table>"
                            Next
                            html &= "</td>"
                            html &= "</tr><tr>"
                            html &= "<td style='font-weight:bold;padding:10px 0 0 12px;'><span style='vertical-align:top;'>Catatan:&nbsp;</span>"
                            If read = True Then
                                html &= "<textarea id='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' name='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' class='inpTxtDisabled' readonly='true' rows='3' style='float:none;width:500px;'>" & dt5.Rows(0).Item("la_desc") & "</textarea>"
                            Else
                                html &= "<textarea id='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' name='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' class='inpTxt' rows='3' style='float:none;width:500px;'>" & dt5.Rows(0).Item("la_desc") & "</textarea>"
                            End If

                            html &= "</td>"
                            html &= "</tr></table></td>"
                        Else
                            html &= "<input type='hidden' id='hid_" & dt.Rows(i).Item("lbr_id") & "' value='A'/>"
                            html &= "</td></td>"

                            html &= "<td><table border='0' style='border-collapse:collapse;><tr height='60'>"
                            html &= "<td width='615' >"
                            'loop for the choice answer (detail answer)
                            For k As Integer = 0 To dt2.Rows.Count - 1
                                html &= "<table style='font-weight:bold;'><tr>"
                                html &= "<td style='vertical-align:top;'>" & dt2.Rows(k).Item("lbd_value") & "</td>"
                                html &= "<td>" & dt2.Rows(k).Item("lbd_desc") & "</td>"
                                html &= "</tr></table>"
                            Next
                            html &= "</td>"
                            html &= "</tr><tr>"
                            html &= "<td style='font-weight:bold;padding:10px 0 0 12px;'><span style='vertical-align:top;'>Catatan:&nbsp;</span>"
                            If read = True Then
                                html &= "<textarea id='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' name='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' class='inpTxt' rows='3' style='float:none;width:500px;'></textarea>"
                            Else
                                html &= "<textarea id='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' name='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' class='inpTxt' rows='3' style='float:none;width:500px;'></textarea>"
                            End If
                            html &= "</td>"
                            html &= "</tr></table></td>"
                        End If

                        html &= "</tr>"
                        index = index + 1
                    End If
                Next
                
            Next
        End If


        Return html
    End Function

    Function fillAspek()
        Dim html As String = ""

        Dim ds As DataSet 'bank rating
        Dim dt As DataTable
        ds = oApp.getAspekAgunan(Request.QueryString("appno"))
        dt = ds.Tables(0)
        Dim index As Integer = 0
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dsP As DataSet
                Dim dtP As DataTable
                dsP = oApp.getBankRatingProduct(product)
                dtP = dsP.Tables(0)
                For tmp As Integer = 0 To dtP.Rows.Count - 1
                    If dt.Rows(i).Item("lbr_field") = dtP.Rows(tmp).Item("lbp_lbr_field") = True Then
                        If index Mod 2 = 0 Then
                            html &= "<tr bgcolor='#f6f7f7'><td width='30%'><b>" & dt.Rows(i).Item("lbr_name") & "</b></td>"
                        Else
                            html &= "<tr bgcolor='#ebecec'><td width='30%'><b>" & dt.Rows(i).Item("lbr_name") & "</b></td>"
                        End If

                        If read = True Then
                            'html &= "<td><select disabled='true' id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' onchange='return pilBankRating(" & dt.Rows(i).Item("lbr_id") & ")' class='inpDdl'>"
                            html &= "<td><select disabled='true' id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' class='inpDdl'>"
                        Else
                            'html &= "<td><select id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' onchange='return pilBankRating(" & dt.Rows(i).Item("lbr_id") & ")' class='inpDdl'>"
                            html &= "<td><select id='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' name='selBankDet_" & dt.Rows(i).Item("lbr_id") & "' runat='server' class='inpDdl'>"
                        End If


                        Dim ds2 As DataSet 'bank rating detail
                        Dim dt2 As DataTable
                        ds2 = oApp.getBankRatingDetail(dt.Rows(i).Item("lbr_id"))
                        dt2 = ds2.Tables(0)

                        html &= "<option value='-'>-</option>" 'strip
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            If ViewState("selBankDet_" & dt.Rows(i).Item("lbr_id")) IsNot Nothing And ViewState("selBankDet_" & dt.Rows(i).Item("lbr_id")) = dt2.Rows(j).Item("lbd_value") Then
                                html &= "<option value='" & dt2.Rows(j).Item("lbd_value") & "' Selected >" & dt2.Rows(j).Item("lbd_value") & "</option>"
                            Else
                                html &= "<option value='" & dt2.Rows(j).Item("lbd_value") & "'>" & dt2.Rows(j).Item("lbd_value") & "</option>"
                            End If
                        Next

                        html &= "</select><input type='hidden' id='hid_" & dt.Rows(i).Item("lbr_id") & "' value='A'/></td>"
                        html &= "<td><table width='100%' border='0' style='border-collapse:collapse;><tr height='60'>"
                        html &= "<td width='615'>"
                        For k As Integer = 0 To dt2.Rows.Count - 1
                            'If k = 0 Then
                            '    html &= "<td width='615' id='part_" & dt2.Rows(k).Item("lbd_parent") & dt2.Rows(k).Item("lbd_value") & "'>" & dt2.Rows(k).Item("lbd_desc") & "</td>"
                            'Else
                            '    html &= "<td width='615' id='part_" & dt2.Rows(k).Item("lbd_parent") & dt2.Rows(k).Item("lbd_value") & "' style='display:none;'>" & dt2.Rows(k).Item("lbd_desc") & "</td>"
                            'End If
                            html &= "<table style='font-weight:bold;'></tr>"
                            html &= "<td style='vertical-align:top;'>" & dt2.Rows(k).Item("lbd_value") & "</td>"
                            html &= "<td>" & dt2.Rows(k).Item("lbd_desc") & "</td>"
                            html &= "</tr></table>"
                        Next
                        html &= "</td>"
                        html &= "</tr><tr>"
                        html &= "<td style='font-weight:bold;padding:10px 0 0 12px;'><span style='vertical-align:top;'>Catatan:&nbsp;</span><textarea name='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' id='txtDesc_" & dt.Rows(i).Item("lbr_id") & "' class='inpTxt' rows='3' style='float:none;width:500px;'></textarea></td>"
                        html &= "</tr></table></td></tr>"
                        index = index + 1
                    End If
                Next
            Next
        End If


        Return html
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblNotif.Text = ""
        Dim lNot As String = ""

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getAspekAgunan(Request.QueryString("appno"))
        dt = ds.Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dsP As DataSet
            Dim dtP As DataTable
            dsP = oApp.getBankRatingProduct(product)
            dtP = dsP.Tables(0)
            For tmp As Integer = 0 To dtP.Rows.Count - 1
                If dt.Rows(i).Item("lbr_field") = dtP.Rows(tmp).Item("lbp_lbr_field") = True Then
                    Dim value As String = Request.Form("selBankDet_" & dt.Rows(i).Item("lbr_id"))
                    Dim name As String = dt.Rows(i).Item("lbr_name")

                    If value = "-" Then
                        lNot &= "&bull;&nbsp; " + name + " harus di isi ! </br>"
                    Else
                        ViewState("selBankDet_" & dt.Rows(i).Item("lbr_id")) = value
                    End If
                End If
            Next
        Next

        If lNot <> "" Then
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If

        If appno.Value <> "" And lNot = "" Then
            Try
                ds = oApp.getAspekAgunan(Request.QueryString("appno"))
                dt = ds.Tables(0)
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dsP As DataSet
                    Dim dtP As DataTable
                    dsP = oApp.getBankRatingProduct(product)
                    dtP = dsP.Tables(0)
                    For tmp As Integer = 0 To dtP.Rows.Count - 1
                        If dt.Rows(i).Item("lbr_field") = dtP.Rows(tmp).Item("lbp_lbr_field") = True Then
                            Dim value As String = Request.Form("selBankDet_" & dt.Rows(i).Item("lbr_id"))
                            Dim desc As String = Request.Form("txtDesc_" & dt.Rows(i).Item("lbr_id"))
                            oRat.insertUpdateAspekByAppno(appno.Value, dt.Rows(i).Item("lbr_id"), value, "Aspek Agunan", desc, usrLogin)
                        End If
                    Next

                Next
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
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