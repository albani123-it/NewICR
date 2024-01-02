Public Class glo_financial
    Inherits System.Web.UI.Page
    Dim oFin As New lFinancial
    Dim oFun As New lFunction
    Dim objLog As New lLog
    Protected AllowDelete As Boolean
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()
        If Not IsPostBack Then
            bindGrid()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        grid1.AllowAddingRecords = False
        grid1.Columns(6).AllowEdit = False
        AllowDelete = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_edit" Then
                    grid1.Columns(6).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_delete" Then
                    AllowDelete = True
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
        ds = oFin.getListOfFinancialStatement
        ds.Tables(0).Columns.Add("acc_code_en")
        For Each dr In ds.Tables(0).Rows
            dr("acc_code_en") = oFun.EncryptString(dr("acc_code"))
        Next
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Try
            oFin.deleteFinancialStatement(dataItem("acc_code"), usrLogin)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/glo_financial.aspx';", True)
        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Parameter/glo_financial.aspx';", True)
        End Try
    End Sub
End Class