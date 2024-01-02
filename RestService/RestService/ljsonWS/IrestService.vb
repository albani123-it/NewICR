﻿Imports System.ServiceModel
Imports System.ServiceModel.Web

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IrestService" in both code and config file together.
<ServiceContract()> _
Public Interface IrestService

    <OperationContract()> _
    <WebInvoke(Method:="GET", ResponseFormat:=WebMessageFormat.Json, BodyStyle:=WebMessageBodyStyle.Wrapped, UriTemplate:="json/{id}")> _
    Function JSONData(ByVal id As String) As String


End Interface
