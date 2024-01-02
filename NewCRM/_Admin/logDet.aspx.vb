Imports System.Drawing

Public Class logDet
    Inherits System.Web.UI.Page
    Public oLog As New lLog
    Protected levelCon As Boolean = False

#Region " Global Variable "
    Dim gStrFormName = "document.Form1."
    Dim usrLogin As String = ""
    Dim oGen As New lGeneral
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        detAsset.Visible = False
        detAssetmodel.Visible = False
        detBorrower_Mgmt.Visible = False
        detCollateral_Class.Visible = False
        detData_Mgmt.Visible = False
        detDataJoinMgmt.Visible = False
        detProduct_Mgmt.Visible = False
        detPooling.Visible = False
        detSystem.Visible = False
        detUsers.Visible = False
        detRatingModelRating.Visible = False
        detLevel.Visible = False
        detLogin.Visible = False
        detRule.Visible = False
        detGloBankCapital.Visible = False
        detGloEligible.Visible = False
        detGloPreScreening.Visible = False

        detScorecards.Visible = False
        detScorecardChar.Visible = False
        Dim id As String = Request.QueryString("id")
        Dim code As String = Request.QueryString("code")
        cekUserAuthentication(code)
        getLogDetail(id, code)
    End Sub

    Sub cekUserAuthentication(ByVal code)
        Dim dtUser As DataTable = Session("dtUser")
        Dim permit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log" Then
                    permit = True
                End If
            Next
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
        If permit = False Then
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End If
    End Sub

    'Public Function getLogDetailVerificationStatic(ByVal id, ByVal code)
    '    Dim dt As DataTable
    '    Dim parm1 As String
    '    Dim parm2 As String
    '    dt = oLog.getLogDetail(id, "verification_static")
    '    For i As Integer = 0 To dt.Rows.Count() - 1
    '        parm1 = IIf(dt.Rows(i).Item("flag") = "new", "ver_static_a", "ver_static")
    '        parm2 = IIf(dt.Rows(i).Item("flag") = "new", "ver_static", "ver_static_a")


    '        Dim listItem As New ListItem(oGen.CheckNullString(dt.Rows(i).Item(parm1)))
    '        If Not oGen.CheckNullString(dt.Rows(i).Item(parm1)) = oGen.CheckNullString(dt.Rows(i).Item(parm2)) Then
    '            listItem.Attributes.Add("style", "color:red;")
    '        End If
    '        ver_static.Items.Add(listItem)

    '        Dim listItem_a As New ListItem(oGen.CheckNullString(dt.Rows(i).Item(parm2)))
    '        If Not oGen.CheckNullString(dt.Rows(i).Item(parm1)) = oGen.CheckNullString(dt.Rows(i).Item(parm2)) Then
    '            listItem_a.Attributes.Add("style", "color:red;")
    '        End If
    '        ver_static_a.Items.Add(listItem_a)
    '    Next
    'End Function

    'Public Function getLogDetailVerificationDynamic(ByVal id, ByVal code)
    '    Dim dt As DataTable
    '    Dim parm1 As String
    '    Dim parm2 As String
    '    dt = oLog.getLogDetail(id, "verification_dynamic")
    '    For i As Integer = 0 To dt.Rows.Count() - 1
    '        parm1 = IIf(dt.Rows(i).Item("flag") = "new", "ver_dynamic_a", "ver_dynamic")
    '        parm2 = IIf(dt.Rows(i).Item("flag") = "new", "ver_dynamic", "ver_dynamic_a")


    '        Dim listItem As New ListItem(oGen.CheckNullString(dt.Rows(i).Item(parm1)))
    '        If Not oGen.CheckNullString(dt.Rows(i).Item(parm1)) = oGen.CheckNullString(dt.Rows(i).Item(parm2)) Then
    '            listItem.Attributes.Add("style", "color:red;")
    '        End If
    '        ver_dynamic.Items.Add(listItem)

    '        Dim listItem_a As New ListItem(oGen.CheckNullString(dt.Rows(i).Item(parm2)))
    '        If Not oGen.CheckNullString(dt.Rows(i).Item(parm1)) = oGen.CheckNullString(dt.Rows(i).Item(parm2)) Then
    '            listItem_a.Attributes.Add("style", "color:red;")
    '        End If
    '        ver_dynamic_a.Items.Add(listItem_a)
    '    Next
    'End Function

    Public Function getLogDetail(ByVal id, ByVal code)

        If code = "system" Then
            detSystem.Visible = True
        ElseIf code = "ua" Then
            detLogin.Visible = True
        ElseIf code = "users" Then
            detUsers.Visible = True
        ElseIf code = "level" Then
            detLevel.Visible = True
            levelCon = True
        ElseIf code = "scorecard" Then
            detScorecards.Visible = True
        ElseIf code = "scorecard_char" Then
            detScorecardChar.Visible = True
        ElseIf code = "scorecard_rating" Then
            detRatingModelRating.Visible = True
        ElseIf code = "asset" Then
            detAsset.Visible = True
        ElseIf code = "asset_model" Then
            detAssetmodel.Visible = True
        ElseIf code = "borrowerMgmt" Then
            detBorrower_Mgmt.Visible = True
        ElseIf code = "collateral" Then
            detCollateral_Class.Visible = True
        ElseIf code = "dataMgmt" Then
            detData_Mgmt.Visible = True
        ElseIf code = "dataMgmtJoin" Then
            detDataJoinMgmt.Visible = True
        ElseIf code = "productMgmt" Then
            detProduct_Mgmt.Visible = True
        ElseIf code = "Pool" Then
            detPooling.Visible = True
        ElseIf code = "ruleMgmt" Then
            detRule.Visible = True
        ElseIf code = "glo_bank_capital" Then
            detGloBankCapital.Visible = True
        ElseIf code = "glo_irb_col" Then
            detGloEligible.Visible = True
        ElseIf code = "glo_pre_screening" Then
            detGloPreScreening.Visible = True
        End If

        Try
            Dim dt As DataTable
            Dim dr As DataRow
            Dim dra As DataRow
            dt = oLog.getLogDetail(id, code)
            dr = dt.Rows(0)

            If dt.Rows.Count = 2 Then
                dra = dt.Rows(1)
                Dim lbl_before As Label
                Dim lbl_after As Label
                Dim bef As String
                Dim aft As String
                Dim i As Integer = 0
                'Response.Write(dr.Table.Columns.Count)
                For Each dc As DataColumn In dr.Table.Columns
                    'Response.Write(dr.Table.Columns(i))
                    'i += 1
                    'If Not dc.ColumnName = "lvl_act_tasklist_assign" And Not dc.ColumnName = "lvl_act_leads_assign" Then
                    If Not dc.ColumnName = "lvl_act_tasklist_assign" Then
                        'i = i + 1
                        'Response.Write(dc.ColumnName)
                        lbl_before = FindControl(dc.ColumnName)
                        lbl_after = FindControl(dc.ColumnName & "_a")
                        bef = oGen.CheckNullString(dr.Item(dc.ColumnName))
                        aft = oGen.CheckNullString(dra.Item(dc.ColumnName))
                        If bef = "True" Then
                            bef = "T"
                        End If
                        If aft = "True" Then
                            aft = "T"
                        End If
                        If bef = "False" Then
                            bef = "F"
                        End If
                        If aft = "False" Then
                            aft = "F"
                        End If
                        'Response.Write(bef & " <br />- ")

                        Try

                            If Not bef = aft Then
                                lbl_before.Text = oGen.CheckNullString(bef)
                                lbl_before.ForeColor = Color.Red
                                lbl_after.Text = oGen.CheckNullString(aft)
                                lbl_after.ForeColor = Color.Red
                            Else
                                lbl_before.Text = oGen.CheckNullString(bef)
                                lbl_after.Text = oGen.CheckNullString(aft)
                            End If

                        Catch ex As Exception
                            'Response.Write(bef)
                            'Response.Write(aft)
                        End Try
                    End If
                    'Response.Write(i)
                Next
            ElseIf dr.Item("log_action_mode") = "New" Then
                dra = dt.Rows(0)
                Dim lbl_after As Label
                Dim aft As String
                For Each dc As DataColumn In dr.Table.Columns
                    If Not dc.ColumnName = "lvl_act_tasklist_assign" And Not dc.ColumnName = "lvl_act_leads_assign" Then
                        lbl_after = FindControl(dc.ColumnName & "_a")
                        aft = oGen.CheckNullString(dra.Item(dc.ColumnName))
                        If aft = "True" Then
                            aft = "T"
                        End If
                        If aft = "False" Then
                            aft = "F"
                        End If
                        Try
                            lbl_after.Text = oGen.CheckNullString(aft)
                            lbl_after.ForeColor = Color.Red
                        Catch ex As Exception

                        End Try
                    End If
                Next
            ElseIf dr.Item("log_action_mode") = "Delete" Then
                Dim lbl_before As Label
                Dim bef As String
                For Each dc As DataColumn In dr.Table.Columns
                    If Not dc.ColumnName = "lvl_act_tasklist_assign" And Not dc.ColumnName = "lvl_act_leads_assign" Then
                        lbl_before = FindControl(dc.ColumnName)
                        bef = dr.Item(dc.ColumnName)
                        If bef = "True" Then
                            bef = "T"
                        End If
                        If bef = "False" Then
                            bef = "F"
                        End If
                        Try
                            lbl_before.Text = oGen.CheckNullString(bef)
                            lbl_before.ForeColor = Color.Red
                        Catch ex As Exception

                        End Try
                    End If
                Next
            ElseIf dr.Item("log_action_mode") = "Login" Or dr.Item("log_action_mode") = "LoginF" Or dr.Item("log_action_mode") = "Logout" Then
                Dim lbl_before As Label
                Dim bef As String
                For Each dc As DataColumn In dr.Table.Columns
                    lbl_before = FindControl(dc.ColumnName & "_l")
                    bef = dr.Item(dc.ColumnName)
                    Try
                        lbl_before.Text = oGen.CheckNullString(bef)
                        lbl_before.ForeColor = Color.Red
                    Catch ex As Exception

                    End Try
                Next
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function fillLevelDetBefore()
        Dim html As String = ""
        Dim Id As String = Request.QueryString("id")
        Dim dtbf As DataTable

        'Dim max As Int32

        dtbf = oLog.getLogDetailLevel(Id, "Before")

        'max = Math.Max(dtbf.Rows.Count, dtaf.Rows.Count)

        If dtbf.Rows.Count > 0 Then
            For i As Integer = 0 To dtbf.Rows.Count - 1
                html += "<tr>"
                html += "<td>"
                html += dtbf.Rows(i).Item("mnu_Name")
                html += "</td>"
                html += "</tr>"
            Next
        End If

        Return html
    End Function

    Public Function fillLevelDetAfter()
        Dim html As String = ""
        Dim Id As String = Request.QueryString("id")
        Dim dtaf As DataTable

        dtaf = oLog.getLogDetailLevel(Id, "After")
        If dtaf.Rows.Count > 0 Then
            For i As Integer = 0 To dtaf.Rows.Count - 1
                html += "<tr>"
                html += "<td>"
                html += dtaf.Rows(i).Item("mnu_Name")
                html += "</td>"
                html += "</tr>"
            Next
        End If

        Return html
    End Function
End Class