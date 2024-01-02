Imports System.Data.SqlClient

Public Class lAsset
    Function getListOfAsset()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getListOfAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getListOfAssetStandardized()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getListOfAssetStandardized", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function


    Function getAssetDataSource(ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getListAssetDataSource", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getModelForAsset(ByVal as_id, ByVal as_approach)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getModelForAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = as_id
        cmda.Parameters.Add("@as_approach", SqlDbType.TinyInt).Value = as_approach
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getParentAsset(ByVal as_id, ByVal approach)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getParentIdOfAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = as_id
        cmda.Parameters.Add("@approach", SqlDbType.TinyInt).Value = approach
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getDetailOfAsset(ByVal astid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getDetailOfAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = astid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getCategoryAsset()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getCategoryAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfModel(ByVal moid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_getDetailOfModel", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = moid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function checkCategoryName(ByVal name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_chkCategoryName", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function checkModelName(ByVal name)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_chkModelName", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Sub insertAsset(ByVal obj, ByVal usr, ByVal stat, ByVal id, ByVal typ, ByVal parent)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_insertAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_name", SqlDbType.VarChar, 50).Value = obj.name
        cmda.Parameters.Add("@as_description", SqlDbType.VarChar, 1000).Value = obj.description
        cmda.Parameters.Add("@as_dataSource", SqlDbType.VarChar, 1000).Value = obj.dataSource
        cmda.Parameters.Add("@as_assetClass", SqlDbType.VarChar, 1000).Value = ""
        cmda.Parameters.Add("@as_type", SqlDbType.Char, 1).Value = typ
        cmda.Parameters.Add("@as_parent", SqlDbType.Int).Value = parent
        cmda.Parameters.Add("@as_user", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@stat_mode", SqlDbType.Char, 2).Value = stat
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = id
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertModelAsset(ByVal obj, ByVal usr, ByVal stat, ByVal id, ByVal astid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_insertModelAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_name", SqlDbType.VarChar, 50).Value = obj.name
        cmda.Parameters.Add("@as_description", SqlDbType.VarChar, 1000).Value = obj.description
        'cmda.Parameters.Add("@as_assetClass", SqlDbType.VarChar, 1000).Value = ""
        cmda.Parameters.Add("@as_approach", SqlDbType.TinyInt).Value = obj.approach
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@as_asset_id", SqlDbType.Int).Value = astid
        cmda.Parameters.Add("@as_user", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@stat_mode", SqlDbType.Char, 2).Value = stat
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteAsset(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_deleteAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@astid", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteModelAsset(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_deleteModelAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@moid", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDetailRadio(ByVal scid)
        Dim myDb As New dbconn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getDetailOfRadio", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scid", SqlDbType.Int).Value = scid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        'Return ds
        Return ds.Tables(0)
    End Function

    Function getExposureClass()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getListOfExposureClass", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getMaturityField(ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getListOfMaturityField", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getDetailRuleModel(ByVal scid, ByVal pooling)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getDetailOfRuleModel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scid", SqlDbType.VarChar, 50).Value = scid
        cmdA.Parameters.Add("@pooling", SqlDbType.VarChar, 10).Value = pooling
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Sub insertPoolAsset(ByVal obj, ByVal usr, ByVal stat, ByVal moid, ByVal astyp)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_insertPoolAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_rule", SqlDbType.Int).Value = obj.rule
        cmda.Parameters.Add("@as_rate", SqlDbType.VarChar, 50).Value = obj.rating
        cmda.Parameters.Add("@as_name", SqlDbType.VarChar, 50).Value = obj.name
        cmda.Parameters.Add("@as_value", SqlDbType.VarChar, 50).Value = obj.value
        cmda.Parameters.Add("@as_baseline", SqlDbType.VarChar, 50).Value = obj.baseline
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = moid
        cmda.Parameters.Add("@as_typ", SqlDbType.VarChar, 50).Value = astyp
        cmda.Parameters.Add("@as_usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updatePoolAsset(ByVal obj, ByVal usr, ByVal stat, ByVal moid, ByVal astyp, ByVal pol_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_updatePoolAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_pol_id", SqlDbType.Int).Value = pol_id
        cmda.Parameters.Add("@as_rule", SqlDbType.Int).Value = obj.rule
        cmda.Parameters.Add("@as_rate", SqlDbType.VarChar, 50).Value = obj.rating
        cmda.Parameters.Add("@as_name", SqlDbType.VarChar, 50).Value = obj.name
        cmda.Parameters.Add("@as_value", SqlDbType.VarChar, 50).Value = obj.value
        cmda.Parameters.Add("@as_baseline", SqlDbType.VarChar, 50).Value = obj.baseline
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = moid
        cmda.Parameters.Add("@as_typ", SqlDbType.VarChar, 50).Value = astyp
        cmda.Parameters.Add("@as_usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateModelAsset(ByVal obj, ByVal usr, ByVal moid, ByVal astyp)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_updateModelAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@mod_hurdle_rate", SqlDbType.VarChar, 20).Value = obj.hurdle_rate
        cmda.Parameters.Add("@mod_approach_type", SqlDbType.VarChar, 50).Value = obj.approach_type
        cmda.Parameters.Add("@mod_pd", SqlDbType.VarChar, 50).Value = obj.pd
        cmda.Parameters.Add("@mod_lgd", SqlDbType.VarChar, 50).Value = obj.lgd
        cmda.Parameters.Add("@mod_ead", SqlDbType.VarChar, 50).Value = obj.ead
        cmda.Parameters.Add("@mod_ead_value", SqlDbType.VarChar, 50).Value = obj.ead_value
        cmda.Parameters.Add("@mod_maturity", SqlDbType.VarChar, 50).Value = obj.maturity
        cmda.Parameters.Add("@mod_status", SqlDbType.Int).Value = obj.status
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = moid
        cmda.Parameters.Add("@as_class", SqlDbType.VarChar, 50).Value = astyp
        cmda.Parameters.Add("@as_usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateModelRadio(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_updateModelRadio", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@mod_id", SqlDbType.Int).Value = obj.mod_id
        cmda.Parameters.Add("@pd_text", SqlDbType.Bit).Value = obj.pd_text
        cmda.Parameters.Add("@pd_field", SqlDbType.Bit).Value = obj.pd_field
        cmda.Parameters.Add("@lgd_fixed", SqlDbType.Bit).Value = obj.lgd_fixed
        cmda.Parameters.Add("@lgd_formula", SqlDbType.Bit).Value = obj.lgd_formula
        cmda.Parameters.Add("@lgd_field", SqlDbType.Bit).Value = obj.lgd_field
        cmda.Parameters.Add("@ead_text", SqlDbType.Bit).Value = obj.ead_text
        cmda.Parameters.Add("@ead_field", SqlDbType.Bit).Value = obj.ead_field
        cmda.Parameters.Add("@maturity_text", SqlDbType.Bit).Value = obj.maturity_text
        cmda.Parameters.Add("@maturity_field", SqlDbType.Bit).Value = obj.maturity_field
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkPooling(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_checkPoolAsset", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Sub deletePoolAsset(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_deletePoolAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@as_id", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@as_usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateModelAssetStandardize(ByVal ccf, ByVal exposure, ByVal stat, ByVal usr, ByVal moid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim status As Integer
        If stat = "on" Then
            status = 1
        Else
            status = 2
        End If

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("ast_updateModelAssetStandardized", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = moid
        cmda.Parameters.Add("@ccf", SqlDbType.Decimal).Value = ccf
        cmda.Parameters.Add("@exposure", SqlDbType.VarChar, 500).Value = exposure
        cmda.Parameters.Add("@stat", SqlDbType.Int).Value = status
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getDetailOfPoolPD(ByVal polid, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getDetailOfPoolPD", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@polid", SqlDbType.Int).Value = polid
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getDetailOfPoolLGD(ByVal polid, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getDetailOfPoolLGD", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@polid", SqlDbType.Int).Value = polid
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function getDetailOfPoolEAD(ByVal polid, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_getDetailOfPoolEAD", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@polid", SqlDbType.Int).Value = polid
        cmdA.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function

    Function checkModelStatus(ByVal astid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("ast_chkModelStatus", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@astid", SqlDbType.Int).Value = astid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds
    End Function
End Class
