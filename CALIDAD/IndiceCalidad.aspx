<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTH.Master" AutoEventWireup="true"  CodeBehind="IndiceCalidad.aspx.cs" Inherits="ThermoWeb.CALIDAD.IndiceCalidad" %>
<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>SmarTH</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Accesos de calidad
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Navigation -->
     <div class="container">
        <div class="row mt-2 mb-2">
            <div class="col-md-12">
                <div class="shadow h-100 p-3 bg-light border rounded-1 border-secondary">
                    <img id="img1" class="ms-4" src="../imagenes/logo.png" alt="Thermolympic" runat="server" align="left" vspace="4" />
                    <h1 class="display-5" style="font: bold">SmarTH - Área de calidad</h1>
                    <p class="lead ms-1" >Gestión de procesos y documentación en Thermolympic S.L.</p>
                </div>
            </div>
        </div>
        <div class="row row-cols-1 row-cols-md-4 g-4 mb-2">
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png); border-color:dimgray">
                    <asp:HyperLink ID="hyperlink4" class="card-img-top" NavigateUrl="../AREA_RECHAZO/Area_Rechazo.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOAreaRechazo.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Área de rechazo</h5>
                        <p class="card-text">Control de piezas rechazadas por calidad.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png); border-color:dimgray">
                    <asp:HyperLink ID="hyperlink3" class="card-img-top" NavigateUrl="EtiquetasCalidad.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOEstadoControl.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Etiquetas de control</h5>
                        <p class="card-text">Impresión de etiquetas de producto retenido o pendiente de procesar.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink2" class="card-img-top" NavigateUrl="Metrologia.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/APPMetrologia.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Metrología</h5>
                        <p class="card-text">Equipos de medición y utillajes de control.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink5" class="card-img-top" NavigateUrl="../GP12/GP12.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOGP12.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Muro de calidad</h5>
                        <p class="card-text">Seguimiento y gestión del muro de calidad.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink7" class="card-img-top" NavigateUrl="../CALIDAD/ListaAlertasCalidad.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOPDCA.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">No conformidades</h5>
                        <p class="card-text">Gestión de alertas de calidad y desviaciones.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink1" class="card-img-top" NavigateUrl="http://thermobms/QStation#/InspectionOrders" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOQMaster.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Q-Master</h5>
                        <p class="card-text">Acceso a la plataforma de inspección de BMSVision.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
