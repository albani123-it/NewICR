Imports System.Data.SqlClient
Public Class lProcessApproval
    Function getListProcessApproval(ByVal user)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("get_processApproval", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr_head", user)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAppNoList()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("get_appnolist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getList(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("get_lisapproval", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub approval(ByVal id, ByVal usr, ByVal komen)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("Sp_approval", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@notes", komen)
        cmdA.ExecuteNonQuery()
        myConn.Close()

    End Sub

    Sub reject(ByVal usr, ByVal komen, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("Sp_reject", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@notes", komen)
        cmdA.ExecuteNonQuery()
        myConn.Close()

    End Sub

    Function CheckAvailableApproval(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("apr_checkAvailableHeadApproval", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

End Class
