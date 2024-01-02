Imports System.Data.SqlClient
Public Class enSimResult

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim unCon = ""
    Dim spName = "en_sim_score_"
    Dim mTable = "idx_master_asset"
    Dim resourceTable = "idx_master_asset"
    Dim rwaParam = "12.5"
    Dim uniqueField = "ACCTNO"
#End Region
    Dim gFunction As New lGeneral

    Function enResult(ByVal modelId, ByVal sidID)
        Dim dt As DataTable = gFunction.exec_text("select * from asset_model where mod_id = " & modelId)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("mod_approach") > 1 Then
                Dim modelinfo = dt.Rows(0)
                mTable = "sim_master_asset_" & sidID & "_sc"
                spName = "en_sim_score_" & sidID

                aSql += "EXEC " & spName & "_pool_PD "
                aSql += "EXEC " & spName & "_pool_LGD "
                aSql += "EXEC " & spName & "_pool_EAD "
                aSql += "EXEC " & spName & "_pool_Maturity "

                WriteResult(modelinfo, sidID)
                'Return aSql
            ElseIf dt.Rows(0).Item("mod_approach") = 1 Then
                Dim modelinfo = dt.Rows(0)
                mTable = "sim_master_asset_" & sidID & "_sc"
                resourceTable = "sim_master_asset_" & sidID & "_sc"
                WriteResultSA(modelinfo, sidID)
            End If
        End If
    End Function

    Function WriteResultSA(ByVal modelinfo, ByVal sidID)
        aSql += gFunction.returnLogScript(mTable & "_sc_result", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_sc_result' ) " & _
            vbCrLf & "DROP TABLE " & mTable & "_sc_result "
        'vbCrLf & " dbo.getRWAIRBA(pd, lgd, maturity, BALANCE_AMT, ead)rwa " & _
        'vbCrLf & " into " & mTable & "_result " & _
        aSql += "Select " & resourceTable & "." & uniqueField & ", " & _
                vbCrLf & " '' pd_rating, 0 pd, '' lgd_rating, 0 lgd, '' ead_rating, 0 ead, 0 maturity, CREDIT_LINE_AMT, BALANCE_AMT, " & _
                vbCrLf & " (ATMR_AMT_MRK/" & rwaParam & ") capital_charge, " & _
                vbCrLf & " 0 as expected_loss, " & _
                vbCrLf & " 0 as risk_premium, " & _
                vbCrLf & " ATMR_AMT_MRK rwa, (ATMR_AMT_MRK/" & rwaParam & ") unexpected_loss, 0 as hurdle_rate, 0 as ead_value, 0 as capital_requirement INTO " & mTable & "_sc_result " & _
                vbCrLf & " from " & resourceTable & " "

        aSql += gFunction.returnLogScript(mTable & "_sc_result", "END")
        aSql += " exec en_report_sim " & sidID & ", " & modelinfo("mod_id") & " "

        spName = gFunction.returnSimSPName(sidID, "result", "")
        'Return aSql
        gFunction.createSP(spName, aSql)
    End Function

    Function WriteResult(ByVal modelinfo, ByVal sidID)
        aSql += gFunction.returnLogScript(mTable & "_result", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_result' ) " & _
            vbCrLf & "DROP TABLE " & mTable & "_result "
        'vbCrLf & " dbo.getRWAIRBA(pd, lgd, maturity, BALANCE_AMT, ead)rwa " & _
        'vbCrLf & " into " & mTable & "_result " & _

        Dim dtRadio As DataTable
        With New lAsset
            dtRadio = .getDetailRadio(modelinfo("mod_id"))
        End With
        Dim eadFormula As String
        With dtRadio.Rows(0)
            If .Item("ead_text") Then
                eadFormula = "dbo.getEADValue(BALANCE_AMT, ead, CREDIT_LINE_AMT)"
            ElseIf .Item("ead_field") Then
                eadFormula = gFunction.intFormat(modelinfo("mod_ead_value"))
            End If
        End With

        aSql += "Select " & uniqueField & ", pd_rating, pd, lgd_rating, lgd, ead_rating, ead, maturity, CREDIT_LINE_AMT, BALANCE_AMT, (capital_requirement * ead_value)capital_charge, " & _
                vbCrLf & " dbo.getExpectedLoss(pd, lgd, ead_value)expected_loss, " & _
                vbCrLf & " dbo.getRiskPremium(pd, lgd, capital_requirement, " & gFunction.intFormat(modelinfo("mod_hurdle_rate")) & ") " & _
                vbCrLf & " risk_premium, ((capital_requirement * ead_value) * " & rwaParam & ")rwa, " & _
                vbCrLf & " (capital_requirement * ead_value) unexpected_loss, " & gFunction.intFormat(modelinfo("mod_hurdle_rate")) & " as hurdle_rate, ead_value, capital_requirement INTO " & mTable & "_result FROM (" & _
                vbCrLf & " Select " & mTable & "." & uniqueField & ", " & _
                vbCrLf & " pd_rating, pd, lgd_rating, lgd, ead_rating, ead, maturity, " & _
                vbCrLf & " dbo.getCapitalRequirement(pd, lgd, maturity, BALANCE_AMT, ead, '" & modelinfo("mod_approach_type") & "', " & modelinfo("mod_min_eff_maturity") & ")capital_requirement, " & _
                vbCrLf & " 0 expected_loss, BALANCE_AMT,CREDIT_LINE_AMT, " & eadFormula & " ead_value " & _
                vbCrLf & " from " & mTable & " " & _
                vbCrLf & " left join " & mTable & "_pool_pd on " & mTable & "." & uniqueField & " = " & mTable & "_pool_pd." & uniqueField & " collate database_default " & _
                vbCrLf & " left join " & mTable & "_pool_lgd on " & mTable & "." & uniqueField & " = " & mTable & "_pool_lgd." & uniqueField & " collate database_default " & _
                vbCrLf & " left join " & mTable & "_pool_ead on " & mTable & "." & uniqueField & " = " & mTable & "_pool_ead." & uniqueField & " collate database_default " & _
                vbCrLf & " left join " & mTable & "_maturity on " & mTable & "." & uniqueField & " = " & mTable & "_maturity." & uniqueField & " collate database_default " & _
                vbCrLf & " )allunion "

        aSql += gFunction.returnLogScript(mTable & "_result", "END")
        aSql += " drop table " & mTable & "_pool_pd " & _
            vbCrLf & " drop table " & mTable & "_pool_lgd " & _
            vbCrLf & " drop table " & mTable & "_pool_ead " & _
            vbCrLf & " drop table " & mTable & "_maturity "
        aSql += " exec en_report_sim " & sidID & ", " & modelinfo("mod_id") & " "

        spName = gFunction.returnSimSPName(sidID, "result", "")
        'Return aSql
        gFunction.createSP(spName, aSql)
    End Function

End Class
