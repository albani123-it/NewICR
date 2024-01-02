Imports System.Web.Configuration

Public Class Site
    Inherits System.Web.UI.MasterPage
    Public dtParent As DataTable
    Public dtChild As DataTable
    Public dtChildSub As DataTable
    Dim oMen As New lnav
    Public dtUserMaster As DataTable
    Public url As String = VirtualPathUtility.ToAbsolute("~/")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'UpdateSetting("ConString", "(local)|Skyworx_DEMO|sa|316B1A8832907F27B28AE9D1DEE38FC1|1000|30")

        dtUserMaster = Session("dtUser")
        dtParent = oMen.getParent()


        'UpdateConnectionString("b", "12345")
    End Sub

    Sub getChild(ByVal parentID)
        dtChild = oMen.getChild(parentID)
    End Sub

    Sub getChildSub(ByVal childID)
        dtChildSub = oMen.getChildSub(childID)
    End Sub

    Public Sub UpdateSetting(key As String, value As String)
        Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~")
        If config.AppSettings.Settings(key) Is Nothing Then
            config.AppSettings.Settings.Add(key, value)
        Else
            config.AppSettings.Settings(key).Value = value
        End If
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub

    'Public Sub UpdateConnectionString(key As String, value As String)
    '    Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~")
    '    If config.ConnectionStrings.ConnectionStrings(key) Is Nothing Then
    '        config.ConnectionStrings.ConnectionStrings.Add(New ConnectionStringSettings(key, value))
    '    Else
    '        config.ConnectionStrings.ConnectionStrings(key).ConnectionString = value
    '    End If
    '    config.Save()
    '    ConfigurationManager.RefreshSection("appSettings")
    'End Sub


End Class