Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Net
Imports System.Console

Public Class advanced_IRB_model
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim objLog As New lLog
    Dim oGen As New lGeneral
    Dim oRule As New lRule
    Dim oAdv As New lAdvancedIRB
    Dim oDm As New lDataMgmt
    Public model_status As Integer
    Public hurdleRate As String = ""
    Protected delPool As Boolean = False
    Protected addPool As Boolean = False

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not String.IsNullOrEmpty(Request.QueryString("scid")) Then
            ViewState("astid") = Request.QueryString("scid")
            Page.ClientScript.RegisterHiddenField("astid", ViewState("astid"))
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("iid")) Then
            ViewState("moid") = Request.QueryString("iid")
            If ViewState("astid") <> "" Then
                checkModelStatus(ViewState("astid"), ViewState("moid"))
            End If
            Page.ClientScript.RegisterHiddenField("moid", ViewState("moid"))
        End If

        'ViewState("scid") = Request.QueryString("scid")
        'ViewState("iid") = Request.QueryString("iid")

        If ViewState("astid") <> "" And Not IsPostBack Then
            getDetailAsset()
        End If


        If ViewState("moid") > 0 And Not IsPostBack Then

            getListOfMaturityField()
            getListOfApproachType()
            getListOfEADField()
            getListOfPDField()
            getDetailInternalModel()
            getDetailModel()
        End If

        isiDataPDPooling()
        isiDataLGDPooling()
        isiDataEADPooling()
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        grid1.AllowAddingRecords = False
        grid2.AllowAddingRecords = False
        grid3.AllowAddingRecords = False
        grid1.Columns(8).AllowEdit = False
        grid2.Columns(7).AllowEdit = False
        grid3.Columns(7).AllowEdit = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_add_pool" Then
                    grid1.AllowAddingRecords = True
                    grid2.AllowAddingRecords = True
                    grid3.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_edit_pool" Then
                    grid1.Columns(8).AllowEdit = True
                    grid2.Columns(7).AllowEdit = True
                    grid3.Columns(7).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_view" Then
                    If edit = True Then
                        btnSaveDetail.Enabled = True
                    Else
                        btnSaveDetail.Enabled = False
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

    Sub isiDataPDPooling()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailRuleModel(ViewState("moid"), "pol_pd")
        dt = ds.Tables(0)
        grid1.DataSource = dt
        grid1.DataBind()

    End Sub

    Sub isiDataLGDPooling()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailRuleModel(ViewState("moid"), "pol_lgd")
        dt = ds.Tables(0)
        grid2.DataSource = dt
        grid2.DataBind()

    End Sub

    Sub isiDataEADPooling()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailRuleModel(ViewState("moid"), "pol_ead")
        dt = ds.Tables(0)
        grid3.DataSource = dt
        grid3.DataBind()

    End Sub

    Sub getDetailAsset()

        Dim dsTable As DataSet
        Dim dtTable As DataTable
        With New lAsset
            dsTable = .getDetailOfAsset(ViewState("astid"))
            dtTable = dsTable.Tables(0)
        End With
        With dtTable.Rows(0)
            txtAssetId.Value = .Item("id")
            txtCat.Text = .Item("category_name")
            txtSubCat.Text = .Item("cat_name")
            lblDataRule.Text = oGen.CheckNullString(.Item("data_name"))
        End With
    End Sub


    Sub getListOfMaturityField()
        ddlMaturity.DataTextField = "name"
        ddlMaturity.DataValueField = "value"
        With New lAsset
            ddlMaturity.DataSource = .getMaturityField("maturity")
            ddlMaturity.DataBind()
        End With
    End Sub

    Sub getListOfEADField()
        ddlEAD.DataTextField = "name"
        ddlEAD.DataValueField = "value"
        With New lAsset
            ddlEAD.DataSource = .getMaturityField("ead")
            ddlEAD.DataBind()
        End With
    End Sub

    Sub getListOfPDField()
        ddlPD.DataTextField = "name"
        ddlPD.DataValueField = "value"
        With New lAsset
            ddlPD.DataSource = .getMaturityField("pd")
            ddlPD.DataBind()
        End With
    End Sub

    Sub getDetailInternalModel()
        With New lAdvancedIRB
            txtInternalPD.DataSource = .getInternalModel()
            txtInternalPD.DataBind()
            txtInternalLGD.DataSource = .getInternalModel()
            txtInternalLGD.DataBind()
            txtInternalEAD.DataSource = .getInternalModel()
            txtInternalEAD.DataBind()
        End With
    End Sub

    Sub getListOfApproachType()
        Dim arrBln2 As Array = oGen.parmApproachType
        With ddlApproachType.Items
            ddlApproachType.Items.Clear()
            For i = 0 To arrBln2.Length - 1
                .Add(arrBln2(i))
            Next
        End With
    End Sub


    Sub getDetailModel()
        GetHurdleRate()
        Dim dsDetail As DataSet
        Dim dtDetail As DataTable
        With New lAsset
            dsDetail = .getDetailOfModel(ViewState("moid"))
            dtDetail = dsDetail.Tables(0)
        End With
        With dtDetail.Rows(0)
            txtId.Value = .Item("mod_id")
            txtName.Text = .Item("mod_name")
            txtDescription.Text = .Item("mod_desc")
            ddlApproach.Value = .Item("mod_approach")
            ddlApproachType.Text = oGen.CheckNullString(.Item("mod_approach_type"))
            txtHurdle.Text = oGen.CheckNullString(hurdleRate)
            model_status = .Item("mod_status")
            ViewState("model_status") = model_status
            txtAssetClass.SelectedValue = oGen.CheckNullString(.Item("cat_class"))
            txtInternalPD.Text = oGen.CheckNullString(.Item("mod_pd"))
            txtInternalLGD.Text = oGen.CheckNullString(.Item("mod_lgd"))
            txtInternalEAD.Text = oGen.CheckNullString(.Item("mod_ead"))
            txtEffMaturity.Value = oGen.CheckNullString(.Item("mod_min_eff_maturity"))
            checkMaturity()
            'txtMaturity.Value = oGen.CheckNullDecimal(.Item("mod_maturity"))
            If .Item("mod_status") = 1 Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
        End With

        Try
            'Dim dsTable As DataSet
            Dim dtTable As DataTable
            With New lAsset
                'dsTable = .getDetailRadio(ViewState("iid"))
                dtTable = .getDetailRadio(ViewState("moiid"))
                'dtTable = dsTable.Tables(0)
            End With

            If dtTable.Rows(0).Item("pd_text") Then
                rdPDText.Checked = True
                rdPDField.Checked = False
            ElseIf dtTable.Rows(0).Item("pd_field") Then
                rdPDText.Checked = False
                rdPDField.Checked = dtTable.Rows(0).Item("pd_field")
                ddlPD.SelectedValue = dtDetail.Rows(0).Item("mod_pd")
            End If
            If dtTable.Rows(0).Item("ead_text") Then
                rdEADField.Checked = False
                rdEADText.Checked = True
                'txtEAD.Value = dtDetail.Rows(0).Item("mod_ead_value")
            ElseIf dtTable.Rows(0).Item("ead_field") Then
                rdEADText.Checked = False
                rdEADField.Checked = True
                ddlEAD.SelectedValue = dtDetail.Rows(0).Item("mod_ead_value")
            End If
            If dtTable.Rows(0).Item("maturity_text") Then
                rdMaturityField.Checked = False
                rdMaturityText.Checked = True
                txtMaturity.Value = dtDetail.Rows(0).Item("mod_maturity")
            ElseIf dtTable.Rows(0).Item("maturity_field") Then
                rdMaturityText.Checked = False
                rdMaturityField.Checked = True
                ddlMaturity.SelectedValue = dtDetail.Rows(0).Item("mod_maturity")
            End If

        Catch ex As Exception
            Dim dtGlo As New DataTable
            dtGlo = oAdv.getGlobalSetting()
            Dim als As String = ""
            For i As Integer = 0 To 4
                als = dtGlo.Rows(i).Item("glo_name")
                If als = "PD Default" Then
                    ddlPD.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                ElseIf als = "EAD Default" Then
                    ddlEAD.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                ElseIf als = "Maturity Default" Then
                    ddlMaturity.SelectedValue = oGen.CheckNullString(dtGlo.Rows(i).Item("glo_value"))
                End If
            Next
        End Try
    End Sub

   

    Sub GetHurdleRate()
        Dim dtHurdle As DataTable
        dtHurdle = oAdv.getGlobalSetting()
        Dim als As String = ""
        For i As Integer = 0 To 4
            If dtHurdle.Rows(i).Item("glo_name") = "Hurdle Rate" Then
                hurdleRate = dtHurdle.Rows(i).Item("glo_value")
            End If
        Next
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

    Sub checkMaturity()
        Dim dsTable As DataSet
        Dim dtTable As DataTable
        With New lAsset
            dsTable = .getDetailOfModel(ViewState("moid"))
            dtTable = dsTable.Tables(0)
        End With
        With dtTable.Rows(0)
            Try
                'txtMaturity.Value = oGen.CheckNullString(.Item("mod_maturity"))
                If CDbl(.Item("mod_maturity")) > 0 Then
                    txtMaturity.Value = oGen.CheckNullString(.Item("mod_maturity"))
                End If

            Catch ex As Exception
                txtMaturity.Value = 1
                'txtMaturity.Disabled = True
                rdMaturityField.Checked = True
                'If oGen.CheckNullString(.Item("mod_maturity")) = "" Then
                '    ddlMaturity.SelectedValue = "TENOR_YEAR"
                'Else
                '    ddlMaturity.SelectedValue = oGen.CheckNullString(.Item("mod_maturity"))
                'End If
                ddlMaturity.SelectedValue = oGen.CheckNullString(.Item("mod_maturity"))
                liBodyLoadAction.Text = "document.getElementById('ddlMaturity').style.visibility = 'visible';"
            End Try
        End With
    End Sub

    Sub updateModelAsset(ByVal stat)
        Dim oModel As New AAS.Business.Entity.Basel
        Dim oRadio As New AAS.Business.Entity.Radio
        Dim maturity As String = ""

        If rdMaturityField.Checked Then
            maturity = ddlMaturity.Text
        Else
            maturity = txtMaturity.Value
        End If

        With oModel
            .approach_type = ddlApproachType.Text
            .hurdle_rate = Replace(txtHurdle.Text, ",", ".")

            If rdPDField.Checked Then
                .pd = ddlPD.SelectedValue
            Else
                .pd = txtInternalPD.Text
            End If

            '.lgd = txtInternalLGD.Text
            '.ead = txtInternalEAD.Text

            If rdEADText.Checked Then
                .ead_value = ""
            Else
                .ead_value = ddlEAD.SelectedValue
            End If

            If txtEffMaturity.Value = "" Then
                .effmaturity = 1
            Else
                .effmaturity = txtEffMaturity.Value
            End If

            .maturity = maturity
            .status = stat
            With oRadio
                .mod_id = ViewState("moid")
                .pd_text = rdPDText.Checked
                .pd_field = rdPDField.Checked
                .lgd_fixed = False
                .lgd_formula = False
                .lgd_field = False
                .ead_text = rdEADText.Checked
                .ead_field = rdEADField.Checked
                .maturity_text = rdMaturityText.Checked
                .maturity_field = rdMaturityField.Checked
            End With
            With New lAdvancedIRB
                .updateModelAsset(oModel, Session("UserLoginID"), "NEW", ViewState("moid"), Replace(txtHurdle.Text, ",", "."), txtAssetClass.Text)
            End With
            With New lAsset
                .updateModelRadio(oRadio)
                If btnSaveDetail.Text = "Save" Then
                    liBodyLoadAction.Text = "alert('Model Definition saved.');javascript:CloseMe();"
                Else
                    liBodyLoadAction.Text = "alert('Scorecard Definition saved.');javascript:fScorecardDetail('view=0');"
                End If
            End With
        End With
    End Sub

    Private Sub btnSaveDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveDetail.Click
        cekInput()

        If liBodyLoadAction.Text = "" Then
            Dim oModel As New AAS.Business.Entity.Basel
            Dim oRadio As New AAS.Business.Entity.Radio
            Dim maturity As String = ""
            Dim stat As String = ""
            If Request.Form("chkStatus") = "on" Then
                stat = "Active"
                oModel.status = 1
            Else
                stat = "Inactive"
                oModel.status = 2
            End If

            If rdMaturityField.Checked Then
                maturity = ddlMaturity.Text
            Else
                maturity = txtMaturity.Value
            End If

            With oModel
                .approach_type = ddlApproachType.Text
                .hurdle_rate = Replace(txtHurdle.Text, ",", ".")

                If rdPDField.Checked Then
                    .pd = ddlPD.SelectedValue
                Else
                    .pd = txtInternalPD.Text
                End If

                .lgd = txtInternalLGD.Text
                .ead = txtInternalEAD.Text

                If rdEADText.Checked Then
                    .ead_value = ""
                Else
                    .ead_value = ddlEAD.SelectedValue
                End If
                .maturity = maturity
                .effmaturity = txtEffMaturity.Value
                With oRadio
                    .mod_id = ViewState("moid")
                    .pd_text = rdPDText.Checked
                    .pd_field = rdPDField.Checked
                    .lgd_fixed = False
                    .lgd_formula = False
                    .lgd_field = False
                    .ead_text = rdEADText.Checked
                    .ead_field = rdEADField.Checked
                    .maturity_text = rdMaturityText.Checked
                    .maturity_field = rdMaturityField.Checked
                End With
                Try
                    With New lAdvancedIRB
                        .updateModelAsset(oModel, Session("UserLoginID"), "NEW", ViewState("moid"), Replace(txtHurdle.Text, ",", "."), txtAssetClass.Text)
                    End With
                    With New lAsset
                        .updateModelRadio(oRadio)
                    End With
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model updated - MODELNAME=" & txtName.Text & ";ASSETCLASS=" & txtAssetClass.SelectedValue & ";STATUS=" & stat)
                    objLog.insertLog("asset_model", logs)
                    liBodyLoadAction.Text = "alert('Asset Model Definition Saved');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert(" & ex.Message.Replace("'", "\'") & ")"
                End Try

            End With
            Try

            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        
        If rdMaturityText.Checked = True Then
            If txtMaturity.Value = "" Then
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

    Protected Sub deletePD(ByVal sender As Object, ByVal e As CommandEventArgs)
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
                liBodyLoadAction.Text = "reloadOboutGrid();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub

    Protected Sub deleteLGD(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid2.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.checkPooling(dataItem("pol_id"))
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            Try
                oAsset.deletePoolAsset(dataItem("pol_id"), usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "LGD Pool Deleted - POLLID=" & dt.Rows(0).Item("pol_id") & ";POOLNAME=" & dt.Rows(0).Item("pol_name"))
                objLog.insertLog("Pool", logs)
                'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('PD Pooling has been successfully deleted.');reloadOboutGrid();", True)
                liBodyLoadAction.Text = "reloadOboutGrid();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub

    Protected Sub deleteCCF(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid3.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.checkPooling(dataItem("pol_id"))
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            Try
                oAsset.deletePoolAsset(dataItem("pol_id"), usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "EAD Pool Deleted - POLLID=" & dt.Rows(0).Item("pol_id") & ";POOLNAME=" & dt.Rows(0).Item("pol_name"))
                objLog.insertLog("Pool", logs)
                'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('PD Pooling has been successfully deleted.');reloadOboutGrid();", True)
                liBodyLoadAction.Text = "reloadOboutGrid();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If
    End Sub
End Class