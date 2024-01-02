Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Data

Public Class R_AsumsiRating
    Inherits System.Web.UI.Page

    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim oAsm As New lAsumsiRating
    Dim oRerat As New lRerating
    Dim cs As ClientScriptManager = Page.ClientScript
    Public myTable As New DataTable
    Protected dataTable As DataTable
    Protected pagename As String = "Asumsi Rating"
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
        ElseIf Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
            brwcode.Value = Session("brwcode")
        End If
        dataTable = oRerat.getListOfMenu("Borrower")
        checkAppByBrwCode(Session("brwcode"))
        cekSector(appno.Value)
        If Session("brwcode") IsNot Nothing And Not IsPostBack Then
            fillDdl()
            isiData(Session("brwcode"))
            getMenuNext("Borrower")
            executeJavascript()
        End If
    End Sub

    Sub executeJavascript()
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        dset = oApp.checkAppByBrwCode(brwcode.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                    url = dataTable.Rows(i).Item("lmr_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & brwcode.Value & "', 'Borrower');"
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

    Sub checkAppByBrwCode(ByVal brwcode)

        Dim ds As DataSet
        ds = oApp.checkAppByBrwCode(brwcode)

        If ds.Tables(0).Rows.Count > 0 Then
            appno.Value = ds.Tables(0).Rows(0).Item("app_no")
        End If

    End Sub

    Sub getMenuNext(ByVal typ)
        For i As Integer = 0 To dataTable.Rows.Count - 1
            If dataTable.Rows(i).Item("lmr_name") = pagename Then
                Dim dt2 As DataTable
                dt2 = oRerat.getNextMenu(dataTable.Rows(i).Item("lmr_id"), typ)
                If dt2.Rows.Count > 0 Then
                    menuNext = dt2.Rows(0).Item("lmr_link")
                End If
            End If
        Next
    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        Dim typ = dataTable.Rows(0).Item("lmr_type").ToString()

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lmr_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        Dim typ = dataTable.Rows(0).Item("lmr_type").ToString()

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lmr_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

    Sub isiData(ByVal brw)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkExRatingInfo(brw)
        dt = ds.Tables(0)
    End Sub

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
            oAsm.delAsumsiRating(dataItem("lpard_id"))
            getAsumsiDtl(txtIdRating.Text)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiHapus() + "')", True)
        Catch ex As Exception
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert(" + ex.Message + ")", True)
        End Try

    End Sub

End Class