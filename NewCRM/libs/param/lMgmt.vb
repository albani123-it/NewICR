Imports System.Data.SqlClient

Public Class lMgmt
    Function getDataBorrower()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("brw_getDataBorrower", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDataProduct()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_getDataProduct", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getBorrowerScc()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("brw_getBorrowerScc", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getBorrowerSector()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("brw_getBorrowerSector", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailBorrower(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("brw_getDetailBorrower", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getProductList()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_getProductList", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getProductScc(ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_getProductScc", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@category", SqlDbType.VarChar, 10).Value = typ
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailProduct(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_getDetailProduct", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertBorrower(ByVal e, ByVal usr)
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
        Dim cmda As New SqlCommand("brw_insertBorrower", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = e.form("txtBrwType")
        cmda.Parameters.Add("@sec", SqlDbType.Int).Value = e.form("ddlSector")
        cmda.Parameters.Add("@scc", SqlDbType.VarChar, 50).Value = e.form("ddlBrwScc")
        cmda.Parameters.Add("@cutApp", SqlDbType.Int).Value = e.form("txtCutApp")
        cmda.Parameters.Add("@cutRej", SqlDbType.Int).Value = e.form("txtCutRej")
        cmda.Parameters.Add("@status", SqlDbType.Bit).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub editBorrower(ByVal e, ByVal usr, ByVal id)
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
        Dim cmda As New SqlCommand("brw_editBorrower", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = e.form("txtBrwType")
        cmda.Parameters.Add("@sec", SqlDbType.Int).Value = e.form("ddlSector")
        cmda.Parameters.Add("@scc", SqlDbType.VarChar, 50).Value = e.form("ddlBrwScc")
        cmda.Parameters.Add("@cutApp", SqlDbType.Int).Value = e.form("txtCutApp")
        cmda.Parameters.Add("@cutRej", SqlDbType.Int).Value = e.form("txtCutRej")
        cmda.Parameters.Add("@status", SqlDbType.Bit).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertProduct(ByVal e, ByVal usr, ByVal retail, ByVal facility)
        Dim stat As Boolean
        Dim retailFlag As Boolean
        Dim prdScc As String = ""
        Dim prdRetail As String = ""

        If e.form("chkStatus") = "on" Then
            stat = True
        Else
            stat = False
        End If

        If e.form("chkRetail") = "on" Then
            prdRetail = retail
            prdScc = ""
            retailFlag = True
        Else
            prdScc = facility
            prdRetail = ""
            retailFlag = False
        End If

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_insertProduct", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@prd_product", SqlDbType.VarChar, 50).Value = e.form("ddlProduct")
        cmda.Parameters.Add("@prd_name", SqlDbType.VarChar, 250).Value = e.form("txtProductName")
        cmda.Parameters.Add("@prd_desc", SqlDbType.VarChar, 2000).Value = e.form("txtProductDesc")
        cmda.Parameters.Add("@prd_retail_flag", SqlDbType.Bit).Value = retailFlag
        cmda.Parameters.Add("@prd_retail_scc", SqlDbType.VarChar, 50).Value = prdRetail
        cmda.Parameters.Add("@prd_nonretail_facility_scc", SqlDbType.VarChar, 50).Value = prdScc
        cmda.Parameters.Add("@prd_sla", SqlDbType.Int).Value = e.form("txtSLA")
        cmda.Parameters.Add("@prd_credit_limit", SqlDbType.Text).Value = e.form("txtCreditFormula")
        cmda.Parameters.Add("@prd_cut_approved", SqlDbType.Int).Value = e.form("txtCutApp")
        cmda.Parameters.Add("@prd_cut_rejected", SqlDbType.Int).Value = e.form("txtCutRej")
        cmda.Parameters.Add("@status", SqlDbType.Bit).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub editProduct(ByVal e, ByVal usr, ByVal id, ByVal retail, ByVal facility)
        Dim stat As Boolean
        Dim retailFlag As Boolean
        Dim prdScc As String = ""
        Dim prdRetail As String = ""

        If e.form("chkStatus") = "on" Then
            stat = True
        Else
            stat = False
        End If

        If e.form("chkRetail") = "on" Then
            prdRetail = retail
            prdScc = ""
            retailFlag = True
        Else
            prdScc = facility
            prdRetail = ""
            retailFlag = False
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_editProduct", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmda.Parameters.Add("@prd_product", SqlDbType.VarChar, 50).Value = e.form("ddlProduct")
        cmda.Parameters.Add("@prd_name", SqlDbType.VarChar, 250).Value = e.form("txtProductName")
        cmda.Parameters.Add("@prd_desc", SqlDbType.VarChar, 2000).Value = e.form("txtProductDesc")
        cmda.Parameters.Add("@prd_retail_flag", SqlDbType.Bit).Value = retailFlag
        cmda.Parameters.Add("@prd_retail_scc", SqlDbType.VarChar, 50).Value = prdRetail
        cmda.Parameters.Add("@prd_nonretail_facility_scc", SqlDbType.VarChar, 50).Value = prdScc
        cmda.Parameters.Add("@prd_sla", SqlDbType.Int).Value = e.form("txtSLA")
        cmda.Parameters.Add("@prd_credit_limit", SqlDbType.Text).Value = e.form("txtCreditFormula")
        cmda.Parameters.Add("@prd_cut_approved", SqlDbType.Int).Value = e.form("txtCutApp")
        cmda.Parameters.Add("@prd_cut_rejected", SqlDbType.Int).Value = e.form("txtCutRej")
        cmda.Parameters.Add("@status", SqlDbType.Bit).Value = stat
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteBorrower(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("brw_deleteBorrower", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteProduct(ByVal id, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_deleteProduct", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function checkUsedBrwRatingModel(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("brw_checkUsedRatingModel", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function checkUsedPrdRatingModel(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("prd_checkUsedRatingModel", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = id
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function
End Class
