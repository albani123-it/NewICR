
Public Class enRun

#Region " Global Variable "
    Dim gFunction As New lGeneral
#End Region


    Sub testSimBaselEngine(ByVal userid)
        With New enSimAsset
            .enAsset(userid)
        End With

        Dim modId As Integer
        Dim sidId As Integer
        Dim sql As String
        Dim oGen As New lGeneral

        sql = "select * from simulation" & _
        vbCrLf & " inner join simulation_detail on sim_id = sid_sim_id " & _
        vbCrLf & " inner join asset_model on sid_mod_id = mod_id " & _
        vbCrLf & " WHERE sim_finish_date Is null "
        Dim dt As DataTable = oGen.exec_text(sql)
        If dt.Rows.Count > 0 Then
            For x As Integer = 0 To dt.Rows.Count - 1
                modId = dt.Rows(x).Item("mod_id")
                sidId = dt.Rows(x).Item("sid_id")
                If dt.Rows(x).Item("mod_approach") > 1 Then
                    'With New enSimScore
                    '    .enScore(modId, sidId)
                    'End With
                    With New enSimMaturity
                        .enMaturity(modId, sidId)
                    End With
                    With New enSimPool
                        .enPool(modId, sidId)
                    End With
                End If
                With New enSimResult
                    .enResult(modId, sidId)
                End With
            Next
        End If

        With New enSimMaster
            .enMaster()
        End With

        sql = "EXEC en_engine_date"
        gFunction.exec_sp(sql)
    End Sub


    Sub testBaselEngine(ByVal userid)
        With New enAsset
            .enAsset(userid)
        End With

        Dim modId As Integer
        Dim sql As String
        Dim oGen As New lGeneral

        sql = "select a.* from asset_model a" & _
        vbCrLf & " left join asset b on a.mod_ast_id = b.id" & _
        vbCrLf & " where a.mod_status = 1 And b.id Is Not null  and (mod_pd is not null or mod_approach = 1)"
        Dim dt As DataTable = oGen.exec_text(sql)
        If dt.Rows.Count > 0 Then
            For x As Integer = 0 To dt.Rows.Count - 1
                modId = dt.Rows(x).Item("mod_id")
                If dt.Rows(x).Item("mod_approach") > 1 Then
                    With New enScores
                        .enScore(modId)
                    End With
                    With New enMaturity
                        .enMaturity(modId)
                    End With
                    With New enPool
                        .enPool(modId)
                    End With
                End If
                With New enResult
                    .enResult(modId)
                End With
            Next
        End If

        With New enMaster
            .enMaster()
        End With

        With New enReset
            .enReset()
        End With

        sql = "EXEC en_engine_date"
        gFunction.exec_sp(sql)
    End Sub
End Class
