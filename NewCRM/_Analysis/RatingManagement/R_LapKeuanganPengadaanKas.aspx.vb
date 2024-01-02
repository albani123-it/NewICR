Imports System.Data.SqlClient

Public Class R_LapKeuanganPengadaanKas
    Inherits System.Web.UI.Page
    Dim oUsr As New lUsers
    Dim oApp As New lApps
    Dim oRerat As New lRerating
    Dim oGen As New lGeneral
    Protected dataTable As DataTable
    Protected pagename As String = "Pengadaan Kas"
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

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        'lblNotif.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        reporttype.Value = "Pernyataan_pengadaan_kas"

        cekUserAuthentication()
        controlHome()
        myTable.Columns.Add("styles")
        myTable.Columns.Add("identasi")

        If Request.QueryString("appno") IsNot Nothing Then
            Session("appno") = Request.QueryString("appno")
        ElseIf Request.QueryString("brwcode") IsNot Nothing Then
            Session("brwcode") = Request.QueryString("brwcode")
            brwcode.Value = Request.QueryString("brwcode")
            dataTable = oRerat.getListOfMenu("Borrower")
        End If

        If Session("brwcode") IsNot Nothing And Not IsPostBack Then
            getMenuNext("Borrower")
            checkAppByBrwCode(Session("brwcode"))
            cekSector(appno.Value)
            executeJavascript()
        End If

        bindata()
        generetform()
    End Sub

    Sub executeJavascript()
        Dim dset As DataSet
        Dim dtbl As DataTable
        Dim url As String = ""
        dset = oApp.checkAppByBrwCode(brwcode.Value)
        dtbl = dset.Tables(0)
        If dtbl.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1
                If dataTable.Rows(i).Item("lmr_name") = pagename Then
                    url = dataTable.Rows(i).Item("lmr_link")
                End If
            Next
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim script As String = "aa_aBc('" & url & "','" & pagename & "','" & brwcode.Value & "', 'Borrower');"
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

    Sub controlHome()
        Dim dtTable(9) As String

        dtTable = oUsr.getUserDetail(usrLogin)
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

    Sub bindata()
        ds = getAppendAccformula(reporttype.Value, sektor.Value, brwcode.Value)
    End Sub

    Sub generetform()
        Dim styles As String = ""
        Dim countIdt As Integer = 0

        Dim classtable = "style='text-align:left;border: thin solid #bcbcbc;border-radius: 5px;margin-bottom:10px;  '"
        Dim countCols As Integer = ds.Tables(0).Columns.Count()
        htmlForm += "<table width='100%'  " & classtable & " id='tblNeraca' class='tbl'>"
        htmlForm += "<tr colspan='2' style='background-color:#f6f7f7;'>"
        htmlForm += "<td width='50%' align='center'>Description </td>"
        For index = 2 To countCols - 1
            htmlForm += "<td width='5%' align='center'>" + ds.Tables(0).Columns(index).ToString() + " </td>"
        Next
        htmlForm += "</tr>"

        For index = 0 To ds.Tables(0).Rows.Count() - 1

            'htmlForm += "<tr>"
            Dim identasi As String = ""
            myTable = oGen.checkStyle(ds.Tables(0).Rows(index).Item("acc_code"))

            countIdt = Convert.ToInt32(myTable.Rows(0)(1))
            If countIdt > 0 Then
                For idt = 1 To countIdt
                    identasi += "&nbsp; "
                Next
            End If

            htmlForm += "<tr style='" + myTable.Rows(0)(0) + "'>"
            For cols = 0 To ds.Tables(0).Columns.Count() - 1
                If cols <> 0 Then
                    If ds.Tables(0).Rows(index)(cols).ToString() = "n/a" Then
                        val = "n/a"
                    Else
                        val = moneyFormatLabel(ds.Tables(0).Rows(index)(cols).ToString())
                    End If
                Else
                    val = ds.Tables(0).Rows(index)(cols).ToString()
                End If

                If cols <> 1 Then
                    If cols = 0 Then
                        htmlForm += "<td width='5%'>" & identasi & val & " </td>"
                    ElseIf cols <> 0 Then
                        htmlForm += "<td width='5%' align='right'>" & val & " </td>"
                    End If
                End If
            Next

            htmlForm += "</tr>"
        Next

        htmlForm += "</table>"
    End Sub

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

End Class