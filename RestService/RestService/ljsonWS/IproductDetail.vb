Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IproductDetail" in both code and config file together.
<ServiceContract()> _
Public Interface IproductDetail

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/?passkey={pass}&acctno={acctno}&cardno={cardno}&tbl={tbl}")> _
    Function productDetailData(ByVal pass As String, ByVal acctno As String, ByVal cardno As String, ByVal tbl As String) As String


End Interface
