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

Public Class Frm_Tectical_Account_Planning
    Inherits System.Web.UI.Page
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    'Dim oRpt As New lReportStress
    Dim oRptDKI As New lReportDKI
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
        ds = oRptDKI.getAppReport
        dt = ds.Tables(0)
        'ddlPeriod.Items.Clear()
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    ddlPeriod.Items.Add(New System.Web.UI.WebControls.ListItem(dt.Rows(i).Item("field"), dt.Rows(i).Item("value")))
        'Next

        lbljudul1.Visible = True
        Create_Table()
        Create_Table_A()
        Create_Table_B()
        Create_Table_C()
        Create_Table_E()
        Create_Signature()

        'ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("TAP", appno)
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

    Public Sub Create_Table()
        Dim oXMLReport As New DataReport
        littap.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getTap_HeaderKebutuhanProduk("88899", "20160013")
        dsData = oRptDKI.getTap_HeaderKebutuhanProduk(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/TAP/TAP.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            littap.Text = oXMLReport.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_A()
        Dim oXMLReport_A As New DataReport
        Littap_A.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getTap_KebutuhanProduk(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/TAP/TAP_A.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            Littap_A.Text = oXMLReport_A.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_B()
        Dim oXMLReport_B As New DataReport
        littap1.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getTap_walletReport(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/TAP/TAP_B.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            littap1.Text = oXMLReport_B.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_C()
        Dim oXMLReport_C As New DataReport
        littap2.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getTap_AcctionPlan(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/TAP/TAP_C.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            littap2.Text = oXMLReport_C.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    'Public Sub Create_Table_D()
    '    Dim oXMLReport_D As New DataReport
    '    littap3.Text = ""
    '    Dim dsData As DataSet
    '    dsData = oRptDKI.getTap_KebutuhanProduk("88899", "20160013")
    '    Dim dtData As DataTable
    '    dtData = dsData.Tables(0)
    '    If dtData.Columns.Count <> 0 Then
    '        Dim filePath As String = Server.MapPath("~/xml/TAP_D.xml")
    '        Dim xmlDoc As New System.Xml.XmlDocument()
    '        xmlDoc.Load(filePath)
    '        Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
    '        littap3.Text = oXMLReport_D.mainLCR(nodes, dtData)
    '        btnExport.Visible = True
    '        ddlExportType.Visible = True
    '    End If
    'End Sub
    Public Sub Create_Table_E()
        Dim oXMLReport_E As New DataReport
        littap4.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getTap_KebutuhanProduk(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/TAP/TAP_E.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            littap4.Text = oXMLReport_E.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    '--update----
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'If ddlExportType.SelectedValue = "Word" Then
        exportWord()
        'End If
    End Sub

    Private Sub exportWord()
        Response.Clear()
        Response.Buffer = True
        Dim fileName As String = "Formulir_Tectical_Account_Planning"
        Dim signPdf As String = SignaturePDF("TAP", appno)
        Dim html As String = ""

        html += "<div align='center'><span style='font-weight:bold;font-size:15px;width:99%;'>TACTICAL ACCOUNT PLANNING (TAP)</span></div>"
        html += littap.Text
        html += Littap_A.Text
        html += "<br />"
        html += "<span style='font-weight:bold;font-size:14px;width:99%;float:left;'>2.	Wallet Share</span>"
        html += littap1.Text
        html += "<br />"
        html += "<span runat='server' style='font-weight:bold;font-size:14px;width:99%;float:left;'>3.	Action Plan</span>"
        html += littap2.Text
        html += "<br />"
        html += littap4.Text
        html += signPdf

        Dim pdfDoc As New Document(PageSize.A4.Rotate, 20.0F, 20.0F, 20.0F, 0.0F)
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

        'Dim objSR As New StringReader(html.ToString())
        'Dim objPDF As New Document(PageSize.A4, 20.0F, 20.0F, 20.0F, 0.0F)
        'Dim objHW As New HTMLWorker(objPDF)
        'PdfWriter.GetInstance(objPDF, Response.OutputStream)
        'objPDF.Open()
        'objHW.Parse(objSR)
        'objPDF.Close()
        'Response.Write(objPDF)
        'Response.End()

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
    '---end---
End Class