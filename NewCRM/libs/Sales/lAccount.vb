Imports System.Data.SqlClient

Public Class lAccount
    Public Function calculateAnnuitas(ByVal amount, ByVal interest, ByVal term)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("acc_calcAnnuitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@amount", amount)
        cmdA.Parameters.AddWithValue("@interest", interest)
        cmdA.Parameters.AddWithValue("@term", term)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getAccounManagementList(ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("acm_getAccountManagementList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getOfficer(ByVal usrid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getOfficerInformation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usrid", usrid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getMainIndustryAcm()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getMainIndustry", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getPositionAcm()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProductType(ByVal productType, ByVal category)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getProductType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@productType", productType)
        cmdA.Parameters.AddWithValue("@category", category)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getMCM(ByVal RM)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getMCM", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@NPK_RM", RM)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getCurrency()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getCurrency", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Sub insertAccountManagement(csv, cnum, referrer, refName, refRelation, typeOfCust, groupName, business, DOID, CustName, address, mainindustry, conPerson, position, OfficePhoneNumber, mobileNumber, extNumber, email, businessDescription, opportunities, usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insAccountManagement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", usrLogin)
        cmdA.Parameters.AddWithValue("@acm_csv", csv)
        cmdA.Parameters.AddWithValue("@acm_cnum", cnum)
        cmdA.Parameters.AddWithValue("@acm_referrer_id", referrer)
        cmdA.Parameters.AddWithValue("@acm_referrer_name", refName)
        cmdA.Parameters.AddWithValue("@acm_referrer_relation", refRelation)
        cmdA.Parameters.AddWithValue("@acm_business", business)
        cmdA.Parameters.AddWithValue("@acm_customer_name", CustName)
        cmdA.Parameters.AddWithValue("@acm_address", address)
        cmdA.Parameters.AddWithValue("@acm_main_industry", mainindustry)
        cmdA.Parameters.AddWithValue("@acm_contact_person", conPerson)
        cmdA.Parameters.AddWithValue("@acm_office_phone_number", OfficePhoneNumber)
        cmdA.Parameters.AddWithValue("@acm_mobile_number", mobileNumber)
        cmdA.Parameters.AddWithValue("@acm_email", email)
        cmdA.Parameters.AddWithValue("@acm_customer_type", typeOfCust)
        cmdA.Parameters.AddWithValue("@acm_group_name", groupName)
        cmdA.Parameters.AddWithValue("@acm_doid", DOID)
        cmdA.Parameters.AddWithValue("@acm_position", position)
        cmdA.Parameters.AddWithValue("@acm_description", businessDescription)
        cmdA.Parameters.AddWithValue("@acm_opportunities", opportunities)
        cmdA.Parameters.AddWithValue("@acm_ext", extNumber)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function getProductInterest(ByVal productType)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getProductInterest", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@productType", productType)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getProdPart(ByVal prodInterest)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getProductPartner", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@prodprtner", prodInterest)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getProdPartCD(ByVal prodInterest)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getProductPartnerCD", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@prodprtner", prodInterest)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Public Sub InsertToTasklist(ByVal tsk_cust_ref_id, ByVal tsk_usr_create, ByVal tsk_usr_rm, ByVal tsk_csv, ByVal tsk_cnum, ByVal tsk_product_offer, ByVal tsk_estimated, ByVal tsk_main_industry, ByVal tsk_currency, ByVal tsk_last_update_by, ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodpartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal startCheck, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_InsertToTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@tsk_cust_ref_id", tsk_cust_ref_id)
        cmdA.Parameters.AddWithValue("@tsk_usr_create", tsk_usr_create)
        cmdA.Parameters.AddWithValue("@tsk_usr_rm", tsk_usr_rm)
        cmdA.Parameters.AddWithValue("@tsk_csv", tsk_csv)
        cmdA.Parameters.AddWithValue("@tsk_cnum", tsk_cnum)
        cmdA.Parameters.AddWithValue("@tsk_product_offer", tsk_product_offer)
        cmdA.Parameters.AddWithValue("@tsk_estimated", tsk_estimated)
        cmdA.Parameters.AddWithValue("@tsk_main_industry", tsk_main_industry)
        cmdA.Parameters.AddWithValue("@tsk_currency", tsk_currency)
        cmdA.Parameters.AddWithValue("@tsk_last_update_by", tsk_last_update_by)
        cmdA.Parameters.AddWithValue("@amp_tasklist_code", tasklistCode)
        cmdA.Parameters.AddWithValue("@amp_product_type", prodType)
        cmdA.Parameters.AddWithValue("@amp_product_interest", prodInterest)
        cmdA.Parameters.AddWithValue("@amp_product_partner", prodPartner)
        cmdA.Parameters.AddWithValue("@amp_product_partner_cd", prodpartnerCD)
        cmdA.Parameters.AddWithValue("@amp_mcm", mcm)
        cmdA.Parameters.AddWithValue("@amp_estimated_amount", estimated)
        cmdA.Parameters.AddWithValue("@amp_currency", currency)
        cmdA.Parameters.AddWithValue("@amp_expected_revenue", revenue)
        cmdA.Parameters.AddWithValue("@amp_last_status", lastStatus)
        cmdA.Parameters.AddWithValue("@amp_progress_start", progressStart)
        cmdA.Parameters.AddWithValue("@amp_status", status)
        cmdA.Parameters.AddWithValue("@amp_start", startCheck)
        cmdA.Parameters.AddWithValue("@amp_usr_created", usrCreate)
        cmdA.Parameters.AddWithValue("@amp_create_date", createdate)
        cmdA.Parameters.AddWithValue("@amp_last_update_by", DBNull.Value)
        cmdA.Parameters.AddWithValue("@amp_last_update", DBNull.Value)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub insertAccountManagementProduct(ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodPartnerCD, ByVal MCM, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal start, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insertAccountManagementProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@amp_acm_id", "")
        cmdA.Parameters.AddWithValue("@amp_tasklist_code", tasklistCode)
        cmdA.Parameters.AddWithValue("@amp_product_type", prodType)
        cmdA.Parameters.AddWithValue("@amp_product_interest", prodInterest)
        cmdA.Parameters.AddWithValue("@amp_product_partner", prodPartner)
        cmdA.Parameters.AddWithValue("@amp_product_partner_cd", prodPartnerCD)
        cmdA.Parameters.AddWithValue("@amp_mcm", MCM)
        cmdA.Parameters.AddWithValue("@amp_estimated_amount", estimated)
        cmdA.Parameters.AddWithValue("@amp_currency", currency)
        cmdA.Parameters.AddWithValue("@amp_expected_revenue", revenue)
        cmdA.Parameters.AddWithValue("@amp_last_status", lastStatus)
        cmdA.Parameters.AddWithValue("@amp_progress_start", progressStart)
        cmdA.Parameters.AddWithValue("@amp_status", status)
        cmdA.Parameters.AddWithValue("@amp_start", start)
        cmdA.Parameters.AddWithValue("@amp_usr_created", usrCreate)
        cmdA.Parameters.AddWithValue("@amp_create_date", createdate)
        cmdA.Parameters.AddWithValue("@amp_last_update_by", DBNull.Value)
        cmdA.Parameters.AddWithValue("@amp_last_update", DBNull.Value)

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

End Class
