' NOTE: You can use the "Rename" command on the context menu to change the class name "system_alert" in code, svc and config file together.
Public Class system_alert
    Implements Isystem_alert
    Dim pk As New lGlobal
    Dim out As New dbConn

#Region "ljson_line Members"
    Public Function systemAlert(ByVal passkey As String) As String Implements Isystem_alert.getSystemAlertData
        Dim msg As String = ""
        Dim dtpasskey As New DataTable
        Dim uid As String = ""
        Dim ot As String = ""

        Try
            dtpasskey = pk.checkPassKey(passkey)

            If dtpasskey.Rows(0).Item("rowZ") > 0 Then
                Dim dto As New DataTable
                ot = out.getTimeOut()

                dto = pk.timeOut(passkey)

                If dto.Rows(0).Item("OutTime") >= ot Then
                    msg = "session Time out"
                Else
                    pk.passingPassKey(passkey)
                    uid = dtpasskey.Rows(0).Item("userid")


                    With New lGlobal
                        Dim dt As New DataTable
                        dt = .getAlertView(passkey)

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
                End If
            End If
        Catch ex As Exception
            msg = "tidak ada Result"
        End Try

        Return msg
    End Function
#End Region




End Class
