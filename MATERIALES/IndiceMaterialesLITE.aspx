<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTHLite.Master" AutoEventWireup="true" CodeBehind="IndiceMateriales.aspx.cs" Inherits="ThermoWeb.MATERIALES.IndiceMateriales" %>

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


    <script type="text/javascript">
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
                location.href = "http://facts4-srv.thermolympic.local/thermogestion/MATERIALES/UbicacionMateriasPrimasLITE.aspx?" + ubicacion[1];
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
        function QRBuscaProdOLD(codigo) {
            const first = codigo.slice(0, 1);
            const first3 = codigo.slice(0, 3);
            if (first3 == "30S") {
                document.getElementById("NUMMaterial").value = codigo.slice(3, 12);
                document.getElementById("BtnBuscaMat").click();
            }
            else if (codigo > 20000000 && codigo < 40000000) {
                document.getElementById("NUMMaterial").value = codigo;
                document.getElementById("BtnBuscaMat").click();
            }
            else if (first == "P") {
                document.getElementById("NUMMaterial").value = codigo.slice(1, 10);
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
        function QRBuscaProd(codigo) {
            //codigo = "[]>E2*06_P21260004_Q402_T231112_1T_V_KE2*11";


            const firstIN = codigo.slice(0, 9);
            const PROD_IDENT = codigo.slice(9, 10);
            if (firstIN == "[]>E2*06_" && PROD_IDENT == "P") {
                document.getElementById("hiddenCodEtiq").value = codigo;
                document.getElementById("NUMMaterial").value = codigo.slice(10, 19).replace("_", "");
                document.getElementById("BtnBuscaMat").click();
            }
            else {
                alert("El código escaneado no es válido. (Código leído: " + codigo + ")");
            }
        }
        window.onload = function () {
            document.getElementById("NUMMaterial").onchange = function CargaEnvíaDatosQR() {
                codigo = document.getElementById("NUMMaterial").value;
                const firstIN = codigo.slice(0, 9);
                const PROD_IDENT = codigo.slice(9, 10);
                if (firstIN == "[]>E2*06_" && PROD_IDENT == "P") {
                    document.getElementById("hiddenCodEtiq").value = codigo;
                    document.getElementById("NUMMaterial").value = codigo.slice(10, 19).replace("_", "");
                    document.getElementById("BtnBuscaMat").click();
                }
                else {

                }

                //TIP: CONFIGURAR LA LECTORA EN TERMINADOR TAB
            }
        }
    </script>
    <!-- Navigation -->
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <div class="container">
            <div class="row mb-2">
                <div class="col-md-12">
                    <div class="shadow h-100 p-3 bg-light border rounded-1 border-secondary mt-2">
                        <img id="img1" class="ms-4" src="../imagenes/logo.png" alt="Thermolympic" runat="server" align="left" vspace="4" />
                        <h1 class="display-5" style="font: bold">SmarTH - Materiales</h1>
                        <p class="lead ms-1">Gestión de procesos y documentación en Thermolympic S.L.</p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">

                    <h1 class="mt-3 ms-4">Busca un material...</h1>
                    <div class="input-group input-group-lg shadow mb-3 bg-white rounded rounded-2" style="width: 100%">
                        <input class="typeahead form-control border border-dark " list="DatalistNUMMaterial" id="NUMMaterial" runat="server" placeholder="Escribe la referencia">

                        <button type="button" class="btn btn-outline-dark" onclick="fully.scanQrCode('Prompt text','javascript:QRBuscaProd(\'$code\');');"><i class="bi bi-phone"></i></button>

                        <button class="btn btn-outline-secondary border border-dark" type="button" id="BtnBuscaMat" runat="server" onserverclick="BuscarMaterial"><i class="bi bi-search ms-4 me-4"></i></button>
                    </div>
                </div>
            </div>
            <div class="row" style="text-align: end">
                <h1 class="mt-2">...o accede a las aplicaciones de materiales</h1>
            </div>
            <div class="row row-cols-4 g-4 mt-1 mb-2">

                <div class="col">
                    <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png)">
                        <asp:HyperLink ID="hyperlink5" class="card-img-top" NavigateUrl="GestionMolinosLITE.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOGestionMolinos.png" runat="server" ImageWidth="100%" />
                        <div class="card-body">
                            <h5 class="card-title">Gestión de reciclado</h5>
                            <p class="card-text">Gestión de molinos y materiales para reciclado</p>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png)">
                        <asp:HyperLink ID="hyperlink2" class="card-img-top" NavigateUrl="..\LOGISTICA\ConsultaAlmacen.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOLOGISTICA.jpg" runat="server" ImageWidth="100%" />
                        <div class="card-body">
                            <h5 class="card-title">Movimientos de almacén</h5>
                            <p class="card-text">Gestión de stocks y movimientos de productos entre almacenes</p>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png); border-color: dimgray">

                        <asp:HyperLink ID="hyperlink4" class="card-img-top" NavigateUrl="PrevisionSecado.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOGranza.jpg" runat="server" ImageWidth="100%" />
                        <div class="card-body">
                            <h5 class="card-title">Secado de materiales</h5>
                            <p class="card-text">Previsión de estufado y tablas de materiales.</p>
                        </div>
                    </div>
                </div>

                <div class="col">
                    <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png); border-color: dimgray">
                        <button type="button" class="btn btn-outline-dark border-top-0 border-start-0 border-end-0 shadow" style="width: 100%; font-size: 30px; border-bottom-left-radius: 0; border-bottom-right-radius: 0" onclick="fully.scanQrCode('Prompt text','javascript:InsertarQR(\'$code\');');"><i class="bi bi-qr-code-scan"></i></button>

                        <asp:HyperLink ID="hyperlink1" class="card-img-top" NavigateUrl="UbicacionMateriasPrimasLITE.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOUbiMateriales.png" runat="server" ImageWidth="100%" />
                        <div class="card-body">
                            <h5 class="card-title">Ubicaciones materiales</h5>
                            <p class="card-text">Consulta de estanterías y búsqueda de materiales.</p>
                        </div>
                    </div>
                </div>
                <div class="col-1"></div>
            </div>
        </div>
    </div>
    <button class="btn btn-primary" hidden="hidden" id="BtnOffCanvas" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasMateriales" aria-controls="offcanvasRight">Toggle right offcanvas</button>

    <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasMateriales" aria-labelledby="offcanvasRightLabel">
        <div class="offcanvas-header shadow-sm" style="background-color: lightgray">
            <h4 class="offcanvas-title" id="offcanvasRightLabel">Thermolympic</h4>
            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>


        </div>
        <div class="offcanvas-body">
            <asp:Label ID="lblProducto" CssClass="h5" runat="server">--</asp:Label>
            <asp:HiddenField ID="hiddenCodEtiq" runat="server" />
            <div class="rounded rounded-1 mt-2 border border-dark shadow" runat="server" id="DivContenidoEtiqueta" style="background: url(LOGOFONDOTHERMO.png) right top no-repeat; background-size:150px ">
                <label class="h6 m-2">Nº Pedido: &nbsp</label><asp:Label ID="lblPedido" Font-Italic="true" runat="server">--</asp:Label>
                <br />
                <label class="h6 m-2">Lote/Lote Cliente:</label><asp:Label ID="lblLote" Font-Italic="true" runat="server">--</asp:Label>
                <br />
                <label class="h6 m-2">Cantidad unitaria:</label><asp:Label ID="lblCantidadUd" Font-Italic="true" runat="server">--</asp:Label>
                <br />
                <label class="h6 m-2">Proveedor:</label><asp:Label ID="lblProveedor" Font-Italic="true" runat="server">--</asp:Label>
            </div>
            <p class="mt-3">
                <asp:Label ID="lblCantidadDisponible" runat="server" Font-Bold="true">0</asp:Label>
                kgs./Uds. disponibles</p>
            <p><strong>Próxima entrada:&nbsp</strong><asp:Label ID="LblProximaEntrada" runat="server">No hay entradas previstas</asp:Label></p>
            <div class="card">
                <div class="card-header">
                    <h5>Disponible en:</h5>
                </div>
                <asp:GridView ID="GridUbicacion" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table" OnRowCommand="GridCommandEventHandler" OnRowDataBound="GridView_RowDataBound" AutoGenerateColumns="false" ShowHeader="false" EmptyDataText="No disponible">
                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                    <RowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />

                    <Columns>
                        <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <%-- 
                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                    </asp:LinkButton>
                                --%>
                                <div class="input-group">

                                    <label>&nbsp</label>
                                    <asp:Button CssClass="btn btn-sm btn-dark" ID="Label1" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Estanteria") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                    <label style="padding-top: 7px; font-weight: bold" cssclass="btn btn-sm btn-dark me-1" backcolor="Black" forecolor="White" font-bold="true">&nbsp.&nbsp</label>
                                    <asp:Button CssClass="btn btn-sm btn-dark" ID="Label2" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Modulo") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                    <label style="padding-top: 7px; font-weight: bold" cssclass="btn btn-sm btn-dark me-1" backcolor="Black" forecolor="White" font-bold="true">&nbsp.&nbsp</label>
                                    <asp:Button CssClass="btn btn-sm btn-dark" ID="Label3" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Balda") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                    <label>&nbsp</label>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblLote" runat="server" Text='<%#"<strong>Lote: </strong>" + Eval("Lote") %>' />
                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#"<br />(Ubicado el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="card mt-2" id="CardUltimasCon" runat="server" visible="false">
                <div class="card-header">
                    <h5>Últimas ubicaciones conocidas:</h5>
                </div>
                <asp:GridView ID="GridUltimaUbicacion" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table" OnRowCommand="GridCommandEventHandler" AutoGenerateColumns="false" ShowHeader="false" EmptyDataText="Material sin ubicación definida">
                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                    <RowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />

                    <Columns>
                        <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                            <ItemTemplate>

                                <div class="input-group">

                                    <label>&nbsp</label>
                                    <asp:Button CssClass="btn btn-sm btn-dark" ID="Label1" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Estanteria") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                    <label style="padding-top: 7px; font-weight: bold" cssclass="btn btn-sm btn-dark me-1" backcolor="Black" forecolor="White" font-bold="true">&nbsp.&nbsp</label>
                                    <asp:Button CssClass="btn btn-sm btn-dark" ID="Label2" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Modulo") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                    <label style="padding-top: 7px; font-weight: bold" cssclass="btn btn-sm btn-dark me-1" backcolor="Black" forecolor="White" font-bold="true">&nbsp.&nbsp</label>
                                    <asp:Button CssClass="btn btn-sm btn-dark" ID="Label3" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Balda") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                    <label>&nbsp</label>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%#Eval("OCUPADO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
