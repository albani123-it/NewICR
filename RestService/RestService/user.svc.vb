' NOTE: You can use the "Rename" command on the context menu to change the class name "user" in code, svc and config file together.
Public Class user
    Implements Iuser


    Public Function addUser(ByVal userID As String, ByVal name As String, ByVal npk As String, ByVal accessLevel As String, ByVal status As String, ByVal msgi As String) As postUSer Implements Iuser.addUser
        CekInput(userID, name, npk, accessLevel, status, msgi)
        Dim obj As New postUSer
        If msgi = "" Then
            obj.userID = userID
            obj.name = name
            obj.npk = npk
            obj.accessLevel = accessLevel
            obj.status = status
        Else
            msgi = "Error"
        End If

        Return obj
    End Function

    Sub CekInput(ByVal userID, ByVal name, ByVal npk, ByVal accessLevel, ByVal status, ByVal msgi)
        Dim lMsg As String = ""

        If userID = "" Then
            lMsg = lMsg + "Please input User ID.\n"
            With New lUser
                Try
                    Select Case .ValidateUser(userID, "new")
                        Case 2
                            lMsg = "UserID must have between 6 to 15 characters!\n" 'ganti
                        Case 3
                            lMsg = "UserID has already existed. Please input a different userID!\n" 'ganti
                    End Select
                Catch ex As Exception

                End Try
            End With
        ElseIf accessLevel <> "-1" Then
            With New lUser
                Try
                    Select Case .ValidateUser(userID, "")
                        Case 1
                            lMsg = "The user id is currently logging in. It cannot be edited or deleted.\n" 'ganti
                    End Select
                Catch ex As Exception

                End Try
            End With
        End If

        If name = "" Then
            lMsg = lMsg + "Please input User Name.\n"
        End If

        If npk = "" Then
            lMsg = lMsg + "Please input User NPK.\n"
        End If

        If accessLevel = "0" Then
            lMsg = lMsg + "Please select Access Level.\n"
        End If

        If lMsg <> "" Then
            msgi = lMsg
        End If
    End Sub


End Class
