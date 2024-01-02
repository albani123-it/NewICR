Imports Obout.Grid

Public Class glo_counter_party_CR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Sub BindGrid()
        With New lglobalSettings
            Dim ds As New DataTable
            ds = .getListGloCounterpartyCreditRisk()
            gridGloCounterPartyCreditRisk.DataSource = ds
            gridGloCounterPartyCreditRisk.DataBind()
        End With
    End Sub

    Sub RebindGrid()
        BindGrid()
    End Sub


    Sub InsertRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim ResidualMaturity As Double = e.Record("gccr_residual_maturity")
        Dim InterestRate As Double = e.Record("gccr_interest_rate")
        Dim ExchangeRateAndGold As Double = e.Record("gccr_exchange_rate_and_gold")
        Dim Stock As Double = e.Record("gccr_stock")
        Dim MetalBesideGold As Double = e.Record("gccr_metal_beside_gold")
        Dim Other As Double = e.Record("gccr_other")

        With New lglobalSettings
            .InsertGlobalCounterParty(ResidualMaturity, InterestRate, ExchangeRateAndGold, Stock, MetalBesideGold, Other)
        End With
    End Sub

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim ID As Double = e.Record("gccr_id")
        Dim ResidualMaturity As Double = e.Record("gccr_residual_maturity")
        Dim InterestRate As Double = e.Record("gccr_interest_rate")
        Dim ExchangeRateAndGold As Double = e.Record("gccr_exchange_rate_and_gold")
        Dim Stock As Double = e.Record("gccr_stock")
        Dim MetalBesideGold As Double = e.Record("gccr_metal_beside_gold")
        Dim Other As Double = e.Record("gccr_other")

        With New lglobalSettings
            .updateGlobalCounterParty(ID, ResidualMaturity, InterestRate, ExchangeRateAndGold, Stock, MetalBesideGold, Other)
        End With
    End Sub

    Sub DeleteRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        With New lglobalSettings
            .deleteGlobalCounterParty(e.Record("gccr_id"))
        End With
    End Sub
End Class