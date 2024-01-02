Imports System.Data.SqlClient

Public Class lProjection
    '==================== old ============================
    Function getListOfProfitLoss(ByVal sector)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ppl_getListOfProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@sector", sector)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataOfProfitLoss(ByVal brwcode, ByVal period, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ppl_getDataOfProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@period", period)
        cmdA.Parameters.AddWithValue("@code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdateDataPL(ByVal brwcode, ByVal period, ByVal code, ByVal val, ByVal usr)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ppl_insertUpdateData", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@period", period)
        cmdA.Parameters.AddWithValue("@code", code)
        cmdA.Parameters.AddWithValue("@value", val)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '==================== end old ============================

End Class
