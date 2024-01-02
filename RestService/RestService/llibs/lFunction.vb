Imports System.Security.Cryptography
Imports System.IO

Public Class lFunction
    Dim NPKey As String
    Dim acak As String
    Dim time As String
    Dim jam As String = ""
    Dim jamSplit As String = ""
    Dim splitjam() As String
    Dim passKey As String = ""


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

    '=================================IZ====================================

    Public Function pazzKey(ByVal NPK As String) As String
        NPKey = NPK.Substring(0, 5)
        acak = GenerateRandomString(2)
        time = DateTime.Now.ToString("HH:mm:ss")

        splitjam = time.Split(":")
        For count = 0 To splitjam.Length - 1
            jam = splitjam(count)

            If jamSplit = "" Then
                jamSplit = jam
            Else
                jamSplit = jamSplit & jam
            End If
        Next

        passKey = NPKey & acak & jamSplit

        Return passKey

    End Function

    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += NPKey(rdm.Next(0, NPKey.Length))
        Next

        Return sResult
    End Function





End Class
