Imports Obout.Grid

Public Class glo_rat_based
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Sub RebindGrid()
        BindGrid()
    End Sub

    Sub BindGrid()
        With New lglobalSettings
            Dim dt As New DataTable
            dt = .getListGloRatingBasedRiskWeight()
            gridGloRatBased.DataSource = dt
            gridGloRatBased.DataBind()

        End With
    End Sub

    Sub InsertRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim AssetClass As String = e.Record("glr_asset_class")
        Dim RatingAgency As String = e.Record("glr_agency")
        Dim ExternalRating As String = e.Record("glr_external_rating")
        Dim RiskWeight As Double = e.Record("glr_rwa")

        With New lglobalSettings
            .InsertGlobalRatingBased(AssetClass, RatingAgency, ExternalRating, RiskWeight)
        End With
    End Sub

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim ID As Double = e.Record("glr_id")
        Dim AssetClass As String = e.Record("glr_asset_class")
        Dim RatingAgency As String = e.Record("glr_agency")
        Dim ExternalRating As String = e.Record("glr_external_rating")
        Dim RiskWeight As Double = e.Record("glr_rwa")

        With New lglobalSettings
            .UpdateGlobalRatingBased(ID, AssetClass, RatingAgency, ExternalRating, RiskWeight)
        End With
    End Sub

    Sub DeleteRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        With New lglobalSettings
            .deleteGlobalRatingBased(e.Record("glr_id"))
        End With
    End Sub

    Function fillRatingAgency()
        Dim htmlSelectMCM = ""
        Try
            With New lglobalSettings
                Dim PIset As New DataSet

                PIset = .getAgencyRating("A")

                Dim nilai As New DataTable
                nilai = PIset.Tables(0)
                htmlSelectMCM = htmlSelectMCM & "<option value=''>-- Select One --</option>"
                If nilai.Rows.Count > 0 Then
                    For i As Integer = 1 To nilai.Rows.Count()
                        htmlSelectMCM = htmlSelectMCM & "<option value='" & nilai.Rows(i - 1).Item("ddlvalue") & "'>" & nilai.Rows(i - 1).Item("ddlfield") & "</option>"
                    Next
                End If
            End With
        Catch ex As Exception
            htmlSelectMCM = ""
            htmlSelectMCM = htmlSelectMCM & "<option value=''>Data Not Found</option>"
        End Try

        Return htmlSelectMCM
    End Function

    Function fillAssetClass()
        Dim htmlSelectMCM = ""
        Try
            With New lglobalSettings
                Dim PIset As New DataSet

                PIset = .getAgencyRating("A")

                Dim nilai As New DataTable
                nilai = PIset.Tables(0)
                htmlSelectMCM = htmlSelectMCM & "<option value=''>-- Select One --</option>"
                If nilai.Rows.Count > 0 Then
                    For i As Integer = 1 To nilai.Rows.Count()
                        htmlSelectMCM = htmlSelectMCM & "<option value='" & nilai.Rows(i - 1).Item("ddlvalue") & "'>" & nilai.Rows(i - 1).Item("ddlfield") & "</option>"
                    Next
                End If
            End With
        Catch ex As Exception
            htmlSelectMCM = ""
            htmlSelectMCM = htmlSelectMCM & "<option value=''>Data Not Found</option>"
        End Try

        Return htmlSelectMCM
    End Function

    Function fillExternalRating()
        Dim htmlSelectMCM = ""
        Try
            With New lglobalSettings
                Dim PIset As New DataSet

                PIset = .getAgencyRating("R")

                Dim nilai As New DataTable
                nilai = PIset.Tables(0)
                htmlSelectMCM = htmlSelectMCM & "<option value=''>-- Select One --</option>"
                If nilai.Rows.Count > 0 Then
                    For i As Integer = 1 To nilai.Rows.Count()
                        htmlSelectMCM = htmlSelectMCM & "<option value='" & nilai.Rows(i - 1).Item("ddlvalue") & "'>" & nilai.Rows(i - 1).Item("ddlfield") & "</option>"
                    Next
                End If
            End With
        Catch ex As Exception
            htmlSelectMCM = ""
            htmlSelectMCM = htmlSelectMCM & "<option value=''>Data Not Found</option>"
        End Try

        Return htmlSelectMCM
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function getExternalRating(ByVal Agency As String) As String
        Dim ResponseData As String = ""
        Dim dt As New DataTable

        With New lglobalSettings
            dt = .getListExtRating(Agency)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count() - 1
                    ResponseData += "<option value='" & dt.Rows(i).Item("rating") & "'>" & dt.Rows(i).Item("rating") & "</option>"
                Next
            End If
        End With

        Return ResponseData
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function pickExternalRating(ByVal ID As String) As String
        Dim ResponseData As String = ""
        Dim dt As New DataTable

        With New lglobalSettings
            dt = .getDataExtRating(ID)

            If dt.Rows.Count > 0 Then
                ResponseData = dt.Rows(0).Item("glr_external_rating")
            End If
        End With

        Return ResponseData
    End Function

End Class