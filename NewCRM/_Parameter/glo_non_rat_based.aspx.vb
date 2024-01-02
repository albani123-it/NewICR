Public Class glo_non_rat_based
    Inherits System.Web.UI.Page
    Public dtParent As DataTable
    Public dtChild As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("gnrID") <> "" Then
            delete(Request.QueryString("gnrID"))
        End If
        GetDataListOfGlobalNonRating()
    End Sub

    Sub delete(ByVal id)
        With New lglobalSettings
            .deleteGlobalNonRatingBased(id)
        End With
        Response.Redirect("glo_non_rat_based.aspx")
    End Sub

    Private Sub GetDataListOfGlobalNonRating()
        With New lglobalSettings
            Try
                Dim ds As DataSet
                ds = .getListGloNonRatingBasedRiskWeight(0)
                dtParent = ds.Tables(0)
            Catch ex As Exception

            End Try
        End With
    End Sub

    Sub getChild(ByVal idParent)
        With New lglobalSettings
            Try
                Dim ds As DataSet
                ds = .getDetailNonRatingBasedRiskWeightDetail(idParent)
                dtChild = ds.Tables(0)
            Catch ex As Exception

            End Try
        End With
    End Sub


End Class