Imports System.Data.SqlClient

Public Class lUser
    Public Function getDataSetUser()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getAllUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Public Function userFailedLogin(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_userFailedLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getFailedLogin(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getFailedLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function InsertDayOff(ByVal code, ByVal AD, ByVal reason, ByVal desc, ByVal user, ByVal AD2)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_insertDayOff", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 20)
        cmdA.Parameters("@code").Value = code
        cmdA.Parameters.Add("@absent_date", SqlDbType.Date)
        cmdA.Parameters("@absent_date").Value = AD
        cmdA.Parameters.Add("@reason", SqlDbType.VarChar, 20)
        cmdA.Parameters("@reason").Value = reason
        cmdA.Parameters.Add("@description", SqlDbType.VarChar)
        cmdA.Parameters("@description").Value = desc
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = user
        cmdA.Parameters.Add("@absent_date2", SqlDbType.Date)
        cmdA.Parameters("@absent_date2").Value = AD2
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    'Public Function ReplaceDayOff(ByVal code, ByVal AD, ByVal reason, ByVal desc, ByVal user)
    '    Dim myDb As New dbConn
    '    Dim myConn As SqlConnection
    '    myConn = myDb.getConStr
    '    Dim cmdA As New SqlCommand("sp_insertDayOff", myConn)
    '    cmdA.CommandType = CommandType.StoredProcedure
    '    cmdA.Parameters.Add("@code", SqlDbType.VarChar, 20)
    '    cmdA.Parameters("@code").Value = code
    '    cmdA.Parameters.Add("@absent_date", SqlDbType.Date)
    '    cmdA.Parameters("@absent_date").Value = AD
    '    cmdA.Parameters.Add("@reason", SqlDbType.VarChar, 20)
    '    cmdA.Parameters("@reason").Value = reason
    '    cmdA.Parameters.Add("@description", SqlDbType.VarChar)
    '    cmdA.Parameters("@description").Value = desc
    '    cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
    '    cmdA.Parameters("@usr").Value = user
    '    Dim sda As New SqlDataAdapter(cmdA)
    '    Dim ds As New DataSet
    '    sda.Fill(ds)
    '    myConn.Close()
    '    Return ds
    'End Function

    Public Function LoadDayOffGrid(ByVal user)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sp_getDayOffListTableGrid", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = user
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function userIsLogin(ByVal usr, ByVal ip_address)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_userIsLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        cmdA.Parameters.Add("@ip_address", SqlDbType.VarChar, 50)
        cmdA.Parameters("@ip_address").Value = ip_address
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
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
        Dim cmdA As New SqlCommand("us_cekUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function chkLock(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_chkLock", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr_id", SqlDbType.VarChar, 255)
        cmdA.Parameters("@usr_id").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function updateLastAccess(ByVal usr)
        If Not usr = "" Then
            Dim myDb As New dbConn
            Dim myConn As SqlConnection
            myConn = myDb.getConStr
            Dim cmdA As New SqlCommand("sp_updateLastAccess", myConn)
            cmdA.CommandType = CommandType.StoredProcedure
            cmdA.Parameters.Add("@parUsersID", SqlDbType.VarChar, 16)
            cmdA.Parameters("@parUsersID").Value = usr
            myConn.Open()
            cmdA.ExecuteNonQuery()
            myConn.Close()
        End If
        Return True
    End Function

    Public Function getAccessLevel()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("prm_access_level", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

  

    Public Function getUserLevelDetail(ByVal usrid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_getUserLevelDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr_id", SqlDbType.VarChar).Value = usrid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function ValidateUser(ByVal usrId, ByVal mode) As Integer
        If mode = "new" Then
            Dim lngh As Integer = usrId.Length
            If lngh < 3 Or lngh > 15 Then
                Return 2
            End If
            Dim jUsr As Integer = checkStatAdmin(usrId, "name")
            If jUsr = 1 Then
                Return 3
            End If
        Else
            'Dim xUsr As Boolean = checkStatAdmin(usrId, "login") '209-212 yang menyebabkan error waktu edit. sp_checkstatadmin returnya 0.
            'If xUsr = True Then
            '    Return 1
            'End If
        End If
    End Function

    Function checkStatAdmin(ByVal usr, ByVal mode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim tOut As String = myDb.getTimeOut()
        Dim cmdA As New SqlCommand("sp_checkstatadmin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        cmdA.Parameters.Add("@mode", SqlDbType.VarChar, 16)
        cmdA.Parameters("@mode").Value = mode
        cmdA.Parameters.Add("@buffTime", SqlDbType.Int)
        cmdA.Parameters("@buffTime").Value = tOut
        cmdA.Connection = myConn
        Dim rdr As SqlDataReader
        rdr = cmdA.ExecuteReader(CommandBehavior.CloseConnection)
        'Dim i As Integer
        Dim strRtn As String = ""
        While rdr.Read()
            strRtn = rdr.Item(rdr.GetOrdinal("stat"))
        End While
        myConn.Close()
        Return strRtn
    End Function

    Public Function chkActive(ByVal usrid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sp_chkActive", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr_id", SqlDbType.VarChar).Value = usrid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getNotes(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getNotes", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 16)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getUserReferenceUpdate()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getUserReferenceUpdate", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item("last_update")
    End Function

    Public Function getUserReference()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getUserReference", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Public Function insertWSTracer(ByVal passKey, ByVal npk, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_insWSTracer", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@passKey", SqlDbType.VarChar, 30)
        cmdA.Parameters("@passKey").Value = passKey
        cmdA.Parameters.Add("@NPK", SqlDbType.VarChar, 8)
        cmdA.Parameters("@NPK").Value = npk
        cmdA.Parameters.Add("@status", SqlDbType.VarChar, 10)
        cmdA.Parameters("@status").Value = status
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

End Class
