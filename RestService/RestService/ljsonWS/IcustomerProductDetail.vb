Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IcustomerProductDetail" in both code and config file together.
<ServiceContract()> _
Public Interface IcustomerProductDetail

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}&acctnum={acctnum}")> _
    Function customerProductDetailData(ByVal pass As String, ByVal acctnum As String) As String

End Interface
