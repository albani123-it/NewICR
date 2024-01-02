Imports System.Data.SqlClient

Public Class lProcessOffer

    Sub SaveAsDraft(id, field, value)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("pro_saveAsDraft", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@field", field)
        cmdA.Parameters.AddWithValue("@value", value)

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

End Class
