Imports System.Data.SqlClient

Public Class lglobalSettings

    Function getListGlobalSetting()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalSetting", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getListGloRatingBasedRiskWeight()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalRatingBasedRiskWeight", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getAgencyRating(ByVal mode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getDropdownlistGlobalRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@mode", SqlDbType.VarChar, 20).Value = mode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getListGloHaircutSecurity()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalHaircutSecurity", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getListGloNonRatingBasedRiskWeight(ByVal gnrID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalNonRatingBasedRiskWeight", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@gnr_id", SqlDbType.BigInt).Value = gnrID
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getDetailNonRatingBasedRiskWeightDetail(ByVal idParent)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalNonRatingBasedRiskWeightDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@gnr_id", SqlDbType.BigInt).Value = idParent
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function InsertGlobalRatingBased(ByVal assetClass, ByVal ratingAgency, ByVal externalRating, ByVal riskWeight)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_insertGlobalRatingBased", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@assetClass", SqlDbType.VarChar, 100).Value = assetClass
        cmdA.Parameters.Add("@ratingAgency", SqlDbType.VarChar, 100).Value = ratingAgency
        cmdA.Parameters.Add("@externalRating", SqlDbType.VarChar, 100).Value = externalRating
        cmdA.Parameters.Add("@riskWeight", SqlDbType.Float).Value = riskWeight
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function UpdateGlobalRatingBased(ByVal Id, ByVal assetClass, ByVal ratingAgency, ByVal externalRating, ByVal riskWeight)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalRatingBased", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@assetClass", SqlDbType.VarChar, 100).Value = assetClass
        cmdA.Parameters.Add("@ratingAgency", SqlDbType.VarChar, 100).Value = ratingAgency
        cmdA.Parameters.Add("@externalRating", SqlDbType.VarChar, 100).Value = externalRating
        cmdA.Parameters.Add("@riskWeight", SqlDbType.Float).Value = riskWeight
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = Id
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteGlobalRatingBased(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalRatingBased", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@glr_id", SqlDbType.BigInt).Value = id
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function insertGlobalNonRatingBased(ByVal JenisTagihan)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_insertGlobalNonRatingBased", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@JenisTagihan", SqlDbType.VarChar, 100).Value = JenisTagihan
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function insertGlobalNonRatingBasedDetail(ByVal gnrID, ByVal JenisTagihan, ByVal RiskWeight)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_insertGlobalNonRatingBasedDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@gnrID", SqlDbType.VarChar, 100).Value = gnrID
        cmdA.Parameters.Add("@JenisTagihan", SqlDbType.VarChar, 100).Value = JenisTagihan
        cmdA.Parameters.Add("@RiskWeight", SqlDbType.VarChar, 100).Value = RiskWeight
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteGlobalNonRatingBased(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalNonRatingBased", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@gnr_id", SqlDbType.BigInt).Value = id
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function updateGlobalNonRatingBasedDetail(ByVal ID, ByVal JenisTagihan, ByVal riskWeight)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalNonRatingBasedDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ID", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@JenisTagihan", SqlDbType.VarChar, 100).Value = JenisTagihan
        cmdA.Parameters.Add("@RiskWeight", SqlDbType.Float).Value = riskWeight
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteGlobalNonRatingBasedDetailGrid(ByVal type, ByVal ID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalNonRatingBasedDetailGrid", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@GNDID", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@type", SqlDbType.BigInt).Value = type
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function editGlobalNonRatingBasedDetail(ByVal GNRID, ByVal JenisTagihan, ByVal Bobot)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_editGlobalNonRatingBasedDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@GNRID", SqlDbType.BigInt).Value = GNRID
        cmdA.Parameters.Add("@JenisTagihan", SqlDbType.VarChar, 100).Value = JenisTagihan
        cmdA.Parameters.Add("@Bobot", SqlDbType.BigInt).Value = Bobot
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function



    Function getListGloEligibleIRB()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalEligibleIRB", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function updateGlobalEligibleIRB(ByVal ID, ByVal CollateralClass, ByVal MinLGD, ByVal MinCollateralLevel, ByVal MaxCollateralLevel)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalEligibleIRB", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = ID
        cmdA.Parameters.Add("@col_class", SqlDbType.VarChar, 100).Value = CollateralClass
        cmdA.Parameters.Add("@min_lgd", SqlDbType.VarChar, 100).Value = MinLGD
        cmdA.Parameters.Add("@min_col_level", SqlDbType.VarChar, 100).Value = MinCollateralLevel
        cmdA.Parameters.Add("@max_col_level", SqlDbType.VarChar, 100).Value = MaxCollateralLevel
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function getListGloSettlementRisk()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalSettlementRisk", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getListGloCounterpartyCreditRisk()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalCounterPartyCreditRisk", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function InsertGlobalHaircutSecurity(ByVal Agency, ByVal ExternalRating, ByVal DueDate, ByVal BankProvider, ByVal OtherProvider)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_insertGlobalHaircutSecurity", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 100).Value = Agency
        cmdA.Parameters.Add("@externalRating", SqlDbType.VarChar, 100).Value = ExternalRating
        cmdA.Parameters.Add("@dueDate", SqlDbType.Float).Value = DueDate
        cmdA.Parameters.Add("@bankProvider", SqlDbType.Float).Value = BankProvider
        cmdA.Parameters.Add("@otherProvider", SqlDbType.Float).Value = OtherProvider
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function UpdateGlobalHaircutSecurity(ByVal ID, ByVal Agency, ByVal ExternalRating, ByVal DueDate, ByVal BankProvider, ByVal OtherProvider)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalHaircutSecurity", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 100).Value = Agency
        cmdA.Parameters.Add("@externalRating", SqlDbType.VarChar, 100).Value = ExternalRating
        cmdA.Parameters.Add("@dueDate", SqlDbType.Float).Value = DueDate
        cmdA.Parameters.Add("@bankProvider", SqlDbType.Float).Value = BankProvider
        cmdA.Parameters.Add("@otherProvider", SqlDbType.Float).Value = OtherProvider
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteGlobalHaircutSecurity(ByVal ID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalHaircutSecurity", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@GLHID", SqlDbType.BigInt).Value = ID
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function insertGlobalSettlementRisk(ByVal day, ByVal percent)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_insertGlobalSettlementRisk", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@day", SqlDbType.VarChar, 100).Value = day
        cmdA.Parameters.Add("@percentage", SqlDbType.Float).Value = percent
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function updateGlobalSettlementRisk(ByVal id, ByVal day, ByVal percent)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalSettlementRisk", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmdA.Parameters.Add("@day", SqlDbType.VarChar, 100).Value = day
        cmdA.Parameters.Add("@percentage", SqlDbType.Float).Value = percent
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteGlobalSettlementRisk(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalSettlementRisk", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function InsertGlobalCounterParty(ByVal ResidualMaturity, ByVal InterestRate, ByVal ExchangeRateAndGold, ByVal Stock, ByVal MetalBesideGold, ByVal Other)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_InsertGlobalCounterParty", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@residualMaturity", SqlDbType.Float).Value = ResidualMaturity
        cmdA.Parameters.Add("@interestRate", SqlDbType.Float).Value = InterestRate
        cmdA.Parameters.Add("@exchangeRateAndGold", SqlDbType.Float).Value = ExchangeRateAndGold
        cmdA.Parameters.Add("@stock", SqlDbType.Float).Value = Stock
        cmdA.Parameters.Add("@metalBesideGold", SqlDbType.Float).Value = MetalBesideGold
        cmdA.Parameters.Add("@other", SqlDbType.Float).Value = Other
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function updateGlobalCounterParty(ByVal ID, ByVal ResidualMaturity, ByVal InterestRate, ByVal ExchangeRateAndGold, ByVal Stock, ByVal MetalBesideGold, ByVal Other)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalCounterParty", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        cmdA.Parameters.Add("@residualMaturity", SqlDbType.Float).Value = ResidualMaturity
        cmdA.Parameters.Add("@interestRate", SqlDbType.Float).Value = InterestRate
        cmdA.Parameters.Add("@exchangeRateAndGold", SqlDbType.Float).Value = ExchangeRateAndGold
        cmdA.Parameters.Add("@stock", SqlDbType.Float).Value = Stock
        cmdA.Parameters.Add("@metalBesideGold", SqlDbType.Float).Value = MetalBesideGold
        cmdA.Parameters.Add("@other", SqlDbType.Float).Value = Other
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function deleteGlobalCounterParty(ByVal ID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalCounterParty", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function

    Function getListExtRating(ByVal Agency)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListExtRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@agency", SqlDbType.VarChar, 50).Value = Agency
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getDataExtRating(ByVal ID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getDataExtRating", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = ID
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function getListGloPreScreening()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_getListGlobalPreScreening", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function updateGlobalPreScreening(ByVal ID, ByVal brw_type, ByVal MinScore)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalPreScreening", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ID", SqlDbType.Int).Value = ID
        cmdA.Parameters.Add("@brw_type", SqlDbType.VarChar, 100).Value = brw_type
        cmdA.Parameters.Add("@min_score", SqlDbType.Float).Value = MinScore
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
        Return True
    End Function



    Function getDetailOfGlobalCapital(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getDetailOfGlobalCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDataGlobalCapitalUsed(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getDataGlobalCapitalUsed", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getDetailOfGlobalCapitalByPeriod(ByVal month, ByVal year)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getDetailOfGlobalCapitalByPeriod", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@month", month)
        cmdA.Parameters.AddWithValue("@year", year)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Sub insertGlobalCapital(ByVal e, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_insertGlobalCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@month", e.form("ddlMonth"))
        cmdA.Parameters.AddWithValue("@year", e.form("ddlYear"))
        cmdA.Parameters.AddWithValue("@capital", e.form("txtCapital").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@houseLimit", e.form("txtHouseLimit").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@usr", usr)
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateGlobalCapital(ByVal e, ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_updateGlobalCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@month", e.form("ddlMonth"))
        cmdA.Parameters.AddWithValue("@year", e.form("ddlYear"))
        cmdA.Parameters.AddWithValue("@capital", e.form("txtCapital").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@houseLimit", e.form("txtHouseLimit").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@usr", usr)
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteGlobalCapital(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        Dim cmdA As New SqlCommand("glo_deleteGlobalCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@usr", usr)
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListGloCapitalMonth()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getListGlobalCapitalMonth", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListGloCapitalYear()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getListGlobalCapitalYear", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Function getListGloCapital()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr()
        myConn.Open()
        Dim cmdA As New SqlCommand("glo_getListGlobalCapital", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function
End Class
