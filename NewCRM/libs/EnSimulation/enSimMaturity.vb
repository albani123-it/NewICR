Imports System.Data.SqlClient
Public Class enSimMaturity

#Region " Global Variable "
    Dim sql = ""
    Dim aSql = ""
    Dim unCon = ""
    Dim spName = ""
    Dim mTable = "sim_master_asset_"
    Dim effMaturity = 1
    Dim uniqueField = "ACCTNO"
#End Region
    Dim gFunction As New lGeneral

    Function enMaturity(ByVal modelId, ByVal sidID)
        Dim dt As DataTable = gFunction.exec_text("select * from asset_model where mod_id = " & modelId)
        Dim modelinfo = dt.Rows(0)
        mTable = "sim_master_asset_" & sidID & "_sc"
        If Not IsDBNull(modelinfo.Item("mod_maturity")) Then
            effMaturity = modelinfo.Item("mod_maturity")
        End If
        aSql = gFunction.returnLogScript(mTable & "_maturity", "START")
        aSql += "IF EXISTS ( SELECT name FROM sysobjects WHERE name = '" & mTable & "_maturity' ) " & _
            vbCrLf & "DROP TABLE " & mTable & "_maturity "

        aSql += "select " & uniqueField & ", " & effMaturity & " maturity into " & mTable & "_maturity from " & mTable & " "
        aSql += gFunction.returnLogScript(mTable & "_maturity", "END")
        'Return aSql
        spName = gFunction.returnSimSPName(sidID, "pool", "Maturity")
        gFunction.createSP(spName, aSql)
    End Function

End Class
