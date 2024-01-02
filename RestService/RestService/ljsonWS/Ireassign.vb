Imports System.ServiceModel
Imports System.ServiceModel.Web
' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ireassign" in both code and config file together.
<ServiceContract()> _
Public Interface Ireassign



    <OperationContract()> _
     <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Xml, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/reassign")> _
    Function reassign(ByVal assignto As String, ByVal notes As String, ByVal keepTracking As Boolean) As person


End Interface
