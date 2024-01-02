Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Isystem_alert" in both code and config file together.
<ServiceContract()> _
Public Interface Isystem_alert

    <OperationContract()> _
     <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/passkey={passkey}")> _
    Function getSystemAlertData(ByVal passkey As String) As String


End Interface
