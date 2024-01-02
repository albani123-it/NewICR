Imports System.Data.SqlClient

Public Class lCalendar
    Public Function insDataAppointment(ByVal id, ByVal title, ByVal attandance, ByVal location, ByVal dateFrom, ByVal dateTo, ByVal room, ByVal desc, ByVal userlogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_insDatAppointment", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@title", title)
        cmd.Parameters.AddWithValue("@attandance", attandance)
        cmd.Parameters.AddWithValue("@location", location)
        cmd.Parameters.AddWithValue("@dateFrom", dateFrom)
        cmd.Parameters.AddWithValue("@dateTo", dateTo)
        cmd.Parameters.AddWithValue("@room", room)
        cmd.Parameters.AddWithValue("@desc", desc)
        cmd.Parameters.AddWithValue("@userlogin", userlogin)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getDataAppointment(ByVal userlogin)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_getDataAppointmentByUserlogin", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@userlogin", userlogin)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function

    Public Function getDataAppointmentByid(ByVal id)
        Dim myDb As New dbConn
        Dim myConn As SqlConnection
        myConn = myDb.getConStr
        myConn.Open()
        Dim cmd As New SqlCommand("sp_loadAppointmentById", myConn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", id)
        Dim sda As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        sda.Fill(ds)
        myConn.Close()
        Return ds.Tables(0)
    End Function
End Class
