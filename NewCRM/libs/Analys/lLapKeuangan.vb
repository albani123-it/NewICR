Imports System.Data.SqlClient
Public Class lLapKeuangan
    Function getFormulaPctView(ByVal code, ByVal nameCode, ByVal identity, ByVal cls)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getFormulaPctView", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@code", code)
        cmdA.Parameters.AddWithValue("@nameCode", nameCode)
        cmdA.Parameters.AddWithValue("@identity", identity)
        cmdA.Parameters.AddWithValue("@class", cls)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Public Function getProyeksiWorksheetManufacture(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("view_proyeksi_worksheet_manufacture", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getAppendAccformula(ByVal report, ByVal sector, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("view_sql_formula", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@report", report)
        cmdA.Parameters.AddWithValue("@sector", sector)
        cmdA.Parameters.AddWithValue("@brwcode", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

    Public Function getPeriodByStatus(ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getPeriodeLapKeuByStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@status", status)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

    Sub runProyeksiYearly(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("run_proyeksi_yearly", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub runProyeksiOthers(ByVal brwcode, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("run_proyeksi_other", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@date", period)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function CheckAccDataByBrwcode(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkAccDataByBrwcode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Public Function getperiodeByBrwcode(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getPeriodeByBrwcode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Sub DelReportByPeriode(ByVal brwcode, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_deleteAccDataDetByPeriode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@periode", period)
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

End Class
