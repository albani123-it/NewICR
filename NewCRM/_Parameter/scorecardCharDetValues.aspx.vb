Public Class scorecardCharDetValues
    Inherits System.Web.UI.Page
    Dim oSc As New lScorecards

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        cekUserAuthentication()
        Dim field = Request.QueryString("f")
        DestinationControl.Value = Request.QueryString("destination")
        If Not IsPostBack Then
            isiData(field)
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1

            Next
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Sub isiData(ByVal field)
        Dim ds As DataSet
        ds = oSc.getSchValues(field)
        lstParameters.DataSource = ds
        lstParameters.DataTextField = "lbd_desc"
        lstParameters.DataValueField = "lbd_value"
        lstParameters.DataBind()
    End Sub
End Class