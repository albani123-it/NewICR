Public Class R_FacilityRating
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oRerat As New lRerating
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Rating Fasilitas"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        controlHome()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If

        If Session("appno") IsNot Nothing Then
            If Not IsPostBack Then
                cekData(Request.QueryString("appno"))
            End If
            appno.Value = Request.QueryString("appno")
        End If

        If Request.QueryString("typ") IsNot Nothing Then
            If Request.QueryString("typ") = "Borrower" Then
                DataTable = oRerat.getListOfMenu("Borrower")
            ElseIf Request.QueryString("typ") = "Facility" Then
                DataTable = oRerat.getListOfMenu("Facility")
            End If
            getMenuNext(Request.QueryString("typ"))
            executeJavascript()
        Else
            Response.Redirect("ratingMgmt.aspx")
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
                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                    url = dataTable.Rows(i).Item("lmr_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & appno.Value & "', 'Facility');"
        Dim javascript = "<script type='text/javascript'>" & script & "</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
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
        Dim typ = Request.QueryString("typ")

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
        Dim typ = Request.QueryString("typ")

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lmr_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_rat" Then
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

    Sub cekDeskripsi(ByVal rating, ByVal scc)

        Dim dsRat As DataSet
        Dim dtRat As DataTable
        dsRat = oApp.getRatingDesc(rating, scc)
        dtRat = dsRat.Tables(0)
        If dtRat.Rows.Count > 0 Then
            lblDeskripsi.Text = dtRat.Rows(0).Item("base_cat")
        End If
    End Sub

    Sub cekData(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            isiData(dtbl.Rows(0).Item("app_no"), dtbl.Rows(0).Item("app_prd_scc"))
            brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
        Else
            Response.Redirect("RatingMgmt.aspx")
        End If
    End Sub

    Sub isiData(ByVal no, ByVal scc)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oRat.checkFacilityRating(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            If oGen.CheckNullString(dt.Rows(0).Item("lfr_ratingDate")) = "" Then
                lblLastUpdate.Text = ""
            Else
                lblLastUpdate.Text = String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lfr_ratingDate"))
            End If
            lblScore.Text = oGen.CheckNullString(dt.Rows(0).Item("lfr_score"))
            lblRating.Text = oGen.CheckNullString(dt.Rows(0).Item("lfr_rating"))
            score.Value = oGen.CheckNullString(dt.Rows(0).Item("lfr_score"))
            rating.Value = oGen.CheckNullString(dt.Rows(0).Item("lfr_rating"))
            txtRatingDate.Text = oGen.CheckNullString(dt.Rows(0).Item("lfr_ratingDate"))
            'txtNextRating.Text = oGen.CheckNullString(dt.Rows(0).Item("lfr_nextRating"))
            'lblDeskripsi.Text = oGen.CheckNullString(dt.Rows(0).Item("base_cat"))
            cekDeskripsi(rating.Value, scc)
        End If
        grid1.DataSource = ds
        grid1.DataBind()

        ds = oApp.getListOfRatingDesc(scc)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("lblScore")
            For i As Integer = 0 To dt.Rows.Count - 1
                If i = dt.Rows.Count - 1 Then
                    dt.Rows(i).Item("lblScore") = "<= " & dt.Rows(i).Item("base_upper") & ""
                Else
                    dt.Rows(i).Item("lblScore") = "> " & dt.Rows(i + 1).Item("base_upper") & " - " & dt.Rows(i).Item("base_upper")
                End If
            Next
        End If
        grid2.DataSource = dt
        grid2.DataBind()
    End Sub

    Sub controlRO()
        txtRatingDate.Enabled = False
        btnSave.Visible = False
        btnRating.Visible = False
        btnNext.Visible = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblNotif.Text = ""
        cekInput()
        If lblNotif.Text = "" Then
            Try
                'Dim ratingdate As String = Mid(txtRatingDate.Text, 7, 4) + "-" & Mid(txtRatingDate.Text, 4, 2) + "-" & Mid(txtRatingDate.Text, 1, 2)
                Dim ratingdate As String = Now.ToString("yyyy-MM-dd")
                'Dim nextRating As String = Mid(txtNextRating.Text, 7, 4) + "-" & Mid(txtNextRating.Text, 4, 2) + "-" & Mid(txtNextRating.Text, 1, 2)
                Dim nextRating As String = "1900-01-01"
                oRat.insertFacilityRatingInfo(appno.Value, score.Value, rating.Value, ratingdate, nextRating, usrLogin, scc.Value)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        lblNotif.Text = ""
        Dim lNot As String = ""


        lNot &= oGen.checkInputInaHTML("TextBox", "Skor", score.Value)
        lNot &= oGen.checkInputInaHTML("TextBox", "Rating", rating.Value)
        lNot &= oGen.checkInputInaHTML("TextBox", "Tgl. Rating", txtRatingDate.Text)
        'If txtNextRating.Text <> "" Then
        '    Dim tgl As String = Mid(txtNextRating.Text, 7, 4) + "-" & Mid(txtNextRating.Text, 4, 2) + "-" & Mid(txtNextRating.Text, 1, 2)
        '    Dim result = DateTime.Compare(tgl, Now.ToString("yyyy-MM-dd"))
        '    If result < 0 Then
        '        lNot &= "Tgl. Rating selanjutnya tidak boleh kurang dari hari ini."
        '    End If
        'End If
        If scc.Value = "" Then
            lNot &= "Mohon melakukan rating kembali."
        End If

        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub btnRating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRating.Click
        If appno.Value <> "" Then
            Try
                Dim ds As DataSet
                Dim dt As DataTable
                ds = oRat.checkFacilityCredit(appno.Value)
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    Dim dset As DataSet
                    Dim dtbl As DataTable
                    dset = oApp.checkApp(appno.Value)
                    dtbl = dset.Tables(0)
                    If dtbl.Rows.Count > 0 Then
                        oApp.insertFacilitySummary(appno.Value, dtbl.Rows(0).Item("app_brw_code"), usrLogin)
                        scc.Value = dtbl.Rows(0).Item("app_prd_scc")
                        Dim dsRat As DataSet
                        Dim dtRat As DataTable
                        dsRat = oApp.getFacilityRating(appno.Value, dtbl.Rows(0).Item("app_prd_scc"))
                        dtRat = dsRat.Tables(0)
                        If dtRat.Rows.Count > 0 Then
                            lblRating.Text = dtRat.Rows(0).Item("Rating")
                            rating.Value = dtRat.Rows(0).Item("Rating")
                            lblScore.Text = String.Format("{0:N2}", CDbl(dtRat.Rows(0).Item("Score")))
                            score.Value = String.Format("{0:N2}", CDbl(dtRat.Rows(0).Item("Score")))
                            txtRatingDate.Text = Now.ToString("dd-MM-yyyy")
                            txtNextRating.Text = ""
                            cekDeskripsi(rating.Value, dtbl.Rows(0).Item("app_prd_scc"))
                        Else
                            liBodyLoadAction.Text = "alert('Data Summary tidak ada.');window.location.href=window.location.href;"
                        End If
                    End If
                Else
                    liBodyLoadAction.Text = "alert('Data Permohonan Kredit belum ada.');window.location.href=window.location.href;"
                End If
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        Else
            liBodyLoadAction.Text = "alert('No. Aplikasi tidak tersedia');window.location.href=window.location.href;"
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value & "&typ=Facility")
        Else
            Response.Redirect("RatingMgmt.aspx")
        End If
    End Sub
End Class