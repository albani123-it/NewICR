Public Class scorecard
    Inherits System.Web.UI.Page
    Dim oSc As New lScorecards
    Dim objLog As New lLog
    Protected AllowDelete As Boolean
    Protected AllowEdit As Boolean
    Protected AllowRating As Boolean
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
        AllowDelete = False
        AllowEdit = False
        AllowRating = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_edit" Then
                    AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_rating" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_approve" Then
                    AllowRating = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_scc_delete" Then
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

    Sub bindGrid()
        Dim ds As DataSet
        ds = oSc.getListOfScorecard

        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = oSc.getDetailOfScorecard(dataItem("scc_code"))
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        Dim dt As DataTable = oSc.checkUsedScorecard(dataItem("scc_code"), ds.Tables(0).Rows(0).Item("scc_category"))
        If dt.Rows.Count > 0 Then
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Rating Model can\'t be deleted, because still being in used.');window.location.href='" & urlBase & "_Parameter/scorecard.aspx';", True)
        Else
            Try
                oSc.deleteScorecard(dataItem("scc_code"), usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Scorecard Pending Deletion - SCORECARDCODE=" & dataItem("scc_code") & ";STATUS=Pending Deletion")
                objLog.insertLog("scorecard", logs)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been successfully updated.');window.location.href='" & urlBase & "_Parameter/scorecard.aspx';", True)

            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & "');window.location.href='" & urlBase & "_Parameter/scorecard.aspx';", True)
            End Try
        End If
        
    End Sub
End Class