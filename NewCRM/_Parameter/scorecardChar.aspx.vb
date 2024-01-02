Public Class scorecardChar
    Inherits System.Web.UI.Page
    Dim oSc As New lScorecards
    Dim objLog As New lLog
    Protected AllowDelete As Boolean
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()
        ViewState("scid") = Request.QueryString("scid")
        ViewState("category") = Request.QueryString("cat")
        If Not IsPostBack Then
            bindGrid(ViewState("scid"))
            scid.Value = ViewState("scid")
            sccat.Value = ViewState("category")

        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        btnNew.Enabled = False
        grid1.Columns(3).AllowEdit = False
        AllowDelete = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_new" Then
                    btnNew.Enabled = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_approve" Then
                    grid1.Columns(3).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_sch_delete" Then
                    AllowDelete = True
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

    Sub bindGrid(ByVal id)
        Dim ds As DataSet
        ds = oSc.getListScorecardChar(id)
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = oSc.getDetailScorecardChar(dataItem("sch_id"))
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        Try
            oSc.deleteScorecardChar(dataItem("sch_id"), usrLogin)
            
            Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Characteristic Pending Deletion - SCORECARDCODE=" & ViewState("scid") & ";STATUS=Pending Deletion")
            objLog.insertLog("scorecard_char", logs)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Scorecard Characteristic on pending deletion.');window.location.href='" & urlBase & "_Parameter/scorecardChar.aspx?scid=" & ViewState("scid") & "&cat=" & ViewState("category") & "';", True)

        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "\'")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Parameter/scorecardChar.aspx?scid=" & ViewState("scid") & "&cat=" & ViewState("category") & "';", True)
        End Try


    End Sub

End Class