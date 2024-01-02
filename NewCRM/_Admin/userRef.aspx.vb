Imports System.Web.Script.Services

Public Class userRef
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("-", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Page User Reference", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With
            Catch ex As Exception
                Session.Abandon()
                Response.Redirect("../Login")
            End Try
        End If
    End Sub


    <System.Web.Services.WebMethod()>
    Public Shared Function getOrganizationChart(ByVal userid As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""

        With New lUsers
            ds = .getOrganization(userid)
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