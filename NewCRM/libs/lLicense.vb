Imports System.Data.SqlClient
Public Class lLicense
    ''' <summary>
    ''' to check license key
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function checkLicense(ByVal keys)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr

        Dim cmdA As New SqlCommand("z_checkLicense", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@keys", keys)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' to active license
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <remarks></remarks>
    Sub activeLicense(ByVal keys)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("z_activeLicense", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@keys", keys)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    ''' <summary>
    ''' to check license period
    ''' </summary>
    ''' <param name="keys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function checkLicensePeriod(ByVal keys)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("z_checkLicensePeriod", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@keys", keys)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
