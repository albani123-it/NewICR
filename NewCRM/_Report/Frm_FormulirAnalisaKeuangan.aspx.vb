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

Public Class Frm_FormulirAnalisaKeuangan
    Inherits System.Web.UI.Page
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    Dim oRptDKI As New lReportDKI
    Dim oGen As New lGeneral
    Dim oApp As New lApps
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        If Not IsPostBack Then
            fillDdl()
            appendFAK()
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
    ''--UPdate---
    Sub fillDdl()
        Dim ds As DataSet
        Dim dt As DataTable

        cekProduct(appno)

        Create_AnalisaKeuHistoris()
        Create_IktisarKeu()
        Create_RasioAktivitasKeu()
        Create_LabaRugi()
        Create_Neraca()
        Create_RekonsulHartaTetap()
        Create_ArusKas()
        'Create_AsumsiProyeksiKeu()
        Create_ProyeksiIktisarKeu()
        Create_ProyeksiLabaRugi()
        Create_ProyeksiLapKeu()
        Create_ProyeksiCashFlow()
        Create_Signature()
        'ddlExportType.Items.Clear()
        ' ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub

    Sub cekProduct(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkGeneral(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            'product = dt.Rows(0).Item("lblProductType")
            'brwcode = dt.Rows(0).Item("lsg_brw_code")
            product = IIf(IsDBNull(dt.Rows(0).Item("lsg_segment")), "", dt.Rows(0).Item("lsg_segment"))
            brwcode = IIf(IsDBNull(dt.Rows(0).Item("lsg_brw_code")), "", dt.Rows(0).Item("lsg_brw_code"))
        End If
    End Sub

    Public Sub Create_AnalisaKeuHistoris()
        Dim oXMLReport As New DataReport
        litkeungan1.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFAK_AnalisaKeuHistori(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_AnalisaKeuHistoris.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_AnalisaKeuHistoris.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan1.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_IktisarKeu()
        Dim oXMLReport As New DataReport
        litkeungan2.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_RasioKeungan("Rasio", "konstruksi", "BRW_Dambha", "Ikhtisar Keuangan")
        dsData = oRptDKI.getFAK_RasioKeungan("Rasio", product, brwcode, "Ikhtisar Keuangan")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_IktisarKeu.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_IktisarKeu.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan2.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_RasioAktivitasKeu()
        Dim oXMLReport As New DataReport
        litkeungan3.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_RasioKeungan("Rasio", "konstruksi", "BRW_Dambha", "Rasio Aktivasi")
        dsData = oRptDKI.getFAK_RasioKeungan("Rasio", product, brwcode, "Rasio Aktivasi")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_RasioAktivitasKeu.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_RasioAktivitasKeu.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan3.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_LabaRugi()
        Dim oXMLReport As New DataReport
        litkeungan4.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_labarugi("Laba_rugi", "konstruksi", "BRW_Dambha", "N")
        dsData = oRptDKI.getFAK_labarugi("Laba_rugi", product, brwcode, "N")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_LabaRugi.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_LabaRugi.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan4.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Neraca()
        Dim oXMLReport As New DataReport
        litkeungan6.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_lapPosisiKeu("Neraca", "konstruksi", "BRW_Dambha", "N")
        dsData = oRptDKI.getFAK_lapPosisiKeu("Neraca", product, brwcode, "N")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_Neraca.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_Neraca.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan6.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_RekonsulHartaTetap()
        Dim oXMLReport As New DataReport
        litkeungan7.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_RekonHartaTetap("Rekonsiliasi", "konstruksi", "BRW_Dambha", "N")
        dsData = oRptDKI.getFAK_RekonHartaTetap("Rekonsiliasi", product, brwcode, "N")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_RekonsulHartaTetap.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_RekonsulHartaTetap.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan7.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_ArusKas()
        Dim oXMLReport As New DataReport
        litkeungan8.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_ArusKas("Pernyataan_pengadaan_kas", "konstruksi", "BRW_Dambha", "N")
        dsData = oRptDKI.getFAK_ArusKas("Pernyataan_pengadaan_kas", product, brwcode, "N")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_ArusKas.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_ArusKas.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan8.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            ' ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_AsumsiProyeksiKeu()
        Dim oXMLReport As New DataReport
        litkeungan9.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFAK_AsumsiProyeksiKeu()
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_AsumsiProyeksiKeu.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_AsumsiProyeksiKeu.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan9.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_ProyeksiIktisarKeu()
        Dim oXMLReport As New DataReport
        litkeungan10.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_Proyeksiikhtisarkeu("Rasio", "konstruksi", "BRW_Dambha", "Ikhtisar Keuangan", "Y")
        dsData = oRptDKI.getFAK_Proyeksiikhtisarkeu("Rasio", product, brwcode, "Ikhtisar Keuangan", "Y")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_ProyeksiIktisarKeu.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_ProyeksiIktisarKeu.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan10.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_ProyeksiLabaRugi()
        Dim oXMLReport As New DataReport
        litkeungan11.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_ProyeksiLabaRugi("Laba_rugi", "konstruksi", "BRW_Dambha", "Y")
        dsData = oRptDKI.getFAK_ProyeksiLabaRugi("Laba_rugi", product, brwcode, "Y")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_ProyeksiLabaRugi.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_ProyeksiLabaRugi.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan11.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_ProyeksiLapKeu()
        Dim oXMLReport As New DataReport
        litkeungan12.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_ProyeksiLapPosisiKeu("Neraca", "konstruksi", "BRW_Dambha", "", "Y")
        dsData = oRptDKI.getFAK_ProyeksiLapPosisiKeu("Neraca", product, brwcode, "", "Y")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_ProyeksiLapKeu.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_ProyeksiLapKeu.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan12.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub

    Public Sub Create_ProyeksiCashFlow()
        Dim oXMLReport As New DataReport
        litkeungan13.Text = ""
        Dim dsData As DataSet
        'dsData = oRptDKI.getFAK_ProyeksiCashFlow("Proyeksi_Cashflow", product, brwcode, "", "Y")
        dsData = oRptDKI.getFAK_ProyeksiCashFlowByYear(brwcode)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FAK_ProyeksiCashFlow.xml")
            Dim filePath As String = Server.MapPath("~/xml/FAK/FAK_ProyeksiCashFlow.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litkeungan13.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    '-- update
    Sub appendFAK()
        Dim dr As DataRow
        Dim ds As DataSet
        With New lReportDKI
            ds = .get_AppenFIA(appno, "FAK")
            For index = 0 To ds.Tables(0).Rows.Count - 1


                If (index + 1) = 1 Then
                    txtmsg1.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg1.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 4 Then
                    txtmsg2.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg2.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 6 Then
                    txtmsg3.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg3.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 7 Then
                    txtmsg4.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg4.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 11 Then
                    txtmsg5.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg5.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 9 Then
                    txtmsg6.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg6.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 10 Then
                    txtmsg7.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg7.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 5 Then
                    txtmsg8.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg8.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 8 Then
                    txtmsg9.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg9.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 12 Then
                    txtmsg10.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg10.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
                ElseIf (index + 1) = 13 Then
                    txtmsg11.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg11.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
                ElseIf (index + 1) = 14 Then
                    txtmsg12.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtmsg12.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 2 Then
                    txtauditor.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtauditor.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                ElseIf (index + 1) = 3 Then
                    txtopini.Value = ds.Tables(0).Rows(index).Item("pak_desc")
                    txtopini.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code")) '
                End If
            Next

        End With
    End Sub
    Sub UpdateReport(ByVal gettxtmsg1, ByVal gettxtmsg2, ByVal gettxtmsg3, ByVal gettxtmsg4, ByVal gettxtmsg5, ByVal gettxtmsg6, ByVal gettxtmsg7, ByVal gettxtmsg8, ByVal gettxtmsg9,
                     ByVal gettxtmsg10, ByVal gettxtmsg11, ByVal gettxtmsg12, ByVal gettxtmsg13, ByVal gettxtmsg14)
        Dim ds As DataSet
        With New lReportDKI
            ds = .UpdateReportFAK(appno, gettxtmsg1(1), gettxtmsg1(0))
            ds = .UpdateReportFAK(appno, gettxtmsg2(1), gettxtmsg2(0))
            ds = .UpdateReportFAK(appno, gettxtmsg3(1), gettxtmsg3(0))
            ds = .UpdateReportFAK(appno, gettxtmsg4(1), gettxtmsg4(0))
            ds = .UpdateReportFAK(appno, gettxtmsg5(1), gettxtmsg5(0))
            ds = .UpdateReportFAK(appno, gettxtmsg6(1), gettxtmsg6(0))
            ds = .UpdateReportFAK(appno, gettxtmsg7(1), gettxtmsg7(0))
            ds = .UpdateReportFAK(appno, gettxtmsg8(1), gettxtmsg8(0))
            ds = .UpdateReportFAK(appno, gettxtmsg9(1), gettxtmsg9(0))
            ds = .UpdateReportFAK(appno, gettxtmsg10(1), gettxtmsg10(0))
            ds = .UpdateReportFAK(appno, gettxtmsg11(1), gettxtmsg11(0))
            ds = .UpdateReportFAK(appno, gettxtmsg12(1), gettxtmsg12(0))
            ds = .UpdateReportFAK(appno, gettxtmsg13(1), gettxtmsg13(0))
            ds = .UpdateReportFAK(appno, gettxtmsg14(1), gettxtmsg14(0))

        End With
    End Sub
    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim gettxtmsg1() As String = {txtmsg1.InnerText, txtmsg1.Attributes("tagcode")}
        Dim gettxtmsg2() As String = {txtmsg2.InnerText, txtmsg2.Attributes("tagcode")}
        Dim gettxtmsg3() As String = {txtmsg3.InnerText, txtmsg3.Attributes("tagcode")}
        Dim gettxtmsg4() As String = {txtmsg4.InnerText, txtmsg4.Attributes("tagcode")}
        Dim gettxtmsg5() As String = {txtmsg5.InnerText, txtmsg5.Attributes("tagcode")}
        Dim gettxtmsg6() As String = {txtmsg6.InnerText, txtmsg6.Attributes("tagcode")}
        Dim gettxtmsg7() As String = {txtmsg7.InnerText, txtmsg7.Attributes("tagcode")}
        Dim gettxtmsg8() As String = {txtmsg8.InnerText, txtmsg8.Attributes("tagcode")}
        Dim gettxtmsg9() As String = {txtmsg9.InnerText, txtmsg9.Attributes("tagcode")}
        Dim gettxtmsg10() As String = {txtmsg10.InnerText, txtmsg10.Attributes("tagcode")}
        Dim gettxtmsg11() As String = {txtmsg11.InnerText, txtmsg11.Attributes("tagcode")}
        Dim gettxtmsg12() As String = {txtmsg12.InnerText, txtmsg12.Attributes("tagcode")}
        Dim gettxtmsg13() As String = {txtauditor.Value, txtauditor.Attributes("tagcode")}
        Dim gettxtmsg14() As String = {txtopini.InnerHtml, txtopini.Attributes("tagcode")}

        UpdateReport(gettxtmsg1, gettxtmsg2, gettxtmsg3, gettxtmsg4, gettxtmsg5, gettxtmsg6, gettxtmsg7,
                     gettxtmsg8, gettxtmsg9, gettxtmsg10, gettxtmsg11, gettxtmsg12, gettxtmsg13, gettxtmsg14)

        exportWord()

    End Sub
    Private Sub exportWord()
        Response.Clear()
        Response.Buffer = True

        Dim fileName As String = "Formulir_Analisa_Keungan"
        Dim css As String = ""

        Dim strMsg1 As String
        strMsg1 = txtmsg1.Value
        strMsg1 = strMsg1.Replace("-", "<br />-")
        Dim strMsg2 As String
        strMsg2 = txtmsg2.Value
        strMsg2 = strMsg2.Replace("-", "<br />-")
        Dim strMsg3 As String
        strMsg3 = txtmsg3.Value
        strMsg3 = strMsg3.Replace("-", "<br />-")
        Dim strMsg4 As String
        strMsg4 = txtmsg4.Value
        strMsg4 = strMsg4.Replace("-", "<br />-")
        Dim strMsg5 As String
        strMsg5 = txtmsg5.Value
        strMsg5 = strMsg5.Replace("-", "<br />-")
        Dim strMsg6 As String
        strMsg6 = txtmsg6.Value
        strMsg6 = strMsg6.Replace("-", "<br />-")
        Dim strMsg7 As String
        strMsg7 = txtmsg7.Value
        strMsg7 = strMsg7.Replace("-", "<br />-")
        Dim strMsg8 As String
        strMsg8 = txtmsg8.Value
        strMsg8 = strMsg8.Replace("-", "<br />-")
        Dim strMsg9 As String
        strMsg9 = txtmsg9.Value
        strMsg9 = strMsg9.Replace("-", "<br />-")
        Dim strMsg10 As String
        strMsg10 = txtmsg10.Value
        strMsg10 = strMsg10.Replace("-", "<br />-")
        Dim strMsg11 As String
        strMsg11 = txtmsg11.Value
        strMsg11 = strMsg11.Replace("-", "<br />-")
        Dim strMsg12 As String
        strMsg12 = txtmsg12.Value
        strMsg12 = strMsg12.Replace("-", "<br />-")
        Dim strMsg13 As String
        strMsg13 = txtopini.Value
        strMsg13 = strMsg13.Replace("-", "<br />-")
        Dim html As String = ""
        Dim signPdf As String = SignaturePDF("FAK", appno)
       
        html += "<table id='tblRpt' style='clear:both; margin:0 auto; padding:0; width:100%;'>"
        html += "<tbody>"
        html += "<tr>"
        html += "<td align='center'><span id='lbljudul1' style='font-weight:bold;font-size:14px;width:99%;'>" & lbljudul1.Text & "</span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader1' style='font-size:14px;font-weight:bold;'>" & lblheader1.Text & "</span>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='background-color:#F6F5F0;border:1px solid #000000;'>" & litkeungan1.Text
        html += "<span id='lblAudtor' style='font-size:12px;font-weight:bold;text-align:center;width:32%;'>" & lblAudtor.Text & "</span>"
        html += txtauditor.Value
        html += "<div><span id='lblopini' valign='top' style='font-size:12px;font-weight:bold;'>" & lblopini.Text & "</span><div style='border: #000000 1px solid;height: 60px;width:100px;'>" & strMsg13.ToString & "</div></div>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='Label1' style='font-size:12px;font-weight:bold;text-align:center;'>IKHTISAR KEUANGAN</span>"
        html += litkeungan2.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg1.ToString & "</div>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg2.ToString & "</div>"
        html += litkeungan3.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height:100px;width:100%;background-color:yello;'>" & strMsg3.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader2' style='font-size:14px;font-weight:bold;'>" & lblheader2.Text & "</span>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblpernyataanlaba' style='font-size:12px;width:99%;font-weight:bold;text-align:center;'>" & lblpernyataanlaba.Text & "</span>"
        html += litkeungan4.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg4.ToString & "</div><br /><br />"

        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader3' style='font-size:14px;font-weight:bold;'>" & lblheader3.Text & "</span>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblneraca' style='font-size:12px;font-weight:bold;'>" & lblneraca.Text & "</span>"
        html += litkeungan6.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg5.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader5' style='font-size:14px;font-weight:bold;'>" & lblheader5.Text & "</span>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader6' style='font-size:12px;font-weight:bold;text-align:center'>" & lblheader6.Text & "</span>"
        html += litkeungan7.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg6.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader8' style='font-size:14px;font-weight:bold;'>" & lblheader8.Text & "</span>"
        html += litkeungan8.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg7.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader9' style='font-size:14px;font-weight:bold;'><u>" & lblheader9.Text & "</u></span>"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader10' style='font-size:14px;font-weight:bold;margin-left: 1cm'>" & lblheader10.Text & "</span>"
        html += litkeungan9.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader11' style='font-size:14px;font-weight:bold;'>" & lblheader11.Text & "</span>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg8.ToString & "</div>"
        html += litkeungan10.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg9.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader12' style='font-size:14px;font-weight:bold;'>" & lblheader12.Text & "</span>"
        html += litkeungan11.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg10.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader13' style='font-size:14px;font-weight:bold;'>" & lblheader13.Text & "</span>"
        html += litkeungan12.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg11.ToString & "</div><br /><br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<span id='lblheader14' style='font-size:14px;font-weight:bold;'>" & lblheader14.Text & "</span>"
        html += litkeungan13.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='border: #000000 1px solid;height: 100px;width:100%;'>" & strMsg12.ToString & "</div>"
        html += "</td>"
        html += "</tr>"
        html += "</tbody></table>"
        html += signPdf

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
    ''--END---

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("FAK", appno)
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