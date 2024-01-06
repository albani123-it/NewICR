Public Class FacilityCreditInfo
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Permohonan Fasilitas Kredit"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Dim usrLogin As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillDdl()
        End If
        liBodyLoadAction.Text = ""
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

    Sub controlHome()
        btnNext.Visible = False
        TxtKurs.ReadOnly = True
        TxtKurs.CssClass = "form-control"
        'TxtNilaiValas.ReadOnly = True
        'TxtNilaiValas.CssClass = "inpTxtDisabled"
        'CheckValas()
    End Sub

    Sub CheckValas()
        Dim ds As DataSet
        Dim dt As DataTable
        Dim app_no As String = Request.QueryString("appno")
        ds = oRat.checkFacilityCredit(app_no)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then

            If DdlJenisCurr.SelectedValue = "IDR" Then
                TxtNilaiRp.ReadOnly = False
                TxtNilaiRp.CssClass = "inpTxt"
                TxtNilaiValas.CssClass = "inpTxtDisabled"
                TxtNilaiValas.Text = 0

                'DdlHouse.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_rumah"))
                TxtNilaiRp.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiRupiah"))
            ElseIf DdlJenisCurr.SelectedValue = "USD" Then
                TxtNilaiRp.ReadOnly = True
                TxtNilaiRp.CssClass = "inpTxtDisabled"
                TxtNilaiValas.CssClass = "inpTxt"
                TxtNilaiValas.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiValas"))
                TxtNilaiRp.Text = 0
            End If




        End If

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

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_no")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                isiData(dtbl.Rows(0).Item("app_no"))
                appno.Value = dtbl.Rows(0).Item("app_no")
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

    Sub isiData(ByVal appno)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oRat.checkFacilityCredit(appno)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            DdlJenisPengajuan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisPengajuan"))
            'TxtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_nama"))
            DdlFasilitas.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_fasilitas"))
            txtNilaiProyek.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiProyek"))
            'TxtAlamat.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_alamat"))
            DdlSitemPembayaran.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisPembayaran"))
            TxtPeruntukan.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_peruntukan"))
            DdlJenisCurr.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisCurr"))
            If DdlJenisCurr.SelectedValue = "IDR" Then
                TxtNilaiRp.ReadOnly = False
                TxtNilaiRp.CssClass = "inpTxt"
                TxtNilaiValas.CssClass = "inpTxtDisabled"
                TxtNilaiValas.Text = 0

                'DdlHouse.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_rumah"))
                TxtNilaiRp.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiRupiah"))
            ElseIf DdlJenisCurr.SelectedValue = "USD" Then
                TxtNilaiRp.ReadOnly = True
                TxtNilaiRp.CssClass = "inpTxtDisabled"
                TxtNilaiValas.CssClass = "inpTxt"
                TxtNilaiValas.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiValas"))
                TxtNilaiRp.Text = 0
            End If
            DdlSifat.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_sifat"))
            TxtKurs.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_kurs"))
            'DdlTujuan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_tujuan"))
            'TxtNilaiValas.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiValas"))
            'DdlHouse.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_rumah"))
            'TxtNilaiRp.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_nilaiRupiah"))
            'DdlSektor.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_sektor"))
            TxtJangkaWaktu.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_jangkaWaktu"))
            'DdlSubsektor.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_subSektor"))
            TxtGrace.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_grace"))
            TxtBunga.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_bungaPersen"))
            RadBunga.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lfc_jenisBunga"))
            TxtTotal.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lfc_total"))
            'If Not oGen.CheckNullString(dt.Rows(0).Item("lfc_availibility_periode")) = "" Then
            '    ddlTahunAvailibility.SelectedValue = Mid(String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lfc_availibility_periode")), 7, 4)
            '    ddlBulanAvailibility.SelectedValue = CInt(Mid(String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lfc_availibility_periode")), 4, 2))
            'End If


            'txtAvailibilityPeriode.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("lfc_availibility_periode")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("lfc_availibility_periode")))
            txtAvailibilityPeriode.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_availibility_periode"))
        End If
    End Sub

    Sub fillDdl()
        Dim dr As DataRow
        Dim dtJenis As New DataTable
        
        dtJenis.Columns.Add("Value")

        Dim dtJenisPengajuan As DataTable
        dtJenisPengajuan = oRat.getDropDownList("RFAPPLICATIONTYPE")
        DdlJenisPengajuan.DataSource = dtJenisPengajuan
        DdlJenisPengajuan.DataTextField = "ldd_desc"
        DdlJenisPengajuan.DataValueField = "ldd_code"
        DdlJenisPengajuan.DataBind()

        Dim dtFasilitas As DataTable
        dtFasilitas = oRat.getDropDownList("RFPRODUCT")
        DdlFasilitas.DataSource = dtFasilitas
        DdlFasilitas.DataTextField = "ldd_desc"
        DdlFasilitas.DataValueField = "ldd_code"
        DdlFasilitas.DataBind()

        Dim dtSistemPembayaran As DataTable
        dtSistemPembayaran = oRat.getDropDownList("RFPAYMENTSYS")
        DdlSitemPembayaran.DataSource = dtSistemPembayaran
        DdlSitemPembayaran.DataTextField = "ldd_desc"
        DdlSitemPembayaran.DataValueField = "ldd_code"
        DdlSitemPembayaran.DataBind()

            Dim dtJenisCurr As DataTable
            dtJenisCurr = oRat.getDropDownList("RFCURRENCY")
            DdlJenisCurr.DataSource = dtJenisCurr
            DdlJenisCurr.DataTextField = "ldd_desc"
            DdlJenisCurr.DataValueField = "ldd_code"
            DdlJenisCurr.DataBind()
      
        Dim dtSifat As DataTable
        dtSifat = oRat.getDropDownList("RFSIFATKREDIT")
        DdlSifat.DataSource = dtSifat
        DdlSifat.DataTextField = "ldd_desc"
        DdlSifat.DataValueField = "ldd_code"
        DdlSifat.DataBind()

        'Dim dtTujuan As DataTable
        'dtTujuan = oRat.getDropDownList("RFPRODUCT")
        'DdlTujuan.DataSource = dtTujuan
        'DdlTujuan.DataTextField = "ldd_desc"
        'DdlTujuan.DataValueField = "ldd_code"
        'DdlTujuan.DataBind()

        'Dim dtHouse As DataTable
        'dtHouse = oRat.getDropDownList("RFHOUSETYPE")
        'DdlHouse.DataSource = dtHouse
        'DdlHouse.DataTextField = "ldd_desc"
        'DdlHouse.DataValueField = "ldd_code"
        'DdlHouse.DataBind()

        'Dim dtSektor As DataTable
        'dtSektor = oRat.getDropDownList("RFBUSINESSSECTOR")
        'DdlSektor.DataSource = dtSektor
        'DdlSektor.DataTextField = "ldd_desc"
        'DdlSektor.DataValueField = "ldd_code"
        'DdlSektor.DataBind()


        'Dim dtSubSektor As DataTable
        'dtSubSektor = oRat.getDropDownList("RFBUSINESSSUBSECTOR")
        'DdlSubsektor.DataSource = dtSubSektor
        'DdlSubsektor.DataTextField = "ldd_desc"
        'DdlSubsektor.DataValueField = "ldd_code"
        'DdlSubsektor.DataBind()

        'Dim year As String = Now.ToString("yyyy")
        'Dim x As Integer = CInt(year)
        'ddlTahunAvailibility.Items.Add(New ListItem("Pilih", "Pilih"))
        'While (x > 2000)
        '    ddlTahunAvailibility.Items.Add(New ListItem(x.ToString, x.ToString))
        '    x = x - 1
        'End While

        'Dim month As Integer = 1
        'ddlBulanAvailibility.Items.Add(New ListItem("Pilih", "Pilih"))
        'While (month <= 12)
        '    ddlBulanAvailibility.Items.Add(New ListItem(month.ToString, month.ToString))
        '    month = month + 1
        'End While
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
        DdlJenisPengajuan.Enabled = False
        DdlJenisPengajuan.CssClass = "inpDdlDisabled"
        DdlFasilitas.Enabled = False
        DdlFasilitas.CssClass = "inpDdlDisabled"
        DdlSitemPembayaran.Enabled = False
        DdlSitemPembayaran.CssClass = "inpDdlDisabled"
        DdlJenisCurr.Enabled = False
        DdlJenisCurr.CssClass = "inpDdlDisabled"
        TxtKurs.Enabled = False
        TxtNilaiRp.Enabled = False
        TxtNilaiValas.Enabled = False
        TxtJangkaWaktu.Enabled = False
        TxtGrace.Enabled = False
        TxtBunga.Enabled = False
        RadBunga.Enabled = False
        TxtTotal.Enabled = False
        'TxtNama.Enabled = False

        'txtNilaiProyek.Enabled = False
        'TxtAlamat.Enabled = False
        TxtPeruntukan.Enabled = False
        DdlSifat.Enabled = False
        DdlSifat.CssClass = "inpDdlDisabled"
        'DdlTujuan.Enabled = False
        'DdlTujuan.CssClass = "inpDdlDisabled"
        'DdlHouse.Enabled = False
        'DdlHouse.CssClass = "inpDdlDisabled"
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Sub controlRO()
        DdlJenisPengajuan.Enabled = False
        DdlJenisPengajuan.CssClass = "inpDdlDisabled"
        DdlFasilitas.Enabled = False
        DdlFasilitas.CssClass = "inpDdlDisabled"
        DdlSitemPembayaran.Enabled = False
        DdlSitemPembayaran.CssClass = "inpDdlDisabled"
        DdlJenisCurr.Enabled = False
        DdlJenisCurr.CssClass = "inpDdlDisabled"
        TxtKurs.Enabled = False
        TxtNilaiRp.Enabled = False
        TxtNilaiValas.Enabled = False
        TxtJangkaWaktu.Enabled = False
        TxtGrace.Enabled = False
        TxtBunga.Enabled = False
        RadBunga.Enabled = False
        TxtTotal.Enabled = False
        'TxtNama.Enabled = False
        txtNilaiProyek.Enabled = False
        'TxtAlamat.Enabled = False
        TxtPeruntukan.Enabled = False
        DdlSifat.Enabled = False
        DdlSifat.CssClass = "inpDdlDisabled"
        'DdlTujuan.Enabled = False
        'DdlTujuan.CssClass = "inpDdlDisabled"
        'DdlHouse.Enabled = False
        'DdlHouse.CssClass = "inpDdlDisabled"
        'DdlSektor.Enabled = False
        'DdlSubsektor.Enabled = False
        txtAvailibilityPeriode.Enabled = False
        'ddlTahunAvailibility.Enabled = False
        'ddlTahunAvailibility.CssClass = "inpDdlDisabled"
        'ddlBulanAvailibility.Enabled = False
        'ddlBulanAvailibility.CssClass = "inpDdlDisabled"
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        lblNotif.Text = ""
        cekInput()
        If lblNotif.Text = "" Then
            Try
                Dim ds As DataSet
                Dim dt As DataTable
                ds = oRat.checkFacilityCredit(appno.Value)
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    'update data
                    oRat.updateFacilityCredit(Request, appno.Value, usrLogin)
                Else
                    'insert data
                    oRat.insertFacilityCredit(Request, appno.Value, usrLogin)
                End If

                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        Dim lNot As String = ""

        lNot &= oGen.checkInputInaHTML("DropDownList", "Jenis Debitur", DdlJenisPengajuan.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Fasilitas", DdlFasilitas.SelectedValue)
        'lNot &= oGen.checkInputInaHTML("TextBox", "Nama Proyek/Usaha", TxtNama.Text)
        'lNot &= oGen.checkInputInaHTML("TextBox", "Alamat Proyek/Usaha", TxtAlamat.Text)
        lNot &= oGen.checkInputInaHTML("TextBox", "Nilai Proyek", txtNilaiProyek.Text)
        'lNot &= oGen.checkInputInaHTML("DropDownList", "Sistem Pembayaran", DdlSitemPembayaran.SelectedValue)
        'lNot &= oGen.checkInputInaHTML("Textbox", "Peruntukan Kredit/Fasilitas", TxtPeruntukan.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Jenis Currency", DdlJenisCurr.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Sifat Kredit/Fasilitas", DdlSifat.SelectedValue)
        'lNot &= oGen.checkInputInaHTML("DropDownList", "Tujuan Penggunaan", DdlTujuan.SelectedValue)
        'lNot &= oGen.checkInputInaHTML("DropDownList", "House Type", DdlHouse.SelectedValue)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Nilai Permohonan (dalam rupiah)", TxtNilaiRp.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Bunga (per annum)", TxtBunga.Text)
        'lNot &= oGen.checkInputInaHTML("TextBox", "Availibility Periode", txtAvailibilityPeriode.Text)

        If txtNilaiProyek.Text = "0" Then
            lNot &= "Nilai Proyek tidak bisa 0."
        End If
        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If

    End Sub

    Private Sub DdlJenisCurr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlJenisCurr.SelectedIndexChanged
        Try
            Dim dt As DataTable
            dt = oRat.getDropDownList("RFCURRATE")
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("ldd_code") = DdlJenisCurr.SelectedValue Then
                        'TxtKurs.Text = dt.Rows(i).Item("ldd_desc")
                        TxtKurs.Text = 1
                    End If
                Next
            End If

            CheckValas()
            If TxtKurs.Text = "Pilih" Then
                TxtKurs.Text = 0
            End If
            If TxtKurs.Text = 0 Then
                TxtNilaiValas.Text = 0
            Else
                'TxtNilaiValas.Text = oGen.moneyFormatTextBox(CInt(TxtKurs.Text) * TxtNilaiRp.Text)
            End If
        Catch ex As Exception
            lblNotif.Text = ex.Message
            divnotif.Attributes.Add("style", "display:''")
        End Try
    End Sub
End Class