Imports System.IO
Imports System.Web.Script.Serialization

' NOTE: You can use the "Rename" command on the context menu to change the class name "ws_tasklist" in code, svc and config file together.
Public Class ws_tasklist
    Implements Iws_tasklist
    Dim lmsg As String

    Public Function getTasklist(ByVal auth As String, ByVal uniqCode As String) As String Implements Iws_tasklist.getTaskListData

        With New lwsToSSG
            Dim dtu As DataTable
            Try
                dtu = .getUser(auth)
                Dim userid As String = dtu.Rows(0).Item("uws_userid")
                Dim user As String = dtu.Rows(0).Item("uws_user")
                If dtu.Rows.Count > 0 Then
                    If dtu.Rows(0).Item("lws_userlevel") = "Tasklist" Then
                        Dim dt As New DataTable

                        Try
                            dt = .getTasklist(uniqCode)

                            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                            Dim list = New List(Of Dictionary(Of String, Object))()

                            For Each row As DataRow In dt.Rows
                                Dim dict = New Dictionary(Of String, Object)()

                                For Each col As DataColumn In dt.Columns
                                    dict(col.ColumnName) = row(col)
                                Next
                                list.Add(dict)
                            Next
                            Dim logs As String = .insLogString(auth, " Get Ws Tasklist - UniqCode = " + uniqCode + " Authentication = " + auth + " UserID = " + user)
                            .insertLog("WSusers", logs, userid)
                            lmsg = serializer.Serialize(list)
                        Catch ex As Exception
                            lmsg = "Tasklist Not Found"
                        End Try
                    Else
                        lmsg = "Permission Denid Access Level."
                    End If
                Else
                    lmsg = "Permission Denid..!!"
                End If
            Catch ex As Exception
                lmsg = "User Authentication Not Found...!!"
            End Try



        End With

        Return lmsg
    End Function

End Class
