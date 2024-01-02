Imports System.Data.SqlClient

Public Class lDropdown
    Function getListOfDdl()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ddl_getListOfDropdownList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfDdl(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ddl_getDetailOfDropdownList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@object", obj)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function insertDdlDetail(ByVal obj, ByVal code, ByVal desc, ByVal stats, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ddl_insertDdlDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@object", obj)
        cmdA.Parameters.AddWithValue("@code", code)
        cmdA.Parameters.AddWithValue("@desc", desc)
        cmdA.Parameters.AddWithValue("@stats", stats)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function updateDdlDetail(ByVal id, ByVal obj, ByVal code, ByVal desc, ByVal stats, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ddl_updateDdlDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@object", obj)
        cmdA.Parameters.AddWithValue("@code", code)
        cmdA.Parameters.AddWithValue("@desc", desc)
        cmdA.Parameters.AddWithValue("@stats", stats)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteDdlDetail(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ddl_deleteDdlDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function
End Class
