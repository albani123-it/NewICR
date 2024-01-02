' NOTE: You can use the "Rename" command on the context menu to change the class name "getPPC" in code, svc and config file together.
Public Class getPPC
    Implements IgetPPC
    Dim out As New dbConn


#Region "lgetPPC Members"
    Public Function searchInquiry(ByVal CSV As String, ByVal ORDER As String, ByVal FIELD As String, ByVal VALUE As String, ByVal SORT As String) As String Implements IgetPPC.getPPCData
        Dim msg As String = ""

        Try
            With New lGlobal
                Dim dt As New DataTable
                dt = .getPPCView(CSV, ORDER, FIELD, VALUE, SORT)

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
            msg = "tidak ada result"
        End Try



        Return msg
    End Function

#End Region
End Class
