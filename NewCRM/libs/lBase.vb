Imports System.Data.SqlClient

Public Class lBase
    Public Function AnalysisProcessDataToDs(spname As String, ParamArray list As String())
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmd As New SqlCommand(spname, myConn)
        cmd.CommandType = CommandType.StoredProcedure
        If list IsNot Nothing AndAlso list.Count() > 0 Then
            For Each item As String In list
                Dim pars = item.Split(",")
                If pars.Count() > 2 Then
                    If pars(2) = "i" Then
                        cmd.Parameters.AddWithValue(pars(0), Convert.ToInt32(pars(1)))
                    ElseIf pars(2) = "s" Then
                        cmd.Parameters.AddWithValue(pars(0), Convert.ToString(pars(1)))
                    ElseIf pars(2) = "d" Then
                        cmd.Parameters.AddWithValue(pars(0), Convert.ToDecimal(pars(1)))
                    Else
                        cmd.Parameters.AddWithValue(pars(0), pars(1))
                    End If
                ElseIf pars.Count() > 1 Then
                    cmd.Parameters.AddWithValue(pars(0), pars(1))
                Else
                    cmd.Parameters.AddWithValue(pars(0), pars(0))
                End If
            Next
        End If

        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
