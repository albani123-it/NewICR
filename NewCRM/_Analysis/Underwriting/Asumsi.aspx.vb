Public Class Asumsi
    Inherits System.Web.UI.Page
    Dim oAsm As New lAsumsi
    Dim oGen As New lGeneral

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            fillDdl()
        End If
        If Request.QueryString("appno") IsNot Nothing Then
            If Not IsPostBack Then
                populateData(Request.QueryString("appno"))
            End If
            appno.Value = Request.QueryString("appno")
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        'Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                
            Next
            'If Not tf Then
            '    Session.Abandon()
            '    Response.Redirect("../Login.aspx")
            'End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../../Login.aspx")
        End Try
    End Sub

    Sub fillDdl()
        Dim year As String = Now.ToString("yyyy")
        Dim x As Integer = CInt(year)
        Dim y As Integer = x + 20
        ddlStartYear.Items.Clear()
        ddlStartYear.Items.Add(New ListItem("Pilih", "Pilih"))
        ddlEndYear.Items.Clear()
        ddlEndYear.Items.Add(New ListItem("Pilih", "Pilih"))

        While x < y
            ddlStartYear.Items.Add(New ListItem(x.ToString, x.ToString))
            ddlEndYear.Items.Add(New ListItem(x.ToString, x.ToString))
            x = x + 1
        End While

        ddlStartYear.SelectedValue = year
        ddlEndYear.SelectedValue = (CInt(year) + 10).ToString
    End Sub

    Sub populateData(ByVal app_no)
        txtAppNo.Text = app_no
        Dim ds As DataSet
        Dim dt As DataTable
        With New lApps
            ds = .checkApp(app_no)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                brw_code.Value = oGen.CheckNullString(dt.Rows(0).Item("app_brw_code"))
                txtNamaPemohon.Text = oGen.CheckNullString(dt.Rows(0).Item("lsb_namaPemohon"))
            End If

            ds = .checkBorrowerSector(app_no)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                txtSector.Text = oGen.CheckNullString(dt.Rows(0).Item("lst_sec_name"))


                With New lAsumsi
                    Dim dset As DataSet
                    Dim dtbl As DataTable
                    dset = .getListOfAsumsiBySector(oGen.CheckNullString(dt.Rows(0).Item("brw_sec")))
                    dtbl = dset.Tables(0)
                    ddlAsumsi.Items.Clear()
                    For i As Integer = 0 To dtbl.Rows.Count - 1
                        ddlAsumsi.Items.Add(New ListItem(oGen.CheckNullString(dtbl.Rows(i).Item("acc_desc")), oGen.CheckNullString(dtbl.Rows(i).Item("acc_code"))))
                    Next
                End With
            End If
        End With

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        liBodyLoadAction.Text = ""
        saveAsumsiYearly()
        liBodyLoadAction.Text &= "alert('Data has been successfully saved.');"
        showData()
    End Sub

    Sub saveAsumsiYearly()
        'Dim obj As New IDX.FinancialStatement.iAssumption

        'For i As Integer = ddlStartYear.SelectedValue To ddlEndYear.SelectedValue
        '    Dim tmp As String = ddlAsumsi.SelectedValue & "_" & i
        '    Dim grw As Decimal = 0
        '    If Request.Form("txtAsmYearlyGrowth_" & tmp) <> "n/a" Then
        '        grw = IIf(String.IsNullOrWhiteSpace(Request.Form("txtAsmYearlyGrowth_" & tmp).ToString()), 0, Request.Form("txtAsmYearlyGrowth_" & tmp).ToString.Replace(",", ""))
        '    End If

        '    obj.brwcode = brw_code.Value
        '    obj.code = ddlAsumsi.SelectedValue
        '    obj.period = i
        '    obj.realization = Request.Form("selAsmYearly_" & tmp)
        '    obj.value = IIf(String.IsNullOrWhiteSpace(Request.Form("txtAsmYearlyAmount_" & tmp).ToString()), 0, Request.Form("txtAsmYearlyAmount_" & tmp).ToString.Replace(",", ""))
        '    obj.growth = grw
        '    obj.reason = Request.Form("txtReason_" & tmp)

        '    If Request.Form("selParam_" & tmp) <> "-1" Then
        '        Try
        '            oAsm.insertUpdateAsumsiYearly(obj, usrLogin)
        '        Catch ex As Exception
        '            liBodyLoadAction.Text &= "alert('" & ex.Message.Replace("'", "\'") & "');"
        '        End Try

        '        'obj.total = Request.QueryString("txtAsmDistAmountTot_" & tmp & "_" & i).ToString.Replace(",", "")
        '        obj.parameter = Request.Form("selParam_" & tmp)
        '        Dim j As Integer = 1
        '        While j <= 12
        '            obj.month = j
        '            obj.value = IIf(String.IsNullOrWhiteSpace(Request.Form("txtAsmDistAmount_" & tmp & "_" & j).ToString()), 0, Request.Form("txtAsmDistAmount_" & tmp & "_" & j).ToString.Replace(",", ""))
        '            obj.growth = IIf(String.IsNullOrWhiteSpace(Request.Form("txtAsmDistGrowth_" & tmp & "_" & j).ToString()), 0, Request.Form("txtAsmDistGrowth_" & tmp & "_" & j).ToString.Replace(",", ""))

        '            Try
        '                oAsm.insertUpdateAsumsiMonthlyDistribution(obj, usrLogin)
        '            Catch ex As Exception
        '                liBodyLoadAction.Text &= "alert('" & ex.Message.Replace("'", "\'") & "');"
        '            End Try
        '            j += 1
        '        End While
        '    End If

        'Next


    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        litHistory.Text = ""
        litAsumsiYearly.Text = ""
        litAsumsiDistribution.Text = ""

        litHistory.Text = generateFormHistory()
        litAsumsiYearly.Text = generateFormAsumsiYearlyBack()
        litAsumsiDistribution.Text = generateFormAsumsiDistribution()
        btnSave.Visible = True
    End Sub

    Sub showData()
        litHistory.Text = ""
        litAsumsiYearly.Text = ""
        litAsumsiDistribution.Text = ""

        litHistory.Text = generateFormHistory()
        litAsumsiYearly.Text = generateFormAsumsiYearlyBack()
        litAsumsiDistribution.Text = generateFormAsumsiDistribution()
        btnSave.Visible = True
    End Sub

    Function generateFormHistory()
        Dim Str As String = ""
        Dim color As String = ""
        Dim brwcode As String = brw_code.Value, start_year As String = ddlStartYear.SelectedValue, end_year As String = ddlEndYear.SelectedValue, asumsi As String = ddlAsumsi.SelectedValue
        'Dim desc As String = ""
        Dim ds As DataSet
        Dim dt As DataTable

        With New lFinancial
            ds = .getDetailOfFinancialStatement(asumsi)
            dt = ds.Tables(0)
            'If dt.Rows.Count > 0 Then
            '    desc = oGen.CheckNullString(dt.Rows(0).Item("acc_desc"))
            'End If
        End With

        Str &= "<fieldset class='fieldset_content'>"
        Str &= "<legend style='font-weight:bold;'>Data Historis&nbsp;"
        Str &= "<img id='imgHistoris' src='../../Images/ec_minus.png' style='cursor:pointer' onclick='fHide(&#39;historis_info&#39;,&#39;imgHistoris&#39;)' />"
        Str &= "</legend>"
        Str &= "<div id='historis_info'>"
        Str &= "<div id='historis_content'>"
        Str &= "<table class='tbl' cellspacing='0' cellpadding='5' border='1' width='100%' style='border-collapse: separate;text-align:center;border: thin solid #bcbcbc;border-radius: 5px;'>"

        Str &= "<th><div>Realisasi</div></th>"
        If dt.Rows.Count > 0 Then
            Str &= "<th><div>" & oGen.CheckNullString(dt.Rows(0).Item("acc_desc")) & "</div></th>"
        Else
            Str &= "<th><div>" & asumsi & "</div></th>"
        End If
        Str &= "<th><div>Sales Growth (%)</div></th>"
        Str &= "<th><div>Tipe Laporan</div></th>"

        Dim i As Integer = CInt(start_year)
        Dim total_row As Integer = 0
        While True
            Dim historis = i - 1
            If i Mod 2 = 0 Then
                color = "#ebecec"
            Else
                color = "#f6f7f7"
            End If

            With New lAsumsi
                ds = .getDataHistorisFinancialStatement(brwcode, asumsi, historis)
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    Str &= "<tr bgcolor='" & color & "'>"
                    'str &= "<td>" & oGen.CheckNullString(dt.Rows(0).Item("acd_date")) & "</td>"
                    Str &= "<td><div>" & historis & "</div></td>"
                    Str &= "<td style='text-align:right;'><div>" & oGen.moneyFormatLabel(dt.Rows(0).Item("add_value")) & "</div></td>"
                    Str &= "<td><div>" & oGen.moneyFormatLabel(dt.Rows(0).Item("sales_growth")) & "</div></td>"
                    Str &= "<td><div>" & oGen.CheckNullString(dt.Rows(0).Item("acd_status")) & "</div></td>"
                    Str &= "<input type='hidden' id='hdHis_" & asumsi & "_" & historis & "' name='hdHis_" & asumsi & "_" & historis & "' value='" & dt.Rows(0).Item("add_value") & "' />"
                    total_row += 1
                Else
                    'str &= "<td>&nbsp;</td>"
                    'str &= "<td>&nbsp;</td>"
                    'str &= "<td>&nbsp;</td>"
                    'str &= "<td>&nbsp;</td>"
                    Exit While
                End If
            End With
            i = i - 1
            Str &= "</tr>"
        End While
        hdRowHis.Value = total_row
        Str &= "</table>"
        Str &= "</div>"

        Dim str2 As string 
        If total_row > 1 Then
            Str &= "<div id='average_content'>"
            Str &= "<table class='tbl' cellspacing='0' cellpadding='5' border='1' width='100%' style='border-collapse: separate;text-align:center;border: thin solid #bcbcbc;border-radius: 5px;'>"

            With New lFinancial
                ds = .getDetailOfFinancialStatement(asumsi)
                dt = ds.Tables(0)
            End With

            Str &= "<th><div>Parameter</div></th>"
            If dt.Rows.Count > 0 Then
                Str &= "<th><div>" & oGen.CheckNullString(dt.Rows(0).Item("acc_desc")) & "</div></th>"
            Else
                Str &= "<th><div>" & asumsi & "</div></th>"
            End If
            Str &= "<th><div>Sales Growth (%)</div></th>"

            Dim i2 As Integer = CInt(start_year)
            Dim counter As Integer = 2
            Dim avg As Decimal = 0
            Dim avg_2 As Decimal = 0
            Dim flag As Boolean = False

            For z As Integer = 0 To total_row - 1
                Dim historis = i2 - 1
                If i2 Mod 2 = 0 Then
                    color = "#ebecec"
                Else
                    color = "#f6f7f7"
                End If

                With New lAsumsi
                    Dim j = 1
                    While j <= counter
                        ds = .getDataHistorisFinancialStatement(brwcode, asumsi, CInt(start_year) - j)
                        dt = ds.Tables(0)
                        If dt.Rows.Count > 0 Then
                            If oGen.CheckNullString(dt.Rows(0).Item("add_value")) <> "" Then
                                avg = avg + dt.Rows(0).Item("add_value")
                            Else
                                avg = 0
                            End If

                            If oGen.CheckNullString(dt.Rows(0).Item("sales_growth")) <> "" Then
                                avg_2 = avg_2 + dt.Rows(0).Item("sales_growth")
                            Else
                                avg_2 = 0
                            End If
                        Else
                            avg = 0
                            avg_2 = 0
                        End If
                        j = j + 1
                    End While

                    ds = .getDataHistorisFinancialStatement(brwcode, asumsi, historis)
                    dt = ds.Tables(0)

                    If dt.Rows.Count > 0 Then
                        Str &= "<tr bgcolor='" & color & "'>"
                        Str &= "<td><div>Average last " & counter & " year</div></td>"
                        Str &= "<td style='text-align:right;'><div>" & IIf(avg = 0, "n/a", oGen.moneyFormatLabel(avg / counter)) & "</div></td>"
                        Str &= "<td><div>" & IIf(avg = 0, "n/a", String.Format("{0:N2}", (avg_2 / counter))) & "</div></td>"
                    End If
                    counter += 1
                End With
                i2 = i2 - 1
                avg = 0
                Str &= "</tr>"
            Next
            

            Str &= "</table>"
            Str &= "</div>"
        End If
        
        Str &= "</div>"
        Str &= "</fieldset>"
        If total_row = 0 Then
            Str = ""
        End If
        Return Str
    End Function

    Function generateFormAsumsiYearlyBack()
        Dim str As String = ""
        Dim color As String = ""
        Dim oGen As New lGeneral
        Dim brwcode As String = brw_code.Value, start_year As String = ddlStartYear.SelectedValue, end_year As String = ddlEndYear.SelectedValue, asumsi As String = ddlAsumsi.SelectedValue
        Dim row_his As Integer = hdRowHis.Value

        Dim ds As DataSet
        Dim dt As DataTable

        With New lFinancial
            ds = .getDetailOfFinancialStatement(asumsi)
            dt = ds.Tables(0)
            'If dt.Rows.Count > 0 Then
            '    desc = oGen.CheckNullString(dt.Rows(0).Item("acc_desc"))
            'End If
        End With

        str &= "<fieldset class='fieldset_content'>"
        str &= "<legend style='font-weight:bold;'>Asumsi Kenaikan Tahunan&nbsp;"
        str &= "<img id='imgAsumsi' src='../../Images/ec_minus.png' style='cursor:pointer' onclick='fHide(&#39;asumsi_yearly_content&#39;,&#39;imgAsumsi&#39;)' />"
        str &= "</legend>"

        str &= "<div id='asumsi_yearly_content'>"

        str &= "<table class='tbl' cellspacing='0' cellpadding='5' border='1' width='100%' style='border-collapse: separate;text-align:center;border: thin solid #bcbcbc;border-radius: 5px;'>"
        str &= "<th><div>Proyeksi</div></th>"
        str &= "<th><div>Realisasi</div></th>"
        If dt.Rows.Count > 0 Then
            str &= "<th><div>" & oGen.CheckNullString(dt.Rows(0).Item("acc_desc")) & "</div></th>"
        Else
            str &= "<th><div>" & asumsi & "</div></th>"
        End If
        str &= "<th><div>Growth (%)</div></th>"
        str &= "<th><div>Reason</div></th>"

        'Dim i As Integer = CInt(start_year)
        Dim total_row As Integer = 0

        '======= cari max year yang ada data historis kalau belum ada isi 0
        Dim max_year_his As Integer = 0
        If row_his > 0 Then
            max_year_his = CInt(start_year) - 1
        End If

        For i As Integer = CInt(start_year) To CInt(end_year)
            Dim seq_year As Integer = CInt(start_year) - row_his '======= tahun berjalan paling awal ==========
            
            If i Mod 2 = 0 Then
                color = "#ebecec"
            Else
                color = "#f6f7f7"
            End If

            ds = oAsm.getDataAsumsiYearly(brwcode, asumsi, i)
            dt = ds.Tables(0)
            

            str &= "<tr bgcolor='" & color & "'>"
            str &= "<td><div>" & i & "</div></td>"
            str &= "<td><div>"
            str &= "<select id='selAsmYearly_" & asumsi & "_" & i & "' name='selAsmYearly_" & asumsi & "_" & i & _
                    "' class='inpDdl' onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;, &#39;" & max_year_his & "&#39;,&#39;Growth&#39;)'>"
            If row_his = 0 And i = CInt(start_year) Then
                str &= "<option value='-'>-</option>"
            Else
                While seq_year < i
                    If dt.Rows.Count > 0 Then
                        If oGen.CheckNullString(dt.Rows(0).Item("asm_realisasi")) = seq_year Then
                            str &= "<option value='" & seq_year & "' selected>" & seq_year & "</option>"
                        Else
                            str &= "<option value='" & seq_year & "'>" & seq_year & "</option>"
                        End If
                    Else
                        str &= "<option value='" & seq_year & "'>" & seq_year & "</option>"
                    End If

                    seq_year += 1
                End While
            End If
            

            str &= "</select></div></td>"

            str &= "<td style='text-align:center;'><div>"
            If dt.Rows.Count > 0 Then
                str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmYearlyAmount_" & asumsi & "_" & i & "' " & _
                    "name='txtAsmYearlyAmount_" & asumsi & "_" & i & "' class='inpTxtNum' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);'" & _
                    "onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;, &#39;" & max_year_his & "&#39;,&#39;Amount&#39;)' value='" & oGen.moneyFormatTextBox(dt.Rows(0).Item("add_value")) & "'>"
            Else
                str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmYearlyAmount_" & asumsi & "_" & i & _
                    "' name='txtAsmYearlyAmount_" & asumsi & "_" & i & "' class='inpTxtNum' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);'" & _
                    "onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;, &#39;" & max_year_his & "&#39;,&#39;Amount&#39;)'>"
            End If
            
            str &= "<td style='text-align:center;'><div>"
            If dt.Rows.Count > 0 Then
                If oGen.CheckNullString(dt.Rows(0).Item("asm_growth")) = "0" Then
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' name='txtAsmYearlyGrowth_" & asumsi & "_" & i & "'" & _
                        "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)' value='n/a'>"
                Else
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' name='txtAsmYearlyGrowth_" & asumsi & "_" & i & "'" & _
                        "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)' value='" & String.Format("{0:N2}", oGen.CheckNullString(dt.Rows(0).Item("asm_growth"))) & "'>"
                End If
                
            Else
                If row_his = 0 And i = CInt(start_year) Then
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' name='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' readonly='1'" & _
                        "class='inpTxtNumDisabled' style='width:50px' onkeypress='return isNumberKey(event)' value='n/a'>"
                Else
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' name='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' " & _
                        "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)' onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;,&#39;" & max_year_his & "&#39;,&#39;Growth&#39;)'>"
                End If
            End If

            str &= "</div></td>"

            If dt.Rows.Count > 0 Then
                str &= "<td><div><textarea id='txtReason_" & asumsi & "_" & i & "' name='txtReason_" & asumsi & "_" & i & "' class='inpTxt' rows='3' cols='100'>" & oGen.CheckNullString(dt.Rows(0).Item("asm_reason")) & "</textarea></div></td>"
            Else
                str &= "<td><div><textarea id='txtReason_" & asumsi & "_" & i & "' name='txtReason_" & asumsi & "_" & i & "' class='inpTxt' rows='3' cols='100'></textarea></div></td>"
            End If

            str &= "</tr>"
        Next

        str &= "</table>"

        str &= "</div>"
        str &= "</fieldset>"
        Return str
    End Function

    Function generateFormAsumsiDistribution()
        Dim str As String = ""
        Dim color As String = ""
        Dim oGen As New lGeneral
        Dim brwcode As String = brw_code.Value, start_year As String = ddlStartYear.SelectedValue, end_year As String = ddlEndYear.SelectedValue, asumsi As String = ddlAsumsi.SelectedValue
        Dim row_his As Integer = hdRowHis.Value

        Dim ds As DataSet
        Dim dt As DataTable

        Dim dset As DataSet
        Dim dtbl As DataTable

        'str &= "<fieldset class='fieldset_content'>"
        'str &= "<legend style='font-weight:bold;'>Asumsi Distribusi Bulanan&nbsp;"
        'str &= "<img id='imgAsumsiDist' src='../../Images/ec_minus.png' style='cursor:pointer' onclick='fHide(&#39;asumsi_dist_content&#39;,&#39;imgAsumsiDist&#39;)' />"
        'str &= "</legend>"
        str &= "<div id='asumsi_dist_content'>"

        str &= "<table id='tblAsmDist' class='tbl' cellspacing='0' cellpadding='5' border='1' width='100%' style='border-collapse: separate;text-align:center;border: thin solid #bcbcbc;border-radius: 5px;'>"
        str &= "<th colspan='2' style='min-width:250px;width:auto'><div>Period</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>Total</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>January</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>February</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>March</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>April</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>May</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>June</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>July</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>August</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>September</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>October</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>November</div></th>"
        str &= "<th style='min-width:150px;width:auto;' class='thAsmDist'><div>December</div></th>"

        Dim total_row As Integer = 0
        
        For i As Integer = CInt(start_year) To CInt(end_year)
            Dim totalAmt As Decimal = 0
            Dim totalGrw As Decimal = 0

            If i Mod 2 = 0 Then
                color = "#ebecec"
            Else
                color = "#f6f7f7"
            End If


            str &= "<tr bgcolor='#E2D4D4'>"
            str &= "<td rowspan='2' style='background-color:" & color & ";' class='tdAsmDist'><div style='font-weight:bold;'>" & i & "<br/><br/>"
            str &= "<select id='selParam_" & asumsi & "_" & i & "' name='selParam_" & asumsi & "_" & i & "' class='inpDdl' onchange='monthDist(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;)'>"

            ds = oAsm.getListOfParameterDistribution(asumsi)
            dt = ds.Tables(0)

            dset = oAsm.getDataAsumsiMonthlyParameter(brwcode, asumsi, i)
            dtbl = dset.Tables(0)

            For j As Integer = 0 To dt.Rows.Count - 1
                If dtbl.Rows.Count > 0 Then
                    If dt.Rows(j).Item("acp_code") = dtbl.Rows(0).Item("asm_parameter") Then
                        str &= "<option value='" & dt.Rows(j).Item("acp_code") & "' selected>" & dt.Rows(j).Item("acp_name") & "</option>"
                    Else
                        str &= "<option value='" & dt.Rows(j).Item("acp_code") & "'>" & dt.Rows(j).Item("acp_name") & "</option>"
                    End If
                Else
                    str &= "<option value='" & dt.Rows(j).Item("acp_code") & "'>" & dt.Rows(j).Item("acp_name") & "</option>"
                End If

            Next

            str &= "</select>"
            str &= "</div></td>"

            '=======Growth Row==============================
            str &= "<td class='tdAsmDist'><div style='font-weight:bold;'>Growth (%)</div></td>"
            For j As Integer = 0 To 11
                dset = oAsm.getDataAsumsiMonthlyDistributionGrowth(brwcode, asumsi, i, j + 1)
                dtbl = dset.Tables(0)
                If dtbl.Rows.Count > 0 Then
                    totalGrw = totalGrw + IIf(oGen.CheckNullString(dtbl.Rows(j).Item("asm_growth")) = "", 0, dtbl.Rows(j).Item("asm_growth"))
                End If
            Next
            str &= "<td class='tdAsmDist'><div>"
            If totalGrw > 0 Then
                str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistGrowthTot_" & asumsi & "_" & i & _
                    "' name='txtAsmDistGrowthTot_" & asumsi & "_" & i & "' readonly='1'" & _
                    "class='inpTxtNumDisabled' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' value='" & oGen.moneyFormatTextBox(totalGrw) & "'>"
            Else
                str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistGrowthTot_" & asumsi & "_" & i & _
                    "' name='txtAsmDistGrowthTot_" & asumsi & "_" & i & "' readonly='1'" & _
                    "class='inpTxtNumDisabled' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);'>"
            End If

            str &= "</div></td>"

            For j As Integer = 1 To 12
                dset = oAsm.getDataAsumsiMonthlyDistributionGrowth(brwcode, asumsi, i, j)
                dtbl = dset.Tables(0)


                str &= "<td class='tdAsmDist'><div>"
                If dtbl.Rows.Count > 0 Then
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistGrowth_" & asumsi & "_" & i & "_" & j & _
                    "' name='txtAsmDistGrowth_" & asumsi & "_" & i & "_" & j & "' " & _
                    "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)'" & _
                    "onchange='growthCalc(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;,&#39;" & j & "&#39;)' value='" & String.Format("{0:N2}", dtbl.Rows(0).Item("asm_growth")) & "' />"
                Else
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistGrowth_" & asumsi & "_" & i & "_" & j & _
                    "' name='txtAsmDistGrowth_" & asumsi & "_" & i & "_" & j & "' " & _
                    "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)'" & _
                    "onchange='growthCalc(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;,&#39;" & j & "&#39;)' />"
                End If

                str &= "</div></td>"
            Next

            str &= "</tr>"
            '=======End Growth Row==============================


            '=======Amount Row==============================
            For j As Integer = 0 To 11
                dset = oAsm.getDataAsumsiMonthlyDistribution(brwcode, asumsi, i, j + 1)
                dtbl = dset.Tables(0)
                If dtbl.Rows.Count > 0 Then
                    totalAmt = totalAmt + IIf(oGen.CheckNullString(dtbl.Rows(j).Item("add_value")) = "", 0, dtbl.Rows(j).Item("add_value"))
                End If
            Next

            str &= "<tr bgcolor='" & color & "'>"
            str &= "<td class='tdAsmDist'><div style='font-weight:bold;'>Amount</div></td>"

            str &= "<td class='tdAsmDist'><div>"
            If totalAmt > 0 Then
                str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistAmountTot_" & asumsi & "_" & i & _
                    "' name='txtAsmDistAmountTot_" & asumsi & "_" & i & "' readonly='1'" & _
                    "class='inpTxtNumDisabled' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' value='" & oGen.moneyFormatTextBox(totalAmt) & "'>"
            Else
                str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistAmountTot_" & asumsi & "_" & i & _
                    "' name='txtAsmDistAmountTot_" & asumsi & "_" & i & "' readonly='1'" & _
                    "class='inpTxtNumDisabled' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' value=''>"
            End If

            str &= "</div></td>"

            For j As Integer = 1 To 12
                dset = oAsm.getDataAsumsiMonthlyDistribution(brwcode, asumsi, i, j)
                dtbl = dset.Tables(0)

                str &= "<td class='tdAsmDist'><div>"
                If dtbl.Rows.Count > 0 Then
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistAmount_" & asumsi & "_" & i & "_" & j & _
                    "' name='txtAsmDistAmount_" & asumsi & "_" & i & "_" & j & "' " & _
                    "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);'" & _
                    "onchange='amountCalc(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;,&#39;" & j & "&#39;)' value='" & oGen.moneyFormatTextBox(dtbl.Rows(0).Item("add_value")) & "' />"
                Else
                    str &= "<input type='text' style='text-align:right;float:none;' id='txtAsmDistAmount_" & asumsi & "_" & i & "_" & j & _
                    "' name='txtAsmDistAmount_" & asumsi & "_" & i & "_" & j & "' " & _
                    "class='inpTxtNum' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);'" & _
                    "onchange='amountCalc(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;,&#39;" & j & "&#39;)' />"
                End If

                str &= "</div></td>"
            Next

            str &= "</tr>"
            '=======End Amount Row==============================
        Next

        str &= "</table>"
        str &= "</div>"
        'str &= "</fieldset>"
        Return str
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function getDistribution(ByVal str As String) As Array
        Dim arr()
        Dim oGen As New lGeneral
        Dim tmp() = str.Split(",")

        'arr(0) = tmp(0) 'brwcode
        'arr(1) = tmp(1) 'asumsi code
        'arr(2) = tmp(2) 'period
        'arr(3) = tmp(3) 'month
        'arr(4) = tmp(4) 'typ

        With New lAsumsi

            Dim dset As DataSet
            Dim dtbl As DataTable
            dset = .getDataAsumsiMonthlyByParameter(tmp(0), tmp(1), tmp(2), tmp(3), tmp(4))
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                'str &= "," & oGen.CheckNullString(dtbl.Rows(0).Item("add_value"))
                str &= "," & oGen.CheckNullString(dtbl.Rows(0).Item("asm_growth"))
            Else
                'str &= ", "
                str &= ", none"
            End If

        End With
        arr = str.Split(",")

        Return arr
    End Function

    '<System.Web.Services.WebMethod()> _
    'Public Shared Function checkApp(ByVal app_no As String) As String
    '    Dim str As String = ""
    '    Dim oGen As New lGeneral
    '    Dim ds As DataSet
    '    Dim dt As DataTable
    '    With New lApps
    '        ds = .checkApp(app_no)
    '        dt = ds.Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            str = oGen.CheckNullString(dt.Rows(0).Item("app_brw_code")) & "," & oGen.CheckNullString(dt.Rows(0).Item("lsb_namaPemohon"))
    '        End If

    '        ds = .checkBorrowerSector(app_no)
    '        dt = ds.Tables(0)
    '        If dt.Rows.Count > 0 Then
    '            str = str & "," & oGen.CheckNullString(dt.Rows(0).Item("brw_sec")) & "," & oGen.CheckNullString(dt.Rows(0).Item("lst_sec_name"))
    '        End If
    '    End With
    '    Return str
    'End Function

    '<System.Web.Services.WebMethod()> _
    'Public Shared Function checkListAsumsi(ByVal sector As String) As Array
    '    Dim str() As String
    '    Dim oGen As New lGeneral
    '    Dim tmp As String = ""
    '    With New lAsumsi

    '        Dim dset As DataSet
    '        Dim dtbl As DataTable
    '        dset = .getListOfAsumsiBySector(sector)
    '        dtbl = dset.Tables(0)
    '        For i As Integer = 0 To dtbl.Rows.Count - 1
    '            If i = dtbl.Rows.Count - 1 Then
    '                tmp &= oGen.CheckNullString(dtbl.Rows(i).Item("acc_code")) & "," & oGen.CheckNullString(dtbl.Rows(i).Item("acc_desc"))
    '            Else
    '                tmp &= oGen.CheckNullString(dtbl.Rows(i).Item("acc_code")) & "," & oGen.CheckNullString(dtbl.Rows(i).Item("acc_desc")) & "|"
    '            End If
    '        Next
    '    End With

    '    str = tmp.Split("|")

    '    Return str
    'End Function

    '<System.Web.Services.WebMethod()> _
    'Public Shared Function generateFormHistoris(ByVal val As String) As String
    '    Dim str As String = ""
    '    Dim color As String = ""
    '    Dim oGen As New lGeneral
    '    Dim tmp = val.Split(",")
    '    Dim brwcode As String = tmp(0), start_year As String = tmp(1), end_year As String = tmp(2), asumsi As String = tmp(3)
    '    'Dim desc As String = ""
    '    Dim ds As DataSet
    '    Dim dt As DataTable

    '    With New lFinancial
    '        ds = .getDetailOfFinancialStatement(asumsi)
    '        dt = ds.Tables(0)
    '        'If dt.Rows.Count > 0 Then
    '        '    desc = oGen.CheckNullString(dt.Rows(0).Item("acc_desc"))
    '        'End If
    '    End With

    '    str &= "<table class='tbl' cellspacing='0' cellpadding='5' border='1' width='100%' style='border-collapse: separate;text-align:left;border: thin solid #bcbcbc;border-radius: 5px;'>"

    '    str &= "<th><div>Realisasi</div></th>"
    '    If dt.Rows.Count > 0 Then
    '        str &= "<th><div>" & oGen.CheckNullString(dt.Rows(0).Item("acc_desc")) & "</div></th>"
    '    Else
    '        str &= "<th><div>" & asumsi & "</div></th>"
    '    End If
    '    str &= "<th><div>Salse Growth (%)</div></th>"
    '    str &= "<th><div>Tipe Laporan</div></th>"

    '    Dim i As Integer = CInt(start_year)
    '    Dim total_row As Integer = 0
    '    While True
    '        Dim historis = i - 1
    '        If i Mod 2 = 0 Then
    '            color = "#ebecec"
    '        Else
    '            color = "#f6f7f7"
    '        End If

    '        With New lAsumsi
    '            ds = .getDataHistorisFinancialStatement(brwcode, asumsi, historis)
    '            dt = ds.Tables(0)
    '            If dt.Rows.Count > 0 Then
    '                str &= "<tr bgcolor='" & color & "'>"
    '                'str &= "<td>" & oGen.CheckNullString(dt.Rows(0).Item("acd_date")) & "</td>"
    '                str &= "<td><div>" & historis & "</div></td>"
    '                str &= "<td><div>" & oGen.moneyFormatLabel(dt.Rows(0).Item("add_value")) & "</div></td>"
    '                str &= "<td><div>" & oGen.CheckNullString(dt.Rows(0).Item("sales_growth")) & "</div></td>"
    '                str &= "<td><div>" & oGen.CheckNullString(dt.Rows(0).Item("acd_status")) & "</div></td>"
    '                str &= "<input type='hidden' id='hdHis_" & asumsi & "_" & historis & "' name='hdHis_" & asumsi & "_" & historis & "' value='" & dt.Rows(0).Item("add_value") & "' />"
    '                total_row += 1
    '            Else
    '                'str &= "<td>&nbsp;</td>"
    '                'str &= "<td>&nbsp;</td>"
    '                'str &= "<td>&nbsp;</td>"
    '                'str &= "<td>&nbsp;</td>"
    '                Exit While
    '            End If
    '        End With
    '        i = i - 1
    '        str &= "</tr>"
    '    End While
    '    str &= "<input type='hidden' id='hdRowHis' name='hdRowHis' value='" & total_row & "' />"
    '    str &= "</table>"

    '    'If total_row = 0 Then
    '    '    str = "Empty"
    '    'End If

    '    Return str
    'End Function

    '<System.Web.Services.WebMethod()> _
    'Public Shared Function generateFormAsumsiYearly(ByVal val As String) As String
    '    Dim str As String = ""
    '    Dim color As String = ""
    '    Dim oGen As New lGeneral
    '    Dim tmp = val.Split(",")
    '    Dim brwcode As String = tmp(0), start_year As String = tmp(1), end_year As String = tmp(2), asumsi As String = tmp(3), row_his As Integer = tmp(4)

    '    Dim ds As DataSet
    '    Dim dt As DataTable

    '    With New lFinancial
    '        ds = .getDetailOfFinancialStatement(asumsi)
    '        dt = ds.Tables(0)
    '        'If dt.Rows.Count > 0 Then
    '        '    desc = oGen.CheckNullString(dt.Rows(0).Item("acc_desc"))
    '        'End If
    '    End With

    '    str &= "<table class='tbl' cellspacing='0' cellpadding='5' border='1' width='100%' style='border-collapse: separate;text-align:left;border: thin solid #bcbcbc;border-radius: 5px;'>"
    '    str &= "<th><div>Proyeksi</div></th>"
    '    str &= "<th><div>Realisasi</div></th>"
    '    If dt.Rows.Count > 0 Then
    '        str &= "<th><div>" & oGen.CheckNullString(dt.Rows(0).Item("acc_desc")) & "</div></th>"
    '    Else
    '        str &= "<th><div>" & asumsi & "</div></th>"
    '    End If
    '    str &= "<th><div>Growth (%)</div></th>"
    '    str &= "<th><div>Reason</div></th>"

    '    'Dim i As Integer = CInt(start_year)
    '    Dim total_row As Integer = 0

    '    '======= cari max year yang ada data historis kalau belum ada isi 0
    '    Dim max_year_his As Integer = 0
    '    If row_his > 0 Then
    '        max_year_his = CInt(start_year) - 1
    '    End If

    '    For i As Integer = CInt(start_year) To CInt(end_year)
    '        Dim seq_year As Integer = CInt(start_year) - row_his '======= tahun berjalan paling awal ==========

    '        If i Mod 2 = 0 Then
    '            color = "#ebecec"
    '        Else
    '            color = "#f6f7f7"
    '        End If

    '        str &= "<tr bgcolor='" & color & "'>"
    '        str &= "<td><div>" & i & "</div></td>"
    '        str &= "<td><div>"
    '        str &= "<select id='selAsmYearly_" & asumsi & "_" & i & "' name='selAsmYearly_" & asumsi & "_" & i & _
    '                "' class='inpDdl' onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;, &#39;" & max_year_his & "&#39;,&#39;Amount&#39;)'>"

    '        While seq_year < i
    '            str &= "<option value='" & seq_year & "'>" & seq_year & "</option>"
    '            seq_year += 1
    '        End While

    '        str &= "</select></div></td>"
    '        str &= "<td><div><input type='text' id='txtAsmYearlyAmount_" & asumsi & "_" & i & "' name='txtAsmYearly_" & asumsi & "_" & i & "' " & _
    '                "class='inpTxt' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;, &#39;" & max_year_his & "&#39;,&#39;Amount&#39;)'></div></td>"
    '        str &= "<td><div><input type='text' id='txtAsmYearlyGrowth_" & asumsi & "_" & i & "' name='txtAsmYearly_" & asumsi & "_" & i & "' " & _
    '                "class='inpTxt' style='width:50px' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' onchange='calcAsumsiYearly(&#39;" & asumsi & "&#39;,&#39;" & i & "&#39;,&#39;" & max_year_his & "&#39;,&#39;Growth&#39;)'></div></td>"
    '        str &= "<td><div><textarea id='txtReason_" & asumsi & "_" & i & "' name='txtReason_" & asumsi & "_" & i & "' class='inpTxt' rows='3' cols='100'></textarea></div></td>"
    '        str &= "</tr>"
    '    Next
    '    'While True
    '    '    If i Mod 2 = 0 Then
    '    '        color = "#ebecec"
    '    '    Else
    '    '        color = "#f6f7f7"
    '    '    End If

    '    '    str &= "<tr bgcolor='" & color & "'>"

    '    '    With New lAsumsi
    '    '        ds = .getDataHistorisFinancialStatement(brwcode, asumsi, i - 1)
    '    '        dt = ds.Tables(0)
    '    '        If dt.Rows.Count > 0 Then
    '    '            'str &= "<td>" & oGen.CheckNullString(dt.Rows(0).Item("acd_date")) & "</td>"
    '    '            str &= "<td>" & i & "</td>"
    '    '            str &= "<td>" & oGen.moneyFormatLabel(dt.Rows(0).Item("add_value")) & "</td>"
    '    '            str &= "<td>" & oGen.CheckNullString(dt.Rows(0).Item("sales_growth")) & "</td>"
    '    '            str &= "<td>" & oGen.CheckNullString(dt.Rows(0).Item("acd_status")) & "</td>"
    '    '            str &= "<input type='hidden' id='hdHis_" & asumsi & "_" & i & "' name='hdHis_" & asumsi & "_" & i & "' value='" & dt.Rows(0).Item("add_value") & "' />"
    '    '            total_row += 1
    '    '        Else
    '    '            'str &= "<td>&nbsp;</td>"
    '    '            'str &= "<td>&nbsp;</td>"
    '    '            'str &= "<td>&nbsp;</td>"
    '    '            'str &= "<td>&nbsp;</td>"
    '    '            Exit While
    '    '        End If
    '    '    End With
    '    '    i = i - 1
    '    '    str &= "</tr>"
    '    'End While
    '    'str &= "<input type='hidden' id='hdRowHis' name='hdRowHis' value='" & total_row & "' />"
    '    str &= "</table>"

    '    Return str
    'End Function

End Class