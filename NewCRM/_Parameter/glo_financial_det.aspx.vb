Public Class glo_financial_det
    Inherits System.Web.UI.Page
    Dim oFin As New lFinancial
    Dim oFun As New lFunction
    Dim objLog As New lLog

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

        If Request.QueryString("c") <> "" And Request.QueryString("c") IsNot Nothing Then
            If Request.QueryString("c") <> "-1" And Not IsPostBack Then
                bindData(oFun.DecryptString(Request.QueryString("c")))
            ElseIf Request.QueryString("c") = "-1" And Not IsPostBack Then
                fillCode()
            End If
        Else
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_edit" Then
                    tf = True
                End If
            Next
            If Not tf Then
                Session.Abandon()
                Response.Redirect("../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub fillCode()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oFin.getNewCodeFinancialStatement
        dt = ds.Tables(0)
        If dt.rows.count > 0 Then
            txtCode.Text = dt.Rows(0).Item("acc_code")
        End If
    End Sub

    Sub fillDdl()
        Dim ds As DataSet
        ds = oFin.getListOfFinancialStatementType
        ddlReport.Items.Clear()
        ddlReport.DataSource = ds
        ddlReport.DataValueField = "acc_rpt"
        ddlReport.DataTextField = "acc_rpt_str"
        ddlReport.DataBind()

        ddlFontStyle.Items.Clear()
        ddlFontStyle.Items.Add(New ListItem("Select One", "Select One"))
        ddlFontStyle.Items.Add(New ListItem("Normal", ""))
        ddlFontStyle.Items.Add(New ListItem("Bold", "B"))
        ddlFontStyle.Items.Add(New ListItem("Italic", "I"))
        ddlFontStyle.Items.Add(New ListItem("Underline", "U"))
    End Sub

    Function fillSector()
        Dim html As String = ""
        html &= "<table>"
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oFin.getFinancialStatementSector
        dt = ds.Tables(0)
        If Request.QueryString("c") <> "-1" Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dset As DataSet
                Dim dtbl As DataTable
                dset = oFin.getFinancialStatementSectorExists(oFun.DecryptString(Request.QueryString("c")))
                dtbl = dset.Tables(0)
                html &= "<tr>"
                For j As Integer = 0 To dtbl.Rows.Count - 1
                    If dt.Rows(i).Item("lst_sec_id") = dtbl.Rows(j).Item("lst_sec_id") Then
                        If dtbl.Rows(j).Item("acc_is_used") = 1 Then
                            html &= "<td style='padding-left:0;'><input type='checkbox' checked id='chk_" & dt.Rows(i).Item("lst_sec_id") & "' name='chk_" & dt.Rows(i).Item("lst_sec_id") & "' />" & dt.Rows(i).Item("lst_sec_name") & "</td>"
                        Else
                            html &= "<td style='padding-left:0;'><input type='checkbox' id='chk_" & dt.Rows(i).Item("lst_sec_id") & "' name='chk_" & dt.Rows(i).Item("lst_sec_id") & "' />" & dt.Rows(i).Item("lst_sec_name") & "</td>"
                        End If
                    End If
                Next
                html &= "</tr>"
            Next
        Else
            For i As Integer = 0 To dt.Rows.Count - 1
                html &= "<tr>"
                html &= "<td style='padding-left:0;'><input type='checkbox' id='chk_" & dt.Rows(i).Item("lst_sec_id") & "' name='chk_" & dt.Rows(i).Item("lst_sec_id") & "' />" & dt.Rows(i).Item("lst_sec_name") & "</td>"
                html &= "</tr>"
            Next
        End If
        
        html &= "</table>"
        Return html
    End Function

    Sub bindData(ByVal code)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oFin.getDetailOfFinancialStatement(code)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            txtCode.Text = dt.Rows(0).Item("acc_code")
            txtDescription.Text = dt.Rows(0).Item("acc_desc")
            rdInputable.SelectedValue = dt.Rows(0).Item("acc_input")
            txtOrder.Text = dt.Rows(0).Item("acc_urut")
            rdProyeksi.SelectedValue = dt.Rows(0).Item("acc_proyeksi")
            txtSQLFormula.Text = dt.Rows(0).Item("acc_sql_formula")
            txtSQLFormulaProyeksi.Text = dt.Rows(0).Item("acc_sql_formula_proyeksi")
            txtJsFormula.Text = dt.Rows(0).Item("acc_js_formula")
            txtJsPercentageFormula.Text = dt.Rows(0).Item("acc_js_formula_pct")
            txtNotes.Text = dt.Rows(0).Item("acc_notes")
            rdConditional.SelectedValue = dt.Rows(0).Item("acc_is_conditional")
            txtCondition.Text = dt.Rows(0).Item("acc_condition")
            txtConditionValue.Text = dt.Rows(0).Item("acc_condition_value")
            ddlReport.SelectedValue = dt.Rows(0).Item("acc_rpt")
            ddlFontStyle.SelectedValue = dt.Rows(0).Item("acc_font_style")
            txtIdentasi.Text = dt.Rows(0).Item("acc_identasi")
            txtColor.Text = dt.Rows(0).Item("acc_color")
        End If
    End Sub

    Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            Try
                Dim dset As DataSet
                Dim dtbl As DataTable
                dset = oFin.getFinancialStatementSectorExists(oFun.DecryptString(Request.QueryString("c")))
                dtbl = dset.Tables(0)
                If Request.QueryString("c") = "-1" Then
                    oFin.insertUpdateFinancialStatement(Request, "insert", usrLogin)
                    For i As Integer = 0 To dtbl.Rows.Count - 1
                        oFin.updateFinancialStatementSector(Request, dtbl.Rows(i).Item("lst_sec_id"))
                    Next

                    liBodyLoadAction.Text = "alert('Financial Statement has been successfully saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Else
                    oFin.insertUpdateFinancialStatement(Request, "update", usrLogin)
                    For i As Integer = 0 To dtbl.Rows.Count - 1
                        oFin.updateFinancialStatementSector(Request, dtbl.Rows(i).Item("lst_sec_id"))
                    Next

                    liBodyLoadAction.Text = "alert('Financial Statement has been successfully updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                End If

            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
    End Sub

    Sub cekInput()

    End Sub
End Class