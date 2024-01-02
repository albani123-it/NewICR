Imports System.Data.SqlClient

Public Class lUsers
    Public Function getDataSetUser()
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

    Public Function getUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@UserID", usr)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getMenuAll(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getAccessMenu", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@parUserID", usr)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Public Function getUserLevelDetail(ByVal usrid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("usr_getUserLevelDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr_id", usrid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function userIsLogin(ByVal usr, ByVal ip_address)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_userIsLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@ip_address", ip_address)
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
        Dim cmdA As New SqlCommand("usr_userFailedLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
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
        Dim cmdA As New SqlCommand("usr_getFailedLogin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function checkLock(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_chkLock", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr_id", usr)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function insertLoginLog(ByVal usr, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_insertLoginLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@typ", typ)
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function deleteUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_deleteUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function getUserTimeout(ByVal usr, ByVal i)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getUserTimeOut", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@tout", i)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item("timout")
    End Function




    Sub InsertUserRef(ByVal e, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("usr_insertUserRefrence", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", e.form("usr_userid"))
        cmdA.Parameters.AddWithValue("@user_name", e.form("usr_name"))
        cmdA.Parameters.AddWithValue("@branch_code", e.form("usr_branch"))
        cmdA.Parameters.AddWithValue("@bm_user_name", e.form("bm_usr_name"))
        cmdA.Parameters.AddWithValue("@user_status", e.form("position"))
        cmdA.Parameters.AddWithValue("@user_type", e.form("usr_type"))
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getParameterDDL(ByVal type, ByVal userid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getParameterDDL", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@type", type)
        cmdA.Parameters.AddWithValue("@userid", userid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getRegion(ByVal valBranch)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getRegionArea", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@val", valBranch)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function checkUser(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_checkUser", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 20)
        cmdA.Parameters("@usr").Value = usr
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub InsertUser(ByVal userid, ByVal name, ByVal nip, ByVal email, ByVal branch, ByVal supervisor, ByVal position, ByVal accLevel, ByVal efectiveDate, ByVal status, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("usr_insertNewUser", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.Parameters.AddWithValue("@name", name)
        cmdA.Parameters.AddWithValue("@nip", nip)
        cmdA.Parameters.AddWithValue("@email", email)
        cmdA.Parameters.AddWithValue("@branchCode", branch)
        cmdA.Parameters.AddWithValue("@supervisor", supervisor)
        cmdA.Parameters.AddWithValue("@position", position)
        cmdA.Parameters.AddWithValue("@access_level", accLevel)
        cmdA.Parameters.AddWithValue("@efectiveDate", efectiveDate)
        With New lLevel
            cmdA.Parameters.AddWithValue("@status", .validate(status))
        End With
        cmdA.Parameters.AddWithValue("@usr", usrLogin)

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub editUser(ByVal userid, ByVal name, ByVal nip, ByVal email, ByVal branch, ByVal supervisor, ByVal position, ByVal accLevel, ByVal efectiveDate, ByVal status, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("usr_editUser", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.Parameters.AddWithValue("@name", name)
        cmdA.Parameters.AddWithValue("@nip", nip)
        cmdA.Parameters.AddWithValue("@email", email)
        cmdA.Parameters.AddWithValue("@branchCode", branch)
        cmdA.Parameters.AddWithValue("@supervisor", supervisor)
        cmdA.Parameters.AddWithValue("@position", position)
        cmdA.Parameters.AddWithValue("@access_level", accLevel)
        cmdA.Parameters.AddWithValue("@efectiveDate", efectiveDate)
        With New lLevel
            cmdA.Parameters.AddWithValue("@status", .validate(status))
        End With
        cmdA.Parameters.AddWithValue("@usr", usrLogin)

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub UpdateRecordActivate(ByVal e, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("usr_editUserActivate", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", e.form("usr_userid"))
        With New lLevel
            cmdA.Parameters.AddWithValue("@status", .validate(e.form("usr_status")))
        End With
        cmdA.Parameters.AddWithValue("@usr", usrLogin)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

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
        End If
    End Function

    Function checkStatAdmin(ByVal usr, ByVal mode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim tOut As String = myDb.getTimeOut()
        Dim cmdA As New SqlCommand("usr_checkStatAdmin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@mode", mode)
        cmdA.Parameters.AddWithValue("@buffTime", tOut)
        cmdA.Connection = myConn
        Dim rdr As SqlDataReader
        rdr = cmdA.ExecuteReader(CommandBehavior.CloseConnection)
        Dim strRtn As String = ""
        While rdr.Read()
            strRtn = rdr.Item(rdr.GetOrdinal("stat"))
        End While
        myConn.Close()
        Return strRtn
    End Function



    Public Function getOrganization(ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getOrganizationChart", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@UserID", usr)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

End Class
