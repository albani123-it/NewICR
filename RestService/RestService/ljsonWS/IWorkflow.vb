Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IWorkflow" in both code and config file together.
<ServiceContract()> _
Public Interface IWorkflow

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/getProductPost")> _
    Function getProduct(ByVal ID As String) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/getFieldInfo")> _
    Function getFieldInfo(ByVal ID As String) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/getWorkflow")> _
    Function getWorkflow(ByVal wpmId As String, ByVal wpmCounter As String, ByVal wfId As String) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/getNextWorkflow")> _
    Function getNextWorflow(ByVal wpmId As String, ByVal wfId As String, Optional ByVal wpmCounter As String = "0") As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/submitDataWf")> _
    Function submitDataWf(ByVal wcr_id As String(), ByVal dataValue As String, ByVal wfId As String, ByVal wpmId As String, ByVal counter As String) As String


    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/submitDataWf2")> _
    Function submitDataWf2(ByVal wcr_id As String(), ByVal dataValue As String(), ByVal wfId As String, ByVal wpmId As String, ByVal counter As String) As String

    <OperationContract()> _
   <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_type")> _
    Function wfType() As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_list")> _
    Function wfList(ByVal wpmId, ByVal User, ByVal Role) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_process")> _
    Function wfProcess(ByVal wacId, ByVal user, ByVal Role) As String


    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_stop")> _
    Function wfStop(ByVal wacId, ByVal user, ByVal Role, Optional ByVal process = 0) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_next")> _
    Function wfNext(ByVal wacId, ByVal user, ByVal Role) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_hit")> _
    Function wfHit(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_info")> _
    Function wfInfo(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_data")> _
    Function wfData(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String

    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_histori")> _
    Function wfHistory(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String


    <OperationContract()> _
    <WebInvoke(Method:="POST", ResponseFormat:=WebMessageFormat.Json, RequestFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.WrappedRequest, UriTemplate:="json/wf_tasklist")> _
    Function wfTasklist(ByVal wacId, ByVal user, ByVal Role, ByVal param) As String

End Interface
