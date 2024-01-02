Public Class R_FacilityCreditInfo
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oRerat As New lRerating
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Permohonan Fasilitas Kredit"
    Protected dtWorkflow As New DataTable
    Protected sector As String = ""
    Protected menuNext As String = ""


#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Not IsPostBack Then
            fillDdl()
        End If

        'If Request.QueryString("appno") IsNot Nothing Then
        '    Session("appno") = Request.QueryString("appno")
        'End If

        'If Session("appno") IsNot Nothing Then
        '    If Not IsPostBack Then
        '        cekData(Request.QueryString("appno"))
        '    End If
        '    appno.Value = Request.QueryString("appno")
        'End If

        'If Request.QueryString("typ") IsNot Nothing Then
        '    If Request.QueryString("typ") = "Borrower" Then
        '        dataTable = oRerat.getListOfMenu("Borrower")
        '        Session("type") = "Borrower"
        '    ElseIf Request.QueryString("typ") = "Facility" Then
        '        dataTable = oRerat.getListOfMenu("Facility")
        '        Session("type") = "Facility"

        '    End If
        '    getMenuNext(Request.QueryString("typ"))
        '    executeJavascript(Request.QueryString("typ"))
        'Else
        '    Response.Redirect("ratingMgmt.aspx")
        'End If

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
            appno.Value = Session("appno")
        ElseIf Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
            brw_code.Value = Session("brwcode")
        End If
        dataTable = oRerat.getListOfMenu("Facility")
        'checkAppByBrwCode(Session("brwcode"))
        If Session("appno") IsNot Nothing And Not IsPostBack Then
            Session("typ") = Request.QueryString("typ2")
            cekApp(Session("appno"))
            getMenuNext("Facility")
            'executeJavascript()
        End If
        controlHome()
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

    Sub executeJavascript(ByVal typ)
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        If typ = "Facility" Then
            dset = oApp.checkApp(appno.Value)
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dataTable.Rows.Count - 1
                    If dataTable.Rows(i).Item("lmr_name") = pagename Then
                        url = dataTable.Rows(i).Item("lmr_link")
                    End If
                Next
            End If
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & appno.Value & "', 'Facility');"
            Dim javascript = "<script type='text/javascript'>" & script & "</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
        End If
        If typ = "Borrower" Then
            dset = oApp.checkAppByBrwCode(brw_code.Value)
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dataTable.Rows.Count - 1
                    If dataTable.Rows(i).Item("lmr_name") = pagename Then
                        url = dataTable.Rows(i).Item("lmr_link")
                    End If
                Next
            End If
            Dim scriptKey As String = "UniqueKeyForThisScript"
            Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & brw_code.Value & "', 'Borrower');"
            Dim javascript = "<script type='text/javascript'>" & script & "</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
        End If
    End Sub

    Sub getMenuNext(ByVal typ)
        For i As Integer = 0 To dataTable.Rows.Count - 1
            If dataTable.Rows(i).Item("lmr_name") = pagename Then
                Dim dt2 As DataTable
                dt2 = oRerat.getNextMenu(dataTable.Rows(i).Item("lmr_id"), typ)
                If dt2.Rows.Count > 0 Then
                    menuNext = dt2.Rows(0).Item("lmr_link")
                End If
            End If
        Next
    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        Dim typ = "Facility"

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lmr_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        Dim typ = "Facility"

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lmr_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_rat" Then
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

    Sub controlHome()
        btnNext.Visible = False
        TxtKurs.ReadOnly = True
        TxtKurs.CssClass = "inpTxtDisabled"
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

    Sub cekData(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            isiData(no)
        Else
            Response.Redirect("RatingMgmt.aspx")
        End If
    End Sub

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oRat.checkFacilityCredit(no)
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
            'CheckValas()
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

            txtAvailibilityPeriode.Text = oGen.CheckNullString(dt.Rows(0).Item("lfc_availibility_periode"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
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
        lNot &= oGen.checkInputInaHTML("DropDownList", "Sistem Pembayaran", DdlSitemPembayaran.SelectedValue)
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

    Private Sub DdlJenisCurr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlJenisCurr.SelectedIndexChanged
        Try
            Dim dt As DataTable
            dt = oRat.getDropDownList("RFCURRATE")
            
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("ldd_code") = DdlJenisCurr.SelectedValue Then
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

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value & "&typ=Facility")
        Else
            Response.Redirect("RatingMgmt.aspx")
        End If
    End Sub
End Class