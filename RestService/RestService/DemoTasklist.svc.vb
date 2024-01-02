Imports System.IO
Imports System.Web.Script.Serialization

' NOTE: You can use the "Rename" command on the context menu to change the class name "DemoTasklist" in code, svc and config file together.
Public Class DemoTasklist
    Implements IDemoTasklist
    Dim pk As New lGlobal
    Dim out As New dbConn

#Region "Itasklist Members"
    Public Function taskListData(ByVal uniqCode As String) As String Implements IDemoTasklist.taskListData


        Dim lmsg As String = ""

        Try

            With New lGlobal
                Dim dt As New DataTable
                dt = .getTasklistDemo(uniqCode)

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim list = New List(Of Dictionary(Of String, Object))()

                For Each row As DataRow In dt.Rows
                    Dim dict = New Dictionary(Of String, Object)()

                    For Each col As DataColumn In dt.Columns
                        dict(col.ColumnName) = FixJsonDateFields(row(col))
                    Next
                    list.Add(dict)
                Next
                lmsg = serializer.Serialize(list)
            End With

        Catch ex As Exception
            lmsg = "Not Result"
        End Try



        Return lmsg
    End Function

    Private Function FixJsonDateFields(ByVal jsonStr As String) As String

        Dim check As Boolean
        Dim tgl As String = ""

        check = IsDate(jsonStr)

        If check = True Then
            tgl = Replace(jsonStr, "/", "-")
        Else
            tgl = jsonStr
        End If
        'Dim regEx As New Regex(":(""\\/)Date(.*?)(\\/"")", RegexOptions.Multiline)
        'If regEx.IsMatch(jsonStr) Then
        '    jsonStr = regEx.Replace(jsonStr, ":new Date$2")
        'End If

        Return tgl
    End Function


#End Region

End Class
