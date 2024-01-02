Imports System.Data.SqlClient

Public Class enMaster

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim unCon = ""
    Dim spName = "en_score_"
#End Region
    Dim gFunction As New lGeneral

    Function enMaster()
        'buat urutan asset by sub-category
        ''khusus IRBA
        sql = "select * from asset_model" & _
            vbCrLf & " LEFT JOIN asset ON asset.id = mod_ast_id " & _
            vbCrLf & " WHERE " & _
            vbCrLf & " mod_status = 1 and mod_approach > 1 and " & _
            vbCrLf & " mod_pd is not null and mod_lgd is not null and " & _
            vbCrLf & " mod_ead is not null and mod_maturity is not null ORDER BY urut asc "

        ''include SA
        'sql = "select a.* from asset_model a " & _
        '    vbCrLf & " LEFT JOIN asset b ON b.id = a.mod_ast_id " & _
        '    vbCrLf & " WHERE " & _
        '    vbCrLf & " a.mod_status = 1 And b.id Is Not null  and (mod_pd is not null or mod_approach = 1) ORDER BY urut asc "
        Dim dt As DataTable = gFunction.exec_text(sql)
        If dt.Rows.Count > 0 Then
            Dim rModel = dt.Rows
            For x As Integer = 0 To rModel.Count - 1
                spName = "en_score_" & rModel(x).Item("mod_id")
                If rModel(x).Item("mod_approach") > 1 Then
                    aSql += " EXEC " & spName & "_sc " & _
                        vbCrLf & " EXEC " & spName & "_result "
                Else
                    aSql += " EXEC " & spName & "_result "
                End If
            Next
            gFunction.createSP("en_calculate", aSql)
        End If
    End Function


End Class
