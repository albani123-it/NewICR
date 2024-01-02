
Imports System.Data.SqlClient

Public Class dbConn
    Public lFun As New lFunction

    Public Function getConStr()
        Dim constring As String = System.Configuration.ConfigurationManager.AppSettings("ConString")
        Dim source() As String = constring.Split("|")
        Dim SQLConn As SqlConnection = New SqlConnection
        If source(2) = "sa" Then
            SQLConn.ConnectionString = "Data Source=" & source(0) & ";" & _
            "Initial Catalog=" & source(1) & ";" & _
            "User ID=" & source(2) & ";" & _
            "Password=" & lFun.DecryptString(source(3)) & ";" & _
            "Max Pool Size=" & source(4) & ";" & _
            "Connect Timeout=" & source(5) & ""
        Else
            SQLConn.ConnectionString = "Data Source=" & lFun.DecryptString(source(0)) & ";" & _
            "Initial Catalog=" & lFun.DecryptString(source(1)) & ";" & _
            "User ID=" & lFun.DecryptString(source(2)) & ";" & _
            "Password=" & lFun.DecryptString(source(3)) & ";" & _
            "Max Pool Size=" & lFun.DecryptString(source(4)) & ";" & _
            "Connect Timeout=" & lFun.DecryptString(source(5)) & ""
        End If
        Return SQLConn
    End Function

    Public Function getConStr_Analysis()
        Dim constring As String = System.Configuration.ConfigurationManager.AppSettings("ConString_Analysis")
        Dim source() As String = constring.Split("|")
        Dim SQLConn As SqlConnection = New SqlConnection
        If source(2) = "sa" Then
            SQLConn.ConnectionString = "Data Source=" & source(0) & ";" &
            "Initial Catalog=" & source(1) & ";" &
            "User ID=" & source(2) & ";" &
            "Password=" & lFun.DecryptString(source(3)) & ";" &
            "Max Pool Size=" & source(4) & ";" &
            "Connect Timeout=" & source(5) & ""
        Else
            SQLConn.ConnectionString = "Data Source=" & lFun.DecryptString(source(0)) & ";" &
            "Initial Catalog=" & lFun.DecryptString(source(1)) & ";" &
            "User ID=" & lFun.DecryptString(source(2)) & ";" &
            "Password=" & lFun.DecryptString(source(3)) & ";" &
            "Max Pool Size=" & lFun.DecryptString(source(4)) & ";" &
            "Connect Timeout=" & lFun.DecryptString(source(5)) & ""
        End If
        Return SQLConn
    End Function

    Public Function getConStr_Neraca()
        'Dim constring As String = System.Configuration.ConfigurationManager.AppSettings("ConStringHelper")
        Dim constring As String = System.Configuration.ConfigurationManager.AppSettings("ConString_Analysis")
        Dim source() As String = constring.Split("|")
        Dim SQLConn As SqlConnection = New SqlConnection
        If source(2) = "sa" Then
            SQLConn.ConnectionString = "Data Source=" & source(0) & ";" &
            "Initial Catalog=" & source(1) & ";" &
            "User ID=" & source(2) & ";" &
            "Password=" & lFun.DecryptString(source(3)) & ";" &
            "Max Pool Size=" & source(4) & ";" &
            "Connect Timeout=" & source(5) & ""
        Else
            SQLConn.ConnectionString = "Data Source=" & lFun.DecryptString(source(0)) & ";" &
            "Initial Catalog=" & lFun.DecryptString(source(1)) & ";" &
            "User ID=" & lFun.DecryptString(source(2)) & ";" &
            "Password=" & lFun.DecryptString(source(3)) & ";" &
            "Max Pool Size=" & lFun.DecryptString(source(4)) & ";" &
            "Connect Timeout=" & lFun.DecryptString(source(5)) & ""
        End If
        Return SQLConn
    End Function
    Public Function getWorkflowURL(hitWorkflow)
        Dim url As String = System.Configuration.ConfigurationManager.AppSettings("iDcWFURL") + hitWorkflow
        Return url
    End Function
    Public Function getCUSTURL(hitWorkflow)
        Dim url As String = System.Configuration.ConfigurationManager.AppSettings("iDcCUSTURL") + hitWorkflow
        Return url
    End Function
    Public Function getENURL(hitWorkflow)
        Dim url As String = System.Configuration.ConfigurationManager.AppSettings("iDcENURL") + hitWorkflow
        Return url
    End Function


    Public Function getConStrReadOnly()
        Dim constring As String = System.Configuration.ConfigurationManager.AppSettings("ConStringReadOnly")
        Dim source() As String = constring.Split("|")
        Dim SQLConn As SqlConnection = New SqlConnection

        SQLConn.ConnectionString = "Data Source=" & lFun.DecryptString(source(0)) & ";" & _
        "Initial Catalog=" & lFun.DecryptString(source(1)) & ";" & _
        "User ID=" & lFun.DecryptString(source(2)) & ";" & _
        "Password=" & lFun.DecryptString(source(3)) & ";" & _
        "Max Pool Size=" & lFun.DecryptString(source(4)) & ";" & _
        "Connect Timeout=" & lFun.DecryptString(source(5)) & ""

        Return SQLConn
    End Function
    Public Function getTimeOut()

        Dim constring As String = System.Configuration.ConfigurationManager.AppSettings("ConString")
        Dim source() As String = constring.Split("|")
        Try
            Return lFun.DecryptString(source(5))
        Catch ex As Exception
            Return source(5)
        End Try

    End Function

    Public Function getConStrDyn(server, db, user, password, maxPoolSize, timeOut)
        Dim SQLConn As String = ""
        SQLConn = "Data Source=" & server & ";" &
            "Initial Catalog=" & db & ";" &
            "User ID=" & user & ";" &
            "Password=" & password & ";" &
            "Max Pool Size=" & maxPoolSize & ";" &
            "Connect Timeout=" & timeOut & ""
        Try
            Dim objConn As SqlConnection = New SqlConnection(SQLConn)
            objConn.Open()
            objConn.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub testConnection(ByVal usr)
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
    End Sub
End Class
