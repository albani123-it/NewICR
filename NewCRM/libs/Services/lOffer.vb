Imports System.Data.SqlClient

Public Class lOffer
    Function LoadGridData()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("ofr_getListOffer", myConn)
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
End Class
