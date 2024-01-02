Imports System.Data.SqlClient
Imports Obout.Grid
Imports Obout.Ajax
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web.Services

Public Class asset
    Inherits System.Web.UI.Page
    Dim oAsset As New lAsset
    Dim objLog As New lLog
    Protected AllowAdd As Boolean
    Dim AllowDelete As Boolean
    Dim AllowEdit As Boolean
    Dim AllowAddModel As Boolean
    Dim AllowEditModel As Boolean
    Dim AllowDeleteModel As Boolean
    Dim urlBase As String
    Dim cs As ClientScriptManager = Page.ClientScript

#Region " Global Variable "
    Dim usrLogin As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        usrLogin = IIf(IsDBNull(Session("UserLoginID")), "IUser", Session("UserLoginID"))
        urlBase = Session("baseUrl")
        cekUserAuthentication()
        If Not String.IsNullOrEmpty(Request.QueryString("del")) Then
            If Request.QueryString("del") = "Category" Then
                If Not String.IsNullOrEmpty(Request.QueryString("astid")) And Not String.IsNullOrEmpty(Request.QueryString("par")) Then
                    If Request.QueryString("par") >= 0 And Request.QueryString("par") < 2 Then
                        deleteCategory(Request.QueryString("astid"), Request.QueryString("par"))
                    End If
                End If
            ElseIf Request.QueryString("del") = "Model" Then
                If Not String.IsNullOrEmpty(Request.QueryString("astid")) And Not String.IsNullOrEmpty(Request.QueryString("astid")) Then
                    deleteModel(Request.QueryString("astid"), Request.QueryString("moid"))
                End If
            End If
        End If
    End Sub

    Sub cekUserAuthentication()
        Dim dtUser As DataTable = Session("dtUser")
        Dim tf As Boolean
        AllowAdd = False
        AllowEdit = False
        AllowAddModel = False
        AllowEditModel = False
        AllowDeleteModel = False
        AllowDelete = False
        Try
            For i As Integer = 0 To dtUser.Rows.Count - 1
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mgt" Then
                    tf = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_new" Then
                    AllowAdd = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_edit" Then
                    AllowEdit = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_delete" Then
                    AllowDelete = True
                End If

                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mdl_new" Then
                    AllowAddModel = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mdl_edit" Then
                    AllowEditModel = True
                End If
                If dtUser.Rows(i).Item("ld_lm_description") = "lvl_prm_ast_mdl_delete" Then
                    AllowDeleteModel = True
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
                    dtModel = oAsset.getModelForAsset(dt.Rows(j - 1).Item("id"), 0)
                    dtModel.TableName = "dtModel_" & dt.Rows(j - 1).Item("id")
                    dsModel.Tables.Add(dtModel.Copy())
                End If
            Next

            For i As Integer = 1 To dt.Rows.Count
                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    htmlsource &= "<tr id='tr" + dt.Rows(i - 1).Item("id").ToString + "' style='background-color:#F6F5F0;'> "
                ElseIf dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    htmlsource &= "<tr id='trdet" + dt.Rows(i - 1).Item("id").ToString + "' style='background-color:#ddedf6;'> "
                End If



                'Pembeda Bold
                If dt.Rows(i - 1).Item("cat_typ") = 1 Then
                    If dt.Rows(i - 1).Item("child") = 0 Then
                        htmlsource &= "<td></td>"
                    Else
                        htmlsource &= "<td><img id='img" + dt.Rows(i - 1).Item("id").ToString + "' src='../Images/ec_minus.png' style='cursor:pointer' onclick='expcoll(&#39;" + dt.Rows(i - 1).Item("id").ToString + "&#39;)' /></td>"
                    End If

                    htmlsource &= "<td width='30%' style='font-weight:bold;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                ElseIf dt.Rows(i - 1).Item("cat_typ") = 2 Then
                    htmlsource &= "<td></td>"
                    htmlsource &= "<td width='30%' style='padding-left:50px;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                Else
                    htmlsource &= "<td></td>"
                    htmlsource &= "<td width='30%' style='padding-left:100px;height:25px;'>" & dt.Rows(i - 1).Item("cat_name") & "</td>"
                End If

                'Menampilkan Description dan Approach
                htmlsource &= "<td width='30%'>" & dt.Rows(i - 1).Item("cat_desc") & "</td>"
                htmlsource &= "<td width='10%'>" & dt.Rows(i - 1).Item("cat_class") & "</td>"
                htmlsource &= "<td width='10%'>&nbsp;</td>"
                htmlsource &= "<td width='5%'>&nbsp;</td>"
                If dt.Rows(i - 1).Item("cat_parent") = 0 Then

                    htmlsource &= "<td style='width:15%;'>"
                    If AllowEdit = True Then
                        htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fAssetDetail(" & dt.Rows(i - 1).Item("id") & ");'>Edit</a>&nbsp;&nbsp;"
                    End If
                    If AllowDelete = True Then
                        htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fDelete(" & dt.Rows(i - 1).Item("id") & ",0);'>Delete</a>"
                    End If
                    htmlsource &= "</td>"
                Else
                    htmlsource &= "<td style='width:15%;'>"
                    If AllowAddModel = True Then
                        htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fModelDetail(" & dt.Rows(i - 1).Item("id") & ",0);'>Add Model</a>&nbsp;&nbsp;"
                    End If
                    If AllowEdit = True Then
                        htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fAssetDetail(" & dt.Rows(i - 1).Item("id") & ");'>Edit</a>&nbsp;&nbsp;"
                    End If
                    If AllowDelete = True Then
                        htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fDelete(" & dt.Rows(i - 1).Item("id") & ",1);'>Delete</a>"
                    End If
                    htmlsource &= "</td>"
                    End If

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
                            htmlsource &= "<td style='text-align:center;'><img id='dgUser__ctl2_imgStatus' src='../images/" & status(dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_status") - 1) & "' Title='" & IIf(status(dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_status") - 1) = "False.gif", "Inactive In Basel", "Active In Basel") & "' border='0' /></td>"
                                htmlsource &= "<td style='width:10%;'>"
                            If AllowEditModel = True Then
                                htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fModelDetail(" & dt.Rows(i - 1).Item("id") & "," & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & ");'>Edit</a>&nbsp;&nbsp;"
                            End If
                            If AllowDeleteModel = True Then
                                htmlsource &= "<a style='color:#1F4697;font-weight:bold;' href='javascript:fModelDelete(" & dt.Rows(i - 1).Item("id") & "," & dsModel.Tables("dtModel_" & dt.Rows(i - 1).Item("id")).Rows(k - 1).Item("mod_id") & ");'>Delete</a>"
                            End If
                            htmlsource &= "</td>"
                            htmlsource &= "</tr>"
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
            With New lAsset
                dt = .getModelForAsset(txt, 0)

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

    Sub deleteCategory(ByVal astid, ByVal par)
        Dim count As Integer = 0
        Dim ds As DataSet
        Dim dat As DataTable
        ds = oAsset.getDetailOfAsset(astid)
        dat = ds.Tables(0)
        If par = 0 Then
            Dim dt As DataTable
            dt = oAsset.getParentAsset(astid, 0)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim dtbl As DataTable
                    dtbl = oAsset.getModelForAsset(dt.Rows(i).Item("id"), 0)
                    If dtbl.Rows.Count > 0 Then
                        For j As Integer = 0 To dtbl.Rows.Count - 1
                            If dtbl.Rows(j).Item("mod_status") = 1 Then
                                count = count + 1
                            End If
                Next
                    End If
                Next
            End If

            If count > 0 Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Cannot delete because there is active model.');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
            Else
                Try
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Dim dtbl As DataTable
                            dtbl = oAsset.getModelForAsset(dt.Rows(i).Item("id"), 0)
                            If dtbl.Rows.Count > 0 Then
                                For j As Integer = 0 To dtbl.Rows.Count - 1
                                    Dim stat As String
                                    If dtbl.Rows(j).Item("mod_status") = 1 Then
                                        stat = "Active"
                                    Else
                                        stat = "Inactive"
                                    End If
                                    oAsset.deleteModelAsset(dtbl.Rows(j).Item("mod_id"), usrLogin)
                                    Dim logModel As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model Deleted - MODELNAME=" & dtbl.Rows(j).Item("mod_name") & ";STATUS=" & stat)
                                    objLog.insertLog("asset_model", logModel)
                                Next
                            End If
                            oAsset.deleteAsset(dt.Rows(i).Item("id"), usrLogin)
                            Dim logSubCat As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Deleted - ID=" & dt.Rows(i).Item("id") & ";CATEGORY=" & dt.Rows(i).Item("cat_name"))
                            objLog.insertLog("asset", logSubCat)
                        Next
                    End If
                    oAsset.deleteAsset(astid, usrLogin)
                    Dim logCat As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Deleted - ID=" & astid & ";CATEGORY=" & dat.Rows(0).Item("cat_name"))
                    objLog.insertLog("asset", logCat)
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Asset Category has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
                Catch ex As Exception
                    Dim err As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & err & "');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
                End Try

            End If

        ElseIf par = 1 Then
            Dim dtbl As DataTable
            dtbl = oAsset.getModelForAsset(astid, 0)
            If dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1                    
                    If dtbl.Rows(0).Item("mod_status") = 1 Then
                        count = count + 1
                    End If
                Next
            End If
            If count > 0 Then
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Cannot delete because model is active.');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
            Else
                Try
                    If dtbl.Rows.Count > 0 Then
                        For j As Integer = 0 To dtbl.Rows.Count - 1
                            Dim stat As String
                            If dtbl.Rows(j).Item("mod_status") = 1 Then
                                stat = "Active"
                            Else
                                stat = "Inactive"
                            End If
                            oAsset.deleteModelAsset(dtbl.Rows(j).Item("mod_id"), usrLogin)
                            Dim logModel As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model Deleted - MODELNAME=" & dtbl.Rows(j).Item("mod_name") & ";STATUS=" & stat)
                            objLog.insertLog("asset_model", logModel)
                        Next
                    End If
                    oAsset.deleteAsset(astid, usrLogin)
                    Dim logSubCat As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Category Deleted - ID=" & astid & ";CATEGORY=" & dat.Rows(0).Item("cat_name"))
                    objLog.insertLog("asset", logSubCat)
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Asset Category has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
                Catch ex As Exception
                    Dim err As String = ex.Message.Replace("'", "\'")
                    cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('" & err & "');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
                End Try
            End If
        End If



    End Sub

    Sub deleteModel(ByVal astid, ByVal moid)
        Dim ds As DataSet
        Dim dt As DataTable
        ds = oAsset.getDetailOfModel(moid)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("mod_status") = 1 Then
                'tidak bisa delete
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Cannot delete.');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
            Else
                'delete
                Dim stat As String
                If dt.Rows(0).Item("mod_status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Inactive"
                End If
                oAsset.deleteModelAsset(moid, usrLogin)
                Dim logModel As String = objLog.getLogString(Request, Session("UserLoginID"), "Asset Model Deleted - MODELNAME=" & dt.Rows(0).Item("mod_name") & ";STATUS=" & stat)
                objLog.insertLog("asset_model", logModel)
                cs.RegisterStartupScript(Page.[GetType](), "alert", "alert('Asset Model has been successfully deleted.');window.location.href='" & urlBase & "_Parameter/asset.aspx';", True)
            End If
        End If
    End Sub
End Class