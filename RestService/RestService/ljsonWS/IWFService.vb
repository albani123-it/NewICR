Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IWFService" in both code and config file together.
<ServiceContract()> _
Public Interface IWFService

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_type")> _
    Function wfType() As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_list")> _
    Function wfList(ByVal wpmType, ByVal Role) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_process")> _
    Function wfProcess(ByVal wpmId, ByVal user, ByVal Role) As String


    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_stop")> _
    Function wfStop(ByVal wacId, ByVal user, ByVal Role, Optional ByVal process = 0) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_next")> _
    Function wfNext(ByVal wacId, ByVal user, ByVal Role) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_hit")> _
    Function wfHit(ByVal wacId, ByVal wpsId, ByVal user, ByVal Role, ByVal param) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_info")> _
    Function wfInfo(ByVal wacId, ByVal user, ByVal Role) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_data")> _
    Function wfData(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_history")> _
    Function wfHistory(ByVal wacId, ByVal user, ByVal Role) As String


    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_tasklist")> _
    Function wfTasklist(ByVal wacId, ByVal user, ByVal Role) As String


End Interface
