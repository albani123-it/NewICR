﻿Public Class analysisConstruction
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected sector As String = ""
    Protected dataTable As DataTable
    Protected pagename As String = "Kertas Kerja"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        lblNotif.Text = ""
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
        txtRencanaProyek.ReadOnly = True
        txtAkseptasiBank.ReadOnly = True
        txtNilaiProyekDiaksep.ReadOnly = True
        txtLaba.ReadOnly = True
        txtPajak.ReadOnly = True
        txtUangMukaPercent.ReadOnly = True
        txtUangMuka.ReadOnly = True
        rdJenisKontrak.Enabled = False
        txtPorsiPembiayaan.ReadOnly = True
        txtProjectCost.ReadOnly = True
        txtNilaiProyekBersih.ReadOnly = True

        txtMaxKebutuhanKMK.ReadOnly = True
        txtKMKBankLainValue1.ReadOnly = True
        txtKMKBankLainValue2.ReadOnly = True
        txtKMKBankLainValue3.ReadOnly = True
        txtKMKBankLainValue4.ReadOnly = True
        txtKMKBankLainValue5.ReadOnly = True
        txtKMKBankLainField1.ReadOnly = True
        txtKMKBankLainField2.ReadOnly = True
        txtKMKBankLainField3.ReadOnly = True
        txtKMKBankLainField4.ReadOnly = True
        txtKMKBankLainField5.ReadOnly = True
        txtKebutuhanFasilitasKMK.ReadOnly = True
        txtPembiayaanBankKMK.ReadOnly = True

        txtSGA.ReadOnly = True
        txtAkseptasiBank2.ReadOnly = True
        txtMaxPembiayaan.ReadOnly = True
        txtKMKBankLain2Value1.ReadOnly = True
        txtKMKBankLain2Value2.ReadOnly = True
        txtKMKBankLain2Value3.ReadOnly = True
        txtKMKBankLain2Value4.ReadOnly = True
        txtKMKBankLain2Value5.ReadOnly = True
        txtKMKBankLain2Field1.ReadOnly = True
        txtKMKBankLain2Field2.ReadOnly = True
        txtKMKBankLain2Field3.ReadOnly = True
        txtKMKBankLain2Field4.ReadOnly = True
        txtKMKBankLain2Field5.ReadOnly = True
        txtKebutuhanFasilitasKMKPRK.ReadOnly = True
        txtPembiayaanBankPRK.ReadOnly = True

        txtRencanaProyek.CssClass = "inpTxtDisabled"
        txtAkseptasiBank.CssClass = "inpTxtDisabled"
        txtNilaiProyekDiaksep.CssClass = "inpTxtDisabled"
        txtLaba.CssClass = "inpTxtDisabled"
        txtPajak.CssClass = "inpTxtDisabled"
        txtUangMukaPercent.CssClass = "inpTxtDisabled"
        txtUangMuka.CssClass = "inpTxtDisabled"
        txtPorsiPembiayaan.CssClass = "inpTxtDisabled"
        txtProjectCost.CssClass = "inpTxtDisabled"
        txtNilaiProyekBersih.CssClass = "inpTxtDisabled"

        txtMaxKebutuhanKMK.CssClass = "inpTxtDisabled"
        txtKMKBankLainValue1.CssClass = "inpTxtDisabled"
        txtKMKBankLainValue2.CssClass = "inpTxtDisabled"
        txtKMKBankLainValue3.CssClass = "inpTxtDisabled"
        txtKMKBankLainValue4.CssClass = "inpTxtDisabled"
        txtKMKBankLainValue5.CssClass = "inpTxtDisabled"
        txtKMKBankLainField1.CssClass = "inpTxtDisabled"
        txtKMKBankLainField2.CssClass = "inpTxtDisabled"
        txtKMKBankLainField3.CssClass = "inpTxtDisabled"
        txtKMKBankLainField4.CssClass = "inpTxtDisabled"
        txtKMKBankLainField5.CssClass = "inpTxtDisabled"
        txtKebutuhanFasilitasKMK.CssClass = "inpTxtDisabled"
        txtPembiayaanBankKMK.CssClass = "inpTxtDisabled"

        txtSGA.CssClass = "inpTxtDisabled"
        txtAkseptasiBank2.CssClass = "inpTxtDisabled"
        txtMaxPembiayaan.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Value1.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Value2.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Value3.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Value4.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Value5.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Field1.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Field2.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Field3.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Field4.CssClass = "inpTxtDisabled"
        txtKMKBankLain2Field5.CssClass = "inpTxtDisabled"
        txtKebutuhanFasilitasKMKPRK.CssClass = "inpTxtDisabled"
        txtPembiayaanBankPRK.CssClass = "inpTxtDisabled"

        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkWorksheet(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            txtRencanaProyek.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_rencanaProyek"))
            txtAkseptasiBank.Text = oGen.CheckNullInt(dt.Rows(0).Item("lws_akseptasiBank"))
            txtNilaiProyekDiaksep.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_nilaiProyekDiaksep"))
            txtLaba.Text = oGen.CheckNullInt(dt.Rows(0).Item("lws_laba"))
            txtPajak.Text = oGen.CheckNullInt(dt.Rows(0).Item("lws_pajak"))
            txtUangMukaPercent.Text = oGen.CheckNullInt(dt.Rows(0).Item("lws_uangMukaPersen"))
            txtUangMuka.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_uangMuka"))

            If oGen.CheckNullString(dt.Rows(0).Item("lws_jenisKontrak")) <> "" Then
                rdJenisKontrak.SelectedValue = dt.Rows(0).Item("lws_jenisKontrak")
            End If

            txtPorsiPembiayaan.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_porsiPembiayaan"))
            txtProjectCost.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_projectCost"))
            txtNilaiProyekBersih.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_nilaiProyekBersih"))

            txtMaxKebutuhanKMK.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_maxKebutuhanKMK"))
            txtKMKBankLainValue1.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue1"))
            txtKMKBankLainValue2.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue2"))
            txtKMKBankLainValue3.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue3"))
            txtKMKBankLainValue4.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue4"))
            txtKMKBankLainValue5.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue5"))
            txtKMKBankLainField1.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField1"))
            txtKMKBankLainField2.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField2"))
            txtKMKBankLainField3.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField3"))
            txtKMKBankLainField4.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField4"))
            txtKMKBankLainField5.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField5"))
            txtKebutuhanFasilitasKMK.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kebutuhanKMK"))
            txtPembiayaanBankKMK.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_pembiayaan"))

            txtSGA.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_sga"))
            txtAkseptasiBank2.Text = oGen.CheckNullInt(dt.Rows(0).Item("lws_akseptasiBank"))
            txtMaxPembiayaan.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_maxPembiayaanPRK"))
            txtKMKBankLain2Value1.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue1"))
            txtKMKBankLain2Value2.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue2"))
            txtKMKBankLain2Value3.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue3"))
            txtKMKBankLain2Value4.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue4"))
            txtKMKBankLain2Value5.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kmkBankLainValue5"))
            txtKMKBankLain2Field1.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField1"))
            txtKMKBankLain2Field2.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField2"))
            txtKMKBankLain2Field3.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField3"))
            txtKMKBankLain2Field4.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField4"))
            txtKMKBankLain2Field5.Text = oGen.CheckNullString(dt.Rows(0).Item("lws_kmkBankLainField5"))
            txtKebutuhanFasilitasKMKPRK.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_kebutuhanPRK"))
            txtPembiayaanBankPRK.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lws_pembiayaanPRK"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            If appno.Value <> "" Then
                Try
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkWorksheet(appno.Value)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update data
                        oApp.updateWorksheetConstruction(Request, appno.Value, usrLogin)
                    Else
                        'insert data
                        oApp.insertWorksheetConstruction(Request, appno.Value, usrLogin)
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

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If

    End Sub
End Class