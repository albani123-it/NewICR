Public Class Proyeksi_PL
    Inherits System.Web.UI.Page
    Dim oPrj As New lProjection
    Dim oGen As New lGeneral

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        If Not IsPostBack Then
            fillDdl()
        End If
    End Sub

    Sub fillDdl()
        Dim year As String = Now.ToString("yyyy")
        Dim x As Integer = CInt(year) + 1
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
    End Sub

    Sub bind()
        Dim myTable As New DataTable
        Dim countIdt As Integer = 0

        Dim brw = IIf(txtBrwCode.Text = "", "BR-BDKI-2017-00018", txtBrwCode.Text)
        Dim sector = IIf(txtSector.Text = "", "BR-BDKI-2017-00018", txtSector.Text)
        Dim cons As String = ""
        Dim start_year As Integer = ddlStartYear.SelectedValue
        Dim end_year As Integer = ddlEndYear.SelectedValue
        cons += "<table id='fixedHeader' class='tbl' cellspacing='0' cellpadding='3' width='100%' style='border-collapse: separate;text-align:left;border: thin solid #bcbcbc;border-radius: 5px;'>"

        Dim ds As DataSet
        Dim dt As DataTable

        '======= header ===========
        Dim period_header As String = ""
        cons += "<thead>"
        cons += "<th style='min-width:150px;width:auto;' colspan='2'><div>Description</div></th>"
        For i As Integer = start_year To end_year - 1
            For j As Integer = 1 To 12
                period_header = oGen.returnShortMonEN(j) & "-" & start_year
                'If j < 10 Then
                '    cons &= "<th style='min-width:150px;width:auto;'><div style='text-align:right;'>" & start_year & "0" & j & "</div></th>"
                'Else
                '    cons &= "<th style='min-width:150px;width:auto;'><div style='text-align:right;'>" & start_year & j & "</div></th>"
                'End If
                cons &= "<th style='min-width:150px;width:auto;'><div style='text-align:right;'>" & period_header & "</div></th>"
            Next
            cons &= "<th style='min-width:150px;width:auto;'><div style='text-align:right;'>Total " & start_year & "</div></th>"
            start_year += 1
        Next
        cons += "</thead>"
        '======= end header =======

        Dim color As String = ""
        Dim dsVal As DataSet
        Dim dtVal As DataTable

        ds = oPrj.getListOfProfitLoss(sector)
        dt = ds.Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1

            start_year = ddlStartYear.SelectedValue
            end_year = ddlEndYear.SelectedValue
            Dim period As String = ""

            '========== set style of row======================
            Dim identasi As String = ""
            myTable = oGen.checkStyle(dt.Rows(i).Item("acc_code"))

            countIdt = Convert.ToInt32(myTable.Rows(0)(1))
            If countIdt > 0 Then
                For idt = 1 To countIdt
                    identasi += "&nbsp; "
                Next
            End If

            If i Mod 2 = 0 Then
                color = "#ebecec"
            Else
                color = "#f6f7f7"
            End If

            If i = 0 Then
                cons &= "<tr bgcolor='" & color & "'>"
            Else
                cons &= "<tr bgcolor='" & color & "' style='" + myTable.Rows(0)(0) + "'>"
            End If
            '========== end set style of row===================

            '======= variable =========
            'cons &= "<td id='td_" & dt.Rows(i).Item("acc_code") & "' name='id='td_" + dt.Rows(i).Item("acc_code") & _
            '    "' style='width:250px;'><div style='width:250px;'>" & identasi & dt.Rows(i).Item("acc_desc") & "</div></td>"
            cons &= "<td id='td_" & dt.Rows(i).Item("acc_code") & "' name='id='td_" + dt.Rows(i).Item("acc_code") & _
                "' style='width:250px;'><div style='width:250px;'>" & dt.Rows(i).Item("acc_desc") & "</div></td>"
            '======= end variable =====

            cons &= "<td style='font-weight:bold;'><div style='width:150px;'>" & _
                "<input type='text' id='' name='' class='inpTxt' style='width:30px;text-align:right;float:none;' />&nbsp;%&nbsp;&nbsp;x" & _
                "&nbsp;&nbsp; <input type='text' id='' name='' class='inpTxt' style='width:30px;text-align:right;float:none;' />&nbsp;year" & _
                "</div></td>"
            '"&nbsp;&nbsp; <input type='text' id='' name='' class='inpTxt' style='text-align:right;float:none;' />&nbsp;" & _
            '======= value ============
            Dim total_per_year As Decimal = 0
            Dim total_per_year_flag As String = "N"
            For j As Integer = start_year To end_year - 1
                For k As Integer = 1 To 12
                    If k < 10 Then
                        period = start_year & "0" & k
                    Else
                        period = start_year & k
                    End If

                    dsVal = oPrj.getDataOfProfitLoss(brw, period, dt.Rows(i).Item("acc_code"))
                    dtVal = dsVal.Tables(0)
                    If dtVal.Rows.Count > 0 Then
                        cons &= "<td title='" & dt.Rows(i).Item("acc_desc") & "'><div style='text-align:right;'>" & _
                            "<input type='text' id='col_" & period & "_" & dt.Rows(i).Item("acc_code") & _
                            "' name='col_" & period & "_" & dt.Rows(i).Item("acc_code") & "' class='inpTxt' style='float:none;text-align:right;' value='" & dtVal.Rows(0).Item("add_value_format") & _
                            "' onkeypress='return isNumberKey(event)'" & _
                            "onkeyup='this.value=formatCurrency(this.value);jsCalcTotalPerYear(&#39;" & period & "&#39;,&#39;" & dt.Rows(i).Item("acc_code") & "&#39;);" & _
                            "jsProyeksiPerPeriod(&#39;col_" & period & "_&#39;,&#39;" & period & "&#39;," & dt.Rows(i).Item("acc_js_formula") & ");'" & _
                            " onchange='updateData(&#39;" & period & "&#39;, &#39;" & dt.Rows(i).Item("acc_code") & "&#39;);' />" & _
                            "</div></td>"
                        total_per_year = total_per_year + CDec(dtVal.Rows(0).Item("add_value"))
                        total_per_year_flag = "Y"
                    Else
                        cons &= "<td title='" & dt.Rows(i).Item("acc_desc") & "'><div style='text-align:right;'>" & _
                            "<input type='text' id='col_" & period & "_" & dt.Rows(i).Item("acc_code") & _
                            "' name='col_" & period & "_" & dt.Rows(i).Item("acc_code") & "' class='inpTxt' style='float:none;text-align:right;'" & _
                            "onkeypress='return isNumberKey(event)'" & _
                            "onkeyup='this.value=formatCurrency(this.value);jsCalcTotalPerYear(&#39;" & period & "&#39;,&#39;" & dt.Rows(i).Item("acc_code") & "&#39;);" & _
                            "jsProyeksiPerPeriod(&#39;col_" & period & "_&#39;,&#39;" & period & "&#39;," & dt.Rows(i).Item("acc_js_formula") & ");'" & _
                            "onchange='updateData(&#39;" & period & "&#39;, &#39;" & dt.Rows(i).Item("acc_code") & "&#39;);' />" & _
                            "</div></td>"
                        '"jsProyeksiPerPeriod(&#39;col_" & period & "_" & dt.Rows(i).Item("acc_code") & "&#39;," & dt.Rows(i).Item("acc_js_formula") & ");'" & _
                    End If

                Next
                If total_per_year_flag <> "Y" Then
                    cons &= "<td title='Total " & dt.Rows(i).Item("acc_desc") & " " & start_year & "'><div style='text-align:right;'><input type='text' readonly='true' id='col_total_" & start_year & "_" & dt.Rows(i).Item("acc_code") & _
                    "' name='col_total_" & start_year & "_" & dt.Rows(i).Item("acc_code") & "' class='inpTxtDisabled' style='float:none;text-align:right;' " & _
                    "' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' /></div></td>"
                Else
                    cons &= "<td title='Total " & dt.Rows(i).Item("acc_desc") & " " & start_year & "'><div style='text-align:right;'><input type='text' readonly='true' id='col_total_" & start_year & "_" & dt.Rows(i).Item("acc_code") & _
                    "' name='col_total_" & start_year & "_" & dt.Rows(i).Item("acc_code") & "' class='inpTxtDisabled' style='float:none;text-align:right;' " & _
                    "value='" & oGen.moneyFormatLabel(total_per_year) & "'" & _
                    "' onkeypress='return isNumberKey(event)' onkeyup='this.value=formatCurrency(this.value);' /></div></td>"
                End If
                

                start_year += 1
            Next
            '======= end value ========

            cons += "</tr>"

        Next
        cons += "</table>"

        litBody.Text = cons
    End Sub

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        cekInput()
        bind()
    End Sub

    Sub cekInput()

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function updateData(ByVal val As String) As String
        Dim json As String = ""
        Dim temp = val.ToString.Split(",")
        With New lProjection
            Try
                .insertUpdateDataPL(temp(0), temp(1), temp(2), temp(3), "88899")
                json = "Success"
            Catch ex As Exception
                json = "Error"
            End Try

        End With

        Return json
    End Function
End Class