Imports System.Data.SqlClient

Public Class lwsUsers
    Public Function getUserList()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_getUserList", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function getDataSetWsUser()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getAllUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getWsUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_getUserID", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function deleteWSUser(ByVal uID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("ws_deleteWSUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uws_userid", SqlDbType.VarChar, 20).Value = uID
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function
    Public Function updateWSUser(ByVal uID, ByVal uName, ByVal uDescription, ByVal uStatus, ByVal uAuth)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_updateWSUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uws_userid", SqlDbType.VarChar, 20).Value = uID
        cmdA.Parameters.Add("@uws_name", SqlDbType.VarChar, 100).Value = uName
        cmdA.Parameters.Add("@uws_description", SqlDbType.VarChar, 100).Value = uDescription
        cmdA.Parameters.Add("@uws_status", SqlDbType.Bit).Value = uStatus
        cmdA.Parameters.Add("@uws_authkey", SqlDbType.VarChar, 20).Value = uAuth
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function insertWSUser(ByVal uID, ByVal uName, ByVal uDescription, ByVal uStatus, ByVal uAuth)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_insertWSUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uws_userid", SqlDbType.VarChar, 20).Value = uID
        cmdA.Parameters.Add("@uws_name", SqlDbType.VarChar, 100).Value = uName
        cmdA.Parameters.Add("@uws_description", SqlDbType.VarChar, 100).Value = uDescription
        cmdA.Parameters.Add("@uws_status", SqlDbType.Bit).Value = uStatus
        cmdA.Parameters.Add("@uws_authkey", SqlDbType.VarChar, 20).Value = uAuth
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function insertWSLevel(ByVal uID, ByVal level, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_getInsertWSLevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uID", SqlDbType.VarChar, 20).Value = uID
        cmdA.Parameters.Add("@level", SqlDbType.VarChar, 100).Value = level
        cmdA.Parameters.Add("@code", SqlDbType.Bit).Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function updateWSLevel(ByVal uID, ByVal level, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_getInsertWSLevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uID", SqlDbType.VarChar, 20).Value = uID
        cmdA.Parameters.Add("@level", SqlDbType.VarChar, 100).Value = level
        cmdA.Parameters.Add("@code", SqlDbType.Bit).Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
    Public Function cekUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_checkWSUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 20)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
    Public Function authKey() As String
        Dim time As String
        Dim jam As String = ""
        Dim jamSplit As String = ""
        Dim splitjam() As String
        Dim passKey As String = ""

        time = DateTime.Now.ToString("dd:MM:yy:HH:mm:ss")

        splitjam = time.Split(":")
        For count = 0 To splitjam.Length - 1
            jam = splitjam(count)

            If jamSplit = "" Then
                jamSplit = jam
            Else
                jamSplit = jamSplit & jam
            End If
        Next

        passKey = jamSplit


        Dim s As String = passKey
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 14
            Dim idx As Integer = r.Next(0, 11)
            sb.Append(s.Substring(idx, 1))
        Next
        Dim auth As String = sb.ToString

        Return auth

    End Function
End Class
