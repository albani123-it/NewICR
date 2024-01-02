Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Data.OleDb

Public Class lDataUploadProyeksi
    Public path As String = "~/DataUpload/"
    Public path_tmp As String = "~/DataUpload/tmp/"

    Function getDataUploadToGrid(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmda As New SqlCommand("dtu_getDataUpload", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@appno", appno)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function InsertTable(ByVal appno As String, ByVal fileName As String, ByVal filePath As String, ByVal path As String, ByVal tableName As String, ByVal usrLogin As String, ByVal sheetName As String)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        sheetName = "All"
        Dim rowcount As String = 0


        'rowcount As String = CreateTableFromExcel(filePath, path & fileName, tableName, sheetName)
        'runDataUploadProyeksi(appno)
        Dim cmdA As New SqlCommand("dtu_insertDataUpload", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@row_count", SqlDbType.BigInt).Value = rowcount
        'cmdA.Parameters.Add("@sheet_name", SqlDbType.VarChar, 100).Value = Replace(Replace(sheetName, "$", ""), "'", "")
        cmdA.Parameters.AddWithValue("@sheet_name", sheetName)
        cmdA.Parameters.Add("@file_location", SqlDbType.VarChar, 100).Value = fileName
        cmdA.Parameters.Add("@upload_usr", SqlDbType.VarChar).Value = usrLogin
        cmdA.Parameters.AddWithValue("@appno", appno)
        cmdA.ExecuteNonQuery()
        myConn.Close()

        Return rowcount
    End Function

    Function runDataUploadProyeksi(ByVal appno)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("dtu_insertDataUploadToMasterProyeksi", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@appno", SqlDbType.VarChar, 50).Value = appno
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Function CreateTableFromExcel(ByVal filepath As String, ByVal path As String, ByVal tablename As String, ByVal sheetName As String) As String
        Dim dt As DataTable
        dt = ReadExcel(filepath, sheetName)

        Dim ctStr As String = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" & tablename & "') BEGIN drop table " & tablename & " END CREATE TABLE [" & tablename & "](" & vbCr & vbLf
        For Each row In dt.Rows
            For i As Integer = 0 To row.table.columns.count - 1
                Dim field As String = ""
                field = Trim(row.table.columns(i).ToString())
                ctStr += "  [" & field & "] [varchar](4000) NULL"
                If i < row.table.columns.count Then
                    ctStr += ","
                End If
                ctStr += vbCr & vbLf
            Next
            Exit For
        Next

        ctStr += ")"


        ' Runs the sql command to make the destination table
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim command As New SqlCommand(ctStr, myConn)
        command.ExecuteNonQuery()

        'Populate the new table with data
        'dt.Rows(0).Delete()


        Dim row_count As String = dt.Rows.Count
        Using copy As New SqlBulkCopy(myConn)
            copy.DestinationTableName = tablename
            copy.WriteToServer(dt)
            copy.Close()
        End Using

        Return row_count
    End Function


    Function ReadExcel(ByVal path As String, ByVal sheetName As String) As System.Data.DataTable
        Dim connString As String = String.Empty
        Dim extension As String = ".xls"
        Select Case Extension
            Case ".xls"
                'Excel 97-03
                connString = ConfigurationManager.ConnectionStrings("Excel03ConString") _
                         .ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07
                connString = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                          .ConnectionString
                Exit Select
        End Select

        connString = String.Format(connString, path, "yes")

        Dim connExcel As New OleDbConnection(connString)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Read Data from First Sheet
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & sheetName & "] where periode is not null"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        Return dt
    End Function


End Class
