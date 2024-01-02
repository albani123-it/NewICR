Imports System.Data.SqlClient
Public Class lAccountManagement
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
        cmdA.Parameters.Add("@productType", SqlDbType.VarChar, 20).Value = productType
        cmdA.Parameters.Add("@category", SqlDbType.VarChar, 1).Value = category
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
        cmdA.Parameters.Add("@NPK_RM", SqlDbType.VarChar, 20).Value = RM
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getProductInterest(ByVal productType)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getProductInterest", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@productType", SqlDbType.VarChar, 20).Value = productType
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
        cmdA.Parameters.Add("@prodprtner", SqlDbType.VarChar, 50).Value = prodInterest
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
        cmdA.Parameters.Add("@prodprtner", SqlDbType.VarChar, 50).Value = prodInterest
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

    Public Function getAccounManagementList(ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("acm_getAccountManagementList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 50).Value = userid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getDetACM(ByVal acmID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getDetailAccountManagement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@acm_id", SqlDbType.BigInt).Value = acmID
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getLastStatus(ByVal Status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getLastStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@last_status", SqlDbType.VarChar, 20).Value = Status
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getDetACMGCN(ByVal GCN)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getDetailAccountManagementFromGCN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@gcn", SqlDbType.VarChar, 100).Value = GCN
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getDetACMCSV(ByVal CSV)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getDetailAccountManagementFromCSV", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CSVCode", SqlDbType.VarChar, 100).Value = CSV
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getViewACMCNUM(ByVal CNUM)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getViewAccountManagementProductCNUM", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CNUM", SqlDbType.VarChar, 100).Value = CNUM
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getViewACMCSV(ByVal CSV)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getViewAccountManagementProductCSV", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CSV", SqlDbType.VarChar, 100).Value = CSV
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Sub insertAccountManagement(ByVal csv, ByVal cnum, ByVal referrerID, ByVal referrerName, ByVal referrerRelation, ByVal business, ByVal custName, ByVal address, ByVal mainIndustry, ByVal contactPerson, ByVal phoneNumber, ByVal mobileNumber, ByVal extNumber, ByVal email, ByVal TOC, ByVal groupName, ByVal DOID, ByVal position, ByVal officeNumber, ByVal description, ByVal opprtunities)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insAccountManagement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@acm_csv", SqlDbType.VarChar, 100).Value = csv
        cmdA.Parameters.Add("@acm_cnum", SqlDbType.VarChar, 100).Value = cnum
        cmdA.Parameters.Add("@acm_referrer_id", SqlDbType.VarChar, 100).Value = referrerID
        cmdA.Parameters.Add("@acm_referrer_name", SqlDbType.VarChar, 100).Value = referrerName
        cmdA.Parameters.Add("@acm_referrer_relation", SqlDbType.VarChar, 100).Value = referrerRelation
        cmdA.Parameters.Add("@acm_business", SqlDbType.VarChar, 100).Value = business
        cmdA.Parameters.Add("@acm_customer_name", SqlDbType.VarChar, 100).Value = custName
        cmdA.Parameters.Add("@acm_address", SqlDbType.VarChar).Value = address
        cmdA.Parameters.Add("@acm_main_industry", SqlDbType.VarChar, 100).Value = mainIndustry
        cmdA.Parameters.Add("@acm_contact_person", SqlDbType.VarChar, 100).Value = contactPerson
        cmdA.Parameters.Add("@acm_phone_number", SqlDbType.VarChar, 100).Value = phoneNumber
        cmdA.Parameters.Add("@acm_mobile_number", SqlDbType.VarChar, 100).Value = mobileNumber
        cmdA.Parameters.Add("@acm_email", SqlDbType.VarChar, 100).Value = email
        cmdA.Parameters.Add("@acm_customer_type", SqlDbType.VarChar, 100).Value = TOC
        cmdA.Parameters.Add("@acm_group_name", SqlDbType.VarChar, 100).Value = groupName
        cmdA.Parameters.Add("@acm_doid", SqlDbType.VarChar, 20).Value = DOID
        cmdA.Parameters.Add("@acm_position", SqlDbType.VarChar, 100).Value = position
        cmdA.Parameters.Add("@acm_office_number", SqlDbType.VarChar, 100).Value = officeNumber
        cmdA.Parameters.Add("@acm_description", SqlDbType.VarChar).Value = description
        cmdA.Parameters.Add("@acm_opportunities", SqlDbType.VarChar).Value = opprtunities
        cmdA.Parameters.Add("@acm_ext", SqlDbType.VarChar).Value = extNumber
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub updateAccountManagement(ByVal acmid, ByVal csv, ByVal cnum, ByVal referrerID, ByVal referrerName, ByVal referrerRelation, ByVal business, ByVal custName, ByVal address, ByVal mainIndustry, ByVal contactPerson, ByVal phoneNumber, ByVal mobileNumber, ByVal extNumber, ByVal email, ByVal TOC, ByVal groupName, ByVal DOID, ByVal position, ByVal officeNumber, ByVal description, ByVal opportunities)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_updateAccountManagement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@acm_id", SqlDbType.BigInt).Value = acmid
        cmdA.Parameters.Add("@acm_csv", SqlDbType.VarChar, 100).Value = csv
        cmdA.Parameters.Add("@acm_cnum", SqlDbType.VarChar, 100).Value = cnum
        cmdA.Parameters.Add("@acm_referrer_id", SqlDbType.VarChar, 100).Value = referrerID
        cmdA.Parameters.Add("@acm_referrer_name", SqlDbType.VarChar, 100).Value = referrerName
        cmdA.Parameters.Add("@acm_referrer_relation", SqlDbType.VarChar, 100).Value = referrerRelation
        cmdA.Parameters.Add("@acm_business", SqlDbType.VarChar, 100).Value = business
        cmdA.Parameters.Add("@acm_customer_name", SqlDbType.VarChar, 100).Value = custName
        cmdA.Parameters.Add("@acm_address", SqlDbType.VarChar).Value = address
        cmdA.Parameters.Add("@acm_main_industry", SqlDbType.VarChar, 100).Value = mainIndustry
        cmdA.Parameters.Add("@acm_contact_person", SqlDbType.VarChar, 100).Value = contactPerson
        'cmdA.Parameters.Add("@acm_phone_number", SqlDbType.VarChar, 100).Value = phoneNumber
        cmdA.Parameters.Add("@acm_mobile_number", SqlDbType.VarChar, 100).Value = mobileNumber
        cmdA.Parameters.Add("@acm_email", SqlDbType.VarChar, 100).Value = email
        cmdA.Parameters.Add("@acm_customer_type", SqlDbType.VarChar, 100).Value = TOC
        cmdA.Parameters.Add("@acm_group_name", SqlDbType.VarChar, 100).Value = groupName
        'cmdA.Parameters.Add("@acm_doid", SqlDbType.VarChar).Value = DOID
        cmdA.Parameters.Add("@acm_position", SqlDbType.VarChar, 100).Value = position
        cmdA.Parameters.Add("@acm_office_number", SqlDbType.VarChar, 100).Value = officeNumber
        cmdA.Parameters.Add("@acm_description", SqlDbType.VarChar).Value = description
        cmdA.Parameters.Add("@acm_opportunities", SqlDbType.VarChar).Value = opportunities
        cmdA.Parameters.Add("@acm_ext", SqlDbType.VarChar).Value = extNumber
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub insertAccountManagementProduct(ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodPartnerCD, ByVal MCM, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal start, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insAccountManagementProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@amp_acm_id", SqlDbType.VarChar, 100).Value = ""
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 100).Value = tasklistCode
        cmdA.Parameters.Add("@amp_product_type", SqlDbType.VarChar, 100).Value = prodType
        cmdA.Parameters.Add("@amp_product_interest", SqlDbType.VarChar, 100).Value = prodInterest
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 20).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 20).Value = prodPartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 20).Value = MCM
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_status", SqlDbType.VarChar, 100).Value = lastStatus
        cmdA.Parameters.Add("@amp_progress_start", SqlDbType.DateTime).Value = progressStart
        cmdA.Parameters.Add("@amp_status", SqlDbType.VarChar, 100).Value = status
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = start
        cmdA.Parameters.Add("@amp_usr_created", SqlDbType.VarChar, 100).Value = usrCreate
        cmdA.Parameters.Add("@amp_create_date", SqlDbType.DateTime).Value = createdate
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = DBNull.Value
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = DBNull.Value

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub
    Public Sub insertAccountManagementProduct2(ByVal acc_mgt_id, ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodPartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal start, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insAccountManagementProductSecond", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@amp_acm_id", SqlDbType.VarChar, 100).Value = acc_mgt_id
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 100).Value = tasklistCode
        cmdA.Parameters.Add("@amp_product_type", SqlDbType.VarChar, 100).Value = prodType
        cmdA.Parameters.Add("@amp_product_interest", SqlDbType.VarChar, 20).Value = prodInterest
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 20).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 20).Value = prodPartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 20).Value = mcm
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_status", SqlDbType.VarChar, 100).Value = lastStatus
        cmdA.Parameters.Add("@amp_progress_start", SqlDbType.DateTime).Value = progressStart
        cmdA.Parameters.Add("@amp_status", SqlDbType.VarChar, 100).Value = status
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = start
        cmdA.Parameters.Add("@amp_usr_created", SqlDbType.VarChar, 100).Value = usrCreate
        cmdA.Parameters.Add("@amp_create_date", SqlDbType.DateTime).Value = createdate
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = lastupdateby
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = lastupdate

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub updateProductOnlyAndTasklist(ByVal ampHid, ByVal acc_mgt_id, ByVal prodPartner, ByVal prodpartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastupdateby, ByVal lastupdate, ByVal mainindustry, ByVal referreName)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_updateProductOnly", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@amp_id", SqlDbType.VarChar, 100).Value = ampHid
        cmdA.Parameters.Add("@amp_acm_id", SqlDbType.VarChar, 100).Value = acc_mgt_id
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 100).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 100).Value = prodpartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 100).Value = mcm
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = lastupdateby
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = lastupdate

        cmdA.Parameters.Add("@tsk_main_industry", SqlDbType.VarChar, 100).Value = mainindustry
        cmdA.Parameters.Add("@tsk_customer_ref_id", SqlDbType.VarChar, 100).Value = referreName
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub
    Public Sub updateProductPlanning(ByVal amphid, ByVal acc_mgt_id, ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodpartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal start, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_updateProductExist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@amp_id", SqlDbType.VarChar, 100).Value = amphid
        cmdA.Parameters.Add("@amp_acm_id", SqlDbType.VarChar, 100).Value = acc_mgt_id
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 100).Value = tasklistCode
        cmdA.Parameters.Add("@amp_product_type", SqlDbType.VarChar, 100).Value = prodType
        cmdA.Parameters.Add("@amp_product_interest", SqlDbType.VarChar, 100).Value = prodInterest
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 20).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 20).Value = prodpartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 20).Value = mcm
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_status", SqlDbType.VarChar, 100).Value = lastStatus
        cmdA.Parameters.Add("@amp_progress_start", SqlDbType.DateTime).Value = progressStart
        cmdA.Parameters.Add("@amp_status", SqlDbType.VarChar, 100).Value = status
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = start
        cmdA.Parameters.Add("@amp_usr_created", SqlDbType.VarChar, 100).Value = usrCreate
        cmdA.Parameters.Add("@amp_create_date", SqlDbType.DateTime).Value = createdate
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = lastupdateby
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = lastupdate
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub updateProduct(ByVal IDProduct, ByVal cekStart)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_updateProductStart", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@IDproduct", SqlDbType.BigInt).Value = IDProduct
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = cekStart
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub delPRoductPlanningTemp()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_delPRoductPlanningTemp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub insertProductPlanning(ByVal id, ByVal acm_id, ByVal product, ByVal productpartner, ByVal productpartnercd, ByVal estimated, ByVal currency, ByVal revenue, ByVal checkStart)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insertProductPlanning", myConn)
        If checkStart = True Then
            checkStart = 1
        Else
            checkStart = 0
        End If
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@acm_id", SqlDbType.BigInt).Value = acm_id
        cmdA.Parameters.Add("@acm_product", SqlDbType.VarChar, 100).Value = product
        cmdA.Parameters.Add("@acm_productPartner", SqlDbType.VarChar, 100).Value = productpartner
        cmdA.Parameters.Add("@acm_productPartnerCD", SqlDbType.VarChar, 100).Value = productpartnercd
        cmdA.Parameters.Add("@acm_estimated", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@acm_currency", SqlDbType.VarChar, 20).Value = currency
        cmdA.Parameters.Add("@acm_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@acm_start", SqlDbType.Bit).Value = checkStart
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function cekProductID(ByVal IDProduct)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_checkProductExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@IDProduct", SqlDbType.VarChar, 50).Value = IDProduct
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Public Sub insertTask(ByVal acmid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insertProductToTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@acmid", SqlDbType.BigInt).Value = acmid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function cek_ampID(ByVal amp_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_checkProductExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@IDProduct", SqlDbType.VarChar, 100).Value = amp_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function cek_ProductTrueTasklist(ByVal amp_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_checkProductTrueTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@IDProduct", SqlDbType.VarChar, 100).Value = amp_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function DeleteProduct(ByVal ampid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_DeleteProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@amp_id", SqlDbType.VarChar, 100).Value = ampid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function insertProductToTasklist(ByVal ampid, ByVal tasklistCode, ByVal uniqueCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_insertProductToTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ampid", SqlDbType.BigInt).Value = ampid
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 10).Value = tasklistCode
        cmdA.Parameters.Add("@tsk_unique_code", SqlDbType.VarChar, 50).Value = uniqueCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getCNUMDetail(ByVal cnumCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getDetailAccountManagementFromCNUM", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CNUM", SqlDbType.VarChar, 100).Value = cnumCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Sub InsTasklist(ByVal tsk_cust_ref_id, ByVal tsk_usr_create, ByVal tsk_usr_rm, ByVal tsk_csv, ByVal tsk_cnum, ByVal tsk_product_offer, ByVal tsk_estimated, ByVal tsk_main_industry, ByVal tsk_currency, ByVal tsk_last_update_by, ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodpartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal startCheck, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_InsTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_cust_ref_id", SqlDbType.VarChar, 100).Value = tsk_cust_ref_id
        cmdA.Parameters.Add("@tsk_usr_create", SqlDbType.VarChar, 100).Value = tsk_usr_create
        cmdA.Parameters.Add("@tsk_usr_rm", SqlDbType.VarChar, 100).Value = tsk_usr_rm
        cmdA.Parameters.Add("@tsk_csv", SqlDbType.VarChar, 100).Value = tsk_csv
        cmdA.Parameters.Add("@tsk_cnum", SqlDbType.VarChar, 100).Value = tsk_cnum
        cmdA.Parameters.Add("@tsk_product_offer", SqlDbType.VarChar, 100).Value = tsk_product_offer
        cmdA.Parameters.Add("@tsk_estimated", SqlDbType.VarChar, 100).Value = tsk_estimated
        cmdA.Parameters.Add("@tsk_main_industry", SqlDbType.VarChar, 100).Value = tsk_main_industry
        cmdA.Parameters.Add("@tsk_currency", SqlDbType.VarChar, 100).Value = tsk_currency
        cmdA.Parameters.Add("@tsk_last_update_by", SqlDbType.VarChar, 100).Value = tsk_last_update_by
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 100).Value = tasklistCode
        cmdA.Parameters.Add("@amp_product_type", SqlDbType.VarChar, 100).Value = prodType
        cmdA.Parameters.Add("@amp_product_interest", SqlDbType.VarChar, 100).Value = prodInterest
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 20).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 20).Value = prodpartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 20).Value = mcm
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_status", SqlDbType.VarChar, 100).Value = lastStatus
        cmdA.Parameters.Add("@amp_progress_start", SqlDbType.DateTime).Value = progressStart
        cmdA.Parameters.Add("@amp_status", SqlDbType.VarChar, 100).Value = status
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = startCheck
        cmdA.Parameters.Add("@amp_usr_created", SqlDbType.VarChar, 100).Value = usrCreate
        cmdA.Parameters.Add("@amp_create_date", SqlDbType.DateTime).Value = createdate
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = DBNull.Value
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = DBNull.Value
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub InsTasklist2(ByVal tsk_cust_ref_id, ByVal tsk_usr_create, ByVal tsk_usr_rm, ByVal tsk_csv, ByVal tsk_cnum, ByVal tsk_product_offer, ByVal tsk_estimated, ByVal tsk_main_industry, ByVal tsk_currency, ByVal tsk_last_update_by, ByVal amp_acm_id, ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodpartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal startCheck, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_InsTasklist2", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_cust_ref_id", SqlDbType.VarChar, 100).Value = tsk_cust_ref_id
        cmdA.Parameters.Add("@tsk_usr_create", SqlDbType.VarChar, 100).Value = tsk_usr_create
        cmdA.Parameters.Add("@tsk_usr_rm", SqlDbType.VarChar, 100).Value = tsk_usr_rm
        cmdA.Parameters.Add("@tsk_csv", SqlDbType.VarChar, 100).Value = tsk_csv
        cmdA.Parameters.Add("@tsk_cnum", SqlDbType.VarChar, 100).Value = tsk_cnum
        cmdA.Parameters.Add("@tsk_product_offer", SqlDbType.VarChar, 100).Value = tsk_product_offer
        cmdA.Parameters.Add("@tsk_estimated", SqlDbType.VarChar, 100).Value = tsk_estimated
        cmdA.Parameters.Add("@tsk_main_industry", SqlDbType.VarChar, 100).Value = tsk_main_industry
        cmdA.Parameters.Add("@tsk_currency", SqlDbType.VarChar, 100).Value = tsk_currency
        cmdA.Parameters.Add("@tsk_last_update_by", SqlDbType.VarChar, 100).Value = tsk_last_update_by
        cmdA.Parameters.Add("@amp_acm_id", SqlDbType.VarChar, 100).Value = amp_acm_id
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 100).Value = tasklistCode
        cmdA.Parameters.Add("@amp_product_type", SqlDbType.VarChar, 100).Value = prodType
        cmdA.Parameters.Add("@amp_product_interest", SqlDbType.VarChar, 100).Value = prodInterest
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 20).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 20).Value = prodpartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 20).Value = mcm
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_status", SqlDbType.VarChar, 100).Value = lastStatus
        cmdA.Parameters.Add("@amp_progress_start", SqlDbType.DateTime).Value = progressStart
        cmdA.Parameters.Add("@amp_status", SqlDbType.VarChar, 100).Value = status
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = startCheck
        cmdA.Parameters.Add("@amp_usr_created", SqlDbType.VarChar, 100).Value = usrCreate
        cmdA.Parameters.Add("@amp_create_date", SqlDbType.DateTime).Value = createdate
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = DBNull.Value
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = DBNull.Value
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub updateProductPlanningToTasklist(ByVal tsk_cust_ref_id, ByVal tsk_usr_create, ByVal tsk_usr_rm, ByVal tsk_csv, ByVal tsk_cnum, ByVal tsk_product_offer, ByVal tsk_estimated, ByVal tsk_main_industry, ByVal tsk_currency, ByVal tsk_last_update_by, ByVal amphid, ByVal acc_mgt_id, ByVal tasklistCode, ByVal prodType, ByVal prodInterest, ByVal prodPartner, ByVal prodpartnerCD, ByVal mcm, ByVal estimated, ByVal currency, ByVal revenue, ByVal lastStatus, ByVal progressStart, ByVal status, ByVal start, ByVal usrCreate, ByVal createdate, ByVal lastupdateby, ByVal lastupdate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_updateProductExistToTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_cust_ref_id", SqlDbType.VarChar, 100).Value = tsk_cust_ref_id
        cmdA.Parameters.Add("@tsk_usr_create", SqlDbType.VarChar, 100).Value = tsk_usr_create
        cmdA.Parameters.Add("@tsk_usr_rm", SqlDbType.VarChar, 100).Value = tsk_usr_rm
        cmdA.Parameters.Add("@tsk_csv", SqlDbType.VarChar, 100).Value = tsk_csv
        cmdA.Parameters.Add("@tsk_cnum", SqlDbType.VarChar, 100).Value = tsk_cnum
        cmdA.Parameters.Add("@tsk_product_offer", SqlDbType.VarChar, 100).Value = tsk_product_offer
        cmdA.Parameters.Add("@tsk_estimated", SqlDbType.VarChar, 100).Value = tsk_estimated
        cmdA.Parameters.Add("@tsk_main_industry", SqlDbType.VarChar, 100).Value = tsk_main_industry
        cmdA.Parameters.Add("@tsk_currency", SqlDbType.VarChar, 100).Value = tsk_currency
        cmdA.Parameters.Add("@tsk_last_update_by", SqlDbType.VarChar, 100).Value = tsk_last_update_by
        cmdA.Parameters.Add("@amp_id", SqlDbType.VarChar, 100).Value = amphid
        cmdA.Parameters.Add("@amp_acm_id", SqlDbType.VarChar, 100).Value = acc_mgt_id
        cmdA.Parameters.Add("@amp_tasklist_code", SqlDbType.VarChar, 100).Value = tasklistCode
        cmdA.Parameters.Add("@amp_product_type", SqlDbType.VarChar, 100).Value = prodType
        cmdA.Parameters.Add("@amp_product_interest", SqlDbType.VarChar, 100).Value = prodInterest
        cmdA.Parameters.Add("@amp_product_partner", SqlDbType.VarChar, 20).Value = prodPartner
        cmdA.Parameters.Add("@amp_product_partner_cd", SqlDbType.VarChar, 20).Value = prodpartnerCD
        cmdA.Parameters.Add("@amp_mcm", SqlDbType.VarChar, 20).Value = mcm
        cmdA.Parameters.Add("@amp_estimated_amount", SqlDbType.Decimal, 18).Value = estimated
        cmdA.Parameters.Add("@amp_currency", SqlDbType.VarChar, 100).Value = currency
        cmdA.Parameters.Add("@amp_expected_revenue", SqlDbType.Decimal, 18).Value = revenue
        cmdA.Parameters.Add("@amp_last_status", SqlDbType.VarChar, 100).Value = lastStatus
        cmdA.Parameters.Add("@amp_progress_start", SqlDbType.DateTime).Value = progressStart
        cmdA.Parameters.Add("@amp_status", SqlDbType.VarChar, 100).Value = status
        cmdA.Parameters.Add("@amp_start", SqlDbType.Bit).Value = start
        cmdA.Parameters.Add("@amp_usr_created", SqlDbType.VarChar, 100).Value = usrCreate
        cmdA.Parameters.Add("@amp_create_date", SqlDbType.DateTime).Value = createdate
        cmdA.Parameters.Add("@amp_last_update_by", SqlDbType.VarChar, 100).Value = lastupdateby
        cmdA.Parameters.Add("@amp_last_update", SqlDbType.DateTime).Value = lastupdate
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function chkUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_checkUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 20).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function getOfficer(ByVal usrid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_getOfficerInformation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usrid").Value = usrid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function CheckCSVExists(ByVal csvcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_checkCSVExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CSVCode", SqlDbType.VarChar, 100).Value = csvcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function CheckCNUMExists(ByVal cnumCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acm_checkCNUMExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@CNUMCode", SqlDbType.VarChar, 100).Value = cnumCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
