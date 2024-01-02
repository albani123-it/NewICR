Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters



Class Requests
    <JsonProperty("status")>
    Public Property status() As String

    <JsonProperty("message")>
    Public Property message() As String

    <JsonProperty("data")>
    Public Property data() As List(Of Data)

    <JsonProperty("info")>
    Public Property info() As List(Of info)

    <JsonProperty("validate")>
    Public Property validate() As validate

    <JsonProperty("form")>
    Public Property form() As List(Of data_form)
End Class

Class RequestsField
    <JsonProperty("status")>
    Public Property status() As String

    <JsonProperty("message")>
    Public Property message() As String

    <JsonProperty("info")>
    Public Property info() As List(Of info)

    <JsonProperty("form")>
    Public Property form() As List(Of data_form)

End Class

Class DataCust
    <JsonProperty("status")>
    Public Property status() As String

    <JsonProperty("message")>
    Public Property message() As String

    <JsonProperty("data")>
    Public Property data() As List(Of data_cust)
End Class

Class RequestRehit
    <JsonProperty("status")>
    Public Property status() As String

    <JsonProperty("message")>
    Public Property message() As String

    <JsonProperty("info")>
    Public Property info() As List(Of info)

    <JsonProperty("validate")>
    Public Property validate() As validate

    <JsonProperty("form")>
    Public Property form() As List(Of data_form)
End Class

Class validate
    <JsonProperty("param")>
    Public Property param() As List(Of data_form)

    <JsonProperty("prompt")>
    Public Property prompt() As String
End Class
Class Data
    <JsonProperty("wpm_id")>
    Public Property wpm_id() As Integer
    <JsonProperty("wpm_name")>
    Public Property wpm_name() As String
    <JsonProperty("wpm_desc")>
    Public Property wpm_desc() As String
    <JsonProperty("wpm_type")>
    Public Property wpm_type() As String

    <JsonProperty("wac_id")>
    Public Property wac_id() As Integer
    <JsonProperty("wac_wpm_id")>
    Public Property wac_wpm_id() As Integer

    <JsonProperty("wac_start")>
    Public Property wac_start() As String

    <JsonProperty("wac_finish")>
    Public Property wac_finish() As String

    <JsonProperty("wac_user")>
    Public Property wac_user() As String
    <JsonProperty("wac_acc_no")>
    Public Property wac_acc_no() As String

    <JsonProperty("wad_id")>
    Public Property wad_id() As Integer
    <JsonProperty("wad_wac_id")>
    Public Property wad_wac_id() As Integer
    <JsonProperty("wad_process_id")>
    Public Property wad_process_id() As Integer
    <JsonProperty("wad_process_name")>
    Public Property wad_process_name() As String
    <JsonProperty("wad_activity_date")>
    Public Property wad_activity_date() As String
    <JsonProperty("wad_user")>
    Public Property wad_user() As String

    'Form Wizard
    <JsonProperty("activity")>
    Public Property activity() As String

    <JsonProperty("sla")>
    Public Property sla() As String

    <JsonProperty("type")>
    Public Property type() As String

    <JsonProperty("no")>
    Public Property no() As String

End Class

Class info
    <JsonProperty("wad_wac_id")>
    Public Property wad_wac_id() As Integer

    <JsonProperty("wad_process_id")>
    Public Property wad_process_id() As Integer

    <JsonProperty("wad_process_name")>
    Public Property wad_process_name() As String

    <JsonProperty("wcp_type")>
    Public Property wcp_type() As String
End Class

Class data_form
    <JsonProperty("wpf_id")>
    Public Property wpf_id() As Integer

    <JsonProperty("wps_id")>
    Public Property wps_id() As Integer

    <JsonProperty("wpf_name")>
    Public Property wpf_name() As String

    <JsonProperty("wpf_hint")>
    Public Property wpf_hint() As String

    <JsonProperty("wpf_type")>
    Public Property wpf_type() As String

    <JsonProperty("wpf_field")>
    Public Property wpf_field() As String

    <JsonProperty("wpf_element")>
    Public Property wpf_element() As String

    <JsonProperty("wpf_option")>
    Public Property wpf_option() As String

    <JsonProperty("wpf_order")>
    Public Property wpf_order() As Integer

    <JsonProperty("wpm_id")>
    Public Property wpm_id() As Integer

    <JsonProperty("param")>
    Public Property param() As String

    <JsonProperty("prompt")>
    Public Property prompt() As String
End Class

Class data_cust
    <JsonProperty("cst_cif")>
    Public Property cst_cif() As String

    <JsonProperty("cst_fname")>
    Public Property cst_fname() As String

    <JsonProperty("cst_lname")>
    Public Property cst_lname() As String

    <JsonProperty("cst_dob")>
    Public Property cst_dob() As String

    <JsonProperty("cst_sex")>
    Public Property cst_sex() As String

    <JsonProperty("cst_marital_status")>
    Public Property cst_marital_status() As String

    <JsonProperty("cst_address")>
    Public Property cst_address() As String

    <JsonProperty("cst_phone")>
    Public Property cst_phone() As String

    <JsonProperty("cst_postal_cd")>
    Public Property cst_postal_cd() As String

    <JsonProperty("cst_status")>
    Public Property cst_status() As String
End Class

