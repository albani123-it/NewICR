Imports System.Data.SqlClient
Imports System
Imports System.Data
Imports System.IO
Imports System.Web.Configuration

Public Class lLog
    Dim lUser As New lUsers

    Public Function getSessionStateTimeout()
        Dim sessionSection = DirectCast(WebConfigurationManager.GetSection("system.web/sessionState"), SessionStateSection)
        Dim timeout As String = sessionSection.Timeout.ToString
        Return timeout
    End Function


    'Public Function getLogString(ByVal req, ByVal usr, ByVal message, Optional ByVal i = 0)
    '    Dim time As DateTime
    '    If message = "User session time out" Then
    '        time = lUser.getUserTimeout(usr, i)
    '    Else
    '        time = DateTime.Now
    '    End If

    '    Dim format As String = "MM/dd/yyyy HH:mm:ss"
    '    Dim timeFormat As String = time.ToString(format)
    '    Dim applicationName As String = "Skyworx"

    '    Dim hostName As String
    '    Dim IPAddress As String = getIPAddress()
    '    hostName = System.Net.Dns.GetHostName()

    '    hostName = "localhost" 'host.HostName


    '    Dim clientLoginName As String = IIf(IsDBNull(usr), "IUser", usr)
    '    Return timeFormat + "|" + applicationName + "|" + IPAddress + "|" + hostName + "|" + clientLoginName + "|" + usr + "|" + message
    'End Function

    Public Function getIPAddress()
        Dim IPAddress As String
        Dim currentRequest As HttpRequest = HttpContext.Current.Request
        IPAddress = currentRequest.ServerVariables("REMOTE_ADDR")
        Return IPAddress
    End Function

    Public Function insertLog(ByVal code, ByVal strlog)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_insertLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@strlog", SqlDbType.VarChar, 1000).Value = strlog
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function log_user_tracking(ByVal log_code, ByVal log_userid, ByVal log_date, ByVal log_https, ByVal log_server_name, ByVal log_activity, ByVal log_page_url, ByVal log_query_string, ByVal log_browser, ByVal log_OS)
        'Dim myDb As New dbConn
        'Dim myConn As SqlConnection
        'myConn = myDb.getConStr
        'myConn.Open()

        'Dim IPAddress As String = getIPAddress()

        'Dim cmdA As New SqlCommand("log_getUserTrack", myConn)
        'cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.AddWithValue("@log_code", log_code)
        'cmdA.Parameters.AddWithValue("@log_userid", log_userid)
        'cmdA.Parameters.AddWithValue("@log_date", log_date)
        'cmdA.Parameters.AddWithValue("@log_https", log_https)
        'cmdA.Parameters.AddWithValue("@log_server_name", log_server_name)
        'cmdA.Parameters.AddWithValue("@log_activity", log_activity)
        'cmdA.Parameters.AddWithValue("@log_page_url", log_page_url)
        'cmdA.Parameters.AddWithValue("@log_query_string", log_query_string)
        'cmdA.Parameters.AddWithValue("@log_browser", log_browser)
        'cmdA.Parameters.AddWithValue("@log_OS", System.Environment.OSVersion.ToString())
        'cmdA.Parameters.AddWithValue("@log_client_ip", IPAddress)

        'Dim sda As New SqlDataAdapter(cmdA)
        'Dim ds As New DataSet
        'sda.Fill(ds)
        'myConn.Close()
        'Return ds


    End Function


    '----- Log Activity -----'
    Public Function getListLogActivity(ByVal startDate, ByVal EndDate)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_listActivityLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@startDate", startDate)
        cmdA.Parameters.AddWithValue("@endDate", EndDate)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getDetailActivityLog(ByVal userid, ByVal dateLog)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_detailActivityLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@userid", userid)
        cmdA.Parameters.AddWithValue("@date", dateLog)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getViewDetailActivityLog(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_viewDetailActivityLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function insertLoginLog(ByVal usr, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_insertLoginLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = typ
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function getLogString(ByVal req, ByVal usr, ByVal message, Optional ByVal i = 0)
        Dim time As DateTime
        If message = "User session time out" Then
            time = getUserTimeout(usr, i)
        Else
            time = DateTime.Now
        End If

        Dim format As String = "MM/dd/yyyy HH:mm:ss"
        Dim timeFormat As String = time.ToString(format)
        Dim applicationName As String = "Skyworx"

        Dim hostName As String
        Dim IPAddress As String = getIPAddress()
        hostName = System.Net.Dns.GetHostName()

        hostName = "localhost" 'host.HostName


        Dim clientLoginName As String = IIf(IsDBNull(usr), "IUser", usr)
        Return timeFormat + "|" + applicationName + "|" + IPAddress + "|" + hostName + "|" + clientLoginName + "|" + usr + "|" + message
    End Function

    Public Function getUserTimeout(ByVal usr, ByVal i)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("usr_getUserTimeOut", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@tout", SqlDbType.Int).Value = i
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item("timout")
    End Function
    Public Function getLogDetail(ByVal id, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_getLogDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getLogDetailLevel(ByVal ID, ByVal Typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("log_getLogDetailLevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = Typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

End Class
