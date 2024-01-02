Imports System.Data.SqlClient

Public Class lAsumsi
    Function getListOfAsumsi()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getListOfAsumsi", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfAsumsiBySector(ByVal sector)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getListOfAsumsiBySector", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@sector", sector)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataHistorisFinancialStatement(ByVal brwcode, ByVal code, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getDataHistorisFinancialStatement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@acc_code", code)
        cmdA.Parameters.AddWithValue("@period", period)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataAsumsiYearly(ByVal brwcode, ByVal code, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getDataAsumsiYearly", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", code)
        cmdA.Parameters.AddWithValue("@asm_period", period)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataAsumsiMonthlyByParameter(ByVal brwcode, ByVal code, ByVal period, ByVal month, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getDataAsumsiMonthlyByParameter", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", code)
        cmdA.Parameters.AddWithValue("@asm_period", period)
        cmdA.Parameters.AddWithValue("@asm_month", month)
        cmdA.Parameters.AddWithValue("@asm_parameter", typ)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataAsumsiMonthlyParameter(ByVal brwcode, ByVal code, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getDataAsumsiMonthlyParameter", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", code)
        cmdA.Parameters.AddWithValue("@asm_period", period)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataAsumsiMonthlyDistribution(ByVal brwcode, ByVal code, ByVal period, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getDataAsumsiMonthlyDistribution", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", code)
        cmdA.Parameters.AddWithValue("@asm_period", period)
        cmdA.Parameters.AddWithValue("@asm_month", month)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataAsumsiMonthlyDistributionGrowth(ByVal brwcode, ByVal code, ByVal period, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getDataAsumsiMonthlyDistributionGrowth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", code)
        cmdA.Parameters.AddWithValue("@asm_period", period)
        cmdA.Parameters.AddWithValue("@asm_month", month)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdateAsumsiYearly(ByVal e As Object, ByVal usr As String)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_insertUpdateAsumsiYearly", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", e.brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", e.code)
        cmdA.Parameters.AddWithValue("@asm_period", e.period)
        cmdA.Parameters.AddWithValue("@asm_realisasi", e.realization)
        cmdA.Parameters.AddWithValue("@asm_value", e.value)
        cmdA.Parameters.AddWithValue("@asm_growth", e.growth)
        cmdA.Parameters.AddWithValue("@asm_reason", e.reason)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertUpdateAsumsiMonthlyDistribution(ByVal e As Object, ByVal usr As String)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_insertUpdateAsumsiMonthlyDistribution", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", e.brwcode)
        cmdA.Parameters.AddWithValue("@asm_code", e.code)
        cmdA.Parameters.AddWithValue("@asm_period", e.period)
        cmdA.Parameters.AddWithValue("@asm_month", e.month)
        cmdA.Parameters.AddWithValue("@asm_value", e.value)
        cmdA.Parameters.AddWithValue("@asm_growth", e.growth)
        cmdA.Parameters.AddWithValue("@asm_parameter", e.parameter)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Function getListOfParameterDistribution(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("asm_getListOfParameterDistribution", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acc_code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
