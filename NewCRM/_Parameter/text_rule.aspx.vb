Imports System
Imports System.Net
Imports System.Console
Imports System.IO
Imports System.IO.File
Imports System.Web.HttpRequest

Public Class text_rule
    Inherits System.Web.UI.Page
    Dim oRule As New lRule
    Protected textrule
    Protected op, valz, idz

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        valz = Request.QueryString("scid")
        valz = Mid(valz, 1, InStr(valz, "|") - 1)
        idz = Request.QueryString("idz")

        DestinationControl.Value = idz

        If Not IsPostBack Then
            Session.Remove("chkboxitem")
            op = Request.QueryString("op")
            ViewState("op") = op
            FillComboBox(valz, op)
        End If
    End Sub

    Sub FillComboBox(ByVal valz, ByVal op)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oRule.getListofTextRule(valz)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Rows.Count
                If op = "in" Or op = "not in" Then
                    chkbox.Visible = True
                    chkbox.Items.Add(dt.Rows(i - 1).Item("value"))
                Else
                    chkradio.Visible = True
                    chkradio.Items.Add(dt.Rows(i - 1).Item("value"))
                End If
            Next
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim ds As DataSet
        Dim dt As DataTable
        Dim x As Integer = 0

        ds = oRule.getListofTextRule(valz)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Rows.Count
                If chkbox.Items.Count <> 0 Then
                    If chkbox.Items(i - 1).Selected Then
                        If x > 0 Then
                            Session("chkboxitem") = Session("chkboxitem") & ",'" & chkbox.Items(i - 1).Text & "'"
                        Else
                            Session("chkboxitem") = Session("chkboxitem") & "'" & chkbox.Items(i - 1).Text & "'"
                        End If

                        x = x + 1
                    End If
                End If
                If chkradio.Items.Count <> 0 Then
                    If chkradio.Items(i - 1).Selected Then
                        If ViewState("op") = "like" Or ViewState("op") = "not like" Or ViewState("op") = "startswith" Or ViewState("op") = "endswith" Or ViewState("op") = "equal" Or ViewState("op") = "notequal" Then
                            Session("chkboxitem") = Session("chkboxitem") & chkradio.Items(i - 1).Text
                        Else
                            Session("chkboxitem") = Session("chkboxitem") & "'" & chkradio.Items(i - 1).Text & "'"
                        End If
                    End If
                End If
            Next
        End If
        temp1.Value = Session("chkboxitem")
        liBodyLoadAction.Text = "dhanysuramoz();CloseMe();"
    End Sub
End Class