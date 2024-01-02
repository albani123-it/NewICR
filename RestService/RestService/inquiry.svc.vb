Imports System.Data.SqlClient

' NOTE: You can use the "Rename" command on the context menu to change the class name "inquiry" in code, svc and config file together.
Public Class inquiry
    Implements Iinquiry

    Dim oGen As New lGeneral
    Dim oFun As New lFunction
    Dim oInq As New lGlobal
    Dim out As New dbConn

#Region "Iinquiry Members"

    Public Function searchInquiry(ByVal passKey As String, ByVal code As String, ByVal val As String, ByVal tgl As String) As String Implements Iinquiry.inquiryData
        Dim dtpasskey As New DataTable
        Dim lmsg As String = ""
        Dim uid As String = ""
        Dim ot As String = ""
        Try
            dtpasskey = oInq.checkPassKey(passKey)


            If dtpasskey.Rows(0).Item("rowZ") > 0 Then
                Dim dto As New DataTable
                ot = out.getTimeOut()

                dto = oInq.timeOut(passKey)

                If dto.Rows(0).Item("OutTime") >= ot Then
                    lmsg = "session Time out"
                Else
                    oInq.passingPassKey(passKey)
                    uid = dtpasskey.Rows(0).Item("userid")



                    Dim whr As String = ""

                    If code = "acctnum" Then
                        whr &= " acctnum = '" + val + "' "

                    End If

                    If code = "cardnum" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " cust_cardnum = '" + oGen.crypPwd(val) + "' "
                    End If

                    If code = "nmatgl" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " cust_name LIKE '%" + val + "%' "

                    End If
                    If code = "nmatgl" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " cust_dob = '" + Mid(tgl, 5, 4) + "-" & Mid(tgl, 3, 2) + "-" & Mid(tgl, 1, 2) + "' "

                    End If

                    If code = "cnum" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " cnum = '" + val + "' "

                    End If


                    If code = "FSV" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " fsv = '" + val + "' "

                    End If


                    If code = "gcn" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " csv = '" + val + "' "

                    End If

                    If code = "csv" Then
                        If Not whr = "" Then
                            whr &= " AND "
                        End If
                        whr &= " csv = '" + val + "' "

                    End If

                    If whr = "" Then
                        whr &= " 1 = 0 "
                    End If


                    Dim dt As New DataTable
                    dt = oInq.getCustInquiry(whr)
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

                End If
            End If

        Catch ex As Exception
            lmsg = "Passkey Tidak ditemukan"
        End Try

        Return lmsg


       

    End Function


#End Region
End Class
