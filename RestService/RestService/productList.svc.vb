' NOTE: You can use the "Rename" command on the context menu to change the class name "productList" in code, svc and config file together.
Public Class productList
    Implements IproductList
    Dim pk As New lGlobal
    Dim out As New dbConn

#Region "lproductList Members"
    Public Function getProduct(ByVal passKey As String, ByVal CSV As String, ByVal ORDER As String, ByVal FIELD As String, ByVal VALUE As String, ByVal SORT As String) As String Implements IproductList.productListData
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
                        dt = .getProductList(CSV, ORDER, FIELD, VALUE, SORT)

                        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                        Dim list = New List(Of Dictionary(Of String, Object))()

                        For Each row As DataRow In dt.Rows
                            Dim dict = New Dictionary(Of String, Object)()

                            For Each col As DataColumn In dt.Columns

                                'If Not IsDBNull(row(col)) Then
                                '    row(col) = row(col)
                                'Else
                                '    row(col) = ""
                                'End If
                                dict(col.ColumnName) = row(col)
                                'dict(col.ColumnName) = FixJsonDateFields(row(col))
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

    'Private Function FixJsonDateFields(ByVal jsonStr As String) As String

    '    Dim check As Boolean
    '    Dim tgl As String = ""

    '    check = IsDate(jsonStr)

    '    If check = True Then
    '        tgl = Replace(jsonStr, "/", "-")
    '    End If
    '    'Dim regEx As New Regex(":(""\\/)Date(.*?)(\\/"")", RegexOptions.Multiline)
    '    'If regEx.IsMatch(jsonStr) Then
    '    '    jsonStr = regEx.Replace(jsonStr, ":new Date$2")
    '    'End If

    '    Return tgl
    'End Function
#End Region
End Class
