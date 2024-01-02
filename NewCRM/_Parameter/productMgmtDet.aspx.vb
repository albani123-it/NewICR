Public Class productMgmtDet
    Inherits System.Web.UI.Page
    Dim oMgmt As New lMgmt
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        Dim prd_id As String
        
        If Not IsPostBack Then
            fillProduct()
            fillSccRetail()
            fillFacilityNonRetail()
            ddlPrdSccRetail.Enabled = False
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("prd_id")) Then
            prd_id = Request.QueryString("prd_id")
            If prd_id <> -1 And Not IsPostBack Then
                isiData(prd_id)
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_view" Then
                    If edit = False Then
                        ddlProduct.Enabled = False
                        ddlPrdSccRetail.Enabled = False
                        ddlFacNonRetail.Enabled = False
                        txtCreditFormula.Enabled = False
                        txtSLA.Enabled = True
                        txtCutApp.Enabled = False
                        txtCutRej.Enabled = False
                    End If
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

    Sub fillProduct()
        Dim ds As DataSet
        ds = oMgmt.getProductList()
        ddlProduct.DataSource = ds
        ddlProduct.DataValueField = "lst_prd_code"
        ddlProduct.DataTextField = "lst_prd_type"
        ddlProduct.DataBind()
    End Sub

    Sub fillSccRetail()
        Dim ds As DataSet
        ds = oMgmt.getProductScc("RT")
        ddlPrdSccRetail.DataSource = ds
        ddlPrdSccRetail.DataValueField = "scc_code"
        ddlPrdSccRetail.DataTextField = "scc_name"
        ddlPrdSccRetail.DataBind()
    End Sub

    Sub fillFacilityNonRetail()
        Dim ds As DataSet
        ds = oMgmt.getProductScc("NR")
        ddlFacNonRetail.DataSource = ds
        ddlFacNonRetail.DataValueField = "scc_code"
        ddlFacNonRetail.DataTextField = "scc_name"
        ddlFacNonRetail.DataBind()
    End Sub

    Sub isiData(ByVal prd_id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oMgmt.getDetailProduct(prd_id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            product_id.Value = dt.Rows(0).Item("prd_id")
            ddlProduct.SelectedValue = dt.Rows(0).Item("prd_product")
            txtProductName.Text = dt.Rows(0).Item("prd_name")
            txtProductDesc.Text = dt.Rows(0).Item("prd_desc")
            'txtCreditFormula.Text = dt.Rows(0).Item("prd_credit_limit")
            txtCutApp.Text = dt.Rows(0).Item("prd_cut_approved")
            txtCutRej.Text = dt.Rows(0).Item("prd_cut_rejected")
            'txtSLA.Text = dt.Rows(0).Item("prd_SLA")

            If dt.Rows(0).Item("prd_retail_flag") = True Then
                chkRetail.Checked = True
                ddlPrdSccRetail.SelectedValue = dt.Rows(0).Item("prd_retail_scc")
                ddlFacNonRetail.SelectedIndex = 0
            Else
                chkRetail.Checked = False
                ddlFacNonRetail.SelectedValue = dt.Rows(0).Item("prd_nonretail_facility_scc")
                ddlPrdSccRetail.SelectedIndex = 0
            End If

            If dt.Rows(0).Item("prd_is_active") = True Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim stat As String
        Dim oCe As New enRating

        If liBodyLoadAction.Text = "" Then
            If Request.QueryString("prd_id") <> -1 Then
                Try
                    oMgmt.editProduct(Request, usrLogin, product_id.Value, ddlPrdSccRetail.SelectedValue, ddlFacNonRetail.SelectedValue)
                    If chkStatus.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Product Management Updated - PRODUCT=" & ddlProduct.SelectedItem.Text & ";SCORECARDRETAIL=" & Request.Form("ddlPrdSccRetail") & ";FACILITYRATINGNONRETAIL=" & Request.Form("ddlFacNonRetail") & ";STATUS=" + stat)
                    objLog.insertLog("productMgmt", logs)
                    oCe.Facility_Ctl()
                    'oCe.Retail_Ctl()
                    'oCe.NR_Recommendation()
                    liBodyLoadAction.Text = "alert('Product Management updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try
                    oMgmt.insertProduct(Request, usrLogin, ddlPrdSccRetail.SelectedValue, ddlFacNonRetail.SelectedValue)
                    If chkStatus.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Product Management Creation - PRODUCT=" & ddlProduct.SelectedItem.Text & ";SCORECARDRETAIL=" & Request.Form("ddlPrdSccRetail") & ";FACILITYRATINGNONRETAIL=" & Request.Form("ddlFacNonRetail") & ";STATUS=" & stat)
                    objLog.insertLog("productMgmt", logs)
                    oCe.Facility_Ctl()
                    'oCe.Retail_Ctl()
                    'oCe.NR_Recommendation()
                    liBodyLoadAction.Text = "alert('Product Management Saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If ddlProduct.SelectedIndex = 0 Then
            lMsg &= "Please select product. \n"
        End If
        If chkRetail.Checked = True Then
            If ddlPrdSccRetail.SelectedIndex = 0 Then
                lMsg &= "Please select micro rating model. \n"
            End If
        Else
            If ddlFacNonRetail.SelectedIndex = 0 Then
                lMsg &= "Please select facility rating model. \n"
            End If
        End If
        'If txtCreditFormula.Text = "" Then
        '    lMsg &= "Please input credit formula. \n"
        'End If
        If txtCutApp.Text = "" Then
            lMsg &= "Please input cut off approved. \n"
        End If
        If txtCutRej.Text = "" Then
            lMsg &= "Please input cut off rejected. \n"
        End If

        If txtCutApp.Text <> "" And txtCutRej.Text <> "" Then
            If CDbl(txtCutRej.Text) > CDbl(txtCutApp.Text) Then
                lMsg &= "Cut Off (Score) Approve must be larger than Cut Off (Score) Reject"
            ElseIf CDbl(txtCutRej.Text) = CDbl(txtCutApp.Text) Then
                lMsg &= "Cut Off (Score) Approve cannot be equal Cut Off (Score) Reject"
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class