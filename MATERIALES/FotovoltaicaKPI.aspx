<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTHLite.Master" AutoEventWireup="true" CodeBehind="FotovoltaicaKPI.aspx.cs" Inherits="ThermoWeb.MATERIALES.FOTOVOLTAICA" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>SmarTH</title>
    <link rel="shortcut icon" type="image/x-icon" href="../FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Accesos
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">  
    <script  type="text/javascript">
        $(function () {
            $("#NUMMaterial").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../AutoCompleteServicio.asmx/GetAutoCompleteListaMateriales", // Ruta al método web de servidor
                        data: JSON.stringify({ term: request.term }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (xhr, status, error) {
                            console.log(xhr.responseText);
                        }
                    });
                },
                minLength: 2 // Longitud mínima para activar el autocompletado
            });
        });
    </script>
    <script type="text/javascript">
        function ShowPopDocVinculados() {
            document.getElementById("BtnOffCanvas").click();
        }
        function InsertarQR(codigo) {

            const first3 = codigo.slice(0, 18);
            if (first3 == "http://facts4-srv/") {
                const ubicacion = codigo.split("?");
                location.href = "http://facts4-srv/thermogestion/MATERIALES/UbicacionMateriasPrimasLITE.aspx?" + ubicacion[1];
                //alert("http://facts4-srv/thermogestion/MATERIALES/UbicacionMateriasPrimasLITE.aspx?"+ ubicacion[1]);
                //document.getElementById("NUMMaterial").value = codigo.slice(3, 12);
            }
            else {
                alert("El código escaneado no es válido. (Código leído: " + first3 + ")");
            }

            //console.log(first2); // Co
            //const first6 = str.slice(0, 6);
            //console.log(first6); // Coding
            //const first8 = str.slice(0, 8);
            //console.log(first8); // Coding B
            //alert(codigo);
        }
        function QRBuscaProd(codigo) {

            const first3 = codigo.slice(0, 3);
            if (first3 == "30S") {
                document.getElementById("NUMMaterial").value = codigo.slice(3, 12);
                document.getElementById("BtnBuscaMat").click();
            }
            else {
                alert("El código escaneado no es válido. (Código leído: " + codigo + ")");
            }

            //console.log(first2); // Co
            //const first6 = str.slice(0, 6);
            //console.log(first6); // Coding
            //const first8 = str.slice(0, 8);
            //console.log(first8); // Coding B
            //alert(codigo);
        }
    </script>
    <!-- Navigation -->
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
    <div class="container">
       <iframe width="100%" height="100%" translate="YES" src="http://10.0.0.82/page/index.php?s=registro"></iframe>
    </div>
        </div>
    
</asp:Content>
