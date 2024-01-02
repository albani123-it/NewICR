Imports System.Data.SqlClient
Public Class lRetentionReason
    Function LoadGridData()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRetReasonTableGrid", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds
    End Function


    Sub DeleteRetReason(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim dateupdate As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim myComm As New SqlCommand("sp_deleteRetReason", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateupdate
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub

    Function LoadAvailableOffering()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getOfferRetention", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds.Tables(0)
    End Function


    Function LoadFinalOffering(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getFinalOfferRetReason", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@ID", SqlDbType.Int).Value = id
        myConn.Open()
        Dim adapter As New SqlDataAdapter()
        Dim ds As New DataSet()
        myComm.ExecuteNonQuery()
        adapter.SelectCommand = myComm
        adapter.Fill(ds)
        adapter.Dispose()
        myComm.Dispose()
        myConn.Close()
        Return ds
    End Function

    Sub InsertRetReason(ByVal code, ByVal name, ByVal status, ByVal finalReason, ByVal usrLogin)
        Dim checked As Integer
        If status <> "" Then
            checked = 1
        Else
            checked = 0
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim dateinsert As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim cmdA As New SqlCommand("sp_insertRetReason", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ReasonCode", SqlDbType.NVarChar).Value = code.ToString().ToUpper
        cmdA.Parameters.Add("@ReasonName", SqlDbType.NVarChar).Value = name
        cmdA.Parameters.Add("@isActive", SqlDbType.Int).Value = checked
        cmdA.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateinsert
        cmdA.Parameters.Add("@isDelete", SqlDbType.Int).Value = 0
        cmdA.Parameters.Add("@finaloffers", SqlDbType.VarChar).Value = finalReason
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub UpdateReason(ByVal name, ByVal status, ByVal id, ByVal finalreason, ByVal usrLogin)
        Dim checked As Integer
        If status <> "Deactivate" Then
            checked = 1
        Else
            checked = 0
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim dateupdate As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim cmdA As New SqlCommand("sp_updateRetReason", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ReasonName", SqlDbType.NVarChar).Value = name
        cmdA.Parameters.Add("@isActive", SqlDbType.Int).Value = checked
        cmdA.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateupdate
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@finaloffers", SqlDbType.VarChar).Value = finalreason
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()

        Dim cmdB As New SqlCommand("sp_deleteFinalOfferRetReason", myConn)
        cmdB.CommandType = CommandType.StoredProcedure
        cmdB.Parameters.Add("@ID", SqlDbType.Int).Value = id
        cmdB.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub InsertRetReasonDetail(ByVal reasoncode, ByVal offercode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_insertRetReasonDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ReasonCode", SqlDbType.NVarChar).Value = reasoncode
        cmdA.Parameters.Add("@OfferCode", SqlDbType.NVarChar).Value = offercode
        cmdA.ExecuteNonQuery()
        cmdA.Parameters.Clear()
        myConn.Close()
    End Sub

    Function CheckReasonCode(ByVal reasoncode) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkRetReasonCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ReasonCode", SqlDbType.NVarChar).Value = reasoncode

        'Create a SqlParameter object to hold the output parameter value
        Dim retValParam As New SqlParameter("@RETURN_VALUE", SqlDbType.Int)

        'IMPORTANT - must set Direction as ReturnValue
        retValParam.Direction = ParameterDirection.ReturnValue

        'Finally, add the parameter to the Command's Parameters collection
        cmdA.Parameters.Add(retValParam)
        Dim reader As SqlDataReader = cmdA.ExecuteReader()
        isExists = Convert.ToInt32(retValParam.Value)
        myConn.Close()
        Return isExists
    End Function

    Function load_details(ByVal id As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getRetReasonDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(myComm)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
