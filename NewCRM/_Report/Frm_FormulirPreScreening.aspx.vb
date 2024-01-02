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

Public Class Frm_FormulirPreScreening
    Inherits System.Web.UI.Page
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    Dim oRptDKI As New lReportDKI
    Dim oApp As New lApps
    'Dim oRpt As New lReportStress
    Dim oGen As New lGeneral
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
        Dim ds As DataSet
        Dim dt As DataTable
        Dim month As String = ""
        'ds = oRpt.getPeriod
        'dt = ds.Tables(0)
        cekProduct(appno)
        CreateTable_A()
        CreateTable_Keyperson()
        CreateTable_B()
        CreateTable_C()
        CreateTable_Alasan()
        Create_Signature()

        'ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub

    Sub cekProduct(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkGeneral(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            product = IIf(IsDBNull(dt.Rows(0).Item("lblProductType")), "", dt.Rows(0).Item("lblProductType"))
            brwcode = IIf(IsDBNull(dt.Rows(0).Item("lsg_brw_code")), "", dt.Rows(0).Item("lsg_brw_code"))
        End If
    End Sub

    Public Sub CreateTable_A()
        Dim oXMLReport As New DataReport
        litFPS.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFPS_getHeader("88899", "BR-BTN-2016-00011")
        dsData = oRptDKI.getFPS_getHeader(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FPS/FPS.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litFPS.Text = oXMLReport.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub CreateTable_Keyperson()
        Dim oXMLReport As New DataReport
        litKeyperson.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFPS_getHeader("88899", "BR-BTN-2016-00011")
        dsData = oRptDKI.getFPS_getKeyperson(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FPS/FPS_KeyPerson.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litKeyperson.Text = oXMLReport.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub

    Public Sub CreateTable_B()
        Dim oXMLReport As New DataReport
        litFPS_A.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFPS_getkeriteria(usrLogin, brwcode)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FPS/FPS_A.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litFPS_A.Text = oXMLReport.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub CreateTable_C()
        Dim oXMLReport As New DataReport
        litFPS_B.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFPS_getSkor(usrLogin, brwcode)
        Dim dtData As DataTable
        dtData = dsData.Tables(1)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FPS/FPS_B.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litFPS_B.Text = oXMLReport.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub CreateTable_Alasan()
        Dim oXMLReport As New DataReport
        litFPS_Alasan.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFPS_getReason(usrLogin, brwcode)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FPS/FPS_Reason.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litFPS_Alasan.Text = oXMLReport.mainLCR(nodes, dtData)

            btnExport.Visible = True
        End If
    End Sub
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'If ddlExportType.SelectedValue = "Word" Then
        exportWord()
        ' End If
    End Sub
    Private Sub exportWord()

        Dim fileName As String = "Formulir_PreScrenning"
        Dim css As String = ""
        Dim html As String = ""
        Dim signPdf As String = SignaturePDF("FPS", appno)

        html &= "<table align='center'>"
        html &= "<tr><td colspan='2'><p align='center'><strong>" & lbljudul.Text & "</strong></p></td></tr>"
        html &= "</table>"
        html &= " <br />"
        html &= "<table border = '1' align='center' style='border-spacing:initial;margin: 3px 2px 3px 2px;width:98%;'>"
        html &= "<tr><td colspan='4'><p style='height:25px;font-size:10px' align='justify'>" & litFPS.Text & "</p></td></tr>"
        html &= "<tr><td colspan='4'><p style='height:25px;font-size:10px' align='justify'>" & litKeyperson.Text & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='border-spacing:1px;margin: 3px 2px 3px 2px;width:98%;'>"
        html &= "<tr><td colspan='4'><p style='height:25px;font-size:10px' align='justify'>" & litFPS_A.Text & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='border-spacing:initial;margin: 3px 2px 3px 2px;width:98%;'>"
        html &= "<tr><td colspan='4'><p style='height:25px;font-size:10px' align='justify'>" & litFPS_B.Text & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='border-spacing:initial;margin: 3px 2px 3px 2px;width:98%;'>"
        html &= "<tr><td colspan='4'><p style='height:25px;font-size:10px' align='justify'>" & litFPS_Alasan.Text & "</p></td></tr>"
        html &= "</table>"
        html &= signPdf
        'Response.Write(html)
        'Response.Output.Write(sw.ToString())
        'Response.Flush()
        'Response.End()

        'css = File.ReadAllText(MapPath("~/Styles/Site.css"))


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
    '---End---

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("FPS", appno)
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