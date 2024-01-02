Public Class foundationDet
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim objLog As New lLog
    Dim oGen As New lGeneral
    Dim oRule As New lRule
    Dim oDm As New lDataMgmt
    Dim oGlo As New lglobalSettings
    Protected delPool As Boolean = False
    Protected addPool As Boolean = False
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()

        If Not String.IsNullOrEmpty(Request.QueryString("astid")) Then
            ViewState("astid") = Request.QueryString("astid")
            Page.ClientScript.RegisterHiddenField("astid", ViewState("astid"))
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("moid")) Then
            ViewState("moid") = Request.QueryString("moid")
            Page.ClientScript.RegisterHiddenField("moid", ViewState("moid"))
        End If
        If ViewState("astid") <> "" And Not IsPostBack Then
            isiDataAsset(ViewState("astid"))
        End If
        If ViewState("moid") > 0 And Not IsPostBack Then
            isiDataModel(ViewState("moid"))
            If ViewState("astid") <> "" Then
                checkModelStatus(ViewState("astid"), ViewState("moid"))
            End If
            'isiDataPoolingRating(ViewState("moid"))
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("pid")) Then
            deletePooling(Request.QueryString("pid"))
        End If

        If Not IsPostBack Then
            isiDataApproachType()
            isiDataPDField()
            isiDataLGDField()
            isiDataEADField()
            isiDataMaturityField()
            'isiDataPoolingRule()


        End If
        isiDataPDPooling()
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        grid1.AllowAddingRecords = False
        grid1.Columns(8).AllowEdit = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_fon_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_fon_add_pool" Then
                    addPool = True
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_fon_edit_pool" Then
                    grid1.Columns(8).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_fon_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_fon_view" Then
                    If edit = False Then
                        ddlApproach.Enabled = False
                        ddlAssetClass.Enabled = False
                        rdPDText.Disabled = True
                        rdPDField.Disabled = True
                        rdLGDFixed.Disabled = True
                        rdLGDFormula.Disabled = True
                        rdLGDField.Disabled = True
                        rdEADFixed.Disabled = True
                        rdEADField.Disabled = True
                        txtCCF.Disabled = True
                        rdMaturityFixed.Disabled = True
                        rdMaturityField.Disabled = True
                    End If
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_fon_delete_pool" Then
                    delPool = True
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

    Sub isiDataApproachType()
        Try
            Dim arr As Array = {"CORPORATE", "SME", "HVCRE", "RESIDENTIAL MORTGAGE", "REVOLVING RETAIL", "OTHER RETAIL"}
            ddlApproach.DataSource = arr
            ddlApproach.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Sub isiDataPDField()
        Dim ds As DataSet
        ds = oAsset.getMaturityField("pd")
        ddlPDField.DataSource = ds
        ddlPDField.DataTextField = "name"
        ddlPDField.DataValueField = "value"
        ddlPDField.DataBind()
    End Sub

    Sub isiDataLGDField()
        Dim ds As DataSet
        ds = oAsset.getMaturityField("lgd")
        ddlLGDField.DataSource = ds
        ddlLGDField.DataTextField = "name"
        ddlLGDField.DataValueField = "value"
        ddlLGDField.DataBind()
    End Sub

    Sub isiDataEADField()
        Dim ds As DataSet
        ds = oAsset.getMaturityField("ead")
        ddlEADField.DataSource = ds
        ddlEADField.DataTextField = "name"
        ddlEADField.DataValueField = "value"
        ddlEADField.DataBind()
    End Sub

    Sub isiDataMaturityField()
        Try
            Dim ds As DataSet
            ds = oAsset.getMaturityField("maturity")
            ddlMaturityField.DataSource = ds
            ddlMaturityField.DataTextField = "name"
            ddlMaturityField.DataValueField = "value"
            ddlMaturityField.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Sub isiDataAsset(ByVal astid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfAsset(astid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblAssetCat.Text = dt.Rows(0).Item("category_name")
            lblAssetSubCat.Text = dt.Rows(0).Item("cat_name")
            lblRule.Text = oGen.CheckNullString(dt.Rows(0).Item("data_name"))
            dataid.Value = dt.Rows(0).Item("cat_data")
        End If
    End Sub

    Sub isiDataModel(ByVal moid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfModel(moid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblModelName.Text = dt.Rows(0).Item("mod_name")
            lblDesc.Text = dt.Rows(0).Item("mod_desc")
            ddlApproach.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("mod_approach_type"))
            ddlAssetClass.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("cat_class"))
            txtEffMaturity.Value = oGen.CheckNullString(dt.Rows(0).Item("mod_min_eff_maturity"))
            If dt.Rows(0).Item("mod_status") = 1 Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
            checkRadio(moid, dt)
        End If

        checkMaturity(moid)
    End Sub

    Sub checkModelStatus(ByVal assetid, ByVal modelid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.checkModelStatus(assetid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If modelid <> dt.Rows(i).Item("mod_id") Then
                    lblStatus.Visible = True
                    chkStatus.Disabled = True
                End If
            Next

        End If

    End Sub

    Sub checkMaturity(ByVal moid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfModel(moid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            Try
                If CDbl(dt.Rows(0).Item("mod_maturity")) > 0 Then
                    txtMaturityFixed.Value = oGen.CheckNullString(dt.Rows(0).Item("mod_maturity"))
                End If
            Catch ex As Exception
                txtMaturityFixed.Value = 1
                rdMaturityField.Checked = True
                If oGen.CheckNullString(dt.Rows(0).Item("mod_maturity")) = "" Then
                    ddlMaturityField.SelectedValue = "TENOR YEAR"
                Else
                    ddlMaturityField.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("mod_maturity"))
                End If
                ddlMaturityField.Attributes.Add("style", "display:'';")
            End Try
        End If
    End Sub

    Sub checkRadio(ByVal moid As String, ByVal dtbl As DataTable)
        Dim dt As DataTable
        dt = oAsset.getDetailRadio(moid)
        If dt.Rows.Count > 0 Then
            Try
                If dt.Rows(0).Item("pd_text") = True Then
                    rdPDText.Checked = True
                    rdPDField.Checked = False
                ElseIf dt.Rows(0).Item("pd_field") = True Then
                    rdPDText.Checked = False
                    rdPDField.Checked = True
                    pdPooling.Attributes.Add("style", "display:'none'")
                    ddlPDField.SelectedValue = dtbl.Rows(0).Item("mod_pd")
                End If

                If dt.Rows(0).Item("lgd_fixed") = True Then
                    rdLGDFixed.Checked = True
                    rdLGDFormula.Checked = False
                    rdLGDField.Checked = False
                    txtLGDFixed.Value = dtbl.Rows(0).Item("mod_lgd")
                ElseIf dt.Rows(0).Item("lgd_formula") = True Then
                    rdLGDFormula.Checked = True
                    rdLGDField.Checked = False
                    rdLGDFixed.Checked = False
                    'txtLGDFormula.Value = dtbl.Rows(0).Item("mod_lgd")
                ElseIf dt.Rows(0).Item("lgd_field") = True Then
                    rdLGDField.Checked = True
                    rdLGDFixed.Checked = False
                    rdLGDFormula.Checked = False
                    ddlLGDField.SelectedValue = dtbl.Rows(0).Item("mod_lgd")
                End If

                If dt.Rows(0).Item("ead_text") = True Then
                    rdEADFixed.Checked = True
                    rdEADField.Checked = False
                    txtCCF.Value = dtbl.Rows(0).Item("mod_ead")
                ElseIf dt.Rows(0).Item("ead_field") = True Then
                    rdEADField.Checked = True
                    rdEADFixed.Checked = False
                    ddlEADField.SelectedValue = dtbl.Rows(0).Item("mod_ead_value")
                End If

                If dt.Rows(0).Item("maturity_text") = True Then
                    rdMaturityFixed.Checked = True
                    rdMaturityField.Checked = False
                    txtMaturityFixed.Value = dtbl.Rows(0).Item("mod_maturity")
                ElseIf dt.Rows(0).Item("ead_text") = True Then
                    rdMaturityField.Checked = True
                    rdMaturityFixed.Checked = False
                    ddlMaturityField.SelectedValue = dtbl.Rows(0).Item("mod_maturity")
                End If
            Catch ex As Exception
                Dim dtGlo As New DataTable
                dtGlo = oGlo.getListGlobalSetting
                Dim als As String = ""
                For i As Integer = 0 To 4
                    als = dtGlo.Rows(i).Item("glo_name")
                    If als = "PD Default" Then
                        ddlPDField.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                    ElseIf als = "LGD Default" Then
                        ddlLGDField.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                    ElseIf als = "EAD Default" Then
                        ddlEADField.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                    ElseIf als = "Maturity Default" Then
                        ddlEADField.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                    End If
                Next
            End Try

        End If
    End Sub

    'Sub isiDataPoolingRule()
    '    Dim ds As DataSet
    '    ds = oRule.getPoolRule(ViewState("astid"))
    '    ddlRule.DataSource = ds
    '    ddlRule.DataTextField = "rul_name"
    '    ddlRule.DataValueField = "rul_id"
    '    ddlRule.DataBind()
    'End Sub

    'Sub isiDataPoolingRating(ByVal moid)
    '    Dim ds As DataSet
    '    ds = oRule.getPoolRating(moid, "PD")
    '    ddlRating.DataSource = ds
    '    ddlRating.DataTextField = "rate"
    '    ddlRating.DataValueField = "rate"
    '    ddlRating.DataBind()
    'End Sub

    'Function isiDataPDPooling()
    '    Dim htmlsource As String = ""
    '    Dim ds As DataSet
    '    Dim dtRuleModel As DataTable
    '    ds = oAsset.getDetailRuleModel(ViewState("moid"), "pol_pd")
    '    dtRuleModel = ds.Tables(0)
    '    If dtRuleModel.Rows.Count > 0 Then
    '        For i As Integer = 1 To dtRuleModel.Rows.Count
    '            If i Mod 2 = 0 Then
    '                htmlsource &= "<tr style='height:25px;background-color:rgb(239, 237, 226);'>"
    '            Else
    '                htmlsource &= "<tr style='height:25px;'>"
    '            End If
    '            htmlsource &= "<td width='25%'>" & dtRuleModel.Rows(i - 1).Item("pol_name") & "</td>" & _
    '            "<td width='25%'>" & dtRuleModel.Rows(i - 1).Item("rul_name") & "</td>" & _
    '            "<td width='10%'>" & dtRuleModel.Rows(i - 1).Item("pol_rule_rate") & "</td>" & _
    '            "<td width='15%'>" & oGen.intFormat(dtRuleModel.Rows(i - 1).Item("pol_pd")) & "</td>" & _
    '            "<td width='15%'>" & oGen.intFormat(dtRuleModel.Rows(i - 1).Item("pol_pd_baseline")) & "</td>"
    '            htmlsource &= "<td width='10%'>"
    '            If delPool = True Then
    '                htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fDeletePool(" & _
    '                    vbCrLf & " " & dtRuleModel.Rows(i - 1).Item("mod_ast_id") & ", " & _
    '                    vbCrLf & " " & dtRuleModel.Rows(i - 1).Item("mod_id") & ", " & _
    '                    vbCrLf & " " & dtRuleModel.Rows(i - 1).Item("pol_id") & ", " & ViewState("astid") & " " & _
    '                    vbCrLf & ");'>Delete</a>"
    '            End If
    '            htmlsource &= "</td></tr>"
    '        Next
    '    End If

    '    Return htmlsource
    'End Function

    Sub isiDataPDPooling()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailRuleModel(ViewState("moid"), "pol_pd")
        dt = ds.Tables(0)
        grid1.DataSource = dt
        grid1.DataBind()

    End Sub

    'Private Sub btnAddPool_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddPool.Click
    'cekInputModel()
    'Dim oModel As New AAS.Business.Entity.FoundationPooling
    'If liBodyLoadAction.Text = "" Then
    '    oModel.name = txtRuleName.Text
    '    oModel.rule = ddlRule.SelectedValue
    '    oModel.rating = ddlRating.SelectedValue
    '    oModel.value = txtPDValue.Text
    '    oModel.baseline = txtDistBaseline.Text
    '    Try
    '        oAsset.insertPoolAsset(oModel, usrLogin, "NEW", ViewState("moid"), "PD")
    '        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "PD Pooling Creation - NAME=" & txtRuleName.Text)
    '        objLog.insertLog("Pool", logs)
    '        liBodyLoadAction.Text = "alert('PD Pool Definition Saved');window.location.href = window.location.href;"
    '    Catch ex As Exception
    '        Dim msg As String = ex.Message.Replace("'", "\'")
    '        liBodyLoadAction.Text = "alert('" & msg & "');"
    '    End Try
    'End If
    'End Sub

    'Sub cekInputModel()
    '    Dim lMsg As String = ""
    '    If txtRuleName.Text = "" Then
    '        lMsg &= "Please input pooling name.\n"
    '    End If
    '    If txtPDValue.Text = "" Then
    '        lMsg &= "Please input PD value.\n"
    '    End If
    '    If txtDistBaseline.Text = "" Then
    '        lMsg &= "Please input distribution baseline.\n"
    '    End If

    '    If lMsg <> "" Then
    '        liBodyLoadAction.Text = "alert('" & lMsg & "');"
    '    End If
    'End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim oModel As New AAS.Business.Entity.Basel
        Dim oRadio As New AAS.Business.Entity.Radio
        If liBodyLoadAction.Text = "" Then
            oModel.approach_type = ddlApproach.SelectedValue
            oModel.hurdle_rate = 0
            If rdPDText.Checked = True Then
                oModel.pd = 0
            ElseIf rdPDField.Checked = True Then
                oModel.pd = ddlPDField.SelectedValue
            End If

            If rdLGDFixed.Checked = True Then
                oModel.lgd = txtLGDFixed.Value
            ElseIf rdLGDFormula.Checked = True Then
                oModel.lgd = txtLGDFormula.Value
            ElseIf rdLGDField.Checked = True Then
                oModel.lgd = ddlLGDField.SelectedValue
            End If

            If rdEADFixed.Checked = True Then
                oModel.ead = txtCCF.Value
                oModel.ead_value = ""
            ElseIf rdEADField.Checked = True Then
                oModel.ead = ""
                oModel.ead_value = ddlEADField.SelectedValue
            End If

            If rdMaturityFixed.Checked = True Then
                oModel.maturity = txtMaturityFixed.Value
            ElseIf rdMaturityField.Checked = True Then
                oModel.maturity = ddlMaturityField.SelectedValue
            End If

            If txtEffMaturity.Value = "" Then
                oModel.effmaturity = 1
            Else
                oModel.effmaturity = txtEffMaturity.Value
            End If

            Dim stat As String
            If Request.Form("chkStatus") = "on" Then
                stat = "Active"
                oModel.status = 1
            Else
                stat = "Inactive"
                oModel.status = 2
            End If

            oRadio.mod_id = ViewState("moid")
            oRadio.pd_text = rdPDText.Checked
            oRadio.pd_field = rdPDField.Checked
            oRadio.lgd_fixed = rdLGDFixed.Checked
            oRadio.lgd_formula = rdLGDFormula.Checked
            oRadio.lgd_field = rdLGDField.Checked
            oRadio.ead_text = rdEADFixed.Checked
            oRadio.ead_field = rdEADField.Checked
            oRadio.maturity_text = rdMaturityFixed.Checked
            oRadio.maturity_field = rdMaturityField.Checked

            Try
                oAsset.updateModelAsset(oModel, usrLogin, ViewState("moid"), ddlAssetClass.SelectedValue)
                oAsset.updateModelRadio(oRadio)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model updated - MODELNAME=" & lblModelName.Text & ";ASSETCLASS=" & ddlAssetClass.SelectedValue & ";STATUS=" & stat)
                objLog.insertLog("asset_model", logs)

                liBodyLoadAction.Text = "alert('Asset Model Definition Saved');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If rdLGDFixed.Checked = True Then
            If txtLGDFixed.Value = "" Then
                lMsg &= "Please input LGD Fixed value.\n"
            End If
        End If
        'If rdLGDFormula.Checked = True Then
        '    If txtLGDFormula.Value = "" Then
        '        lMsg &= "Please input LGD Formula.\n"
        '    End If
        'End If
        If rdEADFixed.Checked = True Then
            If txtCCF.Value = "" Then
                lMsg &= "Please input Credit Conversion Center.\n"
            End If
        End If
        If rdMaturityFixed.Checked = True Then
            If txtMaturityFixed.Value = "" Then
                lMsg &= "Please input Maturity Fixed value.\n"
            End If
        End If

        If Request.Form("chkStatus") = "on" Then
            
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oAsset.getDetailOfAsset(ViewState("astid"))
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                Dim ds2 As DataSet
                Dim dt2 As DataTable
                ds2 = oDm.getDataManagementDetail(dt.Rows(0).Item("cat_data"))
                dt2 = ds2.Tables(0)
                If dt2.Rows.Count > 0 Then
                    If dt.Rows(0).Item("is_Active") = False Then
                        lMsg &= "Can\'t active this asset because Data Source is not active.\n"
                    End If
                End If
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Sub deletePooling(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.checkPooling(id)
        dt = ds.Tables(0)
        If dt.Rows.Count = 1 Then
            Try
                oAsset.deletePoolAsset(id, usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "PD Pool Deleted - POLLID=" & dt.Rows(0).Item("pol_id") & ";POOLNAME=" & dt.Rows(0).Item("pol_name"))
                objLog.insertLog("PD", logs)

                liBodyLoadAction.Text = "alert('Pooling has been successfully.');window.location.href = window.location.href;"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.checkPooling(dataItem("pol_id"))
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            Try
                oAsset.deletePoolAsset(dataItem("pol_id"), usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "PD Pool Deleted - POLLID=" & dt.Rows(0).Item("pol_id") & ";POOLNAME=" & dt.Rows(0).Item("pol_name"))
                objLog.insertLog("Pool", logs)
                'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('PD Pooling has been successfully deleted.');reloadOboutGrid();", True)
                liBodyLoadAction.Text = "alert('Pooling has been successfully.');reloadOboutGrid();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub
End Class