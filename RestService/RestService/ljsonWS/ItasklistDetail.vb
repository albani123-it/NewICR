Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ItasklistDetail" in both code and config file together.
<ServiceContract()> _
Public Interface ItasklistDetail

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}&tskcode={tsk_code}")> _
    Function taskListDetailData(ByVal pass As String, ByVal tsk_code As String) As String


End Interface
