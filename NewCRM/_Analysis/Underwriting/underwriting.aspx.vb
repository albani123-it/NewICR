Imports System.Data.SqlClient

Public Class underwriting
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Public allowDetail As Boolean
    Public allowLogs As Boolean

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
        grid1.AllowAddingRecords = False
        allowDetail = False
        allowLogs = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und_new" Then
                    grid1.AllowAddingRecords = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und_det" Then
                    allowDetail = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und_log" Then
                    allowLogs = True
                End If
            Next
            If Not tf Then
                Session.Abandon()
                Response.Redirect("../../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../../Login.aspx")
        End Try
    End Sub

    Sub isiData()
        Dim ds As DataSet
        ds = oApp.getListOfAssignApp(usrLogin)
        grid1.DataSource = ds
        grid1.DataBind()

        'ds = oApp.getListOfSubmitApp(usrLogin)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    grid2.DataSource = ds
        '    grid2.DataBind()
        '    spnAnalyst.Visible = True
        'Else
        '    grid2.Visible = False
        '    spnAnalyst.Visible = False
        'End If
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function encrpytAppNo(ByVal appno As String) As String
        Dim result As String = ""

        With New lFunction
            result = .EncryptString(appno)
        End With

        Return result
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function checkAppModel(ByVal appno As String) As String
        Dim result As String = appno
        Dim brw_typ As String = ""
        Dim prd_typ As String = ""

        With New lApps
            Dim oGen As New lGeneral
            Dim dset As DataSet
            Dim dtbl As DataTable
            dset = .checkAppModel(appno)
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                brw_typ = oGen.CheckNullString(dtbl.Rows(0).Item("brw_id"))
                prd_typ = oGen.CheckNullString(dtbl.Rows(0).Item("prd_id"))
            End If
        End With

        result = result & "," & brw_typ & "," & prd_typ
        Return result
    End Function
End Class