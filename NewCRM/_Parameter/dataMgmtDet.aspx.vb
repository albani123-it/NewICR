Public Class dataMgmtDet
    Inherits System.Web.UI.Page
    Public myTable As New DataTable
    Dim data_id
    Dim mode
    Dim cstype As Type = Me.[GetType]()
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim oGen As New lGeneral
    Dim oDm As New lDataMgmt
    Dim oSc As New lScorecard
    Dim objLog As New lLog

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))

        data_id = "-1"
        If Not Request.QueryString("id") Is Nothing Then
            data_id = Request.QueryString("id")
        End If

        mode = "-1"
        If Not Request.QueryString("mode") Is Nothing Then
            mode = Request.QueryString("mode")
        End If

        cekUserAuthentication()

        myTable.Columns.Add("DataSource")
        myTable.Columns.Add("MergingCondition")
        myTable.Columns.Add("TableName")
        myTable.Columns.Add("TableAlias")
        myTable.Columns.Add("Fields")
        myTable.Columns.Add("Condition")
        myTable.Columns.Add("MergingKey")

        If Not IsPostBack Then
            If mode = 1 Then
                isiData()
            Else
                Bind()
            End If

        End If

    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_new" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_data_view" Then
                    If Not edit = True Then
                        ddlDataType.Enabled = False
                        btnAdd.Enabled = False
                        txtDataName.Enabled = False
                        txtDataDesc.Enabled = False
                        ddlDataSourceStat.Enabled = False
                        ddlTableNameStat.Enabled = False
                        txtFieldStat.Enabled = False
                        txtConditionStat.Enabled = False
                        txtAliasStat.Enabled = False
                        txtFinalField.Enabled = False
                        txtFinalRule.Enabled = False
                        btnValidate.Enabled = False
                    End If
                End If
            Next
            If Not tf Then
                Session.Abandon()
                Response.Redirect("../Login.aspx")
            End If
        Catch ex As Exception
            Session.Abandon()
            Response.Redirect("../Login.aspx")
        End Try
    End Sub

    Protected Sub Bind()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()
    End Sub

    Sub isiData()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oDm.getDataManagementDetail(data_id)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            ddlDataType.SelectedValue = dt.Rows(0).Item("data_type")
            txtDataName.Text = dt.Rows(0).Item("data_name")
            txtDataDesc.Text = dt.Rows(0).Item("data_desc")
            If dt.Rows(0).Item("is_Active") = True Then
                chkStatus.Checked = True
            Else
                chkStatus.Checked = False
            End If

            Dim dattblstat As DataTable = oDm.getDataSourceDetail(dt.Rows(0).Item("data_id"), "stat")
            ddlDataSourceStat.SelectedValue = dattblstat.Rows(0).Item("DataSource").ToString
            loadTbl()
            ddlTableNameStat.SelectedValue = dattblstat.Rows(0).Item("TableName").ToString
            txtAliasStat.Text = dattblstat.Rows(0).Item("TableAlias").ToString
            txtFieldStat.Text = dattblstat.Rows(0).Item("Fields").ToString
            txtConditionStat.Text = dattblstat.Rows(0).Item("Condition").ToString


            myTable = oDm.getDataSourceDetail(dt.Rows(0).Item("data_id"), "dyn")
            bindDataSourceEdit()

            txtFinalField.Text = dt.Rows(0).Item("final_field")
            txtFinalRule.Text = dt.Rows(0).Item("final_rule")
        End If
    End Sub

    Sub bindDataSourceEdit()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()

        For j As Integer = 0 To Repeater1.Items.Count - 1

            Dim ddlDataSourceDyn As DropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlDataSourceDyn"), DropDownList)
            Dim ddlMergeCondDyn As DropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlMergeCondDyn"), DropDownList)
            Dim ddlTableNameDyn As DropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlTableNameDyn"), DropDownList)

            Dim txtAliasDyn As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtAliasDyn"), TextBox)
            Dim txtFieldDyn As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtFieldDyn"), TextBox)
            Dim txtConditionDyn As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtConditionDyn"), TextBox)
            Dim txtMergeKeyDyn As TextBox = DirectCast(Repeater1.Items(j).FindControl("txtMergeKeyDyn"), TextBox)

            ddlDataSourceDyn.SelectedIndex = ddlDataSourceDyn.Items.IndexOf(ddlDataSourceDyn.Items.FindByValue(myTable.Rows(j)("DataSource").ToString()))
            ddlDataSourceDyn.DataBind()

            ddlMergeCondDyn.SelectedIndex = ddlMergeCondDyn.Items.IndexOf(ddlMergeCondDyn.Items.FindByValue(myTable.Rows(j)("MergingCondition").ToString()))
            ddlMergeCondDyn.DataBind()

            ddlTableNameDyn.Items.Clear()

            Dim dt As DataTable
            With New lDataMgmt
                If ddlDataSourceDyn.SelectedValue = "dataupload" Then
                    dt = .getDataSource("du")
                ElseIf ddlDataSourceDyn.SelectedValue = "datamart" Then
                    dt = .getDataSource("dm")
                End If

                With ddlTableNameDyn
                    .DataSource = dt
                    .DataTextField = "table_name"
                    .DataValueField = "table_name"
                    .DataBind()
                End With
            End With

            ddlTableNameDyn.Items.Insert(0, New ListItem("Select One", "Select One"))
            If myTable.Rows(j)("TableName").ToString <> "" And myTable.Rows(j)("DataSource").ToString <> "Select One" Then
                ddlTableNameDyn.Items.FindByValue(myTable.Rows(j)("TableName").ToString()).Selected = True
            End If

            txtAliasDyn.Text = myTable.Rows(j)("TableAlias").ToString()
            txtFieldDyn.Text = myTable.Rows(j)("Fields").ToString()
            If Not String.IsNullOrEmpty(myTable.Rows(j)("Condition").ToString) Then
                txtConditionDyn.Text = myTable.Rows(j)("Condition").ToString()
            Else
                txtConditionDyn.Text = ""
            End If
            txtMergeKeyDyn.Text = myTable.Rows(j)("MergingKey").ToString()
        Next
    End Sub

    Private Sub ddlDataSourceStat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDataSourceStat.SelectedIndexChanged
        loadTbl()

        txtFieldStat.Text = ""
        txtConditionStat.Text = ""
    End Sub

    Private Sub loadTbl()
        ddlTableNameStat.Items.Clear()

        Dim dt As DataTable
        With New lDataMgmt
            If ddlDataSourceStat.SelectedValue = "dataupload" Then
                dt = .getDataSource("du")
            ElseIf ddlDataSourceStat.SelectedValue = "datamart" Then
                dt = .getDataSource("dm")
            End If

            With ddlTableNameStat
                .DataSource = dt
                .DataTextField = "table_name"
                .DataValueField = "table_name"
                .DataBind()
            End With
        End With

        ddlTableNameStat.Items.Insert(0, New ListItem("Select One", "Select One"))
        ddlTableNameStat.SelectedIndex = 0
    End Sub

    Private Sub ddlTableNameStat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTableNameStat.SelectedIndexChanged

    End Sub

    Public Sub ddlDataSourceDyn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ddlDataSourceDyn As DropDownList = DirectCast(sender, DropDownList)
        Dim ri As RepeaterItem = DirectCast(ddlDataSourceDyn.Parent, RepeaterItem)
        Dim ddlTableNameDyn As DropDownList = DirectCast(ri.FindControl("ddlTableNameDyn"), DropDownList)
        Dim txtAliasDyn As TextBox = DirectCast(ri.FindControl("txtAliasDyn"), TextBox)
        Dim txtFieldDyn As TextBox = DirectCast(ri.FindControl("txtFieldDyn"), TextBox)
        Dim txtConditionDyn As TextBox = DirectCast(ri.FindControl("txtConditionDyn"), TextBox)
        Dim txtMergeKeyDyn As TextBox = DirectCast(ri.FindControl("txtMergeKeyDyn"), TextBox)

        ddlTableNameDyn.Items.Clear()

        Dim dt As DataTable
        With New lDataMgmt
            If ddlDataSourceDyn.SelectedValue = "dataupload" Then
                dt = .getDataSource("du")
            ElseIf ddlDataSourceDyn.SelectedValue = "datamart" Then
                dt = .getDataSource("dm")
            End If

            With ddlTableNameDyn
                .DataSource = dt
                .DataTextField = "table_name"
                .DataValueField = "table_name"
                .DataBind()
            End With
        End With

        ddlTableNameDyn.Items.Insert(0, New ListItem("Select One", "Select One"))
        ddlTableNameDyn.SelectedIndex = 0

        txtAliasDyn.Text = ""
        txtFieldDyn.Text = ""
        txtConditionDyn.Text = ""
        txtMergeKeyDyn.Text = ""
    End Sub

    Public Sub ddlTableNameDyn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlTableNameDyn As DropDownList = DirectCast(sender, DropDownList)
        Dim ri As RepeaterItem = DirectCast(ddlTableNameDyn.Parent, RepeaterItem)

        If ddlTableNameDyn.SelectedValue <> "Select One" Then
            Dim fieldsstr As String
            With New lDataMgmt
                fieldsstr = .LoadDataUploadTableFields(ddlTableNameDyn.SelectedValue.ToString)
            End With
            Dim fielddyn As TextBox = DirectCast(ri.FindControl("txtFieldDyn"), TextBox)
            'fielddyn.Text = fieldsstr
            fielddyn.Text = ""
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        PopulateDataTable()
        myTable.Rows.Add(myTable.NewRow())

        Bind_Add()
    End Sub

    Protected Sub Repeater1_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs) Handles Repeater1.ItemCommand
        PopulateDataTable()
        myTable.Rows(e.Item.ItemIndex).Delete()
        Bind_Add()
    End Sub

    Protected Sub PopulateDataTable()
        For Each item As RepeaterItem In Repeater1.Items

            Dim ddlDataSourceDyn As DropDownList = DirectCast(item.FindControl("ddlDataSourceDyn"), DropDownList)
            Dim ddlMergeCondDyn As DropDownList = DirectCast(item.FindControl("ddlMergeCondDyn"), DropDownList)
            Dim ddlTableNameDyn As DropDownList = DirectCast(item.FindControl("ddlTableNameDyn"), DropDownList)

            Dim txtAliasDyn As TextBox = DirectCast(item.FindControl("txtAliasDyn"), TextBox)
            Dim txtFieldDyn As TextBox = DirectCast(item.FindControl("txtFieldDyn"), TextBox)
            Dim txtConditionDyn As TextBox = DirectCast(item.FindControl("txtConditionDyn"), TextBox)
            Dim txtMergeKeyDyn As TextBox = DirectCast(item.FindControl("txtMergeKeyDyn"), TextBox)

            Dim row As DataRow = myTable.NewRow()
            row("DataSource") = ddlDataSourceDyn.SelectedValue.ToString
            row("MergingCondition") = ddlMergeCondDyn.SelectedValue.ToString
            row("TableName") = ddlTableNameDyn.SelectedValue.ToString.Trim
            row("TableAlias") = txtAliasDyn.Text
            row("Fields") = txtFieldDyn.Text
            row("Condition") = txtConditionDyn.Text
            row("MergingKey") = txtMergeKeyDyn.Text

            myTable.Rows.Add(row)

        Next

    End Sub

    Protected Sub Bind_Add()
        Repeater1.DataSource = myTable
        Repeater1.DataBind()

        For j As Integer = 0 To Repeater1.Items.Count - 1

            Dim ddlDataSourceDyn As DropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlDataSourceDyn"), DropDownList)
            Dim ddlMergeCondDyn As DropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlMergeCondDyn"), DropDownList)
            Dim ddlTableNameDyn As DropDownList = DirectCast(Repeater1.Items(j).FindControl("ddlTableNameDyn"), DropDownList)


            ddlDataSourceDyn.SelectedIndex = ddlDataSourceDyn.Items.IndexOf(ddlDataSourceDyn.Items.FindByValue(myTable.Rows(j)("DataSource").ToString()))
            ddlDataSourceDyn.DataBind()

            ddlMergeCondDyn.SelectedIndex = ddlMergeCondDyn.Items.IndexOf(ddlMergeCondDyn.Items.FindByValue(myTable.Rows(j)("MergingCondition").ToString()))
            ddlMergeCondDyn.DataBind()

            ddlTableNameDyn.Items.Clear()

            Dim dt As DataTable
            With New lDataMgmt
                If ddlDataSourceDyn.SelectedValue = "dataupload" Then
                    dt = .getDataSource("du")
                ElseIf ddlDataSourceDyn.SelectedValue = "datamart" Then
                    dt = .getDataSource("dm")
                End If

                With ddlTableNameDyn
                    .DataSource = dt
                    .DataTextField = "table_name"
                    .DataValueField = "table_name"
                    .DataBind()
                End With
            End With

            ddlTableNameDyn.Items.Insert(0, New ListItem("Select One", "Select One"))
            If myTable.Rows(j)("TableName").ToString <> "" And myTable.Rows(j)("DataSource").ToString <> "Select One" Then
                ddlTableNameDyn.Items.FindByValue(myTable.Rows(j)("TableName").ToString()).Selected = True
            End If


        Next

    End Sub

#Region "Function for Data Validation"
    Function getDataSource(Optional ByVal chk = "")
        Dim aSql As String = ""

        If ddlDataSourceStat.SelectedValue = "datamart" Then
            'aSql += "SELECT " & txtFinalField.Text & " FROM (SELECT " & txtFieldStat.Text & " FROM " & chk & "" & ddlTableNameStat.SelectedValue
            aSql += "SELECT " & txtFinalField.Text & " FROM (SELECT " & txtFieldStat.Text & " FROM " & ddlTableNameStat.SelectedValue
        Else
            aSql += "SELECT " & txtFinalField.Text & " FROM (SELECT " & txtFieldStat.Text & " FROM " & ddlTableNameStat.SelectedValue
        End If
        If txtConditionStat.Text.Length > 0 Then
            aSql += " WHERE " & txtConditionStat.Text
        End If
        aSql += ") " & txtAliasStat.Text & ""
        If Repeater1.Items.Count > 0 Then
            For Each i As RepeaterItem In Repeater1.Items

                Dim ddlDataSourceDyn As DropDownList = DirectCast(i.FindControl("ddlDataSourceDyn"), DropDownList)
                Dim ddlMergeCondDyn As DropDownList = DirectCast(i.FindControl("ddlMergeCondDyn"), DropDownList)
                Dim ddlTableNameDyn As DropDownList = DirectCast(i.FindControl("ddlTableNameDyn"), DropDownList)
                Dim txtAliasDyn As TextBox = DirectCast(i.FindControl("txtAliasDyn"), TextBox)
                Dim txtFieldDyn As TextBox = DirectCast(i.FindControl("txtFieldDyn"), TextBox)
                Dim txtConditionDyn As TextBox = DirectCast(i.FindControl("txtConditionDyn"), TextBox)
                Dim txtMergeKeyDyn As TextBox = DirectCast(i.FindControl("txtMergeKeyDyn"), TextBox)

                If ddlDataSourceDyn.SelectedValue = "datamart" Then
                    'aSql += vbCrLf & ddlMergeCondDyn.SelectedValue & " (SELECT " & txtFieldDyn.Text & " FROM " & chk & "" & ddlTableNameDyn.SelectedValue
                    aSql += vbCrLf & ddlMergeCondDyn.SelectedValue & " (SELECT " & txtFieldDyn.Text & " FROM " & ddlTableNameDyn.SelectedValue
                Else
                    aSql += vbCrLf & ddlMergeCondDyn.SelectedValue & " (SELECT " & txtFieldDyn.Text & " FROM " & ddlTableNameDyn.SelectedValue
                End If
                If txtConditionDyn.Text.Length > 0 Then
                    aSql += " WHERE " & txtConditionDyn.Text
                End If
                aSql += ")" & txtAliasDyn.Text & " ON " & txtMergeKeyDyn.Text
            Next
        End If
        If txtFinalRule.Text = "" Then
            aSql += " WHERE 1=1"
        Else
            aSql += " WHERE " & txtFinalRule.Text
        End If

        Return aSql
    End Function

    Sub dataMaster()
        'Dim spName As String = "en_master_" & scc_code1.Text & scc_code2.Text
        Dim spName As String = "sdm_" & txtDataName.Text
        Dim aSql As String = "", aSql2 As String = "", aSql3 As String = ""

        Try
            '---------------- create SP
            'aSql += "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'master_" & scc_code1.Text & scc_code2.Text & "') drop table master_" & scc_code1.Text & scc_code2.Text & ""
            aSql += "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'dm_" & txtDataName.Text & "') drop table dm_" & txtDataName.Text & ""
            'aSql += vbCrLf & "SELECT * INTO master_" & scc_code1.Text & scc_code2.Text & " FROM ("
            aSql += vbCrLf & "SELECT * INTO dm_" & txtDataName.Text & " FROM ("
            aSql += vbCrLf & getDataSource("")
            'If txtFinalRule.Text.Length > 0 Then
            '    aSql += vbCrLf & " WHERE " & txtFinalRule.Text
            'End If
            aSql += vbCrLf & ")zzz"
            'Response.Write(aSql)
            With New lGeneral
                .createSP(spName, aSql)
            End With
            '----------------
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "") & "');"
        End Try
        
        Try
            '---------------- create empty table
            'aSql2 += "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'master_" & scc_code1.Text & scc_code2.Text & "') drop table master_" & scc_code1.Text & scc_code2.Text & ""
            aSql2 += "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'dm_" & txtDataName.Text & "') drop table dm_" & txtDataName.Text & " "
            'aSql2 += vbCrLf & "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'dm_sc_" & txtDataName.Text & "') drop table dm_sc_" & txtDataName.Text & " "
            'aSql2 += vbCrLf & "SELECT top 0 * INTO master_" & scc_code1.Text & scc_code2.Text & " FROM ("
            aSql2 += vbCrLf & "SELECT top 0 * INTO dm_" & txtDataName.Text & " FROM ("
            aSql2 += vbCrLf & getDataSource("chk_")
            'If txtFinalRule.Text.Length > 0 Then
            '    aSql2 += vbCrLf & " WHERE " & txtFinalRule.Text
            'End If
            aSql2 += vbCrLf & ")zzz"
            'Response.Write("-----------------")
            'Response.Write(aSql2)
            With New lGeneral
                .run_text(aSql2)
            End With
            '----------------
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "") & "');"
        End Try
        
        Try
            '---------------- create checking table
            'aSql3 += "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'chk_master_" & scc_code1.Text & scc_code2.Text & "') drop table chk_master_" & scc_code1.Text & scc_code2.Text & ""
            aSql3 += "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE NAME = 'dm_" & txtDataName.Text & "_check') drop table dm_" & txtDataName.Text & "_check"
            'aSql3 += vbCrLf & "SELECT top 0 * INTO chk_master_" & scc_code1.Text & scc_code2.Text & " FROM ("
            aSql3 += vbCrLf & "SELECT top 0 * INTO dm_" & txtDataName.Text & "_check FROM ("
            aSql3 += vbCrLf & getDataSource("chk_")
            'If txtFinalRule.Text.Length > 0 Then
            '    aSql3 += vbCrLf & " WHERE " & txtFinalRule.Text
            'End If
            aSql3 += vbCrLf & ")zzz"
            'Response.Write("-----------------")
            'Response.Write(aSql3)
            With New lGeneral
                .run_text(aSql3)
            End With
            '----------------
        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "") & "');"
        End Try
        

        'Response.Write(aSql)
    End Sub
#End Region

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim info As String
        info = checkSpace(txtDataName.Text)
        If info = "" Then
            If liBodyLoadAction.Text = "" Then
                dataMaster()
                If liBodyLoadAction.Text = "" Then
                    SaveData()
                End If
            End If
        Else
            cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + info + "');", True)
            txtDataName.Focus()
        End If
    End Sub

    Function checkSpace(text)
        Dim info As String
        info = ""
        Dim count As Integer
        count = text.IndexOf(" "c)
        If count >= 0 Then
            info = "Data name tidak boleh ada spasi."
        End If

        Return info
    End Function

    Sub cekInput()
        Dim lMsg As String = ""
        Dim lAction As Integer = 0

        If txtDataName.Text = "" Then
            'lMsg += "Please input data name.\n"
            lMsg += "Mohon untuk mengisi data name.\n"
            If lAction = 0 Then
                txtDataName.Focus()
                lAction = 1
            End If
        End If

        With New lScorecard
            Dim errmsg As String = .IsQueryValid(getDataSource("chk_"))
            If (errmsg <> "") Then
                'lMsg += getDataSource("chk_")
                'lMsg += "Please check your Data Source.\n"
                lMsg += errmsg
            End If
        End With

        If (txtFinalField.Text = "") Then
            'lMsg += "Please input Final Field.\n"
            lMsg += "Mohon untuk mengisi Final Field.\n"
            If lAction = 0 Then
                txtFinalField.Focus()
                lAction = 1
            End If
        End If

        'Try
        '    Dim ds As DataSet
        '    Dim dt As DataTable
        '    ds = oSc.checkQueryField(getDataSource("chk_"))
        '    dt = ds.Tables(0)
        '    If ddlDataType.SelectedValue = "Asset Category" Then
        '        Dim check1 As Boolean = False
        '        Dim check2 As Boolean = False
        '        Dim check3 As Boolean = False
        '        Dim check4 As Boolean = False
        '        Dim check5 As Boolean = False
        '        Dim check6 As Boolean = False
        '        Dim check7 As Boolean = False
        '        Dim check8 As Boolean = False
        '        Dim check9 As Boolean = False
        '        Dim check10 As Boolean = False
        '        Dim check11 As Boolean = False
        '        Dim check12 As Boolean = False
        '        For Each column As DataColumn In dt.Columns
        '            If column.ColumnName = "ACCTNO" Then
        '                check1 = True
        '            End If
        '            If column.ColumnName = "balance_amt" Then
        '                check2 = True
        '            End If
        '            If column.ColumnName = "plafon" Then
        '                check3 = True
        '            End If
        '            If column.ColumnName = "KOLEKTIBILITAS_BI" Then
        '                check4 = True
        '            End If
        '            If column.ColumnName = "LAS_CREDIT_RATING" Then
        '                check5 = True
        '            End If
        '            If column.ColumnName = "FACILITY_RATING" Then
        '                check6 = True
        '            End If
        '            If column.ColumnName = "CREDIT_LINE_AMT" Then
        '                check7 = True
        '            End If
        '            If column.ColumnName = "product_segment_grp" Then
        '                check8 = True
        '            End If
        '            If column.ColumnName = "product_grp" Then
        '                check9 = True
        '            End If
        '            If column.ColumnName = "economic_sector_grp" Then
        '                check10 = True
        '            End If
        '            If column.ColumnName = "branch_grp" Then
        '                check11 = True
        '            End If
        '            If column.ColumnName = "sisa_tenor_year" Then
        '                check12 = True
        '            End If
        '        Next
        '        If check1 = False Then
        '            lMsg &= "Field ACCTNO doesn't exists.\n"
        '        End If
        '        If check2 = False Then
        '            lMsg &= "Field balance_amt doesn't exists.\n"
        '        End If
        '        If check3 = False Then
        '            lMsg &= "Field plafon doesn't exists.\n"
        '        End If
        '        If check4 = False Then
        '            lMsg &= "Field KOLEKTIBILITAS_BI doesn't exists.\n"
        '        End If
        '        If check5 = False Then
        '            lMsg &= "Field LAS_CREDIT_RATING doesn't exists.\n"
        '        End If
        '        If check6 = False Then
        '            lMsg &= "Field FACILITY_RATING doesn't exists.\n"
        '        End If
        '        If check7 = False Then
        '            lMsg &= "Field CREDIT_LINE_AMT doesn't exists.\n"
        '        End If
        '        If check8 = False Then
        '            lMsg &= "Field product_segment_grp doesn't exists.\n"
        '        End If
        '        If check9 = False Then
        '            lMsg &= "Field product_grp doesn't exists.\n"
        '        End If
        '        If check10 = False Then
        '            lMsg &= "Field economic_sector_grp doesn't exists.\n"
        '        End If
        '        If check11 = False Then
        '            lMsg &= "Field branch_grp doesn't exists.\n"
        '        End If
        '        If check12 = False Then
        '            lMsg &= "Field sisa_tenor_year doesn't exists.\n"
        '        End If
        '    Else
        '        Dim check1 As Boolean = False
        '        Dim check2 As Boolean = False
        '        Dim check3 As Boolean = False
        '        For Each column As DataColumn In dt.Columns
        '            If column.ColumnName = "ACCTNO" Then
        '                check1 = True
        '            End If
        '            If column.ColumnName = "collateral_class" Then
        '                check2 = True
        '            End If
        '            If column.ColumnName = "collateral_value" Then
        '                check3 = True
        '            End If
        '        Next
        '        If check1 = False Then
        '            lMsg &= "Field ACCTNO doesn't exists.\n"
        '        End If
        '        If check2 = False Then
        '            lMsg &= "Field collateral_class doesn't exists.\n"
        '        End If
        '        If check3 = False Then
        '            lMsg &= "Field collateral_value doesn't exists.\n"
        '        End If
        '    End If
        'Catch ex As Exception
        '    lMsg &= ex.Message.Replace("'", "\'")
        'End Try
        
        

        If lMsg <> "" Then
            lMsg = lMsg.Replace("'", "\'")
            lMsg = Replace(lMsg, vbCrLf, " ")
            'cs.RegisterStartupScript(cstype, "zzz", "<script language='javascript'> alert('" + lMsg + "' )</script>")
            liBodyLoadAction.Text = "alert('" + lMsg + "');"
        End If
    End Sub

    Sub SaveData()
        Try
            Dim stat As String
            If Request.Form("chkStatus") = "on" Then
                stat = "Active"
            Else
                stat = "Inactive"
            End If
            If mode = 1 Then
                oDm.updateDataManagement(Request, data_id, usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Data Management Updated - Name=" & Request.Form("txtDataName") & ";STATUS=" & stat)
                objLog.insertLog("dataMgmt", logs)
                oDm.deleteDataSourceJoin(data_id, usrLogin)
                Dim logs2 As String = objLog.getLogString(Request, Session("UserLoginID"), "Data Management Source Join Deletion - DATANAME=" & Request.Form("txtDataName"))
                objLog.insertLog("dataMgmtJoin", logs2)
            Else
                oDm.insertDataManagement(Request, usrLogin)
                Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Data Management Creation - Name=" & Request.Form("txtDataName") & ";STATUS=" & stat)
                objLog.insertLog("dataMgmt", logs)
            End If

            Dim ds_datasource = ""
            Dim ds_merging_condition = ""
            Dim ds_tbl_name = ""
            Dim ds_alias = ""
            Dim ds_fields = ""
            Dim ds_condition = ""
            Dim ds_merging_keys = ""
            Dim ds_data_id = data_id

            ds_datasource = ddlDataSourceStat.SelectedValue
            ds_tbl_name = ddlTableNameStat.SelectedValue
            ds_alias = txtAliasStat.Text
            ds_fields = txtFieldStat.Text
            ds_condition = txtConditionStat.Text

            oDm.insertDataJoin(ds_datasource, ds_merging_condition, ds_tbl_name, ds_fields, ds_condition, ds_merging_keys, ds_data_id, ds_alias, usrLogin)
            Dim logs3 As String = objLog.getLogString(Request, Session("UserLoginID"), "Data Management Source Join Creation - DATANAME=" & Request.Form("txtDataName"))
            objLog.insertLog("dataMgmtJoin", logs3)
            If Repeater1.Items.Count > 0 Then
                For Each i As RepeaterItem In Repeater1.Items
                    Dim ddlDataSourceDyn As DropDownList = DirectCast(i.FindControl("ddlDataSourceDyn"), DropDownList)
                    Dim ddlMergeCondDyn As DropDownList = DirectCast(i.FindControl("ddlMergeCondDyn"), DropDownList)
                    Dim ddlTableNameDyn As DropDownList = DirectCast(i.FindControl("ddlTableNameDyn"), DropDownList)
                    Dim txtAliasDyn As TextBox = DirectCast(i.FindControl("txtAliasDyn"), TextBox)
                    Dim txtFieldDyn As TextBox = DirectCast(i.FindControl("txtFieldDyn"), TextBox)
                    Dim txtConditionDyn As TextBox = DirectCast(i.FindControl("txtConditionDyn"), TextBox)
                    Dim txtMergeKeyDyn As TextBox = DirectCast(i.FindControl("txtMergeKeyDyn"), TextBox)

                    ds_datasource = ddlDataSourceDyn.SelectedValue
                    ds_merging_condition = ddlMergeCondDyn.SelectedValue
                    ds_tbl_name = ddlTableNameDyn.SelectedValue
                    ds_alias = txtAliasDyn.Text
                    ds_fields = txtFieldDyn.Text
                    ds_condition = txtConditionDyn.Text
                    ds_merging_keys = txtMergeKeyDyn.Text

                    oDm.insertDataJoin(ds_datasource, ds_merging_condition, ds_tbl_name, ds_fields, ds_condition, ds_merging_keys, ds_data_id, ds_alias, usrLogin)
                    Dim logs4 As String = objLog.getLogString(Request, Session("UserLoginID"), "Data Management Source Join Creation - DATANAME=" & Request.Form("txtDataName"))
                    objLog.insertLog("dataMgmtJoin", logs4)
                Next
            End If
            'liBodyLoadAction.Text = "alert('Data Management saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            liBodyLoadAction.Text = "alert('Data Management berhasil disimpan.');window.opener.location.href = window.opener.location.href;parent.window.close();"
        Catch ex As Exception
            Dim err As String = ex.Message.Replace("'", "\'")
            liBodyLoadAction.Text = "alert('" & err & "');"
        End Try
        
    End Sub

    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Dim lMsg As String = ""
        'Dim str As String

        'str = "SELECT " & datalevel & " FROM (" & tbl & ") TABLE1" 'Edited by harfiq
        With New lScorecard
            Dim errmsg As String = .IsQueryValid(getDataSource("chk_"))
            If errmsg <> "" Then
                lMsg &= errmsg.Replace("'", "\'")
                'liBodyLoadAction.Text = "alert('Query not valid. Please check field and condition');"
                'Else

                '    liBodyLoadAction.Text = "alert('Query valid');"
            End If
        End With

        'Try
        '    Dim ds As DataSet
        '    Dim dt As DataTable
        '    ds = oSc.checkQueryField(getDataSource("chk_"))
        '    dt = ds.Tables(0)
        '    If ddlDataType.SelectedValue = "Asset Category" Then
        '        Dim check1 As Boolean = False
        '        Dim check2 As Boolean = False
        '        Dim check3 As Boolean = False
        '        Dim check4 As Boolean = False
        '        Dim check5 As Boolean = False
        '        Dim check6 As Boolean = False
        '        Dim check7 As Boolean = False
        '        Dim check8 As Boolean = False
        '        Dim check9 As Boolean = False
        '        Dim check10 As Boolean = False
        '        Dim check11 As Boolean = False
        '        Dim check12 As Boolean = False
        '        For Each column As DataColumn In dt.Columns
        '            If column.ColumnName = "ACCTNO" Then
        '                check1 = True
        '            End If
        '            If column.ColumnName = "balance_amt" Then
        '                check2 = True
        '            End If
        '            If column.ColumnName = "plafon" Then
        '                check3 = True
        '            End If
        '            If column.ColumnName = "KOLEKTIBILITAS_BI" Then
        '                check4 = True
        '            End If
        '            If column.ColumnName = "LAS_CREDIT_RATING" Then
        '                check5 = True
        '            End If
        '            If column.ColumnName = "FACILITY_RATING" Then
        '                check6 = True
        '            End If
        '            If column.ColumnName = "CREDIT_LINE_AMT" Then
        '                check7 = True
        '            End If
        '            If column.ColumnName = "product_segment_grp" Then
        '                check8 = True
        '            End If
        '            If column.ColumnName = "product_grp" Then
        '                check9 = True
        '            End If
        '            If column.ColumnName = "economic_sector_grp" Then
        '                check10 = True
        '            End If
        '            If column.ColumnName = "branch_grp" Then
        '                check11 = True
        '            End If
        '            If column.ColumnName = "sisa_tenor_year" Then
        '                check12 = True
        '            End If
        '        Next
        '        If check1 = False Then
        '            lMsg &= "Field ACCTNO doesn't exists.\n"
        '        End If
        '        If check2 = False Then
        '            lMsg &= "Field balance_amt doesn't exists.\n"
        '        End If
        '        If check3 = False Then
        '            lMsg &= "Field plafon doesn't exists.\n"
        '        End If
        '        If check4 = False Then
        '            lMsg &= "Field KOLEKTIBILITAS_BI doesn't exists.\n"
        '        End If
        '        If check5 = False Then
        '            lMsg &= "Field LAS_CREDIT_RATING doesn't exists.\n"
        '        End If
        '        If check6 = False Then
        '            lMsg &= "Field FACILITY_RATING doesn't exists.\n"
        '        End If
        '        If check7 = False Then
        '            lMsg &= "Field CREDIT_LINE_AMT doesn't exists.\n"
        '        End If
        '        If check8 = False Then
        '            lMsg &= "Field product_segment_grp doesn't exists.\n"
        '        End If
        '        If check9 = False Then
        '            lMsg &= "Field product_grp doesn't exists.\n"
        '        End If
        '        If check10 = False Then
        '            lMsg &= "Field economic_sector_grp doesn't exists.\n"
        '        End If
        '        If check11 = False Then
        '            lMsg &= "Field branch_grp doesn't exists.\n"
        '        End If
        '        If check12 = False Then
        '            lMsg &= "Field sisa_tenor_year doesn't exists.\n"
        '        End If
        '    Else
        '        Dim check1 As Boolean = False
        '        Dim check2 As Boolean = False
        '        Dim check3 As Boolean = False
        '        For Each column As DataColumn In dt.Columns
        '            If column.ColumnName = "ACCTNO" Then
        '                check1 = True
        '            End If
        '            If column.ColumnName = "collateral_class" Then
        '                check2 = True
        '            End If
        '            If column.ColumnName = "collateral_value" Then
        '                check3 = True
        '            End If
        '        Next
        '        If check1 = False Then
        '            lMsg &= "Field ACCTNO doesn't exists.\n"
        '        End If
        '        If check2 = False Then
        '            lMsg &= "Field collateral_class doesn't exists.\n"
        '        End If
        '        If check3 = False Then
        '            lMsg &= "Field collateral_value doesn't exists.\n"
        '        End If
        '    End If
        'Catch ex As Exception
        '    lMsg &= ex.Message.Replace("'", "\'")
        'End Try

        If lMsg <> "" Then
            lMsg = lMsg.Replace("'", "\'")
            lMsg = Replace(lMsg, vbCrLf, " ")
            'cs.RegisterStartupScript(cstype, "zzz", "<script language='javascript'> alert('" + lMsg + "' )</script>")
            liBodyLoadAction.Text = "alert('Query is not valid. Please check field and condition');"
        Else
            liBodyLoadAction.Text = "alert('Query valid.');"
        End If
    End Sub
End Class