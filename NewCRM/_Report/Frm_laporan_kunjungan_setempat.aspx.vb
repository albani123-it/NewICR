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
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Security.AccessControl
Imports iTextSharp.tool.xml


Public Class Frm_laporan_kunjungan_setempat
    Inherits System.Web.UI.Page
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim brwcode As String
    Dim product As String
    Dim oRptDKI As New lReportDKI
    'Dim oRpt As New lReportStress
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim myTbl As New DataTable
    Public htmlform As String = ""
#End Region

    Private Property imgPath As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        fillDdl()
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
        Dim itemText As String = String.Empty
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oRptDKI.getFKS_TblInfo(usrLogin, appno, 0)
        dt = ds.Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1

            Create_Table_A(i)
            Create_Table_B(i)
            Create_Table_C(i)
            Create_Signature()

        Next

        'ddlExportType.Items.Clear()
        'ddlExportType.Items.Add(New System.Web.UI.WebControls.ListItem("Word", "Word"))
    End Sub

    Public Sub Create_Table_A(ByVal i)
        Try
            Dim oXMLReport As New DataReport
            htmlform.ToString()
            Dim dsData As DataSet
            dsData = oRptDKI.getFKS_TblInfo(usrLogin, appno, i)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)
            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FKS/FKS_A.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                htmlform &= oXMLReport.mainLoop(nodes, dtData, dsData)
                btnExport.Visible = True
                'ddlExportType.Visible = True
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub Create_Table_B(ByVal i)
        Dim oXMLReport_B As New DataReport
        htmlform.ToString()
        Dim dsData As DataSet
        dsData = oRptDKI.getFKS_TblInfo(usrLogin, appno, i)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FKS/FKS_B.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            htmlform &= oXMLReport_B.mainLCR(nodes, dtData)
            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub
    Public Sub Create_Table_C(ByVal i)
        Dim oXMLReport_C As New DataReport
        htmlform.ToString()
        Dim dsData As DataSet
        dsData = oRptDKI.getFKS_Foto_kunjunganReport(usrLogin, appno, i)
        Dim dtData As DataTable
        dtData = dsData.Tables(0)
        If dtData.Columns.Count <> 0 Then
            Dim filePath As String = Server.MapPath("~/xml/FKS/FKS_C.xml")
            Dim xmlDoc As New System.Xml.XmlDocument()
            xmlDoc.Load(filePath)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
            htmlform &= oXMLReport_C.mainLCR(nodes, dtData)

            btnExport.Visible = True
            'ddlExportType.Visible = True
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'If ddlExportType.SelectedValue = "Word" Then
        exportWord()

        'End If
    End Sub
    ''--Update---
    Private Sub exportWord()
        Response.Clear()
        Response.Buffer = True
        Dim fileNamez As String = "Laporan_Kunjungan_Setempat"

        Response.ContentEncoding = System.Text.Encoding.UTF8
        Dim html As String = ""
        Dim signPdf As String = SignaturePDF("FKS", appno)

        Dim ds As DataSet
        Dim dtble As DataTable
        ds = oRptDKI.getFKS_TblInfo(usrLogin, appno, 0)
        dtble = ds.Tables(0)
        Dim find As String = "../Document Upload/" & appno & "/LKN/"
        For i As Integer = 0 To dtble.Rows.Count - 1
            dtble = binimages(i)
            dtble = getimage(i)

            Dim findstring As String = htmlform.ToString()

            For row = 0 To dtble.Rows.Count() - 1
                Dim filePath = dtble.Rows(row).Item("images").ToString()
                Dim fileArray As String() = filePath.Split("\")
                Dim original As System.Drawing.Image = System.Drawing.Image.FromFile(filePath)
                Dim resized As System.Drawing.Image = ResizeImage(original, New Size(300, 200))
                Dim memStream As MemoryStream = New MemoryStream()
                Dim filename As String = fileArray(fileArray.Length - 1).ToString() + "_.png"
                For x = 1 To 5
                    filename = filename.Replace(x.ToString(), Convert.ToString(x) + "_")
                Next
                resized.Save(Server.MapPath(find) + filename)
                resized.Dispose()
                memStream.Close()

                Dim tmpimg As String = ""
                Dim imgPath1 As String = ""
                tmpimg = (dtble.Rows(row).Item("x").ToString()).Remove(0, 3)

                imgPath1 = System.Configuration.ConfigurationManager.AppSettings("pathImg")
                imgPath1 = imgPath1 + tmpimg

                If findstring.Contains(find) Then
                    htmlform = Replace(htmlform.ToString(), "<img width='65%' height='65%' src='" & dtble.Rows(row).Item("x").ToString() & "'>", "<img   src='" & Replace(imgPath1, fileArray(fileArray.Length - 1).ToString(), filename) & "'>")
                End If

                Dim findstringtextaera As String = htmlform.ToString()
                Dim findtextarea As String = "Berdasarkan hasil kunjungan ke lokasi kantor, lokasi usaha dan gudang didapatkan kesimpulan sebagai berikut :"
                If findstringtextaera.Contains(findtextarea) Then
                    htmlform = Replace(htmlform.ToString, "<textarea rows='2' cols='135' >Berdasarkan hasil kunjungan ke lokasi kantor, lokasi usaha dan gudang didapatkan kesimpulan sebagai berikut :</textarea>", "Berdasarkan hasil kunjungan ke lokasi kantor, lokasi usaha dan gudang didapatkan kesimpulan sebagai berikut :")
                End If
            Next
        Next

        Dim litScoreUpdate As String
        litScoreUpdate = htmlform.ToString()
        litScoreUpdate = litScoreUpdate.Replace("1. ", "<br/>1.")
        litScoreUpdate = litScoreUpdate.Replace("2. ", "<br/>2.")
        litScoreUpdate = litScoreUpdate.Replace("3. ", "<br/>3.")
        litScoreUpdate = litScoreUpdate.Replace("4. ", "<br/>4.")
        litScoreUpdate = litScoreUpdate.Replace("5. ", "<br/>5.")

        litScoreUpdate = litScoreUpdate.Replace("<tr  style=''><div  ", "<tr><td align='center' style='width:100%;' ><div  ")
        litScoreUpdate = litScoreUpdate.Replace("</div></tr>", "</div></td></tr>")
        litScoreUpdate = litScoreUpdate.Replace("table style='cellpadding=", "table style='border:1px solid #000;cellpadding:")
        litScoreUpdate = litScoreUpdate.Replace("table border = '1' style='cellpadding=3;width:0%;border:1px solid #000;border-spacing:initial;'", "table style='cellpadding:3;border:1px solid #000'")
        litScoreUpdate = litScoreUpdate.Replace("table style='border:1px solid #000;cellpadding:3;width:0%;border:1px solid #000;border-spacing:initial;", "table style='border:1px solid #000;cellpadding:3;width:100%;")


        html &= "<table align='center'>"
        html &= "<tr><td ><p align='center'><b>" & lbljudul1.Text & "</b></p></td></tr>"
        html &= "</table>"
        html &= "<br/>"
        html &= "<br/>"
        html &= litScoreUpdate.ToString()
        html &= signPdf

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
        Response.AddHeader("content-disposition", "attachment;filename=" & fileNamez & ".pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(pdfDoc)
        Response.End()

    End Sub

    Public Function getimage(ByVal z)
        'Dim myTbl As DataTable
        myTbl.Clear()
        Dim tmpimg As String
        Dim tpimg As String
        Dim ds As DataSet
        Dim dt As DataTable


        ds = oRptDKI.getFKS_Foto_kunjunganReport(usrLogin, appno, z)
        dt = ds.Tables(0)
        For index = 0 To dt.Rows.Count - 1
            tpimg = (dt.Rows(index).Item("lsld_foto_kunjungan").ToString())
            tmpimg = (dt.Rows(index).Item("lsld_foto_kunjungan").ToString()).Remove(0, 3)
            Dim al As String = tmpimg.Replace("/", "\")

            Dim imgPath1 As String = HttpContext.Current.Request.PhysicalApplicationPath + al
            Dim row As DataRow = myTbl.NewRow()
            row("x") = tpimg
            row("images") = imgPath1
            row("keterangan") = dt.Rows(index).Item("lsld_keterangan_foto").ToString()
            myTbl.Rows.Add(row)
        Next


        Return myTbl
    End Function
    Public Function binimages(ByVal i)
        Dim tmpimg As String
        'litScore2.Visible = True
        Dim ds As DataSet
        Dim dt As DataTable
        myTbl.Clear()

        myTbl.Columns.Add("x")
        myTbl.Columns.Add("images")
        myTbl.Columns.Add("keterangan")

        ds = oRptDKI.getFKS_Foto_kunjunganReport(usrLogin, appno, i)
        dt = ds.Tables(0)

        For index = 0 To dt.Rows.Count - 1

            tmpimg = (dt.Rows(index).Item("lsld_foto_kunjungan").ToString()).Remove(0, 3)
            'Dim al As String = tmpimg.Replace("/", "\")
            'Dim imgPath1 As String = HttpContext.Current.Request.PhysicalApplicationPath + al
            'Return imgPath1
            'Dim imgPath1 As String = ("C:\inetpub\wwwroot\ICR\") + al

            Dim imgPath1 As String = ""
            imgPath1 = System.Configuration.ConfigurationManager.AppSettings("pathImg")

            imgPath1 = imgPath1 + tmpimg

            Dim img As String = imgPath1.Replace("<br>", "_")
            Dim row As DataRow = myTbl.NewRow()

            row("images") = img
            row("keterangan") = dt.Rows(index).Item("lsld_keterangan_foto").ToString()
            myTbl.Rows.Add(row)

        Next

        Return myTbl
    End Function
    ''----END---

    Public Function ResizeImage(ByVal image As System.Drawing.Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = False) As System.Drawing.Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth,
        percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If
        Dim newImage As System.Drawing.Image = New Bitmap(newWidth, newHeight)

        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("FKS", appno)
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
                    html += "<div class='module' style='margin-right: 100px; '>"
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