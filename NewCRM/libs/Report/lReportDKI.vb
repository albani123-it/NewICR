Imports System.Data.SqlClient
Public Class lReportDKI

    Function getFPS_getReason(ByVal usr, ByVal BRWCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptDataAndSummaryPrescreeningReason", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = BRWCode
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getAppReport()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("rpt_appreport", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)
        myConn.Close()
        Return dts
    End Function
    Function getTap_walletReport(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getDataWalletShare", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getTap_AcctionPlan(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getDataInfoActionPlan", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getTap_KebutuhanProduk(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getDataInfoTap", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getTap_HeaderKebutuhanProduk(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getDataInfoTap", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFKS_TblInfo(ByVal usr, ByVal period, ByVal row)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rpt_GetInfoLkn", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        cmda.Parameters.Add("@row", SqlDbType.Int).Value = row
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFKS_Foto_kunjunganReport(ByVal usr, ByVal period, ByVal row)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptgetDataDtlLkn", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        cmda.Parameters.Add("@row", SqlDbType.VarChar, 50).Value = row
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
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
    Function getFRN_SecurityCoverageRatio(ByVal period, ByVal no)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("fia_coverage_ratio", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = period
        cmda.Parameters.Add("@inout", SqlDbType.VarChar, 50).Value = no
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFRN_AsumsiRetingDeatel(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptAsumsiRatingDtl", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@periode", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getMAK_AnalisisKreditDetail(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptMakInfo", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getMBK_BisnisKreditDetail(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptMbkInfo", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFPS_getkeriteria(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptDataAndSummaryPrescreening", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFPS_getSkor(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptDataAndSummaryPrescreening", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFPS_getHeader(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("RptFPS_header", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFPS_getKeyperson(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getKeyPerson", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFIA_getInfoAgunan(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getAgunanNameDebitur", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFIA_getInfoAgunan_A(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptAgunanAppraisal", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFIA_Foto_PetaReport(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_getDataAgunanImage", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)
        myConn.Close()
        Return dts
    End Function
    Public Function getFAK_RasioKeungan(ByVal Rasio, ByVal konstruksi, ByVal code, ByVal keuangan)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Rasio
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = keuangan
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_labarugi(ByVal laba_rugi, ByVal konstruksi, ByVal code, ByVal laba)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = laba_rugi
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = laba
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_lapPosisiKeu(ByVal Rasio, ByVal konstruksi, ByVal code, ByVal laba)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Rasio
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = laba
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_RekonHartaTetap(ByVal Rasio, ByVal konstruksi, ByVal code, ByVal laba)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Rasio
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = laba
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_ArusKas(ByVal Rasio, ByVal konstruksi, ByVal code, ByVal laba)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Rasio
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = laba
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_AsumsiProyeksiKeu()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("acc_asumsi_list", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_Proyeksiikhtisarkeu(ByVal Rasio, ByVal konstruksi, ByVal code, ByVal iktisar, ByVal y)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Rasio
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = iktisar
        cmdA.Parameters.Add("@isproyeksi", SqlDbType.VarChar, 50).Value = y
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_ProyeksiLabaRugi(ByVal laba, ByVal konstruksi, ByVal code, ByVal y)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = laba
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = y
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_ProyeksiLapPosisiKeu(ByVal Neraca, ByVal konstruksi, ByVal code, ByVal kosong, ByVal y)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Neraca
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = kosong
        cmdA.Parameters.Add("@isproyeksi", SqlDbType.VarChar, 50).Value = y
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Public Function getFAK_ProyeksiCashFlow(ByVal Neraca, ByVal konstruksi, ByVal code, ByVal kosong, ByVal y)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = Neraca
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = konstruksi
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@rptsection", SqlDbType.VarChar, 50).Value = kosong
        cmdA.Parameters.Add("@isproyeksi", SqlDbType.VarChar, 50).Value = y
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

    Public Function getFAK_ProyeksiCashFlowByYear(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report_proyeksi_for_fak", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function
    Function getFIA_RincianAgunan(ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptGetDataAgunanIn", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFIA_PenukaranAgunan(ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptGetDataAgunanOut", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFIA_PengikatanAgunan(ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptGetDataAgunanPengikatNAsuransiAgunanIn", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function getFIA_PengiktanAgunanOut(ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("sp_rptGetDataAgunanPengikatNAsuransiAgunanIn", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Function get_AppenFIA(ByVal code, ByVal rpt)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("get_rpt_master", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@brw_code", SqlDbType.VarChar, 20).Value = code
        cmda.Parameters.Add("@rpt", SqlDbType.VarChar, 20).Value = rpt
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    ''---punya ibay---''
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
        cmdA.Parameters.Add("@rpt", SqlDbType.VarChar).Value = IIf(String.IsNullOrWhiteSpace(name), "", name)
        cmdA.Parameters.Add("@pak_desc", SqlDbType.VarChar).Value = desc
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function UpdateReportFAK(ByVal code, ByVal name, ByVal desc)
        If name = Nothing Then
            name = ""
        End If

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
    Function getFAK_AnalisaKeuHistori(ByVal usr, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("SP_getAnalisakeuHistori", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        'cmda.Parameters.Add("@appno", SqlDbType.VarChar, 20).Value = usr
        cmda.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = period
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function get_Signature(ByVal codeReport, ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("pak_getRptSign", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@rpt", SqlDbType.VarChar).Value = codeReport
        cmda.Parameters.Add("@app_no", SqlDbType.VarChar).Value = appNo
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function generate_signature(ByVal dt)
        Dim html As String = ""

        Dim rows As Integer
        Dim count1 As Integer = 0
        Dim count2 As Integer = 0
        Dim count3 As Integer = 4
        Dim clm As Integer = 0
        rows = Math.Ceiling(dt.Rows.Count / 4)

        html += "<table align=center style='font-size:14px;'>"
        For rw = 1 To rows
            html += "<tr>"
            If rw = 1 Then
                If dt.Rows.Count <= 4 Then
                    For clm = count1 To dt.Rows.Count - 1
                        html += gentDetail(dt.Rows(clm).Item("usr_name").ToString(), dt.Rows(clm).Item("usr_access_level").ToString())
                        count2 = clm
                    Next
                Else
                    For clm = count1 To (4 - 1)
                        html += gentDetail(dt.Rows(clm).Item("usr_name").ToString(), dt.Rows(clm).Item("usr_access_level").ToString())
                        count2 = clm
                    Next
                End If

            ElseIf rows = rw Then
                For clm = count1 To dt.Rows.Count - 1
                    html += gentDetail(dt.Rows(clm).Item("usr_name").ToString(), dt.Rows(clm).Item("usr_access_level").ToString())
                    count2 = clm
                Next
            Else
                For clm = (count1) To (count3 + count1)
                    html += gentDetail(dt.Rows(clm).Item("usr_name").ToString(), dt.Rows(clm).Item("usr_access_level").ToString())
                    count2 = clm
                Next
            End If

            count1 = count2 + 1
            html += "</tr>"
        Next
        html += "</table>"

        Return html
    End Function

    Function gentDetail(ByVal username, ByVal level)
        Dim html As String = ""
        html += "<td style='width: 150px;padding-top: 20px;padding-right: 30px'>"
        html += "<div style='width: 100%;text-align: center;'>"
        html += "<br/><br/><br/><br/><br/><br/><br/>"
        html += username
        html += "<hr style='border:0;background-color:#000;height:1px;' />"
        html += level
        html += "</div>"
        html += "</td>"
        Return html
    End Function

    Function getCheckApproval(ByVal appNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("rpt_check_approval", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@noapp", appNo)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
End Class
