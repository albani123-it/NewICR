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

Public Class IkhtisarPengusulanKredit
    Inherits System.Web.UI.Page
    'Public appNo As String = "000153016072600001"
    'Public brwCode As String = "CO16072600017"
    Dim msgErr As String = ""

#Region " Global Variable "
    Dim usrLogin As String
    Dim oRptDKI As New lReport
    Dim oRptDKI2 As New lReportDKI
    Dim oGen As New lGeneral
    Dim appno As String
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        If Not IsPostBack Then
            fillDdl()
            textAreaIPK()

            'appendSPPK()
            'appendEfektifKMK1()
            'appendEfektifKMK2()
            'appendSwiteChable()
            'appendSyaratLain()
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

        Try
            msgErr = "Create_Table_A - Sp_GetIPK_Header : "
            Create_Table_A()
            msgErr = "Create_Table_B - Sp_GetIPK_Header : "
            Create_Table_B()
            msgErr = "Create_Table_C - SP_getIPK_Keyperson : "
            Create_Table_C()
            msgErr = "Create_Table_D - getIPK_BrwRatting : "
            Create_Table_D()
            msgErr = "Create_Table_Profil - getIPK_BMPKreport : "
            Create_Table_Profil()
            msgErr = "Create_Table_E - SpIPK_getTotalKebutuhanKredit : "
            Create_Table_E()
            msgErr = "Create_Table_F - getIPK_rasioRpt : "
            Create_Table_F()
            msgErr = "Create_Table_G - sp_rptInfoAsumsiRatingInfo : "
            Create_Table_G()
            
            msgErr = "Create_KMKAgunan - getIPK_agunan : "
            Create_KMKAgunan()
            msgErr = "Create_KMKAgunanFixed - getIPK_agunan : "
            Create_KMKAgunanFixed()
            msgErr = "Create_KMKAgunanFixedTunai - getIPK_agunan : "
            Create_KMKAgunanFixedTunai()

            Create_Signature()
            msgErr = "Create_KetentuanKMK1 - Sp_getkmkI : "
            'Create_Table_H()
            Create_KetentuanKMK1()

            checkApproval(appno)

        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & msgErr & ex.Message & " App No :" & appno & "');"
        End Try
        'Create_KetentuanKMK2()
        'ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub
    Public Sub Create_Table_A()
        Dim oXMLReport As New DataReport
        litIPK1.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_Header(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)

        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_A.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK1.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_B()
        Dim oXMLReport_B As New DataReport
        litIPK2.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_Header(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_B.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK2.Text = oXMLReport_B.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_C()
        Dim oXMLReport_C As New DataReport
        litIPK3.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_Keyperson(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_C.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK3.Text = oXMLReport_C.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_D()
        'Dim oXMLReport As New DataReport
        'litIPK4.Text = ""
        'Dim dsData As DataSet
        'dsData = oRptDKI.getIPK_ProfilRisiko(usrLogin, appno)
        'Dim dtData As DataTable
        'dtData = dsData.Tables(0)
        'If dtData.Columns.Count <> 0 Then
        '    Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_D.xml")
        '    Dim xmlDoc As New System.Xml.XmlDocument()
        '    xmlDoc.Load(filePath)
        '    Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
        '    litIPK4.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
        '    btnExport.Visible = True
        '    'ddlExportType.Visible = True
        Dim oXMLReport As New DataReport
        litIPK4.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_ProfilRisiko(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_D.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK4.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True

        End If
        'End If
    End Sub
    Public Sub Create_Table_Profil()
        Dim oXMLReport As New DataReport
        litPerofilRisiko.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_ProfilRisikoII(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_ProfilRisiko.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litPerofilRisiko.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_E()
        Dim oXMLReport As New DataReport
        litIPK5.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_fasilitaskredit(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_E.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK5.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_F()
        Dim oXMLReport As New DataReport
        litIPK6.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_tingkatrasio(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_F.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK6.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_G()
        Dim oXMLReport As New DataReport
        litIPK7.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getFRN_AsumsiRetinginfo(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_G.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litIPK7.Text = oXMLReport.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    'Public Sub Create_Table_H()
    '    Dim oXMLReport As New DataReport
    '    litIPK8.Text = ""
    '    Dim dsData As DataSet
    '    dsData = oRptDKI.getFRN_AsumsiRetinginfo(usrLogin, appNo)
    '    Dim dtData As DataTable
    '    dtData = dsData.Tables(0)
    '    If dtData.Columns.Count <> 0 Then
    '        Dim filePath As String = Server.MapPath("~/xml/IPK/IPK_H.xml")
    '        Dim xmlDoc As New System.Xml.XmlDocument()
    '        xmlDoc.Load(filePath)
    '        Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
    '        litIPK8.Text = oXMLReport.mainLCR(nodes, dtData)
    '        btnExport.Visible = True
    '        'ddlExportType.Visible = True
    '    End If
    'End Sub
    Public Sub Create_KetentuanKMK1()
        Dim oXMLReportkmk As New DataReport
        litKetentuanKMK1.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_KMKI(usrLogin, appno)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/KetentuanKMK1.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litKetentuanKMK1.Text = oXMLReportkmk.mainLCR(nodes, dtData)
            btnExport.Visible = True

        End If
    End Sub
    Public Sub Create_KMKAgunan()
        Dim oXMLReport As New DataReport
        litKetentuanKMKAgunan.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_KMKagunan(usrLogin, appno, "REFCOLFIXED")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/AgunanFixednonAsset.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litKetentuanKMKAgunan.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True

        End If
    End Sub
    Public Sub Create_KMKAgunanFixed()
        Dim oXMLReport As New DataReport
        litKetentuanKMKFixed.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_KMKagunan(usrLogin, appno, "REFCOLNONFIXED")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/AgunanFixedAsset.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litKetentuanKMKFixed.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True

        End If
    End Sub
    Public Sub Create_KMKAgunanFixedTunai()
        Dim oXMLReport As New DataReport
        litKetentuanKMKFixedTunai.Text = ""
        Dim dsData As DataSet
        dsData = oRptDKI.getIPK_KMKagunan(usrLogin, appno, "REFCOLTUNAI")
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/IPK/AgunanFixedTunai.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            litKetentuanKMKFixedTunai.Text = oXMLReport.mainLoop(nodes, dtData, dsData)
            btnExport.Visible = True

        End If
    End Sub
    Sub textAreaIPK()
        Try
            Dim ds As DataSet
            Dim dt As DataTable

            ds = oRptDKI.getDataIPK(usrLogin, appno)
            dt = ds.Tables(0)

            txtPerjanjianKredit.Text = dt.Rows(0).Item("rpk_perjanjian_kredit").ToString()
            txtEfektifKMK1.Text = dt.Rows(0).Item("rpk_efektif_kmk").ToString()
            txtPenarikanKMK2.Text = dt.Rows(0).Item("rpk_penarikan_kmk").ToString()
            txtFasilitasKMK.Text = dt.Rows(0).Item("rpk_fasilitas_kmk").ToString()
            txtSyaratLainnya.Text = dt.Rows(0).Item("rpk_syarat_lainnya").ToString()
            txtpejabat.Text = dt.Rows(0).Item("rpk_pejabat_pemutus").ToString()
            txtkomite.Text = dt.Rows(0).Item("rpk_komite_kredit").ToString()
            txtaudit.Value = dt.Rows(0).Item("rpk_audit_report").ToString()
            txtAppraisel.Value = dt.Rows(0).Item("rpk_appraisel").ToString()
            txtmarginal.Value = dt.Rows(0).Item("rpk_marginal").ToString()
            txtother.Value = dt.Rows(0).Item("rpk_other").ToString()
        Catch ex As Exception

        End Try
    End Sub

    'Sub appendSPPK()
    '    Dim dr As DataRow
    '    Dim ds As DataSet
    '    With New lReport
    '        Dim msg As String = ""
    '        ds = .get_AppenIPK("BRW_Dambha", "SPPK")
    '        For index = 0 To ds.Tables(0).Rows.Count - 1

    '            If (index + 1) = 1 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 2 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 3 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 4 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 5 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 9 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 7 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 10 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 6 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 8 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            End If
    '        Next

    '        txtPerjanjianKredit.Text = msg
    '    End With
    'End Sub
    'Sub appendEfektifKMK1()
    '    Dim dr As DataRow
    '    Dim ds As DataSet
    '    With New lReport
    '        ds = .get_AppenIPK("BRW_Dambha", "KMK")
    '        Dim msg As String = ""
    '        For index = 0 To ds.Tables(0).Rows.Count - 1
    '            If (index + 1) = 1 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 2 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 3 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 4 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 5 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            End If
    '        Next

    '        txtEfektifKMK1.Text = msg
    '    End With
    'End Sub

    'Sub appendEfektifKMK2()
    '    Dim dr As DataRow
    '    Dim ds As DataSet
    '    With New lReport
    '        ds = .get_AppenIPK("BRW_Dambha", "Penarikan")
    '        Dim msg As String = ""
    '        For index = 0 To ds.Tables(0).Rows.Count - 1
    '            If (index + 1) = 1 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 2 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 3 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 4 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            End If
    '        Next

    '        txtPenarikanKMK2.Text = msg
    '    End With
    'End Sub

    'Sub appendSwiteChable()
    '    Dim dr As DataRow
    '    Dim ds As DataSet
    '    With New lReport
    '        ds = .get_AppenIPK("BRW_Dambha", "SwiteChable")
    '        Dim msg As String = ""
    '        For index = 0 To ds.Tables(0).Rows.Count - 1
    '            If (index + 1) = 1 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 2 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 3 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            End If
    '        Next

    '        txtFasilitasKMK.Text = msg
    '    End With
    'End Sub

    'Sub appendSyaratLain()
    '    Dim dr As DataRow
    '    Dim ds As DataSet
    '    With New lReport
    '        ds = .get_AppenIPK("BRW_Dambha", "Syarat")
    '        Dim msg As String = ""
    '        For index = 0 To ds.Tables(0).Rows.Count - 1
    '            If (index + 1) = 5 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 1 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 4 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 6 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 2 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 7 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 8 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 9 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            ElseIf (index + 1) = 10 Then
    '                msg += ds.Tables(0).Rows(index).Item("ipk_desc")
    '            End If
    '        Next
    '        txtSyaratLainnya.Text = msg
    '    End With
    'End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'If ddlExportType.SelectedValue = "Word" Then

        Dim PerjanjianKredit As String = Request.Form("txtPerjanjianKredit")
        Dim EfektifKMK As String = Request.Form("txtEfektifKMK1")
        Dim PenarikanKMK As String = Request.Form("txtPenarikanKMK2")
        Dim FasilitasKMK As String = Request.Form("txtFasilitasKMK")
        Dim SyaratLainnya As String = Request.Form("txtSyaratLainnya")
        Dim pejabat As String = Request.Form("txtpejabat")
        Dim komite As String = Request.Form("txtkomite")
        Dim audit As String = Request.Form("txtaudit")
        Dim apprasel As String = Request.Form("txtAppraisel")
        Dim marginal As String = Request.Form("txtmarginal")
        Dim other As String = Request.Form("txtother")
        Dim dt As DataTable

        dt = oRptDKI.checkCountInTableIPK(appno)

        If dt.Rows(0).Item("cRows") > 0 Then
            oRptDKI.SaveOrUpdateTextAreaIPK(appno, PerjanjianKredit, EfektifKMK, PenarikanKMK, FasilitasKMK, SyaratLainnya, pejabat, komite, audit, apprasel, marginal, other, "Update")
        Else
            oRptDKI.SaveOrUpdateTextAreaIPK(appno, PerjanjianKredit, EfektifKMK, PenarikanKMK, FasilitasKMK, SyaratLainnya, pejabat, komite, audit, apprasel, marginal, other, "Insert")
        End If

        If btnExport.Text = "Save & Export" Then
            exportWord()
        End If
        'End If
    End Sub
    Private Sub exportWord()
        Response.Clear()
        Response.Buffer = True
        Dim fileName As String = "Iktisar_Pengusulan_Kredit"

        Dim html As String = ""
        Dim signPdf As String = SignaturePDF("IPK", appno)
        Dim litScoreUpdate As String = ""
        litScoreUpdate = txtPerjanjianKredit.Text
        litScoreUpdate = litScoreUpdate.Replace("1.", "<br/>1.")
        litScoreUpdate = litScoreUpdate.Replace("2.", "<br/>2.")
        litScoreUpdate = litScoreUpdate.Replace("3.", "<br/>3.")
        litScoreUpdate = litScoreUpdate.Replace("4.", "<br/>4.")
        litScoreUpdate = litScoreUpdate.Replace("5.", "<br/>5.")
        litScoreUpdate = litScoreUpdate.Replace("6.", "<br/>6.")

        litScoreUpdate = litScoreUpdate.Replace("a.", "<br/>a.")
        litScoreUpdate = litScoreUpdate.Replace("b.", "<br/>b.")
        litScoreUpdate = litScoreUpdate.Replace("c.", "<br/>c.")
        litScoreUpdate = litScoreUpdate.Replace("d.", "<br/>d.")

        Dim litScoreUpdate1 As String
        litScoreUpdate1 = txtEfektifKMK1.Text
        litScoreUpdate1 = litScoreUpdate1.Replace("1.", "<br/>1.")
        litScoreUpdate1 = litScoreUpdate1.Replace("2.", "<br/>2.")
        litScoreUpdate1 = litScoreUpdate1.Replace("3.", "<br/>3.")
        litScoreUpdate1 = litScoreUpdate1.Replace("4.", "<br/>4.")
        litScoreUpdate1 = litScoreUpdate1.Replace("5.", "<br/>5.")
        litScoreUpdate1 = litScoreUpdate1.Replace("6.", "<br/>6.")

        litScoreUpdate1 = litScoreUpdate1.Replace("a.", "<br/>a.")
        litScoreUpdate1 = litScoreUpdate1.Replace("b.", "<br/>b.")
        litScoreUpdate1 = litScoreUpdate1.Replace("c.", "<br/>c.")
        litScoreUpdate1 = litScoreUpdate1.Replace("d.", "<br/>d.")
        Dim litScoreUpdate2 As String
        litScoreUpdate2 = txtPenarikanKMK2.Text
        litScoreUpdate2 = litScoreUpdate2.Replace("1.", "<br/>1.")
        litScoreUpdate2 = litScoreUpdate2.Replace("2.", "<br/>2.")
        litScoreUpdate2 = litScoreUpdate2.Replace("3.", "<br/>3.")
        litScoreUpdate2 = litScoreUpdate2.Replace("4.", "<br/>4.")
        litScoreUpdate2 = litScoreUpdate2.Replace("5.", "<br/>5.")
        litScoreUpdate2 = litScoreUpdate2.Replace("6.", "<br/>6.")

        litScoreUpdate2 = litScoreUpdate2.Replace("a.", "<br/>a.")
        litScoreUpdate2 = litScoreUpdate2.Replace("b.", "<br/>b.")
        litScoreUpdate2 = litScoreUpdate2.Replace("c.", "<br/>c.")
        litScoreUpdate2 = litScoreUpdate2.Replace("d.", "<br/>d.")
        Dim litScoreUpdate3 As String
        litScoreUpdate3 = txtFasilitasKMK.Text
        litScoreUpdate3 = litScoreUpdate3.Replace("1.", "<br/>1.")
        litScoreUpdate3 = litScoreUpdate3.Replace("2.", "<br/>2.")
        litScoreUpdate3 = litScoreUpdate3.Replace("3.", "<br/>3.")
        litScoreUpdate3 = litScoreUpdate3.Replace("4.", "<br/>4.")
        litScoreUpdate3 = litScoreUpdate3.Replace("5.", "<br/>5.")
        litScoreUpdate3 = litScoreUpdate3.Replace("6.", "<br/>6.")

        litScoreUpdate3 = litScoreUpdate3.Replace("a.", "<br/>a.")
        litScoreUpdate3 = litScoreUpdate3.Replace("b.", "<br/>b.")
        litScoreUpdate3 = litScoreUpdate3.Replace("c.", "<br/>c.")
        litScoreUpdate3 = litScoreUpdate3.Replace("d.", "<br/>d.")
        Dim litScoreUpdate4 As String
        litScoreUpdate4 = txtSyaratLainnya.Text
        litScoreUpdate4 = litScoreUpdate4.Replace("1.", "<br/>1.")
        litScoreUpdate4 = litScoreUpdate4.Replace("2.", "<br/>2.")
        litScoreUpdate4 = litScoreUpdate4.Replace("3.", "<br/>3.")
        litScoreUpdate4 = litScoreUpdate4.Replace("4.", "<br/>4.")
        litScoreUpdate4 = litScoreUpdate4.Replace("5.", "<br/>5.")
        litScoreUpdate4 = litScoreUpdate4.Replace("6.", "<br/>6.")

        litScoreUpdate4 = litScoreUpdate4.Replace("a.", "<br/>a.")
        litScoreUpdate4 = litScoreUpdate4.Replace("b.", "<br/>b.")
        litScoreUpdate4 = litScoreUpdate4.Replace("c.", "<br/>c.")
        litScoreUpdate4 = litScoreUpdate4.Replace("d.", "<br/>d.")

        html &= "<table align='center' width='100%'>"
        html &= "<tr><td colspan='2'><p align='center'><b>" & lbljudul1.Text & "</b></p></td></tr>"
        html &= "</table>"
        html &= " <br />"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='font-size:14px'>" & litIPK1.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='font-size:14px'>" & litIPK2.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='font-size:14px'>" & litIPK3.Text & "</td></tr>"
        html &= "</table>"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr>"
        html &= "<td colspan='2' style='font-size:14px'>"
        html &= litIPK4.Text
        html &= litPerofilRisiko.Text
        html &= "</td>"
        html &= "</tr>"
        html &= "</table>"
        html &= "<table align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='font-size:14px'>" & litIPK5.Text & "</td></tr>"
        html &= "</table>"
        html &= " <br />"
        html &= "<table border = '1' width='100%' style='border:1px solid #000'>"
        html &= "<tr><td colspan='3' width='100%' ><p align='left' style='font-size:14px'>" & litIPK6.Text & "</p></td><td><p></p></td></tr>"
        html &= "<tr><td ><p align='left' style='height:25px;font-size:14px;font-style: italic'>" & lblAudtor.Text & "</p></td><td style='font-size:14px;width:100%'>" & txtaudit.Value & "</td></tr>"
        html &= "<tr><td ><p align='left' style='height:25px;font-size:14px;font-style: italic'>" & lblappraisal.Text & "</p></td><td style='font-size:14px'>" & txtAppraisel.Value & "</td></tr>"
        html &= "<tr><td ><p align='left' style='height:25px;font-size:14px;font-style: italic'>" & lblmargin.Text & "</p></td><td style='font-size:14px'>" & txtmarginal.Value & "</td></tr>"
        html &= "<tr><td ><p align='left' style='height:25px;font-size:14px;font-style: italic'>" & lblother.Text & "</p></td><td style='font-size:14px'>" & txtother.Value & "</td></tr>"
        html &= "</table>"
        html &= "<br />"
        html &= "<table border = '1' width='100%' align='center' style='border:1px solid #000;'>"
        html &= "<tr><td colspan='2' style='font-size:14px'>" & litIPK7.Text & "</td></tr>"
        html &= "<tr><td colspan='2' style='padding-left:8px'><p align='justify' align='left' style='font-size:14px;'>" & txtpejabat.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtkomite.Text & "</p></td></tr>"
        html &= "</table>"
        'html &= "<table align='center' width='80%'>"
        'html &= "<tr><td colspan='2'>" & litIPK8.Text & "</td></tr>"
        'html &= "</table>"
        html &= " <br />"
        html &= " <br />"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td style='font-size:14px'><p align='left'><b>" & lblKMK1.Text & "</b></p></td></tr>"
        html &= "<tr><td style='font-size:14px;'><p >" & litKetentuanKMK1.Text & "</p></td></tr>"
        html &= "<tr><td style='font-size:14px;'><p >" & litKetentuanKMKAgunan.Text & "</p></td></tr>"
        html &= "<tr><td style='font-size:14px;'><p >" & litKetentuanKMKFixed.Text & "</p></td></tr>"
        html &= "<tr><td style='font-size:14px;'><p >" & litKetentuanKMKFixedTunai.Text & "</p></td></tr>"
        html &= "</table>"
        'html &= "<table align='center' width='80%'>"
        'html &= "<tr><td colspan='2'><p align='left'><b>" & lblKMK2.Text & "<b></p></td></tr>"
        'html &= "<tr><td colspan='2'><p align='justify'>" & litKetentuanKMK2.Text & "</p></td></tr>"
        'html &= "</table>"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='height:25px;font-size:14px'><p align='left'><b>" & lblSPPK.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2' ><p  style='text-align:justify;font-size:14px'>" & litScoreUpdate.ToString() & "</p></td></tr>"
        html &= "</table>"
        html &= "<br />"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='height:25px;font-size:14px'><p align='left'><b>" & lblkmk.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'><p  style='text-align:justify;font-size:14px'>" & litScoreUpdate1.ToString() & "</p></td></tr>"
        html &= "</table>"
        html &= "<br />"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='height:25px;font-size:14px'><p align='left'><b>" & lblpenariknKMK.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & litScoreUpdate2.ToString() & "</p></td></tr>"
        html &= "</table>"
        html &= "<br />"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='height:25px;font-size:14px'><p align='left'><b>" & lblsyaratSwitchable.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2' ><p  style='text-align:justify;font-size:14px'>" & litScoreUpdate3.ToString() & "</p></td></tr>"
        html &= "</table>"
        html &= "<br />"
        html &= "<table border = '1' align='center' width='100%'>"
        html &= "<tr><td colspan='2' style='height:25px;font-size:14px'><p align='left'><b>" & lblSyarat.Text & "</b></p></td></tr>"
        html &= "<tr><td colspan='2'><p style='text-align:justify;font-size:14px'>" & litScoreUpdate4.ToString() & "</p></td></tr>"
        html &= "</table>"
        html &= signPdf

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

    End Sub

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI2.get_Signature("IPK", appno)
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
                    html += "<div class='module'style='margin-right: 30px; '>"
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

        ds = oRptDKI2.get_Signature(rpt, appno)
        Dim dt As DataTable
        dt = ds.Tables(0)

        html = oRptDKI2.generate_signature(dt)

        Return html
    End Function

    Sub checkApproval(ByVal appno)
        Dim ds As DataSet
        ds = oRptDKI2.getCheckApproval(appno)
        Dim check = ds.Tables(0).Rows(0).Item("is_approved")
        If (check = 0) Then
            btnExport.Text = "Save"
        Else
            btnExport.Text = "Save & Export"
        End If

    End Sub
End Class