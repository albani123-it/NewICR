' NOTE: You can use the "Rename" command on the context menu to change the class name "json_trb" in code, svc and config file together.
Public Class json_line
    Implements Ijson_line

#Region "ljson_line Members"
    Public Function searchInquiry(ByVal CSV As String) As String Implements Ijson_line.getTRBLineData
        Dim msg As String = ""

        Try
            With New lGlobal
                Dim dt As New DataTable
                dt = .getTRBLineView(CSV)

                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim list = New List(Of Dictionary(Of String, Object))()

                For Each row As DataRow In dt.Rows
                    Dim dict = New Dictionary(Of String, Object)()

                    For Each col As DataColumn In dt.Columns
                        dict(col.ColumnName) = FixJsonDateFields(row(col))
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
