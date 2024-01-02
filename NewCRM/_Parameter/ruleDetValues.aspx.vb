Public Class ruleDetValues
    Inherits System.Web.UI.Page
    Dim oRule As New lRule

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        cekUserAuthentication()
        Dim field = Request.QueryString("f")
        Dim source = Request.QueryString("source")
        SourceTable.Value = source
        DestinationControl.Value = Request.QueryString("destination")
        If Not IsPostBack Then
            isiData(field, source)
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

    Sub isiData(ByVal field, ByVal sourcevalue)
        

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
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oRule.getListofPageRule(field)
            dt = ds.Tables(0)
            lstParameters.DataSource = ds
            lstParameters.DataTextField = "name"
            lstParameters.DataValueField = "value"
            lstParameters.DataBind()
        End If
    End Sub
End Class