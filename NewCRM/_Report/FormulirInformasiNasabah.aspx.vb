Imports IDX.XMLReport
Imports System.Xml
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.tool.xml

Public Class FormulirInformasiNasabah
    Inherits System.Web.UI.Page
    Dim oRpt As New lReport
    Public dtble As DataTable

    'Public appNo As String = "000153016072600001"

    Public brwCode As String = "CO16072600017"
    Protected app_no As String = ""
#Region " Global Variable "
    Dim usrLogin As String
    Dim appno As String
    Dim oRptDKI As New lReportDKI
    Dim myTbl As New DataTable
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekAuthentication()
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        appno = Request.QueryString("appno")
        If Not IsPostBack Then
            Bind()
            'Keyperson(appno)
            'keyPersonName.Enabled = False
            'keyPersonJabatan.Enabled = False
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
    'Sub BindGrid()
    '    Dim ds As DataSet
    '    Dim dt As DataTable

    '    Dim appNo As String = "20160010"
    '    With New lReport
    '        ds = .getDataFIN(appNo)
    '        dt = ds.Tables(0)

    '        lblNamaNasabah.Text = dt.Rows(0).Item("lsb_namaPemohon").ToString()
    '        lblTanggalFIN.Text = dt.Rows(0).Item("FIN").ToString()
    '        lblPengelola.Text = dt.Rows(0).Item("usr_name").ToString()
    '        lblJabatanPengelola.Text = dt.Rows(0).Item("usr_access_level").ToString()

    '        'Dim ds1 As DataSet
    '        'ds1 = .getLegalitasUsaha("ad")


    '    End With
    'End Sub

    Sub Bind()
        textAreaFIN()
        FIN()
        LegalitasUsaha()
        KepemilikanPerusahaan()
        PengurusDanPenilaianManajemen()
        ProfilManajemenPerusahaan()
        KeyPerson()
        Create_Signature()
        'DanaAtauFunding()
        'FasilitasKredit1()
        'FasilitasKredit2()
        'FasilitasKredit3()
        'FasilitasKredit4()
        'FasilitasKredit5()
    End Sub

    Sub textAreaFIN()
        Try
            Dim ds As DataSet
            Dim dt As DataTable

            ds = oRpt.getDataFINText(usrLogin, appno)
            dt = ds.Tables(0)

            txaLatarBelakang.Text = dt.Rows(0).Item("rfn_latar_belakang").ToString()
            txaLegalitasUsaha.Text = dt.Rows(0).Item("rfn_legalitas").ToString()
            txtpemegangsaham.Text = dt.Rows(0).Item("rfn_pemegang_saham").ToString()
            txtsusunanpengurus.Text = dt.Rows(0).Item("rfn_susunan_pengurus").ToString()
            txtpengursumanagemen.Text = dt.Rows(0).Item("rfn_keseluruhan_managemen").ToString()
            txaPenilaianManajemen.Text = dt.Rows(0).Item("rfn_penilaian_management").ToString()

            'KeyPerson
            'Dim dtP As DataTable

            'dtP = oRpt.getKeyPerson(usrLogin, appno)
            'keyPersonName.Text = dtP.Rows(0).Item("lsk_nama").ToString()
            'keyPersonJabatan.Text = dtP.Rows(0).Item("lsk_jabatan").ToString()

        Catch ex As Exception

        End Try
    End Sub
    'Sub Keyperson(ByVal appno)
    '    Dim dtble As DataTable
    '    Dim ds As DataSet
    '    With New lReport
    '        ds = .getKeyPerson(usrLogin, appno)
    '        dtble = ds.Tables(0)

    '        'For Each dc As DataColumn In dr.Table.Columns
    '        For i As Integer = 0 To dtble.Rows.Count - 1
    '            keyPersonName.Text = dtble.Rows(i).Item("lsp_nama")
    '            keyPersonJabatan.Text = dtble.Rows(i).Item("lsp_nama_jabatan")
    '        Next
    '    End With
    'End Sub
    Sub FIN()
        Try
            Dim oXMLReport As New DataReport
            ltrlFIN.Text = ""
            Dim dsData As DataSet
            dsData = oRpt.getDataFIN(usrLogin, appno)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)

            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FIN/FIN.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                ltrlFIN.Text = oXMLReport.mainLCR(nodes, dtData)


                'btnExport.Visible = True
                'ddlExportType.Visible = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Sub LegalitasUsaha()
        Try
            Dim oXMLReport_legUs As New DataReport
            ltrlLegalitasUsaha.Text = ""

            Dim dsData As DataSet
            dsData = oRpt.getLegalitasUsaha(usrLogin, appno)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)
            Dim ds As DataSet
            ds = oRpt.getLegalitasUsahaField()

            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FIN/FIN_Legalitas_Usaha.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                ltrlLegalitasUsaha.Text = oXMLReport_legUs.mainLoop(nodes, dtData, ds)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub KepemilikanPerusahaan()
        Try
            Dim oXMLReport_KepPer As New DataReport
            ltrlKepemilikanPerusahaan.Text = ""

            Dim dsData As DataSet
            dsData = oRpt.getKepemilikanPerusahaan(usrLogin, appno)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)
            Dim ds As DataSet
            ds = oRpt.getKepemilikanPerusahaanField()

            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FIN/FIN_Kepemilikan_Perusahaan.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                ltrlKepemilikanPerusahaan.Text = oXMLReport_KepPer.mainLoop(nodes, dtData, ds)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub PengurusDanPenilaianManajemen()
        Try
            ltrlPengurusdanPenilaianManajemen.Text = ""

            Dim oXMLReport_PengPenMan As New DataReport
            Dim dsData As DataSet
            dsData = oRpt.getSusunanPengurus(usrLogin, appno)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)
            Dim dsPeng As DataSet
            dsPeng = oRpt.getPengurusDanPenilaianManajemenTableField()

            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FIN/FIN_Pengurus_Dan_Penilaian_Manajemen.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                ltrlPengurusdanPenilaianManajemen.Text = oXMLReport_PengPenMan.mainLoop(nodes, dtData, dsPeng)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub ProfilManajemenPerusahaan()
        Try
            Dim oXMLReport_profil As New DataReport
            ltrlProfilManajemenPerusahaan.Text = ""
            Dim dsData As DataSet
            dsData = oRpt.getPengurusDanPenilaianManajemenTable(usrLogin, appno)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)
            Dim dsProf As DataSet
            dsProf = oRpt.getProfileManajemenPerusahaanField()

            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FIN/FIN_Profile_Manajemen_Perusahaan.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                ltrlProfilManajemenPerusahaan.Text = oXMLReport_profil.mainLoop(nodes, dtData, dsProf)

            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub KeyPerson()
        Try
            Dim oXMLReport As New DataReport
            litkeyperson.Text = ""
            Dim dsData As DataSet
            dsData = oRpt.getFINKeyPerson(usrLogin, appno)
            Dim dtData As DataTable
            dtData = dsData.Tables(0)

            If dtData.Columns.Count <> 0 Then
                Dim filePath As String = Server.MapPath("~/xml/FIN/FIN_KeyPerson.xml")
                Dim xmlDoc As New System.Xml.XmlDocument()
                xmlDoc.Load(filePath)
                Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Report")
                litkeyperson.Text = oXMLReport.mainLoop(nodes, dtData, dsData)


                'btnExport.Visible = True
                'ddlExportType.Visible = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            Dim latarBelakang As String = Request.Form("txaLatarBelakang")
            Dim pemegang As String = txtpemegangsaham.Text
            Dim Legalitas As String = txaLegalitasUsaha.Text
            Dim susunan As String = txtsusunanpengurus.Text
            Dim managemen As String = txtpengursumanagemen.Text
            Dim penilaianJaminan As String = Request.Form("txaPenilaianManajemen")
            'Dim dt As DataTable

            'dt = oRpt.checkCountInTableFIN(appno)

            'If dt.Rows(0).Item("cRows") > 0 Then
            '    oRpt.UpdateTextAreaFIN(appno, latarBelakang, pemegang, Legalitas, susunan, managemen, penilaianJaminan, "Update")
            'Else
            '    oRpt.UpdateTextAreaFIN(appno, latarBelakang, pemegang, Legalitas, susunan, managemen, penilaianJaminan, "Insert")
            'End If
            oRpt.insertUpdateTblFin(appno, latarBelakang, pemegang, Legalitas, susunan, managemen, penilaianJaminan)

            exportWord()
        Catch ex As Exception
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + ex.Message + "');", True)
        End Try
    End Sub

    Private Sub exportWord()
        Dim fileName As String = "Formulir_Informasi_Nasabah"
        Dim html As String = ""
        Dim dtble As DataTable
        dtble = binimages()
        Dim signPdf As String = SignaturePDF("FIN", appno)

        html += "<table style='border:1px solid #000;clear:both; margin:0 auto; padding:0; width:100%;'>"
        html += "<tbody>"
        html += "<tr>"
        html += "<td>"
        html += "<div style='text-align:center;'>"
        html += "<span id='lbljudul' style='height:30px;font-weight:bold;font-size:14px;width:99%;'>FORMULIR INFORMASI NASABAH</span>"
        html += "</div>"
        html += "<br />"
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>                         "
        html += ltrlFIN.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;font-size:13px;font-weight:bold;'><strong>1. LATAR BELAKANG USAHA DEBITUR</strong></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += Request.Form("txaLatarBelakang").ToString()
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;font-size:13px;font-weight:bold;'><span><strong>2. LEGALITAS / PERIZINAN USAHA</strong></span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;'><span style='margin-left:12px;font-size:13px;font-weight:bold;'><strong>Legalitas Usaha</strong></span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += ltrlLegalitasUsaha.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;'><span style='margin-left:13px;font-size:12px;font-weight:bold;'><strong>Legalitas Permohonan Kredit</strong></span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += Request.Form("txaLegalitasUsaha").ToString()
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;font-size:13px;font-weight:bold;'><span><strong>3. KEPEMILIKAN PERUSAHAAN</strong></span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;'><span style='height:30px;margin-left:12px;font-size:13px;'>3.1 Pemegang Saham</span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += Request.Form("txtpemegangsaham").ToString()
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += ltrlKepemilikanPerusahaan.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;'><span style='height:30px;margin-left:12px;font-size:13px;'>3.2 Key Person</span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;'>"
        html += litkeyperson.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;font-size:13px;font-weight:bold'><span><strong>4. PENGURUS DAN PENILAIAN MANAJEMEN</strong></span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;'><span style='height:30px;margin-left:12px;font-size:13px;'>4.1 Susunan Pengurus Perusahaan</span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += Request.Form("txtsusunanpengurus").ToString()
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += ltrlPengurusdanPenilaianManajemen.Text
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += Request.Form("txtpengursumanagemen").ToString()
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        For index = 0 To dtble.Rows.Count - 1
            html &= "<table style='cellpadding=2;width:98%;border:1px solid #000;border-bottom: 1px solid #ddd;border-spacing:initial;'>"
            html &= "<tr><td align='left' style='text-align:center;width:20%;border-bottom:1px solid #000;font-size:13px;padding-left:4px'><p><img width='65%' height='65%' src='" & dtble.Rows(index).Item("images").ToString() & "' /></p></td><td style=''></td><td style='padding-top:2px;' ><p style='font-weight:bold;font-size:14px;margin-top:1px;'>" & dtble.Rows(index).Item("nama").ToString() & "<br/></p><p style='text-align:justify;font-size:14px'>" & dtble.Rows(index).Item("keterangan").ToString() & " </p></td></tr>"
            html &= "</table>"
        Next
        html += "</td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='height:30px;width:80%;'><span style='height:30px;margin-left:12px;font-size:13px'>4.2 Penilaian Manajemen</span></td>"
        html += "</tr>"
        html += "<tr>"
        html += "<td style='width:80%;' align='center'>"
        html += Request.Form("txaPenilaianManajemen").ToString()
        html += "</td>"
        html += "</tr>"
        html += "</tbody>"
        html += "</table>"
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


    Public Function binimages()
        Dim tmpimg As String
        'Dim user_chart As String = "88899"
        ltrlProfilManajemenPerusahaan.Visible = True
        Dim ds As DataSet
        Dim dt As DataTable
        Dim dr As DataRow


        myTbl.Columns.Add("images")
        myTbl.Columns.Add("nama")
        myTbl.Columns.Add("keterangan")


        ds = oRpt.getPengurusDanPenilaianManajemenTable(usrLogin, appno)
        dt = ds.Tables(0)

        For index = 0 To dt.Rows.Count - 1

            tmpimg = (dt.Rows(index).Item("foto").ToString()).Remove(0, 3)
            Dim al As String = tmpimg.Replace("/", "\")
            Dim imgPath1 As String = HttpContext.Current.Request.PhysicalApplicationPath + al
            'Return imgPath1

            Dim row As DataRow = myTbl.NewRow()
            row("images") = imgPath1
            row("nama") = dt.Rows(index).Item("lsp_nama").ToString()
            row("keterangan") = dt.Rows(index).Item("lsp_riwayat").ToString()
            myTbl.Rows.Add(row)

        Next

        Return myTbl
    End Function

    Sub Create_Signature()
        Dim html As String = ""
        Dim ds As DataSet
        ds = oRptDKI.get_Signature("FIN", appno)
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