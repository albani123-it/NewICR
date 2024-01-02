Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class lws
    Public Function SendRequest(ByVal uri As Uri, ByVal jsonDataBytes As Byte(), ByVal contentType As String, ByVal method As String) As String
        Dim req As WebRequest = WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length


        Dim stream = req.GetRequestStream()
        stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
        stream.Close()

        Dim response = req.GetResponse().GetResponseStream()

        Dim reader As New StreamReader(response)
        Dim res = reader.ReadToEnd()
        reader.Close()
        response.Close()

        res = res.Replace("\", "")
        res = res.Replace("""{", "{")
        res = res.Replace("}""", "}")

        Return res
    End Function


    Public Function SendGETRequest(ByVal uri As Uri, ByVal contentType As String, ByVal method As String) As String
        Dim req As WebRequest = WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method

        Dim response = req.GetResponse().GetResponseStream()

        Dim reader As New StreamReader(response)
        Dim res = reader.ReadToEnd()
        reader.Close()
        response.Close()

        res = res.Replace("\", "")
        res = res.Replace("""{", "{")
        res = res.Replace("}""", "}")

        Return res
    End Function


End Class
