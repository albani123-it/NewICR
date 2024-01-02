Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IcustomerDetail" in both code and config file together.
<ServiceContract()> _
Public Interface IcustomerDetail

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}&csv={CSV}")> _
    Function customerDataView(ByVal pass As String, ByVal CSV As String) As String


End Interface
