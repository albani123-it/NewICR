Public Class workflowProcessDet
    Inherits System.Web.UI.Page
    Dim oWk As New lWorkflow
    Dim oGen As New lGeneral
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        cekUserAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")



        If Not String.IsNullOrEmpty(Request.QueryString("wfid")) Then
            wfid.Value = Request.QueryString("wfid")
            If Not IsPostBack Then
                bindGrid(Request.QueryString("wfid"))
                fillDdlWorkflow(Request.QueryString("wfid"))
                fillDdlWorkflowProcess(Request.QueryString("wfid"))
            End If
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "window.location.href='" & urlBase & "_Workflow/workflowMgmt.aspx';", True)
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("wfpdid")) Then
            wfpdid.Value = Request.QueryString("wfpdid")
            If Not IsPostBack Then
                isiData(Request.QueryString("wfpdid"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        'Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            'For i As Integer = 0 To dtUser.Rows.Count - 1

            'Next
            'If Not tf Then
            '    Session.Abandon()
            '    Response.Redirect("../Login.aspx")
            'End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub fillDdlWorkflow(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oWk.getListOfWorkflow()
        dt = ds.Tables(0)
        ddlWorkflow.Items.Clear()
        ddlWorkflow.DataSource = dt
        ddlWorkflow.DataTextField = "lsw_name"
        ddlWorkflow.DataValueField = "lsw_id"
        ddlWorkflow.SelectedValue = id
        ddlWorkflow.DataBind()
        ddlWorkflow.Enabled = False
    End Sub

    Sub fillDdlWorkflowProcess(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oWk.getListOfWorkflowProcess(id)
        dt = ds.Tables(0)
        ddlProcess.Items.Clear()
        ddlProcess.DataSource = dt
        ddlProcess.DataTextField = "lwp_name"
        ddlProcess.DataValueField = "lwp_id"
        ddlProcess.DataBind()

        ddlNextAction.Items.Clear()
        ddlNextAction.DataSource = dt
        ddlNextAction.DataTextField = "lwp_name"
        ddlNextAction.DataValueField = "lwp_id"
        ddlNextAction.DataBind()
    End Sub

    Sub bindGrid(ByVal id)
        Dim ds As DataSet
        ds = oWk.getListOfWorkflowProcessDetail(id)
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oWk.getDetailOfWorkflowProcessDetail(id)
        dt = ds.Tables(0)
        txtActionName.Text = oGen.CheckNullString(dt.Rows(0).Item("lwd_action"))
        txtVar.Text = oGen.CheckNullString(dt.Rows(0).Item("lwd_var"))
        txtValue.Text = oGen.CheckNullString(dt.Rows(0).Item("lwd_value"))
        ddlProcess.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lwd_lwp_id"))
        ddlNextAction.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lwd_next"))
        If oGen.CheckNullString(dt.Rows(0).Item("lwd_with_cond")) <> "" Then
            If dt.Rows(0).Item("lwd_with_cond") = True Then
                rdCondition.SelectedValue = "1"
                txtVar.ReadOnly = True
                txtVar.CssClass = "inpTxt"
                ddlCond.Enabled = True
                ddlCond.CssClass = "inpDdl"
                txtValue.ReadOnly = True
                txtValue.CssClass = "inpTxt"
            Else
                rdCondition.SelectedValue = "0"
                txtVar.ReadOnly = True
                txtVar.CssClass = "inpTxtDisabled"
                ddlCond.Enabled = False
                ddlCond.CssClass = "inpDdlDisabled"
                txtValue.ReadOnly = True
                txtValue.CssClass = "inpTxtDisabled"
            End If
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        cs.RegisterStartupScript(Page.[GetType](), "alert", "window.location.href='" & urlBase & "_Workflow/workflowProcessDet.aspx?wfid=" & wfid.Value & "';", True)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If wfid.Value <> "" And wfpdid.Value <> "" Then
                Try
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oWk.getDetailOfWorkflowProcessDetail(wfpdid.Value)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        oWk.updateWorkflowProcessDetail(Request, wfid.Value, wfpdid.Value, usrLogin)
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been updated.');window.location.href='" & urlBase & "_Workflow/workflowProcessDet.aspx?wfid=" & wfid.Value & "';", True)
                    Else
                        oWk.insertWorkflowProcessDetail(Request, wfid.Value, usrLogin)
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been saved.');window.location.href='" & urlBase & "_Workflow/workflowProcessDet.aspx?wfid=" & wfid.Value & "';", True)
                    End If
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Workflow/workflowProcessDet.aspx?wfid=" & wfid.Value & "';", True)
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""

        If ddlProcess.SelectedValue = "Select One" Then
            lMsg &= "Please select process.\n"
        End If

        If ddlNextAction.Text = "Select One" Then
            lMsg &= "Please select next action.\n"
        End If

        If txtActionName.Text = "" Then
            lMsg &= "Please input action name.\n"
        End If

        If rdCondition.SelectedValue = "1" Then
            If txtVar.Text = "" Then
                lMsg &= "Please input variable.\n"
            End If
            If ddlCond.SelectedValue = "Select One" Then
                lMsg &= "Please select operator.\n"
            End If
            If txtValue.Text = "" Then
                lMsg &= "Please input value.\n"
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class