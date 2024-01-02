Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Itasklist" in both code and config file together.
<ServiceContract()> _
Public Interface Itasklist

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}")> _
    Function taskListData(ByVal pass As String) As String

End Interface
