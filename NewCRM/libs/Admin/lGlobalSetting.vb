Imports System.Data.SqlClient
Public Class lGlobalSetting
    Public path As String = "~/DataUpload/"

    Sub InsertGlobalDuration(ByVal usrLogin, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertGlobalProductivityDuration", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@Type", SqlDbType.VarChar).Value = type
        cmdA.Parameters.Add("@Call", SqlDbType.Int).Value = 1
        cmdA.Parameters.Add("@Visit", SqlDbType.Int).Value = 1
        cmdA.Parameters.Add("@WIC", SqlDbType.Int).Value = 1
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Function LoadGridData(ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getGlobalSettingGrid", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@tablename", SqlDbType.VarChar).Value = type
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
    Function load_details_prod(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getGlobalSettingProdDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@code", SqlDbType.VarChar).Value = id
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

    Function load_details_tasklist(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getGlobalSettingTasklistDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(myComm)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Sub UpdateGlobalDuration(ByVal e, ByVal usrLogin, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateGlobalProductivityDuration", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@Type", SqlDbType.NVarChar).Value = code
        cmdA.Parameters.Add("@Call", SqlDbType.Int).Value = e.form("CallDuration")
        cmdA.Parameters.Add("@Visit", SqlDbType.Int).Value = e.form("VisitDuration")
        cmdA.Parameters.Add("@WIC", SqlDbType.Int).Value = e.form("WICDuration")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub InsertTasklistStatus(ByVal code, ByVal activity, ByVal name, ByVal Description, ByVal Status, ByVal SLA, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertGlobalTasklistStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.VarChar).Value = code.ToString.ToUpper
        cmdA.Parameters.Add("@type", SqlDbType.VarChar).Value = activity.ToString
        cmdA.Parameters.Add("@name", SqlDbType.VarChar).Value = name
        cmdA.Parameters.Add("@description", SqlDbType.VarChar).Value = Description
        cmdA.Parameters.Add("@status", SqlDbType.VarChar).Value = Status.ToString
        cmdA.Parameters.Add("@sla", SqlDbType.VarChar).Value = SLA.ToString
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub UpdateTasklistStatus(ByVal usrLogin, ByVal code, ByVal type, ByVal name, ByVal desc, ByVal status, ByVal sla, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateGlobalTasklistStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@code", SqlDbType.VarChar).Value = code.ToString().ToUpper
        cmdA.Parameters.Add("@type", SqlDbType.VarChar).Value = type.ToString()
        cmdA.Parameters.Add("@name", SqlDbType.VarChar).Value = name
        cmdA.Parameters.Add("@description", SqlDbType.VarChar).Value = desc
        cmdA.Parameters.Add("@status", SqlDbType.VarChar).Value = status.ToString()
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@sla", SqlDbType.VarChar).Value = sla.ToString
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Function CheckTasklistNameCode(ByVal code, ByVal name, ByVal type, ByVal id) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkGlobalTasklistNameCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.NVarChar).Value = code
        cmdA.Parameters.Add("@name", SqlDbType.NVarChar).Value = name
        cmdA.Parameters.Add("@type", SqlDbType.NVarChar).Value = type
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

    Function GetGlobalTableToDownload(ByVal name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getGlobalSetttingTableToDL", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@tablename", SqlDbType.NVarChar).Value = name
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
    Function getListGloCapital()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getListGlobalCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfGlobalCapitalByPeriod(ByVal month, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getDetailOfGlobalCapitalByPeriod", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@month", month)
        cmdA.Parameters.AddWithValue("@year", year)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListGloCapitalMonth()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getListGlobalCapitalMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListGloPreScreening()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalPreScreening", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getListGloCapitalYear()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getListGlobalCapitalYear", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
