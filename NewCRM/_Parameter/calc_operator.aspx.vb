Public Class calc_operator
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        cekUserAuthentication()
        Dim typ = Request.QueryString("typ")
        DestinationControl.Value = Request.QueryString("destination")
        If Not IsPostBack Then
            isiData(typ)
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

    Sub isiData(ByVal typ)
        lstParameters.Items.Clear()
        lstParameters.Items.Add("")
        
        If typ = "javascript" Then
            lstParameters.Items.Insert(0, New ListItem("(", "("))
            lstParameters.Items.Insert(1, New ListItem("+", "+"))
            lstParameters.Items.Insert(2, New ListItem("-", "-"))
            lstParameters.Items.Insert(3, New ListItem("*", "*"))
            lstParameters.Items.Insert(4, New ListItem("/", "/"))
            lstParameters.Items.Insert(5, New ListItem("==", "=="))
            lstParameters.Items.Insert(6, New ListItem("!=", "!="))
            lstParameters.Items.Insert(7, New ListItem(">", ">"))
            lstParameters.Items.Insert(8, New ListItem(">=", ">="))
            lstParameters.Items.Insert(9, New ListItem("<", "<"))
            lstParameters.Items.Insert(10, New ListItem("<=", "<="))
            lstParameters.Items.Insert(11, New ListItem(")", ")"))
        Else
            lstParameters.Items.Insert(0, New ListItem("(", "("))
            lstParameters.Items.Insert(1, New ListItem("+", "+"))
            lstParameters.Items.Insert(2, New ListItem("-", "-"))
            lstParameters.Items.Insert(3, New ListItem("*", "*"))
            lstParameters.Items.Insert(4, New ListItem("/", "/"))
            lstParameters.Items.Insert(5, New ListItem("=", "="))
            lstParameters.Items.Insert(6, New ListItem("<>", "<>"))
            lstParameters.Items.Insert(7, New ListItem(">", ">"))
            lstParameters.Items.Insert(8, New ListItem(">=", ">="))
            lstParameters.Items.Insert(9, New ListItem("<", "<"))
            lstParameters.Items.Insert(10, New ListItem("<=", "<="))
            lstParameters.Items.Insert(11, New ListItem(")", ")"))
        End If

        

    End Sub

End Class