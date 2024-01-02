Public Class tacticalAccountPlanning
    Inherits System.Web.UI.Page
    Dim oTap As New lTacticalAccountPlanning
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oRat As New lRating
    Dim oUsr As New lUsers
    Dim oPrdOffer As New lProductOffer
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected dataTable As DataTable
    Protected pagename As String = "Tactical Account Planning"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected product As String = ""
    Protected sector As String = ""
    Protected allowDelete As Boolean
    Public myTable As New DataTable

    Public Shared _me As New tacticalAccountPlanning

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        lblNotif.Text = ""
        allowDelete = True
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))

        myTable.Columns.Add("value")
        myTable.Columns.Add("text")

        cekUserAuthentication()
        controlHome()

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        End If

        If Session("appno") IsNot Nothing Then
            'cekProduct(Session("appno"))
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

        If Not IsPostBack Then
            FillDropdown()
            GetThnPeriode()
            GetDataTap(appno.Value)
            bindGrid()
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

    Sub cekSector(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerSector(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            sector = dt.Rows(0).Item("brw_sec")
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

    Sub controlHome()
        Dim dtTable(9) As String

        dtTable = oUsr.getUserDetail(usrLogin)

        'btnNext.Visible = True
    End Sub

    Sub fillDdl()
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oApp.getListOfBorrowerType()
        dt = ds.Tables(0)
    End Sub

    Sub bindGrid()
        Dim ds As DataSet
        ds = oTap.getDataProductOffer
        'grid1.DataSource = ds
        'grid1.DataBind()
    End Sub

    Sub cekProduct(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkGeneral(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            product = dt.Rows(0).Item("lblsegment")
        End If
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
                isiData(no)
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

    Sub isiData(ByVal appno)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oTap.getDataTapByAppno(appno)
        dt = ds.Tables(0)

        bindDataSourceEdit()

    End Sub

    Sub bindDataSourceEdit()
    End Sub

    Sub execWorkflow(ByVal workflow, ByVal posisi, ByVal convent)
        Dim ds As DataSet
        'ds = oApp.checkWorkflowRole(workflow, posisi, product)
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

    Sub controlRO()
        btnSave.Visible = False
        'btnNext.Visible = True
        txtTanggal.Enabled = False
        ddlUnitPengusul.Enabled = False
        ddlUnitPengusul.CssClass = "inpDdlDisabled"
        txtFunding.Enabled = False
        txtLending.Enabled = False
        txtServices.Enabled = False
        txtCatatan.Enabled = False
        cs.RegisterStartupScript(Page.[GetType](), "callFunction", "callReadOnly();", True)
    End Sub

    Sub controlEdit()
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

    Function checkChildHD(ByVal id, ByVal row)
        Dim dt As DataTable
        Dim html As String = ""
        Dim count As Integer = 0
        'dt = oApp.getChildOfMenu(id, product)
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

    Function getMenuChild(ByVal id)
        Dim dt As DataTable
        Dim html As String = ""
        'dt = oApp.getChildOfMenu(id, product)
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

    Sub fillDropdownByWs(ByVal kode)
        Dim ds As DataSet

        ds = oTap.getDataDropdownWs(kode)
        ddlNamaProduk.DataSource = ds
        ddlNamaProduk.DataValueField = "value"
        ddlNamaProduk.DataTextField = "text"
        ddlNamaProduk.DataBind()
    End Sub

    Sub FillDropdown()
        Dim ds As DataSet

        ds = oPrdOffer.getDropDownlistData("TYPEOFF")
        ddlKategoriProduk.DataSource = ds
        ddlKategoriProduk.DataValueField = "dd_value"
        ddlKategoriProduk.DataTextField = "dd_text"
        ddlKategoriProduk.DataBind()

        Dim dt As DataTable
        dt = oRat.getDropDownList("RFUNIT")
        ddlUnitPengusul.Items.Clear()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlUnitPengusul.Items.Add(New ListItem(dt.Rows(i).Item("ldd_desc"), dt.Rows(i).Item("ldd_code")))
            Next
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        PopulateData()
        myTable.Rows.Add(myTable.NewRow())
        Bind_Add()

    End Sub

    Protected Sub PopulateData()

    End Sub

    Protected Sub Bind_Add()


    End Sub

    Protected Sub btnSavedWS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavedWS.Click
        CekInput()

        If liBodyLoadAction.Text = "" Then
            Try
                Dim ds As DataSet
                ds = oTap.insertTapAndWs(Request, appno.Value, usrLogin)
                txtTapKode.Text = ds.Tables(0).Rows(0).Item("ltapw_ltap_kode").ToString()
                GetDataWalletShare(ds.Tables(0).Rows(0).Item("ltapw_ltap_kode").ToString())
                fillDropdownByWs(ds.Tables(0).Rows(0).Item("ltapw_ltap_kode").ToString())
                'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil disimpan.');", True)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If

    End Sub

    Protected Sub btnSavedAP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavedAP.Click

        If liBodyLoadAction.Text = "" Then
            Try
                Dim prospek As Integer

                If chkProspek.Checked = True Then
                    prospek = 1
                Else
                    prospek = 0
                End If


                Dim ds As DataSet
                ds = oTap.insertActionPlan(Request, usrLogin, prospek)
                grid2.DataSource = ds
                grid2.DataBind()
                'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil disimpan.');", True)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');", True)
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If

    End Sub

    Sub CekInput()
        Dim lMsg As String = ""

        lMsg &= oGen.checkInputInaJavascript("TextBox", "Tanggal", txtTanggal.Text)
        lMsg &= oGen.checkInputInaJavascript("DropDownList", "Unit Pengusul", ddlUnitPengusul.SelectedValue)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Funding", txtFunding.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Lending", txtLending.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Service", txtServices.Text)
        lMsg &= oGen.checkInputInaJavascript("TextBox", "Nama Produk", txtNamaProduk.Text)

        If lMsg <> "" Then
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + lMsg + "');", True)
        End If
    End Sub

    Sub GetThnPeriode()
        Dim today = Date.Now()
        Dim year = today.Year

        For index = year - 5 To year - 1
            Dim row As DataRow = myTable.NewRow()
            row("value") = index
            row("text") = index
            myTable.Rows.Add(row)
        Next

        For index = year To year + 5
            Dim row As DataRow = myTable.NewRow()
            row("value") = index
            row("text") = index
            myTable.Rows.Add(row)
        Next

        ddlPeriode1.DataSource = myTable
        ddlPeriode1.DataTextField = "text"
        ddlPeriode1.DataValueField = "value"
        ddlPeriode1.DataBind()
        ddlPeriode1.SelectedValue = year

        ddlPeriode2.DataSource = myTable
        ddlPeriode2.DataTextField = "text"
        ddlPeriode2.DataValueField = "value"
        ddlPeriode2.DataBind()
        ddlPeriode2.SelectedValue = year

        ddlThnMulai.DataSource = myTable
        ddlThnMulai.DataTextField = "text"
        ddlThnMulai.DataValueField = "value"
        ddlThnMulai.DataBind()
        ddlThnMulai.SelectedValue = year

        ddlThnSelesai.DataSource = myTable
        ddlThnSelesai.DataTextField = "text"
        ddlThnSelesai.DataValueField = "value"
        ddlThnSelesai.DataBind()
        ddlThnSelesai.SelectedValue = year

    End Sub

    Sub GetDataTap(ByVal appno)
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = oTap.getDataTap(appno)
            dt = ds.Tables(0)

            txtTanggal.Text = IIf(oGen.CheckNullString(dt.Rows(0).Item("ltap_tanggal")) = "", "", String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("ltap_tanggal")))
            txtTapKode.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_kode"))
            ddlUnitPengusul.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("ltap_unit_pengusul"))
            txtFunding.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_funding"))
            txtLending.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_lending"))
            txtServices.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_services"))
            txtCatatan.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_catatan"))

            GetDataWalletShare(dt.Rows(0).Item("ltap_kode"))
            GetDataActionPlan(dt.Rows(0).Item("ltap_kode"))
            fillDropdownByWs(dt.Rows(0).Item("ltap_kode"))

        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        End Try
    End Sub

    Sub GetDataWalletShare(ByVal kode)
        Try
            Dim ds As DataSet
            ds = oTap.getDataWs(kode)
            grid1.DataSource = ds
            grid1.DataBind()
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        End Try
    End Sub

    Sub GetDataActionPlan(ByVal kode)
        Try
            Dim ds As DataSet
            ds = oTap.getDataActionPlan(kode)
            grid2.DataSource = ds
            grid2.DataBind()
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
        End Try
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function getDataWalledShareById(ByVal Id) As String
        Return _me.GetDataById(Id)
    End Function

    Function GetDataById(ByVal Id)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oTap.getDataWalletShareById(Id)
        dt = ds.Tables(0)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))
        Dim row As Dictionary(Of String, Object)

        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function checkWalletShare(ByVal kode) As String
        Return _me.getkWalletShare(kode)
    End Function

    Function getkWalletShare(ByVal kode)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oTap.getDataWs(kode)
        dt = ds.Tables(0)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))
        Dim row As Dictionary(Of String, Object)

        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function getDataAPById(ByVal Id) As String
        Return _me.GetDataAPId(Id)
    End Function

    Function GetDataAPId(ByVal Id)
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oTap.getDataActionPlanById(Id)
        dt = ds.Tables(0)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))
        Dim row As Dictionary(Of String, Object)

        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim checked As String
        checked = checkInput()
        If checked = "" Then
            Try
                Dim ds As DataSet
                Dim dt As DataTable

                ds = oTap.insertTacticalAccountPlanning(Request, appno.Value, usrLogin)
                dt = ds.Tables(0)
                txtTanggal.Text = String.Format("{0:dd-MM-yyyy}", dt.Rows(0).Item("ltap_tanggal"))
                txtTapKode.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_kode"))
                ddlUnitPengusul.SelectedValue = oGen.CheckNullString(dt.Rows(0).Item("ltap_unit_pengusul"))
                txtFunding.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_funding"))
                txtLending.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_lending"))
                txtServices.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_services"))
                txtCatatan.Text = oGen.CheckNullString(dt.Rows(0).Item("ltap_catatan"))

                'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data berhasil disimpan.');", True)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiBerhasil() + "');", True)
            Catch ex As Exception
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + ex.Message + "');", True)
            End Try
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + (checked.Replace("&bull;&nbsp;", "")).Replace("<br/>", "") + "');", True)
        End If
    End Sub

    Function checkInput()
        Dim text As String = ""
        text &= oGen.checkInputInaHTML("DropDownList", "Unit Pengusul", ddlUnitPengusul.SelectedValue)
        Return text
    End Function

    Protected Sub deleteWS(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)

        Try
            oTap.deleteWalletShareById(dataItem("ltapw_id"))
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiHapus() + "');", True)
            GetDataWalletShare(txtTapKode.Text)

        Catch ex As Exception
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + ex.Message + "');", True)
        End Try

    End Sub

    Protected Sub deleteAP(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid2.Rows(rowIndex).ToHashtable(), Hashtable)

        Try
            oTap.deleteAccountPlanningById(dataItem("ltapa_id"))
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + oGen.NotifikasiHapus() + "');", True)
            GetDataActionPlan(txtTapKode.Text)

        Catch ex As Exception
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + ex.Message + "');", True)
        End Try

    End Sub

End Class