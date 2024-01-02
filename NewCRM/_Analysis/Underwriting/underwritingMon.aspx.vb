Public Class underwritingMon
    Inherits System.Web.UI.Page
    Dim oFlo As New lAppsFlow
    Dim oFun As New lFunction
    Dim oActLog As New lActLog
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()

        If Request.QueryString("appno") IsNot Nothing Then
            If Not IsPostBack Then
                isiData(oFun.DecryptString(Request.QueryString("appno")))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_und" Then
                    tf = True
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

    Sub isiData(ByVal appno)
        Dim ds As DataSet
        ds = oFlo.getListOfAppFlow(appno)
        grid1.DataSource = ds
        grid1.DataBind()

        ds = oActLog.getListOfActivityLog(appno)
        grid2.DataSource = ds
        grid2.DataBind()
    End Sub

    Protected Sub export(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid2.Rows(rowIndex).ToHashtable(), Hashtable)

        If Request.QueryString("appno") IsNot Nothing Then
            Dim dt As DataTable = oActLog.getDetailOfActivityLog(dataItem("code"), oFun.DecryptString(Request.QueryString("appno")))

            If dt.Rows.Count() > 0 Then
                Response.Clear()
                'How add the data from the Grid to csv table
                Dim i As Integer = 0
                Dim j As Integer = 0
                For Each dr As DataRow In dt.Rows
                    If i = 0 Then
                        For Each dc As DataColumn In dt.Columns
                            If j > 0 Then
                                Response.Write("|")
                            End If
                            Response.Write(dc.ColumnName)
                            j += 1
                        Next
                    End If
                    Response.Write(vbLf)
                    j = 0
                    For Each dc As DataColumn In dt.Columns
                        If j > 0 Then
                            Response.Write("|")
                        End If
                        Response.Write(dr.Item(dc.ColumnName))
                        j += 1
                    Next
                    i += 1
                Next

                ' Send the data and the appropriate headers to the browser        
                Response.AddHeader("content-disposition", "attachment;filename=Skyworx_activity_log.csv")
                Response.ContentType = "text/csv"
                Response.[End]()
            End If
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('No. Aplikasi tidak ada.');window.location.href=window.location.href';", True)
            'liBodyLoadAction.Text = "alert('No. Aplikasi tidak ada');"
        End If
    End Sub
End Class