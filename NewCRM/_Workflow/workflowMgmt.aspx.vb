Public Class workflowMgmt
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

        If Not IsPostBack Then
            bindGrid()
        End If


        If Not String.IsNullOrEmpty(Request.QueryString("wfid")) Then
            wfid.Value = Request.QueryString("wfid")
            If Not IsPostBack Then
                isiData(Request.QueryString("wfid"))
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

    Sub bindGrid()
        Dim ds As DataSet
        ds = oWk.getListOfWorkflow
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oWk.getDetailOfWorkflow(id)
        dt = ds.Tables(0)
        txtWorkflowName.Value = oGen.CheckNullString(dt.Rows(0).Item("lsw_name"))
        txtWorkflowDesc.Value = oGen.CheckNullString(dt.Rows(0).Item("lsw_desc"))
        If oGen.CheckNullString(dt.Rows(0).Item("lsw_is_Active")) <> "" Then
            If dt.Rows(0).Item("lsw_is_Active") = True Then
                chkActive.Checked = True
            Else
                chkActive.Checked = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If wfid.Value <> "" Then
                Try
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oWk.getDetailOfWorkflow(wfid.Value)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        oWk.updateWorkflow(Request, wfid.Value, usrLogin)
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been updated.');window.location.href='" & urlBase & "_Workflow/workflowMgmt.aspx';", True)
                    Else
                        oWk.insertWorkflow(Request, usrLogin)
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been saved.');window.location.href='" & urlBase & "_Workflow/workflowMgmt.aspx';", True)
                    End If
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Workflow/workflowMgmt.aspx';", True)
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""

        If txtWorkflowName.Value = "" Then
            lMsg &= "Please input workflow name.\n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        cs.RegisterStartupScript(Page.[GetType](), "alert", "window.location.href='" & urlBase & "_Workflow/workflowMgmt.aspx';", True)
    End Sub
End Class