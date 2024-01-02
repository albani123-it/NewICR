Public Class simulationResult
    Inherits System.Web.UI.Page
    Dim oStress As New lStress
    Dim oGen As New lGeneral

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        liBodyLoadAction.Text = ""
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            If Not IsPostBack Then
                isiData(Request.QueryString("id"))
            End If
        End If
    End Sub

    Sub cekUserAuthentication()

    End Sub

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oStress.getDetailStress(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            lblName.Text = dt.Rows(0).Item("ssm_name")
            lblDesc.Text = dt.Rows(0).Item("ssm_desc")
            lblFinishDate.Text = oGen.CheckNullString(dt.Rows(0).Item("ssm_finish_date"))
        End If
    End Sub

    Function isiDataStressResult()
        Dim html As String = ""
        Dim colors As String = ""
        Dim ending As String = ""
        Dim ds As DataSet
        Dim dt As DataTable
        If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
            ds = oStress.getDetailStressResult(Request.QueryString("id"))
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                html &= "<table cellspacing='0' cellpadding='1' width='100%' style='text-align:left;border: thin solid #bcbcbc;'><tr><td style='padding-left:0px;'>"
                For i As Integer = 1 To dt.Rows.Count
                    If i Mod 2 = 0 Then
                        colors = "style='background-color:#ebecec;border-bottom:1px solid black;'"
                    Else
                        colors = "style='background-color:#f6f7f7;border-bottom:1px solid black;'"
                    End If
                    html += "<table id='tblMin" & dt.Rows(i - 1).Item("sst_id").ToString & "' cellspacing='0' cellpadding='1' width='100%' style='text-align:left;display:none;'>"
                    html += "<tr><td width='10px'><img id='imgMin" + dt.Rows(i - 1).Item("sst_id").ToString + "' src='../Images/ec_plus.png' style='cursor:pointer' onclick='expcoll(&#39;" + dt.Rows(i - 1).Item("sst_id").ToString + "&#39;)' /></td>"
                    html += "<td width='20%'><strong>Category Name</strong></td>"
                    html += "<td width='20%'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                    html += "<td width='20%'>&nbsp;</td>"
                    html += "<td width='20%'>&nbsp;</td>"
                    html += "<td width='20%'>&nbsp;</td>"
                    html += "</tr></table>"

                    html += "<table id='tbl" & dt.Rows(i - 1).Item("sst_id").ToString & "' cellspacing='0' cellpadding='1' width='100%' style='text-align:left;'>"
                    html += "<tr " & colors & ">"
                    html += "<td width='10px'><img id='img" + dt.Rows(i - 1).Item("sst_id").ToString + "' src='../Images/ec_minus.png' style='cursor:pointer' onclick='expcoll(&#39;" + dt.Rows(i - 1).Item("sst_id").ToString + "&#39;)' /></td>"
                    html += "<td width='20%'><strong>Category Name</strong></td>"
                    html += "<td width='20%'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                    html += "<td width='20%'>&nbsp;</td>"
                    html += "<td width='20%'>PD Stress</td>"
                    html += "<td width='20%'>" & dt.Rows(i - 1).Item("sst_pd_shock_cond") & dt.Rows(i - 1).Item("sst_pd_shock") & "&nbsp;" & dt.Rows(i - 1).Item("sst_pd_shock_type") & "</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Model Name</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("mod_name") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>LGD Stress</td>"
                    html += "<td>" & dt.Rows(i - 1).Item("sst_lgd_shock_cond") & dt.Rows(i - 1).Item("sst_lgd_shock") & "&nbsp;" & dt.Rows(i - 1).Item("sst_lgd_shock_type") & "</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Approach Type</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("mod_appr") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>EAD</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("ead_value") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr style='background-color:rgb(243, 233, 208);'>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Original</strong></td>"
                    html += "<td><strong>Result</strong></td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Capital Charge</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("capital_charge") & "</td>"
                    html += "<td>" & dt.Rows(i - 1).Item("capital_charge_stress") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Expected Loss</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("expected_loss") & "</td>"
                    html += "<td>" & dt.Rows(i - 1).Item("expected_loss_stress") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Risk Weight Asset (RWA)</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("rwa") & "</td>"
                    html += "<td>" & dt.Rows(i - 1).Item("rwa_stress") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Unexpected Loss</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("unexpected_loss") & "</td>"
                    html += "<td>" & dt.Rows(i - 1).Item("unexpected_loss_stress") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr>"
                    html += "<tr " & colors & ">"
                    html += "<td>&nbsp;</td>"
                    html += "<td><strong>Risk Premium</strong></td>"
                    html += "<td>" & dt.Rows(i - 1).Item("risk_premium") & "</td>"
                    html += "<td>" & dt.Rows(i - 1).Item("risk_premium_stress") & "</td>"
                    html += "<td>&nbsp;</td>"
                    html += "<td>&nbsp;</td>"
                    html += "</tr></table>"

                Next
                html &= "</td></tr></table>"
            End If
        End If
        Return html
    End Function

End Class