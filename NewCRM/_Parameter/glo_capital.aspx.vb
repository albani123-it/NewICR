Public Class glo_capital
    Inherits System.Web.UI.Page
    Dim oGlo As New lglobalSettings
    Dim objLog As New lLog
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_capital" Then
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
        Dim ds As DataSet
        ds = oGlo.getListGloCapital
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = oGlo.getDetailOfGlobalCapital(dataItem("id"))
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oGlo.getDataGlobalCapitalUsed(dataItem("id"))
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Can\'t delete this time. Data still being in used');window.location.href=window.location.href';", True)
        Else
            Try
                oGlo.deleteGlobalCapital(dataItem("id"), usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Bank Capital Deletion")
                objLog.insertLog("glo_bank_capital", logs)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been successfully deleted.');window.location.href=window.location.href;", True)
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "")
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href=window.location.href;", True)
            End Try
        End If
    End Sub
End Class