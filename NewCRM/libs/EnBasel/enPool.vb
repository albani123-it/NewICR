Imports System.Data.SqlClient
Public Class enPool

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim unCon = ""
    Dim spName = "en_score_"
    Dim mTable = "idx_master_asset"
    Dim uniqueField = "ACCTNO"
    Dim masterCollateral = "idx_master_collateral_result"
#End Region
    Dim gFunction As New lGeneral

    Function enPool(ByVal modelId)
        Dim dt As DataTable = gFunction.exec_text("select * from asset_model where mod_id = " & modelId)
        Dim modelinfo = dt.Rows(0)
        mTable = "idx_master_asset_" & modelinfo("mod_id") & "_sc"
        'Return gFunction.returnSPName(modelinfo("mod_id"), "pool", "LGD")
        enPoolPD(modelinfo)
        If modelinfo.Item("mod_approach") = 3 Then
            enPoolLGD(modelinfo)
            enPoolEAD(modelinfo)
        Else
            enPoolFIRB(modelinfo)
        End If
        Return aSql
    End Function

    Function enPoolFIRB(ByVal info)
        aSql = gFunction.returnLogScript(mTable & "_pool_lgd", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_pool_lgd' ) " & _
             vbCrLf & "DROP TABLE " & mTable & "_pool_lgd "

        Dim dtRadio As DataTable
        With New lAsset
            dtRadio = .getDetailRadio(info("mod_id"))
        End With
        If dtRadio.Rows(0).Item("lgd_fixed") Then
            aSql += "Select " & uniqueField & ", '' lgd_rating, " & info("mod_lgd") & " lgd INTO " & mTable & "_pool_lgd from " & mTable & " "
        ElseIf dtRadio.Rows(0).Item("lgd_formula") Then
            Dim lgdFormula As String
            'lgdFormula = " 0.01 * (E_STAR / BALANCE_AMT) * " & info("mod_lgd")
            lgdFormula = " LGD "
            aSql += "Select " & mTable & "." & uniqueField & ", '' lgd_rating, " & lgdFormula & " lgd INTO " & mTable & "_pool_lgd from " & mTable & " "
            aSql += "left join  " & masterCollateral & " on idx_master_collateral_result.ACCTNO = " & mTable & ".ACCTNO "
        ElseIf dtRadio.Rows(0).Item("lgd_field") Then
            aSql += "Select " & uniqueField & ", '' lgd_rating, " & info("mod_lgd") & " lgd INTO " & mTable & "_pool_lgd from " & mTable & " "
        End If

        aSql += gFunction.returnLogScript(mTable & "_pool_lgd", "END")
        spName = gFunction.returnSPName(info("mod_id"), "pool", "LGD")
        gFunction.createSP(spName, aSql)



        aSql = gFunction.returnLogScript(mTable & "_pool_ead", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_pool_ead' ) " & _
            vbCrLf & "DROP TABLE " & mTable & "_pool_ead "
        Dim ccfFormula As String
        If dtRadio.Rows(0).Item("ead_field") Then
            ccfFormula = "dbo.getCCFValue(BALANCE_AMT, " & info("mod_ead_value") & ", CREDIT_LINE_AMT)"
            aSql += "Select " & uniqueField & ", '' ead_rating, " & ccfFormula & " ead INTO " & mTable & "_pool_ead from " & mTable & " "
        Else
            aSql += "Select " & uniqueField & ", '' ead_rating, " & info("mod_ead") & " ead INTO " & mTable & "_pool_ead from " & mTable & " "
        End If

        aSql += gFunction.returnLogScript(mTable & "_pool_ead", "END")
        spName = gFunction.returnSPName(info("mod_id"), "pool", "EAD")
        'Return aSql
        gFunction.createSP(spName, aSql)
    End Function

    Function enPoolPD(ByVal info)
        unCon = ""
        sql = "select * from asset_model_pool " & _
            vbCrLf & "left join rules on rul_id=pol_rule " & _
            vbCrLf & "where pol_mod_id = " & info("mod_id") & " and pol_pd is not null"


        aSql = gFunction.returnLogScript(mTable & "_pool_pd", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_pool_pd' ) " & _
             vbCrLf & "DROP TABLE " & mTable & "_pool_pd "

        Dim dtRadio As DataTable
        With New lAsset
            dtRadio = .getDetailRadio(info("mod_id"))
        End With
        If dtRadio.Rows(0).Item("pd_text") Then
            Dim dt As DataTable = gFunction.exec_text(sql)
            If dt.Rows.Count > 0 Then
                Dim dr = dt.Rows
                aSql += "SELECT * INTO " & mTable & "_pool_pd FROM ("
                For i As Integer = 0 To dt.Rows.Count - 1
                    If i > 0 Then
                        aSql += " UNION "
                    End If

                    Dim pd As String = dr(i).Item("pol_pd")
                    'if kolek > 2 then 1 else pd
                    aSql += "SELECT " & uniqueField & " , '" & dr(i).Item("pol_rule_rate") & "' pd_rating, " & _
                        vbCrLf & " case when KOLEKTIBILITAS_BI > 2 then 1 else " & pd.Replace(",", ".") & " end pd " & _
                        vbCrLf & " FROM " & mTable & " WHERE " & dr(i).Item("rul_sql") & " "

                    If unCon <> "" Then
                        aSql += unCon
                    End If
                    unCon = " AND NOT ( " & dr(i).Item("rul_sql") & " )"
                Next
                aSql += ")alias"
                'Return aSql
            Else
                aSql += "SELECT '' " & uniqueField & ", '' pd_rating, '' pd INTO " & mTable & "_pool_pd "
            End If
        ElseIf dtRadio.Rows(0).Item("pd_field") Then
            aSql += "SELECT * INTO " & mTable & "_pool_pd FROM ("
            aSql += "SELECT " & uniqueField & " , '' pd_rating, " & _
                        vbCrLf & "  " & info("mod_pd") & " pd " & _
                        vbCrLf & " FROM " & mTable & " "
            aSql += ")alias"
        End If

        aSql += gFunction.returnLogScript(mTable & "_pool_pd", "END")
        spName = gFunction.returnSPName(info("mod_id"), "pool", "PD")
        gFunction.createSP(spName, aSql)
    End Function

    Function enPoolLGD(ByVal info)
        unCon = ""
        sql = "select * from asset_model_pool " & _
            vbCrLf & "left join rules on rul_id=pol_rule " & _
            vbCrLf & "where pol_mod_id = " & info("mod_id") & " and pol_lgd is not null"
        Dim dt As DataTable = gFunction.exec_text(sql)

        aSql = gFunction.returnLogScript(mTable & "_pool_lgd", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_pool_lgd' ) " & _
             vbCrLf & "DROP TABLE " & mTable & "_pool_lgd "

        If dt.Rows.Count > 0 Then
            Dim dr = dt.Rows
            aSql += "SELECT * INTO " & mTable & "_pool_lgd FROM ("
            For i As Integer = 0 To dt.Rows.Count - 1
                If i > 0 Then
                    aSql += " UNION "
                End If

                Dim lgd As String = dr(i).Item("pol_lgd")
                aSql += "SELECT " & uniqueField & " , '" & dr(i).Item("pol_rule_rate") & "' lgd_rating, " & lgd.Replace(",", ".") & " lgd " & _
                    vbCrLf & " FROM " & mTable & " WHERE " & dr(i).Item("rul_sql") & " "

                If unCon <> "" Then
                    aSql += unCon
                End If
                unCon += " AND NOT ( " & dr(i).Item("rul_sql") & " )"
            Next
            aSql += ")alias"
        Else
            aSql += "SELECT '' " & uniqueField & ", '' lgd_rating, '' lgd INTO " & mTable & "_pool_lgd "
        End If
        aSql += gFunction.returnLogScript(mTable & "_pool_lgd", "END")
        spName = gFunction.returnSPName(info("mod_id"), "pool", "LGD")
        gFunction.createSP(spName, aSql)
        Return aSql
    End Function

    Function enPoolEAD(ByVal info)
        unCon = ""
        sql = "select * from asset_model_pool " & _
            vbCrLf & "left join rules on rul_id=pol_rule " & _
            vbCrLf & "where pol_mod_id = " & info("mod_id") & " and pol_ead is not null"
        Dim dt As DataTable = gFunction.exec_text(sql)

        aSql = gFunction.returnLogScript(mTable & "_pool_ead", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_pool_ead' ) " & _
                 vbCrLf & "DROP TABLE " & mTable & "_pool_ead "
        If dt.Rows.Count > 0 Then
            Dim dr = dt.Rows
            aSql += "SELECT * INTO " & mTable & "_pool_ead FROM ("
            For i As Integer = 0 To dt.Rows.Count - 1
                If i > 0 Then
                    aSql += " UNION "
                End If

                Dim ead As String = dr(i).Item("pol_ead")
                aSql += "SELECT " & uniqueField & " , '" & dr(i).Item("pol_rule_rate") & "' ead_rating, " & ead.Replace(",", ".") & " ead " & _
                    vbCrLf & " FROM " & mTable & " WHERE " & dr(i).Item("rul_sql") & " "

                If unCon <> "" Then
                    aSql += unCon
                End If
                unCon += " AND NOT ( " & dr(i).Item("rul_sql") & " )"
            Next
            aSql += ")alias"
        Else
            aSql += "SELECT '' " & uniqueField & ", '' ead_rating, '' ead INTO " & mTable & "_pool_ead "
        End If
        aSql += gFunction.returnLogScript(mTable & "_pool_ead", "END")
        spName = gFunction.returnSPName(info("mod_id"), "pool", "EAD")
        gFunction.createSP(spName, aSql)
    End Function

End Class
