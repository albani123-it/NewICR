Public Class scorecardDet
    Inherits System.Web.UI.Page
    Dim oSc As New lScorecards
    Dim objLog As New lLog
    Dim prefix = "IIR"
    Dim iType = 1
    Dim mn As Boolean = False

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        Dim irCode As String
        irCode = oSc.getNextScorecardCode(prefix)

        If Not IsPostBack Then
            fillDdlCategory()
            fillDdlAction(mn)
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("scc_code")) And Not IsPostBack Then
            If Request.QueryString("scc_code") <> "-1" Then
                irCode = Request.QueryString("scc_code")
                isiData(irCode)
            End If
        End If
        If Not IsPostBack Then
            lblSccCode.Text = irCode
            scorecard_code.Value = irCode
        End If
        
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        btnSave.Enabled = False
        btnApprove.Enabled = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_new" Then
                    btnSave.Enabled = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_approve" Then
                    btnApprove.Enabled = True
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

    Sub fillDdlCategory()
        ddlSccCategory.Items.Insert(0, New ListItem("Select One", "0"))
        ddlSccCategory.Items.Insert(1, New ListItem("Borrower Rating", "Borrower Rating"))
        ddlSccCategory.Items.Insert(2, New ListItem("Facility Rating", "Facility Rating"))
        'ddlSccCategory.Items.Insert(3, New ListItem("Micro Rating", "Micro Rating"))
        ddlSccCategory.SelectedIndex = 0
    End Sub


    Sub fillDdlAction(ByVal delpar)
        Dim ds As DataSet
        ds = oSc.getListStatus(delpar)
        
        ddlAction.DataSource = ds
        ddlAction.DataTextField = "stu_name"
        ddlAction.DataValueField = "stu_id"
        ddlAction.DataBind()

    End Sub


    Sub isiData(ByVal code)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oSc.getDetailOfScorecard(code)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            ddlSccCategory.SelectedValue = dt.Rows(0).Item("scc_category")
            lblSccCode.Text = dt.Rows(0).Item("scc_code")
            txtSccName.Text = dt.Rows(0).Item("scc_name")
            txtSccDesc.Text = dt.Rows(0).Item("scc_description")
            txtGoodCond.Text = dt.Rows(0).Item("scc_good")
            txtBadCond.Text = dt.Rows(0).Item("scc_bad")
            txtAddPoint.Text = dt.Rows(0).Item("scc_additive_points")
            ddlAction.SelectedValue = dt.Rows(0).Item("scc_action")
            If dt.Rows(0).Item("scc_status") = 4 Then
                btnApprove.Enabled = True
            Else
                btnApprove.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim oScorecard As New AAS.Business.Entity.scorecard

        Dim status = 1
        If liBodyLoadAction.Text = "" Then
            oScorecard.code = lblSccCode.Text
            oScorecard.name = txtSccName.Text
            oScorecard.additive_points = txtAddPoint.Text
            oScorecard.description = txtSccDesc.Text
            oScorecard.good = txtGoodCond.Text
            oScorecard.bad = txtBadCond.Text
            oScorecard.status = status
            If Request.QueryString("scc_code") <> "-1" Then
                Try
                    oSc.insertUpdateScorecard(oScorecard, usrLogin, "UO", ddlSccCategory.SelectedValue, ddlAction.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Rating Model Updated - CODE=" & lblSccCode.Text & ";NAME=" & txtSccName.Text & ";CATEGORY=" & ddlSccCategory.SelectedValue & ";Action=" + ddlAction.SelectedValue)
                    objLog.insertLog("scorecard", logs)
                    liBodyLoadAction.Text = "alert('Rating Model Definition saved.');"
                    Response.Redirect("scorecardChar.aspx?scid=" + scorecard_code.Value + "&cat=" + ddlSccCategory.SelectedValue)
                    'Response.Redirect("scorecardBaseline.aspx?scid=" + scorecard_code.Value + "&cat=" + ddlSccCategory.SelectedValue)
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                End Try
            Else
                Try
                    oSc.insertUpdateScorecard(oScorecard, usrLogin, "IN", ddlSccCategory.SelectedValue, ddlAction.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Rating Model Creation - CODE=" & lblSccCode.Text & ";NAME=" & txtSccName.Text & ";CATEGORY=" & ddlSccCategory.SelectedValue & ";Action=" + ddlAction.SelectedValue)
                    objLog.insertLog("scorecard", logs)
                    liBodyLoadAction.Text = "alert('Rating Model Definition saved.');"
                    Response.Redirect("scorecardChar.aspx?scid=" + scorecard_code.Value + "&cat=" + ddlSccCategory.SelectedValue)
                    'Response.Redirect("scorecardBaseline.aspx?scid=" + scorecard_code.Value + "&cat=" + ddlSccCategory.SelectedValue)
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                End Try
            End If

        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If ddlSccCategory.SelectedValue = "0" Then
            lMsg &= "Please select the category.\n"
        End If
        If txtSccName.Text = "" Then
            lMsg &= "Please input scorecard name.\n"
        End If
        If txtAddPoint.Text = "" Then
            lMsg &= "Please input additive points.\n"
        ElseIf IsNumeric(txtAddPoint.Text) = False Then
            lMsg &= "Please input numeric value in additive points.\n"
        End If
        If ddlAction.SelectedValue = "0" Then
            lMsg &= "Please select the action. \n"
        End If

        Dim msg As String = ""
        Dim msg2 As String = ""
        If txtGoodCond.Text <> "" Then
            msg = oSc.IsQueryValidBoolean("select * from view_summary where " & txtGoodCond.Text)
        End If
        If txtBadCond.Text <> "" Then
            msg2 = oSc.IsQueryValidBoolean("select * from view_summary where " & txtBadCond.Text)
        End If

        If msg = "0" Then
            lMsg &= "Good condition is not valid"
        End If
        If msg2 = "0" Then
            lMsg &= "Bad condition is not valid"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub


    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oSc.getDetailOfScorecard(scorecard_code.Value)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                oSc.deleteAllScorecard(scorecard_code.Value, usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Rating Model Deleted - CODE=" & lblSccCode.Text & ";NAME=" & txtSccName.Text & ";CATEGORY=" & ddlSccCategory.SelectedValue)
                objLog.insertLog("scorecard", logs)
                liBodyLoadAction.Text = "alert('Data has successfully deleted.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            End If
            
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & ".');window.opener.location.href = window.opener.location.href;parent.window.close();"
        End Try
    End Sub
End Class