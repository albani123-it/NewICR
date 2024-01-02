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
Imports System.Net
Imports System.Security.AccessControl
Imports System.Xml.Xsl
'Imports DocumentFormat.OpenXml.Packaging
'Imports DocumentFormat.OpenXml.Wordprocessing
Imports Microsoft.Win32
Imports System.Reflection
Imports iTextSharp.tool.xml

Public Class Frm_FormulirInfoAgunan
    Inherits System.Web.UI.Page
    Protected app_no As String = ""
    Dim oAgu As New lAgunan
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    Dim oRptDKI As New lReportDKI
    Dim oGen As New lGeneral
    Dim oApp As New lApps
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim myTbl As New DataTable
#End Region
    Private Property imgPath As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        cekProduct(appno)
        If Not IsPostBack Then
            fillDdl()
            appendFIA()
            'updateReport()
        End If
        'If Session("app_no") IsNot Nothing And Not IsPostBack Then
        '    checkAppByBrwCode(Session("app_no"))

        'End If
    End Sub
    'Sub checkAppByBrwCode(ByVal brwcode)

    '    Dim ds As DataSet
    '    ds = oApp.checkAppByBrwCode(brwcode)

    '    If ds.Tables(0).Rows.Count > 0 Then
    '        appno = ds.Tables(0).Rows(0).Item("brwcode")
    '    End If

    'End Sub
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
        Dim month As String = ""
        cekProduct(appno)

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAgu.getDataBorrowerType(appno)
        dt = ds.Tables(0)

        If (dt.Rows(0)(5).ToString() = "5") Or (dt.Rows(0)(5).ToString() = "12") Then
            Create_Table()
            Create_Table_A()
            Create_Table_B()
            Create_Table_C()
            Create_Table_D()
            Create_Table_E()
            Create_Table_F()
            Create_Signature()
        Else
            Create_Table()
            Create_Table_A()
            Create_Table_B()
            Create_Table_C()
            Create_Table_D()
            Create_Table_E()
            Create_Table_F()
            Create_Table_G()
            Create_Table_H()
            Create_Signature()
        End If

        'ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub
    Sub cekProduct(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkGeneral(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            'product = dt.Rows(0).Item("lblProductType")
            'brwcode = dt.Rows(0).Item("lsg_brw_code")
            product = IIf(IsDBNull(dt.Rows(0).Item("lblProductType")), "", dt.Rows(0).Item("lblProductType"))
            brwcode = IIf(IsDBNull(dt.Rows(0).Item("lsg_brw_code")), "", dt.Rows(0).Item("lsg_brw_code"))
        End If
    End Sub
    '--update baru
    Public Sub Create_Table()
        Dim oXMLReport As New DataReport
        littgl.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_getInfoAgunan(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            littgl.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    '--end
    Public Sub Create_Table_A()
        Dim oXMLReport_A As New DataReport
        litagunan1.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_getInfoAgunan_A(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_A.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_A.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan1.Text = oXMLReport_A.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_B()
        Dim oXMLReport_B As New DataReport
        litagunan2.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_RincianAgunan(appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_B.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_B.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan2.Text = oXMLReport_B.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_C()
        Dim oXMLReport_C As New DataReport
        litagunan3.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_Foto_PetaReport(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_C.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_C.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan3.Text = oXMLReport_C.mainLoop(nodes, dtData, dsData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_D()
        Dim oXMLReport_C As New DataReport
        litagunan4.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_PenukaranAgunan(appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_D.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_D.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan4.Text = oXMLReport_C.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_E()
        Dim oXMLReport_E As New DataReport
        litagunan5.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_PengikatanAgunan(appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_E.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_E.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan5.Text = oXMLReport_E.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_F()
        Dim oXMLReport_F As New DataReport
        litagunan6.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFIA_PengiktanAgunanOut(appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_F.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_F.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan6.Text = oXMLReport_F.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_G()
        Dim oXMLReport_G As New DataReport
        litagunan7.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFRN_SecurityCoverageRatio(appno, "1")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_G.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_G.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan7.Text = oXMLReport_G.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub

    Public Sub Create_Table_H()
        Dim oXMLReport_H As New DataReport
        litagunan8.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFRN_SecurityCoverageRatio(appno, "2")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            'Dim filePath As String = Server.MapPath("~/xml/FIA_H.xml")
            Dim filePath As String = Server.MapPath("~/xml/FIA/FIA_H.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litagunan8.Text = oXMLReport_H.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    '----Update---'
    Sub updateReport(ByVal gettxtmsg1() As String, ByVal gettxtmsg2() As String, ByVal gettxtmsg3() As String, ByVal gettxtmsg4() As String, ByVal gettxtmsg5() As String, ByVal gettxtmsg6() As String,
                     ByVal gettxtmsg7() As String, ByVal gettxtmsg8() As String, ByVal gettxtmsg9() As String, ByVal gettxtmsg10() As String, ByVal gettxtmsg11() As String, ByVal gettxtmsg12() As String,
                     ByVal gettxtmsg13() As String, ByVal gettxtmsg14() As String, ByVal gettxtmsg15() As String, ByVal gettxtmsg16() As String, ByVal gettxtmsg17() As String, ByVal gettxtmsg18() As String)
        Dim dt As DataTable
        Dim ds As DataSet
        With New lReportDKI
            ds = .UpdateReportFIA(appno, gettxtmsg1(1), gettxtmsg1(0))
            ds = .UpdateReportFIA(appno, gettxtmsg2(1), gettxtmsg2(0))
            ds = .UpdateReportFIA(appno, gettxtmsg3(1), gettxtmsg3(0))
            ds = .UpdateReportFIA(appno, gettxtmsg4(1), gettxtmsg4(0))
            ds = .UpdateReportFIA(appno, gettxtmsg5(1), gettxtmsg5(0))
            ds = .UpdateReportFIA(appno, gettxtmsg6(1), gettxtmsg6(0))
            ds = .UpdateReportFIA(appno, gettxtmsg7(1), gettxtmsg7(0))
            ds = .UpdateReportFIA(appno, gettxtmsg8(1), gettxtmsg8(0))
            ds = .UpdateReportFIA(appno, gettxtmsg9(1), gettxtmsg9(0))
            ds = .UpdateReportFIA(appno, gettxtmsg10(1), gettxtmsg10(0))
            ds = .UpdateReportFIA(appno, gettxtmsg11(1), gettxtmsg11(0))
            ds = .UpdateReportFIA(appno, gettxtmsg12(1), gettxtmsg12(0))
            ds = .UpdateReportFIA(appno, gettxtmsg13(1), gettxtmsg13(0))
            ds = .UpdateReportFIA(appno, gettxtmsg14(1), gettxtmsg14(0))
            ds = .UpdateReportFIA(appno, gettxtmsg15(1), gettxtmsg15(0))
            ds = .UpdateReportFIA(appno, gettxtmsg16(1), gettxtmsg16(0))
            ds = .UpdateReportFIA(appno, gettxtmsg17(1), gettxtmsg17(0))
            ds = .UpdateReportFIA(appno, gettxtmsg18(1), gettxtmsg18(0))

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
        Dim gettxtmsg13() As String = {txtmsg13.InnerText, txtmsg13.Attributes("tagcode")}
        Dim gettxtmsg14() As String = {txtmsg14.InnerText, txtmsg14.Attributes("tagcode")}
        Dim gettxtmsg15() As String = {txtmsg15.InnerText, txtmsg15.Attributes("tagcode")}
        Dim gettxtmsg16() As String = {txtmsg16.InnerText, txtmsg16.Attributes("tagcode")}
        Dim gettxtmsg17() As String = {txtmsg17.InnerText, txtmsg17.Attributes("tagcode")}
        Dim gettxtmsg18() As String = {txtmsg18.InnerText, txtmsg18.Attributes("tagcode")}

        'If ddlExportType.SelectedValue = "Word" Then
        updateReport(gettxtmsg1, gettxtmsg2, gettxtmsg3, gettxtmsg4, gettxtmsg5, gettxtmsg6, gettxtmsg7, gettxtmsg8, gettxtmsg9, gettxtmsg10, gettxtmsg11, gettxtmsg12, gettxtmsg13, gettxtmsg14, gettxtmsg15, gettxtmsg16, gettxtmsg17, gettxtmsg18)
        exportWord()
        'Catch ex As Exception
        '    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('information is not complete.');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
        'End Try

        'End If
    End Sub
    Sub appendFIA()
        Dim dr As DataRow
        Dim ds As DataSet
        With New lReportDKI
            ds = .get_AppenFIA(appno, "FIA")
            'For index = 0 To ds.Tables(0).Rows.Count - 1

            '    If (index + 1) = 1 Then
            '        txtmsg1.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg1.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 2 Then
            '        txtmsg2.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg2.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 3 Then
            '        txtmsg3.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg3.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 4 Then
            '        txtmsg4.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg4.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 5 Then
            '        txtmsg5.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg5.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 6 Then
            '        txtmsg6.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg6.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 7 Then
            '        txtmsg7.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg7.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 8 Then
            '        txtmsg8.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg8.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 9 Then
            '        txtmsg9.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg9.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 10 Then
            '        txtmsg10.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg10.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 11 Then
            '        txtmsg11.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg11.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 12 Then
            '        txtmsg12.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg12.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 13 Then
            '        txtmsg13.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg13.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 14 Then
            '        txtmsg14.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg14.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 15 Then
            '        txtmsg15.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg15.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 16 Then
            '        txtmsg16.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg16.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 17 Then
            '        txtmsg17.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg17.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    ElseIf (index + 1) = 18 Then
            '        txtmsg18.Value = ds.Tables(0).Rows(index).Item("pak_desc")
            '        txtmsg18.Attributes.Add("tagcode", ds.Tables(0).Rows(index).Item("pak_code"))
            '    End If
            'Next

            For xyz = 0 To ds.Tables(0).Rows.Count - 1

                If (xyz + 1) = 1 Then
                    txtmsg1.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg1.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 2 Then
                    txtmsg2.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg2.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 3 Then
                    txtmsg3.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg3.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 4 Then
                    txtmsg4.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg4.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 5 Then
                    txtmsg5.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg5.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 6 Then
                    txtmsg6.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg6.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 7 Then
                    txtmsg7.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg7.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 8 Then
                    txtmsg8.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg8.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 9 Then
                    txtmsg9.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg9.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 10 Then
                    txtmsg10.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg10.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 11 Then
                    txtmsg11.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg11.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 12 Then
                    txtmsg12.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg12.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 13 Then
                    txtmsg13.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg13.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 14 Then
                    txtmsg14.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg14.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 15 Then
                    txtmsg15.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg15.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 16 Then
                    txtmsg16.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg16.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 17 Then
                    txtmsg17.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg17.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                ElseIf (xyz + 1) = 18 Then
                    txtmsg18.Value = ds.Tables(0).Rows(xyz).Item("pak_desc")
                    txtmsg18.Attributes.Add("tagcode", ds.Tables(0).Rows(xyz).Item("pak_code"))
                End If
            Next

        End With
    End Sub

    Private Sub exportWord()
        Response.Clear()
        Response.Buffer = True
        Dim fileName As String = "Formulir_Info_Agunan"
        Dim signPdf As String = SignaturePDF("FIA", appno)

        Dim dtble As DataTable
        dtble = binimages(appno, "FIA")
        Dim strmsg18 As String
        strmsg18 = txtmsg18.Value
        strmsg18 = strmsg18.Replace("-", "<br>-")
        Dim strmsg17 As String
        strmsg17 = txtmsg17.Value
        strmsg17 = strmsg17.Replace("-", "<br>-")
        Dim html As String = ""
        html &= "<table align='center' width='80%'>"
        html &= "<tr><td colspan='2'><p align='center'><b>" & lbljudul1.Text & "</b></p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 20px 0px 0px 0px;width:90%'>"
        html &= "<tr><td colspan='2'>" & littgl.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' cellpadding='10' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><b>" & lblheader1.Text & "</b></td></tr>"
        html &= "<tr><td colspan='2'  >" & litagunan1.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' cellpadding='10' style='margin:0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2' ><b>" & lblheader2.Text & "</b></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><b>" & lblket1.Text & "</b></td></tr>"
        html &= "<tr><td colspan='2' ><p  style='text-align:justify;font-size:14px'>" & txtmsg1.Value & "</p></td></tr>"
        html &= "<tr><td colspan='2' >" & litagunan2.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg2.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg3.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><b>" & lblket2.Text & "</b></td></tr>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg4.Value & "</p></td></tr>"
        html &= "</table>"
        For xyz = 0 To dtble.Rows.Count - 1
            html &= "<table align='center' width='90%' style='border-spacing:initial;'>"
            html &= "<tr><td style='width:80%' >'<p colspan='2' align='center'>" & dtble.Rows(xyz).Item("keterangan").ToString() & "</p><p align='center'> <img  width='65%' height='65%' src='" & dtble.Rows(xyz).Item("images").ToString() & "'></img></p> </td></tr> "
            html &= "</table>"
        Next
        html &= " <br />"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><b>" & lblket3.Text & "</b></td></tr>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg5.Value & "</p></td></tr>"
        html &= "<tr><td colspan='2'>" & litagunan4.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg6.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg7.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg8.Value & "</p></td></tr>"
        html &= "</table>"
        html &= " <br />"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><b>" & lblheader3.Text & "</b></td></tr>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg9.Value & "</p></td></tr>"
        html &= "<tr><td colspan='2'>" & litagunan5.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg10.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg11.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg12.Value & "</p></td></tr>"
        html &= "<tr><td colspan='2'>" & litagunan6.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg13.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p align='justify'><b>" & lblheader4.Text & "</b></p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p align='justify'><b>" & lblket4.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'>" & litagunan7.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg14.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p align='justify'><b>" & lblket5.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'>" & litagunan8.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p align='justify'><b>" & lblket6.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg15.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & txtmsg16.Value & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 0px 0px 20px 0px;width:90%'>"
        html &= "<tr><td colspan='2'><p align='justify'><b>" & lblket7.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'><p style='font-size:14px'>" & strmsg17.ToString & "</p></td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' style='margin: 3px 2px 3px 2px;width:90%'>"
        html &= "<tr><td colspan='2'><p align='justify'><b>" & lblket8.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'><p style='font-size:14px'>" & strmsg18.ToString & "</p></td></tr>"
        html &= "</table>"
        html &= signPdf

        Dim pdfDoc As New Document(PageSize.A4, 20.0F, 20.0F, 20.0F, 10.0F)
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



        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.openxmlformatsofficedocument.wordprocessingml.document"
        'Response.ContentEncoding = System.Text.Encoding.UTF8
        'Response.AddHeader("Content-Disposition", "attachment;filename=Formulir Info Agunan.doc")
        'Response.Charset = ""
        'Me.EnableViewState = False
        'Dim oStringWriter As StringWriter = New StringWriter
        'Dim oHtmlTextWriter As HtmlTextWriter = New HtmlTextWriter(oStringWriter)
        'Me.tblFia.RenderControl(oHtmlTextWriter)
        'Response.Write(oStringWriter.ToString())
        ''Response.Output.Write(html)
        'Response.Flush()
        'Response.End()

    End Sub
    Public Function binimages(ByVal appno, ByVal type)
        Dim tmpimg As String
        litagunan3.Visible = True
        Dim ds As DataSet
        Dim dt As DataTable
        'Dim dr As DataRow

        myTbl.Columns.Add("images")
        myTbl.Columns.Add("keterangan")

        'Dim coba As String
        ds = oRptDKI.getFIA_Foto_PetaReport(usrLogin, appno)
        dt = ds.Tables(0)

        Dim path As String = String.Empty

        For xyz = 0 To dt.Rows.Count - 1

            tmpimg = (dt.Rows(xyz).Item("ldad_image_nama").ToString()).Remove(0, 3)

            'Dim x() = tmpimg.Split("/")
            'Dim y = x(x.Length - 1).ToString()

            'Dim al As String = tmpimg.Replace("/", "\")
            'Dim loc As String = HttpContext.Current.Request.PhysicalApplicationPath + al
            'Using client As New WebClient()
            '    client.DownloadFile(loc, path + y)
            '    client.Dispose()
            'End Using

            'Dim imgPath1 As String = HttpContext.Current.Request.PhysicalApplicationPath + al
            Dim imgPath1 As String = ""
            imgPath1 = System.Configuration.ConfigurationManager.AppSettings("pathImg")

            imgPath1 = imgPath1 + tmpimg

            'Return imgPath1
            Dim row As DataRow = myTbl.NewRow()
            row("images") = imgPath1
            'row("images") = path + y
            row("keterangan") = dt.Rows(xyz).Item("ldad_image_keterangan").ToString()
            myTbl.Rows.Add(row)

        Next





        Return myTbl
    End Function

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    '---END---

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("FIA", appno)
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