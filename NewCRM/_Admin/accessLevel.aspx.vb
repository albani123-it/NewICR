Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Newtonsoft.Json

Public Class accessLevel
    Inherits System.Web.UI.Page
    Public AllowDelete As Boolean
    Public dtParent As DataTable
    Public dtChild As DataTable
    Public dtSubChild As DataTable
    Public dtSubSubChild As DataTable
    Public dtlang As DataTable
    Public dtLvlDet As DataTable
    Dim oLevel As New lLevel
    Dim oGen As New lGeneral
    Dim objLog As New lLog
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim gStrFormName = "document.Form1."
    Dim usrLogin As String
    Dim lm_description As String
    Dim urlBase As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        liBodyLoadAction.Text = ""

        If Not IsPostBack Then
            Try
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("-", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Page Access Level", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With
            Catch ex As Exception
                Session.Abandon()
                Response.Redirect("../Login")
            End Try
        End If

        BindGrid()

        dtParent = oLevel.getParentLevel()
    End Sub

    Sub BindGrid()
        Dim ds As DataSet = oLevel.getListOfLevel()
        grdAccessLevel.DataSource = ds
        grdAccessLevel.DataBind()
    End Sub


    Sub getChild(ByVal parentID)
        dtChild = oLevel.getChild(parentID)
    End Sub

    Sub getSubChild(ByVal ChildID)
        dtSubChild = oLevel.getSubChild(ChildID)
    End Sub

    Sub getSubSubChild(ByVal SubChildID)
        dtSubSubChild = oLevel.getSubSubChild(SubChildID)
    End Sub

    Sub getLang(ByVal ID)
        dtlang = oLevel.getLang(ID)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim lvlCodeExist As DataTable
        Dim lvlNameExist As DataTable

        TrimAllInput()
        CheckInput()

        Dim id = hidLevelCode.Value

        If Not id = "" Then
            If id > 0 Then
                editLevel(id)
            Else
                Dim lMsg As String = ""
                Dim lAction As String = ""
                Try
                    lvlCodeExist = oLevel.checkLevel(txtLevelCode.Text, "CK")
                    If lvlCodeExist.Rows(0).Item("tRow") = 1 Then
                        liBodyLoadAction.Text = "alert('User Role Code is exist.');"
                    Else
                        lvlNameExist = oLevel.checkLevel(txtLevelName.Text, "CN")
                        insertLevel()
                    End If
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message & "');window.opener.location.href = window.opener.location.href;parent.window.close();window.refresh();"
                End Try

            End If
        End If
    End Sub

    Sub TrimAllInput()
        hdId.Value = hdId.Value.Trim
        txtLevelCode.Text = txtLevelCode.Text.Trim
        txtLevelName.Text = txtLevelName.Text.Trim
        txtLevelDescription.Text = txtLevelDescription.Text.Trim
    End Sub

    Sub CheckInput()

    End Sub

    Sub editLevel(ByVal id)
        Try
            oLevel.editRecordV2(usrLogin, id, txtLevelCode.Text, txtLevelName.Text,
                                txtLevelDescription.Text, chkStatus.Checked, txtLimitFrom.Text, txtLimitTo.Text)
            Try
                Dim dt As DataTable
                dt = oLevel.getListLevelMaster()
                If dt.Rows.Count > 0 Then
                    oLevel.DeleteAccessLevelDetail(hidLevelCode.Value)
                    For i As Integer = 0 To dt.Rows.Count() - 1
                        lm_description = dt.Rows(i).Item("lm_description").ToString()
                        If Request.Form(lm_description) = "on" Then
                            oLevel.InsertAccessLevelDetail(txtLevelCode.Text, lm_description, usrLogin)
                        End If
                    Next
                End If

                Dim logs As String = ""
                logs = "User Level Updated - User Role Code=" + txtLevelCode.Text + ";User Role Name=" + txtLevelName.Text
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("level", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), logs, Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With

                BindGrid()
                liBodyLoadAction.Text = "alert('User Role updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message & "');window.opener.location.href = window.opener.location.href;parent.window.close();"
            End Try
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message & "');window.opener.location.href = window.opener.location.href;parent.window.close();"
        End Try

    End Sub
    Sub insertLevel()

        Dim lvlCode As String = txtLevelCode.Text
        Dim lvlName As String = txtLevelName.Text
        Dim lvlDescription As String = txtLevelDescription.Text
        Dim lvlStatus As String = chkStatus.Checked
        Dim lvlLimitFrom As String = txtLimitFrom.Text
        Dim lvlLimitTo As String = txtLimitTo.Text

        Try
            oLevel.InsertRecord(lvlCode, lvlName, lvlDescription, lvlStatus, lvlLimitFrom, lvlLimitTo, "crm_admin")
            Try
                Dim dt As DataTable
                dt = oLevel.getListLevelMaster()
                If dt.Rows.Count > 0 Then
                    oLevel.DeleteAccessLevelDetail(txtLevelCode.Text)
                    For i As Integer = 0 To dt.Rows.Count() - 1
                        lm_description = dt.Rows(i).Item("lm_description").ToString()
                        If Request.Form(lm_description) = "on" Then
                            oLevel.InsertAccessLevelDetail(txtLevelCode.Text, lm_description, usrLogin)
                        End If
                    Next
                End If
                liBodyLoadAction.Text = "alert('User Role saved.');"
                ReBinGrid()
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message & "');"
            End Try
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message & "');"
        End Try
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grdAccessLevel.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim userAccessLevelExist As DataTable

        userAccessLevelExist = oLevel.checkAccessLevel(dataItem("id"))

        If userAccessLevelExist.Rows(0).Item("tRow") > 0 Then
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Used level can not be deleted.');window.location.href='Level.aspx';", True)
        Else
            oLevel.deleteAccessLevel(dataItem("id"), usrLogin)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been deleted successfully.');", True)
            ReBinGrid()
        End If
    End Sub
    Protected Sub edit(ByVal sender As Object, ByVal e As CommandEventArgs)
        txtLevelCode.Text = "asdasd"
        txtLevelName.Text = "bani"
        liBodyLoadAction.Text = "
                $(document).ready(function() {
                    $('#accessLevelModal').modal({show: 'true'});
                });
        "

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function SendParameters(name As String, age As Integer) As String
        Return String.Format("Name: {0}{2}Password: {1}", name, age, Environment.NewLine)
    End Function

    Public Function getgrdAccessLevelDataItem(ByVal indexDatagrid)
        Dim dataItem As Hashtable = TryCast(grdAccessLevel.Rows(indexDatagrid).ToHashtable(), Hashtable)

        Dim userAccessLevelExist As DataTable

        Return dataItem("id")
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function getAccessDetail(id As String) As String
        Dim JSONresult As String
        Dim oLevel As New lLevel
        Dim objAK = New accessLevel
        Dim dtAccess1 As DataTable = oLevel.getAccessLevelDetail(id)
        Dim level_code = dtAccess1.Rows(0).Item("lu_level_code")
        Dim dtAccess2 As DataTable = oLevel.getAccessLevelListDetail(level_code)


        Dim data = New With {
            Key .dt1 = dtAccess1,
            Key .dt2 = dtAccess2
        }

        JSONresult = JsonConvert.SerializeObject(data, Formatting.None)

        Return JSONresult
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function getEditLevel(ByVal lvlCode As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""
        With New lLevel
            ds = .getListLevelMaster()
            dt = ds.Tables(0)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            json = serializer.Serialize(rows)
        End With


        Return json

    End Function


    Sub ReBinGrid()
        BindGrid()
    End Sub

End Class