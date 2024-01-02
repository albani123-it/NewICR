Public Class scorecardRating
    Inherits System.Web.UI.Page
    Dim oSc As New lScorecards
    Dim oGen As New lGeneral
    Dim objLog As New lLog
    Dim mn As Boolean = False

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            fillDdlAction(mn)
        End If
        If Not IsPostBack Then
            If Not Request.QueryString("scc_code") Is Nothing Then
                ViewState("scid") = Request.QueryString("scc_code")
                isiData(Request.QueryString("scc_code"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        btnSave.Enabled = False

        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_rating" Then
                    btnSave.Enabled = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_approve" Then
                    If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_rating" Then

                    End If
                End If
            Next
            If Not tf Then
                Session.Abandon()
                Response.Redirect("../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub fillDdlAction(ByVal delpar)
        Dim ds As DataSet
        ds = oSc.getListStatus(delpar)

        ddlAction.DataSource = ds
        ddlAction.DataTextField = "stu_name"
        ddlAction.DataValueField = "stu_id"
        ddlAction.DataBind()

    End Sub

    Sub isiData(ByVal scid)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oSc.getDetailOfScorecard(scid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblSccCode.Text = dt.Rows(0).Item("scc_code")
            lblSccName.Text = dt.Rows(0).Item("scc_name")


            numField.Value = "nume"

            Dim dsBin As DataSet
            Dim dtBin As DataTable

            dsBin = oSc.getDetailOfScorecardBaseline(scid)
            dtBin = dsBin.Tables(0)

            If dtBin.Rows.Count > 0 Then
                ddlAction.SelectedValue = dtBin.Rows(0).Item("base_status")

                Dim txtLimit As TextBox
                
                For i As Integer = 1 To dtBin.Rows.Count
                    If Not IsDBNull(dtBin.Rows(i - 1).Item("base_upper")) And Not IsDBNull(dtBin.Rows(i - 1).Item("base_rate")) Then
                        txtLimit = FindControl("txtLimit" & i)
                        txtLimit.Text = IIf(CDbl(dtBin.Rows(i - 1).Item("base_upper")) = 0, "", dtBin.Rows(i - 1).Item("base_upper"))
                    End If
                Next
            End If

            dsBin = oSc.getDetailOfScorecardRating(scid)
            dtBin = dsBin.Tables(0)

            If dtBin.Rows.Count > 0 Then
                ddlAction.SelectedValue = dtBin.Rows(0).Item("base_status")
                Dim txtScore As TextBox
                Dim txtLimit As TextBox
                Dim txtKategori As TextBox
                Dim txtDesc As TextBox
                For i As Integer = 1 To dtBin.Rows.Count
                    If Not IsDBNull(dtBin.Rows(i - 1).Item("base_upper")) And Not IsDBNull(dtBin.Rows(i - 1).Item("base_rate")) Then
                        txtLimit = FindControl("txtLimit" & i)
                        txtLimit.Text = IIf(CDbl(dtBin.Rows(i - 1).Item("base_upper")) = 0, "", dtBin.Rows(i - 1).Item("base_upper"))
                        txtScore = FindControl("txtScore" & i)
                        txtScore.Text = IIf(dtBin.Rows(i - 1).Item("base_rate") = "0", "", dtBin.Rows(i - 1).Item("base_rate"))
                        txtKategori = FindControl("txtKategori" & i)
                        txtKategori.Text = oGen.CheckNullString(dtBin.Rows(i - 1).Item("base_cat"))
                        txtDesc = FindControl("txtDesc" & i)
                        txtDesc.Text = oGen.CheckNullString(dtBin.Rows(i - 1).Item("base_desc"))
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim oData As New AAS.Business.Entity.scorecard_char
        For i As Integer = 1 To 20
            oData.att_from(i - 1) = ""
            oData.att_to(i - 1) = ""
            oData.baseline(i - 1) = ""
            oData.ratingCat(i - 1) = ""
            oData.ratingDesc(i - 1) = ""
        Next

        Dim limit As TextBox
        Dim score As TextBox
        Dim cat As TextBox
        Dim desc As TextBox

        If liBodyLoadAction.Text = "" Then
            Dim ds As DataSet
            Dim dt As DataTable

            ds = oSc.getDetailOfScorecardRating(ViewState("scid"))
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                Try
                    For i As Integer = 1 To dt.Rows.Count
                        limit = FindControl("txtLimit" & i)
                        oData.att_from(i - 1) = dt.Rows(i - 1).Item("base_upper")
                        score = FindControl("txtScore" & i)
                        oData.att_to(i - 1) = dt.Rows(i - 1).Item("base_rate")
                        oData.baseline(i - 1) = dt.Rows(i - 1).Item("base_count")
                        cat = FindControl("txtKategori" & i)
                        oData.ratingCat(i - 1) = dt.Rows(i - 1).Item("base_cat")
                        desc = FindControl("txtDesc" & i)
                        oData.ratingDesc(i - 1) = dt.Rows(i - 1).Item("base_desc")
                        'oData.baseline(i - 1) = dt.Rows(i - 1).Item("base_count")
                        'oData.baseline(i - 1) = dt.Rows(i - 1).Item("base_count")
                    Next
                    For index As Integer = dt.Rows.Count + 1 To 20
                        limit = FindControl("txtLimit" & index)
                        oData.att_from(index - 1) = 0
                        score = FindControl("txtScore" & index)
                        oData.att_to(index - 1) = 0
                        oData.baseline(index - 1) = 0
                        cat = FindControl("txtKategori" & index)
                        oData.ratingCat(index - 1) = ""
                        desc = FindControl("txtDesc" & index)
                        oData.ratingDesc(index - 1) = ""
                    Next
                    oSc.insertScorecardRatingLog(oData, ViewState("scid"), "Before", usrLogin, ddlAction.SelectedValue)

                    For i As Integer = 1 To 20
                        limit = FindControl("txtLimit" & i)
                        oData.att_from(i - 1) = IIf(limit.Text = "", 0, limit.Text) 'untuk input limit
                        score = FindControl("txtScore" & i)
                        oData.att_to(i - 1) = IIf(score.Text = "", 0, score.Text) 'untuk input score
                        cat = FindControl("txtKategori" & i)
                        oData.ratingCat(i - 1) = cat.Text 'untuk input kategori
                        desc = FindControl("txtDesc" & i)
                        oData.ratingDesc(i - 1) = desc.Text 'untuk input deskripsi
                    Next
                    oSc.insertScorecardRating(oData, ViewState("scid"), usrLogin, ddlAction.SelectedValue)
                    oSc.insertScorecardRatingLog(oData, ViewState("scid"), "After", usrLogin, ddlAction.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Baseline Updated - SCORECARDCODE=" & ViewState("scid"))
                    objLog.insertLog("scorecard_rating", logs)
                    liBodyLoadAction.Text = "alert('Scorecard Rating updated.');window.opener.location.href = window.opener.location.href; parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try
                    For i As Integer = 1 To 20
                        limit = FindControl("txtLimit" & i)
                        oData.att_from(i - 1) = IIf(limit.Text = "", 0, limit.Text) 'untuk input limit
                        score = FindControl("txtScore" & i)
                        oData.att_to(i - 1) = IIf(score.Text = "", 0, score.Text) 'untuk input score
                        oData.baseline(i - 1) = 0 'untuk input baseline
                        cat = FindControl("txtKategori" & i)
                        oData.ratingCat(i - 1) = cat.Text 'untuk input kategori
                        desc = FindControl("txtDesc" & i)
                        oData.ratingDesc(i - 1) = desc.Text 'untuk input deskripsi
                    Next
                    oSc.insertScorecardRating(oData, ViewState("scid"), usrLogin, ddlAction.SelectedValue)
                    oSc.insertScorecardRatingLog(oData, ViewState("scid"), "New", usrLogin, ddlAction.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Rating Creation - SCORECARDCODE=" & ViewState("scid"))
                    objLog.insertLog("scorecard_rating", logs)
                    liBodyLoadAction.Text = "alert('Scorecard Rating saved.');window.opener.location.href = window.opener.location.href; parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            End If

        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        Dim txtLim As TextBox
        Dim txtSco As TextBox
        Dim ctr As Integer = 0, ctrlimnum As Integer = 0, ctrsconum As Integer = 0
        For i As Integer = 1 To 20
            txtLim = FindControl("txtLimit" & i)
            If Not txtLim.Text = "" Then
                If Not IsNumeric(txtLim.Text) Then
                    ctrlimnum = ctrlimnum + 1
                End If
                txtSco = FindControl("txtScore" & i)
                If Not txtSco.Text = "" Then
                    'If Not IsNumeric(txtSco.Text) Then
                    '    ctrsconum = ctrsconum + 1
                    'End If
                    ctr = ctr + 1
                Else
                    lMsg = lMsg + "Please insert rating.\n"
                    txtSco.Focus()
                End If
            End If
        Next
        If ctr = 0 Then
            lMsg = lMsg + "Please insert at least 1 Scorecard Rating.\n"
        ElseIf ctrlimnum <> 0 Or ctrsconum <> 0 Then
            lMsg = lMsg + "Please check your Upper Limit or Rating.\n"
        Else
            lMsg = checkUpperLimit()
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Function checkUpperLimit()
        Dim lMsg = ""
        Dim c_val As String = 0
        Dim c_last As String = 0
        Dim c_upper As TextBox
        For i As Integer = 1 To 20
            c_upper = FindControl("txtLimit" & i)
            If Not c_upper.Text = "" Then
                c_last = c_upper.Text
            End If
            Dim tempVal = IIf(c_last < 0, c_last, c_last)
            If Not c_upper.Text = "" And i > 1 Then
                If Not (CDbl(c_val) < CDbl(tempVal) Or tempVal = 0) Then
                    lMsg = "Invalid Upper Limit. An Upper Limit can NOT be less than or equal than the previous Upper Limit."
                End If
            Else

                c_val = tempVal
            End If
        Next
        Return lMsg
    End Function

    'Private Sub btnApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApp.Click
    '    Try

    '    Catch ex As Exception
    '        Dim msg As String = ex.Message.Replace("'", "\'")
    '        liBodyLoadAction.Text = "alert('" & msg & "');"
    '    End Try
    'End Sub
End Class