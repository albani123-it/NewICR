Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ijson_activity_list" in both code and config file together.
<ServiceContract()> _
Public Interface Ijson_activity_list

    <OperationContract()> _
     <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/")> _
    Function getActivityListData() As String


End Interface
