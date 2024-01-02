Public Class stressTest
    Inherits System.Web.UI.Page
    Dim oStress As New lStress
    Protected allowDelete As Boolean = False
    Protected allowEdit As Boolean = False
    Protected viewResult As Boolean = False
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim urlBase As String

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        'cekUserAuthentication()
        If Not IsPostBack Then
            bindGrid()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        grid1.AllowAddingRecords = False
        allowDelete = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_stress_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_stress_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_stress_edit" Then
                    allowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_stress_result" Then
                    viewResult = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_stress_delete" Then
                    allowDelete = True
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

    Sub bindGrid()
        Dim ds As DataSet
        ds = oStress.getDataStress
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Try
            oStress.deleteStress(dataItem("ssm_id"))
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil dihapus.');window.location.href='stressTest.aspx'", True)
        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='stressTest.aspx'", True)
        End Try
    End Sub
End Class