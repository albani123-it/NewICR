Imports System
Imports System.Net
Imports System.Console
Imports System.IO
Imports System.IO.File
Imports System.Web.HttpRequest

Public Class ruleDet
    Inherits System.Web.UI.Page
    Dim oRule As New lRule
    Dim oGen As New lGeneral
    Dim oAsset As New lAsset
    Dim oSc As New lScorecards
    Dim objLog As New lLog
    Protected ddlparam As String
    Protected scidGo
    Protected ptr
    Protected tipeBrowser As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim gStrFormName = "document.Form1."
    Public Shared ipadd = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName).AddressList(0).ToString

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        tipeBrowser = Request.Browser.Browser
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            FillDdlData()
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("scid")) Then
            ViewState("scid") = Request.QueryString("scid")
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("data")) Then
            If Not IsPostBack Then
                ddlRuleData.SelectedValue = Request.QueryString("data")
            End If
        End If
        
        scidGo = Request.QueryString("scid")
        ptr = Request.QueryString("valPtr")

        ddlparam = FillCombobox(ddlRuleData.SelectedItem.Text)

        If ViewState("scid") <> "" And Not IsPostBack Then
            getRuleData()
            ddlparam = FillCombobox(ddlRuleData.SelectedItem.Text)
        End If

        If ViewState("scid") <> "" And Not IsPostBack Then
            FillListField(ddlRuleData.SelectedItem.Text)
        End If

        If ViewState("scid") = "" Then
            File.Delete(Server.MapPath("text\") & ipadd & "temp.txt")
        Else
            File.Delete(Server.MapPath("text\") & ViewState("scid") & "temp.txt")

            If IO.File.Exists(Server.MapPath("text\") & ipadd & "_textrule.txt") Then
                File.Delete(Server.MapPath("text\") & ipadd & "_textrule.txt")
            End If
            If IO.File.Exists(Server.MapPath("text\") & ipadd & "temp.txt") Then
                File.Delete(Server.MapPath("text\") & ipadd & "temp.txt")
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_rule_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_rule_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_rule_edit" Then
                    edit = True
                End If
                
            Next
            If edit = True Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
            If Not tf Then
                Session.Abandon()
                Response.Redirect("../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub FillDdlData()
        Dim ds As DataSet
        ds = oAsset.getAssetDataSource("")
        ddlRuleData.DataSource = ds
        ddlRuleData.DataTextField = "data_name"
        ddlRuleData.DataValueField = "data_id"
        ddlRuleData.DataBind()
    End Sub

    Function FillCombobox(ByVal tbl)
        Dim htmlsource = ""
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oRule.getListofPageRule(tbl)
        dt = ds.Tables(0)
        lstField.DataTextField = "name"
        lstField.DataValueField = "value"

        'With New lScorecard
        lstField.DataSource = ds
        lstField.DataBind()
        'End With
        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Rows.Count
                If IsDBNull(dt.Rows(i - 1).Item("name")) Then
                    htmlsource = htmlsource & "<option value='" & dt.Rows(i - 1).Item("val") & "'>" & dt.Rows(i - 1).Item("val").ToString.Substring(0, dt.Rows(i - 1).Item("val").ToString.IndexOf("|")) & "</option>"
                Else
                    htmlsource = htmlsource & "<option value='" & dt.Rows(i - 1).Item("val") & "'>" & dt.Rows(i - 1).Item("name") & "</option>"
                End If

            Next
        Else
            htmlsource = "<option value='Select One'>Select One</option>"
        End If
        Return htmlsource
    End Function

    Sub FillListField(ByVal tbl)
        lstField.DataTextField = "name"
        lstField.DataValueField = "value"


        lstField.DataSource = oRule.getListofPageRule(tbl)
        lstField.DataBind()

    End Sub

    Sub getRuleData()
        Dim ds As New DataSet
        Dim dt As DataTable
        ds = oRule.getDetailRule(ViewState("scid"))
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            ddlRuleData.SelectedValue = dt.Rows(0).Item("rul_data")
            ruleId.Value = dt.Rows(0).Item("rul_id")
            txtRuleName.Text = dt.Rows(0).Item("rul_name")
            ViewState("VS_rul_name") = dt.Rows(0).Item("rul_name")
            txtDescription.Text = oGen.CheckNullString(dt.Rows(0).Item("rul_desc"))
            rlSQL.Text = dt.Rows(0).Item("rul_sql")

        End If

    End Sub

    Sub writeFile(ByVal zzz, ByVal isi, ByVal isi2)
        If File.Exists(Server.MapPath("text\") & zzz & ".txt") Then
            File.Delete(Server.MapPath("text\") & zzz & ".txt")
        End If
        Dim objWriter As New IO.StreamWriter(File.Open(Server.MapPath("text\") & zzz & ".txt", IO.FileMode.OpenOrCreate))
        objWriter.Write(isi)
        objWriter.Flush()
        objWriter.Close()
        objWriter = Nothing
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRule(ByVal txt)
        Dim html = ""
        Dim ds As New DataSet
        Dim dt As New DataTable
        Try
            With New lRule
                ds = .getListofPageRule(txt)
                dt = ds.Tables(0)
                ' += dt.Rows.Count.ToString
                If dt.Rows.Count > 0 Then
                    For i As Integer = 1 To dt.Rows.Count
                        html &= "<option value='" & dt.Rows(i - 1).Item("val") & "'>" & dt.Rows(i - 1).Item("name") & "</option>"
                    Next
                Else
                    html = txt.ToString
                End If
            End With
        Catch ex As Exception
            html += ex.ToString
        End Try
        Return html
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Sub writeFileTextRule(ByVal isi)
        Dim zzz As String = ""
        zzz = ipadd & "_textrule"
        If File.Exists(System.Web.HttpContext.Current.Server.MapPath("text\") & zzz & ".txt") Then
            File.Delete(System.Web.HttpContext.Current.Server.MapPath("text\") & zzz & ".txt")
        End If
        Dim objWriter As New IO.StreamWriter(File.Open(System.Web.HttpContext.Current.Server.MapPath("text\") & zzz & ".txt", IO.FileMode.OpenOrCreate))
        objWriter.Write(isi)
        objWriter.Flush()
        objWriter.Close()
        objWriter = Nothing
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim obRule As New AAS.Business.Entity.Rule
        Dim tbl As String = "dm_" & ddlRuleData.SelectedItem.Text & "_check"
        CekInput(tbl)
        If liBodyLoadAction.Text = "" Then
            obRule.name = txtRuleName.Text
            obRule.description = txtDescription.Text
            obRule.sql = rlSQL.Text

            Dim dt As DataTable
            Dim id
            If ruleId.Value = "" Then
                Try
                    oRule.insertRule(obRule, 0, "", ddlRuleData.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Rule Management Creation - RULENAME=" & txtRuleName.Text)
                    objLog.insertLog("ruleMgmt", logs)
                    dt = oSc.getID()

                    id = dt.Rows(0).Item(0)

                    writeFile(id, valSave.Value, valSave2.Value)
                    File.Delete(Server.MapPath("text\") & ViewState("scid") & "temp.txt")
                    liBodyLoadAction.Text = "alert('Rule saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim err As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & err & "');", True)
                    'liBodyLoadAction.Text = "alert('" & err & "');"
                End Try

            Else
                Try
                    oRule.insertRule(obRule, ruleId.Value, "", ddlRuleData.SelectedValue)
                    Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Rule Management Updated - RULENAME=" & txtRuleName.Text)
                    objLog.insertLog("ruleMgmt", logs)
                    writeFile(ruleId.Value, valSave.Value, valSave2.Value)
                    liBodyLoadAction.Text = "alert('Rule updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
                Catch ex As Exception
                    Dim err As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & err & "');", True)
                    'liBodyLoadAction.Text = "alert('" & err & "');"
                End Try
            End If
        End If

    End Sub

    Sub CekInput(ByVal tbl)
        Dim lMsg As String = ""
        If ViewState("scid") = "" Or ViewState("scid") = -1 Then
            writeFile(ipadd & "temp", valSave.Value, valSave2.Value) 'tulis ke file untuk sementara
        Else
            writeFile(ViewState("scid") & "temp", valSave.Value, valSave2.Value) 'tulis ke file untuk sementara
        End If

        If txtRuleName.Text = "" Then
            lMsg = "Please enter rule name. \n"
        Else

            Dim dt As DataTable
            dt = oRule.getRuleName(txtRuleName.Text)

            If dt.Rows(0).Item(0) > 0 And ViewState("VS_rul_name") <> txtRuleName.Text Then
                lMsg = "Rule name already exist. \n"
            End If
        End If

        If rlSQL.Text = "" Then
            lMsg = lMsg & "Please enter a SQL Script.\n"

        ElseIf InStr(rlSQL.Text, "!= null") <> 0 Or InStr(rlSQL.Text, "!=null") <> 0 Then
            lMsg = lMsg & "\nPlease use:\nISNULL(field,0)<>0 for numeric field\nor: ISNULL(field,\'\')<>\'\' for text field\ninstead of: field != NULL\n"
        ElseIf InStr(rlSQL.Text, "is null") <> 0 Or ((InStr(rlSQL.Text, "=null") <> 0 Or InStr(rlSQL.Text, "= null") <> 0)) Then
            lMsg = lMsg & "\nPlease use:\nISNULL(field,0)=0 for numeric field\nor: ISNULL(field,\'\')=\'\' for text field\ninstead of: field IS NULL or field = NULL\n"
        ElseIf InStr(rlSQL.Text, "is not null") <> 0 Then
            lMsg = lMsg & "\nPlease use:\nISNULL(field,0)<>0 for numeric field\nor: ISNULL(field,\'\')<>\'\' for text field\ninstead of: field IS NOT NULL\n"
        ElseIf InStr(rlSQL.Text, "<> null") <> 0 Or InStr(rlSQL.Text, "<>null") <> 0 Then
            lMsg = lMsg & "\nPlease use:\nISNULL(field,0)<>0 for numeric field\nor: ISNULL(field,\'\')<>\'\' for text field\ninstead of: field <> NULL\n"
        Else
            Dim a As String = oSc.valSQL(rlSQL.Text, tbl)
            If a <> "" Then
                lMsg = lMsg & a
            End If
        End If

        If lMsg <> "" Then
            'liBodyLoadAction.Text = "alert('" + lMsg + "');"
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & lMsg & "');", True)
            If ViewState("scid") = "" Or ViewState("scid") = -1 Then
                File.Delete(Server.MapPath("text\") & ipadd & "temp.txt")
            Else
                File.Delete(Server.MapPath("text\") & ViewState("scid") & "temp.txt")
            End If
        End If
    End Sub
End Class