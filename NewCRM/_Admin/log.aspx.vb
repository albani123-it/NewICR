Imports System.Data.SqlClient
Imports Obout.Grid
Imports System.Net.Mime
Imports System.IO

Public Class log

    Inherits System.Web.UI.Page
    Dim gStrFormName = "document.Form1."
    Dim isvaliddate As Boolean = False
    Dim oGen As New lGeneral
    Public oUsr As New lUsers
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cekUserAuthentication()
        End If

        grid1.Serialize = True
        grid1.Visible = False
        grid1.Width = Unit.Percentage(100)
        OboutButton2.Visible = False

        If IsPostBack Then
            BindData(Session("mode"), Session("valfrom"), Session("valto"))
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim permit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log" Then
                    permit = True
                End If
            Next
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
        If permit = False Then
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End If


        Try

            optionMode.Items.Clear()
            optionMode.Items.Add(New ListItem("Select One", "Select One"))
            'For i As Integer = 0 To dtUser.Rows.Count - 1

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            '    optionMode.Items.Add(New ListItem("Access Level", "access_level"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_usr" Then
            optionMode.Items.Add(New ListItem("Asset", "asset"))
            'End If

            'tambahan
            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Asset Model", "asset_model"))
            'End If

            ' If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Borrower Management", "borrowerMgmt"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Collateral Classification", "collateral"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Data Management", "dataMgmt"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Data Join Management", "dataMgmtJoin"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            '    optionMode.Items.Add(New ListItem("Data Upload", "dataupload"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            'optionMode.Items.Add(New ListItem("PD", "PD"))
            'End If
            optionMode.Items.Add(New ListItem("Global Setting Bank Capital", "glo_bank_capital"))
            optionMode.Items.Add(New ListItem("Global Setting Eligible IRB Collateral", "glo_irb_col"))
            optionMode.Items.Add(New ListItem("Global Setting Pre-Screening", "glo_pre_screening"))

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Product Management", "productMgmt"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Pooling (PD/LGD/CCF)", "Pool"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Rating Model", "scorecard"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            'optionMode.Items.Add(New ListItem("Scorecard Baseline", "scorecard_baseline"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Rating Model Characteristic", "scorecard_char"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_acl" Then
            optionMode.Items.Add(New ListItem("Rating Model Rating", "scorecard_rating"))
            'End If

            optionMode.Items.Add(New ListItem("Rule Management", "ruleMgmt"))

            optionMode.Items.Add(New ListItem("System", "system"))

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_ua" Then
            optionMode.Items.Add(New ListItem("User Access", "ua"))
            'End If
            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_usr" Then
            optionMode.Items.Add(New ListItem("User Management", "users"))
            'End If

            'If dtUser.Rows(i).Item("ld_lm_description") = "lvl_adm_log_usr" Then
            optionMode.Items.Add(New ListItem("User Role", "level"))
            'End If
            'Next

        Catch ex As Exception
        End Try


    End Sub

    Public Sub BindData(ByVal val, ByVal valfrom, ByVal valto)
        Try
            Dim myDb As New dbConn
            Dim myConn As SqlConnection
            myConn = myDb.getConStr
            Dim whr As String = ""
            If Not valfrom = "" Then
                whr = " AND update_date >= convert(datetime, '" & valfrom & "', 105) "
            End If
            If Not valto = "" Then
                whr &= " AND update_date < dateadd(day,1,convert(datetime,'" & valto & "', 105)) "
            End If
            Dim myComm As New SqlCommand("Select * from logs where code = '" & val & "' " & whr & " order by log_id desc", myConn)
            myConn.Open()
            Dim myreader As SqlDataReader = myComm.ExecuteReader()
            If myreader.HasRows Then
                OboutButton2.Visible = True
            Else
                OboutButton2.Visible = False
            End If
            grid1.DataSource = myreader
            grid1.DataBind()
            myConn.Close()
            grid1.Width = Unit.Percentage(100)
            grid1.Visible = True


        Catch SqlEx As SqlException
            Dim myError As SqlError
            Debug.WriteLine("Errors Count:" & SqlEx.Errors.Count)
            For Each myError In SqlEx.Errors
                Debug.WriteLine(myError.Number & " - " & myError.Message)
            Next
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim mode As String = optionMode.SelectedValue
        Dim valfrom As String = txtFrom.Value
        Dim valto As String = txtTo.Value
        isvaliddate = CekInput(valfrom, valto)
        If Not mode = "" And isvaliddate = True Then
            Session("mode") = mode
            Session("valfrom") = valfrom
            Session("valto") = valto
            optionMode.SelectedValue = mode
            BindData(mode, valfrom, valto)
        End If
    End Sub

    Public Function CekInput(ByVal valfrom, ByVal valto)
        Dim lMsg As String = ""
        Dim lAction As String = ""
        If optionMode.SelectedValue = "Select One" Then
            lMsg &= "Please choose Log Mode.\n"
        End If
        If Not oGen.CheckInputDate(valfrom) Then
            lMsg = lMsg + "Date format is invalid!\n"
            If lAction = "" Then
                lAction = gStrFormName + txtFrom.ID + ".focus()"
            End If
        ElseIf Not oGen.CheckInputDate(valto) Then
            lMsg = lMsg + "Date format is invalid!\n"
            If lAction = "" Then
                lAction = gStrFormName + txtTo.ID + ".focus()"
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" + lMsg + "');" + lAction
            Return False
        Else
            liBodyLoadAction.Text = ""
            Return True
        End If
    End Function


    Protected Sub OboutButton2_Click(ByVal sender As Object, ByVal e As EventArgs)
        grid1.PageSize = -1
        grid1.DataBind()
        ExportGridToCSV()
    End Sub

    Private Sub ExportGridToCSV()
        Response.Clear()
        If grid1.Rows.Count > 0 Then
            Dim j As Integer = 0
            'How add the data from the Grid to csv table
            For i As Integer = 0 To grid1.Rows.Count - 1

                Dim dataItem As Hashtable = grid1.Rows(i).ToHashtable()
                'Dim x As String = dataItem.ToString().Replace(";STATUS=Inactive", "")
                j = 0

                Response.Write(vbLf)
                For Each col As Column In grid1.Columns
                    If j = 0 Then
                        Response.Write(dataItem(col.DataField).ToString())

                        j += 1
                    End If

                Next
            Next

            ' Send the data and the appropriate headers to the browser
            'SheetReport.Range("A2").ColumnWidth = 30

            'Worksheets("Sheet1").Range("A1:E1").Columns.AutoFit()
            Response.AddHeader("content-disposition", "attachment;filename=Log.csv")
            Response.ContentType = "text/csv"
            Response.[End]()
        Else
            liBodyLoadAction.Text = "alert('There are no records available to download.');"
        End If
    End Sub



End Class