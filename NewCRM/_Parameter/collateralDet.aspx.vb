Public Class collateralDet
    Inherits System.Web.UI.Page
    Dim oColl As New lCollateral
    Dim objLog As New lLog
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
        Dim collid As String
        If Not IsPostBack Then
            fillCollClass()
            fillCollSource()
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("collid")) Then
            collid = Request.QueryString("collid")
            If collid <> -1 And Not IsPostBack Then
                isiData(collid)
            End If
        End If
        
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_view" Then
                    If edit = False Then
                        txtCollType.Enabled = False
                        ddlCollClass.Enabled = False
                        ddlCollSource.Enabled = False
                        txtCollDesc.Enabled = False
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

    Sub fillCollClass()
        Dim ds As DataSet
        ds = oColl.getCollateralClass

        ddlCollClass.DataSource = ds
        ddlCollClass.DataValueField = "backend_code"
        ddlCollClass.DataTextField = "collateral_class"
        ddlCollClass.DataBind()
    End Sub

    Sub fillCollSource()
        Dim ds As DataSet
        ds = oColl.getCollateralSource

        ddlCollSource.DataSource = ds
        ddlCollSource.DataValueField = "data_name"
        ddlCollSource.DataTextField = "data_name"
        ddlCollSource.DataBind()
    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oColl.getDetailCollateral(id)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            collateral_id.Value = dt.Rows(0).Item("coll_id")
            txtCollType.Text = dt.Rows(0).Item("coll_type")
            ddlCollClass.SelectedValue = dt.Rows(0).Item("coll_class")
            txtCollDesc.Text = dt.Rows(0).Item("coll_desc")
            ddlCollSource.SelectedValue = dt.Rows(0).Item("coll_source")
            If dt.Rows(0).Item("coll_is_active") = True Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim stat As String
        If liBodyLoadAction.Text = "" Then
            If Request.QueryString("collid") <> -1 Then
                Try
                    If chkStatus.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If
                    oColl.editCollateral(Request, usrLogin, collateral_id.Value)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Collateral Classification Updated - TYPE=" + Request.Form("txtCollType") + ";CLASS=" + Request.Form("ddlCollClass") + ";DATASOURCE=" + Request.Form("ddlCollSource") + ";STATUS=" + stat)
                    objLog.insertLog("collateral", logs)
                    liBodyLoadAction.Text = "alert('Collateral Classfication updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try

                    If chkStatus.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If

                    oColl.insertCollateral(Request, usrLogin)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Collateral Classification Saved - TYPE=" + Request.Form("txtCollType") + ";CLASS=" + Request.Form("ddlCollClass") + ";DATASOURCE=" + Request.Form("ddlCollSource") + ";STATUS=" + stat)
                    objLog.insertLog("collateral", logs)
                    liBodyLoadAction.Text = "alert('Collateral Classfication saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If txtCollType.Text = "" Then
            lMsg &= "Please input collateral type. \n"
        End If
        If ddlCollClass.SelectedIndex = 0 Then
            lMsg &= "Please choose collateral class. \n"
        End If
        If ddlCollSource.SelectedIndex = 0 Then
            lMsg &= "Please choose data source. \n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class