Public Class borrowerMgmtDet
    Inherits System.Web.UI.Page
    Dim oMgmt As New lMgmt
    Dim objLog As New lLog
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        Dim borrower_id As String

        If Not IsPostBack Then
            fillBrwScc()
            fillSector()
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("brw_id")) Then
            borrower_id = Request.QueryString("brw_id")
            If Request.QueryString("brw_id") <> -1 And Not IsPostBack Then
                isiData(borrower_id)
            End If
        End If
        
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_brw_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_brw_mgt_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_brw_mgt_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_brw_mgt_view" Then
                    If edit = False Then
                        txtBrwType.Enabled = False
                        ddlBrwScc.Enabled = False
                        txtCutApp.Enabled = False
                        txtCutRej.Enabled = False
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

    Sub fillSector()
        Dim ds As DataSet
        ds = oMgmt.getBorrowerSector

        ddlSector.DataSource = ds
        ddlSector.DataValueField = "lst_sec_id"
        ddlSector.DataTextField = "lst_sec_name"
        ddlSector.DataBind()
    End Sub

    Sub fillBrwScc()
        Dim ds As DataSet
        ds = oMgmt.getBorrowerScc

        ddlBrwScc.DataSource = ds
        ddlBrwScc.DataValueField = "scc_code"
        ddlBrwScc.DataTextField = "scc_name"
        ddlBrwScc.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oMgmt.getDetailBorrower(id)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            brw_id.Value = dt.Rows(0).Item("brw_id")
            txtBrwType.Text = dt.Rows(0).Item("brw_type")
            ddlSector.SelectedValue = dt.Rows(0).Item("brw_sec")
            ddlBrwScc.SelectedValue = dt.Rows(0).Item("brw_scc")
            txtCutApp.Text = dt.Rows(0).Item("brw_cut_approved")
            txtCutRej.Text = dt.Rows(0).Item("brw_cut_rejected")
            If dt.Rows(0).Item("brw_is_active") = True Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CekInput()
        Dim stat As String
        Dim oCe As New enRating

        If liBodyLoadAction.Text = "" Then
            If Request.QueryString("brw_id") <> -1 Then
                Try
                    oMgmt.editBorrower(Request, usrLogin, brw_id.Value)
                    If chkStatus.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Borrower Management Updated - TYPE=" + Request.Form("txtBrwType") + ";SCORECARD=" + Request.Form("ddlBrwScc") + ";STATUS=" + stat)
                    objLog.insertLog("borrowerMgmt", logs)
                    oCe.Borrower_Ctl()
                    liBodyLoadAction.Text = "alert('Borrower Management updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try
                    oMgmt.insertBorrower(Request, usrLogin)
                    If chkStatus.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Borrower Management Creation - TYPE=" + Request.Form("txtBrwType") + ";SCORECARD=" + Request.Form("ddlBrwScc") + ";STATUS=" + stat)
                    objLog.insertLog("borrowerMgmt", logs)
                    oCe.Borrower_Ctl()
                    liBodyLoadAction.Text = "alert('Borrower Management saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            End If
        End If
    End Sub

    Sub CekInput()
        Dim lMsg As String = ""
        If txtBrwType.Text = "" Then
            lMsg &= "Please input borrower type. \n"
        End If
        If ddlSector.SelectedIndex = 0 Then
            lMsg &= "Please select borrower sector. \n"
        End If
        If ddlBrwScc.SelectedIndex = 0 Then
            lMsg &= "Please select borrower scorecard. \n"
        End If
        If txtCutApp.Text = "Please input cut off approved. \n" Then
            lMsg &= ""
        End If
        If txtCutRej.Text = "" Then
            lMsg &= "Please input cut off rejected. \n"
        End If
        If txtCutApp.Text <> "" And txtCutRej.Text <> "" Then
            If CDbl(txtCutRej.Text) > CDbl(txtCutApp.Text) Then
                lMsg &= "Cut Off (Score) Approve must be larger than Cut Off (Score) Reject"
            ElseIf CDbl(txtCutRej.Text) = CDbl(txtCutApp.Text) Then
                lMsg &= "Cut Off (Score) Approve can not be equal with Cut Off (Score) Reject"
            End If

            If lMsg <> "" Then
                liBodyLoadAction.Text = "alert('" & lMsg & "');"
            End If
        End If
    End Sub
End Class