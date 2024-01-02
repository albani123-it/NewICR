Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ilogout" in both code and config file together.
<ServiceContract()> _
Public Interface Ilogout

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/{passKey}")> _
    Function passKeyData(ByVal passkey As String) As String

End Interface
