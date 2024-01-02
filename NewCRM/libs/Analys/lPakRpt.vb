Imports System.Data.SqlClient

Public Class lPakRpt
    Function getPakReportList(ByVal appno, ByVal type)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getKondisiPakReport", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@type", type)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
End Class
