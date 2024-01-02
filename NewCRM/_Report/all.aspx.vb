Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Public Class all
    Inherits System.Web.UI.Page

#Region " Global Variable "
    Dim usrLogin As String
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()
        ReportViewer1.ProcessingMode = ProcessingMode.Remote
        ReportViewer1.ShowCredentialPrompts = True
        ReportViewer1.ShowExportControls = True

        If Not IsPostBack Then
            'ReportViewer1.ServerReport.ReportServerCredentials = New ReportServerCredentials()
            ReportViewer1.ServerReport.ReportServerUrl = New Uri("http://localhost/reportserver")
            ReportViewer1.ServerReport.ReportPath = "/basel_dki/all"
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_rpt_all" Then
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
End Class