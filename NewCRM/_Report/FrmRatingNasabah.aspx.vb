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

Public Class FrmRatingNasabah
    Inherits System.Web.UI.Page
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    Dim periode As String
    Dim oRptDKI As New lReportDKI
    'Dim oRpt As New lReportStress
    Dim oGen As New lGeneral
    Dim oAsr As New lAsumsiRating
    Dim oApp As New lApps
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region

    'Private Property imgPath As String
    'Dim oXMLReport As New DataReport
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        cekProduct(appno)
        If Not IsPostBack Then
            fillDdl()
            appendFRN()
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
        Dim ds As DataSet
        Dim dt As DataTable
        Dim month As String = ""
        'ds = oRpt.getPeriod
        dt = oAsr.getLastPeriodeAsumsiByAppno(appno)
        If dt.Rows.Count > 0 Then
            periode = dt.Rows(0).Item("lpar_periode").ToString()
        End If

        Create_Table()
        Create_Table_B()
        Create_Table_A()
        Create_Signature()
        ' ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub

    Sub cekProduct(ByVal appno)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkGeneral(appno)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            'product = dt.Rows(0).Item("lblProductType")
            brwcode = dt.Rows(0).Item("lsg_brw_code")
        End If
    End Sub

    Public Sub Create_Table()
        Dim oXMLReport As New DataReport
        litScore.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFRN_AsumsiRetinginfo("20160013", "20161214")
        dsData = oRptDKI.getFRN_AsumsiRetinginfo(appno, periode)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FormRatingNasbah.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            lbljudul2.Text = oXMLReport.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_A()
        Dim oXMLReport As New DataReport
        litScore.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFRN_AsumsiRetinginfo(appno, periode)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FormRatingNasbah_A.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            'litScore.Text = oXMLReport.mainLCR(nodes, dtData)
            litScore.Text = oXMLReport.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_B()
        Dim oXMLReport As New DataReport
        litScore1.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFRN_AsumsiRetingDeatel(appno, periode)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FormRatingNasbah_B.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litScore1.Text = oXMLReport.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    '---Update---
    Sub appendFRN()
        'Dim dr As DataRow
        Dim ds As DataSet
        With New lReportDKI
            ds = .get_AppenFIA(appno, "FRN")
            For index = 0 To ds.Tables(0).Rows.Count - 1
                If (index + 1) = 1 Then
                    txtmsg1.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg1.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
                ElseIf (index + 1) = 2 Then
                    txtmsg2.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg2.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
                End If
            Next

        End With
    End Sub
    Sub updateReport(ByVal gettxtmsg1, ByVal gettxtmsg2)
        Dim ds As DataSet
        With New lReportDKI
            ds = .UpdateReportFIA(appno, gettxtmsg1(1), gettxtmsg1(0))
            ds = .UpdateReportFIA(appno, gettxtmsg2(1), gettxtmsg2(0))
        End With
    End Sub
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim gettxtmsg1() As String = {txtmsg1.InnerText, txtmsg1.Attributes("tagcode")}
        Dim gettxtmsg2() As String = {txtmsg2.InnerText, txtmsg2.Attributes("tagcode")}
        'Try
        '    'If ddlExportType.SelectedValue = "Word" Then
        '    exportWord()
        '    ' End If
        'Catch ex As Exception
        '    updateReport(gettxtmsg1, gettxtmsg2)
        'End Try
        updateReport(gettxtmsg1, gettxtmsg2)
        exportWord()

    End Sub
    Private Sub exportWord()
        Response.Clear()
        Response.Buffer = True
        Dim fileName As String = "Formulir_Rating_Nasabah"
        Dim html As String = ""
        Dim signPdf As String = SignaturePDF("FRN", appno)
        html &= "<table align='center'  width='100%'>"
        html &= "<tr  border = '1'><td colspan='2'><p align='center'><b>" & lbljudul1.Text & "</b></p></td></tr>"
        html &= "<tr border = '1'><td colspan='2'><p align='center'>" & lbljudul2.Text & "</p></td></tr>"
        html &= "</table>"
        html &= " <br />"
        html &= "<table width='100%' >"
        html &= "<tr border = '1'><td colspan='2'><p align='justify'>" & txtmsg1.Value & "</p></td></tr>"
        html &= "<tr border = '1'><td colspan='2'>" & litScore.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table width='100%'>"
        html &= "<tr border = '1'><td colspan='2'><p align='justify'>" & txtmsg2.Value & "</p></td></tr>"
        html &= "<tr border = '1'><td colspan='2'>" & litScore1.Text & "</td></tr>"
        html &= "</table>"
        html &= signPdf

        'Me.EnableViewState = False
        Dim oStringWriter As StringWriter = New StringWriter
        Dim oHtmlTextWriter As HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        'Me.tblFrn.RenderControl(oHtmlTextWriter)

        Dim pdfDoc As New Document(PageSize.A4, 20.0F, 20.0F, 20.0F, 0.0F)
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
    '---END----

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("FRN", appno)
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