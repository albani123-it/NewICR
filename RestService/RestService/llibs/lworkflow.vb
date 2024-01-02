Imports System.Data.SqlClient

Public Class lworkflow
    Public Function getProduct(ByVal paramProd)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id_product", SqlDbType.VarChar, 16).Value = paramProd
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getWPMInfo(ByVal paramProd)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getWPMInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wpm_id", SqlDbType.VarChar, 16).Value = paramProd
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getFieldInfo(ByVal paramProd)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getFieldInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wps_id", SqlDbType.VarChar, 16).Value = paramProd
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProcCond(ByVal paramWpsId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getProcCond", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wps_id", SqlDbType.VarChar, 16).Value = paramWpsId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProcCond2(ByVal paramWcrId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getProcCond2", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wcr_id", SqlDbType.VarChar, 16).Value = paramWcrId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getWPMInfo(ByVal wpmId, ByVal wpmCounter)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getWorkflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wpm_id", SqlDbType.Int).Value = wpmId
        cmdA.Parameters.Add("@wpm_counter", SqlDbType.Int).Value = wpmCounter
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function insertLog(ByVal userId, ByVal processId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_insertLogActivity", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@user_id", SqlDbType.VarChar, 16).Value = userId
        cmdA.Parameters.Add("@process_id", SqlDbType.VarChar, 16).Value = processId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function insertLogDetail(ByVal wfId, ByVal processId, ByVal counter, ByVal wpmParam, ByVal userId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_insertLogActivityDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wf_id", SqlDbType.Int).Value = wfId
        cmdA.Parameters.Add("@process_id", SqlDbType.Int).Value = processId
        cmdA.Parameters.Add("@counter", SqlDbType.Int).Value = counter
        cmdA.Parameters.Add("@wpm_param", SqlDbType.VarChar, 100).Value = wpmParam
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = userId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getLogDet(ByVal wfId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getWfId", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wfid", SqlDbType.Int).Value = wfId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows.Count
    End Function


End Class
