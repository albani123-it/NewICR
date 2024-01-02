<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dataMgmtDet.aspx.vb" Inherits="NewCRM.dataMgmtDet" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Skyworx - Internal Credit Rating</title>
    <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/jquery.fancybox.min.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/organizationChart.css" rel="stylesheet" />
    <link href="~/Content/Themes1/build/css/Treant.css" rel="stylesheet" />
    <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
   
    <style type="text/css">
        td
        {
        	padding-left: 5px;
        }
        
        input
        {
        	font-family: "Century Gothic";
        }
        .inpTxtCond
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 1px 4px;
            /*background: #f5f5f5;*/
            color: #2b4c61;
            font-family: century gothic;
            font-size: 11px;
            float:left;       	
        }

        .inpTxtCondField
        {
            box-shadow: 1px 2px 1px rgba(201, 150, 26, 0.62);
            border-radius: 5px;
            border: thin solid #aaaaaa;
            padding: 1px 4px;
            /*background: #f5f5f5;*/
            color: #2b4c61;
            font-family: century gothic;
            font-size: 11px;
            float:left;       	
        }
    </style>
    <script type="text/javascript">
        function CloseMe() {
            window.returnValue = window.dialogArguments;
            event.returnValue = false;
            window.close();
        }
        function fLoad(){
			<asp:Literal id="liBodyLoadAction" runat="server"></asp:Literal>
		}
        function popupCallback(data,destination,mode)
        {
            var words = document.getElementById(destination).value;
            var last = words.substring(words.length-1,words.length);

            if(mode=="operator"){
                document.getElementById(destination).value += " " + data + " ";
            }
            else{
                if (words == "" || last == ","){
                    document.getElementById(destination).value += data;
                }
                else{
                    document.getElementById(destination).value += "," + data;
                }
            }
        }
        function fAutoFill(){
             var countRepeater = $('#tblDataSource >tbody >tr').length;
             var rpCount = (countRepeater - 4) / 3;
             var r = "";
            //alert((rpCount - 4) / 3);
            
//             if($("#Repeater1").find('tr:gt(0)').length > 0){
//                 //Do something
//                 alert(1);
//            }
//            else{
//                 alert("You don't have any item");
//            }
            
            var aliasStat = document.getElementById("txtAliasStat").value;
            
            var resultStatArr = document.getElementById("txtFieldStat").value;
            var resultStat = resultStatArr.split(",");
            
            for (i=0;i<resultStat.length;i++){
                r += aliasStat + ".";
                if (i == resultStat.length - 1){
                    if (resultStat[i].toString().substring(0,1) == " "){
                        r += resultStat[i].toString().substring(1,resultStat[i].length);
                    }
                    else{
                        r += resultStat[i];
                    }
                    
                }
                else{
                   if (resultStat[i].toString().substring(0,1) == " "){
                        r += resultStat[i].toString().substring(1,resultStat[i].length) + ", ";
                    }
                    else{
                        r += resultStat[i] + ", ";
                    }
                    //r += resultStat[i] + ", ";
                }
                
            }

            if (rpCount > 0){
                r += ", "
            }

            for (i=0;i<rpCount;i++){
                var alias = document.getElementById("Repeater1_txtAliasDyn_" + i).value;
                var field = document.getElementById("Repeater1_txtFieldDyn_" + i).value;
               
                var result = field.split(",");
                
                for(j=1;j<=result.length;j++){
                    r += alias + ".";
                    if (j == result.length){
                        if (result[j - 1].toString().substring(0,1) == " "){
                            r += result[j - 1].toString().substring(1,result[j - 1].length);
                        }
                        else{
                            r += result[j - 1];
                        }
                        
                    }
                    else{
                        if (result[j - 1].toString().substring(0,1) == " "){
                            r += result[j - 1].toString().substring(1,result[j - 1].length) + ", ";
                        }
                        else{
                            r += result[j - 1] + ", ";
                        }
                        //r += result[j - 1] + ", ";
                    }
                }
                
            }
            document.getElementById("txtFinalField").value = r;
        }
    </script>
</head>
<body onload="fLoad();">
    <form id="form1" runat="server">
       <div class="modal-body">
        <center>
            
                <table class="table table-striped table-bordered" width="100%">
                <tr>
                    
                <td colspan="2"  bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td width="30%"><b>Data Type</b></td>
                    <td><asp:DropDownList runat="server" ID="ddlDataType" Width="300px"  CssClass="form-control">
                        <asp:ListItem Value="Asset Category">Asset Category</asp:ListItem>
                        <asp:ListItem Value="Collateral Classification">Collateral Classification</asp:ListItem>
                    </asp:DropDownList></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Data Name</b></td>
                    <td>
                     <asp:TextBox runat="server" Width="300px"  ID="txtDataName" CssClass="form-control" Style="float: none;"></asp:TextBox><br />
                        <span>Character allowed only A-Z, a-z, 0-9,_</span>
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Description</b></td>
                    <td><asp:TextBox runat="server" ID="txtDataDesc" MaxLength="1000" Width="300px"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td colspan="2"><b>Data Source</b></td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td colspan="2">
                        <table style='font-size:10px;' cellSpacing='1' cellPadding='0' width='100%' id="tblDataSource">
                            <tr>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;" valign='top' width='2%' rowspan='3'>&nbsp;</td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-bottom:none;" valign='top' width='10%'><b>Data Source</b></td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-bottom:none;" valign='top' width='28%'>
                                    <asp:DropDownList ID="ddlDataSourceStat" runat="server" AutoPostBack="TRUE" CssClass="form-control" width="250px">
                                          <asp:ListItem Value="Select One">Select One</asp:ListItem>
                                          <%--<asp:ListItem   Value="dataupload">Data Upload</asp:ListItem>--%>
                                           <asp:ListItem Value="datamart">Datamart</asp:ListItem>
                                    </asp:DropDownList>
				                </td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-bottom:none;" valign='top' width='10%'><b>Table Name-Alias</b></td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-left:none;border-bottom:none;" valign='top' width='50%'>
                                     <asp:DropDownList ID="ddlTableNameStat" runat="server" AutoPostBack="TRUE" CssClass="form-control" Width="250px"
                                         AppendDataBoundItems="true">
						                    <asp:ListItem Selected="true" Value="Select One">Select One</asp:ListItem>
					                </asp:DropDownList> <br />

                                    <asp:TextBox runat="server" ID="txtAliasStat" Text='' CssClass="form-control" Width="25%"></asp:TextBox> <br />
				                </td>
                             
                            </tr>
                            <tr>
				                <td style="margin:1px;padding:1px" valign='top' width='10%'><b></b></td>
				                <td style="margin:1px;padding:1px" valign='top' width='28%'>
                                    &nbsp;
				                </td>
				                <td style="margin:1px;padding:1px" valign='top' width='10%'><b>Fields</b></td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-top:none;border-left:none;border-bottom:none;" valign='top' width='50%'>
					                <asp:TextBox runat="server" ID="txtFieldStat" Text='' Rows="3" TextMode="multiline" CssClass="form-control" Width="90%" Style="float: none;"></asp:TextBox><br />
                                     <span>Browse field (Alt + f)</span>			
				                </td>
			                </tr>
                            <tr>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-top:none;" valign='top' width='10%'></td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-top:none;" valign='top' width='28%'>
					                &nbsp;
				                </td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-top:none;" valign='top' width='10%'><b>Condition</b></td>
                                <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-left:none;border-top:none;" valign='top' width='50%'>
					                <asp:TextBox runat="server" ID="txtConditionStat" Text='' Rows="3" TextMode="multiline" CssClass="form-control" Width="90%" Style="float: none;"></asp:TextBox><br />
                                    <span>Browse field (Alt + f) and operator (Alt + o)</span>
				                </td>
			                </tr>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" ClientIDMode="Predictable">
			                    <ItemTemplate>
			                        <tr>			
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;" valign='top' width='2%' rowspan='3'><asp:ImageButton ID="btnRemove" runat="server" ImageUrl="../images/false.gif"  />&nbsp;</td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-bottom:none;" valign='top' width='10%'><b>Data Source</b></td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-bottom:none;" valign='top' width='28%'>
                                            
                                             <asp:DropDownList ID="ddlDataSourceDyn" runat="server" AutoPostBack="TRUE" CssClass="form-control" Style="height: 19px;" OnSelectedIndexChanged="ddlDataSourceDyn_SelectedIndexChanged">
                                                  <asp:ListItem   Value="Select One">Select One</asp:ListItem>
                                                    <%--<asp:ListItem Value="dataupload">Data Upload</asp:ListItem>--%>
                                                   <asp:ListItem Value="datamart">Datamart</asp:ListItem>
                                            </asp:DropDownList>
				                        </td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-bottom:none;" valign='top' width='10%'><b>Table Name-Alias</b></td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-left:none;border-bottom:none;" valign='top' width='50%'>
                                             <asp:DropDownList ID="ddlTableNameDyn" runat="server" AutoPostBack="TRUE" CssClass="form-control" Style="height: 19px; float: left;">
						                           <asp:ListItem Selected="True" Value="Select One">Select One</asp:ListItem>
					                        </asp:DropDownList>
					                        <asp:TextBox runat="server" ID="txtAliasDyn" Text='<%# Eval("TableAlias").ToString() %>' CssClass="inpTxt" Width="15%"></asp:TextBox>
				                        </td>
			                        </tr>
			                        <tr>
				                        <td style="margin:1px;padding:1px" valign='top' width='10%'><b>Merging Condition</b></td>
				                        <td style="margin:1px;padding:1px" valign='top' width='28%'>
                                             <asp:DropDownList ID="ddlMergeCondDyn" runat="server" AutoPostBack="false" CssClass="form-control" Style="height: 19px;">
						                        <asp:ListItem Value="Left Join" >Left Join</asp:ListItem>
						                        <asp:ListItem Value="Inner Join" >Inner Join</asp:ListItem>
					                        </asp:DropDownList>
				                        </td>
				                        <td style="margin:1px;padding:1px" valign='top' width='10%'><b>Fields</b></td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-top:none;border-left:none;border-bottom:none;" valign='top' width='50%'>
					                         
                                             <asp:TextBox runat="server" ID="txtFieldDyn" Text='<%# Eval("Fields").ToString() %>' Rows="3" TextMode="multiline" CssClass="form-control" Width="90%" Style="float: none;"></asp:TextBox><br />
                                             <span>Browse field (Alt + f)</span>
				                        </td>
			                        </tr>
			                        <tr>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-top:none;" valign='top' width='10%'><b>Merging Key</b></td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-top:none;" valign='top' width='28%'>
					                        
                                             <asp:TextBox runat="server" ID="txtMergeKeyDyn" Text='<%# Eval("MergingKey").ToString() %>' CssClass="form-control" Width="90%"></asp:TextBox>				
				                        </td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-right:none;border-left:none;border-top:none;" valign='top' width='10%'><b>Condition</b></td>
                                        <td style="border: thin dashed #ababab;padding: 1px;margin: 1px;border-left:none;border-top:none;" valign='top' width='50%'>
					                        
                                             <asp:TextBox runat="server" ID="txtConditionDyn" Text='<%# Eval("Condition").ToString() %>' Rows="3" TextMode="multiline" CssClass="form-control" Width="90%" Style="float: none;"></asp:TextBox><br />
                                            <span>Browse field (Alt + f) and operator (Alt + o)</span>
				                        </td>
			                        </tr>
			                    </ItemTemplate>
			                </asp:Repeater>
                            <tr>
				                <td colspan="5" style="padding-left:0;">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="btn btn-info" Style="float: left;" />
				                </td>
			                </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Final Field Selection</b></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFinalField" Width="98%" Height="50px" TextMode="MultiLine" CssClass="form-control" Style="float: none;"></asp:TextBox><br />
                        <input type="button" value="Autofill" id="btnAutoFill" onclick="fAutoFill();" class="btn btn-info" style="margin-top: 5px;" />
                    </td>
                </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Final Rule Condition</b></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFinalRule" Width="98%" Height="50px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><br />
                       <asp:Button ID="btnValidate" runat="server" Text="Validate Final Data Source" CssClass="btn btn-info" Style="margin-top: 5px;"></asp:button>
                    </td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>Activated</b></td>
                    <td><input type="checkbox" runat="server" id="chkStatus" name="chkStatus" /></td>
                </tr>
                <tr style="height:30px">
				    <td colspan="2" style="border-radius: 0 0 5px 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"></asp:button>&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" CssClass="btn btn-default" OnClientClick="CloseMe()"></asp:button>
                    </td>
			    </tr>
            </table>
        </center>
    </div>
    </form>
</body>
<script type="text/javascript">
    $(".inpTxtCondField").bind('keydown', 'alt+f', function () {
        var control = window.event.srcElement.id;
        var row = control.substring(control.lastIndexOf("_") + 1);
        var table = "Repeater1_ddlTableNameDyn_" + row
        var e = document.getElementById(table);
        table = e.options[e.selectedIndex].value;

        var left = (screen.width / 2) - (330 / 2);
        var top = (screen.height / 2) - (318 / 2);
        var url = "DataSourceTableFields.aspx?source=" + table + "&destination=" + control;
        window.open(url, "popupWindow", 'width=318,height=330,scrollbars=yes,top=' + top + ', left=' + left);
        //window.open(url, "popupWindow", 'width=1000,height=1000,scrollbars=yes,top=' + top + ', left=' + left);

    })


    $(".inpTxtCond").bind('keydown', 'alt+o', function () {
        var control = window.event.srcElement.id;
        var source = "operator"
        var left = (screen.width / 2) - (330 / 2);
        var top = (screen.height / 2) - (318 / 2);
        var url = "DataSourceTableFields.aspx?source=" + source + "&destination=" + control;
        window.open(url, "popupWindow", 'width=318,height=330,scrollbars=yes,top=' + top + ', left=' + left);
    })
             .bind('keydown', 'alt+f', function () {
                 var control = window.event.srcElement.id;
                 var row = control.substring(control.lastIndexOf("_") + 1);
                 var table = "Repeater1_ddlTableNameDyn_" + row;
                 var e = document.getElementById(table);
                 table = e.options[e.selectedIndex].value;

                 var left = (screen.width / 2) - (330 / 2);
                 var top = (screen.height / 2) - (318 / 2);
                 var url = "DataSourceTableFields.aspx?source=" + table + "&destination=" + control;
                 window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);
                 //window.open(url, "popupWindow", 'width=1000,height=1000,scrollbars=yes,top=' + top + ', left=' + left);

             })


    $("#<%= txtConditionStat.ClientID %>").bind('keydown', 'alt+o', function () {
        var control = window.event.srcElement.id;
        var source = "operator"
        var left = (screen.width / 2) - (330 / 2);
        var top = (screen.height / 2) - (318 / 2);
        var url = "DataSourceTableFields.aspx?source=" + source + "&destination=" + control;
        window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);
    })
             .bind('keydown', 'alt+f', function () {
                 var control = window.event.srcElement.id;
                 var table = $("#<%= ddlTableNameStat.ClientID %>").val();

                 var left = (screen.width / 2) - (330 / 2);
                 var top = (screen.height / 2) - (318 / 2);
                 var url = "DataSourceTableFields.aspx?source=" + table + "&destination=" + control;
                 window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);


             })


    $("#<%= txtFieldStat.ClientID %>").bind('keydown', 'alt+f', function () {
        var control = window.event.srcElement.id;
        var table = $("#<%= ddlTableNameStat.ClientID %>").val();

        var left = (screen.width / 2) - (330 / 2);
        var top = (screen.height / 2) - (318 / 2);
        var url = "DataSourceTableFields.aspx?source=" + table + "&destination=" + control;
        window.open(url, "popupWindow", 'width=335,height=330,scrollbars=yes,top=' + top + ', left=' + left);


    })
</script>
</html>
