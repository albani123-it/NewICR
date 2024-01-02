Imports System.Data.SqlClient

Public Class lRating
    Public Function CheckStringNull(ByVal parVal As String) As Object
        If parVal = "" Then
            Return DBNull.Value
        Else
            Return parVal
        End If
    End Function

    Function getDetailOfFacilityCredit(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_getDetailFacilityCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub updateFacilityCredit(ByVal e, ByVal AppNo, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        'Dim avperiode As String = "1900-01-01"

        'If Not e.form(master + "txtAvailibilityPeriode") = "" Then
        '    avperiode = Mid(e.form(master + "txtAvailibilityPeriode"), 7, 4) + "-" & Mid(e.form(master + "txtAvailibilityPeriode"), 4, 2) + "-" & Mid(e.form(master + "txtAvailibilityPeriode"), 1, 2)
        'End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisPengajuan")
        cmdA.Parameters.Add("@fasilitas", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlFasilitas")
        cmdA.Parameters.Add("@jenisPembayaran", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSitemPembayaran")
        cmdA.Parameters.Add("@jenisCurr", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisCurr")
        cmdA.Parameters.Add("@kurs", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtKurs")
        cmdA.Parameters.Add("@nilaiValas", SqlDbType.Decimal).Value = e.Form(master + "TxtNilaiValas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiRupiah", SqlDbType.Decimal).Value = e.Form(master + "TxtNilaiRp").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jangkaWaktu", SqlDbType.Int).Value = e.Form(master + "TxtJangkaWaktu")
        cmdA.Parameters.Add("@grace", SqlDbType.Int).Value = e.Form(master + "TxtGrace")
        cmdA.Parameters.Add("@bungaPersen", SqlDbType.Float).Value = e.Form(master + "TxtBunga")
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "RadBunga")
        cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtTotal").ToString().Replace(",", "")
        'cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = e.Form(master + "TxtNama")
        'cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtAlamat")
        cmdA.Parameters.Add("@peruntukan", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtPeruntukan")
        cmdA.Parameters.Add("@sifat", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSifat")
        'cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlTujuan")
        'cmdA.Parameters.Add("@rumah", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlHouse")
        'cmdA.Parameters.Add("@sektor", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlSektor")
        'cmdA.Parameters.Add("@subsektor", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlSubsektor")

        cmdA.Parameters.Add("@nilaiProyek", SqlDbType.Decimal).Value = e.Form(master + "TxtNilaiProyek").ToString().Replace(",", "")
        'cmdA.Parameters.AddWithValue("@avperiode", avperiode)
        cmdA.Parameters.AddWithValue("@avperiode", e.Form(master + "txtAvailibilityPeriode"))
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditRr(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityCreditRr", myConn)
        cmdA.CommandType = CommandType.StoredProcedure


        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@fasilitas", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlFasilitas")
        cmdA.Parameters.Add("@jenisPembayaran", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSitemPembayaran")
        cmdA.Parameters.Add("@jenisCurr", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisCurr")
        cmdA.Parameters.Add("@kurs", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtKurs")
        cmdA.Parameters.Add("@nilaiRupiah", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtNilaiRp").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jangkaWaktu", SqlDbType.Int).Value = e.Form(master + "TxtJangkaWaktu")
        cmdA.Parameters.Add("@grace", SqlDbType.Int).Value = e.Form(master + "TxtGrace")
        cmdA.Parameters.Add("@bungaPersen", SqlDbType.Int).Value = e.Form(master + "TxtBunga")
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "RadBunga")
        'cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = e.Form(master + "TxtNama")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtAlamat")
        cmdA.Parameters.Add("@peruntukan", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtPeruntukan")
        cmdA.Parameters.Add("@sifat", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSifat")
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlTujuan")
        cmdA.Parameters.Add("@rumah", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlHouse")
        cmdA.Parameters.Add("@nilaiProyek", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtNilaiProyek").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertFacilityCredit(ByVal e, ByVal AppNo, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        'Dim m_avperiod As String = e.form(master & "ddlTahunAvailibility")
        'Dim y_avperiod As String = e.form(master & "ddlBulanAvailibility")
        'Dim avperiode As String = "01" & "-" & m_avperiod & "-" & y_avperiod
        'If Not e.form(master + "txtAvailibilityPeriode") = "" Then
        '    avperiode = Mid(e.form(master + "txtAvailibilityPeriode"), 7, 4) + "-" & Mid(e.form(master + "txtAvailibilityPeriode"), 4, 2) + "-" & Mid(e.form(master + "txtAvailibilityPeriode"), 1, 2)
        'End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisPengajuan")
        cmdA.Parameters.Add("@fasilitas", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlFasilitas")
        cmdA.Parameters.Add("@jenisPembayaran", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSitemPembayaran")
        cmdA.Parameters.Add("@jenisCurr", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisCurr")
        cmdA.Parameters.Add("@kurs", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtKurs")
        cmdA.Parameters.Add("@nilaiValas", SqlDbType.Decimal).Value = e.Form(master + "TxtNilaiValas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiRupiah", SqlDbType.Decimal).Value = e.Form(master + "TxtNilaiRp").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jangkaWaktu", SqlDbType.Int).Value = e.Form(master + "TxtJangkaWaktu")
        cmdA.Parameters.Add("@grace", SqlDbType.Int).Value = e.Form(master + "TxtGrace")
        cmdA.Parameters.Add("@bungaPersen", SqlDbType.Float).Value = e.Form(master + "TxtBunga")
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "RadBunga")
        cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtTotal").ToString().Replace(",", "")
        'cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = e.Form(master + "TxtNama")
        'cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtAlamat")
        cmdA.Parameters.Add("@peruntukan", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtPeruntukan")
        cmdA.Parameters.Add("@sifat", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSifat")
        'cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlTujuan")
        'cmdA.Parameters.Add("@rumah", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlHouse")
        'cmdA.Parameters.Add("@sektor", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlSektor")
        'cmdA.Parameters.Add("@subsektor", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlSubsektor")

        cmdA.Parameters.Add("@nilaiProyek", SqlDbType.Decimal).Value = e.Form(master + "TxtNilaiProyek").ToString().Replace(",", "")
        'cmdA.Parameters.AddWithValue("@avperiode", avperiode)
        cmdA.Parameters.AddWithValue("@avperiode", e.Form(master + "txtAvailibilityPeriode"))
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertFacilityCreditRr(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = AppNo
        'cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisPengajuan")
        cmdA.Parameters.Add("@fasilitas", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlFasilitas")
        cmdA.Parameters.Add("@jenisPembayaran", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSitemPembayaran")
        cmdA.Parameters.Add("@jenisCurr", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlJenisCurr")
        cmdA.Parameters.Add("@kurs", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtKurs")
        'cmdA.Parameters.Add("@nilaiValas", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtNilaiValas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiRupiah", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtNilaiRp").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jangkaWaktu", SqlDbType.Int).Value = e.Form(master + "TxtJangkaWaktu")
        cmdA.Parameters.Add("@grace", SqlDbType.Int).Value = e.Form(master + "TxtGrace")
        cmdA.Parameters.Add("@bungaPersen", SqlDbType.Float).Value = e.Form(master + "TxtBunga")
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "RadBunga")
        'cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 100).Value = e.Form(master + "TxtNama")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtAlamat")
        cmdA.Parameters.Add("@peruntukan", SqlDbType.VarChar, 500).Value = e.Form(master + "TxtPeruntukan")
        'cmdA.Parameters.Add("@sifat", SqlDbType.VarChar, 50).Value = e.Form(master + "DdlSifat")
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlTujuan")
        cmdA.Parameters.Add("@rumah", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlHouse")
        'cmdA.Parameters.Add("@sektor", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlSektor")
        'cmdA.Parameters.Add("@subsektor", SqlDbType.VarChar, 100).Value = e.Form(master + "DdlSubsektor")

        cmdA.Parameters.Add("@nilaiProyek", SqlDbType.VarChar, 50).Value = e.Form(master + "TxtNilaiProyek").ToString().Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityCredit(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteFacilityCredit(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_deleteFacilityCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDetailOfFacilityAgunan(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_getDetailFacilityAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub insertFacilityAgunan(ByVal e, ByVal AppNo)
        Dim tglTerbit As String = "1900-01-01"
        Dim tglKadaluarsa As String = "1900-01-01"
        Dim tglImb As String = "1900-01-01"
        If Not e.form("TxtTglTerbit") = "" Then
            tglTerbit = Mid(e.form("TxtTglTerbit"), 7, 4) + "-" & Mid(e.form("TxtTglTerbit"), 4, 2) + "-" & Mid(e.form("TxtTglTerbit"), 1, 2)
        End If
        If Not e.form("TxtTglKadaluarsa") = "" Then
            tglKadaluarsa = Mid(e.form("TxtTglKadaluarsa"), 7, 4) + "-" & Mid(e.form("TxtTglKadaluarsa"), 4, 2) + "-" & Mid(e.form("TxtTglKadaluarsa"), 1, 2)
        End If
        If Not e.form("TxtTglIMB") = "" Then
            tglImb = Mid(e.form("TxtTglIMB"), 7, 4) + "-" & Mid(e.form("TxtTglIMB"), 4, 2) + "-" & Mid(e.form("TxtTglIMB"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@control", SqlDbType.VarChar, 50).Value = e.Form("DdlControled")
        cmdA.Parameters.Add("@agunan", SqlDbType.VarChar, 50).Value = e.Form("DdlAgunan")
        cmdA.Parameters.Add("@pengikatan", SqlDbType.VarChar, 50).Value = e.Form("DdlPengikatan")
        cmdA.Parameters.Add("@deskripsi", SqlDbType.VarChar, 50).Value = e.Form("TxtDeskripsi")
        cmdA.Parameters.Add("@klasAgunan", SqlDbType.VarChar, 50).Value = e.Form("DdlKlasAgunan")
        cmdA.Parameters.Add("@ket", SqlDbType.VarChar, 50).Value = e.Form("TxtKeterangan")
        cmdA.Parameters.Add("@ketJaminan", SqlDbType.VarChar, 50).Value = e.Form("TxtKetJaminan")
        cmdA.Parameters.Add("@mataUang", SqlDbType.VarChar, 50).Value = e.Form("DdlMataUang")
        cmdA.Parameters.Add("@bukti", SqlDbType.VarChar, 50).Value = e.Form("DdlBukti")
        cmdA.Parameters.Add("@properti", SqlDbType.VarChar, 50).Value = e.Form("DdlProperty")
        cmdA.Parameters.Add("@noSertifikat", SqlDbType.VarChar, 50).Value = e.Form("TxtNoSertif")
        cmdA.Parameters.Add("@tglTerbit", SqlDbType.VarChar, 50).Value = tglTerbit
        cmdA.Parameters.Add("@tglKadaluarsa", SqlDbType.VarChar, 50).Value = tglKadaluarsa
        cmdA.Parameters.Add("@noImb", SqlDbType.VarChar, 50).Value = e.Form("TxtNoImb")
        cmdA.Parameters.Add("@tglImb", SqlDbType.VarChar, 50).Value = tglImb
        cmdA.Parameters.Add("@tanah", SqlDbType.VarChar, 50).Value = e.Form("TxtTanah")
        cmdA.Parameters.Add("@bangunan", SqlDbType.VarChar, 50).Value = e.Form("TxtBangunan")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.Form("TxtNama")
        cmdA.Parameters.Add("@hubungan", SqlDbType.VarChar, 50).Value = e.Form("DdlHubungan")
        cmdA.Parameters.Add("@developer", SqlDbType.VarChar, 50).Value = e.Form("DdlDev")
        cmdA.Parameters.Add("@nilaiJaminan", SqlDbType.VarChar, 50).Value = e.Form("TxtNilaiJaminan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasilNilai", SqlDbType.VarChar, 50).Value = e.Form("TxtHasilNilai").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiPasar", SqlDbType.VarChar, 50).Value = e.Form("TxtNilaiPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiAgunan", SqlDbType.VarChar, 50).Value = e.Form("TxtNilaiAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jalan", SqlDbType.VarChar, 50).Value = e.Form("TxtJalan")
        cmdA.Parameters.Add("@rt", SqlDbType.VarChar, 50).Value = e.Form("TxtRt")
        cmdA.Parameters.Add("@rw", SqlDbType.VarChar, 50).Value = e.Form("TxtRw")
        cmdA.Parameters.Add("@noRumah", SqlDbType.VarChar, 50).Value = e.Form("TxtNoRumah")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.Form("TxtKecamatan")
        cmdA.Parameters.Add("@lokasi", SqlDbType.VarChar, 50).Value = e.Form("DdlLokasi")
        cmdA.Parameters.Add("@penilaian", SqlDbType.VarChar, 50).Value = e.Form("DdlNilaiMenurut")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityAgunan(ByVal e, ByVal ID)
        Dim tglTerbit As String = "1900-01-01"
        Dim tglKadaluarsa As String = "1900-01-01"
        Dim tglImb As String = "1900-01-01"
        If Not e.form("TxtTglTerbit") = "" Then
            tglTerbit = Mid(e.form("TxtTglTerbit"), 7, 4) + "-" & Mid(e.form("TxtTglTerbit"), 4, 2) + "-" & Mid(e.form("TxtTglTerbit"), 1, 2)
        End If
        If Not e.form("TxtTglKadaluarsa") = "" Then
            tglKadaluarsa = Mid(e.form("TxtTglKadaluarsa"), 7, 4) + "-" & Mid(e.form("TxtTglKadaluarsa"), 4, 2) + "-" & Mid(e.form("TxtTglKadaluarsa"), 1, 2)
        End If
        If Not e.form("TxtTglIMB") = "" Then
            tglImb = Mid(e.form("TxtTglIMB"), 7, 4) + "-" & Mid(e.form("TxtTglIMB"), 4, 2) + "-" & Mid(e.form("TxtTglIMB"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@control", SqlDbType.VarChar, 50).Value = e.Form("DdlControled")
        cmdA.Parameters.Add("@agunan", SqlDbType.VarChar, 50).Value = e.Form("DdlAgunan")
        cmdA.Parameters.Add("@pengikatan", SqlDbType.VarChar, 50).Value = e.Form("DdlPengikatan")
        cmdA.Parameters.Add("@deskripsi", SqlDbType.VarChar, 50).Value = e.Form("TxtDeskripsi")
        cmdA.Parameters.Add("@klasAgunan", SqlDbType.VarChar, 50).Value = e.Form("DdlKlasAgunan")
        cmdA.Parameters.Add("@ket", SqlDbType.VarChar, 50).Value = e.Form("TxtKeterangan")
        cmdA.Parameters.Add("@ketJaminan", SqlDbType.VarChar, 50).Value = e.Form("TxtKetJaminan")
        cmdA.Parameters.Add("@mataUang", SqlDbType.VarChar, 50).Value = e.Form("DdlMataUang")
        cmdA.Parameters.Add("@bukti", SqlDbType.VarChar, 50).Value = e.Form("DdlBukti")
        cmdA.Parameters.Add("@properti", SqlDbType.VarChar, 50).Value = e.Form("DdlProperty")
        cmdA.Parameters.Add("@noSertifikat", SqlDbType.VarChar, 50).Value = e.Form("TxtNoSertif")
        cmdA.Parameters.Add("@tglTerbit", SqlDbType.Date).Value = tglTerbit
        cmdA.Parameters.Add("@tglKadaluarsa", SqlDbType.Date).Value = tglKadaluarsa
        cmdA.Parameters.Add("@noImb", SqlDbType.VarChar, 50).Value = e.Form("TxtNoImb")
        cmdA.Parameters.Add("@tglImb", SqlDbType.Date).Value = tglImb
        cmdA.Parameters.Add("@tanah", SqlDbType.VarChar, 50).Value = e.Form("TxtTanah")
        cmdA.Parameters.Add("@bangunan", SqlDbType.VarChar, 50).Value = e.Form("TxtBangunan")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.Form("TxtNama")
        cmdA.Parameters.Add("@hubungan", SqlDbType.VarChar, 50).Value = e.Form("DdlHubungan")
        cmdA.Parameters.Add("@developer", SqlDbType.VarChar, 50).Value = e.Form("DdlDev")
        cmdA.Parameters.Add("@nilaiJaminan", SqlDbType.VarChar, 50).Value = e.Form("TxtNilaiJaminan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasilNilai", SqlDbType.VarChar, 50).Value = e.Form("TxtHasilNilai").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiPasar", SqlDbType.VarChar, 50).Value = e.Form("TxtNilaiPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiAgunan", SqlDbType.VarChar, 50).Value = e.Form("TxtNilaiAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jalan", SqlDbType.VarChar, 50).Value = e.Form("TxtJalan")
        cmdA.Parameters.Add("@rt", SqlDbType.VarChar, 50).Value = e.Form("TxtRt")
        cmdA.Parameters.Add("@rw", SqlDbType.VarChar, 50).Value = e.Form("TxtRw")
        cmdA.Parameters.Add("@noRumah", SqlDbType.VarChar, 50).Value = e.Form("TxtNoRumah")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.Form("TxtKecamatan")
        cmdA.Parameters.Add("@lokasi", SqlDbType.VarChar, 50).Value = e.Form("DdlLokasi")
        cmdA.Parameters.Add("@penilaian", SqlDbType.VarChar, 50).Value = e.Form("DdlNilaiMenurut")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteFacilityAgunan(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_deleteFacilityAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityAgunan(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfFacilityAppraisal(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_getDetailFacilityAppraisal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub insertFacilityAppraisal(ByVal e, ByVal AguID)
        Dim tglPeriksa As String = "1900-01-01"
        Dim tglDok As String = "1900-01-01"
        Dim tglTempo As String = "1900-01-01"
        If Not e.form("tanggalPeriksa") = "" Then
            tglPeriksa = Mid(e.form("tanggalPeriksa"), 7, 4) + "-" & Mid(e.form("tanggalPeriksa"), 4, 2) + "-" & Mid(e.form("tanggalPeriksa"), 1, 2)
        End If
        If Not e.form("tanggalDokumen") = "" Then
            tglDok = Mid(e.form("tanggalDokumen"), 7, 4) + "-" & Mid(e.form("tanggalDokumen"), 4, 2) + "-" & Mid(e.form("tanggalDokumen"), 1, 2)
        End If
        If Not e.form("tempoDokumen") = "" Then
            tglTempo = Mid(e.form("tempoDokumen"), 7, 4) + "-" & Mid(e.form("tempoDokumen"), 4, 2) + "-" & Mid(e.form("tempoDokumen"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityAppraisal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agu_id", SqlDbType.BigInt).Value = AguID
        cmdA.Parameters.Add("@perusahaan", SqlDbType.VarChar, 50).Value = e.Form("perusahaan")
        cmdA.Parameters.Add("@noSurat", SqlDbType.VarChar, 50).Value = e.Form("noSurat")
        cmdA.Parameters.Add("@tanggalPeriksa", SqlDbType.Date).Value = tglPeriksa
        cmdA.Parameters.Add("@penilai1", SqlDbType.VarChar, 50).Value = e.Form("penilai1")
        cmdA.Parameters.Add("@jabatan1", SqlDbType.VarChar, 50).Value = e.Form("jabatan1")
        cmdA.Parameters.Add("@penilai2", SqlDbType.VarChar, 50).Value = e.Form("penilai2")
        cmdA.Parameters.Add("@jabatan2", SqlDbType.VarChar, 50).Value = e.Form("jabatan2")
        cmdA.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = e.Form("status")
        cmdA.Parameters.Add("@noBukti", SqlDbType.VarChar, 50).Value = e.Form("noBukti")
        cmdA.Parameters.Add("@tanggalDokumen", SqlDbType.Date).Value = tglDok
        cmdA.Parameters.Add("@tempoDokumen", SqlDbType.Date).Value = tglTempo
        cmdA.Parameters.Add("@taksasi", SqlDbType.VarChar, 50).Value = e.Form("taksasi")
        cmdA.Parameters.Add("@atasnama", SqlDbType.VarChar, 50).Value = e.Form("atasnama")
        cmdA.Parameters.Add("@lokasi", SqlDbType.VarChar, 50).Value = e.Form("lokasi")
        cmdA.Parameters.Add("@kondisi", SqlDbType.VarChar, 50).Value = e.Form("kondisi")
        cmdA.Parameters.Add("@noLpa", SqlDbType.VarChar, 50).Value = e.Form("noLpa")

        cmdA.Parameters.Add("@luasTanahSek", SqlDbType.VarChar, 50).Value = e.Form("luasTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@hargaTanahSek", SqlDbType.VarChar, 50).Value = e.Form("hargaTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@pasarTanahSek", SqlDbType.VarChar, 50).Value = e.Form("pasarTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@jualTanahSek", SqlDbType.VarChar, 50).Value = e.Form("jualTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@luasBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("luasBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@hargaBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("hargaBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@pasarBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("pasarBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@jualBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("jualBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@totalPasarSek", SqlDbType.VarChar, 50).Value = e.Form("totalPasarSek").Replace(",", "")
        cmdA.Parameters.Add("@totalJualSek", SqlDbType.VarChar, 50).Value = e.Form("totalJualSek").Replace(",", "")
        cmdA.Parameters.Add("@pasarLainSek", SqlDbType.VarChar, 50).Value = e.Form("pasarLainSek").Replace(",", "")
        cmdA.Parameters.Add("@jualLainSek", SqlDbType.VarChar, 50).Value = e.Form("jualLainSek").Replace(",", "")

        cmdA.Parameters.Add("@luasTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("luasTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@hargaTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("hargaTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@pasarTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("pasarTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@jualTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("jualTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@luasBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("luasBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@hargaBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("hargaBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@pasarBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("pasarBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@jualBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("jualBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@totalPasarBtn", SqlDbType.VarChar, 50).Value = e.Form("totalPasarBtn").Replace(",", "")
        cmdA.Parameters.Add("@totalJualBtn", SqlDbType.VarChar, 50).Value = e.Form("totalJualBtn").Replace(",", "")
        cmdA.Parameters.Add("@pasarLainBtn", SqlDbType.VarChar, 50).Value = e.Form("pasarLainBtn").Replace(",", "")
        cmdA.Parameters.Add("@jualLainBtn", SqlDbType.VarChar, 50).Value = e.Form("jualLainBtn").Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityAppraisal(ByVal e, ByVal AguID)
        Dim tglPeriksa As String = "1900-01-01"
        Dim tglDok As String = "1900-01-01"
        Dim tglTempo As String = "1900-01-01"
        If Not e.form("tanggalPeriksa") = "" Then
            tglPeriksa = Mid(e.form("tanggalPeriksa"), 7, 4) + "-" & Mid(e.form("tanggalPeriksa"), 4, 2) + "-" & Mid(e.form("tanggalPeriksa"), 1, 2)
        End If
        If Not e.form("tanggalDokumen") = "" Then
            tglDok = Mid(e.form("tanggalDokumen"), 7, 4) + "-" & Mid(e.form("tanggalDokumen"), 4, 2) + "-" & Mid(e.form("tanggalDokumen"), 1, 2)
        End If
        If Not e.form("tempoDokumen") = "" Then
            tglTempo = Mid(e.form("tempoDokumen"), 7, 4) + "-" & Mid(e.form("tempoDokumen"), 4, 2) + "-" & Mid(e.form("tempoDokumen"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityAppraisal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@aguid", SqlDbType.BigInt).Value = AguID
        cmdA.Parameters.Add("@perusahaan", SqlDbType.VarChar, 50).Value = e.Form("perusahaan")
        cmdA.Parameters.Add("@noSurat", SqlDbType.VarChar, 50).Value = e.Form("noSurat")
        cmdA.Parameters.Add("@tanggalPeriksa", SqlDbType.Date).Value = tglPeriksa
        cmdA.Parameters.Add("@penilai1", SqlDbType.VarChar, 50).Value = e.Form("penilai1")
        cmdA.Parameters.Add("@jabatan1", SqlDbType.VarChar, 50).Value = e.Form("jabatan1")
        cmdA.Parameters.Add("@penilai2", SqlDbType.VarChar, 50).Value = e.Form("penilai2")
        cmdA.Parameters.Add("@jabatan2", SqlDbType.VarChar, 50).Value = e.Form("jabatan2")
        cmdA.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = e.Form("status")
        cmdA.Parameters.Add("@noBukti", SqlDbType.VarChar, 50).Value = e.Form("noBukti")
        cmdA.Parameters.Add("@tanggalDokumen", SqlDbType.Date).Value = tglDok
        cmdA.Parameters.Add("@tempoDokumen", SqlDbType.Date).Value = tglTempo
        cmdA.Parameters.Add("@taksasi", SqlDbType.VarChar, 50).Value = e.Form("taksasi")
        cmdA.Parameters.Add("@atasnama", SqlDbType.VarChar, 50).Value = e.Form("atasnama")
        cmdA.Parameters.Add("@lokasi", SqlDbType.VarChar, 50).Value = e.Form("lokasi")
        cmdA.Parameters.Add("@kondisi", SqlDbType.VarChar, 50).Value = e.Form("kondisi")
        cmdA.Parameters.Add("@noLpa", SqlDbType.VarChar, 50).Value = e.Form("noLpa")

        cmdA.Parameters.Add("@luasTanahSek", SqlDbType.VarChar, 50).Value = e.Form("luasTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@hargaTanahSek", SqlDbType.VarChar, 50).Value = e.Form("hargaTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@pasarTanahSek", SqlDbType.VarChar, 50).Value = e.Form("pasarTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@jualTanahSek", SqlDbType.VarChar, 50).Value = e.Form("jualTanahSek").Replace(",", "")
        cmdA.Parameters.Add("@luasBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("luasBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@hargaBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("hargaBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@pasarBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("pasarBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@jualBangunanSek", SqlDbType.VarChar, 50).Value = e.Form("jualBangunanSek").Replace(",", "")
        cmdA.Parameters.Add("@totalPasarSek", SqlDbType.VarChar, 50).Value = e.Form("totalPasarSek").Replace(",", "")
        cmdA.Parameters.Add("@totalJualSek", SqlDbType.VarChar, 50).Value = e.Form("totalJualSek").Replace(",", "")
        cmdA.Parameters.Add("@pasarLainSek", SqlDbType.VarChar, 50).Value = e.Form("pasarLainSek").Replace(",", "")
        cmdA.Parameters.Add("@jualLainSek", SqlDbType.VarChar, 50).Value = e.Form("jualLainSek").Replace(",", "")

        cmdA.Parameters.Add("@luasTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("luasTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@hargaTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("hargaTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@pasarTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("pasarTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@jualTanahBtn", SqlDbType.VarChar, 50).Value = e.Form("jualTanahBtn").Replace(",", "")
        cmdA.Parameters.Add("@luasBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("luasBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@hargaBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("hargaBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@pasarBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("pasarBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@jualBangunanBtn", SqlDbType.VarChar, 50).Value = e.Form("jualBangunanBtn").Replace(",", "")
        cmdA.Parameters.Add("@totalPasarBtn", SqlDbType.VarChar, 50).Value = e.Form("totalPasarBtn").Replace(",", "")
        cmdA.Parameters.Add("@totalJualBtn", SqlDbType.VarChar, 50).Value = e.Form("totalJualBtn").Replace(",", "")
        cmdA.Parameters.Add("@pasarLainBtn", SqlDbType.VarChar, 50).Value = e.Form("pasarLainBtn").Replace(",", "")
        cmdA.Parameters.Add("@jualLainBtn", SqlDbType.VarChar, 50).Value = e.Form("jualLainBtn").Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityAppraisal(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityAppraisal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertFacilityCreditAnalyze(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCreditAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@app_no", SqlDbType.BigInt).Value = AppNo
        cmdA.Parameters.Add("@tingkatJual", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTingkatJual").ToString().Replace(",", "")
        cmdA.Parameters.Add("@peningkatan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeningkatan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@perPeningkatan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeningkatan1")
        cmdA.Parameters.Add("@valPeningkatan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeningkatan2").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@aktiva", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAktiva").ToString().Replace(",", "")
        cmdA.Parameters.Add("@longgar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLonggar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pembayaran", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPembayaran").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahKebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlahKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahKmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlahKmk").ToString().Replace(",", "")
        cmdA.Parameters.Add("@bentukKmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBentukKmk")
        cmdA.Parameters.Add("@totalNilai", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalLonggar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiResult", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSumNilai").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proporsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProporsi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@perProporsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProporsi1")
        cmdA.Parameters.Add("@valProporsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProporsi2").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penghasilan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenghasilan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@angsuranExt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuranExt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penghasilanPotong", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenghasilanPotong").ToString().Replace(",", "")
        cmdA.Parameters.Add("@angsuranLunas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuranLunas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@repay", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRepay").ToString().Replace(",", "")
        cmdA.Parameters.Add("@perKmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKmk1")
        cmdA.Parameters.Add("@kmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKmk").ToString().Replace(",", "")
        cmdA.Parameters.Add("@max", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMax").ToString().Replace(",", "")
        cmdA.Parameters.Add("@bunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasil", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHasil").ToString().Replace(",", "")
        cmdA.Parameters.Add("@usul", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUsul").ToString().Replace(",", "")
        cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@debt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDebt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rc", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRc").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ratio", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penjualan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataKasBank", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRataKasBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPiutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRataPiutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPersediaan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRataPersediaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahHari", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlahHari").ToString()
        cmdA.Parameters.Add("@kasBank", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKasBank").ToString()
        cmdA.Parameters.Add("@piutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPiutang").ToString()
        cmdA.Parameters.Add("@persediaan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPersediaan").ToString()
        cmdA.Parameters.Add("@lama", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLama").ToString()
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisBunga")
        cmdA.Parameters.Add("@angsuranPB", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuranPB").ToString().Replace(",", "")
        cmdA.Parameters.Add("@laporan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLaporan").ToString()
        cmdA.Parameters.Add("@periode", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeriode").ToString()

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditAnalyze(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityCreditAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@app_no", SqlDbType.BigInt).Value = AppNo
        cmdA.Parameters.Add("@tingkatJual", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTingkatJual").ToString().Replace(",", "")
        cmdA.Parameters.Add("@peningkatan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeningkatan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@perPeningkatan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeningkatan1")
        cmdA.Parameters.Add("@valPeningkatan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeningkatan2").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@aktiva", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAktiva").ToString().Replace(",", "")
        cmdA.Parameters.Add("@longgar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLonggar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pembayaran", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPembayaran").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahKebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlahKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahKmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlahKmk").ToString().Replace(",", "")
        cmdA.Parameters.Add("@bentukKmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBentukKmk")
        cmdA.Parameters.Add("@totalNilai", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalLonggar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nilaiResult", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSumNilai").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proporsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProporsi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@perProporsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProporsi1")
        cmdA.Parameters.Add("@valProporsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProporsi2").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penghasilan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenghasilan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@angsuranExt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuranExt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penghasilanPotong", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenghasilanPotong").ToString().Replace(",", "")
        cmdA.Parameters.Add("@angsuranLunas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuranLunas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@repay", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRepay").ToString().Replace(",", "")
        cmdA.Parameters.Add("@perKmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKmk1")
        cmdA.Parameters.Add("@kmk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKmk").ToString().Replace(",", "")
        cmdA.Parameters.Add("@max", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMax").ToString().Replace(",", "")
        cmdA.Parameters.Add("@bunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasil", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHasil").ToString().Replace(",", "")
        cmdA.Parameters.Add("@usul", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUsul").ToString().Replace(",", "")
        cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@debt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDebt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rc", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRc").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ratio", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penjualan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataKasBank", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRataKasBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPiutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRataPiutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPersediaan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRataPersediaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahHari", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlahHari").ToString()
        cmdA.Parameters.Add("@kasBank", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKasBank").ToString()
        cmdA.Parameters.Add("@piutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPiutang").ToString()
        cmdA.Parameters.Add("@persediaan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPersediaan").ToString()
        cmdA.Parameters.Add("@lama", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLama").ToString()
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisBunga")
        cmdA.Parameters.Add("@angsuranPB", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuranPB").ToString().Replace(",", "")
        cmdA.Parameters.Add("@laporan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLaporan").ToString()
        cmdA.Parameters.Add("@periode", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPeriode").ToString()

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityCreditAnalyze(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityCreditAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    'new
    Function checkPLBSYear(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkPLBSYear", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkBSPrevYear(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkBSPrevYear", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkBSThisYear(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkBSThisYear", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub insertFacilityCreditInvestment(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCreditInvestment", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@proyek", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyek").ToString().Replace(",", "")
        cmdA.Parameters.Add("@self", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSelf").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlah").ToString().Replace(",", "")

        cmdA.Parameters.Add("@kredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@laba", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLaba").ToString().Replace(",", "")
        cmdA.Parameters.Add("@labaPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLabaPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@potongan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPotongan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@dipotong", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDipotong").ToString().Replace(",", "")
        cmdA.Parameters.Add("@angsuran", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuran").ToString().Replace(",", "")
        cmdA.Parameters.Add("@disponable", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDisponable").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisBunga")
        cmdA.Parameters.Add("@bunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@diangsur", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDiangsur").ToString().Replace(",", "")
        cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotal").ToString().Replace(",", "")

        cmdA.Parameters.Add("@perLaba", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPerLaba").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditInvestment(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityCreditInvestment", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.BigInt).Value = AppNo
        cmdA.Parameters.Add("@proyek", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyek").ToString().Replace(",", "")
        cmdA.Parameters.Add("@self", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSelf").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJumlah").ToString().Replace(",", "")

        cmdA.Parameters.Add("@kredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@laba", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLaba").ToString().Replace(",", "")
        cmdA.Parameters.Add("@labaPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLabaPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@potongan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPotongan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@dipotong", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDipotong").ToString().Replace(",", "")
        cmdA.Parameters.Add("@angsuran", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAngsuran").ToString().Replace(",", "")
        cmdA.Parameters.Add("@disponable", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDisponable").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisBunga")
        cmdA.Parameters.Add("@bunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@diangsur", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDiangsur").ToString().Replace(",", "")
        cmdA.Parameters.Add("@total", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotal").ToString().Replace(",", "")

        cmdA.Parameters.Add("@perLaba", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPerLaba").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityCreditInvestment(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityCreditInvestment", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDropDownList(ByVal Obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("los_dropdownlist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@object", SqlDbType.VarChar, 50).Value = Obj
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getRatingAgency()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_getRatingAgency", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getRating(ByVal Agency)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_getRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 50).Value = Agency
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkExRatingInfo(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertExRatingInfo(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglRating As String = "1900-01-01"
        If Not e.form(master + "InpTglRating") = "" Then
            tglRating = Mid(e.form(master + "InpTglRating"), 7, 4) + "-" & Mid(e.form(master + "InpTglRating"), 4, 2) + "-" & Mid(e.form(master + "InpTglRating"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@ratingAgency", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatingAgency")
        cmdA.Parameters.Add("@corpType", SqlDbType.VarChar, 50).Value = e.Form(master + "InpCorp")
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRating")
        cmdA.Parameters.AddWithValue("@rating", e.Form(master + "InpRating"))
        cmdA.Parameters.Add("@tglRating", SqlDbType.Date).Value = tglRating
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateExRatingInfo(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglRating As String = "1900-01-01"
        If Not e.form(master + "InpTglRating") = "" Then
            tglRating = Mid(e.form(master + "InpTglRating"), 7, 4) + "-" & Mid(e.form(master + "InpTglRating"), 4, 2) + "-" & Mid(e.form(master + "InpTglRating"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@ratingAgency", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatingAgency")
        cmdA.Parameters.Add("@corpType", SqlDbType.VarChar, 50).Value = e.Form(master + "InpCorp")
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRating")
        cmdA.Parameters.AddWithValue("@rating", e.Form(master + "InpRating"))
        cmdA.Parameters.Add("@tglRating", SqlDbType.Date).Value = tglRating
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityRating(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertFacilityRatingInfo(ByVal AppNo, ByVal Score, ByVal Rating, ByVal RatingDate, ByVal NextRating, ByVal usr, ByVal scc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@score", SqlDbType.Float).Value = Score
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = Rating
        cmdA.Parameters.AddWithValue("@rating", Rating)
        cmdA.Parameters.Add("@ratingDate", SqlDbType.Date).Value = RatingDate
        cmdA.Parameters.Add("@nextRatingDate", SqlDbType.Date).Value = NextRating
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@scc", SqlDbType.VarChar, 50).Value = scc
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListOfFullMenu()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfFullMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    '================================== TAMBAHAN RIMA ==============================

    Function getAppNo(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getAppNo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkAppByBrwcode(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkAppByBrwcode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBorrowerCoouple(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkBorrowerCouple", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertBorrowerCoupleInfo(ByVal e, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertBorrowerCoupleInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.nama
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.tempatLahir
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = CheckStringNull(e.tglLahir)
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.noKTP
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = CheckStringNull(e.tglLahir)
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.gelarDepan
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.gelarBelakang
        cmdA.Parameters.Add("@pekerjaan", SqlDbType.VarChar, 50).Value = e.pekerjaan
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateBorrowerCoupleInfo(ByVal e, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateBorrowerCoupleInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.nama
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.tempatLahir
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = CheckStringNull(e.tglLahir)
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.noKTP
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = CheckStringNull(e.tglKTP)
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.gelarDepan
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.gelarBelakang
        cmdA.Parameters.Add("@pekerjaan", SqlDbType.VarChar, 50).Value = e.pekerjaan
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListOfMenu(ByVal typ, ByVal prod)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@typ1", SqlDbType.VarChar, 50).Value = typ
        cmdA.Parameters.Add("@prod", SqlDbType.VarChar, 50).Value = prod
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getNextMenu(ByVal pgname, ByVal prod)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getNextMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@pgname", SqlDbType.VarChar, 50).Value = pgname
        cmdA.Parameters.Add("@prod", SqlDbType.VarChar, 50).Value = prod
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getChildOfMenu(ByVal id, ByVal prod)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getChildOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@prod", SqlDbType.VarChar, 50).Value = prod
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getDetailOfMenu(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkApp(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkAppInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBorrowerByCIF(ByVal cif)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkBorrowerByCIF", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = cif
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBorrower(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkBorrowerInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertBorrowerInfo(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertBorrowerInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = obj.brwcode
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = obj.noCIF
        cmdA.Parameters.Add("@jenisDeb", SqlDbType.VarChar, 50).Value = obj.jenisDebitur
        cmdA.Parameters.Add("@namaPemohon", SqlDbType.VarChar, 50).Value = obj.namaPemohon
        cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 10).Value = obj.jenisKelamin
        cmdA.Parameters.Add("@pekerjaan", SqlDbType.VarChar, 50).Value = obj.pekerjaan
        cmdA.Parameters.Add("@statusKawin", SqlDbType.VarChar, 50).Value = obj.statusPerkawinan
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = obj.NPWPPemohon
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = obj.tglLahir
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = obj.tempatLahir
        cmdA.Parameters.Add("@namaGadisIbu", SqlDbType.VarChar, 50).Value = obj.namaGadisIbuKandung
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = obj.gelarDepan
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = obj.gelarBelakang
        cmdA.Parameters.Add("@statusGelar", SqlDbType.VarChar, 50).Value = obj.statusGelar
        cmdA.Parameters.Add("@ketStatusGelar", SqlDbType.VarChar, 50).Value = obj.ketStatusGelar
        cmdA.Parameters.Add("@noKTPAkte", SqlDbType.VarChar, 50).Value = obj.noKTPAkte
        cmdA.Parameters.Add("@tglKTPAkte", SqlDbType.Date).Value = obj.tglPembuatanKTPAkte
        cmdA.Parameters.Add("@deskripsi", SqlDbType.Text).Value = obj.deskripsi
        cmdA.Parameters.Add("@daerahPemasaran", SqlDbType.VarChar, 500).Value = obj.daerahPemasaran
        cmdA.Parameters.Add("@sektorEkonomi", SqlDbType.VarChar, 50).Value = obj.sektorEkonomi
        cmdA.Parameters.Add("@sasaranPembeli", SqlDbType.VarChar, 500).Value = obj.sasaranPembeli
        cmdA.Parameters.Add("@jmlKaryawan", SqlDbType.VarChar, 50).Value = obj.jumlahKaryawan

        cmdA.Parameters.Add("@alamatRmh", SqlDbType.VarChar, 500).Value = obj.alamatRumah
        cmdA.Parameters.Add("@kodePosRmh", SqlDbType.VarChar, 20).Value = obj.kodePosRumah
        cmdA.Parameters.Add("@kelRmh", SqlDbType.VarChar, 50).Value = obj.kelurahanRumah
        cmdA.Parameters.Add("@kecRmh", SqlDbType.VarChar, 50).Value = obj.kecamatanRumah
        cmdA.Parameters.Add("@kabRmh", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenKantor
        cmdA.Parameters.Add("@propRmh", SqlDbType.VarChar, 50).Value = obj.propinsiRumah
        cmdA.Parameters.Add("@kodeAreaTlpRmh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpRumah
        cmdA.Parameters.Add("@tlpRmh", SqlDbType.VarChar, 20).Value = obj.noTlpRumah
        cmdA.Parameters.Add("@extTlpRmh", SqlDbType.VarChar, 20).Value = obj.noExtTlpRumah
        cmdA.Parameters.Add("@kodeAreaTlpRmh2", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpRumah2
        cmdA.Parameters.Add("@tlpRmh2", SqlDbType.VarChar, 20).Value = obj.noTlpRumah2
        cmdA.Parameters.Add("@extTlpRmh2", SqlDbType.VarChar, 20).Value = obj.noExtTlpRumah2
        cmdA.Parameters.Add("@kodeAreaTlpRmh3", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpRumah3
        cmdA.Parameters.Add("@tlpRmh3", SqlDbType.VarChar, 20).Value = obj.noTlpRumah3
        cmdA.Parameters.Add("@extTlpRmh3", SqlDbType.VarChar, 20).Value = obj.noExtTlpRumah3
        cmdA.Parameters.Add("@kodeAreaFaxRmh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaFaxRumah
        cmdA.Parameters.Add("@faxRmh", SqlDbType.VarChar, 20).Value = obj.noTlpFaxRumah
        cmdA.Parameters.Add("@extFaxRmh", SqlDbType.VarChar, 20).Value = obj.noExtFaxRumah

        cmdA.Parameters.Add("@namaUsh", SqlDbType.VarChar, 50).Value = obj.namaUsaha
        cmdA.Parameters.Add("@alamatUsh", SqlDbType.VarChar, 500).Value = obj.alamatUsaha
        cmdA.Parameters.Add("@kodePosUsh", SqlDbType.VarChar, 20).Value = obj.kodePosUsaha
        cmdA.Parameters.Add("@kelUsh", SqlDbType.VarChar, 50).Value = obj.kelurahanUsaha
        cmdA.Parameters.Add("@kecUsh", SqlDbType.VarChar, 50).Value = obj.kecamatanUsaha
        cmdA.Parameters.Add("@kabUsh", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenUsaha
        cmdA.Parameters.Add("@propUsh", SqlDbType.VarChar, 50).Value = obj.propinsiUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha
        cmdA.Parameters.Add("@tlpUsh", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha
        cmdA.Parameters.Add("@extTlpUsh", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsh2", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha2
        cmdA.Parameters.Add("@tlpUsh2", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha2
        cmdA.Parameters.Add("@extTlpUsh2", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha2
        cmdA.Parameters.Add("@kodeAreaTlpUsh3", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha3
        cmdA.Parameters.Add("@tlpUsh3", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha3
        cmdA.Parameters.Add("@extTlpUsh3", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha3
        cmdA.Parameters.Add("@kodeAreaFaxUsh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaFaxUsaha
        cmdA.Parameters.Add("@faxUsh", SqlDbType.VarChar, 20).Value = obj.noTlpFaxUsaha
        cmdA.Parameters.Add("@extFaxUsh", SqlDbType.VarChar, 20).Value = obj.noExtFaxUsaha
        cmdA.Parameters.Add("@lamaUsh", SqlDbType.VarChar, 50).Value = obj.lamaUsaha

        cmdA.Parameters.Add("@alamatSrt", SqlDbType.VarChar, 500).Value = obj.alamatSurat
        cmdA.Parameters.Add("@kodePosSrt", SqlDbType.VarChar, 20).Value = obj.kodePosSurat
        cmdA.Parameters.Add("@kelSrt", SqlDbType.VarChar, 50).Value = obj.kelurahanSurat
        cmdA.Parameters.Add("@kecSrt", SqlDbType.VarChar, 50).Value = obj.kecamatanSurat
        cmdA.Parameters.Add("@kabSrt", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenSurat
        cmdA.Parameters.Add("@sandiBI", SqlDbType.VarChar, 50).Value = obj.sandiLokasiBI
        cmdA.Parameters.Add("@propSrt", SqlDbType.VarChar, 50).Value = obj.propinsiSurat
        cmdA.Parameters.Add("@kodeAreaTlpSrt", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpSurat
        cmdA.Parameters.Add("@tlpSrt", SqlDbType.VarChar, 20).Value = obj.noTlpSurat
        cmdA.Parameters.Add("@extTlpSrt", SqlDbType.VarChar, 20).Value = obj.noExtTlpSurat
        cmdA.Parameters.Add("@emailSrt", SqlDbType.VarChar, 50).Value = obj.emailSurat

        cmdA.Parameters.Add("@debSejak", SqlDbType.Date).Value = obj.menjadiDebiturSejak
        cmdA.Parameters.Add("@thnHubBank", SqlDbType.VarChar, 4).Value = obj.tahunHubunganDenganBTN
        cmdA.Parameters.Add("@blnHubBank", SqlDbType.VarChar, 2).Value = obj.bulanHubunganDenganBTN
        cmdA.Parameters.Add("@noPermohonanDeb", SqlDbType.VarChar, 50).Value = obj.noPermohonanDebitur
        cmdA.Parameters.Add("@tglPermohonanDeb", SqlDbType.Date).Value = obj.tglPermohonanDebitur
        cmdA.Parameters.Add("@tglPenerimaanBerkas", SqlDbType.Date).Value = obj.tglPenerimaanBerkas

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateBorrowerInfo(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateBorrowerInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = obj.brwcode
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = obj.noCIF
        cmdA.Parameters.Add("@jenisDeb", SqlDbType.VarChar, 50).Value = obj.jenisDebitur
        cmdA.Parameters.Add("@namaPemohon", SqlDbType.VarChar, 50).Value = obj.namaPemohon
        cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 10).Value = obj.jenisKelamin
        cmdA.Parameters.Add("@pekerjaan", SqlDbType.VarChar, 50).Value = obj.pekerjaan
        cmdA.Parameters.Add("@statusKawin", SqlDbType.VarChar, 50).Value = obj.statusPerkawinan
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = obj.NPWPPemohon
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = obj.tglLahir
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = obj.tempatLahir
        cmdA.Parameters.Add("@namaGadisIbu", SqlDbType.VarChar, 50).Value = obj.namaGadisIbuKandung
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = obj.gelarDepan
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = obj.gelarBelakang
        cmdA.Parameters.Add("@statusGelar", SqlDbType.VarChar, 50).Value = obj.statusGelar
        cmdA.Parameters.Add("@ketStatusGelar", SqlDbType.VarChar, 50).Value = obj.ketStatusGelar
        cmdA.Parameters.Add("@noKTPAkte", SqlDbType.VarChar, 50).Value = obj.noKTPAkte
        cmdA.Parameters.Add("@tglKTPAkte", SqlDbType.Date).Value = obj.tglPembuatanKTPAkte
        cmdA.Parameters.Add("@deskripsi", SqlDbType.Text).Value = obj.deskripsi
        cmdA.Parameters.Add("@daerahPemasaran", SqlDbType.VarChar, 500).Value = obj.daerahPemasaran
        cmdA.Parameters.Add("@sektorEkonomi", SqlDbType.VarChar, 50).Value = obj.sektorEkonomi
        cmdA.Parameters.Add("@sasaranPembeli", SqlDbType.VarChar, 500).Value = obj.sasaranPembeli
        cmdA.Parameters.Add("@jmlKaryawan", SqlDbType.VarChar, 50).Value = obj.jumlahKaryawan

        cmdA.Parameters.Add("@alamatRmh", SqlDbType.VarChar, 500).Value = obj.alamatRumah
        cmdA.Parameters.Add("@kodePosRmh", SqlDbType.VarChar, 20).Value = obj.kodePosRumah
        cmdA.Parameters.Add("@kelRmh", SqlDbType.VarChar, 50).Value = obj.kelurahanRumah
        cmdA.Parameters.Add("@kecRmh", SqlDbType.VarChar, 50).Value = obj.kecamatanRumah
        cmdA.Parameters.Add("@kabRmh", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenKantor
        cmdA.Parameters.Add("@propRmh", SqlDbType.VarChar, 50).Value = obj.propinsiRumah
        cmdA.Parameters.Add("@kodeAreaTlpRmh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpRumah
        cmdA.Parameters.Add("@tlpRmh", SqlDbType.VarChar, 20).Value = obj.noTlpRumah
        cmdA.Parameters.Add("@extTlpRmh", SqlDbType.VarChar, 20).Value = obj.noExtTlpRumah
        cmdA.Parameters.Add("@kodeAreaTlpRmh2", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpRumah2
        cmdA.Parameters.Add("@tlpRmh2", SqlDbType.VarChar, 20).Value = obj.noTlpRumah2
        cmdA.Parameters.Add("@extTlpRmh2", SqlDbType.VarChar, 20).Value = obj.noExtTlpRumah2
        cmdA.Parameters.Add("@kodeAreaTlpRmh3", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpRumah3
        cmdA.Parameters.Add("@tlpRmh3", SqlDbType.VarChar, 20).Value = obj.noTlpRumah3
        cmdA.Parameters.Add("@extTlpRmh3", SqlDbType.VarChar, 20).Value = obj.noExtTlpRumah3
        cmdA.Parameters.Add("@kodeAreaFaxRmh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaFaxRumah
        cmdA.Parameters.Add("@faxRmh", SqlDbType.VarChar, 20).Value = obj.noTlpFaxRumah
        cmdA.Parameters.Add("@extFaxRmh", SqlDbType.VarChar, 20).Value = obj.noExtFaxRumah

        cmdA.Parameters.Add("@namaUsh", SqlDbType.VarChar, 50).Value = obj.namaUsaha
        cmdA.Parameters.Add("@alamatUsh", SqlDbType.VarChar, 500).Value = obj.alamatUsaha
        cmdA.Parameters.Add("@kodePosUsh", SqlDbType.VarChar, 20).Value = obj.kodePosUsaha
        cmdA.Parameters.Add("@kelUsh", SqlDbType.VarChar, 50).Value = obj.kelurahanUsaha
        cmdA.Parameters.Add("@kecUsh", SqlDbType.VarChar, 50).Value = obj.kecamatanUsaha
        cmdA.Parameters.Add("@kabUsh", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenUsaha
        cmdA.Parameters.Add("@propUsh", SqlDbType.VarChar, 50).Value = obj.propinsiUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha
        cmdA.Parameters.Add("@tlpUsh", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha
        cmdA.Parameters.Add("@extTlpUsh", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsh2", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha2
        cmdA.Parameters.Add("@tlpUsh2", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha2
        cmdA.Parameters.Add("@extTlpUsh2", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha2
        cmdA.Parameters.Add("@kodeAreaTlpUsh3", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha3
        cmdA.Parameters.Add("@tlpUsh3", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha3
        cmdA.Parameters.Add("@extTlpUsh3", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha3
        cmdA.Parameters.Add("@kodeAreaFaxUsh", SqlDbType.VarChar, 10).Value = obj.noKodeAreaFaxUsaha
        cmdA.Parameters.Add("@faxUsh", SqlDbType.VarChar, 20).Value = obj.noTlpFaxUsaha
        cmdA.Parameters.Add("@extFaxUsh", SqlDbType.VarChar, 20).Value = obj.noExtFaxUsaha
        cmdA.Parameters.Add("@lamaUsh", SqlDbType.VarChar, 50).Value = obj.lamaUsaha

        cmdA.Parameters.Add("@alamatSrt", SqlDbType.VarChar, 500).Value = obj.alamatSurat
        cmdA.Parameters.Add("@kodePosSrt", SqlDbType.VarChar, 20).Value = obj.kodePosSurat
        cmdA.Parameters.Add("@kelSrt", SqlDbType.VarChar, 50).Value = obj.kelurahanSurat
        cmdA.Parameters.Add("@kecSrt", SqlDbType.VarChar, 50).Value = obj.kecamatanSurat
        cmdA.Parameters.Add("@kabSrt", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenSurat
        cmdA.Parameters.Add("@sandiBI", SqlDbType.VarChar, 50).Value = obj.sandiLokasiBI
        cmdA.Parameters.Add("@propSrt", SqlDbType.VarChar, 50).Value = obj.propinsiSurat
        cmdA.Parameters.Add("@kodeAreaTlpSrt", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpSurat
        cmdA.Parameters.Add("@tlpSrt", SqlDbType.VarChar, 20).Value = obj.noTlpSurat
        cmdA.Parameters.Add("@extTlpSrt", SqlDbType.VarChar, 20).Value = obj.noExtTlpSurat
        cmdA.Parameters.Add("@emailSrt", SqlDbType.VarChar, 250).Value = obj.emailSurat

        cmdA.Parameters.Add("@debSejak", SqlDbType.Date).Value = obj.menjadiDebiturSejak
        cmdA.Parameters.Add("@thnHubBank", SqlDbType.VarChar, 4).Value = obj.tahunHubunganDenganBTN
        cmdA.Parameters.Add("@blnHubBank", SqlDbType.VarChar, 2).Value = obj.bulanHubunganDenganBTN
        cmdA.Parameters.Add("@noPermohonanDeb", SqlDbType.VarChar, 50).Value = obj.noPermohonanDebitur
        cmdA.Parameters.Add("@tglPermohonanDeb", SqlDbType.Date).Value = obj.tglPermohonanDebitur
        cmdA.Parameters.Add("@tglPenerimaanBerkas", SqlDbType.Date).Value = obj.tglPenerimaanBerkas

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkGeneral(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkGeneralInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkGeneralBrw(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkGeneralInfoBrw", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwCode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub updateGeneralInfo(ByVal e)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateGeneralInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = e.noApp
        cmdA.Parameters.Add("@kantorPusat", SqlDbType.VarChar, 50).Value = e.kantorPusat
        cmdA.Parameters.Add("@spoke", SqlDbType.VarChar, 50).Value = e.spoke
        cmdA.Parameters.Add("@RM", SqlDbType.VarChar, 50).Value = e.rm
        cmdA.Parameters.Add("@omzet", SqlDbType.Decimal).Value = e.omzet
        cmdA.Parameters.Add("@program", SqlDbType.VarChar, 50).Value = e.program
        cmdA.Parameters.Add("@channels", SqlDbType.VarChar, 50).Value = e.channels
        cmdA.Parameters.Add("@segment", SqlDbType.VarChar, 50).Value = e.segment
        cmdA.Parameters.Add("@jenisKeuangan", SqlDbType.VarChar, 50).Value = e.jenisKeuangan
        cmdA.Parameters.Add("@kantorCabang", SqlDbType.VarChar, 50).Value = e.kantorCabang
        cmdA.Parameters.Add("@brwTyp", SqlDbType.VarChar, 50).Value = e.brwScc
        cmdA.Parameters.Add("@prdTyp", SqlDbType.VarChar, 50).Value = e.prdScc

        cmdA.Parameters.Add("@badanUsaha", SqlDbType.VarChar, 50).Value = e.badanUsaha
        cmdA.Parameters.Add("@plafon", SqlDbType.VarChar, 50).Value = e.plafon
        cmdA.Parameters.Add("@bakiDebit", SqlDbType.VarChar, 50).Value = e.bakiDebit
        cmdA.Parameters.Add("@kelonggaran", SqlDbType.VarChar, 50).Value = e.kelonggaranTarik
        cmdA.Parameters.Add("@tenor", SqlDbType.VarChar, 50).Value = e.tenor
        cmdA.Parameters.Add("@tglRek", SqlDbType.Date).Value = e.tglPembukaanRek
        cmdA.Parameters.Add("@noRek", SqlDbType.VarChar, 50).Value = e.noRek
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAppInfo(ByVal appno, ByVal brwcode, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateAppInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertGeneralInfo(ByVal e)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertGeneralInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = e.noApp
        cmdA.Parameters.Add("@tglApp", SqlDbType.Date).Value = e.tglApp
        cmdA.Parameters.Add("@kantorPusat", SqlDbType.VarChar, 50).Value = e.kantorPusat
        cmdA.Parameters.Add("@spoke", SqlDbType.VarChar, 50).Value = e.spoke
        cmdA.Parameters.Add("@RM", SqlDbType.VarChar, 50).Value = e.rm
        cmdA.Parameters.Add("@omzet", SqlDbType.Decimal).Value = e.omzet
        cmdA.Parameters.Add("@program", SqlDbType.VarChar, 50).Value = e.program
        cmdA.Parameters.Add("@channels", SqlDbType.VarChar, 50).Value = e.channels
        cmdA.Parameters.Add("@segment", SqlDbType.VarChar, 50).Value = e.segment
        'tambahan
        cmdA.Parameters.Add("@jenisKeuangan", SqlDbType.VarChar, 50).Value = e.jenisKeuangan
        cmdA.Parameters.Add("@kantorCabang", SqlDbType.VarChar, 50).Value = e.kantorCabang

        cmdA.Parameters.Add("@brwTyp", SqlDbType.VarChar, 50).Value = e.brwScc
        cmdA.Parameters.Add("@prdTyp", SqlDbType.VarChar, 50).Value = e.prdScc

        cmdA.Parameters.Add("@badanUsaha", SqlDbType.VarChar, 50).Value = e.badanUsaha
        cmdA.Parameters.Add("@plafon", SqlDbType.VarChar, 50).Value = e.plafon
        cmdA.Parameters.Add("@bakiDebit", SqlDbType.VarChar, 50).Value = e.bakiDebit
        cmdA.Parameters.Add("@kelonggaran", SqlDbType.VarChar, 50).Value = e.kelonggaranTarik
        cmdA.Parameters.Add("@tenor", SqlDbType.VarChar, 50).Value = e.tenor
        cmdA.Parameters.Add("@tglRek", SqlDbType.Date).Value = e.tglPembukaanRek
        cmdA.Parameters.Add("@noRek", SqlDbType.VarChar, 50).Value = e.noRek
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkLegalitas(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfLegalitas(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteLegalitas(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertLegalitasInfo(ByVal e, ByVal brwcode)
        Dim tglLegalitas As String = ""
        Dim tglJatuhTempo As String = ""
        If Not e.form("txtTglLegalitas") = "" Then
            tglLegalitas = Mid(e.form("txtTglLegalitas"), 7, 4) + "-" & Mid(e.form("txtTglLegalitas"), 4, 2) + "-" & Mid(e.form("txtTglLegalitas"), 1, 2)
        End If
        If Not e.form("txtTglJatuhTempo") = "" Then
            tglJatuhTempo = Mid(e.form("txtTglJatuhTempo"), 7, 4) + "-" & Mid(e.form("txtTglJatuhTempo"), 4, 2) + "-" & Mid(e.form("txtTglJatuhTempo"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertLegalitasInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@jenisIjin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisIjin")
        cmdA.Parameters.Add("@noLegalitas", SqlDbType.VarChar, 50).Value = e.form("txtNoLegalitas")
        cmdA.Parameters.Add("@tglLegalitas", SqlDbType.VarChar, 50).Value = tglLegalitas
        cmdA.Parameters.Add("@tglJatuhTempo", SqlDbType.VarChar, 50).Value = tglJatuhTempo
        cmdA.Parameters.Add("@perihal", SqlDbType.VarChar, 50).Value = e.form("txtPerihal")
        cmdA.Parameters.Add("@noPengesahan", SqlDbType.VarChar, 50).Value = e.form("txtNoPengesahan")
        cmdA.Parameters.Add("@namaNotaris", SqlDbType.VarChar, 50).Value = e.form("txtNamaNotaris")
        cmdA.Parameters.Add("@keterangan", SqlDbType.Text).Value = e.form("txtKeterangan")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateLegalitasInfo(ByVal e, ByVal brwcode, ByVal id)
        Dim tglLegalitas As String = ""
        Dim tglJatuhTempo As String = ""
        If Not e.form("txtTglLegalitas") = "" Then
            tglLegalitas = Mid(e.form("txtTglLegalitas"), 7, 4) + "-" & Mid(e.form("txtTglLegalitas"), 4, 2) + "-" & Mid(e.form("txtTglLegalitas"), 1, 2)
        End If
        If Not e.form("txtTglJatuhTempo") = "" Then
            tglJatuhTempo = Mid(e.form("txtTglJatuhTempo"), 7, 4) + "-" & Mid(e.form("txtTglJatuhTempo"), 4, 2) + "-" & Mid(e.form("txtTglJatuhTempo"), 1, 2)
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateLegalitasInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@jenisIjin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisIjin")
        cmdA.Parameters.Add("@noLegalitas", SqlDbType.VarChar, 50).Value = e.form("txtNoLegalitas")
        cmdA.Parameters.Add("@tglLegalitas", SqlDbType.Date).Value = CheckStringNull(tglLegalitas)
        cmdA.Parameters.Add("@tglJatuhTempo", SqlDbType.Date).Value = CheckStringNull(tglJatuhTempo)
        cmdA.Parameters.Add("@perihal", SqlDbType.VarChar, 50).Value = e.form("txtPerihal")
        cmdA.Parameters.Add("@noPengesahan", SqlDbType.VarChar, 50).Value = e.form("txtNoPengesahan")
        cmdA.Parameters.Add("@namaNotaris", SqlDbType.VarChar, 50).Value = e.form("txtNamaNotaris")
        cmdA.Parameters.Add("@keterangan", SqlDbType.Text).Value = e.form("txtKeterangan")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkKeyPerson(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfKeyPerson(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteKeyPerson(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_deleteKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateKeyPersonInfo(ByVal e, ByVal brwcode, ByVal id)
        Dim tglLahir As String = ""
        Dim tglKTP As String = ""
        Dim tglExpKTP As String = ""
        If Not e.form("txtTglLahir") = "" Then
            tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        End If
        If Not e.form("txtTglKTP") = "" Then
            tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        End If
        If Not e.form("txtExpKTP") = "" Then
            tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateKeyPersonInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.form("txtTempatLahir")
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = tglLahir
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoKTP")
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = tglKTP
        cmdA.Parameters.Add("@tglExpKTP", SqlDbType.Date).Value = tglExpKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@statusKawin", SqlDbType.VarChar, 50).Value = e.form("ddlStatusKawin")
        cmdA.Parameters.Add("@kebangsaan", SqlDbType.VarChar, 50).Value = e.form("txtKebangsaan")
        cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisKelamin")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamatRmh")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.Parameters.Add("@noHP", SqlDbType.VarChar, 50).Value = e.form("txtNoHP")
        cmdA.Parameters.Add("@namaIbu", SqlDbType.VarChar, 50).Value = e.form("txtNamaIbu")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertKeyPersonInfo(ByVal e, ByVal brwcode)
        Dim tglLahir As String = ""
        Dim tglKTP As String = ""
        Dim tglExpKTP As String = ""
        If Not e.form("txtTglLahir") = "" Then
            tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        End If
        If Not e.form("txtTglKTP") = "" Then
            tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        End If
        If Not e.form("txtExpKTP") = "" Then
            tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertKeyPersonInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.form("txtTempatLahir")
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = tglLahir
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoKTP")
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = tglKTP
        cmdA.Parameters.Add("@tglExpKTP", SqlDbType.Date).Value = tglExpKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@statusKawin", SqlDbType.VarChar, 50).Value = e.form("ddlStatusKawin")
        cmdA.Parameters.Add("@kebangsaan", SqlDbType.VarChar, 50).Value = e.form("txtKebangsaan")
        cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisKelamin")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamatRmh")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.Parameters.Add("@noHP", SqlDbType.VarChar, 50).Value = e.form("txtNoHP")
        cmdA.Parameters.Add("@namaIbu", SqlDbType.VarChar, 50).Value = e.form("txtNamaIbu")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkIndividualShareholders(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfIndividualShareholders(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertIndividualShareholders(ByVal e, ByVal brwcode)
        Dim tglLahir As String = ""
        Dim tglKTP As String = ""
        Dim tglExpKTP As String = ""
        If Not e.form("txtTglLahir") = "" Then
            tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        End If
        If Not e.form("txtTglKTP") = "" Then
            tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        End If
        If Not e.form("txtExpKTP") = "" Then
            tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Int).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Int).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.form("txtTempatLahir")
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = tglLahir
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoKTP")
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = tglKTP
        cmdA.Parameters.Add("@tglExpKTP", SqlDbType.Date).Value = tglExpKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisKelamin")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamatRumah")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.Parameters.Add("@noHP", SqlDbType.VarChar, 50).Value = e.form("txtNoHP")
        cmdA.Parameters.Add("@namaIbu", SqlDbType.VarChar, 50).Value = e.form("txtNamaIbu")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateIndividualShareholders(ByVal e, ByVal brwcode, ByVal id)
        Dim tglLahir As String = ""
        Dim tglKTP As String = ""
        Dim tglExpKTP As String = ""
        If Not e.form("txtTglLahir") = "" Then
            tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        End If
        If Not e.form("txtTglKTP") = "" Then
            tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        End If
        If Not e.form("txtExpKTP") = "" Then
            tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Int).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Int).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.form("txtTempatLahir")
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = tglLahir
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoKTP")
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = tglKTP
        cmdA.Parameters.Add("@tglExpKTP", SqlDbType.Date).Value = tglExpKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisKelamin")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamatRumah")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.Parameters.Add("@noHP", SqlDbType.VarChar, 50).Value = e.form("txtNoHP")
        cmdA.Parameters.Add("@namaIbu", SqlDbType.VarChar, 50).Value = e.form("txtNamaIbu")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteIndividualShareholders(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_deleteIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkCorporateShareholders(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfCorporateShareholders(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteCorporateShareholders(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_deleteCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertCorporateShareholders(ByVal e, ByVal brwcode)
        Dim tglAkteKTP As String = ""

        If Not e.form("txtTglAkteKTP") = "" Then
            tglAkteKTP = Mid(e.form("txtTglAkteKTP"), 7, 4) + "-" & Mid(e.form("txtTglAkteKTP"), 4, 2) + "-" & Mid(e.form("txtTglAkteKTP"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 50).Value = e.form("ddlTipeBadan")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@noAkteKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoAkteKTP")
        cmdA.Parameters.Add("@tglAkteKTP", SqlDbType.Date).Value = tglAkteKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Int).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Int).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamat")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateCorporateShareholders(ByVal e, ByVal brwcode, ByVal id)
        Dim tglAkteKTP As String = ""

        If Not e.form("txtTglAkteKTP") = "" Then
            tglAkteKTP = Mid(e.form("txtTglAkteKTP"), 7, 4) + "-" & Mid(e.form("txtTglAkteKTP"), 4, 2) + "-" & Mid(e.form("txtTglAkteKTP"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 50).Value = e.form("ddlTipeBadan")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@noAkteKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoAkteKTP")
        cmdA.Parameters.Add("@tglAkteKTP", SqlDbType.Date).Value = tglAkteKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Int).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Int).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamat")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkProfitLoss(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProfitLossLast(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkProfitLossLast", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfProfitLoss(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertProfitLossInfo(ByVal e, ByVal brwcode)
        Dim ots As Boolean = False
        Dim audit As Boolean = False
        Dim pln As Boolean = False
        If e.form("chkOTS") = "on" Then
            ots = True
        End If
        If e.form("chkAudited") = "on" Then
            audit = True
        End If
        If e.form("chkPLN") = "on" Then
            pln = True
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertProfitLossInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@bulan", SqlDbType.VarChar, 20).Value = e.form("ddlBulan")
        cmdA.Parameters.Add("@tahun", SqlDbType.VarChar, 4).Value = e.form("ddlTahun")
        cmdA.Parameters.Add("@ots", SqlDbType.Bit).Value = ots
        cmdA.Parameters.Add("@audit", SqlDbType.Bit).Value = audit
        cmdA.Parameters.Add("@pln", SqlDbType.Bit).Value = pln
        cmdA.Parameters.Add("@netSales", SqlDbType.Decimal).Value = e.form("txtPenjualanBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@costOfGoodSold", SqlDbType.Decimal).Value = e.form("txtHargaPokokPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@operatingExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaOperasional").ToString.Replace(",", "")
        cmdA.Parameters.Add("@rentExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaSewa").ToString.Replace(",", "")
        cmdA.Parameters.Add("@operatingIncome", SqlDbType.Decimal).Value = e.form("txtLabaOperasional").ToString.Replace(",", "")
        cmdA.Parameters.Add("@bankExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaLainnya").ToString.Replace(",", "")
        cmdA.Parameters.Add("@expensesInput1", SqlDbType.VarChar, 50).Value = e.form("txtBiayaInputanF1")
        cmdA.Parameters.Add("@expensesInputValue1", SqlDbType.Decimal).Value = e.form("txtBiayaInputan1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@expensesInput2", SqlDbType.VarChar, 50).Value = e.form("txtBiayaInputanF2")
        cmdA.Parameters.Add("@expensesInputValue2", SqlDbType.Decimal).Value = e.form("txtBiayaInputan2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@expensesInput3", SqlDbType.VarChar, 50).Value = e.form("txtBiayaInputanF3")
        cmdA.Parameters.Add("@expensesInputValue3", SqlDbType.Decimal).Value = e.form("txtBiayaInputan3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInput1", SqlDbType.VarChar, 50).Value = e.form("txtPendapatanInputanF1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInputValue1", SqlDbType.Decimal).Value = e.form("txtPendapatanInputan1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInput2", SqlDbType.VarChar, 50).Value = e.form("txtPendapatanInputanF2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInputValue2", SqlDbType.Decimal).Value = e.form("txtPendapatanInputan2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInput3", SqlDbType.VarChar, 50).Value = e.form("txtPendapatanInputanF3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInputValue3", SqlDbType.Decimal).Value = e.form("txtPendapatanInputan3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherRevenue", SqlDbType.Decimal).Value = e.form("txtPendapatanLainnya").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ebit", SqlDbType.Decimal).Value = e.form("txtEbit").ToString.Replace(",", "")
        cmdA.Parameters.Add("@da", SqlDbType.Decimal).Value = e.form("txtBiayaPenyusutan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ebitda", SqlDbType.Decimal).Value = e.form("txtEbitda").ToString.Replace(",", "")
        cmdA.Parameters.Add("@interestExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaBunga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ebt", SqlDbType.Decimal).Value = e.form("txtEbt").ToString.Replace(",", "")
        cmdA.Parameters.Add("@taxRevenue", SqlDbType.Decimal).Value = e.form("txtPajakPendapatan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@eat", SqlDbType.Decimal).Value = e.form("txtEat").ToString.Replace(",", "")
        cmdA.Parameters.Add("@eatPerM", SqlDbType.Decimal).Value = e.form("txtEatPerBulan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@netSalesPerM", SqlDbType.Decimal).Value = e.form("txtPenjualanBersihPerBulan").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateProfitLossInfo(ByVal e, ByVal brwcode)
        Dim ots As Boolean = False
        Dim audit As Boolean = False
        Dim pln As Boolean = False
        If e.form("chkOTS") = "on" Then
            ots = True
        End If
        If e.form("chkAudited") = "on" Then
            audit = True
        End If
        If e.form("chkPLN") = "on" Then
            pln = True
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateProfitLossInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@bulan", SqlDbType.VarChar, 20).Value = e.form("ddlBulan")
        cmdA.Parameters.Add("@tahun", SqlDbType.VarChar, 4).Value = e.form("ddlTahun")
        cmdA.Parameters.Add("@ots", SqlDbType.Bit).Value = ots
        cmdA.Parameters.Add("@audit", SqlDbType.Bit).Value = audit
        cmdA.Parameters.Add("@pln", SqlDbType.Bit).Value = pln
        cmdA.Parameters.Add("@netSales", SqlDbType.Decimal).Value = e.form("txtPenjualanBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@costOfGoodSold", SqlDbType.Decimal).Value = e.form("txtHargaPokokPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@operatingExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaOperasional").ToString.Replace(",", "")
        cmdA.Parameters.Add("@rentExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaSewa").ToString.Replace(",", "")
        cmdA.Parameters.Add("@operatingIncome", SqlDbType.Decimal).Value = e.form("txtLabaOperasional").ToString.Replace(",", "")
        cmdA.Parameters.Add("@bankExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaLainnya").ToString.Replace(",", "")
        cmdA.Parameters.Add("@expensesInput1", SqlDbType.VarChar, 50).Value = e.form("txtBiayaInputanF1")
        cmdA.Parameters.Add("@expensesInputValue1", SqlDbType.Decimal).Value = e.form("txtBiayaInputan1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@expensesInput2", SqlDbType.VarChar, 50).Value = e.form("txtBiayaInputanF2")
        cmdA.Parameters.Add("@expensesInputValue2", SqlDbType.Decimal).Value = e.form("txtBiayaInputan2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@expensesInput3", SqlDbType.VarChar, 50).Value = e.form("txtBiayaInputanF3")
        cmdA.Parameters.Add("@expensesInputValue3", SqlDbType.Decimal).Value = e.form("txtBiayaInputan3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInput1", SqlDbType.VarChar, 50).Value = e.form("txtPendapatanInputanF1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInputValue1", SqlDbType.Decimal).Value = e.form("txtPendapatanInputan1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInput2", SqlDbType.VarChar, 50).Value = e.form("txtPendapatanInputanF2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInputValue2", SqlDbType.Decimal).Value = e.form("txtPendapatanInputan2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInput3", SqlDbType.VarChar, 50).Value = e.form("txtPendapatanInputanF3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@revenueInputValue3", SqlDbType.Decimal).Value = e.form("txtPendapatanInputan3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherRevenue", SqlDbType.Decimal).Value = e.form("txtPendapatanLainnya").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ebit", SqlDbType.Decimal).Value = e.form("txtEbit").ToString.Replace(",", "")
        cmdA.Parameters.Add("@da", SqlDbType.Decimal).Value = e.form("txtBiayaPenyusutan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ebitda", SqlDbType.Decimal).Value = e.form("txtEbitda").ToString.Replace(",", "")
        cmdA.Parameters.Add("@interestExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaBunga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ebt", SqlDbType.Decimal).Value = e.form("txtEbt").ToString.Replace(",", "")
        cmdA.Parameters.Add("@taxRevenue", SqlDbType.Decimal).Value = e.form("txtPajakPendapatan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@eat", SqlDbType.Decimal).Value = e.form("txtEat").ToString.Replace(",", "")
        cmdA.Parameters.Add("@eatPerM", SqlDbType.Decimal).Value = e.form("txtEatPerBulan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@netSalesPerM", SqlDbType.Decimal).Value = e.form("txtPenjualanBersihPerBulan").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDetailOfBalanceSheet(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfBalanceSheet", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertBalanceSheetInfo(ByVal e, ByVal brwcode)
        Dim month As Integer = 0
        If e.form("ddlBulan") Is Nothing Then
            month = 12
        Else
            month = e.form("ddlBulan")
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertBalanceSheetInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@bulan", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@tahun", SqlDbType.Int).Value = e.form("ddlTahun")
        '------aktiva------
        cmdA.Parameters.Add("@cashAndBank", SqlDbType.Decimal).Value = e.form("txtKasBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@marketableSecurities", SqlDbType.Decimal).Value = e.form("txtSuratBerharga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@netReceivable", SqlDbType.Decimal).Value = e.form("txtPiutangBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@inventory", SqlDbType.Decimal).Value = e.form("txtPersediaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherCurrentAssets", SqlDbType.Decimal).Value = e.form("txtHartaLancarLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@prepaidExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaDibayarDimuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@jobOnFinished", SqlDbType.Decimal).Value = e.form("txtPekerjaanDalamPenyelesaian").ToString.Replace(",", "")
        cmdA.Parameters.Add("@prepaidTax", SqlDbType.Decimal).Value = e.form("txtPajakDibayarDimuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@persekot", SqlDbType.Decimal).Value = e.form("txtPersekot").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherReceivable", SqlDbType.Decimal).Value = e.form("txtPiutangLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentAssetsInput1", SqlDbType.VarChar, 50).Value = e.form("txtAktivaLancar1")
        cmdA.Parameters.Add("@currentAssetsInputValue1", SqlDbType.Decimal).Value = e.form("txtAktivaLancarValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentAssetsInput2", SqlDbType.VarChar, 50).Value = e.form("txtAktivaLancar2")
        cmdA.Parameters.Add("@currentAssetsInputValue2", SqlDbType.Decimal).Value = e.form("txtAktivaLancarValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentAssetsInput3", SqlDbType.VarChar, 50).Value = e.form("txtAktivaLancar3")
        cmdA.Parameters.Add("@currentAssetsInputValue3", SqlDbType.Decimal).Value = e.form("txtAktivaLancarValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCurrentAssets", SqlDbType.Decimal).Value = e.form("txtTotalHartaLancar").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssets", SqlDbType.Decimal).Value = e.form("txtHartaTetapBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@intangibleFixedAssets", SqlDbType.Decimal).Value = e.form("txtHartaImmaterial").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherAssets", SqlDbType.Decimal).Value = e.form("txtAktivaLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsInput1", SqlDbType.VarChar, 50).Value = e.form("txtAktivaTetap1")
        cmdA.Parameters.Add("@fixedAssetsInputValue1", SqlDbType.Decimal).Value = e.form("txtAktivaTetapValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsInput2", SqlDbType.VarChar, 50).Value = e.form("txtAktivaTetap2")
        cmdA.Parameters.Add("@fixedAssetsInputValue2", SqlDbType.Decimal).Value = e.form("txtAktivaTetapValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsInput3", SqlDbType.VarChar, 50).Value = e.form("txtAktivaTetap3")
        cmdA.Parameters.Add("@fixedAssetsInputValue3", SqlDbType.Decimal).Value = e.form("txtAktivaTetapValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalFixedAssets", SqlDbType.Decimal).Value = e.form("txtTotalHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalAssets", SqlDbType.Decimal).Value = e.form("txtTotalHarta").ToString.Replace(",", "")
        '--------pasiva
        cmdA.Parameters.Add("@insideShortTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPendek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@outsideShortTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPendekOther").ToString.Replace(",", "")
        cmdA.Parameters.Add("@accountPayable", SqlDbType.Decimal).Value = e.form("txtHutangUsaha").ToString.Replace(",", "")
        cmdA.Parameters.Add("@taxPayable", SqlDbType.Decimal).Value = e.form("txtPajakYangMasihHarusDibayar").ToString.Replace(",", "")
        cmdA.Parameters.Add("@deferredLiabilities", SqlDbType.Decimal).Value = e.form("txtKewajibanYangDitangguhkan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermLiabilities", SqlDbType.Decimal).Value = e.form("txtBagianLancarHutangJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentLiabilitiesInput1", SqlDbType.VarChar, 50).Value = e.form("txtHutangLancar1")
        cmdA.Parameters.Add("@currentLiabilitiesInputValue1", SqlDbType.Decimal).Value = e.form("txtHutangLancarValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentLiabilitiesInput2", SqlDbType.VarChar, 50).Value = e.form("txtHutangLancar2")
        cmdA.Parameters.Add("@currentLiabilitiesInputValue2", SqlDbType.Decimal).Value = e.form("txtHutangLancarValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentLiabilitiesInput3", SqlDbType.VarChar, 50).Value = e.form("txtHutangLancar3")
        cmdA.Parameters.Add("@currentLiabilitiesInputValue3", SqlDbType.Decimal).Value = e.form("txtHutangLancarValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCurrentLiabilities", SqlDbType.Decimal).Value = e.form("txtTotalHutangLancar").ToString.Replace(",", "")
        cmdA.Parameters.Add("@insideLongTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@outsideLongTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPanjangOther").ToString.Replace(",", "")
        cmdA.Parameters.Add("@debtToShareholders", SqlDbType.Decimal).Value = e.form("txtHutangPadaPemegangSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermDebtInput1", SqlDbType.VarChar, 50).Value = e.form("txtHutangJangkaPanjang1")
        cmdA.Parameters.Add("@longTermDebtInputValue1", SqlDbType.Decimal).Value = e.form("txtHutangJangkaPanjangValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermDebtInput2", SqlDbType.VarChar, 50).Value = e.form("txtHutangJangkaPanjang2")
        cmdA.Parameters.Add("@longTermDebtInputValue2", SqlDbType.Decimal).Value = e.form("txtHutangJangkaPanjangValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermDebtInput3", SqlDbType.VarChar, 50).Value = e.form("txtHutangJangkaPanjang3")
        cmdA.Parameters.Add("@longTermDebtInputValue3", SqlDbType.Decimal).Value = e.form("txtHutangJangkaPanjangValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalLongTermDebt", SqlDbType.Decimal).Value = e.form("txtTotalHutangJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalLiabilities", SqlDbType.Decimal).Value = e.form("txtTotalHutang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@capitalStock", SqlDbType.Decimal).Value = e.form("txtModalYangDisetor").ToString.Replace(",", "")
        cmdA.Parameters.Add("@paidUpCapital", SqlDbType.Decimal).Value = e.form("txtSetoranModal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@retainedEarning", SqlDbType.Decimal).Value = e.form("txtLabaYangDitahan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@dividen", SqlDbType.Decimal).Value = e.form("txtPriveDividen").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentEarnLoss", SqlDbType.Decimal).Value = e.form("txtLabaRugiTahunBerjalan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCapital", SqlDbType.Decimal).Value = e.form("txtTotalModal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalLiabilitiesCapital", SqlDbType.Decimal).Value = e.form("txtTotalHutangModal").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateBalanceSheetInfo(ByVal e, ByVal brwcode)
        Dim month As Integer = 0
        If e.form("ddlBulan") Is Nothing Then
            month = 12
        Else
            month = e.form("ddlBulan")
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateBalanceSheetInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@bulan", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@tahun", SqlDbType.Int).Value = e.form("ddlTahun")
        '------aktiva------
        cmdA.Parameters.Add("@cashAndBank", SqlDbType.Decimal).Value = e.form("txtKasBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@marketableSecurities", SqlDbType.Decimal).Value = e.form("txtSuratBerharga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@netReceivable", SqlDbType.Decimal).Value = e.form("txtPiutangBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@inventory", SqlDbType.Decimal).Value = e.form("txtPersediaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherCurrentAssets", SqlDbType.Decimal).Value = e.form("txtHartaLancarLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@prepaidExpenses", SqlDbType.Decimal).Value = e.form("txtBiayaDibayarDimuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@jobOnFinished", SqlDbType.Decimal).Value = e.form("txtPekerjaanDalamPenyelesaian").ToString.Replace(",", "")
        cmdA.Parameters.Add("@prepaidTax", SqlDbType.Decimal).Value = e.form("txtPajakDibayarDimuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@persekot", SqlDbType.Decimal).Value = e.form("txtPersekot").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherReceivable", SqlDbType.Decimal).Value = e.form("txtPiutangLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentAssetsInput1", SqlDbType.VarChar, 50).Value = e.form("txtAktivaLancar1")
        cmdA.Parameters.Add("@currentAssetsInputValue1", SqlDbType.Decimal).Value = e.form("txtAktivaLancarValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentAssetsInput2", SqlDbType.VarChar, 50).Value = e.form("txtAktivaLancar2")
        cmdA.Parameters.Add("@currentAssetsInputValue2", SqlDbType.Decimal).Value = e.form("txtAktivaLancarValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentAssetsInput3", SqlDbType.VarChar, 50).Value = e.form("txtAktivaLancar3")
        cmdA.Parameters.Add("@currentAssetsInputValue3", SqlDbType.Decimal).Value = e.form("txtAktivaLancarValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCurrentAssets", SqlDbType.Decimal).Value = e.form("txtTotalHartaLancar").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssets", SqlDbType.Decimal).Value = e.form("txtHartaTetapBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@intangibleFixedAssets", SqlDbType.Decimal).Value = e.form("txtHartaImmaterial").ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherAssets", SqlDbType.Decimal).Value = e.form("txtAktivaLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsInput1", SqlDbType.VarChar, 50).Value = e.form("txtAktivaTetap1")
        cmdA.Parameters.Add("@fixedAssetsInputValue1", SqlDbType.Decimal).Value = e.form("txtAktivaTetapValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsInput2", SqlDbType.VarChar, 50).Value = e.form("txtAktivaTetap2")
        cmdA.Parameters.Add("@fixedAssetsInputValue2", SqlDbType.Decimal).Value = e.form("txtAktivaTetapValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsInput3", SqlDbType.VarChar, 50).Value = e.form("txtAktivaTetap3")
        cmdA.Parameters.Add("@fixedAssetsInputValue3", SqlDbType.Decimal).Value = e.form("txtAktivaTetapValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalFixedAssets", SqlDbType.Decimal).Value = e.form("txtTotalHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalAssets", SqlDbType.Decimal).Value = e.form("txtTotalHarta").ToString.Replace(",", "")
        '--------pasiva
        cmdA.Parameters.Add("@insideShortTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPendek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@outsideShortTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPendekOther").ToString.Replace(",", "")
        cmdA.Parameters.Add("@accountPayable", SqlDbType.Decimal).Value = e.form("txtHutangUsaha").ToString.Replace(",", "")
        cmdA.Parameters.Add("@taxPayable", SqlDbType.Decimal).Value = e.form("txtPajakYangMasihHarusDibayar").ToString.Replace(",", "")
        cmdA.Parameters.Add("@deferredLiabilities", SqlDbType.Decimal).Value = e.form("txtKewajibanYangDitangguhkan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermLiabilities", SqlDbType.Decimal).Value = e.form("txtBagianLancarHutangJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentLiabilitiesInput1", SqlDbType.VarChar, 50).Value = e.form("txtHutangLancar1")
        cmdA.Parameters.Add("@currentLiabilitiesInputValue1", SqlDbType.Decimal).Value = e.form("txtHutangLancarValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentLiabilitiesInput2", SqlDbType.VarChar, 50).Value = e.form("txtHutangLancar2")
        cmdA.Parameters.Add("@currentLiabilitiesInputValue2", SqlDbType.Decimal).Value = e.form("txtHutangLancarValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentLiabilitiesInput3", SqlDbType.VarChar, 50).Value = e.form("txtHutangLancar3")
        cmdA.Parameters.Add("@currentLiabilitiesInputValue3", SqlDbType.Decimal).Value = e.form("txtHutangLancarValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCurrentLiabilities", SqlDbType.Decimal).Value = e.form("txtTotalHutangLancar").ToString.Replace(",", "")
        cmdA.Parameters.Add("@insideLongTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@outsideLongTermCredit", SqlDbType.Decimal).Value = e.form("txtKreditBankJangkaPanjangOther").ToString.Replace(",", "")
        cmdA.Parameters.Add("@debtToShareholders", SqlDbType.Decimal).Value = e.form("txtHutangPadaPemegangSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermDebtInput1", SqlDbType.VarChar, 50).Value = e.form("txtHutangJangkaPanjang1")
        cmdA.Parameters.Add("@longTermDebtInputValue1", SqlDbType.Decimal).Value = e.form("txtHutangJangkaPanjangValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermDebtInput2", SqlDbType.VarChar, 50).Value = e.form("txtHutangJangkaPanjang2")
        cmdA.Parameters.Add("@longTermDebtInputValue2", SqlDbType.Decimal).Value = e.form("txtHutangJangkaPanjangValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@longTermDebtInput3", SqlDbType.VarChar, 50).Value = e.form("txtHutangJangkaPanjang3")
        cmdA.Parameters.Add("@longTermDebtInputValue3", SqlDbType.Decimal).Value = e.form("txtHutangJangkaPanjangValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalLongTermDebt", SqlDbType.Decimal).Value = e.form("txtTotalHutangJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalLiabilities", SqlDbType.Decimal).Value = e.form("txtTotalHutang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@capitalStock", SqlDbType.Decimal).Value = e.form("txtModalYangDisetor").ToString.Replace(",", "")
        cmdA.Parameters.Add("@paidUpCapital", SqlDbType.Decimal).Value = e.form("txtSetoranModal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@retainedEarning", SqlDbType.Decimal).Value = e.form("txtLabaYangDitahan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@dividen", SqlDbType.Decimal).Value = e.form("txtPriveDividen").ToString.Replace(",", "")
        cmdA.Parameters.Add("@currentEarnLoss", SqlDbType.Decimal).Value = e.form("txtLabaRugiTahunBerjalan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCapital", SqlDbType.Decimal).Value = e.form("txtTotalModal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalLiabilitiesCapital", SqlDbType.Decimal).Value = e.form("txtTotalHutangModal").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkReconciliation(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkReconciliation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfReconciliation(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfReconciliation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertReconciliation(ByVal e As HttpRequest, ByVal brwcode As String, ByVal arr As Array)
        Dim month As Integer = 0
        If e.Form("ddlBulan") Is Nothing Then
            month = 12
        Else
            month = e.Form("ddlBulan")
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertReconciliationInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = e.Form("ddlTahun")
        cmdA.Parameters.Add("@startCapitalBalance", SqlDbType.Decimal).Value = e.Form("txtSaldoAwalModal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveCapital", SqlDbType.Decimal).Value = arr(0).ToString.Replace(",", "")
        cmdA.Parameters.Add("@endCapitalBalance", SqlDbType.Decimal).Value = arr(1).ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsBalance", SqlDbType.Decimal).Value = e.Form("txtSaldoAwalHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@netFixedAssetsPurchase", SqlDbType.Decimal).Value = e.Form("txtPembelianHartaTetapNetto").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsSales", SqlDbType.Decimal).Value = e.Form("txtPenjualanHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@grossCapitalPurchase", SqlDbType.Decimal).Value = arr(2).ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherNonCashExpense", SqlDbType.Decimal).Value = e.Form("txtBiayaNonKasLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@grossOperatingCash", SqlDbType.Decimal).Value = arr(3).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveAccountPayable", SqlDbType.Decimal).Value = e.Form("txtMoveHutangDagang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveTaxPayable", SqlDbType.Decimal).Value = e.Form("txtMoveHutangPajak").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveDeferredLiabilities", SqlDbType.Decimal).Value = e.Form("txtMoveKewajibanDitangguhkan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveLongTermLiabilities", SqlDbType.Decimal).Value = e.Form("txtMoveBagianLancarHutangJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@recEntry", SqlDbType.VarChar, 50).Value = e.Form("txtInputan")
        cmdA.Parameters.Add("@recEntryValue", SqlDbType.Decimal).Value = e.Form("txtInputanValue").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalOperatingCashSource", SqlDbType.Decimal).Value = arr(4).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveMarketableSecurities", SqlDbType.Decimal).Value = e.Form("txtMoveSuratBerharga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveNetReceivable", SqlDbType.Decimal).Value = e.Form("txtMovePiutangBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveStock", SqlDbType.Decimal).Value = e.Form("txtMovePersediaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveOtherNonOperatingExpense", SqlDbType.Decimal).Value = e.Form("txtMoveBiayaLainNonOperasional").ToString.Replace(",", "")
        cmdA.Parameters.Add("@movePrepaidExpense", SqlDbType.Decimal).Value = e.Form("txtMoveBiayaDibayarDimuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveOtherReceivable", SqlDbType.Decimal).Value = e.Form("txtMovePiutangLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalOperatingCashNecesssary", SqlDbType.Decimal).Value = arr(5).ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalNetOperatingCash", SqlDbType.Decimal).Value = arr(6).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveShortTermDebt", SqlDbType.Decimal).Value = e.Form("txtMoveKrediBankJangkaPendek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveLongTermDebt", SqlDbType.Decimal).Value = e.Form("txtMoveKreditBankJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveOtherLongTermDebt", SqlDbType.Decimal).Value = e.Form("txtMoveHutangJangkaPanjangLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashFixedAssetsSales", SqlDbType.Decimal).Value = e.Form("txtPenjualanHartaTetapKas").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashFixedAssetsPurchase", SqlDbType.Decimal).Value = e.Form("txtPembelianHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@saleOfShares", SqlDbType.Decimal).Value = e.Form("txtPenjualanSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@prive", SqlDbType.Decimal).Value = e.Form("txtPembagianKeuntunganPriveKas").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalNonOperatingCash", SqlDbType.Decimal).Value = arr(7).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveNetCash", SqlDbType.Decimal).Value = arr(8).ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateReconciliation(ByVal e As HttpRequest, ByVal brwcode As String, ByVal arr As Array)
        Dim month As Integer = 0
        If e.Form("ddlBulan") Is Nothing Then
            month = 12
        Else
            month = e.Form("ddlBulan")
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateReconciliationInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = e.Form("ddlTahun")
        cmdA.Parameters.Add("@startCapitalBalance", SqlDbType.Decimal).Value = e.Form("txtSaldoAwalModal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveCapital", SqlDbType.Decimal).Value = arr(0).ToString.Replace(",", "")
        cmdA.Parameters.Add("@endCapitalBalance", SqlDbType.Decimal).Value = arr(1).ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsBalance", SqlDbType.Decimal).Value = e.Form("txtSaldoAwalHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@netFixedAssetsPurchase", SqlDbType.Decimal).Value = e.Form("txtPembelianHartaTetapNetto").ToString.Replace(",", "")
        cmdA.Parameters.Add("@fixedAssetsSales", SqlDbType.Decimal).Value = e.Form("txtPenjualanHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@grossCapitalPurchase", SqlDbType.Decimal).Value = arr(2).ToString.Replace(",", "")
        cmdA.Parameters.Add("@otherNonCashExpense", SqlDbType.Decimal).Value = e.Form("txtBiayaNonKasLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@grossOperatingCash", SqlDbType.Decimal).Value = arr(3).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveAccountPayable", SqlDbType.Decimal).Value = e.Form("txtMoveHutangDagang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveTaxPayable", SqlDbType.Decimal).Value = e.Form("txtMoveHutangPajak").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveDeferredLiabilities", SqlDbType.Decimal).Value = e.Form("txtMoveKewajibanDitangguhkan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveLongTermLiabilities", SqlDbType.Decimal).Value = e.Form("txtMoveBagianLancarHutangJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@recEntry", SqlDbType.VarChar, 50).Value = e.Form("txtInputan")
        cmdA.Parameters.Add("@recEntryValue", SqlDbType.Decimal).Value = e.Form("txtInputanValue").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalOperatingCashSource", SqlDbType.Decimal).Value = arr(4).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveMarketableSecurities", SqlDbType.Decimal).Value = e.Form("txtMoveSuratBerharga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveNetReceivable", SqlDbType.Decimal).Value = e.Form("txtMovePiutangBersih").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveStock", SqlDbType.Decimal).Value = e.Form("txtMovePersediaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveOtherNonOperatingExpense", SqlDbType.Decimal).Value = e.Form("txtMoveBiayaLainNonOperasional").ToString.Replace(",", "")
        cmdA.Parameters.Add("@movePrepaidExpense", SqlDbType.Decimal).Value = e.Form("txtMoveBiayaDibayarDimuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveOtherReceivable", SqlDbType.Decimal).Value = e.Form("txtMovePiutangLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalOperatingCashNecesssary", SqlDbType.Decimal).Value = arr(5).ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalNetOperatingCash", SqlDbType.Decimal).Value = arr(6).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveShortTermDebt", SqlDbType.Decimal).Value = e.Form("txtMoveKrediBankJangkaPendek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveLongTermDebt", SqlDbType.Decimal).Value = e.Form("txtMoveKreditBankJangkaPanjang").ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveOtherLongTermDebt", SqlDbType.Decimal).Value = e.Form("txtMoveHutangJangkaPanjangLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashFixedAssetsSales", SqlDbType.Decimal).Value = e.Form("txtPenjualanHartaTetapKas").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashFixedAssetsPurchase", SqlDbType.Decimal).Value = e.Form("txtPembelianHartaTetap").ToString.Replace(",", "")
        cmdA.Parameters.Add("@saleOfShares", SqlDbType.Decimal).Value = e.Form("txtPenjualanSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@prive", SqlDbType.Decimal).Value = e.Form("txtPembagianKeuntunganPriveKas").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalNonOperatingCash", SqlDbType.Decimal).Value = arr(7).ToString.Replace(",", "")
        cmdA.Parameters.Add("@moveNetCash", SqlDbType.Decimal).Value = arr(8).ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDetailOfRatio(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub calculateRatio(ByVal brwcode, ByVal year, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_calculateRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDetailOfCalculationWorkingCapital(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfCalculationWorkingCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkLotsUsaha(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkLotsInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertLotsUsaha(ByVal obj, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertLotsInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@tglTinjau", SqlDbType.Date).Value = obj.tanggal
        cmdA.Parameters.Add("@tempatTinjau", SqlDbType.VarChar, 500).Value = obj.tempat
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 50).Value = obj.tujuan
        cmdA.Parameters.Add("@dokumen", SqlDbType.VarChar, 50).Value = obj.kelengkapanDokumenScoring
        cmdA.Parameters.Add("@pendidikan", SqlDbType.VarChar, 50).Value = obj.pendidikan
        cmdA.Parameters.Add("@pemahamanKewirausahaan", SqlDbType.VarChar, 50).Value = obj.pemahamanKewirausahaan
        cmdA.Parameters.Add("@pengalamanBerusaha", SqlDbType.VarChar, 250).Value = obj.pengalamanUsaha
        cmdA.Parameters.Add("@tempatUsaha", SqlDbType.VarChar, 50).Value = obj.tempatUsaha
        cmdA.Parameters.Add("@lokasiUsaha", SqlDbType.VarChar, 50).Value = obj.lokasiUsaha
        cmdA.Parameters.Add("@kegiatanUsaha", SqlDbType.VarChar, 50).Value = obj.kegiatanUsaha
        cmdA.Parameters.Add("@hubunganUsaha", SqlDbType.VarChar, 50).Value = obj.hubunganUsaha
        cmdA.Parameters.Add("@lamaUsaha", SqlDbType.Int).Value = obj.lamaUsaha
        cmdA.Parameters.Add("@hartaTetapYangDimiliki", SqlDbType.VarChar, 50).Value = obj.hartaTetapYangDimiliki
        cmdA.Parameters.Add("@hartaLancar", SqlDbType.VarChar, 50).Value = obj.hartaLancar
        cmdA.Parameters.Add("@perhitunganDalamPeriode", SqlDbType.Bit).Value = obj.perhitunganPeriode
        cmdA.Parameters.Add("@hariKerja", SqlDbType.VarChar, 50).Value = obj.hariKerja
        cmdA.Parameters.Add("@penjualanHarian", SqlDbType.VarChar, 50).Value = obj.penjualanHarian
        cmdA.Parameters.Add("@penjualanBulanan", SqlDbType.VarChar, 50).Value = obj.penjualanBulanan
        cmdA.Parameters.Add("@penjualanKeterangan", SqlDbType.VarChar, 500).Value = obj.penjualanKeterangan
        cmdA.Parameters.Add("@hppHarian", SqlDbType.VarChar, 50).Value = obj.HPPHarian
        cmdA.Parameters.Add("@hppBulanan", SqlDbType.VarChar, 50).Value = obj.HPPBulanan
        cmdA.Parameters.Add("@hppKeterangan", SqlDbType.VarChar, 500).Value = obj.HPPKeterangan
        cmdA.Parameters.Add("@labaKotorHarian", SqlDbType.VarChar, 50).Value = obj.labaKotorHarian
        cmdA.Parameters.Add("@labaKotorBulanan", SqlDbType.VarChar, 50).Value = obj.labaKotorBulanan
        cmdA.Parameters.Add("@labaKotorKeterangan", SqlDbType.VarChar, 500).Value = obj.labaKotorKeterangan
        cmdA.Parameters.Add("@biayaOperasionalHarian", SqlDbType.VarChar, 50).Value = obj.biayaOperasionalHarian
        cmdA.Parameters.Add("@biayaOperasionalBulanan", SqlDbType.VarChar, 50).Value = obj.biayaOperasionalBulanan
        cmdA.Parameters.Add("@biayaOperasionalKeterangan", SqlDbType.VarChar, 500).Value = obj.biayaOperasionalKeterangan
        cmdA.Parameters.Add("@biayaLainHarian", SqlDbType.VarChar, 50).Value = obj.biayaLainHarian
        cmdA.Parameters.Add("@biayaLainBulanan", SqlDbType.VarChar, 50).Value = obj.biayaLainBulanan
        cmdA.Parameters.Add("@biayaLainKeterangan", SqlDbType.VarChar, 500).Value = obj.biayaLainKeterangan
        cmdA.Parameters.Add("@labaBersihHarian", SqlDbType.VarChar, 50).Value = obj.labaBersihHarian
        cmdA.Parameters.Add("@labaBersihBulanan", SqlDbType.VarChar, 50).Value = obj.labaBersihBulanan
        cmdA.Parameters.Add("@labaBersihKeterangan", SqlDbType.VarChar, 500).Value = obj.labaBersihKeterangan
        cmdA.Parameters.Add("@angsuranHutangBulanan", SqlDbType.VarChar, 50).Value = obj.angsuranHutangBulanan
        cmdA.Parameters.Add("@angsuranHutangKeterangan", SqlDbType.VarChar, 500).Value = obj.angsuranHutangKeterangan
        cmdA.Parameters.Add("@kreditPercentageBulanan", SqlDbType.VarChar, 50).Value = obj.kreditPercentage
        cmdA.Parameters.Add("@kreditBulanan", SqlDbType.VarChar, 50).Value = obj.kredit
        cmdA.Parameters.Add("@kreditKeterangan", SqlDbType.VarChar, 500).Value = obj.kreditKeterangan
        cmdA.Parameters.Add("@tunaiBulanan", SqlDbType.VarChar, 50).Value = obj.tunai
        cmdA.Parameters.Add("@tunaiKeterangan", SqlDbType.VarChar, 500).Value = obj.tunaiKeterangan
        cmdA.Parameters.Add("@lain", SqlDbType.VarChar, 2500).Value = obj.lain

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateLotsUsaha(ByVal obj, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateLotsInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@tglTinjau", SqlDbType.Date).Value = obj.tanggal
        cmdA.Parameters.Add("@tempatTinjau", SqlDbType.VarChar, 500).Value = obj.tempat
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 50).Value = obj.tujuan
        cmdA.Parameters.Add("@dokumen", SqlDbType.VarChar, 50).Value = obj.kelengkapanDokumenScoring
        cmdA.Parameters.Add("@pendidikan", SqlDbType.VarChar, 50).Value = obj.pendidikan
        cmdA.Parameters.Add("@pemahamanKewirausahaan", SqlDbType.VarChar, 50).Value = obj.pemahamanKewirausahaan
        cmdA.Parameters.Add("@pengalamanBerusaha", SqlDbType.VarChar, 250).Value = obj.pengalamanUsaha
        cmdA.Parameters.Add("@tempatUsaha", SqlDbType.VarChar, 50).Value = obj.tempatUsaha
        cmdA.Parameters.Add("@lokasiUsaha", SqlDbType.VarChar, 50).Value = obj.lokasiUsaha
        cmdA.Parameters.Add("@kegiatanUsaha", SqlDbType.VarChar, 50).Value = obj.kegiatanUsaha
        cmdA.Parameters.Add("@hubunganUsaha", SqlDbType.VarChar, 50).Value = obj.hubunganUsaha
        cmdA.Parameters.Add("@lamaUsaha", SqlDbType.Int).Value = obj.lamaUsaha
        cmdA.Parameters.Add("@hartaTetapYangDimiliki", SqlDbType.VarChar, 50).Value = obj.hartaTetapYangDimiliki
        cmdA.Parameters.Add("@hartaLancar", SqlDbType.VarChar, 50).Value = obj.hartaLancar
        cmdA.Parameters.Add("@perhitunganDalamPeriode", SqlDbType.Bit).Value = obj.perhitunganPeriode
        cmdA.Parameters.Add("@hariKerja", SqlDbType.VarChar, 50).Value = obj.hariKerja
        cmdA.Parameters.Add("@penjualanHarian", SqlDbType.VarChar, 50).Value = obj.penjualanHarian
        cmdA.Parameters.Add("@penjualanBulanan", SqlDbType.VarChar, 50).Value = obj.penjualanBulanan
        cmdA.Parameters.Add("@penjualanKeterangan", SqlDbType.VarChar, 500).Value = obj.penjualanKeterangan
        cmdA.Parameters.Add("@hppHarian", SqlDbType.VarChar, 50).Value = obj.HPPHarian
        cmdA.Parameters.Add("@hppBulanan", SqlDbType.VarChar, 50).Value = obj.HPPBulanan
        cmdA.Parameters.Add("@hppKeterangan", SqlDbType.VarChar, 500).Value = obj.HPPKeterangan
        cmdA.Parameters.Add("@labaKotorHarian", SqlDbType.VarChar, 50).Value = obj.labaKotorHarian
        cmdA.Parameters.Add("@labaKotorBulanan", SqlDbType.VarChar, 50).Value = obj.labaKotorBulanan
        cmdA.Parameters.Add("@labaKotorKeterangan", SqlDbType.VarChar, 500).Value = obj.labaKotorKeterangan
        cmdA.Parameters.Add("@biayaOperasionalHarian", SqlDbType.VarChar, 50).Value = obj.biayaOperasionalHarian
        cmdA.Parameters.Add("@biayaOperasionalBulanan", SqlDbType.VarChar, 50).Value = obj.biayaOperasionalBulanan
        cmdA.Parameters.Add("@biayaOperasionalKeterangan", SqlDbType.VarChar, 500).Value = obj.biayaOperasionalKeterangan
        cmdA.Parameters.Add("@biayaLainHarian", SqlDbType.VarChar, 50).Value = obj.biayaLainHarian
        cmdA.Parameters.Add("@biayaLainBulanan", SqlDbType.VarChar, 50).Value = obj.biayaLainBulanan
        cmdA.Parameters.Add("@biayaLainKeterangan", SqlDbType.VarChar, 500).Value = obj.biayaLainKeterangan
        cmdA.Parameters.Add("@labaBersihHarian", SqlDbType.VarChar, 50).Value = obj.labaBersihHarian
        cmdA.Parameters.Add("@labaBersihBulanan", SqlDbType.VarChar, 50).Value = obj.labaBersihBulanan
        cmdA.Parameters.Add("@labaBersihKeterangan", SqlDbType.VarChar, 500).Value = obj.labaBersihKeterangan
        cmdA.Parameters.Add("@angsuranHutangBulanan", SqlDbType.VarChar, 50).Value = obj.angsuranHutangBulanan
        cmdA.Parameters.Add("@angsuranHutangKeterangan", SqlDbType.VarChar, 500).Value = obj.angsuranHutangKeterangan
        cmdA.Parameters.Add("@kreditPercentageBulanan", SqlDbType.VarChar, 50).Value = obj.kreditPercentage
        cmdA.Parameters.Add("@kreditBulanan", SqlDbType.VarChar, 50).Value = obj.kredit
        cmdA.Parameters.Add("@kreditKeterangan", SqlDbType.VarChar, 500).Value = obj.kreditKeterangan
        cmdA.Parameters.Add("@tunaiBulanan", SqlDbType.VarChar, 50).Value = obj.tunai
        cmdA.Parameters.Add("@tunaiKeterangan", SqlDbType.VarChar, 500).Value = obj.tunaiKeterangan
        cmdA.Parameters.Add("@lain", SqlDbType.VarChar, 2500).Value = obj.lain

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkAspek(ByVal brwcode, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAspekApp(ByVal appno, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkAspekApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAspekAgunan()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfAspekAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAspekFinancial()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfAspekFinancial", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAspekManagement()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfAspekManagement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAspekPasar()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfAspekPasar", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAspekRPC()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfAspekRPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAspekTeknis()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfAspekTeknis", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Function getBankRatingDetail(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getBankRatingDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@lbr_id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfFacilityAspek(ByVal AppNo, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfFacilityAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfBorrowerAspek(ByVal AppNo, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfBorrowerAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfAppAspek(ByVal AppNo, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfAppAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdateAspek(ByVal brwcode, ByVal id, ByVal value, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertUpdateAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 1).Value = value
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkBorrowerRating(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkBorrowerRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertBorrowerRating(ByVal brwcode, ByVal score, ByVal rating, ByVal ratingDate, ByVal nextRatingDate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertBorrowerRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@score", SqlDbType.Int).Value = score
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = rating
        cmdA.Parameters.AddWithValue("@rating", rating)
        cmdA.Parameters.Add("@ratingDate", SqlDbType.Date).Value = ratingDate
        cmdA.Parameters.Add("@nextRatingDate", SqlDbType.Date).Value = nextRatingDate
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkDocument(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertDocumentInfo(ByVal e, ByVal Data(), ByVal BrwCode)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglSurat As String = ""
        Dim tglUndangan As String = ""
        If Not e.Form(master + "InptglSurat") = "" Then
            tglSurat = Mid(e.Form(master + "InptglSurat"), 7, 4) + "-" & Mid(e.Form(master + "InptglSurat"), 4, 2) + "-" & Mid(e.Form(master + "InptglSurat"), 1, 2)
        End If
        If Not e.Form(master + "InptglUndangan") = "" Then
            tglUndangan = Mid(e.Form(master + "InptglUndangan"), 7, 4) + "-" & Mid(e.Form(master + "InptglUndangan"), 4, 2) + "-" & Mid(e.Form(master + "InptglUndangan"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertDocumentInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@surat", SqlDbType.Char).Value = Data(0)
        cmdA.Parameters.Add("@suratKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpSuratKet").ToString()
        cmdA.Parameters.Add("@pendirian", SqlDbType.Char).Value = Data(1)
        cmdA.Parameters.Add("@pendirianKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpPendirianKet").ToString()
        cmdA.Parameters.Add("@perubahan", SqlDbType.Char).Value = Data(2)
        cmdA.Parameters.Add("@perubahanKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpPerubahanKet").ToString()
        cmdA.Parameters.Add("@pengesahan", SqlDbType.Char).Value = Data(3)
        cmdA.Parameters.Add("@pengesahanKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpPengesahanKet").ToString()
        cmdA.Parameters.Add("@tanda", SqlDbType.Char).Value = Data(4)
        cmdA.Parameters.Add("@tandaKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpTandaKet").ToString()
        cmdA.Parameters.Add("@npwp", SqlDbType.Char).Value = Data(5)
        cmdA.Parameters.Add("@npwpKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpNpwpKet").ToString()
        cmdA.Parameters.Add("@siup", SqlDbType.Char).Value = Data(6)
        cmdA.Parameters.Add("@siupKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpSiupKet").ToString()
        cmdA.Parameters.Add("@izin", SqlDbType.Char).Value = Data(7)
        cmdA.Parameters.Add("@izinKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpIzinKet").ToString()
        cmdA.Parameters.Add("@laporan", SqlDbType.Char).Value = Data(8)
        cmdA.Parameters.Add("@laporanKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpLaporanKet").ToString()
        cmdA.Parameters.Add("@rek", SqlDbType.Char).Value = Data(9)
        cmdA.Parameters.Add("@rekKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpRekKet").ToString()
        cmdA.Parameters.Add("@imb", SqlDbType.Char).Value = Data(10)
        cmdA.Parameters.Add("@imbKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpImbKet").ToString()
        cmdA.Parameters.Add("@hak", SqlDbType.Char).Value = Data(11)
        cmdA.Parameters.Add("@hakKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpHakKet").ToString()
        cmdA.Parameters.Add("@rincian", SqlDbType.Char).Value = Data(12)
        cmdA.Parameters.Add("@rincianKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpRincianKet").ToString()

        cmdA.Parameters.Add("@noSurat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpnoSurat").ToString()
        cmdA.Parameters.Add("@tglSurat", SqlDbType.Date).Value = tglSurat
        cmdA.Parameters.Add("@kompSatu", SqlDbType.VarChar, 50).Value = e.Form(master + "InpkompSatu").ToString()
        cmdA.Parameters.Add("@jabatanSatu", SqlDbType.VarChar, 50).Value = e.Form(master + "InpjabatanSatu").ToString()
        cmdA.Parameters.Add("@kompDua", SqlDbType.VarChar, 50).Value = e.Form(master + "InpkompDua").ToString()
        cmdA.Parameters.Add("@jabatanDua", SqlDbType.VarChar, 50).Value = e.Form(master + "InpjabatanDua").ToString()
        cmdA.Parameters.Add("@tglUndangan", SqlDbType.Date).Value = tglUndangan
        cmdA.Parameters.Add("@tempat", SqlDbType.VarChar, 50).Value = e.Form(master + "Inptempat").ToString()
        cmdA.Parameters.Add("@waktu", SqlDbType.VarChar, 50).Value = e.Form(master + "Inpwaktu").ToString()
        cmdA.Parameters.Add("@agenda", SqlDbType.VarChar, 50).Value = e.Form(master + "Inpagenda").ToString()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateDocumentInfo(ByVal e, ByVal Data(), ByVal BrwCode)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglSurat As String = ""
        Dim tglUndangan As String = ""
        If Not e.Form(master + "InptglSurat") = "" Then
            tglSurat = Mid(e.Form(master + "InptglSurat"), 7, 4) + "-" & Mid(e.Form(master + "InptglSurat"), 4, 2) + "-" & Mid(e.Form(master + "InptglSurat"), 1, 2)
        End If
        If Not e.Form(master + "InptglUndangan") = "" Then
            tglUndangan = Mid(e.Form(master + "InptglUndangan"), 7, 4) + "-" & Mid(e.Form(master + "InptglUndangan"), 4, 2) + "-" & Mid(e.Form(master + "InptglUndangan"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateDocumentInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@surat", SqlDbType.Char).Value = Data(0)
        cmdA.Parameters.Add("@suratKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpSuratKet").ToString()
        cmdA.Parameters.Add("@pendirian", SqlDbType.Char).Value = Data(1)
        cmdA.Parameters.Add("@pendirianKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpPendirianKet").ToString()
        cmdA.Parameters.Add("@perubahan", SqlDbType.Char).Value = Data(2)
        cmdA.Parameters.Add("@perubahanKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpPerubahanKet").ToString()
        cmdA.Parameters.Add("@pengesahan", SqlDbType.Char).Value = Data(3)
        cmdA.Parameters.Add("@pengesahanKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpPengesahanKet").ToString()
        cmdA.Parameters.Add("@tanda", SqlDbType.Char).Value = Data(4)
        cmdA.Parameters.Add("@tandaKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpTandaKet").ToString()
        cmdA.Parameters.Add("@npwp", SqlDbType.Char).Value = Data(5)
        cmdA.Parameters.Add("@npwpKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpNpwpKet").ToString()
        cmdA.Parameters.Add("@siup", SqlDbType.Char).Value = Data(6)
        cmdA.Parameters.Add("@siupKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpSiupKet").ToString()
        cmdA.Parameters.Add("@izin", SqlDbType.Char).Value = Data(7)
        cmdA.Parameters.Add("@izinKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpIzinKet").ToString()
        cmdA.Parameters.Add("@laporan", SqlDbType.Char).Value = Data(8)
        cmdA.Parameters.Add("@laporanKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpLaporanKet").ToString()
        cmdA.Parameters.Add("@rek", SqlDbType.Char).Value = Data(9)
        cmdA.Parameters.Add("@rekKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpRekKet").ToString()
        cmdA.Parameters.Add("@imb", SqlDbType.Char).Value = Data(10)
        cmdA.Parameters.Add("@imbKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpImbKet").ToString()
        cmdA.Parameters.Add("@hak", SqlDbType.Char).Value = Data(11)
        cmdA.Parameters.Add("@hakKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpHakKet").ToString()
        cmdA.Parameters.Add("@rincian", SqlDbType.Char).Value = Data(12)
        cmdA.Parameters.Add("@rincianKet", SqlDbType.VarChar, 200).Value = e.Form(master + "InpRincianKet").ToString()

        cmdA.Parameters.Add("@noSurat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpnoSurat").ToString()
        cmdA.Parameters.Add("@tglSurat", SqlDbType.Date).Value = tglSurat
        cmdA.Parameters.Add("@kompSatu", SqlDbType.VarChar, 50).Value = e.Form(master + "InpkompSatu").ToString()
        cmdA.Parameters.Add("@jabatanSatu", SqlDbType.VarChar, 50).Value = e.Form(master + "InpjabatanSatu").ToString()
        cmdA.Parameters.Add("@kompDua", SqlDbType.VarChar, 50).Value = e.Form(master + "InpkompDua").ToString()
        cmdA.Parameters.Add("@jabatanDua", SqlDbType.VarChar, 50).Value = e.Form(master + "InpjabatanDua").ToString()
        cmdA.Parameters.Add("@tglUndangan", SqlDbType.Date).Value = tglUndangan
        cmdA.Parameters.Add("@tempat", SqlDbType.VarChar, 50).Value = e.Form(master + "Inptempat").ToString()
        cmdA.Parameters.Add("@waktu", SqlDbType.VarChar, 50).Value = e.Form(master + "Inpwaktu").ToString()
        cmdA.Parameters.Add("@agenda", SqlDbType.VarChar, 50).Value = e.Form(master + "Inpagenda").ToString()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Function getListOfScorecard(ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getListOfScorecard", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@category", SqlDbType.VarChar, 50).Value = cat
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkWorkflowRole(ByVal id, ByVal posisi)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkWorkflowRole", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getCounter(ByVal param)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDataCounter", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@param", SqlDbType.VarChar, 50).Value = param
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfLKN(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertLKNInfo(ByVal e, ByVal appno)
        Dim Tanggal As String = ""
        If Not e.form("InpTanggal") = "" Then
            Tanggal = Mid(e.Form("InpTanggal"), 7, 4) + "-" & Mid(e.Form("InpTanggal"), 4, 2) + "-" & Mid(e.Form("InpTanggal"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertLKNInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.Form("Inpnama")
        cmdA.Parameters.Add("@bentuk", SqlDbType.VarChar, 50).Value = e.Form("Inpbentuk")
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 50).Value = e.Form("Inptujuan")
        cmdA.Parameters.Add("@tanggal", SqlDbType.Date).Value = Tanggal
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.Form("Inpjabatan")
        cmdA.Parameters.Add("@hasil", SqlDbType.VarChar, 200).Value = e.Form("Inphasil")
        cmdA.Parameters.Add("@tindak", SqlDbType.VarChar, 200).Value = e.Form("Inptindak")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateLKNInfo(ByVal e, ByVal ID)
        Dim Tanggal As String = ""
        If Not e.form("InpTanggal") = "" Then
            Tanggal = Mid(e.Form("InpTanggal"), 7, 4) + "-" & Mid(e.Form("InpTanggal"), 4, 2) + "-" & Mid(e.Form("InpTanggal"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateLKNInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.Form("InpNama")
        cmdA.Parameters.Add("@bentuk", SqlDbType.VarChar, 50).Value = e.Form("InpBentuk")
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 50).Value = e.Form("InpTujuan")
        cmdA.Parameters.Add("@tanggal", SqlDbType.Date).Value = Tanggal
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.Form("InpJabatan")
        cmdA.Parameters.Add("@hasil", SqlDbType.VarChar, 200).Value = e.Form("InpHasil")
        cmdA.Parameters.Add("@tindak", SqlDbType.VarChar, 200).Value = e.Form("InpTindak")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkLKN(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkFacilityAgunanInfo(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityAgunanInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub updateFacilityAgunanInfo(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityAgunanInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.BigInt).Value = AppNo
        cmdA.Parameters.Add("@agunanPokokPasar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanPokokPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahanPasar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahanPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanPasar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanPokokCepat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanPokokCepat").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahanCepat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahanCepat").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanCepat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanCepat").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ratioltv", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLtv")
        cmdA.Parameters.Add("@ratioTotal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatioTotal")
        cmdA.Parameters.Add("@likUtama", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLikUtama")
        cmdA.Parameters.Add("@likTambahan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLikTambahan")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertFacilityAgunanInfo(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCreditAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@agunanPokokPasar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanPokokPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahanPasar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahanPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanPasar", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanPasar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanPokokCepat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanPokokCepat").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahanCepat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahanCepat").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanCepat", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanCepat").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ratioltv", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLtv")
        cmdA.Parameters.Add("@ratioTotal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatioTotal")
        cmdA.Parameters.Add("@likUtama", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLikUtama")
        cmdA.Parameters.Add("@likTambahan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLikTambahan")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkAppAspek(ByVal appno, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkAppAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdateAspekByAppno(ByVal appno, ByVal id, ByVal value, ByVal cat, ByVal desc, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertUpdateAspekByAppno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 1).Value = value
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.Parameters.Add("@desc", SqlDbType.Text).Value = desc
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertUpdateAspekIsian(ByVal appno, ByVal field_name, ByVal value, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertUpdateAspekIsian", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@field_name", SqlDbType.VarChar).Value = field_name
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 50).Value = value
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkAspekIsian(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_checkAspekIsian", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

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

    Function checkFacilityCreditKonstruksi(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityCreditKons", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertFacilityCreditKonstruksi(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCreditKons", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appNo", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@biayaTanah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBangunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sarana", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prasarana", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPrasarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalBiaya", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalBiaya").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasilPenjualan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHasilPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@labaKotor", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLabaKotor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaKonstruksi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaKonstruksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@salesMargin", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSalesMargin").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDev", SqlDbType.VarChar, 50).Value = e.Form(master + "InpShareDev").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDevPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpShareDevPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaDibutuhkan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDanaDibutuhkan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prosPembiayaan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProsPembiayaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maksTotalTarikan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMaksTotalTarikan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@taksasiTanah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTaksasiTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRabBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunanPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRabBangunanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minimalTotal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinimalTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanPenjualan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenerimaanPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUangMuka").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUangMukaPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanKpr", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenerimaanKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDanaJaminan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminanPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDanaJaminanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nettKpr", SqlDbType.VarChar, 50).Value = e.Form(master + "InpNettKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalNeraca", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@derEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDerEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiDer", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiDer").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalDisetor", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRasio", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiRasio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditKonstruksi(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityCreditKons", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appNo", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@biayaTanah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBangunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sarana", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prasarana", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPrasarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalBiaya", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalBiaya").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasilPenjualan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHasilPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@labaKotor", SqlDbType.VarChar, 50).Value = e.Form(master + "InpLabaKotor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaKonstruksi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaKonstruksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@salesMargin", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSalesMargin").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDev", SqlDbType.VarChar, 50).Value = e.Form(master + "InpShareDev").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDevPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpShareDevPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaDibutuhkan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDanaDibutuhkan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prosPembiayaan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProsPembiayaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maksTotalTarikan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMaksTotalTarikan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@taksasiTanah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTaksasiTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRabBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunanPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRabBangunanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minimalTotal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinimalTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanPenjualan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenerimaanPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUangMuka").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUangMukaPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanKpr", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenerimaanKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDanaJaminan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminanPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDanaJaminanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nettKpr", SqlDbType.VarChar, 50).Value = e.Form(master + "InpNettKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalNeraca", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@derEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDerEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiDer", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiDer").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalDisetor", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRasio", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiRasio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkFacilityCreditKontraktor(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_checkFacilityCreditKont", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertFacilityCreditKontraktor(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_insertFacilityCreditKont", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appNo", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@nilaiKontrak", SqlDbType.VarChar, 50).Value = e.Form(master + "InpNilaiKontrak").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pajak", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPajak").ToString().Replace(",", "")
        cmdA.Parameters.Add("@keuntungan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKeuntungan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@keuntunganPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKeuntunganPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hargaPokok", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHargaPokok").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUangMuka").ToString().Replace(",", "")
        cmdA.Parameters.Add("@terminTxt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTerminTxt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@termin", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTermin").ToString().Replace(",", "")
        cmdA.Parameters.Add("@inputTxt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpInputTxt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@input", SqlDbType.VarChar, 50).Value = e.Form(master + "InpInput").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalKerja", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalKerja").ToString().Replace(",", "")
        cmdA.Parameters.Add("@share", SqlDbType.VarChar, 50).Value = e.Form(master + "InpShare").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sharePersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSharePersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@porsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPorsi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maksTotal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMaksTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisPencarian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisPencarian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalNilai", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalNilai").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minTotalAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@spk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSpk").ToString().Replace(",", "")
        cmdA.Parameters.Add("@retensi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRetensi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalNeraca", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@derEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDerEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiDer", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiDer").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalDisetor", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRasio", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiRasio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditKontraktor(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fac_updateFacilityCreditKont", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appNo", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@nilaiKontrak", SqlDbType.VarChar, 50).Value = e.Form(master + "InpNilaiKontrak").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pajak", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPajak").ToString().Replace(",", "")
        cmdA.Parameters.Add("@keuntungan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKeuntungan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@keuntunganPersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKeuntunganPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hargaPokok", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHargaPokok").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.VarChar, 50).Value = e.Form(master + "InpUangMuka").ToString().Replace(",", "")
        cmdA.Parameters.Add("@terminTxt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTerminTxt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@termin", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTermin").ToString().Replace(",", "")
        cmdA.Parameters.Add("@inputTxt", SqlDbType.VarChar, 50).Value = e.Form(master + "InpInputTxt").ToString().Replace(",", "")
        cmdA.Parameters.Add("@input", SqlDbType.VarChar, 50).Value = e.Form(master + "InpInput").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalKerja", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalKerja").ToString().Replace(",", "")
        cmdA.Parameters.Add("@share", SqlDbType.VarChar, 50).Value = e.Form(master + "InpShare").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sharePersen", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSharePersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@porsi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPorsi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maksTotal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMaksTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisPencarian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisPencarian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalNilai", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalNilai").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minTotalAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@spk", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSpk").ToString().Replace(",", "")
        cmdA.Parameters.Add("@retensi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRetensi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.VarChar, 50).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalian", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRasioPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutang", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalNeraca", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitas", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@derEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpDerEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiDer", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiDer").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalDisetor", SqlDbType.VarChar, 50).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangEksis", SqlDbType.VarChar, 50).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRasio", SqlDbType.VarChar, 50).Value = e.Form(master + "InpProyeksiRasio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModal", SqlDbType.VarChar, 50).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")


        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertFacilitySummary(ByVal appno, ByVal brwcode, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ceg_facility", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getFacilityRating(ByVal appno, ByVal scc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ce_facility_" + scc, myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getRetailRating(ByVal appno, ByVal scc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ce_retail_" + scc, myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function checkAspekRPC(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAspekRPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertAspekRPC(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertAspekRPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = obj.appno
        cmdA.Parameters.Add("@shareCalonDebitur", SqlDbType.Float).Value = obj.shareCalonDebitur
        cmdA.Parameters.Add("@BCRtoPI", SqlDbType.Float).Value = obj.BCRtoPI
        cmdA.Parameters.Add("@IRR", SqlDbType.Float).Value = obj.IRR
        cmdA.Parameters.Add("@NPV", SqlDbType.Float).Value = obj.NPV
        cmdA.Parameters.Add("@totalPenjualanToTotalTarikan", SqlDbType.Float).Value = obj.totalPenjualanToTotalTarikan
        cmdA.Parameters.Add("@netIncomeToTotalKewajiban", SqlDbType.Float).Value = obj.netIncomeToTotalKewajiban
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAspekRPC(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateAspekRPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = obj.appno
        cmdA.Parameters.Add("@shareCalonDebitur", SqlDbType.Float).Value = obj.shareCalonDebitur
        cmdA.Parameters.Add("@BCRtoPI", SqlDbType.Float).Value = obj.BCRtoPI
        cmdA.Parameters.Add("@IRR", SqlDbType.Float).Value = obj.IRR
        cmdA.Parameters.Add("@NPV", SqlDbType.Float).Value = obj.NPV
        cmdA.Parameters.Add("@totalPenjualanToTotalTarikan", SqlDbType.Float).Value = obj.totalPenjualanToTotalTarikan
        cmdA.Parameters.Add("@netIncomeToTotalKewajiban", SqlDbType.Float).Value = obj.netIncomeToTotalKewajiban
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getBankRatingProduct(ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getBankRatingProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = type
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertBorrowerSummary(ByVal brwcode, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ceg_borrower", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getBorrowerRating(ByVal brwcode, ByVal scc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ce_borrower_" + scc, myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfBalanceSheetLast(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_getDetailOfBalanceSheetLast", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertReratingETL(ByVal BrwCode, ByVal Typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertReratingEtl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = Typ
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertReratingETLRequest(ByVal CIF, ByVal NPWP, ByVal NoRek, ByVal Typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertReratingEtlRequest", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = CIF
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = NPWP
        cmdA.Parameters.Add("@norek", SqlDbType.VarChar, 50).Value = NoRek
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = Typ
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertSummaryResultRr(ByVal AppNo, ByVal Typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_insertSummaryResultRR", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = Typ
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    '==================tambahan=====================
    Sub updateModelBrwRerating(ByVal model, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateModelBrwRerating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@brw_type", SqlDbType.VarChar, 50).Value = model
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateModelFacilityRerating(ByVal e As Array, ByVal appno As String)
        Dim chk As Boolean = False
        If e(1) Then
            chk = True
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("rat_updateModelFacilityRerating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@prd_type", SqlDbType.VarChar, 50).Value = e(0)
        cmdA.Parameters.Add("@chkRetail", SqlDbType.Bit).Value = chk
        cmdA.Parameters.Add("@retail_type", SqlDbType.VarChar, 50).Value = e(2)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '==================end tambahan=================

    Function getOptGroupByOwner(ByVal owner)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getOptGroupByOwner", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@owner", owner)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function saveExtInfoDtl(ByVal appno, ByVal rincian, ByVal asumsi, ByVal id, ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertExtInfoDtl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@rincian", rincian)
        cmdA.Parameters.AddWithValue("@asumsi", asumsi)
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getExtInfoDtlByAppno(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataExtInfoDtl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub deleteExtInfoDtlById(ByVal id, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_deleteExtInfoById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

End Class
