Imports System.Data.SqlClient

Public Class lGlobal
    Public Function CheckUsr(ByVal usr, ByVal pass)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("xml_getUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        cmdA.Parameters.Add("@pass", SqlDbType.VarChar, 16)
        cmdA.Parameters("@pass").Value = pass
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function checkPassKey(ByVal passkey)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_checkPassKey", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@passkey", SqlDbType.VarChar, 16)
        cmdA.Parameters("@passkey").Value = passkey
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function checkLoginPasskey(ByVal npk)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_checkLoginPasskey", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@npk", SqlDbType.VarChar, 16)
        cmdA.Parameters("@npk").Value = npk
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function logOut(ByVal passkey)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_logOut", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@pass", SqlDbType.VarChar, 16)
        cmdA.Parameters("@pass").Value = passkey
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function timeOut(ByVal passkey)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_timeOut", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@passkey", SqlDbType.VarChar, 16)
        cmdA.Parameters("@passkey").Value = passkey
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function passingPassKey(ByVal passkey)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_updatePassKey", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@passkey", SqlDbType.VarChar, 16)
        cmdA.Parameters("@passkey").Value = passkey
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getTasklist(ByVal uniqueCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_getTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@unique_code", SqlDbType.VarChar, 20)
        cmdA.Parameters("@unique_code").Value = uniqueCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function getCustInquiry(ByVal where)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("SELECT DISTINCT CSV, FSV, CUST_NAME, CUST_DOB, CUST_ID, CUST_ID_TYPE, FSV_RELATION FROM [CUST_INQUIRY] WHERE " & where & " AND CSV IS NOT NULL", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function getProductList(ByVal CSV, ByVal ORDER, ByVal FIELD, ByVal VALUE, ByVal SORT)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_getProductList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 50).Value = CSV
        cmdA.Parameters.Add("@order", SqlDbType.VarChar, 50).Value = ORDER
        cmdA.Parameters.Add("@field", SqlDbType.VarChar, 50).Value = FIELD
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 50).Value = VALUE
        cmdA.Parameters.Add("@sorting", SqlDbType.VarChar, 50).Value = SORT
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getPPC(ByVal cond)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getPPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cond", SqlDbType.VarChar, 1000)
        cmdA.Parameters("@cond").Value = cond
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getCustomerDetail(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getDetailCustomerViewWS ", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 50)
        cmdA.Parameters("@csv").Value = csv
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getCustomerProductDetail(ByVal acctnum)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getCustomerDetailByAcctnum", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@acctnum", SqlDbType.VarChar, 50)
        cmdA.Parameters("@acctnum").Value = acctnum
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Shared Function getPPCInfo()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("select top 10 * from CUST_INQUIRY", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTasklistProspect(ByVal tsk_campaign_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("xml_getTasklistProspect", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_campaign_code", SqlDbType.VarChar, 20)
        cmdA.Parameters("@tsk_campaign_code").Value = tsk_campaign_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTasklistCampaign(ByVal tsk_campaign_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("xml_getTasklistCampaign", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_campaign_code", SqlDbType.VarChar, 20)
        cmdA.Parameters("@tsk_campaign_code").Value = tsk_campaign_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getCustFRB(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("xml_getCustFRB", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 50)
        cmdA.Parameters("@csv").Value = csv
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function getTdDetail(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("xml_getTdDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 50)
        cmdA.Parameters("@csv").Value = csv
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTasklistView(ByVal usr_rm)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar, 50)
        cmdA.Parameters("@usrid").Value = usr_rm
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTasklistDemo(ByVal uniqCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getTasklistDemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uniqCode", SqlDbType.VarChar, 50).Value = uniqCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getLevelView(ByVal passkey)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_getLevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@passkey", SqlDbType.VarChar, 50).Value = passkey
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function



    'Public Function getTasklistDetailView(ByVal tskcode, ByVal usrView)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr
    '    myConn.Open()
    '    Dim cmdA As New SqlCommand("sp_getTasklistDetail", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.Add("@tsk_id", SqlDbType.BigInt)
    '    cmdA.Parameters("@tsk_id").Value = tskcode
    '    cmdA.Parameters.Add("@usr_view", SqlDbType.VarChar, 50)
    '    cmdA.Parameters("@usr_view").Value = usrView
    '    Dim sda As New SqlDataAdapter(cmdA)
    '    Dim ds As New DataSet
    '    sda.Fill(ds)
    '    myConn.Close()
    '    Return ds.Tables(0)
    'End Function

    Public Function getTasklistDetailView(ByVal tskcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("xml_getTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@unique_code", SqlDbType.VarChar, 100).Value = tskcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function getProductDetailView(ByVal acctno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getCasaDetailByAccno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@accno", SqlDbType.VarChar, 50).Value = acctno
        cmdA.Parameters.Add("@cardno", SqlDbType.VarChar, 50).Value = cardno
        cmdA.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = tbl
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProductDetailView2(ByVal acctno, ByVal cardno, ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim sp_source As String
        If tbl = "CASATD_DTL" Then
            sp_source = "sp_getCasaDetailByAccno"
        ElseIf tbl = "CC_DTL" Then
            sp_source = "sp_getCCDetailByAccno"
        ElseIf tbl = "LOAN_DTL" Then
            sp_source = "sp_getLoanDetailByAccno"
        ElseIf tbl = "MF_DTL" Then
            sp_source = "sp_getMfDetailByAccno"
        ElseIf tbl = "BA_DTL" Then
            sp_source = "sp_getBaDetailByAccno"
        End If
        Dim cmdA As New SqlCommand(sp_source, myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@accno", SqlDbType.VarChar, 50).Value = acctno
        If Not tbl = "LOAN_DTL" Then
            cmdA.Parameters.Add("@cardno", SqlDbType.VarChar, 50).Value = cardno
        End If
        cmdA.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = tbl
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getPPCView(ByVal csv, ByVal order, ByVal field, ByVal value, ByVal sort)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_getPPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar).Value = csv
        cmdA.Parameters.Add("@order", SqlDbType.VarChar).Value = order
        cmdA.Parameters.Add("@field", SqlDbType.VarChar).Value = field
        cmdA.Parameters.Add("@value", SqlDbType.VarChar).Value = value
        cmdA.Parameters.Add("@sorting", SqlDbType.VarChar).Value = sort
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getAlertView(ByVal passkey)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_systemAlert", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@passkey", SqlDbType.VarChar, 16)
        cmdA.Parameters("@passkey").Value = passkey
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTRBView(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_cust_trb", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 100)
        cmdA.Parameters("@csv").Value = csv
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTRBLoanView(ByVal csv, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_cust_trb_pct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 100)
        cmdA.Parameters("@csv").Value = csv
        cmdA.Parameters.Add("@stat", SqlDbType.VarChar, 100)
        cmdA.Parameters("@stat").Value = status
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTRBLineView(ByVal csv)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_trbline", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 30)
        cmdA.Parameters("@csv").Value = csv
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getActivityListView()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_activity_list", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function saveProspect(ByVal custName, ByVal contPerson, ByVal phoneNumber, ByVal TypeOfCustomer, ByVal prodInterest, ByVal closingDate, ByVal estimatedAmount, ByVal currency, ByVal action)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_newProspect", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'tasklist Customer Prospect
        cmdA.Parameters.Add("@tsc_customer_name", SqlDbType.VarChar, 100).Value = custName
        cmdA.Parameters.Add("@tsc_contact_person_1", SqlDbType.VarChar, 100).Value = contPerson
        cmdA.Parameters.Add("@tsc_phone_1", SqlDbType.VarChar, 100).Value = phoneNumber
        cmdA.Parameters.Add("@tsc_customer_type", SqlDbType.VarChar, 100).Value = TypeOfCustomer

        'tasklist
        cmdA.Parameters.Add("@tsk_product_offer", SqlDbType.VarChar, 100).Value = prodInterest
        cmdA.Parameters.Add("@tsk_end_period", SqlDbType.VarChar, 100).Value = closingDate
        cmdA.Parameters.Add("@tsk_amount_estimate", SqlDbType.VarChar, 100).Value = estimatedAmount
        cmdA.Parameters.Add("@tsk_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@tsk_usr_rm", SqlDbType.VarChar, 100).Value = action
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function commentTasklist(ByVal tsk_id, ByVal tsk_notes, ByVal tsk_act, ByVal new_rm, ByVal tracking, ByVal usr, ByVal update_date, ByVal followup_date)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_commentTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_id", SqlDbType.BigInt).Value = tsk_id
        cmdA.Parameters.Add("@tsk_notes", SqlDbType.VarChar, 1000).Value = tsk_notes
        cmdA.Parameters.Add("@tsk_act", SqlDbType.VarChar, 50).Value = tsk_act
        cmdA.Parameters.Add("@new_rm", SqlDbType.VarChar, 50).Value = new_rm
        cmdA.Parameters.Add("@tracking", SqlDbType.Bit).Value = tracking
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@update_date", SqlDbType.VarChar, 50).Value = update_date
        cmdA.Parameters.Add("@followup_date", SqlDbType.VarChar, 50).Value = followup_date
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

End Class
