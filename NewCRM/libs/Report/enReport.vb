Imports System.Data.SqlClient
Public Class enReport

#Region " Global Variable "
    Dim spName = "ce_baseline_"
#End Region
    Dim oGen As New lGeneral
    Dim CEG As New enRatingCalculation

    Public Function createPopGBKS()
        Dim year As String = Now.ToString("yyyy")
        Dim month As String = Now.ToString("MM")
        Dim count As String = ""
        Dim aa As String = ""

        Dim dt As New DataTable
        dt = CEG.getSccAll()
        Dim dts As New DataTable
        Dim dtr As New DataTable

        Dim condGood As String = ""
        Dim condBad As String = ""

        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            Dim dtFrom = "=" & -999999999999
            Dim str As String = ""
            If dr("scc_good") = "" Then
                condGood = "1=1"
            Else
                condGood = dr("scc_good")
            End If

            If dr("scc_bad") = "" Then
                condBad = "1=1"
            Else
                condBad = dr("scc_bad")
            End If
            dtr = CEG.getScBaseline(dr("scc_code"))
            If dtr.Rows.Count() > 0 Then
                For Each drr In dtr.Rows
                    If Len(drr("base_upper")) > 0 And drr("base_upper") <> 0 Then
                        str = ">" & dtFrom & " AND <= " & drr("base_upper")

                        sql.AppendLine("IF NOT EXISTS (SELECT * FROM sc_validation where scorecard_code = '" & dr("scc_code") & "' and score = '" & drr("base_upper") & "'")
                        sql.AppendLine("and data_period = '" & year & month & "')")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("    IF EXISTS (SELECT * FROM view_summary where APNO = @app_no and " & condGood & ")")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        INSERT INTO sc_validation values ('" & year & month & "', '" & dr("scc_code") & "', '" & drr("base_upper") & "', '1', '0','0','" & str & "')")
                        sql.AppendLine("    END")
                        sql.AppendLine("    ELSE IF EXISTS (SELECT * FROM view_summary where APNO = @app_no and " & condBad & ")")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        INSERT INTO sc_validation values ('" & year & month & "', '" & dr("scc_code") & "', '" & drr("base_upper") & "', '0', '1', '0', '" & str & "')")
                        sql.AppendLine("    END")
                        sql.AppendLine("    ELSE")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        INSERT INTO sc_validation values ('" & year & month & "', '" & dr("scc_code") & "', '" & drr("base_upper") & "', '0', '0', '1', '" & str & "')")
                        sql.AppendLine("    END")
                        sql.AppendLine("END")
                        sql.AppendLine("ELSE")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("    IF EXISTS (SELECT * FROM view_summary where APNO = @app_no and " & condGood & ")")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        UPDATE sc_validation set good_count = good_count + 1 where scorecard_code = '" & dr("scc_code") & "' and score = '" & drr("base_upper") & "'")
                        sql.AppendLine("    END")
                        sql.AppendLine("    ELSE IF EXISTS (SELECT * FROM view_summary where APNO = @app_no and " & condBad & ")")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        UPDATE sc_validation set bad_count = bad_count + 1 where scorecard_code = '" & dr("scc_code") & "' and score = '" & drr("base_upper") & "'")
                        sql.AppendLine("    END")
                        sql.AppendLine("    ELSE")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        UPDATE sc_validation set indeterminate_count = indeterminate_count + 1 where scorecard_code = '" & dr("scc_code") & "' and score = '" & drr("base_upper") & "'")
                        sql.AppendLine("    END")
                        sql.AppendLine("END")
                        'sql.AppendLine(" WHEN score <=  " & drr("base_upper") & " THEN '" & drr("base_rate") & "' ")
                    End If
                    dtFrom = drr("base_upper")
                Next
            End If
            oGen.createSP(spName + dr("scc_code"), sql.ToString(), "@app_no varchar(50)")
            'aa = sql.ToString() + vbCrLf
        Next
        'Return aa
    End Function
End Class
