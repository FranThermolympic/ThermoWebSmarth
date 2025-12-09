<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTH.Master" AutoEventWireup="true" CodeBehind="IndiceMateriales20240524.aspx.cs" Inherits="ThermoWeb.MATERIALES.IndiceMateriales" %>

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
    </script>
    <!-- Navigation -->
    <div class="container">
        <div class="row mt-2 mb-2">
            <div class="col-md-12">
                <div class="shadow h-100 p-3 bg-light border rounded-1 border-secondary">
                    <img id="img1" class="ms-4" src="../imagenes/logo.png" alt="Thermolympic" runat="server" align="left" vspace="4" />
                    <h1 class="display-5" style="font: bold">SmarTH - Materiales</h1>
                    <p class="lead ms-1">Gestión de procesos y documentación en Thermolympic S.L.</p>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                
                <h1 class="mt-3 ms-4">Busca un material...</h1>
                <div class="input-group input-group-lg shadow mb-3" style="width: 100%">
                    <input class="typeahead form-control border border-dark " list="DatalistNUMMaterial" id="NUMMaterial" runat="server" placeholder="Escribe la referencia">
                    <button class="btn btn-outline-secondary border border-dark" type="button" id="BtnBuscaMat" runat="server" onserverclick="BuscarMaterial"><i class="bi bi-search ms-4 me-4"></i></button>
                </div>
            </div>
        </div>
        <div class="row" style="text-align: end">
            <h1 class="mt-2">...o accede a las aplicaciones de materiales</h1>
        </div>
        <div class="row row-cols-1 row-cols-md-4 g-4 mt-1 mb-2">
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png)">
                    <asp:HyperLink ID="hyperlink5" class="card-img-top" NavigateUrl="GestionMolinos.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOGestionMolinos.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Gestión de reciclado</h5>
                        <p class="card-text">Gestión de molinos y materiales para reciclado</p>
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

                    <asp:HyperLink ID="hyperlink1" class="card-img-top" NavigateUrl="UbicacionMateriasPrimas.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOUbiMateriales.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Ubicaciones materiales</h5>
                        <p class="card-text">Consulta de estanterías y búsqueda de materiales.</p>
                    </div>
                </div>
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
            <p><asp:Label ID="lblCantidadDisponible" runat="server">0</asp:Label> kgs./Uds. disponibles</p>
            <p><strong>Próxima entrada:&nbsp</strong><asp:Label ID="LblProximaEntrada" runat="server">No hay entradas previstas</asp:Label></p>
            <div class="card">
                <div class="card-header">
                    <h5>Disponible en:</h5>
                </div>
                <asp:GridView ID="GridUbicacion" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table" OnRowCommand="GridCommandEventHandler" OnRowDataBound="GridView_RowDataBound" AutoGenerateColumns="false" ShowHeader="false" EmptyDataText="Material sin ubicación definida">
                        <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                        <RowStyle BackColor="White" />
                        <AlternatingRowStyle BackColor="WhiteSmoke"/>
                    
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                <ItemTemplate>
                                    <%-- 
                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                    </asp:LinkButton>
                                    --%>
                                    <div class="input-group">
                                       
                                        <Label>&nbsp</Label>
                                        <asp:Button CssClass="btn btn-sm btn-dark" ID="Label1" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Estanteria") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                        <Label style="padding-top:7px; font-weight:bold" CssClass="btn btn-sm btn-dark me-1" BackColor="Black" ForeColor="White" Font-Bold="true">&nbsp.&nbsp</Label>
                                        <asp:Button CssClass="btn btn-sm btn-dark" ID="Label2" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Modulo") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                        <Label style="padding-top:7px; font-weight:bold" CssClass="btn btn-sm btn-dark me-1"  BackColor="Black" ForeColor="White" Font-Bold="true">&nbsp.&nbsp</Label>
                                        <asp:Button CssClass="btn btn-sm btn-dark" ID="Label3" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Balda") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                        <Label>&nbsp</Label>
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
                        <AlternatingRowStyle BackColor="WhiteSmoke"/>
                    
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                <ItemTemplate>
                                   
                                    <div class="input-group">
                                       
                                        <Label>&nbsp</Label>
                                        <asp:Button CssClass="btn btn-sm btn-dark" ID="Label1" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Estanteria") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                        <Label style="padding-top:7px; font-weight:bold" CssClass="btn btn-sm btn-dark me-1" BackColor="Black" ForeColor="White" Font-Bold="true">&nbsp.&nbsp</Label>
                                        <asp:Button CssClass="btn btn-sm btn-dark" ID="Label2" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Modulo") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                        <Label style="padding-top:7px; font-weight:bold" CssClass="btn btn-sm btn-dark me-1"  BackColor="Black" ForeColor="White" Font-Bold="true">&nbsp.&nbsp</Label>
                                        <asp:Button CssClass="btn btn-sm btn-dark" ID="Label3" BackColor="Black" ForeColor="White" Font-Bold="true" runat="server" Text='<%#Eval("Balda") %>' CommandArgument='<%#Eval("Estanteria") + "." + Eval("Modulo") %>' />
                                        <Label>&nbsp</Label>
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
