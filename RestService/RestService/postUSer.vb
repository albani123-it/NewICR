
Public Class postUSer

    Private _userID As String
    Public Property userID As String
        Get
            Return _userID
        End Get
        Set(ByVal value As String)
            _userID = value
        End Set
    End Property

    Private _name As String
    Public Property name As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _npk As String
    Public Property npk As String
        Get
            Return _npk
        End Get
        Set(ByVal value As String)
            _npk = value
        End Set
    End Property

    Private _accessLevel As String
    Public Property accessLevel As String
        Get
            Return _accessLevel
        End Get
        Set(ByVal value As String)
            _accessLevel = value
        End Set
    End Property

    Private _status As String
    Public Property status As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Private _msgi As String
    Public Property msgi As String
        Get
            Return _msgi
        End Get
        Set(ByVal value As String)
            _msgi = value
        End Set
    End Property
End Class
