Imports System.Data.SqlClient

Public Class lAssignment
    Sub updateAppUser(ByVal appno, ByVal usrid, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateAppInfoUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@usr", usrid)
        cmdA.Parameters.AddWithValue("@usrLogin", usrLogin)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
