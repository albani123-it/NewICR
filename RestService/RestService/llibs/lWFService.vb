Imports System.Data.SqlClient
Public Class lWFService
    Public Function wf_getType()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getWfType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_getList(ByVal wpmType, ByVal userRole)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getListWfType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wpmType", SqlDbType.VarChar, 100).Value = wpmType
        cmdA.Parameters.Add("@userRole", SqlDbType.VarChar, 100).Value = userRole
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_getProcessField(ByVal wpsId, ByVal role)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getWfProcessField", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wpsId", SqlDbType.BigInt).Value = wpsId
        cmdA.Parameters.Add("@role", SqlDbType.VarChar, 500).Value = role
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_insertAct(ByVal wpmId, ByVal userId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_insertAct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wpmId", SqlDbType.BigInt).Value = wpmId
        cmdA.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = userId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_insertActDetail(ByVal wacId, ByVal procId, ByVal userId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_insertActDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wac_id", SqlDbType.BigInt).Value = wacId
        cmdA.Parameters.Add("@proc_id", SqlDbType.BigInt).Value = procId
        cmdA.Parameters.Add("@user_id", SqlDbType.VarChar, 100).Value = userId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function wf_getNextWF(ByVal wacId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getNextWF", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wacId", SqlDbType.BigInt).Value = wacId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_getHistory(ByVal wacId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getHistory", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wacId", SqlDbType.BigInt).Value = wacId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_getTasklistUser(ByVal userId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getTasklistUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@userId", SqlDbType.VarChar, 500).Value = userId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function wf_getAllStatus(ByVal wacId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_getCurrentStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wacId", SqlDbType.BigInt).Value = wacId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function wf_insertLogUserAct(ByVal wacId, ByVal procId, ByVal userId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_insertLogUserAct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wac_id", SqlDbType.BigInt).Value = wacId
        cmdA.Parameters.Add("@proc_id", SqlDbType.BigInt).Value = procId
        cmdA.Parameters.Add("@user_id", SqlDbType.VarChar, 100).Value = userId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Public Sub wf_updateActFinish(ByVal wacId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("wf_updateActFinish", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@wacId", SqlDbType.BigInt).Value = wacId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub
End Class
