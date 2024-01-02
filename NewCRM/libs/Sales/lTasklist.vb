Imports System.Data.SqlClient

Public Class lTasklist
    Public Function getAllTasklist(userID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("tsk_getAllTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        cmdA.Parameters.AddWithValue("@userid", "crm_ao02")
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Sub updateTskCourtesyDetail(ByVal TskID, ByVal Req, ByVal Reason, ByVal Notes, ByVal ExtUntil, ByVal UserID, ByVal NewID, ByVal check)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_updateTskCourtesyDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tskid", SqlDbType.VarChar, 50).Value = TskID
        cmdA.Parameters.Add("@req", SqlDbType.VarChar, 50).Value = Req
        cmdA.Parameters.Add("@reason", SqlDbType.VarChar, 100).Value = Reason
        cmdA.Parameters.Add("@notes", SqlDbType.VarChar, 1000).Value = Notes
        cmdA.Parameters.Add("@until", SqlDbType.VarChar, 10).Value = ExtUntil
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = UserID
        cmdA.Parameters.Add("@newid", SqlDbType.VarChar, 20).Value = NewID
        cmdA.Parameters.Add("@check", SqlDbType.Bit).Value = check
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function checkNPKName(ByVal Npk, ByVal UserId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getNpkName", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@npk", SqlDbType.VarChar, 20).Value = Npk
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = UserId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getApprovalList(ByVal UserId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getApprovalList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = UserId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getApprovalDetail(ByVal TcdId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getApprovalDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tcdid", SqlDbType.VarChar, 20).Value = TcdId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Sub updateApprovalStatus(ByVal TcdId, ByVal Stat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_updateApprovalStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tcdid", SqlDbType.BigInt).Value = TcdId
        cmdA.Parameters.Add("@stat", SqlDbType.BigInt).Value = Stat
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Sub updateAppApprove(ByVal TcdId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_updateAppApprove", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tcdid", SqlDbType.BigInt).Value = TcdId
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Public Function getListOfLeadStatus(ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getLeadStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar, 50).Value = userid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getDetailTasklist(ByVal tskID, ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getTasklistDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@tsk_id", tskID)
        cmdA.Parameters.AddWithValue("@usr_view", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0)
    End Function
    Public Function getUserStatus(ByVal UserId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getUserStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = UserId
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getCountUploadMemo(ByVal tskID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getCountUploadMemo", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tskID", SqlDbType.BigInt).Value = tskID
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Public Function getDetailTasklistLog(ByVal tskID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getTasklistlogDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@tsk_id", tskID)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getTasklistActivity(ByVal tskID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getTasklistActivity", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@tskID", tskID)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Sub updateTasklist(ByVal tskID, ByVal notes, ByVal activity, ByVal usr, ByVal backDate, ByVal followupDate, ByVal amount, ByVal memo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tsk_id", SqlDbType.BigInt).Value = tskID
        cmdA.Parameters.Add("@tsk_notes", SqlDbType.VarChar, 1000).Value = notes
        cmdA.Parameters.Add("@tsk_act", SqlDbType.VarChar, 50).Value = activity
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@update_date", SqlDbType.VarChar, 50).Value = backDate
        cmdA.Parameters.Add("@followup_date", SqlDbType.VarChar, 50).Value = followupDate
        cmdA.Parameters.Add("@amount", SqlDbType.VarChar, 50).Value = amount
        cmdA.Parameters.Add("@memo", SqlDbType.VarChar, 100).Value = memo
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Public Function checkStatusExtendTime(ByVal tsk_id)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_checkStatusExtendTime", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tskid", SqlDbType.BigInt).Value = tsk_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function
    Public Function getDetailExtendTime(ByVal TskID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("tsk_getDetailTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tskid", SqlDbType.VarChar, 50).Value = TskID
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
