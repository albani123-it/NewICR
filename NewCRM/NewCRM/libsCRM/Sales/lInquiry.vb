Imports System.Data.SqlClient

Public Class lInquiry
    Public Function getCustomer(AcctNo, CardNo, CNUM, FSV, CSV, GCN, Name, DOB)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("inq_getCustomer", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@AccountNo", AcctNo)
        cmdA.Parameters.AddWithValue("@CardNo", CardNo)
        cmdA.Parameters.AddWithValue("@CNUM", CNUM)
        cmdA.Parameters.AddWithValue("@FSV", FSV)
        cmdA.Parameters.AddWithValue("@CSV", CSV)
        cmdA.Parameters.AddWithValue("@GCN", GCN)
        cmdA.Parameters.AddWithValue("@Name", Name)
        cmdA.Parameters.AddWithValue("@DOB", DOB)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    'Public Function removeLink(ByVal csv, ByVal usr)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr
    '    myConn.Open()
    '    Dim cmdA As New SqlCommand("sp_insertUnlinkage", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.AddWithValue("@csv", csv)
    '    cmdA.Parameters.AddWithValue("@usr", usr)
    '    cmdA.ExecuteNonQuery()
    '    myConn.Close()
    'End Function

    Public Function getDetailCustomer(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getDetailCustomerView", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@csv", csv)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProductOwn(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getProductOwn", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@csv", csv)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getFeature(ByVal CSV)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getFeature", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@csv", CSV)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getFeatureDetail(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getFeatureDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getPieChart(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("inq_getPieChart", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@csv", csv)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getBarChart(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("inq_getBarChart", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@csv", csv)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getCasaDetailByAccno(ByVal accno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getCasaDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@accno", accno)
        cmdA.Parameters.AddWithValue("@cardno", cardno)
        cmdA.Parameters.AddWithValue("@tbl", tbl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getCCDetailByAccno(ByVal accno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getCCDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@accno", accno)
        cmdA.Parameters.AddWithValue("@cardno", cardno)
        cmdA.Parameters.AddWithValue("@tbl", tbl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getLoanDetailByAccno(ByVal accno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getLoanDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@accno", accno)
        cmdA.Parameters.AddWithValue("@tbl", tbl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0)
    End Function

    Public Function getMfDetailByAccno(ByVal accno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getMfDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@accno", accno)
        cmdA.Parameters.AddWithValue("@cardno", cardno)
        cmdA.Parameters.AddWithValue("@tbl", tbl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getBaDetailByAccno(ByVal accno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getBaDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@accno", accno)
        cmdA.Parameters.AddWithValue("@cardno", cardno)
        cmdA.Parameters.AddWithValue("@tbl", tbl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getTradeDetailByAccno(ByVal accno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("inq_getTradeDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@accno", accno)
        cmdA.Parameters.AddWithValue("@cardno", cardno)
        cmdA.Parameters.AddWithValue("@tbl", tbl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
