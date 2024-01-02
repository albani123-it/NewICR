Imports Obout.Grid

Public Class glo_picklist
    Inherits System.Web.UI.Page
    Dim oDdl As New lDropdown
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim scriptKey As String = "UniqueKeyForThisScript"

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")

        cekUserAuthentication()
        grid2.Visible = False
        isiData()
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_glo_ddl" Then
                    tf = True
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

    Sub isiData()
        Dim ds As DataSet
        ds = oDdl.getListOfDdl
        grid1.DataSource = ds
        grid1.DataBind()
    End Sub

    Protected Sub detailDdl(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid1.Rows(rowIndex).ToHashtable(), Hashtable)
        ViewState("object") = dataItem("ldr_object")
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oDdl.getDetailOfDdl(dataItem("ldr_object"))
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            grid2.DataSource = ds
            grid2.DataBind()
        End If
        grid2.Visible = True
    End Sub

    Sub RebindGrid()
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oDdl.getDetailOfDdl(ViewState("object"))
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            grid2.DataSource = ds
            grid2.DataBind()
        End If
    End Sub

    Protected Sub deleteDetailDdl(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grid2.Rows(rowIndex).ToHashtable(), Hashtable)
    End Sub

    Sub UpdateRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim msg = ""
        msg = cekInput(e)
        If msg = "" Then
            Dim id As String = e.Record("ldd_id")
            Dim obj As String = e.Record("ldd_object")
            Dim code As String = e.Record("ldd_code")
            Dim desc As String = e.Record("ldd_desc")
            Dim stats As Boolean
            If e.Record("ldd_status") = "Active" Then
                stats = True
            Else
                stats = False
            End If


            With New lDropdown
                ''.updateGlobalEligibleIRB(ID, CollateralClass, MinLGD, MinCollateralLevel, MaxCollateralLevel, usrLogin)
                '.updateGlobalEligibleIRB(ID, CollateralClass, MinLGD, MinCollateralLevel, MaxCollateralLevel)
                'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Eligible IRB Collateral Updated")
                'objLog.insertLog("glo_irb_col", logs)
                Try
                    .updateDdlDetail(id, obj, code, desc, stats, usrLogin)
                Catch ex As Exception
                    Dim script As String = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    Dim javascript = "<script type='text/javascript'>" & script & "</script>"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
                    'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & ex.Message.Replace("'", "\'") & "');", True)
                End Try
            End With
        Else
            Dim script As String = "alert('" & msg & "');"
            Dim javascript = "<script type='text/javascript'>" & script & "</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
        End If
    End Sub

    Sub InsertRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        'Dim obj As String = e.Record("ldd_object")
        Dim msg = ""
        msg = cekInput(e)
        If msg = "" Then
            Dim obj As String = ViewState("object")
            Dim code As String = e.Record("ldd_code")
            Dim desc As String = e.Record("ldd_desc")
            Dim stats As Boolean
            If e.Record("ldd_status") = "Active" Then
                stats = True
            Else
                stats = False
            End If

            With New lDropdown
                'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Eligible IRB Collateral Updated")
                'objLog.insertLog("glo_irb_col", logs)
                Try
                    .insertDdlDetail(obj, code, desc, stats, usrLogin)
                Catch ex As Exception
                    Dim script As String = "alert('" & ex.Message.Replace("'", "\'") & "');"
                    Dim javascript = "<script type='text/javascript'>" & script & "</script>"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
                    'cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & ex.Message.Replace("'", "\'") & "');", True)
                End Try
            End With
        Else
            Dim script As String = "alert('" & msg & "');"
            Dim javascript = "<script type='text/javascript'>" & script & "</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javascript)
        End If
        
    End Sub

    Sub DeleteRecord(ByVal sender As Object, ByVal e As GridRecordEventArgs)
        Dim id As String = e.Record("ldd_id")
        With New lDropdown
            Try
                .deleteDdlDetail(id, usrLogin)
                'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Global Setting Eligible IRB Collateral Updated")
                'objLog.insertLog("glo_irb_col", logs)
            Catch ex As Exception
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & ex.Message.Replace("'", "\'") & "');", True)
            End Try
        End With
    End Sub

    Function cekInput(ByVal e)
        Dim msg As String = ""

        If e.Record("ldd_code") = "" Then
            msg &= "Please fill code.\n"
        End If

        If e.Record("ldd_desc") = "" Then
            msg &= "Please fill description.\n"
        End If

        If e.Record("ldd_status") = "" Then
            msg &= "Please select status.\n"
        End If

        Return msg
    End Function
End Class