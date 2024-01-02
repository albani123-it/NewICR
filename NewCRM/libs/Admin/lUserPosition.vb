Imports System.Data.SqlClient

Public Class lUserPosition

    Public Function getDataUserPosition()
        Dim myDb As New dbConn
        Dim myConn As New SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usp_getAllUserPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()

        Return ds

    End Function

    Public Function getUserPosition(ByVal value)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usp_getDataUserPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usp_value", value)
        'cmdA.Parameters.AddWithValue("@usr", usr)

        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


    Sub deleteUserPosition(ByVal usp_code, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As New SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("usp_deleteUserPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usp_code", SqlDbType.VarChar).Value = usp_code
        'cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.ExecuteNonQuery()
        myConn.Close()

    End Sub

    Sub editRecord(ByVal txtCode, ByVal txtValue, ByVal txtDesc, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("usp_editUsersPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usp_code", txtCode)
        cmdA.Parameters.AddWithValue("@usp_value", txtValue)
        cmdA.Parameters.AddWithValue("@usp_desc", txtDesc)
        cmdA.Parameters.AddWithValue("@usr", usrLogin)



        cmdA.ExecuteNonQuery()
        myConn.Close()

    End Sub

    Public Function checkUserPosition(ByVal check, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usp_checkUserPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@check", check)
        cmdA.Parameters.AddWithValue("@code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)

    End Function

    Sub InsertRecord(ByVal uspCode, ByVal uspValue, ByVal uspDescription, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("usp_insertUsersPosition", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@uspCode", uspCode)
        cmdA.Parameters.AddWithValue("@uspValue", uspValue)
        cmdA.Parameters.AddWithValue("@uspDescription", uspDescription)
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
        Dim value As Int32
        If mode = "new" Then
            Dim lngh As Integer = usrId.Length
            If lngh < 3 Or lngh > 15 Then
                value = 2
                Return value
            End If
            Dim jUsr As Integer = checkStatAdmin(usrId, "name")
            If jUsr = 1 Then
                value = 3
                Return value
            End If
        End If
        Return value
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
End Class
