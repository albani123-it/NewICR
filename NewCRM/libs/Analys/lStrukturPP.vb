Imports System.Data.SqlClient

Public Class lStrukturPP
    Sub insertStrukturPengurus(ByVal e, ByVal AppNo, ByVal userid, ByVal nameFile)
        Dim tglLahir As String = "1900-01-01"
        Dim tglStart As String = "1900-01-01"
        Dim tglAkhir As String = "1900-01-01"
        Dim key As Boolean = False
        If Not e.form("txtTgllahir") = "" Then
            tglLahir = Mid(e.form("txtTgllahir"), 7, 4) + "-" & Mid(e.form("txtTgllahir"), 4, 2) + "-" & Mid(e.form("txtTgllahir"), 1, 2)
        End If
        If Not e.form("txtMasaKerjaDari") = "" Then
            tglStart = Mid(e.form("txtMasaKerjaDari"), 7, 4) + "-" & Mid(e.form("txtMasaKerjaDari"), 4, 2) + "-" & Mid(e.form("txtMasaKerjaDari"), 1, 2)
        End If
        If Not e.form("txtSampaiTgl") = "" Then
            tglAkhir = Mid(e.form("txtSampaiTgl"), 7, 4) + "-" & Mid(e.form("txtSampaiTgl"), 4, 2) + "-" & Mid(e.form("txtSampaiTgl"), 1, 2)
        End If
        If e.Form("chkKeyPerson") = "on" Then
            key = True
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertSusunanPengurus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@app_on", AppNo)
        cmdA.Parameters.AddWithValue("@nama", e.Form("txtNama"))
        cmdA.Parameters.AddWithValue("@tanggal", tglLahir)
        cmdA.Parameters.AddWithValue("@gender", e.Form("ddlGender"))
        cmdA.Parameters.AddWithValue("@alamat", e.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@namaPerusahaan", e.Form("txtNamaPerusahaan"))
        cmdA.Parameters.AddWithValue("@namajabatan", e.Form("txtNamaJabatan"))
        cmdA.Parameters.AddWithValue("@hubAntarPengurus", e.Form("txtHUbunganAntarPengurus"))
        cmdA.Parameters.AddWithValue("@masaKerja1", tglStart)
        cmdA.Parameters.AddWithValue("@masaKerja2", tglAkhir)
        cmdA.Parameters.AddWithValue("@riwayat", e.Form("txtRiwayat"))
        cmdA.Parameters.AddWithValue("@path", nameFile)
        cmdA.Parameters.AddWithValue("@keyPerson", key)
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDataStrukturPerusahaan(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getStrukturPengurusPerusahaan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataStrukturPerusahaanById(ByVal Id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataStrukturPengurusById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub updateStrukturPengurus(ByVal e, ByVal Id, ByVal userid, ByVal nameFile)
        Dim tglLahir As String = "1900-01-01"
        Dim tglStart As String = "1900-01-01"
        Dim tglAkhir As String = "1900-01-01"
        Dim key As Boolean = False
        If Not e.form("txtTgllahir") = "" Then
            tglLahir = Mid(e.form("txtTgllahir"), 7, 4) + "-" & Mid(e.form("txtTgllahir"), 4, 2) + "-" & Mid(e.form("txtTgllahir"), 1, 2)
        End If
        If Not e.form("txtMasaKerjaDari") = "" Then
            tglStart = Mid(e.form("txtMasaKerjaDari"), 7, 4) + "-" & Mid(e.form("txtMasaKerjaDari"), 4, 2) + "-" & Mid(e.form("txtMasaKerjaDari"), 1, 2)
        End If
        If Not e.form("txtSampaiTgl") = "" Then
            tglAkhir = Mid(e.form("txtSampaiTgl"), 7, 4) + "-" & Mid(e.form("txtSampaiTgl"), 4, 2) + "-" & Mid(e.form("txtSampaiTgl"), 1, 2)
        End If
        If e.Form("chkKeyPerson") = "on" Then
            key = True
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateStrukturPengurusPerusahaan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        cmdA.Parameters.AddWithValue("@nama", e.Form("txtNama"))
        cmdA.Parameters.AddWithValue("@tanggal", tglLahir)
        cmdA.Parameters.AddWithValue("@gender", e.Form("ddlGender"))
        cmdA.Parameters.AddWithValue("@alamat", e.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@namaPerusahaan", e.Form("txtNamaPerusahaan"))
        cmdA.Parameters.AddWithValue("@namajabatan", e.Form("txtNamaJabatan"))
        cmdA.Parameters.AddWithValue("@hubAntarPengurus", e.Form("txtHUbunganAntarPengurus"))
        cmdA.Parameters.AddWithValue("@masaKerja1", tglStart)
        cmdA.Parameters.AddWithValue("@masaKerja2", tglAkhir)
        cmdA.Parameters.AddWithValue("@riwayat", e.Form("txtRiwayat"))
        cmdA.Parameters.AddWithValue("@path", nameFile)
        cmdA.Parameters.AddWithValue("@keyPerson", key)
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub DeleteStrukturPengurus(ByVal Id, ByVal nameFile, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_deleteStrukturPengurusPerusahaan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        cmdA.Parameters.AddWithValue("@nameFile", nameFile)
        cmdA.Parameters.AddWithValue("@type", type)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
