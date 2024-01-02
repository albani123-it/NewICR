Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ilogin" in both code and config file together.
<ServiceContract()> _
Public Interface Ilogin


    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?npk={npk}&pass={pass}")> _
    Function USRNPKData(ByVal npk As String, ByVal pass As String) As String



End Interface
