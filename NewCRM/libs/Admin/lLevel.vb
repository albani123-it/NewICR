Imports System.Data.SqlClient

Public Class lLevel

    Public Function getListOfLevel()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_getAllLevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getParentLevel()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("list_level", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function


    Public Function getChild(ByVal parentID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("SELECT * FROM level_master where lm_parentID =" & parentID & " order by lm_parentID, lm_urut", myConn)
        cmda.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getSubChild(ByVal childID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("SELECT * FROM level_master where lm_parentID =" & childID & " order by lm_parentID, lm_urut", myConn)
        cmda.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getSubSubChild(ByVal SubChildID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("SELECT * FROM level_master where lm_parentID =" & SubChildID & " order by lm_parentID, lm_urut", myConn)
        cmda.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getLang(ByVal ID)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("acl_list_level_checklist", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@id", SqlDbType.BigInt).Value = ID
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function checkLevel(ByVal check, ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_checkLevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@check", check)
        cmdA.Parameters.AddWithValue("@code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub InsertRecord(ByVal lvlCode, ByVal lvlName, ByVal lvlDescription, ByVal lvlStatus, ByVal lvlLimitFrom, ByVal lvlLimitTo, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_insertAccesslevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@lu_level_code", lvlCode)
        cmdA.Parameters.AddWithValue("@lu_level_name", lvlName)
        cmdA.Parameters.AddWithValue("@lu_level_description", lvlDescription)
        cmdA.Parameters.AddWithValue("@lu_status", validate(lvlStatus))
        cmdA.Parameters.AddWithValue("@lu_limit_from", lvlLimitFrom.ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@lu_limit_to", lvlLimitTo.ToString.Replace(",", ""))
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub editRecord(ByVal e, ByVal usrLogin, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_editAccesslevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id", id)
        cmdA.Parameters.AddWithValue("@lu_level_code", e.form("txtLevelCode"))
        cmdA.Parameters.AddWithValue("@lu_level_name", e.form("txtLevelName"))
        cmdA.Parameters.AddWithValue("@lu_level_description", e.form("txtLevelDescription"))
        cmdA.Parameters.AddWithValue("@lu_status", validate(e.form("chkStatus")))
        cmdA.Parameters.AddWithValue("@userID", usrLogin)
        cmdA.Parameters.AddWithValue("@lu_limit_from", e.form("txtLimitFrom").ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@lu_limit_to", e.form("txtLimitTo").ToString.Replace(",", ""))
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getListLevelMaster()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("acl_getListLevelMaster", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub InsertAccessLevelDetail(ByVal lvl_code, ByVal id_level, ByVal Usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_insertAccesslevelDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@usr", Usr)
        cmdA.Parameters.AddWithValue("@ld_lu_level_code", lvl_code)
        cmdA.Parameters.AddWithValue("@ld_lm_description", id_level)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub DeleteAccessLevelDetail(ByVal lvl_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_deleteAccesslevelDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@ld_lu_level_code", lvl_code)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function checkAccessLevel(ByVal id_level)

        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("acl_countUserLevel", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@lvl_id", SqlDbType.VarChar, 50).Value = id_level
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function editRecordV2(ByVal usrLogin, ByVal id, ByVal txtLevelCode, ByVal txtLevelName, ByVal txtLevelDescription, ByVal chkStatus, ByVal txtLimitFrom, ByVal txtLimitTo)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()


        Dim cmdA As New SqlCommand("acl_editAccesslevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@id_level", id)
        cmdA.Parameters.AddWithValue("@lu_level_code", txtLevelCode)
        cmdA.Parameters.AddWithValue("@lu_level_name", txtLevelName)
        cmdA.Parameters.AddWithValue("@lu_level_description", txtLevelDescription)
        cmdA.Parameters.AddWithValue("@lu_status", chkStatus)
        cmdA.Parameters.AddWithValue("@userID", usrLogin)
        cmdA.Parameters.AddWithValue("@lu_limit_from", txtLimitFrom.ToString.Replace(",", ""))
        cmdA.Parameters.AddWithValue("@lu_limit_to", txtLimitTo.ToString.Replace(",", ""))
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Sub deleteAccessLevel(ByVal lvl_id, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("acl_deleteAccesslevel", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@lvl_id", lvl_id)
        cmdA.Parameters.AddWithValue("@usr", usrLogin)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getAccessLevelDetail(ByVal id_level)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("acl_getAccessDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@id_lvl", id_level)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Public Function getAccessLevelListDetail(ByVal id_level)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("acl_getLevelChekListDetail", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.AddWithValue("@level_code", id_level)
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function


    Function validate(ByVal text) As Boolean
        If text <> "" And text = True Then
            text = 1
        Else
            text = 0
        End If
        Return text
    End Function

End Class
