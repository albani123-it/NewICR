Imports System.Data.SqlClient
Public Class lActList
    Public Function getActivityGroup()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_getactivitygroup", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = a
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getAvailable()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_getavailable", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = a
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub insertGroup(ByVal name, ByVal desc, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_insertGroup", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@name", SqlDbType.VarChar).Value = name.ToString.ToUpper
        cmdA.Parameters.Add("@description", SqlDbType.VarChar).Value = desc
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertGroupDet(ByVal groupid, ByVal val, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_insertGroupDet", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar).Value = groupid
        cmdA.Parameters.Add("@code", SqlDbType.VarChar).Value = val
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getAutoGroupId(ByVal name, ByVal desc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_getautogroupid", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@name", SqlDbType.VarChar).Value = name.ToString.ToUpper
        cmdA.Parameters.Add("@description", SqlDbType.VarChar).Value = desc
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Sub updateGroup(ByVal groupid, ByVal name, ByVal desc, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_updateGroup", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar).Value = groupid
        cmdA.Parameters.Add("@name", SqlDbType.VarChar).Value = name.ToString.ToUpper
        cmdA.Parameters.Add("@description", SqlDbType.VarChar).Value = desc
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteGroupDet(ByVal groupid, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_deleteGroupDet", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar).Value = groupid
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getActivityGroupDet(ByVal groupid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_getactivitygroupdet", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar).Value = groupid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
