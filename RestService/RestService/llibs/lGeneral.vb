Imports System.Data.SqlClient
Public Class lGeneral
    Public Function getVersion()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getVersion", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0).Rows(0)
    End Function

    Public Function getVersionDetail()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getVersionDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Public Function getVersionModule(ByVal ver)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_getVersionModule", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ver", SqlDbType.VarChar, 50).Value = ver
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Public Function CheckNullString(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return ""
        Else
            Return CType(parVal, String)
        End If
    End Function

    Public Function ValidateNullString(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return True
        Else
            Return CType(parVal, String)
        End If
    End Function

    Public Function CheckNullDecimal(ByVal parVal As Object) As Decimal
        If IsDBNull(parVal) Then
            Return 0
        Else
            Return CType(parVal, Decimal)
        End If
    End Function

    Shared Function isDBTypeNumeric(ByVal pDBType As Integer) As Boolean
        Select Case pDBType
            Case 0, 1, 2
                Return False
            Case Else
                Return True
        End Select
    End Function

    Public Function intFormat(ByVal parVal)
        Dim str As String = parVal
        Return str.Replace(",", ".")
    End Function
    Public Function decFormat(ByVal parVal)
        Dim str As String = parVal
        Return str.Replace(",", "")
    End Function

    'Function TDES_encryption(ByVal pass)
    '    Dim TripleDESKey As String = "PermataiDecisionSolution"

    '    Dim ADAuth As vTDES = New vTDES
    '    Dim EncPassword As String = vTDES.Encrypt(pass, TripleDESKey, False)
    '    Return EncPassword
    'End Function

    'Function TDES_decryption(ByVal pass)
    '    Dim TripleDESKey As String = "PermataiDecisionSolution"
    '    Dim ADAuth As vTDES = New vTDES
    '    Dim EncPassword As String = vTDES.Decrypt(pass, TripleDESKey, False)
    '    Return EncPassword
    'End Function

    Function crypPwd(ByVal pwd As String) As String
        Dim strTempChar As String, i As Integer
        strTempChar = ""
        For i = 1 To Len(pwd)
            If Asc(Mid$(pwd, i, 1)) < 128 Then
                strTempChar = _
          CType(Asc(Mid$(pwd, i, 1)) + 128, String)
            ElseIf Asc(Mid$(pwd, i, 1)) > 128 Then
                strTempChar = _
          CType(Asc(Mid$(pwd, i, 1)) - 128, String)
            End If
            Mid$(pwd, i, 1) = _
                Chr(CType(strTempChar, Integer))
        Next i
        Return pwd
    End Function

    Public Function CheckInputDate(ByVal dateString As String) As Boolean
        Try
            Dim dateParts() As String = dateString.Split("-")
            Dim testDate As New Date(Convert.ToInt32(dateParts(2)), _
                                     Convert.ToInt32(dateParts(1)), _
                                     Convert.ToInt32(dateParts(0)))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub createSP(ByVal sp, ByVal sql)
        Dim xStr As String = " if exists ( Select name FROM sysobjects where name = '" & sp & "' ) DROP PROCEDURE dbo." & sp
        exec_sp(xStr)
        Dim str As String = " CREATE Procedure " & sp & " AS " & sql & " "
        exec_sp(str)
    End Sub

    Function exec_sp(ByVal qry)
        If qry <> "" Then
            Dim myDb As New dbConn
            Dim myConn As SqlConnection
            myConn = myDb.getConStr
            Dim cmdA As New SqlCommand(qry, myConn)
            cmdA.CommandType = CommandType.Text
            myConn.Open()
            cmdA.ExecuteNonQuery()
            myConn.Close()
        End If
        Return True
    End Function

    Sub run_text(ByVal qry)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand(qry, myConn)
        cmdA.CommandType = CommandType.Text
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function delete_sp(ByVal sp)
        If Not sp = "" Then
            Dim xStr As String = " if exists ( Select name FROM sysobjects where name = '" & sp & "' ) DROP PROCEDURE dbo." & sp
            run_text(xStr)
        End If
        Return True
    End Function

    Function go_sp()
        Return " GO "
    End Function

    Function script_end_sp()
        Dim xStr As String = go_sp()
        'Dim xStr As String = go_sp() & " SET QUOTED_IDENTIFIER OFF " & go_sp() & " SET ANSI_NULLS ON " & go_sp()
        Return xStr
    End Function

    Function script_delete_tbl(ByVal tbl)
        Return " if exists ( Select name FROM sysobjects where name = '" & tbl & "' ) DROP table " & tbl
    End Function

    Sub insertLog(ByVal dt, ByVal userip, ByVal url, ByVal msg)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("er_insertLog", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@date", SqlDbType.DateTime).Value = dt
        cmdA.Parameters.Add("@userip", SqlDbType.VarChar).Value = userip
        cmdA.Parameters.Add("@url", SqlDbType.Text).Value = url
        cmdA.Parameters.Add("@msg", SqlDbType.Text).Value = msg

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
