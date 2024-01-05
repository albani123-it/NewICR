<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="brw_rating_detail.aspx.vb" Inherits="NewCRM.brw_rating_detail" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="clear: both; margin: 0 auto; padding: 0; width: 98%">
        <form>
            <div class="row">
                <div class="form-group-sm col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="borrowerCode">Borrower Code</label>
                            <input id="borrowerCode" type="text" class="form-control" />
                        </div>
                    </div>
                </div>
            </div><br />
            <div class="row">
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <button id="btnViewReport" class="btn btn-primary">View Report</button>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div id="detailRatingBorrower" style="width: 50%;">
            <div id="titleMenu" class="bg-primary">
                <h2 class="text-center">Borrower Rating Detail Report</h2>
            </div>
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th style="width: 50%"><span style="font-weight:bold; font-size:small">Borrower Code</span></th>
                        <th style="width: 50%"><span style="font-weight:bold; font-size:small">BR-BKYZ-2018-00001</span></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><span style="font-weight:bold; font-size:small">Summary</span></td>
                    </tr>
                    <tr>
                        <td>Rating Model</td>
                        <td>IIR - Borrower Manufactur</td>
                    </tr>
                    <tr>
                        <td>Score</td>
                        <td>53</td>
                    </tr>
                    <tr>
                        <td>Rating</td>
                        <td>B</td>
                    </tr>
                    <tr>
                        <td>Category</td>
                        <td>B</td>
                    </tr>
                    <tr>
                        <td>Description</td>
                        <td>Debitur berpotensi secara rutin gagal memenuhi sebagian kecil kewajiban pokok, dan bunga, sebagian karakter dan kapasitas individu atau lingkungan di bawah rata-rata, kemungkinan default sedang</td>
                    </tr>
                    <tr>
                        <td>Rating Date</td>
                        <td>21 Feb 2018</td>
                    </tr>
                    <tr>
                        <td>Financial Statement Period</td>
                        <td>31 Mar 2017</td>
                    </tr>
                    <tr>
                        <td>Rating By</td>
                        <td>testing -</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
