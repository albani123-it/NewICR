Public Class assetDet
    Inherits System.Web.UI.Page
    Dim oRule As New lRule
    Dim oAsset As New lAsset
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        Dim astid As String
        If Not IsPostBack Then
            fillDdlCategoryGroup()
            fillDdlDataSource()
            If Not String.IsNullOrEmpty(Request.QueryString("astid")) Then
                astid = Request.QueryString("astid")
                isiData(astid)
            Else
                chkParentCat.Checked = True
                ruleCondition.Attributes.Add("style", "display:none")
                ddlCategoryGroup.Enabled = False
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        btnSave.Enabled = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_edit" Then
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

    Sub fillDdlCategoryGroup()
        Dim ds As DataSet
        ds = oAsset.getCategoryAsset
        ddlCategoryGroup.DataSource = ds
        ddlCategoryGroup.DataValueField = "id"
        ddlCategoryGroup.DataTextField = "cat_name"
        ddlCategoryGroup.DataBind()
    End Sub

    Sub fillDdlDataSource()
        Dim ds As DataSet
        ds = oAsset.getAssetDataSource("A")

        ddlDataSource.DataSource = ds
        ddlDataSource.DataValueField = "data_id"
        ddlDataSource.DataTextField = "data_name"
        ddlDataSource.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfAsset(id)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("cat_parent") > 0 Then
                ddlCategoryGroup.SelectedValue = dt.Rows(0).Item("cat_parent")
            ElseIf dt.Rows(0).Item("cat_parent") = 0 Then
                ddlCategoryGroup.SelectedValue = dt.Rows(0).Item("id")
            End If

            categoryName.Text = dt.Rows(0).Item("cat_name")
            ViewState("cat_name") = dt.Rows(0).Item("cat_name")
            txtDescription.Text = dt.Rows(0).Item("cat_desc")

            If Not IsDBNull(dt.Rows(0).Item("cat_data")) Then
                If Not dt.Rows(0).Item("cat_data") = "" Then
                    ddlDataSource.SelectedValue = dt.Rows(0).Item("cat_data")
                End If
            End If
            If dt.Rows(0).Item("cat_parent") = 0 Then
                chkParentCat.Checked = True
                chkParentCat.Disabled = True
                ddlCategoryGroup.Enabled = False
                ruleCondition.Attributes.Add("style", "display:none")
            End If
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim obAset As New AAS.Business.Entity.Asset
        obAset.name = categoryName.Text
        obAset.description = txtDescription.Text

        If String.IsNullOrEmpty(Request.QueryString("astid")) Then

            If chkParentCat.Checked = True Then
                cekInputParent()
                If liBodyLoadAction.Text = "" Then
                    Try
                        obAset.dataSource = ""
                        oAsset.insertAsset(obAset, usrLogin, "IN", 0, 1, 0)
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Creation - CATEGORY=" & categoryName.Text)
                        objLog.insertLog("asset", logs)
                        liBodyLoadAction.Text = "alert('Category Definition saved');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        Dim err As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert(" & err & ");"
                    End Try
                End If
            Else
                cekInputSub()
                If liBodyLoadAction.Text = "" Then
                    Try
                        obAset.dataSource = ddlDataSource.SelectedValue
                        oAsset.insertAsset(obAset, usrLogin, "IN", 0, 2, ddlCategoryGroup.SelectedValue)
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Creation - CATEGORY=" & categoryName.Text)
                        objLog.insertLog("asset", logs)
                        liBodyLoadAction.Text = "alert('Category Definition saved');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        Dim err As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert(" & err & ");"
                    End Try
                End If

            End If
        Else
            Dim astid As String
            astid = Request.QueryString("astid")
            If chkParentCat.Checked = True Then
                cekInputParent()
                If liBodyLoadAction.Text = "" Then
                    Try
                        obAset.dataSource = ""
                        oAsset.insertAsset(obAset, usrLogin, "UO", astid, 1, 0)
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Updated - CATEGORY=" & categoryName.Text)
                        objLog.insertLog("asset", logs)
                        liBodyLoadAction.Text = "alert('Category Definition updated');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        Dim err As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert(" & err & ");"
                    End Try
                End If
            Else
                cekInputSub()
                If liBodyLoadAction.Text = "" Then
                    Try
                        obAset.dataSource = ddlDataSource.SelectedValue
                        oAsset.insertAsset(obAset, usrLogin, "UO", astid, 2, ddlCategoryGroup.SelectedValue)
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Updated - CATEGORY=" & categoryName.Text)
                        objLog.insertLog("asset", logs)
                        liBodyLoadAction.Text = "alert('Category Definition updated');window.opener.location.href = window.opener.location.href;parent.window.close();"
                    Catch ex As Exception
                        Dim err As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert(" & err & ");"
                    End Try
                End If
            End If
        End If

    End Sub

    Sub cekInputParent()
        Dim lMsg As String = ""

        If categoryName.Text = "" Then
            lMsg &= "Please input category name. \n"
        Else
            Dim dt As DataTable
            dt = oAsset.checkCategoryName(categoryName.Text)
            If dt.Rows(0).Item(0) > 0 And ViewState("cat_name") <> categoryName.Text Then
                lMsg &= "Category Name already exist. \n"
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Sub cekInputSub()
        Dim lMsg As String = ""
        If ddlCategoryGroup.SelectedValue = 0 Then
            lMsg &= "Please choose category group. \n"
        End If
        If categoryName.Text = "" Then
            lMsg &= "Please input category name. \n"
        Else
            Dim dt As DataTable
            dt = oAsset.checkCategoryName(categoryName.Text)
            If dt.Rows(0).Item(0) > 0 And ViewState("cat_name") <> categoryName.Text Then
                lMsg &= "Category Name already exist. \n"
            End If
            If chkParentCat.Checked = False Then
                ruleCondition.Style.Add("display", "")
            End If
        End If
        If ddlDataSource.SelectedValue = 0 Then
            lMsg &= "Please choose data source. \n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class