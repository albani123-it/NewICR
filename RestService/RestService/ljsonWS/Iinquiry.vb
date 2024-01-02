Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Iinquiry" in both code and config file together.
<ServiceContract()> _
Public Interface Iinquiry

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}&txtbox={code}&val={value}&tgl={tgl}")> _
    Function inquiryData(ByVal pass As String, ByVal code As String, ByVal value As String, ByVal tgl As String) As String

End Interface
