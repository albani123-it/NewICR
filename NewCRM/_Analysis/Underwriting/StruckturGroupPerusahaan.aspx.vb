﻿Imports System.IO
Imports System.Security.AccessControl

Public Class StruckturGroupPerusahaan
    Inherits System.Web.UI.Page

    Dim oUsr As New lUsers
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oSgp As New lStrukturGroupPerusahaan
    Dim cs As ClientScriptManager = Page.ClientScript

    Protected dataTable As DataTable
    Protected sector As String = ""
    Protected pagename As String = "Struktur Grup Perusahaan"
    Dim type As String = "StrukturGroupPerusahaan"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""

    Public myTable As New DataTable
    Public RowMicro As Boolean = False
#Region " Global Variable "
    Dim usrLogin As String
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        'lblNotif.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        controlHome()

        myTable.Columns.Add("subgroup_id")
        myTable.Columns.Add("subgroup_code")
        myTable.Columns.Add("subgroup_name")
        myTable.Columns.Add("subgroup_desc")

        If Not IsPostBack Then
            'fillDdl()
            'bindGrid()
            'cekApp(Session("appno"))
        End If

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
        Dim dtTable(9) As String

        dtTable = oUsr.getUserDetail(usrLogin)

        'With dtTable
        '    If .Length > 0 Then

        '        hdCapem.Value = dtTable(9)
        '        txtSpokeCapem.Text = dtTable(10)
        '        If dtTable(8) <> "HEAD" Then
        '            Dim ds As DataSet
        '            Dim dt As DataTable
        '            ds = oUsr.getUserBranchParent(dtTable(9))
        '            dt = ds.Tables(0)
        '            If dt.Rows.Count > 0 Then
        '                hdKanwil.Value = dt.Rows(0).Item("KDCAB")
        '                txtKantorPusatKanwil.Text = dt.Rows(0).Item("CABANG")
        '            Else
        '                hdKanwil.Value = ""
        '                txtKantorPusatKanwil.Text = ""
        '            End If
        '        Else
        '            hdKanwil.Value = dtTable(9)
        '            txtKantorPusatKanwil.Text = dtTable(10)
        '        End If

        '    End If
        'End With

        'btnNext.Visible = False
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

    Sub checkMicro(ByVal AppNo)
        Dim dt As DataTable
        dt = oApp.getMicroApp(AppNo)

        If dt.Rows(0).Item("Ret") = "Yes" Then
            RowMicro = True
        Else
            RowMicro = False
        End If
    End Sub

    Sub cekApp(ByVal appno)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            isiData(appno)
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

    Sub isiData(ByVal app_no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oSgp.getDataStrukturPerusahaanByAppno(app_no)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            appno.Value = oGen.CheckNullString(dt.Rows(0).Item("lsgp_appno"))
            txtGroupCode.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgp_kode"))
            txtGroupName.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgp_nama"))
            txtGroupDesc.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgp_keterangan"))
            txtSubGroupId1.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgpd_id"))
            txtSubGroupCode1.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgpd_kode"))
            txtSubGroupName1.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgpd_nama"))
            txtSubgroupDesc1.Text = oGen.CheckNullString(dt.Rows(0).Item("lsgpd_keterangan"))
            textFile.Value = oGen.CheckNullString(dt.Rows(0).Item("lsgp_filename"))

            For index = 1 To dt.Rows.Count - 1
                Dim row As DataRow = myTable.NewRow()
                row("subgroup_id") = dt.Rows(index).Item("lsgpd_id")
                row("subgroup_code") = dt.Rows(index).Item("lsgpd_kode")
                row("subgroup_name") = dt.Rows(index).Item("lsgpd_nama")
                row("subgroup_desc") = dt.Rows(index).Item("lsgpd_keterangan")

                myTable.Rows.Add(row)
            Next

            Dim files As New List(Of ListItem)()
            files.Add(New ListItem("~/Document Upload/" & appno.Value & "/" + type + "/" + textFile.Value))
            GridView1.DataSource = files
            GridView1.DataBind()

        End If

      

        bindDataSourceEdit()
    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet
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

    Sub controlRO()
        txtGroupCode.Enabled = False
        txtGroupName.Enabled = False
        txtGroupDesc.Enabled = False
        txtSubGroupCode1.Enabled = False
        txtSubGroupName1.Enabled = False
        txtSubgroupDesc1.Enabled = False
        btnAdd.Enabled = False
        btnSave.Enabled = False

        If Repeater1.Items.Count > 0 Then
            For index = 0 To Repeater1.Items.Count - 1
                Dim btnRemove As ImageButton = DirectCast(Repeater1.Items(index).FindControl("btnRemove"), ImageButton)
                Dim txtSubGroupCode As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubGroupCode"), TextBox)
                Dim txtSubGroupName As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubGroupName"), TextBox)
                Dim txtSubGroupDesc As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubgroupDesc"), TextBox)

                txtSubGroupCode.Enabled = False
                txtSubGroupName.Enabled = False
                txtSubGroupDesc.Enabled = False
                btnRemove.Enabled = False
            Next
        End If

    End Sub

    Sub controlEdit()
    End Sub

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

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)

        PopulateData()
        myTable.Rows.Add(myTable.NewRow())

        Bind_Add()

    End Sub

    Protected Sub PopulateData()
        For Each item As RepeaterItem In Repeater1.Items

            Dim txtSubGroupId As TextBox = DirectCast(item.FindControl("txtSubGroupId"), TextBox)
            Dim txtSubGroupCode As TextBox = DirectCast(item.FindControl("txtSubGroupCode"), TextBox)
            Dim txtSubGroupName As TextBox = DirectCast(item.FindControl("txtSubGroupName"), TextBox)
            Dim txtSubGroupDesc As TextBox = DirectCast(item.FindControl("txtSubgroupDesc"), TextBox)

            Dim row As DataRow = myTable.NewRow()

            row("subgroup_id") = txtSubGroupId.Text
            row("subgroup_code") = txtSubGroupCode.Text
            row("subgroup_name") = txtSubGroupName.Text
            row("subgroup_desc") = txtSubGroupDesc.Text

            myTable.Rows.Add(row)
        Next
    End Sub

    Protected Sub Bind_Add()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()
        If Repeater1.Items.Count > 0 Then
            For index = 0 To Repeater1.Items.Count - 1
                Dim txtSubGroupId As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubGroupId"), TextBox)
                Dim txtSubGroupCode As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubGroupCode"), TextBox)
                Dim txtSubGroupName As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubGroupName"), TextBox)
                Dim txtSubGroupDesc As TextBox = DirectCast(Repeater1.Items(index).FindControl("txtSubgroupDesc"), TextBox)

                txtSubGroupId.Text = If(IsDBNull(myTable.Rows(index).Item("subgroup_id")), "0", myTable.Rows(index).Item("subgroup_id"))
                txtSubGroupCode.Text = If(IsDBNull(myTable.Rows(index).Item("subgroup_code")), "", myTable.Rows(index).Item("subgroup_code"))
                txtSubGroupName.Text = If(IsDBNull(myTable.Rows(index).Item("subgroup_name")), "", myTable.Rows(index).Item("subgroup_name"))
                txtSubGroupDesc.Text = If(IsDBNull(myTable.Rows(index).Item("subgroup_desc")), "", myTable.Rows(index).Item("subgroup_desc"))

            Next
        End If

    End Sub

    Protected Sub Repeater1_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs) Handles Repeater1.ItemCommand
        PopulateData()
        Dim x = myTable.Rows(e.Item.ItemIndex).ItemArray(0)
        'oSgp.deleteStrukturGroupDtl(appno.Value, x)
        myTable.Rows(e.Item.ItemIndex).Delete()
        Bind_Add()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CekInput()

        If liBodyLoadAction.Text = "" Then
            Dim _kodegroup = ""
            Dim _namagroup = ""
            Dim _keterangan = ""
            Dim _idsubgroup1 = ""
            Dim _kodesubgroup1 = ""
            Dim _namasubgroup1 = ""
            Dim _ketsubgroup1 = ""
            Dim _idsubgroup = ""
            Dim _kodesubgroup = ""
            Dim _namasubgroup = ""
            Dim _ketsubgroup = ""

            _kodegroup = txtGroupCode.Text
            _namagroup = txtGroupName.Text
            _keterangan = txtGroupDesc.Text
            _idsubgroup1 = txtSubGroupId1.Text
            _kodesubgroup1 = txtSubGroupCode1.Text
            _namasubgroup1 = txtSubGroupName1.Text
            _ketsubgroup1 = txtSubgroupDesc1.Text

            Dim dSet As DataSet
            'Dim dTbl As DataTable
            oSgp.insertStrukturGroupHdr(_kodegroup, _namagroup, _keterangan, appno.Value, textFile.Value, usrLogin)
            'If _kodesubgroup1 <> "" Then
            dSet = oSgp.insertStrukturGroupDtl(appno.Value, _idsubgroup1, _kodesubgroup1, _namasubgroup1, _ketsubgroup1, usrLogin)
            'End If

            'dTbl = dSet.Tables(0)
            'txtSubGroupId1.Text = dTbl.Rows(0).Item("lsgpd_id")

            If Repeater1.Items.Count > 0 Then
                For Each i As RepeaterItem In Repeater1.Items
                    Dim txtIdgroup As TextBox = DirectCast(i.FindControl("txtSubGroupId"), TextBox)
                    Dim txtKodeSubgroup As TextBox = DirectCast(i.FindControl("txtSubGroupCode"), TextBox)
                    Dim txtNamaSubGroup As TextBox = DirectCast(i.FindControl("txtSubGroupName"), TextBox)
                    Dim txtKetSubgroup As TextBox = DirectCast(i.FindControl("txtSubgroupDesc"), TextBox)

                    _idsubgroup = txtIdgroup.Text
                    _kodesubgroup = txtKodeSubgroup.Text
                    _namasubgroup = txtNamaSubGroup.Text
                    _ketsubgroup = txtKetSubgroup.Text

                    dSet = oSgp.insertStrukturGroupDtl(appno.Value, _idsubgroup, _kodesubgroup, _namasubgroup, _ketsubgroup, usrLogin)
                    'dTbl = dSet.Tables(0)
                    'txtIdgroup.Text = dTbl.Rows(0).Item("lsgpd_id")

                Next
            End If

            'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil disimpan.');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
        End If

    End Sub

    Sub CekInput()
        Dim lMsg As String = ""

        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Kode Group", txtGroupCode.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Group", txtGroupName.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Keterangan", txtGroupDesc.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Kode Sub Group ke 1", txtSubGroupCode1.Text)
        'lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Sub Group Ke 1", txtSubGroupName1.Text)

        If txtSubGroupCode1.Text <> "" Then
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Sub Group Ke 1", txtSubGroupName1.Text)
        End If

        Dim count As Integer = 2

        If Repeater1.Items.Count > 0 Then
            For Each i As RepeaterItem In Repeater1.Items
                Dim txtKodegroup As TextBox = DirectCast(i.FindControl("txtSubGroupCode"), TextBox)
                Dim txtNamaSubGroup As TextBox = DirectCast(i.FindControl("txtSubGroupName"), TextBox)

                lMsg &= oGen.checkInputInaJavascript("TextBox", "Kode Sub Group ke " & count & "", txtKodegroup.Text)
                lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Sub Group Ke " & count & "", txtNamaSubGroup.Text)
                count = count + 1
            Next
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + lMsg + "')", True)
        End If
    End Sub

    Sub bindDataSourceEdit()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()

        For j As Integer = 0 To Repeater1.Items.Count - 1

            Dim txtIdSubgroup As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtSubGroupId"), TextBox)
            Dim txtKodeSubgroup As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtSubGroupCode"), TextBox)
            Dim txtNamaSubgroup As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtSubGroupName"), TextBox)
            Dim txtKetSubgroup As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtSubgroupDesc"), TextBox)

            txtIdSubgroup.Text = myTable.Rows(j)("subgroup_id").ToString()
            txtKodeSubgroup.Text = myTable.Rows(j)("subgroup_code").ToString()
            txtNamaSubgroup.Text = myTable.Rows(j)("subgroup_name").ToString()
            txtKetSubgroup.Text = myTable.Rows(j)("subgroup_desc").ToString()
        Next
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim check As String = ""
        If lblNotif.Text = "" Then
            Dim fileList As String()

            Dim files As HttpPostedFile = DirectCast(FileUpload1.PostedFile, HttpPostedFile)

            Dim filename As String = ""
            filename = Path.GetFileName(FileUpload1.FileName)
            fileList = filename.Split(".")
            Dim dTbl As DataTable
            dTbl = oGen.getDataExtentionFile(fileList(fileList.Length - 1).ToString())
            If dTbl.Rows.Count > 0 Then
                If dTbl.Rows(0).Item("extention_name").ToString() = "image" Then
                    check = oGen.checkUploadData(dTbl.Rows(0).Item("extention_group").ToString(), files.ContentLength)
                    If check = "" Then
                        Dim mkFolder As String
                        mkFolder = "~/Document Upload/" & appno.Value & "/" + type + "/"

                        Try
                            If Not Directory.Exists(Server.MapPath(mkFolder)) Then
                                editpermit()
                                Directory.CreateDirectory(Server.MapPath(mkFolder))
                            End If
                            If System.IO.File.Exists(Server.MapPath(mkFolder) & filename) Then
                                editpermit()
                                Dim FileToDelete As String
                                FileToDelete = Server.MapPath(mkFolder) & filename
                                File.Delete(FileToDelete)
                            End If

                            FileUpload1.SaveAs(Server.MapPath(mkFolder) + filename)
                            textFile.Value = filename

                            Dim filePaths As String() = Directory.GetFiles(Server.MapPath(mkFolder))
                            Dim filez As New List(Of ListItem)()
                            For Each filePath As String In filePaths
                                Dim nameFile As String = Path.GetFileName(filePath)
                                If filename = nameFile Then
                                    filez.Add(New ListItem("~/Document Upload/" & appno.Value & "/" + type + "/" + nameFile))
                                End If
                            Next
                            GridView1.DataSource = filez
                            GridView1.DataBind()
                        Catch ex As Exception
                            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + ex.Message + "');", True)
                        End Try
                    Else
                        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + check + "');", True)
                    End If

                Else
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('File yang diupload bukan gambar/foto');", True)

                End If
            Else
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('File yang diupload bukan gambar/foto');", True)

            End If
        End If
    End Sub

    Protected Sub editpermit()
        Dim securityRules As DirectorySecurity = New DirectorySecurity()
        securityRules.AddAccessRule(New FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow))
    End Sub

End Class