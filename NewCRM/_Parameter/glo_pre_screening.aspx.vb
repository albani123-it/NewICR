Imports Obout.Grid

Public Class glo_pre_screening
    Inherits System.Web.UI.Page
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_prs" Then
                    tf = True
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

    Sub BindData()
        With New lglobalSettings
            Dim dt As DataTable
            dt = .getListGloPreScreening()
            grid1.DataSource = dt
            grid1.DataBind()
        End With
    End Sub

    Sub RebindGrid()
        BindData()
    End Sub

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim ID As String = e.Record("ID")
        Dim brwType As String = e.Record("brw_type")
        Dim MinScore As String = e.Record("min_score")

        With New lglobalSettings
            '.updateGlobalPreScreening(ID, brwType, MinScore, usrLogin)
            .updateGlobalPreScreening(ID, brwType, MinScore)
            Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Pre-Screening Updated")
            objLog.insertLog("glo_pre_screening", logs)
        End With
    End Sub
End Class