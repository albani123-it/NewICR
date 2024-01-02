Public Class ratingMgmtDet
    Inherits System.Web.UI.Page
    Dim oApp As New lApps
    Dim oRat As New lRating
    Dim oGen As New lGeneral
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim stat As String = ""

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not IsPostBack Then
            fillDdl()
        End If
        If Request.QueryString("appno") IsNot Nothing Then
            appno.Value = Request.QueryString("appno")
            tblExists2.Visible = False
        End If
        If Request.QueryString("brw_code") IsNot Nothing Then
            brw_code.Value = Request.QueryString("brw_code")
            tblExists.Visible = False
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

    Sub fillDdl()
        Dim ds As DataSet
        Dim dt As DataTable

        ds = oApp.getListOfBorrowerType()
        dt = ds.Tables(0)
        ddlBrwTypEx.Items.Clear()
        ddlBrwTypEx.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlBrwTypEx.Items.Add(New ListItem(dt.Rows(i).Item("brw_type"), dt.Rows(i).Item("brw_id")))
            Next
        End If

        ds = oApp.getListOfRetail()
        dt = ds.Tables(0)
        ddlRetailEx.Items.Clear()
        ddlRetailEx.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlRetailEx.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
            Next
        End If

        ds = oApp.getListOfProduct()
        dt = ds.Tables(0)
        ddlPrdTypEx.Items.Clear()
        ddlPrdTypEx.Items.Add(New ListItem("Pilih", "Pilih"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlPrdTypEx.Items.Add(New ListItem(dt.Rows(i).Item("prd_name"), dt.Rows(i).Item("prd_id")))
            Next
        End If

    End Sub

    Private Sub btnSaveEx_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveEx.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            If appno.Value <> "" Then
                Try
                    Dim arr(2)
                    
                    arr(0) = ddlPrdTypEx.SelectedValue
                    arr(1) = chkRetailEx.Checked
                    arr(2) = ddlRetailEx.SelectedValue

                    oRat.updateModelFacilityRerating(arr, appno.Value)

                    liBodyLoadAction.Text = "alert('Data berhasil di simpan.');parent.window.close();window.opener.location.href = 'R_General.aspx?appno=" & appno.Value & "&typ=fac&sts=1';"
                Catch ex As Exception
                    liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
                End Try
            End If
            
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If appno.Value <> "" Then
            If chkRetailEx.Checked Then
                lMsg &= oGen.checkInputInaJavascript("DropDownList", "Product Micro Type", ddlRetailEx.SelectedValue)
            Else
                lMsg &= oGen.checkInputInaJavascript("DropDownList", "Borrower Type", ddlBrwTypEx.SelectedValue)
                lMsg &= oGen.checkInputInaJavascript("DropDownList", "Product Type", ddlPrdTypEx.SelectedValue)
            End If
        End If
        If brw_code.Value <> "" Then

            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Borrower Type", ddlBrwTypEx.SelectedValue)

        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Private Sub btnSaveEx2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveEx2.Click
        cekInput2()
        If brw_code.Value <> "" Then
            Try

                oRat.updateModelBrwRerating(ddlBrwTypEx.SelectedValue, brw_code.Value)

                liBodyLoadAction.Text = "alert('Data berhasil di simpan.');parent.window.close();window.opener.location.href = 'R_General.aspx?brwcode=" & brw_code.Value & "&typ=brw&sts=1';"
            Catch ex As Exception
                liBodyLoadAction.Text = "alert('" & ex.Message.Replace("'", "\'") & "');"
            End Try
        End If
    End Sub

    Sub cekInput2()
        Dim lMsg As String = ""
        If brw_code.Value <> "" Then

            lMsg &= oGen.checkInputInaJavascript("DropDownList", "Borrower Type", ddlBrwTypEx.SelectedValue)

        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub
End Class