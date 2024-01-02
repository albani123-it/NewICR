Imports System.Data.SqlClient

Public Class lCaseManagement
    Public Function getTicketByUserid(ByVal userId As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmd As New SqlCommand("csm_getListTicket", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@userid", userId)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
