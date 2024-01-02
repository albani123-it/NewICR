<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="logDet.aspx.vb" EnableEventValidation="false" Inherits="NewCRM.logDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Skyworx - Internal Credit Rating</title>
        <link href="~/Content/Themes1/build/css/custom.css" rel="stylesheet" />
        <link href="~/Content/Themes1/build/css/jquery-ui.css" rel="stylesheet" />
        <link href="~/Content/Themes1/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
        <script type="text/javascript">
            function CloseMe() {
                window.returnValue = window.dialogArguments;
                event.returnValue = false;
                window.close();
            }	
        </script>
        <style type="text/css">
		    td{
			    word-break: break-all;
			    vertical-align:top;
			    padding:5px;
            }        
            .style1
            {
                text-align: center;
            }
            </style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <center>
			
            <table class="table table-striped table-bordered">
				<tr>
                    <td bgcolor="#2A3F54" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
				</tr>
			</table>
			
            <table class="table table-striped table-bordered" width="98%">
				<tr bgColor="#f6f7f7">
                    <td width="50%" valign="top" style="padding:0;" >
                    <table runat="server" id="detAsset" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category Group</b></td>
                                <td><asp:Label ID="cat_typ" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cat_typ_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Parent Cateogry</b></td>
                                <td><asp:Label ID="cat_parent" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cat_parent_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category Name</b></td>
                                <td><asp:Label ID="cat_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cat_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Description</b></td>
                                <td><asp:Label ID="cat_desc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cat_desc_a" runat="server"></asp:Label></td>
				            </tr>
				            <tr bgColor="#f6f7f7">
					            <td><b>Data Source</b></td>
                                <td><asp:Label ID="data_source" runat="server"></asp:Label></td>
                                <td><asp:Label ID="data_source_a" runat="server"></asp:Label></td>
				            </tr>       
                        </table>
                        <table runat="server" id="detAssetmodel" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Model Name</b></td>
                                <td><asp:Label ID="mod_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Description</b></td>
                                <td><asp:Label ID="mod_desc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_desc_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Approach Type</b></td>
                                <td><asp:Label ID="mod_approach" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_approach_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Asset Class</b></td>
                                <td><asp:Label ID="cat_class" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cat_class_a" runat="server"></asp:Label></td>
				            </tr>
				            <tr bgColor="#f6f7f7">
					            <td><b>Probabilty of Default (PD)</b></td>
                                <td><asp:Label ID="mod_pd" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_pd_a" runat="server"></asp:Label></td>
				            </tr>   
                             <tr bgColor="#ebecec">
					            <td><b>Loss Given Default (LGD)</b></td>
                                <td><asp:Label ID="mod_lgd" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_lgd_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#f6f7f7">
					            <td><b>Exposure At Default (EAD)</b></td>
                                <td><asp:Label ID="mod_ead" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_ead_a" runat="server"></asp:Label></td>
				            </tr>  
                            <tr bgColor="#ebecec">
					            <td><b>Credit Conversion Factor (CCF)</b></td>
                                <td><asp:Label ID="mod_ead_value" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_ead_value_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#f6f7f7">
					            <td><b>Maturity</b></td>
                                <td><asp:Label ID="mod_maturity" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_maturity_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#ebecec">
					            <td><b>Activated</b></td>
                                <td><asp:Label ID="mod_status" runat="server"></asp:Label></td>
                                <td><asp:Label ID="mod_status_a" runat="server"></asp:Label></td>
				            </tr>            
                        </table>
                        <table runat="server" id="detBorrower_Mgmt" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Borrower Type</b></td>
                                <td><asp:Label ID="brw_type" runat="server"></asp:Label></td>
                                <td><asp:Label ID="brw_type_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Sector</b></td>
                                <td><asp:Label ID="brw_sec" runat="server"></asp:Label></td>
                                <td><asp:Label ID="brw_sec_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Borrower Rating Model</b></td>
                                <td><asp:Label ID="brw_scc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="brw_scc_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Cut Off (Score) Approved</b></td>
                                <td><asp:Label ID="brw_cut_approved" runat="server"></asp:Label></td>
                                <td><asp:Label ID="brw_cut_approved_a" runat="server"></asp:Label></td>
				            </tr>
				            <tr bgColor="#f6f7f7">
					            <td><b>Cut Off (Score) Rejected</b></td>
                                <td><asp:Label ID="brw_cut_rejected" runat="server"></asp:Label></td>
                                <td><asp:Label ID="brw_cut_rejected_a" runat="server"></asp:Label></td>
				            </tr>   
                            <tr bgColor="#ebecec">
					            <td><b>Activated</b></td>
                                <td><asp:Label ID="brw_is_active" runat="server"></asp:Label></td>
                                <td><asp:Label ID="brw_is_active_a" runat="server"></asp:Label></td>
				            </tr>            
                        </table>
                        <table runat="server" id="detCollateral_Class" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Collateral Type</b></td>
                                <td><asp:Label ID="coll_type" runat="server"></asp:Label></td>
                                <td><asp:Label ID="coll_type_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Collateral Class</b></td>
                                <td><asp:Label ID="coll_class" runat="server"></asp:Label></td>
                                <td><asp:Label ID="coll_classP_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Description</b></td>
                                <td><asp:Label ID="coll_desc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="coll_desc_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Data Source</b></td>
                                <td><asp:Label ID="coll_source" runat="server"></asp:Label></td>
                                <td><asp:Label ID="coll_source_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Activated</b></td>
                                <td><asp:Label ID="coll_is_active" runat="server"></asp:Label></td>
                                <td><asp:Label ID="coll_is_active_a" runat="server"></asp:Label></td>
				            </tr>            
                        </table>
                        <table runat="server" id="detGloEligible" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Collateral Class</b></td>
                                <td><asp:Label ID="collateral_class" runat="server"></asp:Label></td>
                                <td><asp:Label ID="collateral_class_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Min LGD</b></td>
                                <td><asp:Label ID="min_lgd" runat="server"></asp:Label></td>
                                <td><asp:Label ID="min_lgd_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Min Collateral Level (C*)</b></td>
                                <td><asp:Label ID="min_collateral_level" runat="server"></asp:Label></td>
                                <td><asp:Label ID="min_collateral_level_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Max Collateral Level (C**)</b></td>
                                <td><asp:Label ID="max_collateral_level" runat="server"></asp:Label></td>
                                <td><asp:Label ID="max_collateral_level_a" runat="server"></asp:Label></td>
				            </tr>         
                        </table>
                        <table runat="server" id="detGloPreScreening" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Borrower Type</b></td>
                                <td><asp:Label ID="glo_pre_brw_type" runat="server"></asp:Label></td>
                                <td><asp:Label ID="glo_pre_brw_type_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Minimal Score (%)</b></td>
                                <td><asp:Label ID="glo_pre_min_score" runat="server"></asp:Label></td>
                                <td><asp:Label ID="glo_pre_min_score_a" runat="server"></asp:Label></td>
				            </tr>     
                        </table>
                        <table runat="server" id="detGloBankCapital" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Month</b></td>
                                <td><asp:Label ID="cap_month" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cap_month_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Year</b></td>
                                <td><asp:Label ID="cap_year" runat="server"></asp:Label></td>
                                <td><asp:Label ID="cap_year_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Capital</b></td>
                                <td><asp:Label ID="capital_value" runat="server"></asp:Label></td>
                                <td><asp:Label ID="capital_value_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>House Limit</b></td>
                                <td><asp:Label ID="capital_house" runat="server"></asp:Label></td>
                                <td><asp:Label ID="capital_house_a" runat="server"></asp:Label></td>
				            </tr>
                        </table>
                        <table runat="server" id="detData_Mgmt" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Data Type</b></td>
                                <td><asp:Label ID="data_type" runat="server"></asp:Label></td>
                                <td><asp:Label ID="data_type_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Data Name</b></td>
                                <td><asp:Label ID="data_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="data_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Description</b></td>
                                <td><asp:Label ID="data_desc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="data_desc_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Final Field Selection</b></td>
                                <td><asp:Label ID="final_field" runat="server"></asp:Label></td>
                                <td><asp:Label ID="final_field_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#f6f7f7">
					            <td><b>Final Rule Condition</b></td>
                                <td><asp:Label ID="final_rule" runat="server"></asp:Label></td>
                                <td><asp:Label ID="final_rule_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#f6f7f7">
					            <td><b>Activated</b></td>
                                <td><asp:Label ID="is_Active" runat="server"></asp:Label></td>
                                <td><asp:Label ID="is_Active_a" runat="server"></asp:Label></td>
				            </tr>             
                        </table>
                        <table runat="server" id="detDataJoinMgmt" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Data Source</b></td>
                                <td><asp:Label ID="join_datasource" runat="server"></asp:Label></td>
                                <td><asp:Label ID="join_datasource_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Merging Condition</b></td>
                                <td><asp:Label ID="join_merging_condition" runat="server"></asp:Label></td>
                                <td><asp:Label ID="join_merging_condition_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#f6f7f7">
					            <td><b>Table Name-Alias</b></td>
                                <td><asp:Label ID="join_tbl_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="join_tbl_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Fields</b></td>
                                <td><asp:Label ID="join_fields" runat="server"></asp:Label></td>
                                <td><asp:Label ID="join_fields_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Condition</b></td>
                                <td><asp:Label ID="join_condition" runat="server"></asp:Label></td>
                                <td><asp:Label ID="join_condition_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#ebecec">
					            <td><b>Merging Key</b></td>
                                <td><asp:Label ID="join_merging_keys" runat="server"></asp:Label></td>
                                <td><asp:Label ID="join_merging_keys_a" runat="server"></asp:Label></td>
				            </tr>           
                        </table>
                        <table runat="server" id="detProduct_Mgmt" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Product List</b></td>
                                <td><asp:Label ID="prd_product" runat="server"></asp:Label></td>
                                <td><asp:Label ID="prd_product_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Product Name</b></td>
                                <td><asp:Label ID="prd_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="prd_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#f6f7f7">
					            <td><b>Product Description</b></td>
                                <td><asp:Label ID="prd_desc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="prd_desc_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Cut Off (Score) Approved</b></td>
                                <td><asp:Label ID="prd_cut_approved" runat="server"></asp:Label></td>
                                <td><asp:Label ID="prd_cut_approved_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Cut Off (Score) Rejected</b></td>
                                <td><asp:Label ID="prd_cut_rejected" runat="server"></asp:Label></td>
                                <td><asp:Label ID="prd_cut_rejected_a" runat="server"></asp:Label></td>
				            </tr> 
                            <tr bgColor="#ebecec">
					            <td><b>Activated</b></td>
                                <td><asp:Label ID="prd_is_active" runat="server"></asp:Label></td>
                                <td><asp:Label ID="prd_is_active_a" runat="server"></asp:Label></td>
				            </tr>           
                        </table>
                        <table runat="server" id="detPooling" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Name</b></td>
                                <td><asp:Label ID="pol_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Rule</b></td>
                                <td><asp:Label ID="pol_rul_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_rul_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>PD Value</b></td>
                                <td><asp:Label ID="pol_pd" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_pd_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>LGD Value</b></td>
                                <td><asp:Label ID="pol_lgd" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_lgd_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>EAD Value</b></td>
                                <td><asp:Label ID="pol_ead" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_ead_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="pol_rule_rate" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_rule_rate_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Distribution Baseline</b></td>
                                <td><asp:Label ID="pol_pd_baseline" runat="server"></asp:Label></td>
                                <td><asp:Label ID="pol_pd_baseline_a" runat="server"></asp:Label></td>
				            </tr> 
                        </table>
                        <table runat="server" id="detRatingModelRating" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="400px">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Rating Model Code</b></td>
                                <td><asp:Label ID="scr_scc_code" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_code_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Status</b></td>
                                <td><asp:Label ID="scr_status_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_status_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 1</b></td>
                                <td><asp:Label ID="scr_scc_limit1" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit1_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score1" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score1_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 1</b></td>
                                <td><asp:Label ID="scr_scc_cat1" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat1_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 1</b></td>
                                <td><asp:Label ID="scr_scc_desc1" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc1_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 2</b></td>
                                <td><asp:Label ID="scr_scc_limit2" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit2_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score2" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score2_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 2</b></td>
                                <td><asp:Label ID="scr_scc_cat2" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat2_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 2</b></td>
                                <td><asp:Label ID="scr_scc_desc2" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc2_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 3</b></td>
                                <td><asp:Label ID="scr_scc_limit3" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit3_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score3" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score3_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 3</b></td>
                                <td><asp:Label ID="scr_scc_cat3" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat3_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 3</b></td>
                                <td><asp:Label ID="scr_scc_desc3" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc3_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 4</b></td>
                                <td><asp:Label ID="scr_scc_limit4" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit4_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score4" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score4_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 4</b></td>
                                <td><asp:Label ID="scr_scc_cat4" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat4_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 4</b></td>
                                <td><asp:Label ID="scr_scc_desc4" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc4_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 5</b></td>
                                <td><asp:Label ID="scr_scc_limit5" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit5_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score5" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score5_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 5</b></td>
                                <td><asp:Label ID="scr_scc_cat5" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat5_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 5</b></td>
                                <td><asp:Label ID="scr_scc_desc5" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc5_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 6</b></td>
                                <td><asp:Label ID="scr_scc_limit6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit6_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score6_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 6</b></td>
                                <td><asp:Label ID="scr_scc_cat6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat6_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 6</b></td>
                                <td><asp:Label ID="scr_scc_desc6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc6_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 7</b></td>
                                <td><asp:Label ID="scr_scc_limit7" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit7_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score7" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score7_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 7</b></td>
                                <td><asp:Label ID="scr_scc_cat7" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat7_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 7</b></td>
                                <td><asp:Label ID="scr_scc_desc7" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc7_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 8</b></td>
                                <td><asp:Label ID="scr_scc_limit8" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit8_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score8" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score8_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 8</b></td>
                                <td><asp:Label ID="scr_scc_cat8" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat8_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 8</b></td>
                                <td><asp:Label ID="scr_scc_desc8" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc8_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 9</b></td>
                                <td><asp:Label ID="scr_scc_limit9" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit9_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score9" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score9_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 9</b></td>
                                <td><asp:Label ID="scr_scc_cat9" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat9_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 9</b></td>
                                <td><asp:Label ID="scr_scc_desc9" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc9_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 10</b></td>
                                <td><asp:Label ID="scr_scc_limit10" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit10_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score10" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score10_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 10</b></td>
                                <td><asp:Label ID="scr_scc_cat10" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat10_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 10</b></td>
                                <td><asp:Label ID="scr_scc_desc10" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc10_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 11</b></td>
                                <td><asp:Label ID="scr_scc_limit11" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit11_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score11" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score11_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 11</b></td>
                                <td><asp:Label ID="scr_scc_cat11" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat11_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 11</b></td>
                                <td><asp:Label ID="scr_scc_desc11" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc11_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 12</b></td>
                                <td><asp:Label ID="scr_scc_limit12" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit12_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score12" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score12_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 12</b></td>
                                <td><asp:Label ID="scr_scc_cat12" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat12_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 12</b></td>
                                <td><asp:Label ID="scr_scc_desc12" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc12_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 13</b></td>
                                <td><asp:Label ID="scr_scc_limit13" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit13_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score13" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score13_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 13</b></td>
                                <td><asp:Label ID="scr_scc_cat13" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat13_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 13</b></td>
                                <td><asp:Label ID="scr_scc_desc13" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc13_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 14</b></td>
                                <td><asp:Label ID="scr_scc_limit14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit14_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score14_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 14</b></td>
                                <td><asp:Label ID="scr_scc_cat14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat14_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 14</b></td>
                                <td><asp:Label ID="scr_scc_desc14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc14_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 15</b></td>
                                <td><asp:Label ID="scr_scc_limit15" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit15_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score15" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score15_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 15</b></td>
                                <td><asp:Label ID="scr_scc_cat15" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat15_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 15</b></td>
                                <td><asp:Label ID="scr_scc_desc15" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc15_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 16</b></td>
                                <td><asp:Label ID="scr_scc_limit16" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit16_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score16" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score16_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 16</b></td>
                                <td><asp:Label ID="scr_scc_cat16" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat16_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 16</b></td>
                                <td><asp:Label ID="scr_scc_desc16" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc16_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 17</b></td>
                                <td><asp:Label ID="scr_scc_limit17" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit17_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score17" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score17_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 17</b></td>
                                <td><asp:Label ID="scr_scc_cat17" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat17_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 17</b></td>
                                <td><asp:Label ID="scr_scc_desc17" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc17_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 18</b></td>
                                <td><asp:Label ID="scr_scc_limit18" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit18_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score18" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score18_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 18</b></td>
                                <td><asp:Label ID="scr_scc_cat18" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat18_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 18</b></td>
                                <td><asp:Label ID="scr_scc_desc18" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc18_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 19</b></td>
                                <td><asp:Label ID="scr_scc_limit19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit19_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score19_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 19</b></td>
                                <td><asp:Label ID="scr_scc_cat19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat19_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 19</b></td>
                                <td><asp:Label ID="scr_scc_desc19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc19_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 20</b></td>
                                <td><asp:Label ID="scr_scc_limit20" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_limit20_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rating</b></td>
                                <td><asp:Label ID="scr_scc_score20" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_score20_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Category 20</b></td>
                                <td><asp:Label ID="scr_scc_cat20" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_cat20_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Description 20</b></td>
                                <td><asp:Label ID="scr_scc_desc20" runat="server"></asp:Label></td>
                                <td><asp:Label ID="scr_scc_desc20_a" runat="server"></asp:Label></td>
				            </tr>
                        </table>
                        <table runat="server" id="detRule" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="30%">Field Name</td>
                                <td width="35%">Before</td>
                                <td width="35%">After</td>
                            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Master Data</b></td>
                                <td><asp:Label ID="rul_data_source" runat="server"></asp:Label></td>
                                <td><asp:Label ID="rul_data_source_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Rule Name</b></td>
                                <td><asp:Label ID="rul_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="rul_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Description</b></td>
                                <td><asp:Label ID="rul_desc" runat="server"></asp:Label></td>
                                <td><asp:Label ID="rul_desc_a" runat="server"></asp:Label></td>
				            </tr>
                             <tr bgColor="#ebecec">
					            <td><b>SQL Query</b></td>
                                <td><asp:Label ID="rul_sql" runat="server"></asp:Label></td>
                                <td><asp:Label ID="rul_sql_a" runat="server"></asp:Label></td>
				            </tr>
                        </table>
                        <table runat="server" id="detSystem" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                    <td width="30%">Field Name</td>
                                    <td width="35%">Before</td>
                                    <td width="35%">After</td>
                                </tr>
                                <tr bgcolor="#f6f7f7" style="font-weight: bold;">
                                    <td colspan="3" style="color:#0C416F">BACKUP</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Backup Location</b></td>
                                    <td width="35%"><asp:Label ID="sys_backup_all_tbl_loc" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="sys_backup_all_tbl_loc_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#ebecec" id="bak_tr">
                                    <td width="30%"><b>Backup Schedule</b></td>
                                    <td width="35%">
                                        <asp:Label ID="sys_backup_sched" runat="server"></asp:Label>, 
                                        Day <asp:Label ID="sys_backup_all_tbl_date" runat="server"></asp:Label>, 
                                        Time <asp:Label ID="sys_backup_all_tbl_time" runat="server"></asp:Label>(hh:mm)</td>
                                    <td width="35%">
                                        <asp:Label ID="sys_backup_sched_a" runat="server"></asp:Label>, 
                                        Day <asp:Label ID="sys_backup_all_tbl_date_a" runat="server"></asp:Label>, 
                                        Time <asp:Label ID="sys_backup_all_tbl_time_a" runat="server"></asp:Label>(hh:mm)</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Backup Mode</b></td>
                                    <td width="35%"><asp:Label ID="sys_backup_mode" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="sys_backup_mode_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#f6f7f7" style="font-weight: bold;">
                                    <td colspan="3" style="color:#0C416F">
                                        BASEL ENGINE
                                    </td>
                                </tr>
                                <tr bgcolor="#ebecec" id="etl_tr">
                                    <td width="30%"><b>Basel Engine Schedule</b></td>
                                    <td width="35%"><asp:Label ID="sys_etl_sched" runat="server"></asp:Label>, 
                                        <span id="etl_ddl">Day <asp:Label ID="sys_etl_date" runat="server"></asp:Label>,</span> 
                                        Time <asp:Label ID="sys_etl_time" runat="server"></asp:Label>(hh:mm)</td>
                                    <td width="35%"><asp:Label ID="sys_etl_sched_a" runat="server"></asp:Label>, 
                                        <span id="Span1">Day <asp:Label ID="sys_etl_date_a" runat="server"></asp:Label>,</span> 
                                        Time <asp:Label ID="sys_etl_time_a" runat="server"></asp:Label>(hh:mm)</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Basel Engine Running Mode</b></td>
                                    <td width="35%"><asp:Label ID="sys_etl_mode" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="sys_etl_mode_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#f6f7f7" style="font-weight: bold;">
                                    <td colspan="3" style="color:#0C416F">
                                        RISK CALCULATION ENGINE</td>
                                </tr>
                                <tr bgcolor="#ebecec" id="scc_tr">
                                    <td width="30%"><b>Risk Calculation Engine Schedule</b></td>
                                    <td width="35%"><asp:Label ID="sys_en_sched" runat="server"></asp:Label>, 
                                        <span id="scc_ddl">Day <asp:Label ID="sys_en_date" runat="server"></asp:Label>,</span>
                                        Time <asp:Label ID="sys_en_time" runat="server"></asp:Label>(hh:mm)</td>
                                    <td width="35%"><asp:Label ID="sys_en_sched_a" runat="server"></asp:Label>, 
                                        <span id="Span2">Day <asp:Label ID="sys_en_date_a" runat="server"></asp:Label>,</span>
                                        Time <asp:Label ID="sys_en_time_a" runat="server"></asp:Label>(hh:mm)</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Risk Calculation Engine Running Mode</b></td>
                                    <td width="35%"><asp:Label ID="sys_en_mode" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="sys_en_mode_a" runat="server"></asp:Label></td>
                                </tr>

                                <tr bgcolor="#f6f7f7" style="font-weight: bold;">
                                    <td colspan="3" style="color:#0C416F">
                                        ARCHIVE
                                    </td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Archive Location</b></td>
                                    <td width="35%"><asp:Label ID="sys_arch_loc" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="sys_arch_loc_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#ebecec" id="arc_tr">
                                    <td width="30%"><b>Archive Schedule</b></td>
                                    <td width="35%"><asp:Label ID="sys_arch_sched" runat="server"></asp:Label>, 
                                        <span id="Span3">Day <asp:Label ID="sys_arch_date" runat="server"></asp:Label>,</span>
                                        Time <asp:Label ID="sys_arch_time" runat="server"></asp:Label>(hh:mm)</td>
                                    <td width="35%"><asp:Label ID="sys_arch_sched_a" runat="server"></asp:Label>, 
                                        <span id="Span4">Day <asp:Label ID="sys_arch_date_a" runat="server"></asp:Label>,</span>
                                        Time <asp:Label ID="sys_arch_time_a" runat="server"></asp:Label>(hh:mm)</td>
                                </tr>
                                <%--<tr bgcolor="#ebecec">
                                    <td width="30%"><b>Archive Summary-Table older than</b></td>
                                    <td width="35%"><asp:Label ID="arc_older_summary" runat="server"></asp:Label> Month</td>
                                    <td width="35%"><asp:Label ID="arc_older_summary_a" runat="server"></asp:Label> Month</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Archive Agent Activity History older than</b></td>
                                    <td width="35%"><asp:Label ID="arc_older_activity" runat="server"></asp:Label> Month</td>
                                    <td width="35%"><asp:Label ID="arc_older_activity_a" runat="server"></asp:Label> Month</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Archive Log History older than</b></td>
                                    <td width="35%"><asp:Label ID="arc_older_log" runat="server"></asp:Label> Month</td>
                                    <td width="35%"><asp:Label ID="arc_older_log_a" runat="server"></asp:Label> Month</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Archive Closed Tasklist older than</b></td>
                                    <td width="35%"><asp:Label ID="arc_older_tasklist" runat="server"></asp:Label> Month</td>
                                    <td width="35%"><asp:Label ID="arc_older_tasklist_a" runat="server"></asp:Label> Month</td>
                                </tr>--%>
                                <%--<tr bgcolor="#f6f7f7" style="font-weight: bold;">
                                    <td colspan="3" style="color:#0C416F">
                                        USER SETTINGS
                                    </td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>User Inactive after</b></td>
                                    <td width="35%"><asp:Label ID="usr_inactive" runat="server"></asp:Label> days</td>
                                    <td width="35%"><asp:Label ID="usr_inactive_a" runat="server"></asp:Label> days</td>
                                </tr>
                                <tr bgcolor="#ebecec" style="display:none;">
                                    <td width="30%"><b>Session Timeout</b></td>
                                    <td width="35%"><asp:Label ID="usr_timeout" runat="server"></asp:Label> minutes</td>
                                    <td width="35%"><asp:Label ID="usr_timeout_a" runat="server"></asp:Label> minutes</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><b>Wrong Password Limit</b></td>
                                    <td width="35%"><asp:Label ID="usr_fail_password" runat="server"></asp:Label> times</td>
                                    <td width="35%"><asp:Label ID="usr_fail_password_a" runat="server"></asp:Label> times</td>
                                </tr>--%>
                                <tr bgcolor="#f6f7f7" style="font-weight: bold;">
                                    <td colspan="3" style="color:#0C416F">
                                        ERROR NOTIFICATION EMAIL ADDRESS</td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td width="30%"><strong>Email Address</strong></td>
                                    <td width="35%"><asp:Label ID="sys_email" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="sys_email_a" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                            <table runat="server" id="detLogin" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
	                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
		                            <td width="30%">Field Name</td>
		                            <td width="35%">Users <asp:Label ID="log_action_mode_l" runat="server"></asp:Label> Detail</td>
	                            </tr>
	                            <tr bgcolor="#f6f7f7">
		                            <td width="30%"><b>User ID</b></td>
		                            <td width="35%"><asp:Label ID="usr_usersid_l" runat="server"></asp:Label></td>
	                            </tr>
	                            <tr bgcolor="#ebecec">
		                            <td><b>Name</b></td>
		                            <td width="35%"><asp:Label ID="usr_name_l" runat="server"></asp:Label></td>
	                            </tr>
	                            <tr bgcolor="#f6f7f7">
		                            <td><b>NIP</b></td>
		                            <td width="35%"><asp:Label ID="usr_nip_l" runat="server"></asp:Label></td>
	                            </tr>
	                            <tr bgcolor="#ebecec">
		                            <td><b>Access Level</b></td>
		                            <td width="35%"><asp:Label ID="usr_lvl_name_l" runat="server"></asp:Label></td>
	                            </tr>
	                            <tr bgcolor="#f6f7f7">
		                            <td><b>Lock Status</b></td>
		                            <td width="35%"><asp:Label ID="usr_status_name_l" runat="server"></asp:Label></td>
	                            </tr>
	                            <tr bgcolor="#ebecec">
		                            <td><b>Access From IP</b></td>
		                            <td width="35%"><asp:Label ID="usr_ip_address_l" runat="server"></asp:Label></td>
	                            </tr>
                            </table>
                            <table runat="server" id="detUsers" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                    <td width="30%">Field Name</td>
                                    <td width="35%">Before</td>
                                    <td width="35%">After</td>
                                </tr>
                                <tr bgcolor="#f6f7f7">
                                    <td width="30%"><b>User ID</b></td>
                                    <td width="35%"><asp:Label ID="usr_usersid" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="usr_usersid_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td><b>Name</b></td>
                                    <td width="35%"><asp:Label ID="usr_name" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="usr_name_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#f6f7f7">
                                    <td><b>NIP</b></td>
                                    <td width="35%"><asp:Label ID="usr_nip" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="usr_nip_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#ebecec">
                                    <td><b>Access Level</b></td>
                                    <td width="35%"><asp:Label ID="usr_lvl_name" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="usr_lvl_name_a" runat="server"></asp:Label></td>
                                </tr>
                                <tr bgcolor="#f6f7f7">
                                    <td><b>Lock Status</b></td>
                                    <td width="35%"><asp:Label ID="usr_status_name" runat="server"></asp:Label></td>
                                    <td width="35%"><asp:Label ID="usr_status_name_a" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        <table runat="server" id="detScorecardChar" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                            <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                <td width="40%">Field Name</td>
                                <td width="30%">Before</td>
                                <td width="30%">After</td>
                            </tr>
                            <tr bgcolor="#f6f7f7">
					            <td><b>Field</b></td>
                                <td><asp:Label ID="sch_field_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_field_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Default Score</b></td>
                                <td><asp:Label ID="sch_default_score" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_default_score_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#f6f7f7">
					            <td><b>Koefisien</b></td>
                                <td><asp:Label ID="sch_koefisien_score" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_koefisien_score_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgcolor="#ebecec">
					            <td><b>Status</b></td>
                                <td><asp:Label ID="status_name" runat="server"></asp:Label></td>
                                <td><asp:Label ID="status_name_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 1</b></td>
                                <td><asp:Label ID="sch_limit1" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit1_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 1</b></td>
                                <td><asp:Label ID="sch_score1" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score1_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 2</b></td>
                                <td><asp:Label ID="sch_limit2" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit2_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 2</b></td>
                                <td><asp:Label ID="sch_score2" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score2_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 3</b></td>
                                <td><asp:Label ID="sch_limit3" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit3_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 3</b></td>
                                <td><asp:Label ID="sch_score3" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score3_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 4</b></td>
                                <td><asp:Label ID="sch_limit4" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit4_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 4</b></td>
                                <td><asp:Label ID="sch_score4" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score4_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 5</b></td>
                                <td><asp:Label ID="sch_limit5" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit5_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 5</b></td>
                                <td><asp:Label ID="sch_score5" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score5_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 6</b></td>
                                <td><asp:Label ID="sch_limit6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit6_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 6</b></td>
                                <td><asp:Label ID="sch_score6" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score6_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 7</b></td>
                                <td><asp:Label ID="sch_limit7" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit7_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 7</b></td>
                                <td><asp:Label ID="sch_score7" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score7_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 8</b></td>
                                <td><asp:Label ID="sch_limit8" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit8_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 8</b></td>
                                <td><asp:Label ID="sch_score8" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score8_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 9</b></td>
                                <td><asp:Label ID="sch_limit9" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit9_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 9</b></td>
                                <td><asp:Label ID="sch_score9" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score9_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 10</b></td>
                                <td><asp:Label ID="sch_limit10" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit10_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 10</b></td>
                                <td><asp:Label ID="sch_score10" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score10_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 11</b></td>
                                <td><asp:Label ID="sch_limit11" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit11_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 11</b></td>
                                <td><asp:Label ID="sch_score11" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score11_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 12</b></td>
                                <td><asp:Label ID="sch_limit12" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit12_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 12</b></td>
                                <td><asp:Label ID="sch_score12" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score12_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 13</b></td>
                                <td><asp:Label ID="sch_limit13" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit13_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 13</b></td>
                                <td><asp:Label ID="sch_score13" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score13_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 14</b></td>
                                <td><asp:Label ID="sch_limit14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit14_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 14</b></td>
                                <td><asp:Label ID="sch_score14" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score14_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 15</b></td>
                                <td><asp:Label ID="sch_limit15" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit15_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 15</b></td>
                                <td><asp:Label ID="sch_score15" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score15_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 16</b></td>
                                <td><asp:Label ID="sch_limit16" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit16_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 16</b></td>
                                <td><asp:Label ID="sch_score16" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score16_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 17</b></td>
                                <td><asp:Label ID="sch_limit17" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit17_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 17</b></td>
                                <td><asp:Label ID="sch_score17" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score17_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 18</b></td>
                                <td><asp:Label ID="sch_limit18" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit18_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 18</b></td>
                                <td><asp:Label ID="sch_score18" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score18_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 19</b></td>
                                <td><asp:Label ID="sch_limit19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit19_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 19</b></td>
                                <td><asp:Label ID="sch_score19" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score19_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#f6f7f7">
					            <td><b>Upper Limit 20</b></td>
                                <td><asp:Label ID="sch_limit20" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_limit20_a" runat="server"></asp:Label></td>
				            </tr>
                            <tr bgColor="#ebecec">
					            <td><b>Score 20</b></td>
                                <td><asp:Label ID="sch_score20" runat="server"></asp:Label></td>
                                <td><asp:Label ID="sch_score20_a" runat="server"></asp:Label></td>
				            </tr>       
                            </table>

                            <table runat="server" id="detScorecards" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                <tr bgcolor="#ebecec"  style="font-weight: bold;">
                                    <td width="30%">Field Name</td>
                                    <td width="35%">Before</td>
                                    <td width="35%">After</td>
                                </tr>
                                 <tr bgColor="#f6f7f7">
					                <td><b>Rating Model Category</b></td>
                                    <td><asp:Label ID="scc_category" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="scc_category_a" runat="server"></asp:Label></td>
				                </tr>
                                <tr bgColor="#ebecec">
					                <td><b>Rating Model Code</b></td>
                                    <td><asp:Label ID="scc_code" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="scc_code_a" runat="server"></asp:Label></td>
				                </tr>
                                <tr bgcolor="#f6f7f7">
					                <td><b>Rating Model Name</b></td>
                                    <td><asp:Label ID="scc_name" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="scc_name_a" runat="server"></asp:Label></td>
				                </tr>
				                <tr bgcolor="#ebecec">
					                <td><b>Description</b></td>
                                    <td><asp:Label ID="scc_description" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="scc_description_a" runat="server"></asp:Label></td>
				                </tr>
                                <tr bgColor="#f6f7f7">
					                <td><b>Additive Points</b></td>
                                    <td><asp:Label ID="scc_additive_points" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="scc_additive_points_a" runat="server"></asp:Label></td>
				                </tr>
                                <tr bgColor="#ebecec">
					                <td><b>Action</b></td>
                                    <td><asp:Label ID="scc_status" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="scc_status_a" runat="server"></asp:Label></td>
				                </tr>  
                            </table>

                            
            <table runat="server" id="detLevel" style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                <tr bgcolor="#ebecec"  style="font-weight: bold;">
                    <td width="30%">Field Name</td>
                    <td width="35%">Before(B)</td>
                    <td width="35%">After(A)</td>
                </tr>
				<tr bgcolor="#f6f7f7">
                    <td width="30%"><b>User Role Code</b></td>
                    <td width="35%"><asp:Label ID="lu_level_code" runat="server"></asp:Label></td>
                    <td width="35%"><asp:Label ID="lu_level_code_a" runat="server"></asp:Label></td>
                </tr>
                <tr bgcolor="#ebecec">
                    <td><b>User Role Name</b></td>
                    <td width="35%"><asp:Label ID="lu_level_name_level" runat="server"></asp:Label></td>
                    <td width="35%"><asp:Label ID="lu_level_name_level_a" runat="server"></asp:Label></td>
                    </tr>
                <tr bgcolor="#f6f7f7">
                    <td><b>Description</b></td>
                    <td width="35%"><asp:Label ID="lu_level_description" runat="server"></asp:Label></td>
                    <td width="35%"><asp:Label ID="lu_level_description_a" runat="server"></asp:Label></td>
                </tr>
				<tr bgcolor="#ebecec">
                    <td><b>Activated</b></td>
                    <td width="35%"><asp:Label ID="lu_status" runat="server"></asp:Label></td>
                    <td width="35%"><asp:Label ID="lu_status_a" runat="server"></asp:Label></td>
                </tr>
                <%--<tr><td colspan="3">
                    <table style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
	            <tr>
		            <td valign="top" width="20%">
                        <fieldset style="background:white; border-color:#f6f7f7; min-height:445px;height:800px">
                            <table style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
				            <tr>
					            <td>
						            <table style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width:20px;text-align:center;font-weight: bold;">(B)</td>
                                            <td style="width:20px;text-align:center;font-weight: bold;">(A)</td>
                                        </tr>
							            <tr>
								            <td colSpan="4">Admin</td>
                                            <td class="style1"><asp:Label ID="lvl_adm" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td colSpan="3">User</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">User Management</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <td>New</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_new" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_new_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td width="20"></td>
								            <td width="20"></td>
								            <td width="20"></td>
								            <td>Edit</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_edit" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_edit_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <td>Delete</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_delete" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_delete_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <td>View &amp; Activate</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_view" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_mgmt_view_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Access Level</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <td>New</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_new" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_new_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <td>Edit</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_edit" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_edit_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <td>Delete</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_delete" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_delete_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td></td>
								            <TD>View &amp; Activate</TD>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_view" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_usr_ac_view_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td colSpan="3">System</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_sys" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_sys_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Edit</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_sys_edit" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_sys_edit_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td colSpan="3">Log</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">User Access</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_ua" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_ua_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">User Management</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_usr" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_usr_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Access Level</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_ac" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_ac_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">System Control</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_sys" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_sys_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Verification</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_ver" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_ver_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Customer Inquiry</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_inq" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_inq_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Offering</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_off" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_off_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">EOD</td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_eod" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_adm_log_eod_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td colSpan="3">Data Upload</td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">New</td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_new" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_new_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Replace</td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_replace" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_replace_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Delete</td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_delete" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_delete_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Truncate</td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_truncate" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_truncate_a" runat="server"></asp:Label></td>
							            </tr>
							            <tr>
								            <td></td>
								            <td></td>
								            <td colspan="2">Download</td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_download" runat="server"></asp:Label></td>
                                            <td class="style1"><asp:Label ID="lvl_dataupld_download_a" runat="server"></asp:Label></td>
							            </tr>
						            </table>
					            </td>
				            </tr>
				            </table>
			            </fieldset>
		            </td>
		            <td valign="top" width="20%">
			            <fieldset style="min-height:429px; background:white; border-color:#f6f7f7; min-height:445px;height:800px">
				            <table  style="padding: 0px 5px; font-size:11px; font-family:century gothic; height: 800px;" 
                                cellSpacing="1" cellPadding="0" width="98%">
					            <tr>
						            <td style="height: 399px; vertical-align: top;">
							            <table  style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                            <tr>
                                                <td></td><td></td><td></td><td></td><td style="width:20px;text-align:center;font-weight: bold;">(B)</td><td style="width:20px;text-align:center;font-weight: bold;">(A)</td>
                                            </tr>
                                            <tr>
									            <td colSpan="4">Parameters</td>
                                                <td class="style1"><asp:Label ID="lvl_param" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
								                <td width="20"></td>
									            <td colSpan="3">Verification</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">View</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ver_delete_A" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
								                <td width="20"></td>
									            <td colSpan="3">Campaign</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">View</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmp_delete_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
								                <td width="20"></td>
									            <td colSpan="3">Offering</td>
                                                <td class="style1"><asp:Label ID="lvl_param_off" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">View</td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_off_delete_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20" colspan="3">Retention</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Planning</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td>&nbsp;</td>
									            <td>New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td>&nbsp;</td>
									            <td>Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td>&nbsp;</td>
									            <td>View</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_view" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_ret_view_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Reason</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td>&nbsp;</td>
									            <td>New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td>&nbsp;</td>
									            <td>Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td>&nbsp;</td>
									            <td>Delete</td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_ret_rea_delete_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20" colspan="3">RFM</td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Delete</td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_rfm_delete_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20" colspan="3">Scorecard</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Delete</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_delete_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Set Bin</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_set_bin" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_set_bin_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20" colspan="3">Scorecard Characteristic</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">New</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_new" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_new_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Edit</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_edit" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_edit_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Delete</td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_delete" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_scc_car_delete_a" runat="server"></asp:Label></td>
								            </tr>
                                             <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20" colspan="3">Contact Management</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmt" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmt_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Permanent CM</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmt_pmt" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmt_pmt_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Temporary CM</td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmt_tmt" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_cmt_tmt_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20" colspan="3">CossSell Priorization</td>
                                                <td class="style1"><asp:Label ID="lvl_param_xsell" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_param_xsell_a" runat="server"></asp:Label></td>
								            </tr>
							            </table>
						            </td>
					            </tr>
				            </table>
			             </fieldset>
		            </td>
		            <td valign="top" width="20%">
			            <fieldset style="min-height:429px; background:white; border-color:#f6f7f7; min-height:445px;height:800px">
				            <table  style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
					            <tr>
						            <td style="height: 399px; vertical-align: top;">
							            <table  style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                            <tr>
                                                <td></td><td></td><td></td><td></td><td style="width:20px;text-align:center;font-weight: bold;">(B)</td><td style="width:20px;text-align:center;font-weight: bold;">(A)</td>
                                            </tr>
								            <tr>
									            <td colSpan="4">Activity</td>
                                                <td class="style1"><asp:Label ID="lvl_act" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td colSpan="3">Account Management</td>
                                                <td class="style1"><asp:Label ID="lvl_act_managemnt" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_managemnt_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td colSpan="3">Inquiry</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Family Link</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_family" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_family_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Possible Family</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_possible" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_possible_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Product Inquiry</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_prod" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_prod_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
                                                <td width="20"></td>
									            <td>Customer Verification</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_prod_ver" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_prod_ver_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
                                                <td width="20"></td>
									            <td>Customer View & Verification</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_prod_view" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_prod_view_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Retention</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_ret" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_ret_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Cross Sell</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_xsell" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_xsell_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">History</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_hist" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_hist_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Eligible Product</td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_elp" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_inq_elp_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">New Prospect</td>
                                                <td class="style1"><asp:Label ID="lvl_act_prospect" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_prospect_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Task List</td>
                                                <td class="style1"><asp:Label ID="lvl_act_tasklist" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_tasklist_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Lead Status</td>                                                
                                                <td class="style1"><asp:Label ID="lvl_act_leads_status" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_leads_status_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr style="display:none">
									            <td width="20"></td>
									            <td width="20"></td>
									            <td colspan="2">Task List Assign</td>
                                                <td class="style1"><asp:Label ID="lvl_act_tasklist_assign" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_tasklist_assign_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
									            <td colspan="2">Lead Assign</td>
                                                <td class="style1"><asp:Label ID="lvl_act_leads_assign" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_act_leads_assign_a" runat="server"></asp:Label></td>
								            </tr>
							            </table>
						            </td>
					            </tr>
				            </table>
			             </fieldset>
		             </td>
		            <td valign="top" width="20%">
			            <fieldset style="min-height:429px; background:white; border-color:#f6f7f7; min-height:445px;height:800px">
				            <table  style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
					            <tr>
						            <td style="height: 399px; vertical-align: top;">
							            <table  style="font-size:11px; font-family:century gothic;" cellSpacing="0" cellPadding="1" width="100%">
                                            <tr>
                                                <td></td><td></td><td></td><td></td><td style="width:20px;font-weight: bold;" 
                                                    class="style1">(B)</td><td style="width:20px;font-weight: bold;" 
                                                    class="style1">(A)</td>
                                            </tr>
								            <tr>
									            <td colSpan="4">Report</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
									            <td colSpan="3">Internal</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
                                                <td width="20"></td>
									            <td colSpan="2">Verification</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_iver" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_iver_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
                                                <td width="20"></td>
									            <td colSpan="2">Verification Summary</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_summ" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_summ_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20"></td>
                                                <td width="20"></td>
									            <td colSpan="2">Data Verification</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_ver" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_internal_ver_a" runat="server"></asp:Label></td>
								            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Campaign</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_cmp" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_cmp_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Cross Sell</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_xcell" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_xcell_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Prospect</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_pros" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_pros_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Campaign Report</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_cmp" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_cmp_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Productivity</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_prod" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_prod_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Recap</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_rm" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_rm_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Sales Funnel</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_sf" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_sf_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Sales Cycle</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_sc" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_sc_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Contact Time Analysis</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_cta" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_cta_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">Maintenance Activity</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_rma" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_rma_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20"></td>
	                                            <td width="20"></td>
	                                            <td colSpan="2">ETL Activity</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_etl" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_internal_etl_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20">&nbsp;</td>
	                                            <td width="20">&nbsp;</td>
	                                            <td colSpan="2">Upload Report</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_online" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_online_a" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
	                                            <td width="20">&nbsp;</td>
	                                            <td width="20">&nbsp;</td>
	                                            <td colSpan="2">Query Analyzer</td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_query" runat="server"></asp:Label></td>
	                                            <td class="style1"><asp:Label ID="lvl_rpt_query_a" runat="server"></asp:Label></td>
                                            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td colSpan="3">Wholesale Banking</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td colSpan="2">RM</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>TAT Liabilities</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_fund" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_fund_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>TAT Asset</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_land" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_land_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>Productivity</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_prod" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_prod_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>Customer by Activity</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_nas" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_rm_nas_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td colSpan="2">Product Partners</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>TAT Liabilities</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_fund" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_fund_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>TAT Asset</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_land" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_land_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>Productivity</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_prod" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_prod_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>Customer by Activity</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_nas" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_tb_nas_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td colSpan="2">Portfolio</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_port" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_wb_port_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
									            <td colSpan="3">Report Setting</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_set" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_set_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>Productivity WB</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_set_prod" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_set_prod_a" runat="server"></asp:Label></td>
								            </tr>
								            <tr>
									            <td width="20">&nbsp;</td>
                                                <td width="20">&nbsp;</td>
									            <td width="20">&nbsp;</td>
                                                <td>Report Campaign</td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_set_cmp" runat="server"></asp:Label></td>
                                                <td class="style1"><asp:Label ID="lvl_rpt_set_cmp_a" runat="server"></asp:Label></td>
								            </tr>
                                         </table>                                     
                                    </td>
					            </tr>
				            </table>
		             </td>

	            </tr>
            </table>    

                </td></tr>--%>
                <tr bgcolor="#f6f7f7">
                    <td style="width:32.5%"><b>Menu</b></td>
                    <td>
                        <table id="Table1" style="font-size:11px; font-family:century gothic;" cellspacing="0" cellpadding="1">
                                <%If levelCon = True Then%>
                                        <%= fillLevelDetBefore()%>
                                <%End If%>
                        </table>   
                    </td>
                    <td>
                        <table id="Table2" style="font-size:11px; font-family:century gothic;" cellspacing="0" cellpadding="1">
                                <%If levelCon = True Then%>
                                        <%= fillLevelDetAfter()%>
                                <%End If%>
                        </table> 
                    </td>
                </tr>
            </table>
                        </fieldset>                        
                    </td>
				</tr>
			</table>
			<table background="../styles/grid/footer.gif" cellSpacing="0" cellPadding="0" width="98%" style="border: thin solid #bcbcbc;border-radius: 0 0 5px 5px;height:30px;border-top:0;border-bottom:0">
                <tr>
					<td style="vertical-align:middle">
						<asp:button id="btnCancel" runat="server" Text="Close" CausesValidation="False" CssClass="inpBtn" OnClientClick="CloseMe()"></asp:button></td>
                    </td>
				</tr>
			</table>
            </center>
		</form>
	</body>
</html>
