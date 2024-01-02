Imports System.Drawing
Imports System.IO
Imports System.Security.AccessControl
Imports Microsoft.VisualBasic
Imports System.Net.Mime
Public Class mak_download
    Inherits System.Web.UI.Page
    Dim usrLogin As String
    Dim oApp As New lApps
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected proses As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        If Request.QueryString("appno") IsNot Nothing Then
            If Not Page.IsPostBack Then
                Dim ds As DataSet
                Dim dt As DataTable
                '===========attachment================
                ds = oApp.checkMAKFile(Request.QueryString("appno"), "All", proses)
                grid2.DataSource = ds
                grid2.DataBind()
                'If ds2.Tables(0).Rows.Count > 0 Then
                '    btnAdd.Visible = False
                'End If
                '===========end attachment============

            End If
        End If
    End Sub

    Protected Sub download(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim ds As DataSet
        Dim dt As DataTable
        Dim json As String = ""
        ds = oApp.checkMAKFile_byappno(Request.QueryString("appno"))
        dt = ds.Tables(0)
        Dim docname As String = dt.Rows(0).Item("lmakf_file")
        Dim mkFolder As String
        mkFolder = "~/Document Upload/" & Request.QueryString("appno") & "/MAK_files/"
        Dim pathes As String = Server.MapPath(mkFolder & docname)
        If File.Exists(pathes) Then
            Response.ContentType = "application/octet-stream"
            Dim cd = New ContentDisposition()
            cd.Inline = False
            cd.FileName = Path.GetFileName(pathes)
            Response.AppendHeader("Content-Disposition", cd.ToString())

            Dim fileData As Byte() = System.IO.File.ReadAllBytes(pathes)
            Response.OutputStream.Write(fileData, 0, fileData.Length)
            Response.[End]()

        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('File cannot be downloaded at this time.');window.location.href=window.location.href", True)
        End If
    End Sub
End Class