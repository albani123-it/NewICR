Imports System.Data.SqlClient

Public Class lScorecards
    Function valSQL(ByVal rule, ByVal tbl)
        Dim str As String

        str = "SELECT * FROM " & tbl & " WHERE " & rule
        Return IsQueryValid(str)
        'Return str
    End Function

    Public Function checkQueryField(ByVal strQuery As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand(strQuery, myConn)
        cmd.CommandType = CommandType.Text
        myConn.Close()
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)

        Return ds
    End Function

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

    Function getID()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmdA As New SqlCommand("select max(rul_id) from rules", myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getListOfScorecard()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getListOfScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfScorecard(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getDetailOfScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function checkUsedScorecard(ByVal code, ByVal type)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_checkUsedScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = code
        cmda.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = type
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts.Tables(0)
    End Function

    Function getDetailOfScorecardBaseline(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getDetailOfScorecardBaseline", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailOfScorecardRating(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getDetailOfScorecardRating", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getListStatus(ByVal delpar)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getListOfStatus", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@delpar", SqlDbType.Bit).Value = delpar
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getNextScorecardCode(ByVal prefix)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getNextScorecardCode", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@trg", SqlDbType.VarChar, 10).Value = prefix
        cmda.Parameters.Add("@iCount", SqlDbType.Int).Direction = ParameterDirection.Output
        cmda.ExecuteNonQuery()
        Dim iCount, nextVal As String
        iCount = CInt(cmda.Parameters("@iCount").Value) + 1
        If iCount.Length = 1 Then
            iCount = "0" & iCount
        End If
        nextVal = prefix & iCount

        myConn.Close()
        Return nextVal
    End Function

    Sub insertUpdateScorecard(ByVal obj, ByVal usr, ByVal stat, ByVal cat, ByVal action)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_insertUpdateScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@scc_category", SqlDbType.VarChar, 50).Value = cat
        cmda.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = obj.code
        cmda.Parameters.Add("@scc_name", SqlDbType.VarChar, 50).Value = obj.name
        cmda.Parameters.Add("@scc_additive_points", SqlDbType.VarChar, 50).Value = obj.additive_points
        cmda.Parameters.Add("@scc_description", SqlDbType.VarChar).Value = obj.description
        cmda.Parameters.Add("@scc_good", SqlDbType.VarChar).Value = obj.good
        cmda.Parameters.Add("@scc_bad", SqlDbType.VarChar).Value = obj.bad
        cmda.Parameters.Add("@scc_status", SqlDbType.TinyInt).Value = obj.status
        cmda.Parameters.Add("@scc_Action", SqlDbType.TinyInt).Value = action
        cmda.Parameters.Add("@stat_mode", SqlDbType.Char, 2).Value = stat
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getListScorecardChar(ByVal sccode)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getListOfScorecardChar", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scid", SqlDbType.VarChar, 50).Value = sccode
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getScorecardCharDetailField(ByVal cat, ByVal scid, ByVal schid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getScorecardCharDetailField", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@category", SqlDbType.VarChar, 50).Value = cat
        cmda.Parameters.Add("@iType", SqlDbType.VarChar, 50).Value = scid
        cmda.Parameters.Add("@schId", SqlDbType.BigInt).Value = schid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Function getDetailScorecardChar(ByVal schid)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getDetailScorecardChar", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@parSCHID", SqlDbType.BigInt).Value = schid
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub insertScorecardChar(ByVal data, ByVal typ, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_insertUpdateScorecardChar", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@parSCHID", SqlDbType.BigInt).Value = data.schid
        cmda.Parameters.Add("@parSCCode", SqlDbType.VarChar, 50).Value = data.sc_code
        cmda.Parameters.Add("@parField", SqlDbType.VarChar, 128).Value = data.field
        cmda.Parameters.Add("@parDefaultScore", SqlDbType.Decimal).Value = data.default_score
        cmda.Parameters.Add("@parKoefisienScore", SqlDbType.Decimal).Value = data.koefisien_score
        cmda.Parameters.Add("@parFieldType", SqlDbType.TinyInt).Value = data.field_type
        cmda.Parameters.Add("@parMode", SqlDbType.Char, 1).Value = typ
        cmda.Parameters.Add("@parUserLoginID", SqlDbType.VarChar, 50).Value = usr

        For i As Integer = 0 To 19
            cmda.Parameters.Add("@parLimit" & (i + 1), SqlDbType.VarChar).Value = data.att_from(i)
            cmda.Parameters.Add("@parScore" & (i + 1), SqlDbType.VarChar).Value = data.att_to(i)
            cmda.Parameters.Add("@parCount" & (i + 1), SqlDbType.VarChar).Value = data.baseline(i)
            cmda.Parameters.Add("@parGoodCount" & (i + 1), SqlDbType.VarChar).Value = data.base_char_good(i)
            cmda.Parameters.Add("@parBadCount" & (i + 1), SqlDbType.VarChar).Value = data.base_char_bad(i)
        Next
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub updateScorecardChar(ByVal data, ByVal typ, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_insertUpdateScorecardChar", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@parSCHID", SqlDbType.BigInt).Value = data.schid
        cmda.Parameters.Add("@parSCCode", SqlDbType.VarChar, 50).Value = data.sc_code
        cmda.Parameters.Add("@parField", SqlDbType.VarChar, 128).Value = data.field
        cmda.Parameters.Add("@parDefaultScore", SqlDbType.Decimal).Value = data.default_score
        cmda.Parameters.Add("@parKoefisienScore", SqlDbType.Decimal).Value = data.koefisien_score
        cmda.Parameters.Add("@parFieldType", SqlDbType.TinyInt).Value = data.field_type
        cmda.Parameters.Add("@parMode", SqlDbType.Char, 1).Value = typ
        cmda.Parameters.Add("@parUserLoginID", SqlDbType.VarChar, 50).Value = usr

        For i As Integer = 0 To 19
            cmda.Parameters.Add("@parLimit" & (i + 1), SqlDbType.VarChar).Value = data.att_from(i)
            cmda.Parameters.Add("@parScore" & (i + 1), SqlDbType.VarChar).Value = data.att_to(i)
            cmda.Parameters.Add("@parCount" & (i + 1), SqlDbType.VarChar).Value = data.baseline(i)
            cmda.Parameters.Add("@parGoodCount" & (i + 1), SqlDbType.VarChar).Value = data.base_char_good(i)
            cmda.Parameters.Add("@parBadCount" & (i + 1), SqlDbType.VarChar).Value = data.base_char_bad(i)
        Next
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertScorecardBaseline(ByVal data, ByVal scid, ByVal usr, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        For i As Integer = 0 To 19
            Dim cmda As New SqlCommand("scc_insertUpdateScorecardBaseline", myConn)
            cmda.CommandType = CommandType.StoredProcedure
            cmda.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = scid
            cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
            cmda.Parameters.Add("@parLimit", SqlDbType.VarChar).Value = data.att_from(i)
            cmda.Parameters.Add("@parScore", SqlDbType.VarChar).Value = data.att_to(i)
            cmda.Parameters.Add("@parCount", SqlDbType.Decimal).Value = data.baseline(i)
            cmda.Parameters.Add("@parGoodCount", SqlDbType.Decimal).Value = data.base_good(i)
            cmda.Parameters.Add("@parBadCount", SqlDbType.Decimal).Value = data.base_bad(i)
            cmda.Parameters.Add("@stat", SqlDbType.VarChar, 50).Value = status
            If i = 0 Then
                cmda.Parameters.Add("@delete", SqlDbType.Bit).Value = True
            Else
                cmda.Parameters.Add("@delete", SqlDbType.Bit).Value = False
            End If
            cmda.ExecuteNonQuery()
        Next
        myConn.Close()
    End Sub

    Sub insertScorecardBaselineLog(ByVal data, ByVal scid, ByVal mode, ByVal usr, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_insertScorecardBaselineLog", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = scid
        cmda.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = status
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@mode", SqlDbType.VarChar, 50).Value = mode
        For i As Integer = 0 To 19
            cmda.Parameters.Add("@parLimit" & (i + 1), SqlDbType.VarChar).Value = data.att_from(i)
            cmda.Parameters.Add("@parScore" & (i + 1), SqlDbType.VarChar).Value = data.att_to(i)
            cmda.Parameters.Add("@parCount" & (i + 1), SqlDbType.Decimal).Value = data.baseline(i)
            cmda.Parameters.Add("@parGoodCount" & (i + 1), SqlDbType.Decimal).Value = data.base_good(i)
            cmda.Parameters.Add("@parBadCount" & (i + 1), SqlDbType.Decimal).Value = data.base_bad(i)
        Next
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub insertScorecardRating(ByVal data, ByVal scid, ByVal usr, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        For i As Integer = 0 To 19
            Dim cmda As New SqlCommand("scc_insertUpdateScorecardRating", myConn)
            cmda.CommandType = CommandType.StoredProcedure
            cmda.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = scid
            cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
            cmda.Parameters.Add("@parLimit", SqlDbType.VarChar).Value = data.att_from(i)
            cmda.Parameters.Add("@parScore", SqlDbType.VarChar).Value = data.att_to(i)
            cmda.Parameters.Add("@parCount", SqlDbType.Decimal).Value = data.baseline(i)
            cmda.Parameters.Add("@parCat", SqlDbType.VarChar, 500).Value = data.ratingCat(i)
            cmda.Parameters.Add("@parDesc", SqlDbType.VarChar, 5000).Value = data.ratingDesc(i)
            cmda.Parameters.Add("@stat", SqlDbType.VarChar, 50).Value = status
            If i = 0 Then
                cmda.Parameters.Add("@delete", SqlDbType.Bit).Value = True
            Else
                cmda.Parameters.Add("@delete", SqlDbType.Bit).Value = False
            End If
            cmda.ExecuteNonQuery()
        Next
        myConn.Close()
    End Sub

    Sub insertScorecardRatingLog(ByVal data, ByVal scid, ByVal mode, ByVal usr, ByVal status)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_insertScorecardRatingLog", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scc_code", SqlDbType.VarChar, 50).Value = scid
        cmda.Parameters.Add("@status", SqlDbType.VarChar, 50).Value = status
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.Parameters.Add("@mode", SqlDbType.VarChar, 50).Value = mode
        For i As Integer = 0 To 19
            cmda.Parameters.Add("@parLimit" & (i + 1), SqlDbType.VarChar).Value = data.att_from(i)
            cmda.Parameters.Add("@parScore" & (i + 1), SqlDbType.VarChar).Value = data.att_to(i)
            cmda.Parameters.Add("@parCount" & (i + 1), SqlDbType.Decimal).Value = data.baseline(i)
            cmda.Parameters.Add("@parCat" & (i + 1), SqlDbType.VarChar, 500).Value = data.ratingCat(i)
            cmda.Parameters.Add("@parDesc" & (i + 1), SqlDbType.VarChar, 5000).Value = data.ratingDesc(i)
        Next
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub


    Function getSchValues(ByVal field)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getScorecardCharValues", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@field", SqlDbType.VarChar, 100).Value = field
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function

    Sub deleteScorecard(ByVal scid, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_deleteScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scid", SqlDbType.VarChar, 50).Value = scid
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteAllScorecard(ByVal scid, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_deleteAllScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scid", SqlDbType.VarChar, 50).Value = scid
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub deleteScorecardChar(ByVal schid, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_deleteScorecardChar", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@schid", SqlDbType.BigInt).Value = schid
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub approveScorecardChar(ByVal schid, ByVal status, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_approveScorecardChar", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@schid", SqlDbType.BigInt).Value = schid
        cmda.Parameters.Add("@status", SqlDbType.TinyInt).Value = status
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Sub approveScorecardBaseline(ByVal scid, ByVal status, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_approveScorecard", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        cmda.Parameters.Add("@scid", SqlDbType.VarChar, 50).Value = scid
        cmda.Parameters.Add("@status", SqlDbType.TinyInt).Value = status
        cmda.Parameters.Add("@usr", SqlDbType.VarChar, 50).Value = usr
        cmda.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function getFieldSummaryBasel()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection

        myConn = myDb.getConStr
        myConn.Open()
        Dim cmda As New SqlCommand("scc_getFieldSummaryBasel", myConn)
        cmda.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmda)
        Dim dts As New DataSet
        sda.Fill(dts)

        myConn.Close()
        Return dts
    End Function
End Class
