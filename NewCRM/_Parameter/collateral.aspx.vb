Imports System.Web.Script.Services
Imports Obout.Grid
Imports Obout.Interface

Public Class collateral
    Inherits System.Web.UI.Page
    Dim oColl As New lCollateral
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
            BindGrid()
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
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_view" Then
                    grid1.Columns(6).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_coll_delete" Then
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

    Sub BindGrid()
        Dim ds As DataSet
        ds = oColl.getDataCollateral
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = oColl.getDetailCollateral(dataItem("coll_id"))
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        Try
            oColl.deleteCollateral(dataItem("coll_id"), usrLogin)
            Dim stat As String
            If dr.Item("coll_is_active") = True Then
                stat = "Active"
            Else
                stat = "Inactive"
            End If
            Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Collateral Classification Deletion - TYPE=" & dataItem("coll_type") & ";CLASS=" & dataItem("coll_class") & ";DATASOURCE=" & dr.Item("coll_source") & ";STATUS=" & stat)
            objLog.insertLog("collateral", logs)
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/collateral.aspx';", True)

        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Parameter/collateral.aspx';", True)
        End Try


    End Sub
End Class