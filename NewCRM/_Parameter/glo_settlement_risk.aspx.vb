Imports Obout.Grid

Public Class glo_settlement_risk
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Sub BindGrid()
        With New lglobalSettings
            Dim ds As New DataTable
            ds = .getListGloSettlementRisk()
            gridGloSettlementRisk.DataSource = ds
            gridGloSettlementRisk.DataBind()
        End With
    End Sub

    Sub RebindGrid()
        BindGrid()
    End Sub


    Sub InsertRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim SettlementDate As String = e.Record("glsr_settlement_date")
        Dim Percentage As Double = e.Record("glsr_percentage")

        With New lglobalSettings
            .insertGlobalSettlementRisk(SettlementDate, Percentage)
        End With
    End Sub

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim ID As Double = e.Record("glsr_id")
        Dim SettlementDate As String = e.Record("glsr_settlement_date")
        Dim Percentage As Double = e.Record("glsr_percentage")

        With New lglobalSettings
            .updateGlobalSettlementRisk(ID, SettlementDate, Percentage)
        End With
    End Sub

    Sub DeleteRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        With New lglobalSettings
            .deleteGlobalSettlementRisk(e.Record("glsr_id"))
        End With
    End Sub

End Class