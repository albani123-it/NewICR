﻿
Imports System.Security.Cryptography
Imports System.IO

Public Class lFunction

    Public Function EncryptString(ByVal inputString As String) As String
        Dim TripleDESKey As String = "B3h4vioUr-scoR1ngPRojecT"
        Dim ADAuth As vTDES = New vTDES
        Dim encryptedQueryString As String = vTDES.Encrypt(inputString, TripleDESKey, False)
        Return encryptedQueryString
    End Function

    Public Function DecryptString(ByVal inputString As String) As String
        Dim TripleDESKey As String = "B3h4vioUr-scoR1ngPRojecT"
        Dim ADAuth As vTDES = New vTDES
        Dim decryptedQueryString As String = vTDES.Decrypt(inputString, TripleDESKey, False)
        Return decryptedQueryString
    End Function

End Class
