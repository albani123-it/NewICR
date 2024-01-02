Imports System.IO
Imports System.Data.OleDb

Public Class DataUploadProyeksiDet
    Inherits System.Web.UI.Page
    Dim DataUpload As New lDataUploadProyeksi
    Dim oGen As New lGeneral
    Dim appno As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim Filename As String = lblFileExcel.Text
            Dim FolderPath As String = DataUpload.path
            Dim table_name As String = "Data_Upload_From_Excel" 

            Dim FilePath As String = Server.MapPath(FolderPath + Filename)
            FileUploadXLS.SaveAs(FilePath)

            DataUpload.InsertTable(appno, Filename, FilePath, FolderPath, table_name, usrLogin, ddlSheets.Text)
            'liBodyLoadAction.Text = "alert('successfully.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            liBodyLoadAction.Text = "alert('" + oGen.NotifikasiUpload() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "')"
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        If FileUploadXLS.HasFile Then
            Dim FileName As String = Path.GetFileName(FileUploadXLS.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadXLS.PostedFile.FileName)
            Dim FolderPath As String = DataUpload.path
            Dim FilePath As String = Server.MapPath(FolderPath + FileName)
            FileUploadXLS.SaveAs(FilePath)
            'GetExcelSheets(FilePath, Extension, "Yes")
            lblFileExcel.Text = FileName
            FileUploadXLS.Visible = False
            btnUpload.Visible = False
            'TRSheet.Style.Add("display", "")
            TRWord.Style.Add("display", "none")
        End If
    End Sub

    Private Sub GetExcelSheets(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Dim conStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings("Excel03ConString") _
                         .ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
                          .ConnectionString
                Exit Select
        End Select

        conStr = String.Format(conStr, FilePath, isHDR)
        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        cmdExcel.Connection = connExcel
        connExcel.Open()


        ddlSheets.Items.Clear()
        ddlSheets.Items.Add(New ListItem("--Select Sheet--", ""))
        ddlSheets.DataSource = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        ddlSheets.DataTextField = "TABLE_NAME"
        ddlSheets.DataValueField = "TABLE_NAME"
        ddlSheets.DataBind()
        connExcel.Close()
    End Sub

End Class