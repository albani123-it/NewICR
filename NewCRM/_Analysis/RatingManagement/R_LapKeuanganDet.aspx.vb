Imports System.Data.SqlClient
Public Class R_LapKeuanganDet
    Inherits System.Web.UI.Page
    Public ds As DataSet
    Dim dts As DataTable
    Dim oGen As New lGeneral
    Dim oLap As New lLapKeuangan
    Public html As String
    Dim appno As String
    Dim report As String
    Dim sector As String
    Dim code As String
    Dim typ As String
    Dim dates = "2014"
    Dim lvl As Integer = 0
    Dim tanggal As String
    Dim usrLogin As String
    Dim cs As ClientScriptManager = Page.ClientScript

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))

        If Request.QueryString("appno") IsNot Nothing Then
            appno = Request.QueryString("appno")
        End If

        If Request.QueryString("report") IsNot Nothing Then
            report = Request.QueryString("report")
        End If
        If Request.QueryString("sektor") IsNot Nothing Then
            sector = Request.QueryString("sektor")
        End If
        If Request.QueryString("brwcode") IsNot Nothing Then
            code = Request.QueryString("brwcode")
        End If

        btnsave.Visible = False
        btnCancel.Visible = False
        If Not IsPostBack Then
            getdate(code)
        End If
        liBodyLoadAction.Text = ""
    End Sub

    Sub getdate(ByVal brwcode)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = getAppenddate(brwcode)
        dt = ds.Tables(0)
        ddltanggal.DataSource = dt
        ddltanggal.DataValueField = "period"
        ddltanggal.DataTextField = "period"
        ddltanggal.DataBind()
    End Sub

    Public Function getAppenddate(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        Dim cmdA As New SqlCommand("get_periode_acc", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@brwcode", brwcode)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Protected Sub btnview_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnview.Click
        liBodyLoadAction.Text = ""

        Dim tgl = Request.Form("txtDate")
        typ = ddlStatus.SelectedValue
        If typ = "Proyeksi" Then
            Dim tglp = ddltanggal.SelectedValue
            tglp = tglp.Substring(tglp.Length - 4, 4) + tglp.Substring(tglp.Length - 7, 2) + tglp.Substring(0, 2)
            oLap.runProyeksiOthers(code, tglp)
        End If

        If ddltanggal.SelectedValue = "New Periode" Then
            If tgl = "" Or tgl = String.Empty Or tgl = "New Periode" Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Tanggal diisi terlebih dahulu');", True)
                txtDate.Focus()
            Else
                txtDate.Text = tgl
                tanggal = txtDate.Text
                bindata()
                'genForm(ds)
                generateForm(ds)
            End If

        ElseIf ddltanggal.SelectedValue <> "New Periode" Then
            tanggal = ddltanggal.SelectedValue
            txtDate.Text = ddltanggal.SelectedValue
            bindata()
            'genForm(ds)
            generateForm(ds)
        Else
            tanggal = txtDate.Text
            bindata()
            'genForm(ds)
            generateForm(ds)
        End If

        btnsave.Visible = True
        btnCancel.Visible = True

    End Sub

    Sub cekInput()
        Dim lNot As String = ""
    End Sub

    Sub bindata()
        ds = getAppendAccformula(report, sector, code, tanggal)
    End Sub

    Sub generateForm(ByVal ds)
        Dim classInput = "style='text-align:right;box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);border-radius: 5px;border: thin solid #aaaaaa;padding: 1px 4px;color: #2b4c61;font-family: century gothic; font-size: 12px;float:left;'"
        Dim classtable = "style='text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-bottom:10px;'"
        Dim styleView = "style='border: 0px;background:transparent;font-family: century gothic; font-size: 12px;float:left;text-align:right;-webkit-box-shadow: none;-moz-box-shadow: none;box-shadow: none;'"
        Dim curne = String.Format("{0:#.00}", Convert.ToDecimal(lvl) / 100)
        Dim val As String
        Dim status As String = ""
        Dim summary As String = "'"
        Dim summaryPct As String = "'"
        html += "<table width='65%'  " & classtable & " id='tblneraca'>"

        Dim dTbl As DataTable
        dTbl = ds.Tables(0)

        For index = 0 To dTbl.Rows.Count - 1
            If dTbl.Rows(index).Item("add_value") = "n/a" Then
                val = ""
            Else
                'val = dr.Item("add_value")
                val = moneyFormatLabel(dTbl.Rows(index).Item("add_value"))

            End If
            If dTbl.Rows(index).Item("viewed") = 1 Then
                html += "<tr >"
                html += "<td width='50%'>" & dTbl.Rows(index).Item("acc_desc") & "</td>"
                If dTbl.Rows(index).Item("acc_input") = "Y" Then
                    html += "<td width='5%'><input class='clsInput' runat='server' type='text' onkeypress='return isNumberKey(event)' " & classInput & " id='" & dTbl.Rows(index).Item("acc_code") & "' name='" & dTbl.Rows(index).Item("acc_code") & "' value='" & val & "'  onkeyup='" & dTbl.Rows(index).Item("acc_js_formula") & ";  " & dTbl.Rows(index).Item("acc_js_formula_pct") & ";'/></td>"
                Else
                    html += "<td width='5%'><input class='clsInput' Readonly type='text' onkeypress='return isNumberKey(event)'  " & styleView & "  " & classInput & "id='" & dTbl.Rows(index).Item("acc_code") & "' name='" & dTbl.Rows(index).Item("acc_code") & "' value='" & val & "'  onkeyup='" & dTbl.Rows(index).Item("acc_js_formula") & ";  " & dTbl.Rows(index).Item("acc_js_formula_pct") & ";'/></td>"
                End If
                html += "<td width='5%'><input type='text' onkeypress='return isNumberKey(event)' " & curne & " " & styleView & " Readonly id='pct" & dTbl.Rows(index).Item("acc_code") & "' name='pct" & dTbl.Rows(index).Item("acc_code") & "' /></td>"
                html += "</tr>"

            ElseIf dTbl.Rows(index).Item("viewed") = 0 Then
                html += "<tr  style='display:none'>"
                html += "<td>" & dTbl.Rows(index).Item("acc_desc") & "</td>"
                If dTbl.Rows(index).Item("acc_input") = "Y" Then
                    html += "<td  width='5%'><input class='clsInput' type='text' onkeypress='return isNumberKey(event)' " & classInput & " id='" & dTbl.Rows(index).Item("acc_code") & "' name='" & dTbl.Rows(index).Item("acc_code") & "' value='" & val & "'   onkeyup='" & dTbl.Rows(index).Item("acc_js_formula") & ";  " & dTbl.Rows(index).Item("acc_js_formula_pct") & ";'/></td>"
                Else
                    html += "<td width='5%'><input class='clsInput' Readonly='true' type='text' onkeypress='return isNumberKey(event)' " & styleView & " id='pct" & dTbl.Rows(index).Item("acc_code") & "' name='pct" & dTbl.Rows(index).Item("acc_code") & "' value='" & val & "'  onkeyup='" & dTbl.Rows(index).Item("acc_js_formula") & ";  " & dTbl.Rows(index).Item("acc_js_formula_pct") & ";'/></td>"
                End If
                html += "</tr>"
            End If
            'status = dr.Item("acd_status")
            If IsDBNull(dTbl.Rows(index).Item("acd_status")) Then
                status = ""
            Else
                status = dTbl.Rows(index).Item("acd_status")
            End If

            If index = dTbl.Rows.Count - 1 Then
                summary += dTbl.Rows(index).Item("acc_js_formula") + "'"
                summaryPct += dTbl.Rows(index).Item("acc_js_formula_pct") + "'"
            Else
                summary += dTbl.Rows(index).Item("acc_js_formula") + ";"
                summaryPct += dTbl.Rows(index).Item("acc_js_formula_pct") + ";"
            End If
        Next

        cs.RegisterStartupScript(Page.[GetType](), "callFunction", "callFunctionCall(" + summaryPct + ")", True)

        html += "</table>"
        ddlStatus.SelectedValue = status

        inputSummary.Value = summary
        inputSummaryPct.Value = summaryPct

    End Sub

    Public Function getAppendAccformula(ByVal report, ByVal sector, ByVal code, ByVal period)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("get_sql_formula", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = report
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = sector
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@date", SqlDbType.VarChar, 10).Value = period
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 20).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

    Public Function moneyFormatLabel(ByVal parVal)
        'return 100,000,000.00
        Dim str As String = ""
        Try
            If CheckNullString(parVal) = "" Then parVal = 0
            parVal = Convert.ToInt32(Convert.ToDecimal(parVal))

            If parVal = 0 Then
                str = "0"
            Else
                str = String.Format("{0:N0}", parVal)
            End If
            Return str
        Catch ex As Exception
            str = String.Format("{0:N0}", parVal)
        End Try
        Return str
    End Function

    Public Function CheckNullString(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return ""
        Else
            Return CType(parVal, String)
        End If
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsave.Click
        liBodyLoadAction.Text = ""
        cekInput()

        If liBodyLoadAction.Text = "" Then
            'Try
            If ddltanggal.SelectedValue <> "New Periode" Then
                tanggal = ddltanggal.SelectedValue
            Else
                tanggal = txtDate.Text
            End If
            typ = ddlStatus.SelectedValue
            ds = getAppendAccformula(report, sector, code, tanggal)
            parseData(Request.Form)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');", True)
            'liBodyLoadAction.Text = "alert('Data Sudah Tersimpan.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            'Response.Redirect("LapKeuanganDet.aspx?brwcode=" + code + "&appno=" + appno + "&sektor=" + sector + "&report=" + report)
            'Catch ex As Exception

            'End Try
        Else
            txtDate.Focus()
        End If

    End Sub

    Sub parseData(ByVal form)
        For Each dr As DataRow In ds.Tables(0).Rows
            If dr.Item("viewed") = 1 Then
                updateField(report, sector, code, txtDate.Text, dr.Item("acc_code"), form(dr.Item("acc_code")), usrLogin, ddlStatus.SelectedValue)
            End If
        Next
    End Sub

    Sub updateField(ByVal report, ByVal sector, ByVal brw_code, ByVal dates, ByVal acc_code, ByVal acc_value, ByVal usr, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("acc_update_data", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@report", report)
        cmdA.Parameters.AddWithValue("@sector", sector)
        cmdA.Parameters.AddWithValue("@brwcode", brw_code)
        cmdA.Parameters.AddWithValue("@date", dates)
        cmdA.Parameters.AddWithValue("@acccode", acc_code)
        cmdA.Parameters.AddWithValue("@accvalue", acc_value)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.Parameters.AddWithValue("@status", status)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Protected Sub ddltanggal_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddltanggal.SelectedIndexChanged
        txtDate.Text = ddltanggal.SelectedValue
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dSet As DataSet
        Dim dTbl As DataTable
        If ddlStatus.SelectedValue.ToString() = "Proyeksi" Then
            oLap.runProyeksiYearly(code)
            dSet = oLap.getPeriodByStatus(ddlStatus.SelectedValue.ToString())

            dTbl = dSet.Tables(0)
            ddltanggal.DataSource = dTbl
            ddltanggal.DataValueField = "period"
            ddltanggal.DataTextField = "period"
            ddltanggal.DataBind()

            'disable by harfiq
            'Dim tgl = dSet.Tables(0).Rows(0).Item("period").ToString()
            'txtDate.Text = tgl
            'tgl = tgl.Substring(tgl.Length - 4, 4) + tgl.Substring(tgl.Length - 7, 2) + tgl.Substring(0, 2)
            'oLap.runProyeksiOthers(code, tgl)

            rowPeriode.Attributes.Add("style", "display:'';")
            rowTgl.Attributes.Add("style", "display:none;")
            rowBtn.Attributes.Add("style", "display:'';")
        ElseIf ddlStatus.SelectedValue.ToString() = "Pilih Satu" Then
            rowPeriode.Attributes.Add("style", "display:none;")
            rowTgl.Attributes.Add("style", "display:none;")
            rowBtn.Attributes.Add("style", "display:none;")
        Else
            dSet = oLap.getPeriodByStatus(ddlStatus.SelectedValue.ToString())

            dTbl = dSet.Tables(0)
            ddltanggal.DataSource = dTbl
            ddltanggal.DataValueField = "period"
            ddltanggal.DataTextField = "period"
            ddltanggal.DataBind()

            txtDate.Text = dSet.Tables(0).Rows(0).Item("period").ToString()

            rowPeriode.Attributes.Add("style", "display:'';")
            rowTgl.Attributes.Add("style", "display:'';")
            rowBtn.Attributes.Add("style", "display:'';")
        End If

    End Sub

End Class