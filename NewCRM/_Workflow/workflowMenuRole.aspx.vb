Public Class workflowMenuRole
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

        grid1.Visible = False

        If Not IsPostBack Then
            fillDdlSector()
            'fillDdlRole()
        End If

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

    Sub fillDdlSector()
        Dim ds As DataSet
        ds = oWk.getListOfSector
        ddlSector.Items.Clear()
        ddlSector.DataSource = ds
        ddlSector.DataTextField = "lst_sec_name"
        ddlSector.DataValueField = "lst_sec_id"
        ddlSector.DataBind()
    End Sub

    'Sub fillDdlMenu()
    '    Dim ds As DataSet

    '    ddlMenu.Items.Clear()
    '    ddlMenu.DataSource = ds
    '    ddlMenu.DataTextField = "lst_sec_name"
    '    ddlMenu.DataValueField = "lst_sec_id"
    '    ddlMenu.DataBind()
    'End Sub

    'Sub fillDdlRole()
    '    ddlRole.Items.Clear()
    '    ddlRole.Items.Add(New ListItem("Select One", "Select One"))
    '    ddlRole.Items.Add(New ListItem("Full Access", "FA"))
    '    ddlRole.Items.Add(New ListItem("Read Only", "RO"))
    '    ddlRole.Items.Add(New ListItem("Hidden", "HD"))
    'End Sub

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
        ddlProcess.Items.Clear()
        ddlProcess.DataSource = ds
        ddlProcess.DataTextField = "lwp_name"
        ddlProcess.DataValueField = "lwp_id"
        ddlProcess.DataBind()
    End Sub

    Sub isiData(ByVal id)
        'Dim ds As DataSet
        'Dim dt As DataTable
        'ds = oWk.getDetailOfWorkflowProcess(id)
        'dt = ds.Tables(0)
        
    End Sub

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        If ddlSector.SelectedValue = "0" Then
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Please select sector.');window.location.href='" & urlBase & "_Workflow/workflowMenuRole.aspx?wfid=" & wfid.Value & "';", True)
        Else
            If ddlProcess.SelectedValue <> "0" Or ddlProcess.SelectedValue <> "Select One" Then
                Dim ds As DataSet
                ds = oWk.getListOfMenuRoleBySector(wfid.Value, ddlProcess.SelectedValue, ddlSector.SelectedValue)
                grid1.DataSource = ds
                grid1.DataBind()
                grid1.Visible = True
            End If
        End If
        
    End Sub
End Class