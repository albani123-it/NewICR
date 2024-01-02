Imports System.Data.SqlClient
Public Class lVerification
    Public Function getVerificationProduct()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("prm_verification_product", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Public Function getSavedProductType(ByVal ver_product, ByVal ver_sub)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getRecordProductType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_product", SqlDbType.VarChar, 50).Value = ver_product
        cmdA.Parameters.Add("@ver_sub", SqlDbType.VarChar, 50).Value = ver_sub
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Public Function getSelectedVerification(ByVal ver_product, ByVal ver_sub, ByVal ver_product_type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getSelectedVerification", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_product", SqlDbType.VarChar, 50).Value = ver_product
        cmdA.Parameters.Add("@ver_sub", SqlDbType.VarChar, 50).Value = ver_sub
        cmdA.Parameters.Add("@ver_product_type", SqlDbType.VarChar, 50).Value = ver_product_type
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getCorrectAnswer(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("select ver_static_num, ver_dynamic_num from verification where ver_id = '" & id & "'", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getSavedVerificationDynamic(ByVal ver_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getRecordDynamic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.VarChar, 50)
        cmdA.Parameters("@ver_id").Value = ver_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getSavedVerificationSystem(ByVal ver_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getRecordSystem", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.VarChar, 50)
        cmdA.Parameters("@ver_id").Value = ver_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getSavedVerificationStatic(ByVal ver_id, ByVal SOURCE)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getRecordStatic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.VarChar, 50)
        cmdA.Parameters("@ver_id").Value = ver_id
        cmdA.Parameters.Add("@source", SqlDbType.VarChar, 50)
        cmdA.Parameters("@source").Value = SOURCE
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Public Function getDisplayVerificationStatic(ByVal field, ByVal acct, ByVal card, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getDisplayVerificationStatic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@field", SqlDbType.VarChar, 500).Value = field
        cmdA.Parameters.Add("@acct", SqlDbType.VarChar, 50).Value = acct
        cmdA.Parameters.Add("@card", SqlDbType.VarChar, 50).Value = card
        cmdA.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = tbl
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0)
    End Function
    Public Function getVerificationStatic(ByVal product)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("prm_verification_static", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@product", SqlDbType.VarChar, 50)
        cmdA.Parameters("@product").Value = product
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function
    Sub ClearRecordDynamic(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_clearVerifikasiDynamic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub ClearRecordSystem(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_clearVerifikasiSystem", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub ClearRecordStatic(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_clearVerifikasiStatic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub InsertRecordStatic(ByVal id, ByVal value, ByVal usr, ByVal stat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertVerifikasiStatic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@ver_static", SqlDbType.VarChar).Value = value
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.Parameters.Add("@stat", SqlDbType.VarChar).Value = stat
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub InsertRecordDynamic(ByVal id, ByVal value, ByVal status, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertVerifikasiDynamic", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@ver_dynamic", SqlDbType.VarChar).Value = value
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.Parameters.Add("@status", SqlDbType.BigInt).Value = Integer.Parse(status.ToString)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub InsertRecordSystem(ByVal id, ByVal value, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertVerifikasiSystem", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@ver_system", SqlDbType.VarChar).Value = value
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub UpdateRecord(ByVal e, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_editVerifikasi", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_name", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$txtname")
        cmdA.Parameters.Add("@ver_product", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ver_product")
        cmdA.Parameters.Add("@ver_sub", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ddlsub")
        cmdA.Parameters.Add("@ver_clue", SqlDbType.Text).Value = e.Form("ctl00$MainContent$txtClue")
        cmdA.Parameters.Add("@ver_static_num", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_static_num")
        cmdA.Parameters.Add("@ver_static_num_wr", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_static_num_wr")
        cmdA.Parameters.Add("@ver_dynamic_num", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_dynamic_nim")
        cmdA.Parameters.Add("@ver_dynamic_num_wr", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_dynamic_max_wr")
        cmdA.Parameters.Add("@ver_last_usr", SqlDbType.VarChar).Value = "IUser"
        cmdA.Parameters.Add("@ver_id", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ver_id")
        cmdA.Parameters.Add("@ver_product_type", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ddlproduct")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub InsertRecord(ByVal e, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertVerifikasi", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver_name", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$txtname")
        cmdA.Parameters.Add("@ver_product", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ver_product")
        cmdA.Parameters.Add("@ver_sub", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ddlsub")
        cmdA.Parameters.Add("@ver_clue", SqlDbType.Text).Value = e.Form("ctl00$MainContent$txtClue")
        cmdA.Parameters.Add("@ver_static_num", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_static_num")
        cmdA.Parameters.Add("@ver_static_num_wr", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_static_num_wr")
        cmdA.Parameters.Add("@ver_dynamic_num", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_dynamic_nim")
        cmdA.Parameters.Add("@ver_dynamic_num_wr", SqlDbType.Int).Value = e.Form("ctl00$MainContent$ver_dynamic_max_wr")
        cmdA.Parameters.Add("@ver_product_type", SqlDbType.VarChar).Value = e.Form("ctl00$MainContent$ddlproduct")
        cmdA.Parameters.Add("@ver_last_usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub insertVerificationResult(ByVal acctnum, ByVal cardnum, ByVal source, ByVal ver_id, ByVal usr, ByVal stat, ByVal dynamic)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertVerifikasiResult", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ves_acctnum", SqlDbType.VarChar).Value = acctnum
        cmdA.Parameters.Add("@ves_cardnum", SqlDbType.VarChar).Value = cardnum
        cmdA.Parameters.Add("@ves_source", SqlDbType.VarChar).Value = source
        cmdA.Parameters.Add("@ves_ver_id", SqlDbType.Int).Value = ver_id
        cmdA.Parameters.Add("@ves_usr", SqlDbType.VarChar).Value = usr
        cmdA.Parameters.Add("@ves_correct_static", SqlDbType.Int).Value = stat
        cmdA.Parameters.Add("@ves_correct_dynamic", SqlDbType.Int).Value = dynamic
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Public Function getVerificationDetail(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getVerificationDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@verid", SqlDbType.VarChar, 50)
        cmdA.Parameters("@verid").Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getProductType()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("prm_product_type", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Sub DeleteVerification(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_deleteVerification", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub
End Class
