Imports System.Data.SqlClient
Public Class enScoreBin
#Region " Global Variable "
    Public allSQL As New StringBuilder
    Dim scCode As String
    Public dt As New DataTable
    Dim oGen As New lGeneral
    Dim oScc As New lScorecard
    Public spName As String
    Public tblMaster As String
    Public tblResult As String
    Dim param = "score"
#End Region

    Function generated(ByVal scc_code As String)
        dt = getScBinDetail(scc_code)
        scCode = scc_code
        spName = "en_score_bin_" & scCode
        tblMaster = "sc_score_" & scCode
        tblResult = "sc_result_" & scCode
        allSQL.AppendLine(" ")
        oGen.delete_sp(spName)
        allSQL.AppendLine("CREATE PROCEDURE " + spName + " AS")
        binDefinition()
        oGen.exec_sp(allSQL.ToString)
        oGen.exec_sp("exec sc_setSchedule '" & scCode & "','bin',' " & spName & " '")
        Return allSQL
    End Function

    Function binDefinition()
        allSQL.AppendLine(oGen.script_delete_tbl(tblResult))
        allSQL.AppendLine("select " & tblMaster & ".*,")
        'loop bin
        allSQL.AppendLine("case ")
        Dim dtFrom As String
        dtFrom = "=" & -999999999999
        Dim tmp = 1
        For x As Integer = 1 To 20
            Dim dtTo As String = dt.Rows(0).Item("scb_limit" & x)
            If dtFrom <> "" And dtTo <> "" Then
                allSQL.AppendLine("when " & param & " <= " & oGen.intFormat(dt.Rows(0).Item("scb_limit" & x)) & " then " & oGen.intFormat(dt.Rows(0).Item("scb_score" & x)) & " ")
                tmp = x + 1
            End If
        Next
        allSQL.AppendLine("else " & tmp & " ")
        allSQL.AppendLine("end [bin] ")
        allSQL.AppendLine("into " & tblResult & " ")
        allSQL.AppendLine("from " & tblMaster & " ")
    End Function

    Public Function getScBinDetail(ByVal scCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getScBinDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar, 30).Value = scCode.ToString
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

End Class
