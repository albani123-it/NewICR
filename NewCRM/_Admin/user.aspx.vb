Imports System.Web.Script.Services
Imports Obout.Grid
Imports Obout.Interface

Public Class user
    Inherits System.Web.UI.Page
    Public lUsr As New lUsers
    Public lFun As New lFunction
    Public llog As New lLog
    Public lGen As New lGeneral
    Public AllowDelete As Boolean
    Protected exists As Boolean = False

#Region " Global Variable "
    Dim gStrFormName = "document.Form1."
    Dim usrLogin As String
    Dim usExist As DataTable
#End Region
    Private Shared _me As New user()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        liBodyLoadAction.Text = ""
        cekUserAuthentication()

        If Not IsPostBack Then
            Try
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("-", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Page User Management", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With
            Catch ex As Exception
                Session.Abandon()
                Response.Redirect("../Login")
            End Try

            Try
                BindGrid()
                BindMasterData()
            Catch ex As Exception
                liBodyLoadAction.Text = "swal('" & ex.Message & "');"
            End Try

        End If



    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            trStat.Attributes.Add("style", "display:none")

            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_usr_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_usr_mgt_view" Then
                    trStat.Attributes.Add("style", "display:''")
                End If

            Next

            If Not tf Then
                Session.Abandon()
            End If

        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login")
        End Try
    End Sub

    Sub BindGrid()
        Dim ds As DataSet = lUsr.getDataSetUser()
        ds.Tables(0).Columns.Add("usr_en")
        For Each dr In ds.Tables(0).Rows
            dr("usr_en") = lFun.EncryptString(dr("usr_usersid"))
        Next
        grdUser.DataSource = ds
        grdUser.DataBind()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If liBodyLoadAction.Text = "" Then
            Dim lMsg As String = ""
            Dim lAction As String = ""


            If hidID.Value <> "" Then
                editUser()
            Else
                With New lUsers
                    usExist = .checkUser(txtUserID.Text)
                End With
                If usExist.Rows(0).Item("tRow") = 1 Then
                    lMsg = lMsg + "User ID is exist.\n"
                    If lAction = "" Then
                        lAction = gStrFormName + txtUserID.ID + ".focus()"
                    End If
                    If lMsg <> "" Then
                        liBodyLoadAction.Text = "swal('" + lMsg + "');" + lAction
                    End If
                Else
                    insertUser()
                End If
            End If

        End If
    End Sub

    Sub insertUser()
        Dim userid As String = txtUserID.Text
        Dim name As String = txtUserName.Text
        Dim nip As String = txtNIP.Text
        Dim email As String = txtEmail.Text
        Dim branch As String = Request.Form(lGen.requestForm("ddlBranch"))
        Dim supervisor As String = Request.Form(lGen.requestForm("ddlSupervisor"))
        Dim position As String = Request.Form(lGen.requestForm("ddlPosition"))
        Dim accLevel As String = Request.Form(lGen.requestForm("ddlLevel"))
        Dim efectiveDate As String = txtEfectiveDate.Value
        Dim status As String = chkStatus.Checked

        lUsr.InsertUser(userid, name, nip, email, branch, supervisor, position, accLevel, efectiveDate, status, usrLogin)
        Dim stat As String = ""
        If chkStatus.Checked = True Then
            stat = "Active"
        Else
            stat = "Inactive"
        End If
        Dim logs As String = "User Creation - USERID=" + userid + ";NIP=" + nip + ";LEVELID=" + accLevel + ";STATUS=" + stat
        'user track
        With New lLog
            Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
            .log_user_tracking("users", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), logs, Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
        End With
        liBodyLoadAction.Text = "sweetURL('Successfully', 'User has been saved', 'success','" & Request.Url.GetLeftPart(UriPartial.Authority) + Request.FilePath & "');"
    End Sub

    Sub editUser()
        Dim userid As String = txtUserID.Text
        Dim name As String = txtUserName.Text
        Dim nip As String = txtNIP.Text
        Dim email As String = txtEmail.Text
        Dim branch As String = Request.Form(lGen.requestForm("ddlBranch"))
        Dim supervisor As String = Request.Form(lGen.requestForm("ddlSupervisor"))
        Dim position As String = Request.Form(lGen.requestForm("ddlPosition"))
        Dim accLevel As String = Request.Form(lGen.requestForm("ddlLevel"))
        Dim efectiveDate As String = txtEfectiveDate.Value
        Dim status As String = chkStatus.Checked


        lUsr.editUser(userid, name, nip, email, branch, supervisor, position, accLevel, efectiveDate, status, usrLogin)

        Dim stat As String = ""
        If chkStatus.Checked = True Then
            stat = "Active"
        Else
            stat = "Inactive"
        End If

        Dim logs As String = ""
        logs = "User Profile Change - USERID=" + userid + ";LEVELID=" + accLevel + ";STATUSID=" + stat

        'user track
        With New lLog
            Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
            .log_user_tracking("users", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), logs, Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
        End With

        liBodyLoadAction.Text = "sweetURL('Successfully','User has been updated','success','" & Request.Url.GetLeftPart(UriPartial.Authority) + Request.FilePath & "');"
    End Sub

    Sub BindMasterData()
        Dim dtLevel As DataTable
        Dim dtBranch As DataTable
        Dim dtposition As DataTable
        Dim dtsupervesor As DataTable

        dtLevel = lUsr.getParameterDDL("level", usrLogin)
        ddlLevel.DataSource = dtLevel
        ddlLevel.DataTextField = "level_view"
        ddlLevel.DataValueField = "level_value"
        ddlLevel.DataBind()


        dtBranch = lUsr.getParameterDDL("branch", usrLogin)
        ddlBranch.DataSource = dtBranch
        ddlBranch.DataTextField = "branch_view"
        ddlBranch.DataValueField = "branch_value"
        ddlBranch.DataBind()

        dtsupervesor = lUsr.getParameterDDL("supervisor", usrLogin)
        ddlSupervisor.DataSource = dtsupervesor
        ddlSupervisor.DataTextField = "superv_name"
        ddlSupervisor.DataValueField = "superv_value"
        ddlSupervisor.DataBind()

        'dtposition = lUsr.getParameterDDL("position", usrLogin)
        'ddlPosition.DataSource = dtposition
        'ddlPosition.DataTextField = "status_view"
        'ddlPosition.DataValueField = "status_value"
        'ddlPosition.DataBind()

    End Sub

    Sub TrimAllInput()
        txtUserID.Text = txtUserID.Text.Trim
        txtUserName.Text = txtUserName.Text.Trim
        txtNIP.Text = txtNIP.Text.Trim
    End Sub

    Sub CekInput()
        Dim lMsg As String = ""
        Dim lAction As String = ""

        If txtUserID.Text = "" Then
            lMsg = lMsg + "Please input User ID.\n"
            If lAction = "" Then
                lAction = gStrFormName + txtUserID.ID + ".focus()"
            End If
        ElseIf ViewState("mode") = "NEW" Then
            Try
                Select Case lUsr.ValidateUser(txtUserID.Text, "new")
                    Case 2
                        lMsg = "UserID must have between 6 to 15 characters!\n"
                    Case 3
                        lMsg = "UserID has already existed. Please input a different userID!\n"
                End Select
            Catch ex As Exception
                lMsg = ex.Message.Replace("'", "\'").Replace(vbCrLf, "\n") + ".\n"
                lAction = gStrFormName + txtUserID.ID + ".focus()"
            End Try
        ElseIf hidID.Value <> "-1" Then
            Try
                Select Case lUsr.ValidateUser(txtUserID.Text, "")
                    Case 1
                        lMsg = "The user id is currently logging in. It cannot be edited or deleted.\n"
                End Select
            Catch ex As Exception
                lMsg = ex.Message.Replace("'", "\'").Replace(vbCrLf, "\n") + ".\n"
                lAction = gStrFormName + txtUserID.ID + ".focus()"
            End Try
        End If
        If txtUserName.Text = "" Then
            lMsg = lMsg + "Please input User Name.\n"
            If lAction = "" Then
                lAction = gStrFormName + txtUserName.ID + ".focus()"
            End If
        End If
        If txtNIP.Text = "" Then
            lMsg = lMsg + "Please input User NPK.\n"
            If lAction = "" Then
                lAction = gStrFormName + txtNIP.ID + ".focus()"
            End If
        End If
        If ddlLevel.Value = "0" Then
            lMsg = lMsg + "Please select Access Level.\n"
            If lAction = "" Then
                lAction = gStrFormName + ddlLevel.ID + ".focus()"
            End If
        End If

        If ddlBranch.Value = "" Then
            lMsg = lMsg + "Please select Branch.\n"
            If lAction = "" Then
                lAction = gStrFormName + ddlBranch.ID + ".focus()"
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "swal('" + lMsg + "');" + lAction
        End If
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grdUser.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = lUsr.getUser(dataItem("usr_userid"))
        Dim dr As DataRow = ds.Tables(0).Rows(0)
        'Dim myDb As New dbConn
        'Dim myConn As SqlConnection
        'myConn = myDb.getConStr
        'myConn.Open()

        If Not usrLogin = dr.Item("usr_userid") Then
            'Dim cmdA As New SqlCommand("sp_deleteUser", myConn)
            'cmdA.CommandType = CommandType.StoredProcedure
            'cmdA.Parameters.Add("@userid", SqlDbType.VarChar).Value = dataItem("usr_userid")
            'cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin
            'cmdA.ExecuteNonQuery()
            'myConn.Close()

            'Dim objLog As New lLog

            'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "UID Deletion - USERID=" + dataItem("usr_userid") + ";NPK=" + dataItem("usr_npk") + ";LEVELID=" + dr.Item("usr_lvl_name") + ";STATUSID=" + dr.Item("usr_status_name") + "")
            'objLog.insertLog("users", logs)
            'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been deleted successfully.');window.location.href='User.aspx';", True)
        Else
            'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Delete failed. You cannot delete your self.');window.location.href='User.aspx';", True)
        End If

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function getDetailUser(ByVal userid As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""

        With New lUsers
            ds = .getUser(userid)
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


    <System.Web.Services.WebMethod()>
    Public Shared Function getRegionForLabel(ByVal value As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""

        With New lUsers
            ds = .getRegion(value)
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

    <System.Web.Services.WebMethod()>
    Public Shared Function getDeleteUser(ByVal userid As String) As String
        Return _me.deleteUser(userid)
    End Function

    Private Function deleteUser(ByVal userid As String) As String
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        Dim dr As DataRow
        Dim ds As DataSet
        Dim json As String = ""


        ds = lUsr.getUser(userid)
        dr = ds.Tables(0).Rows(0)

        If Not usrLogin = dr.Item("usr_usersid") Then
            lUsr.deleteUser(dr.Item("usr_usersid"))
            Return True
        Else
            Return False
        End If

        Return json
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function fillList() As String
        Dim html As String = ""

        html &= "<table>"
        Dim ds As DataSet
        Dim dt As DataTable
        With New lUsers
            ds = .getPositionByWorkflow
            dt = ds.Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            html &= "<tr>"
            html &= "<td style='padding-left:0;'><input type='checkbox' id='chk_" & dt.Rows(i).Item("lwp_id") & "' name='chk_" & dt.Rows(i).Item("lwp_id") & "' />" & dt.Rows(i).Item("lwp_handle") & "</td>"
            html &= "</tr>"
        Next
            html &= "</table>"
        End With

        Return html
    End Function
    <System.Web.Services.WebMethod()>
    Function fillListExisting()
        Dim html As String = ""
        html &= "<table>"
        Dim ds As DataSet
        Dim dt As DataTable
        ds = lUsr.getPositionByWorkflow
        dt = ds.Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dset As DataSet
            Dim dtbl As DataTable
            dset = lUsr.getPositionByWorkflowExisting(lFun.DecryptString(Request.QueryString("lvlid")))
            dtbl = dset.Tables(0)
            html &= "<tr>"
            For j As Integer = 0 To dtbl.Rows.Count - 1
                If dt.Rows(i).Item("lwp_id") = dtbl.Rows(j).Item("usr_position") Then
                    html &= "<td style='padding-left:0;'><input type='checkbox' checked id='chk_" & dt.Rows(i).Item("lwp_id") & "' name='chk_" & dt.Rows(i).Item("lwp_id") & "' />" & dt.Rows(i).Item("lwp_handle") & "</td>"
                Else
                    html &= "<td style='padding-left:0;'><input type='checkbox' id='chk_" & dt.Rows(i).Item("lwp_id") & "' name='chk_" & dt.Rows(i).Item("lwp_id") & "' />" & dt.Rows(i).Item("lwp_handle") & "</td>"
                End If
            Next
            html &= "</tr>"
        Next
        html &= "</table>"
        Return html
    End Function
End Class