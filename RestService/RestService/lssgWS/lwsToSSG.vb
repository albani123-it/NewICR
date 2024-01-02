Imports System.Data.SqlClient
Imports System
Imports System.Data
Imports System.IO


Public Class lwsToSSG
    Function getIpLocal()
        Dim strHostName As String
        Dim strIPAddress As String

        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(1).ToString()
        Return strIPAddress
    End Function

    Public Function getTasklist(ByVal uniqCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_getTasklist", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@uniqCode", SqlDbType.VarChar, 50).Value = uniqCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getUser(ByVal auth)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("ws_getUserAuth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@auth", SqlDbType.VarChar, 50).Value = auth
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function insLogString(ByVal usr, ByVal message, Optional ByVal i = 0)
        Dim time As DateTime

        time = DateTime.Now

        Dim format As String = "MM/dd/yyyy HH:mm:ss"
        Dim timeFormat As String = time.ToString(format)
        Dim applicationName As String = "iDecisionCRMPermata"

        Dim hostName As String
        Dim IPAddress As String = getIpLocal()
        hostName = System.Net.Dns.GetHostName()

        'hostName = "localhost" 'host.HostName


        Dim clientLoginName As String = IIf(IsDBNull(usr), "IUser", usr)
        Return timeFormat + "|" + applicationName + "|" + IPAddress + "|" + clientLoginName + "|" + message
    End Function

    Public Function insertLog(ByVal code, ByVal strlog, ByVal wsuserid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ws_insertLog", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        cmdA.Parameters.Add("@strlog", SqlDbType.VarChar).Value = strlog
        cmdA.Parameters.Add("@wsuserid", SqlDbType.VarChar).Value = wsuserid
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    'Public Function getIPAddress()
    '    Dim IPAddress As String
    '    Dim currentRequest As HttpRequest = HttpContext.Current.Request
    '    IPAddress = currentRequest.ServerVariables("REMOTE_ADDR")
    '    Return IPAddress
    'End Function
End Class
