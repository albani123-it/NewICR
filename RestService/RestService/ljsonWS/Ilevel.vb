Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ilevel" in both code and config file together.
<ServiceContract()> _
Public Interface Ilevel

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}")> _
    Function LevelViewData(ByVal pass As String) As String

End Interface
