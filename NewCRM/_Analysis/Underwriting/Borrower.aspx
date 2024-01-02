<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Borrower.aspx.vb" Inherits="NewCRM.Borrower" %>

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


        function fDetail(url){
            var appno = document.getElementById("<%=appno.clientID %>").value;
            if (appno == "")
                window.location.href = url;
            else
                window.location.href = url +"?appno=" + appno;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;

            return true;
        }

        function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
            }
            else{
                if (isNaN(num))
                    num = "";
                sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num);
            }
            
        }

        function checkOffice(){
            
            if (document.getElementById("<%=chkAlamatRumah.clientID %>").checked == true){
                document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").value = document.getElementById("<%=txtAlamatRumah.clientID %>").value;
                document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").value = document.getElementById("<%=txtKodePosRumah.clientID %>").value;
                document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").value = document.getElementById("<%=txtKelurahanRumah.clientID %>").value;
                document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").value = document.getElementById("<%=txtKecamatanRumah.clientID %>").value;
                document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").value = document.getElementById("<%=txtKotamadyaKabupatenKantor.clientID %>").value;
                document.getElementById("<%=txtPropinsiUsaha.clientID %>").value = document.getElementById("<%=txtPropinsiRumah.clientID %>").value;
                document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").value = document.getElementById("<%=txtKodeAreaTlpRumah.clientID %>").value;
                
                document.getElementById("<%=txtTlpUsaha.clientID %>").value = document.getElementById("<%=txtTlpRumah.clientID %>").value;
                document.getElementById("<%=txtExtTlpUsaha.clientID %>").value = document.getElementById("<%=txtExtTlpRumah.clientID %>").value;
                document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").value = document.getElementById("<%=txtKodeAreaTlpRumah2.clientID %>").value;
                document.getElementById("<%=txtTlpUsaha2.clientID %>").value=document.getElementById("<%=txtTlpRumah2.clientID %>").value;
                document.getElementById("<%=txtExtTlpUsaha2.clientID %>").value = document.getElementById("<%=txtExtTlpRumah2.clientID %>").value;
                document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").value =document.getElementById("<%=txtKodeAreaTlpRumah3.clientID %>").value;
                document.getElementById("<%=txtTlpUsaha3.clientID %>").value =document.getElementById("<%=txtTlpRumah3.clientID %>").value;
                document.getElementById("<%=txtExtTlpUsaha3.clientID %>").value = document.getElementById("<%=txtExtTlpRumah3.clientID %>").value;
                document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").value = document.getElementById("<%=txtKodeAreaFaxRumah.clientID %>").value;
                document.getElementById("<%=txtFaxUsaha.clientID %>").value =document.getElementById("<%=txtFaxRumah.clientID %>").value;
                document.getElementById("<%=txtExtFaxUsaha.clientID %>").value = document.getElementById("<%=txtExtFaxRumah.clientID %>").value;

                document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtPropinsiUsaha.clientID %>").readOnly = true;

                document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtTlpUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtExtTlpUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").readOnly = true;
                document.getElementById("<%=txtTlpUsaha2.clientID %>").readOnly = true;
                document.getElementById("<%=txtExtTlpUsaha2.clientID %>").readOnly = true;
                document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").readOnly = true;
                document.getElementById("<%=txtTlpUsaha3.clientID %>").readOnly = true;
                document.getElementById("<%=txtExtTlpUsaha3.clientID %>").readOnly = true;
                document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtFaxUsaha.clientID %>").readOnly = true;
                document.getElementById("<%=txtExtFaxUsaha.clientID %>").readOnly = true;

                document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtPropinsiUsaha.clientID %>").className = "inpTxtDisabled";

                document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtTlpUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtExtTlpUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtTlpUsaha2.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtExtTlpUsaha2.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtTlpUsaha3.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtExtTlpUsaha3.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtFaxUsaha.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtExtFaxUsaha.clientID %>").className = "inpTxtDisabled";
            }
            if (document.getElementById("<%=chkAlamatKantorKTP.clientID %>").checked == true){
                document.getElementById("<%=txtAlamatSurat.clientID %>").value = document.getElementById("<%=txtAlamatRumah.clientID %>").value;
                document.getElementById("<%=txtKodePosSurat.clientID %>").value = document.getElementById("<%=txtKodePosRumah.clientID %>").value;
                document.getElementById("<%=txtKelurahanSurat.clientID %>").value = document.getElementById("<%=txtKelurahanRumah.clientID %>").value;
                document.getElementById("<%=txtKecamatanSurat.clientID %>").value = document.getElementById("<%=txtKecamatanRumah.clientID %>").value;
                document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").value = document.getElementById("<%=txtKotamadyaKabupatenKantor.clientID %>").value;
                document.getElementById("<%=txtPropinsiSurat.clientID %>").value = document.getElementById("<%=txtPropinsiRumah.clientID %>").value;
                document.getElementById("<%=txtKodeAreaTlpSurat.clientID %>").value = document.getElementById("<%=txtKodeAreaTlpRumah.clientID %>").value;
                document.getElementById("<%=txtTlpSurat.clientID %>").value = document.getElementById("<%=txtTlpRumah.clientID %>").value;
                document.getElementById("<%=txtExtTlpSurat.clientID %>").value = document.getElementById("<%=txtExtTlpRumah.clientID %>").value;

                document.getElementById("<%=txtAlamatSurat.clientID %>").readOnly = true;
                document.getElementById("<%=txtKodePosSurat.clientID %>").readOnly = true;
                document.getElementById("<%=txtKelurahanSurat.clientID %>").readOnly = true;
                document.getElementById("<%=txtKecamatanSurat.clientID %>").readOnly = true;
                document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").readOnly = true;
                document.getElementById("<%=txtPropinsiSurat.clientID %>").readOnly = true;
                
                document.getElementById("<%=txtAlamatSurat.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKodePosSurat.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKelurahanSurat.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKecamatanSurat.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").className = "inpTxtDisabled";
                document.getElementById("<%=txtPropinsiSurat.clientID %>").className = "inpTxtDisabled";
            }
        }

        function checkSama(x){
            if(x=="rumah"){
               
                if (document.getElementById("<%=chkAlamatRumah.clientID %>").checked == true){
                    document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").value = document.getElementById("<%=txtAlamatRumah.clientID %>").value;
                    document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").value = document.getElementById("<%=txtKodePosRumah.clientID %>").value;
                    document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").value = document.getElementById("<%=txtKelurahanRumah.clientID %>").value;
                    document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").value = document.getElementById("<%=txtKecamatanRumah.clientID %>").value;
                    document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").value = document.getElementById("<%=txtKotamadyaKabupatenKantor.clientID %>").value;
                    document.getElementById("<%=txtPropinsiUsaha.clientID %>").value = document.getElementById("<%=txtPropinsiRumah.clientID %>").value;

                    document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").value = document.getElementById("<%=txtKodeAreaTlpRumah.clientID %>").value;
                    document.getElementById("<%=txtTlpUsaha.clientID %>").value = document.getElementById("<%=txtTlpRumah.clientID %>").value;
                    document.getElementById("<%=txtExtTlpUsaha.clientID %>").value = document.getElementById("<%=txtExtTlpRumah.clientID %>").value;
                    document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").value = document.getElementById("<%=txtKodeAreaTlpRumah2.clientID %>").value;
                    document.getElementById("<%=txtTlpUsaha2.clientID %>").value=document.getElementById("<%=txtTlpRumah2.clientID %>").value;
                    document.getElementById("<%=txtExtTlpUsaha2.clientID %>").value = document.getElementById("<%=txtExtTlpRumah2.clientID %>").value;
                    document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").value =document.getElementById("<%=txtKodeAreaTlpRumah3.clientID %>").value;
                    document.getElementById("<%=txtTlpUsaha3.clientID %>").value =document.getElementById("<%=txtTlpRumah3.clientID %>").value;
                    document.getElementById("<%=txtExtTlpUsaha3.clientID %>").value = document.getElementById("<%=txtExtTlpRumah3.clientID %>").value;
                    document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").value = document.getElementById("<%=txtKodeAreaFaxRumah.clientID %>").value;
                    document.getElementById("<%=txtFaxUsaha.clientID %>").value =document.getElementById("<%=txtFaxRumah.clientID %>").value;
                    document.getElementById("<%=txtExtFaxUsaha.clientID %>").value = document.getElementById("<%=txtExtFaxRumah.clientID %>").value;

                    document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").readonly = true;
                    document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtPropinsiUsaha.clientID %>").readOnly = true;

                    document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtTlpUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtExtTlpUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").readOnly = true;
                    document.getElementById("<%=txtTlpUsaha2.clientID %>").readOnly = true;
                    document.getElementById("<%=txtExtTlpUsaha2.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").readOnly = true;
                    document.getElementById("<%=txtTlpUsaha3.clientID %>").readOnly = true;
                    document.getElementById("<%=txtExtTlpUsaha3.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtFaxUsaha.clientID %>").readOnly = true;
                    document.getElementById("<%=txtExtFaxUsaha.clientID %>").readOnly = true;

                    document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtPropinsiUsaha.clientID %>").className = "inpTxtDisabled";

                    document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtTlpUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtExtTlpUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtTlpUsaha2.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtExtTlpUsaha2.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtTlpUsaha3.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtExtTlpUsaha3.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtFaxUsaha.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtExtFaxUsaha.clientID %>").className = "inpTxtDisabled";
                }
                else{
                    document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtPropinsiUsaha.clientID %>").value = "";

                    document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtTlpUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtExtTlpUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").value = "";
                    document.getElementById("<%=txtTlpUsaha2.clientID %>").value = "";
                    document.getElementById("<%=txtExtTlpUsaha2.clientID %>").value = "";
                    document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").value = "";
                    document.getElementById("<%=txtTlpUsaha3.clientID %>").value = "";
                    document.getElementById("<%=txtExtTlpUsaha3.clientID %>").value = "";
                    document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtFaxUsaha.clientID %>").value = "";
                    document.getElementById("<%=txtExtFaxUsaha.clientID %>").value = "";

                    document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtPropinsiUsaha.clientID %>").readOnly = false;

                    document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtTlpUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtExtTlpUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").readOnly = false;
                    document.getElementById("<%=txtTlpUsaha2.clientID %>").readOnly = false;
                    document.getElementById("<%=txtExtTlpUsaha2.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").readOnly = false;
                    document.getElementById("<%=txtTlpUsaha3.clientID %>").readOnly = false;
                    document.getElementById("<%=txtExtTlpUsaha3.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtFaxUsaha.clientID %>").readOnly = false;
                    document.getElementById("<%=txtExtFaxUsaha.clientID %>").readOnly = false;

                    document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtPropinsiUsaha.clientID %>").className = "inpTxt";

                    document.getElementById("<%=txtKodeAreaTlpUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtTlpUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtExtTlpUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKodeAreaTlpUsaha2.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtTlpUsaha2.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtExtTlpUsaha2.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKodeAreaTlpUsaha3.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtTlpUsaha3.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtExtTlpUsaha3.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKodeAreaFaxUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtFaxUsaha.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtExtFaxUsaha.clientID %>").className = "inpTxt";
                }
            }
            else if(x=="ktp"){
                if (document.getElementById("<%=chkAlamatKantorKTP.clientID %>").checked == true){
                    document.getElementById("<%=txtAlamatSurat.clientID %>").value = document.getElementById("<%=txtAlamatRumah.clientID %>").value;
                    document.getElementById("<%=txtKodePosSurat.clientID %>").value = document.getElementById("<%=txtKodePosRumah.clientID %>").value;
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").value = document.getElementById("<%=txtKelurahanRumah.clientID %>").value;
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").value = document.getElementById("<%=txtKecamatanRumah.clientID %>").value;
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").value = document.getElementById("<%=txtKotamadyaKabupatenKantor.clientID %>").value;
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").value = document.getElementById("<%=txtPropinsiRumah.clientID %>").value;
                    document.getElementById("<%=txtKodeAreaTlpSurat.clientID %>").value = document.getElementById("<%=txtKodeAreaTlpRumah.clientID %>").value;
                    document.getElementById("<%=txtTlpSurat.clientID %>").value = document.getElementById("<%=txtTlpRumah.clientID %>").value;
                    document.getElementById("<%=txtExtTlpSurat.clientID %>").value = document.getElementById("<%=txtExtTlpRumah.clientID %>").value;
                    
                    document.getElementById("<%=chkAlamatUsaha.clientID %>").checked = false;

                    document.getElementById("<%=txtAlamatSurat.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKodePosSurat.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").readOnly = true;
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").readOnly = true;
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").readOnly = true;

                    document.getElementById("<%=txtAlamatSurat.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKodePosSurat.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").className = "inpTxtDisabled";
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").className = "inpTxtDisabled";
                }
                else{
                    document.getElementById("<%=txtAlamatSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKodePosSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").value = "";
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKodeAreaTlpSurat.clientID %>").value = "";
                    document.getElementById("<%=txtTlpSurat.clientID %>").value = "";
                    document.getElementById("<%=txtExtTlpSurat.clientID %>").value = "";

                    document.getElementById("<%=txtAlamatSurat.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKodePosSurat.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").readOnly = false;
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").readOnly = false;
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").readOnly = false;

                    document.getElementById("<%=txtAlamatSurat.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKodePosSurat.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").className = "inpTxt";
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").className = "inpTxt";
                }
            }
            else if(x=="usaha"){
                if (document.getElementById("<%=chkAlamatUsaha.clientID %>").checked == true){
                    document.getElementById("<%=txtAlamatSurat.clientID %>").value = document.getElementById("<%=txtAlamatProyekUsaha.clientID %>").value;
                    document.getElementById("<%=txtKodePosSurat.clientID %>").value = document.getElementById("<%=txtKodePosProyekUsaha.clientID %>").value;
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").value = document.getElementById("<%=txtKelurahanProyekUsaha.clientID %>").value;
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").value = document.getElementById("<%=txtKecamatanProyekUsaha.clientID %>").value;
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").value = document.getElementById("<%=txtKotamadyaKabupatenUsaha.clientID %>").value;
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").value = document.getElementById("<%=txtPropinsiUsaha.clientID %>").value;

                    document.getElementById("<%=chkAlamatKantorKTP.clientID %>").checked = false;
                }
                else{
                    document.getElementById("<%=txtAlamatSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKodePosSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKelurahanSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKecamatanSurat.clientID %>").value = "";
                    document.getElementById("<%=txtKotamadyaKabupatenSurat.clientID %>").value = "";
                    document.getElementById("<%=txtPropinsiSurat.clientID %>").value = "";
                }
            }
            return true;
        }
        $(function() {
            $("#<%=txtTglLahir.clientID %>").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtTglPembuatanKTPAkte.clientID %>").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtTglPenerimaanBerkas.clientID %>").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtMenjadiDebiturSejak.clientID %>").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtTglPermohonanDebitur.clientID %>").datepicker(
            { duration: '',
                yearRange: "-100:+0",
                constrainInput: true,
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true
            });

            function formatCurrency(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (num == "-"){
                return num;
            }
            else{
                if (isNaN(num))
                    num = "";
                sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num);
            }
            
        }

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
                    <table width="100%" border="0">
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
                        <table width="100%">
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
                <div>
                    <table class="tbl" cellspacing="0" cellpadding="3" width="100%" id="tblUploadFile" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px 5px 0 0; ">
                        <tr>
                        <td colspan="2" background="../../Images/bg-head.gif" bgcolor="#ddedf6" class="tb-header">UPLOAD FILE FIN</td>
                    </tr>
                        <tr bgcolor="#f6f7f7">
                            <td width="30%">
                                <b>Upload File Report FIN</b>
                            </td>
                            <td width="70%">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <br />
                                <span>Hanya File PDF yang boleh di upload.</span><br />
                            </td>
                        </tr>
                        <tr bgcolor="#ebecec">
                            <td>
                                <b>&nbsp;</b>
                            </td>
                            <td>
                               <asp:Label ID="lblFileUpload" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#f6f7f7">
                            <td>
                                <b>&nbsp;</b>
                            </td>
                            <td>
                               <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="inpBtn"></asp:Button>
                            </td>
                        </tr>
                        <%--<tr style="height:30px">
				        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:button id="Button1" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn"></asp:button>
                            <asp:button id="Button2" runat="server" Text="Lanjut" CssClass="inpBtn" style="display:none;"></asp:button>
                            <input type="button" id="btnLanjut" value="Lanjut" onclick="fDetail('<%= menuNext %>');" class="inpBtn" />
                            <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                        </td>
			        </tr>--%>
                    </table>
                    
                </div>
                <table class="tbl" cellspacing="0" cellpadding="2" width="100%" style="text-align:left;border: thin solid #bcbcbc;border-radius: 5px;">
                    <tr>
                        <td colspan="4" background="../../Images/bg-head.gif" bgcolor="#ddedf6" style="border-radius: 5px 5px 0 0;">&nbsp;</td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td width="20%"><b>No. CIF</b></td>
                        <td width="30%">
                            <asp:TextBox ID="txtCIF" runat="server" CssClass="inpTxt" style="float:none;"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnLook" runat="server" CssClass="inpBtn" Text="Lookup" Visible="false" />
                        </td>
                        <td width="20%"><b>No. Rekening</b></td>
                        <td width="30%"><asp:TextBox ID="txtAcctno" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Jenis Debitur</b></td>
                        <td>
                            <asp:DropDownList ID="ddlJenisDebitur" runat="server" CssClass="inpDdl" readonly="true">
                                <asp:ListItem Value="Pilih">Pilih</asp:ListItem>
                                <asp:ListItem Value="Badan Usaha">Badan Usaha</asp:ListItem>
                            </asp:DropDownList>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                        </td>
                        <td><b>Kode Borrower</b></td>
                        <td><b><asp:label ID="lblBrwCode" runat="server"></asp:label></b></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Nama Pemohon</b></td>
                        <td>
                            <asp:TextBox ID="txtNamaPemohon" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                        </td>
                        <td><b>Deskripsi</b></td>
                        <td><asp:TextBox ID="txtDeskripsi" runat="server" CssClass="inpTxt" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>NPWP Pemohon</b></td>
                        <td><asp:TextBox ID="txtNPWP" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                        <td><b>Sektor Ekonomi LBU</b></td>
                        <td>
                            <asp:DropDownList ID="ddlSektorEkonomi" runat="server" CssClass="inpDdl" style="width:200px;"></asp:DropDownList>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Berdiri Sejak</b></td>
                        <td>
                            <asp:TextBox ID="txtTglLahir" name="txtTglLahir" runat="server" CssClass="inpTxt" ReadOnly="true" EnableViewState= "true" style="float:none;"></asp:TextBox><span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                        <%--<td><b>Sub Sektor</b></td>
                        <td><asp:DropDownList ID="ddlSubSektor" runat="server" CssClass="inpDdl"></asp:DropDownList></td>--%>
                        <td><b>Daerah/Area Pemasaran</b></td>
                        <td><asp:TextBox ID="txtDaerahPemasaran" runat="server" CssClass="inpTxt" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>No. Akte Pendirian</b></td>
                        <td>
                            <asp:TextBox ID="txtNoKTPAkte" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                        </td>
                        <td><b>Sasaran Pembeli</b></td>
                        <td><asp:TextBox ID="txtSasaranPembeli" runat="server" CssClass="inpTxt" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Tanggal Pembuatan Akte Pendirian</b></td>
                        <td>
                            <asp:TextBox ID="txtTglPembuatanKTPAkte" runat="server" CssClass="inpTxt" ReadOnly="true" style="float:none;"></asp:TextBox><span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                        <td><b>Jumlah Karyawan</b></td>
                        <td><asp:TextBox ID="txtJumlahKaryawan" runat="server" CssClass="inpTxt" style="text-align:right" value="0" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Alamat Kantor</b></td>
                        <td>
                            <asp:TextBox ID="txtAlamatRumah" runat="server" CssClass="inpTxt" Width="250px" Height="40px" TextMode="MultiLine" onkeyup="checkOffice();"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                        </td>
                        <td><b>Hubungan dengan Bank Sejak</b></td>
                        <td>
                            Tahun&nbsp;<asp:DropDownList ID="ddlTahunHubunganDenganBTN" runat="server" CssClass="inpDdl"></asp:DropDownList>
                            Bulan&nbsp;<asp:DropDownList ID="ddlBulanHubunganDenganBTN" runat="server" CssClass="inpDdl"></asp:DropDownList>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kode Pos</b></td>
                        <td><asp:TextBox ID="txtKodePosRumah" runat="server" CssClass="inpTxt" onkeyup="checkOffice();"></asp:TextBox></td>
                        <td><b>Menjadi Debitur Sejak</b></td>
                        <td>
                            <asp:TextBox ID="txtMenjadiDebiturSejak" runat="server" ReadOnly="true" CssClass="inpTxt" style="float:none;"></asp:TextBox><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kelurahan</b></td>
                        <td><asp:TextBox ID="txtKelurahanRumah" runat="server" CssClass="inpTxt" style="width:200px;" onkeyup="checkOffice();"></asp:TextBox></td>
                        <td><b>No. Permohonan Debitur</b></td>
                        <td>
                            <asp:TextBox ID="txtNoPermohonanDebitur" runat="server" CssClass="inpTxt"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kecamatan</b></td>
                        <td><asp:TextBox ID="txtKecamatanRumah" runat="server" CssClass="inpTxt" style="width:200px;" onkeyup="checkOffice();"></asp:TextBox></td>
                        <td><b>Tanggal Permohonan Debitur</b></td>
                        <td>
                            <asp:TextBox ID="txtTglPermohonanDebitur" runat="server" ReadOnly="true" CssClass="inpTxt" style="float:none;" onkeyup="checkOffice();"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kotamadya/Kabupaten</b></td>
                        <td><asp:TextBox ID="txtKotamadyaKabupatenKantor" runat="server"  CssClass="inpTxt" style="width:200px;" onkeyup="checkOffice();"></asp:TextBox></td>
                        <td><b>Tanggal Penerimaan Berkas</b></td>
                        <td>
                            <asp:TextBox ID="txtTglPenerimaanBerkas" runat="server" ReadOnly="true" CssClass="inpTxt" style="float:none;"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">&nbsp;*</span><br />
                            <span style="color:Red;margin-top:2px;float:left;">(dd-mm-yyyy)</span>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Propinsi</b></td>
                        <td><asp:TextBox ID="txtPropinsiRumah" runat="server" CssClass="inpTxt" style="width:200px;" onkeyup="checkOffice();"></asp:TextBox></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>No. Telepon Kantor</b></td>
                        <td>
                            <asp:TextBox ID="txtKodeAreaTlpRumah" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="margin-right:3px;float:none;margin-bottom:3px;" onkeyup="checkOffice();" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtTlpRumah" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;" onkeyup="checkOffice();"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpRumah" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;" onkeyup="checkOffice();"></asp:TextBox>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">*</span>
                            
                            <asp:TextBox ID="txtKodeAreaTlpRumah2" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeyup="checkOffice();" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtTlpRumah2" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;" onkeyup="checkOffice();"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpRumah2" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;"></asp:TextBox>
                            
                            <asp:TextBox ID="txtKodeAreaTlpRumah3" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeyup="checkOffice();" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtTlpRumah3" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;" onkeyup="checkOffice();"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpRumah3" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;" onkeyup="checkOffice();"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>No. Fax Kantor</b></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtKodeAreaFaxRumah" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtFaxRumah" runat="server" CssClass="inpTxt" style="margin-right:3px;float:none;margin-bottom:3px;"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtFaxRumah" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;margin-bottom:3px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Nama Proyek/Usaha</b></td>
                        <td colspan="3"><asp:TextBox ID="txtNamaProyekUsaha" runat="server" CssClass="inpTxt" style="width:250px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Alamat Proyek/Usaha</b></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAlamatProyekUsaha" runat="server" CssClass="inpTxt" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                            &nbsp;<input type="checkbox" id="chkAlamatRumah" runat="server" onclick="return checkSama('rumah');" /><b>Sama seperti Alamat Kantor</b>
                        </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kode Pos</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKodePosProyekUsaha" CssClass="inpTxt" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kelurahan</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKelurahanProyekUsaha" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kecamatan</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKecamatanProyekUsaha" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kotamadya/Kabupaten Usaha</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKotamadyaKabupatenUsaha" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Propinsi</b></td>
                        <td colspan="3"><asp:TextBox ID="txtPropinsiUsaha" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>No. Telepon Usaha</b></td>
                        <td>
                            <asp:TextBox ID="txtKodeAreaTlpUsaha" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="margin-right:3px;float:none;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtTlpUsaha" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpUsaha" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;"></asp:TextBox>
                            
                            <asp:TextBox ID="txtKodeAreaTlpUsaha2" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtTlpUsaha2" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpUsaha2" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;"></asp:TextBox>
                            
                            <asp:TextBox ID="txtKodeAreaTlpUsaha3" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtTlpUsaha3" runat="server" CssClass="inpTxt" style="float:none;margin-right:3px;margin-bottom:3px;"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpUsaha3" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>No. Fax Usaha</b></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtKodeAreaFaxUsaha" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:TextBox ID="txtFaxUsaha" runat="server" CssClass="inpTxt" style="margin-right:3px;float:none;margin-bottom:3px;"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtFaxUsaha" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;margin-bottom:3px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Bidang Usaha</b></td>
                        <td colspan="3"><asp:DropDownList ID="ddlBidangUsaha" runat="server" CssClass="inpDdl"></asp:DropDownList></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Grup Usaha</b></td>
                        <td colspan="3"><asp:TextBox ID="txtGrupUsaha" runat="server" CssClass="inpTxt" TextMode="MultiLine" Width="250px" Height="40px"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Lama Usaha</b></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLamaUsaha" runat="server" CssClass="inpTxt" style="width:30px;float:none;" onkeypress="return isNumberKey(event)" onkeyup="this.value=formatCurrency(this.value);" value="0"></asp:TextBox>&nbsp;Tahun
                       </td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Alamat Surat Menyurat</b></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAlamatSurat" runat="server" CssClass="inpTxt" Width="250px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                            &nbsp;<input type="checkbox" id="chkAlamatKantorKTP" runat="server" onclick="return checkSama('ktp');" /><b>Sama seperti Alamat Kantor</b><br />
                            &nbsp;<input type="checkbox" id="chkAlamatUsaha" runat="server" onclick="return checkSama('usaha');" style="display:none;" /><%--<b>Sama seperti Alamat Usaha</b>--%>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kode Pos Surat Menyurat</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKodePosSurat" runat="server" CssClass="inpTxt"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kelurahan Surat Menyurat</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKelurahanSurat" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Kecamatan Surat Menyurat</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKecamatanSurat" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>Kotamadya/Kabupaten Surat Menyurat</b></td>
                        <td colspan="3"><asp:TextBox ID="txtKotamadyaKabupatenSurat" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Propinsi Surat Menyurat</b></td>
                        <td colspan="3"><asp:TextBox ID="txtPropinsiSurat" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr bgcolor="#ebecec">
                        <td><b>No. Telepon Surat Menyurat</b></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtKodeAreaTlpSurat" runat="server" CssClass="inpTxt" Width="30px" MaxLength="3" style="float:none;margin-right:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            <asp:TextBox ID="txtTlpSurat" runat="server" CssClass="inpTxt" style="margin-right:3px;float:none;margin-bottom:3px;" onkeypress="return isNumberKey(event);"></asp:TextBox>
                            <span style="margin-right:3px;font-weight:bold;margin-bottom:3px;">Ext.</span>
                            <asp:TextBox ID="txtExtTlpSurat" runat="server" CssClass="inpTxt" Width="50px" style="float:none;margin-bottom:3px;margin-bottom:3px;" onkeypress="return isNumberKey(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#f6f7f7">
                        <td><b>Email Surat Menyurat</b></td>
                        <td colspan="3"><asp:TextBox ID="txtEmailSurat" runat="server" CssClass="inpTxt" style="width:200px;"></asp:TextBox></td>
                    </tr>
                    <tr style="height:30px">
				        <td background="../../styles/grid/footer.gif" colspan="4" style="border-radius: 0 0 5px 5px;">
                            <asp:button id="btnSave" runat="server" Text="Simpan & Lanjut" CssClass="inpBtn"></asp:button>
                            <asp:button id="btnNext" runat="server" Text="Lanjut" CssClass="inpBtn"></asp:button>
                            <span style="color:Red;font-size:11pt;font-weight:bold;">* <span style="font-size:13px;">Harus Diisi</span></span>
                        </td>
			        </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>