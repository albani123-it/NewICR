Imports Obout.Grid

Public Class glo_eligible_IRB
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
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_eligible" Then
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
            dt = .getListGloEligibleIRB()
            gridGloEligibleIRB.DataSource = dt
            gridGloEligibleIRB.DataBind()
        End With
    End Sub

    Sub RebindGrid()
        BindData()
    End Sub

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim ID As String = e.Record("ID")
        Dim CollateralClass As String = e.Record("collateral_class")
        Dim MinLGD As String = e.Record("min_lgd")
        Dim MinCollateralLevel As String = e.Record("min_collateral_level")
        Dim MaxCollateralLevel As String = e.Record("max_collateral_level")

        With New lglobalSettings
            '.updateGlobalEligibleIRB(ID, CollateralClass, MinLGD, MinCollateralLevel, MaxCollateralLevel, usrLogin)
            .updateGlobalEligibleIRB(ID, CollateralClass, MinLGD, MinCollateralLevel, MaxCollateralLevel)
            Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Eligible IRB Collateral Updated")
            objLog.insertLog("glo_irb_col", logs)
        End With
    End Sub
End Class