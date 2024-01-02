Imports System.Data.SqlClient
Public Class enReset

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim mSql = ""
    Dim unCon = ""
    Dim spName = "en_reset"
    Dim rTable = "id_master_result"
    Dim uTable = "idx_master_asset_union"
    Dim resultfield = "scc_code_pd, score_pd, scc_code_lgd, score_lgd, scc_code_ead, score_ead, pd_rating, case when pd = 0 then '' else pd end pd, lgd_rating, lgd, ead_rating, ead, maturity, capital_charge, expected_loss, unexpected_loss, rwa, risk_premium, cat_name, mod_id, mod_name, hurdle_rate, ead_value, capital_requirement"
    Dim logSql = ""
    Dim aLogSql = ""
#End Region

    Dim gFunction As New lGeneral

    Function enReset()
        ''FOR IRBA ONLY
        'Dim dt As DataTable = gFunction.exec_text("select * from asset_model left join asset on id = mod_ast_id where mod_status = 1 and cat_rule > 0 and mod_approach > 1 and mod_pd is not null and mod_lgd is not null and mod_ead is not null and mod_maturity is not null")
        mSql = "select * from asset_model a " & _
            vbCrLf & " LEFT JOIN asset b ON b.id = a.mod_ast_id " & _
            vbCrLf & " WHERE " & _
            vbCrLf & " a.mod_status = 1 And b.id Is Not null  and (mod_pd is not null or mod_approach = 1) ORDER BY urut asc "
        Dim dt As DataTable = gFunction.exec_text(mSql)

        If dt.Rows.Count > 0 Then
            aSql += gFunction.returnLogScript(uTable, "START")
            aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & uTable & "' ) " & _
                vbCrLf & "DROP TABLE " & uTable & " "
            aSql += " select * into " & uTable & " from ( "
            For i As Integer = 0 To dt.Rows.Count - 1
                If i > 0 Then
                    aSql += " union "
                End If
                aSql += " select idx_master_asset_" & dt.Rows(i).Item("mod_id") & "_sc_result.*, " & _
                        vbCrLf & " '" & dt.Rows(i).Item("cat_name") & "' as cat_name, " & _
                        vbCrLf & " '" & dt.Rows(i).Item("mod_id") & "' as mod_id, " & _
                        vbCrLf & " '" & dt.Rows(i).Item("mod_name") & "' as mod_name " & _
                        vbCrLf & " from idx_master_asset_" & dt.Rows(i).Item("mod_id") & "_sc_result "

                sql += " TRUNCATE TABLE idx_master_asset_" & dt.Rows(i).Item("mod_ast_id") & " "
                If dt.Rows(i).Item("mod_approach") > 1 Then
                    sql += " TRUNCATE TABLE idx_master_asset_" & dt.Rows(i).Item("mod_id") & "_sc "
                End If
                sql += " TRUNCATE TABLE idx_master_asset_" & dt.Rows(i).Item("mod_id") & "_sc_result "
                enLog(dt.Rows(i).Item("mod_ast_id"), dt.Rows(i).Item("mod_id"))
            Next
            aSql += " )alias "
            aSql += gFunction.returnLogScript(uTable, "END")
            enGenerateSPLog()
        End If

        'aSql += gFunction.returnLogScript(rTable, "START")
        'aSql += " IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & rTable & "' ) " & _
        '        vbCrLf & "DROP TABLE " & rTable & " "
        'aSql += " SELECT a.*, " & resultfield & " INTO " & rTable & " FROM " & mTable & " a LEFT JOIN " & uTable & " b ON a.acc_no = b.acc_no"
        'aSql += gFunction.returnLogScript(rTable, "END")
        'aSql += sql

        aSql += " EXEC sp_cleanup_tmp "
        aSql += " EXEC en_scoring_log '','END' "
        'Return aSql
        gFunction.createSP(spName, aSql)
    End Function

    Function enLog(ByVal ast_id, ByVal mod_id)
        If Not logSql = "" Then
            logSql += " UNION "
        End If
        Dim mLogTable = "idx_master_asset_" & mod_id
        logSql += " select '" & mLogTable & "_sc_result' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_sc_maturity' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_sc_pool_ead' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_sc_pool_lgd' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_sc_pool_pd' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_sc' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_score_ead' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_score_lgd' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_score_pd' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select '" & mLogTable & "_score_tmpsc' as dTable " & _
            vbCrLf & " UNION " & _
            vbCrLf & " select 'idx_master_asset_" & ast_id & "' as dTable "
    End Function

    Function enGenerateSPLog()
        Dim spLogName = "log_en_scoring"
        aLogSql = "select def.*, a.*,  DATEDIFF(MI, prl_start, prl_end)m from ("
        aLogSql += logSql
        aLogSql += ") def " & _
            vbCrLf & " left join idx_scoring_log_detail a on def.dTable = a.prl_table " & _
            vbCrLf & " where prl_parent = @id" & _
            vbCrLf & " order by substring(def.dTable, 1, 20), prl_start asc"
        Dim xLogStr As String = " if exists ( Select name FROM sysobjects where name = '" & spLogName & "' ) DROP PROCEDURE dbo." & spLogName
        gFunction.exec_sp(xLogStr)
        Dim str As String = " CREATE Procedure " & spLogName & " @id int  AS " & aLogSql & " "
        gFunction.exec_sp(str)
    End Function
End Class
