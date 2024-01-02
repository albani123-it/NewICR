Public Class enRating
    Dim oBo As New enBorrower
    Dim oFo As New enFacility
    Dim oRo As New enRetail

    Public Function Borrower_Ctl()
        Return (oBo.rebuildEngine())
    End Function

    Public Function Facility_Ctl()
        Return (oFo.rebuildEngine())
    End Function

    Public Function Retail_Ctl()
        Return (oRo.rebuildEngine())
    End Function

    Public Function NR_Recommendation()
        Return (oFo.rebuildRecommendation())
        'Return (oFo.recommandation(prdCode))
    End Function
End Class
