Imports System.IO
Imports System.Net
Imports Newtonsoft.Json


Public Class processOffer
    Inherits System.Web.UI.Page
    Dim cWs As New lws
    Dim con As New dbConn
    Dim cFun As New lFunction
    Public html As String = ""
    Public Shared _me As New processOffer()

#Region " Global Variable "
    Dim usrLogin As String
    Dim usrRole As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        usrRole = IIf(IsDBNull(Session("user_role")), "IUser", Session("user_role"))
        cekUserAuthentication()
        Try
            If Not IsPostBack Then
                'User track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("-", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Page Log Activity", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With
            End If

            getProcessType()

        Catch ex As Exception
            Response.Redirect("../Login")
        End Try
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            'For i As Integer = 0 To dtUser.Rows.Count - 1
            '    If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_usr_mgt" Then
            '        tf = True
            '    End If
            'Next

            'If Not tf Then
            '    Session.Abandon()
            'End If

        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login")
        End Try
    End Sub

    Sub getProcessType()
        Try
            Dim url As String = con.getWorkflowURL("process")
            Dim myURI As Uri = New Uri(url)
            Dim result_get = cWs.SendGETRequest(myURI, "application/json", "GET")
            Dim jsonDeserialize = JsonConvert.DeserializeObject(Of Requests)(result_get)
            For Each row In jsonDeserialize.data
                ddlType.Items.Add(New ListItem(row.wpm_type, row.wpm_type))
            Next
        Catch ex As Exception
            liBodyLoadAction.Text = "sweet('','" & ex.Message & "','error');"
        End Try


    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        liBodyLoadAction.Text = ""
        lblOffer.Visible = False
        ddlOffer.Visible = True
        btnSubmitOffer.Visible = True
        Try
            If ddlType.SelectedItem.Value <> "-" Then
                ddlOffer.Items.Clear()
                Dim url As String = con.getWorkflowURL("process")
                Dim myURI As Uri = New Uri(url)
                Dim dtJson As String = "{""offer_type"":""" + ddlType.SelectedItem.Value + """,""user_role"":""" & usrRole & """}"
                Dim data = Encoding.UTF8.GetBytes(dtJson)
                Dim result_post = cWs.SendRequest(myURI, data, "application/json", "POST")
                Dim jsonDeserialize = JsonConvert.DeserializeObject(Of Requests)(result_post)

                If jsonDeserialize.status <> "error" Then
                    For Each row In jsonDeserialize.data
                        ddlOffer.Items.Add(New ListItem(row.wpm_name, row.wpm_id))
                    Next

                    If jsonDeserialize.data.Count > 0 Then
                        lblOffer.Visible = True
                        ddlOffer.Visible = True
                        btnSubmitOffer.Visible = True
                    Else
                        liBodyLoadAction.Text = "sweet('', 'You can not access this page', 'error');"
                    End If
                Else
                    liBodyLoadAction.Text = "sweet('', '" & jsonDeserialize.message & "', 'error');"
                End If
            Else
                lblOffer.Visible = False
                ddlOffer.Visible = False
                btnSubmitOffer.Visible = False
            End If

        Catch ex As Exception
            liBodyLoadAction.Text = "sweet('', '" & ex.Message & "', 'error');"
        End Try
    End Sub

    Protected Sub btnSubmitOffer_Click(sender As Object, e As EventArgs) Handles btnSubmitOffer.Click
        Response.Redirect("processOfferForm?id=" & cFun.EncryptString(ddlOffer.SelectedItem.Value) & "")
    End Sub
End Class