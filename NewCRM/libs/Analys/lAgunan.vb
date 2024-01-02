Imports System.Data.SqlClient

Public Class lAgunan
    Dim oBase As New lBase
    Sub insertAgunanNonFixed(ByVal e, ByVal AppNo, ByVal userid)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertAgunanNonFixed", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appNo", AppNo)
        cmdA.Parameters.AddWithValue("@jnsAgunan", e.Form("ddlAgunanNonFixed"))
        cmdA.Parameters.AddWithValue("@keterangan1", e.Form("txtKeteranganNF"))
        cmdA.Parameters.AddWithValue("@keterangan2", e.Form("txtKeteranganLainNF"))
        cmdA.Parameters.AddWithValue("@buktiKepemilikan", e.Form("txtBuktiKepemilikanNF"))
        cmdA.Parameters.AddWithValue("@buktiPengikatan", e.Form("txtBuktiPengikatanNF"))
        cmdA.Parameters.AddWithValue("@nilaiPasar", Convert.ToDecimal(e.Form("txtNilaiPasarNF")))
        cmdA.Parameters.AddWithValue("@nilaiLikuiditas", Convert.ToDecimal(e.Form("txtNilaiLikuidasiNF")))
        cmdA.Parameters.AddWithValue("@asuransiPerusahaan", e.Form("txtAssuransiPerusahaanNF"))
        cmdA.Parameters.AddWithValue("@asuransiPertanggungan", e.Form("txtAsuransiPertanggunganNF"))
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAgunanNonFixed(ByVal e, ByVal Id, ByVal userid)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateAgunanNonFixed", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        cmdA.Parameters.AddWithValue("@jnsAgunan", e.Form("ddlAgunanNonFixed"))
        cmdA.Parameters.AddWithValue("@keterangan1", e.Form("txtKeteranganNF"))
        cmdA.Parameters.AddWithValue("@keterangan2", e.Form("txtKeteranganLainNF"))
        cmdA.Parameters.AddWithValue("@buktiKepemilikan", e.Form("txtBuktiKepemilikanNF"))
        cmdA.Parameters.AddWithValue("@buktiPengikatan", e.Form("txtBuktiPengikatanNF"))
        cmdA.Parameters.AddWithValue("@nilaiPasar", Convert.ToDecimal(e.Form("txtNilaiPasarNF")))
        cmdA.Parameters.AddWithValue("@nilaiLikuiditas", Convert.ToDecimal(e.Form("txtNilaiLikuidasiNF")))
        cmdA.Parameters.AddWithValue("@asuransiPerusahaan", e.Form("txtAssuransiPerusahaanNF"))
        cmdA.Parameters.AddWithValue("@asuransiPertanggungan", e.Form("txtAsuransiPertanggunganNF"))
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDataAgunanNonFixedById(ByVal Id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataAgunanNonFixedById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Sub deleteAgunanNonFixedById(ByVal Id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_delDataAgunanNonFixedById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getCheckAgunanFixed(ByVal val)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getListDataAgunanNonTanahBangunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@val", val)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Function getCheckTipeAgunan(ByVal val)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkDataTipeAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@val", val)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Function insertUpdateDataAgunan(ByVal e, ByVal AppNo, ByVal userid, ByVal inout, ByVal flag, ByVal namafile)
        Dim tglAgunan As String = "1900-01-01"
        Dim tglAppraisal As String = "1900-01-01"
        Dim tglPenarikan As String = "1900-01-01"
        If Not e.Form("txtTglAgunan") = "" Then
            tglAgunan = Mid(e.form("txtTglAgunan"), 7, 4) + "-" & Mid(e.form("txtTglAgunan"), 4, 2) + "-" & Mid(e.form("txtTglAgunan"), 1, 2)
        End If

        If Not e.Form("txtTglAppraisal") = "" Then
            tglAppraisal = Mid(e.form("txtTglAppraisal"), 7, 4) + "-" & Mid(e.form("txtTglAppraisal"), 4, 2) + "-" & Mid(e.form("txtTglAppraisal"), 1, 2)
        End If

        If Not e.Form("txtTanggal") = "" Then
            tglPenarikan = Mid(e.form("txtTanggal"), 7, 4) + "-" & Mid(e.form("txtTanggal"), 4, 2) + "-" & Mid(e.form("txtTanggal"), 1, 2)
        End If

        Dim img As String = e.Form("txtFotoId")
        Dim id As String = e.Form("txtId")

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuDataAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", e.Form("txtId"))
        cmdA.Parameters.AddWithValue("@idDtl", e.Form("txtIdDtl"))
        cmdA.Parameters.AddWithValue("@idImg", e.Form("txtFotoId"))
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@tglAgunan", tglAgunan)
        cmdA.Parameters.AddWithValue("@tipeAgunan", e.Form("ddlTipeAgunan"))
        cmdA.Parameters.AddWithValue("@kontrolAgunan", e.Form("DdlControled"))
        cmdA.Parameters.AddWithValue("@jenisAgunan", e.Form("DdlAgunan"))
        cmdA.Parameters.AddWithValue("@jenisPengikatan", e.Form("DdlPengikatan"))
        cmdA.Parameters.AddWithValue("@deskripsi", e.Form("txtDeskripsi"))
        cmdA.Parameters.AddWithValue("@klasAgunan", e.Form("DdlKlasAgunan"))
        cmdA.Parameters.AddWithValue("@ketlain", e.Form("txtKeterangan"))
        cmdA.Parameters.AddWithValue("@statusKepemilikan", If(String.IsNullOrEmpty(e.Form("ddlStatus")), "", e.Form("ddlStatus")))
        cmdA.Parameters.AddWithValue("@buktiKepemilikan", If(String.IsNullOrEmpty(e.Form("txtBuktiKepemilikan")), "", e.Form("txtBuktiKepemilikan")))
        cmdA.Parameters.AddWithValue("@lokasiAgunan", If(String.IsNullOrEmpty(e.Form("txtLokasiAgunan")), "", e.Form("txtLokasiAgunan")))
        cmdA.Parameters.AddWithValue("@namaPemilik", If(String.IsNullOrEmpty(e.Form("txtNamaPemilik")), "", e.Form("txtNamaPemilik")))
        cmdA.Parameters.AddWithValue("@appraisal", If(String.IsNullOrEmpty(e.Form("txtAppraisal")), "", e.Form("txtAppraisal")))
        cmdA.Parameters.AddWithValue("@noAppraisal", If(String.IsNullOrEmpty(e.Form("txtNoAppraisal")), "", e.Form("txtNoAppraisal")))
        cmdA.Parameters.AddWithValue("@tglAppraisal", tglAppraisal)
        cmdA.Parameters.AddWithValue("@nilaiPasar", Convert.ToDecimal(e.Form("txtNilaiPasar")))
        cmdA.Parameters.AddWithValue("@nilaiLikuidasi", Convert.ToDecimal(e.Form("txtNilaiLikuidasi")))
        cmdA.Parameters.AddWithValue("@nilaiPengikatan", Convert.ToDecimal(e.Form("txtNilaiPengikatan")))
        cmdA.Parameters.AddWithValue("@asuransiPenjamin", If(String.IsNullOrEmpty(e.Form("txtAsuransiPenjamin")), "", e.Form("txtAsuransiPenjamin")))
        cmdA.Parameters.AddWithValue("@nilaiAsuransi", Convert.ToDecimal(e.Form("txtNilaiAsuransi")))
        cmdA.Parameters.AddWithValue("@luasTanah", Convert.ToDecimal(e.Form("txtLuasTanah")))
        cmdA.Parameters.AddWithValue("@nilaiPasarLT", Convert.ToDecimal(e.Form("txtNilaiPasarLT")))
        cmdA.Parameters.AddWithValue("@nilaiLikuidasiLT", Convert.ToDecimal(e.Form("txtNilaiLikuidasiLT")))
        cmdA.Parameters.AddWithValue("@nilaiPengikatanLT", Convert.ToDecimal(e.Form("txtNilaiPengikatanLT")))
        cmdA.Parameters.AddWithValue("@asuransiPenjaminLT", If(String.IsNullOrEmpty(e.Form("txtAsuransiPenjaminLT")), "", e.Form("txtAsuransiPenjaminLT")))
        cmdA.Parameters.AddWithValue("@nilaiAsuransiLT", Convert.ToDecimal(e.Form("txtNilaiAsuransiLT")))
        cmdA.Parameters.AddWithValue("@luasBangunan", Convert.ToDecimal(e.Form("txtLuasBangunan")))
        cmdA.Parameters.AddWithValue("@nilaiPasarLB", Convert.ToDecimal(e.Form("txtNilaiPasarLB")))
        cmdA.Parameters.AddWithValue("@nilaiLikuidasiLB", Convert.ToDecimal(e.Form("txtNilaiLikuidasiLB")))
        cmdA.Parameters.AddWithValue("@nilaiPengikatanLB", Convert.ToDecimal(e.Form("txtNilaiPengikatanLB")))
        cmdA.Parameters.AddWithValue("@asuransiPenjaminLB", If(String.IsNullOrEmpty(e.Form("txtAsuransiPenjaminLB")), "", e.Form("txtAsuransiPenjaminLB")))
        cmdA.Parameters.AddWithValue("@nilaiAsuransiLB", Convert.ToDecimal(e.Form("txtNilaiAsuransiLB")))
        cmdA.Parameters.AddWithValue("@keteranganPenarikan", If(String.IsNullOrEmpty(e.Form("txtKetPenarikan")), "", e.Form("txtKetPenarikan")))
        cmdA.Parameters.AddWithValue("@tglPenarikan", tglPenarikan)
        cmdA.Parameters.AddWithValue("@inout", inout)
        cmdA.Parameters.AddWithValue("@flag", flag)
        cmdA.Parameters.AddWithValue("@imageNama", namafile)
        cmdA.Parameters.AddWithValue("@imageKeterangan", e.Form("txtInfoFoto"))
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListDataAgunan(ByVal appno)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getListDataAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

    Function getDataAgunanById(ByVal Id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataAgunanById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Function getDataAgunanImageByHdr(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataAgunanImageByHdr", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function delDataAgunanImageById(ByVal id, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_delDataAgunanImageById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@type", type)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteDataAgunanByID(ByVal Id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_deleteDataAgunanByID", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", Id)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    ' add by fhy 20170816
    Public Function getDataBorrowerType(ByVal appno)
        Dim ds As New DataSet

        Dim spname As String = "agu_getDataBorrowerTypeByAppno"
        Dim p1 As String = "@appno," + appno + ",s"
        ds = oBase.AnalysisProcessDataToDs(spname, p1)
        Return ds
    End Function


End Class
