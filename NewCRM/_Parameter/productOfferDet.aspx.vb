Public Class productOfferDet
    Inherits System.Web.UI.Page
    Dim oPrdOffer As New lProductOffer
    Dim objLog As New lLog
#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        Dim prd_id As String

        If Not IsPostBack Then
            fillCategory()
            fillType()
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("productOffer_id")) Then
            prd_id = Request.QueryString("productOffer_id")
            If prd_id <> -1 And Not IsPostBack Then
                isiData(Convert.ToString(prd_id))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        Dim edit As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_edit" Then
                    edit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_prd_mgt_view" Then
                    If edit = False Then
                        txtOfferingCode.Enabled = False
                        ddlCategory.Enabled = False
                        txtProduct.Enabled = False
                        ddlType.Enabled = False
                        txtSLA.Enabled = True
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

    Sub fillCategory()
        Dim ds As DataSet
        ds = oPrdOffer.getDropDownlistData("CATEOFF")
        ddlCategory.DataSource = ds
        ddlCategory.DataValueField = "dd_value"
        ddlCategory.DataTextField = "dd_text"
        ddlCategory.DataBind()


    End Sub

    Sub fillType()
        Dim ds As DataSet
        ds = oPrdOffer.getDropDownlistData("TYPEOFF")
        ddlType.DataSource = ds
        ddlType.DataValueField = "dd_value"
        ddlType.DataTextField = "dd_text"
        ddlType.DataBind()
    End Sub

    Sub isiData(ByVal po_id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oPrdOffer.getDataProductOfferById(po_id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            prod_id.Value = dt.Rows(0).Item("po_id")
            txtOfferingCode.Text = dt.Rows(0).Item("po_offering_code")
            ddlCategory.SelectedValue = dt.Rows(0).Item("po_category")
            txtProduct.Text = dt.Rows(0).Item("po_product")
            ddlType.SelectedValue = dt.Rows(0).Item("po_type")
            txtSLA.Text = dt.Rows(0).Item("po_sla")
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""

        If txtOfferingCode.Text = "" Then
            lMsg &= "Please input Offering Code. \n"
        End If
        If txtProduct.Text = "" Then
            lMsg &= "Please input Product. \n"
        End If

        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        Dim stat As String
        Dim oCe As New enRating
        Dim prd_id As String

        prd_id = Request.QueryString("productOffer_id").ToString()
        If Integer.Parse(prd_id) > 0 Then
            'update
            Try
                oPrdOffer.updateDataProductOffering(Request, usrLogin)
                liBodyLoadAction.Text = "alert('product Offering updated.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        Else
            Try
                oPrdOffer.insertDataProductOffering(Request, usrLogin)
                liBodyLoadAction.Text = "alert('product Offering saved.');window.opener.location.href = window.opener.location.href;parent.window.close();"
            Catch ex As Exception
                Dim msg As String = ex.Message.Replace("'", "\'")
                liBodyLoadAction.Text = "alert('" & msg & "');"
            End Try
        End If

    End Sub

End Class