Public Class standardizedDet
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim objLog As New lLog
    Dim oGen As New lGeneral

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Not IsPostBack Then
            isiDataExposure()
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("astid")) And Not IsPostBack Then
            ViewState("astid") = Request.QueryString("astid")
            isiDataAsset(ViewState("astid"))
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("moid")) And Not IsPostBack Then
            ViewState("moid") = Request.QueryString("moid")
            isiDataModel(ViewState("moid"))
        End If

    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_std_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_std_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_std_view" Then
                    If Not edit = True Then
                        txtCCF.Enabled = False
                        ddlExposure.Enabled = False
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

    Sub isiDataExposure()
        Dim ds As DataSet
        ds = oAsset.getExposureClass()
        ddlExposure.DataSource = ds
        ddlExposure.DataTextField = "std_field"
        ddlExposure.DataValueField = "std_id"
        ddlExposure.DataBind()
    End Sub

    Sub isiDataAsset(ByVal astid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfAsset(astid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblAssetCat.Text = dt.Rows(0).Item("category_name")
            lblAssetSubCat.Text = dt.Rows(0).Item("cat_name")
            lblDataSource.Text = oGen.CheckNullString(dt.Rows(0).Item("data_name"))
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
            txtCCF.Text = dt.Rows(0).Item("mod_ead")
            If IIf(oGen.CheckNullString(dt.Rows(0).Item("ams_exposure_class")) = "", 0, oGen.CheckNullString(dt.Rows(0).Item("ams_exposure_class"))) > 0 Then
                ddlExposure.SelectedValue = dt.Rows(0).Item("ams_exposure_class")
            End If
            If dt.Rows(0).Item("mod_status") = 1 Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            Try
                Dim stat As String
                If Request.Form("chkStatus") = "on" Then
                    stat = "Active"
                Else
                    stat = "Inactive"
                End If
                oAsset.updateModelAssetStandardize(txtCCF.Text, ddlExposure.SelectedValue, Request.Form("chkStatus"), usrLogin, ViewState("moid"))
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model Updated - Name=" & lblModelName.Text & ";STATUS=" & stat)
                objLog.insertLog("asset_model", logs)
                liBodyLoadAction.Text = "alert('Asset Model Definition Saved');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                Dim err As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & err & "');"
            End Try
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If txtCCF.Text = "" Then
            lMsg &= "Please input Credit Conversion Factor (CCF).\n"
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class