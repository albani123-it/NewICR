Imports System.Web.Script.Services
Imports Obout.Grid
Imports Obout.Interface

Public Class dataMgmt
    Inherits System.Web.UI.Page
    Dim oDm As New lDataMgmt
    Dim objLog As New lLog
    Dim urlBase As String
    Protected AllowDelete As Boolean
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
        grid1.Columns(5).AllowEdit = False
        AllowDelete = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_view" Then
                    grid1.Columns(5).AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_delete" Then
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
        ds = oDm.getListDataMgmt
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim lMsg As String = ""

        'check in Asset
        Dim dsC As DataSet
        Dim dtC As DataTable
        dsC = oDm.chkExistsInAsset(dataItem("data_id"))
        dtC = dsC.Tables(0)
        If dtC.Rows.Count > 0 Then
            lMsg &= "Data Management still use in Asset Category.\n"
        End If

        'check in Rule
        Dim dsC2 As DataSet
        Dim dtC2 As DataTable
        dsC2 = oDm.chkExistsInRule(dataItem("data_id"))
        dtC2 = dsC2.Tables(0)
        If dtC2.Rows.Count > 0 Then
            lMsg &= "Data Management still use in Rule Management.\n"
        End If

        If lMsg <> "" Then
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & lMsg & "');window.location.href='" & urlBase & "_Parameter/dataMgmt.aspx';", True)
        Else
            Dim ds As DataSet = oDm.getDataManagementDetail(dataItem("data_id"))
            Dim dr As DataRow = ds.Tables(0).Rows(0)

            Try
                oDm.deleteDataManagement(dataItem("data_id"), dr.Item("data_name"), usrLogin)
                Dim stat As String
                If dr.Item("is_Active") = True Then
                    stat = "Active"
                Else
                    stat = "Inactive"
                End If
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Data Management Deletion - NAME=" & dr.Item("data_name") & ";TYPE=" & dr.Item("data_type") & ";STATUS=" & stat)
                objLog.insertLog("dataMgmt", logs)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/dataMgmt.aspx';", True)

            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='" & urlBase & "_Parameter/dataMgmt.aspx';", True)
            End Try
        End If




    End Sub
End Class