<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTH.Master" AutoEventWireup="true"  CodeBehind="index.aspx.cs" Inherits="ThermoWeb.index" %>
<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>SmarTH</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Índice
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
   
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Navigation -->

    <div class="container">
        <div class="row mt-2 mb-2">
            <div class="col-md-12">
                <div class="shadow h-100 p-3 bg-light border rounded-1 border-secondary">
                    <img id="img1" class="ms-4" src="imagenes/logo.png" alt="Thermolympic" runat="server" align="left" vspace="4" />
                    <h1 class="display-5" style="font: bold">SmarTH</h1>
                    <p class="lead ms-1" >Gestión de procesos y documentación en Thermolympic S.L.</p>
                </div>
            </div>
        </div>
        <div class="row row-cols-1 row-cols-md-4 g-4 mb-2">
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png); border-color:dimgray">

                    <asp:HyperLink ID="hyperlink1" class="card-img-top" NavigateUrl="~/CALIDAD/IndiceCalidad.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGOAreaCalidad.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Área de calidad</h5>
                        <p class="card-text">Gestión de no conformidades, muro de calidad e inspecciones.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink2" class="card-img-top" NavigateUrl="KPI/KPIIndice.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGOCuadroMando.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Cuadros de mando</h5>
                        <p class="card-text">Resultados de las distintas aplicaciones.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink3" class="card-img-top" NavigateUrl="DOCUMENTAL/AccesoDocumentalMaquina.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGODocumentacion.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Documentos en línea</h5>
                        <p class="card-text">Documentos en línea y gestión de los mismos.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink4" class="card-img-top" NavigateUrl="LIBERACIONES/EstadoLiberacion.aspx" ImageUrl= "SMARTH_fonts/INTERNOS/LOGOLiberadook.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Liberación de serie</h5>
                        <p class="card-text">Liberación de producciones en Thermolympic.</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row row-cols-1 row-cols-md-4 g-4 mb-4">
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink7" class="card-img-top" NavigateUrl="MANTENIMIENTO/MantenimientoIndex.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGOMantenimiento.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Mantenimiento</h5>
                        <p class="card-text">Gestión del mantenimiento de moldes y máquinas.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink8" class="card-img-top" NavigateUrl="MATERIALES/IndiceMateriales.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGOMOVIALMACEN.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Materiales y logística</h5>
                        <p class="card-text">Previsión de estufado, movimientos de almacenes y gestión de molidos.</p>
                    </div>
                </div>
            </div>
     
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink5" class="card-img-top" NavigateUrl="~/PDCA/PDCA_Indice.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGOPDCACCION.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Planes de acción</h5>
                        <p class="card-text">Acciones abiertas en gestión operativa de planta.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(imagenes/unnamed.png)">
                    <asp:HyperLink ID="hyperlink9" class="card-img-top" NavigateUrl="PRODUCCION/IndiceProduccion.aspx" ImageUrl="SMARTH_fonts/INTERNOS/LOGOProduccion.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Producción</h5>
                        <p class="card-text">Fichas de parámetros y gestión de moldes</p>
                    </div>
                </div>
            </div>
        </div>


    </div>

    <!-- Footer -->
</asp:Content>
