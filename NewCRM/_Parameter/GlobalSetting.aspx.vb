Public Class GlobalSetting
    Inherits System.Web.UI.Page

#Region " Global Variable "
    Dim usrLogin As String
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        urlBase = Session("baseUrl")
        GetDataListOfGlobalSetting()
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_mgt" Then
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

    Sub GetDataListOfGlobalSetting()
        With New lglobalSettings
            Dim ds As New DataTable
            ds = .getListGlobalSetting()
            GloSetGrid.DataSource = ds
            GloSetGrid.DataBind()
        End With
    End Sub

    Protected Sub manage(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(GloSetGrid.Rows(rowIndex).ToHashtable(), Hashtable)

        cs.RegisterStartupScript(Page.[GetType](), "PopUp", "callPopUp(" & dataItem("gls_id") & ");", True)
    End Sub

End Class