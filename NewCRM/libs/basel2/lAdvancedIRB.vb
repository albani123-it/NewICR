Imports System.Data.SqlClient

Public Class lAdvancedIRB

    Function getDetailRuleModel(ByVal scid, ByVal pol_for)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("adv_getAdvancedDetailOfRuleModel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scid", SqlDbType.Int).Value = scid
        cmdA.Parameters.Add("@pol_for", SqlDbType.VarChar, 10).Value = pol_for
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getGlobalSetting()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("adv_getGlobalSetting", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getInternalModel()
        Dim myDb As New dbConn
        Dim rdr As SqlDataReader
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("adv_getListOfScorecardByApproach", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Connection.Open()
        rdr = cmdA.ExecuteReader(CommandBehavior.CloseConnection)
        Return rdr
    End Function

    Function getRateData(ByVal modelid, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("adv_getListOfRate", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@modId", SqlDbType.Int).Value = modelid
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getActiveModel(ByVal as_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("adv_getActiveModel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@as_id", SqlDbType.Int).Value = as_id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function updateModelAsset(ByVal obj, ByVal usr, ByVal stat, ByVal id, ByVal hr, ByVal assetClass)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("adv_updateModelAsset", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@mod_hurdle_rate", SqlDbType.VarChar, 20).Value = hr
        cmdA.Parameters.Add("@mod_approach_type", SqlDbType.VarChar, 50).Value = obj.approach_type
        cmdA.Parameters.Add("@mod_pd", SqlDbType.VarChar, 50).Value = obj.pd
        cmdA.Parameters.Add("@mod_lgd", SqlDbType.VarChar, 50).Value = obj.lgd
        cmdA.Parameters.Add("@mod_ead", SqlDbType.VarChar, 50).Value = obj.ead
        cmdA.Parameters.Add("@mod_ead_value", SqlDbType.VarChar, 50).Value = obj.ead_value
        cmdA.Parameters.Add("@mod_maturity", SqlDbType.VarChar, 50).Value = obj.maturity
        cmdA.Parameters.Add("@mod_eff_maturity", SqlDbType.VarChar, 50).Value = obj.effmaturity
        cmdA.Parameters.Add("@mod_status", SqlDbType.Int).Value = obj.status
        cmdA.Parameters.Add("@as_id", SqlDbType.Int).Value = id
        cmdA.Parameters.Add("@as_user", SqlDbType.VarChar, 50).Value = usr
        cmdA.Parameters.Add("@assetClass", SqlDbType.VarChar, 50).Value = assetClass
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Sub runCalculationEngine()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("run_CalculationEngine", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
