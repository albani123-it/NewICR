Imports System.Web.Script.Services
Imports Obout.Grid
Imports Obout.Interface

Public Class userPosition
    Inherits System.Web.UI.Page

    Public lFun As New lFunction
    Public AllowDelete As Boolean
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim lUPosition As New lUserPosition
    Dim objLog As New lLog

#Region " Global Variable "
    Dim gStrFormName = "document.Form1."
    Public Shared usrLogin As String
    Dim urlBase As String

    Public ReadOnly Property user_login() As String
        Get
            Return usrLogin
        End Get
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'BindGrid()
        'BindMasterData()

        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        liBodyLoadAction.Text = ""

        BindGrid()
    End Sub


    Sub BindGrid()
        Dim ds As DataSet = lUPosition.getDataUserPosition()
        'ds.Tables(0).Columns.Add("usr_en")
        'For Each dr In ds.Tables(0).Rows
        '    dr("usr_en") = lFun.EncryptString(dr("usr_userid"))
        'Next
        grdUserPosition.DataSource = ds
        grdUserPosition.DataBind()
    End Sub



    Sub TrimAllInput()
        txtCode.Text = txtCode.Text.Trim
        txtValue.Text = txtValue.Text.Trim
        txtDesc.Text = txtDesc.Text.Trim
    End Sub


    <System.Web.Services.WebMethod()>
    Public Shared Function getEditUserPosition(ByVal usp_value As String) As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim json As String = ""



        With New lUserPosition
            ds = .getUserPosition(usp_value)
            dt = ds.Tables(0)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim rows As New List(Of Dictionary(Of String, Object))()
            Dim row As Dictionary(Of String, Object)
            For Each dr As DataRow In dt.Rows
                row = New Dictionary(Of String, Object)()
                For Each col As DataColumn In dt.Columns
                    row.Add(col.ColumnName, dr(col))
                Next
                rows.Add(row)
            Next
            json = serializer.Serialize(rows)
        End With


        Return json

    End Function


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim uspCodeExit As DataTable
        Dim uspValueExit As DataTable

        TrimAllInput()
        CekInput()

        If liBodyLoadAction.Text = "" Then
            'Dim uspExist As DataTable
            Dim objLog As New lLog

            If hidID.Value <> "" Then


                Try

                    Dim value As String
                    Dim desc As String
                    Dim code As String

                    code = txtCode.Text
                    value = txtValue.Text
                    desc = txtDesc.Text

                    lUPosition.editRecord(code, value, desc, usrLogin)

                    'Dim logs As String = ""
                    'logs = objLog.getLogString(Request, Session("UserLoginID"), "User Position Updated - User Role Code=" + Request.Form("usp_code") + ";User Role Name=" + Request.Form("usp_value"))

                    'objLog.insertLog("usp_code", logs)
                    liBodyLoadAction.Text = "alert('User Position updated.');"
                    'liBodyLoadAction.Text = "alert('User Position Role updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"

                Catch ex As Exception
                    'liBodyLoadAction.Text = "alert('" & ex.Message & "');window.opener.location.href = window.opener.location.href;parent.window.close();"
                End Try


            Else


                Dim lMsg As String = ""
                Dim lAction As String = ""
                Try
                    uspCodeExit = lUPosition.checkUserPosition(txtCode.Text, "CK")
                    If uspCodeExit.Rows(0).Item("tRow") = 1 Then
                        liBodyLoadAction.Text = "alert('User Position Role Code is exist.');"
                    Else
                        uspValueExit = lUPosition.checkUserPosition(txtValue.Text, "CN")
                        insertUserPosition()
                    End If
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message & "');window.opener.location.href = window.opener.location.href;parent.window.close();window.refresh();"
                End Try


            End If


        End If

        ReBinGrid()
    End Sub

    Sub CekInput()
        Dim lMsg As String = ""
        Dim lAction As String = ""

        If txtCode.Text = "" Then
            lMsg = lMsg + "Please input Code Position.\n"
            If lAction = "" Then
                lAction = gStrFormName + txtCode.ID + ".focus()"
            End If
        ElseIf ViewState("mode") = "NEW" Then
            Try
                Select Case lUPosition.ValidateUser(txtValue.Text, "new")
                    Case 2
                        lMsg = "Code must have between 6 to 15 characters!\n"
                    Case 3
                        lMsg = "Value has already existed. Please input a different Value!\n"
                End Select
            Catch ex As Exception
                lMsg = ex.Message.Replace("'", "\'").Replace(vbCrLf, "\n") + ".\n"
                lAction = gStrFormName + txtValue.ID + ".focus()"
            End Try

        End If
        If txtValue.Text = "" Then
            lMsg = lMsg + "Please input User Value.\n"
            If lAction = "" Then
                lAction = gStrFormName + txtValue.ID + ".focus()"
            End If
        End If
        If txtDesc.Text = "" Then
            lMsg = lMsg + "Please input Description.\n"
            If lAction = "" Then
                lAction = gStrFormName + txtDesc.ID + ".focus()"
            End If
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" + lMsg + "');" + lAction
        End If
    End Sub

    Protected Sub delete(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim dataItem As Hashtable = TryCast(grdUserPosition.Rows(rowIndex).ToHashtable(), Hashtable)


        lUPosition.deleteUserPosition(dataItem("usp_code"), usrLogin)
        cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Data has been deleted successfully.');", True)
        ReBinGrid()

    End Sub

    Sub insertUserPosition()

        Dim uspCode As String = txtCode.Text
        Dim uspValue As String = txtValue.Text
        Dim uspDescription As String = txtDesc.Text

        Try

            lUPosition.InsertRecord(uspCode, uspValue, uspDescription, "crm_admin")
            liBodyLoadAction.Text = "alert('User Position saved.');"
            ReBinGrid()

        Catch ex As Exception
            liBodyLoadAction.Text = "alert('" & ex.Message & "');"
        End Try
    End Sub

    Sub ReBinGrid()
        BindGrid()
    End Sub
End Class