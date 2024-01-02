Public Class person

    Private _assignto As String
    Public Property assignto As String
        Get
            Return _assignto
        End Get
        Set(ByVal value As String)
            _assignto = value
        End Set
    End Property

    Private _notes As String
    Public Property notes As String
        Get
            Return _notes
        End Get
        Set(ByVal value As String)
            _notes = value
        End Set
    End Property

    Private _keepTracking As Boolean
    Public Property keepTracking As Boolean
        Get
            Return _keepTracking
        End Get
        Set(ByVal value As Boolean)
            _keepTracking = value
        End Set
    End Property


End Class
