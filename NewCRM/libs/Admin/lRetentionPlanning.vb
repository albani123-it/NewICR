Imports System.Data.SqlClient
Public Class lRetentionPlanning
    Function LoadGridData()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRetPlanTableGrid", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds
    End Function
    Sub DeleteRetPlan(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim dateupdate As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim myComm As New SqlCommand("sp_deleteRetPlan", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateupdate
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub
    Sub ApproveRetPlan(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim dateupdate As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim myComm As New SqlCommand("sp_approveRetPlan", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateupdate
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub

    Sub DeactivateRetPlan(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_deactivateRetPlan", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub
    Function LoadAvailableReasons()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getReasonRetention", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Sub InsertRetPlan(ByVal strperiod, ByVal endperiod, ByVal desc, ByVal role, ByVal sourch, ByVal finalreasons, ByVal usrLogin, ByVal retentioncode, ByVal status)
        Dim isactive As Integer
        If status = "Active" Then
            isactive = 1
        Else
            isactive = 0
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim dateinsert As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim PeriodStart As Date = Date.Parse(PeriodStart)
        Dim PeriodEnd As Date = Date.Parse(PeriodEnd)
        Dim cmdA As New SqlCommand("sp_insertRetPlan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@RetentionCode", SqlDbType.NVarChar).Value = retentioncode
        cmdA.Parameters.Add("@Description", SqlDbType.NVarChar).Value = desc
        cmdA.Parameters.Add("@RetRule", SqlDbType.NVarChar).Value = role
        cmdA.Parameters.Add("@SourceTable", SqlDbType.NVarChar).Value = sourch
        cmdA.Parameters.Add("@PeriodStart", SqlDbType.DateTime).Value = PeriodStart
        cmdA.Parameters.Add("@PeriodEnd", SqlDbType.DateTime).Value = PeriodEnd
        cmdA.Parameters.Add("@isActive", SqlDbType.Int).Value = isactive
        cmdA.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateinsert
        cmdA.Parameters.Add("@isDelete", SqlDbType.Int).Value = 0
        cmdA.Parameters.Add("@isApprove", SqlDbType.Int).Value = 0
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@finalreasons", SqlDbType.VarChar).Value = finalreasons
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub UpdateRetention(ByVal strperiod, ByVal endperiod, ByVal desc, ByVal role, ByVal status, ByVal id, ByVal finalreasons, ByVal usrLogin, ByVal sourcetable)
        Dim isactive As Integer
        If status = "Deactivate" Then
            isactive = 0
        Else
            isactive = 1
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim dateinsert As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim PeriodStart As Date = Date.Parse(strperiod)
        Dim PeriodEnd As Date = Date.Parse(endperiod)
        Dim cmdA As New SqlCommand("sp_updateRetPlan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@Description", SqlDbType.NVarChar).Value = desc
        cmdA.Parameters.Add("@RetRule", SqlDbType.NVarChar).Value = role
        cmdA.Parameters.Add("@SourceTable", SqlDbType.NVarChar).Value = sourcetable
        cmdA.Parameters.Add("@PeriodStart", SqlDbType.DateTime).Value = PeriodStart
        cmdA.Parameters.Add("@PeriodEnd", SqlDbType.DateTime).Value = PeriodEnd
        cmdA.Parameters.Add("@isActive", SqlDbType.Int).Value = isactive
        cmdA.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateinsert
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@isApprove", SqlDbType.Int).Value = 0
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@finalreasons", SqlDbType.VarChar).Value = finalreasons
        cmdA.ExecuteNonQuery()

        Dim cmdB As New SqlCommand("sp_deleteFinalReasonsRetPlan", myConn)
        cmdB.CommandType = CommandType.StoredProcedure
        cmdB.Parameters.Add("@ID", SqlDbType.Int).Value = id
        cmdB.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub InsertRetPlanDetail(ByVal retentioncode, ByVal reasoncode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertRetPlanDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ReasonCode", SqlDbType.NVarChar).Value = reasoncode
        cmdA.Parameters.Add("@RetentionCode", SqlDbType.NVarChar).Value = retentioncode
        cmdA.ExecuteNonQuery()
        cmdA.Parameters.Clear()
        myConn.Close()
    End Sub

    Function CheckRetentionCode(ByVal retentioncode) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkRetPlanCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@RetentionCode", SqlDbType.NVarChar).Value = retentioncode
        Dim retValParam As New SqlParameter("@RETURN_VALUE", SqlDbType.Int)
        retValParam.Direction = ParameterDirection.ReturnValue
        cmdA.Parameters.Add(retValParam)
        Dim reader As SqlDataReader = cmdA.ExecuteReader()
        isExists = Convert.ToInt32(retValParam.Value)
        myConn.Close()
        Return isExists
    End Function
    Function load_details(ByVal id As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRetPlanDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@ID", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(myComm)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function load_details_bycode(ByVal id As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRetPlanDetailsByCode", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@code", SqlDbType.NVarChar).Value = id
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Function LoadFinalReasons(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getFinalReasonRetPlan", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@ID", SqlDbType.Int).Value = id
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds
    End Function
    Function LoadSourceTableColumns(ByVal table_name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRetPlanSourceTableColumns", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = table_name
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Function ValidateFilter(ByVal source, ByVal rule)
        Dim str As String
        Dim tbl As String = source
        str = "SELECT top 10 * FROM " & tbl & " WHERE " & rule
        Dim isValidFilter As Integer = IsQueryValid(str)
        'Return str
        Return isValidFilter
    End Function
    Public Function IsQueryValid(ByVal strQuery As String) As Integer
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand(strQuery, myConn)

        Dim IsQueryOK As Integer = 1
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            IsQueryOK = 0
        End Try
        myConn.Close()

        Return IsQueryOK
    End Function

    Function CreateSP(ByVal strperiod, ByVal endperiod, ByVal code, ByVal role, ByVal id, ByVal souretable)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim PeriodStart As Date = Date.Parse(strperiod)
        Dim PeriodEnd As Date = Date.Parse(endperiod)
        Dim myComm As New SqlCommand("sp_getRetPlanWithinDateRange", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@SourceTable", SqlDbType.NVarChar).Value = souretable
        myComm.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = PeriodStart
        myComm.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = PeriodEnd
        myComm.Parameters.Add("@ID", SqlDbType.Int).Value = id
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()


        Dim tblcol As String

        Select Case souretable

            Case "BA_DTL"
                tblcol = "ACCTNUM, @RetCode RetentionCode "
            Case "CASATD_DTL"
                tblcol = "ACCTNUM,FUND_CARDNUM, @RetCode RetentionCode "
            Case "CC_DTL"
                'tblcol = "ACCTNUM,ACCT_CARDNUM, @RetCode RetentionCode "
                tblcol = "ACCTNUM,CARDNUM, @RetCode RetentionCode "
            Case "LOAN_DTL"
                tblcol = "ACCTNUM, @RetCode RetentionCode "
            Case "MF_DTL"
                tblcol = "ACCTNUM, @RetCode RetentionCode "
            Case Else
                tblcol = "*"
        End Select

        Dim rettablename = souretable & "_RET"
        rettablename = rettablename.Replace(" ", "_")
        Dim strselect As String = " INSERT INTO " & rettablename & " SELECT  " & tblcol & " FROM " & souretable & vbCr & vbLf &
        "WHERE " & role & vbCr & vbLf
        For i = 0 To ds.Tables(0).Rows.Count - 1
            strselect += " AND NOT (" & ds.Tables(0).Rows(i).Item("RetRule") & ")"
        Next
        Dim spname = "en_retention_" & code.ToString().ToUpper
        spname = spname.Replace(" ", "_")

        Dim myComm3 As New SqlCommand("sp_checkRetentionSP", myConn)
        myComm3.CommandType = CommandType.StoredProcedure
        myComm3.Parameters.Add("@spname", SqlDbType.NVarChar).Value = spname
        myComm3.ExecuteNonQuery()

        Dim spcmdstrng As String = ""
        Dim cmdB As New SqlCommand
        cmdB.Connection = myConn

        Dim strsql As New StringBuilder
        strsql.AppendLine("CREATE PROCEDURE " & spname)
        strsql.AppendLine(" AS ")
        strsql.AppendLine(" BEGIN ")
        strsql.AppendLine(" DECLARE @RetCode nvarchar(50) = '" & code.ToString().ToUpper & "'  ")
        strsql.AppendLine(strselect)
        strsql.AppendLine("END")

        cmdB.CommandText = strsql.ToString
        cmdB.ExecuteNonQuery()
        myConn.Close()
        Return cmdB.CommandText
    End Function

    Sub DeleteApprovedSP(ByVal retentioncode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_dropRetPlanSPonUpdate", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@RetCode", SqlDbType.NVarChar).Value = retentioncode
        cmdA.ExecuteNonQuery()
        cmdA.Parameters.Clear()
        myConn.Close()
    End Sub
    Function AlterRetentionSSIS()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getallSPRetPlan", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()

        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim spname As String
        Dim retcode As String
        Dim sourcetable As String
        Dim executestr As String
        executestr = ""
        dt = ds.Tables(0)


        Dim myComm2 As New SqlCommand("sp_getallValidRetPlanTables", myConn)
        myComm2.CommandType = CommandType.StoredProcedure
        Dim adapter2 As New SqlDataAdapter()
        Dim ds2 As New DataSet()
        myComm2.ExecuteNonQuery()
        adapter.SelectCommand = myComm2
        adapter.Fill(ds2)
        adapter.Dispose()
        myComm2.Dispose()
        Dim dt2 As DataTable
        dt2 = ds2.Tables(0)
        Dim strsqltruncate As New StringBuilder


        For i = 0 To dt2.Rows.Count - 1
            strsqltruncate.AppendLine("TRUNCATE TABLE " & dt2.Rows(i).Item("SourceTable").ToString + "_RET")
        Next

        For i = 0 To dt.Rows.Count - 1
            spname = dt.Rows(i).Item("ROUTINE_NAME").ToString()
            retcode = spname.Substring(13)
            'retcode = retcode.Replace("_", " ")
            Dim myComm1 As New SqlCommand("sp_getsourcetableSPRetPlan", myConn)
            myComm1.CommandType = CommandType.StoredProcedure
            myComm1.Parameters.Add("@RetentionCode", SqlDbType.NVarChar).Value = retcode
            Dim adapter1 As New SqlDataAdapter()
            Dim ds1 As New DataSet()
            myComm1.ExecuteNonQuery()
            adapter.SelectCommand = myComm1
            adapter.Fill(ds1)
            adapter.Dispose()
            myComm1.Dispose()
            myComm1.Parameters.Clear()
            dt1 = ds1.Tables(0)
            sourcetable = dt1.Rows(0).Item("SourceTable").ToString()

            Dim tblcol As String
            Dim rettablename As String = sourcetable & "_RET"
            Select Case sourcetable
                Case "BA_DTL"
                    tblcol = "ACCTNUM "
                Case "CASATD_DTL"
                    tblcol = "ACCTNUM,FUND_CARDNUM"
                Case "CC_DTL"
                    tblcol = "ACCT_CARDNUM"

                Case "LOAN_DTL"
                    tblcol = "ACCTNUM"
                Case "MF_DTL"
                    tblcol = "ACCTNUM "
            End Select

            Dim strsql As New StringBuilder
            strsql.AppendLine(" ")
            strsql.AppendLine("EXEC " & spname)
            strsql.AppendLine(" ")
            executestr += strsql.ToString
        Next
        Dim cmdB As New SqlCommand
        cmdB.Connection = myConn

        If executestr = "" Then
            executestr = "print 'Nothing to execute'"
        End If
        Dim strsql_alter As New StringBuilder
        strsql_alter.AppendLine("ALTER PROCEDURE enall_retention")
        strsql_alter.AppendLine(" AS ")
        strsql_alter.AppendLine(" BEGIN ")
        strsql_alter.AppendLine(strsqltruncate.ToString)
        strsql_alter.AppendLine(executestr)
        strsql_alter.AppendLine("END")
        cmdB.CommandText = strsql_alter.ToString
        cmdB.ExecuteNonQuery()
        myConn.Close()
        Return strsql_alter.ToString


    End Function
    Sub InsertTasklistRetention(ByVal usrLogin, ByVal rm, ByVal csv, ByVal cnum, ByVal offercode, ByVal notes, ByVal branch_code)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sp_insertTasklistfromRetention", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@offerCode", SqlDbType.NVarChar).Value = offercode
        cmdA.Parameters.Add("@ao_code", SqlDbType.NVarChar).Value = rm
        cmdA.Parameters.Add("@csv", SqlDbType.NVarChar).Value = csv
        cmdA.Parameters.Add("@cnum", SqlDbType.NVarChar).Value = cnum
        cmdA.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = branch_code
        cmdA.Parameters.Add("@usr", SqlDbType.NVarChar).Value = usrLogin
        cmdA.Parameters.Add("@notes", SqlDbType.VarChar).Value = notes
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub InsertRetentionResult(ByVal cnum, ByVal retcode, ByVal notes, ByVal status, ByVal acctnum, ByVal cardno, ByVal offercode)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sp_inserttoRetentionResult", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CNUM", SqlDbType.NVarChar).Value = cnum
        cmdA.Parameters.Add("@RetentionCode", SqlDbType.NVarChar).Value = retcode
        cmdA.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status
        cmdA.Parameters.Add("@ACCTNUM", SqlDbType.NVarChar).Value = acctnum
        cmdA.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = notes
        cmdA.Parameters.Add("@cardno", SqlDbType.VarChar).Value = cardno
        cmdA.Parameters.Add("@offercode", SqlDbType.VarChar).Value = offercode
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Function GetRetIDonInsert()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("SELECT MAX(ID) FROM RetPlanning", myConn)
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        cmd.ExecuteNonQuery()
        adapter.SelectCommand = cmd
        adapter.Fill(ds)
        adapter.Dispose()
        cmd.Dispose()
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
