Imports System.Data.SqlClient

Public Class lProductOffer
    Function getDataProductOffer()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_getProductOffering", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Function getDropDownlistData(ByVal obj)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_dropdownCtl", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@object", obj)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Function getDataCategory()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_getCategoryList", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Function getDataType()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_getTypeList", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Function getDataProductOfferById(ByVal poId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_getProductOfferingById", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@poId", poId)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Function updateDataProductOffering(ByVal req, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_updProductOffer", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@poId", req.form("prod_id"))
        cmd.Parameters.AddWithValue("@offeringCode", req.form("txtOfferingCode"))
        cmd.Parameters.AddWithValue("@category", req.form("ddlCategory"))
        cmd.Parameters.AddWithValue("@product", req.form("txtProduct"))
        cmd.Parameters.AddWithValue("@type", req.form("ddlType"))
        cmd.Parameters.AddWithValue("@sla", req.form("txtSLA"))
        cmd.Parameters.AddWithValue("@userid", usr)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Function insertDataProductOffering(ByVal req, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_insProductOffer", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@offeringCode", req.form("txtOfferingCode"))
        cmd.Parameters.AddWithValue("@category", req.form("ddlCategory"))
        cmd.Parameters.AddWithValue("@product", req.form("txtProduct"))
        cmd.Parameters.AddWithValue("@type", req.form("ddlType"))
        cmd.Parameters.AddWithValue("@sla", req.form("txtSLA"))
        cmd.Parameters.AddWithValue("@userid", usr)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        myConn.Close()
        Return ds
    End Function

    Sub deleteDataProductOfferingById(ByVal poId)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_delProductOfferById", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@poid", poId)
        cmd.ExecuteNonQuery()
        myConn.Close()
    End Sub

End Class
