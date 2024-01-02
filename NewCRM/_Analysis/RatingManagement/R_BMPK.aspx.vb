Public Class R_BMPK
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRerat As New lRerating
    Dim oGen As New lGeneral
    Dim oGlo As New lglobalSetting
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "BMPK"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        controlHome()
        If Not IsPostBack Then
            fillDdl()
        End If

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If

        If Session("appno") IsNot Nothing Then
            If Not IsPostBack Then
                cekData(Request.QueryString("appno"))
            End If
            appno.Value = Request.QueryString("appno")
        End If

        If Request.QueryString("typ") IsNot Nothing Then
            If Request.QueryString("typ") = "Borrower" Then
                DataTable = oRerat.getListOfMenu("Borrower")
            ElseIf Request.QueryString("typ") = "Facility" Then
                DataTable = oRerat.getListOfMenu("Facility")
            End If
            getMenuNext(Request.QueryString("typ"))
            executeJavascript()
        Else
            Response.Redirect("ratingMgmt.aspx")
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
                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                    url = dataTable.Rows(i).Item("lmr_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & appno.Value & "', 'Facility');"
        Dim javascript = "<script type='text/javascript'>" & script & "</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
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
        Dim typ = Request.QueryString("typ")

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
        Dim typ = Request.QueryString("typ")

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
        Dim ds As DataSet
        Dim dt As DataTable
        ddlBulan.Items.Clear()
        ddlTahun.Items.Clear()
        ddlBulan.Items.Add(New ListItem("Pilih", "Pilih"))
        ddlTahun.Items.Add(New ListItem("Pilih", "Pilih"))
        ds = oGlo.getListGloCapital
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlBulan.Items.Add(New ListItem(dt.Rows(i).Item("month_desc"), dt.Rows(i).Item("cap_month")))
                ddlTahun.Items.Add(New ListItem(dt.Rows(i).Item("cap_year"), dt.Rows(i).Item("cap_year")))
            Next
        End If
        'ddlBulan.Items.Clear()
        'ddlBulan.Items.Add(New ListItem("Pilih", "Pilih"))
        'ddlBulan.Items.Add(New ListItem("Januari", "1"))
        'ddlBulan.Items.Add(New ListItem("Februari", "2"))
        'ddlBulan.Items.Add(New ListItem("Maret", "3"))
        'ddlBulan.Items.Add(New ListItem("April", "4"))
        'ddlBulan.Items.Add(New ListItem("Mei", "5"))
        'ddlBulan.Items.Add(New ListItem("Juni", "6"))
        'ddlBulan.Items.Add(New ListItem("Juli", "7"))
        'ddlBulan.Items.Add(New ListItem("Agustus", "8"))
        'ddlBulan.Items.Add(New ListItem("September", "9"))
        'ddlBulan.Items.Add(New ListItem("Oktober", "10"))
        'ddlBulan.Items.Add(New ListItem("November", "11"))
        'ddlBulan.Items.Add(New ListItem("Desember", "12"))
        ''ddlBulan.SelectedValue = "12"

        'ddlTahun.Items.Clear()
        'Dim year As String = Now.ToString("yyyy")
        'Dim x As Integer = CInt(year)
        'ddlTahun.Items.Add(New ListItem("Pilih", "Pilih"))

        'For i As Integer = 1 To 6
        '    ddlTahun.Items.Add(New ListItem(x.ToString, x.ToString))
        '    x = x - 1
        'Next
    End Sub

    Sub controlHome()
        btnNext.Visible = False
    End Sub

    Sub cekData(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            isiData(no)
            brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
        Else
            Response.Redirect("RatingMgmt.aspx")
        End If
    End Sub

    Sub isiData(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBMPK(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            ddlBulan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lbk_month"))
            ddlTahun.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lbk_year"))
            txtModal.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_modal"))
            txtDebiturTerkait.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_debitur_terkait"))
            txtDebiturPihakKetiga.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_debitur_pihak_ketiga"))
            txtDebiturPihakKetigaGrup.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_debitur_pihak_ketiga_grup"))
            txtDebiturBUMN.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_debitur_bumn"))
            txtInhousePihakKetiga.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_inhouse_limit"))
            txtDebitur1.Text = oGen.CheckNullString(dt.Rows(0).Item("lbk_limit_field1"))
            txtDebiturLimit1.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_limit_value1"))
            txtDebitur2.Text = oGen.CheckNullString(dt.Rows(0).Item("lbk_limit_field2"))
            txtDebiturLimit2.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_limit_value2"))
            txtDebitur3.Text = oGen.CheckNullString(dt.Rows(0).Item("lbk_limit_field3"))
            txtDebiturLimit3.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_limit_value3"))
            txtDebitur4.Text = oGen.CheckNullString(dt.Rows(0).Item("lbk_limit_field4"))
            txtDebiturLimit4.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_limit_value4"))
            txtDebitur5.Text = oGen.CheckNullString(dt.Rows(0).Item("lbk_limit_field5"))
            txtDebiturLimit5.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_limit_value5"))
            txtTotalExposureKredit.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_total"))
            txtKelonggaranBMPK.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_kelonggaran"))
            txtKelonggaranInhouse.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lbk_kelonggaran_inhouse"))
            txtProsentaseKreditToBMPK.Text = String.Format("{0:N2}", dt.Rows(0).Item("lbk_rasio_total_to_bmpk"))
            txtKesimpulan.Text = oGen.CheckNullString(dt.Rows(0).Item("lbk_kesimpulan"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            If appno.Value <> "" Then
                Try
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oApp.checkBMPK(appno.Value)
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        'update data
                        oApp.updateBMPK(Request, appno.Value, usrLogin)
                    Else
                        'insert data
                        oApp.insertBMPK(Request, appno.Value, usrLogin)
                    End If
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                Catch ex As Exception
                    lblNotif.Text = ex.Message
                    divnotif.Attributes.Add("style", "display:''")
                End Try
            End If
        End If
    End Sub

    Sub cekInput()

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value & "&typ=Facility")
        Else
            Response.Redirect("RatingMgmt.aspx")
        End If
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function checkModal(ByVal period As String) As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim json As String = ""
        Dim ar = period.Split(",")

        With New lglobalSetting
            ds = .getDetailOfGlobalCapitalByPeriod(ar(0), ar(1))
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then

                json = dt.Rows(0).Item("cap_value") & "," & dt.Rows(0).Item("cap_house")
            Else
                json = "Data tidak ada."
            End If
        End With

        Return json
    End Function
End Class