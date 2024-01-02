Imports System.Data.SqlClient

Public Class lRule
    Function getRuleData()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_getListOfRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function


    Function getListofPageRule(ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_getListPageRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = tbl
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getTypeRule(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_getTypeRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailRule(ByVal scid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_getDetailOfRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scid", SqlDbType.Int).Value = scid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getRuleName(ByVal name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_chkRuleName", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Sub insertRule(ByVal obj, ByVal id, ByVal userid, ByVal data)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_insertRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_name", SqlDbType.VarChar, 50).Value = obj.name
        cmda.Parameters.Add("@as_description", SqlDbType.VarChar, 1000).Value = obj.description
        cmda.Parameters.Add("@as_sql", SqlDbType.Text).Value = obj.sql
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@as_user", SqlDbType.VarChar, 50).Value = userid
        cmda.Parameters.Add("@as_data", SqlDbType.Int).Value = data
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deletRule(ByVal ID, ByVal UserId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_deleteRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@ID", SqlDbType.BigInt).Value = ID
        cmda.Parameters.Add("@userid", SqlDbType.VarChar, 50).Value = UserId
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListofTextRule(ByVal valz)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_strvar_id", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@par", SqlDbType.VarChar, 50)
        cmda.Parameters("@par").Value = valz
        Dim sda As New SqlDataAdapter(cmda)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getPoolRule(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_getListOfPoolRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getPoolRating(ByVal moid, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rul_getListOfPoolRating", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@moid", SqlDbType.Int).Value = moid
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = typ
        Dim sda As New SqlDataAdapter(cmda)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function
End Class
