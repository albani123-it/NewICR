Imports System.Data.SqlClient
Public Class lProspect
    Public Function getID()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getID", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
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
        Dim cmdA As New SqlCommand("pr_getOfficer", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usrid").Value = usrid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getMainIndustry()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getMainIndustry", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getAssignedUser(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getAssignedUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar, 16)
        cmdA.Parameters("@id").Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        'Return ds.Tables(0).Rows(0).Item(0)
        Return ds.Tables(0)
    End Function

    Public Function getProduct(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 30)
        cmdA.Parameters("@tipe").Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getproduct_userid(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 30)
        cmdA.Parameters("@tipe").Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getTipe(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usrid").Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Sub insertPros(ByVal tsc_csv, ByVal tsc_cnum, ByVal tsc_customer_name, ByVal tsc_group_name, ByVal tsc_address, ByVal tsc_birthdate, ByVal tsc_gender, ByVal tsc_badan_usaha, ByVal tsc_contact_person_1, ByVal tsc_contact_person_2, ByVal tsc_position_1, ByVal tsc_refer, ByVal tsc_phone_1, ByVal tsc_email_1, ByVal tsc_customer_type, ByVal tsc_marital, ByVal tsc_mobile, ByVal tsc_office)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_insertPros", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsc_csv", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_csv").Value = tsc_csv
        cmdA.Parameters.Add("@tsc_cnum", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_cnum").Value = tsc_cnum
        cmdA.Parameters.Add("@tsc_customer_name", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_customer_name").Value = tsc_customer_name
        cmdA.Parameters.Add("@tsc_group_name", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_group_name").Value = tsc_group_name
        cmdA.Parameters.Add("@tsc_address", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_address").Value = tsc_address
        cmdA.Parameters.Add("@tsc_birthdate", SqlDbType.VarChar, 50)
        cmdA.Parameters("@tsc_birthdate").Value = tsc_birthdate
        cmdA.Parameters.Add("@tsc_gender", SqlDbType.Char, 1)
        cmdA.Parameters("@tsc_gender").Value = tsc_gender
        cmdA.Parameters.Add("@tsc_badan_usaha", SqlDbType.VarChar, 50)
        cmdA.Parameters("@tsc_badan_usaha").Value = tsc_badan_usaha
        cmdA.Parameters.Add("@tsc_contact_person_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_contact_person_1").Value = tsc_contact_person_1
        cmdA.Parameters.Add("@tsc_contact_person_2", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_contact_person_2").Value = tsc_contact_person_2
        cmdA.Parameters.Add("@tsc_position_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_position_1").Value = tsc_position_1
        cmdA.Parameters.Add("@tsc_refer", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_refer").Value = tsc_refer
        cmdA.Parameters.Add("@tsc_phone_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_phone_1").Value = tsc_phone_1
        cmdA.Parameters.Add("@tsc_email_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_email_1").Value = tsc_email_1
        cmdA.Parameters.Add("@tsc_customer_type", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_customer_type").Value = tsc_customer_type
        cmdA.Parameters.Add("@tsc_marital", SqlDbType.Char, 1)
        cmdA.Parameters("@tsc_marital").Value = tsc_marital
        cmdA.Parameters.Add("@tsc_mobile", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_mobile").Value = tsc_mobile
        cmdA.Parameters.Add("@tsc_office", SqlDbType.VarChar, 50)
        cmdA.Parameters("@tsc_office").Value = tsc_office
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub updatePros(ByVal tscid, ByVal tsc_csv, ByVal tsc_cnum, ByVal tsc_customer_name, ByVal tsc_group_name, ByVal tsc_address, ByVal tsc_birthdate, ByVal tsc_gender, ByVal tsc_badan_usaha, ByVal tsc_contact_person_1, ByVal tsc_contact_person_2, ByVal tsc_position_1, ByVal tsc_refer, ByVal tsc_phone_1, ByVal tsc_email_1, ByVal tsc_customer_type, ByVal tsc_marital, ByVal tsc_mobile, ByVal tsc_office)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_updatePros", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tscid", SqlDbType.BigInt)
        cmdA.Parameters("@tscid").Value = tscid
        cmdA.Parameters.Add("@tsc_csv", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_csv").Value = tsc_csv
        cmdA.Parameters.Add("@tsc_cnum", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_cnum").Value = tsc_cnum
        cmdA.Parameters.Add("@tsc_customer_name", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_customer_name").Value = tsc_customer_name
        cmdA.Parameters.Add("@tsc_group_name", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_group_name").Value = tsc_group_name
        cmdA.Parameters.Add("@tsc_address", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_address").Value = tsc_address
        'cmdA.Parameters.Add("@tsc_birthdate", SqlDbType.VarChar, 50)
        'cmdA.Parameters("@tsc_birthdate").Value = tsc_birthdate
        cmdA.Parameters.Add("@tsc_gender", SqlDbType.Char, 1)
        cmdA.Parameters("@tsc_gender").Value = tsc_gender
        cmdA.Parameters.Add("@tsc_badan_usaha", SqlDbType.VarChar, 50)
        cmdA.Parameters("@tsc_badan_usaha").Value = tsc_badan_usaha
        cmdA.Parameters.Add("@tsc_contact_person_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_contact_person_1").Value = tsc_contact_person_1
        cmdA.Parameters.Add("@tsc_contact_person_2", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_contact_person_2").Value = tsc_contact_person_2
        cmdA.Parameters.Add("@tsc_position_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_position_1").Value = tsc_position_1
        cmdA.Parameters.Add("@tsc_refer", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_refer").Value = tsc_refer
        cmdA.Parameters.Add("@tsc_phone_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_phone_1").Value = tsc_phone_1
        cmdA.Parameters.Add("@tsc_email_1", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_email_1").Value = tsc_email_1
        cmdA.Parameters.Add("@tsc_customer_type", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_customer_type").Value = tsc_customer_type
        cmdA.Parameters.Add("@tsc_marital", SqlDbType.Char, 1)
        cmdA.Parameters("@tsc_marital").Value = tsc_marital
        cmdA.Parameters.Add("@tsc_mobile", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsc_mobile").Value = tsc_mobile
        cmdA.Parameters.Add("@tsc_office", SqlDbType.VarChar, 50)
        cmdA.Parameters("@tsc_office").Value = tsc_office
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub insertTask(ByVal tsk_unique_code, ByVal tsk_counter, ByVal tsk_create_date, ByVal tsk_last_update, ByVal tsk_customer_ref_id, ByVal tsk_usr_created, ByVal tsk_usr_rm, ByVal tsk_csv, ByVal tsk_cnum, ByVal tsk_activity_type, ByVal tsk_product_offer, ByVal tsk_amount_estimate, ByVal tsk_currency, ByVal tsk_main_industry, ByVal tsk_notes, ByVal tsk_campaign_code, ByVal tsk_end_period, ByVal tsk_status, ByVal tsk_is_read, ByVal tsk_last_update_by, ByVal tsk_usr_br)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_insertTask", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_unique_code", SqlDbType.VarChar, 50).Value = tsk_unique_code
        cmdA.Parameters.Add("@tsk_counter", SqlDbType.Int).Value = tsk_counter
        cmdA.Parameters.Add("@tsk_create_date", SqlDbType.DateTime).Value = tsk_create_date
        cmdA.Parameters.Add("@tsk_last_update", SqlDbType.DateTime).Value = tsk_last_update
        cmdA.Parameters.Add("@tsk_customer_ref_id", SqlDbType.VarChar, 50).Value = tsk_customer_ref_id
        cmdA.Parameters.Add("@tsk_usr_created", SqlDbType.VarChar, 50).Value = tsk_usr_created
        cmdA.Parameters.Add("@tsk_usr_rm", SqlDbType.VarChar, 50).Value = tsk_usr_rm
        cmdA.Parameters.Add("@tsk_csv", SqlDbType.VarChar, 100).Value = tsk_csv
        cmdA.Parameters.Add("@tsk_cnum", SqlDbType.VarChar, 100).Value = tsk_cnum
        cmdA.Parameters.Add("@tsk_activity_type", SqlDbType.VarChar, 50).Value = tsk_activity_type
        cmdA.Parameters.Add("@tsk_product_offer", SqlDbType.VarChar, 100).Value = tsk_product_offer
        cmdA.Parameters.Add("@tsk_amount_estimate", SqlDbType.Decimal, 18).Value = tsk_amount_estimate
        cmdA.Parameters.Add("@tsk_currency", SqlDbType.VarChar, 50).Value = tsk_currency
        cmdA.Parameters.Add("@tsk_main_industry", SqlDbType.VarChar, 500).Value = tsk_main_industry
        cmdA.Parameters.Add("@tsk_notes", SqlDbType.Text).Value = tsk_notes
        cmdA.Parameters.Add("@tsk_campaign_code", SqlDbType.VarChar, 50).Value = tsk_campaign_code
        cmdA.Parameters.Add("@tsk_end_period", SqlDbType.Date).Value = tsk_end_period
        cmdA.Parameters.Add("@tsk_status", SqlDbType.VarChar, 50).Value = tsk_status
        cmdA.Parameters.Add("@tsk_is_read", SqlDbType.TinyInt).Value = tsk_is_read
        cmdA.Parameters.Add("@tsk_last_update_by", SqlDbType.VarChar, 50).Value = tsk_last_update_by
        cmdA.Parameters.Add("@tsk_usr_br", SqlDbType.VarChar, 50).Value = tsk_usr_br
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function getProsData(ByVal tscid, ByVal tskid, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getProsData", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tscid", SqlDbType.VarChar, 16)
        cmdA.Parameters("@tscid").Value = tscid
        cmdA.Parameters.Add("@tskid", SqlDbType.VarChar, 16)
        cmdA.Parameters("@tskid").Value = tskid
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 16)
        cmdA.Parameters("@code").Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProsData2(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_getProsData2", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@csv", SqlDbType.VarChar, 16)
        cmdA.Parameters("@csv").Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Public Function cekCompany(ByVal company)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_cekCompany", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@company", SqlDbType.VarChar, 100)
        cmdA.Parameters("@company").Value = company
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getCompany(ByVal codeBehind)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdI As New SqlCommand("pr_getCompany", myConn)
        cmdI.CommandType = CommandType.StoredProcedure
        cmdI.Parameters.Add("@code", SqlDbType.VarChar, 100)
        cmdI.Parameters("@code").Value = codeBehind
        Dim sda As New SqlDataAdapter(cmdI)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getBranchWithUser()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("prm_getBranchWithUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getListOfProspectList(ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getProspectList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar, 50).Value = userid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function cekUserReference(ByVal cekUser)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "SELECT COUNT(user_type) as rowT FROM dtupload_user_reference WHERE userid = '" & cekUser & "'"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getTypePros()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "SELECT distinct(type) as Type FROM offering where isDelete=0 AND Type <> '' order by Type ASC"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Sub updateTask(ByVal tsk_id, ByVal tsk_customer_ref_id, ByVal tsk_main_industry, ByVal tsk_amount_estimate, ByVal tsk_currency)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("pr_updateTask", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_id", SqlDbType.VarChar, 10)
        cmdA.Parameters("@tsk_id").Value = tsk_id
        cmdA.Parameters.Add("@tsk_customer_ref_id", SqlDbType.VarChar, 100)
        cmdA.Parameters("@tsk_customer_ref_id").Value = tsk_customer_ref_id
        'cmdA.Parameters.Add("@tsk_end_period", SqlDbType.Date)
        'cmdA.Parameters("@tsk_end_period").Value = tsk_end_period
        cmdA.Parameters.Add("@tsk_main_industry", SqlDbType.VarChar, 500)
        cmdA.Parameters("@tsk_main_industry").Value = tsk_main_industry
        cmdA.Parameters.Add("@tsk_amount_estimate", SqlDbType.Decimal, 18)
        cmdA.Parameters("@tsk_amount_estimate").Value = tsk_amount_estimate
        cmdA.Parameters.Add("@tsk_currency", SqlDbType.VarChar, 10)
        cmdA.Parameters("@tsk_currency").Value = tsk_currency
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function getProsDetail(ByVal tsk_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getProspectListDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_id", SqlDbType.VarChar, 20)
        cmdA.Parameters("@tsk_id").Value = tsk_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function commentNewProspect(ByVal tsk_id, ByVal tsk_act, ByVal new_rm, ByVal tracking, ByVal usr, ByVal followup_date)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_commentNewProspect", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_id", SqlDbType.BigInt).Value = tsk_id
        cmdA.Parameters.Add("@tsk_act", SqlDbType.VarChar, 50).Value = tsk_act
        cmdA.Parameters.Add("@new_rm", SqlDbType.VarChar, 50).Value = new_rm
        cmdA.Parameters.Add("@tracking", SqlDbType.Bit).Value = tracking
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@followup_date", SqlDbType.VarChar, 50).Value = followup_date
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function
End Class
