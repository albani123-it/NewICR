Imports System.Data.SqlClient
Public Class lContact
    Function LoadGridData(ByVal tipe)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getContactTableGrid", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@tipe", SqlDbType.VarChar).Value = tipe
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

    Sub DeleteContact(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_deleteContact", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@ID", SqlDbType.Int).Value = id
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub
    Sub InsertContact(ByVal nama, ByVal datasource, ByVal typ, ByVal role, ByVal usrLogin)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertContact", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@name", SqlDbType.NVarChar).Value = nama
        cmdA.Parameters.Add("@data_source", SqlDbType.NVarChar).Value = datasource
        cmdA.Parameters.Add("@type", SqlDbType.NVarChar).Value = typ
        cmdA.Parameters.Add("@rule_definition", SqlDbType.NVarChar).Value = role
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Sub UpdateContact(ByVal nama, ByVal datasource, ByVal typ, ByVal role, ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateContact", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@name", SqlDbType.NVarChar).Value = nama
        cmdA.Parameters.Add("@data_source", SqlDbType.NVarChar).Value = datasource
        cmdA.Parameters.Add("@type", SqlDbType.NVarChar).Value = typ
        cmdA.Parameters.Add("@rule_definition", SqlDbType.NVarChar).Value = role
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function load_details(ByVal id As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getContactDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@ID", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(myComm)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Function GetAllRuleByType(ByVal type As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getAllContactRuleByType", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@type", SqlDbType.VarChar).Value = type
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

    Function CheckContactName(ByVal name, ByVal id) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checContactName", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@name", SqlDbType.NVarChar).Value = name
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = id
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

    Function LoadDataMartTablesCSV()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getDataMartTablescsv", myConn)
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

    Sub AlterSP(ByVal cmdstr, ByVal spname)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdB As New SqlCommand
        cmdB.Connection = myConn


        Dim strsql As New StringBuilder
        strsql.AppendLine("Alter PROCEDURE " & spname)
        strsql.AppendLine(" AS ")
        strsql.AppendLine(" BEGIN ")
        strsql.AppendLine(cmdstr)
        strsql.AppendLine("END")

        cmdB.CommandText = strsql.ToString
        cmdB.ExecuteNonQuery()

        myConn.Close()

    End Sub


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
    Public Function IsQueryValidwithMessage(ByVal strQuery As String) As String
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand(strQuery, myConn)

        Dim errormsg As String = ""

        Try
            cmd.ExecuteNonQuery()
        Catch SqlEx As SqlException
            Dim myError As SqlError
            Dim strsqlerror As New StringBuilder
            For Each myError In SqlEx.Errors

                strsqlerror.AppendLine("Error: " & myError.Message.ToString)
            Next

            errormsg += strsqlerror.ToString

        End Try

        myConn.Close()

        Return errormsg
    End Function
End Class
