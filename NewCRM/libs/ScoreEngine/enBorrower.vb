Public Class enBorrower
    Dim oGen As New lGeneral
    Dim CEG As New enRatingCalculation

#Region " Global Variable "
    Dim spName = "ce_borrower_"
    Dim spNameChar = "ce_borrower_char_"
    Dim spNameCar = "ce_borrower_car_"
#End Region

    Public Function rebuildEngine()
        Dim dt As New DataTable
        dt = CEG.getSccforBorrower()
        Dim dts As New DataTable
        Dim dtr As New DataTable
        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            dtr = CEG.getScRating(dr("brw_scc"))
            If dtr.Rows.Count() > 0 Then
                sql.AppendLine(" SELECT Score, CASE ")
                For Each drr In dtr.Rows
                    If Len(drr("base_upper")) > 0 Then
                        sql.AppendLine(" WHEN score <=  " & drr("base_upper") & " THEN '" & drr("base_rate") & "' ")
                    End If
                Next
                sql.AppendLine(" ELSE '' ")
                sql.AppendLine(" END rating FROM ( ")
            Else
                sql.AppendLine(" SELECT score, '' as Rating FROM ( ")
            End If
            sql.AppendLine(" SELECT ( ")
            dts = CEG.getScChar(dr("brw_scc"))

            For Each drs In dts.Rows
                Dim dtFrom = "=" & -999999999999
                sql.AppendLine(" (CASE ")
                For i As Integer = 1 To 20
                    If Len(drs("sch_limit" & i).ToString) > 0 And Len(drs("sch_score" & i).ToString) > 0 Then
                        Dim str = ""
                        Dim dtTo As String = drs("sch_limit" & i)
                        'Dim dtScore As String = drs("sch_score" & i) * drs("sch_koefisien_score")
                        Dim dtScore As String = drs("sch_score" & i)
                        '--
                        If Not lGeneral.isDBTypeNumeric(CInt(drs("sch_field_type"))) Then
                            Dim arr() As String
                            arr = dtTo.Split(",")
                            If arr.GetUpperBound(0) > 0 Then
                                str += " WHEN " & drs("sch_field") & " IN ( "
                                Dim xtemp = ""
                                For x As Integer = 0 To arr.GetUpperBound(0)
                                    If xtemp <> "" Then
                                        xtemp += ", '" & arr(x) & "' "
                                    Else
                                        xtemp += " '" & arr(x) & "' "
                                    End If
                                Next
                                str += xtemp & " ) THEN " & dtScore & ""
                            ElseIf Not dtTo = "" And dtScore <> "0" Then
                                str += " WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN " & dtScore
                            End If
                        Else
                            str += " WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN " & dtScore & " "
                        End If
                        dtFrom = drs("sch_limit" & i)
                        '--
                        sql.AppendLine(str)
                        'sql.AppendLine(" WHEN " & drs("sch_field") & " <=  " + drs("sch_limit" & i) & " THEN " & drs("sch_score" & i) & " ")
                    End If
                Next
                sql.AppendLine(" ELSE " & oGen.intFormat(drs("sch_default_score")) & " ")
                sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & " + ")
            Next
            sql.AppendLine(" " & oGen.intFormat(dr("scc_additive_points")) & ")score ")
            sql.AppendLine(" , lbm_id FROM losnr_brw_summary WHERE lbm_brw_code = @brw_code  ")
            sql.AppendLine(" )als order by lbm_id desc")
            oGen.createSP(spName + dr("brw_scc"), sql.ToString(), "@brw_code varchar(50)")
            'Return sql.ToString() + spName + dr("brw_scc") + "@brw_code varchar(50)"
        Next
        createChar()
        'createCAR()
    End Function

    Sub createChar()
        Dim dt As New DataTable
        dt = CEG.getSccforBorrower()
        Dim dts As New DataTable
        Dim dtr As New DataTable
        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            dts = CEG.getScChar(dr("brw_scc"))
            For Each drs In dts.Rows
                sql.AppendLine(" ")
                Dim dtFrom = "=" & -999999999999
                sql.AppendLine(" if LEFT('" & drs("sch_field") & "',3) = 'LBR' ")
                sql.AppendLine("begin")
                sql.AppendLine(" insert into rating_summary_detail ")
                'sql.AppendLine(" SELECT @id,'" & dr("brw_scc") & "','" & drs("sch_field") & "',")
                sql.AppendLine(" SELECT @id,'brw',(select lsp_name from losnr_summary_parameter where lsp_field = '" & drs("sch_field") & "'),")
                sql.AppendLine(" (CASE ")
                For i As Integer = 1 To 20
                    If Len(drs("sch_limit" & i).ToString) > 0 And Len(drs("sch_score" & i).ToString) > 0 Then
                        Dim str = ""
                        Dim dtTo As String = drs("sch_limit" & i)
                        'Dim dtScore As String = drs("sch_score" & i) * drs("sch_koefisien_score")
                        Dim dtScore As String = drs("sch_score" & i)
                        '--
                        If Not lGeneral.isDBTypeNumeric(CInt(drs("sch_field_type"))) Then
                            Dim arr() As String
                            arr = dtTo.Split(",")
                            If arr.GetUpperBound(0) > 0 Then
                                str += " WHEN " & drs("sch_field") & " IN ( "
                                Dim xtemp = ""
                                For x As Integer = 0 To arr.GetUpperBound(0)
                                    If xtemp <> "" Then
                                        xtemp += ", '" & arr(x) & "' "
                                    Else
                                        xtemp += " '" & arr(x) & "' "
                                    End If
                                Next
                                str += xtemp & " ) THEN " & dtScore & ""
                            ElseIf Not dtTo = "" And dtScore <> "0" Then
                                str += " WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN " & dtScore
                            End If
                        Else
                            str += " WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN " & dtScore & " "
                        End If
                        dtFrom = drs("sch_limit" & i)
                        '--
                        sql.AppendLine(str)
                        'sql.AppendLine(" WHEN " & drs("sch_field") & " <=  " + drs("sch_limit" & i) & " THEN " & drs("sch_score" & i) & " ")
                    End If
                Next
                sql.AppendLine(" ELSE " & oGen.intFormat(drs("sch_default_score")) & " ")
                'sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & ",")
                'sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & "," & "(SELECT " & drs("sch_field") & " FROM losnr_brw_summary where lbm_brw_code = @brw_code)")
                sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & ",")
                'sql.AppendLine("(select case when left(" & drs("sch_field") & ",3) = 'LBR' then (select lbd_desc from losnr_bank_rating join losnr_bank_rating_detail on lbr_id = lbd_parent ")
                'sql.AppendLine("where lbr_field = '" & drs("sch_field") & "' and lbd_value = (SELECT " & drs("sch_field") & " FROM losnr_brw_summary where lbm_brw_code = @brw_code))")
                'sql.AppendLine("else  (SELECT " & drs("sch_field") & " FROM losnr_brw_summary where lbm_brw_code = @brw_code) end)")
                sql.AppendLine("(select lbd_desc from losnr_bank_rating join losnr_bank_rating_detail on lbr_id = lbd_parent ")
                sql.AppendLine("where lbr_field = '" & drs("sch_field") & "' and lbd_value = (SELECT " & drs("sch_field") & " FROM losnr_brw_summary where lbm_brw_code = @brw_code)")
                sql.AppendLine(")")
                sql.AppendLine(" FROM losnr_brw_summary WHERE lbm_brw_code = @brw_code  ")
                sql.AppendLine("end")

                dtFrom = "=" & -999999999999

                sql.AppendLine("else  ")
                sql.AppendLine("begin")
                sql.AppendLine(" insert into losnr_summary_result_detail ")
                sql.AppendLine(" SELECT @id,'brw',(select lsp_name from losnr_summary_parameter where lsp_field = '" & drs("sch_field") & "'),")
                sql.AppendLine(" (CASE ")
                For i As Integer = 1 To 20
                    If Len(drs("sch_limit" & i).ToString) > 0 And Len(drs("sch_score" & i).ToString) > 0 Then
                        Dim str = ""
                        Dim dtTo As String = drs("sch_limit" & i)
                        'Dim dtScore As String = drs("sch_score" & i) * drs("sch_koefisien_score")
                        Dim dtScore As String = drs("sch_score" & i)
                        '--
                        If Not lGeneral.isDBTypeNumeric(CInt(drs("sch_field_type"))) Then
                            Dim arr() As String
                            arr = dtTo.Split(",")
                            If arr.GetUpperBound(0) > 0 Then
                                str += " WHEN " & drs("sch_field") & " IN ( "
                                Dim xtemp = ""
                                For x As Integer = 0 To arr.GetUpperBound(0)
                                    If xtemp <> "" Then
                                        xtemp += ", '" & arr(x) & "' "
                                    Else
                                        xtemp += " '" & arr(x) & "' "
                                    End If
                                Next
                                str += xtemp & " ) THEN " & dtScore & ""
                            ElseIf Not dtTo = "" And dtScore <> "0" Then
                                str += " WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN " & dtScore
                            End If
                        Else
                            str += " WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN " & dtScore & " "
                        End If
                        dtFrom = drs("sch_limit" & i)
                        '--
                        sql.AppendLine(str)
                        'sql.AppendLine(" WHEN " & drs("sch_field") & " <=  " + drs("sch_limit" & i) & " THEN " & drs("sch_score" & i) & " ")
                    End If
                Next
                sql.AppendLine(" ELSE " & oGen.intFormat(drs("sch_default_score")) & " ")
                'sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & ",")
                'sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & "," & "(SELECT " & drs("sch_field") & " FROM losnr_brw_summary where lbm_brw_code = @brw_code)")
                sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & ",")
                sql.AppendLine("(SELECT " & drs("sch_field") & " FROM losnr_brw_summary where lbm_brw_code = @brw_code)")
                sql.AppendLine(" FROM losnr_brw_summary WHERE lbm_brw_code = @brw_code  ")
                sql.AppendLine("end")
            Next
            
            oGen.createSP(spNameChar + dr("brw_scc"), sql.ToString(), "@brw_code varchar(50),@id int")
        Next
    End Sub


    Function createCAR()
        Dim year As String = Now.ToString("yyyy")
        Dim month As String = Now.ToString("MM")
        Dim count As String = ""
        Dim aa As String = ""

        Dim dt As New DataTable
        dt = CEG.getSccforBorrower()
        Dim dts As New DataTable
        Dim dtr As New DataTable

        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            sql.AppendLine("DECLARE @default as varchar(100) = 'False'")
            dts = CEG.getScChar(dr("brw_scc"))
            For Each drs In dts.Rows

                Dim dtFrom = "= " & -999999999999
                For i As Integer = 1 To 20

                    If Len(drs("sch_limit" & i).ToString) > 0 And Len(drs("sch_score" & i).ToString) > 0 Then

                        Dim str = " "
                        Dim attr As String = ""
                        Dim cond As String = ""
                        Dim dtTo As String = drs("sch_limit" & i)
                        Dim dtScore As String = drs("sch_score" & i)
                        Dim dtCount As String = drs("sch_count" & i)

                        If Not lGeneral.isDBTypeNumeric(CInt(drs("sch_field_type"))) Then
                            Dim arr() As String
                            arr = dtTo.Split(",")
                            If arr.GetUpperBound(0) > 0 Then
                                str += " WHEN " & drs("sch_field") & " IN ( "
                                Dim xtemp = ""
                                For x As Integer = 0 To arr.GetUpperBound(0)
                                    If xtemp <> "" Then
                                        xtemp += ", '" & arr(x) & "' "
                                    Else
                                        xtemp += " '" & arr(x) & "' "
                                    End If
                                Next
                                str += xtemp & " ) THEN " & dtScore & ""
                            ElseIf Not dtTo = "" And dtScore <> "0" Then
                                str += "'" & dtTo & "', "
                                attr = dtTo
                                count = "(SELECT CASE WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN 1 ELSE 0 END FROM losnr_brw_summary where lbm_brw_code = @brw_code), "
                                cond = "(SELECT CASE WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN 1 ELSE 0 END FROM losnr_brw_summary where lbm_brw_code = @brw_code)"
                            End If
                        Else
                            str += "'>" & dtFrom & " AND <= " & dtTo & "', "
                            attr = ">" & dtFrom & " AND <= " & dtTo
                            count = "(SELECT CASE WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN 1 ELSE 0 END FROM losnr_brw_summary where lbm_brw_code = @brw_code), "
                            cond = "(SELECT CASE WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN 1 ELSE 0 END FROM losnr_brw_summary where lbm_brw_code = @brw_code)"
                        End If
                        dtFrom = drs("sch_limit" & i)
                        str += "'" & dtScore & "', '" & year & month & "', "
                        str += count
                        If dtCount = "" Then
                            str += "'0'"
                        Else
                            str += "'" & dtCount & "'"
                        End If
                        str += " )"

                        sql.AppendLine("IF " & cond & " = 1")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("SET @default = 'True'")
                        sql.AppendLine("END")
                        sql.AppendLine("IF NOT EXISTS (SELECT * FROM history_characteristic where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                        sql.AppendLine("and attribute = '" & attr & "' and period = '" & year & month & "')")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("    INSERT INTO history_characteristic values ( '" & dr("brw_scc") & "', '" & drs("sch_field") & "', ")
                        'sql.AppendLine(" INSERT INTO scorecard_charactheristic_history values ( '" & dr("brw_scc") & "', '" & drs("sch_field") & "', ")
                        sql.AppendLine(str)
                        sql.AppendLine("END")
                        sql.AppendLine("ELSE")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("    IF " & cond & " = 1")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        UPDATE history_characteristic set period_count = period_count + 1")
                        sql.AppendLine("        where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                        sql.AppendLine("        and attribute = '" & attr & "' and period = '" & year & month & "'")
                        sql.AppendLine("    END")
                        sql.AppendLine("END")

                    End If
                Next
                '=======default attribute===============
                sql.AppendLine("IF @default = 'False'")
                sql.AppendLine("BEGIN")
                sql.AppendLine("    IF NOT EXISTS (SELECT * FROM history_characteristic where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("    and attribute = 'Default Attribute' and period = '" & year & month & "')")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        INSERT INTO history_characteristic values ( '" & dr("brw_scc") & "', '" & drs("sch_field") & "', 'Default Attribute', ")
                sql.AppendLine("'" & drs("sch_default_score") & "', '" & year & month & "', '1','0' )")

                'sql.AppendLine(" DELETE FROM history_characteristic where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                'sql.AppendLine("and attribute = 'Default Attribute' and period = '" & year & month & "'")
                sql.AppendLine("    END")
                sql.AppendLine("    ELSE")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        UPDATE history_characteristic set period_count = period_count + 1")
                sql.AppendLine("        where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("        and attribute = 'Default Attribute' and period = '" & year & month & "'")
                sql.AppendLine("    END")
                sql.AppendLine("END")
                sql.AppendLine("ELSE")
                sql.AppendLine("BEGIN")
                sql.AppendLine("    IF NOT EXISTS (SELECT * FROM history_characteristic where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("    and attribute = 'Default Attribute' and period = '" & year & month & "')")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        INSERT INTO history_characteristic values ( '" & dr("brw_scc") & "', '" & drs("sch_field") & "', 'Default Attribute', ")
                sql.AppendLine("'" & drs("sch_default_score") & "', '" & year & month & "', '0','0' )")

                'sql.AppendLine(" DELETE FROM history_characteristic where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                'sql.AppendLine("and attribute = 'Default Attribute' and period = '" & year & month & "'")
                sql.AppendLine("    END")
                sql.AppendLine("    ELSE")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        UPDATE history_characteristic set period_count = 0")
                sql.AppendLine("        where scorecard_code = '" & dr("brw_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("        and attribute = 'Default Attribute' and period = '" & year & month & "'")
                sql.AppendLine("    END")
                sql.AppendLine("END")
                '=======end default attribute===============
                'sql.AppendLine("END")
                
            Next
            'sql.AppendLine(" " & oGen.intFormat(dr("scc_additive_points")) & ")score ")
            'sql.AppendLine(" , lbm_id FROM losnr_brw_summary WHERE lbm_brw_code = @brw_code  ")
            'sql.AppendLine(" )als order by lbm_id desc")
            oGen.createSP(spNameCar + dr("brw_scc"), sql.ToString(), "@brw_code varchar(50)")
            'Return sql.ToString() + spName + dr("brw_scc") + "@brw_code varchar(50)"
            'aa = sql.ToString() + vbCrLf
        Next
        'Return aa
    End Function
End Class
