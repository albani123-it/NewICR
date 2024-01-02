Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


' NOTE: You can use the "Rename" command on the context menu to change the class name "Workflow" in code, svc and config file together.
Public Class Workflow
    Implements IWorkflow
    Dim objWork As New lworkflow

#Region "Global Variable"
#End Region


    Function getProduct(ByVal ID As String) As String Implements IWorkflow.getProduct
        Dim data
        Dim JSONresult As String
        Try
            Dim dtAccess2 As DataTable = objWork.getProduct(ID.ToString)
            data = New With {
                Key .status = "success",
                Key .message = "success",
                Key .data = dtAccess2
            }
        Catch ex As Exception
            data = New With {
                Key .status = "error",
                Key .message = ex.Message
            }
        End Try
        JSONresult = JsonConvert.SerializeObject(data, Formatting.None)
        Return JSONresult

    End Function

    Function getFieldInfo(ByVal ID As String) As String Implements IWorkflow.getFieldInfo
        Dim data
        Dim data2
        Dim JSONresult As String
        Try
            Dim dtFeldInfo As DataTable = objWork.getFieldInfo(ID.ToString)
            Dim dtDesc As DataTable = objWork.getWPMInfo(ID.ToString)
            data2 = New With {
                Key .FieldInfo = dtFeldInfo,
                    Key .Desc = dtDesc}
            data = New With {
                Key .status = "success",
                Key .message = "success",
                Key .data = data2
            }
        Catch ex As Exception
            data = New With {
                Key .status = "error",
                Key .message = ex.Message
            }
        End Try
        JSONresult = JsonConvert.SerializeObject(data, Formatting.None)
        Return JSONresult
    End Function


    Function getWorkflow(ByVal wpmId As String, ByVal wpmCounter As String, ByVal wfId As String) As String Implements IWorkflow.getWorkflow

        Dim Data
        Try
            Dim dtWPM As DataTable = objWork.getWPMInfo(wpmId, wpmCounter)
            Dim wpm_id As Integer = dtWPM.Rows(0).Item("wpm_id")
            Dim wps_id As Integer = dtWPM.Rows(0).Item("wps_id")
            Dim wpm_counter As Integer = dtWPM.Rows(0).Item("wpm_counter")
            Dim dtFeldInfo As DataTable = objWork.getFieldInfo(wps_id)
            Dim dtReturn As DataTable
            Dim cek = objWork.getLogDet(wfId)
            If cek = 0 Then
                dtReturn = objWork.insertLog("-", wpm_id)
                If dtReturn.Rows.Count > 0 Then
                    wfId = dtReturn.Rows(0).Item("wf_id")
                End If
            Else
                dtReturn = objWork.insertLogDetail(conVal(wfId), wpm_id, wpm_counter, "", "")
                If dtReturn.Rows.Count > 0 Then
                    wfId = dtReturn.Rows(0).Item("wf_id")
                End If
            End If
            Data = New With
                        {
                           Key .status = "success",
                           Key .message = "success",
                           Key .data_wf = dtWPM,
                           Key .data_field = dtFeldInfo,
                           Key .wfId = wfId
                       }

        Catch ex As Exception
            Data = New With
                        {
                            Key .status = "error",
                            Key .message = ex.Message
                        }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function getNextWorflow(ByVal wpmId As String, ByVal wfId As String, Optional ByVal wpmCounter As String = "0") As String Implements IWorkflow.getNextWorflow


        Dim Data

        Try
            Dim dtWPM As DataTable = objWork.getWPMInfo(wpmId, wpmCounter)
            Dim wpm_id As Integer = dtWPM.Rows(0).Item("wpm_id")
            Dim wps_id As Integer = dtWPM.Rows(0).Item("wps_id")
            Dim wpm_counter As Integer = dtWPM.Rows(0).Item("wpm_counter")

            Dim dtWPM_next As DataTable = objWork.getWPMInfo(wpmId, wpm_counter + 1)
            Dim wps_id_next As Integer = dtWPM_next.Rows(0).Item("wps_id")
            Dim dtProcCond As DataTable = objWork.getProcCond(wps_id_next)
            Dim dtReturn As DataTable
            Dim cek = objWork.getLogDet(wfId)
            If cek = 0 Then
                dtReturn = objWork.insertLog("-", wpm_id)
                If dtReturn.Rows.Count > 0 Then
                    wfId = dtReturn.Rows(0).Item("wf_id")
                End If
            Else
                dtReturn = objWork.insertLogDetail(conVal(wfId), wpm_id, wpm_counter, "", "")
                If dtReturn.Rows.Count > 0 Then
                    wfId = dtReturn.Rows(0).Item("wf_id")
                End If
            End If

            Data = New With
                        {
                           Key .status = "success",
                           Key .message = "success",
                           Key .data_nextwf = dtWPM_next,
                           Key .data_param = dtProcCond,
                           Key .wfId = wfId
                       }
        Catch ex As Exception
            Data = New With
                        {
                            Key .status = "error",
                            Key .message = ex.Message
                        }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function submitDataWf(ByVal wcr_id As String(), ByVal dataValue As String, ByVal wfId As String, ByVal wpmId As String, ByVal counter As String) As String Implements IWorkflow.submitDataWf
        Dim dtProc As DataTable
        Dim wcr_con As String()
        Dim wcr_d = JsonConvert.DeserializeObject(dataValue)
        Dim wcr_data = JArray.Parse(dataValue.ToArray).ToArray
        Dim wcr_next
        Dim wcr_next_2

        Dim rtn As Boolean = False
        Dim countTrue As Integer = 0
        Dim countData As Integer = 0
        Dim countRule As Integer = 0
        Dim arrTampung As String = ""
        Dim prmVal As String = ""
        Dim Data
        Try
            


            'For Each row As Integer In wcr_id 'looping wcr
            countRule = 0
            dtProc = objWork.getProcCond2(wcr_id(0))
            wcr_con = dtProc.Rows(0).Item("wcr_rule").ToString.Split("&") 'split rule
            If dtProc.Rows(0).Item("wcr_rule").ToString.Contains("NOT") Then
                wcr_con = dtProc.Rows(0).Item("wcr_rule").ToString.Replace("NOT", "").Replace("(", "").Replace(")", "").Split("&")
            End If
            For Each xrow In wcr_con ' looping rule
                Dim rule As String() = xrow.Trim().Split(" ")
                'For v As Integer = 0 To wcr_data.Count 'looping data
                For Each xw In wcr_data
                    'Dim wcr_data2 = xw.
                    Dim xv As String() = xw.ToString.Replace("{", "").Replace("}", "").Replace("""", "").Trim().Split(":")

                    If xv(0).Trim() = rule(0).Trim() Then
                        prmVal &= xv(0) & "=" & xv(1) & "|"
                        Dim fdate = JObject.Parse(xw.ToString)(rule(0).Trim()).ToString
                        If parsingParameter(xrow.Trim(), fdate.Trim()) Then 'wcr_data(countData)(wcr_data2(0))
                            countTrue = countTrue + 1
                        End If
                    End If
                    countData = countData + 1
                Next
                countData = 0
                countRule = countRule + 1
            Next

            Dim dtReturn As DataTable
            Dim cek = objWork.getLogDet(wfId)
            If cek = 0 Then
                dtReturn = objWork.insertLog("-", wpmId)
                If dtReturn.Rows.Count > 0 Then
                    wfId = dtReturn.Rows(0).Item("wf_id")
                End If
            Else
                dtReturn = objWork.insertLogDetail(conVal(wfId), wpmId, counter, "", "")
                If dtReturn.Rows.Count > 0 Then
                    wfId = dtReturn.Rows(0).Item("wf_id")
                End If
            End If

            If countTrue = countRule Then
                arrTampung = dtProc.Rows(0).Item("wcr_message").ToString
                wcr_next = dtProc.Rows(0).Item("wcr_next_process")
                wcr_next_2 = dtProc.Rows(0).Item("wcr_next_process_2")

                doNextProcess(wcr_next)
                doNextProcess(wcr_next_2)
            Else
                dtProc = objWork.getProcCond2(wcr_id(1))
                arrTampung = dtProc.Rows(0).Item("wcr_message").ToString
                wcr_next = dtProc.Rows(0).Item("wcr_next_process")
                wcr_next_2 = dtProc.Rows(0).Item("wcr_next_process_2")

                doNextProcess(wcr_next)
                doNextProcess(wcr_next_2)
            End If
            'Next
            Data = New With
                        {
                           Key .status = "success",
                           Key .message = "success",
                           Key .outputMsg = arrTampung,
                           Key .wcrNextProces = wcr_next,
                           Key .wcrNextProces_2 = wcr_next_2
                       }
        Catch ex As Exception
            Data = New With
                        {
                            Key .status = "error",
                            Key .message = ex.Message
                        }
        End Try

        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function parsingParameter(ByVal dataParam As String, ByVal dataValue As String)
        Dim Bool As Boolean = False
        Dim str_split As String()
        If dataParam.Contains(" < ") Then
            str_split = dataParam.ToString.Split("<")
            If conVal(dataValue.Trim()) < conVal(str_split(1).Trim()) Then
                Bool = True
            End If
        ElseIf dataParam.Contains(" > ") Then
            str_split = dataParam.ToString.Split(">")
            If conVal(dataValue.Trim()) > conVal(str_split(1).Trim()) Then
                Bool = True
            End If
        ElseIf dataParam.Contains(" = ") Then
            str_split = dataParam.ToString.Split("=")
            If conVal(dataValue.Trim()) = conVal(str_split(1).Trim()) Then
                Bool = True
            End If
        ElseIf dataParam.Contains(" <> ") Then
            str_split = dataParam.ToString.Replace(">", "").Split("<")
            If conVal(dataValue.Trim()) <> conVal(str_split(1).Trim()) Then
                Bool = True
            End If
        End If
        Return Bool
    End Function

    Function conVal(ByVal param)
        Dim Rtn
        If IsNumeric(param) Then
            Rtn = Integer.Parse(param)
        ElseIf param Is Nothing Then
            Rtn = ""
        ElseIf param.ToString.Contains("''") Then
            Rtn = ""
        Else
            Rtn = param
        End If
        Return Rtn
    End Function

    Sub doNextProcess(ByVal wcrId As Integer)
        If wcrId <> 0 Then
        End If
    End Sub

    Sub createLog(ByVal wfId As Integer, ByVal wpmId As Integer)
        Dim user As String = "-"
        Dim dtReturn As DataTable
        If wfId <> 0 Then
            dtReturn = objWork.insertLog(user, wpmId)
        Else
            'dtReturn = objWork
        End If
    End Sub



End Class
