Imports System.Data.SqlClient

Public Class lStress
    Function getDataStress()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getDataStress", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailStress(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getDetailStress", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailStressResult(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getDetailStressResultList", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getListOfAssetStress()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getListOfAssetStress", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getListOfAssetModelStress(ByVal astid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getListOfAssetModelStress", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@astid", SqlDbType.Int).Value = astid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getLastStressId()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getLastStressId", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getStressDetail(ByVal sID, ByVal moID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_getDetailStressDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@moID", SqlDbType.Int).Value = moID
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertStress(ByVal sID, ByVal usr, ByVal name, ByVal desc)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_insertStress", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@sName", SqlDbType.VarChar, 100).Value = name
        cmda.Parameters.Add("@sDesc", SqlDbType.Text).Value = desc
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertStressDetail(ByVal sID, ByVal moID, ByVal typSelPD, ByVal txtPD, ByVal typPD, ByVal typSelLGD, ByVal txtLGD, ByVal typLGD, ByVal usr, ByVal mode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_insertStressDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@moID", SqlDbType.Int).Value = moID
        cmda.Parameters.Add("@typSelPD", SqlDbType.VarChar, 1).Value = typSelPD
        cmda.Parameters.Add("@txtPD", SqlDbType.Decimal).Value = txtPD
        cmda.Parameters.Add("@typPD", SqlDbType.VarChar, 10).Value = typPD
        cmda.Parameters.Add("@typSelLGD", SqlDbType.VarChar, 1).Value = typSelLGD
        cmda.Parameters.Add("@txtLGD", SqlDbType.Decimal).Value = txtLGD
        cmda.Parameters.Add("@typLGD", SqlDbType.VarChar, 10).Value = typLGD
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@mode", SqlDbType.VarChar, 10).Value = mode
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteStressDetail(ByVal sID, ByVal moID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sts_deleteStressDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@moID", SqlDbType.Int).Value = moID
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteStress(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sts_deleteStress", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
