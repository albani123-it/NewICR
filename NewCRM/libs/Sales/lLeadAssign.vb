Imports System.Data.SqlClient
Public Class lLeadAssign
    Public Function getLeadAssign(ByVal a)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getLeadAssign", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = a
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getLeadAssignChg(ByVal user, ByVal codecamp, ByVal name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getLeadAssignChg", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = user
        cmdA.Parameters.Add("@campcd", SqlDbType.VarChar).Value = codecamp
        cmdA.Parameters.Add("@campname", SqlDbType.VarChar).Value = name
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getLeadAssignDet(ByVal campcode, ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getLeadAssignDet", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@campcode", SqlDbType.VarChar, 50)
        cmdA.Parameters("@campcode").Value = campcode
        cmdA.Parameters.Add("@userid", SqlDbType.VarChar, 16)
        cmdA.Parameters("@userid").Value = userid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getRMList(ByVal usrid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getRMList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = usrid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getRMListnLeads(ByVal usrid, ByVal campcd)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getRMListnLeads", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = usrid
        cmdA.Parameters.Add("@campcode", SqlDbType.VarChar).Value = campcd
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getLeads(ByVal usrid, ByVal campcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getLeads", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usrid", SqlDbType.VarChar).Value = usrid
        cmdA.Parameters.Add("@campcode", SqlDbType.VarChar).Value = campcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Sub befCommentTasklist(ByVal a, ByVal b, ByVal c, ByVal d)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_befCommentTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@campcode", SqlDbType.VarChar).Value = a
        cmdA.Parameters.Add("@totleads", SqlDbType.Int).Value = b
        cmdA.Parameters.Add("@bm", SqlDbType.VarChar).Value = c
        cmdA.Parameters.Add("@rm", SqlDbType.VarChar).Value = d
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub befCommentTasklistChg(ByVal a, ByVal b, ByVal c, ByVal d, ByVal e)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_befCommentTasklistChg", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@campcode", SqlDbType.VarChar).Value = a
        cmdA.Parameters.Add("@totleads", SqlDbType.Int).Value = b
        cmdA.Parameters.Add("@bm", SqlDbType.VarChar).Value = c
        cmdA.Parameters.Add("@rm", SqlDbType.VarChar).Value = d
        cmdA.Parameters.Add("@newrm", SqlDbType.VarChar).Value = e
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Sub insertLeadAssignHist(ByVal a, ByVal b, ByVal c, ByVal d, ByVal e)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_insertLeadAssignHist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@campcode", SqlDbType.VarChar).Value = a
        cmdA.Parameters.Add("@bm", SqlDbType.VarChar).Value = b
        cmdA.Parameters.Add("@rm", SqlDbType.VarChar).Value = c
        cmdA.Parameters.Add("@totleads", SqlDbType.Int).Value = d
        cmdA.Parameters.Add("@duedate", SqlDbType.VarChar).Value = e
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function getAssignedUser(ByVal id, ByVal rm)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("la_getAssignedUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar, 16)
        cmdA.Parameters("@id").Value = id
        cmdA.Parameters.Add("@rm", SqlDbType.VarChar, 16)
        cmdA.Parameters("@rm").Value = rm
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
