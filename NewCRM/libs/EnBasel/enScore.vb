Imports System.Data.SqlClient

Public Class enScores

#Region " Global Variable "
    Dim sql = ""
    Dim attField = ""
    Dim aSql = ""
    Dim aRule = ""
    Dim tmpTbl = ""
    Dim str = ""
    Dim attJoin = ""
    Dim spName = "en_score_"
    Dim mTable = "id_master_asset"
    Dim rTable = "id_master_asset"
    Dim defaultSCC = "IIR01"
    Dim uniqueField = "ACCTNO"
#End Region
    Dim gFunction As New lGeneral

    Function enScore(ByVal modelId)
        Dim dt As DataTable = gFunction.exec_text("select a.*, " &
            vbCrLf & "pd_text, pd_field, lgd_fixed, lgd_formula, lgd_field, ead_text, ead_field, maturity_text, maturity_field " &
            vbCrLf & "from asset_model a " &
            vbCrLf & "left join asset_model_radio b on a.mod_id = b.mod_id  where a.mod_id = " & modelId)
        Dim modelinfo = dt.Rows(0)
        mTable = "idx_master_asset_" & modelinfo("mod_ast_id")
        'If Len(modelinfo("mod_pd")) > 3 And modelinfo.Item("pd_text") = 1 Then
        If Left(modelinfo("mod_pd"), 3) = "IIR" Then
            enScorePD(modelinfo)
        Else
            unScorePD(modelinfo)
        End If
        If modelinfo.Item("mod_approach") = 3 Then
            If Not modelinfo("mod_lgd") = "" Then
                'If Len(modelinfo("mod_lgd")) > 3 Then
                If Left(modelinfo("mod_lgd"), 3) = "IIR" Then
                    enScoreLGD(modelinfo)
                Else
                    unScoreLGD(modelinfo)
                End If
            End If
            If Not modelinfo("mod_ead") = "" Then
                'If Len(modelinfo("mod_ead")) > 3 And modelinfo.Item("ead_text") = 1 Then
                If Left(modelinfo("mod_ead"), 3) = "IIR" Then
                    enScoreEAD(modelinfo)
                Else
                    unScoreEAD(modelinfo)
                End If
            End If
        End If
        Return enScoreSummary(modelinfo)
    End Function

    Function enScoreSummary(ByVal info)
        Dim scc, scc_lgd, scc_ead
        If Left(info.item("mod_pd"), 3) = "IIR" Then
            scc = info.Item("mod_pd")
        Else
            scc = 0
        End If

        If Left(info.item("mod_lgd"), 3) = "IIR" Then
            scc_lgd = info.Item("mod_lgd")
        Else
            scc_lgd = 0
        End If

        If Left(info.item("mod_ead"), 3) = "IIR" Then
            scc_ead = info.Item("mod_ead")
        Else
            scc_ead = 0
        End If

        spName = "en_score_" & info("mod_id")
        'for FIRB
        aSql = "EXEC " & spName & "_" & scc & "_PD "
        'for AIRB
        If info.Item("mod_approach") = 3 Then
            If info.Item("mod_lgd") <> "" Then
                'aSql += "EXEC " & spName & "_" & info.Item("mod_lgd") & "_LGD "
                aSql += "EXEC " & spName & "_" & scc_lgd & "_LGD "
            End If
            If info.Item("mod_ead") <> "" Then
                'aSql += "EXEC " & spName & "_" & info.Item("mod_ead") & "_EAD "
                aSql += "EXEC " & spName & "_" & scc_ead & "_EAD "
            End If
        End If
        mTable = "idx_master_asset_" & info("mod_id")
        rTable = "idx_master_asset_" & info("mod_ast_id")

        Dim field = " cast(null as varchar(5)) scc_code_lgd, cast(null as numeric(18,2)) score_lgd, cast(null as varchar(5)) scc_code_ead, cast(null as numeric(18,2)) score_ead "
        Dim joins = ""
        Dim drop = ""
        If info.Item("mod_approach") = 3 Then
            field = "" & rTable & "_score_lgd.scc_code_lgd, " & rTable & "_score_lgd.score_lgd, " & rTable & "_score_ead.scc_code_ead,  " & rTable & "_score_ead.score_ead "
            joins = " left join " & rTable & "_score_lgd on " & rTable & "." & uniqueField & " = " & rTable & "_score_lgd." & uniqueField & " " &
                vbCrLf & " left join " & rTable & "_score_ead on " & rTable & "." & uniqueField & " = " & rTable & "_score_ead." & uniqueField & " "
            drop = "drop table " & rTable & "_score_lgd " &
                vbCrLf & "drop table " & rTable & "_score_ead "
        End If
        aSql += gFunction.returnLogScript(mTable & "_sc", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_sc' ) " &
            vbCrLf & "DROP TABLE " & mTable & "_sc "
        aSql += "Select " & rTable & ".*, " &
                vbCrLf & " scc_code_pd, score_pd, " &
                vbCrLf & field &
                vbCrLf & " into " & mTable & "_sc " &
                vbCrLf & " from " & rTable & " " &
                vbCrLf & " left join " & rTable & "_score_pd on " & rTable & "." & uniqueField & " = " & rTable & "_score_pd." & uniqueField & " " &
                vbCrLf & joins

        aSql += gFunction.returnLogScript(mTable & "_sc", "END")
        aSql += "drop table " & rTable & "_score_pd " & drop
        'Return aSql
        spName = gFunction.returnSPName(info("mod_id"), "sc", "")
        'Return spName
        gFunction.createSP(spName, aSql)
    End Function

    Function unScorePD(ByVal info)
        Dim mdl = info("mod_id")
        'Dim scc = info("mod_pd")
        Dim scc = 0
        aSql = gFunction.returnLogScript(mTable & "_score_pd", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_score_pd' ) " &
                vbCrLf & "DROP TABLE " & mTable & "_score_pd "
        aSql += "SELECT " & uniqueField & ", '' as scc_code_pd, 0 as score_pd INTO " & mTable & "_score_pd " &
                vbCrLf & " FROM " & mTable & " " & vbCrLf
        aSql += gFunction.returnLogScript(mTable & "_score_pd", "END")
        spName = gFunction.returnSPName(mdl, scc, "PD")
        gFunction.createSP(spName, aSql)
        'Return spName
    End Function

    Function enScorePD(ByVal info)
        Dim mdl = info("mod_id")
        Dim scc = info("mod_pd")
        If IsDBNull(scc) Then
            scc = defaultSCC
        End If

        'create global score (additive points)
        tmpTbl = "tmp_idx_" & mdl & "_" & scc & ""
        aSql = gFunction.returnLogScript(tmpTbl, "START")
        Try
            aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & tmpTbl & "' ) " &
                vbCrLf & "DROP TABLE " & tmpTbl & " "

            Dim dt As DataTable = gFunction.exec_text("select * from scorecard where scc_code = '" & scc & "'")
            Dim scInfo = dt.Rows(0)
            aSql += "SELECT *, '" & scc & "' AS scc_code, " & scInfo("scc_additive_points") & " AS additive_points " &
                vbCrLf & " INTO " & tmpTbl & " FROM " & mTable & " "
            aSql += gFunction.returnLogScript(tmpTbl, "END")

            'create score by scorecard characteristic
            writeAttribut(scc, tmpTbl, uniqueField)
            calculateScore(scc, tmpTbl, mTable & "_tmpsc", uniqueField)
            aSql += str
            str = ""

            aSql += gFunction.returnLogScript(mTable & "_score_pd", "START")
            aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_score_pd' ) " &
                vbCrLf & "DROP TABLE " & mTable & "_score_pd "
            aSql += "SELECT " & uniqueField & ", scc_code as scc_code_pd, score as score_pd INTO " & mTable & "_score_pd " &
                    vbCrLf & " FROM " & mTable & "_tmpsc " & vbCrLf

            aSql += "drop table " & mTable & "_tmpsc " & vbCrLf
        Catch ex As Exception

        End Try
        aSql += gFunction.returnLogScript(mTable & "_score_pd", "END")
        spName = gFunction.returnSPName(mdl, scc, "PD")
        gFunction.createSP(spName, aSql)

        'Return aSql
    End Function

    Function unScoreLGD(ByVal info)
        Dim mdl = info("mod_id")
        'Dim scc = info("mod_lgd")
        Dim scc = 0
        aSql = gFunction.returnLogScript(mTable & "_score_lgd", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_score_lgd' ) " &
                vbCrLf & "DROP TABLE " & mTable & "_score_lgd "
        aSql += "SELECT " & uniqueField & ", '' as scc_code_lgd, 0 as score_lgd INTO " & mTable & "_score_lgd " &
                vbCrLf & " FROM " & mTable & " " & vbCrLf
        aSql += gFunction.returnLogScript(mTable & "_score_lgd", "END")
        spName = gFunction.returnSPName(mdl, scc, "LGD")
        gFunction.createSP(spName, aSql)
    End Function

    Function enScoreLGD(ByVal info)
        Dim mdl = info("mod_id")
        Dim scc = info("mod_lgd")
        'create global score (additive points)
        tmpTbl = "tmp_idx_" & mdl & "_" & scc & ""
        aSql = gFunction.returnLogScript(tmpTbl, "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & tmpTbl & "' ) " &
            vbCrLf & "DROP TABLE " & tmpTbl & " "

        Dim dt As DataTable = gFunction.exec_text("select * from scorecard where scc_code = '" & scc & "'")
        Dim scInfo = dt.Rows(0)
        aSql += "SELECT *, '" & scc & "' AS scc_code, " & scInfo("scc_additive_points") & " AS additive_points " &
            vbCrLf & " INTO " & tmpTbl & " FROM " & mTable & " "
        aSql += gFunction.returnLogScript(tmpTbl, "END")

        'create score by scorecard characteristic
        writeAttribut(scc, tmpTbl, uniqueField)
        calculateScore(scc, tmpTbl, mTable & "_tmpsc", uniqueField)
        aSql += str
        str = ""

        aSql += gFunction.returnLogScript(mTable & "_score_lgd", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_score_lgd' ) " &
            vbCrLf & "DROP TABLE " & mTable & "_score_lgd "
        aSql += "SELECT " & uniqueField & ", scc_code as scc_code_lgd, score as score_lgd INTO " & mTable & "_score_lgd " &
                vbCrLf & " FROM " & mTable & "_tmpsc " & vbCrLf
        aSql += gFunction.returnLogScript(mTable & "_score_lgd", "END")
        aSql += "drop table " & mTable & "_tmpsc " & vbCrLf
        spName = gFunction.returnSPName(mdl, scc, "LGD")
        gFunction.createSP(spName, aSql)
        Return aSql
    End Function

    Function unScoreEAD(ByVal info)
        Dim mdl = info("mod_id")
        'Dim scc = info("mod_ead")
        Dim scc = 0
        aSql = gFunction.returnLogScript(mTable & "_score_ead", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_score_ead' ) " &
                vbCrLf & "DROP TABLE " & mTable & "_score_ead "
        aSql += "SELECT " & uniqueField & ", '' as scc_code_ead, 0 as score_ead INTO " & mTable & "_score_ead " &
                vbCrLf & " FROM " & mTable & " " & vbCrLf
        aSql += gFunction.returnLogScript(mTable & "_score_ead", "END")
        spName = gFunction.returnSPName(mdl, scc, "EAD")
        gFunction.createSP(spName, aSql)
    End Function

    Function enScoreEAD(ByVal info)
        Dim mdl = info("mod_id")
        Dim scc = info("mod_ead")
        'create global score (additive points)
        tmpTbl = "tmp_idx_" & mdl & "_" & scc & ""
        aSql = gFunction.returnLogScript(tmpTbl, "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & tmpTbl & "' ) " &
            vbCrLf & "DROP TABLE " & tmpTbl & " "

        Dim dt As DataTable = gFunction.exec_text("select * from scorecard where scc_code = '" & scc & "'")
        Dim scInfo = dt.Rows(0)
        aSql += "SELECT *, '" & scc & "' AS scc_code, " & scInfo("scc_additive_points") & " AS additive_points " &
            vbCrLf & " INTO " & tmpTbl & " FROM " & mTable & " "
        aSql += gFunction.returnLogScript(tmpTbl, "END")

        'create score by scorecard characteristic
        writeAttribut(scc, tmpTbl, uniqueField)
        calculateScore(scc, tmpTbl, mTable & "_tmpsc", uniqueField)
        aSql += str
        str = ""

        aSql += gFunction.returnLogScript(mTable & "_score_ead", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_score_ead' ) " &
            vbCrLf & "DROP TABLE " & mTable & "_score_ead "
        aSql += "SELECT " & uniqueField & ", scc_code as scc_code_ead, score as score_ead INTO " & mTable & "_score_ead " &
                vbCrLf & " FROM " & mTable & "_tmpsc " & vbCrLf
        aSql += gFunction.returnLogScript(mTable & "_score_ead", "END")
        aSql += "drop table " & mTable & "_tmpsc " & vbCrLf
        spName = gFunction.returnSPName(mdl, scc, "EAD")
        gFunction.createSP(spName, aSql)
        Return aSql
    End Function

    Function writeAttribut(ByVal sc, ByVal tblSc, ByVal accField)
        Dim scData As DataTable = getListAttribut(sc)
        Dim tmpTbl As String = ""
        For Each dr As DataRow In scData.Rows
            If dr("sch_field") <> "" Then
                tmpTbl = tblSc & "_" & dr("sch_field")
                str += gFunction.returnLogScript(tmpTbl, "START")
                str += delete_tbl(tmpTbl)
                writeAttributValue(dr("sch_id"), tmpTbl, accField)
                str += " INTO " & tmpTbl
                str += " FROM " & tblSc
                str += gFunction.returnLogScript(tmpTbl, "END")
            End If
            writeAttJoin(tblSc, tblSc & "_" & dr("sch_field"), accField)
        Next
        Return str
        'str += delete_tbl("tmp_sc_" & sc)
        'Return True
    End Function

    Function writeAttributValue(ByVal schId, ByVal tmpTbl, ByVal accField)
        Dim scAtt As DataTable = getDetailAttribut(schId)
        str += " SELECT DISTINCT(" & accField & "), scc_code, CASE "
        Dim dtFrom As String
        dtFrom = "=" & -999999999999
        For Each drAtt As DataRow In scAtt.Rows
            For x As Integer = 1 To 20
                Dim dtTo As String = drAtt("sch_limit" & x)
                If drAtt("sch_score" & x) = "" Then
                    drAtt("sch_score" & x) = 0
                End If
                Dim dtScore As String = drAtt("sch_score" & x)
                dtScore = dtScore.Replace(",", ".")

                If IsDBNull(dtScore) Then
                    dtScore = 0
                End If
                If Not dtScore <> 0 Then
                    dtScore = 0
                End If
                If dtFrom <> "" And dtTo <> "" Then
                    If Not lGeneral.isDBTypeNumeric(CInt(drAtt("sch_field_type"))) Then
                        Dim arr() As String
                        arr = dtTo.Split(",")
                        If arr.GetUpperBound(0) > 0 Then
                            str += " WHEN " & drAtt("sch_field") & " IN ( "
                            Dim xtemp = ""
                            For i As Integer = 0 To arr.GetUpperBound(0)
                                If xtemp <> "" Then
                                    xtemp += ", '" & arr(i) & "' "
                                Else
                                    xtemp += " '" & arr(i) & "' "
                                End If
                            Next
                            str += xtemp & " ) THEN " & dtScore & ""
                        ElseIf Not dtTo = "" And dtScore <> "0" Then
                            str += " WHEN " & drAtt("sch_field") & "= '" & dtTo & "' THEN " & dtScore
                        End If
                    Else
                        str += " WHEN " & drAtt("sch_field") & "> " & dtFrom & " AND " & drAtt("sch_field") & "<=" & dtTo & " THEN " & dtScore & " "
                    End If
                End If
                dtFrom = drAtt("sch_limit" & x)
            Next
            Dim defaultscore As String = drAtt("sch_default_score")
            str += " WHEN 1=1 THEN " & defaultscore.Replace(",", ".") & " END * " & gFunction.intFormat(drAtt("sch_koefisien_score")) & " as score_" & drAtt("sch_field")
            writeAttField("score_" & drAtt("sch_field"), tmpTbl)
        Next
        Return True
    End Function

    Function calculateScore(ByVal sc, ByVal tblSc, ByVal tblScoreSc, ByVal accField)
        str += gFunction.returnLogScript(tblScoreSc, "START")
        str += delete_tbl(tblScoreSc)
        If attField = "" Then
            str += " SELECT DISTINCT(" & tblSc & "." & accField & "), " & tblSc & ".scc_code, " & tblSc & ".additive_points as score INTO " & tblScoreSc & " FROM " & tblSc & " " & attJoin
        Else
            str += " SELECT DISTINCT(" & tblSc & "." & accField & "), " & tblSc & ".scc_code, (" & attField & " + " & tblSc & ".additive_points ) as score INTO " & tblScoreSc & " FROM " & tblSc & " " & attJoin
        End If
        str += gFunction.returnLogScript(tblScoreSc, "END")
        attField = ""
        attJoin = ""
        Return True
    End Function

    Function writeAttField(ByVal fld, ByVal tmpTbl)
        If attField = "" Then
            attField += " " & tmpTbl & "." & fld
        Else
            attField += " + " & tmpTbl & "." & fld
        End If
        Return True
    End Function

    Function getListAttribut(ByVal scId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("sp_getListAttribut", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scId", SqlDbType.VarChar, 50)
        cmdA.Parameters("@scId").Value = scId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getDetailAttribut(ByVal schId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("sp_getListOfSCH", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@parSCHID", SqlDbType.BigInt)
        cmdA.Parameters("@parSCHID").Value = schId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function delete_tbl(ByVal tbl)
        Return " if exists ( Select name FROM sysobjects where name = '" & tbl & "' ) DROP table " & tbl
    End Function

    Function writeAttJoin(ByVal tblSc, ByVal tblAtt, ByVal accField)
        attJoin += " LEFT JOIN " & tblAtt & " ON " & tblSc & "." & accField & "=" & tblAtt & "." & accField & " "
        Return True
    End Function

End Class
