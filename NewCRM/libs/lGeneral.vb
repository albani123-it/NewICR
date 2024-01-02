Imports System.Web.Security
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net


Public Class lGeneral
    Function crypPwd(ByVal pwd As String) As String
        Dim strTempChar As String = ""
        Dim i As Integer
        For i = 1 To Len(pwd)
            If Asc(Mid$(pwd, i, 1)) < 128 Then
                strTempChar = _
          CType(Asc(Mid$(pwd, i, 1)) + 128, String)
            ElseIf Asc(Mid$(pwd, i, 1)) > 128 Then
                strTempChar = _
          CType(Asc(Mid$(pwd, i, 1)) - 128, String)
            End If
            Mid$(pwd, i, 1) = _
                Chr(CType(strTempChar, Integer))
        Next i
        Return pwd
    End Function
    Function delete_sp(ByVal sp)
        If Not sp = "" Then
            Dim xStr As String = " if exists ( Select name FROM sysobjects where name = '" & sp & "' ) DROP PROCEDURE dbo." & sp
            run_text(xStr)
        End If
        Return True
    End Function

    Function script_delete_tbl(ByVal tbl)
        Return " if exists ( Select name FROM sysobjects where name = '" & tbl & "' ) DROP table " & tbl
    End Function

    Public Function ValidateNullString(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return True
        Else
            Return CType(parVal, String)
        End If
    End Function

    Public Function parmTimeHour()
        Dim hour As String() = {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"}
        Return hour
    End Function

    Public Function parmTimeMin()
        Dim min As String() = {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"}
        Return min
    End Function

    Public Function parmTgl()
        Dim bln As String() = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"}
        Return bln
    End Function

    Public Function parmIntBln()
        Dim bln As String() = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"}
        Return bln
    End Function

    Public Function parmIntBlnHist()
        Dim bln As String() = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84"}
        Return bln
    End Function

    Public Function parmApproachType()
        Dim parm As String() = {"CORPORATE", "SME", "HVCRE", "RESIDENTIAL MORTGAGE", "REVOLVING RETAIL", "OTHER RETAIL"}
        Return parm
    End Function

    Shared Function isDBTypeNumeric(ByVal pDBType As Integer) As Boolean
        Select Case pDBType
            Case 0, 1, 2
                Return False
            Case Else
                Return True
        End Select
    End Function

    Public Function moneyFormatTextBox(ByVal parVal)
        'return 100,000,000

        If CheckNullString(parVal) = "" Then parVal = 0

        Dim str As String = ""
        If IsDBNull(parVal) Then
            parVal = 0
        End If
        If parVal = 0 Then
            str = "0"
        Else
            str = String.Format("{0:N2}", parVal)
            End If
            Return str


    End Function

    Public Function moneyFormatLabel(ByVal parVal)
        'return 100,000,000.00
        If CheckNullString(parVal) = "" Then parVal = 0
        Dim str As String = ""
        If parVal = 0 Then
            str = "0.00"
        Else
            'str = String.Format("{0:0,0.00}", parVal)
            str = String.Format("{0:N2}", parVal)
        End If
        Return str
    End Function

    Public Function CountGrowth(ByVal A, ByVal B)
        A = A.ToString()
        B = B.ToString()
        If B = "" Or B = "N/A" Or B = "0" Or B = "0.00" Or A = "" Or A = "0" Or A = "0.00" Or A = "N/A" Then
            Return "N/A"
        Else
            Return Math.Round(((A - B) / B) * 100, 2).ToString() + " %"
        End If
    End Function

    Public Function checkInputInaJavascript(ByVal type, ByVal name, ByVal par)
        Dim lMsg As String = ""
        If type = "DropDownList" Then
            If par = "Pilih" Then
                lMsg = name & " harus dipilih.\n"
            End If
        End If
        If type = "TextBox" Then
            If par = "" Then
                lMsg = name & " harus diisi.\n"
            End If
        End If
        Return lMsg
    End Function

    Public Function checkInputInaHTML(ByVal type, ByVal name, ByVal par)
        Dim lMsg As String = ""
        If type = "DropDownList" Then
            If par = "Pilih" Then
                lMsg = "&bull;&nbsp;" & name & " harus dipilih.<br/>"
            End If
        End If
        If type = "TextBox" Then
            If par = "" Then
                lMsg = "&bull;&nbsp;" & name & " harus diisi.<br/>"
            End If
        End If
        Return lMsg
    End Function

    Public Function checkDateParInHtml(ByVal type, ByVal name1, ByVal name2, ByVal par1, ByVal par2)
        Dim lMsg As String = ""
        If type = "less" Then
            If par1 < par2 Then
                lMsg = "&bull;&nbsp;" & name1 & " tidak boleh kurang dari " & name2 & ".<br/>"
            End If
        End If
        If type = "more" Then
            If par1 > par2 Then
                lMsg = "&bull;&nbsp;" & name1 & " tidak boleh lebih dari " & name2 & ".<br/>"
            End If
        End If
        If type = "less than" Then
            If par1 <= par2 Then
                lMsg = "&bull;&nbsp;" & name1 & " tidak boleh kurang dari " & name2 & ".<br/>"
            End If
        End If
        If type = "more than" Then
            If par1 >= par2 Then
                lMsg = "&bull;&nbsp;" & name1 & " tidak boleh lebih dari " & name2 & ".<br/>"
            End If
        End If
        Return lMsg
    End Function

    Function schedMode(ByVal type)
        Dim mode As String = "X"
        If type = "A" Then
            mode = "Automatic"
        ElseIf type = "M" Then
            mode = "Manual"
        End If
        Return mode
    End Function

    Public Function CheckInputDate(ByVal dateString As String) As Boolean
        Try
            Dim dateParts() As String = dateString.Split("-")
            Dim testDate As New Date(Convert.ToInt32(dateParts(2)), _
                                     Convert.ToInt32(dateParts(1)), _
                                     Convert.ToInt32(dateParts(0)))
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckNullString(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return "-"
        Else
            Return CType(parVal, String)
        End If
    End Function

    Public Function CheckNullInt(ByVal parVal As Object) As String
        If IsDBNull(parVal) Then
            Return "0"
        Else
            Return CType(parVal, String)
        End If
    End Function

    Function exec_text(ByVal qry)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand(qry, myConn)
        cmdA.CommandType = CommandType.Text
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function returnLogScript(ByVal table As String, ByVal stat As String)
        Dim idparent As String
        idparent = ""
        Return " exec en_scoring_log_detail '" & table & "', '" & stat & "', '" & idparent & "' "
    End Function

    Function returnMonthIna(ByVal param)
        Dim str As String = ""
        If CheckNullString(param) = "" Then
            str = ""
        ElseIf param = 1 Then
            str = "Januari"
        ElseIf param = 2 Then
            str = "Februari"
        ElseIf param = 3 Then
            str = "Maret"
        ElseIf param = 4 Then
            str = "April"
        ElseIf param = 5 Then
            str = "Mei"
        ElseIf param = 6 Then
            str = "Juni"
        ElseIf param = 7 Then
            str = "Juli"
        ElseIf param = 8 Then
            str = "Agustus"
        ElseIf param = 9 Then
            str = "September"
        ElseIf param = 10 Then
            str = "Oktober"
        ElseIf param = 11 Then
            str = "November"
        ElseIf param = 12 Then
            str = "Desember"
        End If
        Return str
    End Function


    Function returnLogScriptAsset(ByVal table As String, ByVal stat As String, ByVal idparent As String)
        Return " exec en_scoring_log_detail '" & table & "', '" & stat & "', '" & idparent & "' "
    End Function

    Function returnSPName(ByVal modelId, ByVal scc_code, ByVal approach)
        If approach = "" Then
            Return "en_score_" & modelId & "_" & scc_code
        Else
            Return "en_score_" & modelId & "_" & scc_code & "_" & approach
        End If
    End Function

    Function returnSimSPName(ByVal modelId, ByVal scc_code, ByVal approach)
        If approach = "" Then
            Return "en_sim_score_" & modelId & "_" & scc_code
        Else
            Return "en_sim_score_" & modelId & "_" & scc_code & "_" & approach
        End If
    End Function


    Public Function createSP(ByVal sp, ByVal sql, Optional ByVal param = "")
        Dim xStr As String = " if exists ( Select name FROM sysobjects where name = '" & sp & "' ) DROP PROCEDURE dbo." & sp
        exec_sp(xStr)
        Dim str As String = " CREATE Procedure " & sp & " " & param & " AS " & sql & " "
        exec_sp(str)
    End Function

    Sub run_text(ByVal qry)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand(qry, myConn)
        cmdA.CommandType = CommandType.Text
        myConn.Open()
        cmdA.ExecuteNonQuery()
        myConn.Close()
    End Sub

    Function exec_sp(ByVal qry)
        If qry <> "" Then
            Dim myDb As New dbConn
            Dim myConn As SqlConnection
            myConn = myDb.getConStr
            Dim cmdA As New SqlCommand(qry, myConn)
            cmdA.CommandType = CommandType.Text
            myConn.Open()
            cmdA.ExecuteNonQuery()
            myConn.Close()
        End If
        Return True
    End Function


    Function checkUploadData(ByVal extgroup, ByVal filesize)
        Dim lMsg As String = ""

        If extgroup = "images" Then
            If filesize > 204800 Then
                lMsg = lMsg + "File lebih image dari 200 KB"
            End If
        ElseIf extgroup = "documents" Then
            If filesize > 2097152 Then
                lMsg = lMsg + "File lebih image dari 2 MB"
            End If
        ElseIf extgroup = "datas" Then
            If filesize > 10485760 Then
                lMsg = lMsg + "File lebih image dari 10 KB"
            End If
        Else
            lMsg = lMsg + "Bukan termasuk kreteria file yang dapat diupload."
        End If
        Return lMsg
    End Function

    Public Function intFormat(ByVal parVal)
        Dim str As String = parVal
        Return str.Replace(",", ".")
    End Function
    Public Function decFormat(ByVal parVal)
        Dim str As String = parVal
        Return str.Replace(",", "")
    End Function

    ' Iqbal Zuhdi add Function
    Public Function requestForm(ByVal val)
        Dim runat As String = "ctl00$MainContent$" + val
        Return runat
    End Function

    Function createField(fldID, fldType, fldFieldType, fldOption)
        Dim rslt As String = ""
        Dim id As String = fldID
        Dim opt As String()
        Dim chkd As String = ""

        id = id.Replace(" ", "")
        id = id.Replace(".", "")

        Select Case fldFieldType
            Case "textbox"
                rslt = "<input type='" + fldFieldType + "' id='" + id + "' name='" + id + "' class='form-control' required />"
            Case "datepicker"
                rslt = "<input type='text' id='" + id + "' name='" + id + "' class='form-control' readonly />"
                rslt += jsDate(id)
            Case "text"
                rslt = "<textarea id='" + id + "' name='" + id + "' class='form-control noresize'></textarea>"
            Case "radio"
                If fldOption <> "" Then
                    opt = fldOption.ToString.Split("|")
                    'rslt += "<div id='" & id & "' class='btn-group' data-toggle='buttons'>"
                    For i As Integer = 0 To opt.Length - 1
                        If i = 0 Then
                            chkd = "checked='checked'"
                        Else
                            chkd = ""
                        End If

                        'rslt += "<Label Class='btn btn-default' data-toggle-class='btn-primary' data-toggle-passive-class='btn-default'>"
                        'rslt += "<input type='radio' name='" + id + "' id='" + id & "_" & opt(i) + "' " & chkd & " value='" & opt(i) & "' data-parsley-multiple='" & id & "' />" & opt(i) & "&nbsp;&nbsp;&nbsp;"
                        'rslt += "</label>"

                        rslt += "<input type='radio' name='" + id + "' id='" + id & "_" & opt(i) + "' " & chkd & " value='" & opt(i) & "' />" & opt(i) & "&nbsp;&nbsp;&nbsp;"
                    Next
                    'rslt += "</div>"
                End If
            Case "dropdown"
                rslt += "<select id='" + id + "' name='" + id + "' class='form-control' required >"
                rslt += "<option value='-' selected disabled>--Select One--</option>"
                If fldOption <> "" Then
                    opt = fldOption.ToString.Split("|")
                    For i As Integer = 0 To opt.Length - 1
                        rslt += "<option value='" & opt(i) & "'>" & opt(i) & "</option>"
                    Next
                End If
                rslt += "</Select>"
            Case Else
                rslt = " & nbsp;"
        End Select

        Return rslt
    End Function

    Function createFieldAndFill(fldID, fldType, fldFieldType, fldOption, fldValue)
        Dim rslt As String = ""
        Dim id As String = fldID
        Dim opt As String()
        Dim chkd As String = ""

        id = id.Replace(" ", "")
        id = id.Replace(".", "")

        Select Case fldFieldType
            Case "textbox"
                rslt = "<input type='" + fldFieldType + "' id='" + id + "' name='" + id + "' value='" & fldValue & "' class='form-control' required />"
            Case "datepicker"
                rslt = "<input type='text' id='" + id + "' name='" + id + "' value='" & fldValue & "' class='form-control' readonly />"
                rslt += jsDate(id)
            Case "text"
                rslt = "<textarea id='" + id + "' name='" + id + "' class='form-control noresize'>" & fldValue & "</textarea>"
            Case "radio"
                If fldOption <> "" Then
                    opt = fldOption.ToString.Split("|")
                    'rslt += "<div id='" & id & "' class='btn-group' data-toggle='buttons'>"
                    For i As Integer = 0 To opt.Length - 1
                        If opt(i) = fldValue Then
                            chkd = "checked='checked'"
                        Else
                            chkd = ""
                        End If

                        'rslt += "<Label Class='btn btn-default' data-toggle-class='btn-primary' data-toggle-passive-class='btn-default'>"
                        'rslt += "<input type='radio' name='" + id + "' id='" + id & "_" & opt(i) + "' " & chkd & " value='" & opt(i) & "' data-parsley-multiple='" & id & "' />" & opt(i) & "&nbsp;&nbsp;&nbsp;"
                        'rslt += "</label>"

                        rslt += "<input type='radio' name='" + id + "' id='" + id & "_" & opt(i) + "' " & chkd & " value='" & opt(i) & "' />" & opt(i) & "&nbsp;&nbsp;&nbsp;"
                    Next
                    'rslt += "</div>"
                End If
            Case "dropdown"
                rslt += "<select id='" + id + "' name='" + id + "' class='form-control' required >"
                rslt += "<option value='-' selected disabled>--Select One--</option>"
                If fldOption <> "" Then
                    opt = fldOption.ToString.Split("|")
                    For i As Integer = 0 To opt.Length - 1

                        If opt(i) = fldValue Then
                            chkd = "selected"
                        Else
                            chkd = ""
                        End If

                        rslt += "<option value='" & opt(i) & "' " & chkd & ">" & opt(i) & "</option>"
                    Next
                End If
                rslt += "</Select>"
            Case Else
                rslt = " & nbsp;"
        End Select

        Return rslt
    End Function

    Function jsDate(txtID)
        Dim dtpicker As String = ""

        dtpicker += "<script>"
        dtpicker += "$(function () {"
        dtpicker += "$('#" & txtID & "').datepicker({"
        dtpicker += "calender_style: 'picker_4',"
        dtpicker += "duration: '',"
        dtpicker += "yearRange: '-50:+0',"
        dtpicker += "dateFormat: 'dd-mm-yy',"
        dtpicker += "constrainInput: true,"
        dtpicker += "changeMonth: true,"
        dtpicker += "changeYear: true"
        dtpicker += "});"
        dtpicker += "});"
        dtpicker += "</script>"

        Return dtpicker
    End Function



    Public Function ConvertJSONToDataTable(jsonString As String) As DataTable
        Dim dt As New DataTable
        'strip out bad characters
        Dim jsonParts As String() = jsonString.Replace("[", "").Replace("]", "").Split("},{")

        'hold column names
        Dim dtColumns As New List(Of String)

        'get columns
        For Each jp As String In jsonParts
            'only loop thru once to get column names
            Dim propData As String() = jp.Replace("{", "").Replace("}", "").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
            For Each rowData As String In propData
                Try
                    Dim idx As Integer = rowData.IndexOf(":")
                    Dim n As String = rowData.Substring(0, idx - 1)
                    Dim v As String = rowData.Substring(idx + 1)
                    If Not dtColumns.Contains(n) Then
                        dtColumns.Add(n.Replace("""", ""))
                    End If
                Catch ex As Exception
                    Throw New Exception(String.Format("Error Parsing Column Name : {0}", rowData))
                End Try

            Next
            Exit For
        Next

        'build dt
        For Each c As String In dtColumns
            dt.Columns.Add(c)
        Next
        'get table data
        Dim countData As Integer = 1
        For Each jp As String In jsonParts
            If countData < jsonParts.Length Then
                Dim propData As String() = jp.Replace("{", "").Replace("}", "").Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
                Dim nr As DataRow = dt.NewRow
                For Each rowData As String In propData
                    Try
                        Dim idx As Integer = rowData.IndexOf(":")
                        Dim n As String = rowData.Substring(0, idx - 1).Replace("""", "")
                        Dim v As String = rowData.Substring(idx + 1).Replace("""", "")
                        nr(n) = v
                    Catch ex As Exception
                        Continue For
                    End Try

                Next
                dt.Rows.Add(nr)
            End If
            countData = countData + 1
        Next
        Return dt
    End Function

    Public Function alertMessage(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        Dim cmdA As New SqlCommand("us_alertMsg", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = code
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Function getDataExtentionFile(ByVal ext)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("sp_getDataMasterExtentionFile", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@extention", ext)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function

    Function NotifikasiBerhasil()
        Dim info As String
        info = "Data telah berhasil disimpan."
        Return info
    End Function

    Function NotifikasiUpload()
        Dim info As String
        info = "File telah berhasil diupload."
        Return info
    End Function

    Function NotifikasiHapus()
        Dim info As String
        info = "Data telah berhasil dihapus."
        Return info
    End Function

    Public Function moneyNumericLabel(ByVal parVal)
        'return 100,000,000
        If CheckNullString(parVal) = "" Then parVal = 0
        Dim str As String = ""
        If parVal = 0 Then
            str = "0"
        Else
            str = String.Format(New System.Globalization.CultureInfo("en-US"), "{0:N0}", parVal)
        End If
        Return str
    End Function

    Function checkStyle(ByVal code)
        Dim styles As String = ""
        Dim identasi As Integer = 0
        Dim dt As DataTable
        Dim myTable As New DataTable

        myTable.Columns.Add("styles")
        myTable.Columns.Add("identasi")

        dt = getDataAccReportByAccCode(code)
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = myTable.NewRow()

            If dt.Rows(0).Item("acc_font_style") = "B" Then
                styles &= "font-weight: bold; "
            ElseIf dt.Rows(0).Item("acc_font_style") = "I" Then
                styles &= "font-style: italic; "
            ElseIf dt.Rows(0).Item("acc_font_style") = "O" Then
                styles &= "font-style: oblique; "
            ElseIf dt.Rows(0).Item("acc_font_style") = "N" Then
                styles &= "font-style: normal; "
            End If

            If Not IsDBNull(dt.Rows(0).Item("acc_color")) And dt.Rows(0).Item("acc_color") IsNot "#000000" And dt.Rows(0).Item("acc_color") <> "" Then
                styles &= " background : " + dt.Rows(0).Item("acc_color")
            End If

            row("styles") = styles
            row("identasi") = dt.Rows(0).Item("acc_identasi")

            myTable.Rows.Add(row)

        End If

        Return myTable
    End Function
    Function getDataAccReportByAccCode(ByVal code)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr_Analysis
        Dim cmdA As New SqlCommand("sp_getDataAccReportByCode", myConn)
        cmdA.CommandType = CommandType.StoredProcedure
        cmdA.Parameters.AddWithValue("@code", code)
        Dim sda As New SqlDataAdapter(cmdA)
        Dim ds As New DataSet
        sda.Fill(ds)
        Return ds.Tables(0)
    End Function
    Function returnShortMonEN(ByVal param)
        Dim str As String = ""
        If CheckNullString(param) = "" Then
            str = ""
        ElseIf param = 1 Then
            str = "Jan"
        ElseIf param = 2 Then
            str = "Feb"
        ElseIf param = 3 Then
            str = "Mar"
        ElseIf param = 4 Then
            str = "Apr"
        ElseIf param = 5 Then
            str = "May"
        ElseIf param = 6 Then
            str = "Jun"
        ElseIf param = 7 Then
            str = "Jul"
        ElseIf param = 8 Then
            str = "Aug"
        ElseIf param = 9 Then
            str = "Sep"
        ElseIf param = 10 Then
            str = "Oct"
        ElseIf param = 11 Then
            str = "Nov"
        ElseIf param = 12 Then
            str = "Des"
        End If
        Return str
    End Function

End Class
