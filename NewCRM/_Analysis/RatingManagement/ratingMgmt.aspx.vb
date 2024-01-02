Imports System.Data.SqlClient

Public Class ratingMgmt
    Inherits System.Web.UI.Page
    Dim oRerat As New lRerating
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript
    Protected PageType As String

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()

        PageType = Request.QueryString("typ")

        If PageType = "Borrower" Then
            TxtLegend.Text = "Borrower Re-Rating"
            txtBorrowerCode.Visible = True
        ElseIf PageType = "Facility" Then
            TxtLegend.Text = "Facility Re-rating"
        End If

        If Not IsPostBack Then
            isiUlang()
            removeSession()
        End If
        grid1.Visible = False

        If PageType = "Borrower" Then
            Column3.Visible = False
            Column5.Visible = False
            Column8.Visible = False
            Column9.Visible = False
            Column10.Visible = False
        ElseIf PageType = "Facility" Then
            Column2.Visible = False
            Column4.Visible = False
            Column6.Visible = False
            Column7.Visible = False
            Column8.Visible = False
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

    Sub removeSession()
        Session.Remove("appno")
        Session.Remove("brwcode")
        Session.Remove("type")
    End Sub

    Sub isiUlang()
        If PageType = "Borrower" Then
            If txtBorrowerCode.Value = "" Then
                txtBorrowerCode.Value = Session("txtBorrowerCode")
            End If
            If txtNPWP.Value = "" Then
                txtNPWP.Value = Session("txtNPWP")
            End If
            If txtCIF.Value = "" Then
                txtCIF.Value = Session("txtCIF")
            End If
            If txtNama.Value = "" Then
                txtNama.Value = Session("txtNama")
            End If
        ElseIf PageType = "Facility" Then
            If txtApp.Value = "" Then
                txtApp.Value = Session("txtApp")
            End If
            If txtRek.Value = "" Then
                txtRek.Value = Session("txtRek")
            End If
        End If

        If Request.QueryString("rm") <> "" Then
            getData()
        End If

        If PageType = "Borrower" Then
            Session.Remove("txtBorrowerCode")
            Session.Remove("txtNPWP")
            Session.Remove("txtCIF")
            Session.Remove("txtNama")
        Else
            Session.Remove("txtApp")
            Session.Remove("txtRek")
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then
            getData()
        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If PageType = "Borrower" Then
            If txtBorrowerCode.Value = "" And txtNPWP.Value = "" And txtCIF.Value = "" And txtNama.Value = "" Then
                lMsg = "Please enter one of borrower information to be search.\n"
            End If
        ElseIf PageType = "Facility" Then
            If txtApp.Value = "" And txtRek.Value = "" Then
                lMsg = "Please enter one of facility information to be search.\n"
            End If
        End If
        
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Sub getData()
        Dim brwcode As String = ""
        Dim cif As String = ""
        Dim npwp As String = ""
        Dim nama As String = ""
        Dim noapp As String = ""
        Dim rek As String = ""

        brwcode = txtBorrowerCode.Value
        cif = txtCIF.Value
        npwp = txtNPWP.Value
        nama = txtNama.Value
        noapp = txtApp.Value
        rek = txtRek.Value

        Dim ds As New DataSet

        If PageType = "Borrower" Then
            ds = oRerat.getListReRating(brwcode, cif, npwp, nama, noapp, rek, "Borrower")
        ElseIf PageType = "Facility" Then
            ds = oRerat.getListReRating(brwcode, cif, npwp, nama, noapp, rek, "Facility")
        End If

        grid1.DataSource = ds
        grid1.DataBind()
        grid1.Visible = True
        CrtSession()
    End Sub

    Sub CrtSession()
        If PageType = "Borrower" Then
            Session("txtBorrowerCode") = txtBorrowerCode.Value
            Session("txtNPWP") = txtNPWP.Value
            Session("txtCIF") = txtCIF.Value
            Session("txtNama") = txtNama.Value
        ElseIf PageType = "Facility" Then
            Session("txtApp") = txtApp.Value
            Session("txtRek") = txtRek.Value
        End If
    End Sub
End Class