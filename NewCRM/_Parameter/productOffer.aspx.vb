Public Class productOffer
    Inherits System.Web.UI.Page

    Dim oPrdOffer As New lProductOffer
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
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_brw_mgt_view" Then
                    grid1.Columns(7).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_delete" Then
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
        ds = oPrdOffer.getDataProductOffer
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = oPrdOffer.getDataProductOfferById(dataItem("po_id"))
        Dim dt As DataTable = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            'delete
            Try
                oPrdOffer.deleteDataProductOfferingById(dataItem("po_id"))
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/productOffer.aspx';", True)
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "")
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Parameter/productOffer.aspx';", True)
            End Try

        End If

    End Sub

End Class