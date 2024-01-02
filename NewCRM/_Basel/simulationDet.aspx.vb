Public Class simulationDet
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim oSim As New lSimulation
    Dim objLog As New lLog
    Protected edit As Boolean = False

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
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_simulation" Then
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

    Sub isiData(ByVal id)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oSim.getDetailSimulation(id)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            txtStsName.Text = dt.Rows(0).Item("sim_name")
            txtStsDesc.Text = dt.Rows(0).Item("sim_desc")
            txtData.Text = dt.Rows(0).Item("sim_data")
        End If
    End Sub

    Function getAssetList()
        Dim htmlsource As String = ""
        Dim approach() As String = {"Standardized", "Foundation IRB", "Advanced IRB"}
        Dim status() As String = {"True.gif", "False.gif", "False.gif"}

        Dim ds As DataSet
        ds = oSim.getListOfAssetSimulation

        Dim dt As DataTable
        dt = ds.Tables(0)

        Dim dsModel As DataSet = New DataSet("model")
        If dt.Rows.Count > 0 Then
            For j As Integer = 1 To dt.Rows.Count
                If dt.Rows(j - 1).Item("tmod") > 0 Then
                    Dim dtModel As DataTable = New DataTable()
                    dtModel = oSim.getListOfAssetModelSimulation(dt.Rows(j - 1).Item("id"))
                    dtModel.TableName = "dtModel_" & dt.Rows(j - 1).Item("id")
                    dsModel.Tables.Add(dtModel.Copy())
                End If
            Next

            For i As Integer = 1 To dt.Rows.Count
                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    htmlsource &= "<tr id='tr" + dt.Rows(i - 1).Item("id").ToString + "' style='background-color:#F6F5F0;'> "
                ElseIf dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    htmlsource &= "<tr id='trdet" + dt.Rows(i - 1).Item("id").ToString + "' style='background-color:rgb(243, 233, 208);'> "
                End If



                'Pembeda Bold
                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    If dt.Rows(i - 1).Item("child") = 0 Then
                        htmlsource &= "<td width='12px'></td>"
                    Else
                        htmlsource &= "<td width='12px'><img id='img" + dt.Rows(i - 1).Item("id").ToString + "' src='../Images/ec_minus.png' style='cursor:pointer' onclick='expcoll(&#39;" + dt.Rows(i - 1).Item("id").ToString + "&#39;)' /></td>"
                    End If

                    htmlsource &= "<td width='20%' style='font-weight:bold;height:25px;min-width:325px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                ElseIf dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    htmlsource &= "<td></td>"
                    htmlsource &= "<td width='25%' style='padding-left:50px;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                Else
                    htmlsource &= "<td></td>"
                    htmlsource &= "<td width='25%' style='padding-left:100px;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                End If

                'Menampilkan Description dan Approach
                htmlsource &= "<td width='25%'>" & dt.Rows(i - 1).Item("cat_desc") & "</td>"
                htmlsource &= "<td width='15%'>" & dt.Rows(i - 1).Item("cat_class") & "</td>"
                htmlsource &= "<td width='15%'>&nbsp;</td>"
                htmlsource &= "<td width='10%'>&nbsp;</td>"
                htmlsource &= "<td style='width:10%;'>"
                htmlsource &= "&nbsp;</td>"
                htmlsource &= "</tr>"

                If dsModel.Tables.Contains("dtModel_" & dt.Rows(i - 1).Item("id")) Then
                    With dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows
                        For k As Integer = 1 To dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows.Count

                            htmlsource &= "<tr id='trdetail" + dt.Rows(i - 1).Item("id").ToString + "_" + dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id").ToString + "' style='background-color:rgb(239, 237, 226);'> "
                            htmlsource &= "<td></td>"

                            htmlsource &= "<td style='padding-left:100px;height:25px;'>" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_name") & "</td>"
                            htmlsource &= "<td>" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_desc") & "</td>"
                            htmlsource &= "<td>&nbsp;</td>"

                            htmlsource &= "<td>" & approach(dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_approach") - 1) & "</td>"
                            htmlsource &= "<td style='text-align:center;'><img id='dgUser__ctl2_imgStatus' src='../images/" & status(dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_status") - 1) & "' alt='' border='0' /></td>"
                            If Request.QueryString("id") <> -1 And Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                                Dim dst As DataSet
                                Dim dtbl As DataTable
                                dst = oSim.getSimulationDetail(Request.QueryString("id"), dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id"))
                                dtbl = dst.Tables(0)
                                If dtbl.Rows.Count > 0 Then
                                    htmlsource &= "<td style='text-align:center;'><input type='checkbox' checked id='chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & "' name='chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & "' /></td>"
                                    htmlsource &= "</td></tr>"
                                Else
                                    htmlsource &= "<td style='text-align:center;'><input type='checkbox' id='chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & "' name='chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & "' /></td>"
                                    htmlsource &= "</td></tr>"
                                End If
                            Else
                                htmlsource &= "<td style='text-align:center;'><input type='checkbox' id='chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & "' name='chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & "' /></td>"
                                htmlsource &= "</td></tr>"
                            End If
                        Next
                    End With
                End If
            Next
        End If

        Return htmlsource
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function GetData(ByVal txt)
        Dim html = ""
        html += txt.ToString & ","
        Dim dt As New DataTable
        Try
            With New lAsset
                dt = .getParentAsset(txt, 0)
                ' += dt.Rows.Count.ToString
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If i = dt.Rows.Count - 1 Then
                            html &= dt.Rows(i).Item(0).ToString
                        Else
                            html &= dt.Rows(i).Item(0).ToString & ","
                        End If
                    Next
                Else
                    html = txt.ToString
                End If
            End With
        Catch ex As Exception
            html += ex.ToString
        End Try
        Return html
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetDataModel(ByVal txt)
        Dim html = ""
        html += txt.ToString & ","
        Dim dt As New DataTable
        Try
            With New lStress
                dt = .getListOfAssetModelStress(txt)

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If i = dt.Rows.Count - 1 Then
                            html &= dt.Rows(i).Item(0).ToString
                        Else
                            html &= dt.Rows(i).Item(0).ToString & ","
                        End If
                    Next
                Else
                    html = txt.ToString
                End If
            End With
        Catch ex As Exception
            html += ex.ToString
        End Try
        Return html
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        cekInput()
        If liBodyLoadAction.Text = "" Then

            If Request.QueryString("id") <> -1 Then
                Try
                    oSim.insertSimulation(Request.QueryString("id"), usrLogin, txtStsName.Text, txtStsDesc.Text, txtData.Text)
                    editStress(Request.QueryString("id"))
                    liBodyLoadAction.Text = "alert('Simulation updated.');window.opener.location.href = window.opener.location.href; parent.window.close();"
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            Else
                Try
                    oSim.insertSimulation(0, usrLogin, txtStsName.Text, txtStsDesc.Text, txtData.Text)
                    Dim ds As DataSet
                    Dim dt As DataTable
                    ds = oSim.getLastSimulationId
                    dt = ds.Tables(0)
                    If dt.Rows.Count > 0 Then
                        insertStress(dt.Rows(0).Item("sim_id"))
                        liBodyLoadAction.Text = "alert('Simulation saved.');window.opener.location.href = window.opener.location.href; parent.window.close();"
                    End If
                Catch ex As Exception
                    Dim msg As String = ex.Message.Replace("'", "\'")
                    liBodyLoadAction.Text = "alert('" & msg & "');"
                End Try
            End If

        End If
    End Sub

    Sub cekInput()
        Dim lMsg As String = ""
        If txtStsName.Text = "" Then
            lMsg = "Please input stress test name."
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" & lMsg & "');"
        End If
    End Sub

    Sub insertStress(ByVal sID)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getListOfAsset
        dt = ds.Tables(0)
        Dim dsModel As DataSet = New DataSet("model")
        If dt.Rows.Count > 0 Then
            For j As Integer = 1 To dt.Rows.Count
                If dt.Rows(j - 1).Item("tmod") > 0 Then
                    Dim dtModel As DataTable = New DataTable()
                    dtModel = oSim.getListOfAssetModelSimulation(dt.Rows(j - 1).Item("id"))
                    dtModel.TableName = "dtModel_" & dt.Rows(j - 1).Item("id")
                    dsModel.Tables.Add(dtModel.Copy())
                End If
            Next
            For i As Integer = 1 To dt.Rows.Count
                If dsModel.Tables.Contains("dtModel_" & dt.Rows(i - 1).Item("id")) Then
                    With dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows
                        For k As Integer = 1 To dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows.Count
                            Dim moid As String = ""
                            If Request.Form("chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id")) = "on" Then
                                moid = dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id")
                                oSim.insertSimulationDetail(sID, moid, usrLogin, "NEW")
                            End If
                        Next
                    End With
                End If
            Next
        End If

    End Sub

    Sub editStress(ByVal sID)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getListOfAsset
        dt = ds.Tables(0)
        Dim dsModel As DataSet = New DataSet("model")
        If dt.Rows.Count > 0 Then
            For j As Integer = 1 To dt.Rows.Count
                If dt.Rows(j - 1).Item("tmod") > 0 Then
                    Dim dtModel As DataTable = New DataTable()
                    dtModel = oSim.getListOfAssetModelSimulation(dt.Rows(j - 1).Item("id"))
                    dtModel.TableName = "dtModel_" & dt.Rows(j - 1).Item("id")
                    dsModel.Tables.Add(dtModel.Copy())
                End If
            Next
            For i As Integer = 1 To dt.Rows.Count
                If dsModel.Tables.Contains("dtModel_" & dt.Rows(i - 1).Item("id")) Then
                    With dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows
                        For k As Integer = 1 To dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows.Count
                            Dim moid As String = ""
                            If Request.Form("chkBox_" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id")) = "on" Then
                                moid = dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id")
                                oSim.insertSimulationDetail(sID, moid, usrLogin, "EDIT")
                            End If
                        Next
                    End With
                End If
            Next
        End If
    End Sub
End Class