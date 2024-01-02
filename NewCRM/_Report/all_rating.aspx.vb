Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Public Class all_rating
    Inherits System.Web.UI.Page

#Region " Global Variable "
    Dim usrLogin As String
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()
        ReportViewer1.Attributes.Add("style", "display:none")
        ReportViewer2.Attributes.Add("style", "display:none")
        'ReportViewer1.ProcessingMode = ProcessingMode.Remote
        'ReportViewer1.ShowCredentialPrompts = True
        'ReportViewer1.ShowExportControls = True

        'ReportViewer2.ProcessingMode = ProcessingMode.Remote
        'ReportViewer2.ShowCredentialPrompts = True
        'ReportViewer2.ShowExportControls = True

        'If Not IsPostBack Then
        '    'ReportViewer1.ServerReport.ReportServerCredentials = New ReportServerCredentials()
        '    ReportViewer1.ServerReport.ReportServerUrl = New Uri("http://localhost/reportserver")
        '    ReportViewer1.ServerReport.ReportPath = "/basel_dki/borrower_rating"

        '    ReportViewer2.ServerReport.ReportServerUrl = New Uri("http://localhost/reportserver")
        '    ReportViewer2.ServerReport.ReportPath = "/basel_dki/facility_rating"
        'End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_rpt_all_rating" Then
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

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        cekParam()
        If liBodyLoadAction.Text = "" Then
            Dim startDate = Mid(txtStartDate.Text, 7, 4) + "-" & Mid(txtStartDate.Text, 4, 2) + "-" & Mid(txtStartDate.Text, 1, 2)
            Dim endDate = Mid(txtEndDate.Text, 7, 4) + "-" & Mid(txtEndDate.Text, 4, 2) + "-" & Mid(txtEndDate.Text, 1, 2)
            Dim ds As DataSet
            Dim dt As DataTable
            ds = getDataReport(ddlType.SelectedValue, startDate, endDate)
            dt = ds.Tables(0)

            If ddlType.SelectedValue = "Borrower" Then
                'ReportViewer1.Visible = True
                ReportViewer1.Attributes.Add("style", "display:''")
            End If

            If ddlType.SelectedValue = "Facility" Then
                'ReportViewer2.Visible = True
                ReportViewer2.Attributes.Add("style", "display:''")
            End If
        End If
    End Sub

    Sub cekParam()
        Dim lMsg As String = ""
        If ddlType.SelectedValue = "Select One" Then
            lMsg &= "Please choose type.\n"
        End If
        
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" Then
            Dim startDate = Mid(txtStartDate.Text, 7, 4) + "-" & Mid(txtStartDate.Text, 4, 2) + "-" & Mid(txtStartDate.Text, 1, 2)
            Dim endDate = Mid(txtEndDate.Text, 7, 4) + "-" & Mid(txtEndDate.Text, 4, 2) + "-" & Mid(txtEndDate.Text, 1, 2)
            Dim result = DateTime.Compare(endDate, startDate)

            If result < 0 Then
                lMsg = "End Date must be greater than Start date.\n"
            End If
        Else
            If txtStartDate.Text = "" Then
                lMsg &= "Please input start date.\n"
            End If
            If txtEndDate.Text = "" Then
                lMsg &= "Please input end date"
            End If
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Function getDataReport(ByVal typ, ByVal start, ByVal ends)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("rpt_insert_rating", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@type", typ)
        cmda.Parameters.AddWithValue("@startDate", start)
        cmda.Parameters.AddWithValue("@endDate", ends)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
End Class