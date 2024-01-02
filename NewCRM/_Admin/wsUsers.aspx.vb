Imports System.Web.Script.Services
Imports Obout.Grid
Imports Obout.Interface
Public Class wsUsers
    Inherits System.Web.UI.Page
    Public lUsr As New lwsUsers
    Public lFun As New lFunction
    Public llog As New lLog
    Public lGen As New lGeneral
    Public AllowDelete As Boolean

#Region " Global Variable "
    Dim gStrFormName = "document.Form1."
    Dim usrLogin As String
    Dim usExist As DataTable
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region
    Private Shared _me As New wsUsers()
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
            Catch ex As Exception
                liBodyLoadAction.Text = "swal('" & ex.Message & "');"
            End Try
        End If
    End Sub
    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            tractive.Attributes.Add("style", "display:none")
            trprive.Attributes.Add("style", "display:none")
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_usr_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_usr_mgt_view" Then
                    tractive.Attributes.Add("style", "display:''")
                    trprive.Attributes.Add("style", "display:''")
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
        Dim ds As DataSet = lUsr.getUserList()
        ds.Tables(0).Columns.Add("usr_ws")
        For Each dr In ds.Tables(0).Rows
            dr("usr_ws") = lFun.EncryptString(dr("uws_userid"))
            If dr("lws_userlevel") = "-" Then
                dr("lws_userlevel") = "No Access Level"
            End If
        Next
        grdWsUser.DataSource = ds
        grdWsUser.DataBind()
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function getDetailUser(ByVal userid As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""

        With New lwsUsers
            ds = .getWsUser(userid)
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
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If liBodyLoadAction.Text = "" Then
            Dim lMsg As String = ""
            Dim lAction As String = ""


            If hidID.Value <> "" Then
                editWSUser()
            Else
                insertWSUser()

            End If

        End If
    End Sub
    Sub insertWSUser()


        If liBodyLoadAction.Text = "" Then
            Dim uID As String = txtUserID.Text
            Dim uName As String = txtUserName.Text
            Dim uDescription As String = txtDesc.Text

            Dim uStatus As Boolean
            If chkActived.Checked = True Then
                uStatus = True
            Else
                uStatus = False
            End If

            Dim privilage As String = ""
            If chkPrivilage.Checked = True Then
                privilage = "Tasklist"
            Else
                privilage = "-"
            End If

            Try
                With New lwsUsers
                    Dim usExist As DataTable
                    Dim objLog As New lLog
                    Dim uAuth As String
                    Dim lMsg As String = ""
                    Dim lAction As String = ""

                    uAuth = .authKey().ToString()

                    With New lwsUsers
                        usExist = .cekUser(uID)
                    End With

                    Dim stat As String = ""
                    If chkActived.Checked = True Then
                        stat = "Active"
                    Else
                        stat = "Inactive"
                    End If

                    Dim task As String = ""
                    If chkPrivilage.Checked = True Then
                        task = "Tasklist"
                    Else
                        task = ""
                    End If

                    Dim logs As String = ""

                    If usExist.Rows(0).Item("tRow") = 1 Then
                        lMsg = lMsg + "User ID is exist.\n"
                        If lAction = "" Then
                            lAction = gStrFormName + txtUserID.ID + ".focus()"
                        End If
                        If lMsg <> "" Then
                            liBodyLoadAction.Text = "alert('" + lMsg + "');" + lAction
                        End If
                    Else

                        'logs = objLog.getLogString(Request, Session("UserLoginID"), "WS User Profile Change - USERID=" + Request.Form("uws_userid") + ";PRIVILAGE=" + task + ";STATUSID=" + stat)
                        'objLog.insertLog("WSusers", logs)
                        .insertWSUser(uID, uName, uDescription, uStatus, uAuth)
                        .insertWSLevel(uID, privilage, False)

                    End If
                    liBodyLoadAction.Text = "sweetURL('Successfully', 'User has been saved', 'success','" & Request.Url.GetLeftPart(UriPartial.Authority) + Request.FilePath & "');"

                End With
            Catch ex As Exception

            End Try
        End If



    End Sub

    Sub editWSUser()


        If liBodyLoadAction.Text = "" Then
            Dim uID As String = txtUserID.Text
            Dim uName As String = txtUserName.Text
            Dim uDescription As String = txtDesc.Text

            Dim uStatus As Boolean
            If chkActived.Checked = True Then
                uStatus = True
            Else
                uStatus = False
            End If

            Dim privilage As String = ""
            If chkPrivilage.Checked = True Then
                privilage = "Tasklist"
            Else
                privilage = "-"
            End If

            Try
                With New lwsUsers
                    Dim uAuth As String

                    uAuth = .authKey().ToString()
                    .updateWSUser(uID, uName, uDescription, uStatus, uAuth)
                    .updateWSLevel(uID, privilage, 1)

                    liBodyLoadAction.Text = "sweetURL('Successfully','User has been updated','success','" & Request.Url.GetLeftPart(UriPartial.Authority) + Request.FilePath & "');"
                End With
            Catch ex As Exception

            End Try
        End If

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function getDeleteWUser(ByVal userid As String) As String
        Return _me.deleteUser(userid)
    End Function
    Private Function deleteUser(ByVal userid As String) As String
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        'Dim dr As DataRow
        'Dim ds As DataSet
        Dim json As String = ""

        lUsr.deleteWSUser(userid)
            Return True


        'ds = lUsr.getWsUser(userid)
        'dr = ds.Tables(0).Rows(0)

        'If Not usrLogin = dr.Item("uws_userid") Then
        '    lUsr.deleteWSUser(dr.Item("uws_userid"))
        '    Return True
        'Else
        '    Return False
        'End If

        Return json
    End Function


End Class