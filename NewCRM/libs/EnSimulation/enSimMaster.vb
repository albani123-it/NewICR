Imports System.Data.SqlClient

Public Class enSimMaster

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim unCon = ""
    Dim spName = "en_score_"
#End Region
    Dim gFunction As New lGeneral

    Function enMaster()
        'buat urutan asset by sub-category
        ''khusus IRBA
        sql = "select * from simulation inner join simulation_detail on sim_id = sid_sim_id inner join asset_model on sid_mod_id = mod_id WHERE sim_finish_date is null "
        Dim dt As DataTable = gFunction.exec_text(sql)
        If dt.Rows.Count > 0 Then
            Dim rModel = dt.Rows
            For x As Integer = 0 To rModel.Count - 1
                spName = "en_sim_score_" & rModel(x).Item("sid_id")
                If rModel(x).Item("mod_approach") > 1 Then
                    aSql += " EXEC " & spName & "_result "
                Else
                    aSql += " EXEC " & spName & "_result "
                End If
                aSql += " exec en_report_sim " & rModel(x).Item("sid_id") & ", " & rModel(x).Item("mod_id") & " "
            Next
            aSql += " update simulation set sim_finish_date = getdate() where sim_finish_date is null "
            gFunction.createSP("en_sim_calculate", aSql)
        End If
    End Function


End Class
