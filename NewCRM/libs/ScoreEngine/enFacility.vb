Public Class enFacility
    Dim oGen As New lGeneral
    Dim CEG As New enRatingCalculation

#Region " Global Variable "
    Dim spName = "ce_facility_"
    Dim spNameChar = "ce_facility_char_"
    Dim spNameCar = "ce_facility_car_"
#End Region

    Public Function rebuildRecommendation()
        Dim dt As New DataTable
        dt = CEG.getProduct()
        For Each dr In dt.Rows
            recommandation(dr("prd_product"))
        Next

    End Function

    Public Function recommandation(ByVal prdCode)
        Dim dt As New DataTable
        dt = CEG.getDetailProduct(prdCode)
        Dim Sql As String
        Sql = " " & _
            vbCrLf & " select app_id, rec_limit, rec_tenor, rec_interest, " & _
            vbCrLf & "     case when aps_score >= prd_cut_approved then 'Approved' " & _
            vbCrLf & "         when  aps_score <= prd_cut_rejected then 'Rejected' else '' end rec_decision " & _
            vbCrLf & " from ( " & _
            vbCrLf & " select app_id, app_product_code, app_no, " & _
            vbCrLf & "     (" & dt.Rows(0).Item("prd_credit_limit") & ")as rec_limit," & _
            vbCrLf & "     app_req_tenor as rec_tenor, " & _
            vbCrLf & "     (app_req_interest * 100) as rec_interest " & _
            vbCrLf & " from losnr_app_info " & _
            vbCrLf & " left join (select col_app_id, sum(col_value)tot_collateral from losnr_app_collateral group by col_app_id) x on app_id = col_app_id  " & _
            vbCrLf & " where app_id = @app_id)a " & _
            vbCrLf & " left join mgmt_product b on a.app_product_code = b.prd_product " & _
            vbCrLf & " Left Join  " & _
            vbCrLf & "     (select top 1 * from losnr_app_rating where aps_app_id = @app_id order by aps_rating_date desc) c " & _
            vbCrLf & "     on a.app_id = c.aps_app_id " & _
            vbCrLf & " where b.prd_activate = 1 "
        'gFunction.exec_sp(Sql)
        oGen.createSP("ce_recommendation_" & dt.Rows(0).Item("prd_product") & "", Sql, "@app_id bigint")
        Return Sql
    End Function

    Public Function rebuildEngine()
        Dim dt As New DataTable
        dt = CEG.getSccforFacility()
        Dim dts As New DataTable
        Dim dtr As New DataTable
        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            dtr = CEG.getScRating(dr("prd_nonretail_facility_scc"))
            If dtr.Rows.Count() > 0 Then
                sql.AppendLine(" SELECT Score, CASE ")
                For Each drr In dtr.Rows
                    If Len(drr("base_upper")) > 0 Then
                        sql.AppendLine(" WHEN score <=  " & drr("base_upper") & " THEN '" & drr("base_rate") & "' ")
                    End If
                Next
                sql.AppendLine(" ELSE '' ")
                sql.AppendLine(" END Rating FROM ( ")
            Else
                sql.AppendLine(" SELECT score, '' as Rating FROM ( ")
            End If
            sql.AppendLine(" SELECT ( ")
            dts = CEG.getScChar(dr("prd_nonretail_facility_scc"))
            For Each drs In dts.Rows
                Dim dtFrom = "=" & -999999999999
                sql.AppendLine(" (CASE ")
                For i As Integer = 1 To 20
                    Dim str = ""
                    Dim dtTo As String = drs("sch_limit" & i)
                    Dim dtScore As String = drs("sch_score" & i)
                    If Len(drs("sch_limit" & i)) > 0 And Len(drs("sch_score" & i)) > 0 Then
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
                            ElseIf Not dtTo = "" Then
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
                sql.AppendLine(" END ) * " & drs("sch_koefisien_score") & " + ")
            Next
            sql.AppendLine(" " & oGen.intFormat(dr("scc_additive_points")) & ")score ")
            sql.AppendLine(" FROM losnr_app_summary WHERE lam_noApp = @app_no ")
            sql.AppendLine(" )als ")
            'Return sql
            oGen.createSP(spName + dr("prd_nonretail_facility_scc"), sql.ToString(), "@app_no varchar(50)")
        Next
        createChar()
        'createCAR()
    End Function

    Sub createChar()
        Dim dt As New DataTable
        dt = CEG.getSccforFacility()
        Dim dts As New DataTable
        Dim dtr As New DataTable
        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            dts = CEG.getScChar(dr("prd_nonretail_facility_scc"))
            For Each drs In dts.Rows
                Dim dtFrom = "=" & -999999999999
                sql.AppendLine(" if LEFT('" & drs("sch_field") & "',3) = 'LBR' ")
                sql.AppendLine("begin")
                sql.AppendLine(" insert into rating_summary_detail ")
                sql.AppendLine(" SELECT @id,'fcy',(select lsp_name from losnr_summary_parameter where lsp_field = '" & drs("sch_field") & "'),")
                sql.AppendLine(" (CASE ")
                For i As Integer = 1 To 20
                    Dim str = ""
                    Dim dtTo As String = drs("sch_limit" & i)
                    Dim dtScore As String = drs("sch_score" & i)
                    If Len(drs("sch_limit" & i)) > 0 And Len(drs("sch_score" & i)) > 0 Then
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
                            ElseIf Not dtTo = "" Then
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
                sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & ",")
                sql.AppendLine("(select lbd_desc from losnr_bank_rating join losnr_bank_rating_detail on lbr_id = lbd_parent ")
                sql.AppendLine("where lbr_field = '" & drs("sch_field") & "' and lbd_value = (SELECT " & drs("sch_field") & " FROM losnr_app_summary WHERE lam_noApp = @app_no)")
                sql.AppendLine(")")
                sql.AppendLine(" FROM losnr_app_summary WHERE lam_noApp = @app_no  ")
                sql.AppendLine("end")
                'sql.AppendLine(" END  * " & drs("sch_koefisien_score") & ") ," & "(SELECT " & drs("sch_field") & " FROM losnr_app_summary where lam_noApp = @app_no)")

                dtFrom = "=" & -999999999999

                sql.AppendLine("else  ")
                sql.AppendLine("begin")
                sql.AppendLine(" insert into losnr_summary_result_detail ")
                sql.AppendLine(" SELECT @id,'fcy',(select lsp_name from losnr_summary_parameter where lsp_field = '" & drs("sch_field") & "'),")
                sql.AppendLine(" (CASE ")
                For i As Integer = 1 To 20
                    Dim str = ""
                    Dim dtTo As String = drs("sch_limit" & i)
                    Dim dtScore As String = drs("sch_score" & i)
                    If Len(drs("sch_limit" & i)) > 0 And Len(drs("sch_score" & i)) > 0 Then
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
                            ElseIf Not dtTo = "" Then
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
                sql.AppendLine(" END ) *" & drs("sch_koefisien_score") & ",")
                sql.AppendLine("(SELECT " & drs("sch_field") & " FROM losnr_app_summary WHERE lam_noApp = @app_no)")
                sql.AppendLine(" FROM losnr_app_summary WHERE lam_noApp = @app_no  ")
                sql.AppendLine("end")
            Next
            oGen.createSP(spNameChar + dr("prd_nonretail_facility_scc"), sql.ToString(), "@app_no varchar(50), @id int")
        Next
    End Sub

    Function createCAR()
        Dim year As String = Now.ToString("yyyy")
        Dim month As String = Now.ToString("MM")
        Dim count As String = ""
        Dim aa As String = ""

        Dim dt As New DataTable
        dt = CEG.getSccforFacility()
        Dim dts As New DataTable
        Dim dtr As New DataTable

        For Each dr In dt.Rows
            Dim sql As New StringBuilder
            sql.AppendLine(" ")
            sql.AppendLine("DECLARE @default as varchar(100) = 'False'")
            dts = CEG.getScChar(dr("prd_nonretail_facility_scc"))
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
                            ElseIf Not dtTo = "" Then
                                str += "'" & dtTo & "', "
                                attr = dtTo
                                count = "(SELECT CASE WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN 1 ELSE 0 END FROM losnr_app_summary where lam_noApp = @app_no), "
                                cond = "(SELECT CASE WHEN " & drs("sch_field") & "= '" & dtTo & "' THEN 1 ELSE 0 END FROM losnr_app_summary where lam_noApp = @app_no)"
                            End If
                        Else
                            str += "'>" & dtFrom & " AND <= " & dtTo & "', "
                            attr = ">" & dtFrom & " AND <= " & dtTo
                            count = "(SELECT CASE WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN 1 ELSE 0 END FROM losnr_app_summary where lam_noApp = @app_no), "
                            cond = "(SELECT CASE WHEN " & drs("sch_field") & "> " & dtFrom & " AND " & drs("sch_field") & "<=" & dtTo & " THEN 1 ELSE 0 END FROM losnr_app_summary where lam_noApp = @app_no)"
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
                        sql.AppendLine("IF NOT EXISTS (SELECT * FROM history_characteristic where scorecard_code = '" & dr("prd_nonretail_facility_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                        sql.AppendLine("and attribute = '" & attr & "' and period = '" & year & month & "')")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("    INSERT INTO history_characteristic values ( '" & dr("prd_nonretail_facility_scc") & "', '" & drs("sch_field") & "', ")
                        sql.AppendLine(str)
                        sql.AppendLine("END")
                        sql.AppendLine("ELSE")
                        sql.AppendLine("BEGIN")
                        sql.AppendLine("    IF " & cond & " = 1")
                        sql.AppendLine("    BEGIN")
                        sql.AppendLine("        UPDATE history_characteristic set period_count = period_count + 1")
                        sql.AppendLine("        where scorecard_code = '" & dr("prd_nonretail_facility_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                        sql.AppendLine("        and attribute = '" & attr & "' and period = '" & year & month & "'")
                        sql.AppendLine("    END")
                        sql.AppendLine("END")

                    End If
                Next
                '=======default attribute===============
                sql.AppendLine("IF @default = 'False'")
                sql.AppendLine("BEGIN")
                sql.AppendLine("    IF NOT EXISTS (SELECT * FROM history_characteristic where scorecard_code = '" & dr("prd_nonretail_facility_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("    and attribute = 'Default Attribute' and period = '" & year & month & "')")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        INSERT INTO history_characteristic values ( '" & dr("prd_nonretail_facility_scc") & "', '" & drs("sch_field") & "', 'Default Attribute', ")
                sql.AppendLine("'" & drs("sch_default_score") & "', '" & year & month & "', '1','0' )")
                sql.AppendLine("    END")
                sql.AppendLine("    ELSE")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        UPDATE history_characteristic set period_count = period_count + 1")
                sql.AppendLine("        where scorecard_code = '" & dr("prd_nonretail_facility_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("        and attribute = 'Default Attribute' and period = '" & year & month & "'")
                sql.AppendLine("    END")
                sql.AppendLine("END")
                sql.AppendLine("ELSE")
                sql.AppendLine("BEGIN")
                sql.AppendLine("    IF NOT EXISTS (SELECT * FROM history_characteristic where scorecard_code = '" & dr("prd_nonretail_facility_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("    and attribute = 'Default Attribute' and period = '" & year & month & "')")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        INSERT INTO history_characteristic values ( '" & dr("prd_nonretail_facility_scc") & "', '" & drs("sch_field") & "', 'Default Attribute', ")
                sql.AppendLine("'" & drs("sch_default_score") & "', '" & year & month & "', '0','0' )")
                sql.AppendLine("    END")
                sql.AppendLine("    ELSE")
                sql.AppendLine("    BEGIN")
                sql.AppendLine("        UPDATE history_characteristic set period_count = 0")
                sql.AppendLine("        where scorecard_code = '" & dr("prd_nonretail_facility_scc") & "' and characteristic = '" & drs("sch_field") & "'")
                sql.AppendLine("        and attribute = 'Default Attribute' and period = '" & year & month & "'")
                sql.AppendLine("    END")
                sql.AppendLine("END")
                '=======end default attribute===============

            Next
            oGen.createSP(spNameCar + dr("prd_nonretail_facility_scc"), sql.ToString(), "@app_no varchar(50)")
            'aa = sql.ToString() + vbCrLf
        Next
        'Return aa
    End Function
End Class
