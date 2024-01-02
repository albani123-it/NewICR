' NOTE: You can use the "Rename" command on the context menu to change the class name "customerDetail" in code, svc and config file together.
Public Class customerDetail
    Implements IcustomerDetail
    Dim pk As New lGlobal
    Dim out As New dbConn

#Region "IcustomerDetail Members"
    Public Function searchInquiry(ByVal passKey As String, ByVal CSV As String) As String Implements IcustomerDetail.customerDataView
        Dim dtpasskey As New DataTable
        Dim lmsg As String = ""
        Dim ot As String = ""
        Try
            dtpasskey = pk.checkPassKey(passKey)


            If dtpasskey.Rows(0).Item("rowZ") > 0 Then
                Dim dto As New DataTable
                ot = out.getTimeOut()

                dto = pk.timeOut(passKey)

                If dto.Rows(0).Item("OutTime") >= ot Then
                    lmsg = "session Time out"
                Else
                    pk.passingPassKey(passKey)


                    With New lGlobal
                        Dim dt As New DataTable
                        dt = .getCustomerDetail(CSV)

                        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                        Dim list = New List(Of Dictionary(Of String, Object))()

                        For Each row As DataRow In dt.Rows
                            Dim dict = New Dictionary(Of String, Object)()

                            For Each col As DataColumn In dt.Columns
                                dict(col.ColumnName) = row(col)
                            Next
                            list.Add(dict)
                        Next
                        lmsg = serializer.Serialize(list)
                    End With

                End If
            End If

        Catch ex As Exception
            lmsg = "Passkey Tidak ditemukan"
        End Try

        Return lmsg
    End Function

#End Region
End Class
