﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BorrowerRating.aspx.vb" Inherits="NewCRM.BorrowerRating" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        aBc("General");
        aMenu("Analysis");
        function fAction(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        fAction();
        function fDet(id){
            var brw = document.getElementById("<%= brw_code.clientID %>").value;
            var l_obj_argument = new Object();
            l_obj_argument.url = "ProfitLossDet.aspx?id=" + id + "&brwcode=" + brw;
            oWindow(l_obj_argument.url, 1000, 500);
            return false;
        }
        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
        }
        $(function() {
            $("#<%=txtRatingDate.clientID %>").datepicker(
            { duration: '',
                yearRange: "-0:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtRatingDate.clientID %>").datepicker("setDate", new Date());
            $("#<%=txtNextRating.clientID %>").datepicker(
            { duration: '',
                yearRange: "-0:+5",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                minDate : new Date
            });
        });
        </script>
    <style type="text/css">
        .notif
        {
            margin-bottom:10px;
            border: 1px solid #bcbcbc; 
            border-radius:5px;
        }
        .notif-header
        {
            border-top-right-radius: 5px;
            border-top-left-radius: 5px;
            font-size: 12px;
            background: #007cc2;
            color: #fff;
        }
        .notif-body
        {
            border-bottom-right-radius: 5px;
            border-bottom-left-radius: 5px;
            background: #ebecec;
            padding: 5px;
            font-size: 12px;
        }
        a.notif-close {
            font-size: 10px;
            text-decoration: none;
            line-height: 1;
            color:#fff;
            cursor:pointer;
            font-weight:bold;
            width: 1%;
        }
        .aactive
        {
            background-color:transparent;

            border-radius:11px;
            -moz-border-radius:11px;
            -webkit-border-radius:11px;
            color: #fff;
	
	        background-color:#ebebeb;
	        border:thin solid #cccccc;	
        }
        td
        {
            padding-left:5px;
        }
        .tbl
        {
            font-family:"Century Gothic", "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            font-size:11px;
        }
        .spanChild
        {
            margin-left:10px;
        }
        #ui-datepicker-div {display: none;}
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true"></asp:ScriptManager>
    <input type="hidden" id="appno" runat="server" />
    <input type="hidden" id="brw_code" runat="server" />
    <input type="hidden" id="score" runat="server" />
    <input type="hidden" id="rating" runat="server" />
    <input type="hidden" id="scc" runat="server" />
    <div style="clear:both; margin:0 auto; padding:0; width:100%">
        <div id='cssmenu' style="margin-bottom:20px;">
            <ul style="float:left;margin-right:10px;">
                <%  Dim html As String = ""
                    If dataTable.Rows.Count > 0 Then
                        For i As Integer = 0 To dataTable.Rows.Count - 1
                            If Session("appno") IsNot Nothing Then
                                If dtWorkflow.Rows.Count > 0 Then
                                    For j As Integer = 0 To dtWorkflow.Rows.Count - 1
                                        If dataTable.Rows(i).Item("lsm_field") = dtWorkflow.Rows(j).Item("lwr_field") Then
                                               If dtWorkflow.Rows(j).Item("lwr_role") <> "HD" Then
                                                   If dataTable.Rows(i).Item("lsm_parent") = 0 Then
                                                       If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                                           html &= "<li><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                       Else
                                                           html &= "<li><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                       End If
                                                    
                                                   Else
                                                       If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                                           html &= "<li id='c" & dataTable.Rows(i).Item("lsm_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                       Else
                                                       html &= "<li id='c" & dataTable.Rows(i).Item("lsm_position") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                                       End If
                                                   End If
                                               Else
                                                   If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                                       If appno.Value <> "" Then
                                                           Response.Redirect(menuNext & "?appno=" & appno.Value)
                                                       Else
                                                           Response.Redirect(menuNext)
                                                       End If
                                               End If
                                           End If
                                       End If
                                   Next
                                    If dataTable.Rows(i).Item("lsm_field") = "parent" Then
                                       html &= checkChildHD(dataTable.Rows(i).Item("lsm_position"), i)
                                        
                                       html &= getMenuChild(dataTable.Rows(i).Item("lsm_position"))
                                    End If
                                Else
                                    If dataTable.Rows(i).Item("lsm_parent") = 0 Then
                                        If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                            html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        Else
                                            html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        End If
                                        
                                        If dataTable.Rows(i).Item("lsm_field") = "parent" Then
                                            html &= getMenuChild(dataTable.Rows(i).Item("lsm_id"))
                                        End If
                                    Else
                                        If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                            html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        Else
                                            html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                        End If
                                    End If
                                End If
                            Else
                                If dataTable.Rows(i).Item("lsm_parent") = 0 Then
                                    If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                        html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    Else
                                        html &= "<li id='m" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span>&#9632;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    End If
                                        
                                    If dataTable.Rows(i).Item("lsm_field") = "parent" Then
                                        html &= getMenuChild(dataTable.Rows(i).Item("lsm_id"))
                                    End If
                                Else
                                    If dataTable.Rows(i).Item("lsm_name") = pagename Then
                                        html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "') class='aactive'><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    Else
                                        html &= "<li id='c" & dataTable.Rows(i).Item("lsm_id") & "'><a href=javascript:fDetail('" & dataTable.Rows(i).Item("lsm_link") & "')><span class='spanChild'>&bull;&nbsp;&nbsp;" & dataTable.Rows(i).Item("lsm_name") & "</span></a></li>"
                                    End If
                                End If
                            End If
                        Next
                    End If
                %>
                <%=html %>
            </ul>
            <div class="bodyContent">
                <div class="cont-header" stlye="width:100%;">
                    <table width="100%" border="0" class="table table-striped table-bordered">
                        <tr>
                            <td width="20%"><span><b>No. Aplikasi</b></span></td>
                            <td><asp:label ID="lblAppNo_header" runat="server"></asp:label></td>
                        </tr>
                        <tr>
                            <td><span><b>Nama Pemohon</b></span></td>
                            <td><asp:label ID="lblBrwName_header" runat="server"></asp:label></td>
                        </tr>
                    </table>
                </div>
                <div id="divnotif" runat="server" class="notif" style="display:none;">
                    <div class="notif-header" width="100%">
                        <table width="100%" class="table table-striped table-bordered">
                            <tr>
                                <td><span><b>Notifikasi</b></span></td>
                                <td align="right"><a class="notif-close" onclick="document.getElementById('<%=divnotif.clientID %>').style.display='none'" title="Close">X</a></td>
                            </tr>
                        </table>
                    </div>
                    <div class="notif-body">
                        <asp:Label ID="lblNotif" runat="server" style="color:Red;"></asp:Label>
                    </div>
                </div>
                <table class="table table-striped table-bordered" cellspacing="0" cellpadding="2" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;">
                    <tr>
                        <td align="left" colspan="2" bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="30%"><b>Tgl. Rating Terakhir</b></td>
                        <td><asp:label ID="lblLastUpdate" runat="server"></asp:label><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Skor</b></td>
                        <td><b><asp:Label ID="lblScore" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Rating</b></td>
                        <td><b><asp:Label ID="lblRating" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Deskripsi</b></td>
                        <td><b><asp:Label ID="lblDeskripsi" Width="90%" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Tgl. Rating</b></td>
                        <td><asp:TextBox ID="txtRatingDate" runat="server" CssClass="inpTxtDisabled form-control" Width="250px" ReadOnly="true" style="float:none;"></asp:TextBox><span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec" style="display:none;">
                        <td><b>Tgl. Rating Selanjutnya</b></td>
                        <td><asp:TextBox ID="txtNextRating" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox><span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr style="height:30px">
				        <td colspan="4" align="left" background="../../styles/grid/footer.gif" style="border-radius: 0 0 5px 5px;">
                            <asp:Button ID="btnRating" runat="server" CssClass="btn btn-primary" Text="Rating" />
                            <asp:button id="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="btn btn-primary"></asp:button>
                            <asp:button id="btnNext" runat="server" Text="Lanjut" CssClass="btn btn-danger"></asp:button>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                        </td>
                    </tr>
                </table>
                <fieldset style="margin-top:10px;border-radius:5px;background:#ebecec;">
                    <legend><b>Deskripsi Rating</b></legend>
                    <obout:Grid id="grid2" runat="server" CallbackMode="true" Serialize="true" AllowSorting="false" AllowPaging="false"
			                AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowAddingRecords="false" AllowFiltering="false"
                            AllowPageSizeSelection="false" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                        <Columns>
                            <obout:Column id="Column8" runat="server" DataField="base_scorecard" Visible="false" />
                            <obout:Column id="Column9" runat="server" DataField="lblScore" HeaderText="Score" Width="10%" />
                            <obout:Column id="Column10" runat="server" DataField="base_rate" HeaderText="Rating" Width="10%" />
                            <obout:Column id="Column12" runat="server" DataField="base_cat" HeaderText="Kategori" Width="20%" />
                            <obout:Column id="Column13" runat="server" DataField="base_desc" HeaderText="Deskripsi" Width="50%" Wrap="true" />
                        </Columns>
                    </obout:Grid>
                </fieldset>
                <fieldset style="margin-top:10px;border-radius:5px;background:#ebecec;">
                    <legend><b>Rating History</b></legend>
                    <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" AllowSorting="false"
			                AutoGenerateColumns="false" EnableRecordHover="true" Width="100%" AllowAddingRecords="false" AllowFiltering="false"
                            AllowPageSizeSelection="false" AllowRecordSelection="false" FolderStyle="~/styles/grid">
                        <Columns>
                            <obout:Column id="Column1" runat="server" DataField="lbr_id" Visible="false" />
                            <obout:Column id="Column2" runat="server" DataField="lbr_brw_code" HeaderText="Kode Borrower" Width="20%" />
                            <obout:Column id="Column3" runat="server" DataField="lbr_score" HeaderText="Skor" Width="15%" />
                            <obout:Column id="Column4" runat="server" DataField="lbr_rating" HeaderText="Rating" Width="15%" />
                            <obout:Column id="Column5" runat="server" DataField="lbr_ratingDate" HeaderText="Tgl. Rating (dd-mm-yyyy)" Width="22%" />
                            <%--<obout:Column id="Column6" runat="server" DataField="lbr_nextRating" HeaderText="Tgl. Rating Selanjutnya (dd-mm-yyyy)" Width="28%" />--%>
                            <obout:Column id="Column7" runat="server" DataField="lbr_usr" HeaderText="User" Width="20%" />
                        </Columns>
                   </obout:Grid>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>