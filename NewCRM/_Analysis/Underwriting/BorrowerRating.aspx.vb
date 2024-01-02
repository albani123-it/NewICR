Public Class BorrowerRating
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Rating Nasabah"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        lblNotif.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        controlHome()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If
        If Session("appno") IsNot Nothing Then
            cekSector(Session("appno"))
            appno.Value = Session("appno")
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
            If Not IsPostBack Then
                cekApp(Session("appno"))
            End If
            cekWorkflow(Session("appno"))
            executeJavascript()
        Else
            dataTable = oApp.getListOfMenuBySector(sector)
            getMenuNext()
        End If
    End Sub

    Sub executeJavascript()
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    url = dataTable.Rows(i).Item("lsm_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "a_aBc('" & url & "','" & pagename & "','" & appno.Value & "');"
        Dim javascript = "<script type='text/javascript'>" & script & "</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
    End Sub

    Sub cekDeskripsi(ByVal rating, ByVal scc)
       
        Dim dsRat As DataSet
        Dim dtRat As DataTable
        dsRat = oApp.getRatingDesc(rating, scc)
        dtRat = dsRat.Tables(0)
        If dtRat.Rows.Count > 0 Then
            lblDeskripsi.Text = dtRat.Rows(0).Item("base_cat")
        End If
    End Sub

    Sub cekSector(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerSector(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            sector = dt.Rows(0).Item("brw_sec")
        End If
    End Sub

    Sub getMenuNext()
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            Dim dt As DataTable
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    dt = oApp.getNextMenuByWorkflowRole(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dataTable.Rows(i).Item("lsm_id"), sector)
                    If dt.Rows.Count > 0 Then
                        menuNext = dt.Rows(0).Item("lsm_link")
                    Else
                        Dim dt2 As DataTable
                        dt2 = oApp.getDetailOfMenu(dataTable.Rows(i).Item("lsm_id"))
                        If dt2.Rows.Count > 0 Then
                            menuNext = dt2.Rows(0).Item("lsm_link")
                        End If
                    End If
                End If
            Next
        Else
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lsm_name") = pagename Then
                    Dim dt2 As DataTable
                    dt2 = oApp.getDetailOfMenu(dataTable.Rows(i).Item("lsm_id"))
                    If dt2.Rows.Count > 0 Then
                        menuNext = dt2.Rows(0).Item("lsm_link")
                    End If
                End If
            Next
        End If

    End Sub

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        dt = oApp.getChildOfMenu(id, sector)
        If dt.Rows.Count > 0 Then
            html &= "<script type='text/javascript'>"
            For ch As Integer = 0 To dt.Rows.Count - 1
                html &= "$('#m" & id & "').click(function(){$('#c" & dt.Rows(ch).Item("lsm_position") & "').toggle();});"
            Next
            html &= "</script>"
        End If
        Return html
    End Function

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        dt = oApp.getChildOfMenu(id, sector)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                For j As Integer = 0 To dtWorkflow.Rows.Count - 1
                    If dtWorkflow.Rows(j).Item("lwr_field") = dt.Rows(i).Item("lsm_field") Then
                        If dtWorkflow.Rows(j).Item("lwr_role") = "HD" Then
                            count = count + 1
                        End If
                    End If
                Next
            Next
            If count <> dt.Rows.Count Then
                html &= "<li id='m" & id & "'><a href='#'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(row).Item("lsm_name") & "</span></a></li>"
            End If
        End If
        Return html
    End Function

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

    Sub controlHome()
        btnNext.Visible = False
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
                isiData(no, dtbl.Rows(0).Item("app_brw_code"), dtbl.Rows(0).Item("app_brw_scc"))
                brw_code.Value = dtbl.Rows(0).Item("app_brw_code")
            End If
            lblAppNo_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("app_no"))
            lblBrwName_header.Text = oGen.CheckNullString(dtbl.Rows(0).Item("lsb_namaPemohon"))
        End If
    End Sub

    Sub cekWorkflow(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_brw_code")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                execWorkflow(dtbl.Rows(0).Item("app_workflow"), dtbl.Rows(0).Item("app_position"), dtbl.Rows(0).Item("app_convent"))
                If usrLogin <> dtbl.Rows(0).Item("app_usr") Then
                    controlRO()
                End If
            End If
        End If
    End Sub

    Sub isiData(ByVal no, ByVal brwcode, ByVal scc)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerRating(brwcode)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblLastUpdate.Text = dt.Rows(0).Item("lbr_ratingDate")
            lblScore.Text = dt.Rows(0).Item("lbr_score")
            lblRating.Text = dt.Rows(0).Item("lbr_rating")
            score.Value = dt.Rows(0).Item("lbr_score")
            rating.Value = dt.Rows(0).Item("lbr_rating")
            txtRatingDate.Text = dt.Rows(0).Item("lbr_ratingDate")
            'txtNextRating.Text = dt.Rows(0).Item("lbr_nextRating")
            cekDeskripsi(dt.Rows(0).Item("lbr_rating"), scc)
        End If

        grid1.DataSource = ds
        grid1.DataBind()

        ds = oApp.getListOfRatingDesc(scc)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            dt.Columns.Add("lblScore")
            For i As Integer = 0 To dt.Rows.Count - 1
                If i = dt.Rows.Count - 1 Then
                    dt.Rows(i).Item("lblScore") = "<= " & dt.Rows(i).Item("base_upper") & ""
                Else
                    dt.Rows(i).Item("lblScore") = "> " & dt.Rows(i + 1).Item("base_upper") & " - " & dt.Rows(i).Item("base_upper")
                End If
            Next
        End If
        grid2.DataSource = dt
        grid2.DataBind()
    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet

        ds = oApp.checkWorkflowRole(workflow, posisi, sector)
        dtWorkflow = ds.Tables(0)
        For j As Integer = 0 To dtWorkflow.Rows.Count - 1
            If dtWorkflow.Rows(j).Item("lsm_name") = pagename Then
                If dtWorkflow.Rows(j).Item("lwr_role") = "RO" Then
                    controlRO()
                Else
                    If Not convent Then
                        If dtWorkflow.Rows(j).Item("lsm_edit") = 0 Then
                            controlRO()
                        ElseIf dtWorkflow.Rows(j).Item("lsm_edit") = 2 Then
                            controlEdit()
                        End If
                    End If
                End If
            End If

        Next
    End Sub

    Sub controlEdit()

    End Sub

    Sub controlRO()
        txtNextRating.Enabled = False
        txtRatingDate.Enabled = False
        btnRating.Visible = False
        btnSave.Visible = False
        btnNext.Visible = True
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If lblNotif.Text = "" Then
            If brw_code.Value <> "" Then
                Try
                    'Dim ratingdate As String = Mid(txtRatingDate.Text, 7, 4) + "-" & Mid(txtRatingDate.Text, 4, 2) + "-" & Mid(txtRatingDate.Text, 1, 2)
                    Dim ratingdate As String = Now.ToString("yyyy-MM-dd")
                    'Dim nextRating As String = Mid(txtNextRating.Text, 7, 4) + "-" & Mid(txtNextRating.Text, 4, 2) + "-" & Mid(txtNextRating.Text, 1, 2)
                    Dim nextRating As String = "1900-01-01"
                    oApp.insertBorrowerRating(brw_code.Value, score.Value, rating.Value, ratingdate, nextRating, usrLogin, scc.Value)
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                End Try
            End If
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""

        lMsg &= oGen.checkInputInaHTML("TextBox", "Skor", score.Value)
        lMsg &= oGen.checkInputInaHTML("TextBox", "Rating", rating.Value)
        lMsg &= oGen.checkInputInaHTML("TextBox", "Tgl. Rating", txtRatingDate.Text)

        If scc.Value = "" Then
            lMsg &= "Mohon melakukan rating kembali."
        End If
        'lMsg &= oGen.checkInputInaHTML("TextBox", "Tgl. Rating Selanjutnya", txtNextRating.Text)

        'If txtNextRating.Text <> "" Then
        '    Dim tgl As String = Mid(txtNextRating.Text, 7, 4) + "-" & Mid(txtNextRating.Text, 4, 2) + "-" & Mid(txtNextRating.Text, 1, 2)
        '    Dim result = DateTime.Compare(tgl, Now.ToString("yyyy-MM-dd"))
        '    If result < 0 Then
        '        lMsg &= "Tgl. Rating selanjutnya tidak boleh kurang dari hari ini."
        '    End If
        'End If

        If lMsg <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lMsg
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If
    End Sub

    Private Sub btnRating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRating.Click
        If appno.Value <> "" Then
           
            If brw_code.Value = "" Then
                Dim ds As DataSet
                Dim dt As DataTable
                ds = oApp.checkApp(appno.Value)
                dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    brw_code.Value = dt.Rows(0).Item("app_brw_code")
                End If
            End If

            Dim dset As DataSet
            Dim dtbl As DataTable
            dset = oApp.checkApp(appno.Value)
            dtbl = dset.Tables(0)
            If dtbl.Rows.Count > 0 Then
                oApp.insertBorrowerSummary(brw_code.Value, usrLogin)
                scc.Value = dtbl.Rows(0).Item("app_brw_scc")
                Dim dsRat As DataSet
                Dim dtRat As DataTable
                dsRat = oApp.getBorrowerRating(brw_code.Value, dtbl.Rows(0).Item("app_brw_scc"))
                dtRat = dsRat.Tables(0)
                If dtRat.Rows.Count > 0 Then
                    lblRating.Text = dtRat.Rows(0).Item("Rating")
                    rating.Value = dtRat.Rows(0).Item("Rating")
                    lblScore.Text = String.Format("{0:N2}", CDbl(dtRat.Rows(0).Item("Score")))
                    score.Value = String.Format("{0:N2}", CDbl(dtRat.Rows(0).Item("Score")))

                    txtRatingDate.Text = Now.ToString("dd-MM-yyyy")
                    txtNextRating.Text = ""
                    cekDeskripsi(rating.Value, dtbl.Rows(0).Item("app_brw_scc"))
                Else
                    liBodyLoadAction.Text = "alert('Data summary tidak ada.');window.location.href=window.location.href;"
                End If
            End If
        Else
            liBodyLoadAction.Text = "alert('No. Aplikasi tidak tersedia.');window.location.href=window.location.href;"
        End If
    End Sub
End Class