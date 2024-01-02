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
        Return source(5)
    End Function
End Class
