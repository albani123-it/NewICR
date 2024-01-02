Public Class simulation
    Inherits System.Web.UI.Page
    Dim oSim As New lSimulation
    Protected allowDelete As Boolean = False
    Protected allowEdit As Boolean = False
    Protected viewResult As Boolean = False
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()
        If Not IsPostBack Then
            bindGrid()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        grid1.AllowAddingRecords = False
        allowDelete = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_simulation" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_sim_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_sim_edit" Then
                    allowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_sim_result" Then
                    viewResult = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_sim_delete" Then
                    allowDelete = True
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

    Sub bindGrid()
        Dim ds As DataSet
        ds = oSim.getDataSimulation
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)
        Dim sim_id As Integer = dataItem("sim_id")
        'Response.Write(sim_id)
        oSim.deleteSimulation(sim_id, usrLogin)
        Dim objLog As New lLog
        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Simulation Deletion - SIMULATION NAME=" + dataItem("sim_name") + ";Description=" + dataItem("sim_desc") + ";")
        objLog.insertLog("simulation", logs)
        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been deleted successfully.');window.location.href='" & urlBase & "_Basel/Simulation.aspx';", True)
    End Sub

    Private Sub btnRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRun.Click
        Try
            oSim.runSimulationEngine()
            liBodyLoadAction.Text = "alert('Simulation Engine has been successfully worked');"
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        End Try
    End Sub

    Private Sub btnEngine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEngine.Click
        Try
            Dim Obe As New enRun
            Obe.testSimBaselEngine(usrLogin)
            liBodyLoadAction.Text = "alert('Batch scripts generated successfully');"
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" + ex.Message().Replace("'", "\'").Replace(vbCrLf, "\n") + "');"
        End Try
    End Sub
End Class