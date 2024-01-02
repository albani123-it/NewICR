﻿Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Data
Public Class AsumsiRating
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim oAsm As New lAsumsiRating
    Dim cs As ClientScriptManager = Page.ClientScript
    Public myTable As New DataTable
    Protected dataTable As DataTable
    Protected pagename As String = "Deskripsi Rating"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Dim usrLogin As String

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
            If Not IsPostBack Then
                cekApp(Session("appno"))
                fillDdl()
            End If
            cekWorkflow(Session("appno"))
            executeJavascript()
        Else
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
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

    Sub cekSector(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerSector(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            sector = dt.Rows(0).Item("brw_sec")
        End If
    End Sub

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_no")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                isiData(dtbl.Rows(0).Item("app_brw_code"))
                appno.Value = dtbl.Rows(0).Item("app_no")
                brwcode.Value = dtbl.Rows(0).Item("app_brw_code")
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

    Sub isiData(ByVal brw)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkExRatingInfo(brw)
        dt = ds.Tables(0)
    End Sub

    Sub controlRO()
        btnAddNew.Visible = False
    End Sub

    Sub controlEdit()

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

    Sub fillDdl()
        Dim dt As DataTable
        dt = oAsm.getDataRatingDdl(appno.Value, usrLogin)
        ddlPeriodeRating.DataSource = dt
        ddlPeriodeRating.DataTextField = "text"
        ddlPeriodeRating.DataValueField = "value"
        ddlPeriodeRating.DataBind()

        'Dim dtCorp As DataTable
        'dtCorp = oRat.getOptGroupByOwner("ExtRating")
        'ddlRincian1.DataSource = dtCorp
        'ddlRincian1.DataTextField = "text"
        'ddlRincian1.DataValueField = "id"
        'ddlRincian1.DataGroupField = "group_text"
        'ddlRincian1.DataBind()

    End Sub

    Protected Sub ddlPeriodeRating_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPeriodeRating.SelectedIndexChanged
        Dim periode = ddlPeriodeRating.SelectedValue
        If periode <> 0 Then
            Dim dSet As DataSet
            Dim dTbl As DataTable

            dSet = oAsm.getDataRatingDesc(periode, appno.Value, usrLogin)
            dTbl = dSet.Tables(0)
            If dTbl.Rows.Count > 1 Then
                lblCustRating.Text = dTbl.Rows(1).Item("ratingdesc").ToString()
            Else
                lblCustRating.Text = "-"
            End If

            dTbl = dSet.Tables(1)
            If dTbl.Rows.Count > 1 Then
                lblFacRating.Text = dTbl.Rows(1).Item("ratingdesc").ToString()
            Else
                lblFacRating.Text = "-"
            End If

            dSet = oAsm.getDataAsumsiRating(appno.Value, periode)
            If dSet.Tables(0).Rows.Count > 1 Then
                getAsumsiDtl(dSet.Tables(0).Rows(1).Item("id"))
                txtIdRating.Text = dSet.Tables(0).Rows(1).Item("id")
            End If

            rowAsumsi.Visible = True
            dtlAsumsi.Visible = True
        Else
            lblCustRating.Text = ""
            lblFacRating.Text = ""
            rowAsumsi.Visible = False
            dtlAsumsi.Visible = False
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

    Protected Sub btnSaved_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaved.Click
        Dim ds As DataSet

        ds = oAsm.insertUpdatePerhitunganAR(Request, appno.Value, usrLogin)
        If ds.Tables(0).Rows.Count() > 0 Then
            txtIdRating.Text = ds.Tables(0).Rows(0).Item("lpar_id")
            getAsumsiDtl(ds.Tables(0).Rows(0).Item("lpar_id"))

            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "')", True)
        End If

    End Sub

    Sub getAsumsiDtl(ByVal Id)
        Dim ds As DataSet
        ds = oAsm.getListAsumsiDtl(Id)

        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Try
            Dim dt As DataTable
            oAsm.delAsumsiRating(dataItem("lpard_id"))
            getAsumsiDtl(txtIdRating.Text)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiHapus() + "')", True)
        Catch ex As Exception
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert(" + ex.Message + ")", True)
        End Try
        
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If
    End Sub
End Class