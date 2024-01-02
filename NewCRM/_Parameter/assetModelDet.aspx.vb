Public Class assetModelDet
    Inherits System.Web.UI.Page
    Dim oRule As New lRule
    Dim oAsset As New lAsset
    Dim oGen As New lGeneral
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        Dim astid As String
        Dim moid As String
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("astid")) Then
                astid = Request.QueryString("astid")
                bindAsset(astid)
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("moid")) Or Not Request.QueryString("moid") = 0 Then
                moid = Request.QueryString("moid")
                If Not moid = 0 Then
                    bindModel(moid)
                End If
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        btnSave.Enabled = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mdl_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mdl_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mdl_edit" Then
                    btnSave.Enabled = True
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

    Sub bindAsset(ByVal astid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfAsset(astid)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            ViewState("astid") = astid
            lblCategory.Text = dt.Rows(0).Item("category_name")
            lblSubCategory.Text = dt.Rows(0).Item("cat_name")
            'txtRuleCondition.Text = oGen.CheckNullString(dt.Rows(0).Item("rul_sql"))
            lblDataSource.Text = oGen.CheckNullString(dt.Rows(0).Item("data_name"))
        End If

    End Sub

    Sub bindModel(ByVal moid)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oAsset.getDetailOfModel(moid)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            txtModelName.Text = dt.Rows(0).Item("mod_name")
            txtDescription.Text = dt.Rows(0).Item("mod_desc")
            ddlApproach.SelectedValue = dt.Rows(0).Item("mod_approach")
            ViewState("mod_name") = dt.Rows(0).Item("mod_name")
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oModel As New AAS.Business.Entity.Model
        CekInput()
        If liBodyLoadAction.Text = "" Then
            If Not ViewState("astid") = "" Then
                oModel.name = txtModelName.Text
                oModel.description = txtDescription.Text
                oModel.approach = ddlApproach.SelectedValue
                If String.IsNullOrEmpty(Request.QueryString("moid")) Or Request.QueryString("moid") = 0 Then
                    Try
                        oAsset.insertModelAsset(oModel, usrLogin, "IN", 0, ViewState("astid"))
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model Creation - MODELNAME=" & txtModelName.Text)
                        objLog.insertLog("asset_model", logs)
                        liBodyLoadAction.Text = "alert('Model definition saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        Dim err As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert('" & err & "');window.opener.location.href = window.opener.location.href;"
                    End Try

                Else
                    Dim moid As String = Request.QueryString("moid")
                    Try
                        oAsset.insertModelAsset(oModel, usrLogin, "UO", moid, ViewState("astid"))
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model Updated - MODELNAME=" & txtModelName.Text)
                        objLog.insertLog("asset_model", logs)
                        liBodyLoadAction.Text = "alert('Model definition updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        Dim err As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert('" & err & "');window.opener.location.href = window.opener.location.href;"
                    End Try
                End If
            Else
                liBodyLoadAction.Text = "alert('No parameter id. Please reload.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            End If
        End If
    End Sub

    Sub CekInput()
        Dim lMsg As String = ""
        If txtModelName.Text = "" Then
            lMsg &= "Please input model name. \n"
        Else
            Dim dt As DataTable
            dt = oAsset.checkModelName(txtModelName.Text)
            If dt.Rows(0).Item(0) > 0 And ViewState("mod_name") <> txtModelName.Text Then
                lMsg &= "Model name already exists. \n"
            End If
        End If
        If ddlApproach.SelectedValue = 0 Then
            lMsg &= "Please choose approach. \n"
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class