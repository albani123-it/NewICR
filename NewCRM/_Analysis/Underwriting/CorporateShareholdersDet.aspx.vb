Public Class CorporateShareholdersDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            fillDdl()
            If Request.QueryString("sisa") <> "" Then
                sisa.Value = Request.QueryString("sisa")
            End If
        End If

        If Request.QueryString("id") IsNot Nothing Then
            If Request.QueryString("id") > 0 And Not IsPostBack Then
                isiData(Request.QueryString("id"))
            End If
        End If
        If Request.QueryString("brwcode") IsNot Nothing Then
            brw_code.Value = Request.QueryString("brwcode")
        End If

        If Request.QueryString("status") = 0 Then
            btnSave.Enabled = False
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

    Sub fillDdl()
        Dim dt As DataTable
        dt = oRat.getDropDownList("VW_STATUS_BADAN")
        ddlTipeBadan.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlTipeBadan.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.getDetailOfCorporateShareholders(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            brw_code.Value = oGen.CheckNullString(dt.Rows(0).Item("lcs_brw_code"))
            ddlTipeBadan.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lcs_tipe"))
            txtNama.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_nama"))
            txtKepemilikanSaham.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_kepemilikanSaham"))
            txtJumlahLembarSaham.Text = oGen.moneyFormatTextBox(dt.Rows(0).Item("lcs_jumlahLembarSaham"))
            txtNoAkteKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_noAkteKTP"))
            txtTglAkteKTP.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_tglAkteKTP"))
            txtNPWP.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_npwp"))
            txtAlamat.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_alamat"))
            txtKodePos.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_kodePos"))
            txtKelurahan.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_kel"))
            txtKecamatan.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_kec"))
            txtKabupaten.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_kab"))
            txtPropinsi.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_prop"))
            txtKodeAreaTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_kodeAreaTlp"))
            txtTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_tlp"))
            txtExtTlp.Text = oGen.CheckNullString(dt.Rows(0).Item("lcs_extTlp"))
            If Request.QueryString("sisa") <> "" Then
                sisa.Value = CDbl(Request.QueryString("sisa")) + CDbl(oGen.CheckNullInt(dt.Rows(0).Item("lcs_kepemilikanSaham")))
            End If
        End If

        If ddlTipeBadan.SelectedValue.ToString() = "10" Or ddlTipeBadan.SelectedValue.ToString() = "11" Then
            infoAkteNo.Attributes.Add("style", "display:none;")
            infoAkteTgl.Attributes.Add("style", "display:none;")
            infoAkteNPWP.Attributes.Add("style", "display:none;")
        ElseIf ddlTipeBadan.SelectedValue.ToString() = "05" Or ddlTipeBadan.SelectedValue.ToString() = "06" Or ddlTipeBadan.SelectedValue.ToString() = "07" Or ddlTipeBadan.SelectedValue.ToString() = "08" Or ddlTipeBadan.SelectedValue.ToString() = "09" Or ddlTipeBadan.SelectedValue.ToString() = "11" Then
            infoAkteNo.Attributes.Add("style", "display:none;")
            infoAkteTgl.Attributes.Add("style", "display:none;")
            infoAkteNPWP.Attributes.Add("style", "display:none;")
            infoAlamat.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
        Else
            infoAkteNo.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
            infoAkteTgl.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
            infoAkteNPWP.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
        End If

    End Sub

    Sub controlRO()
        ddlTipeBadan.Enabled = False
        ddlTipeBadan.CssClass = "inpDdlDisabled"

        txtNama.ReadOnly = True
        txtKepemilikanSaham.ReadOnly = True
        txtJumlahLembarSaham.ReadOnly = True
        txtNoAkteKTP.ReadOnly = True
        txtTglAkteKTP.ReadOnly = True
        txtNPWP.ReadOnly = True
        txtAlamat.ReadOnly = True
        txtKodePos.ReadOnly = True
        txtKelurahan.ReadOnly = True
        txtKecamatan.ReadOnly = True
        txtKabupaten.ReadOnly = True
        txtPropinsi.ReadOnly = True
        txtKodeAreaTlp.ReadOnly = True
        txtTlp.ReadOnly = True
        txtExtTlp.ReadOnly = True

        txtNama.CssClass = "inpTxtDisabled"
        txtKepemilikanSaham.CssClass = "inpTxtDisabled"
        txtJumlahLembarSaham.CssClass = "inpTxtDisabled"
        txtNoAkteKTP.CssClass = "inpTxtDisabled"
        txtTglAkteKTP.CssClass = "inpTxtDisabled"
        txtNPWP.CssClass = "inpTxtDisabled"
        txtAlamat.CssClass = "inpTxtDisabled"
        txtKodePos.CssClass = "inpTxtDisabled"
        txtKelurahan.CssClass = "inpTxtDisabled"
        txtKecamatan.CssClass = "inpTxtDisabled"
        txtKabupaten.CssClass = "inpTxtDisabled"
        txtPropinsi.CssClass = "inpTxtDisabled"
        txtKodeAreaTlp.CssClass = "inpTxtDisabled"
        txtTlp.CssClass = "inpTxtDisabled"
        txtExtTlp.CssClass = "inpTxtDisabled"
        btnSave.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If brw_code.Value <> "" Then
                If Request.QueryString("id") <> -1 Then
                    'update data
                    Try
                        oApp.updateCorporateShareholders(Request, brw_code.Value, Request.QueryString("id"), usrLogin)
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                Else
                    'insert data
                    Try
                        oApp.insertCorporateShareholders(Request, brw_code.Value, usrLogin)
                        liBodyLoadAction.Text = "alert('" + oGen.NotifikasiBerhasil() + "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    End Try
                End If
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        Dim shm As String
        If txtKepemilikanSaham.Text = "" Then
            shm = 0
        Else
            shm = txtKepemilikanSaham.Text
        End If

        If CDbl(Convert.ToDecimal(shm)) > 100 Then
            lMsg &= "Jumlah Kepemilikan Saham tidak boleh lebih dari 100%.\n"
        End If
        ' If sisa.Value <> "" Then
        If CDbl(Convert.ToDecimal(shm)) > CDbl(sisa.Value) Then
            lMsg &= "Jumlah Saham Perorangan dan Badan tidak boleh lebih dari 100%.\n"
        End If
        'End If

        If ddlTipeBadan.SelectedValue.ToString() = "10" Or ddlTipeBadan.SelectedValue.ToString() = "11" Then
            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Tipe Badan", ddlTipeBadan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
            'lMsg &= oGen.checkInputInaJavascript("TextBox", "Alamat", txtAlamat.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Kepemilikan Saham", txtKepemilikanSaham.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Jumlah Lembar Saham", txtJumlahLembarSaham.Text)
        ElseIf ddlTipeBadan.SelectedValue.ToString() = "05" Or ddlTipeBadan.SelectedValue.ToString() = "06" Or ddlTipeBadan.SelectedValue.ToString() = "07" Or ddlTipeBadan.SelectedValue.ToString() = "08" Or ddlTipeBadan.SelectedValue.ToString() = "09" Or ddlTipeBadan.SelectedValue.ToString() = "11" Then
            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Tipe Badan", ddlTipeBadan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
            'lMsg &= oGen.checkInputInaJavascript("TextBox", "NPWP", txtNPWP.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Alamat", txtAlamat.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Kepemilikan Saham", txtKepemilikanSaham.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Jumlah Lembar Saham", txtJumlahLembarSaham.Text)
        Else
            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Tipe Badan", ddlTipeBadan.SelectedValue)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama", txtNama.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "No. Akte Pendirian/No. KTP", txtNoAkteKTP.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Tgl. Akte Pendirian/Tgl. KTP", Request.Form("txtTglAkteKTP").ToString())
            lMsg &= oGen.checkInputInaJavascript("TextBox", "NPWP", txtNPWP.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Alamat", txtAlamat.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Kepemilikan Saham", txtKepemilikanSaham.Text)
            lMsg &= oGen.checkInputInaJavascript("TextBox", "Jumlah Lembar Saham", txtJumlahLembarSaham.Text)
        End If

        
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub


    Protected Sub ddlTipeBadan_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTipeBadan.SelectedIndexChanged
        If ddlTipeBadan.SelectedValue.ToString() = "10" Or ddlTipeBadan.SelectedValue.ToString() = "11" Then
            infoAkteNo.Attributes.Add("style", "display:none;")
            infoAkteTgl.Attributes.Add("style", "display:none;")
            infoAkteNPWP.Attributes.Add("style", "display:none;")
            infoAlamat.Attributes.Add("style", "display:none;")

        ElseIf ddlTipeBadan.SelectedValue.ToString() = "05" Or ddlTipeBadan.SelectedValue.ToString() = "06" Or ddlTipeBadan.SelectedValue.ToString() = "07" Or ddlTipeBadan.SelectedValue.ToString() = "08" Or ddlTipeBadan.SelectedValue.ToString() = "09" Or ddlTipeBadan.SelectedValue.ToString() = "11" Then
            infoAkteNo.Attributes.Add("style", "display:none;")
            infoAkteTgl.Attributes.Add("style", "display:none;")
            infoAkteNPWP.Attributes.Add("style", "display:none;")
            infoAlamat.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")

        Else
            infoAkteNo.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
            infoAkteTgl.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
            infoAkteNPWP.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
            infoAlamat.Attributes.Add("style", "display:'';color:Red;font-size:11pt;font-weight:bold;")
        End If

    End Sub

End Class