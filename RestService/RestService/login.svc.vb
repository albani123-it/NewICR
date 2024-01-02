Imports System
Imports System.IO
Imports System.Data


Public Class login
    Implements Ilogin
    Dim lMsg As String = ""
    Dim NPKey As String
    Dim tempWord As String = ""
    Dim lfunc As New lFunction
    Dim pk As New lGlobal

#Region "Ilogin Members"
    Public Function USRNPKData(ByVal NPK As String, ByVal pass As String) As String Implements Ilogin.USRNPKData
        Dim acak As String = ""
        Dim time As String = ""
        Dim passKey As String = ""



        CekInput(NPK, pass)
        If lMsg = "" Then
            With New lUser
                Dim dsUser As New DataSet
                Dim ws_tracer_npk As New DataTable

                dsUser = .getUser(NPK)
                If dsUser.Tables(0).Rows.Count > 0 Then
                    If dsUser.Tables(0).Rows(0).Item("usr_is_login") = 1 Then
                        lMsg = "User ID is login."
                    Else
                        If dsUser.Tables(0).Rows(0).Item("usr_status") = 0 Then
                            'lMsg = "User ID is inactive or disabled."
                            lMsg = "User anda tidak aktif"
                        Else
                            Try
                                ws_tracer_npk = pk.checkLoginPasskey(NPK)

                                pk.passingPassKey(ws_tracer_npk.Rows(0).Item("passkey"))
                                'If ws_tracer_npk.Rows(0).Item("rowZ") > 0 Then
                                lMsg = ws_tracer_npk.Rows(0).Item("passkey")
                                'Else
                                '    passKey = lfunc.pazzKey(NPK)
                                '    .insertWSTracer(passKey, NPK, "Login")
                                '    lMsg = passKey
                                'End If
                            Catch ex As Exception
                                passKey = lfunc.pazzKey(NPK)
                                .insertWSTracer(passKey, NPK, "Login")
                                lMsg = passKey
                            End Try
                            ws_tracer_npk = pk.checkLoginPasskey(NPK)

                            If ws_tracer_npk.Rows(0).Item("rowZ") > 0 Then
                                lMsg = ws_tracer_npk.Rows(0).Item("passkey")
                            Else
                                passKey = lfunc.pazzKey(NPK)
                                .insertWSTracer(passKey, NPK, "Login")
                                lMsg = passKey
                            End If

                        End If

                    End If
                Else
                    'lMsg = "User ID or Password is incorrect.\n"
                    lMsg = "User anda belum terdaftar"
                    If Not NPK = "" Then
                        Dim objUsr As New lUser
                        objUsr.userFailedLogin(NPK)
                    End If
                End If
            End With
        End If
        Return lMsg
    End Function

   
#End Region

    Sub CekInput(ByVal NPK, ByVal pass)
        If NPK = "" Then
            lMsg = "User ID field is required. Please input user ID."
        ElseIf pass = "" Then
            lMsg = "Password field is required. Please input password."
        End If
    End Sub

   

   
End Class
