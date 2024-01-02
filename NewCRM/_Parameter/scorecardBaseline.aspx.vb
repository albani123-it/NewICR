Public Class scorecardBaseline
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
            If Not Request.QueryString("scid") Is Nothing Then
                ViewState("scid") = Request.QueryString("scid")
                If Not Request.QueryString("cat") Is Nothing Then
                    ViewState("cat") = Request.QueryString("cat")
                End If
                isiData(ViewState("scid"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()

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
                Dim txtScore As TextBox
                Dim txtLimit As TextBox
                Dim txtGood As TextBox
                Dim txtBad As TextBox
                For i As Integer = 1 To dtBin.Rows.Count
                    If Not IsDBNull(dtBin.Rows(i - 1).Item("base_upper")) And Not IsDBNull(dtBin.Rows(i - 1).Item("base_rate")) Then
                        txtLimit = FindControl("txtLimit" & i)
                        txtLimit.Text = IIf(CDbl(dtBin.Rows(i - 1).Item("base_upper")) = 0, "", dtBin.Rows(i - 1).Item("base_upper"))
                        txtScore = FindControl("txtScore" & i)
                        txtScore.Text = IIf(CInt(dtBin.Rows(i - 1).Item("base_count")) = 0, IIf(CDbl(dtBin.Rows(i - 1).Item("base_upper")) = 0, "", "0"), CInt(dtBin.Rows(i - 1).Item("base_count")))
                        txtGood = FindControl("txtGood" & i)
                        txtGood.Text = IIf(CInt(dtBin.Rows(i - 1).Item("base_good")) = 0, IIf(CDbl(dtBin.Rows(i - 1).Item("base_upper")) = 0, "", "0"), CInt(dtBin.Rows(i - 1).Item("base_good")))
                        txtBad = FindControl("txtBad" & i)
                        txtBad.Text = IIf(CInt(dtBin.Rows(i - 1).Item("base_bad")) = 0, IIf(CDbl(dtBin.Rows(i - 1).Item("base_upper")) = 0, "", "0"), CInt(dtBin.Rows(i - 1).Item("base_bad")))
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim oData As New AAS.Business.Entity.scorecard_char

        Dim limit As TextBox
        Dim score As TextBox
        Dim good As TextBox
        Dim bad As TextBox
        If liBodyLoadAction.Text = "" Then
            Dim ds As DataSet
            Dim dt As DataTable

            ds = oSc.getDetailOfScorecardBaseline(ViewState("scid"))
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                Try
                    For i As Integer = 1 To dt.Rows.Count
                        'limit = FindControl("txtLimit" & i)
                        oData.att_from(i - 1) = dt.Rows(i - 1).Item("base_upper")
                        'score = FindControl("txtScore" & i)
                        oData.att_to(i - 1) = dt.Rows(i - 1).Item("base_rate")
                        oData.baseline(i - 1) = dt.Rows(i - 1).Item("base_count")
                        oData.base_good(i - 1) = dt.Rows(i - 1).Item("base_good")
                        oData.base_bad(i - 1) = dt.Rows(i - 1).Item("base_bad")
                    Next
                    For index As Integer = dt.Rows.Count + 1 To 20
                        'limit = FindControl("txtLimit" & index)
                        oData.att_from(index - 1) = 0
                        'score = FindControl("txtScore" & index)
                        oData.att_to(index - 1) = 0
                        oData.baseline(index - 1) = 0
                        oData.base_good(index - 1) = 0
                        oData.base_bad(index - 1) = 0
                    Next
                    oSc.insertScorecardBaselineLog(oData, ViewState("scid"), "Before", usrLogin, ddlAction.SelectedValue)

                    For i As Integer = 1 To 20
                        oData.att_from(i - 1) = 0 'untuk input limit
                        oData.baseline(i - 1) = 0 'untuk input count
                        oData.base_good(i - 1) = 0 'untuk input good count
                        oData.base_bad(i - 1) = 0 'untuk input bad count
                        oData.att_to(i - 1) = 0
                    Next

                    For i As Integer = 1 To 20
                        limit = FindControl("txtLimit" & i)
                        oData.att_from(i - 1) = IIf(limit.Text = "", 0, limit.Text) 'untuk input limit
                        good = FindControl("txtGood" & i)
                        oData.base_good(i - 1) = IIf(good.Text = "", 0, good.Text) 'untuk input good count
                        bad = FindControl("txtBad" & i)
                        oData.base_bad(i - 1) = IIf(bad.Text = "", 0, bad.Text) 'untuk input bad count
                        score = FindControl("txtScore" & i)
                        If score.Text = "" Then
                            oData.baseline(i - 1) = 0
                        Else
                            oData.baseline(i - 1) = CDbl(good.Text) + CDbl(bad.Text)
                        End If

                        oData.att_to(i - 1) = 0
                    Next
                    oSc.insertScorecardBaseline(oData, ViewState("scid"), usrLogin, ddlAction.SelectedValue)
                    oSc.insertScorecardBaselineLog(oData, ViewState("scid"), "After", usrLogin, ddlAction.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Baseline Updated - SCORECARDCODE=" & ViewState("scid"))
                    objLog.insertLog("scorecard_baseline", logs)
                    Response.Redirect("scorecardChar.aspx?scid=" + ViewState("scid") + "&cat=" + ViewState("cat"))
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try
                    For i As Integer = 1 To 20
                        limit = FindControl("txtLimit" & i)
                        oData.att_from(i - 1) = IIf(limit.Text = "", 0, limit.Text) 'untuk input limit
                        good = FindControl("txtGood" & i)
                        oData.base_good(i - 1) = IIf(good.Text = "", 0, good.Text) 'untuk input good count
                        bad = FindControl("txtBad" & i)
                        oData.base_bad(i - 1) = IIf(bad.Text = "", 0, bad.Text) 'untuk input bad count
                        score = FindControl("txtScore" & i)
                        oData.baseline(i - 1) = IIf(score.Text = "", 0, CInt(good.Text) + CInt(bad.Text)) 'untuk input count
                        oData.att_to(i - 1) = 0
                    Next
                    oSc.insertScorecardBaseline(oData, ViewState("scid"), usrLogin, ddlAction.SelectedValue)
                    oSc.insertScorecardBaselineLog(oData, ViewState("scid"), "New", usrLogin, ddlAction.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Baseline Creation - SCORECARDCODE=" & ViewState("scid"))
                    objLog.insertLog("scorecard_baseline", logs)
                    Response.Redirect("scorecardChar.aspx?scid=" + ViewState("scid") + "&cat=" + ViewState("cat"))
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
                    If Not IsNumeric(txtSco.Text) Then
                        ctrsconum = ctrsconum + 1
                    End If
                    ctr = ctr + 1
                Else
                    lMsg = lMsg + "Please insert Baseline Rating.\n"
                    txtSco.Focus()
                End If
            End If
        Next
        If ctr = 0 Then
            lMsg = lMsg + "Please insert at least 1 Scorecard Baseline.\n"
        ElseIf ctrlimnum <> 0 Or ctrsconum <> 0 Then
            lMsg = lMsg + "Please check your Upper Limit or Rating.\n"
        Else
            lMsg = checkUpperLimit()
        End If

        If ddlAction.SelectedValue = "0" Then
            lMsg &= "Please select action.\n"
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
        Dim c_next As TextBox
        For i As Integer = 1 To 20
            c_upper = FindControl("txtLimit" & i)

            If i <> 20 Then
                c_next = FindControl("txtLimit" & i + 1)
                If c_next.Text <> "" And c_upper.Text = "" Then
                    lMsg = "Invalid Upper Limit. An Upper Limit can NOT be empty."
                End If
            End If

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
End Class