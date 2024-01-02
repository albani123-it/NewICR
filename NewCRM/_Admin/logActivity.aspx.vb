Imports System.Web.Script.Services

Public Class logActivity
    Inherits System.Web.UI.Page
    Dim llogs As New lLog

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("-", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Page Log Activity", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With
            Catch ex As Exception
                Session.Abandon()
                Response.Redirect("../Login")
            End Try
        End If

    End Sub
    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        liBodyLoadAction.Text = ""
        grdListActivityLog.DataSource = Nothing
        grdListActivityLog.DataBind()
        CheckkInput()
    End Sub

    Sub CheckkInput()
        Dim errMsg As String = ""

        'If txtAccNo.Value = "" And txtCardNo.Value = "" And txtName.Value = "" And txtDOB.Value = "" And txtCNUM.Value = "" And txtFSV.Value = "" And txtCSV.Value = "" And txtGCN.Value = "" Then
        '    errMsg = errMsg + "Please enter one of the customer information to be search.\n"
        'ElseIf txtCardNo.Value <> "" And IsNumeric(txtCardNo.Value) <> True Then
        '    errMsg = errMsg + "Card number should be numeric.\n"
        'ElseIf txtName.Value = "" And txtDOB.Value <> "" Then
        '    errMsg = errMsg + "Please enter Name and DOB of the customer information together to be search.\n"
        'ElseIf txtName.Value <> "" And txtDOB.Value = "" Then
        '    errMsg = errMsg + "Please enter Name and DOB of the customer information together to be search.\n"
        'ElseIf txtDOB.Value <> "" And (txtDOB.Value.Length <> 8 Or Not lGen.CheckInputDate(Mid(txtDOB.Value, 1, 2) + "-" & Mid(txtDOB.Value, 3, 2) + "-" & Mid(txtDOB.Value, 5, 4))) Then 'tambahkan cek validasi tanggal
        '    errMsg = errMsg + "Please check DoB format.\n"
        'End If
        If errMsg <> "" Then
            liBodyLoadAction.Text = "alert('" + errMsg + "');"
        Else
            Search()
        End If
    End Sub

    Sub Search()
        Dim dt As DataTable
        Dim ds As DataSet
        Dim startDate As String = txtStartDate.Value
        Dim endDate As String = txtEndDate.Value

        ds = llogs.getListLogActivity(startDate, endDate)
        dt = ds.Tables(0)

        grdListActivityLog.DataSource = ds
        grdListActivityLog.DataBind()


        grdListActivityLog.Visible = True
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function getDetailActivityLog(ByVal userid As String, ByVal datelog As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""

        With New lLog
            ds = .getDetailActivityLog(userid, datelog)
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
    Public Shared Function getViewDetailActivityLog(ByVal logid As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""

        With New lLog
            ds = .getViewDetailActivityLog(logid)
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

End Class