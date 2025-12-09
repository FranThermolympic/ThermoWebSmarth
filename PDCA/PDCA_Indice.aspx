<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTH.Master" AutoEventWireup="true" CodeBehind="PDCA_Indice.aspx.cs" Inherits="ThermoWeb.PDCA.Indice" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>SmarTH</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Índice de planes de acción
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Navigation -->

    <div class="container">

        <div class="row row-cols-1 row-cols-md-4 g-4 mb-2 mt-1">
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png); border-color: dimgray">
                    <asp:HyperLink ID="hyperlink1" class="card-img-top" NavigateUrl="~/PDCA/PDCA_DETALLE.aspx?IDPDCA=2" ImageUrl="../SMARTH_fonts/INTERNOS/PDCAAreaGOP.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Acciones G.O.P.
                            <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgeGOP">0</span>
                        </h5>
                        <p class="card-text">Acciones de gestión operativa de planta.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink4" class="card-img-top" NavigateUrl="~/PDCA/PDCA_DETALLE.aspx?IDPDCA=6" ImageUrl="../SMARTH_fonts/INTERNOS/PDCAAuditoria.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Auditorías
                            <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgeAuditorias">6</span>                                          
                        </h5>
                        <p class="card-text">Acciones de auditorías por capas, proceso, producto y sistema</p>
                    </div>
                </div>
            </div>          
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink3" class="card-img-top" NavigateUrl="~/PDCA/PDCA_DETALLE.aspx?IDPDCA=3" ImageUrl="../SMARTH_fonts/INTERNOS/PDCANormativa.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Procesos y normativa
                            <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" runat="server" id="BadgeProcesosnormativa">3</span>                     
                        </h5>
                        <p class="card-text">Acciones vinculadas a seguridad y procedimientos.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink2" class="card-img-top" NavigateUrl="~/PDCA/PDCA.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/PDCAIconoPlanAccion.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Planes de acción</h5>
                        <p class="card-text">Listado general de planes de acción.</p>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="row row-cols-1 row-cols-md-4 g-4 mb-4" >
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink7" class="card-img-top" NavigateUrl="~/PDCA/Listado_Acciones.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGO_Lecciones_aprendidas.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Histórico de acciones</h5>
                        <p class="card-text">Resumen de acciones y lecciones aprendidas.</p>
                    </div>
                </div>
            </div>
           
        </div>


    </div>

    <!-- Footer -->
</asp:Content>
