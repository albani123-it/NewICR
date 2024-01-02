Imports System.Data.SqlClient
Public Class lOffer1
    Function LoadGridData()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getOfferTableGrid", myConn)
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

    Sub DeleteOffer(ByVal id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_deleteOffering", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.NVarChar).Value = id
        myComm.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub

    Sub InsertOffer(ByVal offercode, ByVal offertxt, ByVal category, ByVal prodak, ByVal group, ByVal subgroup, ByVal dashboard, ByVal usrLogin, ByVal sla, ByVal type, ByVal prodakpartner, ByVal activgroup)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim dateinsert As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim cmdA As New SqlCommand("sp_insertOffering", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@OfferCode", SqlDbType.NVarChar).Value = offercode
        cmdA.Parameters.Add("@Category", SqlDbType.NVarChar).Value = category
        cmdA.Parameters.Add("@Product", SqlDbType.NVarChar).Value = prodak
        cmdA.Parameters.Add("@Offer", SqlDbType.NVarChar).Value = offertxt
        cmdA.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateinsert
        cmdA.Parameters.Add("@isDelete", SqlDbType.Int).Value = 0
        cmdA.Parameters.Add("@group", SqlDbType.VarChar, 100).Value = group
        cmdA.Parameters.Add("@subgroup", SqlDbType.VarChar, 100).Value = subgroup
        cmdA.Parameters.Add("@dashboard", SqlDbType.VarChar, 100).Value = dashboard
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@SLA", SqlDbType.NVarChar).Value = sla
        cmdA.Parameters.Add("@Type", SqlDbType.NVarChar).Value = type
        If type = "WB" Then
            cmdA.Parameters.Add("@ProductPartner", SqlDbType.VarChar).Value = prodakpartner
        Else
            cmdA.Parameters.Add("@ProductPartner", SqlDbType.VarChar).Value = ""
        End If
        cmdA.Parameters.Add("@ActivityGroup", SqlDbType.VarChar).Value = activgroup
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Sub UpdateOffer(ByVal id, ByVal usrLogin, ByVal offercode, ByVal offertxt, ByVal category, ByVal prodak, ByVal group, ByVal subgroup, ByVal dashboard, ByVal sla, ByVal type, ByVal prodakpartner, ByVal activgroup)

        Dim dateupdate As Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_updateOffering", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@OfferCode", SqlDbType.NVarChar).Value = offercode
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@Category", SqlDbType.NVarChar).Value = category
        cmdA.Parameters.Add("@Product", SqlDbType.NVarChar).Value = prodak
        cmdA.Parameters.Add("@Offer", SqlDbType.NVarChar).Value = offertxt
        cmdA.Parameters.Add("@lastUpdateDate", SqlDbType.DateTime).Value = dateupdate
        cmdA.Parameters.Add("@group", SqlDbType.VarChar, 100).Value = group
        cmdA.Parameters.Add("@subgroup", SqlDbType.VarChar, 100).Value = subgroup
        cmdA.Parameters.Add("@dashboard", SqlDbType.VarChar, 100).Value = dashboard
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.Add("@SLA", SqlDbType.NVarChar).Value = sla
        cmdA.Parameters.Add("@Type", SqlDbType.NVarChar).Value = type
        If type = "WB" Then
            cmdA.Parameters.Add("@ProductPartner", SqlDbType.VarChar).Value = prodakpartner
        Else
            cmdA.Parameters.Add("@ProductPartner", SqlDbType.VarChar).Value = ""
        End If
        cmdA.Parameters.Add("@ActivityGroup", SqlDbType.VarChar).Value = activgroup
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getTypeProduct()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "select * from	(	select distinct prd_type as type, prd_type type_value from global_duration	union	select '--Select One--' prd_type, NULL prd_type	)a	order by a.type_value"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function load_details(ByVal id As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sp_getOfferDetails", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(myComm)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function CheckOfferCode(ByVal offercode) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkOfferingCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@OfferCode", SqlDbType.NVarChar).Value = offercode

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


    Function CheckOfferInCampaign(ByVal offercode) As Integer
        Dim isExists As Integer = 0
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_checkOfferInCampaign", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@OfferCode", SqlDbType.NVarChar).Value = offercode
        ' cmdA.Parameters.Add("@id", SqlDbType.Int).Value = offercode
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

    Public Function getGroup()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("of_getGroup", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function cekType(ByVal types)


        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "select COUNT(*)tRow from global_duration where prd_type='" & types & "'"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getSubGroup()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("of_getSubGroup", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getProductPartner()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("off_getProductPartner", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = a
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getGroupName()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("off_getGroupName", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = a
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function cekProdPartners(ByVal pp)


        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "select COUNT(*)tRow from dtupload_wb_ref_salescd where Product_Type='" & pp & "'"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0).Rows(0).Item(0).ToString
    End Function

    Public Function getDashCategory()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("of_getDashCategory", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
