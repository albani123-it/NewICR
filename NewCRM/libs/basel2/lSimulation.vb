Imports System.Data.SqlClient

Public Class lSimulation
    Function getListOfAssetModelSimulation(ByVal astid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_getListOfAssetModelSimulation", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@astid", SqlDbType.Int).Value = astid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getListOfAssetSimulation()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_getListOfAssetSimulation", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertSimulation(ByVal sID, ByVal usr, ByVal name, ByVal desc, ByVal data)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_insertSimulation", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@sName", SqlDbType.VarChar, 100).Value = name
        cmda.Parameters.Add("@sDesc", SqlDbType.Text).Value = desc
        cmda.Parameters.Add("@sData", SqlDbType.Decimal).Value = Data
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub insertSimulationDetail(ByVal sID, ByVal moID, ByVal usr, ByVal mode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_insertSimulationDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@moID", SqlDbType.Int).Value = moID
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@mode", SqlDbType.VarChar, 10).Value = mode
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getLastSimulationId()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_getLastSimulationId", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailSimulation(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_getDetailSimulation", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getSimulationDetail(ByVal sID, ByVal moID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_getDetailSimulationDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@sID", SqlDbType.Int).Value = sID
        cmda.Parameters.Add("@moID", SqlDbType.Int).Value = moID
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub deleteSimulation(ByVal sim_id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sim_delete", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ID", SqlDbType.VarChar).Value = sim_id
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Function getDataSimulation()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("sim_getDataSimulation", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub runSimulationEngine()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("run_SimulationEngine", myConn)
        cmdA.CommandType = CommandType.StoredProcedure

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
