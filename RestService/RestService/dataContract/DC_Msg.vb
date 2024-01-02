<DataContract()> _
Public Class DC_Msg

    <DataMember()> _
    Public Property status() As String
        Get
            Return m_status
        End Get
        Set(value As String)
            m_status = value
        End Set
    End Property
    Private m_status As String
    <DataMember()> _
    Public Property msg() As String
        Get
            Return m_msg
        End Get
        Set(value As String)
            m_status = m_msg
        End Set
    End Property
    Private m_msg As String
    <DataMember()> _
    Public Property data() As String
        Get
            Return m_data
        End Get
        Set(value As String)
            m_status = m_data
        End Set
    End Property
    Private m_data As String

End Class
