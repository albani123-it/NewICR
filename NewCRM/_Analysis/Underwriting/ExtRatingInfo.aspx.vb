Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports System.Data

Public Class ExtRatingInfo
    Inherits System.Web.UI.Page

    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim cs As ClientScriptManager = Page.ClientScript
    Public myTable As New DataTable
    Protected dataTable As DataTable
    Protected pagename As String = "External Rating Nasabah"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Dim usrLogin As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillDdl()

        End If
        InpRatingHid.Value = InpRating.SelectedValue

        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()

        controlHome()

        myTable.Columns.Add("txtId")
        myTable.Columns.Add("rincian")
        myTable.Columns.Add("asumsi")

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

    Sub controlHome()
        btnNext.Attributes.Add("style", "display:none;")
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

    Sub cekApp(ByVal no)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(no)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            If IsDBNull(dtbl.Rows(0).Item("app_no")) Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data nasabah belum tersedia.\nMohon mengisi data nasabah terlebih dahulu.');window.location.href='Borrower.aspx?appno=" & no & "';", True)
            Else
                isiData(dtbl.Rows(0).Item("app_brw_code"))
                appno.Value = dtbl.Rows(0).Item("app_no")
                brwcode.Value = dtbl.Rows(0).Item("app_brw_code")
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

    Sub isiData(ByVal brw)
        Dim ds As DataSet
        Dim dTbl As DataTable
        Dim dt As DataTable
        ds = oApp.checkExRatingInfo(brw)
        dt = ds.Tables(0)


        If dt.Rows.Count > 0 Then
            ddlHaveExtRat.SelectedValue = "Ya"
            InpRatingAgency.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lex_ratingAgency"))
            fillRating(InpRatingAgency.SelectedValue)
            InpCorp.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lex_corpType"))
            InpRating.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("lex_rating"))
            InpTglRating.Text = String.Format("{0:dd-MM-yyyy}", oGen.CheckNullString(dt.Rows(0).Item("lex_tglRating")))

            ratAgency.Attributes.Add("style", "display:'';")
            corType.Attributes.Add("style", "display:'';")
            rating.Attributes.Add("style", "display:'';")
            ratingDate.Attributes.Add("style", "display:'';")
            desc.Attributes.Add("style", "display:'';")
            'data_asumsi.Attributes.Add("style", "display:'';")
            'data_asumsi_dtl.Attributes.Add("style", "display:'';")

            dTbl = oRat.getExtInfoDtlByAppno(brw)



            If dTbl.Rows.Count > 0 Then

                txtId1.Text = oGen.CheckNullString(dTbl.Rows(0).Item("lexd_id"))
                'ddlRincian1.SelectedValue = oGen.CheckNullString(dTbl.Rows(0).Item("lexd_rincian"))
                txtAsumsi1.Text = oGen.CheckNullString(dTbl.Rows(0).Item("lexd_asumsi_desc"))

                For index = 1 To dTbl.Rows.Count - 1
                    Dim row As DataRow = myTable.NewRow()
                    row("txtId") = oGen.CheckNullString(dTbl.Rows(index).Item("lexd_id"))
                    row("rincian") = oGen.CheckNullString(dTbl.Rows(index).Item("lexd_rincian"))
                    row("asumsi") = oGen.CheckNullString(dTbl.Rows(index).Item("lexd_asumsi_desc"))

                    myTable.Rows.Add(row)
                Next

            End If

            bindDataSourceEdit()

        Else
            ddlHaveExtRat.SelectedValue = "Tidak"
            ratAgency.Attributes.Add("style", "display:none;")
            corType.Attributes.Add("style", "display:none;")
            rating.Attributes.Add("style", "display:none;")
            ratingDate.Attributes.Add("style", "display:none;")
            btnSave.Attributes.Add("style", "display:none;")
            desc.Attributes.Add("style", "display:none;")
            btnNext.Attributes.Add("style", "display:'';")
            'data_asumsi.Attributes.Add("style", "display:none;")
            'data_asumsi_dtl.Attributes.Add("style", "display:none;")
            'controlRO
        End If
    End Sub

    Sub bindDataSourceEdit()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()

        'For j As Integer = 0 To Repeater1.Items.Count - 1

        '    Dim txtId As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtId"), TextBox)
        '    Dim ddlRincian As GroupDropDownList.GroupDropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlRincian"), GroupDropDownList.GroupDropDownList)
        '    Dim txtAsumsi As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtAsumsi"), TextBox)

        '    txtId.Text = myTable.Rows(j)("txtId").ToString()
        '    ddlRincian.SelectedValue = myTable.Rows(j)("rincian").ToString()
        '    txtAsumsi.Text = myTable.Rows(j)("asumsi").ToString()

        '    Dim dt As DataTable
        '    dt = oRat.getOptGroupByOwner("ExtRating")
        '    ddlRincian.DataSource = dt
        '    ddlRincian.DataTextField = "text"
        '    ddlRincian.DataValueField = "id"
        '    ddlRincian.DataGroupField = "group_text"
        '    ddlRincian.DataBind()

        'Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        lblNotif.Text = ""
        cekInput()
        'If lblNotif.Text = "" Then
        '    Try
        '        Dim ds As DataSet
        '        Dim dt As DataTable
        '        ds = oApp.checkExRatingInfo(brwcode.Value)
        '        dt = ds.Tables(0)

        '        Dim _txtid1 = ""
        '        Dim _ddlRincian1 = ""
        '        Dim _txtAsumsi1 = ""
        '        Dim _txtid = ""
        '        Dim _ddlRincian = ""
        '        Dim _txtAsumsi = ""

        '        _txtid1 = txtId1.Text
        '        _ddlRincian1 = ddlRincian1.SelectedValue
        '        _txtAsumsi1 = txtAsumsi1.Text
        '        Dim dTbl As DataTable

        '        If dt.Rows.Count > 0 Then
        '            'update data
        '            If ddlHaveExtRat.SelectedValue = "Ya" Then
        '                oApp.updateExRatingInfo(Request, brwcode.Value, usrLogin)
        '                dTbl = oRat.saveExtInfoDtl(brwcode.Value, _ddlRincian1, _txtAsumsi1, _txtid1, usrLogin)
        '                txtId1.Text = dTbl.Rows(0).Item("lexd_id").ToString()

        '                If Repeater1.Items.Count > 0 Then
        '                    For Each item As RepeaterItem In Repeater1.Items
        '                        Dim txtId As TextBox = DirectCast(item.FindControl("txtId"), TextBox)
        '                        Dim ddlRincian As GroupDropDownList.GroupDropDownList = DirectCast(item.FindControl("ddlRincian"), GroupDropDownList.GroupDropDownList)
        '                        Dim txtAsumsi As TextBox = DirectCast(item.FindControl("txtAsumsi"), TextBox)

        '                        _txtid = txtId.Text
        '                        _ddlRincian = ddlRincian.SelectedValue.ToString()
        '                        _txtAsumsi = txtAsumsi.Text

        '                        dTbl = oRat.saveExtInfoDtl(brwcode.Value, _ddlRincian, _txtAsumsi1, _txtid, usrLogin)
        '                        txtId.Text = dTbl.Rows(0).Item("lexd_id").ToString()

        '                    Next

        '                End If

        '            Else
        '                oApp.deleteExRatingInfo(appno.Value)
        '            End If

        '        Else
        '            If ddlHaveExtRat.SelectedValue = "Ya" Then
        '                'insert data
        '                oApp.insertExRatingInfo(Request, brwcode.Value, usrLogin)
        '                dTbl = oRat.saveExtInfoDtl(brwcode.Value, _ddlRincian1, _txtAsumsi1, _txtid1, usrLogin)
        '                txtId1.Text = dTbl.Rows(0).Item("lexd_id").ToString()

        '                If Repeater1.Items.Count > 0 Then
        '                    For Each item As RepeaterItem In Repeater1.Items
        '                        Dim txtId As TextBox = DirectCast(item.FindControl("txtId"), TextBox)
        '                        Dim ddlRincian As GroupDropDownList.GroupDropDownList = DirectCast(item.FindControl("ddlRincian"), GroupDropDownList.GroupDropDownList)
        '                        Dim txtAsumsi As TextBox = DirectCast(item.FindControl("txtAsumsi"), TextBox)

        '                        _txtid = txtId.Text
        '                        _ddlRincian = ddlRincian.SelectedValue.ToString()
        '                        _txtAsumsi = txtAsumsi.Text

        '                        dTbl = oRat.saveExtInfoDtl(brwcode.Value, _ddlRincian, _txtAsumsi1, _txtid, usrLogin)
        '                        txtId.Text = dTbl.Rows(0).Item("lexd_id").ToString()

        '                    Next

        '                End If


        '            End If
        '        End If

        '        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');window.location.href='" & menuNext & "?appno=" & appno.Value & "';", True)
        '    Catch ex As Exception
        '        liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        '    End Try
        'End If
    End Sub

    Sub cekInput()
        Dim lNot As String = ""

        lNot &= oGen.checkInputInaHTML("DropDownList", "Rating Agency", InpRatingAgency.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Corporate Type", InpCorp.Text)
        lNot &= oGen.checkInputInaHTML("DropDownList", "Rating", InpRatingHid.Value)
        lNot &= oGen.checkInputInaHTML("TextBox", "Tgl. Tgl. Rating", InpTglRating.Text)
        If lNot <> "" Then
            'liBodyLoadAction.Text = "alert('" & lMsg & "');"
            lblNotif.Text = lNot
            divnotif.Attributes.Add("style", "display:''")
        End If
    End Sub

    Sub fillDdl()
        Dim dtAgency As DataTable
        dtAgency = oApp.getRatingAgency()
        InpRatingAgency.DataSource = dtAgency
        InpRatingAgency.DataTextField = "Agency"
        InpRatingAgency.DataValueField = "Agency"
        InpRatingAgency.DataBind()

        Dim dtCorp As DataTable
        dtCorp = oRat.getDropDownList("RFCORPTYPE")
        InpCorp.DataSource = dtCorp
        InpCorp.DataTextField = "ldd_desc"
        InpCorp.DataValueField = "ldd_code"
        InpCorp.DataBind()

        'dtCorp = oRat.getOptGroupByOwner("ExtRating")
        'ddlRincian1.DataSource = dtCorp
        'ddlRincian1.DataTextField = "text"
        'ddlRincian1.DataValueField = "id"
        'ddlRincian1.DataGroupField = "group_text"
        'ddlRincian1.DataBind()

    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet
        'Dim dt As DataTable
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
        ddlHaveExtRat.Enabled = False
        ddlHaveExtRat.CssClass = "inpDdlDisabled"
        InpRatingAgency.Enabled = False
        InpRatingAgency.CssClass = "inpDdlDisabled"
        InpCorp.Enabled = False
        InpCorp.CssClass = "inpDdlDisabled"
        InpRating.Enabled = False
        InpRating.CssClass = "inpDdlDisabled"
        InpTglRating.Enabled = False
        InpTglRating.CssClass = "inpTxtDisabled"
        btnSave.Visible = False
        btnNext.Visible = True
        btnNext.Attributes.Add("style", "display:'';")
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If appno.Value <> "" Then
            Response.Redirect(menuNext & "?appno=" & appno.Value)
        Else
            Response.Redirect(menuNext)
        End If

    End Sub

    Sub fillRating(ByVal Rating)
        Dim dtRating As DataTable
        dtRating = oApp.getRating(Rating)
        InpRating.DataSource = dtRating
        InpRating.DataTextField = "Rating"
        InpRating.DataValueField = "Rating"
        InpRating.DataBind()
    End Sub

    Protected Sub InpRatingAgency_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles InpRatingAgency.SelectedIndexChanged
        If ddlHaveExtRat.SelectedValue = "Ya" Then
            fillRating(InpRatingAgency.SelectedValue)
            ratAgency.Attributes.Add("style", "display:'';")
            corType.Attributes.Add("style", "display:'';")
            rating.Attributes.Add("style", "display:'';")
            ratingDate.Attributes.Add("style", "display:'';")
            btnSave.Attributes.Add("style", "display:'';")
            desc.Attributes.Add("style", "display:'';")
            'data_asumsi.Attributes.Add("style", "display:'';")
            'data_asumsi_dtl.Attributes.Add("style", "display:'';")
        End If

    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function getExternalDesc(ByVal Rating As String, ByVal Agency As String) As String
        Dim ResponseData As String = ""
        Dim dt As New DataTable

        With New lApps
            dt = .getExternalRatingDesc(Agency, Rating)
            If (Agency <> "Pilih" And Agency <> "") And (Rating <> "" And Rating <> "Pilih") Then
                With New lGeneral
                    ResponseData = .CheckNullString(dt.Rows(0).Item("desc"))
                End With
            End If
        End With

        Return ResponseData
    End Function

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        ratAgency.Attributes.Add("style", "display:'';")
        corType.Attributes.Add("style", "display:'';")
        rating.Attributes.Add("style", "display:'';")
        ratingDate.Attributes.Add("style", "display:'';")
        desc.Attributes.Add("style", "display:'';")
        btnSave.Attributes.Add("style", "display:'';")
        'data_asumsi.Attributes.Add("style", "display:'';")
        'data_asumsi_dtl.Attributes.Add("style", "display:'';")

        PopulateData()
        myTable.Rows.Add(myTable.NewRow())

        Bind_Add()

    End Sub

    Protected Sub Repeater1_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs) Handles Repeater1.ItemCommand
        PopulateData()
        Dim x = myTable.Rows(e.Item.ItemIndex).ItemArray(0)
        oRat.deleteExtInfoDtlById(x, brwcode.Value)
        myTable.Rows(e.Item.ItemIndex).Delete()

        Bind_Add()
    End Sub

    Protected Sub PopulateData()
        For Each item As RepeaterItem In Repeater1.Items
            Dim ddlRincian As DropDownList = DirectCast(item.FindControl("ddlRincian"), DropDownList)

            Dim txtid As TextBox = DirectCast(item.FindControl("txtId"), TextBox)
            Dim txtAsumsi As TextBox = DirectCast(item.FindControl("txtAsumsi"), TextBox)

            Dim row As DataRow = myTable.NewRow()
            row("txtId") = txtid.Text
            row("rincian") = ddlRincian.SelectedValue.ToString
            row("asumsi") = txtAsumsi.Text

            myTable.Rows.Add(row)
        Next
    End Sub

    Protected Sub Bind_Add()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()

        'For j As Integer = 0 To Repeater1.Items.Count - 1

        '    Dim ddlRincian As GroupDropDownList.GroupDropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlRincian"), GroupDropDownList.GroupDropDownList)

        '    ddlRincian.SelectedIndex = ddlRincian.Items.IndexOf(ddlRincian.Items.FindByValue(myTable.Rows(j)("rincian").ToString()))
        '    ddlRincian.DataBind()

        '    Dim dt As DataTable
        '    dt = oRat.getOptGroupByOwner("ExtRating")
        '    ddlRincian.DataSource = dt
        '    ddlRincian.DataTextField = "text"
        '    ddlRincian.DataValueField = "id"
        '    ddlRincian.DataGroupField = "group_text"
        '    ddlRincian.DataBind()

        'Next

    End Sub

End Class