Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "Ites" in both code and config file together.
<ServiceContract()> _
Public Interface Ites

    <OperationContract()> _
    Sub DoWork()

End Interface
