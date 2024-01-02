Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IgetPPC" in both code and config file together.
<ServiceContract()> _
Public Interface IgetPPC

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?csv={csv}&order={odr}&field={fld}&value={val}&sort={sort}")> _
    Function getPPCData(ByVal csv As String, ByVal odr As String, ByVal fld As String, ByVal val As String, ByVal sort As String) As String


End Interface
