Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IproductList" in both code and config file together.
<ServiceContract()> _
Public Interface IproductList

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}&CSV={csv}&order={odr}&field={fld}&value={val}&sort={sort}")> _
    Function productListData(ByVal pass As String, ByVal csv As String, ByVal odr As String, ByVal fld As String, ByVal val As String, ByVal sort As String) As String

End Interface
