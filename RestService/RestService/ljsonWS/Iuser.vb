Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Iuser" in both code and config file together.
<ServiceContract()> _
Public Interface Iuser

    <OperationContract()> _
        <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Xml, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/addUser")> _
    Function addUser(ByVal userID As String, ByVal name As String, ByVal npk As String, ByVal accessLevel As String, ByVal status As String, ByVal msgi As String) As postUSer


End Interface
