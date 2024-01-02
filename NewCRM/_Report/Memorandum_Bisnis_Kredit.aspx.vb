Imports System.Web.Services
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web
Imports System.Data.SqlClient
Imports System
Imports System.Collections
Imports Obout.Grid
Imports System.IO
Imports System.Configuration
Imports iTextSharp.text
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports IDX.XMLReport
Imports IDX.DocumentExport
Imports System.Xml
Imports iTextSharp.tool.xml

Public Class Memorandum_Bisnis_Kredit
    Inherits System.Web.UI.Page
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    Dim oRptDKI As New lReportDKI
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        If Not IsPostBack Then
            fillDdl()

        End If
    End Sub
    Sub cekAuthentication()
        Try
            If Session("UserLoginID") Is Nothing Then
                Session.Abandon()
                Response.Redirect("../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub fillDdl()


        Create_Table()
        Create_Signature()
        'ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub
    Public Sub Create_Table()
        Dim oXMLReport As New DataReport
        litMbk.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getMBK_BisnisKreditDetail(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/MBK/MBK.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litMbk.Text = oXMLReport.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'If ddlExportType.SelectedValue = "Word" Then
        exportWord()
        'End If
    End Sub
    Private Sub exportWord()
        Dim fileName As String = "Memorandum_Bisnis_Kredit"
        
        Dim html As String = ""
        Dim strMsg As String = ""
        Dim signPdf As String = SignaturePDF("MBK", appno)

        strMsg = litMbk.Text
        strMsg = strMsg.Replace("•", "<br />•")
        strMsg = strMsg.Replace("1)", "<br />1)")
        strMsg = strMsg.Replace("2)", "<br />2)")
        strMsg = strMsg.Replace("3)", "<br />3)")
        strMsg = strMsg.Replace("4)", "<br />4)")
        html &= "<table align='center'>"
        html &= "<tr><td colspan='2'><p align='center'><b>" & lbljudul.Text & "</b></p></td></tr>"
        html &= "</table>"
        html &= " <br />"
        html &= "<table border = '1' align='center' width:80%;>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px;white-space: pre-wrap'>" & strMsg.ToString & "</p></td></tr>"
        html &= "</table>"
        html &= signPdf

        Dim pdfDoc As New Document(PageSize.A4, 20.0F, 20.0F, 20.0F, 20.0F)
        Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        pdfDoc.Open()
        Using cssMemoryStream = New MemoryStream(System.Text.Encoding.UTF8.GetBytes(""))
            Using htmlMemoryStream = New MemoryStream(System.Text.Encoding.UTF8.GetBytes(html))
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, htmlMemoryStream, cssMemoryStream)
            End Using
        End Using
        'XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr)
        pdfDoc.Close()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=" & fileName & ".pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(pdfDoc)
        Response.End()
    End Sub

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("MBK", appno)
        Dim dt As DataTable
        dt = ds.Tables(0)

        Dim opnTr As String = ""
        Dim clsTR As String = ""
        Dim NL As Integer = 1 'New Line
        Dim rw As Decimal = 0

        If dt.Rows.Count > 0 Then
            html += "<div class='autowide'>"
            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("usr_name").ToString() <> "" And dt.Rows(i).Item("usr_access_level").ToString() <> "" Then
                    html += "<div class='module'>"
                    html += "<div class='header-content'>"
                    html += dt.Rows(i).Item("usr_name").ToString()
                    html += "<hr style='border:0;background-color:#000;height:1px;' />"
                    html += dt.Rows(i).Item("usr_access_level").ToString()
                    html += "</div>"
                    html += "</div>"
                End If

            Next
            html += "</div>"
        End If


        litSignature.Text = html
    End Sub

    Function SignaturePDF(ByVal rpt, ByVal appno)
        Dim html As String = ""
        Dim ds As DataSet

        ds = oRptDKI.get_Signature(rpt, appno)
        Dim dt As DataTable
        dt = ds.Tables(0)

        html = oRptDKI.generate_signature(dt)

        Return html
    End Function
End Class