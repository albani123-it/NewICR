<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="systems.aspx.vb" Inherits="NewCRM.systems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>System <small>&nbsp;</small></h2>
                    <div style="float:right"><button type="button" class="btn btn-default btn-sm">Edit</button></div>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
                          <table width="500px" align="center" class="table table-striped" border="0">
                            <tr>
                                <td><b>BACKUP - Last Update</b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Backup Location</td>
                              <td>:</td>
                              <td>D:\Data\BACKUP</td>
                              <td>&nbsp;</td>
                              <td>Backup Mode</td>
                              <td>:</td>
                              <td>Manual</td>
                            </tr>   
                            <tr>
                              <td>Backup Schedule</td>
                              <td>:</td>
                              <td>Monthly, Day 1, Time 11:41</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run Backup</button></td>
                            </tr> 
                              
                            <tr>
                                <td><b>DATA RETRIEVAL (ETL) - Last Updated</b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>ETL Schedule</td>
                              <td>:</td>
                              <td>D:\Data\BACKUP</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run ETL</button></td>
                            </tr>   
                            <tr>
                              <td>ETL Running Mode</td>
                              <td>:</td>
                              <td>Manual</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr> 
                              
                             <tr>
                                <td><b>DATA PROCESSING - Last Updated</b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Data Processing Schedule</td>
                              <td>:</td>
                              <td>Monthly, Day 4, Time 05:00</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run Data Processing</button></td>
                            </tr>   
                            <tr>
                              <td>Data Processing Running Mode</td>
                              <td>:</td>
                              <td>Manual</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>
                              
                             <tr>
                                <td><b>COURTESY VISIT - Last Updated</b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Courtesy Visit Schedule</td>
                              <td>:</td>
                              <td>Monthly, Day 4, Time 05:00</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run Courtesy Visit</button></td>
                            </tr>   
                            <tr>
                              <td>Courtesy Visit Running Mode</td>
                              <td>:</td>
                              <td>Manual</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>
                               
                            <tr>
                                <td><b>DATA MONTHLY - Last Updated</b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Data Monthly Schedule</td>
                              <td>:</td>
                              <td>Monthly, Day 4, Time 05:00</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run Monthly Processing</button></td>
                            </tr>   
                            <tr>
                              <td>Data Monthly Running Mode</td>
                              <td>:</td>
                              <td>Manual</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr> 
                              
                             <tr>
                                <td><b>ARCHIVE - Last Updated </b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Archive Location</td>
                              <td>:</td>
                              <td>D:\Data\ARCHIVE</td>
                              <td>&nbsp;</td>
                              <td>Archive Agent Activity History older than</td>
                              <td>:</td>
                              <td>12 Month</td>
                            </tr>   
                            <tr>
                              <td>Archive Schedule</td>
                              <td>:</td>
                              <td>Monthly, Day 3, Time 11:46</td>
                              <td>&nbsp;</td>
                              <td>Archive Closed Task List older than</td>
                              <td>:</td>
                              <td>12 Month</td>
                            </tr> 
                            <tr>
                              <td>Archive Summary-Table older than</td>
                              <td>:</td>
                              <td>12 Month</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run Archive</button></td>
                            </tr>   
                            <tr>
                              <td>Archive Log History older than</td>
                              <td>:</td>
                              <td>12 Month</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>
                              
                            <tr>
                                <td><b>EOD - Last Updated</b></td>
                                <td>:</td>
                                <td><span class="label label-danger">May 17 2016 4:20PM</span></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>EOD Schedule</td>
                              <td>:</td>
                              <td>Monthly, Day 4, Time 05:00</td>
                              <td>&nbsp;</td>
                              <td>Manual Run</td>
                              <td>:</td>
                              <td><button type="button" class="btn btn-default btn-sm">Run Data Processing</button></td>
                            </tr>   
                            <tr>
                              <td>EOD Running Mode</td>
                              <td>:</td>
                              <td>Manual</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>
                              
                            <tr>
                                <td><b>USER SETTINGS</b></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>User Inactive Offer</td>
                              <td>:</td>
                              <td>90 Days</td>
                              <td>&nbsp;</td>
                              <td>Wrong Password Limit</td>
                              <td>:</td>
                              <td>3 Times</td>
                            </tr>   
                            
                            <tr>
                                <td><b>CONTACT TIME ANALYSIS REPORT</b></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Path</td>
                              <td>:</td>
                              <td>D:\\Data\\TEL\\</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>
                              
                            <tr>
                                <td><b>UPLOAD REPORT SETTINGS</b></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Library File</td>
                              <td>:</td>
                              <td>C:\Report\upload_report.csv</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>   
                            <tr>
                              <td>Report Folder Location</td>
                              <td>:</td>
                              <td>C:\Report\</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Report Folder Location</td>
                              <td>:</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr> 
                              
                            <tr>
                                <td><b>ERROR NOTIFICATION EMAIL ADDRESS</b></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                              <td>Email Address</td>
                              <td>:</td>
                              <td>iqbal.zuhdi@idxpartners</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                            </tr>                           
                          </table>
                  </div>
                </div>
              </div>
            </div>

</asp:Content>
