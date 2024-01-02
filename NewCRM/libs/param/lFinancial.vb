Imports System.Data.SqlClient

Public Class lFinancial
    ''' <summary>
    ''' return list of financial statement
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getListOfFinancialStatement()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_getListOfFinancialStatement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' get detail of financial statement by code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getDetailOfFinancialStatement(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_getDetailOfFinancialStatement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acc_code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' get new code (auto generate)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getNewCodeFinancialStatement()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_getNewCodeFinancialStatement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' get list of financial statement type
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getListOfFinancialStatementType()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_getListOfFinancialStatementType", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' get financial statement sector
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getFinancialStatementSector()
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_getFinancialStatementSector", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' get financial statement sector exists by code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getFinancialStatementSectorExists(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_getFinancialStatementSectorExists", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acc_code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds
    End Function

    ''' <summary>
    ''' INSERT and UPDATE financial statement
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="stat"></param>
    ''' <param name="usr"></param>
    ''' <remarks></remarks>
    Sub insertUpdateFinancialStatement(ByVal e As HttpRequest, ByVal stat As String, ByVal usr As String)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_insertUpdateFinancialStatement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acc_code", e.Form("txtCode"))
        cmdA.Parameters.AddWithValue("@acc_desc", e.Form("txtDescription"))
        cmdA.Parameters.AddWithValue("@acc_input", e.Form("rdInputable"))
        cmdA.Parameters.AddWithValue("@acc_urut", e.Form("txtOrder"))
        cmdA.Parameters.AddWithValue("@acc_proyeksi", e.Form("rdProyeksi"))
        cmdA.Parameters.AddWithValue("@acc_sql_formula", e.Form("txtSQLFormula"))
        cmdA.Parameters.AddWithValue("@acc_sql_formula_proyeksi", e.Form("txtSQLFormulaProyeksi"))
        cmdA.Parameters.AddWithValue("@acc_js_formula", e.Form("txtJsFormula"))
        cmdA.Parameters.AddWithValue("@acc_js_formula_pct", e.Form("txtJsPercentageFormula"))
        cmdA.Parameters.AddWithValue("@acc_notes", e.Form("txtNotes"))
        cmdA.Parameters.AddWithValue("@acc_is_conditional", e.Form("rdConditional"))
        cmdA.Parameters.AddWithValue("@acc_condition", e.Form("txtCondition"))
        cmdA.Parameters.AddWithValue("@acc_condition_value", e.Form("txtConditionValue"))
        cmdA.Parameters.AddWithValue("@acc_rpt", e.Form("ddlReport"))
        cmdA.Parameters.AddWithValue("@acc_font_style", e.Form("ddlFontStyle"))
        cmdA.Parameters.AddWithValue("@acc_identasi", e.Form("txtIdentasi"))
        cmdA.Parameters.AddWithValue("@acc_color", e.Form("txtColor"))
        cmdA.Parameters.AddWithValue("@stat", stat)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    ''' <summary>
    ''' update financial statement per sector
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="sec"></param>
    ''' <remarks></remarks>
    Sub updateFinancialStatementSector(ByVal e As HttpRequest, ByVal sec As String)
        Dim stat As String = 0
        If e.Form("chk_" & sec) = "on" Then
            stat = 1
        End If
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_updateFinancialStatementSector", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acc_code", e.Form("txtCode"))
        cmdA.Parameters.AddWithValue("@sec_id", sec)
        cmdA.Parameters.AddWithValue("@val", stat)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    ''' <summary>
    ''' DELETE financial statement
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="usr"></param>
    ''' <remarks></remarks>
    Sub deleteFinancialStatement(ByVal code, ByVal usr)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        myConn.Open()
        Dim cmdA As New SqlCommand("fin_deleteFinancialStatement", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@acc_code", code)
        cmdA.Parameters.AddWithValue("@usr", usr)
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub
End Class
