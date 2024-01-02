Imports System.DirectoryServices
Imports System.Configuration
Imports System.IO
Imports System.Security.Cryptography
Imports System.Data.SqlClient



Public Class Login1
    Inherits System.Web.UI.Page
    Dim cs As ClientScriptManager = Page.ClientScript
    Dim lFun As New lFunction
    Dim lSys As New lSystem
    Dim dbconn As New dbConn
    Dim lUsr As New lUsers
    Dim lGen As New lGeneral
    Dim llog As New lLog
    Dim oLic As New lLicense

#Region " Global Variable "
    Dim vForm = "document.Form1."
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        liBodyLoadAction.Text = ""
        'Try
        '    dbconn.testConnection(txtUserID.Text)

        '    If lSys.getStatMsg = 1 Then
        '        statmsg.Visible = True
        '    Else
        '        statmsg.Visible = False
        '    End If

        '    readCookie()
        'Catch ex As Exception
        '    liBodyLoadAction.Text = "swal('Failed', 'Database Connection Failed!', 'error');"
        'End Try
    End Sub

    Sub readCookie()
        If Request.Cookies("crmcookie") IsNot Nothing Then
            Dim userlogin As String
            If (Request.Cookies("crmcookie")("user_login") IsNot Nothing) Then
                userlogin = Request.Cookies("crmcookie")("user_login")
                Dim objLog As New lLog
                Dim sesParam As Integer = Session.Timeout()


                lUsr.insertLoginLog(userlogin, "Logout")
                'user track
                With New lLog
                    Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
                    .log_user_tracking("ua", userlogin, Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "User session time out", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
                End With

                Dim myCookie As HttpCookie
                myCookie = New HttpCookie("crmcookie")
                myCookie.Expires = DateTime.Now.AddDays(-1D)
                Response.Cookies.Add(myCookie)
            End If
        End If
    End Sub

    'Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
    '    Dim lMsg As String = ""


    '    If liBodyLoadAction.Text = "" Then

    '        Dim dsUser As DataSet
    '        Dim dtUser As DataRow

    '        dsUser = lUsr.getUser(txtUserID.Text)
    '        If dsUser.Tables(0).Rows.Count > 0 Then

    '            If dsUser.Tables(0).Rows(0).Item("usr_is_login") = 1 Then
    '                lMsg = "User ID is login."
    '            Else
    '                If dsUser.Tables(0).Rows(0).Item("usr_status") = 0 Then
    '                    lMsg = "User ID is inactive or disabled."

    '                    lUsr.insertLoginLog(txtUserID.Text, "Login")

    '                    With New lLog
    '                        Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
    '                        .log_user_tracking("ua", txtUserID.Text, Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Login Failure - User ID is inactive or disabled", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
    '                    End With

    '                Else
    '                    Dim x As Boolean = True
    '                    Dim SkyworxLicence As String = System.Configuration.ConfigurationManager.AppSettings("Skyworxlicense")

    '                    If Not SkyworxLicence = "IDXVERSION" Then
    '                        Dim domainName As String = System.Configuration.ConfigurationManager.AppSettings("DomainName")
    '                        x = localDirectoryIsAuthenticated(domainName, txtUserID.Text.Trim, txtPassword.Text.Trim)
    '                    End If

    '                    If SkyworxLicence = "IDXC-PRMB-1302-0001" Or SkyworxLicence = "IDXVERSION" Then
    '                        If x = True Then
    '                            dtUser = dsUser.Tables(0).Rows(0)
    '                            Session("UserLoginID") = dtUser.Item("usr_userid")
    '                            Session("UserLoginName") = dtUser.Item("usr_name")
    '                            Session("user_role") = dtUser.Item("usr_lvl_name")
    '                            Dim dt As DataTable = lUsr.getUserLevelDetail(dtUser.Item("usr_userid"))
    '                            Session("dtUser") = dt

    '                            lUsr.insertLoginLog(Session("UserLoginID"), "Login")

    '                            With New lLog
    '                                Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
    '                                .log_user_tracking("ua", Session("UserLoginID"), Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), "Login Success", Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
    '                            End With

    '                            lUsr.userIsLogin(Session("UserLoginID"), llog.getIPAddress())


    '                            Dim objMenu As New lUsers
    '                            Dim mn As DataTable = lUsr.getMenuAll(Session("UserLoginID"))
    '                            Session("mn") = mn
    '                            Session("baseUrl") = VirtualPathUtility.ToAbsolute("~/")

    '                            FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)
    '                        Else
    '                            lMsg = "User ID or Password is incorrect.\n"
    '                            Dim fl As String = ""
    '                            If Not txtUserID.Text = "" Then
    '                                lUsr.userFailedLogin(txtUserID.Text)
    '                                fl = lUsr.getFailedLogin(txtUserID.Text)
    '                            End If

    '                            Dim logs As String = ""

    '                            If lUsr.checkLock(txtUserID.Text) = 0 Then
    '                                logs = "Login Failure - Wrong password "
    '                            Else
    '                                logs = "Login Failure - User ID is inactive or disabled"
    '                            End If


    '                            lUsr.insertLoginLog(txtUserID.Text, "LoginF")

    '                            'user track
    '                            With New lLog
    '                                Dim objBrwInfo As HttpBrowserCapabilities = Request.Browser
    '                                .log_user_tracking("ua", txtUserID.Text, Date.Now, Request.ServerVariables("HTTPS"), Request.ServerVariables("SERVER_NAME"), logs, Request.ServerVariables("SCRIPT_NAME"), Request.ServerVariables("QUERY_STRING"), objBrwInfo.Browser & " " & objBrwInfo.Version, objBrwInfo.Platform)
    '                            End With
    '                        End If
    '                    Else
    '                        lMsg = "Application license Failed."
    '                    End If
    '                End If
    '            End If
    '        Else
    '            lMsg = "User ID or Password is incorrect.\n"
    '            If Not txtUserID.Text = "" Then
    '                Dim objUsr As New lUsers
    '                objUsr.userFailedLogin(txtUserID.Text)
    '            End If
    '        End If


    '        If lMsg <> "" Then
    '            liBodyLoadAction.Text = "swal('Failed','" & lMsg & "','error');"
    '        End If
    '    End If
    'End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Dim dataUser(9) As String
        Dim lMsg As String = ""
        Dim lAction As String = ""
        Dim objLog As New lLog
        CekInput()
        'Dim myLog As New lLog
        'CekInput()
        If liBodyLoadAction.Text = "" Then

            With New lUsers
                Dim dsUser As DataSet
                Dim dtUser As DataRow
                dsUser = .getUser(txtUserID.Text)
                Dim dtUserId As DataTable
                dtUserId = .userCheckUserId(txtUserID.Text)
                If dtUserId.Rows.Count = 0 Then
                    lMsg = "User ID is not available"
                ElseIf dsUser.Tables(0).Rows.Count > 0 Then
                    'If dsUser.Tables(0).Rows(0).Item("usr_is_login") = 1 And Not dsUser.Tables(0).Rows(0).Item("usr_ip_address") = objLog.getIPAddress() Then
                    If dsUser.Tables(0).Rows(0).Item("usr_is_login") = True Then
                        lMsg = "User ID is login."
                    Else
                        If dsUser.Tables(0).Rows(0).Item("usr_status") = False Then
                            lMsg = "User ID is inactive or disabled."

                            Dim Logs_disabled As String = objLog.getLogString(Request, txtUserID.Text, "Login Failure - User ID is inactive or disabled")
                            objLog.insertLoginLog(txtUserID.Text, "Login")
                            objLog.insertLog("ua", Logs_disabled)
                        Else
                            Dim x As Boolean = True
                            Dim SkyworxLicence As String = System.Configuration.ConfigurationManager.AppSettings("Skyworxlicense")
                            Dim dsLic As DataSet
                            Dim dtLic As DataTable
                            dsLic = oLic.checkLicense(SkyworxLicence)
                            dtLic = dsLic.Tables(0)
                            If dtLic.Rows.Count = 1 Then
                                If lGen.CheckNullString(dtLic.Rows(0).Item("z_start")) = "" Then
                                    oLic.activeLicense(SkyworxLicence)
                                End If
                                Dim dsLicPeriod As DataSet
                                Dim dtLicPeriod As DataTable
                                dsLicPeriod = oLic.checkLicensePeriod(SkyworxLicence)
                                dtLicPeriod = dsLicPeriod.Tables(0)
                                If dtLicPeriod.Rows.Count = 1 Then
                                    If dtLicPeriod.Rows(0).Item("lic_status") = "active" Then
                                        If dtLicPeriod.Rows(0).Item("lic_avblty_period") <= dtLic.Rows(0).Item("z_notif_period") Then
                                            lMsg &= "Your license will be expired in " & dtLicPeriod.Rows(0).Item("lic_avblty_period") & "days. \n"
                                        End If
                                        If dtLic.Rows(0).Item("z_use_ldap") = True Then
                                            Dim domainName As String = System.Configuration.ConfigurationManager.AppSettings("DomainName")
                                            x = localDirectoryIsAuthenticated(domainName, txtUserID.Text.Trim, txtPassword.Text.Trim)
                                        End If
                                        If x = True Then
                                            dtUser = dsUser.Tables(0).Rows(0)
                                            Session("UserLoginID") = dtUser.Item("usr_usersid")
                                            Session("UserLoginName") = dtUser.Item("usr_name")
                                            'If Not String.IsNullOrEmpty(dtUser.Item("user_type").ToString) Then
                                            '    Session("UserType") = dtUser.Item("user_type")
                                            'Else
                                            '    Session("UserType") = "-"
                                            'End If
                                            Dim dt As DataTable = .getUserLevelDetail(dtUser.Item("usr_usersid"))
                                            Session("dtUser") = dt
                                            'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "User " & Session("UserLoginID") & " Login Success")
                                            Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Login Success")
                                            objLog.insertLoginLog(Session("UserLoginID"), "Login")
                                            objLog.insertLog("ua", logs)
                                            .userIsLogin(Session("UserLoginID"), objLog.getIPAddress())
                                            'WriteCookie(Session("UserLoginID"))

                                            'redirectPage(dt)
                                            Dim objMenu As New lUsers
                                            Dim mn As DataTable = objMenu.getMenuAll(Session("UserLoginID"))
                                            Session("mn") = mn
                                            Session("baseUrl") = VirtualPathUtility.ToAbsolute("~/")

                                            If lMsg <> "" Then
                                                cs.RegisterStartupScript(Page.[GetType](), "redirect", "alert('" & lMsg & "'); window.location='" + Request.ApplicationPath + "/default.aspx';", True)
                                            Else
                                                FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)
                                            End If

                                        Else
                                            lMsg = "User ID or Password is incorrect.\n"
                                            Dim fl As String
                                            If Not txtUserID.Text = "" Then
                                                Dim objUsr As New lUsers
                                                objUsr.userFailedLogin(txtUserID.Text)
                                                fl = objUsr.getFailedLogin(txtUserID.Text)
                                            End If
                                            'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "User " & Session("UserLoginID") & " Login Failure")
                                            Dim logs As String = ""
                                            'Response.Write(txtUserID.Text)
                                            With New lUsers
                                                If .chkLock(txtUserID.Text) = 0 Then
                                                    logs = objLog.getLogString(Request, txtUserID.Text, "Login Failure - Wrong password " + fl + " times")
                                                Else
                                                    logs = objLog.getLogString(Request, txtUserID.Text, "Login Failure - User ID is inactive or disabled")
                                                End If
                                            End With
                                            'Response.Write(logs)
                                            objLog.insertLoginLog(txtUserID.Text, "LoginF")
                                            objLog.insertLog("ua", logs)
                                        End If
                                    Else
                                        'license key expired
                                        lMsg &= "Application license key has been expired. Please renew your license key.\n"
                                        If dtLic.Rows(0).Item("z_use_ldap") = True Then
                                            Dim domainName As String = System.Configuration.ConfigurationManager.AppSettings("DomainName")
                                            x = localDirectoryIsAuthenticated(domainName, txtUserID.Text.Trim, txtPassword.Text.Trim)
                                        End If
                                        If x = True Then
                                            dtUser = dsUser.Tables(0).Rows(0)
                                            Session("UserLoginID") = dtUser.Item("usr_usersid")
                                            Session("UserLoginName") = dtUser.Item("usr_name")
                                            'If Not String.IsNullOrEmpty(dtUser.Item("user_type").ToString) Then
                                            '    Session("UserType") = dtUser.Item("user_type")
                                            'Else
                                            '    Session("UserType") = "-"
                                            'End If
                                            Dim dt As DataTable = .getUserLevelDetail(dtUser.Item("usr_usersid"))
                                            Session("dtUser") = dt
                                            'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "User " & Session("UserLoginID") & " Login Success")
                                            Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Login Success")
                                            objLog.insertLoginLog(Session("UserLoginID"), "Login")
                                            objLog.insertLog("ua", logs)
                                            .userIsLogin(Session("UserLoginID"), objLog.getIPAddress())
                                            'WriteCookie(Session("UserLoginID"))

                                            'redirectPage(dt)
                                            Dim objMenu As New lUsers
                                            Dim mn As DataTable = objMenu.getMenuAll(Session("UserLoginID"))
                                            Session("mn") = mn
                                            Session("baseUrl") = VirtualPathUtility.ToAbsolute("~/")

                                            If lMsg <> "" Then
                                                cs.RegisterStartupScript(Page.[GetType](), "redirect", "alert('" & lMsg & "'); window.location='" + Request.ApplicationPath + "/default.aspx';", True)
                                            Else
                                                FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)
                                            End If


                                        Else
                                            lMsg = "User ID or Password is incorrect.\n"
                                            Dim fl As String
                                            If Not txtUserID.Text = "" Then
                                                Dim objUsr As New lUsers
                                                objUsr.userFailedLogin(txtUserID.Text)
                                                fl = objUsr.getFailedLogin(txtUserID.Text)
                                            End If
                                            'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "User " & Session("UserLoginID") & " Login Failure")
                                            Dim logs As String = ""
                                            'Response.Write(txtUserID.Text)
                                            With New lUsers
                                                If .chkLock(txtUserID.Text) = 0 Then
                                                    logs = objLog.getLogString(Request, txtUserID.Text, "Login Failure - Wrong password " + fl + " times")
                                                Else
                                                    logs = objLog.getLogString(Request, txtUserID.Text, "Login Failure - User ID is inactive or disabled")
                                                End If
                                            End With
                                            'Response.Write(logs)
                                            objLog.insertLoginLog(txtUserID.Text, "LoginF")
                                            objLog.insertLog("ua", logs)
                                        End If
                                    End If
                                Else
                                    'license not register
                                    lMsg &= "Application license key does not registered to our product."
                                End If

                            Else
                                'license key not registered
                                lMsg &= "Application license Failed."
                            End If

                            'If Not SkyworxLicence = "vyxellversion" Then
                            '    Dim domainName As String = System.Configuration.ConfigurationManager.AppSettings("DomainName")
                            '    x = localDirectoryIsAuthenticated(domainName, txtUserID.Text.Trim, txtPwd.Text.Trim)
                            'End If
                            'If SkyworxLicence = "IDXC-PRMB-1302-0001" Or SkyworxLicence = "vyxellversion" Then
                            '    If x = True Then
                            '        dtUser = dsUser.Tables(0).Rows(0)
                            '        Session("UserLoginID") = dtUser.Item("usr_usersid")
                            '        Session("UserLoginName") = dtUser.Item("usr_name")
                            '        'If Not String.IsNullOrEmpty(dtUser.Item("user_type").ToString) Then
                            '        '    Session("UserType") = dtUser.Item("user_type")
                            '        'Else
                            '        '    Session("UserType") = "-"
                            '        'End If
                            '        Dim dt As DataTable = .getUserLevelDetail(dtUser.Item("usr_usersid"))
                            '        Session("dtUser") = dt
                            '        'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "User " & Session("UserLoginID") & " Login Success")
                            '        Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "Login Success")
                            '        objLog.insertLoginLog(Session("UserLoginID"), "Login")
                            '        objLog.insertLog("ua", logs)
                            '        .userIsLogin(Session("UserLoginID"), objLog.getIPAddress())
                            '        'WriteCookie(Session("UserLoginID"))

                            '        'redirectPage(dt)
                            '        Dim objMenu As New lUsers
                            '        Dim mn As DataTable = objMenu.getMenuAll(Session("UserLoginID"))
                            '        Session("mn") = mn
                            '        Session("baseUrl") = VirtualPathUtility.ToAbsolute("~/")

                            '        FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)

                            '    Else
                            '        lMsg = "User ID or Password is incorrect.\n"
                            '        Dim fl As String
                            '        If Not txtUserID.Text = "" Then
                            '            Dim objUsr As New lUsers
                            '            objUsr.userFailedLogin(txtUserID.Text)
                            '            fl = objUsr.getFailedLogin(txtUserID.Text)
                            '        End If
                            '        'Dim logs As String = objLog.getLogString(Request, Session("UserLoginID"), "User " & Session("UserLoginID") & " Login Failure")
                            '        Dim logs As String = ""
                            '        'Response.Write(txtUserID.Text)
                            '        With New lUsers
                            '            If .chkLock(txtUserID.Text) = 0 Then
                            '                logs = objLog.getLogString(Request, txtUserID.Text, "Login Failure - Wrong password " + fl + " times")
                            '            Else
                            '                logs = objLog.getLogString(Request, txtUserID.Text, "Login Failure - User ID is inactive or disabled")
                            '            End If
                            '        End With
                            '        'Response.Write(logs)
                            '        objLog.insertLoginLog(txtUserID.Text, "LoginF")
                            '        objLog.insertLog("ua", logs)
                            '    End If
                            'Else
                            '    lMsg = "Application license Failed."
                            'End If
                        End If
                    End If
                Else
                    lMsg = "User ID or Password is incorrect.\n"

                    'Try
                    '    Dim dtal As DataTable
                    '    With New lGeneral
                    '        dtal = .alertMessage("usr_no_reg")
                    '        lMsg = dtal.Rows(0).Item("message").ToString
                    '    End With
                    'Catch ex As Exception
                    '    lMsg = "Table Alert Not Found."
                    'End Try
                    'lMsg = "User anda belum terdaftar, mohon registrasi terlebih dahulu melalui admin anda.  \n 1. SME – Adiati Winarsih \n 2. Mortgage – Christian Hartanto \n 3. Network – Nurlaela Syabani \n 4. General – Oki Bakti Imansyah"
                    'lAction = vForm + txtUserID.ID + ".focus()"
                    If Not txtUserID.Text = "" Then
                        Dim objUsr As New lUsers
                        objUsr.userFailedLogin(txtUserID.Text)
                    End If
                End If
                'FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)
            End With
            If lMsg <> "" Then
                liBodyLoadAction.Text = "alert('" + lMsg + "');" + lAction
            End If
        End If
    End Sub

    Sub CekInput()
        Dim lMsg As String = ""
        Dim lAction As String = ""
        If txtUserID.Text = "" Then
            lMsg = "User ID field is required. Please input user ID!\n"
            lAction = vForm + txtUserID.ID + ".focus()"
        ElseIf txtPassword.Text = "" Then
            lMsg = "Password field is required. Please input password!\n"
            lAction = vForm + txtPassword.ID + ".focus()"
        End If
        If lMsg <> "" Then
            liBodyLoadAction.Text = "alert('" + lMsg + "');" + lAction
        End If
    End Sub



    Public Function localDirectoryIsAuthenticated(ByVal domain As String, ByVal username As String, ByVal pwd As String) As Boolean
        Dim _path As String
        Dim _filterAttribute As String
        Dim servername As String = ConfigurationSettings.AppSettings("serverpath").ToString
        Dim domainAndUsername As String = domain + "\" + username
        Dim entry As New DirectoryEntry("LDAP://" + servername, domainAndUsername, pwd)
        Try
            Dim obj As Object = entry.NativeObject
            Dim search As New DirectorySearcher(entry)
            search.Filter = "(SAMAccountName=" + username + ")"
            search.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = search.FindOne
            If result Is Nothing Then
                Return False
            End If
            _path = result.Path
            _filterAttribute = CType(result.Properties("cn")(0), String)

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


End Class