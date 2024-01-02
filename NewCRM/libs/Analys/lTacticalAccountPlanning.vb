Imports System.Data.SqlClient
Public Class lTacticalAccountPlanning
    Function getDataProductOffer()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_getProductOffering", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Sub insertTapHdr(ByVal funding, ByVal lending, ByVal services, ByVal keterangan, ByVal AppNo, ByVal userid)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertTapHdr", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@funding", funding)
        cmdA.Parameters.AddWithValue("@lending", lending)
        cmdA.Parameters.AddWithValue("@services", services)
        cmdA.Parameters.AddWithValue("@keterangan", keterangan)
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function insertTapDtl(ByVal AppNo, ByVal Id, ByVal prodType, ByVal ProdInt, ByVal ttlBank, ByVal bdki, ByVal rate, ByVal revenue, ByVal persenDki,
                          ByVal startForm, ByVal startTo, ByVal responsibility, ByVal activity, ByVal prospect, ByVal userid)

        Dim tglForm As String = "1950-01-01"
        Dim tglTo As String = "1950-12-31"

        If Not startForm = "" Then
            tglForm = startForm + "-01" + "-01"
        End If
        If Not startTo = "" Then
            tglTo = startTo + "-12" + "-31"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertTapDtl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@id", Id)
        cmdA.Parameters.AddWithValue("@prodType", prodType)
        cmdA.Parameters.AddWithValue("@prodInt", ProdInt)
        cmdA.Parameters.AddWithValue("@ttlBank", Convert.ToDecimal(ttlBank))
        cmdA.Parameters.AddWithValue("@bdki", Convert.ToDecimal(bdki))
        cmdA.Parameters.AddWithValue("@rate", Convert.ToDecimal(rate))
        cmdA.Parameters.AddWithValue("@revenue", Convert.ToDecimal(revenue))
        cmdA.Parameters.AddWithValue("@persenDki", Convert.ToDecimal(persenDki))
        cmdA.Parameters.AddWithValue("@startForm", tglForm)
        cmdA.Parameters.AddWithValue("@startTo", tglTo)
        cmdA.Parameters.AddWithValue("@responsibility", responsibility)
        cmdA.Parameters.AddWithValue("@activity", activity)
        cmdA.Parameters.AddWithValue("@prospect", prospect)
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataTapByAppno(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataTapByAppno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteTapDtlById(ByVal appno, ByVal id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_deleteTapDtlById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Function insertTapAndWs(ByVal obj, ByVal AppNo, ByVal userid)
        Dim prefix As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglTap As String = "1900-01-01"
        If Not obj.form(prefix + "txtTanggal").ToString() = "" Then
            tglTap = Mid(obj.form(prefix + "txtTanggal"), 7, 4) + "-" & Mid(obj.form(prefix + "txtTanggal"), 4, 2) + "-" & Mid(obj.form(prefix + "txtTanggal"), 1, 2)

        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuTapAndWalletShare", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@kode", obj.form(prefix + "txtTapKode"))
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@tglTap", tglTap)
        cmdA.Parameters.AddWithValue("@unitPengusul", obj.form(prefix + "ddlUnitPengusul"))
        cmdA.Parameters.AddWithValue("@funding", obj.form(prefix + "txtFunding"))
        cmdA.Parameters.AddWithValue("@lending", obj.form(prefix + "txtLending"))
        cmdA.Parameters.AddWithValue("@service", obj.form(prefix + "txtServices"))
        cmdA.Parameters.AddWithValue("@catatan", obj.form(prefix + "txtCatatan"))
        cmdA.Parameters.AddWithValue("@idWs", obj.form(prefix + "txtIdWS"))
        cmdA.Parameters.AddWithValue("@kategori", obj.form(prefix + "ddlKategoriProduk"))
        cmdA.Parameters.AddWithValue("@namaProduk", obj.form(prefix + "txtNamaProduk"))
        cmdA.Parameters.AddWithValue("@periode1", obj.form(prefix + "ddlPeriode1"))
        cmdA.Parameters.AddWithValue("@ttlBank1", Convert.ToDecimal(obj.form(prefix + "txtTotalBank1").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@bdki1", Convert.ToDecimal(obj.form(prefix + "txtBdki1").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@rate1", Convert.ToDecimal(obj.form(prefix + "txtRates1").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@revenue1", Convert.ToDecimal(obj.form(prefix + "txtRev1").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@persenBdki1", Convert.ToDecimal(obj.form(prefix + "txtPersentaseBdki1").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@periode2", obj.form(prefix + "ddlPeriode2"))
        cmdA.Parameters.AddWithValue("@ttlBank2", Convert.ToDecimal(obj.form(prefix + "txtTotalBank2").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@bdki2", Convert.ToDecimal(obj.form(prefix + "txtBdki2").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@rate2", Convert.ToDecimal(obj.form(prefix + "txtRates2").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@revenue2", Convert.ToDecimal(obj.form(prefix + "txtRev2").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@persenBdki2", Convert.ToDecimal(obj.form(prefix + "txtPersentaseBdki2").Replace(",", "")))
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataTap(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataTap", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataWs(ByVal kode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataWs", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@kode", kode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataWalletShareById(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getWalletShareById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataDropdownWs(ByVal kode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDropdownWs", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@kode", kode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function insertActionPlan(ByVal obj, ByVal userid, ByVal prospek)
        Dim prefix As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuTapActionPlan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@kode", obj.form(prefix + "txtTapKode"))
        cmdA.Parameters.AddWithValue("@idAp", obj.form(prefix + "txtIdAP"))
        'cmdA.Parameters.AddWithValue("@namaProduk", obj.form(prefix + "ddlNamaProduk"))
        cmdA.Parameters.AddWithValue("@namaProduk", obj.form(prefix + "txtNamaProduk1"))
        cmdA.Parameters.AddWithValue("@tahunMulai", obj.form(prefix + "ddlThnMulai"))
        cmdA.Parameters.AddWithValue("@tahunSelesai", obj.form(prefix + "ddlThnSelesai"))
        cmdA.Parameters.AddWithValue("@responsible", obj.form(prefix + "txtResponsible"))
        cmdA.Parameters.AddWithValue("@activity", obj.form(prefix + "txtActivity"))
        cmdA.Parameters.AddWithValue("@prospek", prospek)
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataActionPlan(ByVal kode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataActionPlan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@kode", kode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataActionPlanById(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDataAPById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function insertTacticalAccountPlanning(ByVal obj, ByVal AppNo, ByVal userid)
        Dim prefix As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglTap As String = "1900-01-01"
        If Not obj.form(prefix + "txtTanggal").ToString() = "" Then
            tglTap = Mid(obj.form(prefix + "txtTanggal"), 7, 4) + "-" & Mid(obj.form(prefix + "txtTanggal"), 4, 2) + "-" & Mid(obj.form(prefix + "txtTanggal"), 1, 2)

        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuTacticalAccountPlanning", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@kode", obj.form(prefix + "txtTapKode"))
        cmdA.Parameters.AddWithValue("@appno", AppNo)
        cmdA.Parameters.AddWithValue("@tglTap", tglTap)
        cmdA.Parameters.AddWithValue("@unitPengusul", obj.form(prefix + "ddlUnitPengusul"))
        cmdA.Parameters.AddWithValue("@funding", obj.form(prefix + "txtFunding"))
        cmdA.Parameters.AddWithValue("@lending", obj.form(prefix + "txtLending"))
        cmdA.Parameters.AddWithValue("@service", obj.form(prefix + "txtServices"))
        cmdA.Parameters.AddWithValue("@catatan", obj.form(prefix + "txtCatatan"))
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    sub deleteWalletShareById(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_delWalletShareById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteAccountPlanningById(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_delAccountPlanningById", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


End Class
