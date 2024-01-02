Public Class R_IndividualShareholders
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oRerat As New lRerating
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected allowDelete As Boolean = True
    Protected dataTable As DataTable
    Protected pagename As String = "Pemegang Saham Perorangan"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected app_no As String = ""
    Protected product As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        ElseIf Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
            brw_code.Value = Session("brwcode")
        End If

        dataTable = oRerat.getListOfMenu("Borrower")

        If Session("brwcode") IsNot Nothing And Not IsPostBack Then
            cekApp(brw_code.Value)
            isiData(Session("brwcode"))
            getMenuNext("Borrower")
            executeJavascript()
        End If
    End Sub

    Sub executeJavascript()
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        dset = oApp.checkAppByBrwCode(brw_code.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                    url = dataTable.Rows(i).Item("lmr_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & brw_code.Value & "', 'Borrower');"
        Dim javascript = "<script type='text/javascript'>" & script & "</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_anl_rat" Then
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

    Sub getMenuNext(ByVal typ)
        For i As Integer = 0 To dataTable.Rows.Count - 1
            If dataTable.Rows(i).Item("lmr_name") = pagename Then
                Dim dt2 As DataTable
                dt2 = oRerat.getNextMenu(dataTable.Rows(i).Item("lmr_id"), typ)
                If dt2.Rows.Count > 0 Then
                    menuNext = dt2.Rows(0).Item("lmr_link")
                End If
            End If
        Next
    End Sub

    Sub isiData(ByVal brwcode)
        Dim ds As DataSet
        ds = oApp.checkIndividualShareholders(brwcode)

        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        Dim typ = dataTable.Rows(0).Item("lmr_type").ToString()

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lmr_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        Dim typ = dataTable.Rows(0).Item("lmr_type").ToString()

        dt = oRerat.getChildOfMenu(id, typ)

        If dt.Rows.Count > 0 Then
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lmr_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Dim ds As DataSet = oApp.getDetailOfIndividualShareholders(dataItem("lis_id"))
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        Try
            oApp.deleteIndividualShareholders(dataItem("lis_id"))
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil dihapus.');window.location.href='IndividualShareholders.aspx?appno=" & appno.Value & "';", True)
        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='IndividualShareholders.aspx?appno=" & appno.Value & "';", True)
        End Try
    End Sub

    Sub cekApp(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oApp.checkTotalCorporateShareholders(brw_code.Value)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblNotif.Text = "Jumlah Pemegang Saham Badan : " & dt.Rows(0).Item("lcs_kepemilikanSaham") & " % <br/>"
            divnotif.Attributes.Add("style", "display:''")

            If CDbl(dt.Rows(0).Item("lcs_kepemilikanSaham")) = 100 Then
                grid1.AllowAddingRecords = False
            End If

        End If

        Dim ds2 As DataSet
        Dim dt2 As DataTable
        ds2 = oApp.checkTotalIndividualShareholders(brw_code.Value)
        dt2 = ds2.Tables(0)
        If dt2.Rows.Count > 0 Then
            lblNotif.Text &= "Jumlah Pemegang Saham Perorangan : " & dt2.Rows(0).Item("lis_kepemilikanSaham") & " %"
            divnotif.Attributes.Add("style", "display:''")

            If CDbl(dt.Rows(0).Item("lcs_kepemilikanSaham") + dt2.Rows(0).Item("lis_kepemilikanSaham")) >= 100 Then
                grid1.AllowAddingRecords = False
            End If
            sisa.Value = CDbl(dt.Rows(0).Item("lcs_kepemilikanSaham") + dt2.Rows(0).Item("lis_kepemilikanSaham"))
        End If
    End Sub

End Class