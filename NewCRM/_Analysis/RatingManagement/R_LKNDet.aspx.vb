Imports System.IO
Imports System.Security.AccessControl

Public Class R_LKNDet
    Inherits System.Web.UI.Page
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Dim oApp As New lApps
    Dim oLkn As New lLkn
    Protected allowDelete As Boolean = True
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Request.QueryString("id") IsNot Nothing Then
            If Request.QueryString("id") > 0 And Not IsPostBack Then
                isiData(Request.QueryString("id"))
            End If
        End If
        If Request.QueryString("appno") IsNot Nothing Then
            appno.Value = Request.QueryString("appno")
        End If
        If Request.QueryString("status") = 0 Then
            'btnSave.Enabled = False
            controlRO()
        End If
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

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        'ds = oApp.getDetailOfLKN(id)
        ds = oLkn.getDataLKN(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            appno.Value = oGen.CheckNullString(dt.Rows(0).Item("lsl_app_no"))
            txtIdLkn.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_id"))
            InpNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_nama"))
            InpBentuk.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_bentuk"))
            InpTujuan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_tujuan"))
            InpTanggal.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lsl_tanggal")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lsl_tanggal")))
            InpJabatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_jabatan"))
            InpHasil.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_hasil"))
            InpTindak.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_tindak"))
            txtAlamat.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_alamat"))
            txtHasilKunjungan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsl_hasil_kunjungan"))
        End If

        FillDataDtlById(id)
    End Sub

    Sub controlRO()
        InpNama.Enabled = False
        InpBentuk.Enabled = False
        InpTujuan.Enabled = False
        InpTanggal.Enabled = False
        InpJabatan.Enabled = False
        InpHasil.Enabled = False
        InpTindak.Enabled = False
        txtAlamat.Enabled = False
        txtHasilKunjungan.Enabled = False
        FileUpload1.Enabled = False
        txtInfoFoto.Enabled = False
        btnAdd.Enabled = False
        cs.RegisterStartupScript(Page.[GetType](), "callFunction", "callReadOnly();", True)
        btnSave.Enabled = False
        allowDelete = False
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""

        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Yang Di Kontak", InpNama.Text)
        If lMsg <> "" Then
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Sub FillDataDtlById(ByVal Id)
        Dim ds As DataSet
        ds = oLkn.getDataLKNDtl(Id)
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            Dim ds As DataSet
            Dim filename As String = ""
            'ds = oLkn.insertupdateDataLkn(Request, appno.Value, filename, usrLogin)
            'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
            Try
                ds = oLkn.insertupdateDataLkn(Request, appno.Value, filename, usrLogin)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + ".');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
            Catch ex As Exception
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + ex.Message.Replace("'", "\'") + ".');window.opener.location.href = window.opener.location.href;parent.window.close();", True)
            End Try
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        cekInput()
        'cekInputUpload()
        Dim type As String = "LKN"
        'Dim data As String = Path.GetFileName(FileUpload1.FileName)
        'cs.RegisterStartupScript(Page.[GetType](), "callFunction", "test()", True)

        If lblNotif.Text = "" Then

            Dim filename As String = ""
            filename = Path.GetFileName(FileUpload1.FileName)
            txtFilename.Text = filename
            Dim files As String()
            files = filename.Split(".")
            Dim dt As DataTable
            dt = oGen.getDataExtentionFile(files(files.Length - 1).ToString())
            If dt.Rows.Count > 0 Then
                cekInputUpload(dt.Rows(0).Item("extention_group").ToString())

                If lblNotif.Text = "" Then
                    If dt.Rows(0).Item("extention_name").ToString() = "image" Then
                        Try
                            Dim mkFolder As String
                            mkFolder = "~/Document Upload/" & appno.Value & "/LKN/"

                            'if directory isn't exist || Server.MapPath("~/Document Upload/")
                            'check folder exists
                            If Not Directory.Exists(Server.MapPath(mkFolder)) Then
                                editpermit()
                                Directory.CreateDirectory(Server.MapPath(mkFolder))
                            End If

                            'check file exists
                            If System.IO.File.Exists(Server.MapPath(mkFolder) & filename) Then
                                System.IO.File.Delete(filename)
                                'liBodyLoadAction.Text = "alert('Tidak bisa upload file. File sudah ada.');window.location.href=window.location.href"
                            End If

                            FileUpload1.SaveAs(Server.MapPath(mkFolder) + filename)

                            Dim ds As DataSet
                            If appno.Value <> "" Then
                                Try
                                    oLkn.updateAttachmentFile(appno.Value, FileUpload1.FileName, type)

                                    ds = oLkn.insertupdateDataLkn(Request, appno.Value, filename, usrLogin)
                                    txtIdDtl.Text = ds.Tables(0).Rows(0).Item("lsld_id")

                                    FillDataDtlById(Request.QueryString("id"))
                                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();", True)

                                Catch ex As Exception
                                    lblNotif.Text = ex.Message
                                    divnotif.Attributes.Add("style", "display:''")
                                End Try
                            End If

                        Catch ex As Exception
                            liBodyLoadAction.Text = "alert('" & ex.Message & "');"

                        End Try
                    Else
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('File yang diupload bukan gambar/foto');", True)
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

            'folderUrl = mkFolder & "/"

            'Dim path As String = Server.MapPath(folderUrl)
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

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Try
            oLkn.delLKNDetail(dataItem("lsld_id"))
            FillDataDtlById(txtIdLkn.Text)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiHapus() + "')", True)
        Catch ex As Exception
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert(" + ex.Message + ")", True)
        End Try

    End Sub
End Class