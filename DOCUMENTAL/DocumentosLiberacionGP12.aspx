<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DocumentosLiberacionGP12.aspx.cs" Inherits="ThermoWeb.DOCUMENTAL.DocumentosLiberacionGP12" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Defectos localizados muro de calidad</title>
    <link rel="shortcut icon" type="image/x-icon" href="FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Defectos localizados en GP12
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row mt-2">
            <ul class="nav nav-pills mb-2 nav-fill  " id="pills-tab" role="tablist">
                <li class="nav-item " role="presentation">
                    <button class="nav-link shadow active " id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">DETECCIONES: MURO DE CALIDAD</button>
                </li>
                <li class="nav-item" role="presentation">
                    <button class="nav-link shadow " id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">ESTÁNDAR: DEFECTOLOGÍA</button>
                </li>
            </ul>
            <div class="tab-content shadow" id="pills-tabContent">
                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                    <div id="carouselExampleControls" class="carousel slide shadow" data-bs-ride="carousel">
                        <div class="carousel-inner" id="ACTIVOS" runat="server">
                            <div class="carousel-item active">
                                <img src="http://facts4-srv/thermogestion/DOCUMENTAL/DOCUMENTOS/LAYOUTESTANDAR.png" class="d-block w-100" style="width: 100%;">
                            </div>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                </div>
                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <div class="ratio ratio-16x9 shadow" style="width:100%">
                        <iframe id="DEFECTOS" runat="server" src="http://facts4-srv/thermogestion/SMARTH_docs/SINDOCUMENTO.png" allowfullscreen></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>




