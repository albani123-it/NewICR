Imports System.Data.SqlClient
Public Class lcrosssell
    Public Function getproduct(ByVal prod)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "SELECT * FROM offering where category = '" & prod & "' and isDelete = 0"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getFeature(ByVal feat)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "SELECT * FROM offering where category = '" & feat & "' and isDelete = 0 "
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getReward(ByVal rwd)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "SELECT * FROM offering where category = '" & rwd & "' and isDelete = 0"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getListFeature()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "select b.scc_code,a.scc_name from scorecard a,scorecard_schedule b where b.scc_code = a.scc_code and b.scc_last_run is not null order by b.scc_code asc"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)

    End Function

    Public Function getXsellPriority()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "SELECT * FROM crosssell_priority"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)

    End Function

    Public Function insertCrosssell(ByVal type, ByVal check, ByVal prio, ByVal userLogin)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdX As New SqlCommand("sp_insertXsell", myConn)

        cmdX.CommandType = CommandType.StoredProcedure
        cmdX.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = type
        cmdX.Parameters.Add("@check", SqlDbType.VarChar, 50).Value = check
        cmdX.Parameters.Add("@prio", SqlDbType.VarChar, 50).Value = prio
        cmdX.Parameters.Add("@usr", SqlDbType.VarChar).Value = userLogin
        cmdX.ExecuteNonQuery()

        myConn.Close()

        'Dim myDb As New dbConn
        'Dim myConn As SqlConnection

        'myConn = myDb.getConStr
        'myConn.Open()

        'Dim sql As String = "INSERT INTO crosssell_priority(crs_type,crs_name,crs_priority) VALUES ('" & type & "','" & check & "','" & prio & "')"
        'Dim sda As New SqlDataAdapter
        'sda.SelectCommand = New SqlCommand(sql, myConn)
        'Dim dts As New DataSet
        'sda.Fill(dts, "crosssell_priority")

        'myConn.Close()
        'myConn.Dispose()

    End Function

    Public Function getXPrio(ByVal Prod)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "select * from crosssell_priority a left join offering b on a.crs_type = b.OfferCode collate Latin1_General_CI_AS left join scorecard c on a.crs_priority = c.scc_code where b.Category ='" & Prod & "'"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function DelXsellPriority()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim sql As String = "DELETE FROM crosssell_priority"
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = New SqlCommand(sql, myConn)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
    Public Function updateXsell(ByRef nameX, ByVal userLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdX As New SqlCommand("sp_updateCrossSell", myConn)

        cmdX.CommandType = CommandType.StoredProcedure
        cmdX.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = nameX
        cmdX.Parameters.Add("@usr", SqlDbType.VarChar).Value = userLogin
        cmdX.ExecuteNonQuery()

        myConn.Close()
    End Function
End Class
