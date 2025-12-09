<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GestionAlmacenes.aspx.cs" Inherits="ThermoWeb.LOGISTICA.UbicacionMateriales" MasterPageFile="~/SMARTH.Master"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" EnableViewState="true" %>

<asp:Content ID="Cabecera" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Almacén de materiales</title>
    <link rel="shortcut icon" type="image/x-icon" href="../FAVICON.ico" />
</asp:Content>
<asp:Content ID="NavbarAPP" ContentPlaceHolderID="NavbarAPP" runat="server">
    &nbsp- Almacén de materiales            
</asp:Content>
<asp:Content ID="NavbarACCESOS" ContentPlaceHolderID="NavbarACCESOS" runat="server">
    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown2" role="button" data-bs-toggle="dropdown" aria-expanded="false">Consultas
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown2">
                <li><a class="dropdown-item" href="../LIBERACIONES/EstadoLiberacion.aspx">Liberaciones de serie</a></li>
                <li><a class="dropdown-item" href="UbicacionMateriasPrimas.aspx">Ubicaciones materiales</a></li>
            </ul>
        </li>
        <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle active" href="#" id="navbarDropdown3" role="button" data-bs-toggle="dropdown" aria-expanded="false">Instrucciones
            </a>
            <ul class="dropdown-menu dropdown-menu-dark text-small shadow" aria-labelledby="navbarDropdown3">
                <li><a class="dropdown-item" href="../SMARTH_docs/POCS/ITGs-125 -PROCEDIMIENTO MATERIALES Ed.3.pdf">ITGs-125 Procedimiento preparación materiales Ed.3</a></li>
                <li><a class="dropdown-item" href="../SMARTH_docs/POCS/ITGs-141 Instrucción manipulación secado (alimentación manual) Ed.2.pdf">ITGs-141 Manipulación secado alimentación manual Ed.2</a></li>

            </ul>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Cuerpo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript"><%--Configurar onloads--%>
    window.onload = function () {
        document.getElementById("PILL_EST0").onclick = function CargaEST0() {
            document.getElementById("BTN_S0_M1").onclick();
        }
        document.getElementById("PILL_EST1").onclick = function CargaEST1() {
            document.getElementById("BTN_E1_M1").onclick();
        }
        document.getElementById("PILL_EST2").onclick = function CargaEST2() {
            document.getElementById("BTN_E2_M1").onclick();
        }
        document.getElementById("PILL_EST3").onclick = function CargaEST3() {
            document.getElementById("BTN_E3_M1").onclick();
        }
        document.getElementById("PILL_EST4").onclick = function CargaEST4() {
            document.getElementById("BTN_E4_M1").onclick();
        }
        document.getElementById("PILL_EST5").onclick = function CargaEST5() {
            document.getElementById("BTN_E5_M1").onclick();
        }
        document.getElementById("PILL_EST6").onclick = function CargaEST6() {
            document.getElementById("BTN_E6_M1").onclick();
        }
        document.getElementById("PILL_EST7").onclick = function CargaEST7() {
            document.getElementById("BTN_E7_M1").onclick();
        }
        document.getElementById("PILL_EST8").onclick = function CargaEST8() {
            document.getElementById("BTN_E8_M1").onclick();
        }
        document.getElementById("PILL_EST9").onclick = function CargaEST9() {
            document.getElementById("BTN_E9_M1").onclick();
        }
        document.getElementById("PILL_EST10").onclick = function CargaEST10() {
            document.getElementById("BTN_E10_M1").onclick();
        }
        

    }
    </script>
    <script type="text/javascript">
        function ShowPopDocVinculados() {
            document.getElementById("btnPopDocVinculados").click();
        }
        
    </script>
    <style>
        .ps-25 {
            padding-left: 0.75rem !important;
            padding-right: 0.75rem !important;
        }
    </style>
    <div style="background: url(LOGOFONDOTHERMO.png) right top no-repeat">
        <button type="button" id="btnPopDocVinculados" class="btn btn-primary invisible" data-bs-toggle="modal" data-bs-target="#PopDocVinculados"></button>

        <div class="container-fluid">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-1 p-0">
                        <ul class="nav flex-column nav-pills rounded rounded-2 border border-secondary shadow bg-white mt-0" style="width: 100%" id="pills-tab" role="tablist">
                            <h6 class="ms-2 mt-1 " style="font-style: italic"><i class="bi bi-layout-sidebar-inset"></i>&nbsp SILOS</h6>
                            <li class="nav-item ms-1 me-1" role="presentation" id="Li6" runat="server">
                                <button class="nav-link shadow  border border-secondary" runat="server" id="PILL_EST0" data-bs-toggle="pill" data-bs-target="#TAB_EST_0" type="button" role="tab" aria-controls="TAB_EST_1" aria-selected="true" style="font-weight: bold; width: 100%"><i class="bi bi-bookshelf"></i>&nbsp SILOS</button>
                            </li>
                            <h6 class="ms-2 mt-3" style="font-style: italic"><i class="bi bi-layout-sidebar-inset"></i>&nbsp ESTANTERÍAS</h6>
                            <li class="nav-item ms-1 me-1" role="presentation" id="ref0lab" runat="server">
                                <button class="nav-link shadow  border border-secondary active " runat="server" id="PILL_EST1" data-bs-toggle="pill" data-bs-target="#TAB_EST_1" type="button" role="tab" aria-controls="TAB_EST_1" aria-selected="true" style="font-weight: bold; width: 100%"><i class="bi bi-columns"></i>&nbsp E1</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="ref1lab" runat="server">
                                <button class="nav-link  shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST2" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_2" type="button" role="tab" aria-controls="pills-home" aria-selected="true"><i class="bi bi-columns"></i>&nbsp E2</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="ref2lab" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST3" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_3" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E3</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="ref3lab" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST4" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_4" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E4</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="ref4lab" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST5" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_5" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E5</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="Li1" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST6" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_6" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E6</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="Li2" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST7" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_7" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E7</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="Li3" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST8" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_8" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E8</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="Li4" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST9" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_9" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E9</button>
                            </li>
                            <li class="nav-item ms-1 me-1" role="presentation" id="Li5" runat="server">
                                <button class="nav-link shadow  border border-dark" style="font-weight: bold; width: 100%" id="PILL_EST10" runat="server" data-bs-toggle="pill" data-bs-target="#TAB_EST_10" type="button" role="tab" aria-controls="pills-profile" aria-selected="false"><i class="bi bi-columns"></i>&nbsp E10</button>
                            </li>
                        </ul>
                    </div>
                    <div class="col-lg-11 ps-0">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <asp:Image ID="IMGESTANTE" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ESTANTERIA1.png" CssClass="border border-dark rounded rounded-1 shadow" Width="100%" />
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                            <div class="container-fluid shadow">
                                <div class="row" style="text-align: center; background-color: cornflowerblue; border: 1px solid black">
                                    <div class="col-lg-12">
                                        <h2 style="text-shadow: 1px 1px 1px black; color: white" runat="server" id="HeaderEstanteria">ESTANTERÍA 1 - MÓDULO 1</h2>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="tab-content p-0">
                                        <div id="TAB_EST_0" class="tab-pane fade" runat="server">
                                            <div class="col-lg-12 p-0" style="background-color: gainsboro">
                                                <button type="button" id="BTN_S0_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp NAVE 1</button>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_1" class="tab-pane fade show active" runat="server">
                                            <div class="row ps-25">

                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E1_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E1_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E1_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E1_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_2" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E2_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E2_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E2_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E2_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_3" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-12 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E3_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_4" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E4_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E4_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E4_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E4_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E4_M5" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 5</button>
                                                </div>

                                            </div>
                                        </div>
                                        <div id="TAB_EST_5" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E5_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E5_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E5_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E5_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E5_M5" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 5</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_6" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E6_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E6_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E6_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E6_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E6_M5" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 5</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E6_M6" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 6</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_7" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 1</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 2</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 3</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 4</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M5" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 5</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M6" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 6</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M7" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 7</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M8" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 8</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M9" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 9</button>
                                                </div>
                                                <div class="col-lg-1 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M10" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 10</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E7_M11" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓD. 11</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_8" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E8_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E8_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E8_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-2 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E8_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E8_M5" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 5</button>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TAB_EST_9" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E9_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E9_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E9_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E9_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>

                                            </div>
                                        </div>
                                        <div id="TAB_EST_10" class="tab-pane fade" runat="server">
                                            <div class="row ps-25">
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E10_M1" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 1</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E10_M2" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 2</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E10_M3" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 3</button>
                                                </div>
                                                <div class="col-lg-3 p-0" style="background-color: gainsboro">
                                                    <button type="button" id="BTN_E10_M4" runat="server" class="btn btn-outline-dark" style="width: 100%; font-weight: bold" onserverclick="Rellenar_Grid"><i class="bi bi-grid-3x3"></i>&nbsp MÓDULO 4</button>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="AltSilos">
                                    <div class="col-lg-12 p-0 border border-dark">
                                        <asp:GridView ID="GridSilos" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                            <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                            <RowStyle BackColor="White" />
                                            <AlternatingRowStyle BackColor="#ccccff" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large" ItemStyle-BackColor="CornflowerBlue" ItemStyle-ForeColor="White">
                                                    <HeaderTemplate>
                                                        <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square"></i></label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSilo" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("NombreUbicacion")  %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                    <ItemTemplate>
                                                        <div class="btn-group me-2">
                                                            <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("NombreUbicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("NombreUbicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                        <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                        <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                        <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticuloSILO" CommandArgument='<%#Eval("NombreUbicacion")  %>' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="AltE">
                                    <div class="col-lg-1" style="background-color: cornflowerblue; border: 1px solid black">
                                        <h1 class="text-white" style="text-shadow: 1px 1px 1px black">E</h1>
                                    </div>
                                    <div class="col-lg-11">
                                        <div class="row" style="background-color: gainsboro">
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_E1">
                                                <asp:Image ID="IMGVCE1" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridE1" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A1</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument="E1" UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_E2">
                                                <asp:Image ID="IMGVCE2" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridE2" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A2</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='E2' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_E3">
                                                <asp:Image ID="IMGVCE3" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridE3" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='E3' CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_E4">
                                                <asp:Image ID="IMGVCE4" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridE4" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='E4' CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="AltD">
                                    <div class="col-lg-1" style="background-color: cornflowerblue; border: 1px solid black">
                                        <h1 class="text-white" style="text-shadow: 1px 1px 1px black">D</h1>
                                    </div>
                                    <div class="col-lg-11">
                                        <div class="row" style="background-color: gainsboro">
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_D1">
                                                <asp:Image ID="IMGVCD1" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridD1" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.D1</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='D1' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_D2">
                                                <asp:Image ID="IMGVCD2" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridD2" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.D2</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='D2' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_D3">
                                                <asp:Image ID="IMGVCD3" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridD3" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.D3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='D3' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_D4">
                                                <asp:Image ID="IMGVCD4" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridD4" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='D4' CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="AltC">
                                    <div class="col-lg-1" style="background-color: cornflowerblue; border: 1px solid black">
                                        <h1 class="text-white" style="text-shadow: 1px 1px 1px black">C</h1>
                                    </div>
                                    <div class="col-lg-11">
                                        <div class="row" style="background-color: gainsboro">
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_C1">
                                                <asp:Image ID="IMGVCC1" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridC1" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.C1</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='C1' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_C2">
                                                <asp:Image ID="IMGVCC2" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridC2" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.C2</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='C2' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_C3">
                                                <asp:Image ID="IMGVCC3" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridC3" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.C3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='C3' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_C4">
                                                <asp:Image ID="IMGVCC4" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridC4" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='C4' CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="AltB">
                                    <div class="col-lg-1" style="background-color: cornflowerblue; border: 1px solid black">
                                        <h1 class="text-white" style="text-shadow: 1px 1px 1px black">B</h1>
                                    </div>
                                    <div class="col-lg-11">
                                        <div class="row" style="background-color: gainsboro">
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_B1">
                                                <asp:Image ID="IMGVCB1" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridB1" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.B1</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='B1' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_B2">
                                                <asp:Image ID="IMGVCB2" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridB2" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.B2</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='B2' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_B3">
                                                <asp:Image ID="IMGVCB3" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridB3" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.B3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='B3' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_B4">
                                                <asp:Image ID="IMGVCB4" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridB4" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='B4' CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="AltA">
                                    <div class="col-lg-1" style="background-color: cornflowerblue; border: 1px solid black">
                                        <h1 class="text-white" style="text-shadow: 1px 1px 1px black">A</h1>
                                    </div>
                                    <div class="col-lg-11">
                                        <div class="row" style="background-color: gainsboro">
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_A1">
                                                <asp:Image ID="IMGVCA1" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridA1" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A1</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='A1' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_A2">
                                                <asp:Image ID="IMGVCA2" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridA2" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A2</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='A2' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_A3">
                                                <asp:Image ID="IMGVCA3" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridA3" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='A3' UseSubmitBehavior="true" CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-3 border border-dark p-0" runat="server" id="COL_A4">
                                                <asp:Image ID="IMGVCA4" runat="server" ImageUrl="..\SMARTH_fonts\INTERNOS\ubivacia.png" CssClass="border border-dark border-start-0 border-end-0" Width="100%" />
                                                <asp:GridView ID="GridA4" runat="server" AllowSorting="True" Style="width: 100%" CssClass="table border border-dark border-start-0 border-end-0 mb-0" AutoGenerateColumns="false" ShowHeader="true" OnRowDataBound="GridView_RowDataBound" OnRowCommand="GridCommandEventHandler">
                                                    <HeaderStyle BackColor="DarkOrange" Font-Bold="True" Font-Size="X-Large" ForeColor="White" />
                                                    <RowStyle BackColor="White" />
                                                    <AlternatingRowStyle BackColor="#ccccff" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="Large">
                                                            <HeaderTemplate>
                                                                <label style="text-shadow: 1px 1px 1px black"><i class="bi bi-arrow-down-square">&nbsp M1.A3</i></label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="btn-group me-2">
                                                                    <asp:LinkButton runat="server" ID="BTNAuditaUbicacion" CommandName="ValidaArticulo" CommandArgument='<%#Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-success border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-check-lg"></i>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="BTNEliminaArticulo" CommandName="EliminaArticulo" CommandArgument='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") + "¬" + Eval("Articulo") + "¬" + Eval("Ubicacion") %>' UseSubmitBehavior="true" CssClass="btn  btn-danger border border-dark shadow" Style="font-size: 1rem">
                                                            <i class="bi bi-x"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <asp:Label ID="lblArticuloText" Font-Italic="true" Font-Bold="true" runat="server" Text='<%#Eval("Articulo") + " " + Eval("Descripcion")  %>' />
                                                                <asp:Label ID="lblFechaEntrada" Font-Size="Smaller" Visible="false" runat="server" Font-Italic="true" Text='<%#"<br />(Desde el " + Eval("Fechaentrada","{0:dd/MM/yyyy}") + ")" %>' />
                                                                <asp:Label ID="lblAuxFechaEntrada" Visible="false" Font-Size="Smaller" runat="server" Font-Italic="true" Text='<%#Eval("Fechaentrada","{0:dd/MM/yyyy}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%">
                                                            <HeaderTemplate>
                                                                <asp:LinkButton runat="server" ID="BTNAgregaLinea" CommandName="AgregaArticulo" CommandArgument='A4' CssClass="btn btn-light border border-dark shadow float-end" Style="font-size: 1rem">
                                                            <i class="bi bi-box-arrow-in-right"></i>
                                                                </asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="PopDocVinculados" tabindex="-1" data-bs-backdrop="static" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header bg-primary border border-dark shadow">
                            <h4 class="modal-title text-white" style="text-shadow: 1px 1px 2px black"><i class="bi bi-box-arrow-in-right">&nbsp Añadir producto a ubicación</i></h4>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body border border-dark border-top-0">
                            <div class="row">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <asp:Image runat="server" ID="IMGAuxCamara" CssClass="border border-dark rounded rounded-2 mb-2" ImageUrl="..\SMARTH_fonts\INTERNOS\notcamera.png" Width="100%" />
                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                            <div class="row">
                                <h2 runat="server" id="AuxTBUbicacion"></h2>
                                <input class="form-control border border-dark shadow" list="DatalistNUMMaterial" id="NUMMaterial" runat="server" placeholder="Escribe un producto..." autocomplete="off">
                                <datalist id="DatalistNUMMaterial" runat="server">
                                </datalist>
                            </div>
                        </div>
                        <button type="button" class="btn btn-success btn-lg border border-dark border-top-0 shadow shadow-sm" style="border-top-left-radius: 0; border-top-right-radius: 0; text-shadow: 1px 1px 2px black" runat="server" onserverclick="AgregarMaterialXUbicacion">Agregar a la ubicación</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
















