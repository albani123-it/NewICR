Imports Newtonsoft.Json

Public Class processOfferForm
    Inherits System.Web.UI.Page
    Public html As String
    Dim cWs As New lws
    Dim cPO As New lProcessOffer
    Dim cGen As New lGeneral
    Dim con As New dbConn
    Dim cFun As New lFunction
    Dim identity As Integer
    Dim wacID As String
    Dim wpsID As String
    Dim wcpType As String

#Region " Global Variable "
    Dim usrLogin As String
    Dim usrRole As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        usrRole = IIf(IsDBNull(Session("user_role")), "IUser", Session("user_role"))

        identity = cFun.DecryptString(Request.QueryString("id"))
        getProcessType(identity, usrLogin, usrRole)
    End Sub

    Sub getProcessType(id, user, role)
        Try
            Dim i As Integer = 1
            Dim url As String = con.getWorkflowURL("hit")
            Dim myURI As Uri = New Uri(url)
            Dim dtJson As String = "{""id"":" & id & ",""user"":""" & user & """,""role"":""" & role & """}"
            Dim data = Encoding.UTF8.GetBytes(dtJson)
            Dim result_post = cWs.SendRequest(myURI, data, "application/json", "POST")
            Dim jsonDeserialize = JsonConvert.DeserializeObject(Of RequestsField)(result_post)
            Dim jsonSerialize = JsonConvert.SerializeObject(jsonDeserialize.info)

            If jsonDeserialize.status <> "error" Then
                For Each rw In jsonDeserialize.info
                    spnTitle.InnerHtml = rw.wad_process_name
                    wacID = rw.wad_wac_id
                    wpsID = rw.wad_process_id
                    wcpType = rw.wcp_type
                Next

                Session.Remove("fld")
                For Each row In jsonDeserialize.form
                    html += "<div class=""col-md-6 col-sm-6 col-xs-12 form-group has-feedback"">"
                    html += "<Label Class=""control-label col-md-4"">" + row.wpf_name + "</label>"
                    html += "<div Class=""col-md-8"">"
                    If i = 1 Then
                        Session("fld") += row.wpf_field
                    Else
                        Session("fld") += "|" & row.wpf_field
                    End If

                    html += cGen.createField(row.wpf_field, row.wpf_type, row.wpf_element, row.wpf_option)
                    html += "</div>"
                    html += "</div>"
                    i = i + 1
                Next
            Else
                liBodyLoadAction.Text = "sweetURL('','" & jsonDeserialize.message & "','" & jsonDeserialize.status & "','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOffer');"
            End If


        Catch ex As Exception
            liBodyLoadAction.Text = "sweetURL('','" & ex.Message & "','error','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOffer');"

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim spltField As String()
            spltField = Session("fld").Split("|")
            Dim formAndValue As String = ""

            formAndValue += "{"
            For i As Integer = 0 To spltField.Length - 1
                If i <> 0 Then
                    formAndValue += ",""" & spltField(i) & """:""" & Request.Form(spltField(i)) & """"
                Else
                    formAndValue += """" & spltField(i) & """:""" & Request.Form(spltField(i)) & """"
                End If

            Next
            formAndValue += "}"

            Dim url As String = con.getWorkflowURL("hit/rehit")
            Dim myURI As Uri = New Uri(url)
            Dim dtJson As String = "{""act"":""Offer"",""wac_id"":""" & wacID & """,""wps_id"":""" & wpsID & """,""wcp_type"":""" & wcpType & """,""user"":""" & usrLogin & """,""rule"":""" & usrRole & """,""form"":[" & formAndValue & "]}"
            Dim data = Encoding.UTF8.GetBytes(dtJson)
            Dim result_post = cWs.SendRequest(myURI, data, "application/json", "POST")
            Dim jsonDeserialize = JsonConvert.DeserializeObject(Of Requests)(result_post)


            If jsonDeserialize.status <> "error" Then
                Response.Redirect("processOffer")
            Else
                liBodyLoadAction.Text = "sweetURL('','" & jsonDeserialize.message & "','error','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOfferForm');"
            End If
        Catch ex As Exception
            liBodyLoadAction.Text = "sweetURL('','" & ex.Message & "','error','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOfferForm');"
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Dim url As String = con.getWorkflowURL("hit/cancel")
            Dim myURI As Uri = New Uri(url)
            Dim dtJson As String = "{""wac_id"":""" & wacID & """,""user"":""" & usrLogin & """,""Role"":""" & usrRole & """}"
            Dim data = Encoding.UTF8.GetBytes(dtJson)
            Dim result_post = cWs.SendRequest(myURI, data, "application/json", "POST")
            Dim jsonDeserialize = JsonConvert.DeserializeObject(Of Requests)(result_post)

            If jsonDeserialize.status <> "error" Then
                Response.Redirect("processOffer")
            Else
                liBodyLoadAction.Text = "sweetURL('','" & jsonDeserialize.message & "','error','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOfferForm');"
            End If
        Catch ex As Exception
            liBodyLoadAction.Text = "sweetURL('','" & ex.Message & "','error','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOfferForm');"
        End Try
    End Sub

    Protected Sub btnSaD_Click(sender As Object, e As EventArgs) Handles btnSaD.Click
        Try
            insertSaveAsDraft()
        Catch ex As Exception
            liBodyLoadAction.Text = "sweetURL('','" & ex.Message & "','error','" & Request.Url.GetLeftPart(UriPartial.Authority) & "/NewCRM/_Sales/processOfferForm');"
        End Try

    End Sub

    Sub insertSaveAsDraft()
        Dim spltField As String()
        spltField = Session("fld").Split("|")

        For i As Integer = 0 To spltField.Length - 1
            cPO.SaveAsDraft(Request.QueryString("id"), spltField(i), Request.Form(spltField(i)))
        Next

        'liBodyLoadAction.Text = "sweetURL('Successfully', 'User has been saved', 'success','" & Request.Url.GetLeftPart(UriPartial.Authority) + Request.FilePath & "');"
    End Sub

End Class