<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DashboardVisitas.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.DashboardVisitas" MasterPageFile="~/SMARTHLite.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Dashboard Thermolympic</title>
    <link rel="shortcut icon" type="image/x-icon" href="ICONOS/FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Dashboard Thermolympic            
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Gestión Documental
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown">
                <li><a class="dropdown-item" href="FichaReferencia.aspx">Documentación de referencia</a></li>
                <li><a class="dropdown-item" href="GestionDocumentalPendientes.aspx">Produciendo sin digitalizar</a></li>
                <li><a class="dropdown-item" href="AccesoDocumentalMaquina.aspx">Tablero de máquinas</a></li>
            </ul>
        </li>
    </ul>
    
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="text-align:center" >
        <asp:Image ID="IMGVisita" runat="server" ImageUrl="../SMARTH_docs/AUDIOVISUAL/VISITALOGOTHERMO.png" Height="95%" />
    </div>
</asp:Content>
