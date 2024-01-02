Imports System.Data.SqlClient

Public Class lDataMgmt
    Function getListDataMgmt()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_getListOfDataManagement", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDataSource(ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_getDataSource", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getDataSourceDetail(ByVal id, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_getDataSourceDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@data_id", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = typ
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function LoadSourceTableColumns(ByVal source)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_getDataSourceTableColumns", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@table_name", SqlDbType.VarChar, 500).Value = source
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function LoadDataUploadTableFields(ByVal table_name) As String
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        Dim fields As String
        Dim isDelete As Integer = 0
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("dtm_getDataUploadFields", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@table_name", SqlDbType.VarChar, 500).Value = table_name
        myConn.Open()
        fields = myComm.ExecuteScalar
        myConn.Close()
        Return fields
    End Function

    Function getDataManagementDetail(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_getDataManagementDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@data_id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function chkExistsInAsset(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_chkExistsInAsset", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@data_id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function chkExistsInRule(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_chkExistsInRule", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@data_id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertDataManagement(ByVal e, ByVal usr)
        Dim stat As Boolean
        If e.form("chkStatus") = "on" Then
            stat = True
        Else
            stat = False
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_insertDataManagement", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@datatype", SqlDbType.VarChar, 100).Value = e.form("ddlDataType")
        cmda.Parameters.Add("@dataname", SqlDbType.VarChar, 100).Value = e.form("txtDataName")
        cmda.Parameters.Add("@datadesc", SqlDbType.Text).Value = e.form("txtDataDesc")
        cmda.Parameters.Add("@finalfield", SqlDbType.VarChar, 1000).Value = e.form("txtFinalField")
        cmda.Parameters.Add("@finalrule", SqlDbType.VarChar, 1000).Value = e.form("txtFinalRule")
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@is_active", SqlDbType.Bit).Value = stat
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateDataManagement(ByVal e, ByVal dataid, ByVal usr)
        Dim stat As Boolean
        If e.form("chkStatus") = "on" Then
            stat = True
        Else
            stat = False
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_updateDataManagement", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@datatype", SqlDbType.VarChar, 100).Value = e.form("ddlDataType")
        cmda.Parameters.Add("@dataname", SqlDbType.VarChar, 100).Value = e.form("txtDataName")
        cmda.Parameters.Add("@datadesc", SqlDbType.Text).Value = e.form("txtDataDesc")
        cmda.Parameters.Add("@finalfield", SqlDbType.VarChar, 1000).Value = e.form("txtFinalField")
        cmda.Parameters.Add("@finalrule", SqlDbType.VarChar, 1000).Value = e.form("txtFinalRule")
        cmda.Parameters.Add("@is_active", SqlDbType.Bit).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@data_id", SqlDbType.Int).Value = dataid
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteDataSourceJoin(ByVal dataid, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_deleteDataSourceJoin", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@data_id", SqlDbType.Int).Value = dataid
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertDataJoin(ByVal ds_datasource, ByVal ds_merging_condition, ByVal ds_tbl_name, ByVal ds_fields, ByVal ds_condition, ByVal ds_merging_keys, ByVal id, ByVal ds_alias, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_insertDataJoin", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@datasource", SqlDbType.VarChar, 100).Value = ds_datasource
        cmda.Parameters.Add("@datamergingcondition", SqlDbType.VarChar, 1000).Value = ds_merging_condition
        cmda.Parameters.Add("@datatblname", SqlDbType.VarChar, 1000).Value = ds_tbl_name
        cmda.Parameters.Add("@datafields", SqlDbType.VarChar, 1000).Value = ds_fields
        cmda.Parameters.Add("@datajoincondition", SqlDbType.VarChar, 100).Value = ds_condition
        cmda.Parameters.Add("@datajoinmergingkeys", SqlDbType.VarChar, 100).Value = ds_merging_keys
        cmda.Parameters.Add("@datamgmt_id", SqlDbType.Int).Value = id
        cmda.Parameters.Add("@datajoinalias", SqlDbType.VarChar, 100).Value = ds_alias
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteDataManagement(ByVal dataid, ByVal name, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("dtm_deleteDataManagement", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.Int).Value = dataid
        cmda.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
