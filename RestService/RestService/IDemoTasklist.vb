Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IDemoTasklist" in both code and config file together.
<ServiceContract()> _
Public Interface IDemoTasklist

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/UniqCode={uniqcode}")> _
    Function taskListData(ByVal uniqCode As String) As String

End Interface
