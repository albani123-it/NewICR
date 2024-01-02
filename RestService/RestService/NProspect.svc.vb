Imports System.IO
Imports System.Web.Script.Serialization


' NOTE: You can use the "Rename" command on the context menu to change the class name "system_alert" in code, svc and config file together.
Public Class NProspect
    Implements INProspect


    Public Function AddUser(ByVal customer As customerProspect) As String Implements INProspect.ProcessPost
        Dim strJSON As String = ""

        Dim custName As String = customer.customerName.ToString()
        Dim contPerson As String = customer.contactPerson.ToString()
        Dim phoneNumber As String = customer.phoneNumber.ToString()
        Dim TypeOfCustomer As String = customer.typeOfCustomer.ToString()
        Dim prodInterest As String = customer.productInterest.ToString()

        Dim closingDate As String = customer.closingDate.ToString()
        Dim estimatedAmount As String = customer.estimatedAmount.ToString()
        Dim currency As String = customer.currency.ToString()
        Dim action As String = customer.action.ToString()
        Try
            With New lGlobal
                Dim dt As New DataTable
                dt = .saveProspect(custName, contPerson, phoneNumber, TypeOfCustomer, prodInterest, closingDate, estimatedAmount, currency, action)
            End With
        Catch ex As Exception

        End Try


        Dim obj As New customerProspect()
        obj.customerName = customer.customerName.ToString()
        obj.contactPerson = customer.contactPerson.ToString()
        obj.phoneNumber = customer.phoneNumber.ToString()
        obj.typeOfCustomer = customer.typeOfCustomer.ToString()
        obj.productInterest = customer.productInterest.ToString()
        obj.closingDate = customer.closingDate.ToString()
        obj.estimatedAmount = customer.estimatedAmount.ToString()
        obj.currency = customer.currency.ToString()
        obj.action = customer.action.ToString()


        'adding student objects to list
        Dim objStudentList As New List(Of customerProspect)() From {obj}

        Dim objJSSerializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        'Serialization .NET Object to JSON
        strJSON = objJSSerializer.Serialize(objStudentList)

        Return strJSON

    End Function
End Class
