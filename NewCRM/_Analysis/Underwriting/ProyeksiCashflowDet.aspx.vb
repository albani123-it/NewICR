Imports System.Data.SqlClient
'Imports IDX.LibMaster
Imports IDX.DocumentExport
Public Class ProyeksiCashflowDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oLap As New lLapKeuangan
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Public ds As DataSet
    Dim dts As DataTable
    Public html2 As String
    Protected sector As String = ""
    Protected dataTable As DataTable
    Protected pagename As String = "Proyeksi Cashflow"
    Protected dtWorkflow As New DataTable
    Protected menuNext As String = ""
    Dim brwcode = "XXX"
    Dim curency = "& format(parVal) &"

#Region " Global Variable "
    Dim usrLogin As String
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
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
        bindata()

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
        'btnNext.Visible = False
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
                brwcode = dtbl.Rows(0).Item("app_brw_code")
                isiData(dtbl.Rows(0).Item("app_brw_code"))
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
        'read = True
        'btnSave.Visible = False
        'btnNext.Visible = True
    End Sub
    Sub isiData(ByVal brwcode)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oApp.checkAspek(brwcode, "Aspek Financial")
        dt = ds.Tables(0)
        'If dt.Rows.Count > 0 Then
        '    exists = True
        'End If
    End Sub
    Sub generetform(ByVal ds)

        Dim classtable = "style='text-align:left;border: thin solid #bcbcbc;border-radius: 0px 0px 5 5 ;margin-bottom:10px;  '"
        'html2 += "<table " & classtable & " id='tblcashflow' border='1'>"
        html2 += "<table id='tblcashflow' cellspacing='0' cellpadding='3' style:'border-spacing:10px;border-collapse: separate;'>"
        'html2 += "<tr ><td colspan='" + Convert.ToString((ds.Tables(0).columns.count() - 1)) + "' background='../../Images/bg-head.gif' bgcolor='#ddedf6'>&nbsp;</td></tr>"

        html2 += "<thead>"
        html2 += "<tr style='background-color:#f6f7f7;'>"
        html2 += "<th style='min-width:300px;text-align:center;width:auto;'>Description </th>"
        For i As Integer = 2 To ds.Tables(0).columns.count() - 1
            Try
                html2 += "<th style='text-align:right;min-width:150px;width:auto;'>" & ds.Tables(0).columns(i).columnname & "</th> "
            Catch ex As Exception

            End Try
        Next
        html2 += "</tr>"
        html2 += "</thead>"

        Dim rand As Integer = 0
        Dim color As String = ""
        For Each dr As DataRow In ds.Tables(0).Rows
            If rand Mod 2 = 0 Then
                color = "#ebecec"
            Else
                color = "#f6f7f7"
            End If

            html2 += "<tr bgcolor='" & color & "' >"
            'If rand = 0 Then
            '    html2 += "<td style='text-align:left;font-weight:bold;'>" & dr.Item("acc_desc") & " </td>"
            'Else
            html2 += "<td style='text-align:left;font-weight:bold;'>" & dr.Item("acc_desc") & " </td>"
            'End If

            For i As Integer = 2 To ds.Tables(0).columns.count() - 1
                Try
                    If rand = 0 Then
                        html2 += "<td style='text-align:left;'>" & dr(i) & "</td> "
                    Else
                        html2 += "<td style='text-align:right;'>" & dr(i) & "</td> "
                    End If

                Catch ex As Exception

                End Try
            Next
            html2 += "</tr>"
            rand = rand + 1
        Next

        html2 += "</table>"
    End Sub

    Sub bindata()
        Dim dTbl As DataTable
        dTbl = oLap.CheckAccDataByBrwcode(brwcode)
        'If dTbl.Rows.Count > 0 Then
        '    ds = getAppendProyeksiCashflow(brwcode)
        '    generetform(ds)
        'Else
        '    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Silahkan upload data proyeksi terlebih dahulu');window.location.href='DataUploadProyeksi.aspx?appno=" & appno.Value & "';", True)
        'End If
        Response.Redirect("worksheetTemplate.aspx?appno=" & appno.Value)
    End Sub

    Public Function getAppendProyeksiCashflow(ByVal brwcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("view_report_proyeksi", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@brwcode", SqlDbType.VarChar, 50).Value = brwcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds

    End Function

End Class