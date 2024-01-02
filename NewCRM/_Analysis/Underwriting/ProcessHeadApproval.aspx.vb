Public Class ProcessHeadApproval
    Inherits System.Web.UI.Page
    Public allowDetail As Boolean
    Dim cs As ClientScriptManager = Page.ClientScript
    Public allowLogs As Boolean
    Dim oPa As New lProcessApproval

#Region " Global Variable "
    Public usrLogin As String
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))

        isiData(usrLogin)
    End Sub

    Sub isiData(ByVal usrLogin)
        Dim ds As DataSet
        'Dim ds2 As DataSet
        'Dim dt2 As DataTable

        ds = oPa.getListProcessApproval(usrLogin)
        grid1.DataSource = ds
        grid1.DataBind()


    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        allowDetail = False
        allowLogs = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und" Then
                    tf = True
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