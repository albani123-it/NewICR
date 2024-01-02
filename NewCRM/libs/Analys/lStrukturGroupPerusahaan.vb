Imports System.Data.SqlClient

Public Class lStrukturGroupPerusahaan
    Sub insertStrukturGroupHdr(ByVal kodeGroup, ByVal namaGroup, ByVal keterangan, ByVal AppNo, ByVal filename, ByVal userid)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertStrukturGroupPerusahaan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@kodegroup", kodeGroup)
        cmdA.Parameters.AddWithValue("@namagroup", namaGroup)
        cmdA.Parameters.AddWithValue("@keterangan", keterangan)
        cmdA.Parameters.AddWithValue("@filename", filename)
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function insertStrukturGroupDtl(ByVal AppNo, ByVal subgroupId, ByVal subgroupCode, ByVal subgroupName, ByVal subgroupDesc, ByVal userid)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertStrukturGroupPerusahaanDtl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@idSubgroup", subgroupId)
        cmdA.Parameters.AddWithValue("@kodeSubgroup", subgroupCode)
        cmdA.Parameters.AddWithValue("@namaSubgroup", subgroupName)
        cmdA.Parameters.AddWithValue("@ketSubgroup", subgroupDesc)
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataStrukturPerusahaanByAppno(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataStrukturPerusahaanByAppNo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteStrukturGroupDtl(ByVal appno, ByVal id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_deleteStrukturPerusahaanDtlById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

End Class
