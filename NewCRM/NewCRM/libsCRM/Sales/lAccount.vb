Imports System.Data.SqlClient

Public Class lAccount
    Public Function calculateAnnuitas(ByVal amount, ByVal interest, ByVal term)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("acc_calcAnnuitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@amount", amount)
        cmdA.Parameters.AddWithValue("@interest", interest)
        cmdA.Parameters.AddWithValue("@term", term)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
