﻿Imports System.Data.SqlClient

Public Class enSimAsset

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim aRule = ""
    Dim spName = "en_sim_master_asset"
#End Region

    'Flow 
    '1. buat urutan asset by sub-category
    '2. buat tabel kosong sebagai master tabel untuk setiap sub-category asset
    '3. buat sp en_master_asset untuk setiap asset
    'ex :
    '   IF EXISTS ( SELECT name FROM sysobjects WHERE name = 'idx_master_asset_[ID]' ) 
    '   DROP TABLE idx_master_asset_[ID] 
    '   SELECT idx_master_summary.* INTO idx_master_asset_[ID] FROM idx_master_summary WHERE [RULE ASSET]

    Function enAsset(ByVal usr)
        Dim gFunction As New lGeneral
        'buat urutan asset by sub-category
        Dim ds As New DataSet
        ds = getListOfAsset()
        Dim dtAsset As DataTable
        dtAsset = ds.Tables(0)

        If dtAsset.Rows.Count > 0 Then
            Dim rAsset = dtAsset.Rows
            aSql += " EXEC en_scoring_log '" & usr & "','START','CREDIT RISK ENGINE & CRM BASEL II IRBA (SIMULATION)' "
            For x As Integer = 0 To dtAsset.Rows.Count - 1
                'buat tabel kosong sebagai master tabel untuk setiap sub-category asset
                sql = "IF EXISTS ( SELECT name FROM sysobjects WHERE name = 'sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc' )" & _
                    vbCrLf & "DROP TABLE sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc " & _
                    vbCrLf & "SELECT top 0 * INTO sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc " & _
                    vbCrLf & "FROM dm_" & rAsset(x).Item("data_name") & " "
                gFunction.exec_sp(sql)

                aSql += gFunction.returnLogScriptAsset("sim_master_asset_" & rAsset(x).Item("sid_id") & "", "START", rAsset(x).Item("sid_id"))
                'buat sp en_master_asset untuk setiap asset
                aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = 'sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc' ) " & _
                    vbCrLf & "DROP TABLE sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc " & _
                    vbCrLf & "SELECT TOP " & rAsset(x).Item("sim_data") & " PERCENT * INTO sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc " & _
                    vbCrLf & "FROM dm_" & rAsset(x).Item("data_name") & " WHERE " & rAsset(x).Item("final_rule") & " "
                If aRule <> "" Then
                    aSql += " " & aRule & " "
                End If
                'If x > 0 And aRule <> rAsset(x - 1).Item("final_rule") Then
                '    aRule += " AND NOT ( " & rAsset(x).Item("final_rule") & ")"
                'End If

                'aSql += gFunction.returnLogScript("sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc ", "END")
                aSql += gFunction.returnLogScript("sim_master_asset_" & rAsset(x).Item("sid_id") & "_sc", "END")
            Next
        End If
        gFunction.createSP(spName, aSql)
        Return aSql
    End Function

    Function getListOfAsset()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("en_getSimulation", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

End Class