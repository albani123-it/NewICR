Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web.Services

Public Class standardized
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim allowEdit As Boolean

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        cekUserAuthentication()
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        allowEdit = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_std_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_std_edit" Or dtUser.Rows(i).Item("ld_lm_description") = "lvl_bsl_std_view" Then
                    allowEdit = True
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

    Function getAssetList()
        Dim htmlsource As String = ""
        Dim approach() As String = {"Standardized", "Foundation IRB", "Advanced IRB"}
        Dim status() As String = {"True.gif", "False.gif", "False.gif"}
        Dim ds As DataSet
        ds = oAsset.getListOfAsset

        Dim dt As DataTable
        dt = ds.Tables(0)

        Dim dsModel As DataSet = New DataSet("model")



        If dt.Rows.Count > 0 Then
            For j As Integer = 1 To dt.Rows.Count
                If dt.Rows(j - 1).Item("tmod") > 0 Then
                    Dim dtModel As DataTable = New DataTable()
                    dtModel = oAsset.getModelForAsset(dt.Rows(j - 1).Item("id"), 1)
                    dtModel.TableName = "dtModel_" & dt.Rows(j - 1).Item("id")
                    dsModel.Tables.Add(dtModel.Copy())
                End If
            Next

            Dim headsource As String = ""
            Dim oversource As String = ""

            For i As Integer = 1 To dt.Rows.Count
                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    headsource &= "<tr id='tr" + dt.Rows(i - 1).Item("id").ToString + "' style='background-color:#F6F5F0;'> "
                ElseIf dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    headsource &= "<tr id='trdet" + dt.Rows(i - 1).Item("id").ToString + "' style='background-color:#ddedf6;'> "
                End If



                'Pembeda Bold
                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    If dt.Rows(i - 1).Item("child") = 0 Then
                        headsource &= "<td></td>"
                    Else
                        headsource &= "<td><img id='img" + dt.Rows(i - 1).Item("id").ToString + "' src='../Images/ec_minus.png' style='cursor:pointer' onclick='expcoll(&#39;" + dt.Rows(i - 1).Item("id").ToString + "&#39;)' /></td>"
                    End If

                    headsource &= "<td width='30%' style='font-weight:bold;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                ElseIf dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    headsource &= "<td></td>"
                    headsource &= "<td width='30%' style='padding-left:50px;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                Else
                    headsource &= "<td></td>"
                    headsource &= "<td width='30%' style='padding-left:100px;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                End If

                'Menampilkan Description dan Approach
                headsource &= "<td width='30%'>" & dt.Rows(i - 1).Item("cat_desc") & "</td>"
                headsource &= "<td width='10%'>" & dt.Rows(i - 1).Item("cat_class") & "</td>"
                headsource &= "<td width='10%'>&nbsp;</td>"
                headsource &= "<td width='10%'>&nbsp;</td>"
                headsource &= "<td width='10%'>&nbsp;</td>"
                headsource &= "</tr>"

                If dsModel.Tables.Contains("dtModel_" & dt.Rows(i - 1).Item("id")) Then
                    With dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows
                        For k As Integer = 1 To dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).rows.Count
                            htmlsource &= oversource & headsource & "<tr id='trdetail" + dt.Rows(i - 1).Item("id").ToString + "_" + dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id").ToString + "' style='background-color:rgb(239, 237, 226);'> "
                            htmlsource &= "<td></td>"

                            htmlsource &= "<td style='padding-left:100px;height:25px;'>" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_name") & "</td>"
                            htmlsource &= "<td>" & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_desc") & "</td>"
                            htmlsource &= "<td>&nbsp;</td>"

                            htmlsource &= "<td>" & approach(dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_approach") - 1) & "</td>"
                            htmlsource &= "<td style='text-align:center;'><img id='dgUser__ctl2_imgStatus' src='../images/" & status(dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_status") - 1) & "' alt='' border='0' /></td>"
                            htmlsource &= "<td style='width:10%;'>"
                            If allowEdit = True Then
                                htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fModelDetail(" & dt.Rows(i - 1).Item("id") & "," & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & ");'>View</a>"
                            End If
                            htmlsource &= "</td>"
                            htmlsource &= "</tr>"
                            headsource = ""
                            oversource = ""
                        Next
                    End With
                End If

                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    oversource = headsource
                    headsource = ""
                End If
                If dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    headsource = ""
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
                dt = .getParentAsset(txt, 1)
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
            With New lAsset
                dt = .getModelForAsset(txt, 1)

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

End Class