Public Class enSA
    Dim gFunction As New lGeneral
    Dim oAsset As New lAsset
    Dim rwa As Boolean = 0

    Function enSA()
        'Ambil semua asset SA yang akan dihitung
        Dim ds As DataSet
        ds = oAsset.getListOfAssetStandardized
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Rows.Count
                If dt.Rows(i).Item("ams_code") = 1 Then
                    'Exposure pada neraca (ON - Balance)

                ElseIf dt.Rows(i).Item("ams_code") = 2 Then
                    'Transaksi rekening administrative (Off-Balance)

                ElseIf dt.Rows(i).Item("ams_code") = 3 Then
                    'Counterparty credit risk

                Else
                    'Settlement Risk
                    If dt.Rows(i).Item("ams_exposure_class") = 17 Then
                        'Transaksi non DvP
                        rwa = 0
                    Else
                        'Transaksi DvP
                    End If
                End If
            Next
        End If

    End Function
End Class

