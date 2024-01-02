Imports System.Data.SqlClient

Public Class lApps
    Function checkAppModel(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppModel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub updateAppModel(ByVal appno, ByVal brwtype, ByVal prdtype, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateAppModel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@brwtype", brwtype)
        cmdA.Parameters.AddWithValue("@prdtype", prdtype)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getRM_inAppFlow(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("getRMfromAppflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

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

    Function getRatingDesc(ByVal rating, ByVal scc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBorrowerRatingDesc", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = rating
        'cmdA.Parameters.Add("@scc", SqlDbType.VarChar, 50).Value = scc
        cmdA.Parameters.AddWithValue("@rating", rating)
        cmdA.Parameters.AddWithValue("@scc", scc)

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

    Sub insertRetailSummary(ByVal appno, ByVal brwcode, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("ceg_retail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListOfMenuBySector(ByVal sec)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@sector", SqlDbType.Int).Value = sec
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getListOfMenu()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getNextMenuByWorkflowRole(ByVal id, ByVal posisi, ByVal menuid, ByVal sec)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getNextMenuByWorkflowRole", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        cmdA.Parameters.Add("@menuid", SqlDbType.Int).Value = menuid
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = sec
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getDetailOfWorkflow(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfWorkflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getDetailOfWorkflowProcess(ByVal workflow, ByVal posisi)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfWorkflowProcess", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = workflow
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getActionOfWorkflowProcess(ByVal workflow, ByVal posisi)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getActionOfWorkflowProcess", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = workflow
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkActionOfWorkflowProcessByVariable(ByVal appno, ByVal var, ByVal operand, ByVal value)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkActionOfWorkflowProcessByVariable", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@var", SqlDbType.VarChar, 250).Value = var
        cmdA.Parameters.Add("@operand", SqlDbType.VarChar, 50).Value = operand
        cmdA.Parameters.Add("@val", SqlDbType.VarChar, 2000).Value = value
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkActionOfWorkflowProcessBykre_rec_info(ByVal appno, ByVal var, ByVal operand, ByVal value)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkActionOfWorkflowProcessBylosnr_kre_rec_info", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@var", SqlDbType.VarChar, 250).Value = var
        cmdA.Parameters.Add("@operand", SqlDbType.VarChar, 50).Value = operand
        cmdA.Parameters.Add("@val", SqlDbType.VarChar, 2000).Value = value
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfMenu(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    'Function getChildOfMenu(ByVal id)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr_Analysis
    '    myConn.Open()
    '    Dim cmdA As New SqlCommand("app_getChildOfMenu", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
    '    Dim sda As New SqlDataAdapter(cmdA)
    '    Dim ds As New DataSet
    '    sda.Fill(ds)
    '    myConn.Close()
    '    Return ds.Tables(0)
    'End Function

    Function getChildOfMenu(ByVal id, ByVal sec)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getChildOfMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@sector", SqlDbType.Int).Value = sec
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Function getListOfApp()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfFailedRequest()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfFailedRequest", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfAssignApp(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfAssignApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfSubmitApp(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfSubmitApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfUnassignApp(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfUnassignApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfUnassignAppInbox(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfUnassignAppInbox", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub submitApp(ByVal appno, ByVal nextPosisi, ByVal usr, ByVal nextUsr, ByVal notes)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_submitApplication", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@nextPosisi", SqlDbType.Int).Value = nextPosisi
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@nextUsr", SqlDbType.VarChar, 50).Value = nextUsr
        cmdA.Parameters.Add("@notes", SqlDbType.VarChar, 50).Value = notes
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub submitAllHead(ByVal appno, ByVal brw_code, ByVal debitur, ByVal ca, ByVal head, ByVal usr, ByVal head_pos)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        'Dim cmdA As New SqlCommand("app_submitApplicationtoAllHead", myConn)
        Dim cmdA As New SqlCommand("app_submitAppGenAllHead", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@brw_code", brw_code)
        cmdA.Parameters.AddWithValue("@debitur", debitur)
        cmdA.Parameters.AddWithValue("@ca", ca)
        cmdA.Parameters.AddWithValue("@head", head)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@head_pos", head_pos)

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub submitAllDivHead(ByVal appno, ByVal brw_code, ByVal debitur, ByVal ca, ByVal head, ByVal usr, ByVal head_pos)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        'Dim cmdA As New SqlCommand("app_submitApplicationtoAllDivHead", myConn)
        Dim cmdA As New SqlCommand("app_submitAppGenDivHead", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@brw_code", brw_code)
        cmdA.Parameters.AddWithValue("@debitur", debitur)
        cmdA.Parameters.AddWithValue("@ca", ca)
        cmdA.Parameters.AddWithValue("@head", head)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@head_pos", head_pos)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub ResetApprovalHead(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_resetApprovalHead", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getAllRating(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getAllRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    'Function submitApp(ByVal appno, ByVal nextPosisi)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr_Analysis
    '    myConn.Open()
    '    Dim cmdA As New SqlCommand("app_submitApplication", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
    '    cmdA.Parameters.Add("@nextPosisi", SqlDbType.VarChar, 50).Value = nextPosisi
    '    'cmdA.Parameters.Add("@workflow", SqlDbType.VarChar, 50).Value = workflow
    '    'cmdA.Parameters.Add("@posisi", SqlDbType.VarChar, 50).Value = posisi
    '    Dim sda As New SqlDataAdapter(cmdA)
    '    Dim ds As New DataSet
    '    sda.Fill(ds)
    '    myConn.Close()
    '    Return ds.Tables(0)
    'End Function

    Function getListOfScorecard(ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfScorecard", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@category", SqlDbType.VarChar, 50).Value = cat
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfRetail()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfRetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfProduct()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfProductType(ByVal param)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("los_dropdownlist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@object", SqlDbType.VarChar, 50).Value = param
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfWorkflow()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfWorkflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfBorrowerType()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfBorrowerType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
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
        Dim cmdA As New SqlCommand("app_getDataCounter", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@param", SqlDbType.VarChar, 50).Value = param
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getMicroApp(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getMicroApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
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
        Dim cmdA As New SqlCommand("app_checkAppInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function CheckApproval(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkHeadApproval", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAppWithCIF(ByVal appno, ByVal cif)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppWithCIF", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@cif", cif)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Function checkAppWithNPWP(ByVal appno, ByVal npwp)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppWithNPWP", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@npwp", npwp)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAppByBrwCode(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppByBrwCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAppByCIF(ByVal cif)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppByCIF", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@cif", cif)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAppByNPWP(ByVal npwp)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppByNPWP", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@npwp", npwp)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBorrowerSector(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBorrowerSector", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProductType(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProductType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    'Function checkWorkflowRole(ByVal id, ByVal posisi)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr_Analysis
    '    myConn.Open()
    '    Dim cmdA As New SqlCommand("app_checkWorkflowRole", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
    '    cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
    '    Dim sda As New SqlDataAdapter(cmdA)
    '    Dim ds As New DataSet
    '    sda.Fill(ds)
    '    myConn.Close()
    '    Return ds
    'End Function

    Function checkWorkflowRole(ByVal id, ByVal posisi, ByVal sec)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkWorkflowRole", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        cmdA.Parameters.Add("@sector", SqlDbType.Int).Value = sec
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkDropdown(ByVal obj, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkDropdown", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@object", SqlDbType.VarChar, 50).Value = obj
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkGeneral(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkGeneralInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkGeneralByBrwCode(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkGeneralInfoByBrwCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
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
        Dim cmdA As New SqlCommand("app_checkBorrowerInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
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
        Dim cmdA As New SqlCommand("app_checkBorrowerByCIF", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@cif", cif)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBorrowerByAcctno(ByVal acctno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBorrowerByAcctno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acctno", acctno)
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
        Dim cmdA As New SqlCommand("app_checkBorrowerCouple", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkLegalitas(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkKeyPerson(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkIndividualShareholders(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkTotalIndividualShareholders(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkTotalIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkCorporateShareholders(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkTotalCorporateShareholders(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkTotalCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProfitLoss(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkReconciliation(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkReconciliation", myConn)
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
        Dim cmdA As New SqlCommand("app_checkLotsInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAspek(ByVal brwcode, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAppAspek(ByVal appno, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkCreditRecommendation(ByVal appno, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkCreditRecommendation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkCreditFinal(ByVal appno, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkCreditFinal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function reviewApp(ByVal appno, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_reviewApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAppUserByWorkflow(ByVal appno, ByVal workflow, ByVal posisi, ByVal nextPosisi)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getAppUserByWorkflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@workflow", SqlDbType.Int).Value = workflow
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        cmdA.Parameters.Add("@next", SqlDbType.Int).Value = nextPosisi
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getAppUserBeforeByWorkflow(ByVal appno, ByVal workflow, ByVal posisi, ByVal nextPosisi, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getAppUserBeforeByWorkflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@workflow", SqlDbType.Int).Value = workflow
        cmdA.Parameters.Add("@posisi", SqlDbType.Int).Value = posisi
        cmdA.Parameters.Add("@next", SqlDbType.Int).Value = nextPosisi
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBorrowerRating(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBorrowerRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkRetailRating(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkRetailRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfRatingDesc(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfRatingDesc", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = scc_code
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
        Dim cmdA As New SqlCommand("app_getDetailOfLegalitas", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
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
        Dim cmdA As New SqlCommand("app_getDetailOfKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
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
        Dim cmdA As New SqlCommand("app_getDetailOfIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
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
        Dim cmdA As New SqlCommand("app_getDetailOfCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfProfitLoss(ByVal brwcode, ByVal year, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfProfitLossByMonth(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfProfitLossByMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfBalanceSheet(ByVal brwcode, ByVal year, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfBalanceSheet", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfBalanceSheetByMonth(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfBalanceSheetByMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfReconciliation(ByVal brwcode, ByVal year, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfReconciliation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfReconciliationByMonth(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfReconciliationByMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfRatio(ByVal brwcode, ByVal year, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfRatioByMonth(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfRatioByMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfOtherFinRatio(ByVal brwcode, ByVal year, ByVal month)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfOtherFinRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfOtherFinRatioByMonth(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfOtherFinRatioByMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfCalculationWorkingCapital(ByVal brwcode, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfCalculationWorkingCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.VarChar, 4).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfBorrowerAspek(ByVal brwcode, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfBorrowerAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfAppAspek(ByVal appno, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfAppAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfFinalCredit(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfFinalCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfRecommendationCredit(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfRecommendationCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfRequestCredit(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfRequestCredit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfProduct(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getBankRatingSector(ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getBankRatingSector", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@type", SqlDbType.Int).Value = type
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getBankRatingProduct(ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getBankRatingProduct", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@type", SqlDbType.Int).Value = type
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

    Sub deleteKeyPerson(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteKeyPerson", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteIndividualShareholders(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteCorporateShareholders(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub assignApp(ByVal id, ByVal usrbefore, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_assignApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@usrBefore", SqlDbType.VarChar, 50).Value = usrbefore
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub assignAppInbox(ByVal id, ByVal usrbefore, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_assignAppInbox", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@usrBefore", SqlDbType.VarChar, 50).Value = usrbefore
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function insertUnderwriting(ByVal e As Array, ByVal cif As String, ByVal is_exists As Boolean, ByVal usr As String)
        'Dim chk As Boolean = False
        'If e(3) Then
        '    chk = True
        'End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUnderwriting", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        'cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.AddWithValue("@cif", cif)
        cmdA.Parameters.Add("@workflow", SqlDbType.VarChar, 50).Value = e(0)
        cmdA.Parameters.Add("@brw_type", SqlDbType.VarChar, 50).Value = e(1)
        'cmdA.Parameters.Add("@chkRetail", SqlDbType.Bit).Value = chk
        cmdA.Parameters.Add("@prd_type", SqlDbType.VarChar, 50).Value = e(2)
        cmdA.Parameters.Add("@appsType", SqlDbType.VarChar, 50).Value = e(3)
        cmdA.Parameters.Add("@appnoEx", SqlDbType.VarChar, 50).Value = e(4)
        cmdA.Parameters.Add("@is_exists", SqlDbType.Bit).Value = is_exists
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        'cmdA.ExecuteNonQuery()
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUnderwritingExists(ByVal e As Array, ByVal appno As String, ByVal brwcode As String, ByVal usr As String)
        Dim chk As Boolean = False
        If e(3) Then
            chk = True
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUnderwriting", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@workflow", SqlDbType.VarChar, 50).Value = e(0)
        cmdA.Parameters.Add("@brw_type", SqlDbType.VarChar, 50).Value = e(1)
        cmdA.Parameters.Add("@chkRetail", SqlDbType.Bit).Value = chk
        cmdA.Parameters.Add("@prd_type", SqlDbType.VarChar, 50).Value = e(2)
        cmdA.Parameters.Add("@prd_retail", SqlDbType.VarChar, 50).Value = e(4)
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertCounter(ByVal appno, ByVal brwcode, ByVal exists)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertCounter", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@is_exists", SqlDbType.Bit).Value = exists
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertAppInfo(ByVal appno, ByVal cif, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertAppInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = cif
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAppInfo(ByVal appno, ByVal brwcode, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateAppInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAppInfoByWorkflow(ByVal appno, ByVal posisi, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateAppInfoByWorkflow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@posisi", SqlDbType.VarChar, 50).Value = posisi
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertGeneralInfo(ByVal e, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertGeneralInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = e.noApp
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = e.brwcode
        cmdA.Parameters.Add("@tglApp", SqlDbType.Date).Value = e.tglApp
        cmdA.Parameters.Add("@kantorPusat", SqlDbType.VarChar, 50).Value = e.kantorPusat
        cmdA.Parameters.Add("@spoke", SqlDbType.VarChar, 50).Value = e.spoke
        cmdA.Parameters.Add("@RM", SqlDbType.VarChar, 50).Value = e.rm
        'cmdA.Parameters.Add("@omzet", SqlDbType.Decimal).Value = e.omzet
        'cmdA.Parameters.Add("@program", SqlDbType.VarChar, 50).Value = e.program
        'cmdA.Parameters.Add("@channels", SqlDbType.VarChar, 50).Value = e.channels
        cmdA.Parameters.Add("@segment", SqlDbType.VarChar, 50).Value = e.segment
        cmdA.Parameters.Add("@unitPengelola", SqlDbType.VarChar, 50).Value = e.unitPengelola
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = e.jenisPengajuan
        cmdA.Parameters.Add("@limit", SqlDbType.Decimal).Value = e.limit
        cmdA.Parameters.Add("@kantorCabang", SqlDbType.VarChar, 50).Value = e.kantorCabang
        'cmdA.Parameters.Add("@brwTyp", SqlDbType.VarChar, 50).Value = e.brwScc
        'cmdA.Parameters.Add("@prdTyp", SqlDbType.VarChar, 50).Value = e.prdScc
        'cmdA.Parameters.Add("@chkRetail", SqlDbType.Bit).Value = e.chkRetail
        'cmdA.Parameters.Add("@prdMicroTyp", SqlDbType.VarChar, 50).Value = e.prdMicroTyp
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateGeneralInfo(ByVal e, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateGeneralInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = e.noApp
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = e.brwcode
        cmdA.Parameters.Add("@kantorPusat", SqlDbType.VarChar, 50).Value = e.kantorPusat
        cmdA.Parameters.Add("@spoke", SqlDbType.VarChar, 50).Value = e.spoke
        cmdA.Parameters.Add("@RM", SqlDbType.VarChar, 50).Value = e.rm
        'cmdA.Parameters.Add("@omzet", SqlDbType.Decimal).Value = e.omzet
        'cmdA.Parameters.Add("@program", SqlDbType.VarChar, 50).Value = e.program
        'cmdA.Parameters.Add("@channels", SqlDbType.VarChar, 50).Value = e.channels
        cmdA.Parameters.Add("@segment", SqlDbType.VarChar, 50).Value = e.segment
        cmdA.Parameters.Add("@unitPengelola", SqlDbType.VarChar, 50).Value = e.unitPengelola
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = e.jenisPengajuan
        cmdA.Parameters.Add("@limit", SqlDbType.Decimal).Value = e.limit
        cmdA.Parameters.Add("@kantorCabang", SqlDbType.VarChar, 50).Value = e.kantorCabang
        'cmdA.Parameters.Add("@brwTyp", SqlDbType.VarChar, 50).Value = e.brwScc
        'cmdA.Parameters.Add("@prdTyp", SqlDbType.VarChar, 50).Value = e.prdScc
        'cmdA.Parameters.Add("@chkRetail", SqlDbType.Bit).Value = e.chkRetail
        'cmdA.Parameters.Add("@prdMicroTyp", SqlDbType.VarChar, 50).Value = e.prdMicroTyp
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertBorrowerInfo(ByVal obj, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertBorrowerInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = obj.brwcode
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = obj.noCIF
        cmdA.Parameters.Add("@acctno", SqlDbType.VarChar, 50).Value = obj.acctno
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
        cmdA.Parameters.Add("@subSektorEkonomi", SqlDbType.VarChar, 50).Value = ""
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

        cmdA.Parameters.Add("@namaUsaha", SqlDbType.VarChar, 50).Value = obj.namaUsaha
        cmdA.Parameters.Add("@alamatUsaha", SqlDbType.VarChar, 500).Value = obj.alamatUsaha
        cmdA.Parameters.Add("@kodePosUsaha", SqlDbType.VarChar, 20).Value = obj.kodePosUsaha
        cmdA.Parameters.Add("@kelUsaha", SqlDbType.VarChar, 50).Value = obj.kelurahanUsaha
        cmdA.Parameters.Add("@kecUsaha", SqlDbType.VarChar, 50).Value = obj.kecamatanUsaha
        cmdA.Parameters.Add("@kabUsaha", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenUsaha
        cmdA.Parameters.Add("@propUsaha", SqlDbType.VarChar, 50).Value = obj.propinsiUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsaha", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha
        cmdA.Parameters.Add("@tlpUsaha", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha
        cmdA.Parameters.Add("@extTlpUsaha", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsaha2", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha2
        cmdA.Parameters.Add("@tlpUsaha2", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha2
        cmdA.Parameters.Add("@extTlpUsaha2", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha2
        cmdA.Parameters.Add("@kodeAreaTlpUsaha3", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha3
        cmdA.Parameters.Add("@tlpUsaha3", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha3
        cmdA.Parameters.Add("@extTlpUsaha3", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha3
        cmdA.Parameters.Add("@kodeAreaFaxUsaha", SqlDbType.VarChar, 10).Value = obj.noKodeAreaFaxUsaha
        cmdA.Parameters.Add("@faxUsaha", SqlDbType.VarChar, 20).Value = obj.noTlpFaxUsaha
        cmdA.Parameters.Add("@extFaxUsaha", SqlDbType.VarChar, 20).Value = obj.noExtFaxUsaha
        cmdA.Parameters.Add("@lamaUsaha", SqlDbType.VarChar, 50).Value = obj.lamaUsaha
        cmdA.Parameters.Add("@bidangUsaha", SqlDbType.VarChar, 50).Value = obj.bidangUsaha
        cmdA.Parameters.Add("@grupUsaha", SqlDbType.VarChar, 50).Value = obj.grupUsaha

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
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateBorrowerInfo(ByVal obj, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateBorrowerInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = obj.brwcode
        cmdA.Parameters.Add("@cif", SqlDbType.VarChar, 50).Value = obj.noCIF
        cmdA.Parameters.Add("@acctno", SqlDbType.VarChar, 50).Value = obj.acctno
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
        cmdA.Parameters.Add("@subSektorEkonomi", SqlDbType.VarChar, 50).Value = ""
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

        cmdA.Parameters.Add("@namaUsaha", SqlDbType.VarChar, 50).Value = obj.namaUsaha
        cmdA.Parameters.Add("@alamatUsaha", SqlDbType.VarChar, 500).Value = obj.alamatUsaha
        cmdA.Parameters.Add("@kodePosUsaha", SqlDbType.VarChar, 20).Value = obj.kodePosUsaha
        cmdA.Parameters.Add("@kelUsaha", SqlDbType.VarChar, 50).Value = obj.kelurahanUsaha
        cmdA.Parameters.Add("@kecUsaha", SqlDbType.VarChar, 50).Value = obj.kecamatanUsaha
        cmdA.Parameters.Add("@kabUsaha", SqlDbType.VarChar, 50).Value = obj.kotamadyaKabupatenUsaha
        cmdA.Parameters.Add("@propUsaha", SqlDbType.VarChar, 50).Value = obj.propinsiUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsaha", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha
        cmdA.Parameters.Add("@tlpUsaha", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha
        cmdA.Parameters.Add("@extTlpUsaha", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha
        cmdA.Parameters.Add("@kodeAreaTlpUsaha2", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha2
        cmdA.Parameters.Add("@tlpUsaha2", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha2
        cmdA.Parameters.Add("@extTlpUsaha2", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha2
        cmdA.Parameters.Add("@kodeAreaTlpUsaha3", SqlDbType.VarChar, 10).Value = obj.noKodeAreaTlpUsaha3
        cmdA.Parameters.Add("@tlpUsaha3", SqlDbType.VarChar, 20).Value = obj.noTlpUsaha3
        cmdA.Parameters.Add("@extTlpUsaha3", SqlDbType.VarChar, 20).Value = obj.noExtTlpUsaha3
        cmdA.Parameters.Add("@kodeAreaFaxUsaha", SqlDbType.VarChar, 10).Value = obj.noKodeAreaFaxUsaha
        cmdA.Parameters.Add("@faxUsaha", SqlDbType.VarChar, 20).Value = obj.noTlpFaxUsaha
        cmdA.Parameters.Add("@extFaxUsaha", SqlDbType.VarChar, 20).Value = obj.noExtFaxUsaha
        cmdA.Parameters.Add("@lamaUsaha", SqlDbType.VarChar, 50).Value = obj.lamaUsaha
        cmdA.Parameters.Add("@bidangUsaha", SqlDbType.VarChar, 50).Value = obj.bidangUsaha
        cmdA.Parameters.Add("@grupUsaha", SqlDbType.VarChar, 50).Value = obj.grupUsaha

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
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertBorrowerCoupleInfo(ByVal e, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertBorrowerCoupleInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.nama
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.tempatLahir
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = e.tglLahir
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.noKTP
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = e.tglKTP
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
        Dim cmdA As New SqlCommand("app_updateBorrowerCoupleInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.nama
        cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.tempatLahir
        cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = e.tglLahir
        cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.noKTP
        cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = e.tglKTP
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.gelarDepan
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.gelarBelakang
        cmdA.Parameters.Add("@pekerjaan", SqlDbType.VarChar, 50).Value = e.pekerjaan
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertLegalitasInfo(ByVal e, ByVal brwcode, ByVal usr)
        Dim tglLegalitas As String = "1900-01-01"
        Dim tglJatuhTempo As String = "1900-01-01"
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
        Dim cmdA As New SqlCommand("app_insertLegalitasInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@jenisIjin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisIjin")
        cmdA.Parameters.Add("@noLegalitas", SqlDbType.VarChar, 50).Value = e.form("txtNoLegalitas")
        cmdA.Parameters.Add("@tglLegalitas", SqlDbType.VarChar, 50).Value = tglLegalitas
        cmdA.Parameters.Add("@tglJatuhTempo", SqlDbType.VarChar, 50).Value = tglJatuhTempo
        'cmdA.Parameters.Add("@perihal", SqlDbType.VarChar, 50).Value = e.form("txtPerihal")
        cmdA.Parameters.AddWithValue("@perihal", e.form("txtPerihal"))
        cmdA.Parameters.Add("@noPengesahan", SqlDbType.VarChar, 50).Value = e.form("txtNoPengesahan")
        cmdA.Parameters.Add("@namaNotaris", SqlDbType.VarChar, 50).Value = e.form("txtNamaNotaris")
        cmdA.Parameters.Add("@keterangan", SqlDbType.Text).Value = e.form("txtKeterangan")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateLegalitasInfo(ByVal e, ByVal brwcode, ByVal id, ByVal usr)
        Dim tglLegalitas As String = "1900-01-01"
        Dim tglJatuhTempo As String = "1900-01-01"
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
        Dim cmdA As New SqlCommand("app_updateLegalitasInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@jenisIjin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisIjin")
        cmdA.Parameters.Add("@noLegalitas", SqlDbType.VarChar, 50).Value = e.form("txtNoLegalitas")
        cmdA.Parameters.Add("@tglLegalitas", SqlDbType.Date).Value = tglLegalitas
        cmdA.Parameters.Add("@tglJatuhTempo", SqlDbType.Date).Value = tglJatuhTempo
        'cmdA.Parameters.Add("@perihal", SqlDbType.VarChar, 50).Value = e.form("txtPerihal")
        cmdA.Parameters.AddWithValue("@perihal", e.form("txtPerihal"))
        cmdA.Parameters.Add("@noPengesahan", SqlDbType.VarChar, 50).Value = e.form("txtNoPengesahan")
        cmdA.Parameters.Add("@namaNotaris", SqlDbType.VarChar, 50).Value = e.form("txtNamaNotaris")
        cmdA.Parameters.Add("@keterangan", SqlDbType.Text).Value = e.form("txtKeterangan")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertKeyPersonInfo(ByVal e, ByVal brwcode, ByVal usr)
        'Dim tglLahir As String = ""
        'Dim tglKTP As String = ""
        'Dim tglExpKTP As String = ""
        'If Not e.form("txtTglLahir") = "" Then
        '    tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        'Else
        '    tglLahir = "1900-01-01"
        'End If
        'If Not e.form("txtTglKTP") = "" Then
        '    tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        'Else
        '    tglKTP = "1900-01-01"
        'End If
        'If Not e.form("txtExpKTP") = "" Then
        '    tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        'Else
        '    tglExpKTP = "1900-01-01"
        'End If

        'Dim myDb As New dbConn
        'Dim myConn As SqlConnection
        'myConn = myDb.getConStr_Analysis
        'myConn.Open()
        'Dim cmdA As New SqlCommand("app_insertKeyPersonInfo", myConn)
        'cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        'cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        ''cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        ''cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        'cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        'cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.form("txtTempatLahir")
        'cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = tglLahir
        'cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoKTP")
        'cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = tglKTP
        'cmdA.Parameters.Add("@tglExpKTP", SqlDbType.Date).Value = tglExpKTP
        'cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        'cmdA.Parameters.Add("@statusKawin", SqlDbType.VarChar, 50).Value = e.form("ddlStatusKawin")
        'cmdA.Parameters.Add("@kebangsaan", SqlDbType.VarChar, 50).Value = e.form("ddlKebangsaan")
        'cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisKelamin")
        'cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamatRmh")
        'cmdA.Parameters.Add("@alamatUsaha", SqlDbType.VarChar, 500).Value = e.form("txtAlamatUsaha")
        ''cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        ''cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        ''cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        ''cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        ''cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        'cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        'cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        'cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        'cmdA.Parameters.Add("@noHP", SqlDbType.VarChar, 50).Value = e.form("txtNoHP")
        ''cmdA.Parameters.Add("@namaIbu", SqlDbType.VarChar, 50).Value = e.form("txtNamaIbu")
        'cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        'cmdA.ExecuteNonQuery()
        'myConn.Close()
        Dim tglLahir As String = "1900-01-01"
        Dim tglStart As String = "1900-01-01"
        Dim tglAkhir As String = "1900-01-01"
        Dim key As Boolean = False
        If Not e.form("txtTgllahir") = "" Then
            tglLahir = Mid(e.form("txtTgllahir"), 7, 4) + "-" & Mid(e.form("txtTgllahir"), 4, 2) + "-" & Mid(e.form("txtTgllahir"), 1, 2)
        End If
        If Not e.form("txtMasaKerjaDari") = "" Then
            tglStart = Mid(e.form("txtMasaKerjaDari"), 7, 4) + "-" & Mid(e.form("txtMasaKerjaDari"), 4, 2) + "-" & Mid(e.form("txtMasaKerjaDari"), 1, 2)
        End If
        If Not e.form("txtSampaiTgl") = "" Then
            tglAkhir = Mid(e.form("txtSampaiTgl"), 7, 4) + "-" & Mid(e.form("txtSampaiTgl"), 4, 2) + "-" & Mid(e.form("txtSampaiTgl"), 1, 2)
        End If
        If e.Form("chkKeyPerson") = "on" Then
            key = True
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertKeyPersonInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@nama", e.Form("txtNama"))
        cmdA.Parameters.AddWithValue("@tanggal", tglLahir)
        cmdA.Parameters.AddWithValue("@gender", e.Form("ddlGender"))
        cmdA.Parameters.AddWithValue("@alamat", e.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@namaPerusahaan", e.Form("txtNamaPerusahaan"))
        cmdA.Parameters.AddWithValue("@namajabatan", e.Form("txtNamaJabatan"))
        cmdA.Parameters.AddWithValue("@hubAntarPengurus", e.Form("txtHUbunganAntarPengurus"))
        cmdA.Parameters.AddWithValue("@masaKerja1", tglStart)
        cmdA.Parameters.AddWithValue("@masaKerja2", tglAkhir)
        cmdA.Parameters.AddWithValue("@riwayat", e.Form("txtRiwayat"))
        cmdA.Parameters.AddWithValue("@userid", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateKeyPersonInfo(ByVal e, ByVal brwcode, ByVal id, ByVal usr)
        'Dim tglLahir As String = ""
        'Dim tglKTP As String = ""
        'Dim tglExpKTP As String = ""
        'If Not e.form("txtTglLahir") = "" Then
        '    tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        'Else
        '    tglLahir = "1900-01-01"
        'End If
        'If Not e.form("txtTglKTP") = "" Then
        '    tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        'Else
        '    tglKTP = "1900-01-01"
        'End If
        'If Not e.form("txtExpKTP") = "" Then
        '    tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        'Else
        '    tglExpKTP = "1900-01-01"
        'End If

        'Dim myDb As New dbConn
        'Dim myConn As SqlConnection
        'myConn = myDb.getConStr_Analysis
        'myConn.Open()
        'Dim cmdA As New SqlCommand("app_updateKeyPersonInfo", myConn)
        'cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        'cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        'cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        ''cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        ''cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        'cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        'cmdA.Parameters.Add("@tempatLahir", SqlDbType.VarChar, 50).Value = e.form("txtTempatLahir")
        'cmdA.Parameters.Add("@tglLahir", SqlDbType.Date).Value = tglLahir
        'cmdA.Parameters.Add("@noKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoKTP")
        'cmdA.Parameters.Add("@tglKTP", SqlDbType.Date).Value = tglKTP
        'cmdA.Parameters.Add("@tglExpKTP", SqlDbType.Date).Value = tglExpKTP
        'cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        'cmdA.Parameters.Add("@statusKawin", SqlDbType.VarChar, 50).Value = e.form("ddlStatusKawin")
        'cmdA.Parameters.Add("@kebangsaan", SqlDbType.VarChar, 50).Value = e.form("ddlKebangsaan")
        'cmdA.Parameters.Add("@jenisKelamin", SqlDbType.VarChar, 50).Value = e.form("ddlJenisKelamin")
        'cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamatRmh")
        'cmdA.Parameters.Add("@alamatUsaha", SqlDbType.VarChar, 500).Value = e.form("txtAlamatUsaha")
        ''cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        ''cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        ''cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        ''cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        ''cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        'cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        'cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        'cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        'cmdA.Parameters.Add("@noHP", SqlDbType.VarChar, 50).Value = e.form("txtNoHP")
        ''cmdA.Parameters.Add("@namaIbu", SqlDbType.VarChar, 50).Value = e.form("txtNamaIbu")
        'cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        'cmdA.ExecuteNonQuery()
        'myConn.Close()

        Dim tglLahir As String = "1900-01-01"
        Dim tglStart As String = "1900-01-01"
        Dim tglAkhir As String = "1900-01-01"
        Dim key As Boolean = False
        If Not e.form("txtTgllahir") = "" Then
            tglLahir = Mid(e.form("txtTgllahir"), 7, 4) + "-" & Mid(e.form("txtTgllahir"), 4, 2) + "-" & Mid(e.form("txtTgllahir"), 1, 2)
        End If
        If Not e.form("txtMasaKerjaDari") = "" Then
            tglStart = Mid(e.form("txtMasaKerjaDari"), 7, 4) + "-" & Mid(e.form("txtMasaKerjaDari"), 4, 2) + "-" & Mid(e.form("txtMasaKerjaDari"), 1, 2)
        End If
        If Not e.form("txtSampaiTgl") = "" Then
            tglAkhir = Mid(e.form("txtSampaiTgl"), 7, 4) + "-" & Mid(e.form("txtSampaiTgl"), 4, 2) + "-" & Mid(e.form("txtSampaiTgl"), 1, 2)
        End If
        If e.Form("chkKeyPerson") = "on" Then
            key = True
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateKeyPersonInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@nama", e.Form("txtNama"))
        cmdA.Parameters.AddWithValue("@tanggal", tglLahir)
        cmdA.Parameters.AddWithValue("@gender", e.Form("ddlGender"))
        cmdA.Parameters.AddWithValue("@alamat", e.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@namaPerusahaan", e.Form("txtNamaPerusahaan"))
        cmdA.Parameters.AddWithValue("@namajabatan", e.Form("txtNamaJabatan"))
        cmdA.Parameters.AddWithValue("@hubAntarPengurus", e.Form("txtHUbunganAntarPengurus"))
        cmdA.Parameters.AddWithValue("@masaKerja1", tglStart)
        cmdA.Parameters.AddWithValue("@masaKerja2", tglAkhir)
        cmdA.Parameters.AddWithValue("@riwayat", e.Form("txtRiwayat"))
        cmdA.Parameters.AddWithValue("@userid", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertIndividualShareholders(ByVal e, ByVal brwcode, ByVal usr)
        Dim tglLahir As String = ""
        Dim tglKTP As String = ""
        Dim tglExpKTP As String = ""
        If Not e.form("txtTglLahir") = "" Then
            tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        Else
            tglLahir = "1900-01-01"
        End If
        If Not e.form("txtTglKTP") = "" Then
            tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        Else
            tglKTP = "1900-01-01"
        End If
        If Not e.form("txtExpKTP") = "" Then
            tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        Else
            tglExpKTP = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Float).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Decimal).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
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
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        'cmdA.Parameters.Add("@alamatUsaha", SqlDbType.VarChar, 500).Value = e.form("txtAlamatUsaha")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateIndividualShareholders(ByVal e, ByVal brwcode, ByVal id, ByVal usr)
        Dim tglLahir As String = ""
        Dim tglKTP As String = ""
        Dim tglExpKTP As String = ""
        If Not e.form("txtTglLahir") = "" Then
            tglLahir = Mid(e.form("txtTglLahir"), 7, 4) + "-" & Mid(e.form("txtTglLahir"), 4, 2) + "-" & Mid(e.form("txtTglLahir"), 1, 2)
        Else
            tglLahir = "1900-01-01"
        End If
        If Not e.form("txtTglKTP") = "" Then
            tglKTP = Mid(e.form("txtTglKTP"), 7, 4) + "-" & Mid(e.form("txtTglKTP"), 4, 2) + "-" & Mid(e.form("txtTglKTP"), 1, 2)
        Else
            tglKTP = "1900-01-01"
        End If
        If Not e.form("txtExpKTP") = "" Then
            tglExpKTP = Mid(e.form("txtExpKTP"), 7, 4) + "-" & Mid(e.form("txtExpKTP"), 4, 2) + "-" & Mid(e.form("txtExpKTP"), 1, 2)
        Else
            tglExpKTP = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateIndividualShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@gelarD", SqlDbType.VarChar, 50).Value = e.form("txtGelarD")
        cmdA.Parameters.Add("@gelarB", SqlDbType.VarChar, 50).Value = e.form("txtGelarB")
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.form("txtJabatan")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Float).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Decimal).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
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
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertCorporateShareholders(ByVal e, ByVal brwcode, ByVal usr)
        Dim tglAkteKTP As String = ""

        If Not e.form("txtTglAkteKTP") = "" Then
            tglAkteKTP = Mid(e.form("txtTglAkteKTP"), 7, 4) + "-" & Mid(e.form("txtTglAkteKTP"), 4, 2) + "-" & Mid(e.form("txtTglAkteKTP"), 1, 2)
        Else
            tglAkteKTP = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 50).Value = e.form("ddlTipeBadan")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@noAkteKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoAkteKTP")
        cmdA.Parameters.Add("@tglAkteKTP", SqlDbType.Date).Value = tglAkteKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Float).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Decimal).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamat")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateCorporateShareholders(ByVal e, ByVal brwcode, ByVal id, ByVal usr)
        Dim tglAkteKTP As String = ""

        If Not e.form("txtTglAkteKTP") = "" Then
            tglAkteKTP = Mid(e.form("txtTglAkteKTP"), 7, 4) + "-" & Mid(e.form("txtTglAkteKTP"), 4, 2) + "-" & Mid(e.form("txtTglAkteKTP"), 1, 2)
        Else
            tglAkteKTP = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateCorporateShareholders", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 50).Value = e.form("ddlTipeBadan")
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.form("txtNama")
        cmdA.Parameters.Add("@noAkteKTP", SqlDbType.VarChar, 50).Value = e.form("txtNoAkteKTP")
        cmdA.Parameters.Add("@tglAkteKTP", SqlDbType.Date).Value = tglAkteKTP
        cmdA.Parameters.Add("@npwp", SqlDbType.VarChar, 50).Value = e.form("txtNPWP")
        cmdA.Parameters.Add("@kepemilikanSaham", SqlDbType.Float).Value = e.form("txtKepemilikanSaham")
        cmdA.Parameters.Add("@jumlahLembarSaham", SqlDbType.Decimal).Value = e.form("txtJumlahLembarSaham").ToString.Replace(",", "")
        cmdA.Parameters.Add("@alamat", SqlDbType.VarChar, 500).Value = e.form("txtAlamat")
        cmdA.Parameters.Add("@kodePos", SqlDbType.VarChar, 20).Value = e.form("txtKodePos")
        cmdA.Parameters.Add("@kelurahan", SqlDbType.VarChar, 50).Value = e.form("txtKelurahan")
        cmdA.Parameters.Add("@kecamatan", SqlDbType.VarChar, 50).Value = e.form("txtKecamatan")
        cmdA.Parameters.Add("@kabupaten", SqlDbType.VarChar, 50).Value = e.form("txtKabupaten")
        cmdA.Parameters.Add("@propinsi", SqlDbType.VarChar, 50).Value = e.form("txtPropinsi")
        cmdA.Parameters.Add("@kodeAreaTlp", SqlDbType.VarChar, 10).Value = e.form("txtKodeAreaTlp")
        cmdA.Parameters.Add("@tlp", SqlDbType.VarChar, 20).Value = e.form("txtTlp")
        cmdA.Parameters.Add("@extTlp", SqlDbType.VarChar, 20).Value = e.form("txtExtTlp")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertProfitLossInfo(ByVal e, ByVal brwcode)
        Dim month As Integer = 0
        If e.form("ddlBulan") Is Nothing Then
            month = 12
        Else
            month = e.form("ddlBulan")
        End If
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
        Dim cmdA As New SqlCommand("app_insertProfitLossInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@bulan", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@tahun", SqlDbType.Int).Value = e.form("ddlTahun")
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
        Dim month As Integer = 0
        If e.form("ddlBulan") Is Nothing Then
            month = 12
        Else
            month = e.form("ddlBulan")
        End If
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
        Dim cmdA As New SqlCommand("app_updateProfitLossInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@bulan", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@tahun", SqlDbType.Int).Value = e.form("ddlTahun")
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
        Dim cmdA As New SqlCommand("app_insertBalanceSheetInfo", myConn)
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
        Dim cmdA As New SqlCommand("app_updateBalanceSheetInfo", myConn)
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
        Dim cmdA As New SqlCommand("app_insertReconciliationInfo", myConn)
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
        Dim cmdA As New SqlCommand("app_updateReconciliationInfo", myConn)
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

    Sub insertOtherFinRatio(ByVal e As HttpRequest, ByVal brwcode As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertOtherFinRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = e.Form("ddlBulan")
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = e.Form("ddlTahun")
        cmdA.Parameters.Add("@aum", SqlDbType.Decimal).Value = e.Form("txtAUM").ToString.Replace(",", "")
        cmdA.Parameters.Add("@bopo", SqlDbType.Float).Value = e.Form("txtBOPO")
        cmdA.Parameters.Add("@car", SqlDbType.Float).Value = e.Form("txtCAR")
        cmdA.Parameters.Add("@apyd_to_ap", SqlDbType.Float).Value = e.Form("txtAPYDtoAP")
        cmdA.Parameters.Add("@npl_gross", SqlDbType.Float).Value = e.Form("txtNPLGross")
        cmdA.Parameters.Add("@net_interest_margin", SqlDbType.Float).Value = e.Form("txtNetInterestMargin")
        cmdA.Parameters.Add("@cash_ratio", SqlDbType.Float).Value = e.Form("txtCashRatio")
        cmdA.Parameters.Add("@loan_to_deposit", SqlDbType.Float).Value = e.Form("txtLoanToDepositRatio")
        cmdA.Parameters.Add("@asset_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanAsset")
        cmdA.Parameters.Add("@aum_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanAUM")
        cmdA.Parameters.Add("@expense_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanBiaya")
        cmdA.Parameters.Add("@own_capital_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanModalSendiri")
        cmdA.Parameters.Add("@npl", SqlDbType.Float).Value = e.Form("txtNPL")
        cmdA.Parameters.Add("@gwm", SqlDbType.Float).Value = e.Form("txtGWM")
        cmdA.Parameters.Add("@net_devisa", SqlDbType.Float).Value = e.Form("txtPosisiDevisaNet")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateOtherFinRatio(ByVal e As HttpRequest, ByVal brwcode As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateOtherFinRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = e.Form("ddlBulan")
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = e.Form("ddlTahun")
        cmdA.Parameters.Add("@aum", SqlDbType.Decimal).Value = e.Form("txtAUM").ToString.Replace(",", "")
        cmdA.Parameters.Add("@bopo", SqlDbType.Float).Value = e.Form("txtBOPO")
        cmdA.Parameters.Add("@car", SqlDbType.Float).Value = e.Form("txtCAR")
        cmdA.Parameters.Add("@apyd_to_ap", SqlDbType.Float).Value = e.Form("txtAPYDtoAP")
        cmdA.Parameters.Add("@npl_gross", SqlDbType.Float).Value = e.Form("txtNPLGross")
        cmdA.Parameters.Add("@net_interest_margin", SqlDbType.Float).Value = e.Form("txtNetInterestMargin")
        cmdA.Parameters.Add("@cash_ratio", SqlDbType.Float).Value = e.Form("txtCashRatio")
        cmdA.Parameters.Add("@loan_to_deposit", SqlDbType.Float).Value = e.Form("txtLoanToDepositRatio")
        cmdA.Parameters.Add("@asset_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanAsset")
        cmdA.Parameters.Add("@aum_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanAUM")
        cmdA.Parameters.Add("@expense_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanBiaya")
        cmdA.Parameters.Add("@own_capital_growth", SqlDbType.Float).Value = e.Form("txtPertumbuhanModalSendiri")
        cmdA.Parameters.Add("@npl", SqlDbType.Float).Value = e.Form("txtNPL")
        cmdA.Parameters.Add("@gwm", SqlDbType.Float).Value = e.Form("txtGWM")
        cmdA.Parameters.Add("@net_devisa", SqlDbType.Float).Value = e.Form("txtPosisiDevisaNet")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertLotsUsaha(ByVal obj, ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertLotsInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
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
        Dim cmdA As New SqlCommand("app_updateLotsInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = brwcode
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

    Sub insertUpdateAspek(ByVal brwcode, ByVal id, ByVal value, ByVal cat, ByVal desc, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUpdateAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 1).Value = value
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.Parameters.Add("@desc", SqlDbType.Text).Value = desc
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertUpdateAppAspek(ByVal appno, ByVal id, ByVal value, ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUpdateAppAspek", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 1).Value = value
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 50).Value = cat
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertBorrowerRating(ByVal brwcode, ByVal score, ByVal rating, ByVal ratingDate, ByVal nextRatingDate, ByVal usr, ByVal scc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertBorrowerRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@score", SqlDbType.Float).Value = score
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = rating
        cmdA.Parameters.AddWithValue("@rating", rating)
        cmdA.Parameters.Add("@ratingDate", SqlDbType.Date).Value = ratingDate
        cmdA.Parameters.Add("@nextRatingDate", SqlDbType.Date).Value = nextRatingDate
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@scc", SqlDbType.VarChar, 50).Value = scc
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertRetailRating(ByVal appno, ByVal score, ByVal rating, ByVal ratingDate, ByVal nextRatingDate, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertRetailRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@score", SqlDbType.Float).Value = score
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = rating
        cmdA.Parameters.AddWithValue("@rating", rating)
        cmdA.Parameters.Add("@ratingDate", SqlDbType.Date).Value = ratingDate
        cmdA.Parameters.Add("@nextRatingDate", SqlDbType.Date).Value = nextRatingDate
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub calculateRatio(ByVal brwcode, ByVal year, ByVal month, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_calculateRatio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub calculateRatioByMonth(ByVal brwcode, ByVal year, ByVal month, ByVal type, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_calculateRatioByMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        cmdA.Parameters.Add("@month", SqlDbType.Int).Value = month
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = type
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getAspekAgunan(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfAspekAgunan", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
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
        Dim cmdA As New SqlCommand("app_getListOfAspekFinancial", myConn)
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
        Dim cmdA As New SqlCommand("app_getListOfAspekManagement", myConn)
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
        Dim cmdA As New SqlCommand("app_getListOfAspekPasar", myConn)
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
        Dim cmdA As New SqlCommand("app_getListOfAspekRPC", myConn)
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
        Dim cmdA As New SqlCommand("app_getListOfAspekTeknis", myConn)
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
        Dim cmdA As New SqlCommand("app_getBankRatingDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@lbr_id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getExternalRatingAgency()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDataExternalRatingAgency", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getExternalRatingbyAgency(ByVal agency)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDataRatingByAgency", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 50).Value = agency
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    '============facility================
    Sub insertFacilityCreditRecommendation(ByVal obj, ByVal proses, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertFacilityCreditRecommendation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = obj.noApp
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = obj.jenisPengajuan
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = obj.jenisKredit
        cmdA.Parameters.Add("@jenisValas", SqlDbType.VarChar, 50).Value = obj.jenisValas
        cmdA.Parameters.Add("@nilaiValas", SqlDbType.VarChar, 50).Value = obj.nilaiValas
        'cmdA.Parameters.Add("@jangkaWaktuSP2K", SqlDbType.VarChar, 50).Value = obj.jangkaWaktuSP2K
        cmdA.Parameters.Add("@sistemPembayaran", SqlDbType.VarChar, 50).Value = obj.sistemPembayaran
        cmdA.Parameters.Add("@maxKreditValas", SqlDbType.Decimal).Value = obj.maxKreditValas
        cmdA.Parameters.Add("@maxKreditRec", SqlDbType.Decimal).Value = obj.maxKreditRec
        cmdA.Parameters.Add("@jangkaWaktuRec", SqlDbType.Int).Value = obj.jangkaWaktuRec
        cmdA.Parameters.Add("@gracePeriod", SqlDbType.Int).Value = obj.gracePeriod
        'cmdA.Parameters.Add("@jadwalPembayaranPokokKredit", SqlDbType.Date).Value = obj.jadwalPembayaranPokokKredit
        'cmdA.Parameters.Add("@proyeksiBunga", SqlDbType.Float).Value = obj.proyeksiBunga
        cmdA.Parameters.Add("@sukuBunga", SqlDbType.Float).Value = obj.sukuBunga
        cmdA.Parameters.Add("@jenisSukuBunga", SqlDbType.VarChar, 50).Value = obj.jenisSukuBunga
        cmdA.Parameters.Add("@provisi", SqlDbType.Float).Value = obj.provisi
        'cmdA.Parameters.Add("@commitmentFee", SqlDbType.Float).Value = obj.commitmentFee
        cmdA.Parameters.Add("@biayaAdmin", SqlDbType.Decimal).Value = obj.biayaAdmin
        cmdA.Parameters.Add("@denda", SqlDbType.Float).Value = obj.denda
        'cmdA.Parameters.Add("@bebanBungaPerBulan", SqlDbType.Decimal).Value = obj.bebanBungaPerBulan
        'cmdA.Parameters.Add("@angsuranPokokPerBulan", SqlDbType.Decimal).Value = obj.angsuranPokokPerBulan
        'cmdA.Parameters.Add("@totalAngsuranPokokBunga", SqlDbType.Decimal).Value = obj.totalAngsuranPokokBunga
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = obj.sifatKredit
        cmdA.Parameters.Add("@maxTarikanKredit", SqlDbType.Decimal).Value = obj.maxTarikanKredit
        'cmdA.Parameters.Add("@skimKMK", SqlDbType.VarChar, 50).Value = obj.skimKMK
        cmdA.Parameters.Add("@rec", SqlDbType.Bit).Value = obj.recommendation
        cmdA.Parameters.Add("@pendapat", SqlDbType.Text).Value = obj.pendapat
        cmdA.Parameters.Add("@pertimbangan", SqlDbType.Text).Value = obj.pertimbangan
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditRecommendation(ByVal obj, ByVal proses, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateFacilityCreditRecommendation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = obj.noApp
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = obj.jenisPengajuan
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = obj.jenisKredit
        cmdA.Parameters.Add("@jenisValas", SqlDbType.VarChar, 50).Value = obj.jenisValas
        cmdA.Parameters.Add("@nilaiValas", SqlDbType.VarChar, 50).Value = obj.nilaiValas
        'cmdA.Parameters.Add("@jangkaWaktuSP2K", SqlDbType.VarChar, 50).Value = obj.jangkaWaktuSP2K
        cmdA.Parameters.Add("@sistemPembayaran", SqlDbType.VarChar, 50).Value = obj.sistemPembayaran
        cmdA.Parameters.Add("@maxKreditValas", SqlDbType.Decimal).Value = obj.maxKreditValas
        cmdA.Parameters.Add("@maxKreditRec", SqlDbType.Decimal).Value = obj.maxKreditRec
        cmdA.Parameters.Add("@jangkaWaktuRec", SqlDbType.Int).Value = obj.jangkaWaktuRec
        cmdA.Parameters.Add("@gracePeriod", SqlDbType.Int).Value = obj.gracePeriod
        'cmdA.Parameters.Add("@jadwalPembayaranPokokKredit", SqlDbType.Date).Value = obj.jadwalPembayaranPokokKredit
        'cmdA.Parameters.Add("@proyeksiBunga", SqlDbType.Float).Value = obj.proyeksiBunga
        cmdA.Parameters.Add("@sukuBunga", SqlDbType.Float).Value = obj.sukuBunga
        cmdA.Parameters.Add("@jenisSukuBunga", SqlDbType.VarChar, 50).Value = obj.jenisSukuBunga
        cmdA.Parameters.Add("@provisi", SqlDbType.Float).Value = obj.provisi
        'cmdA.Parameters.Add("@commitmentFee", SqlDbType.Float).Value = obj.commitmentFee
        cmdA.Parameters.Add("@biayaAdmin", SqlDbType.Decimal).Value = obj.biayaAdmin
        cmdA.Parameters.Add("@denda", SqlDbType.Float).Value = obj.denda
        'cmdA.Parameters.Add("@bebanBungaPerBulan", SqlDbType.Decimal).Value = obj.bebanBungaPerBulan
        'cmdA.Parameters.Add("@angsuranPokokPerBulan", SqlDbType.Decimal).Value = obj.angsuranPokokPerBulan
        'cmdA.Parameters.Add("@totalAngsuranPokokBunga", SqlDbType.Decimal).Value = obj.totalAngsuranPokokBunga
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = obj.sifatKredit
        cmdA.Parameters.Add("@maxTarikanKredit", SqlDbType.Decimal).Value = obj.maxTarikanKredit
        'cmdA.Parameters.Add("@skimKMK", SqlDbType.VarChar, 50).Value = obj.skimKMK
        cmdA.Parameters.Add("@rec", SqlDbType.Bit).Value = obj.recommendation
        cmdA.Parameters.Add("@pendapat", SqlDbType.Text).Value = obj.pendapat
        cmdA.Parameters.Add("@pertimbangan", SqlDbType.Text).Value = obj.pertimbangan
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertFacilityCreditFinal(ByVal obj, ByVal proses, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertFacilityCreditFinal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = obj.noApp
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = obj.jenisPengajuan
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = obj.jenisKredit
        cmdA.Parameters.Add("@jenisValas", SqlDbType.VarChar, 50).Value = obj.jenisValas
        cmdA.Parameters.Add("@nilaiValas", SqlDbType.VarChar, 50).Value = obj.nilaiValas
        cmdA.Parameters.Add("@sistemPembayaran", SqlDbType.VarChar, 50).Value = obj.sistemPembayaran
        cmdA.Parameters.Add("@maxKreditValas", SqlDbType.Decimal).Value = obj.maxKreditValas
        cmdA.Parameters.Add("@maxKredit", SqlDbType.Decimal).Value = obj.maxKreditFin
        cmdA.Parameters.Add("@jangkaWaktu", SqlDbType.Int).Value = obj.jangkaWaktuFin
        cmdA.Parameters.Add("@gracePeriod", SqlDbType.Int).Value = obj.gracePeriod
        cmdA.Parameters.Add("@sukuBunga", SqlDbType.Float).Value = obj.sukuBunga
        cmdA.Parameters.Add("@jenisSukuBunga", SqlDbType.VarChar, 50).Value = obj.jenisSukuBunga
        cmdA.Parameters.Add("@provisi", SqlDbType.Float).Value = obj.provisi
        'cmdA.Parameters.Add("@bebanBungaPerBulan", SqlDbType.Decimal).Value = obj.bebanBungaPerBulan
        'cmdA.Parameters.Add("@angsuranPokokPerBulan", SqlDbType.Decimal).Value = obj.angsuranPokokPerBulan
        'cmdA.Parameters.Add("@totalAngsuran", SqlDbType.Decimal).Value = obj.totalAngsuranPokokBunga
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = obj.sifatKredit
        cmdA.Parameters.Add("@maxTarikanKredit", SqlDbType.Decimal).Value = obj.maxTarikanKredit
        cmdA.Parameters.Add("@biayaAdmin", SqlDbType.Decimal).Value = obj.biayaAdmin
        cmdA.Parameters.Add("@denda", SqlDbType.Float).Value = obj.denda
        cmdA.Parameters.Add("@alasan", SqlDbType.Text).Value = obj.alasan
        cmdA.Parameters.Add("@keputusan", SqlDbType.Bit).Value = obj.keputusan
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditFinal(ByVal obj, ByVal proses, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateFacilityCreditFinal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@app_no", SqlDbType.VarChar, 50).Value = obj.noApp
        cmdA.Parameters.Add("@jenisPengajuan", SqlDbType.VarChar, 50).Value = obj.jenisPengajuan
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = obj.jenisKredit
        cmdA.Parameters.Add("@jenisValas", SqlDbType.VarChar, 50).Value = obj.jenisValas
        cmdA.Parameters.Add("@nilaiValas", SqlDbType.VarChar, 50).Value = obj.nilaiValas
        cmdA.Parameters.Add("@sistemPembayaran", SqlDbType.VarChar, 50).Value = obj.sistemPembayaran
        cmdA.Parameters.Add("@maxKreditValas", SqlDbType.Decimal).Value = obj.maxKreditValas
        cmdA.Parameters.Add("@maxKredit", SqlDbType.Decimal).Value = obj.maxKreditFin
        cmdA.Parameters.Add("@jangkaWaktu", SqlDbType.Int).Value = obj.jangkaWaktuFin
        cmdA.Parameters.Add("@gracePeriod", SqlDbType.Int).Value = obj.gracePeriod
        cmdA.Parameters.Add("@sukuBunga", SqlDbType.Float).Value = obj.sukuBunga
        cmdA.Parameters.Add("@jenisSukuBunga", SqlDbType.VarChar, 50).Value = obj.jenisSukuBunga
        cmdA.Parameters.Add("@provisi", SqlDbType.Float).Value = obj.provisi
        'cmdA.Parameters.Add("@bebanBungaPerBulan", SqlDbType.Decimal).Value = obj.bebanBungaPerBulan
        'cmdA.Parameters.Add("@angsuranPokokPerBulan", SqlDbType.Decimal).Value = obj.angsuranPokokPerBulan
        'cmdA.Parameters.Add("@totalAngsuran", SqlDbType.Decimal).Value = obj.totalAngsuranPokokBunga
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = obj.sifatKredit
        cmdA.Parameters.Add("@maxTarikanKredit", SqlDbType.Decimal).Value = obj.maxTarikanKredit
        cmdA.Parameters.Add("@biayaAdmin", SqlDbType.Decimal).Value = obj.biayaAdmin
        cmdA.Parameters.Add("@denda", SqlDbType.Float).Value = obj.denda
        cmdA.Parameters.Add("@alasan", SqlDbType.Text).Value = obj.alasan
        cmdA.Parameters.Add("@keputusan", SqlDbType.Bit).Value = obj.keputusan
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    '==========tambahan wisnu=============
    Function getDetailOfLKN(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertLKNInfo(ByVal e, ByVal appno, ByVal UserId)
        Dim Tanggal As String = "1900-01-01"
        If Not e.form("InpTanggal") = "" Then
            Tanggal = Mid(e.Form("InpTanggal"), 7, 4) + "-" & Mid(e.Form("InpTanggal"), 4, 2) + "-" & Mid(e.Form("InpTanggal"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertLKNInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.Form("Inpnama")
        cmdA.Parameters.Add("@bentuk", SqlDbType.VarChar, 50).Value = e.Form("Inpbentuk")
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 50).Value = e.Form("Inptujuan")
        cmdA.Parameters.Add("@tanggal", SqlDbType.Date).Value = Tanggal
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.Form("Inpjabatan")
        cmdA.Parameters.Add("@hasil", SqlDbType.VarChar, 200).Value = e.Form("Inphasil")
        cmdA.Parameters.Add("@tindak", SqlDbType.VarChar, 200).Value = e.Form("Inptindak")
        cmdA.Parameters.AddWithValue("@alamat", e.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@hasilKunjungan", e.Form("txtHasilKunjungan"))
        cmdA.Parameters.AddWithValue("@userid", UserId)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateLKNInfo(ByVal e, ByVal ID, ByVal UserId)
        Dim Tanggal As String = ""
        If Not e.form("InpTanggal") = "" Then
            Tanggal = Mid(e.Form("InpTanggal"), 7, 4) + "-" & Mid(e.Form("InpTanggal"), 4, 2) + "-" & Mid(e.Form("InpTanggal"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateLKNInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@nama", SqlDbType.VarChar, 50).Value = e.Form("InpNama")
        cmdA.Parameters.Add("@bentuk", SqlDbType.VarChar, 50).Value = e.Form("InpBentuk")
        cmdA.Parameters.Add("@tujuan", SqlDbType.VarChar, 50).Value = e.Form("InpTujuan")
        cmdA.Parameters.Add("@tanggal", SqlDbType.Date).Value = Tanggal
        cmdA.Parameters.Add("@jabatan", SqlDbType.VarChar, 50).Value = e.Form("InpJabatan")
        cmdA.Parameters.Add("@hasil", SqlDbType.VarChar, 200).Value = e.Form("InpHasil")
        cmdA.Parameters.Add("@tindak", SqlDbType.VarChar, 200).Value = e.Form("InpTindak")
        cmdA.Parameters.AddWithValue("@alamat", e.Form("txtAlamat"))
        cmdA.Parameters.AddWithValue("@hasilKunjungan", e.Form("txtHasilKunjungan"))
        cmdA.Parameters.AddWithValue("@userid", UserId)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkLKN(ByVal BrwCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = BrwCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteLKN(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteLKN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertDocumentInfo(ByVal e, ByVal Data(), ByVal BrwCode)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglSurat As String = ""
        Dim tglUndangan As String = ""
        If Not e.Form(master + "InpTglSurat") = "" Then
            tglSurat = Mid(e.Form(master + "InptglSurat"), 7, 4) + "-" & Mid(e.Form(master + "InptglSurat"), 4, 2) + "-" & Mid(e.Form(master + "InptglSurat"), 1, 2)
        Else
            tglSurat = "1900-01-01"
        End If
        If Not e.Form(master + "InpTglUndangan") = "" Then
            tglUndangan = Mid(e.Form(master + "InptglUndangan"), 7, 4) + "-" & Mid(e.Form(master + "InptglUndangan"), 4, 2) + "-" & Mid(e.Form(master + "InptglUndangan"), 1, 2)
        Else
            tglUndangan = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertDocumentInfo", myConn)
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
        Else
            tglSurat = "1900-01-01"
        End If
        If Not e.Form(master + "InptglUndangan") = "" Then
            tglUndangan = Mid(e.Form(master + "InptglUndangan"), 7, 4) + "-" & Mid(e.Form(master + "InptglUndangan"), 4, 2) + "-" & Mid(e.Form(master + "InptglUndangan"), 1, 2)
        Else
            tglUndangan = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateDocumentInfo", myConn)
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

    Function checkDocument(ByVal BrwCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkDocument", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = BrwCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertPreScreenInfo(ByVal e, ByVal BrwCode)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertPreScreenInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@pemilikHitam", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPemilikHitam").ToString()
        cmdA.Parameters.Add("@manajemenHitam", SqlDbType.VarChar, 50).Value = e.Form(master + "InpManajemenHitam").ToString()
        cmdA.Parameters.Add("@perusahaanHitam", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPerusahaanHitam").ToString()
        cmdA.Parameters.Add("@pemilikMasalah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPemilikMasalah").ToString()
        cmdA.Parameters.Add("@manajemenMasalah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpManajemenMasalah").ToString()
        cmdA.Parameters.Add("@perusahaanMasalah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPerusahaanMasalah").ToString()
        cmdA.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = e.Form(master + "InpStatus").ToString()
        cmdA.Parameters.Add("@rencana", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRencana").ToString()
        cmdA.Parameters.Add("@statusBi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpStatusBi").ToString()
        cmdA.Parameters.Add("@rencanaBi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRencanaBi").ToString()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updatePreScreenInfo(ByVal e, ByVal BrwCode)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updatePreScreenInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@pemilikHitam", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPemilikHitam").ToString()
        cmdA.Parameters.Add("@manajemenHitam", SqlDbType.VarChar, 50).Value = e.Form(master + "InpManajemenHitam").ToString()
        cmdA.Parameters.Add("@perusahaanHitam", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPerusahaanHitam").ToString()
        cmdA.Parameters.Add("@pemilikMasalah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPemilikMasalah").ToString()
        cmdA.Parameters.Add("@manajemenMasalah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpManajemenMasalah").ToString()
        cmdA.Parameters.Add("@perusahaanMasalah", SqlDbType.VarChar, 50).Value = e.Form(master + "InpPerusahaanMasalah").ToString()
        cmdA.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = e.Form(master + "InpStatus").ToString()
        cmdA.Parameters.Add("@rencana", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRencana").ToString()
        cmdA.Parameters.Add("@statusBi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpStatusBi").ToString()
        cmdA.Parameters.Add("@rencanaBi", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRencanaBi").ToString()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkPreScreen(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkPreScreen", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brw_code", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkDuplicate(ByVal BrwCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkDuplicate", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = BrwCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertDuplicateInfo(ByVal e, ByVal Data(), ByVal BrwCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertDuplicateInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@debitur", SqlDbType.Char).Value = Data(0)
        cmdA.Parameters.Add("@pengurus", SqlDbType.Char).Value = Data(1)
        cmdA.Parameters.Add("@perusahaan", SqlDbType.Char).Value = Data(2)
        cmdA.Parameters.Add("@obligor", SqlDbType.Char).Value = Data(3)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateDuplicateInfo(ByVal e, ByVal Data(), ByVal BrwCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateDuplicateInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = BrwCode
        cmdA.Parameters.Add("@debitur", SqlDbType.Char).Value = Data(0)
        cmdA.Parameters.Add("@pengurus", SqlDbType.Char).Value = Data(1)
        cmdA.Parameters.Add("@perusahaan", SqlDbType.Char).Value = Data(2)
        cmdA.Parameters.Add("@obligor", SqlDbType.Char).Value = Data(3)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

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

    Sub insertAspekRPC(ByVal obj, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertAspekRPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = obj.appno
        cmdA.Parameters.Add("@shareCalonDebitur", SqlDbType.VarChar, 50).Value = obj.shareCalonDebitur
        cmdA.Parameters.Add("@BCRtoPI", SqlDbType.VarChar, 50).Value = obj.BCRtoPI
        cmdA.Parameters.Add("@RasioJumlahKreditToNilaiProyek", SqlDbType.VarChar, 50).Value = obj.RasioJumlahKreditToNilaiProyek
        cmdA.Parameters.Add("@RasioTotalPenjualantoTarikanPinjaman", SqlDbType.VarChar, 50).Value = obj.RasioTotalPenjualantoTarikanPinjaman
        cmdA.Parameters.Add("@RasioNetIncomeToTotalKewajiban", SqlDbType.VarChar, 50).Value = obj.RasioNetIncomeToTotalKewajiban
        cmdA.Parameters.Add("@NPV", SqlDbType.VarChar, 50).Value = obj.NPV
        cmdA.Parameters.Add("@ProjectedCashFlow", SqlDbType.VarChar, 50).Value = obj.ProjectedCashFlow
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateAspekRPC(ByVal obj, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateAspekRPC", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", obj.appno)
        cmdA.Parameters.AddWithValue("@shareCalonDebitur", obj.shareCalonDebitur)
        cmdA.Parameters.AddWithValue("@BCRtoPI", obj.BCRtoPI)
        cmdA.Parameters.AddWithValue("@RasioJumlahKreditToNilaiProyek", obj.RasioJumlahKreditToNilaiProyek)
        cmdA.Parameters.AddWithValue("@RasioTotalPenjualantoTarikanPinjaman", obj.RasioTotalPenjualantoTarikanPinjaman)
        cmdA.Parameters.AddWithValue("@RasioNetIncomeToTotalKewajiban", obj.RasioNetIncomeToTotalKewajiban)
        cmdA.Parameters.AddWithValue("@NPV", obj.NPV)
        cmdA.Parameters.AddWithValue("@ProjectedCashFlow", obj.ProjectedCashFlow)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    '===========end tambahan wisnu=================

    '===========edit untuk borrower================
    Function checkFacilityKreditInfoFromBrw(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkFacilityCreditInfoByBrw", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertFacilityKreditInfoFromBrw(ByVal obj, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertFacilityCreditInfoByBrw", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
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
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityKreditInfoFromBrw(ByVal obj, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateFacilityCreditInfoByBrw", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
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
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '===========end edit untuk borrower============


    '===========proyeksi cash flow dan laba rugi=========
    Function checkProjectionCashFlow(ByVal appno, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProjectionCashFlow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProjectionProfitLoss(ByVal appno, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProjectionProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = year
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProjectionCashFlowFile(ByVal appno, ByVal doc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProjectionCashFlowFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@doc", SqlDbType.VarChar, 5000).Value = doc
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProjectionCashFlowTotal(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProjectionCashFlowTotal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkProjectionProfitLossTotal(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProjectionProfitLossTotal", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertProjectedCashFlow(ByVal obj, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertProjectedCashFlow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = obj.form("ddlTahun")
        cmdA.Parameters.Add("@saldoAwal", SqlDbType.Decimal).Value = obj.form("txtSaldoAwal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@penerimaanPenjualan", SqlDbType.Decimal).Value = obj.form("txtPenerimaanPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pencairanKredit", SqlDbType.Decimal).Value = obj.form("txtPencairanKredit").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashIn3Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashIn3")
        cmdA.Parameters.Add("@cashIn3Value", SqlDbType.Decimal).Value = obj.form("txtCashIn3Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashIn4Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashIn4")
        cmdA.Parameters.Add("@cashIn4Value", SqlDbType.Decimal).Value = obj.form("txtCashIn4Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCashIn", SqlDbType.Decimal).Value = obj.form("txtTotalCashIn").ToString.Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = obj.form("txtBiayaBunga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kewajibanPokokJatuhTempo", SqlDbType.Decimal).Value = obj.form("txtKewajibanPokokJatuhTempo").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashOut3Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashOut3")
        cmdA.Parameters.Add("@cashOut3Value", SqlDbType.Decimal).Value = obj.form("txtCashOut3Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashOut4Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashOut4")
        cmdA.Parameters.Add("@cashOut4Value", SqlDbType.Decimal).Value = obj.form("txtCashOut4Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCashOut", SqlDbType.Decimal).Value = obj.form("txtTotalCashOut").ToString.Replace(",", "")
        cmdA.Parameters.Add("@saldoAkhir", SqlDbType.Decimal).Value = obj.form("txtSaldoAkhir").ToString.Replace(",", "")
        cmdA.Parameters.Add("@posisiOutstanding", SqlDbType.Decimal).Value = obj.form("txtPosisiOutstanding").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateProjectedCashFlow(ByVal obj, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateProjectedCashFlow", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = obj.form("ddlTahun")
        cmdA.Parameters.Add("@saldoAwal", SqlDbType.Decimal).Value = obj.form("txtSaldoAwal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@penerimaanPenjualan", SqlDbType.Decimal).Value = obj.form("txtPenerimaanPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pencairanKredit", SqlDbType.Decimal).Value = obj.form("txtPencairanKredit").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashIn3Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashIn3")
        cmdA.Parameters.Add("@cashIn3Value", SqlDbType.Decimal).Value = obj.form("txtCashIn3Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashIn4Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashIn4")
        cmdA.Parameters.Add("@cashIn4Value", SqlDbType.Decimal).Value = obj.form("txtCashIn4Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCashIn", SqlDbType.Decimal).Value = obj.form("txtTotalCashIn").ToString.Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = obj.form("txtBiayaBunga").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kewajibanPokokJatuhTempo", SqlDbType.Decimal).Value = obj.form("txtKewajibanPokokJatuhTempo").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashOut3Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashOut3")
        cmdA.Parameters.Add("@cashOut3Value", SqlDbType.Decimal).Value = obj.form("txtCashOut3Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@cashOut4Field", SqlDbType.VarChar, 250).Value = obj.form("txtCashOut4")
        cmdA.Parameters.Add("@cashOut4Value", SqlDbType.Decimal).Value = obj.form("txtCashOut4Value").ToString.Replace(",", "")
        cmdA.Parameters.Add("@totalCashOut", SqlDbType.Decimal).Value = obj.form("txtTotalCashOut").ToString.Replace(",", "")
        cmdA.Parameters.Add("@saldoAkhir", SqlDbType.Decimal).Value = obj.form("txtSaldoAkhir").ToString.Replace(",", "")
        cmdA.Parameters.Add("@posisiOutstanding", SqlDbType.Decimal).Value = obj.form("txtPosisiOutstanding").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertProjectedProfitLoss(ByVal obj, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertProjectedProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = obj.form("ddlTahun")
        cmdA.Parameters.Add("@ebitda", SqlDbType.Decimal).Value = obj.form("txtEBITDA").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateProjectedProfitLoss(ByVal obj, ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateProjectedProfitLoss", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@year", SqlDbType.Int).Value = obj.form("ddlTahun")
        cmdA.Parameters.Add("@ebitda", SqlDbType.Decimal).Value = obj.form("txtEBITDA").ToString.Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateProjectedCashFlowFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateProjectedCashFlowFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '===========end proyeksi cash flow dan laba rugi=========

    '===========profit loss and balance sheet latest data=============
    Function checkProfitLossLatest(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkProfitLossLatest", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBalanceSheetLatest(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBalanceSheetLatest", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    '===========end profit loss and balance sheet latest data=============

    '===========external rating=============
    Function getRatingAgency()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getRatingAgency", myConn)
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
        Dim cmdA As New SqlCommand("app_getRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 50).Value = Agency
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getExternalRatingDesc(ByVal Agency, ByVal Rating)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getRatingDesc", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 50).Value = Agency
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = Rating
        cmdA.Parameters.AddWithValue("@rating", Rating)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkExRatingInfo(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertExRatingInfo(ByVal e, ByVal brwcode, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglRating As String = "1900-01-01"
        If Not e.form(master + "InpTglRating") = "" Then
            tglRating = Mid(e.form(master + "InpTglRating"), 7, 4) + "-" & Mid(e.form(master + "InpTglRating"), 4, 2) + "-" & Mid(e.form(master + "InpTglRating"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@ratingAgency", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatingAgency")
        cmdA.Parameters.Add("@corpType", SqlDbType.VarChar, 50).Value = e.Form(master + "InpCorp")
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRating")
        cmdA.Parameters.AddWithValue("@rating", e.Form(master + "InpRating"))
        cmdA.Parameters.Add("@tglRating", SqlDbType.Date).Value = tglRating
        'cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateExRatingInfo(ByVal e, ByVal brwcode, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim tglRating As String = "1900-01-01"
        If Not e.form(master + "InpTglRating") = "" Then
            tglRating = Mid(e.form(master + "InpTglRating"), 7, 4) + "-" & Mid(e.form(master + "InpTglRating"), 4, 2) + "-" & Mid(e.form(master + "InpTglRating"), 1, 2)
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@ratingAgency", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRatingAgency")
        cmdA.Parameters.Add("@corpType", SqlDbType.VarChar, 50).Value = e.Form(master + "InpCorp")
        'cmdA.Parameters.Add("@rating", SqlDbType.VarChar, 50).Value = e.Form(master + "InpRating")
        cmdA.Parameters.AddWithValue("@rating", e.Form(master + "InpRating"))
        cmdA.Parameters.Add("@tglRating", SqlDbType.Date).Value = tglRating
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteExRatingInfo(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteExRatingInfo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '===========end external rating=============

    '===========facility credit investment=============
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

    Sub insertFacilityCreditInvestment(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertFacilityCreditInvestment", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@nilaiProyek", SqlDbType.Decimal).Value = e.Form(master + "InpProyek").ToString().Replace(",", "")
        cmdA.Parameters.Add("@selfFinancing", SqlDbType.Decimal).Value = e.Form(master + "InpSelf").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maxKreditBTN", SqlDbType.Decimal).Value = e.Form(master + "InpJumlah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafonKredit", SqlDbType.Decimal).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@porsiTerhadapTotalKebutuhan", SqlDbType.Float).Value = e.Form(master + "InpPorsiYangDiusulkan")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit")
        cmdA.Parameters.Add("@maxTarikanKredit", SqlDbType.Decimal).Value = e.Form(master + "InpMaksTotalTarikan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@IRR", SqlDbType.Float).Value = e.Form(master + "InpIRR").ToString().Replace(",", "")

        cmdA.Parameters.Add("@objekInvestasi", SqlDbType.Decimal).Value = e.Form(master + "InpObjekInvestasi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalNilaiAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpTotalNilaiAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunanTerhadapPlafon", SqlDbType.Float).Value = e.Form(master + "InpRasioAgunanTerhadapPlafond")
        cmdA.Parameters.Add("@minimalTotalAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpMinimalTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganAgunanTambahan").ToString().Replace(",", "")

        cmdA.Parameters.Add("@proyeksiEBITDA", SqlDbType.Decimal).Value = e.Form(master + "InpProyeksiEBITDA").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kewajibanPokokJatuhTempo", SqlDbType.Decimal).Value = e.Form(master + "InpKewajibanPokokJatuhTempo").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalianBank", SqlDbType.Decimal).Value = e.Form(master + "InpTotalPengembalianBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalianKewajiban", SqlDbType.Float).Value = e.Form(master + "InpRasioPengembalianKewajiban")

        cmdA.Parameters.Add("@totalHutangExistingNeraca", SqlDbType.Decimal).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExisting", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ekuitas", SqlDbType.Decimal).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.Decimal).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@DERExisting", SqlDbType.Float).Value = e.Form(master + "InpDerEksis")
        cmdA.Parameters.Add("@proyeksiDER", SqlDbType.Float).Value = e.Form(master + "InpProyeksiDer")
        cmdA.Parameters.Add("@modalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExistingBTN", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiKelayakanModal", SqlDbType.Float).Value = e.Form(master + "InpProyeksiRasio")
        cmdA.Parameters.Add("@minimumModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditInvestment(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateFacilityCreditInvestment", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@nilaiProyek", SqlDbType.Decimal).Value = e.Form(master + "InpProyek").ToString().Replace(",", "")
        cmdA.Parameters.Add("@selfFinancing", SqlDbType.Decimal).Value = e.Form(master + "InpSelf").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maxKreditBTN", SqlDbType.Decimal).Value = e.Form(master + "InpJumlah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafonKredit", SqlDbType.Decimal).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@porsiTerhadapTotalKebutuhan", SqlDbType.Float).Value = e.Form(master + "InpPorsiYangDiusulkan")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit")
        cmdA.Parameters.Add("@maxTarikanKredit", SqlDbType.Decimal).Value = e.Form(master + "InpMaksTotalTarikan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@IRR", SqlDbType.Float).Value = e.Form(master + "InpIRR").ToString().Replace(",", "")

        cmdA.Parameters.Add("@objekInvestasi", SqlDbType.Decimal).Value = e.Form(master + "InpObjekInvestasi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalNilaiAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpTotalNilaiAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunanTerhadapPlafon", SqlDbType.Float).Value = e.Form(master + "InpRasioAgunanTerhadapPlafond")
        cmdA.Parameters.Add("@minimalTotalAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpMinimalTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganAgunanTambahan").ToString().Replace(",", "")

        cmdA.Parameters.Add("@proyeksiEBITDA", SqlDbType.Decimal).Value = e.Form(master + "InpProyeksiEBITDA").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kewajibanPokokJatuhTempo", SqlDbType.Decimal).Value = e.Form(master + "InpKewajibanPokokJatuhTempo").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalianBank", SqlDbType.Decimal).Value = e.Form(master + "InpTotalPengembalianBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalianKewajiban", SqlDbType.Float).Value = e.Form(master + "InpRasioPengembalianKewajiban")

        cmdA.Parameters.Add("@totalHutangExistingNeraca", SqlDbType.Decimal).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExisting", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ekuitas", SqlDbType.Decimal).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.Decimal).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@DERExisting", SqlDbType.Float).Value = e.Form(master + "InpDerEksis")
        cmdA.Parameters.Add("@proyeksiDER", SqlDbType.Float).Value = e.Form(master + "InpProyeksiDer")
        cmdA.Parameters.Add("@modalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExistingBTN", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiKelayakanModal", SqlDbType.Float).Value = e.Form(master + "InpProyeksiRasio")
        cmdA.Parameters.Add("@minimumModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '===========end facility Credit Investment=============

    '===========end facility Credit Construction=============
    Function checkFacilityCreditKonstruksi(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkFacilityCreditKons", myConn)
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
        Dim cmdA As New SqlCommand("app_insertFacilityCreditKons", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appNo", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@biayaTanah", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBangunan", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sarana", SqlDbType.Decimal).Value = e.Form(master + "InpSarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prasarana", SqlDbType.Decimal).Value = e.Form(master + "InpPrasarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalBiaya", SqlDbType.Decimal).Value = e.Form(master + "InpTotalBiaya").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasilPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpHasilPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@labaKotor", SqlDbType.Decimal).Value = e.Form(master + "InpLabaKotor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaKonstruksi", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaKonstruksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@salesMargin", SqlDbType.Float).Value = e.Form(master + "InpSalesMargin").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDev", SqlDbType.Decimal).Value = e.Form(master + "InpShareDev").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDevPersen", SqlDbType.Float).Value = e.Form(master + "InpShareDevPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaDibutuhkan", SqlDbType.Decimal).Value = e.Form(master + "InpDanaDibutuhkan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.Decimal).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prosPembiayaan", SqlDbType.Float).Value = e.Form(master + "InpProsPembiayaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maksTotalTarikan", SqlDbType.Decimal).Value = e.Form(master + "InpMaksTotalTarikan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@taksasiTanah", SqlDbType.Decimal).Value = e.Form(master + "InpTaksasiTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunan", SqlDbType.Decimal).Value = e.Form(master + "InpRabBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunanPersen", SqlDbType.Float).Value = e.Form(master + "InpRabBangunanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@catatanAgunan", SqlDbType.Text).Value = e.Form(master + "InpCatatanAgunan")
        cmdA.Parameters.Add("@rasioAgunan", SqlDbType.Float).Value = e.Form(master + "InpRasioAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minimalTotal", SqlDbType.Decimal).Value = e.Form(master + "InpMinimalTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPenerimaanPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.Decimal).Value = e.Form(master + "InpUangMuka").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.Float).Value = e.Form(master + "InpUangMukaPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanKpr", SqlDbType.Decimal).Value = e.Form(master + "InpPenerimaanKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminan", SqlDbType.Decimal).Value = e.Form(master + "InpDanaJaminan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminanPersen", SqlDbType.Float).Value = e.Form(master + "InpDanaJaminanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nettKpr", SqlDbType.Decimal).Value = e.Form(master + "InpNettKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalian", SqlDbType.Decimal).Value = e.Form(master + "InpTotalPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalian", SqlDbType.Float).Value = e.Form(master + "InpRasioPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hutangEksis", SqlDbType.Decimal).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutang", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutang", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutang", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalNeraca", SqlDbType.Decimal).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitas", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitas", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.Decimal).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@derEksis", SqlDbType.Float).Value = e.Form(master + "InpDerEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiDer", SqlDbType.Float).Value = e.Form(master + "InpProyeksiDer").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangEksis", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRasio", SqlDbType.Float).Value = e.Form(master + "InpProyeksiRasio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minModal", SqlDbType.Decimal).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModal", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditKonstruksi(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateFacilityCreditKons", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appNo", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@biayaTanah", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBangunan", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sarana", SqlDbType.Decimal).Value = e.Form(master + "InpSarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prasarana", SqlDbType.Decimal).Value = e.Form(master + "InpPrasarana").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalBiaya", SqlDbType.Decimal).Value = e.Form(master + "InpTotalBiaya").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hasilPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpHasilPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@labaKotor", SqlDbType.Decimal).Value = e.Form(master + "InpLabaKotor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaKonstruksi", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaKonstruksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@salesMargin", SqlDbType.Float).Value = e.Form(master + "InpSalesMargin").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDev", SqlDbType.Decimal).Value = e.Form(master + "InpShareDev").ToString().Replace(",", "")
        cmdA.Parameters.Add("@shareDevPersen", SqlDbType.Float).Value = e.Form(master + "InpShareDevPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaDibutuhkan", SqlDbType.Decimal).Value = e.Form(master + "InpDanaDibutuhkan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.Decimal).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@prosPembiayaan", SqlDbType.Float).Value = e.Form(master + "InpProsPembiayaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@sifatKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpSifatKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jenisKredit", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisKredit").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maksTotalTarikan", SqlDbType.Decimal).Value = e.Form(master + "InpMaksTotalTarikan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@taksasiTanah", SqlDbType.Decimal).Value = e.Form(master + "InpTaksasiTanah").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunan", SqlDbType.Decimal).Value = e.Form(master + "InpRabBangunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rabBangunanPersen", SqlDbType.Float).Value = e.Form(master + "InpRabBangunanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@catatanAgunan", SqlDbType.Text).Value = e.Form(master + "InpCatatanAgunan")
        cmdA.Parameters.Add("@rasioAgunan", SqlDbType.Float).Value = e.Form(master + "InpRasioAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minimalTotal", SqlDbType.Decimal).Value = e.Form(master + "InpMinimalTotal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPenerimaanPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.Decimal).Value = e.Form(master + "InpUangMuka").ToString().Replace(",", "")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.Float).Value = e.Form(master + "InpUangMukaPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penerimaanKpr", SqlDbType.Decimal).Value = e.Form(master + "InpPenerimaanKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminan", SqlDbType.Decimal).Value = e.Form(master + "InpDanaJaminan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@danaJaminanPersen", SqlDbType.Float).Value = e.Form(master + "InpDanaJaminanPersen").ToString().Replace(",", "")
        cmdA.Parameters.Add("@nettKpr", SqlDbType.Decimal).Value = e.Form(master + "InpNettKpr").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalian", SqlDbType.Decimal).Value = e.Form(master + "InpTotalPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalian", SqlDbType.Float).Value = e.Form(master + "InpRasioPengembalian").ToString().Replace(",", "")
        cmdA.Parameters.Add("@hutangEksis", SqlDbType.Decimal).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutang", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutang", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutang", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalNeraca", SqlDbType.Decimal).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitas", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitas", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.Decimal).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@derEksis", SqlDbType.Float).Value = e.Form(master + "InpDerEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiDer", SqlDbType.Float).Value = e.Form(master + "InpProyeksiDer").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangEksis", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRasio", SqlDbType.Float).Value = e.Form(master + "InpProyeksiRasio").ToString().Replace(",", "")
        cmdA.Parameters.Add("@minModal", SqlDbType.Decimal).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModal", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '===========end facility Credit Construction=============

    '===========facility Credit Contractor=============
    Function checkFacilityCreditKontraktor(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkFacilityCreditKont", myConn)
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
        Dim cmdA As New SqlCommand("app_insertFacilityCreditKont", myConn)
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
        cmdA.Parameters.Add("@jenisPencairan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisPencarian")
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
        Dim cmdA As New SqlCommand("app_updateFacilityCreditKont", myConn)
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
        cmdA.Parameters.Add("@jenisPencairan", SqlDbType.VarChar, 50).Value = e.Form(master + "InpJenisPencarian")
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
    '===========end facility Credit Construction=============

    '===========facility Credit KMK Murni=============
    Function checkFacilityCreditKMK(ByVal AppNo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkFacilityCreditKMK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertFacilityCreditKMK(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertFacilityCreditKMK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@penjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPenjualanProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataKasDanBank", SqlDbType.Decimal).Value = e.Form(master + "InpRataKasBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRataKasDanBank", SqlDbType.Decimal).Value = e.Form(master + "InpRataKasBankProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPiutang", SqlDbType.Decimal).Value = e.Form(master + "InpRataPiutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRataPiutang", SqlDbType.Decimal).Value = e.Form(master + "InpRataPiutangProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPersediaan", SqlDbType.Decimal).Value = e.Form(master + "InpRataPersediaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRataPersediaan", SqlDbType.Decimal).Value = e.Form(master + "InpRataPersediaanProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@HPP", SqlDbType.Decimal).Value = e.Form(master + "InpHPP").ToString().Replace(",", "").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiHPP", SqlDbType.Decimal).Value = e.Form(master + "InpHPPProyeksi").ToString().Replace(",", "").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahHariLaporanKeuangan", SqlDbType.Int).Value = e.Form(master + "InpJumlahHari").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kasDanBank", SqlDbType.Float).Value = e.Form(master + "InpKasBank")
        cmdA.Parameters.Add("@piutang", SqlDbType.Float).Value = e.Form(master + "InpPiutang")
        cmdA.Parameters.Add("@persediaan", SqlDbType.Float).Value = e.Form(master + "InpPersediaan")
        cmdA.Parameters.Add("@WCTO", SqlDbType.Float).Value = e.Form(master + "InpLama")

        cmdA.Parameters.Add("@jumlahHariToWCTO", SqlDbType.Float).Value = e.Form(master + "InpPeriode")
        cmdA.Parameters.Add("@tingkatPenjualanSekarang", SqlDbType.Decimal).Value = e.Form(master + "InpTingkatJual").ToString().Replace(",", "")
        cmdA.Parameters.Add("@peningkatanPenjualanPercent", SqlDbType.Float).Value = e.Form(master + "InpPeningkatan1")
        cmdA.Parameters.Add("@peningkatanPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPeningkatan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanModalKerja", SqlDbType.Decimal).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalAsetLancar", SqlDbType.Decimal).Value = e.Form(master + "InpAktiva").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kelonggaranTarik", SqlDbType.Decimal).Value = e.Form(master + "InpLonggar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalKerjaYangAda", SqlDbType.Decimal).Value = e.Form(master + "InpModalKerja").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pembayaranKepadaBTN", SqlDbType.Decimal).Value = e.Form(master + "InpPembayaranBTN").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pembayaranKepadaBankLain", SqlDbType.Decimal).Value = e.Form(master + "InpPembayaran").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maxKebutuhanModalKerja", SqlDbType.Decimal).Value = e.Form(master + "InpMaxKMK").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.Decimal).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")

        cmdA.Parameters.Add("@persediaanBarangModal", SqlDbType.Decimal).Value = e.Form(master + "InpPersediaanBarangModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPersediaan", SqlDbType.Float).Value = e.Form(master + "InpRasioPersediaan")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalNilaiAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpTotalNilaiAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunanToPlafond", SqlDbType.Float).Value = e.Form(master + "InpRasioAgunanTerhadapPlafond")
        cmdA.Parameters.Add("@minTotalAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpMinTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganAgunanTambahan").ToString().Replace(",", "")

        cmdA.Parameters.Add("@proyeksiEBITDA", SqlDbType.Decimal).Value = e.Form(master + "InpProyeksiEBITDA").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kewajibanPokokJatuhTempo", SqlDbType.Decimal).Value = e.Form(master + "InpKewajibanPokokJatuhTempo").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalianBank", SqlDbType.Decimal).Value = e.Form(master + "InpTotalPengembalianBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalianKewajiban", SqlDbType.Float).Value = e.Form(master + "InpRasioPengembalianKewajiban")

        cmdA.Parameters.Add("@totalHutangExistingNeraca", SqlDbType.Decimal).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExisting", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ekuitas", SqlDbType.Decimal).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.Decimal).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@DERExisting", SqlDbType.Float).Value = e.Form(master + "InpDerEksis")
        cmdA.Parameters.Add("@proyeksiDER", SqlDbType.Float).Value = e.Form(master + "InpProyeksiDer")
        cmdA.Parameters.Add("@modalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExistingBTN", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiKelayakanModal", SqlDbType.Float).Value = e.Form(master + "InpProyeksiRasio")
        cmdA.Parameters.Add("@minimumModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateFacilityCreditKMK(ByVal e, ByVal AppNo)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateFacilityCreditKMK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = AppNo
        cmdA.Parameters.Add("@penjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPenjualan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPenjualanProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataKasDanBank", SqlDbType.Decimal).Value = e.Form(master + "InpRataKasBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRataKasDanBank", SqlDbType.Decimal).Value = e.Form(master + "InpRataKasBankProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPiutang", SqlDbType.Decimal).Value = e.Form(master + "InpRataPiutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRataPiutang", SqlDbType.Decimal).Value = e.Form(master + "InpRataPiutangProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rataPersediaan", SqlDbType.Decimal).Value = e.Form(master + "InpRataPersediaan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiRataPersediaan", SqlDbType.Decimal).Value = e.Form(master + "InpRataPersediaanProyeksi").ToString().Replace(",", "")
        cmdA.Parameters.Add("@HPP", SqlDbType.Decimal).Value = e.Form(master + "InpHPP").ToString().Replace(",", "").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiHPP", SqlDbType.Decimal).Value = e.Form(master + "InpHPPProyeksi").ToString().Replace(",", "").ToString().Replace(",", "")
        cmdA.Parameters.Add("@jumlahHariLaporanKeuangan", SqlDbType.Int).Value = e.Form(master + "InpJumlahHari").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kasDanBank", SqlDbType.Float).Value = e.Form(master + "InpKasBank")
        cmdA.Parameters.Add("@piutang", SqlDbType.Float).Value = e.Form(master + "InpPiutang")
        cmdA.Parameters.Add("@persediaan", SqlDbType.Float).Value = e.Form(master + "InpPersediaan")
        cmdA.Parameters.Add("@WCTO", SqlDbType.Float).Value = e.Form(master + "InpLama")

        cmdA.Parameters.Add("@jumlahHariToWCTO", SqlDbType.Float).Value = e.Form(master + "InpPeriode")
        cmdA.Parameters.Add("@tingkatPenjualanSekarang", SqlDbType.Decimal).Value = e.Form(master + "InpTingkatJual").ToString().Replace(",", "")
        cmdA.Parameters.Add("@peningkatanPenjualanPercent", SqlDbType.Float).Value = e.Form(master + "InpPeningkatan1")
        cmdA.Parameters.Add("@peningkatanPenjualan", SqlDbType.Decimal).Value = e.Form(master + "InpPeningkatan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanModalKerja", SqlDbType.Decimal).Value = e.Form(master + "InpKebutuhan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalAsetLancar", SqlDbType.Decimal).Value = e.Form(master + "InpAktiva").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kelonggaranTarik", SqlDbType.Decimal).Value = e.Form(master + "InpLonggar").ToString().Replace(",", "")
        cmdA.Parameters.Add("@modalKerjaYangAda", SqlDbType.Decimal).Value = e.Form(master + "InpModalKerja").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pembayaranKepadaBTN", SqlDbType.Decimal).Value = e.Form(master + "InpPembayaranBTN").ToString().Replace(",", "")
        cmdA.Parameters.Add("@pembayaranKepadaBankLain", SqlDbType.Decimal).Value = e.Form(master + "InpPembayaran").ToString().Replace(",", "")
        cmdA.Parameters.Add("@maxKebutuhanModalKerja", SqlDbType.Decimal).Value = e.Form(master + "InpMaxKMK").ToString().Replace(",", "")
        cmdA.Parameters.Add("@plafondKredit", SqlDbType.Decimal).Value = e.Form(master + "InpPlafondKredit").ToString().Replace(",", "")

        cmdA.Parameters.Add("@persediaanBarangModal", SqlDbType.Decimal).Value = e.Form(master + "InpPersediaanBarangModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPersediaan", SqlDbType.Float).Value = e.Form(master + "InpRasioPersediaan")
        cmdA.Parameters.Add("@agunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpAgunanTambahan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalNilaiAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpTotalNilaiAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioAgunanToPlafond", SqlDbType.Float).Value = e.Form(master + "InpRasioAgunanTerhadapPlafond")
        cmdA.Parameters.Add("@minTotalAgunan", SqlDbType.Decimal).Value = e.Form(master + "InpMinTotalAgunan").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganAgunanTambahan", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganAgunanTambahan").ToString().Replace(",", "")

        cmdA.Parameters.Add("@proyeksiEBITDA", SqlDbType.Decimal).Value = e.Form(master + "InpProyeksiEBITDA").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kewajibanPokokJatuhTempo", SqlDbType.Decimal).Value = e.Form(master + "InpKewajibanPokokJatuhTempo").ToString().Replace(",", "")
        cmdA.Parameters.Add("@biayaBunga", SqlDbType.Decimal).Value = e.Form(master + "InpBiayaBunga").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalPengembalianBank", SqlDbType.Decimal).Value = e.Form(master + "InpTotalPengembalianBank").ToString().Replace(",", "")
        cmdA.Parameters.Add("@rasioPengembalianKewajiban", SqlDbType.Float).Value = e.Form(master + "InpRasioPengembalianKewajiban")

        cmdA.Parameters.Add("@totalHutangExistingNeraca", SqlDbType.Decimal).Value = e.Form(master + "InpHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanHutangTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExisting", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutang").ToString().Replace(",", "")
        cmdA.Parameters.Add("@ekuitas", SqlDbType.Decimal).Value = e.Form(master + "InpModalNeraca").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penambahanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenambahanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@penurunanEkuitasTahunBerjalan", SqlDbType.Decimal).Value = e.Form(master + "InpPenurunanEkuitas").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalModal", SqlDbType.Decimal).Value = e.Form(master + "InpTotalModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@DERExisting", SqlDbType.Float).Value = e.Form(master + "InpDerEksis")
        cmdA.Parameters.Add("@proyeksiDER", SqlDbType.Float).Value = e.Form(master + "InpProyeksiDer")
        cmdA.Parameters.Add("@modalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpModalDisetor").ToString().Replace(",", "")
        cmdA.Parameters.Add("@totalHutangExistingBTN", SqlDbType.Decimal).Value = e.Form(master + "InpTotalHutangEksis").ToString().Replace(",", "")
        cmdA.Parameters.Add("@proyeksiKelayakanModal", SqlDbType.Float).Value = e.Form(master + "InpProyeksiRasio")
        cmdA.Parameters.Add("@minimumModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpMinModal").ToString().Replace(",", "")
        cmdA.Parameters.Add("@kekuranganModalYangDisetor", SqlDbType.Decimal).Value = e.Form(master + "InpKekuranganModal").ToString().Replace(",", "")
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '===========end facility Credit KMK Murni=============

    '=====================tambahan========================
    Sub deleteProjectedCashFlowFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteProjectedCashFlowFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Function getListOfAllAssignApp()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfAllAssignApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub resetAssignApp(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_resetAssignApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkCreditRecommendationFile(ByVal appno, ByVal doc, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkCreditRecommendationFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = doc
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkMBKFile(ByVal appno, ByVal doc, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkMBKFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = doc
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkMAKFile(ByVal appno, ByVal doc, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkMAKFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = doc
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkMBKFile_byappno(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkMBKFilebyappno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkMAKFile_byappno(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkMAKFilebyappno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkCreditFinalFile(ByVal appno, ByVal doc, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkCreditFinalFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@doc", SqlDbType.VarChar, 5000).Value = doc
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub deleteCreditFinalFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteCreditFinalFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteMBKFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteMBKFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteMAKFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteMAKFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub insertCreditFinalFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertCreditFinalFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteCreditRecommendationFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteCreditRecommendationFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertCreditRecommendationFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertCreditRecommendationFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertMBKFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertMBKFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertMAKFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertMAKFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateCreditRecommendationFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateCreditRecommendationFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateMBKFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateMAKFile(ByVal appno, ByVal file, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.Parameters.Add("@process", SqlDbType.Int).Value = proses
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertProjectedCashFlowFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertProjectedCashFlowFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateCreditFinalFile(ByVal appno, ByVal file)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateCreditFinalFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@file", SqlDbType.VarChar, 5000).Value = file
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '=====================end tambahan========================

    '================ Tambahan 18 Oct 2016 ===================
    Function getListOfAllClosedApp(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfAllClosedApp", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    '================End Tambahan 18 Oct 2016 ===================

    '================Tambahan 11 Nov 2016 ===================
    Function checkCallMemo(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkCallMemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfCallMemo(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDetailOfCallMemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertCallMemo(ByVal e, ByVal appno, ByVal usr)
        Dim tgl As String = ""

        If Not e.form("txtTgl") = "" Then
            tgl = Mid(e.form("txtTgl"), 7, 4) + "-" & Mid(e.form("txtTgl"), 4, 2) + "-" & Mid(e.form("txtTgl"), 1, 2)
        Else
            tgl = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertCallMemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@tgl", SqlDbType.Date).Value = tgl
        cmdA.Parameters.Add("@noTelp", SqlDbType.VarChar, 50).Value = e.form("txtNoTelp")
        cmdA.Parameters.Add("@namaPerusahaan", SqlDbType.VarChar, 500).Value = e.form("txtNama")
        cmdA.Parameters.Add("@noTelpLain", SqlDbType.VarChar, 50).Value = e.form("txtNoTelpLain")
        cmdA.Parameters.Add("@bentukCall", SqlDbType.VarChar, 500).Value = e.form("txtBentukCall")
        cmdA.Parameters.Add("@yangDiCall", SqlDbType.VarChar, 50).Value = e.form("txtPenjabatYangDiCall")
        cmdA.Parameters.Add("@yangMelakukanCall", SqlDbType.VarChar, 50).Value = e.form("txtPenjabatYangMelakukanCall")
        cmdA.Parameters.Add("@tujuanCall", SqlDbType.VarChar, 500).Value = e.form("txtTujuanCall")
        cmdA.Parameters.Add("@hasil", SqlDbType.Text).Value = e.form("txtHasil")
        cmdA.Parameters.Add("@tindakLanjut", SqlDbType.VarChar, 4000).Value = e.form("txtTindakLanjut")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateCallMemo(ByVal e, ByVal appno, ByVal id, ByVal usr)
        Dim tgl As String = ""

        If Not e.form("txtTgl") = "" Then
            tgl = Mid(e.form("txtTgl"), 7, 4) + "-" & Mid(e.form("txtTgl"), 4, 2) + "-" & Mid(e.form("txtTgl"), 1, 2)
        Else
            tgl = "1900-01-01"
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateCallMemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@tgl", SqlDbType.Date).Value = tgl
        cmdA.Parameters.Add("@noTelp", SqlDbType.VarChar, 50).Value = e.form("txtNoTelp")
        cmdA.Parameters.Add("@namaPerusahaan", SqlDbType.VarChar, 500).Value = e.form("txtNama")
        cmdA.Parameters.Add("@noTelpLain", SqlDbType.VarChar, 50).Value = e.form("txtNoTelpLain")
        cmdA.Parameters.Add("@bentukCall", SqlDbType.VarChar, 500).Value = e.form("txtBentukCall")
        cmdA.Parameters.Add("@yangDiCall", SqlDbType.VarChar, 50).Value = e.form("txtPenjabatYangDiCall")
        cmdA.Parameters.Add("@yangMelakukanCall", SqlDbType.VarChar, 50).Value = e.form("txtPenjabatYangMelakukanCall")
        cmdA.Parameters.Add("@tujuanCall", SqlDbType.VarChar, 500).Value = e.form("txtTujuanCall")
        cmdA.Parameters.Add("@hasil", SqlDbType.Text).Value = e.form("txtHasil")
        cmdA.Parameters.Add("@tindakLanjut", SqlDbType.VarChar, 4000).Value = e.form("txtTindakLanjut")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteCallMemo(ByVal id, ByVal usr)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_deleteCallMemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkPreScreening(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkPreScreening", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getPreScreeningCat()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getPreScreeningCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getPreScreeningByCat(ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getPreScreeningByCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 500).Value = cat
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getPreScreening()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getPreScreening", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getPreScreeningExists(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getPreScreeningExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getPreScreeningDetail(ByVal field)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getPreScreeningDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@field", SqlDbType.VarChar, 50).Value = field
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getPreScreeningDetailExists(ByVal brwcode, ByVal field)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getPreScreeningDetailExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@field", SqlDbType.VarChar, 50).Value = field
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdatePreScreening(ByVal appno, ByVal brwcode, ByVal brwtype, ByVal score, ByVal decision, ByVal reason, ByVal usr)
        Dim apr As Boolean
        If decision = 1 Then
            apr = True
        Else
            apr = False
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUpdatePreScreening", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        cmdA.Parameters.AddWithValue("@brwtype", brwtype)
        cmdA.Parameters.AddWithValue("@score", score)
        cmdA.Parameters.AddWithValue("@decision", apr)
        cmdA.Parameters.AddWithValue("@reason", reason)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertUpdatePreScreeningDetail(ByVal brwcode, ByVal field, ByVal value, ByVal weight, ByVal usr)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUpdatePreScreeningDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        cmdA.Parameters.Add("@field", SqlDbType.VarChar, 50).Value = field
        cmdA.Parameters.Add("@value", SqlDbType.VarChar, 500).Value = value
        cmdA.Parameters.Add("@weight", SqlDbType.Int).Value = weight
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '================End Tambahan 11 Nov 2016 ===================

    '================Tambahan 14 Nov 2016 ===================
    Function checkDocumentChecking(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkDocumentChecking", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDocumentCheckingCat()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDocumentCheckingCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDocumentChecking()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDocumentChecking", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDocumentCheckingExists(ByVal appno, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDocumentCheckingExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDocumentCheckingByCat(ByVal cat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getDocumentCheckingByCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat", SqlDbType.VarChar, 500).Value = cat
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdateDocumentChecking(ByVal id, ByVal appno, ByVal name, ByVal value, ByVal desc, ByVal usr)
        Dim val As Boolean
        If value = "on" Then
            val = True
        Else
            val = False
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertUpdateDocumentChecking", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@name", SqlDbType.VarChar, 500).Value = name
        cmdA.Parameters.Add("@value", SqlDbType.Bit).Value = val
        cmdA.Parameters.Add("@desc", SqlDbType.Text).Value = desc
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getAspekOperasional()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getListOfAspekOperasional", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    '================End Tambahan 14 Nov 2016 ===================

    '================Tambahan 18 Nov 2016 ===================
    Function checkMemoBisnis(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkMemoBusiness", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertMemoBisnis(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertMemoBusiness", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@aspekMgmt", SqlDbType.Text).Value = e.Form(master & "txtAspekUmumMgmt")
        cmdA.Parameters.Add("@hubunganBank", SqlDbType.Text).Value = e.Form(master & "txtHubunganBank")
        cmdA.Parameters.Add("@aspekPemasaran", SqlDbType.Text).Value = e.Form(master & "txtAspekPemasaran")
        cmdA.Parameters.Add("@aspekTeknis", SqlDbType.Text).Value = e.Form(master & "txtAspekTeknisProduksiPembelian")
        cmdA.Parameters.Add("@aspekKeuangan", SqlDbType.Text).Value = e.Form(master & "txtAspekKeuangan")
        cmdA.Parameters.Add("@potensiKebutuhanProduk", SqlDbType.Text).Value = e.Form(master & "txtPotensiKebutuhanProduk")
        cmdA.Parameters.Add("@trackRecord", SqlDbType.Text).Value = e.Form(master & "txtTrackRecordPerusahaanDanPemilik")
        cmdA.Parameters.Add("@kebutuhanKredit", SqlDbType.Text).Value = e.Form(master & "txtKebutuhanKredit")
        cmdA.Parameters.Add("@jaminan", SqlDbType.Text).Value = e.Form(master & "txtJaminan")
        cmdA.Parameters.Add("@kesimpulan", SqlDbType.Text).Value = e.Form(master & "txtKesimpulan")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateMemoBisnis(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateMemoBusiness", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@aspekMgmt", SqlDbType.Text).Value = e.Form(master & "txtAspekUmumMgmt")
        cmdA.Parameters.Add("@hubunganBank", SqlDbType.Text).Value = e.Form(master & "txtHubunganBank")
        cmdA.Parameters.Add("@aspekPemasaran", SqlDbType.Text).Value = e.Form(master & "txtAspekPemasaran")
        cmdA.Parameters.Add("@aspekTeknis", SqlDbType.Text).Value = e.Form(master & "txtAspekTeknisProduksiPembelian")
        cmdA.Parameters.Add("@aspekKeuangan", SqlDbType.Text).Value = e.Form(master & "txtAspekKeuangan")
        cmdA.Parameters.Add("@potensiKebutuhanProduk", SqlDbType.Text).Value = e.Form(master & "txtPotensiKebutuhanProduk")
        cmdA.Parameters.Add("@trackRecord", SqlDbType.Text).Value = e.Form(master & "txtTrackRecordPerusahaanDanPemilik")
        cmdA.Parameters.Add("@kebutuhanKredit", SqlDbType.Text).Value = e.Form(master & "txtKebutuhanKredit")
        cmdA.Parameters.Add("@jaminan", SqlDbType.Text).Value = e.Form(master & "txtJaminan")
        cmdA.Parameters.Add("@kesimpulan", SqlDbType.Text).Value = e.Form(master & "txtKesimpulan")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkMemoAnalyze(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkMemoAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertMemoAnalyze(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertMemoAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@aspekMgmt", SqlDbType.Text).Value = e.Form(master & "txtAspekUmumMgmt")
        cmdA.Parameters.Add("@hubunganBank", SqlDbType.Text).Value = e.Form(master & "txtHubunganBank")
        cmdA.Parameters.Add("@aspekPemasaran", SqlDbType.Text).Value = e.Form(master & "txtAspekPemasaran")
        cmdA.Parameters.Add("@aspekTeknis", SqlDbType.Text).Value = e.Form(master & "txtAspekTeknisProduksiPembelian")
        cmdA.Parameters.Add("@aspekKeuangan", SqlDbType.Text).Value = e.Form(master & "txtAspekKeuangan")
        cmdA.Parameters.Add("@kelemahanRisiko", SqlDbType.Text).Value = e.Form(master & "txtKelemahanDanRisiko")
        cmdA.Parameters.Add("@analisaKebutuhanKredit", SqlDbType.Text).Value = e.Form(master & "txtAnalisaKebutuhanKredit")
        cmdA.Parameters.Add("@evaluasiJaminan", SqlDbType.Text).Value = e.Form(master & "txtEvaluasiJaminan")
        cmdA.Parameters.Add("@kesimpulan", SqlDbType.Text).Value = e.Form(master & "txtKesimpulan")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateMemoAnalyze(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateMemoAnalyze", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@aspekMgmt", SqlDbType.Text).Value = e.Form(master & "txtAspekUmumMgmt")
        cmdA.Parameters.Add("@hubunganBank", SqlDbType.Text).Value = e.Form(master & "txtHubunganBank")
        cmdA.Parameters.Add("@aspekPemasaran", SqlDbType.Text).Value = e.Form(master & "txtAspekPemasaran")
        cmdA.Parameters.Add("@aspekTeknis", SqlDbType.Text).Value = e.Form(master & "txtAspekTeknisProduksiPembelian")
        cmdA.Parameters.Add("@aspekKeuangan", SqlDbType.Text).Value = e.Form(master & "txtAspekKeuangan")
        cmdA.Parameters.Add("@kelemahanRisiko", SqlDbType.Text).Value = e.Form(master & "txtKelemahanDanRisiko")
        cmdA.Parameters.Add("@analisaKebutuhanKredit", SqlDbType.Text).Value = e.Form(master & "txtAnalisaKebutuhanKredit")
        cmdA.Parameters.Add("@evaluasiJaminan", SqlDbType.Text).Value = e.Form(master & "txtEvaluasiJaminan")
        cmdA.Parameters.Add("@kesimpulan", SqlDbType.Text).Value = e.Form(master & "txtKesimpulan")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '================End Tambahan 18 Nov 2016 ===================

    '================Tambahan 21 Nov 2016 ===================
    Function checkWorksheet(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkWorksheet", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertWorksheetConstruction(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertWorksheetConstruction", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@rencanaProyek", SqlDbType.Decimal).Value = e.Form(master & "txtRencanaProyek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@akseptasiBank", SqlDbType.Float).Value = e.Form(master & "txtAkseptasiBank")
        cmdA.Parameters.Add("@nilaiProyekDiaksep", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekDiaksep").ToString.Replace(",", "")
        cmdA.Parameters.Add("@laba", SqlDbType.Float).Value = e.Form(master & "txtLaba")
        cmdA.Parameters.Add("@pajak", SqlDbType.Float).Value = e.Form(master & "txtPajak")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.Float).Value = e.Form(master & "txtUangMukaPercent")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.Decimal).Value = e.Form(master & "txtUangMuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@jenisKontrak", SqlDbType.VarChar, 50).Value = e.Form(master & "rdJenisKontrak")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@projectCost", SqlDbType.Decimal).Value = e.Form(master & "txtProjectCost").ToString.Replace(",", "")
        cmdA.Parameters.Add("@nilaiProyekBersih", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekBersih").ToString.Replace(",", "")

        cmdA.Parameters.Add("@maxKebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainField1", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField1")
        cmdA.Parameters.Add("@kmkBankLainField2", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField2")
        cmdA.Parameters.Add("@kmkBankLainField3", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField3")
        cmdA.Parameters.Add("@kmkBankLainField4", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField4")
        cmdA.Parameters.Add("@kmkBankLainField5", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField5")
        cmdA.Parameters.Add("@kmkBankLainValue1", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue2", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue3", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue4", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue5", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", "")

        cmdA.Parameters.Add("@SGA", SqlDbType.Decimal).Value = e.Form(master & "txtSGA")
        cmdA.Parameters.Add("@maxPembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxPembiayaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMKPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertWorksheetConstructionMP(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertWorksheetConstruction", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@rencanaProyek", SqlDbType.Decimal).Value = e.Form(master & "txtRencanaProyek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@akseptasiBank", SqlDbType.Float).Value = e.Form(master & "txtAkseptasiBank")
        cmdA.Parameters.Add("@nilaiProyekDiaksep", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekDiaksep").ToString.Replace(",", "")
        cmdA.Parameters.Add("@laba", SqlDbType.Float).Value = e.Form(master & "txtLaba")
        cmdA.Parameters.Add("@pajak", SqlDbType.Float).Value = e.Form(master & "txtPajak")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.Float).Value = e.Form(master & "txtUangMukaPercent")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.Decimal).Value = e.Form(master & "txtUangMuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@jenisKontrak", SqlDbType.VarChar, 50).Value = e.Form(master & "rdJenisKontrak")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@projectCost", SqlDbType.Decimal).Value = e.Form(master & "txtProjectCost").ToString.Replace(",", "")
        cmdA.Parameters.Add("@nilaiProyekBersih", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekBersih").ToString.Replace(",", "")

        cmdA.Parameters.Add("@maxKebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainField1", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField1")
        cmdA.Parameters.Add("@kmkBankLainField2", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField2")
        cmdA.Parameters.Add("@kmkBankLainField3", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField3")
        cmdA.Parameters.Add("@kmkBankLainField4", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField4")
        cmdA.Parameters.Add("@kmkBankLainField5", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField5")
        cmdA.Parameters.Add("@kmkBankLainValue1", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue2", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue3", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue4", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue5", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", "")

        cmdA.Parameters.Add("@SGA", SqlDbType.Decimal).Value = e.Form(master & "textSGA")
        cmdA.Parameters.Add("@maxPembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxPembiayaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMKPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorksheetConstruction(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateWorksheetConstruction", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@rencanaProyek", SqlDbType.Decimal).Value = e.Form(master & "txtRencanaProyek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@akseptasiBank", SqlDbType.Float).Value = e.Form(master & "txtAkseptasiBank")
        cmdA.Parameters.Add("@nilaiProyekDiaksep", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekDiaksep").ToString.Replace(",", "")
        cmdA.Parameters.Add("@laba", SqlDbType.Float).Value = e.Form(master & "txtLaba")
        cmdA.Parameters.Add("@pajak", SqlDbType.Float).Value = e.Form(master & "txtPajak")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.Float).Value = e.Form(master & "txtUangMukaPercent")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.Decimal).Value = e.Form(master & "txtUangMuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@jenisKontrak", SqlDbType.VarChar, 50).Value = e.Form(master & "rdJenisKontrak")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@projectCost", SqlDbType.Decimal).Value = e.Form(master & "txtProjectCost").ToString.Replace(",", "")
        cmdA.Parameters.Add("@nilaiProyekBersih", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekBersih").ToString.Replace(",", "")

        cmdA.Parameters.Add("@maxKebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainField1", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField1")
        cmdA.Parameters.Add("@kmkBankLainField2", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField2")
        cmdA.Parameters.Add("@kmkBankLainField3", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField3")
        cmdA.Parameters.Add("@kmkBankLainField4", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField4")
        cmdA.Parameters.Add("@kmkBankLainField5", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField5")
        cmdA.Parameters.Add("@kmkBankLainValue1", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue2", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue3", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue4", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue5", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", "")

        cmdA.Parameters.Add("@SGA", SqlDbType.Decimal).Value = e.Form(master & "txtSGA").ToString.Replace(",", "")
        cmdA.Parameters.Add("@maxPembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxPembiayaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMKPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorksheetConstructionMP(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateWorksheetConstruction", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@rencanaProyek", SqlDbType.Decimal).Value = e.Form(master & "txtRencanaProyek").ToString.Replace(",", "")
        cmdA.Parameters.Add("@akseptasiBank", SqlDbType.Float).Value = e.Form(master & "txtAkseptasiBank")
        cmdA.Parameters.Add("@nilaiProyekDiaksep", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekDiaksep").ToString.Replace(",", "")
        cmdA.Parameters.Add("@laba", SqlDbType.Float).Value = e.Form(master & "txtLaba")
        cmdA.Parameters.Add("@pajak", SqlDbType.Float).Value = e.Form(master & "txtPajak")
        cmdA.Parameters.Add("@uangMukaPersen", SqlDbType.Float).Value = e.Form(master & "txtUangMukaPercent")
        cmdA.Parameters.Add("@uangMuka", SqlDbType.Decimal).Value = e.Form(master & "txtUangMuka").ToString.Replace(",", "")
        cmdA.Parameters.Add("@jenisKontrak", SqlDbType.VarChar, 50).Value = e.Form(master & "rdJenisKontrak")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@projectCost", SqlDbType.Decimal).Value = e.Form(master & "txtProjectCost").ToString.Replace(",", "")
        cmdA.Parameters.Add("@nilaiProyekBersih", SqlDbType.Decimal).Value = e.Form(master & "txtNilaiProyekBersih").ToString.Replace(",", "")

        cmdA.Parameters.Add("@maxKebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainField1", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField1")
        cmdA.Parameters.Add("@kmkBankLainField2", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField2")
        cmdA.Parameters.Add("@kmkBankLainField3", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField3")
        cmdA.Parameters.Add("@kmkBankLainField4", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField4")
        cmdA.Parameters.Add("@kmkBankLainField5", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField5")
        cmdA.Parameters.Add("@kmkBankLainValue1", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue2", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue3", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue4", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue5", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", "")

        cmdA.Parameters.Add("@SGA", SqlDbType.Decimal).Value = e.Form(master & "textSGA").ToString.Replace(",", "")
        cmdA.Parameters.Add("@maxPembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxPembiayaan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMKPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaanPRK", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankPRK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertWorksheetManufacturer(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertWorksheetManufacturer", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@COGS", SqlDbType.Decimal).Value = e.Form(master & "txtCOGS").ToString.Replace(",", "")
        cmdA.Parameters.Add("@SGA", SqlDbType.Decimal).Value = e.Form(master & "txtSGA").ToString.Replace(",", "")
        cmdA.Parameters.Add("@Sales", SqlDbType.Decimal).Value = e.Form(master & "txtSales").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ARTO", SqlDbType.Float).Value = e.Form(master & "txtARTO")
        cmdA.Parameters.Add("@ITO", SqlDbType.Float).Value = e.Form(master & "txtITO")
        cmdA.Parameters.Add("@APTO", SqlDbType.Float).Value = e.Form(master & "txtAPTO")
        cmdA.Parameters.Add("@TC", SqlDbType.Float).Value = e.Form(master & "txtTC")
        cmdA.Parameters.Add("@proyeksiKebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtProyeksiKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@maxKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainField1", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField1")
        cmdA.Parameters.Add("@kmkBankLainField2", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField2")
        cmdA.Parameters.Add("@kmkBankLainField3", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField3")
        cmdA.Parameters.Add("@kmkBankLainField4", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField4")
        cmdA.Parameters.Add("@kmkBankLainField5", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField5")
        cmdA.Parameters.Add("@kmkBankLainValue1", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue2", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue3", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue4", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue5", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorksheetManufacturer(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateWorksheetManufacturer", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@COGS", SqlDbType.Decimal).Value = e.Form(master & "txtCOGS").ToString.Replace(",", "")
        cmdA.Parameters.Add("@SGA", SqlDbType.Decimal).Value = e.Form(master & "txtSGA").ToString.Replace(",", "")
        cmdA.Parameters.Add("@Sales", SqlDbType.Decimal).Value = e.Form(master & "txtSales").ToString.Replace(",", "")
        cmdA.Parameters.Add("@ARTO", SqlDbType.Float).Value = e.Form(master & "txtARTO")
        cmdA.Parameters.Add("@ITO", SqlDbType.Float).Value = e.Form(master & "txtITO")
        cmdA.Parameters.Add("@APTO", SqlDbType.Float).Value = e.Form(master & "txtAPTO")
        cmdA.Parameters.Add("@TC", SqlDbType.Float).Value = e.Form(master & "txtTC")
        cmdA.Parameters.Add("@proyeksiKebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtProyeksiKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@maxKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainField1", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField1")
        cmdA.Parameters.Add("@kmkBankLainField2", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField2")
        cmdA.Parameters.Add("@kmkBankLainField3", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField3")
        cmdA.Parameters.Add("@kmkBankLainField4", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField4")
        cmdA.Parameters.Add("@kmkBankLainField5", SqlDbType.VarChar, 1000).Value = e.Form(master & "txtKMKBankLainField5")
        cmdA.Parameters.Add("@kmkBankLainValue1", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue2", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue3", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue4", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kmkBankLainValue5", SqlDbType.Decimal).Value = e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kebutuhanKMK", SqlDbType.Decimal).Value = e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertWorksheetMultifinance(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertWorksheetMultifinance", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@targetBookingPerSales", SqlDbType.Decimal).Value = e.Form(master & "txtTargetBookingToPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@realisasiBookingPerSales", SqlDbType.Decimal).Value = e.Form(master & "txtRealisasiBookingToPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kelonggaranTarikBankLain", SqlDbType.Decimal).Value = e.Form(master & "txtKelonggaranTarikBankLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@rencanaObligasi", SqlDbType.Decimal).Value = e.Form(master & "txtRencanaObligasi").ToString.Replace(",", "")
        cmdA.Parameters.Add("@maxKMK", SqlDbType.Decimal).Value = e.Form(master & "txtTotal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorksheetMultifinance(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateWorksheetMultifinance", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@targetBookingPerSales", SqlDbType.Decimal).Value = e.Form(master & "txtTargetBookingToPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@realisasiBookingPerSales", SqlDbType.Decimal).Value = e.Form(master & "txtRealisasiBookingToPenjualan").ToString.Replace(",", "")
        cmdA.Parameters.Add("@kelonggaranTarikBankLain", SqlDbType.Decimal).Value = e.Form(master & "txtKelonggaranTarikBankLain").ToString.Replace(",", "")
        cmdA.Parameters.Add("@rencanaObligasi", SqlDbType.Decimal).Value = e.Form(master & "txtRencanaObligasi").ToString.Replace(",", "")
        cmdA.Parameters.Add("@maxKMK", SqlDbType.Decimal).Value = e.Form(master & "txtTotal").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertWorksheetCashColl(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertWorksheetCashColl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@deposito", SqlDbType.Decimal).Value = e.Form(master & "txtNominalDeposito").ToString.Replace(",", "")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@maxKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorksheetCashColl(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateWorksheetCashColl", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@deposito", SqlDbType.Decimal).Value = e.Form(master & "txtNominalDeposito").ToString.Replace(",", "")
        cmdA.Parameters.Add("@porsi", SqlDbType.Float).Value = e.Form(master & "txtPorsiPembiayaan")
        cmdA.Parameters.Add("@maxKMK", SqlDbType.Decimal).Value = e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", "")
        cmdA.Parameters.Add("@pembiayaan", SqlDbType.Decimal).Value = e.Form(master & "txtPembiayaanBank").ToString.Replace(",", "")
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    '================End Tambahan 21 Nov 2016 ===================

    '================Tambahan 29 Des 2016 ===================
    Function checkBMPK(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBMPK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertBMPK(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_insertBMPK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@bulan", e.Form(master & "ddlBulan").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@tahun", e.Form(master & "ddlTahun"))
        cmdA.Parameters.AddWithValue("@modal", e.Form(master & "txtModal").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_terkait", e.Form(master & "txtDebiturTerkait").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_pihak_ketiga", e.Form(master & "txtDebiturPihakKetiga").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_pihak_ketiga_grup", e.Form(master & "txtDebiturPihakKetigaGrup").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_bumn", e.Form(master & "txtDebiturBUMN").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@inhouse_limit", e.Form(master & "txtInhousePihakKetiga").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field1", e.Form(master & "txtDebitur1").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value1", e.Form(master & "txtDebiturLimit1").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field2", e.Form(master & "txtDebitur2").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value2", e.Form(master & "txtDebiturLimit2").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field3", e.Form(master & "txtDebitur3").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value3", e.Form(master & "txtDebiturLimit3").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field4", e.Form(master & "txtDebitur4").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value4", e.Form(master & "txtDebiturLimit4").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field5", e.Form(master & "txtDebitur5").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value5", e.Form(master & "txtDebiturLimit5").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@total", e.Form(master & "txtTotalExposureKredit").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@type", e.Form(master & "ddlType"))
        cmdA.Parameters.AddWithValue("@kelonggaran", e.Form(master & "txtKelonggaranBMPK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@keloggaran_inhouse", e.Form(master & "txtKelonggaranInhouse").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@rasio_total_to_bmpk", e.Form(master & "txtProsentaseKreditToBMPK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kesimpulan", e.Form(master & "txtKesimpulan").ToString.Replace(",", ""))
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateBMPK(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_updateBMPK", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@bulan", e.Form(master & "ddlBulan").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@tahun", e.Form(master & "ddlTahun"))
        cmdA.Parameters.AddWithValue("@modal", e.Form(master & "txtModal").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_terkait", e.Form(master & "txtDebiturTerkait").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_pihak_ketiga", e.Form(master & "txtDebiturPihakKetiga").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_pihak_ketiga_grup", e.Form(master & "txtDebiturPihakKetigaGrup").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@debitur_bumn", e.Form(master & "txtDebiturBUMN").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@inhouse_limit", e.Form(master & "txtInhousePihakKetiga").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field1", e.Form(master & "txtDebitur1").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value1", e.Form(master & "txtDebiturLimit1").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field2", e.Form(master & "txtDebitur2").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value2", e.Form(master & "txtDebiturLimit2").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field3", e.Form(master & "txtDebitur3").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value3", e.Form(master & "txtDebiturLimit3").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field4", e.Form(master & "txtDebitur4").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value4", e.Form(master & "txtDebiturLimit4").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_field5", e.Form(master & "txtDebitur5").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@limit_value5", e.Form(master & "txtDebiturLimit5").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@total", e.Form(master & "txtTotalExposureKredit").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@type", e.Form(master & "ddlType"))
        cmdA.Parameters.AddWithValue("@kelonggaran", e.Form(master & "txtKelonggaranBMPK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@keloggaran_inhouse", e.Form(master & "txtKelonggaranInhouse").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@rasio_total_to_bmpk", e.Form(master & "txtProsentaseKreditToBMPK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kesimpulan", e.Form(master & "txtKesimpulan").ToString.Replace(",", ""))
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkAppRating(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkBrwRating(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkBrwRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkRecommendationHistory(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkRecommendationHistory", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkRecommendationHistoryFile(ByVal appno, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkRecommendationHistoryFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@proses", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkFinalHistory(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkFinalHistory", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkFinalHistoryFile(ByVal appno, ByVal proses)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkFinalHistoryFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.Parameters.Add("@proses", SqlDbType.Int).Value = proses
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkNotes(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkNotes", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    '================End Tambahan 29 Des 2016 ===================

    Function checkTotalSaham(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkTotalSaham", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function checkCifNo(ByVal appno, ByVal cifno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkCifNo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@cifno", cifno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkAcctNo(ByVal appno, ByVal acctno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkAcctNo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@acctno", acctno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfAppWithRatingDate()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getListAppWithRatingDateAll", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListOfAppWithRatingDateByUsrLogin(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getListAppWithRatingDateByUsrLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userlogin", usr)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertUpdateWorksheetKI(ByVal e, ByVal appno, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_iuWorksheetKI", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.Parameters.AddWithValue("@nilaiProyek", e.Form(master & "txtNilaiProyek").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@porsiPembiayaan", e.Form(master & "txtPorsiPembiayaan").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@maxKMK", e.Form(master & "txtMaxKebutuhanKMK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kmkBankLainField1", e.Form(master & "txtKMKBankLainField1"))
        cmdA.Parameters.AddWithValue("@kmkBankLainField2", e.Form(master & "txtKMKBankLainField2"))
        cmdA.Parameters.AddWithValue("@kmkBankLainField3", e.Form(master & "txtKMKBankLainField3"))
        cmdA.Parameters.AddWithValue("@kmkBankLainField4", e.Form(master & "txtKMKBankLainField4"))
        cmdA.Parameters.AddWithValue("@kmkBankLainField5", e.Form(master & "txtKMKBankLainField5"))
        cmdA.Parameters.AddWithValue("@kmkBankLainValue1", e.Form(master & "txtKMKBankLainValue1").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kmkBankLainValue2", e.Form(master & "txtKMKBankLainValue2").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kmkBankLainValue3", e.Form(master & "txtKMKBankLainValue3").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kmkBankLainValue4", e.Form(master & "txtKMKBankLainValue4").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kmkBankLainValue5", e.Form(master & "txtKMKBankLainValue5").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@kebutuhanKMK", e.Form(master & "txtKebutuhanFasilitasKMK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@pembiayaan", e.Form(master & "txtPembiayaanBankKMK").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getStatusProc_App(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("get_ApprovaleStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getLastWorkflowUsr(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_checkAppWorkFlowUsr", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@noApp", SqlDbType.VarChar, 50).Value = appno
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function checkMultiProduct(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("app_getsector_byappno", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

End Class

