' NOTE: You can use the "Rename" command on the context menu to change the class name "reassign" in code, svc and config file together.
Public Class reassign
    Implements Ireassign
    Dim msg As String = ""
#Region "Ireassign Members"
    

    Public Function reassign(ByVal assignto As String, ByVal notes As String, ByVal keepTracking As Boolean) As person Implements Ireassign.reassign
        Dim obj As New person

        obj.assignto = assignto
        obj.notes = notes
        obj.keepTracking = keepTracking

        'Try()
        '    With New lGlobal
        '        Dim dt As New DataTable
        '        dt = .commentTasklist(1, notes, "URA", assignto, keepTracking, "23629-00", "", "")
        '        msg = "success"
        '    End With
        'Catch ex As Exception
        '    msg = "Gagal"
        'End Try
        Return obj
    End Function

#End Region

End Class