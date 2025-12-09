<%@ Page Title="" Language="C#" MasterPageFile="~/SMARTH.Master" AutoEventWireup="true"  CodeBehind="IndiceProduccion.aspx.cs" Inherits="ThermoWeb.PRODUCCION.IndiceProduccion" %>
<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>SmarTH</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Accesos de produccion
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
                    <h1 class="display-5" style="font: bold">SmarTH</h1>
                    <p class="lead ms-1" >Gestión de procesos y documentación en Thermolympic S.L.</p>
                </div>
            </div>
        </div>
        <div class="row row-cols-1 row-cols-md-4 g-4 mb-2">
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png); border-color:dimgray">
                    <asp:HyperLink ID="hyperlink4" class="card-img-top" NavigateUrl="MontajesExternos.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/APPMontaje.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Montajes</h5>
                        <p class="card-text">Imputación de montajes fuera de flujo productivo.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png); border-color:dimgray">
                    <asp:HyperLink ID="hyperlink1" class="card-img-top" NavigateUrl="Listado_Manos_Robot.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOROBOT.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Manos de robot</h5>
                        <p class="card-text">Gestión de manos y asignación a moldes.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png)">
                    <asp:HyperLink ID="hyperlink5" class="card-img-top" NavigateUrl="FichasParametros.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOParametros.jpg" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Parámetros</h5>
                        <p class="card-text">Fichas de parámetros de las máquinas.</p>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png); border-color:dimgray">
                    <asp:HyperLink ID="hyperlink6" class="card-img-top" NavigateUrl="IoTRefrigeracion.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LogoIOTemp.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Refrigeración</h5>
                        <p class="card-text">Detalles de refrigeración y caudales en líneas de máquina, moldes.</p>
                    </div>
                </div>
            </div>
           
            
            
        </div>
         <div class="row row-cols-1 row-cols-md-4 g-4 mb-2">

           <div class="col">
                <div class="card shadow h-100 border-dark border-1" style="background-image: url(../SMARTH_fonts/INTERNOS/unnamed.png); border-color:dimgray">
                    <asp:HyperLink ID="hyperlink8" class="card-img-top" NavigateUrl="Gestion_Ubicaciones_Moldes.aspx" ImageUrl="../SMARTH_fonts/INTERNOS/LOGOUbicacionesMoldes.png" runat="server" ImageWidth="100%" />
                    <div class="card-body">
                        <h5 class="card-title">Ubicaciones de moldes</h5>
                        <p class="card-text">Gestionar ubicaciones de planta y externos (moldes).</p>
                    </div>
                </div>
            </div>
            
            
        </div>
    </div>
</asp:Content>
