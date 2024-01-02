' NOTE: You can use the "Rename" command on the context menu to change the class name "WFService" in code, svc and config file together.
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class WFService
    Implements IWFService

#Region "Global Variable"
    Dim objWork As New lWFService
#End Region

    Function wfType() As String Implements IWFService.wfType
        Return showWfType()
    End Function

    Function wfList(ByVal wpmType, ByVal Role) As String Implements IWFService.wfList
        Return showWfList(wpmType, Role)
    End Function

    Function wfProcess(ByVal wpmId, ByVal user, ByVal Role) As String Implements IWFService.wfProcess
        Return showWfProcess(wpmId, user, Role)
    End Function
    Function wfStop(ByVal wacId, ByVal user, ByVal Role, Optional ByVal process = 0) As String Implements IWFService.wfStop

        Return doCancelWF(wacId, user, Role, process)
    End Function

    Function wfNext(ByVal wacId, ByVal user, ByVal Role) As String Implements IWFService.wfNext
        Return showWFNextProcess(wacId, user, Role)
    End Function

    Function wfHit(ByVal wacId, ByVal wpsId, ByVal user, ByVal Role, ByVal param) As String Implements IWFService.wfHit
        Return doHitWF(wacId, wpsId, user, Role, param)
    End Function

    Function wfInfo(ByVal wacId, ByVal user, ByVal Role) As String Implements IWFService.wfInfo
        Return showAllProcess(wacId, user, Role)
    End Function

    Function wfData(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String Implements IWFService.wfData
        Return ""
    End Function

    Function wfHistory(ByVal wacId, ByVal user, ByVal Role) As String Implements IWFService.wfHistory
        Return showHistory(wacId)
    End Function

    Function wfTasklist(ByVal wacId, ByVal user, ByVal Role) As String Implements IWFService.wfTasklist
        Return showTasklist(user)
    End Function

#Region "Function"
    Function showWfType() As String
        Dim Data
        Dim dtWftype As DataTable
        Try
            dtWftype = objWork.wf_getType()
            Data = New With {
                Key .status = "success",
                Key .message = "success",
                Key .data = dtWftype
            }
        Catch ex As Exception
            Data = New With {
                Key .status = "error",
                Key .message = ex.Message
            }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function


    Function showWfList(ByVal wpmType, ByVal userRole) As String
        Dim Data
        Dim dtWftype As DataTable
        Try
            dtWftype = objWork.wf_getList(wpmType, userRole)
            Data = New With {
                Key .status = "success",
                Key .message = "success",
                Key .data = dtWftype
            }
        Catch ex As Exception
            Data = New With {
                Key .status = "error",
                Key .message = ex.Message
            }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function showWfProcess(ByVal wpmId, ByVal User, ByVal Role) As String
        Dim Data, Info, wacId, wpsId, wpmName, wcpType
        Dim dtProcessField As DataTable
        Dim dtWacId As DataTable

        Try
            dtWacId = objWork.wf_insertAct(wpmId, User)
            wacId = dtWacId.Rows(0).Item("wac_id")

            dtProcessField = objWork.wf_getProcessField(wpmId, Role)
            wpsId = dtProcessField.Rows(0).Item("wps_id")


            dtWacId = objWork.wf_insertActDetail(wacId, wpsId, User)
            wpmName = dtWacId.Rows(0).Item("wad_process_name")
            wcpType = dtWacId.Rows(0).Item("wcp_type")

            objWork.wf_insertLogUserAct(wacId, wpsId, User)

            Info = New With {
                Key .wac_id = wacId,
                Key .wpm_name = wpmName,
                Key .wcp_type = wcpType
            }
            Data = New With {
               Key .status = "success",
               Key .message = "success",
               Key .info = [Info],
               Key .data = dtProcessField
           }
            objWork.wf_updateActFinish(wacId)
        Catch ex As Exception
            Data = New With {
              Key .status = "error",
              Key .message = ex.Message
          }
        End Try

        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function doCancelWF(ByVal wacId, ByVal user, ByVal Role, Optional ByVal process = 0) As String
        Dim Data
        Dim dtWacId As DataTable
        If IsDBNull(process) Then
            process = 0
        End If
        Try
            dtWacId = objWork.wf_insertActDetail(wacId, process, user)

            Data = New With {
                 Key .status = "success",
                 Key .message = "Cancel Successfully"
                }
        Catch ex As Exception
            Data = New With {
              Key .status = "error",
              Key .message = ex.Message
              }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function showWFNextProcess(ByVal wacId, ByVal user, ByVal Role)
        Dim Data
        Dim dtNextProcess As DataTable
        Try
            dtNextProcess = objWork.wf_getNextWF(wacId)

            Data = New With {
                 Key .status = "success",
                 Key .message = "success",
                 Key .info_nextwf = dtNextProcess
                }
        Catch ex As Exception
            Data = New With {
              Key .status = "error",
              Key .message = ex.Message
              }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function showAllProcess(ByVal wacId, ByVal user, ByVal Role)
        Dim Data
        Dim dtNextProcess As DataTable
        Try
            dtNextProcess = objWork.wf_getAllStatus(wacId)

            Data = New With {
                 Key .status = "success",
                 Key .message = "success",
                 Key .info_allprocess = dtNextProcess
                }
        Catch ex As Exception
            Data = New With {
              Key .status = "error",
              Key .message = ex.Message
              }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function doHitWF(ByVal wacId, ByVal wpsId, ByVal user, ByVal Role, ByVal param)
        Dim Data, Info, wpmName, wcpType
        Dim dtProcessField As DataTable
        Dim dtWacId As DataTable

        Try
            dtWacId = objWork.wf_insertActDetail(wacId, wpsId, user)
            wpmName = dtWacId.Rows(0).Item("wad_process_name")
            wcpType = dtWacId.Rows(0).Item("wcp_type")

            Select Case wcpType
                Case "REQ"
                    doRequest()
                Case "PRC"
                    doProcess()
                Case "NTF"
                    doNotif()
                Case "EXE"
                    doExecution()
                Case "CON"
                    doCondition(wpsId, param)
                Case "DLV"
                    doDlV()
                Case Else
                    Console.WriteLine("You typed something else")
            End Select


        Catch ex As Exception

        End Try

    End Function

    Function showHistory(wacId) As String
        Dim Data
        Dim dtNextProcess As DataTable
        Try
            dtNextProcess = objWork.wf_getHistory(wacId)

            Data = New With {
                 Key .status = "success",
                 Key .message = "success",
                 Key .info_history = dtNextProcess
                }
        Catch ex As Exception
            Data = New With {
              Key .status = "error",
              Key .message = ex.Message
              }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function

    Function showTasklist(ByVal userId) As String
        Dim Data
        Dim dtNextProcess As DataTable
        Try
            dtNextProcess = objWork.wf_getTasklistUser(userId)

            Data = New With {
                 Key .status = "success",
                 Key .message = "success",
                 Key .info_tasklist = dtNextProcess
                }
        Catch ex As Exception
            Data = New With {
              Key .status = "error",
              Key .message = ex.Message
              }
        End Try
        Return JsonConvert.SerializeObject(Data, Formatting.None)
    End Function


#End Region

    Private Sub doCondition(ByVal wpsId, ByVal param)

    End Sub

    Private Sub doRequest()
        Throw New NotImplementedException
    End Sub

    Private Sub doProcess()
        Throw New NotImplementedException
    End Sub

    Private Sub doNotif()
        Throw New NotImplementedException
    End Sub

    Private Sub doExecution()
        Throw New NotImplementedException
    End Sub

    Private Sub doDlV()
        Throw New NotImplementedException
    End Sub

    
  

End Class
