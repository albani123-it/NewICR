Public Class CCFPoolDet
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim objLog As New lLog
    Dim oGen As New lGeneral
    Dim oRule As New lRule

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not String.IsNullOrEmpty(Request.QueryString("astid")) Then
            ViewState("astid") = Request.QueryString("astid")
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("moid")) Then
            ViewState("moid") = Request.QueryString("moid")
        End If

        If ViewState("moid") > 0 And Not IsPostBack Then
            isiDataPoolingRating(ViewState("moid"))
        End If
        If ViewState("astid") > 0 And Not IsPostBack Then
            isiDataRule()
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("polid")) Then
            ViewState("polid") = Request.QueryString("polid")
            If Not IsPostBack Then
                isiData(ViewState("polid"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_add_pool" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_adv_edit_pool" Then
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

    Sub isiDataRule()
        Dim ds As DataSet
        ds = oRule.getPoolRule(ViewState("astid"))
        ddlRule.DataSource = ds
        ddlRule.DataTextField = "rul_name"
        ddlRule.DataValueField = "rul_id"
        ddlRule.DataBind()
    End Sub

    Sub isiDataPoolingRating(ByVal moid)
        ddlRating.Items.Clear()
        ddlRating.Items.Add(New ListItem("Select One", "Select One"))
        ddlRating.Items.Add(New ListItem("RR01", "RR01"))
        ddlRating.Items.Add(New ListItem("RR02", "RR02"))
        ddlRating.Items.Add(New ListItem("RR03", "RR03"))
        ddlRating.Items.Add(New ListItem("RR04", "RR04"))
        ddlRating.Items.Add(New ListItem("RR05", "RR05"))
        ddlRating.Items.Add(New ListItem("RR06", "RR06"))
        ddlRating.Items.Add(New ListItem("RR07", "RR07"))
        ddlRating.Items.Add(New ListItem("RR08", "RR08"))
        ddlRating.Items.Add(New ListItem("RR09", "RR09"))
        ddlRating.Items.Add(New ListItem("RR10", "RR010"))
        ddlRating.Items.Add(New ListItem("RR11", "RR011"))
        ddlRating.Items.Add(New ListItem("RR12", "RR012"))
        ddlRating.Items.Add(New ListItem("RR13", "RR013"))
        ddlRating.Items.Add(New ListItem("RR14", "RR014"))
        ddlRating.Items.Add(New ListItem("RR15", "RR015"))
        ddlRating.Items.Add(New ListItem("RR16", "RR016"))
        ddlRating.Items.Add(New ListItem("RR17", "RR017"))
        ddlRating.Items.Add(New ListItem("RR18", "RR018"))
        ddlRating.Items.Add(New ListItem("RR19", "RR019"))
        ddlRating.Items.Add(New ListItem("RR20", "RR020"))

        'Dim ds As DataSet
        'ds = oRule.getPoolRating(moid, "PD")
        'ddlRating.DataSource = ds
        'ddlRating.DataTextField = "rate"
        'ddlRating.DataValueField = "rate"
        'ddlRating.DataBind()
    End Sub

    Sub isiData(ByVal polid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfPoolEAD(polid, "EAD")
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            txtPoolName.Text = dt.Rows(0).Item("pol_name")
            txtPoolName.CssClass = "inpTxtDisabled"
            txtPoolName.ReadOnly = True
            ddlRating.Items.Add(New ListItem(dt.Rows(0).Item("pol_rule_rate"), dt.Rows(0).Item("pol_rule_rate")))
            ddlRating.SelectedValue = dt.Rows(0).Item("pol_rule_rate")
            ddlRule.SelectedValue = dt.Rows(0).Item("pol_rule")
            txtCCFValue.Text = dt.Rows(0).Item("pol_pd")
            txtDistributionBaseline.Text = dt.Rows(0).Item("pol_pd_baseline")
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInputModel()
        Dim oModel As New AAS.Business.Entity.FoundationPooling
        If liBodyLoadAction.Text = "" Then
            oModel.name = txtPoolName.Text
            oModel.rule = ddlRule.SelectedValue
            oModel.rating = ddlRating.SelectedValue
            oModel.value = txtCCFValue.Text
            oModel.baseline = txtDistributionBaseline.Text

            If ViewState("polid") IsNot Nothing Then
                Dim ds As DataSet
                Dim dt As DataTable
                ds = oAsset.getDetailOfPoolEAD(ViewState("polid"), "EAD")
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    Try
                        oAsset.updatePoolAsset(oModel, usrLogin, "UPDATE", ViewState("moid"), "EAD", ViewState("polid"))
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "LGD Pooling Updated - NAME=" & txtPoolName.Text)
                        objLog.insertLog("Pool", logs)
                        liBodyLoadAction.Text = "alert('LGD Pool Definition Updated');reloadParentWindow();parent.window.close();"
                    Catch ex As Exception
                        Dim msg As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert('" & msg & "');"
                    End Try
                Else
                    Try
                        oAsset.insertPoolAsset(oModel, usrLogin, "NEW", ViewState("moid"), "EAD")
                        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "LGD Pooling Creation - NAME=" & txtPoolName.Text)
                        objLog.insertLog("Pool", logs)
                        liBodyLoadAction.Text = "alert('LGD Pool Definition Saved');reloadParentWindow();parent.window.close();"
                    Catch ex As Exception
                        Dim msg As String = ex.Message.Replace("'", "\'")
                        liBodyLoadAction.Text = "alert('" & msg & "');"
                    End Try
                End If
            End If
        End If

    End Sub

    Sub cekInputModel()
        Dim lMsg As String = ""
        If txtPoolName.Text = "" Then
            lMsg &= "Please input pooling name.\n"
        End If
        If ddlRule.SelectedValue = "Select One" Then
            lMsg &= "Please select rule.\n"
        End If
        If ddlRating.SelectedValue = "Select One" Then
            lMsg &= "Please select rating.\n"
        End If
        If txtCCFValue.Text = "" Then
            lMsg &= "Please input PD value.\n"
        End If
        'If txtDistributionBaseline.Text = "" Then
        '    lMsg &= "Please input distribution baseline.\n"
        'End If

        If ViewState("polid") = -1 Then
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oAsset.getDetailRuleModel(ViewState("moid"), "pol_ead")
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("pol_name") = txtPoolName.Text Then
                        lMsg &= "Pool name already exists.\n"
                    End If
                    If dt.Rows(i).Item("pol_rule_rate") = ddlRating.SelectedValue Then
                        lMsg &= "Rating already exists.\n"
                    End If
                Next
            End If
            If lMsg <> "" Then
                liBodyLoadAction.Text = "alert('" & lMsg & "');"
            End If
        End If

    End Sub
End Class