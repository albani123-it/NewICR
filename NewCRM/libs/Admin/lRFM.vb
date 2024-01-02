Imports System.Data.SqlClient
Public Class lRFM
    Function LoadGridData()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRFMGrid", myConn)
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

    Function CheckRFMName(ByVal rfmname, ByVal id) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkRFMName", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@RFMName ", SqlDbType.NVarChar).Value = rfmname
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id

        'Create a SqlParameter object to hold the output parameter value
        Dim retValParam As New SqlParameter("@RETURN_VALUE", SqlDbType.Int)

        'IMPORTANT - must set Direction as ReturnValue
        retValParam.Direction = ParameterDirection.ReturnValue

        'Finally, add the parameter to the Command's Parameters collection
        cmdA.Parameters.Add(retValParam)
        Dim reader As SqlDataReader = cmdA.ExecuteReader()
        isExists = Convert.ToInt32(retValParam.Value)
        myConn.Close()
        Return isExists
    End Function

    Function CheckddlProductTableCount(ByVal tablename) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkRFMProductTableCount", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tablename ", SqlDbType.NVarChar).Value = tablename
        'Create a SqlParameter object to hold the output parameter value
        Dim retValParam As New SqlParameter("@RETURN_VALUE", SqlDbType.Int)

        'IMPORTANT - must set Direction as ReturnValue
        retValParam.Direction = ParameterDirection.ReturnValue

        'Finally, add the parameter to the Command's Parameters collection
        cmdA.Parameters.Add(retValParam)
        Dim reader As SqlDataReader = cmdA.ExecuteReader()
        isExists = Convert.ToInt32(retValParam.Value)
        myConn.Close()
        Return isExists
    End Function

    Function InsertRFM(ByVal usrLogin, ByVal tablesource, ByVal rfmname, ByVal status, ByVal role) As Integer
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim isActive As Integer = 0
        If status = "1" Then
            isActive = 1
        End If
        Dim cmdA As New SqlCommand("sp_insertRFM", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        ' cmdA.Parameters.Add("@Name", SqlDbType.NVarChar).Value = e.form("rfm_name").ToString.ToUpper.Replace("_", " ")
        cmdA.Parameters.Add("@Name", SqlDbType.NVarChar).Value = rfmname
        cmdA.Parameters.Add("@Rule", SqlDbType.NVarChar).Value = role
        cmdA.Parameters.Add("@Product", SqlDbType.NVarChar).Value = tablesource
        cmdA.Parameters.Add("@isDelete", SqlDbType.Int).Value = 0
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@isActive", SqlDbType.Int).Value = isActive

        'cmdA.ExecuteNonQuery()
        Dim newID As Integer = CInt(cmdA.ExecuteScalar())
        myConn.Close()
        Return newID
    End Function

    Sub UpdateRFM(ByVal status, ByVal name, ByVal role, ByVal rfm_id, ByVal usrLogin, ByVal tableSource)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim isActive As Integer = 0
        If status.ToString = "1" Then
            isActive = 1
        End If

        Dim cmdA As New SqlCommand("sp_updateRFM", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name.ToString.ToUpper
        cmdA.Parameters.Add("@Rule", SqlDbType.NVarChar).Value = role
        cmdA.Parameters.Add("@Product", SqlDbType.NVarChar).Value = tableSource
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = rfm_id
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@isActive", SqlDbType.Int).Value = isActive

        cmdA.ExecuteNonQuery()
        'Dim newID As Integer = CInt(cmdA.ExecuteScalar())
        myConn.Close()

    End Sub

    Sub InsertRFMDetails(ByVal rfm_id, ByVal category, ByVal recency, ByVal frequency, ByVal monetary, ByVal usrLogin, ByVal recency_stat, ByVal frequency_stat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertRFMdetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@rfm_id", SqlDbType.Int).Value = rfm_id
        cmdA.Parameters.Add("@category", SqlDbType.Int).Value = category
        cmdA.Parameters.Add("@recency", SqlDbType.NVarChar).Value = recency
        cmdA.Parameters.Add("@frequency", SqlDbType.NVarChar).Value = frequency
        cmdA.Parameters.Add("@monetary", SqlDbType.NVarChar).Value = monetary
        cmdA.Parameters.Add("@recency_status", SqlDbType.VarChar).Value = recency_stat
        cmdA.Parameters.Add("@frequency_status", SqlDbType.VarChar).Value = frequency_stat
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub UpdateRFMDetails(ByVal rfm_id, ByVal category, ByVal recency, ByVal frequency, ByVal monetary, ByVal usrLogin, ByVal recency_stat, ByVal frequency_stat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateRFMdetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@rfm_id", SqlDbType.Int).Value = rfm_id
        cmdA.Parameters.Add("@category", SqlDbType.Int).Value = category
        cmdA.Parameters.Add("@recency", SqlDbType.NVarChar).Value = recency
        cmdA.Parameters.Add("@frequency", SqlDbType.NVarChar).Value = frequency
        cmdA.Parameters.Add("@monetary", SqlDbType.NVarChar).Value = monetary
        cmdA.Parameters.Add("@recency_status", SqlDbType.VarChar).Value = recency_stat
        cmdA.Parameters.Add("@frequency_status", SqlDbType.VarChar).Value = frequency_stat
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Sub DeleteRFM(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_deleteRFM", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub


    Sub DeativateRFM(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_deactivateRFM", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub

    Function ValidateFilter(ByVal source, ByVal rule)
        Dim str As String
        Dim tbl As String = source
        str = "SELECT TOP 10 * FROM " & tbl & " WHERE " & rule
        Dim isValidFilter As Integer = IsQueryValid(str)
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


    Sub CreateSP(ByVal role, ByVal id, ByVal tableSource, ByVal rfmname)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRFMDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.Int).Value = id
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()


        Dim tblcol As String
        tblcol = " *,@RFMCode RFM_Code "

        Dim myComm1 As New SqlCommand("sp_getRFMCategory", myConn)
        myComm1.CommandType = CommandType.StoredProcedure
        Dim adapter1 As New SqlDataAdapter()
        Dim ds1 As New DataSet()
        myComm1.ExecuteNonQuery()
        adapter1.SelectCommand = myComm1
        adapter1.Fill(ds1)
        adapter1.Dispose()
        myComm1.Dispose()

        Dim dt As DataTable
        Dim dt1 As DataTable
        Dim rfmtable As String
        Dim rule_rfm As String
        dt = ds1.Tables(0)

        'Dim i As Integer = 0
        For i = 0 To dt.Rows.Count - 1
            rfmtable = ""
            rfmtable = "idx_rfm_" & rfmname & "_" & dt.Rows(i).Item("rfc_code").ToString().Trim
            '   Dim strselect As String = " INSERT INTO " & rfmtable & " SELECT  * ,  " & e.form("rfm_name").ToString.ToUpper & " RFM_Code ," & tableSource & " Product  FROM " & tableSource & vbCr & vbLf &
            '"WHERE " & e.form("rfm_rule") & vbCr & vbLf
            rfmtable = rfmtable.Replace(" ", "_")

            Dim myComm2 As New SqlCommand("sp_getRFMDetailsbyNameCategory", myConn)
            myComm2.CommandType = CommandType.StoredProcedure
            myComm2.Parameters.Add("@category", SqlDbType.Int).Value = Integer.Parse(dt.Rows(i).Item("rfc_id").ToString())
            myComm2.Parameters.Add("@name", SqlDbType.NVarChar).Value = rfmname
            Dim adapter2 As New SqlDataAdapter()
            Dim ds2 As New DataSet()
            myComm2.ExecuteNonQuery()
            adapter2.SelectCommand = myComm2
            adapter2.Fill(ds2)
            adapter2.Dispose()
            myComm2.Dispose()
            dt1 = ds2.Tables(0)

            rule_rfm = "   and (" & dt1.Rows(0).Item("recency").ToString() & ") and  (" & dt1.Rows(0).Item("frequency").ToString() & ")  and  (" & dt1.Rows(0).Item("monetary").ToString() & ")  "

            Dim strselect As String = " SELECT  " & tblcol & " INTO " & rfmtable & " FROM " & tableSource & vbCr & vbLf &
            "WHERE (" & role & " ) " & rule_rfm & vbCr & vbLf
            Dim spname = "rfm_en_" & rfmname & "_" & dt.Rows(i).Item("rfc_code").ToString()
            spname = spname.Replace(" ", "_")
            Dim spcmdstrng As String = ""


            Dim myComm3 As New SqlCommand("sp_checkRFMSPandTable", myConn)
            myComm3.CommandType = CommandType.StoredProcedure
            myComm3.Parameters.Add("@tablename", SqlDbType.NVarChar).Value = rfmtable
            myComm3.Parameters.Add("@spname", SqlDbType.NVarChar).Value = spname
            myComm3.ExecuteNonQuery()

            Dim cmdB As New SqlCommand
            cmdB.Connection = myConn
            Dim strsql As New StringBuilder
            strsql.AppendLine("CREATE PROCEDURE " & spname)
            strsql.AppendLine(" AS ")
            strsql.AppendLine(" BEGIN ")
            strsql.AppendLine(" DECLARE @RFMCode nvarchar(100) = '" & rfmname & "'  ")
            strsql.AppendLine(" DECLARE @Product nvarchar(100) = '" & tableSource.ToString() & "'")
            strsql.AppendLine(strselect)
            strsql.AppendLine("END")

            cmdB.CommandText = strsql.ToString
            cmdB.ExecuteNonQuery()
            cmdB.Dispose()
            'Return strsql.ToString

        Next


        myConn.Close()
    End Sub

    Function AlterRFMSSIS()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getallSPRFM", myConn)
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
        Dim rfmcode As String
        Dim rfmtable As String
        Dim executestr As String
        Dim producttable As String
        executestr = ""
        dt = ds.Tables(0)

        Dim rtn As String = ""
        For i = 0 To dt.Rows.Count - 1
            spname = dt.Rows(i).Item("ROUTINE_NAME").ToString()
            Dim rfmvalue As String = spname.Substring(spname.Length - 3, 3)

            rfmcode = spname.Substring(7)

            rfmtable = "idx_rfm_" & rfmcode
            rfmcode = rfmcode.Substring(0, rfmcode.Length - 4)



            'rfmcode = rfmcode.Replace("_", " ")

            rtn = rtn + " - " + rfmcode


            Dim myComm1 As New SqlCommand("sp_getRFMDetailsbyName", myConn)
            myComm1.CommandType = CommandType.StoredProcedure
            myComm1.Parameters.Add("@Name", SqlDbType.NVarChar).Value = rfmcode
            Dim adapter1 As New SqlDataAdapter()
            Dim ds1 As New DataSet()
            myComm1.ExecuteNonQuery()
            adapter.SelectCommand = myComm1
            adapter.Fill(ds1)
            adapter.Dispose()
            myComm1.Dispose()
            myComm1.Parameters.Clear()
            dt1 = ds1.Tables(0)
            ' producttable = dt1.Rows(0).Item("rfm_product").ToString()
            producttable = dt1.Rows(0).Item("rfm_product").ToString()

            'Dim tblcol As String = "rfm_code, cnum, '" & rfmvalue & "'"
            Dim tblcol As String = "rfm_code, acctnum, '" & rfmvalue & "'"
            Dim tbl As String = "rfm_all"

            Dim strsql As New StringBuilder
            strsql.AppendLine(" ")
            strsql.AppendLine("EXEC " & spname)
            strsql.AppendLine("Insert into " & tbl & " select " & tblcol & " FROM " & rfmtable)
            strsql.AppendLine("Drop TABLE " & rfmtable)
            strsql.AppendLine(" ")
            executestr += strsql.ToString
        Next


        Dim cmdB As New SqlCommand
        cmdB.Connection = myConn

        If executestr = "" Then
            executestr = "print 'Nothing to execute'"
        End If
        Dim strsql_alter As New StringBuilder
        strsql_alter.AppendLine("ALTER PROCEDURE enall_rfm")
        strsql_alter.AppendLine(" AS ")
        strsql_alter.AppendLine(" BEGIN ")
        strsql_alter.AppendLine(" TRUNCATE TABLE rfm_all ")
        strsql_alter.AppendLine(executestr)
        strsql_alter.AppendLine(" EXEC enall_rfm_result ")
        strsql_alter.AppendLine("END")
        cmdB.CommandText = strsql_alter.ToString
        cmdB.ExecuteNonQuery()
        myConn.Close()

        Return rtn
    End Function

    Function chkBatchPro()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_rfmchkDatPro", myConn)
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

    Function chkBatchMonth()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_rfmchkDatMonth", myConn)
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
    Function load_detailsRFM(ByVal id As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRFMDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(myComm)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
