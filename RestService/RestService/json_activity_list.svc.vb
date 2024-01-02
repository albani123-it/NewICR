' NOTE: You can use the "Rename" command on the context menu to change the class name "json__activity_list" in code, svc and config file together.
Public Class json_activity_list
    Implements Ijson_activity_list

#Region "ljson_line Members"
    Public Function searchInquiry() As String Implements Ijson_activity_list.getActivityListData
        Dim msg As String = ""

        Try
            With New lGlobal
                Dim dt As New DataTable
                dt = .getActivityListView()

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim list = New List(Of Dictionary(Of String, Object))()

                For Each row As DataRow In dt.Rows
                    Dim dict = New Dictionary(Of String, Object)()

                    For Each col As DataColumn In dt.Columns
                        dict(col.ColumnName) = row(col)
                    Next
                    list.Add(dict)
                Next
                msg = serializer.Serialize(list)
            End With

        Catch ex As Exception
            msg = "tidak ada query"
        End Try



        Return msg
    End Function
#End Region




End Class
