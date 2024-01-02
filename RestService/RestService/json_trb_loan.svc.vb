' NOTE: You can use the "Rename" command on the context menu to change the class name "json_trb_loan" in code, svc and config file together.
Public Class json_trb_loan
    Implements Ijson_trb_loan

#Region "ljson_trb_loan Members"
    Public Function searchInquiry(ByVal CSV As String, ByVal STATUS As String) As String Implements Ijson_trb_loan.getTRBLoanData
        Dim msg As String = ""

        Try
            With New lGlobal
                Dim dt As New DataTable
                dt = .getTRBLoanView(CSV, STATUS)

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
