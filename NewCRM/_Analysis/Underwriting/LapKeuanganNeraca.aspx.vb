Imports System.Data.SqlClient

Public Class LapKeuanganNeraca
    Inherits System.Web.UI.Page

    Dim oUsr As New lUsers
    Dim oApp As New lApps
    Dim oGen As New lGeneral
    Dim oLap As New lLapKeuangan
    Protected dataTable As DataTable
    Protected pagename As String = "Neraca"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Protected sector As String = ""
    Dim cs As ClientScriptManager = Page.ClientScript

    Public ds As DataSet
    Dim dts As DataTable
    Public myTable As New DataTable
    Public html As String
    Public htmlForm As String
    Dim val As String

    'Dim report = "Neraca"
    'Dim sector = "konstruksi"
    'Dim code = "BRW_Dambha"

#Region " Global Variable "
    Dim usrLogin As String
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        'lblNotif.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        reporttype.Value = "Neraca"

        cekUserAuthentication()
        controlHome()

        myTable.Columns.Add("styles")
        myTable.Columns.Add("identasi")

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
                fillDdl()
                bindata()
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

    Sub cekSector(ByVal no)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkBorrowerSector(no)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            sector = dt.Rows(0).Item("brw_sec")
            sektor.Value = sector
            brwcode.Value = dt.Rows(0).Item("app_brw_code")
        End If
    End Sub

    Sub cekApp(ByVal appno)
        Dim dset As DataSet
        Dim dtbl As DataTable
        dset = oApp.checkApp(appno)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            'isiData(appno)
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

    Sub controlRO()
        'btnSave.Visible = False
        'btnNext.Visible = True
        btnUpdate.Visible = False
        divHapus.Visible = False
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

    Sub generetform()
        Dim styles As String = ""
        Dim countIdt As Integer = 0
        Dim fmlPctView As String = ""
        Dim cls As String = "row"
        Dim persen As String = ""

        'Dim classtable = "style='text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-bottom:10px;  '"
        'Dim countCols As Integer = ds.Tables(0).Columns.Count()
        'htmlForm += "<table width='100%'  " & classtable & " id='tblNeraca' class='tbl'>"
        'htmlForm += "<tr colspan='2' style='background-color:#f6f7f7;'>"
        'htmlForm += "<td width='50%' align='center'>Description </td>"
        'For index = 2 To countCols - 1
        '    htmlForm += "<td width='5%' align='center'>" + ds.Tables(0).Columns(index).ToString() + " </td>"
        'Next
        'htmlForm += "</tr>"

        'For index = 0 To ds.Tables(0).Rows.Count() - 1

        '    Dim identasi As String = ""
        '    myTable = oGen.checkStyle(ds.Tables(0).Rows(index).Item("acc_code"))

        '    countIdt = Convert.ToInt32(myTable.Rows(0)(1))
        '    If countIdt > 0 Then
        '        For idt = 1 To countIdt
        '            identasi += "&nbsp; "
        '        Next
        '    End If

        '    htmlForm += "<tr style='" + myTable.Rows(0)(0) + "'>"

        '    'htmlForm += "<tr>"
        '    For cols = 0 To ds.Tables(0).Columns.Count() - 1
        '        If cols <> 0 Then
        '            If ds.Tables(0).Rows(index)(cols).ToString() = "n/a" Then
        '                val = "n/a"
        '            Else
        '                val = moneyFormatLabel(ds.Tables(0).Rows(index)(cols).ToString())
        '            End If
        '        Else
        '            val = ds.Tables(0).Rows(index)(cols).ToString()
        '        End If

        '        'If cols <> 1 Then
        '        '    htmlForm += "<td width='5%'>" & val & " </td>"
        '        'End If
        '        If cols <> 1 Then
        '            If cols = 0 Then
        '                htmlForm += "<td width='5%'>" & identasi & val & " </td>"
        '            ElseIf cols <> 0 Then
        '                htmlForm += "<td width='5%' align='right'>" & val & " </td>"
        '            End If
        '        End If
        '    Next

        '    htmlForm += "</tr>"
        'Next

        'htmlForm += "</table>"

        Dim classtable = "style='text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-bottom:10px;  '"
        Dim countCols As Integer = ds.Tables(0).Columns.Count()
        htmlForm += "<table width='100%'  " & classtable & " id='tblNeraca' class='tbl'>"
        htmlForm += "<tr colspan='2' style='background-color:#f6f7f7;'>"
        htmlForm += "<td width='50%' align='center'>Description (dalam jutaan rupiah) </td>"
        For index = 2 To countCols - 1
            persen = ds.Tables(0).Columns(index).ToString()
            If persen.Contains("Growth (%)") Then
                persen = "%"
            Else
                persen = ds.Tables(0).Columns(index).ToString()
            End If

            htmlForm += "<td width='5%' align='center'>" + persen + " </td>"
        Next
        htmlForm += "</tr>"

        For index = 0 To ds.Tables(0).Rows.Count() - 1
            Dim identasi As String = ""
            myTable = oGen.checkStyle(ds.Tables(0).Rows(index).Item("acc_code"))

            countIdt = Convert.ToInt32(myTable.Rows(0)(1))
            If countIdt > 0 Then
                For idt = 1 To countIdt
                    identasi += "&nbsp; "
                Next
            End If

            htmlForm += "<tr style='" + myTable.Rows(0)(0) + "' class='" + cls + "'>"
            For cols = 0 To ds.Tables(0).Columns.Count() - 1
                If cols <> 1 Then
                    'htmlForm += "<td width='5%'>" & ds.Tables(0).Rows(index)(cols).ToString() & " </td>"
                    If cols <> 0 Then
                        If ds.Tables(0).Rows(index)(cols).ToString() = "n/a" Then
                            val = "n/a"
                        Else
                            'val = moneyFormatLabel(ds.Tables(0).Rows(index)(cols).ToString())
                            val = ds.Tables(0).Rows(index)(cols).ToString() '''edited by harfiq 12 feb 17
                        End If
                    Else
                        val = ds.Tables(0).Rows(index)(cols).ToString()
                    End If

                    If cols <> 1 Then
                        If cols = 0 Then
                            htmlForm += "<td width='5%' >" & identasi & val & " </td>"
                        ElseIf cols <> 0 Then
                            'collect formula percent view
                            If cols Mod 2 <> 0 Then
                                Dim dTbl As DataTable
                                dTbl = oLap.getFormulaPctView(ds.Tables(0).Rows(index).Item("acc_code"), ds.Tables(0).Rows(index).Item("acc_code") + "_" + Convert.ToString(cols), "_" + Convert.ToString(cols - 1), cls)
                                If dTbl.Rows.Count > 0 Then
                                    If index = ds.Tables(0).Rows.Count() - 1 Then
                                        fmlPctView += dTbl.Rows(0).Item("acc_js_formula_pct_view").ToString()
                                    Else
                                        fmlPctView += dTbl.Rows(0).Item("acc_js_formula_pct_view").ToString() + ";"
                                    End If
                                End If
                            End If

                            htmlForm += "<td width='5%' align='right' data-name='" + ds.Tables(0).Rows(index).Item("acc_code") + "_" + Convert.ToString(cols) + "'>" & val & " </td>"
                        End If
                    End If
                End If
            Next

            htmlForm += "</tr>"
        Next

        cs.RegisterStartupScript(Page.[GetType](), "callFunction", "callFunctionCall('" + fmlPctView + "')", True)

        htmlForm += "</table>"

    End Sub

    Sub bindata()
        ds = getAppendAccformula(reporttype.Value, sektor.Value, brwcode.Value)
        generetform()
        'ds = getAppendAccformula(report, sector, code)
    End Sub

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

    Public Function getAppendAccformula(ByVal report, ByVal sector, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Neraca
        myConn.Open()
        Dim cmdA As New SqlCommand("view_sql_formula", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@report", SqlDbType.VarChar, 50).Value = report
        cmdA.Parameters.Add("@sector", SqlDbType.VarChar, 50).Value = sector
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

    Public Function moneyFormatLabel(ByVal parVal)
        'return 100,000,000.00
        Dim str As String = ""
        Try
            If CheckNullString(parVal) = "" Then parVal = 0
            'parVal = Convert.ToInt32(Convert.ToDecimal(parVal))
            parVal = Convert.ToDecimal(parVal)

            If parVal = 0 Then
                str = "0"
            Else
                str = String.Format("{0:N2}", parVal)
            End If
            Return str
        Catch ex As Exception
            str = String.Format("{0:N2}", parVal)
        End Try
        Return str
    End Function

    Public Function CheckNullString(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return ""
        Else
            Return CType(parVal, String)
        End If
    End Function

    Sub fillDdl()
        Dim dTbl As DataTable
        dTbl = oLap.getperiodeByBrwcode(brwcode.Value)
        ddlPeriode.DataSource = dTbl
        ddlPeriode.DataValueField = "value"
        ddlPeriode.DataTextField = "text"
        ddlPeriode.DataBind()

    End Sub

    Protected Sub btnHapus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHapus.Click
        Dim pilihan As String
        pilihan = ddlPeriode.SelectedValue.ToString()
        If pilihan = "pilih" Then
            bindata()
        Else
            oLap.DelReportByPeriode(brwcode.Value, pilihan)
            bindata()
        End If

    End Sub
End Class