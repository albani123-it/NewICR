' NOTE: You can use the "Rename" command on the context menu to change the class name "customerProductDetail" in code, svc and config file together.
Public Class customerProductDetail
    Implements IcustomerProductDetail
    Dim pk As New lGlobal
    Dim out As New dbConn

    Public Function customerProductDetail(ByVal passkey As String, ByVal acctnum As String) As String Implements IcustomerProductDetail.customerProductDetailData
        Dim dtpasskey As New DataTable
        Dim lmsg As String = ""
        Dim ot As String = ""
        Try
            dtpasskey = pk.checkPassKey(passkey)


            If dtpasskey.Rows(0).Item("rowZ") > 0 Then
                Dim dto As New DataTable
                ot = out.getTimeOut()

                dto = pk.timeOut(passkey)

                If dto.Rows(0).Item("OutTime") >= ot Then
                    lmsg = "session Time out"
                Else
                    pk.passingPassKey(passkey)



                    With New lGlobal
                        Dim dt As New DataTable
                        dt = .getCustomerProductDetail(acctnum)

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

End Class
