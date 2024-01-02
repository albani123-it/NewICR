Imports System.Data.SqlClient

Public Class lLkn
    Function checkAttachmentFile(ByVal appno, ByVal doc, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkAttchFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@doc", doc)
        cmdA.Parameters.AddWithValue("@type", type)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertAttachmentFile(ByVal appno, ByVal file, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertAttachmentFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@file", file)
        cmdA.Parameters.AddWithValue("@type", type)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAttachmentFile(ByVal appno, ByVal file, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateAttachmentFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@file", file)
        cmdA.Parameters.AddWithValue("@type", type)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDataLKN(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Function getDataLKNDtl(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataLknDtl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function insertupdateDataLkn(ByVal Request, ByVal appno, ByVal filename, ByVal userid)
        Dim Tanggal As String = "1900-01-01"
        If Not Request.form("InpTanggal") = "" Then
            Tanggal = Mid(Request.Form("InpTanggal"), 7, 4) + "-" & Mid(Request.Form("InpTanggal"), 4, 2) + "-" & Mid(Request.Form("InpTanggal"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuLKNInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@idHdr", Request.Form("txtIdLkn"))
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@nama", Request.Form("InpNama"))
        cmdA.Parameters.AddWithValue("@bentuk", Request.Form("InpBentuk"))
        cmdA.Parameters.AddWithValue("@tujuan", Request.Form("InpTujuan"))
        cmdA.Parameters.AddWithValue("@tanggal", Tanggal)
        cmdA.Parameters.AddWithValue("@jabatan", Request.Form("InpJabatan"))
        cmdA.Parameters.AddWithValue("@hasil", Request.Form("InpHasil"))
        cmdA.Parameters.AddWithValue("@tidakan", Request.Form("InpTindak"))
        cmdA.Parameters.AddWithValue("@alamat", Request.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@hslKunjungan", Request.Form("txtHasilKunjungan"))
        cmdA.Parameters.AddWithValue("@idDtl", Request.Form("txtIdDtl"))
        cmdA.Parameters.AddWithValue("@fotoKunjungan", filename)
        cmdA.Parameters.AddWithValue("@keteraganFoto", Request.Form("txtInfoFoto"))
        cmdA.Parameters.AddWithValue("@userid", userid)

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub delLKNDetail(ByVal idDtl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_delLknDtlById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@idDtl", idDtl)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

End Class
