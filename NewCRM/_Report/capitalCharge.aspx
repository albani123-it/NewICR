<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="capitalCharge.aspx.vb" Inherits="NewCRM.capitalCharge" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="clear: both; margin: 0 auto; padding: 0; width: 98%">
        <form>
            <div class="row">
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="month">Bulan</label>
                            <select class="form-control" id="month">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Januari</option>
                                <option value="2">Februari</option>
                                <option value="3">Maret</option>
                                <option value="4">April</option>
                                <option value="5">Mei</option>
                                <option value="6">Juni</option>
                                <option value="7">Juli</option>
                                <option value="8">Agustus</option>
                                <option value="9">September</option>
                                <option value="10">Oktober</option>
                                <option value="11">November</option>
                                <option value="12">Desember</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="year">Tahun</label>
                            <select class="form-control" id="year">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="2021">2021</option>
                                <option value="2022">2022</option>
                                <option value="2023">2023</option>
                                <option value="2024">2024</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="asset">Asset</label>
                            <select class="form-control" id="asset">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="segment">Segment</label>
                            <select class="form-control" id="segment">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="product">Product</label>
                            <select class="form-control" id="product">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="sector">Sector</label>
                            <select class="form-control" id="sector">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="branch">Branch</label>
                            <select class="form-control" id="branch">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="rating">Rating</label>
                            <select class="form-control" id="rating">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-6">
                    <div class="col-md-6 mb-6">
                        <div class="row">
                            <label for="kolek">Kolek</label>
                            <select class="form-control" id="kolek">
                                <option selected disabled value="">(All - Show)</option>
                                <option value="1">Test</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
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
        <div id="myChart" style="width: 100%; height: 400px;">
            <div id="titleMenu">
                <h2>Laporan Capital Charge</h2>
            </div>
            <img src="../Images/line-capital-charge.png" />
        </div>
    </div>
</asp:Content>
