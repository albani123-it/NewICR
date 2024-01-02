Public Class workflowProcessMgmt
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
            End If
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "window.location.href='" & urlBase & "_Workflow/workflowMgmt.aspx';", True)
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("wfpid")) Then
            wfpid.Value = Request.QueryString("wfpid")
            If Not IsPostBack Then
                isiData(Request.QueryString("wfpid"))
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

    Sub bindGrid(ByVal id)
        Dim ds As DataSet
        ds = oWk.getListOfWorkflowProcess(id)
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oWk.getDetailOfWorkflowProcess(id)
        dt = ds.Tables(0)
        txtID.Text = oGen.CheckNullString(dt.Rows(0).Item("lwp_id"))
        txtName.Text = oGen.CheckNullString(dt.Rows(0).Item("lwp_name"))
        txtID.ReadOnly = True
        txtID.CssClass = "inpTxtDisabled"
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If wfid.Value <> "" And wfpid.Value <> "" Then
                Try
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oWk.getDetailOfWorkflowProcess(wfpid.Value)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        oWk.updateWorkflowProcess(Request, wfid.Value, wfpid.Value, usrLogin)
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been updated.');window.location.href='" & urlBase & "_Workflow/workflowProcessMgmt.aspx?wfid=" & wfid.Value & "';", True)
                    Else
                        oWk.insertWorkflowProcess(Request, wfid.Value, usrLogin)
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been saved.');window.location.href='" & urlBase & "_Workflow/workflowProcessMgmt.aspx?wfid=" & wfid.Value & "';", True)
                    End If
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Workflow/workflowProcessMgmt.aspx?wfid=" & wfid.Value & "';", True)
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""

        If txtID.Text = "" Then
            lMsg &= "Please input process id.\n"
        End If

        If txtName.Text = "" Then
            lMsg &= "Please input process name.\n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        cs.RegisterStartupScript(Page.[GetType](), "alert", "window.location.href='" & urlBase & "_Workflow/workflowProcessMgmt.aspx?wfid=" & wfid.Value & "';", True)
    End Sub
End Class