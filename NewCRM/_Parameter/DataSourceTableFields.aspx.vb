Public Class DataSourceTableFields
    Inherits System.Web.UI.Page
    Dim oDm As New lDataMgmt
    Dim destination As String
    Dim sourcevalue As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        cekUserAuthentication()
        destination = Request.QueryString("destination")
        sourcevalue = Request.QueryString("source")
        DestinationControl.Value = destination
        SourceTable.Value = sourcevalue

        If (sourcevalue = "operator") Then
            Label1.Text = "Operator"
            lstParameters.Items.Clear()
            lstParameters.Items.Add("")
            lstParameters.Items.Insert(0, New ListItem("and", "and"))
            lstParameters.Items.Insert(1, New ListItem("or", "or"))
            lstParameters.Items.Insert(2, New ListItem("(", "("))
            lstParameters.Items.Insert(3, New ListItem("=", "="))
            lstParameters.Items.Insert(4, New ListItem("<>", "<>"))
            lstParameters.Items.Insert(5, New ListItem(">", ">"))
            lstParameters.Items.Insert(6, New ListItem(">=", ">="))
            lstParameters.Items.Insert(7, New ListItem("<", "<"))
            lstParameters.Items.Insert(8, New ListItem("<=", "<="))
            lstParameters.Items.Insert(9, New ListItem(")", ")"))
        Else
            lstParameters.Items.Clear()
            Label1.Text = "Fields"
            Dim dtlist As DataTable
            dtlist = oDm.LoadSourceTableColumns(sourcevalue)
            With Me.lstParameters
                .DataTextField = dtlist.Columns(1).ColumnName
                .DataValueField = dtlist.Columns(0).ColumnName
                .DataSource = dtlist
                .DataBind()
            End With
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

End Class