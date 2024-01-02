Imports System.Data.SqlClient
Public Class lReport

    Public Function getDataFIN(ByVal userID, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("spFIN_rptHeader", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getDataFIN_Keyperson(ByVal userID, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("spFIN_rptHeader", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getLegalitasUsaha(ByVal userID, ByVal borrowerCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("SpPIN_rptLegalitasUsaha", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = borrowerCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getLegalitasUsahaField()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("select 'lblJenisIjin' as 'sum_period' union select 'lsl_noLegalitas' as 'sum_period' union select 'lsl_namaNotaris' as 'sum_period' union select 'lsl_tglLegalitas' as 'sum_period'  union select 'lsl_tglJatuhTempo' as 'sum_period' ", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getKepemilikanPerusahaan(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("rpt_fin_shareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getKepemilikanPerusahaanField()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("select 1 as 'urut','Nama' as 'sum_period' union select 2 as 'urut','Lembar' as 'sum_period' union select 3 as 'urut','Percentage' as 'sum_period'", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getSusunanPengurus(ByVal userID, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("sp_getStrukturPengurusPerusahaan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getPengurusDanPenilaianManajemenTable(ByVal userID, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("sp_getFINkesManagmen", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getPengurusDanRiawayat(ByVal userID, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("spPIN_getPengurusdanRiwayat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getPengurusDanPenilaianManajemenTableField()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("select 1 as 'urut','lsp_nama' as 'sum_period' union select 2 as 'urut','lsp_nama_jabatan' as 'sum_period' union select 3 as 'urut','lsp_hub_antar_pengurus' as 'sum_period'", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Public Function getProfileManajemenPerusahaan(ByVal userID, ByVal borrowerCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("app_checkLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = borrowerCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getProfileManajemenPerusahaanField()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("select 1 as 'urut','lsp_path_foto' as 'sum_period' union select 2 as 'urut','lsp_nama' as 'sum_period' union select 3 as 'urut','lsp_riwayat' as 'sum_period'", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Public Function getDanaAtauFunding(ByVal userID, ByVal borrowerCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("app_checkLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = borrowerCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getKreditAtauLending_1(ByVal userID, ByVal borrowerCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("app_checkLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = userID
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = borrowerCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function UpdateTextAreaFIN(ByVal appNo, ByVal latarBelakang, ByVal pemegang, ByVal Legalitas, ByVal susunan, ByVal managemen, ByVal penilaianJaminan, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("rpt_insertFIN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        cmdA.Parameters.Add("@latarBelakang", SqlDbType.Text).Value = latarBelakang
        cmdA.Parameters.Add("@pemegangsaham", SqlDbType.Text).Value = pemegang
        cmdA.Parameters.Add("@legalitas", SqlDbType.Text).Value = Legalitas
        cmdA.Parameters.Add("@susuananPengurus", SqlDbType.Text).Value = susunan
        cmdA.Parameters.Add("@kesmanagemen", SqlDbType.Text).Value = managemen
        cmdA.Parameters.Add("@penilaianJaminan", SqlDbType.Text).Value = penilaianJaminan
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = type
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function SaveOrUpdateTextAreaFIN(ByVal appNo, ByVal latarBelakang, ByVal Legalitas, ByVal penilaianJaminan, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("app_insertFIN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        cmdA.Parameters.Add("@latarBelakang", SqlDbType.Text).Value = latarBelakang
        cmdA.Parameters.Add("@legalitas", SqlDbType.Text).Value = Legalitas
        cmdA.Parameters.Add("@penilaianJaminan", SqlDbType.Text).Value = penilaianJaminan
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = type
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function checkCountInTableFIN(ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("select COUNT(*)  as 'cRows' from rpt_fin where rfn_app_no='" & appNo & "'", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getKeyPerson(ByVal user, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("sp_getStrukturPengurusPerusahaan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getFINKeyPerson(ByVal user, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("spFIN_getKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    'Public Function getKeyPerson(ByVal user, ByVal appno)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr_Analysis
    '    Dim cmdA As New SqlCommand("SpFIN_KeyPerson", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
    '    Dim sda As New SqlDataAdapter(cmdA)
    '    Dim ds As New DataSet
    '    sda.Fill(ds)
    '    myConn.Close()
    '    Return ds.Tables(0)
    'End Function

    ''---Ikhtisar Pengusulan Kredit---''
    Function get_AppenIPK(ByVal code, ByVal rpt)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("get_rpt_master_ipk", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@brw_code", SqlDbType.VarChar, 20).Value = code
        cmda.Parameters.Add("@rpt", SqlDbType.VarChar, 20).Value = rpt
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Public Function UpdateReportFIA(ByVal code, ByVal name, ByVal desc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("update_rpt_master", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar).Value = code
        cmdA.Parameters.Add("@rpt", SqlDbType.VarChar).Value = name
        cmdA.Parameters.Add("@pak_desc", SqlDbType.VarChar).Value = desc
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Function getFRN_AsumsiRetinginfo(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptInfoAsumsiRatingInfo", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@periode", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Public Function checkCountInTableIPK(ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("select COUNT(*)  as 'cRows' from rpt_ipk where rpk_app_no='" & appNo & "'", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function SaveOrUpdateTextAreaIPK(ByVal appNo, ByVal PerjanjianKredit, ByVal EfektifKMK, ByVal PenarikanKMK, ByVal FasilitasKMK, ByVal SyaratLainnya, ByVal pejabat, ByVal komite, ByVal audit, ByVal apprasel, ByVal marginal, ByVal other, ByVal Type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("rpt_insertIPK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appNo
        cmdA.Parameters.Add("@perjanjiankredit", SqlDbType.Text).Value = PerjanjianKredit
        cmdA.Parameters.Add("@efektifkmk", SqlDbType.Text).Value = EfektifKMK
        cmdA.Parameters.Add("@penarikankmk", SqlDbType.Text).Value = PenarikanKMK
        cmdA.Parameters.Add("@fasilitaskmk", SqlDbType.Text).Value = FasilitasKMK
        cmdA.Parameters.Add("@syratlainnya", SqlDbType.Text).Value = SyaratLainnya
        cmdA.Parameters.Add("@pejabatpemutus", SqlDbType.Text).Value = pejabat
        cmdA.Parameters.Add("@komitekredit", SqlDbType.Text).Value = komite
        cmdA.Parameters.Add("@audit", SqlDbType.Text).Value = audit
        cmdA.Parameters.Add("@appraisel", SqlDbType.Text).Value = apprasel
        cmdA.Parameters.Add("@marginal", SqlDbType.Text).Value = marginal
        cmdA.Parameters.Add("@outher", SqlDbType.Text).Value = other
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = Type
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getDataIPK(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("rpt_detailOfIPK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getDataFINText(ByVal userID, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("SP_getFINTextarea", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appNo)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getIPK_Header(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("Sp_GetIPK_Header", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getIPK_Keyperson(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("SP_getIPK_Keyperson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getIPK_ProfilRisiko(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("getIPK_BrwRatting", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = userID
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getIPK_ProfilRisikoII(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("getIPK_BMPKreport", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getIPK_fasilitaskredit(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("SpIPK_getTotalKebutuhanKredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getIPK_tingkatrasio(ByVal userID, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("getIPK_rasioRpt", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Function getIPK_KMKI(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("Sp_getkmkI", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getIPK_KMKagunan(ByVal usr, ByVal period, ByVal param)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("getIPK_agunan", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        cmda.Parameters.Add("@param", SqlDbType.VarChar, 50).Value = param
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertUpdateTblFin(ByVal appno, ByVal latarbelakang, ByVal pemegangsaham, ByVal legalitas, ByVal susunanpengurus, ByVal kesmanagemen, ByVal penilaianjaminan)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuTableFin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@latarBelakang", latarbelakang)
        cmdA.Parameters.AddWithValue("@pemegangsaham", pemegangsaham)
        cmdA.Parameters.AddWithValue("@legalitas", legalitas)
        cmdA.Parameters.AddWithValue("@susuananPengurus", susunanpengurus)
        cmdA.Parameters.AddWithValue("@kesmanagemen", kesmanagemen)
        cmdA.Parameters.AddWithValue("@penilaianJaminan", penilaianjaminan)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

End Class
