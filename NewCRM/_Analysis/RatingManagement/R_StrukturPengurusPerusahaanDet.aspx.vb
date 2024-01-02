Imports System.IO
Imports System.Security.AccessControl
Public Class R_StrukturPengurusPerusahaanDet
    Inherits System.Web.UI.Page
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Dim oSPP As New lStrukturPP
    Dim oLkn As New lLkn
    Dim oApp As New lApps
    Dim usrLogin As String
    Dim appNo As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            fillDdl()
            If Request.QueryString("id") IsNot Nothing Then
                If Request.QueryString("id") > 0 And Not IsPostBack Then
                    isiData(Request.QueryString("id"))
                End If
            End If

        End If
        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        ElseIf Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
        End If

        checkAppByBrwCode(Session("brwcode"))

    End Sub
    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und" Then
                    tf = True
                End If
            Next

            If Not tf Then
                Session.Abandon()
                Response.Redirect("../../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../../Login.aspx")
        End Try

    End Sub

    Sub checkAppByBrwCode(ByVal brwcode)

        Dim ds As DataSet
        ds = oApp.checkAppByBrwCode(brwcode)

        If ds.Tables(0).Rows.Count > 0 Then
            appNo = ds.Tables(0).Rows(0).Item("app_no")
        End If

    End Sub
    Sub fillDdl()
        Dim dtJenis As New DataTable
        dtJenis.Columns.Add("Value")

        Dim dtControled As DataTable
        dtControled = oRat.getDropDownList("GENDER")
        ddlGender.DataSource = dtControled
        ddlGender.DataTextField = "ldd_desc"
        ddlGender.DataValueField = "ldd_code"
        ddlGender.DataBind()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        liBodyLoadAction.Text = ""
        CekInput()
        If liBodyLoadAction.Text = "" Then
            If app_no.Value IsNot Nothing Then
                If Request.QueryString("id") <> -1 Then
                    'update data
                    Try
                        'update data fixed asset
                        oSPP.updateStrukturPengurus(Request, Request.QueryString("id"), usrLogin, textFile.Value)
                        'liBodyLoadAction.Text = "alert('Data berhasil diubah.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)

                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                Else
                    'insert data
                    Try
                        'insert data fixed asset
                        oSPP.insertStrukturPengurus(Request, appNo, usrLogin, textFile.Value)
                        'liBodyLoadAction.Text = "alert('Data berhasil ditambah.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)


                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                End If
            End If
        End If
    End Sub

    Sub CekInput()
        Dim lMsg As String = ""

        lMsg &= oGen.checkInputInaJavascript("TextBox", "nama", txtNama.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "TTL", txtTgllahir.Text)
        lMsg &= oGen.checkInputInaJavascript("DropDownList", "Jenis Kelamin", ddlGender.SelectedValue)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Alamat", txtAlamat.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Perusahaan", txtNamaPerusahaan.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Jabatan", txtNamaJabatan.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Hubungan Antar Pengurus", txtHUbunganAntarPengurus.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Masa Kerja Dari", txtMasaKerjaDari.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal", txtSampaiTgl.Text)

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        'cekInputUpload()
        Dim type As String = "StrukturPengurus"

        If lblNotif.Text = "" Then
            Dim fileList As String()

            Dim filename As String = ""
            filename = Path.GetFileName(FileUpload1.FileName)
            fileList = filename.Split(".")
            Dim dTbl As DataTable
            dTbl = oGen.getDataExtentionFile(fileList(fileList.Length - 1).ToString())
            If dTbl.Rows.Count > 0 Then
                cekInputUpload(dTbl.Rows(0).Item("extention_group").ToString())

                If lblNotif.Text = "" Then
                    If dTbl.Rows(0).Item("extention_name").ToString() = "image" Then
                        Dim mkFolder As String
                        mkFolder = "~/Document Upload/" & app_no.Value & "/StrukturPengurus/"

                        Try
                            'if directory isn't exist || Server.MapPath("~/Document Upload/")
                            'check folder exists
                            If Not Directory.Exists(Server.MapPath(mkFolder)) Then
                                editpermit()
                                Directory.CreateDirectory(Server.MapPath(mkFolder))
                            End If

                            'check file exists
                            If System.IO.File.Exists(Server.MapPath(mkFolder) & filename) Then
                                editpermit()
                                ' Set full path to file 
                                Dim FileToDelete As String
                                FileToDelete = Server.MapPath(mkFolder) & filename
                                ' Delete a file
                                File.Delete(FileToDelete)
                            End If

                            FileUpload1.SaveAs(Server.MapPath(mkFolder) + filename)
                            textFile.Value = filename

                            Dim filePaths As String() = Directory.GetFiles(Server.MapPath(mkFolder))
                            Dim files As New List(Of ListItem)()
                            For Each filePath As String In filePaths
                                Dim nameFile As String = Path.GetFileName(filePath)
                                If filename = nameFile Then
                                    files.Add(New ListItem("~/Document Upload/" & app_no.Value & "/StrukturPengurus/" + nameFile))
                                End If
                            Next
                            GridView1.DataSource = files
                            GridView1.DataBind()

                            Dim ds As DataSet
                            Dim dt As DataTable

                            Dim tgl1 As String = Request.Form("txtTgllahir")
                            Dim tgl2 As String = Request.Form("txtMasaKerjaDari")
                            Dim tgl3 As String = Request.Form("txtSampaiTgl")

                            If app_no.Value <> "" Then
                                Try
                                    ds = oLkn.checkAttachmentFile(app_no.Value, FileUpload1.FileName, type)
                                    dt = ds.Tables(0)
                                    If dt.Rows.Count > 0 Then
                                        'update data
                                        oLkn.updateAttachmentFile(app_no.Value, FileUpload1.FileName, type)
                                        'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran dokumen berhasil diubah.');window.location.href=window.location.href;", True)
                                    Else
                                        'insert data
                                        oLkn.insertAttachmentFile(app_no.Value, FileUpload1.FileName, type)
                                        'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Lampiran dokumen berhasil ditambah.');window.location.href=window.location.href;", True)
                                    End If

                                    txtTgllahir.Text = tgl1
                                    txtMasaKerjaDari.Text = tgl2
                                    txtSampaiTgl.Text = tgl3

                                Catch ex As Exception
                                    lblNotif.Text = ex.Message
                                    divnotif.Attributes.Add("style", "display:''")
                                End Try
                            End If

                        Catch ex As Exception
                            lblNotif.Text = "alert('" & ex.Message & "');"

                        End Try
                    End If
                End If

            Else
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('File yang diupload bukan gambar/foto');", True)

            End If

        End If

    End Sub

    Sub cekInputUpload(ByVal extgroup)
        Dim lMsg As String = ""

        If FileUpload1.HasFile Then
            'Dim maxLength As Integer = 20971520
            Dim file As HttpPostedFile = DirectCast(FileUpload1.PostedFile, HttpPostedFile)
            Dim filesize As Integer = file.ContentLength
            Dim filename As String = ""

            filename = Path.GetFileName(FileUpload1.FileName)

            If extgroup = "images" Then
                If filesize > 204800 Then
                    lMsg = lMsg + "File lebih image dari 200 KB.<br/>"
                End If
            ElseIf extgroup = "documents" Then
                If filesize > 2097152 Then
                    lMsg = lMsg + "File lebih image dari 2 MB.<br/>"
                End If
            ElseIf extgroup = "datas" Then
                If filesize > 10485760 Then
                    lMsg = lMsg + "File lebih image dari 10 KB.<br/>"
                End If
            End If

        Else
            lMsg = lMsg + "File upload belum dipilih.<br/>"
        End If

        If lMsg <> "" Then
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oSPP.getDataStrukturPerusahaanById(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            app_no.Value = oGen.CheckNullString(dt.Rows(0).Item("lsp_appno"))
            txtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lsp_nama"))
            txtTgllahir.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsp_tgl_lahir")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsp_tgl_lahir")))
            ddlGender.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lsp_jenis_kelamin"))
            txtNamaPerusahaan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsp_nama_perusahaan"))
            txtNamaJabatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsp_nama_jabatan"))
            txtHUbunganAntarPengurus.Text = oGen.CheckNullString(dt.Rows(0).Item("lsp_hub_antar_pengurus"))
            txtAlamat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsp_alamat"))
            txtMasaKerjaDari.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsp_masa_kerja_awal")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsp_masa_kerja_awal")))
            txtSampaiTgl.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsp_masa_kerja_akhir")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsp_masa_kerja_akhir")))
            txtRiwayat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsp_riwayat"))
            textFile.Value = oGen.CheckNullString(dt.Rows(0).Item("lsp_path_foto"))

            If oGen.CheckNullString(dt.Rows(0).Item("lsp_is_key_person")) <> "" Then
                If dt.Rows(0).Item("lsp_is_key_person") = True Then
                    chkKeyPerson.Checked = True
                Else
                    chkKeyPerson.Checked = False
                End If
            End If
            If textFile.Value <> "" Then
                Dim files As New List(Of ListItem)()
                files.Add(New ListItem("~/Document Upload/" & app_no.Value & "/StrukturPengurus/" + textFile.Value))
                GridView1.DataSource = files
                GridView1.DataBind()
            End If
        End If
    End Sub
End Class