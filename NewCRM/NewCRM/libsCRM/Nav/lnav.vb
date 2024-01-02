Imports System.Data.SqlClient

Public Class lnav
    Public Function getParent()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("mnu_dync_nav", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@mnu_parentID", SqlDbType.VarChar).Value = 0
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getChild(ByVal parentID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("mnu_dync_nav", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@mnu_parentID", SqlDbType.VarChar).Value = parentID
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getChildSub(ByVal childId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("mnu_dync_nav", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@mnu_parentID", SqlDbType.VarChar).Value = childId
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function


   

End Class
