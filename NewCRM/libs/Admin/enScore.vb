Imports System.Data.SqlClient
Public Class enScore
#Region " Global Variable "
    Dim str = ""
    Dim attField = ""
    Dim attJoin = ""
    Dim lastSp = ""
    Dim fromTbl = ""
    Dim sql = ""

    Public allSQL As New StringBuilder
    Dim scCode = ""
    Public dt As New DataTable
    Dim oGen As New lGeneral
    Public sp As String
#End Region

    Function deleteEngine(ByVal scc_code)
        oGen.exec_sp("IF EXISTS(SELECT * FROM dbo.sysobjects WHERE NAME = 'en_score_" + scc_code + "') EXEC ('DROP PROCEDURE en_score_" + scc_code + "') ")
        Return True
    End Function

    Function generated(ByVal scc_code)
        dt = getDetailOfScorecard(scc_code)
        scCode = scc_code
        sp = "en_score_" & scCode
        allSQL.AppendLine(" ")
        oGen.delete_sp(sp)
        allSQL.AppendLine("CREATE PROCEDURE en_score_" + scCode + " AS")

        execMaster()
        characteristicDefinition(dt.Rows(0).Item("scc_data_level"))
        'createResult()
        'updateScore()
        Return allSQL
    End Function

    Function getDetailOfScorecard(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getDetailOfScorecard", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function execMaster()
        allSQL.AppendLine("EXEC en_master_" + scCode + "")
    End Function

    Function characteristicDefinition(ByVal data_level)
        Dim masterTbl As String
        Dim accField As String
        Dim tblSc As String = "master_sc_" & scCode
        Dim tblScoreSc As String = "sc_score_" & scCode
        accField = data_level
        masterTbl = "master_sc_" & scCode

        temporaryAdditivePoints()
        writeAttribut(scCode, tblSc, accField)
        calculateScore(sp, scCode, tblSc, tblScoreSc, accField)
        'allSQL.AppendLine(oGen.script_end_sp())
        oGen.exec_sp(allSQL.ToString)
        oGen.exec_sp("exec sc_setSchedule '" & scCode & "','score',' " & sp & " '")
        Return True
    End Function

    Function temporaryAdditivePoints()
        allSQL.AppendLine("if exists (select * from dbo.sysobjects where name = 'master_sc_" + scCode + "') drop table master_sc_" + scCode + "")
        allSQL.AppendLine("SELECT *, cast('" + dt.Rows(0).Item("scc_additive_point").ToString + "' as numeric(18,10)) as additive_points, '" + scCode + "' scc_code INTO master_sc_" + scCode + " FROM master_" + scCode + "")
    End Function

    Function writeAttribut(ByVal sc, ByVal tblSc, ByVal accField)
        Dim scData As DataTable = getListAttribut(sc)
        Dim tmpTbl As String = ""
        For Each dr As DataRow In scData.Rows
            If dr("sch_field") <> "" Then
                tmpTbl = tblSc & "_" & dr("sch_field")
                allSQL.AppendLine(oGen.script_delete_tbl(tmpTbl))
                If dr("sch_multiply") = "Categorical" Then
                    writeAttributValue(dr("sch_id"), tmpTbl, accField, dr("sch_koefisien"))
                Else
                    writeAttributValueContinues(dr("sch_id"), tmpTbl, accField, dr("sch_koefisien"), dr("sch_field"))
                End If
                allSQL.AppendLine(" INTO " & tmpTbl)
                allSQL.AppendLine(" FROM " & tblSc)
            End If
            writeAttJoin(tblSc, tblSc & "_" & dr("sch_field"), accField)
        Next
        'str += delete_tbl("tmp_sc_" & sc)
        Return True
    End Function

    Function calculateScore(ByVal sp, ByVal sc, ByVal tblSc, ByVal tblScoreSc, ByVal accField)
        allSQL.AppendLine(oGen.script_delete_tbl(tblScoreSc))
        If attField = "" Then
            allSQL.AppendLine(" SELECT DISTINCT(" & tblSc & "." & accField & "), " & tblSc & ".scc_code, " & tblSc & ".additive_points as score, '" & sc.Substring(0, 3) & "' as scorecard INTO " & tblScoreSc & " FROM " & tblSc & " " & attJoin)
        Else
            allSQL.AppendLine(" SELECT DISTINCT(" & tblSc & "." & accField & "), " & tblSc & ".scc_code, (" & attField & " + " & tblSc & ".additive_points ) as score, '" & sc.Substring(0, 3) & "' as scorecard INTO " & tblScoreSc & " FROM " & tblSc & " " & attJoin)
        End If
        attField = ""
        attJoin = ""
        Return True
    End Function

    Function writeAttributValueContinues(ByVal schId, ByVal tmpTbl, ByVal accField, ByVal koefisien, ByVal sch_field)
        allSQL.AppendLine(" SELECT DISTINCT(" & accField & "), '" & scCode & "' scc_code, (" & sch_field & " * " & oGen.intFormat(koefisien) & ") as score_" & sch_field)
        writeAttField("score_" & sch_field, tmpTbl)
    End Function

    Function writeAttributValue(ByVal schId, ByVal tmpTbl, ByVal accField, ByVal koefisien)
        Dim scAtt As DataTable = getDetailAttribut(schId)

        allSQL.AppendLine(" SELECT DISTINCT(" & accField & "), '" & scCode & "' scc_code, " & oGen.intFormat(koefisien) & " * CASE ")
        Dim dtFrom As String
        dtFrom = "=" & -999999999999
        For Each drAtt As DataRow In scAtt.Rows
            For x As Integer = 1 To 20
                Dim dtTo As String = drAtt("sch_limit" & x)
                If drAtt("sch_score" & x).ToString = "" Then
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
                    'If Not oGen.isDBTypeNumeric(CInt(drAtt("sch_field_type"))) Then
                    Dim arr() As String
                        arr = dtTo.Split(",")
                        If arr.GetUpperBound(0) > 0 Then
                            allSQL.AppendLine(" WHEN " & drAtt("sch_field") & " IN ( ")
                            Dim xtemp = ""
                            For i As Integer = 0 To arr.GetUpperBound(0)
                                If xtemp <> "" Then
                                    xtemp += ", '" & arr(i) & "' "
                                Else
                                    xtemp += " '" & arr(i) & "' "
                                End If
                            Next
                            allSQL.AppendLine(xtemp & " ) THEN " & dtScore & "")
                        ElseIf Not dtTo = "" And dtScore <> "0" Then
                            allSQL.AppendLine(" WHEN " & drAtt("sch_field") & "= '" & dtTo & "' THEN " & dtScore)
                        End If
                    Else
                        allSQL.AppendLine(" WHEN " & drAtt("sch_field") & "> " & dtFrom & " AND " & drAtt("sch_field") & "<=" & dtTo & " THEN " & dtScore & " ")
                    End If
                'End If
                dtFrom = drAtt("sch_limit" & x)
            Next
            Dim defaultscore As String = drAtt("sch_default_score")
            allSQL.AppendLine(" WHEN 1=1 THEN " & defaultscore.Replace(",", ".") & " END as score_" & drAtt("sch_field"))
            writeAttField("score_" & drAtt("sch_field"), tmpTbl)
        Next
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

    Function writeAttJoin(ByVal tblSc, ByVal tblAtt, ByVal accField)
        attJoin += " LEFT JOIN " & tblAtt & " ON " & tblSc & "." & accField & "=" & tblAtt & "." & accField & " "
        Return True
    End Function

    Function getDetailAttribut(ByVal schId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getListOfSCH", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@parSCHID", SqlDbType.BigInt)
        cmdA.Parameters("@parSCHID").Value = schId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getListAttribut(ByVal scId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getListAttribut", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scId", SqlDbType.VarChar, 50)
        cmdA.Parameters("@scId").Value = scId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function
End Class
