Imports System.IO
Imports System.Security.AccessControl
Imports System.Net.Mime

Public Class R_DataAgunanDet
    Inherits System.Web.UI.Page
    Dim appNo As String
    Protected allowDelete As Boolean = True
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Dim oApp As New lApps
    Dim oAgu As New lAgunan
    Dim oLkn As New lLkn
    Protected code As String = ""
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim lMsg As String = ""

    Public Shared _me As New DataAgunanDet()

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        If Not IsPostBack Then
            fillDdl()

            code = Request.QueryString("code")
            appNo = Request.QueryString("appno")
            app_no.Value = appNo

            If code = "NEW" Then
                rowHdrPenarikan.Visible = False
                rowPenarikanAgunan.Visible = False
                rowKeteranganPenarikan.Visible = False
                rowTglPenarikan.Visible = False
            Else
                rowHdrPenarikan.Visible = True
                rowPenarikanAgunan.Visible = True
                rowKeteranganPenarikan.Visible = False
                rowTglPenarikan.Visible = False
            End If

            isiData(Request.QueryString("id"))

            If Request.QueryString("status") = 0 Then
                btnSave.Enabled = False
                controlRO()
            End If

        End If
    End Sub

    Sub controlRO()
        ddlTipeAgunan.Enabled = False
        ddlTipeAgunan.CssClass = "inpDdlDisabled"
        ddlControled.Enabled = False
        ddlControled.CssClass = "inpDdlDisabled"
        ddlAgunan.Enabled = False
        ddlAgunan.CssClass = "inpDdlDisabled"
        ddlPengikatan.Enabled = False
        ddlPengikatan.CssClass = "inpDdlDisabled"
        ddlKlasAgunan.Enabled = False
        ddlKlasAgunan.CssClass = "inpDdlDisabled"
        ddlStatus.Enabled = False
        ddlStatus.CssClass = "inpDdlDisabled"
        txtTglAgunan.Enabled = False
        txtKeterangan.Enabled = False
        txtDeskripsi.Enabled = False
        txtBuktiKepemilikan.Enabled = False
        txtLokasiAgunan.Enabled = False
        txtNamaPemilik.Enabled = False
        txtAppraisal.Enabled = False
        txtNoAppraisal.Enabled = False
        txtTglAppraisal.Enabled = False
        txtLuasTanah.Enabled = False
        txtLuasBangunan.Enabled = False
        txtNilaiPasar.Enabled = False
        txtNilaiLikuidasi.Enabled = False
        txtNilaiPengikatan.Enabled = False
        txtAsuransiPenjamin.Enabled = False
        txtAsuransiPenjamin.Enabled = False
        txtNilaiAsuransi.Enabled = False
        txtNilaiPasarLT.Enabled = False
        txtNilaiLikuidasiLT.Enabled = False
        txtNilaiPengikatanLT.Enabled = False
        txtAsuransiPenjaminLT.Enabled = False
        txtAsuransiPenjaminLT.Enabled = False
        txtNilaiAsuransiLT.Enabled = False
        txtNilaiPasarLB.Enabled = False
        txtNilaiLikuidasiLB.Enabled = False
        txtNilaiPengikatanLB.Enabled = False
        txtAsuransiPenjaminLB.Enabled = False
        txtAsuransiPenjaminLB.Enabled = False
        txtNilaiAsuransiLB.Enabled = False
        chkPenarikanAgunan.Enabled = False
        btnSave.Enabled = False
        cs.RegisterStartupScript(Page.[GetType](), "callFunction", "callReadOnly();", True)
    End Sub

    Sub isiData(ByVal id)
        Dim dt As DataTable
        dt = oAgu.getDataAgunanById(id)

        If dt.Rows.Count > 0 Then
            txtId.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_id"))
            ddlTipeAgunan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lda_tipe_agunan"))

            Dim dtJenisAgunan As DataTable
            dtJenisAgunan = oRat.getDropDownList(ddlTipeAgunan.SelectedValue)
            ddlAgunan.DataSource = dtJenisAgunan
            ddlAgunan.DataTextField = "ldd_desc"
            ddlAgunan.DataValueField = "ldd_code"
            ddlAgunan.DataBind()

            txtTglAgunan.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lda_tgl_agunan")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lda_tgl_agunan")))
            ddlControled.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lda_kontrol_agunan"))
            txtDeskripsi.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_deskripsi"))
            ddlAgunan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lda_jenis_agunan"))
            ddlPengikatan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lda_jenis_pengikatan"))
            txtKeterangan.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_keterangan_lain"))
            ddlKlasAgunan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lda_klas_agunan"))
            ddlStatus.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lda_status_kepemilikan"))
            txtBuktiKepemilikan.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_bukti_kepemilikan"))
            txtLokasiAgunan.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_lokasi_agunan"))
            txtNamaPemilik.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_nama_pemilik"))
            txtAppraisal.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_appraisal"))
            txtNoAppraisal.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_no_appraisal"))
            txtTglAppraisal.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lda_tgl_appraisal")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lda_tgl_appraisal")))
            txtIdDtl.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_id"))
            txtNilaiPasar.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_pasar"))
            txtNilaiLikuidasi.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_likuidasi"))
            txtNilaiPengikatan.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_pengikatan"))
            txtAsuransiPenjamin.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_asuransi_penjamin"))
            txtNilaiAsuransi.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_asuransi"))

            If dt.Rows(0).Item("isBangunan") = "TB" Then
                rowLuasTB.Visible = True
                rowNilaiPasarTB.Visible = True
                rowNilaiLikuidTB.Visible = True
                rowNilaiPengikatanTB.Visible = True
                rowAsuransiPenjaminTB.Visible = True
                rowNilaiAsuransiTB.Visible = True
            Else
                rowLuasTB.Visible = False
                rowNilaiPasarTB.Visible = False
                rowNilaiLikuidTB.Visible = False
                rowNilaiPengikatanTB.Visible = False
                rowAsuransiPenjaminTB.Visible = False
                rowNilaiAsuransiTB.Visible = False
            End If

            txtLuasTanah.Text = oGen.CheckNullString(dt.Rows(0).Item("luas_tanah"))
            txtLuasBangunan.Text = oGen.CheckNullString(dt.Rows(0).Item("luas_bangunan"))
            txtNilaiPasarLT.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_pasar_lt"))
            txtNilaiPasarLB.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_pasar_lb"))
            txtNilaiLikuidasiLT.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_likuidasi_lt"))
            txtNilaiLikuidasiLB.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_likuidasi_lb"))
            txtNilaiPengikatanLT.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_pengikatan_lt"))
            txtNilaiPengikatanLB.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_pengikatan_lb"))
            txtAsuransiPenjaminLT.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_asuransi_penjamin_lt"))
            txtAsuransiPenjaminLB.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_asuransi_penjamin_lb"))
            txtNilaiAsuransiLT.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_asuransi_lt"))
            txtNilaiAsuransiLB.Text = oGen.CheckNullString(dt.Rows(0).Item("ldan_nilai_asuransi_lb"))

            If oGen.CheckNullString(dt.Rows(0).Item("lda_flag_inout")) = 1 Then
                chkPenarikanAgunan.Checked = False

                rowPenarikanAgunan.Visible = True
                rowKeteranganPenarikan.Visible = False
                rowTglPenarikan.Visible = False
            Else
                chkPenarikanAgunan.Checked = True

                rowPenarikanAgunan.Visible = True
                rowKeteranganPenarikan.Visible = True
                rowTglPenarikan.Visible = True

            End If
            txtKetPenarikan.Text = oGen.CheckNullString(dt.Rows(0).Item("lda_keterangan_penarikan"))
            txtTanggal.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lda_tanggal_penarikan")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lda_tanggal_penarikan")))

            FillDataDtlById(txtId.Text)


        End If

    End Sub

    Sub fillDdl()
        Dim dtJenis As New DataTable
        dtJenis.Columns.Add("Value")

        Dim dtControled As DataTable
        dtControled = oRat.getDropDownList("RFCONTROLED")
        ddlControled.DataSource = dtControled
        ddlControled.DataTextField = "ldd_desc"
        ddlControled.DataValueField = "ldd_code"
        ddlControled.DataBind()

        Dim dtTypeAgunan As DataTable
        dtTypeAgunan = oRat.getDropDownList("RFCOLASSET")
        ddlTipeAgunan.DataSource = dtTypeAgunan
        ddlTipeAgunan.DataTextField = "ldd_desc"
        ddlTipeAgunan.DataValueField = "ldd_code"
        ddlTipeAgunan.DataBind()

        Dim xjenis As String
        xjenis = ddlTipeAgunan.SelectedValue

        If xjenis = "Pilih" Then
            Dim dtJenisAgunan As DataTable
            dtJenisAgunan = oRat.getDropDownList("Pilih")
            ddlAgunan.DataSource = dtJenisAgunan
            ddlAgunan.DataTextField = "ldd_desc"
            ddlAgunan.DataValueField = "ldd_code"
            ddlAgunan.DataBind()
        End If

        Dim dtStatus As DataTable
        dtStatus = oRat.getDropDownList("RFOWNSTAT")
        ddlStatus.DataSource = dtStatus
        ddlStatus.DataTextField = "ldd_desc"
        ddlStatus.DataValueField = "ldd_code"
        ddlStatus.DataBind()

        Dim dtPengikatan As DataTable
        dtPengikatan = oRat.getDropDownList("RFIKAT")
        ddlPengikatan.DataSource = dtPengikatan
        ddlPengikatan.DataTextField = "ldd_desc"
        ddlPengikatan.DataValueField = "ldd_code"
        ddlPengikatan.DataBind()

        Dim dtKlasAgunan As DataTable
        dtKlasAgunan = oRat.getDropDownList("RFCOLCLASS")
        ddlKlasAgunan.DataSource = dtKlasAgunan
        ddlKlasAgunan.DataTextField = "ldd_desc"
        ddlKlasAgunan.DataValueField = "ldd_code"
        ddlKlasAgunan.DataBind()
    End Sub

    Protected Sub OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim xType As String
        xType = ddlTipeAgunan.SelectedValue.ToString()

        Dim dtJenisAgunan As DataTable
        dtJenisAgunan = oRat.getDropDownList(xType)
        ddlAgunan.DataSource = dtJenisAgunan
        ddlAgunan.DataTextField = "ldd_desc"
        ddlAgunan.DataValueField = "ldd_code"
        ddlAgunan.DataBind()

        If xType <> "REFCOLFIXED" Then
            rowLuasTB.Visible = False
            rowNilaiPasarTB.Visible = False
            rowNilaiLikuidTB.Visible = False
            rowNilaiPengikatanTB.Visible = False
            rowAsuransiPenjaminTB.Visible = False
            rowNilaiAsuransiTB.Visible = False
        End If



    End Sub

    Protected Sub DdlAgunanOnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim xType As String
        Dim xJenis As String
        Dim dt As DataTable
        xType = ddlTipeAgunan.SelectedValue.ToString() '

        If xType = "REFCOLFIXED" Then
            xJenis = ddlAgunan.SelectedValue.ToString()

            dt = oAgu.getCheckAgunanFixed(xJenis)

            If dt.Rows(0).Item("value") = "0" Then
                'bagunan'
                rowLuasTB.Visible = True
                rowNilaiPasarTB.Visible = True
                rowNilaiLikuidTB.Visible = True
                rowNilaiPengikatanTB.Visible = True
                rowAsuransiPenjaminTB.Visible = True
                rowNilaiAsuransiTB.Visible = True
            Else
                'non bagunan'
                rowLuasTB.Visible = False
                rowNilaiPasarTB.Visible = False
                rowNilaiLikuidTB.Visible = False
                rowNilaiPengikatanTB.Visible = False
                rowAsuransiPenjaminTB.Visible = False
                rowNilaiAsuransiTB.Visible = False
            End If
        Else
            rowLuasTB.Visible = False
            rowNilaiPasarTB.Visible = False
            rowNilaiLikuidTB.Visible = False
            rowNilaiPengikatanTB.Visible = False
            rowAsuransiPenjaminTB.Visible = False
            rowNilaiAsuransiTB.Visible = False
        End If
    End Sub

    Protected Sub ChckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        If chkPenarikanAgunan.Checked = True Then
            rowKeteranganPenarikan.Visible = True
            rowTglPenarikan.Visible = True
        Else
            rowKeteranganPenarikan.Visible = False
            rowTglPenarikan.Visible = False
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim inout As String
        Dim flag As Integer
        Dim ds As DataSet
        If chkPenarikanAgunan.Checked = True Then
            CheckInput()
            CheckInputPenarikan()
            inout = "out"
            flag = 2
            ds = oAgu.insertUpdateDataAgunan(Request, app_no.Value, usrLogin, inout, flag, txtFilename.Text)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
        Else
            inout = "in"
            CheckInput()
            flag = 1
            ds = oAgu.insertUpdateDataAgunan(Request, app_no.Value, usrLogin, inout, flag, txtFilename.Text)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
        End If
    End Sub

    Sub CheckInput()
        lMsg = ""

        If (Session("brwtype") = "5") Then
            If ddlTipeAgunan.SelectedValue.ToString() <> "REFCOLNONFIXED" Then
                lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal Agunan", txtTglAgunan.Text)
            End If

            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Tipe Agunan", ddlTipeAgunan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Jenis Agunan", ddlAgunan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Deskripsi", txtDeskripsi.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nilai Pengikatan (Maks) %", txtNilaiPengikatan.Text)

            If ddlTipeAgunan.SelectedValue.ToString() <> "REFCOLNONFIXED" Then
                'lMsg &= oGen.checkInputInaJavascript("TextBox", "Asuransi Penjamin", txtAsuransiPenjamin.Text)
                'lMsg &= oGen.checkInputInaJavascript("TextBox", "Nilai Asuransi", txtNilaiAsuransi.Text)
            End If
        Else
            If ddlTipeAgunan.SelectedValue.ToString() <> "REFCOLNONFIXED" Then
                lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal Agunan", txtTglAgunan.Text)
            End If

            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Tipe Agunan", ddlTipeAgunan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Jenis Agunan", ddlAgunan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Deskripsi", txtDeskripsi.Text)
            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Status Kepemilikan", ddlStatus.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Bukti Kepemilikan", txtBuktiKepemilikan.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Pemilik", txtNamaPemilik.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nilai Pasar", txtNilaiPasar.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nilai Pengikatan", txtNilaiPengikatan.Text)

            If ddlTipeAgunan.SelectedValue.ToString() <> "REFCOLNONFIXED" Then
                'lMsg &= oGen.checkInputInaJavascript("TextBox", "Asuransi Penjamin", txtAsuransiPenjamin.Text)
                'lMsg &= oGen.checkInputInaJavascript("TextBox", "Nilai Asuransi", txtNilaiAsuransi.Text)
            End If
        End If
    End Sub

    Sub CheckInputPenarikan()
        lMsg = ""
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Keterangan Penarikan", txtKetPenarikan.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal Penarikan", txtTanggal.Text)
        'If lMsg <> "" Then
        '    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & lMsg & ".');", True)
        'End If
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNew.Click
        CheckInput()
        cs.RegisterStartupScript(Page.[GetType](), "callfunction", "clearData();", True)
        If lMsg = "" Then
            rowUploadImage.Attributes.Add("style", "display:''")
            btnAddNew.Attributes.Add("style", "display:none")
        Else
            rowUploadImage.Attributes.Add("style", "display:none")
            btnAddNew.Attributes.Add("style", "display:''")
        End If

    End Sub

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Dim inout As String
        Dim flag As Integer
        Dim ds As DataSet

        CheckInput()
        cekInputUpload()
        Dim type As String = "Agunan"

        If lblNotif.Text = "" Then
            Dim mkFolder As String
            mkFolder = "~/Document Upload/" & app_no.Value & "/" + type + "/"
            Dim filename As String = ""
            filename = Path.GetFileName(FileUpload1.FileName)
            txtFilename.Text = filename

            Try
                'if directory isn't exist || Server.MapPath("~/Document Upload/")
                'check folder exists
                If Not Directory.Exists(Server.MapPath(mkFolder)) Then
                    editpermit()
                    Directory.CreateDirectory(Server.MapPath(mkFolder))
                End If

                'check file exists
                If System.IO.File.Exists(Server.MapPath(mkFolder) & filename) Then
                    System.IO.File.Delete(filename)
                End If

                FileUpload1.SaveAs(Server.MapPath(mkFolder) + filename)

                If app_no.Value <> "" Then
                    Try

                        oLkn.updateAttachmentFile(app_no.Value, FileUpload1.FileName, type)
                        If chkPenarikanAgunan.Checked = True Then
                            CheckInput()
                            CheckInputPenarikan()
                            inout = "out"
                            flag = 2
                            ds = oAgu.insertUpdateDataAgunan(Request, app_no.Value, usrLogin, inout, flag, filename)
                            txtFotoId.Text = ds.Tables(0).Rows(0).Item("ldad_id")
                            txtId.Text = ds.Tables(0).Rows(0).Item("lda_id")
                        Else
                            inout = "in"
                            CheckInput()
                            flag = 1
                            ds = oAgu.insertUpdateDataAgunan(Request, app_no.Value, usrLogin, inout, flag, filename)
                            txtFotoId.Text = ds.Tables(0).Rows(0).Item("ldad_id")
                            txtId.Text = ds.Tables(0).Rows(0).Item("lda_id")

                        End If

                        FillDataDtlById(Request.QueryString("id"))
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');", True)

                    Catch ex As Exception
                        lblNotif.Text = ex.Message
                        divnotif.Attributes.Add("style", "display:''")
                    End Try
                End If

            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message & "');"

            End Try
        End If

    End Sub

    Sub cekInputUpload()
        Dim lMsg As String = ""

        If FileUpload1.HasFile Then
            'Dim maxLength As Integer = 20971520
            Dim file As HttpPostedFile = DirectCast(FileUpload1.PostedFile, HttpPostedFile)
            Dim filesize As Integer = file.ContentLength
            Dim filename As String = ""

            'folderUrl = mkFolder & "/"

            'Dim path As String = Server.MapPath(folderUrl)
            filename = Path.GetFileName(FileUpload1.FileName)

            If filesize > 20971520 Then
                lMsg = lMsg + "File lebih dari 20 MB.<br/>"
            End If

            If Len(filename) > 5000 Then
                lMsg = lMsg + "Nama file maksimal  5000 karakter.<br/>"
            End If
        Else
            lMsg = lMsg + "File harus diisi.<br/>"
        End If

        If lMsg <> "" Then
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Sub FillDataDtlById(ByVal Id)
        Dim ds As DataSet
        ds = oAgu.getDataAgunanImageByHdr(Id)
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)
        Dim type As String
        Dim ds As DataSet
        type = "Agunan"
        Try

            ds = oAgu.delDataAgunanImageById(dataItem("ldad_id"), type)
            grid1.DataSource = ds
            grid1.DataBind()

            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiHapus() + "');", True)
        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');", True)
        End Try
    End Sub


    Protected Sub download(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim type As String = "Agunan"
        Dim docname As String = e.CommandArgument.ToString()
        Dim mkFolder As String
        mkFolder = "~/Document Upload/" & app_no.Value & "/" & type & "/"
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