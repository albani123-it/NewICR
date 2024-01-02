' NOTE: You can use the "Rename" command on the context menu to change the class name "json_trb" in code, svc and config file together.
Public Class json_trb
    Implements Ijson_trb

#Region "ljson_trb Members"
    Public Function searchInquiry(ByVal CSV As String) As String Implements Ijson_trb.getTRBData
        Dim msg As String = ""

        Try
            With New lGlobal
                Dim dt As New DataTable
                dt = .getTRBView(CSV)

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
