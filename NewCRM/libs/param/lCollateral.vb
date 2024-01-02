Imports System.Data.SqlClient

Public Class lCollateral
    Function getDataCollateral()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_getDataCollateral", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getCollateralClass()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_getCollateralClass", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getCollateralSource()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_getCollateralSource", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailCollateral(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_getDetailCollateral", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function checkCollateralType(ByVal name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_checkCollateralType", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@name", SqlDbType.VarChar, 1000).Value = name
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertCollateral(ByVal e, ByVal usr)
        Dim stat As Boolean
        If e.form("chkStatus") = "on" Then
            stat = True
        Else
            stat = False
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_insertCollateral", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = e.form("txtCollType")
        cmda.Parameters.Add("@class", SqlDbType.VarChar, 50).Value = e.form("ddlCollClass")
        cmda.Parameters.Add("@desc", SqlDbType.Text).Value = e.form("txtCollDesc")
        cmda.Parameters.Add("@source", SqlDbType.VarChar, 50).Value = e.form("ddlCollSource")
        cmda.Parameters.Add("@status", SqlDbType.Bit).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub editCollateral(ByVal e, ByVal usr, ByVal id)
        Dim stat As Boolean
        If e.form("chkStatus") = "on" Then
            stat = True
        Else
            stat = False
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_editCollateral", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = e.form("txtCollType")
        cmda.Parameters.Add("@class", SqlDbType.VarChar, 50).Value = e.form("ddlCollClass")
        cmda.Parameters.Add("@desc", SqlDbType.Text).Value = e.form("txtCollDesc")
        cmda.Parameters.Add("@source", SqlDbType.VarChar, 50).Value = e.form("ddlCollSource")
        cmda.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteCollateral(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("col_deleteCollateral", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
