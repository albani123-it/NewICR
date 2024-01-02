Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Iws_tasklist" in both code and config file together.
<ServiceContract()> _
Public Interface Iws_tasklist

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?auth={auth}&UniqCode={uniqcode}")> _
    Function getTaskListData(ByVal auth As String, ByVal uniqCode As String) As String

End Interface
