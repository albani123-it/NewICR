Public Class customerProspect

    Private _customername As String
    Public Property customerName As String
        Get
            Return _customername
        End Get
        Set(ByVal value As String)
            _customername = value
        End Set
    End Property

    Private _contactperson As String
    Public Property contactPerson As String
        Get
            Return _contactperson
        End Get
        Set(ByVal value As String)
            _contactperson = value
        End Set
    End Property

    Private _phoneNumber As String
    Public Property phoneNumber As String
        Get
            Return _phoneNumber
        End Get
        Set(ByVal value As String)
            _phoneNumber = value
        End Set
    End Property

    Private _typeOfCustomer As String
    Public Property typeOfCustomer As String
        Get
            Return _typeOfCustomer
        End Get
        Set(ByVal value As String)
            _typeOfCustomer = value
        End Set
    End Property

    Private _productInterest As String
    Public Property productInterest As String
        Get
            Return _productInterest
        End Get
        Set(ByVal value As String)
            _productInterest = value
        End Set
    End Property

    Private _closingDate As String
    Public Property closingDate As String
        Get
            Return _closingDate
        End Get
        Set(ByVal value As String)
            _closingDate = value
        End Set
    End Property

    Private _estimatedAmount As String
    Public Property estimatedAmount As String
        Get
            Return _estimatedAmount
        End Get
        Set(ByVal value As String)
            _estimatedAmount = value
        End Set
    End Property

    Private _currency As String
    Public Property currency As String
        Get
            Return _currency
        End Get
        Set(ByVal value As String)
            _currency = value
        End Set
    End Property

    Private _action As String
    Public Property action As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property

End Class

