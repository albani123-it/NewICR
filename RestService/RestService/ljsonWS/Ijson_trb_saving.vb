Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ijson_trb_saving" in both code and config file together.
<ServiceContract()> _
Public Interface Ijson_trb_saving

    <OperationContract()> _
     <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/csv={csv}&status={stat}")> _
    Function getTRBSavingData(ByVal csv As String, ByVal stat As String) As String


End Interface
