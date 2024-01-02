' NOTE: You can use the "Rename" command on the context menu to change the class name "tasklistDetail" in code, svc and config file together.
Public Class tasklistDetail
    Implements ItasklistDetail
    Dim pk As New lGlobal
    Dim out As New dbConn

#Region "ltasklistDetail Members"
    Public Function viewTasklistDetail(ByVal passkey As String, ByVal tskcode As String) As String Implements ItasklistDetail.taskListDetailData
        Dim dtpasskey As New DataTable
        Dim lmsg As String = ""
        Dim uid As String = ""
        Dim ot As String = ""

        Try
            'dtpasskey = pk.checkPassKey(passkey)


            'If dtpasskey.Rows(0).Item("rowZ") > 0 Then
            '    Dim dto As New DataTable
            '    ot = out.getTimeOut()

            '    dto = pk.timeOut(passkey)

            '    If dto.Rows(0).Item("OutTime") >= ot Then
            '        lmsg = "session Time out"
            '    Else
            '        pk.passingPassKey(passkey)
            '        uid = dtpasskey.Rows(0).Item("userid")

            With New lGlobal
                Dim dt As New DataTable
                'dt = .getTasklistDetailView(tskcode, uid)
                dt = .getTasklistDetailView(tskcode)

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

            '    End If
            'End If

        Catch ex As Exception
            lmsg = "Passkey Tidak ditemukan"
        End Try

        Return lmsg

    End Function

#End Region


End Class
