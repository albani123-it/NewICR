Public Class tracking
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()

        Session.Remove("appno")
        If Not IsPostBack Then
            isiData()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_tracking" Then
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

    Sub isiData()
        Dim ds As DataSet
        ds = oApp.getListOfApp()
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function encrpytAppNo(ByVal appno As String) As String
        Dim result As String = ""

        With New lFunction
            result = .EncryptString(appno)
        End With

        Return result
    End Function
End Class