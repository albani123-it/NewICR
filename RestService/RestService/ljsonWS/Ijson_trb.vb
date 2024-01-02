Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ijson_trb" in both code and config file together.
<ServiceContract()> _
Public Interface Ijson_trb

    <OperationContract()> _
     <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/csv={csv}")> _
    Function getTRBData(ByVal csv As String) As String


End Interface
