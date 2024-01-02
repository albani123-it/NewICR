
<DataContract()> _
Public Class DC_offer

    <DataMember()> _
    Public Property ID() As String
        Get
            Return m_ID
        End Get
        Set(value As String)
            m_ID = value
        End Set
    End Property
    Private m_ID As String
    <DataMember()> _
    Public Property details() As String
        Get
            Return m_Detail
        End Get
        Set(value As String)
            m_Detail = value
        End Set
    End Property
    Private m_Detail As String


End Class