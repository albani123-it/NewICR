' NOTE: You can use the "Rename" command on the context menu to change the class name "logout" in code, svc and config file together.
Public Class logout
    Implements Ilogout
    Dim oUt As New lGlobal
#Region "Ilogout Members"

    Public Function Logout(ByVal passkey As String) As String Implements Ilogout.passKeyData
        Dim dtpasskey As New DataTable
        Dim lmsg As String = ""
        Try
            With New lGlobal
                dtpasskey = .checkPassKey(passkey)
            End With

            If dtpasskey.Rows(0).Item("rowZ") > 0 Then
                oUt.logOut(passkey)
                lmsg = "logout"
            End If

        Catch ex As Exception
            lmsg = "Passkey Tidak ditemukan"
        End Try

        Return lmsg
    End Function


#End Region

End Class
