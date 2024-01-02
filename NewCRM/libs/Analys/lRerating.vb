Imports System.Data.SqlClient

Public Class lRerating
    Function getListReRating(ByVal brwcode, ByVal cif, ByVal npwp, ByVal nama, ByVal noapp, ByVal rek, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDataReRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = cif
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = npwp
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = nama
        cmdA.Parameters.Add("@noapp", SqlDbType.VarChar, 50).Value = noapp
        cmdA.Parameters.Add("@rek", SqlDbType.VarChar, 50).Value = rek
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfMenu(ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getNextMenu(ByVal id, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getNextMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getChildOfMenu(ByVal id, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getChildOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
