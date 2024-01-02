<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="accessLevel.aspx.vb" Inherits="NewCRM.accessLevel" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        /** **/
        function OnBeforeEdit(record) {    

            var obj = {};
            obj.id = record.id;
            $.ajax({
                type: "POST",
                url: "accessLevel.aspx/getAccessDetail",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var objJSON = JSON.parse(r.d);
                    loadForm(objJSON);
                    
                },
                error: function (r) {
                    alert(JSON.stringify(r));
                }
            });

            return false;
        }

        function OnSuccess(data) {  
            var obj = data;
            alert(JSON.stringify(obj));
        }

        function OnBeforeAdd(record) { 
            $('.modal-title').html("Access Level | <small>Add New</small>");
            $('#accessLevelModal').modal('toggle');
            ResetForm();
            return false;
        }

        function ResetForm(){
                 $("#<%= txtLevelCode.ClientID %>").removeAttr('readonly');
                $('input:checkbox').removeAttr('checked');
		        $("#<%= hidLevelCode.ClientID %>").val(0);
                $("#<%= txtLevelCode.ClientID %>").val("");
                $("#<%= txtLevelName.ClientID %>").val("");
                $("#<%= txtLevelDescription.ClientID %>").val("");
                $("#<%= txtLimitFrom.ClientID %>").val(0);
                $("#<%= txtLimitTo.ClientID %>").val(0);
		        $("#<%= chkStatus.ClientID %>").prop("checked",false);
        
        }
        /** **/
    </script>

    <script type="text/javascript">	
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
        }
       
        function fCheckChild(pValue, pArr) {
            for (var i = 0; i < pArr.length; i++) {
                lObj = eval("document.getElementById(" + "'" + pArr[i] + "')")
                lObj.checked = pValue
                if (lObj.title) fCheckChild(pValue, lObj.title.split(","))
            }
        }

        function fCheckParent(pValue, pParent) {
            lObj = eval("document.getElementById(" + "'" + pParent + "')")
           
            if (pValue) {
                lObj.checked = pValue
                if (lObj.lang) fCheckParent(pValue, lObj.lang)
            } else {
                if (lObj.title) {
                    lAllChildUnchecked = true
                    lArr = lObj.title.split(",")
                    for (var i = 0; i < lArr.length; i++) {
                        lObjChild = eval("document.getElementById('" + lArr[i] + "')")
                        if (lObjChild.checked) lAllChildUnchecked = false
                    }
                    if (lAllChildUnchecked) {
                        lObj.checked = pValue
                        if (lObj.lang) fCheckParent(pValue, lObj.lang)
                    }
                }
            }
        }

        

        function fCheck(pObjMenu) {
            
            if (pObjMenu.title) {
                fCheckChild(pObjMenu.checked, pObjMenu.title.split(","));
            }
            if (pObjMenu.lang) {
                fCheckParent(pObjMenu.checked, pObjMenu.lang);
               
            }
        }
        function fSelectAll() {
            document.Form1.chkAdmin.checked = true
            fCheck(document.Form1.chkAdmin)
            document.Form1.chkParameter.checked = true
            fCheck(document.Form1.chkParameter)
            document.Form1.chkActivity.checked = true
            fCheck(document.Form1.chkActivity)
        }
        function fUnSelectAll() {
            document.Form1.chkAdmin.checked = false
            fCheck(document.Form1.chkAdmin)
            document.Form1.chkParameter.checked = false
            fCheck(document.Form1.chkParameter)
            document.Form1.chkActivity.checked = false
            fCheck(document.Form1.chkActivity)
        }
        
        fLoad();
        	
		function fLoad(){
		    <asp:Literal id="liBodyLoadAction" runat="server" />
		    }

		function loadForm(objJSON){

		    var JsonData    = objJSON["dt1"][0];
		    var JsonData2   = objJSON["dt2"];
		    $(document).ready(function(){

		        $('input:checkbox').removeAttr('checked');
		        $("#<%= txtLevelCode.ClientID %>").attr('readonly', 'readonly');
		        $("#<%= hidLevelCode.ClientID %>").val(JsonData.id);
		        $("#<%= txtLevelCode.ClientID %>").val(JsonData.lu_level_code);
		        $("#<%= txtLevelName.ClientID %>").val(JsonData.lu_level_name);
		        $("#<%= txtLevelDescription.ClientID %>").val(JsonData.lu_level_description);
		        $("#<%= txtLimitFrom.ClientID %>").val(JsonData.lu_limit_from);
		        $("#<%= txtLimitTo.ClientID %>").val(JsonData.lu_limit_to);
		        $("#<%= chkStatus.ClientID %>").prop("checked",JsonData.lu_status);

		        for(var i=0; i<JsonData2.length; i++){
		            var TagHtml = JSON.stringify(JsonData2[i].ld_lm_description);
		            TagHtml = "#"+TagHtml.replace(/\"/g, "");
		            if(checkExists(String(TagHtml))){
		                $(String(TagHtml)).prop("checked",true);		            
		            }
		        }

		        $('.modal-title').html("Access Level | <small>Edit</small>");
                $('#accessLevelModal').modal({
		            show: 'true'
		        });   
		    });
		}
        function checkExists(sel) {
            var status = false;
            if ($(sel).length) status = true;
            return status;
        }

        function checkVal(val){
        }

		</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="x_panel">
        <div class="x_title">     
            <h2>Access Level<small>&nbsp;</small></h2>              
            
            <div class="clearfix"></div>            
        </div>
            <div class="x_content">                
                <obout:Grid id="grdAccessLevel" runat="server" CallbackMode="true" Serialize="true" 
                AutoGenerateColumns="false" Width="100%" AllowPageSizeSelection="false" 
                AllowFiltering="false" OnRebind="ReBinGrid" FolderStyle="~\Content\Themes1\Grid">
                <ClientSideEvents OnBeforeClientAdd="OnBeforeAdd" OnBeforeClientEdit="OnBeforeEdit" />
                <Columns>
                <obout:Column ID="Column4" DataField="id" Visible="false" runat="server" ReadOnly="true" />
                <obout:Column ID="Column2" DataField="lu_level_name" HeaderText="Name" runat="server" ShowFilterCriterias="false" Width="20%">
                <TemplateSettings RowEditTemplateControlId="txtLevelname" RowEditTemplateControlPropertyName="value" FilterTemplateId="LevelnameFilter" />
                </obout:Column>
                <obout:Column ID="Column3" DataField="lu_level_description" HeaderText="Description" runat="server" ShowFilterCriterias="false" Width="55%">
                <TemplateSettings RowEditTemplateControlId="txtLeveldesc" RowEditTemplateControlPropertyName="value" FilterTemplateId="LevelnameFilter" />
                </obout:Column>
                <obout:Column ID="Column6" DataField="lu_status_format" HeaderText="Status" runat="server" ShowFilterCriterias="false" Width="10%"></obout:Column>
                <obout:Column ID="Column1" HeaderText="Action" AllowEdit="true"  runat="server" Width="7%" >
<%--                     <TemplateSettings TemplateID = "editTemplate" />--%>
                </obout:Column>
                <obout:Column ID="Column9" HeaderText="" Width="8%" runat="server">
                <TemplateSettings TemplateID = "deleteTemplate" />
                </obout:Column>
                </Columns>
			
                <Templates>
                <obout:GridTemplate runat="server" ID="deleteTemplate" >
                    <Template>
                  
                    <asp:LinkButton ID="zzz" runat="server" OnCommand="delete" OnClientClick="return confirm('Are you certain you want to delete?');" CommandArgument='<%# Container.PageRecordIndex %>'  class='ob_gAL' >  Delete  </asp:LinkButton>
                    
                    </Template>
                </obout:GridTemplate>
                 <obout:GridTemplate runat="server" ID="editTemplate" >
                    <Template>
                        <a href="#" onclick="clickEdit(<%# Container.PageRecordIndex %>)" class='ob_gAL'>Edit</a>
                    </Template>
                </obout:GridTemplate>                     
                </Templates>
                </obout:Grid>
            </div>
        </div>
    </div>
</div>




<div class="modal fade" id="accessLevelModal" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="accessLevelModalLabel">
    <div class="modal-dialog modal-lg" role="document">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Access Level | <small>Add New</small></h4>
                </div>

                <div class="modal-body"> 
                    <asp:Literal id="litModal" runat="server" />
                    <input type="hidden" id="hdId" name="hdId" runat="server">
                    <asp:HiddenField ID="hidLevelCode" runat="server" />
                    <table id="tbl1" class="table table-striped table-bordered" style="text-align:left;" cellSpacing="1" cellPadding="0" width="98%">
                        <tr>
					        <td style="WIDTH: 148px"><b>User Role Code</b></td>
					        <td><asp:textbox id="txtLevelCode" name="txtLevelCode" runat="server" MaxLength="50" Width="350" CssClass="form-control"></asp:textbox></td>
				        </tr>
				        <tr>
					        <td style="WIDTH: 148px"><b>User Role Name</b></td>
					        <td><asp:textbox id="txtLevelName" name="txtLevelName" runat="server" MaxLength="50" Width="350" CssClass="form-control"></asp:textbox></td>
				        </tr>
				        <tr>
					        <td style="WIDTH: 148px"><b>Description</b></td>
					        <td><asp:textbox id="txtLevelDescription" name="txtLevelDescription" runat="server" MaxLength="50" Width="350" CssClass="form-control"></asp:textbox></td>
				        </tr>
                        <tr>
                            <td><b>Limit</b></td>
                            <td>
                                <span style="margin: 0 5px;"><b>Above</b></span>
                                <asp:TextBox ID="txtLimitFrom" runat="server" CssClass="form-control" style="float:none;" value="0" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>
                                <span style="margin: 0 5px;"><b>To</b></span>
                                <asp:TextBox ID="txtLimitTo" runat="server" CssClass="form-control" style="float:none;" value="0" onkeypress="return isNumberKey(event);" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox>
                            </td>
                        </tr>
				        <tr  id="trStat" runat="server">
					        <td style="WIDTH: 148px"><b>Activated</b></td>
					        <td><input type="checkbox"  id="chkStatus" name="chkStatus" runat="server" onclick="checkVal(this)"/></td>
				        </tr>
                    </table>

                    <div class="panel panel-default">
                       <div class="panel-body">
                        <table class="table table-responsive" cellSpacing="0" cellPadding="0" border="0" width="98%" style="text-align:left;border-top:#fff solid;">
	                        <tr>
                                <%
                                    Dim htm As String = ""
                                    Dim lang As String = ""
                                    Dim check As String = ""

                                    Try
                                        If dtParent.Rows.Count > 0 Then
                                            For i As Integer = 0 To dtParent.Rows.Count() - 1

                                                Try
                                                    getChild(dtParent.Rows(i).Item("id"))

                                                    Try
                                                        getLang(dtParent.Rows(i).Item("id"))

                                                        If dtlang.Rows.Count > 0 Then
                                                            lang = dtlang.Rows(0).Item("lang")
                                                        Else
                                                            lang = ""
                                                        End If
                                                    Catch ex As Exception
                                                        dtlang.Reset()
                                                    End Try

                                                Catch ex As Exception
                                                    dtChild.Reset()
                                                End Try

                                                htm += "<td valign='top' width='20%' style='padding:0'>"
                                                htm += "<fieldset style='background:white; border-color:#f6f7f7; min-height:400px; height:100%'>"

                                                htm += "<table cellSpacing='0' cellPadding='0' width='100%' border='0'>"
                                                htm += "<tr>"
                                                htm += "<td>"

                                                htm += "<table cellSpacing='0' cellPadding='0' border='0' style='font-size: 13px;'>"
                                                check = ""
                                                Try
                                                    For l As Integer = 0 To dtLvlDet.Rows.Count() - 1
                                                        If dtParent.Rows(i).Item("lm_description") = dtLvlDet.Rows(l).Item("ld_lm_description") Then
                                                            check = "checked=checked"
                                                        End If
                                                    Next
                                                Catch ex As Exception

                                                End Try

                                                htm += "<tr>"
                                                htm += "<td width='20'><input type='checkbox' runat='server' name='" & dtParent.Rows(i).Item("lm_description") & "' id='" & dtParent.Rows(i).Item("lm_description") & "' title='" & lang & "' onClick='fCheck(this)' " & check & " /></td>"
                                                htm += "<td colSpan='4'>" & dtParent.Rows(i).Item("lm_name") & "&nbsp;&nbsp;&nbsp;</td>"
                                                htm += "</tr>"

                                                lang = ""


                                                For z As Integer = 0 To dtChild.Rows.Count() - 1

                                                    Try
                                                        getSubChild(dtChild.Rows(z).Item("id"))

                                                        Try
                                                            getLang(dtChild.Rows(z).Item("id"))

                                                            If dtlang.Rows.Count > 0 Then
                                                                lang = dtlang.Rows(0).Item("lang")
                                                            Else
                                                                lang = ""
                                                            End If
                                                        Catch ex As Exception
                                                            dtlang.Reset()
                                                        End Try
                                                    Catch ex As Exception
                                                        dtSubChild.Reset()
                                                    End Try


                                                    check = ""
                                                    Try
                                                        For l2 As Integer = 0 To dtLvlDet.Rows.Count() - 1
                                                            If dtChild.Rows(z).Item("lm_description") = dtLvlDet.Rows(l2).Item("ld_lm_description") Then
                                                                check = "checked=checked"
                                                            End If
                                                        Next
                                                    Catch ex As Exception

                                                    End Try

                                                    htm += "<tr>"
                                                    htm += "<td width='20'></td>"
                                                    htm += "<td width='20'><input type='checkbox' runat='server' name='" & dtChild.Rows(z).Item("lm_description") & "' id='" & dtChild.Rows(z).Item("lm_description") & "' lang='" & dtParent.Rows(i).Item("lm_description") & "' title='" & lang & "' onClick='fCheck(this)' " & check & " /></td>"
                                                    htm += "<td colSpan='3'>" & dtChild.Rows(z).Item("lm_name") & "&nbsp;&nbsp;</td>"
                                                    htm += "</tr>"


                                                    lang = ""

                                                    For l As Integer = 0 To dtSubChild.Rows.Count() - 1

                                                        Try
                                                            getSubSubChild(dtSubChild.Rows(l).Item("id"))
                                                            Try
                                                                getLang(dtSubChild.Rows(l).Item("id"))

                                                                If dtlang.Rows.Count > 0 Then
                                                                    lang = dtlang.Rows(0).Item("lang")
                                                                Else
                                                                    lang = ""
                                                                End If
                                                            Catch ex As Exception
                                                                dtlang.Reset()
                                                            End Try
                                                        Catch ex As Exception
                                                            dtSubSubChild.Reset()
                                                        End Try

                                                        check = ""
                                                        Try
                                                            For l3 As Integer = 0 To dtLvlDet.Rows.Count() - 1
                                                                If dtSubChild.Rows(l).Item("lm_description") = dtLvlDet.Rows(l3).Item("ld_lm_description") Then
                                                                    check = "checked=checked"
                                                                End If
                                                            Next
                                                        Catch ex As Exception

                                                        End Try

                                                        htm += "<tr>"
                                                        htm += "<td width='20'></td>"
                                                        htm += "<td width='20'></td>"
                                                        htm += "<td width='20'><input type='checkbox' runat='server' name='" & dtSubChild.Rows(l).Item("lm_description") & "' id='" & dtSubChild.Rows(l).Item("lm_description") & "' lang='" & dtChild.Rows(z).Item("lm_description") & "' title='" & lang & "' onClick='fCheck(this)' " & check & " /></td>"
                                                        htm += "<td colspan='2'>" & dtSubChild.Rows(l).Item("lm_name") & "</td>"
                                                        htm += "</tr>"

                                                        lang = ""

                                                        For u As Integer = 0 To dtSubSubChild.Rows.Count() - 1
                                                            htm += "<tr>"
                                                            htm += "<td></td>"
                                                            htm += "<td></td>"
                                                            htm += "<td></td>"
                                                            check = ""
                                                            Try
                                                                For l4 As Integer = 0 To dtLvlDet.Rows.Count() - 1
                                                                    If dtSubSubChild.Rows(u).Item("lm_description") = dtLvlDet.Rows(l4).Item("ld_lm_description") Then
                                                                        check = "checked=checked"
                                                                    End If
                                                                Next
                                                            Catch ex As Exception

                                                            End Try
                                                            htm += "<td width='20'><input type='checkbox' runat='server' name='" & dtSubSubChild.Rows(u).Item("lm_description") & "' id='" & dtSubSubChild.Rows(u).Item("lm_description") & "' lang='" & dtSubChild.Rows(l).Item("lm_description") & "' onClick='fCheck(this)' " & check & " /></td>"
                                                            htm += "<td class='style2'>" & dtSubSubChild.Rows(u).Item("lm_name") & "</td>"
                                                            htm += "</tr>"
                                                        Next

                                                    Next

                                                Next
                                                htm += "</table>"
                                                htm += "</td>"
                                                htm += "</tr>"
                                                htm += "</table>"
                                                htm += "</fieldset>"
                                                htm += "</td>"
                                            Next
                                        End If
                                        htm += "</tr>"
                                    Catch ex As Exception
                                        Response.Write(ex.Message.ToString())
                                    End Try
                                 %>
                                <%=htm %>
                            </tr>
                        </table>  
                        </div>
                    </div>       
                </div>



             <div class="modal-footer">
                <button type="submit" class="btn btn-default" data-dismiss="modal" aria-label="Close" style="margin-bottom:0px">Cancel</button>
                <asp:button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>
             </div>

          </div>
    </div>
</div>
</asp:Content>
