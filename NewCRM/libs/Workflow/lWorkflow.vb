Imports System.Data.SqlClient

Public Class lWorkflow
    Function getListOfWorkflow()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getListOfWorkflow", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfWorkflow(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getDetailOfWorkflow", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertWorkflow(ByVal e, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim stat As Boolean = False
        If e.form(master & "chkActive") = "on" Then
            stat = True
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_insertWorkflow", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@name", e.form(master & "txtWorkflowName"))
        cmda.Parameters.AddWithValue("@desc", e.form(master & "txtWorkflowDesc"))
        cmda.Parameters.AddWithValue("@stat", stat)
        cmda.Parameters.AddWithValue("@usr", usr)
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorkflow(ByVal e, ByVal id, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        Dim stat As Boolean = False
        If e.form(master & "chkActive") = "on" Then
            stat = True
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_updateWorkflow", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", id)
        cmda.Parameters.AddWithValue("@name", e.form(master & "txtWorkflowName"))
        cmda.Parameters.AddWithValue("@desc", e.form(master & "txtWorkflowDesc"))
        cmda.Parameters.AddWithValue("@stat", stat)
        cmda.Parameters.AddWithValue("@usr", usr)
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListOfWorkflowProcess(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getListOfWorkflowProcess", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@wfid", id)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfWorkflowProcess(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getDetailOfWorkflowProcess", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertWorkflowProcess(ByVal e, ByVal wfid, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_insertWorkflowProcess", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", e.form(master & "txtID"))
        cmda.Parameters.AddWithValue("@name", e.form(master & "txtName"))
        cmda.Parameters.AddWithValue("@wfid", wfid)
        cmda.Parameters.AddWithValue("@usr", usr)
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorkflowProcess(ByVal e, ByVal wfid, ByVal id, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"
        
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_updateWorkflowProcess", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", e.form(master & "txtID"))
        cmda.Parameters.AddWithValue("@name", e.form(master & "txtName"))
        cmda.Parameters.AddWithValue("@wfid", wfid)
        cmda.Parameters.AddWithValue("@usr", usr)
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListOfWorkflowProcessDetail(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getListOfWorkflowProcessDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@wfid", id)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfWorkflowProcessDetail(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getDetailOfWorkflowProcessDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertWorkflowProcessDetail(ByVal e, ByVal wfid, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim var As String = ""
        Dim cond As String = ""
        Dim val As String = ""
        Dim with_cond As Boolean = False
        If e.form(master & "rdCondition") = "1" Then
            var = e.form(master & "txtVar")
            cond = e.form(master & "ddlCond")
            val = e.form(master & "txtValue")
            with_cond = True
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_insertWorkflowProcessDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@wfid", wfid)
        cmda.Parameters.AddWithValue("@process_id", e.form(master & "ddlProcess"))
        cmda.Parameters.AddWithValue("@next_id", e.form(master & "ddlNextAction"))
        cmda.Parameters.AddWithValue("@action", e.form(master & "txtActionName"))
        cmda.Parameters.AddWithValue("@var", var)
        cmda.Parameters.AddWithValue("@cond", cond)
        cmda.Parameters.AddWithValue("@value", val)
        cmda.Parameters.AddWithValue("@with_cond", with_cond)
        cmda.Parameters.AddWithValue("@usr", usr)
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateWorkflowProcessDetail(ByVal e, ByVal wfid, ByVal id, ByVal usr)
        Dim master As String = "ctl00$ctl00$ctl00$MainContent$ContentPlaceHolderInSub$MainContent$"

        Dim var As String = ""
        Dim cond As String = ""
        Dim val As String = ""
        Dim with_cond As Boolean = False
        If e.form(master & "rdCondition") = "1" Then
            var = e.form(master & "txtVar")
            cond = e.form(master & "ddlCond")
            val = e.form(master & "txtValue")
            with_cond = True
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_updateWorkflowProcessDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id", id)
        cmda.Parameters.AddWithValue("@wfid", wfid)
        cmda.Parameters.AddWithValue("@process_id", e.form(master & "ddlProcess"))
        cmda.Parameters.AddWithValue("@next_id", e.form(master & "ddlNextAction"))
        cmda.Parameters.AddWithValue("@action", e.form(master & "txtActionName"))
        cmda.Parameters.AddWithValue("@var", var)
        cmda.Parameters.AddWithValue("@cond", cond)
        cmda.Parameters.AddWithValue("@value", val)
        cmda.Parameters.AddWithValue("@with_cond", with_cond)
        cmda.Parameters.AddWithValue("@usr", usr)
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListOfSector()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getListOfSector", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getListOfMenuRoleBySector(ByVal wfid, ByVal wfpid, ByVal sector)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("wf_getListOfMenuRoleBySector", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@wfid", wfid)
        cmda.Parameters.AddWithValue("@wfpid", wfpid)
        cmda.Parameters.AddWithValue("@sector", sector)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
End Class
