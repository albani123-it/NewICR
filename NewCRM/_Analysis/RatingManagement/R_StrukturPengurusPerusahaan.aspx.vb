Public Class R_StrukturPengurusPerusahaan
    Inherits System.Web.UI.Page
  
    Dim usrLogin As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oRerat As New lRerating
    Dim oSPP As New lStrukturPP
    Protected sector As String = ""
    Protected allowDelete As Boolean
    Protected dataTable As DataTable
    Protected pagename As String = "Struktur Pengurus Perusahaan"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected app_no As String = ""
    Protected product As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        allowDelete = True
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        ElseIf Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
        End If

        If Session("brwcode") IsNot Nothing And Not IsPostBack Then
            dataTable = oRerat.getListOfMenu("Borrower")
            checkAppByBrwCode(Session("brwcode"))
            isiData(appno.Value)
            brw_code.Value = Session("brwcode")
            getMenuNext("Borrower")
            executeJavascript()
        End If
        If Not IsPostBack Then
            cekApp(Session("brwcode"))
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

    Sub checkAppByBrwCode(ByVal brwcode)

        Dim ds As DataSet
        ds = oApp.checkAppByBrwCode(brwcode)

        If ds.Tables(0).Rows.Count > 0 Then
            appno.Value = ds.Tables(0).Rows(0).Item("app_no")
        End If

    End Sub

    Sub cekProduct()
        Dim ds As DataSet
        Dim dt As DataTable
        If Request.QueryString("typ") = "brw" Then
            product = "None"
        Else
            ds = oRat.checkGeneral(Session("appno"))
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                product = dt.Rows(0).Item("lblProductType")
            End If
        End If
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
    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                'execWorkflow(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_convent"))
                isiData(dtbl.Rows(0).Item("app_brw_code"))
                brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
                If usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If
            End If
        End If
    End Sub
    Sub isiData(ByVal no)
        Dim ds As DataSet
        ds = oSPP.getDataStrukturPerusahaan(no)

        grid1.DataSource = ds
        grid1.DataBind()
    End Sub
   
    Sub controlRO()
        grid1.AllowAddingRecords = False
        grid1.Columns(6).AllowEdit = False
        allowDelete = False
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        'Dim ds As DataSet = oApp.getDetailOfLegalitas(dataItem("lsl_id"))
        Dim ds As DataSet = oRat.getDetailOfLegalitas(dataItem("lsl_id"))
        Dim dr As DataRow = ds.Tables(0).Rows(0)

        Try
            'oApp.deleteLegalitas(dataItem("lsl_id"))
            oRat.deleteLegalitas(dataItem("lsl_id"))
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil dihapus.');window.location.href='R_Legalitas.aspx?brwcode=" & Session("brwcode") & "';", True)
        Catch ex As Exception
            Dim msg As String = ex.Message.Replace("'", "")
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & msg & ".');window.location.href='R_Legalitas.aspx?brwcode=" & Session("brwcode") & "';", True)
        End Try
    End Sub
End Class