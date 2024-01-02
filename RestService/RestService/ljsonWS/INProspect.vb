Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.IO


<ServiceContract()> _
Public Interface INProspect


    <OperationContract()> _
   <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/New")> _
    Function ProcessPost(ByVal customer As customerProspect) As String

   
End Interface
