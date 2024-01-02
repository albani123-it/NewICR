Imports System.Data.SqlClient
Public Class lScorecard
    Sub deleteScorecard(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sc_deleteScorecard", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub

    Sub deleteScorecardChar(ByVal sch_id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sc_deleteScorecardChar", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@sch_id", SqlDbType.VarChar).Value = sch_id
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub

    '*************************************************************************'

    '** start of scorecard category **

    Public Function getListOfScorecardCat()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getListOfScorecardCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getListOfScorecardCatDet(ByVal catid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getListOfScorecardCatDet", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@catid", SqlDbType.VarChar, 30)
        cmdA.Parameters("@catid").Value = catid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function chkScorecardCat(ByVal par, ByVal par2)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_chkScorecardCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@par", SqlDbType.VarChar, 30)
        cmdA.Parameters("@par").Value = par
        cmdA.Parameters.Add("@par2", SqlDbType.VarChar, 255)
        cmdA.Parameters("@par2").Value = par2
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Sub InsertScorecardCat(ByVal code, ByVal name, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sc_insertScorecardCat", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@catcode", SqlDbType.VarChar).Value = code.ToString.ToUpper
        cmdA.Parameters.Add("@catname", SqlDbType.VarChar).Value = name
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Sub UpdateScorecardCat(ByVal code, ByVal name, ByVal usrLogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sc_UpdateScorecardCat", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@catcode", SqlDbType.VarChar).Value = code.ToString.ToUpper
        cmdA.Parameters.Add("@catname", SqlDbType.VarChar).Value = name
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrLogin

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    '** end of scorecard category **

    Public Function IsQueryValidBoolean(ByVal strQuery As String) As Integer
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand(strQuery, myConn)

        Dim IsQueryOK As Integer = 1

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            IsQueryOK = 0
        End Try

        myConn.Close()

        Return IsQueryOK
    End Function

    '** start of query parsing **
    Public Function IsQueryValid(ByVal strQuery As String) As String
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand(strQuery, myConn)

        'Dim IsQueryOK As Boolean = True
        Dim msg As String = ""

        Try
            cmd.ExecuteNonQuery()
        Catch ex As SqlException
            'IsQueryOK = False
            Dim myError As SqlError
            Dim strsqlerror As New StringBuilder

            For Each myError In ex.Errors
                strsqlerror.AppendLine(myError.Message.ToString)
            Next

            msg += strsqlerror.ToString
        End Try

        myConn.Close()

        'Return IsQueryOK
        Return msg
    End Function
    '** end of query parsing **

    Public Function getListOfScorecard()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getListOfScorecard", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getScorecardCat()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getScorecardCat", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getID(ByVal cat_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getID", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat_code", SqlDbType.VarChar, 50)
        cmdA.Parameters("@cat_code").Value = cat_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function getDataSource(ByVal tipe)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getDataSource", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tipe", SqlDbType.VarChar, 30)
        cmdA.Parameters("@tipe").Value = tipe
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getFields(ByVal tbl)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getFields", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@tbl", SqlDbType.VarChar, 255)
        cmdA.Parameters("@tbl").Value = tbl
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Sub insertScorecard(ByVal usrlogin, ByVal b, ByVal c, ByVal d, ByVal e, ByVal f, ByVal g, ByVal h, ByVal i, ByVal j, ByVal k, ByVal l, ByVal m, ByVal n, ByVal o, ByVal p, Optional ByVal q = -1)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sc_insertScorecard", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@scc_id", SqlDbType.Int).Value = a
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = b
        cmdA.Parameters.Add("@scc_name", SqlDbType.VarChar).Value = c
        cmdA.Parameters.Add("@scc_additive_point", SqlDbType.VarChar).Value = d
        cmdA.Parameters.Add("@scc_rule", SqlDbType.Text).Value = e
        cmdA.Parameters.Add("@scc_desc", SqlDbType.Text).Value = f
        cmdA.Parameters.Add("@scc_status", SqlDbType.TinyInt).Value = g
        cmdA.Parameters.Add("@scc_last_update", SqlDbType.VarChar).Value = h
        cmdA.Parameters.Add("@scc_updated_by", SqlDbType.VarChar).Value = i
        cmdA.Parameters.Add("@scc_was_run", SqlDbType.Date).Value = j
        cmdA.Parameters.Add("@scc_data_level", SqlDbType.VarChar).Value = k
        cmdA.Parameters.Add("@scc_tbl_name", SqlDbType.VarChar).Value = l
        cmdA.Parameters.Add("@scc_record", SqlDbType.VarChar).Value = m
        cmdA.Parameters.Add("@scc_final_field", SqlDbType.VarChar).Value = n
        cmdA.Parameters.Add("@scc_cat_code", SqlDbType.VarChar).Value = o
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar).Value = p
        cmdA.Parameters.Add("@scc_id", SqlDbType.Int).Value = q

        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrlogin

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertSccDataSource(ByVal usrlogin, ByVal b, ByVal c, ByVal d, ByVal e, ByVal f, ByVal g, ByVal h, ByVal i, ByVal j)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()

        Dim cmdA As New SqlCommand("sc_insertSccDataSource", myConn)

        cmdA.CommandType = CommandType.StoredProcedure
        'cmdA.Parameters.Add("@ds_id", SqlDbType.Int).Value = a
        cmdA.Parameters.Add("@ds_datasource", SqlDbType.VarChar).Value = b
        cmdA.Parameters.Add("@ds_merging_condition", SqlDbType.VarChar).Value = c
        cmdA.Parameters.Add("@ds_tbl_name", SqlDbType.VarChar).Value = d
        cmdA.Parameters.Add("@ds_fields", SqlDbType.VarChar).Value = e
        cmdA.Parameters.Add("@ds_condition", SqlDbType.VarChar).Value = f
        cmdA.Parameters.Add("@ds_merging_keys", SqlDbType.VarChar).Value = g
        cmdA.Parameters.Add("@ds_scc_id", SqlDbType.Int).Value = h
        cmdA.Parameters.Add("@ds_alias", SqlDbType.VarChar).Value = i
        'cmdA.Parameters.Add("@ds_scc_id", SqlDbType.Int).Value = j
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar).Value = j

        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usrlogin

        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function getListOfScorecardChar(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getListOfScorecardChar", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@sch_sc_code", SqlDbType.VarChar).Value = scc_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getScorecardEdit(ByVal sccid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getScorecardEdit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_id", SqlDbType.Int)
        cmdA.Parameters("@scc_id").Value = Convert.ToInt32(sccid)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Function getDataSourceEdit(ByVal sccid, ByVal typ)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getDataSourceEdit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_id", SqlDbType.Int)
        cmdA.Parameters("@scc_id").Value = sccid
        cmdA.Parameters.Add("@typ", SqlDbType.VarChar)
        cmdA.Parameters("@typ").Value = typ
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    Public Sub deleteDatasourceEdit(ByVal sccid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_deleteDataSourceEdit", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@ds_scc_id", SqlDbType.Int)
        cmdA.Parameters("@ds_scc_id").Value = sccid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
    End Sub

    Public Function cekSccCode(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_CekSccCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar, 50)
        cmdA.Parameters("@scc_code").Value = scc_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function getScCharField(ByVal table_name, ByVal scc_code, ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getScCharField", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@table_name", SqlDbType.VarChar).Value = table_name
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        cmdA.Parameters.Add("@id", SqlDbType.Int).Value = id
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function insertCharContinues(ByVal data, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_insertCharContinues", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@sch_id", SqlDbType.Int).Value = data.schid
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = data.sc_code
        cmdA.Parameters.Add("@field", SqlDbType.VarChar).Value = data.field
        cmdA.Parameters.Add("@field_type", SqlDbType.Int).Value = data.field_type
        cmdA.Parameters.Add("@defaultScore", SqlDbType.Decimal).Value = data.default_score
        cmdA.Parameters.Add("@koefisien", SqlDbType.Decimal).Value = data.koefisien_score
        cmdA.Parameters.Add("@multiply", SqlDbType.VarChar).Value = data.multiply
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function insertCharCategorical(ByVal data, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_insertCharCategorical", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@sch_id", SqlDbType.Int).Value = data.schid
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = data.sc_code
        cmdA.Parameters.Add("@field", SqlDbType.VarChar).Value = data.field
        cmdA.Parameters.Add("@field_type", SqlDbType.Int).Value = data.field_type
        cmdA.Parameters.Add("@defaultScore", SqlDbType.Decimal).Value = data.default_score
        cmdA.Parameters.Add("@koefisien", SqlDbType.Decimal).Value = data.koefisien_score
        cmdA.Parameters.Add("@multiply", SqlDbType.VarChar).Value = data.multiply
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        For i As Integer = 0 To 19
            cmdA.Parameters.Add("@parLimit" & (i + 1), SqlDbType.VarChar).Value = data.att_from(i)
            cmdA.Parameters.Add("@parScore" & (i + 1), SqlDbType.VarChar).Value = data.att_to(i)
        Next
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function insertBIN(ByVal data, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_insertBIN", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = data.sc_code
        cmdA.Parameters.Add("@defaultBin", SqlDbType.Decimal).Value = data.default_bin
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        For i As Integer = 0 To 19
            cmdA.Parameters.Add("@parLimit" & (i + 1), SqlDbType.VarChar).Value = data.att_from(i)
            cmdA.Parameters.Add("@parScore" & (i + 1), SqlDbType.VarChar).Value = data.att_to(i)
        Next
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Function

    Public Function getScBinDetail(ByVal scCode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getScBinDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scCode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getScCharDetail(ByVal sccid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getScCharDetail", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@sccid", SqlDbType.Int).Value = sccid
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Sub copyChar(ByVal scc_code_lama, ByVal scc_code, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_copyChar", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code_lama", SqlDbType.VarChar).Value = scc_code_lama
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Sub copyBin(ByVal scc_code_lama, ByVal scc_code, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_copyBin", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code_lama", SqlDbType.VarChar).Value = scc_code_lama
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        cmdA.Parameters.Add("@usr", SqlDbType.VarChar).Value = usr
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Public Function chkScorecardUsed(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_chkScorecardUsed", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function chkBatch()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_chkBatch", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function chkCopyChar(ByVal scc_code, ByVal fld)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_chkCopyChar", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        cmdA.Parameters.Add("@fld", SqlDbType.VarChar).Value = fld
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Function chkCatUsed(ByVal catcode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("sc_chkCatUsed", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@cat_code", SqlDbType.VarChar).Value = catcode
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Sub deleteSccCategory(ByVal cat_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim myComm As New SqlCommand("sc_deleteSccCategory", myConn)
        myComm.CommandType = CommandType.StoredProcedure
        myComm.Parameters.Add("@cat_code", SqlDbType.VarChar).Value = cat_code
        myConn.Open()
        myComm.ExecuteNonQuery()
    End Sub
    Function getDetailOfScorecard(ByVal scc_code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("sc_getDetailOfScorecard", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@scc_code", SqlDbType.VarChar).Value = scc_code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function


End Class
