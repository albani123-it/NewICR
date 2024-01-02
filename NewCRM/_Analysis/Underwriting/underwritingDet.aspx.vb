Imports System.Drawing
Imports System.IO
Imports System.Security.AccessControl
Imports Microsoft.VisualBasic
Imports System.Net.Mime

Public Class underwritingDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript
    'Dim stat As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        'If Request.QueryString("appno") IsNot Nothing And Request.QueryString("appno") <> "-1" Then
        '    appno.Value = Request.QueryString("appno")
        '    lblAppno.Text = Request.QueryString("appno")
        '    lblAppNoEx.Text = Request.QueryString("appno")
        '    'checkMicro(appno.Value)
        'End If
        'If Request.QueryString("brw_code") IsNot Nothing And Request.QueryString("brw_code") <> "-1" Then
        '    brw_code.Value = Request.QueryString("brw_code")
        '    lblBrwCode.Text = Request.QueryString("brw_code")
        '    lblBrwCodeEx.Text = Request.QueryString("brw_code")
        'End If

        If Not IsPostBack Then
            fillDdl()
        End If

        If Request.QueryString("appno") = "-1" And Request.QueryString("brw_code") = "-1" Then
            'If Not IsPostBack Then
            '    isiCode()
            'End If

            'stat = "New"
            'tblNew.Attributes.Add("style", "display:'';")
            'tblExists.Attributes.Add("style", "display:none;")
            'Else
            'tblNew.Attributes.Add("style", "display:none;")
            'tblExists.Attributes.Add("style", "display:'';")
        End If
    End Sub

    'Sub checkMicro(ByVal AppNo)
    '    Dim dt As DataTable
    '    dt = oApp.getMicroApp(AppNo)

    '    If dt.Rows(0).Item("Ret") = "Yes" Then
    '        RowMicro.Visible = True
    '    Else
    '        RowMicro.Visible = False
    '    End If
    'End Sub


    Sub isiCode()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getCounter("Application")
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            appno.Value = dt.Rows(0).Item("lsc_app_no")
            lblAppno.Text = dt.Rows(0).Item("lsc_app_no")
            'lblAppNoEx.Text = dt.Rows(0).Item("lsc_app_no")
        End If
        ds = oApp.getCounter("Borrower")
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            brw_code.Value = dt.Rows(0).Item("lsc_brw_code")
            lblBrwCode.Text = dt.Rows(0).Item("lsc_brw_code")
            'lblBrwCodeEx.Text = dt.Rows(0).Item("lsc_brw_code")
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

    Sub fillDdl()
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oApp.getListOfWorkflow()
        dt = ds.Tables(0)
        ddlWorkflow.Items.Clear()
        ddlWorkflow.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlWorkflow.Items.Add(New ListItem(dt.Rows(i).Item("lsw_name"), dt.Rows(i).Item("lsw_id")))
                ddlWorkflowEx.Items.Add(New ListItem(dt.Rows(i).Item("lsw_name"), dt.Rows(i).Item("lsw_id")))
            Next
        End If

        ds = oApp.getListOfBorrowerType()
        dt = ds.Tables(0)
        ddlBrwType.Items.Clear()
        ddlBrwTypEx.Items.Clear()
        ddlBrwType.Items.Add(New ListItem("Pilih", "Pilih"))
        ddlBrwTypEx.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlBrwType.Items.Add(New ListItem(dt.Rows(i).Item("brw_type"), dt.Rows(i).Item("brw_id")))
                ddlBrwTypEx.Items.Add(New ListItem(dt.Rows(i).Item("brw_type"), dt.Rows(i).Item("brw_id")))
            Next
        End If

        ds = oApp.getListOfRetail()
        dt = ds.Tables(0)
        ddlRetail.Items.Clear()
        ddlRetailEx.Items.Clear()
        ddlRetail.Items.Add(New ListItem("Pilih", "Pilih"))
        ddlRetailEx.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlRetail.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
                ddlRetailEx.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
            Next
        End If

        ds = oApp.getListOfProduct()
        dt = ds.Tables(0)
        ddlPrdType.Items.Clear()
        ddlPrdTypEx.Items.Clear()
        ddlPrdType.Items.Add(New ListItem("Pilih", "Pilih"))
        ddlPrdTypEx.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlPrdType.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
                ddlPrdTypEx.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
            Next
        End If

        dt = oRat.getDropDownList("RFAPPLICATIONTYPE")
        ddlJenisPengajuan.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlJenisPengajuan.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If
        ddlJenisPengajuan.SelectedValue = "B1"
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            'If appno.Value <> "" Then
            Try
                Dim arr(4)
                arr(0) = 1
                arr(1) = Request.Form("ddlBrwType")
                arr(2) = Request.Form("ddlPrdType")
                arr(3) = "B1"
                arr(4) = ""

                Dim ds As DataSet
                Dim dt As DataTable

                If RadExist.Checked Then
                    arr(3) = Request.Form("ddlJenisPengajuan")
                    arr(4) = Request.Form("txtNoAppEx")
                    ds = oApp.insertUnderwriting(arr, Request.Form("lblBrwCode"), RadExist.Checked, usrLogin)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then

                        If arr(3) <> "B1" Then
                            '==========copy folder and content==========
                            Dim oldFolder As String
                            Dim newFolder As String
                            oldFolder = "~/Document Upload/" & arr(4) & "/"
                            newFolder = "~/Document Upload/" & dt.Rows(0).Item("app_no") & "/"

                            Try
                                CopyDirectory(Server.MapPath(oldFolder), Server.MapPath(newFolder))

                            Catch ex As Exception
                                liBodyLoadAction.Text = "alert('" & ex.Message & "');"

                            End Try
                            '==========end copy folder and content======
                        End If
                        'oApp.insertCounter(appno.Value, Request.Form("lblBrwCode"), RadExist.Checked)
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');parent.window.close();window.opener.location.href = 'General.aspx?appno=" & dt.Rows(0).Item("app_no") & "';"
                    End If
                Else
                    ds = oApp.insertUnderwriting(arr, "", RadExist.Checked, usrLogin)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');parent.window.close();window.opener.location.href = 'General.aspx?appno=" & dt.Rows(0).Item("app_no") & "';"
                    End If
                End If

        'liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');parent.window.close();window.opener.location.href = 'General.aspx?appno=" & appno.Value & "';"
        'liBodyLoadAction.Text = "alert('Data berhasil di simpan.');parent.window.close();window.opener.location.href = 'General.aspx?appno=" & appno.Value & "';"
            Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        End Try
            'End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        'If RadExist.Checked = True Then
        '    lMsg &= oGen.checkInputInaJavascript("TextBox", "Kode Borrower", lblBrwCode.Text)

        '    'If chkRetailEx.Checked Then
        '    '    lMsg &= oGen.checkInputInaJavascript("DropDownList", "Product Micro Type", ddlRetailEx.SelectedValue)
        '    'Else
        '    lMsg &= oGen.checkInputInaJavascript("DropDownList", "Borrower Type", ddlBrwTypEx.SelectedValue)
        '    lMsg &= oGen.checkInputInaJavascript("DropDownList", "Product Type", ddlPrdTypEx.SelectedValue)
        '    'End If
        'Else

        'If chkRetail.Checked Then
        '    lMsg &= oGen.checkInputInaJavascript("DropDownList", "Product Micro Type", ddlRetail.SelectedValue)
        'Else
        lMsg &= oGen.checkInputInaJavascript("DropDownList", "Jenis Pengajuan", Request.Form("ddlJenisPengajuan"))
        lMsg &= oGen.checkInputInaJavascript("DropDownList", "Borrower Type", ddlBrwType.SelectedValue)
        lMsg &= oGen.checkInputInaJavascript("DropDownList", "Product Type", ddlPrdType.SelectedValue)
        'End If

        'End If
        If RadExist.Checked = True Then
            'lMsg &= oGen.checkInputInaJavascript("TextBox", "No. CIF", lblBrwCode.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "No. CIF", Request.Form("lblBrwCode"))
            If Request.Form("ddlJenisPengajuan") <> "B1" And Request.Form("ddlJenisPengajuan") <> "Pilih" Then
                lMsg &= oGen.checkInputInaJavascript("TextBox", "No. Aplikasi Existing", txtNoAppEx.Text)
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function getBorrowerCodeNew(ByVal status As String) As String
        Dim result As String = ""
        With New lApps
            Dim ds As DataSet
            Dim dt As DataTable
            ds = .getCounter("Borrower")
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                result = dt.Rows(0).Item("lsc_brw_code")
            End If
        End With
        Return result
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function checkBrwCode(ByVal cif As String) As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim json As String = ""
        Dim Err As String = ""

        With New lApps
            ds = .checkAppByNPWP(cif)
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                json = cif & "," & dt.Rows(0).Item("app_brw_type") & "," & dt.Rows(0).Item("app_prd_type") & "," & dt.Rows(0).Item("lsb_namaPemohon")
            Else
                json = "No. NPWP tidak ada."
            End If
        End With

        Return json
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function checkApps(ByVal appnocif As String) As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim json As String = ""
        Dim Err As String = ""

        Dim temp = appnocif.ToString.Split(",")
        Dim appno As String = temp(0)
        Dim cif As String = temp(1)

        With New lApps
            ds = .checkAppWithNPWP(appno, cif)
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                json = appno & "," & dt.Rows(0).Item("app_prd_type")
            Else
                json = "No. Aplikasi tidak ada."
            End If
        End With

        Return json
    End Function

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

    Public Sub CopyDirectory(ByVal sourcePath As String, ByVal destinationPath As String)
        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)

        'delete existing folder for destination path
        If System.IO.Directory.Exists(destinationPath) Then
            System.IO.Directory.Delete(destinationPath)
        End If

        ' If the destination folder don't exist then create it
        If Not System.IO.Directory.Exists(destinationPath) Then
            System.IO.Directory.CreateDirectory(destinationPath)
        End If

        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            Dim destinationFileName As String =
                System.IO.Path.Combine(destinationPath, fileSystemInfo.Name)

            ' Now check whether its a file or a folder and take action accordingly
            If TypeOf fileSystemInfo Is System.IO.FileInfo Then
                System.IO.File.Copy(fileSystemInfo.FullName, destinationFileName, True)
            Else
                ' Recursively call the mothod to copy all the neste folders
                CopyDirectory(fileSystemInfo.FullName, destinationFileName)
            End If
        Next
    End Sub

    'Private Sub btnSaveEx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveEx.Click
    '    If liBodyLoadAction.Text = "" Then
    '        If appno.Value <> "" Then
    '            Try
    '                Dim arr(4)
    '                If RadExist.Checked Then
    '                    'arr(0) = ddlWorkflow.SelectedValue
    '                    arr(0) = 1
    '                    arr(1) = ddlBrwType.SelectedValue
    '                    arr(2) = ddlPrdType.SelectedValue
    '                    'arr(3) = chkRetail.Checked
    '                    'arr(4) = ddlRetail.SelectedValue

    '                    oApp.insertUnderwriting(arr, appno.Value, lblBrwCode.Text, usrLogin)
    '                    If RadNew.Checked = True Then
    '                        oApp.insertCounter(appno.Value, lblBrwCode.Text, usrLogin)
    '                    End If
    '                Else
    '                    'arr(0) = ddlWorkflowEx.SelectedValue
    '                    arr(0) = 1
    '                    arr(1) = ddlBrwTypEx.SelectedValue
    '                    arr(2) = ddlPrdTypEx.SelectedValue
    '                    'arr(3) = chkRetailEx.Checked
    '                    'arr(4) = ddlRetailEx.SelectedValue

    '                    oApp.insertUnderwritingExists(arr, appno.Value, lblBrwCode.Text, usrLogin)
    '                End If
    '                'oApp.insertUnderwritingExists(arr, appno.Value, brw_code.Value, usrLogin)
    '                liBodyLoadAction.Text = "alert('Data berhasil di simpan.');parent.window.close();window.opener.location.href = 'General.aspx?appno=" & appno.Value & "';"
    '            Catch ex As Exception
    '                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
    '            End Try
    '        End If
    '    End If
    'End Sub
End Class