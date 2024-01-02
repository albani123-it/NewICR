Public Class scorecardCharDet
    Inherits System.Web.UI.Page
    Dim oGen As New lGeneral
    Dim oSc As New lScorecards
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Not IsPostBack Then
            ViewState("scid") = Request.QueryString("scid")
            ViewState("schid") = Request.QueryString("schid")
            ViewState("cat") = Request.QueryString("cat")
            If ViewState("schid") > 0 Then
                FillListField(ViewState("cat"), ViewState("scid"), ViewState("schid"))
                isiData()
            Else
                FillListField(ViewState("cat"), ViewState("scid"), 0)
                trStatus.Visible = False
            End If
        End If
        If ViewState("schid") = -1 Then
            btnApp.Enabled = False
        End If
        setAttributeField(oGen.isDBTypeNumeric(CInt(ddlSCHField.SelectedItem.Value.Split("|")(1))), ddlSCHField.SelectedItem.Value.Split("|")(0))
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        btnSave.Enabled = False
        btnApp.Enabled = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_new" Then
                    btnSave.Enabled = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_approve" Then
                    btnApp.Enabled = True
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

    Sub FillListField(ByVal cat, ByVal scid, ByVal schid)
        Dim ds As DataSet

        ds = oSc.getScorecardCharDetailField(cat, scid, schid)
        
        ddlSCHField.DataSource = ds
        ddlSCHField.DataTextField = "name"
        ddlSCHField.DataValueField = "value"
        ddlSCHField.DataBind()

    End Sub

    Sub isiData()
        ViewState("status") = False
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oSc.getDetailScorecardChar(ViewState("schid"))
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            idHidden.Value = CInt(dt.Rows(0).Item("sch_id"))
            ddlSCHField.SelectedValue = dt.Rows(0).Item("sch_field")
            txtDefaultScore.Text = CInt(dt.Rows(0).Item("sch_default_score"))
            txtKoefisien.Text = oGen.intFormat(dt.Rows(0).Item("sch_koefisien_score"))
            lblStatus.Text = dt.Rows(0).Item("sch_status")
            Dim txtLimit As TextBox
            Dim txtScore As TextBox
            Dim txtCount As TextBox
            Dim txtGood As TextBox
            Dim txtBad As TextBox
            'Dim txtCount As TextBox
            For i As Integer = 1 To 20
                If Not IsDBNull(dt.Rows(0).Item("sch_limit" & i)) And Not IsDBNull(dt.Rows(0).Item("sch_score" & i)) And Not IsDBNull(dt.Rows(0).Item("sch_count" & i)) Then
                    txtLimit = FindControl("txtLimit" & i)
                    txtLimit.Text = dt.Rows(0).Item("sch_limit" & i)
                    txtScore = FindControl("txtScore" & i)
                    txtScore.Text = dt.Rows(0).Item("sch_score" & i)
                    txtCount = FindControl("txtCount" & i)
                    txtCount.Text = dt.Rows(0).Item("sch_count" & i)
                    txtGood = FindControl("txtGood" & i)
                    txtGood.Text = dt.Rows(0).Item("sch_good" & i)
                    txtBad = FindControl("txtBad" & i)
                    txtBad.Text = dt.Rows(0).Item("sch_bad" & i)
                End If
            Next
            If dt.Rows(0).Item("sch_inits") > 1 Then
                ViewState("status") = dt.Rows(0).Item("sch_inits")
                btnApp.Enabled = True
            Else
                btnApp.Enabled = False
            End If
        End If
    End Sub

    Sub setAttributeField(ByVal parIsNumber As Boolean, ByVal fld As String)
        valField.Value = fld
        If Not parIsNumber Then
            ViewState("parse") = "string"
            Dim txtLimit As TextBox
            For i As Integer = 1 To 20
                txtLimit = FindControl("txtLimit" & i)
                txtLimit.Text = txtLimit.Text.Trim
                txtLimit.MaxLength = 255
            Next
            lblAttr.Text = "Values (Alt + f to browse)"
            numField.Value = "inte"
        Else
            ViewState("parse") = "number"
            Dim txtLimit As TextBox
            For i As Integer = 1 To 20
                txtLimit = FindControl("txtLimit" & i)
                txtLimit.MaxLength = 255
                'txtLimit.Text = Left(txtLimit.Text, 12)
            Next
            lblAttr.Text = "Upper Limit"
            numField.Value = "nume"
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim oData As New AAS.Business.Entity.scorecard_char
        If liBodyLoadAction.Text = "" Then
            oData.schid = ViewState("schid")
            oData.sc_code = ViewState("scid")
            oData.field = ddlSCHField.SelectedItem.Value.Split("|")(0)
            oData.field_type = ddlSCHField.SelectedItem.Value.Split("|")(1)
            oData.default_score = txtDefaultScore.Text
            oData.koefisien_score = oGen.intFormat(txtKoefisien.Text)
            Dim limit As TextBox
            Dim score As TextBox
            Dim count As TextBox
            Dim good As TextBox
            Dim bad As TextBox

            For i As Integer = 1 To 20
                limit = FindControl("txtLimit" & i)
                oData.att_from(i - 1) = limit.Text 'untuk input limit
                score = FindControl("txtScore" & i)
                oData.att_to(i - 1) = score.Text 'untuk input score
                count = FindControl("txtCount" & i)
                oData.baseline(i - 1) = count.Text 'untuk input count

                good = FindControl("txtGood" & i)
                oData.base_char_good(i - 1) = good.Text 'untuk input good count
                bad = FindControl("txtBad" & i)
                oData.base_char_bad(i - 1) = bad.Text 'untuk input bad count
            Next

            If ViewState("schid") <> -1 Then
                Try
                    oSc.updateScorecardChar(oData, "U", usrLogin)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Characteristic Updated - CATEGORY=" & ViewState("cat") & ";SCORECARDCODE=" & ViewState("scid") & ";Field=" & ddlSCHField.SelectedItem.Text)
                    objLog.insertLog("scorecard_char", logs)
                    liBodyLoadAction.Text = "alert('Scorecard Characteristic updated.');window.opener.location.href = window.opener.location.href; parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try
                    oSc.insertScorecardChar(oData, "N", usrLogin)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Characteristic Creation - CATEGORY=" & ViewState("cat") & ";SCORECARDCODE=" & ViewState("scid") & ";Field=" & ddlSCHField.SelectedItem.Text)
                    objLog.insertLog("scorecard_char", logs)
                    liBodyLoadAction.Text = "alert('Scorecard Characteristic saved.');window.opener.location.href = window.opener.location.href; parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If ddlSCHField.SelectedIndex < 0 Then
            lMsg &= "Please select the field.\n"
        End If
        If txtDefaultScore.Text = "" Then
            lMsg &= "Please input default score.\n"
        ElseIf IsNumeric(txtDefaultScore.Text) = False Then
            lMsg &= "Default score must be numeric.\n"
        End If
        If txtKoefisien.Text = "" Then
            lMsg &= "Please input koefisien.\n"
        ElseIf IsNumeric(txtKoefisien.Text) = False Then
            lMsg &= "Koefisien score must be numeric.\n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        Else
            lMsg = cekUpperLimit()
            If lMsg <> "" Then
                liBodyLoadAction.Text = "alert('" & lMsg & "');"
            End If
        End If
    End Sub

    Function cekUpperLimit()
        Dim lMsg As String = ""
        Dim c_val As Decimal = 0
        Dim c_last As Decimal = 0
        Dim c_upper As TextBox
        If (oGen.isDBTypeNumeric(CDbl(ddlSCHField.SelectedItem.Value.Split("|")(1)))) Then
            For i As Integer = 1 To 20
                c_upper = FindControl("txtLimit" & i)
                If Not c_upper.Text = "" Then
                    c_last = c_upper.Text
                End If
                Dim tempVal = IIf(c_last < 0, c_last, c_last)
                If Not c_upper.Text = "" And i > 1 Then
                    If Not (c_val < tempVal Or tempVal = 0) Then
                        lMsg = "Invalid Upper Limit. An Upper Limit can NOT be less than or equal than the previous Upper Limit."
                    End If
                Else
                    c_val = tempVal
                End If
            Next
        End If
        Return lMsg
    End Function

    Private Sub btnApp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApp.Click
        Try
            oSc.approveScorecardChar(ViewState("schid"), ViewState("status"), usrLogin)
            If ViewState("status") = 3 Then
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Characteristic Deletion Approved - CATEGORY=" & ViewState("cat") & ";SCORECARDCODE=" & ViewState("scid") & ";Field=" & ddlSCHField.SelectedItem.Text)
                objLog.insertLog("scorecard_char", logs)
                liBodyLoadAction.Text = "alert('Scorecard Characteristic deletion approved.');window.opener.location.href = window.opener.location.href; parent.window.close();"
            Else
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Characteristic Approved - CATEGORY=" & ViewState("cat") & ";SCORECARDCODE=" & ViewState("scid") & ";Field=" & ddlSCHField.SelectedItem.Text)
                objLog.insertLog("scorecard_char", logs)
                liBodyLoadAction.Text = "alert('Scorecard Characteristic approved.');window.opener.location.href = window.opener.location.href; parent.window.close();"
            End If
            
        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "\'")
            liBodyLoadAction.Text = "alert('" & msg & "');"
        End Try
    End Sub
End Class