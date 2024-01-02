Imports Obout.Grid
Imports Obout.Interface

Public Class glo_financial_value
    Inherits System.Web.UI.Page
    Dim oFin As New lFinancial
    Dim oFun As New lFunction
    Dim cs As ClientScriptManager = Page.ClientScript

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cekUserAuthentication()
        If Request.QueryString("src") IsNot Nothing And Request.QueryString("src") <> "" Then
            DestinationControl.Value = Request.QueryString("src")
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Please choose source destination.');window.close();", True)
        End If

        If Not Page.IsPostBack Then
            BindGrid()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_fin_edit" Then
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

    Sub BindGrid()
        Dim ds As DataSet = oFin.getListOfFinancialStatement()
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub
End Class