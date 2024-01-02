Imports System.Drawing
Imports System.IO
Imports System.Security.AccessControl
Imports Microsoft.VisualBasic
Imports System.Net.Mime
Public Class CreditBusiness
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Memorandum Bisnis Kredit"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Protected proses As Integer = 1

#Region " Global Variable "
    Dim usrLogin As String
#End Region
    Private Shared _me As New CreditBusiness()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        lblNotif.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        controlHome()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If
        If Session("appno") IsNot Nothing Then
            cekSector(Session("appno"))
            appno.Value = Session("appno")
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
            If Not IsPostBack Then
                cekApp(Session("appno"))
            End If
            cekWorkflow(Session("appno"))
            executeJavascript()
        Else
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
        End If
    End Sub

    Sub executeJavascript()
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    url = dataTable.Rows(i).Item("lsm_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "a_aBc('" & url & "','" & pagename & "','" & appno.Value & "');"
        Dim javascript = "<script type='text/javascript'>" & script & "</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
    End Sub

    Sub cekSector(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerSector(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            sector = dt.Rows(0).Item("brw_sec")
        End If
    End Sub

    Sub getMenuNext()
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            Dim dt As DataTable
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    dt = oApp.getNextMenuByWorkflowRole(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dataTable.Rows(i).Item("lsm_id"), sector)
                    If dt.Rows.Count > 0 Then
                        menuNext = dt.Rows(0).Item("lsm_link")
                    Else
                        Dim dt2 As DataTable
                        dt2 = oApp.getDetailOfMenu(dataTable.Rows(i).Item("lsm_id"))
                        If dt2.Rows.Count > 0 Then
                            menuNext = dt2.Rows(0).Item("lsm_link")
                        End If
                    End If
                End If
            Next
        Else
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    Dim dt2 As DataTable
                    dt2 = oApp.getDetailOfMenu(dataTable.Rows(i).Item("lsm_id"))
                    If dt2.Rows.Count > 0 Then
                        menuNext = dt2.Rows(0).Item("lsm_link")
                    End If
                End If
            Next
        End If

    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        dt = oApp.getChildOfMenu(id, sector)
        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lsm_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        dt = oApp.getChildOfMenu(id, sector)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To dtWorkflow.Rows.Count - 1
                    If dtWorkflow.Rows(j).Item("lwr_field") = dt.Rows(i).Item("lsm_field") Then
                        If dtWorkflow.Rows(j).Item("lwr_role") = "HD" Then
                            count = count + 1
                        End If
                    End If
                Next
            Next
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lsm_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

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

    Sub controlHome()
        btnNext.Visible = False
    End Sub

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                CheckReportMBK()
                isiData(no, dtbl.Rows(0).Item("app_brw_code"))
                brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
            End If
            lblAppNo_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("app_no"))
            lblBrwName_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("lsb_namaPemohon"))
        End If
    End Sub

    Sub cekWorkflow(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                execWorkflow(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_convent"))
                If usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If
            End If
        End If
    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet
        'Dim dt As DataTable
        ds = oApp.checkWorkflowRole(workflow, posisi, sector)
        dtWorkflow = ds.Tables(0)
        For j As Integer = 0 To dtWorkflow.Rows.Count - 1
            If dtWorkflow.Rows(j).Item("lsm_name") = pagename Then
                If dtWorkflow.Rows(j).Item("lwr_role") = "RO" Then
                    controlRO()
                Else
                    If Not convent Then
                        If dtWorkflow.Rows(j).Item("lsm_edit") = 0 Then
                            controlRO()
                        ElseIf dtWorkflow.Rows(j).Item("lsm_edit") = 2 Then
                            controlEdit()
                        End If
                    End If
                End If
            End If

        Next
    End Sub

    Sub controlEdit()

    End Sub

    Sub controlRO()
        txtAspekUmumMgmt.Enabled = False
        txtHubunganBank.Enabled = False
        txtAspekPemasaran.Enabled = False
        txtAspekTeknisProduksiPembelian.Enabled = False
        txtAspekKeuangan.Enabled = False
        txtPotensiKebutuhanProduk.Enabled = False
        txtTrackRecordPerusahaanDanPemilik.Enabled = False
        txtKebutuhanKredit.Enabled = False
        txtJaminan.Enabled = False
        txtKesimpulan.Enabled = False

        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Sub isiData(ByVal no, ByVal brw)
        Dim ds As DataSet
        Dim ds2 As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrower(brw)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblNamaPerusahaan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaPemohon"))
            lblGrupPerusahaan.Text = oGen.CheckNullString(dt.Rows(0).Item("lblGrupUsaha"))
            lblAlamatPerusahaan.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_alamatRmh"))
            lblBidangUsaha.Text = oGen.CheckNullString(dt.Rows(0).Item("lblBidangUsaha"))
        Else
            lblNotif.Text &= "Data informasi nasabah tidak ada. Silahkan isi informasi nasabah terlebih dahulu.. Klik <a href='Borrower.aspx' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman informasi nasabah.<br/>"
            divnotif.Attributes.Add("style", "display:''")
            controlRO()
        End If

        ds = oApp.getDetailOfRequestCredit(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblJenisPAK.Text = oGen.CheckNullString(dt.Rows(0).Item("lblJenisPengajuan"))
            lblTujuanPAK.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_peruntukan"))
        Else
            lblNotif.Text &= "Data permohonan kredit tidak ada. Silahkan isi permohonan kredit terlebih dahulu. Klik <a href='FacilityCredit.aspx?appno=" & no & "' style='text-decoration:underline;color:red;font-weight:bold;'>disini</a> untuk ke halaman permohonan kredit.<br/>"
            divnotif.Attributes.Add("style", "display:''")
            controlRO()
        End If

        

        ds = oApp.checkMemoBisnis(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            txtAspekUmumMgmt.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_aspekMgmt"))
            txtHubunganBank.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_hubunganBank"))
            txtAspekPemasaran.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_aspekPemasaran"))
            txtAspekTeknisProduksiPembelian.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_aspekTeknis"))
            txtAspekKeuangan.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_aspekKeuangan"))
            txtPotensiKebutuhanProduk.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_potensiKebutuhanProduk"))
            txtTrackRecordPerusahaanDanPemilik.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_trackRecord"))
            txtKebutuhanKredit.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_kebutuhanKredit"))
            txtJaminan.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_jaminan"))
            txtKesimpulan.Text = oGen.CheckNullString(dt.Rows(0).Item("lmk_kesimpulan"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            If appno.Value <> "" Then
                Try
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkMemoBisnis(appno.Value)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update data
                        oApp.updateMemoBisnis(Request, appno.Value, usrLogin)
                    Else
                        'insert data
                        oApp.insertMemoBisnis(Request, appno.Value, usrLogin)
                    End If
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                End Try
            End If
        End If
    End Sub

    Sub cekInput()

    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblNotif.Text = ""
        cekInputUpload()
        Dim type As String = "MBK"

        If lblNotif.Text = "" Then
            Dim mkFolder As String
            mkFolder = "~/Document Upload/" & appno.Value & "/" + type + "/"
            Dim filename As String = ""
            filename = type + "_" + appno.Value + ".pdf"
            Try
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

                lblFileUpload.Text = filename

            Catch ex As Exception

                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & ex.Message & ".');", True)

            End Try
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & lblNotif.Text & ".');", True)
        End If
    End Sub

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Sub CheckReportMBK()
        Dim path As String
        Dim filename As String
        Dim type As String = "MBK"
        filename = type + "_" + appno.Value + ".pdf"
        path = "~/Document Upload/" & appno.Value & "/" + type + "/" + filename
        If File.Exists(Server.MapPath(path)) Then
            lblFileUpload.Text = filename
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

            Dim _filename = filename.Split(".")

            filename = _filename(_filename.Length - 1)

            If filesize > 20971520 Then
                lMsg = lMsg + "File lebih dari 20 MB.<br/>"
            End If

            If filename.ToLower() <> "pdf" Then
                lMsg = lMsg + "Bukan File PDF"
            End If
        Else
            lMsg = lMsg + "Pilih file dahulu diisi"
        End If

        If lMsg <> "" Then
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub
   
    
End Class